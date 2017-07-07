Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOCAL
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : AOO
    '# Table : oocal_t
    '# axmt410 : Quotaion (Price Approval) : Pricing by Qty

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strQtyPriceAppvPricingQty3_Rows100 As String = "select * from oocal_t  where rownum <= 100 "
    Shared Function QtyPriceAppvPricingQty3_Rows100() As String
        Return strQtyPriceAppvPricingQty3_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
