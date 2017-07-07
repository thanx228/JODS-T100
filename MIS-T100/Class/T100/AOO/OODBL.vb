Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OODBL
    '# Module : AOO
    Private Shared AOO As String = "AOO"
    '# Table : oodb_t
    '#### Tax class basic data file
    Public Shared tblTaxTypeDetail As String = "oodbl_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "oodblent"
    Public Shared TransactionTaxArea As String = "oodbl001"
    Public Shared TaxCode As String = "oodbl002"
    Public Shared Language As String = "oodbl003"
    Public Shared Description As String = "oodbl004"

    Public Shared wStandrad As String = ent & "='3' and " & Language & "='en_US'"
End Class
