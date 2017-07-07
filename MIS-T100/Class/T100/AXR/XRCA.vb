Public Class XRCA
    '''<reamrks>Table Maintain Shipping Receivables</reamrks>
    Public Shared tblMSR As String = "xrca_t"
    '''<reamrks> Column </reamrks>
    Public Shared ARNo As String = "xrcadocno"
    Public Shared Stus As String = "xrcastus"
    Public Shared CustID As String = "xrca005"
    Public Shared Payment As String = "xrca008"
    Public Shared ARDue As String = "xrca009"
    Public Shared EmpIDSL As String = "xrca014" '--UserIDSalesCreateSalesInvoice
    Public Shared Invoice As String = "xrca066" '--EX-17010040
    Public Shared Inv As String = "xrca018" '--JP61EX-20170100040
    Public Shared AmountBeforeTax As String = "xrca113"
    Public Shared Tax As String = "xrca114"
    Public Shared AR As String = "xrca118" '--ราคาบวกภาษี
    Public Shared ent As String = "xrcaent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared Unaappoved As String = Stus & "='N'"
    Public Shared Appoved As String = Stus & "='Y'"

    '--Page BillInvoice
    '--Get Invoice Checking DetailInvoice
    Private Shared CollectedAmountInvoice As String = "select " & ARNo & "," & CustID & "," & CustID & "," & EmpIDSL & "," & Invoice & "," & AmountBeforeTax & "," & Tax & "," & AR & ", " &
    "" & Inv & "," & Payment & "," & ARDue & " from " & tblMSR & " " &
    " WHERE " & WStandard & " AND " & CustID & "='@CustID' AND " & EmpIDSL & "='@EmpIDSL' AND " & Inv & "='@Inv' and " & Appoved & ""
    Public Shared Function GetCollectedAmountInvoice(ByVal CustID As String, ByVal EmpIDSL As String, ByVal Inv As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = CollectedAmountInvoice
        Oral = Oral.Replace("@CustID", CustID)
        Oral = Oral.Replace("@EmpIDSL", EmpIDSL)
        Oral = Oral.Replace("@Inv", Inv)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function
End Class
