Public Class XMDH
    '''<reamrks>##########Table SaleOrder Header##############</reamrks>
    Public Shared tblShippingNotice As String = "xmdh_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ShippingNoticeNoLine As String = "xmdhdocno"
    Public Shared SOline As String = "xmdh002"
    Public Shared Memo As String = "xmdh050"
    Public Shared Site As String = "xmdhsite"
    Public Shared ent As String = "xmdhent"
    '''<remarks> Velue </remarks>'''
    Public Shared Jinpao As String = Site & "='JINPAO'"
    Public Shared WStandard As String = ent & " ='3' "

    '--Page CustomsNew
    '--Check ShippingOrderNo from Sales Delivery same ShippingOrderNo from ShippingNote
    Private Shared CheckShippingNoteNoOral As String =
        "select " & SOline & "," & ShippingNoticeNoLine & ",nvl(" & Memo & ",'') " & Memo & " from " & tblShippingNotice & " where " & WStandard & " and " & Jinpao & " and " &
        "" & SOline & " ='@SOline' and " & ShippingNoticeNoLine & " ='@ShippingNoticeNoLine'"
    Public Shared Function PO(ByVal SeqSOformDelivery As String, ByVal ShippingNo As String)
        Dim Oral As String = CheckShippingNoteNoOral
        Dim TempDataTable As New DataTable
        Oral = Oral.Replace("@SOline", SeqSOformDelivery)
        Oral = Oral.Replace("@ShippingNoticeNoLine", ShippingNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, TempDataTable)
        Return TempDataTable
    End Function

End Class
