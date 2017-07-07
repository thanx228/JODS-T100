Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OODB
    '# Module : AOO
    Private Shared AOO As String = "AOO"
    '# Table : oodb_t
    '#### Tax class basic data file
    Public Shared tblOperationSummary As String = "oodb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "oodbent"
    Public Shared Status As String = "oodbstus"
    Public Shared TransactionTaxArea As String = "oodb001"
    Public Shared TaxCode As String = "oodb002"
    Public Shared GeneralNo As String = "oodb003"
    Public Shared TaxingRule As String = "oodb004"
    Public Shared TaxIncluded As String = "oodb005"
    Public Shared TaxRate As String = "oodb006"
    Public Shared FormulaNo As String = "oodb007"
    Public Shared TaxCode2 As String = "oodb008"
    Public Shared DownloadPOS As String = "oodb009"
    Public Shared DownstreamPOSstate As String = "oodb010"
    Public Shared TaxApplication As String = "oodb011"
    Public Shared PrintInvoiceWithVAT As String = "oodb012"
    Public Shared FixedTaxAmount As String = "oodb013"

    Public Shared DataOwner As String = "oodbownid"
    Public Shared DataOwnerDept As String = "oodbowndp"
    Public Shared DataCreatedBy As String = "oodbcrtid"
    Public Shared DataCreatedByDept As String = "oodbcrtdp"
    Public Shared DataCreatedDate As String = "oodbcrtdt"
    Public Shared ModifiedBy As String = "oodbmodid"
    Public Shared LastModifiedDate As String = "oodbmoddt"

End Class
