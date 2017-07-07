Imports System.Globalization
Imports System.Drawing
Public Class SaleQuotation
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempSLQuotation As New TempSLQuotation
    Dim connect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")
                TempSLQuotation.createTempSLQuotion(UserID)
            End If
            HeaderFormT1001.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        Scollbar()
    End Sub

    '--Save To Excel File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    '--Export Excel
    Protected Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        ContDtFormOrl.ExportGridViewToExcel("GvSaleQuotation", GvSaleQuotation)
    End Sub

    '--ShowScollbar สกอบาร์
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvSaleQuotationScrollbar", "GvSaleQuotationScrollbar();", True)
    End Sub

    '--Search
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        '--ShowGvReportHead
        ShowGvSaleQuotation(GvSaleQuotation, True, NowPageIndex.Text)
        CountRow1.RowCount = ContDtFormOrl.RowGridview(GvSaleQuotation)
    End Sub

    '--ShowGvSaleQuotation
    Private Sub ShowGvSaleQuotation(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        Dim Whr As String = "",
            CustNo As String = txtCustID.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            Ver As String = txtSOSeq.Text.Trim,
            Item As String = txtPartNo.Text.Trim,
            Spac As String = txtSpec.Text.Trim,
            RowsStus As String = ddlStatus.SelectedValue,
            DateFrom As String = DateT1001.Text,
            DateTo As String = DateT1002.Text,
            SOTypeCheckList As String = SelectCheckBoxList.MultipleSelect(UsingTypeSaleCheckList1.getObject),
            SOTypeRows As Integer = SelectCheckBoxList.RowNumSelect

        If SOTypeRows > 0 Then
            Whr = Whr & " and substr(" & XMDA.SaleOrderNo & ",3,4)" & " in(" & [String].Join("','", SOTypeCheckList) & "')"
        End If
        If RowsStus <> "0" Then
            Whr = Whr & " and " & XMDC.RowsStus & "= '" & RowsStus & "'"
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
            Whr = Whr & " and " & XMDA.DocumentDate & " BETWEEN TO_DATE ('" & DateFrom & "', 'yyyy/mm/dd') and TO_DATE ('" & DateTo & "', 'yyyy/mm/dd')"
        End If

        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "SaleOrderNo, Version, ItemNo, Spec, CustomerID, CustomerName, OrderQty, SampleQty,SOPrice, UnitPrice, DocumentDate, DeffPrice"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        '--Checking CustID SOChg Header and CustID SOChg Line
        '--Shearch SO RequestQty from where
        XMDA.GetWhrQuotation(Whr, dt)
        For e As Integer = 0 To dt.Rows.Count - 1
            Dim SaleOrderNo As String = "",
                VersionNo As String = "",
                DocumentDate As String = "",
                ItemNo As String = "",
                Specifaction As String = "",
                CustomerId As String = "",
                CustomerName As String = "",
                OrderQty As String = "",
                SOUnitPrice As String = "",
                DeffPrice As String = "",
                RowsStatus As String = ""
            If dt.Rows.Count > 0 Then
                SaleOrderNo = dt.Rows(e).Item("xmdadocno")
                VersionNo = dt.Rows(e).Item("xmda001")
                DocumentDate = dt.Rows(e).Item("xmdadocdt")
                ItemNo = dt.Rows(e).Item("xmdc001")
                Specifaction = dt.Rows(e).Item("imaal004")
                CustomerId = dt.Rows(e).Item("xmda004")
                CustomerName = dt.Rows(e).Item("pmaal004")
                OrderQty = dt.Rows(e).Item("RequestQty")
                Dim Comma As Decimal = CDec(OrderQty)
                OrderQty = String.Format("{0:n0}", Comma)
                SOUnitPrice = dt.Rows(e).Item("xmdc015")
                Dim Comma1 As Decimal = CDec(SOUnitPrice)
                SOUnitPrice = String.Format("{0:n5}", Comma1)
                RowsStatus = dt.Rows(e).Item("xmdc045")
            End If

            dt2 = XMDA.SheachQuotationSample(SaleOrderNo, VersionNo, ItemNo, RowsStatus)
            Dim SampleQty As String = "0"
            If dt2.Rows.Count > 0 Then
                SampleQty = dt2.Rows(0).Item("SampleRequestQty")
                Dim Comma As Decimal = CDec(SampleQty)
                SampleQty = String.Format("{0:n5}", Comma)
            End If

            dt3 = XMDU.ShearchSLPrice(ItemNo, DocumentDate, CustomerId)
            Dim DocDate As String = "-",
                SLPriLinesPricingByQty As String = "",
                UnitPrice As String = "0",
                SLPriLinesPriByQtyInitialQty As String = "0",
                SLPriLinesPriByQtyEndQty As String = "0"
            If dt3.Rows.Count > 0 Then
                DocDate = dt3.Rows(0).Item("xmdtdocdt")
                SLPriLinesPricingByQty = dt3.Rows(0).Item("xmdu009")
                UnitPrice = dt3.Rows(0).Item("xmdu011") '--Price  PricingByQty = N
                SLPriLinesPriByQtyInitialQty = dt3.Rows(0).Item("InitialQty") '--standard Qty from
                SLPriLinesPriByQtyEndQty = dt3.Rows(0).Item("EndQty") '--standard Qty To
            End If

            If SLPriLinesPricingByQty = "Y" Then
                If OrderQty >= SLPriLinesPriByQtyInitialQty Or OrderQty <= SLPriLinesPriByQtyEndQty Then
                    UnitPrice = dt3.Rows(0).Item("UnitPrice") '--Price  PricingByQty =Y
                    Dim Comma As Decimal = CDec(UnitPrice)
                    UnitPrice = String.Format("{0:n5}", Comma)
                End If
            End If

            '--ราคาที่เปิด SalesOrder - ราคาที่ App ใช้
            DeffPrice = SOUnitPrice - UnitPrice
            Dim Comma2 As Decimal = CDec(DeffPrice)
            DeffPrice = String.Format("{0:n5}", Comma2)
            TempSLQuotation.InserSOQuotionDetail(SaleOrderNo, VersionNo, ItemNo, Specifaction, CustomerId, CustomerName, OrderQty, SampleQty, SOUnitPrice, UnitPrice, DeffPrice, DocDate, UserID)
        Next
        Whr = ""
        If cbNotQuon.Checked Then
            Whr = Whr & " and UnitPrice='0' "
        End If
        If cbPrice.Checked Then
            Whr = Whr & " and UnitPrice>SOPrice and UnitPrice>'0'"
        End If
        '--SelectTempSLQuotion
        TempSLQuotation.SelectTempSLQuotion(Whr, UserID, dt1)
        Dim ShowSONo As String = "", ShowVer As String = "",
            ShowItemNo As String = "", ShowSpec As String = "",
            ShowCustID As String = "", ShowCustName As String = "",
            ShowOrderQty As String = "", ShowSampleQty As String = "",
            ShowSOPrice As String = "", ShowUnitPrice As String = "",
            ShowDocDate As String = "", ShowDeffPrice As String = ""
        For i As Integer = 0 To dt1.Rows.Count - 1
            ShowSONo = dt1.Rows(i).Item("SaleOrderNo")
            ShowVer = dt1.Rows(i).Item("VerNo")
            ShowItemNo = dt1.Rows(i).Item("ItemNo")
            ShowSpec = dt1.Rows(i).Item("Spec")
            ShowCustID = dt1.Rows(i).Item("CustID")
            ShowCustName = dt1.Rows(i).Item("CustName")
            ShowOrderQty = dt1.Rows(i).Item("OrderQty")
            ShowSampleQty = dt1.Rows(i).Item("SampleQty")
            ShowSOPrice = dt1.Rows(i).Item("SOPrice")
            ShowUnitPrice = dt1.Rows(i).Item("UnitPrice")
            ShowDocDate = dt1.Rows(i).Item("DocDate")
            ShowDeffPrice = dt1.Rows(i).Item("DeffPrice")
            DtShow.Rows.Add(New Object() {ShowSONo, ShowVer, ShowItemNo, ShowSpec, ShowCustID, ShowCustName, ShowOrderQty, ShowSampleQty, ShowSOPrice, ShowUnitPrice, ShowDocDate, ShowDeffPrice})
        Next
        '--DeleteDetailTempSLQuotion where UserID
        TempSLQuotation.DeleteDetailTempSLQuotion(UserID)

        'เอาเข้า Gridview 
        GvSaleQuotation.DataSource = DtShow
        GvSaleQuotation.DataBind()

        Dim a As Integer = 0
        a = GvSaleQuotation.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvSaleQuotation, "#FFCC99")
        End If
    End Sub

End Class