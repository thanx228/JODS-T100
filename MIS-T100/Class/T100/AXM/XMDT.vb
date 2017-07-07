Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class XMDT
    '# Module : AXM
    '# Table : xmdt_t
    '# AXMT420 : Sales Price Approval List
    '''<reamrks>##########Table Header of Sales Price Approval List ##############</reamrks>
    Public Shared tblPriceApprovalHeader As String = "xmdt_t"
    '''<reamrks> # Field </reamrks>
    Public Shared SLPriHeadPriceValuationNo As String = "xmdtdocno"
    Public Shared SLPriHeadDocumentDate As String = "xmdtdocdt"
    Public Shared SLPriHeadValidDate As String = "xmdt015" '--StartDatePrice
    Public Shared SLPriHeadCustID As String = "xmdt004"
    Public Shared SLPriHeadInvalidDate As String = "xmdt016" '--EndDatePrice
    Public Shared SLPriHeadCurrency As String = "xmdt005"

    '''<reamrks> Condition Field </reamrks>
    Public Shared ent As String = "xmdtent"
    Public Shared Site As String = "xmdtsite"
    Public Shared SLPriHeadStatus As String = "xmdtstus"

    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    Public Shared WStandard As String = ent & "='3'"
    Public Shared Jinpao As String = Site & "='JINPAO'"
    Public Shared Approved As String = SLPriHeadStatus & "='Y'"

End Class
