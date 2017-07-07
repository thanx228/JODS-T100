Imports System.Web.Services
Imports Microsoft.VisualBasic
    Imports System.Data
    Imports System.Configuration
    Imports System.Data.OracleClient
    Public Class cls_PalnByMO
    '# Plan Schedue    
    Private Const strSatColor As String = "Sat"
    Private Const strSunColor As String = "Sun"
    Shared Function SatColor() As String
        Return strSatColor
    End Function
    Shared Function SunColor() As String
        Return strSunColor
    End Function



    'Private Const strSqlWIPAmount As String = "select TI020 from ACMTI where TI003=@YearMon and TI006=@TI006 and TI007=@TI007"
    'Shared Function WIPAmount(ByVal TI007 As String, ByVal TI006 As String, ByVal YearMon As String) As Integer
    '    Dim strSql As String = strSqlWIPAmount
    '    strSql = strSql.Replace("@TI007", "'" & TI007 & "'")
    '    strSql = strSql.Replace("@TI006", "'" & TI006 & "'")
    '    strSql = strSql.Replace("@YearMon", "'" & YearMon & "'")
    '    Return Database.DBAction.Get_value(strSql, ProjectInit.strERPConnectionString)
    'End Function
End Class

