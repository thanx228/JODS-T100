Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFOA
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : ASF
    '# Table : sfoa_t
    '# asft801 : Maintail MO Operation Change : Header

    '# select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strChangeOp_Head_Rows100 As String = "select * from sfoa_t  where rownum <= 100 "
    Shared Function ChangeOp_Head_Rows100() As String
        Return strChangeOp_Head_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
