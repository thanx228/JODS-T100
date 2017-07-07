Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports System.Data.OracleClient.OracleType
Public Class DBClassT100
    Public objConn As OracleConnection
    Public objCmd As OracleCommand
    Public Trans As OracleTransaction
    Public T100ConnString As String
    '-------------必要設定宣告
    Public Shared SessionName As New Hashtable                  '所有使用的session名稱  (名稱,Session名稱)
    Public Shared CodeNameMapping As New Hashtable              '所有code與name對應的資料表     (代號,table)(所有支援語言種類)


    '-------------資料庫連結
    Public Shared DBServerName As String = "192.168.1.13"
    Public Shared DBUserName As String = "ERPBasicDataCheck"
    Public Shared DBPassWord As String = "ERPBasicDataCheck"
    Public Shared DBName As String = "ERPBasicDataCheck"
    Public Shared strConnectionString As String = "Data Source=" & DBServerName &
                                                  ";Initial Catalog= " & DBName &
                                                  ";User Id=" & DBUserName &
                                                  ";Password=" & DBPassWord & ";Max Pool Size=100"

    Public Shared ERPDBServerName As String = "192.168.50.1"
    Public Shared ERPDBUserName As String = "sa"
    Public Shared ERPDBPassWord As String = "Alex0717"
    Public Shared ERPDBName As String = "JINPAO80"
    Public Shared strERPConnectionString As String = "Data Source=" & ERPDBServerName &
                                                  ";Initial Catalog= " & ERPDBName &
                                                  ";User Id=" & ERPDBUserName &
                                                  ";Password=" & ERPDBPassWord & ";Max Pool Size=100"

    Public Shared MISDBServerName As String = "192.168.50.1"
    Public Shared MISDBUserName As String = "sa"
    Public Shared MISDBPassWord As String = "Alex0717"
    Public Shared MISDBName As String = "DBMIST100"
    Public Shared strMISConnectionString As String = "Data Source=" & MISDBServerName &
                                                  ";Initial Catalog= " & MISDBName &
                                                  ";User Id=" & MISDBUserName &
                                                  ";Password=" & MISDBPassWord & ";Max Pool Size=100"

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

    'Public Sub New()
    'T100ConnString = ConfigurationManager.ConnectionStrings("OracleERP_T100").ConnectionString
    'T100ConnString = "DATA SOURCE=192.168.50.15:1521/topprd;PASSWORD=dsdemo;USER ID=dsdemo"
    'End Sub


    '### Oracle T100 ######################################


    '# DataReader (System.Data.DataReader) เป็นรูปแบบการอ่านข้อมูลในทิศทางเดียว ข้อมูลที่ได้จาก DataReader
    '# จากการคำสั่ง SELECT จาก Table หรือ View จะถูกอ่านในรูปแบบทีล่ะ 1 Record จนถึง Record สุดท้าย 
    '# ไม่สามารถย้อนกลับ หรือชัดเรียงได้ และการเชื่อมต่อจะต้องทำการเชื่อมต่อตลอดเวลาเป็นการเชื่อมต่อแบบ Connected
    Public Function QueryDataReader(ByVal strSQL As String) As OracleDataReader
        Dim dtReader As OracleDataReader
        objConn = New OracleConnection
        With objConn
            .ConnectionString = strT100ConnectionString
            .Open()
        End With
        objCmd = New OracleCommand(strSQL, objConn)
        dtReader = objCmd.ExecuteReader()
        Return dtReader '*** Return DataReader ***'
    End Function

    Public Function QueryDataSet(ByVal strSQL As String) As DataSet
        Dim ds As New DataSet
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
        dtAdapter.Fill(ds)
        Return ds   '*** Return DataSet ***'
    End Function

    '# DataTable (System.Data.DataTable) คือกลุ่มของข้อมูลที่ถูกจัดเก็บไว้ในชุดเดียวกัน 
    '# โดยเปรียบเหมือนแผ่นตาราง 1 ตารางที่แบ่งเป็น Cloumn (แนวตั้ง) และ Rows (แนวนอน)
    '# ที่ถูกจัดเก็บไว้เพียง 1 Table เท่านั้น DataTable สามารถแปลงมาจาก DataSet หรือ DataAdapter
    '# หรือจะเป็นการสร้างชุดข้อมูล DataTable ขึ้นมาเองก็ได้ 
    Public Function QueryDataTable(ByVal strSQL As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        objConn = New OracleConnection
        With objConn
            .ConnectionString = strT100ConnectionString
            .Open()
        End With
        dtAdapter = New OracleDataAdapter(strSQL, objConn)
        dtAdapter.Fill(dt)
        Return dt '*** Return DataTable ***'
    End Function

    '# ExecuteNonQuery เป็นชุดคำสั่งที่ทำการ Query 
    '# ชุดคำสั่ง SQL ผ่าน SQLCommand เช่น INSERT,UPDATE,DELETE
    '# โดยทำการ Returns the number Of affected rows กลับมา หรือข้อผิดลพาดที่เกิดขึ้น 
    Public Function QueryExecuteNonQuery(ByVal strSQL As String) As Boolean
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
            Return True '*** Return True ***'
        Catch ex As Exception
            Return False '*** Return False ***'
        End Try
    End Function
    '# ExecuteScalar เป็นชุดคำสั่ง การประมวลผลคำสั่ง Scalar function 
    '# ที่มีการ Return เป็นคาที่ต้องการออกมา เช่น MIN,MAX,COUNT,SUM,AVG 
    Public Function QueryExecuteScalar(ByVal strSQL As String) As Object
        Dim obj As Object
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
            obj = objCmd.ExecuteScalar()  '*** Return Scalar ***'
            Return obj
        Catch ex As Exception
            Return Nothing '*** Return Nothing ***'
        End Try
    End Function

    Public Function TransStart()
        objConn = New OracleConnection
        With objConn
            .ConnectionString = strT100ConnectionString
            .Open()
        End With
        Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted)
    End Function

    Public Function TransExecute(ByVal strSQL As String) As Boolean
        objCmd = New OracleCommand()
        With objCmd
            .Connection = objConn
            .Transaction = Trans
            .CommandType = CommandType.Text
            .CommandText = strSQL
        End With
        objCmd.ExecuteNonQuery()
    End Function

    Public Function TransRollBack()
        Trans.Rollback()
    End Function

    Public Function TransCommit()
        Trans.Commit()
    End Function

    Public Sub Close()
        objConn.Close()
        objConn = Nothing
    End Sub
    '### END Oracle T100 ######################################
End Class
