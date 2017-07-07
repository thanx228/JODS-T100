Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOFC
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : AOO
    '# Table : oofc_t
    '# azmm200 : Customer Info : Tab Communication

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strCustInfoTabComm_Rows100 As String = "select * from oofc_t  where rownum <= 100 "
    Shared Function CustInfoTaComm_Rows100() As String
        Return strCustInfoTabComm_Rows100
    End Function
    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
