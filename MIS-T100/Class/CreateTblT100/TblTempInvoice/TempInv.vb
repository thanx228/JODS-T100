Public Class TempInv
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '##--สร้างตาราง TempBillhead
    Sub CreateTempBillhead()
        Dim Temptable As String = "TempBillhead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & Temptable & " ("
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "BillShow varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CustID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "CustName varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address1 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address2 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Date varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Payment varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ModifyBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BeDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillNo)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempBillhead
    Private Const InserTempBillhead As String =
        "insert into TempBillhead (BillNo,CustID,Address1,Address2,Date,Payment,BillShow,CreateBy,CustName,BillBy,BeDate) values" &
        "('@BillNo','@CustID','@Address1','@Address2','@Date','@Payment','@BillShow','@CreateBy','@CustName','@BillBy','@BeDate')"
    Public Function InserBillhead(ByVal BillNo As String, ByVal BillShow As String, ByVal CustID As String,
                                  ByVal CustName As String, ByVal Ass1 As String, ByVal Ass2 As String,
                                  ByVal BillDate As String, ByVal Payment As String, ByVal CreateBy As String,
                                  ByVal BillBy As String, ByVal BeDate As String)
        Dim StrSQL As String = InserTempBillhead
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        StrSQL = StrSQL.Replace("@BillShow", BillShow)
        StrSQL = StrSQL.Replace("@CustID", CustID)
        StrSQL = StrSQL.Replace("@CustName", CustName)
        StrSQL = StrSQL.Replace("@Address1", Ass1)
        StrSQL = StrSQL.Replace("@Address2", Ass2)
        StrSQL = StrSQL.Replace("@Date", BillDate)
        StrSQL = StrSQL.Replace("@Payment", Payment)
        StrSQL = StrSQL.Replace("@CreateBy", CreateBy)
        StrSQL = StrSQL.Replace("@BillBy", BillBy)
        StrSQL = StrSQL.Replace("@BeDate", BeDate)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '--Select BillNo where BillNo
    Private Const SelectBillNohead As String = "select BillNo from TempBillhead where BillNo ='@BillNo'"
    Public Function GetBillNohead(ByVal BillNo As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectBillNohead
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Select BillNo where  CustommerID and Month
    Private Const SelectBillhead As String = "select BillShow,CustommerID,CustName,Date,BillB,EditBy from TempBillhead " &
        " where CustommerID ='@CustommerID' and Date ='%@Month'"
    Public Function GetCustIDMonth(ByVal CustommerID As String, ByVal Month As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectBillhead
        StrSQL = StrSQL.Replace("@CustommerID", CustommerID)
        StrSQL = StrSQL.Replace("@Month", Month)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Update ModifyBy
    Private Const UpdateModifyBy As String = "Update TempBillhead set EditBy ='@ModifyBy'" &
    " where BillNo ='@BillNo'"
    Public Function GetUpdateModifyBy(ByVal ModifyBy As String, ByVal BillNo As String)
        Dim StrSQL As String = UpdateModifyBy
        StrSQL = StrSQL.Replace("@ModifyBy", ModifyBy)
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '--Select BillNo where BillBY
    Private Const SelectBillhead1 As String = " select * from TempBillhead where BillBy = '@BillBy'"
    Public Function GetBillBy(ByVal BillBy As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectBillhead1
        StrSQL = StrSQL.Replace("@BillBy", BillBy)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '**************************************************************************************************************************************************************************************************************************************************************************************************

    '##--สร้างตาราง TempBillLine
    Sub CreateTempBillLine()
        Dim Temptable As String = "TempBillLine"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & Temptable & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "BillNo varchar (20) NOT NULL ,"
        StrSQL = StrSQL & "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "CustID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "OrderDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowAmount nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowBalance nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Paid Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowPaid varchar (150)  DEFAULT '' ,"
        StrSQL = StrSQL & "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "CustPO varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "Remark varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempBillLine
    Private Const InserTempBillLine As String =
        "insert into TempBillLine (BillNo,InvoiceNo,CustID,OrderDate,DueDate,Amount,ShowAmount,Balance,ShowBalance,Paid,ShowPaid) values " &
                                "('@BillNo','@InvoiceNo','@CustID','@OrderDate','@DueDate','@Amount','@ShowAmount','@Balance','@ShowBalance','@Paid','@ShowPaid')"
    Public Function InserBillLine(ByVal BillNo As String, ByVal InvoiceNo As String,
                                  ByVal DueDate As String, ByVal Amount As String,
                                  ByVal Balance As String, ByVal OrderDate As String,
                                  ByVal ShowAmount As String, ByVal ShowBalance As String,
                                  ByVal ShowPaid As String, ByVal Paid As String,
                                  ByVal CustID As String)
        Dim StrSQL As String = InserTempBillLine
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        StrSQL = StrSQL.Replace("@InvoiceNo", InvoiceNo)
        StrSQL = StrSQL.Replace("@CustID", CustID)
        StrSQL = StrSQL.Replace("@OrderDate", OrderDate)
        StrSQL = StrSQL.Replace("@DueDate", DueDate)
        StrSQL = StrSQL.Replace("@Amount", Amount)
        StrSQL = StrSQL.Replace("@ShowAmount", ShowAmount)
        StrSQL = StrSQL.Replace("@Balance", Balance)
        StrSQL = StrSQL.Replace("@ShowBalance", ShowBalance)
        StrSQL = StrSQL.Replace("@Paid", Paid)
        StrSQL = StrSQL.Replace("@ShowPaid", ShowPaid)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '--Select BillInvoice All.
    Private Const SelctInv As String = "select InvoiceNo from TempBillLine where InvoiceNo='@Inv'"
    Public Function selctInvAll(ByVal Inv As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelctInv
        StrSQL = StrSQL.Replace("@Inv", Inv)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Sum Balance BillInvoiceType <> 'CR1' Or 'CR2'
    Private Const SumBalance1 As String = "select BillNo,sum(Balance) from TempBillLine " &
    " where BillNo ='@BillNo' and InvoiceNo.Substring(1,2) <>'CR1' or InvoiceNo.Substring(1,2) <>'CR2'"
    Public Function GetBillNoSumBalance1(ByVal BillNo As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SumBalance1
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Sum Balance BillInvoiceType = 'CR1' Or 'CR2'
    Private Const SumBalance2 As String = "select BillNo,sum(Balance) from TempBillLine " &
    " where BillNo ='@BillNo' and InvoiceNo.Substring(1,2) ='CR1' or InvoiceNo.Substring(1,2) ='CR2'"
    Public Function GetBillNoSumBalance2(ByVal BillNo As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SumBalance2
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Update SumAmountBalance
    Private Const UpdateSumAmount As String = "Update TempBillLine set AmountBalance ='@TotalSumAmount'" &
    " where BillNo ='@BillNo'"
    Public Function GetTotalSumAmount(ByVal TotalSumAmount As String, ByVal BillNo As String)
        Dim StrSQL As String = UpdateSumAmount
        StrSQL = StrSQL.Replace("@TotalSumAmount", TotalSumAmount)
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '--Update UpdateAmountText
    Private Const UpdateAmountText As String = "Update TempBillLine set AmountText ='@AmountText'" &
    " where BillNo ='@BillNo'"
    Public Function GetAmountText(ByVal EnglishBaht As String, ByVal BillNo As String)
        Dim StrSQL As String = UpdateSumAmount
        StrSQL = StrSQL.Replace("@AmountText", EnglishBaht)
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '--AutoGenerateBillInvoice
    Private Const AutoGenerateBillInv As String = "select substring(BillNo,1,6),max(BillNo) from TempBillLine " &
     " where BillNo like '@Type%' group by substring(BillNo,1,6)"
    Public Function GetAutoGenerate(ByVal Type As String, ByRef tempDataTable As Data.DataTable)
        Dim AotoGenerate As String = ""
        Dim StrSQL As String = AutoGenerateBillInv
        StrSQL = StrSQL.Replace("@Type", Type)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
        If tempDataTable.Rows.Count > 0 Then
            AotoGenerate = CInt(tempDataTable.Rows(0).Item("BillNo"))
            AotoGenerate = CInt(AotoGenerate + 1)
        Else
            AotoGenerate = "BI" & Type & "0001"
        End If
        Return AotoGenerate
    End Function

    '--Select BillLine
    Private Const SelectBillLine As String = "select ID,InvoiceNo, OrderDate, DueDate, Amount, Paid from TempBillLine where BillNo ='@BillNo'"
    Public Function GetBillNoLine(ByVal BillNo As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectBillLine
        StrSQL = StrSQL.Replace("@BillNo", BillNo)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Dalete BillLine
    Private Const DeleteBillLine As String = "delete from TempBillLine where ID ='@ID'"
    Public Function GetID(ByVal ID As String)
        Dim StrSQL As String = DeleteBillLine
        StrSQL = StrSQL.Replace("@ID", ID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

    '**************************************************************************************************************************************************************************************************************************************************************************************************

    '##--สร้างตาราง TempBillSearch
    Sub CreateTempBillSearch()
        Dim Temptable As String = "TempBillSearch"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & Temptable & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempBillSearch
    Private Const InserTempBillSearch As String = "insert into TempBillSearch (InvoiceNo) values " &
        "('@InvoiceNo')"
    Private Const SelctBillInv As String = "select InvoiceNo from TempBillSearch where InvoiceNo ='@InvoiceNo'"
    Public Function GetBillSearch(ByVal InvoiceNo As String, ByRef tempDataTable As Data.DataTable)
        Dim SQL As String = SelctBillInv,
            StrSQL As String = InserTempBillSearch
        SQL = SQL.Replace("@InvoiceNo", InvoiceNo)
        GetData.Get_DataReaderSQL(SQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
        For i As Integer = 0 To tempDataTable.Rows.Count - 1
            If tempDataTable.Rows.Count = 0 Then
                StrSQL = StrSQL.Replace("@InvoiceNo", InvoiceNo)
                DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
            ElseIf tempDataTable.Rows.Count > 0 Then
            End If
        Next
    End Function

    '**************************************************************************************************************************************************************************************************************************************************************************************************
End Class
