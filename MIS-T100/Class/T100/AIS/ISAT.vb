Public Class ISAT
    '''<reamrks>Table Body Maintain Shipping Receivables</reamrks>
    Public Shared tblBodyMSR As String = "isat_t"
    '''<reamrks> Column </reamrks>
    Public Shared Invoice As String = "isatdocno"
    Public Shared EmpIDSL As String = "isat019"
    Public Shared InvDate As String = "isat007"
    Public Shared ent As String = "xrcaent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "

    '--Page BillInvoice
    '--Get Invoice,EmpIDSL Checking Invoice Date.
    Private Shared InvoiceDate As String = "select " & Invoice & "," & EmpIDSL & "," & InvDate & " from " & tblBodyMSR & "  " &
    " WHERE " & WStandard & " AND " & Invoice & "='@Invoice' AND " & EmpIDSL & "='@EmpIDSL' and " & InvDate & "='@InvDate' "
    Public Shared Function GetInvoiceDate(ByVal Invoice As String, ByVal EmpIDSL As String, ByVal InvDate As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = InvoiceDate
        Oral = Oral.Replace("@Invoice", Invoice)
        Oral = Oral.Replace("@EmpIDSL", EmpIDSL)
        Oral = Oral.Replace("@InvDate", InvDate)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function
End Class
