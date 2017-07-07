Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports MySql.Data.MySqlClient
Imports System.Data.OracleClient.OracleType
Imports Oracle.DataAccess.Client
'Imports Oracle.ManagedDataAccess
'Imports Oracle.DataAccess.Client
'Imports Oracle.DataAccess.Types
Public Class clsDBConnectT100

    '****** Original Class By Mr.Alex
    '****** Modify by Pattavee Narumonchavalit

    'Declare Constant for difference database
    Public site As String = "JINPAO" 'Operating Locations
    Public entcode As String = "3" 'Enterprise Code

    Public MIS As String = " MIS"
    Public T100 As String = "T100"
    Public ERP As String = "ERP"
    Public KIOSK As String = "KIOSK"
    'Additional DB
    Public MIS2 As String = "MIS2"

    Public objConn As OracleConnection
    Public objCmd As OracleCommand
    Public Trans As OracleTransaction

    Public objmssqlConn As SqlConnection
    Public objmssqlCmd As SqlCommand
    Public mssqlTrans As SqlTransaction

    Public objmysqlConn As MySqlConnection
    Public objmysqlCmd As MySqlCommand
    Public mysqlTrans As MySqlTransaction

    Public Shared SessionName As New Hashtable
    Public Shared CodeNameMapping As New Hashtable



    Public Shared MIS2DBServerName As String = "192.168.50.1"
    Public Shared MIS2DBUserName As String = "sa"
    Public Shared MIS2DBPassWord As String = "Alex0717"
    Public Shared MIS2DBName As String = "DBMIST100"
    Public Shared strMIS2ConnectionString As String = "Data Source=" & MIS2DBServerName &
                                                  ";Initial Catalog= " & MIS2DBName &
                                                  ";User Id=" & MIS2DBUserName &
                                                  ";Password=" & MIS2DBPassWord & ";Max Pool Size=1000"



    Public Shared DBServerName As String = "192.168.1.13"
    Public Shared DBUserName As String = "ERPBasicDataCheck"
    Public Shared DBPassWord As String = "ERPBasicDataCheck"
    Public Shared DBName As String = "ERPBasicDataCheck"
    Public Shared strConnectionString As String = "Data Source=" & DBServerName &
                                                  ";Initial Catalog= " & DBName &
                                                  ";User Id=" & DBUserName &
                                                  ";Password=" & DBPassWord & ";Max Pool Size=1000"

    Public Shared ERPDBServerName As String = "192.168.50.1"
    Public Shared ERPDBUserName As String = "sa"
    Public Shared ERPDBPassWord As String = "Alex0717"
    Public Shared ERPDBName As String = "JINPAO80"
    Public Shared strERPConnectionString As String = "Data Source=" & ERPDBServerName &
                                                  ";Initial Catalog= " & ERPDBName &
                                                  ";User Id=" & ERPDBUserName &
                                                  ";Password=" & ERPDBPassWord & ";Max Pool Size=1000"

    Public Shared MISDBServerName As String = "192.168.50.1"
    Public Shared MISDBUserName As String = "sa"
    Public Shared MISDBPassWord As String = "Alex0717"
    Public Shared MISDBName As String = "DBMIST100"
    Public Shared strMISConnectionString As String = "Data Source=" & MISDBServerName &
                                                  ";Initial Catalog= " & MISDBName &
                                                  ";User Id=" & MISDBUserName &
                                                  ";Password=" & MISDBPassWord & ";Max Pool Size=1000"

    Public Shared PDMDBServerName As String = "dcis"
    Public Shared PDMDBUserName As String = "Alex"
    Public Shared PDMDBPassWord As String = "Alex0717"
    Public Shared PDMDBName As String = "df3_0"
    Public Shared strPDMConnectionString As String = "Data Source=" & PDMDBServerName &
                                                  ";Persist Security Info=True;" &
                                                  ";User Id=" & PDMDBUserName &
                                                  ";Password=" & PDMDBPassWord & ";"

    Public Shared T100DBServerName As String = "JP"
    Public Shared T100DBUserName As String = "dsdemo"
    Public Shared T100DBPassWord As String = "dsdemo"
    'Public Shared T100DBName As String = ""
    Public Shared strT100ConnectionString As String = "Data Source=" & T100DBServerName &
                                                  ";Persist Security Info=True;" &
                                                  ";User Id=" & T100DBUserName &
                                                  ";Password=" & T100DBPassWord & ";"

    Public Shared KioskDBServerName As String = "192.168.1.17"
    Public Shared KioskDBUserName As String = "vserver"
    Public Shared KioskDBPassWord As String = "Jinpa002"
    Public Shared T100DBName As String = "atis304"
    Public Shared strKioskConnectionString As String = "Server=" & KioskDBServerName &
                                                  ";Database=" & T100DBName &
                                                  ";Uid=" & KioskDBUserName &
                                                  ";Pwd=" & KioskDBPassWord & ";"

    Public Function QueryDataSet(ByVal strSQL As String, ByVal DBType As String) As DataSet

        Dim ds As New DataSet
        Dim conn As String = ""

        Select Case DBType

            Case MIS2

                Dim dtAdapter As New SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMIS2ConnectionString
                    .Open()
                End With
                objmssqlCmd = New SqlCommand
                With objmssqlCmd
                    .Connection = objmssqlConn
                    .CommandText = strSQL
                    .CommandType = CommandType.Text
                End With
                dtAdapter.SelectCommand = objmssqlCmd
                dtAdapter.Fill(ds, "DATASET")
                Close(DBType)
                Return ds

            Case MIS

                Dim dtAdapter As New SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMISConnectionString
                    .Open()
                End With
                objmssqlCmd = New SqlCommand
                With objmssqlCmd
                    .Connection = objmssqlConn
                    .CommandText = strSQL
                    .CommandType = CommandType.Text
                End With
                dtAdapter.SelectCommand = objmssqlCmd
                dtAdapter.Fill(ds, "DATASET")
                Close(DBType)
                Return ds

            Case ERP

                Dim dtAdapter As New SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                objmssqlCmd = New SqlCommand
                With objmssqlCmd
                    .Connection = objmssqlConn
                    .CommandText = strSQL
                    .CommandType = CommandType.Text
                End With
                dtAdapter.SelectCommand = objmssqlCmd
                dtAdapter.Fill(ds, "DATASET")
                Close(DBType)
                Return ds

            Case T100

                Dim dtAdapter As New OracleDataAdapter
                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                objCmd = New OracleCommand
                With objCmd
                    .Connection = objConn
                    .CommandText = strSQL
                    .CommandType = CommandType.Text
                End With
                dtAdapter.SelectCommand = objCmd
                dtAdapter.Fill(ds, "DATASET")
                Close(DBType)
                Return ds

            Case KIOSK

                Dim dtAdapter As New MySqlDataAdapter
                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                objmysqlCmd = New MySqlCommand
                With objmysqlCmd
                    .Connection = objmysqlConn
                    .CommandText = strSQL
                    .CommandType = CommandType.Text
                End With
                dtAdapter.SelectCommand = objmysqlCmd
                dtAdapter.Fill(ds, "DATASET")
                Close(DBType)
                Return ds

            Case Else

                Return Nothing

        End Select

    End Function


    Public Function QueryDataTable(ByVal strSQL As String, ByVal DBType As String) As DataTable

        Dim dt As New DataTable
        Select Case DBType
            Case MIS2

                Dim dtAdapter As SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMIS2ConnectionString
                    .Open()
                End With
                dtAdapter = New SqlDataAdapter(strSQL, objmssqlConn)
                dtAdapter.Fill(dt)
                Close(DBType)
                Return dt
            Case MIS

                Dim dtAdapter As SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMISConnectionString
                    .Open()
                End With
                dtAdapter = New SqlDataAdapter(strSQL, objmssqlConn)
                dtAdapter.Fill(dt)
                Close(DBType)
                Return dt

            Case ERP

                Dim dtAdapter As SqlDataAdapter
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                dtAdapter = New SqlDataAdapter(strSQL, objmssqlConn)
                dtAdapter.Fill(dt)
                Close(DBType)
                Return dt

            Case T100

                Dim dtAdapter As OracleDataAdapter
                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                dtAdapter = New OracleDataAdapter(strSQL, objConn)
                dtAdapter.Fill(dt)
                Close(DBType)
                Return dt

            Case KIOSK

                Dim dtAdapter As MySqlDataAdapter
                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                dtAdapter = New MySqlDataAdapter(strSQL, objmysqlConn)
                dtAdapter.Fill(dt)
                Close(DBType)
                Return dt

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function QueryDataReader(ByVal strSQL As String, ByVal DBType As String)

        Select Case DBType

            Case MIS2

                Dim dtreader As SqlDataReader
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMIS2ConnectionString
                    .Open()
                End With
                objmssqlCmd = New SqlCommand(strSQL, objmssqlConn)
                dtreader = objmssqlCmd.ExecuteReader()
                Close(DBType)
                Return dtreader

            Case ERP

                Dim dtreader As SqlDataReader
                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                objmssqlCmd = New SqlCommand(strSQL, objmssqlConn)
                dtreader = objmssqlCmd.ExecuteReader()
                Close(DBType)
                Return dtreader

            Case T100

                Dim dtreader As OracleDataReader
                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                objCmd = New OracleCommand(strSQL, objConn)
                dtreader = objCmd.ExecuteReader()
                Close(DBType)
                Return dtreader

            Case KIOSK

                Dim dtreader As MySqlDataReader
                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                objmysqlCmd = New MySqlCommand(strSQL, objmysqlConn)
                dtreader = objmysqlCmd.ExecuteReader()
                Close(DBType)
                Return dtreader

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function QueryExecuteNonQuery(ByVal strSQL As String, ByVal DBType As String) As Boolean

        Select Case DBType

            Case MIS2

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMIS2ConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    objmssqlCmd.ExecuteNonQuery()
                    Close(DBType)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case MIS

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMISConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    objmssqlCmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                End Try


            Case ERP

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    objmssqlCmd.ExecuteNonQuery()
                    Close(DBType)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case T100

                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                Try
                    objCmd = New OracleCommand()
                    With objCmd
                        .Connection = objConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    objCmd.ExecuteNonQuery()
                    Close(DBType)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case KIOSK

                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                Try
                    objmysqlCmd = New MySqlCommand()
                    With objmysqlCmd
                        .Connection = objmysqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    objmysqlCmd.ExecuteNonQuery()
                    Close(DBType)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function QueryExecuteScalar(ByVal strSQL As String, ByVal DBType As String) As Object

        Dim obj As Object

        Select Case DBType
            Case MIS2

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMIS2ConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    obj = objmssqlCmd.ExecuteScalar()
                    Close(DBType)
                    Return obj
                Catch ex As Exception
                    Return False
                End Try
            Case MIS

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMISConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    obj = objmssqlCmd.ExecuteScalar()
                    Return obj
                Catch ex As Exception
                    Return False
                End Try

            Case ERP

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                Try
                    objmssqlCmd = New SqlCommand()
                    With objmssqlCmd
                        .Connection = objmssqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    obj = objmssqlCmd.ExecuteScalar()
                    Close(DBType)
                    Return obj
                Catch ex As Exception
                    Return False
                End Try

            Case T100

                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                Try
                    objCmd = New OracleCommand()
                    With objCmd
                        .Connection = objConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    obj = objCmd.ExecuteScalar()
                    Close(DBType)
                    Return obj
                Catch ex As Exception
                    Return False
                End Try

            Case KIOSK

                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                Try
                    objmysqlCmd = New MySqlCommand()
                    With objmysqlCmd
                        .Connection = objmysqlConn
                        .CommandType = CommandType.Text
                        .CommandText = strSQL
                    End With
                    obj = objmysqlCmd.ExecuteScalar()
                    Close(DBType)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function TransStart(ByVal DBType As String)

        Select Case DBType

            Case MIS

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strMISConnectionString
                    .Open()
                End With
                mssqlTrans = objmssqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
                Return Nothing

            Case ERP

                objmssqlConn = New SqlConnection
                With objmssqlConn
                    .ConnectionString = strERPConnectionString
                    .Open()
                End With
                mssqlTrans = objmssqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
                Return Nothing

            Case T100

                objConn = New OracleConnection
                With objConn
                    .ConnectionString = strT100ConnectionString
                    .Open()
                End With
                Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted)
                Return Nothing

            Case KIOSK

                objmysqlConn = New MySqlConnection
                With objmysqlConn
                    .ConnectionString = strKioskConnectionString
                    .Open()
                End With
                mysqlTrans = objmysqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
                Return Nothing

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function TransExecute(ByVal strSQL As String, Optional ByVal DBType As String = "") As Boolean

        Select Case DBType

            Case MIS

                objmssqlCmd = New SqlCommand()
                With objmssqlCmd
                    .Connection = objmssqlConn
                    .Transaction = mssqlTrans
                    .CommandType = CommandType.Text
                    .CommandText = strSQL
                End With
                objmssqlCmd.ExecuteNonQuery()

            Case ERP

                objmssqlCmd = New SqlCommand()
                With objmssqlCmd
                    .Connection = objmssqlConn
                    .Transaction = mssqlTrans
                    .CommandType = CommandType.Text
                    .CommandText = strSQL
                End With
                objmssqlCmd.ExecuteNonQuery()

            Case T100

                objCmd = New OracleCommand()
                With objCmd
                    .Connection = objConn
                    .Transaction = Trans
                    .CommandType = CommandType.Text
                    .CommandText = strSQL
                End With
                objCmd.ExecuteNonQuery()

            Case KIOSK

                objmysqlCmd = New MySqlCommand()
                With objmysqlCmd
                    .Connection = objmysqlConn
                    .Transaction = mysqlTrans
                    .CommandType = CommandType.Text
                    .CommandText = strSQL
                End With
                objmysqlCmd.ExecuteNonQuery()

            Case Else

                Return Nothing

        End Select

    End Function

    Public Function TransRollBack(ByVal DBType As String)

        Select Case DBType

            Case MIS
                mssqlTrans.Rollback()
                Return Nothing
            Case ERP
                mssqlTrans.Rollback()
                Return Nothing
            Case T100
                Trans.Rollback()
                Return Nothing
            Case KIOSK
                mysqlTrans.Rollback()
                Return Nothing
            Case Else
                Return Nothing
        End Select

    End Function

    Public Function TransCommit(ByVal DBType As String)

        Select Case DBType

            Case MIS
                mssqlTrans.Commit()
                Return Nothing
            Case ERP
                mssqlTrans.Commit()
                Return Nothing
            Case T100
                Trans.Commit()
                Return Nothing
            Case KIOSK
                mysqlTrans.Commit()
                Return Nothing
            Case Else
                Return Nothing
        End Select

    End Function

    Public Function Close(ByVal DBType As String)

        Select Case DBType
            Case MIS2
                objmssqlConn.Close()
                Return Nothing
            Case MIS
                objmssqlConn.Close()
                Return Nothing
            Case ERP
                objmssqlConn.Close()
                Return Nothing
            Case T100
                objConn.Close()
                Return Nothing
            Case KIOSK
                objmysqlConn.Close()
                Return Nothing
            Case Else
                Return Nothing

        End Select

    End Function

    Public Function getRowNumber(ByVal ds As DataSet) As Integer
        Dim rownum As Integer = 0
        rownum = ds.Tables("DATASET").Rows.Count
        Return rownum

    End Function

End Class
