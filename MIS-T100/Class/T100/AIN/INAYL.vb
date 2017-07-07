Public Class INAYL
    '''<reamrks>##########Table Maintain Group LocationNo##############</reamrks>
    Public Shared tblMaintainGroupLocationNo As String = "inayl_t"
    '''<reamrks> # Field </reamrks>
    Public Shared STStockNo As String = "inayl001"
    Public Shared STStockName As String = "inayl003"
    Public Shared ent As String = "inaylent"
    Public Shared Langauge As String = "inayl002"

    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared enUS As String = Langauge & "='en_US'"

    '--Page ChackBOM PopUp and SaleUndeliveryStatusPopUp and Page SLUndelStusAmountPopUp and Page SaleUndelivery Status Period 
    '--Select STStockNo STStockNo Where STStockNo / Rrfesh DataTable
    Private Shared SelectSTStockName As String = "Select " & STStockNo & "," & STStockName & " FROM " & tblMaintainGroupLocationNo & "" &
       " where " & WStandard & "and " & enUS & "and " & STStockNo & "='@STStockNo'"
    Public Shared Function StockName(ByVal STStockNo As String)
        Dim Oral As String = SelectSTStockName
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@STStockNo", STStockNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function
End Class
