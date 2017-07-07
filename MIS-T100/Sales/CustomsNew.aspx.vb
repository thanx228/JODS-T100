Imports System.Data
Imports System.Globalization
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Data.OracleClient
Public Class CustomsNew
    Inherits System.Web.UI.Page
    Dim DBCONN_SQL As New clsDBConnect
    Dim ChkbSelect As WebControls.CheckBox
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempCustoms As New TempCustoms
    Dim GetData As New GetData
    Dim Whe As String = "", DtShowGvAMTTH As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")
                TempCustoms.CreateTempCustoms(UserID)
                TempCustoms.CreateTempPrintCustoms(UserID)
            End If
            Call InvType()
            HeaderFormT1001.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        Conceal()
        Scollbar()

    End Sub

    '--'Save To Excel File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    '--InvoiceType
    Private Sub InvType()
        Dim Dt As DataTable = OOBXL.Get_InvType
        With ddlInvType
            .DataSource = Dt
            .DataValueField = "DocType_Id"
            .DataTextField = "InvTypeDescription"
            .DataBind()
        End With
    End Sub

    '--Search>>>Invoice
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim CkDtrowGv As New DataTable
        ShowGvInvNo(GvInvNo, True, NowPageIndex.Text)

        DtShowGvAMTTH.Clear()
        GvATM.DataBind()
        CountRow2.RowCount = String.Empty
    End Sub

    '--ShowGridview InvNo
    Private Sub ShowGvInvNo(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim ShowFiled As String = "InvoiceNo,InvoicDate,CustomerNo,CustomerName"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim dtshow, dt As New Data.DataTable
        If dtshow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, dtshow)
        End If

        Dim InvType As String = ddlInvType.SelectedValue, Datefrom As String = DateT1001.dateText, DateTo As String = DateT1002.dateText

        If tbCust.Text.Trim <> "" Then
            Dim Cust As TextBox = tbCust
            Whe = Whe & " " & ISAF.CustomerID & " in('" & GetData.GetCust(Cust) & "')"
            Whe = Whe & " and " & GetData.ora_dateselector(ISAF.InvoiceDate, Datefrom, DateTo) & ""
        End If
        If tbCust.Text.Trim = "" Then
            Whe = Whe & " " & GetData.ora_dateselector(ISAF.InvoiceDate, Datefrom, DateTo) & ""
        End If


        If InvType <> "" Then
            Whe = Whe & " and substr(" & ISAF.StatementNo & ",5,2) = '" & InvType & "'"
        End If

        ISAF.InvoiceDetail(Whe, dt)
        If dt.Rows.Count > 0 Then
            For c As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(c)
                    Dim InvoiceNo As String = .Item("" & ISAF.StatementNo & "")
                    Dim InvoiceDate As String = .Item("" & ISAF.DocumentDate & "")
                    Dim CustomerNo As String = .Item("" & ISAF.CustomerID & "")
                    Dim CustomerName As String = .Item("" & PMAAL.ContactName & "")

                    dtshow.Rows.Add(New Object() {InvoiceNo, InvoiceDate, CustomerNo, CustomerName})
                End With
            Next
        End If

        GvInvNo.DataSource = dtshow
        GvInvNo.DataBind()

        Dim a As Integer = 0
        a = GvInvNo.Rows.Count
        If a = 0 Then
            CountRow1.RowCount = String.Empty
            '--Cloes
            lblRemarkType.Visible = False
            ddlRarkType.Visible = False
            btnSelectATMUS.Visible = False
            btnSelectATMTH.Visible = False
        Else
            '--Show
            lblRemarkType.Visible = True
            ddlRarkType.Visible = True
            btnSelectATMUS.Visible = True
            btnSelectATMTH.Visible = True
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvInvNo, "#FFCC99")
            CountRow1.RowCount = ContDtFormOrl.RowGridview(GvInvNo)
        End If

    End Sub

    '--Select Invoice US
    Protected Sub btnSelectATMUS_Click(sender As Object, e As EventArgs) Handles btnSelectATMUS.Click
        ShowGvAMTUS(GvATM, True, NowPageIndex.Text)
        CountRow2.RowCount = ContDtFormOrl.RowGridview(GvATM)
        Dim a As Integer = 0
        a = GvATM.Rows.Count
        If a = 0 Then
            '--Show
            btnSelectATMUS.Visible = True
            btnSelectATMTH.Visible = True
            '--Cloes
            btnPrintReport.Visible = False
            btnPrintDelta.Visible = False
            btnPrintChinI.Visible = False
            btnPrintFisher.Visible = False
            btnExcel.Visible = False
        Else
            '--Show
            Panel1.Visible = True
            btnSelectATMUS.Visible = True
            btnSelectATMTH.Visible = True
        End If
    End Sub

    '--ShowGridview AMTUS
    Private Sub ShowGvAMTUS(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim type As String = ""
        Dim UserID As String = Session("UserName")
        TempCustoms.ClearTempCustoms(UserID)
        Select Case ddlRarkType.Text
            Case "Computer"
                type = "(ชิ้นส่วนคอมฯ)"
            Case "Appliances"
                type = "(ชิ้นส่วนประกอบตู้เย็น)"
        End Select

        'ชื่อคอลัมน์หัวตาราง 
        Dim ShowFiled As String = "Seq,Packing,Weight,InvoiceQty,InvoiceAmtTax,InvoiceNo,PO,Remark"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        'วนเช็คขอมูล ที่เลือก
        Dim seq As Integer = 1
        For i As Integer = 0 To GvInvNo.Rows.Count - 1
            ChkbSelect = GvInvNo.Rows(i).FindControl("ChkbSelect")
            If ChkbSelect.Checked = True Then
                Dim Invoice As String = GvInvNo.Rows(i).Cells(1).Text.ToString

                ISAG.InvoiceDetail(Invoice, ddlRarkType.SelectedValue, dt)
                For L_count As Integer = 0 To dt.Rows.Count - 1
                    Dim SeqSOfromSLInv As String = "", ShipOrderNo As String = "",
                        Item As String = ""
                    If dt.Rows.Count > 0 Then
                        SeqSOfromSLInv = dt.Rows(L_count).Item("isag003")
                        ShipOrderNo = dt.Rows(L_count).Item("isag002")
                        Item = dt.Rows(L_count).Item("isag009")
                    End If

                    dt1 = XMDL.SODelDetail(SeqSOfromSLInv, ShipOrderNo)
                    Dim SeqSOfromDelivery As String = "", ShippingNo As String = ""
                    If dt1.Rows.Count > 0 Then
                        SeqSOfromDelivery = dt1.Rows(0).Item("xmdl004")
                        ShippingNo = dt1.Rows(0).Item("xmdl001")
                    End If

                    '--PO
                    dt2 = XMDH.PO(SeqSOfromDelivery, ShippingNo)
                    Dim PO As String = "-"
                    If dt2.Rows.Count > 0 Then
                        PO = dt2.Rows(0).Item("xmdh050")
                    End If

                    dt3 = IMAA.ItemDetail(Item)
                    Dim Packing As String = "0.00"
                    Dim Weight As String = "0.00"
                    If dt3.Rows.Count > 0 Then
                        Packing = dt3.Rows(0).Item("Packigng")
                        Weight = dt3.Rows(0).Item("Weight")
                    End If

                    Dim InvoiceQty As String = dt.Rows(L_count).Item("isag004")
                    Dim InvoiceAmtTax As String = dt.Rows(L_count).Item("isag105")
                    Dim InvoiceNo As String = dt.Rows(L_count).Item("isagdocno")
                    Dim ItemNameRemarkType As String = dt.Rows(L_count).Item("isag010") & type

                    Dim Pack As String = "", Wgh As String = "", InvQty As String = "", InvQtyAmt As String = ""
                    Pack = FormatNumber(Packing, 2,,, TriState.True)
                    Wgh = FormatNumber(Weight, 2,,, TriState.True)
                    InvQty = FormatNumber(InvoiceQty, 2,,, TriState.True)
                    InvQtyAmt = FormatNumber(InvoiceAmtTax, 2,,, TriState.True)


                    DtShow.Rows.Add(New Object() {seq, Pack, Wgh, InvQty, InvQtyAmt, InvoiceNo, PO, ItemNameRemarkType})
                    '--บันทึกข้อมุลลง TempCustoms
                    TempCustoms.InserDataCustoms(seq, Packing, Weight, InvoiceQty, InvoiceAmtTax, InvoiceNo, PO, ItemNameRemarkType, UserID)
                    seq += 1
                Next

            End If
        Next
        'เอาเข้า Gridview 
        GvATM.DataSource = DtShow
        GvATM.DataBind()

        Dim a As Integer = 0
        a = GvInvNo.Rows.Count
        If a = 0 Then

        Else
            '--Show
            lblRemarkType.Visible = True
            ddlRarkType.Visible = True
            btnPrintReport.Visible = True
            btnPrintDelta.Visible = True
            btnPrintChinI.Visible = True
            btnPrintFisher.Visible = True
            btnExcel.Visible = True
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvATM, "#FFCC99")
        End If
    End Sub

    '--Select Invoice TH
    Protected Sub btnSelectATMTH_Click(sender As Object, e As EventArgs) Handles btnSelectATMTH.Click
        ShowGvAMTTH(GvATM, True, NowPageIndex.Text)
        CountRow2.RowCount = ContDtFormOrl.RowGridview(GvATM)
        Dim a As Integer = 0
        a = GvATM.Rows.Count
        If a = 0 Then
            '--Show
            btnSelectATMUS.Visible = True
            btnSelectATMTH.Visible = True
            '--Cloes
            btnPrintReport.Visible = False
            btnPrintDelta.Visible = False
            btnPrintChinI.Visible = False
            btnPrintFisher.Visible = False
            btnExcel.Visible = False
        Else
            '--Show
            Panel1.Visible = True
            btnSelectATMUS.Visible = True
            btnSelectATMTH.Visible = True
        End If
    End Sub

    '--ShowGridview AMTTH
    Private Sub ShowGvAMTTH(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim type As String = ""
        Dim UserID As String = Session("UserName")
        TempCustoms.ClearTempCustoms(UserID)
        Select Case ddlRarkType.Text
            Case "Computer"
                type = "(ชิ้นส่วนคอมฯ ไม่ได้รวมหีบห่อ)"
            Case "Appliances"
                type = "(ชิ้นส่วนประกอบตู้เย็น)"
        End Select

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "Seq,Packing,Weight,InvoiceQty,InvoiceAmtTax,InvoiceNo,PO,Remark"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dt, dt1, dt2, dt3, dt4 As New Data.DataTable
        If DtShowGvAMTTH.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShowGvAMTTH)
        End If

        'วนเช็คขอมูล ที่เลือก
        Dim seq As Integer = 1
        For i As Integer = 0 To GvInvNo.Rows.Count - 1
            ChkbSelect = GvInvNo.Rows(i).FindControl("ChkbSelect")
            If ChkbSelect.Checked = True Then
                Dim Invoice As String = GvInvNo.Rows(i).Cells(1).Text.ToString

                ISAG.InvoiceDetail(Invoice, ddlRarkType.SelectedValue, dt)
                For L_count As Integer = 0 To dt.Rows.Count - 1
                    Dim SeqSOfromSLInv As String = "", ShipOrderNo As String = "",
                        Item As String = ""
                    If dt.Rows.Count > 0 Then
                        SeqSOfromSLInv = dt.Rows(L_count).Item("isag003")
                        ShipOrderNo = dt.Rows(L_count).Item("isag002")
                        Item = dt.Rows(L_count).Item("isag009")
                    End If

                    dt1 = XMDL.SODelDetail(SeqSOfromSLInv, ShipOrderNo)
                    Dim SeqSOfromDelivery As String = "", ShippingNo As String = ""
                    If dt1.Rows.Count > 0 Then
                        SeqSOfromDelivery = dt1.Rows(0).Item("xmdl004")
                        ShippingNo = dt1.Rows(0).Item("xmdl001")
                    End If

                    '--PO
                    dt2 = XMDH.PO(SeqSOfromDelivery, ShippingNo)
                    Dim PO As String = "-"
                    If dt2.Rows.Count > 0 Then
                        PO = dt2.Rows(0).Item(XMDH.Memo)
                    End If

                    dt3 = IMAA.ItemDetail(Item)
                    Dim Packing As String = "0.00"
                    Dim Weight As String = "0.00"
                    If dt3.Rows.Count > 0 Then
                        Packing = dt3.Rows(0).Item("Packigng")
                        Dim Comma2 As Decimal = CDec(Packing)
                        Packing = String.Format("{0:n2}", Comma2)

                        Weight = dt3.Rows(0).Item("Weight")
                        Dim Comma3 As Decimal = CDec(Weight)
                        Weight = String.Format("{0:n2}", Comma3)

                    End If

                    Dim InvoiceQty As String = dt.Rows(L_count).Item("isag004")
                    Dim Comma1 As Decimal = CDec(InvoiceQty)
                    InvoiceQty = String.Format("{0:n2}", Comma1)

                    Dim InvoiceAmtTax As String = dt.Rows(L_count).Item("" & ISAG.AmtBfTaxInLocalCurr & "")
                    Dim Comma As Decimal = CDec(InvoiceAmtTax)
                    InvoiceAmtTax = String.Format("{0:n2}", Comma)

                    Dim InvoiceNo As String = dt.Rows(L_count).Item("isagdocno")
                    Dim ItemNameRemarkType As String = dt.Rows(L_count).Item("isag010") & type

                    DtShowGvAMTTH.Rows.Add(New Object() {seq, Packing, Weight, InvoiceQty, InvoiceAmtTax, InvoiceNo, PO, ItemNameRemarkType})
                    '--บันทึกข้อมุลลง TempCustoms
                    TempCustoms.InserDataCustoms(seq, Packing, Weight, InvoiceQty, InvoiceAmtTax, InvoiceNo, PO, ItemNameRemarkType, UserID)
                    seq += 1
                Next

            End If
        Next
        'เอาเข้า Gridview 
        GvATM.DataSource = DtShowGvAMTTH
        GvATM.DataBind()

        'Dim a As Integer = 0
        'a = GvInvNo.Rows.Count
        If GvInvNo.Rows.Count > 0 Then
            '--Show
            lblRemarkType.Visible = True
            ddlRarkType.Visible = True
            btnPrintReport.Visible = True
            btnPrintDelta.Visible = True
            btnPrintChinI.Visible = True
            btnPrintFisher.Visible = True
            btnExcel.Visible = True
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvATM, "#FFCC99")
        End If
    End Sub

    '--Print Report
    Protected Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
        '--โชว์ปุ่มทั้งหมดเมื่อมีการคลิ๊ก
        ShowAllControl()
        PrintReport()
    End Sub

    '--Print Report Delta
    Protected Sub btnPrintDelta_Click(sender As Object, e As EventArgs) Handles btnPrintDelta.Click
        '--โชว์ปุ่มทั้งหมดเมื่อมีการคลิ๊ก
        ShowAllControl()
        PrintReportDrlts()
    End Sub

    '--Print Report Chin i
    Protected Sub btnPrintChinI_Click(sender As Object, e As EventArgs) Handles btnPrintChinI.Click
        '--โชว์ปุ่มทั้งหมดเมื่อมีการคลิ๊ก
        ShowAllControl()
        PrintReportChin()
    End Sub

    '--Print Report Fisher
    Protected Sub btnPrintFisher_Click(sender As Object, e As EventArgs) Handles btnPrintFisher.Click
        '--โชว์ปุ่มทั้งหมดเมื่อมีการคลิ๊ก
        ShowAllControl()
        PrintReportFisher()
    End Sub

    '--Print Report To Excel
    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        GvATM.Visible = True
        ContDtFormOrl.ExportGridViewToExcel("SaleOrderChangeStatusShow", GvATM)
        '--โชว์ปุ่มทั้งหมดเมื่อมีการคลิ๊ก
        ShowAllControl()
    End Sub

    '--Show All Contorl 
    Private Sub ShowAllControl()
        lblRemarkType.Visible = True
        ddlRarkType.Visible = True
        btnSelectATMUS.Visible = True
        btnSelectATMTH.Visible = True
        btnPrintReport.Visible = True
        btnPrintDelta.Visible = True
        btnPrintChinI.Visible = True
        btnPrintFisher.Visible = True
        btnExcel.Visible = True
    End Sub

    '--Conceal Contorl ซ่อน
    Private Sub Conceal()
        btnSelectATMUS.Visible = False
        btnSelectATMTH.Visible = False
        btnPrintReport.Visible = False
        btnPrintDelta.Visible = False
        btnPrintChinI.Visible = False
        btnPrintFisher.Visible = False
        btnExcel.Visible = False
        lblRemarkType.Visible = False
        ddlRarkType.Visible = False
    End Sub

    '--ShowScollbar
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvInvNoScrollbar", "GvInvNoScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvATMScrollbar", "GvATMScrollbar();", True)
    End Sub

    '--ConnetCRView file Customs.rpt
    Private Sub PrintReport()
        Dim Username As String = Session("UserName")
        TempCustoms.InserDataPrintCustoms(Username)
        Dim paraName As String = ""
        paraName = "username:" & Username
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&f=SLReport&ReportName=Customs.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    '--ConnetCRView file CustomsDrlts.rpt
    Private Sub PrintReportDrlts()
        Dim Username As String = Session("UserName")
        TempCustoms.InserDataPrintCustoms(Username)
        Dim paraName As String = ""
        paraName = "username: " & Username
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&f=SLReport&ReportName=CustomsDelta.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    '--ConnetCRView file CustomsChin.rpt
    Private Sub PrintReportChin()
        Dim Username As String = Session("UserName")
        TempCustoms.InserDataPrintCustoms(Username)
        Dim paraName As String = ""
        paraName = "username: " & Username
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&f=SLReport&ReportName=CustomsChin.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    '--ConnetCRView file CustomsFisher.rpt
    Private Sub PrintReportFisher()
        Dim Username As String = Session("UserName")
        TempCustoms.InserDataPrintCustoms(Username)
        Dim paraName As String = ""
        paraName = "username: " & Username
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&f=SLReport&ReportName=CustomsFisher.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub
End Class