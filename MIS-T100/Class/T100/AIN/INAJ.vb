Public Class INAJ
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '''<reamrks>Table Inventory trading details</reamrks>
    Public Shared tblStockCardLines As String = "inaj_t"
    '''<reamrks> # Field </reamrks>
    Public Shared StockInOutCode As String = "inaj004"
    Public Shared STCardLinesItemNo As String = "inaj005"
    Public Shared STCardLinesStoreNo As String = "inaj008"
    Public Shared TradingQty As String = "inaj011"
    Public Shared DocDeduction As String = "inaj022" '-- วันที่ทำเอกสาร และวันที่ แอพเอกสาร (เบิกงานออก ส่งงานเข้า Stock)
    Public Shared ent As String = "inajent"
    Public Shared Site As String = "inajsite"

    '''<reamrks> Condition Field </reamrks>
    Public Shared Whr As String = ""
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared MRB_NG As String = "2600"
    Public Shared Scrap As String = "2700"
    Public Shared Borrow As String = "2800"

    '--Page UndeliveryStusAmount
    '--Sum Stock In/Out /Refresh Data Table
    Private Shared SelectSumStockInOut As String = "Select " & STCardLinesItemNo & " ,sum(" & StockInOutCode & "*" & TradingQty & ") As StockInOut from " & tblStockCardLines & "" &
        " where " & wStandard & "and " & STCardLinesStoreNo & " not in ('" & MRB_NG & "','" & Scrap & "','" & Borrow & "')" &
        " and " & STCardLinesItemNo & "='@STCardLinesItemNo'and " & DocDeduction & "<=@DocDeduction group by " & STCardLinesItemNo & " having sum(" & StockInOutCode & "*" & TradingQty & ")>0 order by " & STCardLinesItemNo & ""
    Public Shared Function SumStockInOut(ByVal SOReqQtyItemNo As String, ByVal EndDueDate As String)
        Dim Oral As String = SelectSumStockInOut
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@STCardLinesItemNo", SOReqQtyItemNo)
        Oral = Oral.Replace("@DocDeduction", EndDueDate)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function
End Class
