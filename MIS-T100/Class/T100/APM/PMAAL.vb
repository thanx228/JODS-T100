Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class PMAAL
    '# Module : APM
    Private Shared APM As String = "APM"
    '# Table : pmaal_t
    '#  Customer
    '''<reamrks>##########Table Customer##############</reamrks>
    Public Shared tblCustomerName As String = "pmaal_t"
    Public Shared CustomerID As String = "pmaal001"
    Public Shared CustomerName As String = "pmaal003"
    Public Shared CustomerFullName As String = "pmaal004"
    Public Shared CustomerFullName2 As String = "pmaal006"
    Public Shared Langauge As String = "pmaal002"

    Public Shared ContactID As String = "pmaal001"
    Public Shared ContactFullName As String = "pmaal003"
    Public Shared ContactName As String = "pmaal004"
    Public Shared ContactFullName1 As String = "pmaal006"
    '.Public Shared Langauge As String = "pmaal002"
    Public Shared ent As String = "pmaalent"


    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared enUS As String = Langauge & " ='en_US' "

    '--Page SalesOrderChangeStatus and Page BillInvoice ContactFullName
    '--Search CustomerID Where CustID from No Rrfesh DataTable
    Private Shared Customer As String = "Select " & ContactID & "," & ContactFullName & "," & ContactName & " FROM " & tblCustomerName & " " &
    " where " & ContactID & " ='@ContactID' and " & WStandard & ""
    Public Shared Function GetDataCustomer(ByVal Vendor As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = Customer
        Oral = Oral.Replace("@ContactID", Vendor)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Search CustomerID Where CustID from Rrfesh DataTable
    Private Shared RefreshCustomer As String = "Select " & ContactID & "," & ContactFullName & "," & ContactName & " FROM " & tblCustomerName & " " &
    " where " & ContactID & " ='@ContactID' and " & WStandard & ""
    Public Shared Function GetDataCustomerRefresh(ByVal Vendor As String)
        Dim Oral As String = RefreshCustomer
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ContactID", Vendor)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function
    'Public Shared ent As String = "pmaalent"
    'Public Shared Name As String = "pmaal003"


    ''''<reamrks> Condition Field </reamrks>
    'Public Shared WStandard As String = ent & " ='3' "
    'Public Shared enUS As String = Langauge & " ='en_US' "

    ''--Page SalesOrderChangeStatus and Page BillInvoice
    ''--Search CustomerID Where CustID from No Rrfesh DataTable
    'Private Shared Customer As String = "Select " & CustomerID & "," & CustomerName & "," & CustomerFullName & " FROM " & tblCustomerName & " " &
    '" where " & CustomerID & " ='@CustID' and " & WStandard & ""
    'Public Shared Function GetDataCustomer(ByVal CustomerID As String, ByRef tempDataTable As Data.DataTable)
    '    Dim Oral As String = Customer
    '    Oral = Oral.Replace("@CustID", CustomerID)
    '    GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    'End Function

    ''--Search CustomerID Where CustID from Rrfesh DataTable
    'Private Shared RefreshCustomer As String = "Select " & CustomerID & "," & CustomerName & "," & CustomerFullName & " FROM " & tblCustomerName & " " &
    '" where " & CustomerID & " ='@CustID' and " & WStandard & ""
    'Public Shared Function GetDataCustomerRefresh(ByVal CustomerID As String)
    '    Dim Oral As String = RefreshCustomer
    '    Dim tempDataTable As New DataTable
    '    Oral = Oral.Replace("@CustID", CustomerID)
    '    GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    '    Return tempDataTable
    'End Function



























    Private Shared strSQLCustomer As String = "Select " & CustomerID & "," & CustomerName & "," & CustomerFullName & " FROM " & tblCustomerName & " " &
    " where " & CustomerID & " =@CustID  and " & Langauge & "='3' and " & ent & "='en_US' "
    Public Shared Function GetDataCustomerDetail(CustomerID As String) As DataTable
        Dim strSQL As String = strSQLCustomer
        strSQL = strSQL.Replace("@CustID", "'" & CustomerID & "'")
        Dim dt As New DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMAAL", "GetDataCustomerDetail", "strSQL = strSQLCustomer", ex.Message)
            Return Nothing
        Finally
            dtAdapter = Nothing
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


End Class
