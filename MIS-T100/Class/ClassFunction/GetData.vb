Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic
Imports System
Imports System.Reflection

Public Class GetData
    ' Get_DataReaderSQL
    Shared Function Get_DataReaderSQL(ByVal str_sqlcommand As String, ByVal Connection_str As String, ByRef aa As Data.DataTable) As Boolean
        'Dim Connection_str As String = ProjectInit.strDBConnect(UCase(DB_name))
        If str_sqlcommand <> "" Then
            'Dim aa As New Data.DataTable
            Using connection As New System.Data.SqlClient.SqlConnection(Connection_str)
                Dim adapter As New System.Data.SqlClient.SqlDataAdapter()
                adapter.SelectCommand = New System.Data.SqlClient.SqlCommand(str_sqlcommand, connection)
                adapter.Fill(aa)
                adapter.Dispose()
                'Return aa
                'aa = Nothing
                connection.Close()
            End Using
        End If
    End Function
    'Get_DataReadeORACLE
    Shared Function Get_DataReaderOracle(ByVal str_sqlcommand As String, ByVal Connection_str As String, ByRef aa As Data.DataTable) As Boolean
        'Dim Connection_str As String = ProjectInit.strDBConnect(UCase(DB_name))
        If str_sqlcommand <> "" Then
            'Dim aa As New Data.DataTable
            Using connection As New System.Data.OracleClient.OracleConnection(Connection_str)
                Dim adapter As New System.Data.OracleClient.OracleDataAdapter()
                adapter.SelectCommand = New System.Data.OracleClient.OracleCommand(str_sqlcommand, connection)
                adapter.Fill(aa)
                adapter.Dispose()
                'Return aa
                'aa = Nothing
                connection.Close()
            End Using
        End If
    End Function
    'Get_DataValueORACLE
    Public Function Get_Value(ByVal Orlcommand As String, ByVal OrlConnection As String) As String
        Get_Value = ""
        If Orlcommand <> "" Then

            Dim OrlConnection1 As New System.Data.OracleClient.OracleConnection(OrlConnection)
            Dim cmd As New System.Data.OracleClient.OracleCommand
            Dim returnValue As String = ""

            cmd.CommandText = Orlcommand
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = OrlConnection1

            OrlConnection1.Open()

            If IsDBNull(cmd.ExecuteScalar()) = True Then
                returnValue = "0"
            Else
                returnValue = cmd.ExecuteScalar()
            End If
            OrlConnection1.Close()
            Get_Value = returnValue
            Return Get_Value
        End If
    End Function
    'Get_DataValueSQL
    Public Function Get_valueSQL(ByVal str_sqlcommand As String, ByVal Connection_str As String) As String
        Get_valueSQL = ""
        If str_sqlcommand <> "" Then

            Dim sqlConnection1 As New System.Data.SqlClient.SqlConnection(Connection_str)
            Dim cmd As New System.Data.SqlClient.SqlCommand
            Dim returnValue As String = ""

            cmd.CommandText = str_sqlcommand
            cmd.CommandType = Data.CommandType.Text
            cmd.Connection = sqlConnection1

            sqlConnection1.Open()

            If IsDBNull(cmd.ExecuteScalar()) = True Then
                returnValue = "0"
            Else
                returnValue = cmd.ExecuteScalar()
            End If

            sqlConnection1.Close()
            Get_valueSQL = returnValue
            Return Get_valueSQL
        End If
    End Function

    Function Where(ByVal fld As String, ByRef checkboxList As CheckBoxList, Optional ByVal notIn As Boolean = False, Optional ByVal valDefault As String = "", Optional ByVal selectAll As Boolean = False) As String
        Dim type As String = "",
            cnt As Decimal = 0,
            whr As String = "",
            typeAll As String = ""
        For Each boxItem As ListItem In checkboxList.Items
            Dim boxVal As String = CStr(boxItem.Value.Trim)
            typeAll = typeAll & "'" & boxVal & "',"
            If boxItem.Selected Then
                type = type & "'" & boxVal & "',"
                cnt = cnt + 1
            End If
        Next
        If cnt > 0 Then
            Dim strIn As String = " in "
            If notIn Then
                strIn = " not in "
            End If
            whr = " and " & fld & " " & strIn & " (" & type.Substring(0, type.Count - 1) & ")"
        Else
            If valDefault <> "" Then
                whr = " and " & fld & " in (" & valDefault & ")"
            End If
            If selectAll Then
                Dim strIn As String = " in "
                If notIn Then
                    strIn = " not in "
                End If
                whr = " and " & fld & strIn & " (" & typeAll.Substring(0, typeAll.Count - 1) & ")"
            End If
        End If
        Return whr
    End Function

    'add by Top
    '--SequnctailCustID
    Shared Function GetCust(ByRef Cust As TextBox) As String

        Dim pow As String = ""
        For Each box As String In Cust.Text
            If box <> "" Then
                pow &= Replace(box, ",", "','")
            End If
        Next
        Return pow.Substring(0, pow.Length)

    End Function


    '--Get Error Queary
    Shared Function getMyName() As String
        Dim sf As StackFrame = New StackFrame()
        Dim mb As MethodBase = sf.GetMethod()
        Return mb.Name
    End Function

    '--Get Error Queary
    Shared Function WhoCalledMe() As String
        Dim st As StackTrace = New StackTrace()
        Dim sf As StackFrame = st.GetFrame(1)
        Dim mb As MethodBase = sf.GetMethod()
        Return mb.Name
    End Function
    '--Get Error Queary
    Shared Function Get_DataReaderOracle(SQL As String, Optional whoCall As String = "") As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(SQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            'HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), SQL, ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function

    '--ora_dateselector
    Public Function ora_dateselector(ByVal fieldname As String, ByVal fdate As String, ByVal tdate As String)

        Dim strRet As String = ""
        '--TO_CHAR(isaf014,'yyyy/mm/dd') BETWEEN '2017/06/20' AND '2017/06/20'
        If fdate <> "" And tdate <> "" Then      'Define 2 Date! Good Job!
            strRet = " TO_CHAR(" & fieldname & ",'yyyy/mm/dd') BETWEEN '" & fdate & "' AND '" & tdate & "'"
        ElseIf fdate = "" And tdate = "" Then
            fdate = DateTime.Now.ToString("yyyyMMdd")
            tdate = DateTime.Now.ToString("yyyyMMdd")
            strRet = " TO_CHAR(" & fieldname & ",'yyyymmdd') BETWEEN '" & fdate & "' AND '" & tdate & "'"
        ElseIf fdate <> "" And tdate = "" Then
            strRet = " TO_CHAR(" & fieldname & ",'yyyy/mm/dd') >= '" & fdate & "'"
        ElseIf fdate = "" And tdate <> "" Then
            strRet = " TO_CHAR(" & fieldname & ",'yyyy/mm/dd') <= '" & tdate & "'"
        End If

        Return strRet

    End Function

    'add by noi all function below
    Shared Function GetDataReaderOracle(SQL As String, strConOracle As String, Optional whoCall As String = "") As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection
        Try
            'objConn = New OracleConnection(If(strConOracle = "", clsDBConnect.strT100ConnectionString, strConOracle))
            objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
            dtAdapter = New OracleDataAdapter(SQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), SQL, ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Shared Function GetDataReaderOracleDataSet(SQL As String, strConOracle As String, Optional whoCall As String = "") As DataSet
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataSet
        Dim objConn = New OracleConnection
        Try
            'objConn = New OracleConnection(If(strConOracle = "", clsDBConnect.strT100ConnectionString, strConOracle))
            objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
            dtAdapter = New OracleDataAdapter(SQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), SQL, ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Shared Function GetValueOracle(ByVal Orlcommand As String, OrlConnection As String, Optional whoCall As String = "") As String
        'Get_ValueOracle = ""
        If Orlcommand <> "" Then
            Dim OrlConnection1 As New System.Data.OracleClient.OracleConnection(If(OrlConnection = "", clsDBConnect.strT100ConnectionString, OrlConnection))
            Dim cmd As New System.Data.OracleClient.OracleCommand
            Dim returnValue As String = ""
            Try
                cmd.CommandText = Orlcommand
                cmd.CommandType = Data.CommandType.Text
                cmd.Connection = OrlConnection1

                OrlConnection1.Open()

                If IsDBNull(cmd.ExecuteScalar()) = True Then
                    returnValue = "0"
                Else
                    returnValue = cmd.ExecuteScalar()
                End If
                'Get_ValueOracle = returnValue
                Return returnValue
            Catch ex As Exception
                GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), Orlcommand, ex.Message)
            Finally
                OrlConnection1.Close()
                cmd = Nothing
            End Try
        End If
    End Function

    Shared Function ExecuteOracle(ByVal Orlcommand As String, OrlConnection As String, Optional whoCall As String = "") As Boolean
        'Get_ValueOracle = ""
        If Orlcommand <> "" Then
            Dim OrlConnection1 As New System.Data.OracleClient.OracleConnection(If(OrlConnection = "", clsDBConnect.strT100ConnectionString, OrlConnection))
            Dim cmd As New System.Data.OracleClient.OracleCommand
            Dim myOracleTransaction As System.Data.OracleClient.OracleTransaction = OrlConnection1.BeginTransaction()
            Try
                'Dim trans As OleDb.OleDbTransaction = connUsers.BeginTransaction(IsolationLevel.ReadCommitted)
                'Dim oTrans As OracleTransaction = Nothing

                cmd.CommandText = Orlcommand
                cmd.CommandType = Data.CommandType.Text
                cmd.Connection = OrlConnection1

                OrlConnection1.Open()

                Dim success As Boolean
                If cmd.ExecuteNonQuery() = 1 Then
                    success = True
                    myOracleTransaction.Commit()
                Else
                    success = False
                    myOracleTransaction.Rollback()
                End If

                cmd.Dispose()
                OrlConnection1.Close()
                OrlConnection1.Dispose()

                Return success
            Catch ex As Exception
                myOracleTransaction.Rollback()
                GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), Orlcommand, ex.Message)
            Finally
                OrlConnection1.Close()
                cmd = Nothing
            End Try
        End If
    End Function

    Shared Function RunOracleTransaction(ByVal Orlcommand As String, Optional whoCall As String = "")
        Using connection As New OracleConnection(clsDBConnect.strT100ConnectionString)
            connection.Open()
            Dim command As OracleCommand = connection.CreateCommand()
            Dim transaction As OracleTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            ' Assign transaction object for a pending local transaction
            command.Transaction = transaction
            Try
                command.CommandText = Orlcommand
                command.ExecuteNonQuery()
                transaction.Commit()
                'Console.WriteLine("Both records are written to database.")
            Catch ex As Exception
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred
                    ' on the server that would cause the rollback to fail, such as
                    ' a closed connection.
                    GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), Orlcommand, ex2.Message)
                End Try
            End Try
        End Using
    End Function

    Shared Function RunOracleTransaction(ByVal OrlcommandList As ArrayList, Optional whoCall As String = "")
        Using connection As New OracleConnection(clsDBConnect.strT100ConnectionString)
            connection.Open()
            Dim command As OracleCommand = connection.CreateCommand()
            Dim transaction As OracleTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            ' Assign transaction object for a pending local transaction
            command.Transaction = transaction
            Try
                For Each Orlcommand As String In OrlcommandList
                    command.CommandText = Orlcommand
                    command.ExecuteNonQuery()
                Next
                transaction.Commit()
                'Console.WriteLine("Both records are written to database.")
            Catch ex As Exception
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred
                    ' on the server that would cause the rollback to fail, such as
                    ' a closed connection.
                    GetPageError.GetPage(HttpContext.Current.Request.CurrentExecutionFilePath.ToString, If(whoCall = "", WhoCalledMe(), whoCall), "", ex2.Message)
                End Try
            End Try
        End Using
    End Function

End Class
