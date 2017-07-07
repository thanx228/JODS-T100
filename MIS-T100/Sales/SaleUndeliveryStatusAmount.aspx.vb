Imports System.Globalization
Imports System.Drawing
Public Class SaleUndeliveryStatusAmount
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempSOUndeliveryAmount As New TempSOUndeliveryAmount
    Dim connect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")
                TempSOUndeliveryAmount.TempSOUndeliveryAmount(UserID)
            End If
            HeaderFormT1001.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        Scollbar()
    End Sub

    '--'Save To Excel File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    '--ShowScollbar สกอบาร์
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvUndeliveryStusAmountScrollbar", "GvUndeliveryStusAmountScrollbar();", True)
    End Sub

    '--Export Excel
    Protected Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        ContDtFormOrl.ExportGridViewToExcel("SOUndelivery", GvUndeliveryStusAmount)
    End Sub

    '--Search
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '--ShowGvReportHead
        ShowGvUndeliveryStusAmount(GvUndeliveryStusAmount, True, NowPageIndex.Text)
        CountRow1.RowCount = ContDtFormOrl.RowGridview(GvUndeliveryStusAmount)
    End Sub

    '--ShowGvUndeliveryStusAmount
    Private Sub ShowGvUndeliveryStusAmount(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        Dim Whr As String = "",
            CustNo As String = txtCustID.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            Ver As String = txtVersion.Text.Trim,
            Item As String = txtItemNo.Text.Trim,
            Spac As String = txtSpec.Text.Trim,
            Condition As String = ddlCondition.SelectedValue,
            DateFrom As String = DateT1001.Text,
            DateTo As String = DateT1002.Text,
            SOTypeCheckList As String = SelectCheckBoxList.MultipleSelect(UsingTypeSaleCheckList1.getObject),
            SOTypeRows As Integer = SelectCheckBoxList.RowNumSelect

        If SOTypeRows > 0 Then
            Whr = Whr & " and substr(" & XMDA.SaleOrderNo & ",3,4)" & " in(" & [String].Join("','", SOTypeCheckList) & "')"
        End If
        If CustNo <> "" Then
            Whr = Whr & " and " & XMDA.CustomerId & "='" & CustNo & "'"
        End If
        If SONo <> "" And Ver <> "" Then
            Whr = Whr & " and " & XMDA.SaleOrderNo & "= '" & SONo & "' and " & XMDA.VersionNo & "= '" & Ver & "'"
        End If
        If Item <> "" And Spac <> "" Then
            Whr = Whr & " and " & XMDC.Item & "= '" & Item & "' and " & IMAAL.Specifaction & "= '" & Spac & "'"
        End If
        If DateFrom <> "" And DateTo <> "" Then
            Whr = Whr & " and " & XMDC.BookShippingDate & " BETWEEN TO_DATE ('" & DateFrom & "', 'yyyy/mm/dd') and TO_DATE ('" & DateTo & "', 'yyyy/mm/dd')"
        End If

        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "ItemNo,Desc,Spec,UndeliveryQty,UndelQtyAmount,MOQty,POQty,PRQty,StockInOut,StockAmount"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If
        '--GetWhrUndelStusAmt
        XMDA.GetWhrUndelStusAmt(Whr, dt1)
        If dt1.Rows.Count > 0 Then
            For j As Integer = 0 To dt1.Rows.Count - 1
                Dim GroupItemNo As String = "", GroupItemName As String = "",
                    GroupItemSpec As String = "", GroupUnit As String = "",
                    GroupSOPrice As String = "", GroupExchangeReate As String = ""
                If dt1.Rows.Count > 0 Then
                    GroupItemNo = dt1.Rows(j).Item("xmdc001")
                    GroupItemName = dt1.Rows(j).Item("imaal003")
                    GroupItemSpec = dt1.Rows(j).Item("imaal004")
                    GroupUnit = dt1.Rows(j).Item("xmdc006")
                    GroupSOPrice = dt1.Rows(j).Item("Price")
                    GroupExchangeReate = dt1.Rows(j).Item("xmda016")
                End If
                '--Sum SO RequestQty
                dt2 = XMDC.SumSOReqQty(GroupItemNo) '--1
                Dim SOReqQtyItemNo As String = "", SOReqQty As Integer = 0
                If dt2.Rows.Count > 0 Then
                    SOReqQtyItemNo = dt2.Rows(0).Item("xmdc001") 'Key
                    SOReqQty = dt2.Rows(0).Item("SOReqQty") '*
                End If
                '--Sum DeliveryQty where ItemNo
                dt3 = XMDC.SumDeliQty(SOReqQtyItemNo) '--2
                Dim DeliQty As Integer = 0
                If dt3.Rows.Count > 0 Then
                    DeliQty = dt3.Rows(0).Item("DeliveryQty") '*
                End If
                '--Sum SampleReqQty
                dt4 = XMDC.SumSampleReqQty(SOReqQtyItemNo) '--3
                Dim ReqQtySample As Integer = 0
                If dt4.Rows.Count > 0 Then
                    ReqQtySample = dt4.Rows(0).Item("SampleReqQty") '*
                End If
                '--Sum SampleDeliveryQty
                dt5 = XMDC.SumSampleDeliveryQty(SOReqQtyItemNo) '--4
                Dim sampledelQty As Integer = 0
                If dt5.Rows.Count > 0 Then
                    sampledelQty = dt5.Rows(0).Item("sampleDeliQty") '*
                End If
                '--Sum UnDeliveryQty
                Dim SumUndelQty As Integer = 0,
                    UndelQty As String = "0"
                SumUndelQty = (SOReqQty + ReqQtySample - DeliQty - sampledelQty)
                If SumUndelQty > 0 Then
                    UndelQty = SumUndelQty
                    Dim Comma As Decimal = CDec(UndelQty)
                    UndelQty = String.Format("{0:n0}", Comma)
                End If
                '-- Sum UnDeliveryQty Amt
                Dim SumUnDelQtyAmt As Integer = 0,
                    UnDelQtyAmt As String = "0"
                SumUnDelQtyAmt = UndelQty * GroupSOPrice * GroupExchangeReate
                If SumUnDelQtyAmt > 0 Then
                    UnDelQtyAmt = SumUnDelQtyAmt
                    Dim Comma As Decimal = CDec(UnDelQtyAmt)
                    UnDelQtyAmt = String.Format("{0:n2}", Comma)
                    If UnDelQtyAmt = String.Empty Then
                        UnDelQtyAmt = 0
                    End If
                End If
                '--Sum MO  where Item
                dt6 = SFAA.SumMO(SOReqQtyItemNo)
                Dim MOQty As String = "0"
                If dt6.Rows.Count > 0 Then
                    MOQty = dt6.Rows(0).Item("MOQty")
                    Dim Comma As Decimal = CDec(MOQty)
                    MOQty = String.Format("{0:n0}", Comma)
                End If
                '--Sum POQty
                dt7 = PMDN.SumPOQty(SOReqQtyItemNo)
                Dim POQty As String = "0"
                If dt7.Rows.Count > 0 Then
                    POQty = dt7.Rows(0).Item("POQty")
                    Dim Comma As Decimal = CDec(POQty)
                    POQty = String.Format("{0:n0}", Comma)
                End If
                '-- Sum StockInOut  where Item EndDueDate
                Dim EndDueDate As String = ""
                EndDueDate = "TO_DATE ('" & DateTo & "', 'yyyy/mm/dd')"
                dt8 = INAJ.SumStockInOut(SOReqQtyItemNo, EndDueDate)
                Dim StockInOut As String = ""
                If dt8.Rows.Count > 0 Then
                    StockInOut = dt8.Rows(0).Item("StockInOut") '*
                Else
                    If StockInOut = String.Empty Then
                        StockInOut = 0
                    End If
                End If
                '-- Sum Stock Amount  where Item Wh
                Dim Wh As String = ""
                If SOTypeRows > 0 Then
                    Wh = Wh & " and substr(" & XMDA.SaleOrderNo & ",3,4)" & " in(" & [String].Join("','", SOTypeCheckList) & "')"
                End If
                If CustNo <> "" Then
                    Wh = Wh & " and " & XMDA.CustomerId & "='" & CustNo & "'"
                End If
                If SONo <> "" And Ver <> "" Then
                    Wh = Wh & " and " & XMDA.SaleOrderNo & "= '" & SONo & "' and " & XMDA.VersionNo & "= '" & Ver & "'"
                End If
                If Item <> "" And Spac <> "" Then
                    Wh = Wh & " and " & XMDC.Item & "= '" & Item & "' and " & IMAAL.Specifaction & "= '" & Spac & "'"
                End If
                If DateFrom <> "" And DateTo <> "" Then
                    Wh = Wh & " and " & XMDC.BookShippingDate & " <= TO_DATE ('" & DateTo & "', 'yyyy/mm/dd')"
                End If
                dt9 = XMDA.SumStockAmount(SOReqQtyItemNo, Wh, StockInOut)
                Dim StockAmount As String = "0"
                If dt9.Rows.Count > 0 Then
                    StockAmount = dt9.Rows(0).Item("StockAmount")
                    Dim Comma As Decimal = CDec(StockAmount)
                    StockAmount = String.Format("{0:n2}", Comma)
                End If

                '--Insert Detail
                TempSOUndeliveryAmount.InserUndelAmt(SOReqQtyItemNo, GroupItemName, GroupItemSpec, GroupUnit, SOReqQty, ReqQtySample, DeliQty, sampledelQty, UndelQty, UnDelQtyAmt, MOQty, POQty, StockInOut, StockAmount, UserID)
            Next
        Else
            If dt1.Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        Whr = ""
        If Condition <> "0" Then
            Dim stock As String = "stockQty"
            Dim supply As String = stock & "+moQty+poQty+prQty"
            Dim undel As String = "UndelQty"
            Select Case Condition
                Case "1" ' stock >= undelivery
                    Whr = Whr & " and " & stock & "<" & undel
                Case "2" ' supply >= undelivery
                    Whr = Whr & " and " & supply & ">=" & undel
                Case "3" ' supply < undelivery
                    Whr = Whr & " and " & supply & "<" & undel
                Case "4" ' supply < undelivery
                    Whr = Whr & " and " & stock & ">=" & undel
            End Select
        End If

        '--ShowDetail into Gridview Where UserID item
        TempSOUndeliveryAmount.ShowDetail(UserID, Whr, dt10)
        For e As Integer = 0 To dt10.Rows.Count - 1
            Dim Showitem As String = "", ShowDesc As String = "",
            ShowSpec As String = "", ShowCustID As String = "",
            ShowCustName As String = "", ShowUndelQty As String = "",
            ShowMOQty As String = "", ShowPOQty As String = "",
            ShowPRQty As String = "", ShowStockQty As String = "",
            ShowStockAmount As String = "", ShowUndelQtyAmount As String = ""
            If dt10.Rows.Count > 0 Then
                Showitem = dt10.Rows(e).Item("item")
                ShowDesc = dt10.Rows(e).Item("ItemDesc")
                ShowSpec = dt10.Rows(e).Item("Spec")
                ShowUndelQty = dt10.Rows(e).Item("UndelQty")
                ShowUndelQtyAmount = dt10.Rows(e).Item("delAmt")
                ShowMOQty = dt10.Rows(e).Item("moQty")
                ShowPOQty = dt10.Rows(e).Item("poQty")
                ShowPRQty = dt10.Rows(e).Item("prQty")
                ShowStockQty = dt10.Rows(e).Item("stockQty")
                ShowStockAmount = dt10.Rows(e).Item("stockAmt")
                DtShow.Rows.Add(New Object() {Showitem, ShowDesc, ShowSpec, ShowUndelQty, ShowUndelQtyAmount, ShowMOQty, ShowPOQty, ShowPRQty, ShowStockQty, ShowStockAmount})
            End If
        Next

        '##--delete from TempSOUnDeliveryAmount Where UserID
        TempSOUndeliveryAmount.DeleteDetailTempSOUnDeliveryAmount(UserID)

        'เอาเข้า Gridview 
        GvUndeliveryStusAmount.DataSource = DtShow
        GvUndeliveryStusAmount.DataBind()

        Dim a As Integer = 0
        a = GvUndeliveryStusAmount.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvUndeliveryStusAmount, "#FFCC99")
        End If
    End Sub

    '--HyperLink GvUndeliveryStusAmount 
    Protected Sub GvUndeliveryStusAmount_RowDataBound1(sender As Object, e As GridViewRowEventArgs) Handles GvUndeliveryStusAmount.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                Dim item As String = .DataItem("ItemNo")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("ItemNo")) Then
                    Dim link As String = ""
                    link = link & "&DateFrom=" & DateT1001.Text
                    link = link & "&DateTo=" & DateT1002.Text
                    link = link & "&item=" & .DataItem("ItemNo")
                    link = link & "&Spec=" & .DataItem("Spec")
                    link = link & "&UndelQty=" & .DataItem("UndeliveryQty")
                    link = link & "&moQty=" & .DataItem("MOQty")
                    link = link & "&poQty=" & .DataItem("POQty")
                    link = link & "&StockInOut=" & .DataItem("StockInOut")
                    hplDetail.NavigateUrl = "SLUndelStusAmountPopUp.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                    hplDetail.Target = "_blank"
                End If
            End If
        End With
    End Sub
End Class