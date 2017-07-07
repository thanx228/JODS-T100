Public Class SFAC
    '''<reamrks>Table Production Detail</reamrks>
    Public Shared tblManufactureOrderLine As String = "sfac_t"
    Public Shared docNo As String = "sfacdocno"
    Public Shared ItemNo As String = "sfac001"
    Public Shared Quantity As String = "sfac003"
    Public Shared Unit As String = "sfac004"
    Public Shared Site As String = "sfacsite"
    Private Shared ent As String = "sfacent"

    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '--Page SalesOrderChangeStatus
    '--SelectdocNoLine where docNo ItemNo / No Rrfesh DataTable
    Private Shared SelectdocNoLine As String = "select " & docNo & "," & ItemNo & "," & Site & "," & Quantity & "," & Unit & " from " & tblManufactureOrderLine & "" &
        " where " & wStandard & "and  " & docNo & " ='@docNo' and  " & ItemNo & " ='@ItemNo'"
    Public Shared Function GetDocNoMOHead(ByVal DocNoMOHead As String, ByVal ProductionItemMOHead As String)
        Dim Oral As String = SelectdocNoLine
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@docNo", DocNoMOHead)
        Oral = Oral.Replace("@ItemNo", ProductionItemMOHead)
        tempDataTable = GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function
End Class
