Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOAIL
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : AOO
    '# Table : ooail_t
    '# axmt410 : Quotaion (Price Approval) : Pricing by Qty

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strQtyPriceAppvPricingQty_Rows100 As String = "select * from ooail_t  where rownum <= 100 "
    Shared Function QtyPriceAppvPricingQty_Rows100() As String
        Return strQtyPriceAppvPricingQty_Rows100
    End Function
    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function



    '# Function Now

    'Private Const strSqlWIPAmount As String = "select TI020 from ACMTI where TI003=@YearMon and TI006=@TI006 and TI007=@TI007"
    'Shared Function WIPAmount(ByVal TI007 As String, ByVal TI006 As String, ByVal YearMon As String) As Integer
    '    Dim strSql As String = strSqlWIPAmount
    '    strSql = strSql.Replace("@TI007", "'" & TI007 & "'")
    '    strSql = strSql.Replace("@TI006", "'" & TI006 & "'")
    '    strSql = strSql.Replace("@YearMon", "'" & YearMon & "'")
    '    Return Database.DBAction.Get_value(strSql, ProjectInit.strERPConnectionString)
    'End Function
End Class
