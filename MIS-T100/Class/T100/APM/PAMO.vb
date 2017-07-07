Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class PAMO
    ''' <remarks>
    ''' # Module : APM
    ''' # Table : apmo_t
    ''' # axmt410 : Quotaion (Price Approval) : Pricing by Qty
    ''' </remarks>

    'Private Const strWHCustomerItem As String = "Select pmao001 FROM pmao_t where pmao002 =@WHItemRows"
    '''''<remarks># Warehouse DocType DataTable</remarks>
    'Public Shared Function GetDataCustomerItemRows(ByVal WHCustomerItem As String) As DataTable
    '    Dim strSql As String = strWHCustomerItem
    '    strSql = strSql.Replace("@WHItemRows", "'" & WHCustomerItem & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim dt As New DataTable
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSql, objConn)
    '        dtAdapter.Fill(dt)
    '        Return dt '*** Return DataTable ***
    '    Catch ex As Exception
    '        Return Nothing
    '        ex.Message.ToString()
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function
    ''''<remarks># Warehouse DocType DataSet</remarks>
    'Public Shared Function GetDocTypeSaleDataSet(ByVal WHCustomerItem As String) As DataSet
    '    Dim strSql As String = strWHCustomerItem
    '    strSql = strSql.Replace("@WHItemRows", "'" & WHCustomerItem & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim ds As New DataSet
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSql, objConn)
    '        dtAdapter.Fill(ds)
    '        Return ds '*** Return DataSet ***
    '    Catch ex As Exception
    '        Return Nothing
    '        ex.Message.ToString()
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function


End Class
