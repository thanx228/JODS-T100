Imports System.Globalization
Imports System.Drawing
Public Class SaleUndeliveryStatus
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempSOUnDelivery As New TempSOUndelivery
    Dim connect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")
                TempSOUnDelivery.createTempSOUnDelivery(UserID)
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
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gvUndeliveryScrollbar", "gvUndeliveryScrollbar();", True)
    End Sub

    '--Export Excel
    Protected Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        ContDtFormOrl.ExportGridViewToExcel("SOUndelivery", gvUndelivery)
    End Sub

    '--Search
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ShowgvUndelivery(gvUndelivery, True, NowPageIndex.Text)
        CountRow1.RowCount = ContDtFormOrl.RowGridview(gvUndelivery)
    End Sub

    '--ShowgvUndelivery
    Private Sub ShowgvUndelivery(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        Dim Whr As String = "", CustNo As String = txtCustID.Text.Trim,
            SONo As String = txtSONo.Text.Trim, Ver As String = txtVersion.Text.Trim,
            Item As String = txtItemNo.Text.Trim, Spac As String = txtSpec.Text.Trim,
            Condition As String = ddlCondition.SelectedValue, DateFrom As String = DateT1001.Text,
            DateTo As String = DateT1002.Text,
            SOTypeCheckList As String = SelectCheckBoxList.MultipleSelect(UsingTypeSaleCheckList1.getObject),
            SOTypeRows As Integer = SelectCheckBoxList.RowNumSelect,
            Classification As String = SelectCheckBoxList.Multiple(UsingProductClassification1.getObject),
            IndustryRows As Integer = SelectCheckBoxList.RowNum

        If (IndustryRows > 0) And (SOTypeRows <= 0) Then
            Whr = Whr & " and " & IMAA.ProductClassification & " in(" & [String].Join("','", Classification) & "')"
        ElseIf (SOTypeRows > 0) And (IndustryRows <= 0) Then
            Whr = Whr & " and substr(" & XMDA.SaleOrderNo & ",3,4)" & " in(" & [String].Join("','", SOTypeCheckList) & "')"
        ElseIf (IndustryRows > 0) And (SOTypeRows > 0) Then
            Whr = Whr & " and " & IMAA.ProductClassification & " in(" & [String].Join("','", Classification) & "')"
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
        Dim ShowFiled As String = "CustID,ItemNo,Desc,Spec,UndeliveryQty,Unit,MOQty,POQty,PRQty,StockQty,Industry"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If
        '--GetWhrUndelStus
        XMDA.GetWhrUndelStus(Whr, dt)
        If dt.Rows.Count > 0 Then
            Dim ShearchCustid As String = "", ShearchItemNo As String = "",
                ShearchItemName As String = "", ShearchItemSpec As String = "",
                ShearchUnit As String = "", ShearchClssfctDesc As String = ""
            For j As Integer = 0 To dt.Rows.Count - 1
                ShearchCustid = dt.Rows(j).Item("xmda004")
                ShearchItemNo = dt.Rows(j).Item("xmdc001")
                ShearchItemName = dt.Rows(j).Item("imaal003")
                ShearchItemSpec = dt.Rows(j).Item("imaal004")
                ShearchUnit = dt.Rows(j).Item("xmdc006")
                ShearchClssfctDesc = dt.Rows(j).Item("rtaxl003")

                '--Sum SO RequestQty
                dt1 = XMDC.SumSOReqQty(ShearchItemNo) '--1
                Dim SOReqQtyItemNo As String = "", SOReqQty As Integer = 0
                If dt1.Rows.Count > 0 Then
                    SOReqQtyItemNo = dt1.Rows(0).Item("xmdc001") 'Key
                    SOReqQty = dt1.Rows(0).Item("SOReqQty") '*
                End If
                '--Sum DeliveryQty where ItemNo
                dt2 = XMDC.SumDeliQty(SOReqQtyItemNo) '--2
                Dim DeliQty As Integer = 0
                If dt2.Rows.Count > 0 Then
                    DeliQty = dt2.Rows(0).Item("DeliveryQty") '*
                End If
                '--Sum SampleReqQty
                dt3 = XMDC.SumSampleReqQty(SOReqQtyItemNo) '--3
                Dim ReqQtySample As Integer = 0
                If dt3.Rows.Count > 0 Then
                    ReqQtySample = dt3.Rows(0).Item("SampleReqQty") '*
                End If
                '--Sum SampleDeliveryQty
                dt4 = XMDC.SumSampleDeliveryQty(SOReqQtyItemNo) '--4
                Dim sampledelQty As Integer = 0
                If dt4.Rows.Count > 0 Then
                    sampledelQty = dt4.Rows(0).Item("sampleDeliQty") '*
                End If
                '--Sum UnDeliveryQty
                Dim SumUndelQty As Integer = 0,
                    UndelQty As String = "0"
                SumUndelQty = (SOReqQty + ReqQtySample - DeliQty - sampledelQty)
                If SumUndelQty > 0 Then
                    UndelQty = SumUndelQty
                    Dim Comma4 As Decimal = CDec(UndelQty)
                    UndelQty = String.Format("{0:n0}", Comma4)
                Else
                    If UndelQty = String.Empty Then
                        UndelQty = 0
                    End If
                End If
                '--Sum MO  where Item
                dt5 = SFAA.SumMO(SOReqQtyItemNo)
                Dim MOQty As String = "0"
                If dt5.Rows.Count > 0 Then
                    MOQty = dt5.Rows(0).Item("MOQty")
                    Dim Comma5 As Decimal = CDec(MOQty)
                    MOQty = String.Format("{0:n0}", Comma5)
                End If
                '--Sum POQty  where Item
                dt6 = PMDN.SumPOQty(SOReqQtyItemNo)
                Dim POQty As String = "0"
                If dt6.Rows.Count > 0 Then
                    POQty = dt6.Rows(0).Item("POQty")
                    Dim Comma6 As Decimal = CDec(POQty)
                    POQty = String.Format("{0:n0}", Comma6)
                End If
                '-- Sum ItemStock  where Item
                dt7 = IMAAL.SumItemStockUndelStus(SOReqQtyItemNo)
                Dim STStockQty As String = "0"
                If dt7.Rows.Count > 0 Then
                    STStockQty = dt7.Rows(0).Item("StockQty") '*
                    Dim Comma7 As Decimal = CDec(STStockQty)
                    STStockQty = String.Format("{0:n0}", Comma7)
                End If

                Dim ReqQty As String = "", DelQty As String = "",
                    SampleQty As String = "", SmpdelQty As String = ""
                ReqQty = FormatNumber(SOReqQty, 0,,, TriState.True)
                DelQty = FormatNumber(DeliQty, 0,,, TriState.True)
                SampleQty = FormatNumber(ReqQtySample, 0,,, TriState.True)
                SmpdelQty = FormatNumber(sampledelQty, 0,,, TriState.True)

                '--InsertSLUndelStus
                TempSOUnDelivery.InsertSLUndelStus(ShearchCustid, ShearchItemNo, ShearchItemName, ShearchItemSpec, UndelQty, ShearchUnit, ReqQty, SampleQty, DelQty, SmpdelQty, MOQty, POQty, STStockQty, ShearchClssfctDesc, UserID)
            Next
        Else
            Exit Sub
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
        TempSOUndelivery.ShowDetail(UserID, Whr, dt8)
        For e As Integer = 0 To dt8.Rows.Count - 1
            Dim Showitem As String = "", ShowDesc As String = "",
                ShowSpec As String = "", ShowCustID As String = "",
                ShowUndelQty As String = "", ShowUnit As String = "",
                ShowMOQty As String = "", ShowPOQty As String = "",
                ShowPRQty As String = "", ShowStockQty As String = "",
                ShowIndust As String = ""
            If dt8.Rows.Count > 0 Then
                ShowCustID = dt8.Rows(e).Item("custid")
                Showitem = dt8.Rows(e).Item("item")
                ShowDesc = dt8.Rows(e).Item("ItemDesc")
                ShowSpec = dt8.Rows(e).Item("Spec")
                ShowUndelQty = dt8.Rows(e).Item("UndelQty")
                ShowUnit = dt8.Rows(e).Item("Unit")
                ShowMOQty = dt8.Rows(e).Item("moQty")
                ShowPOQty = dt8.Rows(e).Item("poQty")
                ShowPRQty = dt8.Rows(e).Item("prQty")
                ShowStockQty = dt8.Rows(e).Item("StockQty")
                ShowIndust = dt8.Rows(e).Item("Industry")
                DtShow.Rows.Add(New Object() {ShowCustID, Showitem, ShowDesc, ShowSpec, ShowUndelQty, ShowUnit, ShowMOQty, ShowPOQty, ShowPRQty, ShowStockQty, ShowIndust})
            End If
        Next
        '##--delete from TempSOUnDelivery Where UserID
        TempSOUnDelivery.DeleteDetailTempSOUnDelivery(UserID)

        'เอาเข้า Gridview 
        gvUndelivery.DataSource = DtShow
        gvUndelivery.DataBind()

        Dim a As Integer = 0
        a = gvUndelivery.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(gvUndelivery, "#FFCC99")
        End If
    End Sub

    '--HyperLink gvUndelivery
    Protected Sub gvUndelivery_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUndelivery.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                Dim item As String = .DataItem("ItemNo")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("ItemNo")) Then
                    Dim link As String = ""
                    link = link & "&item=" & .DataItem("ItemNo")
                    link = link & "&Spec=" & .DataItem("Spec")
                    link = link & "&UndelQty=" & .DataItem("UndeliveryQty")
                    link = link & "&moQty=" & .DataItem("MOQty")
                    link = link & "&poQty=" & .DataItem("POQty")
                    link = link & "&StockQty=" & .DataItem("StockQty")
                    link = link & "&DateFrom=" & DateT1001.Text
                    link = link & "&DateTo=" & DateT1002.Text
                    hplDetail.NavigateUrl = "SaleUndeliveryStatusPopUp.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                    hplDetail.Target = "_blank"
                End If
            End If
        End With
    End Sub

End Class