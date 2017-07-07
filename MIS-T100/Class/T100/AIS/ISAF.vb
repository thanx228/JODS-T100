Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports MySql.Data.MySqlClient
Imports System.Data.OracleClient.OracleType
Public Class ISAF
    '''<reamrks>Table Invoice</reamrks>
    Public Shared TblSelesInvoice As String = "isaf_t"
    '''<reamrks> Column </reamrks>
    Public Shared DataCreatedBy As String = "isafcrtid"
    Public Shared StatementNo As String = "isafdocno"
    Public Shared DocumentDate As String = "isafdocdt"
    Public Shared CustomerID As String = "isaf003"
    Public Shared AccountMember As String = "isaf005"
    Public Shared InvoiceDate As String = "isaf014"
    Public Shared PurchasePartyName As String = "isaf021" '--CustomerName
    Public Shared Status As String = "isafstus"
    Public Shared ent As String = "isafent"
    Public Shared Site As String = "isafsite"

    '''<reamrks> Condition Where </reamrks>
    Public Shared UnaAppoved As String = "N"
    Public Shared Appoved As String = "Y"
    Public Shared wStandard As String = Site & " ='JINPAO'  and " & ent & "='3' "

    '--Page CustomsNew
    '--InvoiceDetail
    Private Shared SelectInvoiceDetail As String =
        "select " & StatementNo & "," & DocumentDate & "," & CustomerID & "," & PMAAL.ContactName & " from " & TblSelesInvoice & "" &
        " left join " & PMAAL.tblCustomerName & " on " & CustomerID & " = " & PMAAL.ContactID & "" &
        " where " &
        " " & wStandard & " and " & Status & "='" & Appoved & "' AND " &
        " " & PMAAL.WStandard & " and " & PMAAL.enUS & " AND " &
        " @Whe group by " & StatementNo & "," & DocumentDate & "," & CustomerID & "," & PMAAL.ContactName & " order by " & StatementNo & ""
    Public Shared Function InvoiceDetail(ByVal Whe As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectInvoiceDetail
        Oral = Oral.Replace("@Whe", Whe)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page BillInvoice
    '--Checking BillInvoice No. Appoved only. From CustID And InvoiceDate
    Private Shared BillInvoiceOral As String = "select " & CustomerID & "," & DataCreatedBy & "," & StatementNo & "," & InvoiceDate & " from " & TblSelesInvoice & " " &
        " where " & ent & "='3' and " & CustomerID & "='@CustomerID'" &
        " and " & InvoiceDate & " between TO_DATE ('@dateFrom', 'yyyy/mm/dd') and TO_DATE ('@dateTo', 'yyyy/mm/dd') and " & Status & " in ('" & Appoved & "')"
    Public Shared Function GetBillInvoiceOral(ByVal CustID As String, ByVal dateFrom As String, ByVal dateTo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = BillInvoiceOral
        Oral = Oral.Replace("@CustomerID", CustID)
        Oral = Oral.Replace("@dateFrom", dateFrom)
        Oral = Oral.Replace("@dateTo", dateTo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Page BillInvoice
    '--Checking BillInvoice No. Appoved only.  Not Exists BillInvSearch
    Private Shared BillInvoiceNotExists As String = "select " & CustomerID & "," & DataCreatedBy & "," & StatementNo & "," & InvoiceDate & " from " & TblSelesInvoice & " " &
        " where " & ent & "='3' and " & StatementNo & "='@StatementNo' and " & Status & " in ('" & Appoved & "')"
    Public Shared Function GetBillInvSearchSQL(ByVal BillInvSearch As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = BillInvoiceNotExists
        Oral = Oral.Replace("@StatementNo", BillInvSearch)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function






















    Private Shared InvoiceOralT100DateBetWeen As String = "Select " & StatementNo & "," & DocumentDate & "," & CustomerID & "," & PurchasePartyName & " from " & TblSelesInvoice & " " &
        " where " & wStandard & " and @pWhereCustomUsing "
    Public Shared Function Get_InvoiceOracleT100(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = InvoiceOralT100DateBetWeen
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            ' GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BETWEEN_PlanStartDate_Body", "Sql = strBETWEEN_PlanStartDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared dtAdapter As OracleDataAdapter
    Private Shared objConn As OracleConnection
    Private Shared Sub T100Close()
        If objConn.State = ConnectionState.Open Then objConn.Close()
        dtAdapter = Nothing
        objConn = Nothing
    End Sub
End Class
