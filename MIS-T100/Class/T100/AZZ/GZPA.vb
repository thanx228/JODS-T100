Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class GZPA
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module AZZ
    '# Table gzpa_t
    '# Workstation WIP Status : Tab2
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strWorkStasionT2All_Rows100 As String = "select * from gzpa_t  where rownum <= 100 "
    Shared Function WorkStasionT2All_Rows100() As String
        Return strWorkStasionT2All_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
