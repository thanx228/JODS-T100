Imports System.Drawing
Imports System.Globalization

Public Class BillingReceipts
    Inherits System.Web.UI.Page
    Dim ControlFrom As New ConnSQL
    Dim clsDBConnect As New clsDBConnect
    Dim CreateTable As New T100CreateTempTable
    Dim ControlFormT100 As New ControlDataFormT100
    Const BillPurMonth As String = "BillPurMonth"
    Dim chrConn As String = Chr(8)
    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If
            tbDateFromEdit.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateToEdit.Text = Date.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            FromDate.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            ToDate.Text = Date.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateFromDel.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateToDel.Text = Date.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateFromPrint.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateToPrint.Text = Date.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
        End If
        CreateTable.CreateBillPurHead()
        CreateTable.CreateBillPurLine()
        CreateTable.CreateTempBillPurMonth(BillPurMonth)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvSelEdit", "gridviewScrollgvSelEdit();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowEdit1", "gridviewScrollgvShowEdit1();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowDel", "gridviewScrollgvShowDel();", True)
        HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If tbSup.Text = "" Then

            show_message.ShowMessage(Page, "Supplier is null!!!", UpdatePanel1)
            Exit Sub
        End If

        'Show Data
        Dim Programg As New DataTable
        Dim SQL4 = "Select * from pmaal_t where 1=1 And pmaalent= '3' and pmaal001= '" & tbSup.Text.Trim & "'"
        Programg = clsDBConnect.QueryDataTable(SQL4, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)

        If Programg.Rows.Count > 0 Then
            With Programg.Rows(0)
                'txtsupid.Text = .Item("pmaal001")
                'txtname.Text = .Item("pmaal004")
                'txtaddress1.Text = .Item("pmaal003")
                txtsupid.Text = dtRowsFormat.FormatString(Programg, PMAAL.CustomerID)
                txtname.Text = dtRowsFormat.FormatString(Programg, PMAAL.CustomerFullName)
                txtaddress1.Text = dtRowsFormat.FormatString(Programg, PMAAL.CustomerName)
                txtdate.Text = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-US"))
            End With
        Else
            show_message.ShowMessage(Page, "Data Not Found!!!", UpdatePanel1)
            Exit Sub
        End If

        Dim ProgramH As New DataTable
        Dim Number As Integer = 0
        Dim SQL3 = "Select * FROM BillPurHead where BillNo In (Select TOP 1 BillNo FROM BillPurHead  ORDER BY BillNo DESC)"
        ProgramH = clsDBConnect.QueryDataTable(SQL3, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        If ProgramH.Rows.Count > 0 Then
            Number = CInt(ProgramH.Rows(0).Item("BillNo"))
            Number = CInt(Number + 1)
            txtbilling.Text = "BR" & Number.ToString("0000000")
        Else
            txtbilling.Text = "BR0000001"
        End If

        Dim ShowFiled As String = "OrderNumber,SupplierID,InvoiceDate,Invoice,DueDate"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim DT3 As New Data.DataTable
        DataTableTranf(ArrayShowFiled, DT3)
        Dim Program As New DataTable
        Dim SQL2 = "Select * from apbb_t  where 1=1 And apbbent= '3' and TO_CHAR(apbbdocdt,'yyyy/MM/dd') between '" & FromDate.Text.Trim & "' and '" & ToDate.Text.Trim & "' and apbb002='" & txtsupid.Text & "'"
        Program = clsDBConnect.QueryDataTable(SQL2, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)

        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim APBB_Number As String = .Item("apbbdocno")
                'True equl / false not equl
                'ถ้าเท่ากัน จะไม่ต้องแสดงรายการ-> (ไม่เท่ากัน)-> แสดงรายการ
                'Select Case* From apbb_t  Where 1 = 1 And apbbent = '3' and apbbdocdt >='26-DEC-16' and apbbdocdt <='26-DEC-16' 

                Dim APBB_Sup As String = ""
                Dim APBB_InvoiceDate As String = ""
                Dim APBB_Invoice As String = ""
                Dim APBB_Due_Date As String = ""
                Dim APBB_Payment As String = ""
                APBB_Sup = .Item("apbb002")
                APBB_InvoiceDate = CDate(.Item("apbb011")).ToString("yyyy-MM-dd")
                APBB_Invoice = (.Item("apbb010")).ToString
                APBB_Due_Date = CDate(.Item("apbb055")).ToString("yyyy-MM-dd")

                APBB_Payment = .Item("apbb054")
                If APBB_Payment = "001" Then
                    txtpayment.Text = "Cash"
                ElseIf APBB_Payment = "002" Then
                    txtpayment.Text = "30 DAYS"
                ElseIf APBB_Payment = "003" Then
                    txtpayment.Text = "60 DAYS"
                ElseIf APBB_Payment = "004" Then
                    txtpayment.Text = "45 DAYS"
                ElseIf APBB_Payment = "005" Then
                    txtpayment.Text = "90 DAYS"
                ElseIf APBB_Payment = "006" Then
                    txtpayment.Text = "15 DAYS"
                Else
                    txtpayment.Text = "-"
                End If

                'Table BillPurLine(T100MIS)
                Dim BillPurLine As New Data.DataTable
                'BillPurLine = BillLine.GetBillPurLineDoc(APBB_Number)
                Dim SQL = "select * from BillPurLine where 1=1 and InvoiceNo='" & APBB_Number & "'"
                BillPurLine = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)

                Dim BillPurLineNumber As String = ""
                If BillPurLine.Rows.Count > 0 Then


                    'For j As Integer = 0 To BillPurLine.Rows.Count - 1
                    '    With BillPurLine.Rows(j)
                    BillPurLineNumber = IsDBNull(BillPurLine.Rows(0).Item("InvoiceNo"))
                    Dim CLSAPBA As New DataTable
                    'CLSAPBA = APBA.GetPurInvoiceLine(BillPurLineNumber)
                    Dim SQL1 = "select * from apba_t where 1=1 and apbaent='3' and apbadocno= '" & BillPurLineNumber & "'"
                    CLSAPBA = clsDBConnect.QueryDataTable(SQL1, clsDBConnect.T100)
                    clsDBConnect.Close(clsDBConnect.T100)


                    Dim APBA_Amt_Excl As String = ""
                    Dim APBA_Tax As String = ""
                    Dim APBA_Amt_Incl As String = ""

                    If CLSAPBA.Rows.Count > 0 Then
                        APBA_Amt_Excl = CLSAPBA.Rows(0).Item("apba103")
                        APBA_Tax = CLSAPBA.Rows(0).Item("apba104")
                        APBA_Amt_Incl = CLSAPBA.Rows(0).Item("apba105")
                    End If
                    '    End With
                    'Next
                End If
                If BillPurLineNumber = "" Then
                    DT3.Rows.Add(New Object() {APBB_Number, APBB_Sup, APBB_InvoiceDate, APBB_Invoice, APBB_Due_Date})
                End If
            End With
        Next

        ControlFormT100.ShowGridViewT100(gvShowInv, DT3)
        ucCountRow.RowCount = ControlFormT100.rowGridviewT100(gvShowInv)

    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim ChkRows As Integer = 0
        Dim strSql As String = "",
            Reamrk As String = "Contract Accounting & Finance Ext. 140-143"
        For i As Integer = 0 To gvShowInv.Rows.Count - 1
            Dim chkSelect As CheckBox = gvShowInv.Rows(i).Cells(0).FindControl("chkSelect")
            If chkSelect.Checked = True Then
                ChkRows += 1
            End If
        Next


        If ChkRows > 0 Then
            Dim strSqlHead = " insert into BillPurHead (BillNo,SupID,SupName,Address1,Address2,Date,Payment,BillShow,Remark,CreateBy,CreateDate) " &
                 " values('" & txtbilling.Text.Substring(2, 7).Trim & "','" & txtsupid.Text.Trim & "','" & txtname.Text.Trim & "','" & txtaddress1.Text.Trim & "','" & txtaddress2.Text.Trim & "','" & txtdate.Text.Trim.Replace("/", "") & "','" & txtpayment.Text.Trim & "','" & txtbilling.Text.Trim & "','" & Reamrk.Trim & "','580256','" & Date.Now.ToString("yyyyMMdd HH:mm") & "')"
            clsDBConnect.QueryExecuteScalar(strSqlHead, clsDBConnect.MIS2)
        Else
            show_message.ShowMessage(Page, "Please Select Data", UpdatePanel1)
            Exit Sub
        End If

        For i As Integer = 0 To gvShowInv.Rows.Count - 1
            Dim chkSelect As CheckBox = gvShowInv.Rows(i).Cells(0).FindControl("chkSelect")

            If chkSelect.Checked = True Then
                Dim ProgramBillPurLineSum As New DataTable
                Dim Amount As Double = 0.00
                Dim Tax As Double = 0.00
                Dim AmountBal As Double = 0.00
                'sum
                'ProgramBillPurLineSum = APBA.GetPurInvoiceLineSum(gvShowInv.Rows(i).Cells(1).Text)
                Dim SQL = "select sum(apba105),sum(apba104) from apba_t where 1=1 and apbaent='3' and apbadocno= '" & gvShowInv.Rows(i).Cells(1).Text & "'"
                ProgramBillPurLineSum = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For j As Integer = 0 To ProgramBillPurLineSum.Rows.Count - 1
                    With ProgramBillPurLineSum.Rows(j)
                        Amount = .Item(0)
                        Tax = .Item(1)
                        AmountBal = +Amount
                    End With
                Next

                'InvType
                Dim ProgramBillPurHead As New DataTable
                Dim InvType As String = ""
                'ProgramBillPurHead = APBB.GetPurInvoiceDocno(gvShowInv.Rows(i).Cells(1).Text)
                Dim SQL1 = "select * from apbb_t  where 1=1 and apbbent= '3' and apbbdocno='" & gvShowInv.Rows(i).Cells(1).Text & "'"
                ProgramBillPurHead = clsDBConnect.QueryDataTable(SQL1, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For k As Integer = 0 To ProgramBillPurHead.Rows.Count - 1
                    With ProgramBillPurHead.Rows(k)
                        InvType = .Item("apbb008")
                    End With
                Next
                'TypeNO เป็นInvoiceType and InvoiceNumber เก็บเพื่อ?
                'ถ้า TypeNO ไม่เท่ากับ N
                'Amount = Amount * -1
                'Tax = Tax * -1
                'clsCreateTable.CreateBillPurLine()
                Dim StrSqlLine = "insert into BillPurLine(BillNo,InvoiceNo,SupID,OrderDate,DueDate, " &
                    " Amount,Tax,Balance,AmountBalance," &
                    " ShowInvoice,TypeNo,OrderType,CreateBy,CreateDate) values " &
                    " ('" & txtbilling.Text.Substring(2, 7).Trim & "', " &
                     " '" & gvShowInv.Rows(i).Cells(1).Text.Trim & "', " &
                    " '" & gvShowInv.Rows(i).Cells(2).Text.Trim & "', " &
                    " '" & gvShowInv.Rows(i).Cells(3).Text.Replace("-", "").Trim & "', " &
                    " '" & gvShowInv.Rows(i).Cells(5).Text.Replace("-", "").Trim & "', " &
                    " '" & Amount & "', " &
                    " '" & Tax & "', " &
                    " '" & Amount & "', " &
                    " '" & AmountBal & "'," &
                    " '" & gvShowInv.Rows(i).Cells(4).Text.Replace("&nbsp;", "") & "', " &
                    "'" & gvShowInv.Rows(i).Cells(1).Text.Trim & "', " &
                    "'" & InvType.Replace(" ", "") & "','" & Session("UserName") & "','" & Date.Now.ToString("yyyyMMdd HH:mm") & "')"
                clsDBConnect.QueryExecuteScalar(StrSqlLine, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End If
        Next
        Dim StrSum As String = ""
        StrSum = "select SUM(Balance) from BillPurLine where BillNo ='" & txtbilling.Text.Substring(2, 7).Trim & "' "
        Dim SumBal As Double = clsDBConnect.QueryExecuteScalar(StrSum, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        ''Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & txtbilling.Text.Substring(2, 7).Trim & "'"
        clsDBConnect.QueryExecuteScalar(UpdateSumAmount, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
    End Sub

    Function DataTableTranf(ByVal DataTableFiledName() As String, ByRef DT3 As Data.DataTable) As Boolean
        DT3.Clear()
        For L_count As Integer = 0 To DataTableFiledName.Length - 1
            Dim myColumn As New Data.DataColumn
            myColumn.DataType = System.Type.GetType("System.String")
            myColumn.ColumnName = DataTableFiledName(L_count)
            DT3.Columns.Add(myColumn)
        Next

    End Function

    'Edit
    Protected Sub btSearchEdit_V1_Click(sender As Object, e As EventArgs) Handles btSearchEdit_V1.Click
        If tbSupIDEdit.Text = "" Then
            show_message.ShowMessage(Page, "Supplier is null, Please input Data.", UpdatePanel1)
            Exit Sub
        End If

        Dim ShowFiled As String = "BillNo,SupID,SupName,Date"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim DT3 As New Data.DataTable
        DataTableTranf(ArrayShowFiled, DT3)
        'tbSupIDEdit.Text.Trim()
        'tbSupNameEdit.Text.Trim()
        Dim BillPurHead As New DataTable, WHR As String = ""
        'BillPurHead = BillHead.GetBillPurHeadCust((tbSupIDEdit.Text).Trim, "", tbDateFromEdit.Text.Trim, tbDateToEdit.Text.Trim, tbBillNoEdit.Text.Trim)
        WHR = WHR & "and SupID ='" & tbSupIDEdit.Text.Trim & "'"

        If tbBillNoEdit.Text.Trim <> "" Then
            WHR = WHR & "and  BillNo ='" & tbBillNoEdit.Text.Trim & "'"
        End If

        Dim Sql = "SELECT * FROM BillPurHead WHERE 1=1 " & WHR & " and  Date between '" & tbDateFromEdit.Text.Trim.Replace("/", "") & "' and '" & tbDateToEdit.Text.Trim.Replace("/", "") & "' ORDER BY BillNo,SupID DESC"
        BillPurHead = clsDBConnect.QueryDataTable(Sql, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To BillPurHead.Rows.Count - 1
            With BillPurHead.Rows(i)
                Dim BillNo As String = .Item("BillShow")
                Dim SupID As String = .Item("SupID")
                Dim SupName As String = .Item("SupName")
                Dim DocDate As String = .Item("Date")
                DT3.Rows.Add(New Object() {BillNo, SupID, SupName, DocDate})
            End With
        Next
        gvShowEdit1.DataSource = DT3
        gvShowEdit1.DataBind()
        lbCountEdit.Text = gvShowEdit1.Rows.Count
    End Sub
    Protected Sub gvShowEdit1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShowEdit1.RowCommand
        If e.CommandName = "Add" Then
            'Dim k As Integer = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim Rowsel As LinkButton = gvShowEdit1.Rows(rowindex).Cells(0).FindControl(0)
            lbBillNoEdit.Text = gvShowEdit1.Rows(rowindex).Cells(1).Text.Trim
            lbSupEdit.Text = gvShowEdit1.Rows(rowindex).Cells(2).Text.Trim
            lbSupNameEdit.Text = gvShowEdit1.Rows(rowindex).Cells(3).Text.Trim
            lbDateEdit.Text = gvShowEdit1.Rows(rowindex).Cells(4).Text.Trim


            'Dim dt As New DataTable
            Dim ShowFiled As String = "OrderNumber,SupplierID,InvoiceDate,Invoice,DueDate"
            Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
            Dim DT3 As New Data.DataTable
            DataTableTranf(ArrayShowFiled, DT3)
            Dim Program As New DataTable
            'Program = APBB.GetPurInvoiceSup("SU007", "JAN-17")
            'Program = APBB.GetPurInvoiceSup("SU007")
            Dim Sql = "select * from apbb_t  where 1=1 and apbbent= '3' and apbb002 = '" & lbSupEdit.Text & "'"
            Program = clsDBConnect.QueryDataTable(Sql, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            For i As Integer = 0 To Program.Rows.Count - 1
                With Program.Rows(i)
                    'Dim BillNo As String = .Item("BillNo")
                    Dim APBB_Number As String = .Item("apbbdocno")
                    Dim APBB_Sup As String = ""
                    Dim APBB_Date As String = ""
                    Dim APBB_Invoice As String = ""
                    Dim APBB_Due_Date As String = ""

                    APBB_Sup = .Item("apbb002")
                    APBB_Date = .Item("apbb011")
                    APBB_Invoice = .Item("apbb010").ToString
                    APBB_Due_Date = .Item("apbb055")
                    Dim Program1 As New DataTable
                    Dim InvNoLine As String = ""



                    'Program1 = BillLine.GetBillPurLineDoc(APBB_Number)
                    Sql = "select * from BillPurLine where 1=1 and InvoiceNo='" & APBB_Number & "'"
                    Program1 = clsDBConnect.QueryDataTable(Sql, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                    For j As Integer = 0 To Program1.Rows.Count - 1
                        With Program1.Rows(j)
                            InvNoLine = .Item("InvoiceNo")
                        End With
                    Next
                    If InvNoLine = "" Then
                        DT3.Rows.Add(New Object() {APBB_Number, APBB_Sup, APBB_Date, APBB_Invoice, APBB_Due_Date})
                    End If

                End With
            Next
            gvSelEdit.DataSource = DT3
            gvSelEdit.DataBind()
            RowsCount.Text = gvSelEdit.Rows.Count
            Panel2.Visible = True
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowEdit1", "gridviewScrollgvShowEdit1();", True)
    End Sub
    Protected Sub btSaveEdit_Click(sender As Object, e As EventArgs) Handles btSaveEdit.Click

        Dim ChkRows As Integer = 0
        For i As Integer = 0 To gvSelEdit.Rows.Count - 1
            Dim chkSelect As CheckBox = gvSelEdit.Rows(i).Cells(0).FindControl("chkSelectEdit")
            If chkSelect.Checked = True Then
                ChkRows += 1
            End If
        Next

        If ChkRows > 0 Then
            Dim StrSQLHead As String = "update BillPurHead set ChangeBy='" & Session("UserName") & "'," &
                " ChangeDate='" & Date.Now.ToString("yyyyMMdd HH:mm") & "' " &
                " where BillNo='" & lbBillNoEdit.Text.Substring(2, 7).Trim & "'"
            clsDBConnect.QueryExecuteScalar(StrSQLHead, clsDBConnect.MIS2)
        Else
            show_message.ShowMessage(Page, "Please Select Data", UpdatePanel1)
            Exit Sub
        End If

        For i As Integer = 0 To gvSelEdit.Rows.Count - 1
            Dim chkSelect As CheckBox = gvSelEdit.Rows(i).Cells(0).FindControl("chkSelectEdit")
            If chkSelect.Checked = True Then
                Dim ProgramBillPurLineSum As New DataTable
                Dim Amount As Double = 0.00
                Dim Tax As Double = 0.00

                'sum
                'ProgramBillPurLineSum = APBA.GetPurInvoiceLineSum(gvSelEdit.Rows(i).Cells(1).Text)
                Dim Sql = "select sum(apba105),sum(apba104) from apba_t where 1=1 and apbaent='3' and apbadocno= '" & gvSelEdit.Rows(i).Cells(1).Text & "'"
                ProgramBillPurLineSum = clsDBConnect.QueryDataTable(Sql, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For j As Integer = 0 To ProgramBillPurLineSum.Rows.Count - 1
                    With ProgramBillPurLineSum.Rows(j)
                        Amount = .Item(0)
                        Tax = .Item(1)
                    End With
                Next

                'InvType
                Dim ProgramBillPurHead As New DataTable
                Dim InvType As String = ""
                'ProgramBillPurHead = APBB.GetPurInvoiceDocno(gvSelEdit.Rows(i).Cells(1).Text)
                Sql = "select * from apbb_t  where 1=1 and apbbent= '3' and apbbdocno='" & gvSelEdit.Rows(i).Cells(1).Text & "'"
                ProgramBillPurHead = clsDBConnect.QueryDataTable(Sql, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For k As Integer = 0 To ProgramBillPurHead.Rows.Count - 1
                    With ProgramBillPurHead.Rows(k)
                        InvType = .Item("apbb008")
                    End With
                Next

                Dim StrSqlLine = "insert into BillPurLine(BillNo,InvoiceNo,SupID,OrderDate,DueDate, " &
                    " Amount,Tax,Balance," &
                    " ShowInvoice,TypeNo,OrderType,CreateBy,CreateDate) values " &
                    " ('" & lbBillNoEdit.Text.Substring(2, 7).Trim & "', " &
                     " '" & gvSelEdit.Rows(i).Cells(1).Text.Trim & "', " &
                    " '" & gvSelEdit.Rows(i).Cells(2).Text.Trim & "', " &
                    " '" & gvSelEdit.Rows(i).Cells(3).Text.Trim & "', " &
                    " '" & gvSelEdit.Rows(i).Cells(5).Text.Trim & "', " &
                    " '" & Amount & "', " &
                    " '" & Tax & "', " &
                    " '" & Amount & "', " &
                    " '" & gvSelEdit.Rows(i).Cells(4).Text.Replace("&nbsp;", "") & "', " &
                    "'" & gvSelEdit.Rows(i).Cells(1).Text.Trim & "', " &
                    "'" & InvType.Replace(" ", "") & "','" & Session("UserName") & "','" & Date.Now.ToString("yyyyMMdd HH:mm") & "')"
                clsDBConnect.QueryExecuteScalar(StrSqlLine, clsDBConnect.MIS2)
            End If
        Next
        Dim StrSum As String = ""
        StrSum = "select isnull(SUM(Balance),0) from BillPurLine where BillNo ='" & lbBillNoEdit.Text.Substring(2, 7).Trim & "' "
        Dim SumBal As Double = clsDBConnect.QueryExecuteScalar(StrSum, clsDBConnect.MIS2)

        ''Save AmountBalance
        Dim UpdateSumAmount As String
        UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & lbBillNoEdit.Text.Substring(2, 7).Trim & "'"
        clsDBConnect.QueryExecuteScalar(UpdateSumAmount, clsDBConnect.MIS2)
        show_message.ShowMessage(Page, "Update Complete!!!", UpdatePanel1)
    End Sub
    'Delete
    Protected Sub btSearchDel_Click(sender As Object, e As EventArgs) Handles btSearchDel.Click
        ShowDataForDel()
    End Sub
    Private Sub ShowDataForDel()
        Dim ShowFiled As String = "BillNo,SupID,SupName,Date"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim DT3 As New Data.DataTable
        DataTableTranf(ArrayShowFiled, DT3)
        Dim BillPurHead As New DataTable
        Dim WHR As String = ""
        'BillPurHead = BillHead.GetBillPurHeadCust(tbSupplierIDDel.Text.Trim, tbSupplierNameDel.Text.Trim, tbDateFromDel.Text.Trim, tbDateToDel.Text.Trim, tbBillNoDel.Text.Trim)
        If tbSupplierIDDel.Text.Trim <> "" Then
            WHR = WHR & " and SupID = '" & tbSupplierIDDel.Text.Trim & "'"
        End If
        If tbSupplierNameDel.Text.Trim <> "" Then
            WHR = WHR & " and SupName = '" & tbSupplierNameDel.Text.Trim & "'"
        End If
        If tbBillNoDel.Text <> "" Then
            WHR = WHR & " and BillNo= '" & tbBillNoDel.Text.Trim & "'"
        End If


        Dim SQL = "SELECT * FROM BillPurHead WHERE 1=1 " & WHR & " and Date between '" & tbDateFromDel.Text.Trim.Replace("/", "") & "' and '" & tbDateToDel.Text.Trim.Replace("/", "") & "' ORDER BY BillNo,SupID DESC"
        BillPurHead = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To BillPurHead.Rows.Count - 1
            With BillPurHead.Rows(i)
                Dim BillNo As String = Trim(.Item("BillShow"))
                Dim SupID As String = .Item("SupID")
                Dim SupName As String = .Item("SupName")
                Dim DocDate As String = .Item("Date")
                DT3.Rows.Add(New Object() {BillNo, SupID, SupName, DocDate})

            End With
        Next
        gvShowDel.DataSource = DT3
        gvShowDel.DataBind()
        lbCountShowDel.Text = gvShowDel.Rows.Count
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowDel", "gridviewScrollgvShowDel();", True)
    End Sub
    Protected Sub btSaveDel_Click(sender As Object, e As EventArgs) Handles btSaveDel.Click
        Dim ChkRows As Integer = 0
        Dim StrDel As String = ""
        Dim StrSQLHead As String = ""
        For i As Integer = 0 To gvDelete.Rows.Count - 1
            Dim chkSelect As CheckBox = gvDelete.Rows(i).Cells(0).FindControl("chkSelectDel")
            If chkSelect.Checked = True Then
                ChkRows += 1
                StrDel = "Delete from BillPurLine where ID ='" & gvDelete.Rows(i).Cells(1).Text.Trim & "'"
                clsDBConnect.QueryExecuteScalar(StrDel, clsDBConnect.MIS2)
            End If
        Next

        If ChkRows > 0 Then
            StrSQLHead = "update BillPurHead set ChangeBy='" & Session("UserName") & "'," &
                " ChangeDate='" & Date.Now.ToString("yyyyMMdd HH:mm") & "'" &
                " where BillNo='" & lbBillNoDel.Text & "'"
            clsDBConnect.QueryExecuteScalar(StrSQLHead, clsDBConnect.MIS2)
            ShowDataForDel()
            'btSearchDel_Click(e, sender)
            Dim StrSum As String = ""
            StrSum = "select isnull(SUM(Balance),0)  from BillPurLine where BillNo ='" & lbBillNoDel.Text.Trim & "' "
            Dim SumBal As Double = clsDBConnect.QueryExecuteScalar(StrSum, clsDBConnect.MIS2)

            ''Save AmountBalance
            Dim UpdateSumAmount As String
            UpdateSumAmount = "Update BillPurLine set AmountBalance ='" & SumBal & "' where BillNo ='" & lbBillNoDel.Text.Trim & "'"
            clsDBConnect.QueryExecuteScalar(UpdateSumAmount, clsDBConnect.MIS2)
            show_message.ShowMessage(Page, "Update Complete!!!", UpdatePanel1)
        Else
            show_message.ShowMessage(Page, "Please Select Data", UpdatePanel1)
            Exit Sub
        End If
    End Sub
    Protected Sub gvShowDel_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShowDel.RowCommand

        If e.CommandName = "SelDel" Then
            Dim k As Integer = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim Rowsel As LinkButton = gvShowDel.Rows(rowindex).Cells(0).FindControl(0)
            Dim ShowFiled As String = "RefNo,Purchase Invoice,Invoice No,OrderDate,DuaDate,Amount"
            Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
            Dim DT3 As New Data.DataTable
            DataTableTranf(ArrayShowFiled, DT3)
            Dim BillPurLine As New DataTable
            'BillPurLine = BillLine.GetBillPurLineBill(gvShowDel.Rows(rowindex).Cells(1).Text.Substring(2, 7))
            Dim SQL = "select * from BillPurLine where 1=1 and BillNo='" & gvShowDel.Rows(rowindex).Cells(1).Text.Substring(2, 7) & "'"
            BillPurLine = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
            For i As Integer = 0 To BillPurLine.Rows.Count - 1
                With BillPurLine.Rows(i)
                    Dim RefNo As String = .Item("ID")
                    Dim PurchaseInvoice As String = .Item("InvoiceNo")
                    Dim InvoiceNo As String = .Item("ShowInvoice")
                    Dim OrderDate As String = .Item("OrderDate")
                    Dim DueDate As String = .Item("DueDate")
                    Dim Amount As String = .Item("Amount")
                    lbBillNoDel.Text = gvShowDel.Rows(rowindex).Cells(1).Text.Substring(2, 7)
                    lbSupplierDel.Text = gvShowDel.Rows(rowindex).Cells(2).Text & ": " & gvShowDel.Rows(rowindex).Cells(3).Text
                    DT3.Rows.Add(New Object() {RefNo, PurchaseInvoice, InvoiceNo, OrderDate, DueDate, Amount})
                End With
            Next
            gvDelete.DataSource = DT3
            gvDelete.DataBind()
            Panel5.Visible = True
            lbCountDel.Text = gvDelete.Rows.Count
            If gvDelete.Rows.Count > 0 Then
                btSaveDel.Visible = True
            Else
                btSaveDel.Visible = False
            End If
        End If
    End Sub
    Protected Sub btSearchPrint_Click(sender As Object, e As EventArgs) Handles btSearchPrint.Click
        ShowDataForPrint()
    End Sub
    Private Sub ShowDataForPrint()
        Dim ShowFiled As String = "BillNo,SupID,SupName"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim DT3 As New Data.DataTable
        DataTableTranf(ArrayShowFiled, DT3)
        Dim BillPurHead As New DataTable
        Dim WHR As String = ""
        'BillPurHead = BillHead.GetBillPurHeadCust(tbSupplierIDPrint.Text.Trim, tbSupplierNamePrint.Text.Trim, tbDateFromPrint.Text.Trim, tbDateToPrint.Text.Trim, tbBillNoPrint.Text.Trim)

        If tbSupplierIDPrint.Text.Trim <> "" Then
            WHR = WHR & "and SupID ='" & tbSupplierIDPrint.Text.Trim & "'"
        End If
        If tbSupplierNamePrint.Text.Trim <> "" Then
            WHR = WHR & "and SupName='" & tbSupplierNamePrint.Text.Trim & "'"
        End If
        If tbBillNoPrint.Text.Trim <> "" Then
            WHR = WHR & " and BillNo='" & tbBillNoPrint.Text.Trim & "' "
        End If


        Dim SQL = " Select * FROM BillPurHead WHERE 1=1 " & WHR & " And Date between '" & tbDateFromPrint.Text.Trim.Replace("/", "") & "' and '" & tbDateToPrint.Text.Trim.Replace("/", "") & "' ORDER BY BillNo,SupID DESC"
        BillPurHead = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To BillPurHead.Rows.Count - 1
            With BillPurHead.Rows(i)
                Dim BillNo As String = Trim(.Item("BillShow"))
                Dim SupID As String = .Item("SupID")
                Dim SupName As String = .Item("SupName")

                DT3.Rows.Add(New Object() {BillNo, SupID, SupName})
            End With
        Next
        gvShowPrint.DataSource = DT3
        gvShowPrint.DataBind()
        lbCountPrint.Text = gvShowPrint.Rows.Count
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShowPrint", "gridviewScrollgvShowPrint();", True)
    End Sub
    Protected Sub gvShowPrint_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShowPrint.RowCommand
        If e.CommandName = "OnClick" Then
            Dim i As Integer = e.CommandArgument
            Dim BillNo As String = gvShowPrint.Rows(i).Cells(1).Text.Substring(2, 7)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts1.aspx?BillNo=" + BillNo + "');", True)
        End If
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim BillPurHead As New DataTable, WHR1 As String = ""
        'BillPurHead = BillHead.GetBillPurHeadCust(tbSupplierIDPrint.Text.Trim, tbSupplierNamePrint.Text.Trim, tbDateFromPrint.Text.Trim, tbDateToPrint.Text.Trim, tbBillNoPrint.Text.Trim)
        If tbSupplierIDPrint.Text <> "" Then
            WHR1 = WHR1 & "and SupID = '" & tbSupplierIDPrint.Text.Trim & "'"
        End If
        If tbSupplierNamePrint.Text <> "" Then
            WHR1 = WHR1 & "and SupName = '" & tbSupplierNamePrint.Text.Trim & "'"
        End If
        If tbBillNoPrint.Text <> "" Then
            WHR1 = WHR1 & "and BillNo = '" & tbBillNoPrint.Text.Trim & "'"
        End If


        Dim SQL = "SELECT * FROM BillPurHead WHERE 1=1 " & WHR1 & " and Date between '" & tbDateFromPrint.Text.Replace("/", "").Trim & "' and '" & tbDateToPrint.Text.Replace("/", "").Trim & "' ORDER BY BillNo,SupID DESC"
        BillPurHead = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For j As Integer = 0 To BillPurHead.Rows.Count - 1
            With BillPurHead.Rows(j)
                Dim BillNo As String = .Item("BillNo")
                Dim SupCode As String = .Item("SupID")
                Dim SupName As String = .Item("SupName")
                Dim Payment As String = .Item("Payment")
                Dim BillDate As String = .Item("Date")
                Dim BillPurLine As New DataTable
                'BillPurLine = BillLine.GetBillPurLineBill(BillNo)
                Dim strSQL = "select * from BillPurLine where 1=1 and BillNo='" & BillNo & "'"
                BillPurLine = clsDBConnect.QueryDataTable(strSQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
                For i As Integer = 0 To BillPurLine.Rows.Count - 1
                    Dim AmountBalance As String = ""
                    Dim CreateBy As String = ""
                    Dim DueDate As String = ""
                    AmountBalance = BillPurLine.Rows(i).Item("AmountBalance").ToString.Replace(" ", "")
                    CreateBy = BillPurLine.Rows(i).Item("CreateBy").ToString.Replace(" ", "")
                    DueDate = BillPurLine.Rows(i).Item("DueDate").ToString.Replace(" ", "").Replace("-", "")

                    Dim InSQL As String = "Insert into BillPurMonth(BillShow,SupID,Payment,BillDate,AmountBalance,DueDate,CreateBy)"
                    InSQL = InSQL & " Values('" & "BR" & BillNo & "','" & SupCode & "','" & Payment & "',"
                    InSQL = InSQL & "'" & BillDate & "','" & AmountBalance & "','" & DueDate & "',"
                    InSQL = InSQL & "'" & CreateBy & "')"
                    clsDBConnect.QueryExecuteScalar(InSQL, clsDBConnect.MIS2)
                Next
            End With
        Next


        Dim BillShow As String = ""
        Dim SuppID As String = ""
        Dim BillDateS As String = ""
        Dim BillDateE As String = ""

        Dim WHR As String = ""
        'If tbBillNoPrint.Text.Trim <> "" Then
        '    BillShow = "BillShow:and BillShow='" & tbBillNoPrint.Text.Trim & "'"
        '    WHR = WHR & BillShow
        'End If

        If tbDateFromPrint.Text.Trim <> "" Then
            BillDateS = "BillDateS:" & tbDateFromPrint.Text.Replace("/", "").Trim & chrConn & ""
            WHR = WHR & BillDateS
        End If
        If tbDateToPrint.Text.Trim <> "" Then
            BillDateE = "BillDateE:" & tbDateToPrint.Text.Replace("/", "").Trim & ""
            WHR = WHR & BillDateE
        End If
        'If tbSupplierIDPrint.Text.Trim <> "" Then
        '    SuppID = "SupID:and SupID='" & tbSupplierIDPrint.Text.Trim & "'"
        '    WHR = WHR & SuppID
        'End If

        Dim paraName As String = ""
        'paraName = "BillDateS:" & tbDateFromPrint.Text.Trim & chrConn & "BillDateE:" & tbDateToPrint.Text.Trim & chrConn & "SupID: " & tbSupplierNamePrint.Text.Trim
        paraName = WHR
        'Dim S As String = ""
        'Dim E As String = ""
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ReportPurchase.aspx?dbName=MIST100&ReportName=BillingMonth.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('BillingReceipts2.aspx?Paraname=" + tbDateFromPrint.Text.Trim + ",BillDateE=" + tbDateToPrint.Text.Trim + "');", True) '"&Year=" + DropDownList3.SelectedValue +
    End Sub
End Class