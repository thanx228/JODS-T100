Imports System.Data
Imports System.Data.OracleClient
Public Class OOBX
    '''<reamrks>Table Invoice</reamrks>
    Public Shared TblAllType As String = "oobx_t"
    '''<reamrks> Column </reamrks>
    Public Shared DocTypeNo As String = "oobx001" '--TypSalesOrder
    Public Shared DocTypePage As String = "oobx003" '--Document Type page.
    Public Shared ent As String = "oobxent"
    '''<reamrks> Velue </reamrks>'''
    Public Shared UnaAppoved As String = "N"
    Public Shared Appoved As String = "Y"
    Public Shared SalesOrderMaintenance As String = "axmt500"
    Public Shared WStandard As String = ent & " ='3' "

    '--Page SalesOrderChangeStatus
    '--Show Type Sales Order
    Private Shared TypSalesOrder As String = "select " & DocTypeNo & "," & OOBXL.DocType & "," & DocTypeNo & " || ' : ' || " & OOBXL.DocType & " as SOType from " & TblAllType & " " &
        " LEFT JOIN " & OOBXL.tblDocType & " On " & DocTypeNo & " = " & OOBXL.DocTypeId & " " &
        " where  " & WStandard & "and " & OOBXL.wStandard & "and " & OOBXL.enUS & "and  " & DocTypePage & " ='" & SalesOrderMaintenance & "'"
    Public Shared Function SalesOrderTypeOral() As Data.DataTable
        Dim Oral As String = TypSalesOrder
        Dim dt As New DataTable
        Dim dtAdapter = New OracleDataAdapter(Oral, clsDBConnect.strT100ConnectionString)
        dtAdapter.Fill(dt)
        Return dt
    End Function

    '--Show Type Sales Order
    Private Shared SOTypeChkb As String = "select " & DocTypeNo & "," & OOBXL.DocType & "," & DocTypeNo & " || ' : ' || " & OOBXL.DocType & " as SOType from " & TblAllType & " " &
        " LEFT JOIN " & OOBXL.tblDocType & " On " & DocTypeNo & " = " & OOBXL.DocTypeId & " " &
        " where  " & ent & " ='3'and " & OOBXL.ent & " ='3' and  " & DocTypePage & " ='" & SalesOrderMaintenance & "'"
    Public Shared Function SelecSOTypeChkb() As Data.DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(SOTypeChkb, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Sale DocType DataSet</remarks>
    Public Shared Function SelecSOTypeChkbDataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(SOTypeChkb, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    'add by noi start
    Public Shared Function getDocType(whr As String) As DataTable
        Dim SQL As String = " select " & DocTypeNo & "," & DocTypeNo & " || ' : ' || " & OOBXL.DocType & " " & OOBXL.DocType & " from " & TblAllType &
                            " left join " & OOBXL.tblDocType & " on " & OOBXL.ent & "=" & ent & " and " & OOBXL.DocTypeId & "=" & DocTypeNo &
                            " where " & OOBXL.Language & "='en_US' And " & ent & " = '3' " & whr & " order by " & DocTypeNo
        Dim dt As DataTable = GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe)
        Return dt
    End Function
    'add by noi end

    Private Shared dtAdapter As OracleDataAdapter
    Private Shared objConn As OracleConnection
    Private Shared Sub T100Close()
        If objConn.State = ConnectionState.Open Then objConn.Close()
        dtAdapter = Nothing
        objConn = Nothing
    End Sub
End Class
