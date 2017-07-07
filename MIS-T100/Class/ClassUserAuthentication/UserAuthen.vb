Imports System
Imports System.Data
Imports System.IO
Imports System.Data.Sql
Imports System.Data.OracleClient
Imports System.Data.SqlClient
Public Class UserAuthen
    Inherits System.Web.UI.Page

    Public Shared Function LevelWorkcenterT100(Session As String) As String
        Dim Sql As String = "select WC from UserPlanAuthority where Id='" & Session & "' "
        Dim WC As String = GetValue(Sql, clsDBConnect.strMISConnectionString).Replace(",", "','")
        WC = WC.Replace("W", "WC")
        Dim where As String = " '" & [String].Join("','", WC) & "'"
        WorkstationAuten.WC_Auten = WC
        UsingWorkstationCheckList.WC_Auten = WC
        Return WC
    End Function
    Public Shared Function LevelWorkcenterERP(Session As String) As String
        Dim Sql As String = "select WC from UserPlanAuthority where Id='" & Session & "' "
        Dim WC As String = GetValue(Sql, clsDBConnect.strMISConnectionString).Replace(",", "','")
        Dim where As String = " '" & [String].Join("','", WC) & "'"
        WorkstationAuten.WC_Auten = WC
        UsingWorkstationCheckList.WC_Auten = WC
        Return WC
    End Function
    Private Shared Function GetValue(ByVal sql As String, ByVal Connection_str As String) As String
        GetValue = ""
        If sql <> "" Then
            Dim sqlConnection As New SqlConnection(Connection_str)
            Dim cmd As New SqlCommand
            Dim returnValue As String = ""
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection
            sqlConnection.Open()
            If IsDBNull(cmd.ExecuteScalar()) = True Then
                returnValue = "0"
            Else
                returnValue = cmd.ExecuteScalar()
            End If
            sqlConnection.Close()
            GetValue = returnValue
            Return GetValue
        End If
    End Function
End Class
