Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class QCBC
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : AQC
    '# Table : qcbc_t
    '# aqct300 : Maintain QC Inspection Record : Item - Body Tab3 >> Co-Production and result differentiation

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strQC_InsProductionHeadeAll_Rows100 As String = "select * from qcbc_t  where rownum <= 100 "
    Shared Function QC_InsProductionHeadeAll_Rows100() As String
        Return strQC_InsProductionHeadeAll_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class

