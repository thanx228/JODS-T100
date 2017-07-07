Public Class XMDV
    '# Module : AXM
    '# Table : xmdv_t
    '# AXMT420 : Sales Price Approval List
    '''<reamrks>##########Table Approved-price component valuation file ##############</reamrks>
    Public Shared tblPriceAppPricingByQty As String = "xmdv_t"
    '''<reamrks> # Field </reamrks>
    Public Shared SLPriLinesPriByQtyDocNo As String = "xmdvdocno"
    Public Shared SLPriLinesPriByQtyItemSeq As String = "xmdvseq"
    Public Shared SLPriLinesPriByQtyInitialQty As String = "xmdv001"
    Public Shared SLPriLinesPriByQtyEndQty As String = "xmdv002"
    Public Shared SLPriLinesPriByQtyUnitPrice As String = "xmdv003" '- Price = Y

    '''<reamrks> Condition Field </reamrks>
    Public Shared ent As String = "xmdvent"
    Public Shared Site As String = "xmdvsite"

    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    Public Shared WStandard As String = ent & "='3'"
    Public Shared Jinpao As String = Site & "='JINPAO'"


End Class
