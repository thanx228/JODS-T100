Public Class TempBillSearch
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    Private Const CheckingBillInv As String = "select distinct InvoiceNo from  TempBillSearch " &
        " where Not EXISTS(select * from TempBillLine  where TempBillSearch.InvoiceNo = TempBillLine.InvoiceNo)"
    Public Function CheckingBillInvoice(ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = CheckingBillInv
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function
End Class
