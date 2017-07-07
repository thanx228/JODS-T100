Public Class XRCE
    '''<reamrks>Table Maintain Collection Verification Forms</reamrks>
    Public Shared TblMCV As String = "xrce_t"
    '''<reamrks> Column </reamrks>
    Public Shared TransactionNo As String = "xrce003"
    Public Shared BalanInTransCorr As String = "xrce109"
    Public Shared BalanInLocalCorr As String = "xrce119"
    Public Shared ent As String = "xrcaent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "

    '--Page BillInvoice
    '--Checking TransactionNo 
    Private Shared TstionNo As String = "select " & TransactionNo & "," & BalanInTransCorr & "," & BalanInLocalCorr & " from " & TblMCV & " " &
        " where " & WStandard & " and " & TransactionNo & "='@TstNo'"
    Public Shared Function GetARNo(ByVal ARNo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = TstionNo
        Oral = Oral.Replace("@TstNo", ARNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function
End Class
