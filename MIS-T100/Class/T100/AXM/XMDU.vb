Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class XMDU
    '# Module : AXM
    '# Table : xmdt_t
    '# AXMT420 : Sales Price Approval Details
    '''<reamrks>##########Table Sales Price Approval Details ##############</reamrks>
    Public Shared tblPriceApprovalDetails As String = "xmdu_t"
    '''<reamrks> # Field </reamrks>
    Public Shared SLPriLinesDocNo As String = "xmdudocno"
    Public Shared SLPriLinesItemseq As String = "xmduseq"
    Public Shared SLPriLinesItemNo As String = "xmdu002"
    Public Shared SLPriLinesPricingByQty As String = "xmdu009"
    Public Shared SLPriLinesUnitPrice As String = "xmdu011" '- Price = N

    '''<reamrks> Condition Field </reamrks>
    Public Shared ent As String = "xmduent"
    Public Shared Site As String = "xmdusite"

    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '--Page Sales Quotation
    '--Shearch Price Appvoed where Item ValidDate CustID / Refresh DataTable
    Private Shared Selectwhr As String = "Select " & XMDT.SLPriHeadPriceValuationNo & "," & XMDT.SLPriHeadDocumentDate & "," & XMDT.SLPriHeadValidDate & "," & XMDT.SLPriHeadInvalidDate & "," & XMDT.SLPriHeadCustID & "," &
    " " & SLPriLinesItemseq & "," & SLPriLinesItemNo & "," & SLPriLinesUnitPrice & "," & SLPriLinesPricingByQty & "," &
    " nvl(" & XMDV.SLPriLinesPriByQtyInitialQty & ",'0') AS InitialQty,nvl(" & XMDV.SLPriLinesPriByQtyEndQty & ",'0') AS EndQty,nvl(" & XMDV.SLPriLinesPriByQtyUnitPrice & ",'0') AS UnitPrice  from " & tblPriceApprovalDetails & "" &
    " left join  " & XMDT.tblPriceApprovalHeader & " On  " & SLPriLinesDocNo & " = " & XMDT.SLPriHeadPriceValuationNo & "" &
    " Left join " & XMDV.tblPriceAppPricingByQty & " On " & SLPriLinesDocNo & " = " & XMDV.SLPriLinesPriByQtyDocNo & " And " & SLPriLinesItemseq & " = " & XMDV.SLPriLinesPriByQtyItemSeq & "" &
    " where " & wStandard & "" &
    " and " & XMDT.WStandard & " and " & XMDT.Jinpao & " and " & XMDT.Approved & "" &
    " and to_char(" & XMDT.SLPriHeadValidDate & ",'yyyymmdd')<='@ValidDate' and '@ValidDate' between to_char(" & XMDT.SLPriHeadValidDate & ",'yyyymmdd') and  to_char(nvl(" & XMDT.SLPriHeadInvalidDate & ",sysdate),'yyyymmdd')" &
    " and " & SLPriLinesItemNo & "='@SLPriLinesItemNo' and " & XMDT.SLPriHeadCustID & "='@CustID' order by " & XMDT.SLPriHeadValidDate & " desc," & XMDT.SLPriHeadPriceValuationNo & " desc "

    Public Shared Function ShearchSLPrice(ByVal ItemNo As String, ByVal ValidDate As String, ByVal CustID As String)
        Dim Oral As String = Selectwhr
        Dim dt As New DataTable
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SLPriLinesItemNo", ItemNo)
        Oral = Oral.Replace("@ValidDate", ValidDate)
        Oral = Oral.Replace("@CustID", CustID)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, dt)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim DocDate As String = "", PricingByQty As String = "",
                UnitPrice As String = "", PriByQtyInitialQty As String = "",
                PriByQtyEndQty As String = ""
            If i = 0 Then
                DocDate = dt.Rows(i).Item("xmdtdocdt")
                PricingByQty = dt.Rows(i).Item("xmdu009")
                UnitPrice = dt.Rows(i).Item("xmdu011") '--Price  PricingByQty = N
                PriByQtyInitialQty = dt.Rows(i).Item("InitialQty") '--standard Qty from
                PriByQtyEndQty = dt.Rows(i).Item("EndQty") '--standard Qty To
            End If
            GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Next
        Return tempDataTable
    End Function
End Class
