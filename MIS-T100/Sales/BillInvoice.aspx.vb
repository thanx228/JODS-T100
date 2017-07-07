Imports System.Data
Imports System.Globalization
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class BillInvoice
    Inherits System.Web.UI.Page
    Dim ChkbSelect As WebControls.CheckBox
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    Dim TempInvoice As New TempInv
    Dim TmpBillSearch As New TempBillSearch
    Dim dt As New DataTable
    Dim KeyARNo As String
    Dim BillNohead As String
    Dim AmountText As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else
                TempInvoice.CreateTempBillhead()
                TempInvoice.CreateTempBillLine()
            End If
            AddEmployeeID()
            EditEmployeeID()
            ReptEmployeeID()
        End If
        AmountText = lblEnglishBaht.Text
        btnSave.Visible = False
        EdtbtnSave.Visible = False
        DletbtnSave.Visible = False
        Scollbar()
    End Sub
    '##>>>---------------------------------------Add   Data---------------------------------------------<<<##
    '--EmployeeID Section Sales
    Private Sub AddEmployeeID()
        Dim Dt As DataTable = OOAG.GetEmployeeIDOralddl()
        With ddlEmp
            .DataSource = Dt
            .DataValueField = "ooag001"
            .DataTextField = "EmpID"
            .DataBind()
        End With
    End Sub

    '--Shearh Invoice Custommer
    Protected Sub btnShearh_Click1(sender As Object, e As EventArgs) Handles btnShearh.Click
        ShowGvAddData(GvAddData, True, NowPageIndex.Text)
        CountRow1.RowCount = ContDtFormOrl.RowGridview(GvAddData)
    End Sub

    '--ShowGvAddData
    Private Sub ShowGvAddData(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        If txtCustID.Text = "" Then
            Exit Sub
        ElseIf DateT1001.dateText = "" Then
            Exit Sub
        ElseIf DateT1002.dateText = "" Then
            Exit Sub
        End If

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "Date,Invoice,PlanReceiveDate,Amount(Not Including Tax),Tax,Amount( Including Tax),Collected"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12 As New Data.DataTable
        ContDtFormOrl.DataTableTranf(ArrayShowFiled, dt)

        '--Checking CustIDMain, EmployeeID,DateFrom,DateTo from  Invoice Main table.
        ISAF.GetBillInvoiceOral(txtCustID.Text, DateT1001.dateText, DateT1002.dateText, dt1)
        For j As Integer = 0 To dt1.Rows.Count - 1
            If dt1.Rows.Count > 0 Then
                Dim InvToSearch As String = dt1.Rows(j).Item("isafdocno")
                TempInvoice.CreateTempBillSearch()
                TempInvoice.GetBillSearch(InvToSearch, dt9)
            End If
            '--เทียบ BillInv
            TmpBillSearch.CheckingBillInvoice(dt11)
            For l As Integer = 0 To dt11.Rows.Count - 1
                Dim BillInvSearch As String = ""
                If dt11.Rows.Count > 0 Then
                    BillInvSearch = dt11.Rows(l).Item("InvoiceNo")
                End If
                ISAF.GetBillInvSearchSQL(BillInvSearch, dt10)
            Next
        Next

        'วนเช็คข้อมูล ที่ต้องการนำมาโชว์
        For i As Integer = 0 To dt10.Rows.Count - 1
            Dim KeyCustIDInv As String = "",
                KeyEmpSLInv As String = "",
                KeyInv As String = "",
                KeyInvDate As String = ""
            If dt10.Rows.Count > 0 Then
                KeyCustIDInv = dt10.Rows(i).Item("isaf003")
                KeyInv = dt10.Rows(i).Item("isafdocno")
                KeyInvDate = dt10.Rows(i).Item("isaf014")
                KeyEmpSLInv = dt10.Rows(i).Item("isafcrtid") '--EmployeeID Sales Create Sales Invoice
            Else
                Exit Sub
            End If

            '--Checking EmployeeID from EmployeeID Main table EmployeeID Sales noly.
            OOAG.GetEmployeeIDOral(KeyEmpSLInv, dt2)
            Dim KeyEmployeeID As String = dt2.Rows(0).Item("ooag001")

            '--Checking CustomerID from CustomerMain table.
            PMAA.GetCustAddressOral(KeyCustIDInv, dt3)
            Dim KeyCustIDMain As String = "",
                KeyCustMain As String = ""
            If dt3.Rows.Count > 0 Then
                KeyCustIDMain = dt3.Rows(0).Item("pmaa001")
                KeyCustMain = dt3.Rows(0).Item("pmaa027")
            End If

            '--Checking CustomerID from CustBodyL table.
            PMAAL.GetDataCustomer(KeyCustIDMain, dt4)
            Dim CustomerName As String = "",
                CustomerFullName As String = ""
            If dt4.Rows.Count > 0 Then
                CustomerName = dt4.Rows(0).Item("pmaal004")
                CustomerFullName = dt4.Rows(0).Item("pmaal003")
            End If

            '--Checking KeyCustMain from CustBody table.
            OOFB.GetAddressCustOral(KeyCustMain, dt5)
            Dim AddressCust As String = ""
            If dt5.Rows.Count > 0 Then
                AddressCust = dt5.Rows(0).Item("oofb017")
            End If

            '--Checking CustIDInv, EmpSL from Maintain Shipping Receivables table.
            XRCA.GetCollectedAmountInvoice(KeyCustIDMain, KeyEmpSLInv, KeyInv, dt6)
            Dim KeyInvMSR As String = "",
                KeyEmpIDSLMSR As String = "",
                Payment As String = "",
                ARDue As String = "",
                Inv As String = "",
                AMTBeforeTax As String = "",
                Tex As String = "",
                ARAmt As String = ""
            For e As Integer = 0 To dt6.Rows.Count - 1
                If dt6.Rows.Count > 0 Then
                    KeyARNo = dt6.Rows(e).Item("xrcadocno")
                    KeyInvMSR = dt6.Rows(e).Item("xrca018") '--Inv Appoved only(JP61EX-20170100040).
                    KeyEmpIDSLMSR = dt6.Rows(e).Item("xrca014")
                    Payment = dt6.Rows(e).Item("xrca008")
                    ARDue = dt6.Rows(e).Item("xrca009") '--Plan Receive Date
                    AMTBeforeTax = dt6.Rows(e).Item("xrca113") '--Amount(Not Including Tax)
                    Tex = dt6.Rows(e).Item("xrca114")
                    ARAmt = dt6.Rows(e).Item("xrca118") '--AR Amt.(Local Curr.) / Amount( Including Tax)
                    Inv = dt6.Rows(e).Item("xrca066").ToString  '--Inv Appoved only (EX-17010040).
                End If
            Next

            '--Checking ARNo from Maintain Shipping Receivables
            XRCE.GetARNo(KeyARNo, dt7)
            Dim BalanInLocalCorr As String = ""
            If dt7.Rows.Count > 0 Then
                BalanInLocalCorr = dt7.Rows(0).Item("xrce119") '--Collected
            End If

            '--Checking KeyInvMSR, KeyEmpIDSLMSR, KeyInvDate from
            ISAT.GetInvoiceDate(KeyInvMSR, KeyEmpIDSLMSR, KeyInvDate, dt8)
            Dim InvDate As String = ""
            If dt8.Rows.Count > 0 Then
                InvDate = dt8.Rows(0).Item("isat007")
            End If

            If dt6.Rows.Count > 0 Then
                '--Show data into lable
                lblCust.Text = KeyCustIDInv
                lblCustName.Text = CustomerName
                lblPayment.Text = Payment
                lblAddress1.Text = AddressCust
                '--Add data into rows
                dt.Rows.Add(New Object() {InvDate, Inv, ARDue, AMTBeforeTax, Tex, ARAmt, BalanInLocalCorr})
            End If
        Next
        'เอาเข้า Gridview 
        GvAddData.DataSource = dt
        GvAddData.DataBind()

        Dim a As Integer = 0
        a = GvAddData.Rows.Count
        If a = 0 Then
        Else
            '--AutoGenaerateDate
            lblDate.Text = Date.Now.ToString("dd/MM/yyyy")
            lblBeDate.Text = Date.Now.ToString("yyyyMMdd")

            '--AutoGenaerateBillNo
            Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
            lblBillNo.Text = TempInvoice.GetAutoGenerate(DateDoc, dt12)
            lblBill.Text = TempInvoice.GetAutoGenerate(DateDoc, dt12)

            '--ShowButton
            btnSave.Visible = True

            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvAddData, "#7B68EE")
        End If
    End Sub

    '--Save Invoice
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim CreateBy As String = Session("UserName")
        '--DetaillBody
        Dim BillNo As String = lblBillNo.Text,
            BillShow As String = lblBill.Text,
            CustID As String = lblCust.Text,
            CustName As String = lblCustName.Text,
            Ass1 As String = lblAddress1.Text,
            Ass2 As String = lblAddress2.Text,
            BillDate As String = lblDate.Text,
            Paymet As String = lblPayment.Text,
            BillBy As String = ddlEmp.SelectedValue,
            BeDate As String = lblBeDate.Text

        Dim dt, dt1, dt2 As New DataTable
        For i = 0 To GvAddData.Rows.Count - 1
            '--DetaillGvAddData
            Dim InvDate As String = GvAddData.Rows(i).Cells(1).Text,
                Inv As String = GvAddData.Rows(i).Cells(2).Text,
                DueDate As String = GvAddData.Rows(i).Cells(3).Text,
                AmtBeforeTax As Decimal = GvAddData.Rows(i).Cells(4).Text,
                Tex As Decimal = GvAddData.Rows(i).Cells(5).Text,
                ARAmt As Decimal = GvAddData.Rows(i).Cells(6).Text,
                Paid As Decimal = GvAddData.Rows(i).Cells(7).Text

            '--
            Dim Balance As Decimal = "",
                ValuesType As String = "",
                CR As Double = "",
                Type As String = Inv.Substring(1, 2),
                ShowAmount As String = "",
                ShowBalance As String = "",
                SPaid As String = ""

            ChkbSelect = GvAddData.Rows(i).FindControl("ChkB")
            '--ถ้ามีการติ๊กข้อมูล
            If ChkbSelect.Checked = True Then
                '--บันทึกข้อมุลลง TempBillhead
                TempInvoice.InserBillhead(BillNo, BillShow, CustID, CustName, Ass1, Ass2, BillDate, Paymet, CreateBy, BillBy, BeDate)
                Balance = ARAmt - Paid

                If Type = "CR1" Or Type = "CR2" Then
                    CR = Balance
                    ValuesType = "-"
                ElseIf Paid = "0.00" Then
                    SPaid = ""
                ElseIf Paid <> "0.00" Then
                    SPaid = Paid
                Else
                    ValuesType = ""
                End If

                ShowAmount = ValuesType & ARAmt
                ShowBalance = ValuesType & Balance

                TempInvoice.selctInvAll(Inv, dt)
                If dt.Rows.Count = 0 Then
                    '--บันทึกข้อมุลลง TempBillLine
                    TempInvoice.InserBillLine(BillNo, Inv, DueDate, ARAmt, Balance, InvDate, ShowAmount, ShowBalance, SPaid, Paid, CustID)
                ElseIf dt.Rows.Count > 0 Then
                End If

            End If

        Next
        'Sum Balance
        Dim SumBalanceNoCR1CR2 As Double = TempInvoice.GetBillNoSumBalance1(BillShow, dt1)
        Dim SumBalanceCR1CR2 As Double = TempInvoice.GetBillNoSumBalance2(BillShow, dt2)
        Dim TotalSumAmount As Double = SumBalanceNoCR1CR2 - SumBalanceCR1CR2
        '--convert Amount Text 
        SpellNumber(TotalSumAmount)
        '--Update SumAmountBalance
        TempInvoice.GetTotalSumAmount(TotalSumAmount, BillShow)
        '--Update UpdateAmountText
        TempInvoice.GetAmountText(AmountText, BillShow)



    End Sub

    '--ShowScollbar สกอบาร์
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvAddDataScrollbar", "GvAddDataScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvDeleteScrollbar", "GvDeleteScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvEditScrollbar", "GvEditScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvEdit1Scrollbar", "GvEdit1Scrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvReprotScrollbar", "GvReprotScrollbar();", True)
    End Sub

    '--Converts a number
    Function SpellNumber(ByVal MyNumber)
        Dim Dollars, Cents, Temp
        Dim DecimalPlace, Count
        Dim Place(9) As String

        Place(2) = " THOUSAND "
        Place(3) = " MILLION "
        Place(4) = " BILLION "
        Place(5) = " TRILLION "
        ' String representation of amount.
        MyNumber = Trim(Str(MyNumber))
        ' Position of decimal place 0 if none.
        DecimalPlace = InStr(MyNumber, ".")
        ' Convert cents and set MyNumber to dollar amount.
        If DecimalPlace > 0 Then
            Cents = GetTens(Left(Mid(MyNumber, DecimalPlace + 1) &
                      "00", 2))
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If
        Count = 1
        Do While MyNumber <> ""
            Temp = GetHundreds(Right(MyNumber, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(MyNumber) > 3 Then
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = ""
            End If
            Count = Count + 1
        Loop
        Select Case Dollars
            Case ""
                Dollars = "NO DOLLARS"
            Case "One"
                Dollars = "ONE DOLLARS"
            Case Else
                Dollars = Dollars '& " Dollars"
        End Select
        Select Case Cents
            Case ""
                'Cents = " and No Cents"
            Case "One"
                Cents = " AND ONE CENT"
            Case Else
                Cents = " AND " & Cents & " CENTS"
        End Select
        lblEnglishBaht.Text = Dollars & Cents & " ONLY"
        Edtlblbathedit.Text = Dollars & Cents & " ONLY" ' -- อยุ่หน้าแก้ไข
    End Function

    ' Converts a number from 100-999 into text
    Function GetHundreds(ByVal MyNumber)
        Dim Result As String
        If Val(MyNumber) = 0 Then Exit Function
        MyNumber = Right("000" & MyNumber, 3)
        ' Convert the hundreds place.
        If Mid(MyNumber, 1, 1) <> "0" Then
            Result = GetDigit(Mid(MyNumber, 1, 1)) & " HUNDRED "
        End If
        ' Convert the tens and ones place.
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & GetTens(Mid(MyNumber, 2))
        Else
            Result = Result & GetDigit(Mid(MyNumber, 3))
        End If
        GetHundreds = Result
    End Function

    ' Converts a number from 10 to 99 into text.
    Function GetTens(ByVal TensText)
        Dim Result As String
        Result = ""           ' Null out the temporary function value.
        If Val(Left(TensText, 1)) = 1 Then   ' If value between 10-19...
            Select Case Val(TensText)
                Case 10 : Result = "TEN"
                Case 11 : Result = "ELEVEN"
                Case 12 : Result = "TWELVE"
                Case 13 : Result = "THIRTEEN"
                Case 14 : Result = "FOURTEEN"
                Case 15 : Result = "FIFTEEN"
                Case 16 : Result = "SIXTEEN"
                Case 17 : Result = "SEVENTEEN"
                Case 18 : Result = "EIGHTEEN"
                Case 19 : Result = "NINETEEN"
                Case Else
            End Select
        Else                                 ' If value between 20-99...
            Select Case Val(Left(TensText, 1))
                Case 2 : Result = "TWENTY "
                Case 3 : Result = "THIRTY "
                Case 4 : Result = "FORTY "
                Case 5 : Result = "FIFTY "
                Case 6 : Result = "SIXTY "
                Case 7 : Result = "SEVENTY "
                Case 8 : Result = "EIGHTY "
                Case 9 : Result = "NINETY "
                Case Else
            End Select
            Result = Result & GetDigit _
                (Right(TensText, 1))  ' Retrieve ones place.
        End If
        GetTens = Result
    End Function

    ' Converts a number from 1 to 9 into text.
    Function GetDigit(ByVal Digit)
        Select Case Val(Digit)
            Case 1 : GetDigit = "ONE"
            Case 2 : GetDigit = "TWO"
            Case 3 : GetDigit = "THREE"
            Case 4 : GetDigit = "FOUR"
            Case 5 : GetDigit = "FIVE"
            Case 6 : GetDigit = "SIX"
            Case 7 : GetDigit = "SEVEN"
            Case 8 : GetDigit = "EIGHT"
            Case 9 : GetDigit = "NINE"
            Case Else : GetDigit = ""
        End Select
    End Function

    '##>>>---------------------------------------Delete Data---------------------------------------------<<<##
    '--Search BillNo
    Protected Sub DletbtnSearch_Click(sender As Object, e As EventArgs) Handles DletbtnSearch.Click
        ShowGvDelete(GvDelete, True, NowPageIndex.Text)
        CountRow2.RowCount = ContDtFormOrl.RowGridview(GvDelete)
    End Sub

    '--ShowGvDelete
    Private Sub ShowGvDelete(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim BillNo As String = DlettxtBill.Text
        If DletbtnSearch.Text = "" Then
        Else
            Exit Sub
        End If

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "ID,InvoiceNo,OrderDate,DueDate,Amount,Paid"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DT3 As New Data.DataTable
        ContDtFormOrl.DataTableTranf(ArrayShowFiled, DT3)

        Dim dt, dt1 As New DataTable
        TempInvoice.GetBillNohead(BillNo, dt)

        For L_count As Integer = 0 To dt.Rows.Count - 1
            Dim BillNohead As String = dt.Rows(L_count).Item("BillNo")

            TempInvoice.GetBillNoLine(BillNohead, dt1)
            Dim ID As String = dt.Rows(L_count).Item("ID")
            Dim InvoiceNo As String = dt.Rows(L_count).Item("InvoiceNo")
            Dim OrderDate As String = dt.Rows(L_count).Item("OrderDate")
            Dim DueDate As String = dt.Rows(L_count).Item("DueDate")
            Dim Amount As String = dt.Rows(L_count).Item("Amount")
            Dim Paid As String = dt.Rows(L_count).Item("Paid")

            DT3.Rows.Add(New Object() {ID, InvoiceNo, OrderDate, DueDate, Amount, Paid})

        Next
        'เอาเข้า Gridview 
        GvDelete.DataSource = DT3
        GvDelete.DataBind()

        Dim a As Integer = 0
        a = GvDelete.Rows.Count
        If a = 0 Then
        Else
            '--ShowButton
            DletbtnSave.Visible = True

            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvDelete, "#7B68EE")
        End If
    End Sub

    '--Delete Data
    Protected Sub DletbtnSave_Click(sender As Object, e As EventArgs) Handles DletbtnSave.Click
        Dim dt, dt1 As New DataTable
        For M_count As Integer = 0 To GvDelete.Rows.Count - 1
            Dim ID As String = GvDelete.Rows(M_count).Cells(1).Text
            ChkbSelect = GvDelete.Rows(M_count).FindControl("DletChkb")
            If ChkbSelect IsNot Nothing AndAlso ChkbSelect.Checked = False Then
            ElseIf ChkbSelect.Checked = True Then
                '--Delete BillLine
                TempInvoice.GetID(ID)
            End If
        Next
        'Sum Balance
        Dim SumBalanceNoCR1CR2 As Double = TempInvoice.GetBillNoSumBalance1(BillNohead, dt)
        '--convert Amount Text 
        SpellNumber(SumBalanceNoCR1CR2)
        '--Update SumAmountBalance
        TempInvoice.GetTotalSumAmount(SumBalanceNoCR1CR2, BillNohead)
        '--Update UpdateAmountText
        TempInvoice.GetAmountText(AmountText, BillNohead)
    End Sub

    '##>>>---------------------------------------Edit   Data---------------------------------------------<<<##
    '--EmployeeID Section Sales
    Private Sub EditEmployeeID()
        Dim Dt As DataTable = OOAG.GetEmployeeIDOralddl()
        With EdtDDLModfy
            .DataSource = Dt
            .DataValueField = "ooag001"
            .DataTextField = "EmpID"
            .DataBind()
        End With
    End Sub

    '--Search 
    Protected Sub EdtbtnSearch_Click(sender As Object, e As EventArgs) Handles EdtbtnSearch.Click
        ShowGvEdit(GvEdit, True, NowPageIndex.Text)
        CountRow3.RowCount = ContDtFormOrl.RowGridview(GvEdit)
    End Sub

    '--ShowGvEdit
    Private Sub ShowGvEdit(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)

        If EdtlblMonth.Text = "" Or EdttxtCust.Text = "" Then
            Exit Sub
        End If

        Dim custid As String = EdttxtCust.Text.Trim,
            Month As String = "/" & EdtlblMonth.Text
        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "BillShow,CustommerID,CustName,Date"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DT3 As New Data.DataTable
        ContDtFormOrl.DataTableTranf(ArrayShowFiled, DT3)

        Dim dt, dt1 As New DataTable
        TempInvoice.GetCustIDMonth(custid, Month, dt)

        For L_count As Integer = 0 To dt.Rows.Count - 1
            Dim BillShow As String = dt.Rows(L_count).Item("BillShow")
            Dim CustommerID As String = dt.Rows(L_count).Item("CustommerID")
            Dim CustName As String = dt.Rows(L_count).Item("CustName")
            Dim BillDate As String = dt.Rows(L_count).Item("Date")


            DT3.Rows.Add(New Object() {BillShow, CustommerID, CustName, BillDate})

        Next
        'เอาเข้า Gridview 
        GvEdit.DataSource = DT3
        GvEdit.DataBind()

        Dim a As Integer = 0
        a = GvEdit.Rows.Count
        If a = 0 Then
        Else

            '--AutoGenaerateDate
            EdtlblDate.Text = Date.Now.ToString("dd/MM/yyyy")

            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvEdit, "#7B68EE")
        End If
    End Sub

    '--Control GvEdit
    Protected Sub GvEdit_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvEdit.RowCommand
        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            EdtlblBill.Text = GvEdit.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
            EdtlblCustID.Text = GvEdit.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            EdtCustName.Text = GvEdit.Rows(i).Cells(2).Text.Replace("&nbsp;", "")

            Dim dt As New DataTable
            Dim BillNo As String = TempInvoice.GetBillNohead(EdtlblBill.Text, dt)
            Edtlblbillno.Text = BillNo

        End If
    End Sub

    '--Search 1
    Protected Sub EdtbtnSearch1_Click(sender As Object, e As EventArgs) Handles EdtbtnSearch1.Click
        ShowGvEdit1(GvEdit1, True, NowPageIndex.Text)
        CountRow4.RowCount = ContDtFormOrl.RowGridview(GvEdit1)
    End Sub

    '--ShowGvEdit1
    Private Sub ShowGvEdit1(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)

        If DateT1003.Text = "" Or DateT1004.Text = "" Then
            Exit Sub
        ElseIf EdtlblCustID.Text = "" Then
            Exit Sub
        End If

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "Date,Invoice,PlanReceiveDate,Amount(Not Including Tax),Tax,Amount( Including Tax),Collected"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12 As New Data.DataTable
        ContDtFormOrl.DataTableTranf(ArrayShowFiled, dt)

        '--Checking CustIDMain, EmployeeID,DateFrom,DateTo from  Invoice Main table.
        ISAF.GetBillInvoiceOral(EdtlblCustID.Text, DateT1003.dateText, DateT1004.dateText, dt1)
        For j As Integer = 0 To dt1.Rows.Count - 1
            If dt1.Rows.Count > 0 Then
                Dim InvToSearch As String = dt1.Rows(j).Item("isafdocno")
                TempInvoice.GetBillSearch(InvToSearch, dt9)
            End If
            '--เทียบ BillInv
            TmpBillSearch.CheckingBillInvoice(dt11)
            For l As Integer = 0 To dt11.Rows.Count - 1
                Dim BillInvSearch As String = ""
                If dt11.Rows.Count > 0 Then
                    BillInvSearch = dt11.Rows(l).Item("InvoiceNo")
                Else
                End If
                ISAF.GetBillInvSearchSQL(BillInvSearch, dt10)
            Next
        Next

        'วนเช็คข้อมูล ที่ต้องการนำมาโชว์
        For i As Integer = 0 To dt10.Rows.Count - 1
            Dim KeyCustIDInv As String = "",
                KeyEmpSLInv As String = "",
                KeyInv As String = "",
                KeyInvDate As String = ""
            If dt10.Rows.Count > 0 Then
                KeyCustIDInv = dt10.Rows(i).Item("isaf003")
                KeyInv = dt10.Rows(i).Item("isafdocno")
                KeyInvDate = dt10.Rows(i).Item("isaf014")
                KeyEmpSLInv = dt10.Rows(i).Item("isafcrtid") '--EmployeeID Sales Create Sales Invoice
            Else
                Exit Sub
            End If

            '--Checking EmployeeID from EmployeeID Main table EmployeeID Sales noly.
            OOAG.GetEmployeeIDOral(KeyEmpSLInv, dt2)
            Dim KeyEmployeeID As String = dt2.Rows(0).Item("ooag001")

            '--Checking CustomerID from CustomerMain table.
            PMAA.GetCustAddressOral(KeyCustIDInv, dt3)
            Dim KeyCustIDMain As String = "",
                KeyCustMain As String = ""
            If dt3.Rows.Count > 0 Then
                KeyCustIDMain = dt3.Rows(0).Item("pmaa001")
                KeyCustMain = dt3.Rows(0).Item("pmaa027")
            End If

            '--Checking CustomerID from CustBodyL table.
            PMAAL.GetDataCustomer(KeyCustIDMain, dt4)
            Dim CustomerName As String = "",
                CustomerFullName As String = ""
            If dt4.Rows.Count > 0 Then
                CustomerName = dt4.Rows(0).Item("pmaal004")
                CustomerFullName = dt4.Rows(0).Item("pmaal003")
            End If

            '--Checking KeyCustMain from CustBody table.
            OOFB.GetAddressCustOral(KeyCustMain, dt5)
            Dim AddressCust As String = ""
            If dt5.Rows.Count > 0 Then
                AddressCust = dt5.Rows(0).Item("oofb017")
            End If

            '--Checking CustIDInv, EmpSL from Maintain Shipping Receivables table.
            XRCA.GetCollectedAmountInvoice(KeyCustIDMain, KeyEmpSLInv, KeyInv, dt6)
            Dim KeyInvMSR As String = "",
                KeyEmpIDSLMSR As String = "",
                Payment As String = "",
                ARDue As String = "",
                Inv As String = "",
                AMTBeforeTax As String = "",
                Tex As String = "",
                ARAmt As String = ""
            For e As Integer = 0 To dt6.Rows.Count - 1
                If dt6.Rows.Count > 0 Then
                    KeyARNo = dt6.Rows(e).Item("xrcadocno")
                    KeyInvMSR = dt6.Rows(e).Item("xrca018") '--Inv Appoved only(JP61EX-20170100040).
                    KeyEmpIDSLMSR = dt6.Rows(e).Item("xrca014")
                    Payment = dt6.Rows(e).Item("xrca008")
                    ARDue = dt6.Rows(e).Item("xrca009") '--Plan Receive Date
                    AMTBeforeTax = dt6.Rows(e).Item("xrca113") '--Amount(Not Including Tax)
                    Tex = dt6.Rows(e).Item("xrca114")
                    ARAmt = dt6.Rows(e).Item("xrca118") '--AR Amt.(Local Curr.) 
                    Inv = dt6.Rows(e).Item("xrca066").ToString  '--Inv Appoved only (EX-17010040).
                End If
            Next

            '--Checking ARNo from Maintain Shipping Receivables
            XRCE.GetARNo(KeyARNo, dt7)
            Dim BalanInLocalCorr As String = ""
            If dt7.Rows.Count > 0 Then
                BalanInLocalCorr = dt7.Rows(0).Item("xrce119") '--Collected
            End If

            '--Checking KeyInvMSR, KeyEmpIDSLMSR, KeyInvDate from
            ISAT.GetInvoiceDate(KeyInvMSR, KeyEmpIDSLMSR, KeyInvDate, dt8)
            Dim InvDate As String = ""
            If dt8.Rows.Count > 0 Then
                InvDate = dt8.Rows(0).Item("isat007")
            End If

            If dt6.Rows.Count > 0 Then
                '--Add data into rows
                dt.Rows.Add(New Object() {InvDate, Inv, ARDue, AMTBeforeTax, Tex, ARAmt, BalanInLocalCorr})
            End If
        Next

        'เอาเข้า Gridview 
        GvEdit1.DataSource = dt
        GvEdit1.DataBind()

        Dim a As Integer = 0
        a = GvEdit1.Rows.Count
        If a = 0 Then
        Else

            '--ShowButton
            EdtbtnSave.Visible = True

            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvEdit1, "#7B68EE")
        End If
    End Sub

    '--Save GvEdit1
    Protected Sub EdtbtnSave_Click(sender As Object, e As EventArgs) Handles EdtbtnSave.Click
        Dim CreateBy As String = Session("UserName")
        '--DetaillBody
        Dim BillNo As String = Edtlblbillno.Text.Substring(3, 9),
            CustID As String = EdtlblCustID.Text,
            BillDate As String = EdtlblDate.Text,
            AmountText As String = Edtlblbathedit.Text,
            ModifyBy As String = EdtDDLModfy.SelectedValue

        Dim dt, dt1, dt2 As New DataTable
        For i = 0 To GvEdit1.Rows.Count - 1
            '--DetaillGvAddData
            Dim Inv As String = GvEdit1.Rows(i).Cells(2).Text,
                DueDate As String = GvEdit1.Rows(i).Cells(3).Text,
                AmtBeforeTax As Decimal = GvEdit1.Rows(i).Cells(4).Text,
                Tex As Decimal = GvEdit1.Rows(i).Cells(5).Text,
                ARAmt As Decimal = GvEdit1.Rows(i).Cells(6).Text,
                Paid As Decimal = GvEdit1.Rows(i).Cells(7).Text

            Dim Balance As Decimal = "",
                ValuesType As String = "",
                CR As Double = "",
                Type As String = Inv.Substring(1, 2),
                ShowAmount As String = "",
                ShowBalance As String = "",
                SPaid As String = ""

            ChkbSelect = GvEdit1.Rows(i).FindControl("EdtChkb")
            '--ถ้ามีการติ๊กข้อมูล
            If ChkbSelect.Checked = True Then
                '--บันทึกข้อมุลลง TempBillhead

                Balance = ARAmt - Paid

                If Type = "CR1" Or Type = "CR2" Then
                    CR = Balance
                    ValuesType = "-"
                ElseIf Paid = "0.00" Then
                    SPaid = ""
                ElseIf Paid <> "0.00" Then
                    SPaid = Paid
                Else
                    ValuesType = ""
                End If

                ShowAmount = ValuesType & ARAmt
                ShowBalance = ValuesType & Balance

                '--Chack Inv Not EXISTS
                TempInvoice.selctInvAll(Inv, dt)
                If dt.Rows.Count = 0 Then
                    '--บันทึกข้อมุลลง TempBillLine
                    TempInvoice.InserBillLine(BillNo, Inv, CustID, BillDate, DueDate, ARAmt, ShowAmount, Balance, ShowBalance, SPaid, Paid)
                ElseIf dt.Rows.Count > 0 Then
                End If

            End If

        Next
        '--Update ModifyBy InTO TempBillhead
        Dim UpdataModifyBy As String = TempInvoice.GetUpdateModifyBy(ModifyBy, BillNo)
        'Sum Balance
        Dim SumBalanceNoCR1CR2 As Double = TempInvoice.GetBillNoSumBalance1(BillNo, dt1)
        Dim SumBalanceCR1CR2 As Double = TempInvoice.GetBillNoSumBalance2(BillNo, dt2)
        Dim TotalSumAmount As Double = SumBalanceNoCR1CR2 - SumBalanceCR1CR2
        '--convert Amount Text 
        SpellNumber(TotalSumAmount)
        '--Update SumAmountBalance
        TempInvoice.GetTotalSumAmount(TotalSumAmount, BillNo)
        '--Update UpdateAmountText
        TempInvoice.GetAmountText(AmountText, BillNo)
        EdtclearDate()
    End Sub

    '--clearDate
    Sub EdtclearDate()
        EdtlblBill.Text = False
        Edtlblbillno.Text = False
        EdtlblCustID.Text = False
        EdtCustName.Text = False
        EdtlblDate.Text = False
        Edtlblbathedit.Text = False
        EdtDDLModfy.SelectedValue = False
    End Sub

    '##>>>---------------------------------------Print Report---------------------------------------------<<<##

    '--EmployeeID Section Sales
    Private Sub ReptEmployeeID()
        Dim Dt As DataTable = OOAG.GetEmployeeIDOralddl()
        With ReptddlBillBy
            .DataSource = Dt
            .DataValueField = "ooag001"
            .DataTextField = "EmpID"
            .DataBind()
        End With
    End Sub

    '--Saerch custid
    Protected Sub ReptbtnSearchCust_Click(sender As Object, e As EventArgs) Handles ReptbtnSearchCust.Click
        Dim dt As New DataTable
        If RepttxtCustID.Text = "" Or RepttxtMonth.Text = "" Then
            Exit Sub
        ElseIf RepttxtCustID.Text <> "" Or RepttxtMonth.Text <> "" Then
            Dim Month As String = "/" & RepttxtMonth.Text
            TempInvoice.GetCustIDMonth(RepttxtCustID.Text, Month, dt)
            If dt.Rows.Count > 0 Then
                ContDtFormOrl.ShowGridViewSQL(GvReprot, dt)
            ElseIf dt.Rows.Count = 0 Then
            End If
        End If
    End Sub

    '--Saerch billby
    Protected Sub ReptbtnSearchBiilBy_Click(sender As Object, e As EventArgs) Handles ReptbtnSearchBiilBy.Click
        Dim BillBy As String = ReptddlBillBy.Text
        Dim dt As New DataTable
        TempInvoice.GetBillBy(BillBy, dt)
        If dt.Rows.Count > 0 Then
            ContDtFormOrl.ShowGridViewSQL(GvReprot, dt)
        ElseIf dt.Rows.Count = 0 Then
        End If
    End Sub

    '--Print Report
    Protected Sub GvReprot_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvReprot.RowCommand
        Dim dt As New DataTable
        Dim i As Integer = e.CommandArgument,
                    BillNo As String = GvReprot.Rows(i).Cells(0).Text
        If e.CommandName = "OnClick" Then
            Dim BillShow As String = TempInvoice.GetBillNohead(BillNo, dt)
            Dim ID As Integer = BillShow
            ReptlblBillShow.Text = ID

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIST100&ReportName=Billing.rpt&BillNo=" + ReptlblBillShow.Text + "');", True)
        End If


    End Sub
End Class
