Imports System.Globalization
Imports System.Drawing
Public Class SaleUndeliveryStatusPeriod
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempSOUndeliveryPeriod As New TempSOUndeliveryPeriod
    Dim connect As New clsDBConnect
    Dim ConfigDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")

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
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvUndelPeriodScrollbar", "GvUndelPeriodScrollbar();", True)
    End Sub

    '--Export Excel
    Protected Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        ContDtFormOrl.ExportGridViewToExcel("SOUndeliveryPeriod", GvUndelPeriod)
    End Sub

    '--Search
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ShowgvUndeliveryPeriod()
        CountRow1.RowCount = ContDtFormOrl.RowGridview(GvUndelPeriod)
    End Sub

    '--ShowgvUndeliveryPeriod
    Private Sub ShowgvUndeliveryPeriod()
        Dim UserID As String = Session("UserName")
        Dim Whr As String = "",
            CustNo As String = txtCustID.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            Ver As String = txtVersion.Text.Trim,
            Item As String = txtItemNo.Text.Trim,
            Spac As String = txtSpec.Text.Trim,
            Condition As String = ddlCondition.SelectedValue,
            DateFrom As String = DateT1001.Text, DateTo As String = DateT1002.Text,
            SOTypeCheckList As String = SelectCheckBoxList.MultipleSelect(UsingTypeSaleCheckList1.getObject),
            SOTypeRows As Integer = SelectCheckBoxList.RowNumSelect,
            Classification As String = SelectCheckBoxList.Multiple(UsingProductClassification1.getObject),
            IndustryRows As Integer = SelectCheckBoxList.RowNum

        Dim date1 As String = DateFrom,
            date2 As String = DateTo,
            strDate As String = "",
            endDate As String = ""
        Dim Period As String = ""

        If date1 <> "" Then
            strDate = ConfigDate.dateFormat5(date1)

            date1 = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
            Dim DatePeriod As String = date1
            Period = "D" & DatePeriod
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))

            strDate = Date.Today.ToString("yyyyMM", New CultureInfo("en-US"))
            Dim DatePeriod As String = strDate
            Period = "D" & DatePeriod
        End If
        If date2 <> "" Then
            endDate = ConfigDate.dateFormat5(date2)
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If

        '--endDate = endDate & "31" 
        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim amtMonth As Short = DateDiff(DateInterval.Month, beginDate, lastDate)
        If beginDate = lastDate Then
            amtMonth = 1
        End If

        Dim tempTable As String = "TempSOUnDeliveryPeriod" & Session("UserName")
        TempSOUndeliveryPeriod.createTempSOUnDelivery(tempTable, beginDate, amtMonth)


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

        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8 As New Data.DataTable

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
                    Dim Comma As Decimal = CDec(UndelQty)
                    UndelQty = String.Format("{0:n0}", Comma)
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
                    Dim Comma As Decimal = CDec(MOQty)
                    MOQty = String.Format("{0:n0}", Comma)
                End If
                '--Sum POQty  where Item
                dt6 = PMDN.SumPOQty(SOReqQtyItemNo)
                Dim POQty As String = "0"
                If dt6.Rows.Count > 0 Then
                    POQty = dt6.Rows(0).Item("POQty")
                    Dim Comma As Decimal = CDec(POQty)
                    POQty = String.Format("{0:n0}", Comma)
                End If
                '-- Sum ItemStock  where Item
                dt7 = IMAAL.SumTUndelStusPeriod(SOReqQtyItemNo)
                Dim STStockQty As String = "0"
                If dt7.Rows.Count > 0 Then
                    STStockQty = dt7.Rows(0).Item("StockQty") '*
                    Dim Comma As Decimal = CDec(STStockQty)
                    STStockQty = String.Format("{0:n0}", Comma)
                End If

                Dim ReqQty As String = "", DelQty As String = "",
                    SampleQty As String = "", SmpdelQty As String = ""
                ReqQty = FormatNumber(SOReqQty, 0,,, TriState.True)
                DelQty = FormatNumber(DeliQty, 0,,, TriState.True)
                SampleQty = FormatNumber(ReqQtySample, 0,,, TriState.True)
                SmpdelQty = FormatNumber(sampledelQty, 0,,, TriState.True)

                '--InsertSLUndelStus
                TempSOUndeliveryPeriod.InsertSLUndelStus(ShearchCustid, ShearchItemNo, ShearchItemName, ShearchItemSpec, UndelQty, ShearchUnit, ReqQty, SampleQty, DelQty, SmpdelQty, MOQty, POQty, STStockQty, ShearchClssfctDesc, tempTable)

                '--Update Period
                TempSOUndeliveryPeriod.UpdatePeriod(tempTable, Period, UndelQty, SOReqQtyItemNo)

            Next
        Else
            If dt1.Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        '--Where Condition
        Whr = ""
        If Condition <> "0" Then
            Whr = " where "
            Dim stock As String = "stockQty"
            Dim supply As String = stock & "+moQty+poQty+prQty"
            Dim undel As String = "UndelQty"
            Select Case Condition
                Case "1" ' stock >= undelivery
                    Whr = Whr & stock & "<" & undel
                Case "2" ' supply >= undelivery
                    Whr = Whr & supply & ">=" & undel
                Case "3" ' supply < undelivery
                    Whr = Whr & supply & "<" & undel
                Case "4" ' supply < undelivery
                    Whr = Whr & stock & ">=" & undel
            End Select
        End If

        '--For Fid Period
        Dim fld As String = ""
        For i As Integer = 0 To amtMonth
            Dim tdate As String = beginDate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            Dim fldName As String = "D" & tdate
            fld = fld & fldName & ","
        Next

        '--Select Detail
        Dim Sql As String = "select custid,item,ItemDesc,Spec," & fld & "UndelQty,moQty,poQty,prQty,stockQty,Industry from " & tempTable & " " & Whr & " order by item"
        GetData.Get_DataReaderSQL(Sql, clsDBConnect.strMIS2ConnectionString, dt8)

        'Input into Gridview 
        GvUndelPeriod.DataSource = dt8
        GvUndelPeriod.DataBind()

        Dim a As Integer = 0
        a = GvUndelPeriod.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvUndelPeriod, "#FFCC99")
        End If
    End Sub

    '--HyperLink gvUndelivery
    Protected Sub GvUndelPeriod_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvUndelPeriod.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                Dim item As String = .DataItem("item")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("item")) Then
                    Dim link As String = ""
                    link = link & "&item=" & .DataItem("item")
                    link = link & "&Spec=" & .DataItem("Spec")
                    link = link & "&UndelQty=" & .DataItem("UndelQty")
                    link = link & "&moQty=" & .DataItem("moQty")
                    link = link & "&poQty=" & .DataItem("poQty")
                    link = link & "&stockQty=" & .DataItem("StockQty")
                    link = link & "&DateFrom=" & DateT1001.Text
                    link = link & "&DateTo=" & DateT1002.Text
                    hplDetail.NavigateUrl = "SLUndelPeriodPopUp.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                    hplDetail.Target = "_blank"
                End If
            End If
        End With
    End Sub
End Class