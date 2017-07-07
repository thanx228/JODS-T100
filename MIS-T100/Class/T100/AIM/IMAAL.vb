Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class IMAAL
    '# Module : AIM
    Private Shared AIM As String = "AIM"
    '# Table : imaal_t (Relationship to table imae_t)
    '# axmt410 : Quotaion (Price Approval) : Quotation data
    '''<reamrks>##########Table Production Perproty : Header Deatil ##############</reamrks>
    Public Shared tblProductionDetail As String = "imaal_t"
    Public Shared ProductItem As String = "imaal001"
    Public Shared ProductName As String = "imaal003"
    Public Shared Specifaction As String = "imaal004"
    Public Shared Mnemonic As String = "imaal005"
    Public Shared Langauge As String = "imaal002"
    Public Shared ent As String = "imaalent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared W_ProductItem As String = "imaal001"
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared enUS As String = Langauge & "='en_US'"

    '--Select ItemName and Spec where Item / Rrfesh DataTable
    Private Shared SelectItemNewDatable As String = "select " & ProductItem & "," & ProductName & "," & Specifaction & " from " & tblProductionDetail & "" &
        " where  " & WStandard & " and " & enUS & " and " & ProductItem & " ='@ProductItem'"
    Public Shared Function GetItem(ByVal ItemNoMOLine As String)
        Dim Oral As String = SelectItemNewDatable
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", ItemNoMOLine)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    ''--Page SalesOrderChangeStatus
    ''--Select ItemName and Spec where Item / No Rrfesh DataTable
    'Private Shared SelectProductionDetail As String = "select " & ProductItem & "," & ProductName & "," & Specifaction & " from " & tblProductionDetail & "" &
    '    " where  " & WStandard & " and " & enUS & " and " & ProductItem & " ='@ProductItem'"
    'Public Shared Function GetItemNoMOLine(ByVal ItemNoMOLine As String, ByRef tempDataTable As Data.DataTable)
    '    Dim Oral As String = SelectProductionDetail
    '    Oral = Oral.Replace("@ProductItem", ItemNoMOLine)
    '    GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    'End Function

    '--Page SalesOrderChangeStatus
    '--Select ItemName and Spec where Item / Rrfesh DataTable
    Private Shared SelectProductionDetailRrfesh As String = "select " & ProductItem & "," & ProductName & "," & Specifaction & " from " & tblProductionDetail & "" &
        " where  " & WStandard & " and " & enUS & " and " & ProductItem & " ='@ProductItem'"
    Public Shared Function GetItemNoMOLineRrfesh(ByVal ItemNoMOLine As String)
        Dim Oral As String = SelectProductionDetailRrfesh
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", ItemNoMOLine)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SalesOrderChangeStatus
    '--Select ItemName and Spec where Item / No Rrfesh DataTable
    Private Shared SelectProductionDetail As String = "select " & ProductItem & "," & ProductName & "," & Specifaction & " from " & tblProductionDetail & "" &
        " where  " & WStandard & " and " & enUS & " and " & ProductItem & " ='@ProductItem'"
    Public Shared Function GetItemNoMOLine(ByVal ItemNoMOLine As String)
        Dim Oral As String = SelectProductionDetail
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", ItemNoMOLine)
        tempDataTable = GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function

    '--Page SaleUndeliveryStatus  
    '--Sum Stock Where Item /Refresh Data Table
    Private Shared SelectStockUndelStus As String = "Select " & ProductItem & ",sum(" & INAG.BookInventory & ") as StockQty, " & INAG.WH & " FROM " & tblProductionDetail & "" &
        " left join " & INAG.tblInventoryDeatil & " on " & ProductItem & " = " & INAG.ItemNo & "" &
        " where " & WStandard & "and " & enUS & "and " & INAG.wStandard & "and " &
        " " & INAG.WH & " not in ('" & INAG.STCustomerHub & "','" & INAG.STCustomerSupport & "','" & INAG.STMRB_NG & "','" & INAG.STSCRAP & "','" & INAG.STBorrow & "')" &
        " and " & ProductItem & "='@ProductItem' group by " & ProductItem & "," & INAG.WH & " order by " & ProductItem & ""
    Public Shared Function SumItemStockUndelStus(ByVal IndustryItemNo As String)
        Dim Oral As String = SelectStockUndelStus
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", IndustryItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SaleUndelivery Status Period
    '--Sum Stock Where Item /Refresh Data Table
    Private Shared SelectSTUndelStusPeriod As String = "Select " & ProductItem & ",sum(" & INAG.BookInventory & ") as StockQty, " & INAG.WH & " FROM " & tblProductionDetail & "" &
        " left join " & INAG.tblInventoryDeatil & " on " & ProductItem & " = " & INAG.ItemNo & "" &
        " where " & WStandard & "and " & enUS & "and " & INAG.wStandard & "and " &
        " " & INAG.WH & " not in ('" & INAG.STMRB_NG & "','" & INAG.STSCRAP & "','" & INAG.STBorrow & "')" &
        " and " & ProductItem & "='@ProductItem' group by " & ProductItem & "," & INAG.WH & " order by " & ProductItem & ""
    Public Shared Function SumTUndelStusPeriod(ByVal IndustryItemNo As String)
        Dim Oral As String = SelectSTUndelStusPeriod
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", IndustryItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page ChackBOM PopUp and Page SaleUndeliveryStatusPopUp and Page SLUndelStusAmountPopUp and Page SaleUndelivery Status Period 
    '--SumItemStock / No Rrfesh DataTable
    Private Shared SelectItemStocke As String = "Select " & ProductItem & ",cast(" & INAG.BookInventory & " As Decimal(8,0)) as StockQty, " & INAG.WH & " FROM " & tblProductionDetail & "" &
        " left join " & INAG.tblInventoryDeatil & " on " & ProductItem & " = " & INAG.ItemNo & "" &
        " where " & WStandard & "and " & enUS & "and " & INAG.wStandard & "and " & INAG.BookInventory & " > 0" &
        " and " & ProductItem & "='@ProductItem' group by " & ProductItem & "," & INAG.WH & "," & INAG.BookInventory & " order by " & ProductItem & ""
    Public Shared Function SumItemStock(ByVal IndustryItemNo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectItemStocke
        Oral = Oral.Replace("@ProductItem", IndustryItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Page CheckBOM PopUp
    '--Sum Stock Where Item / Refresh Data Table
    Private Shared SelectSTChkBom As String = "Select " & ProductItem & ",sum(" & INAG.BookInventory & ") as StockQty, " & INAG.WH & " FROM " & tblProductionDetail & "" &
        " left join " & INAG.tblInventoryDeatil & " on " & ProductItem & " = " & INAG.ItemNo & "" &
        " where " & WStandard & "and " & enUS & "and " & INAG.wStandard & "and " &
        " " & INAG.BookInventory & ">0 And" &
        " " & INAG.WH & " not in ('" & INAG.STSCRAP & "','" & INAG.STBorrow & "')" &
        " and " & ProductItem & "='@ProductItem' group by " & ProductItem & "," & INAG.WH & " order by " & ProductItem & ""
    Public Shared Function SumSTChkBom(ByVal ItemNo As String)
        Dim Oral As String = SelectSTChkBom
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ProductItem", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function


    '''<remarks> Get  Relationship to Table >> imae_t : Where 1  Production Item_No. ='?' </remarks>  
    Private Shared StrProducItemRowsAll As String = "Select " & ProductItem & "," & ProductName & "," & Specifaction & "," & Mnemonic & " " &
    " FROM " & tblProductionDetail & " where " & ent & "='3' "
    Public Shared Function GetDataProducItemAll() As DataTable
        Dim strSQL As String = StrProducItemRowsAll
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducItemAll", "strSQL = StrProducItemRowsAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataProducItemAllDataSet(ItemRows As String) As DataSet
        Dim strSQL As String = StrProducItemRowsAll
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducItemAllDataSet", "strSQL = StrProducItemRowsAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '''<remarks> Get  Relationship to Table >> imae_t : Where 1  Production Item_No. ='?' </remarks>  
    Private Shared StrProducItemRows As String = "Select " & ProductItem & "," & ProductName & "," & Specifaction & "," & Mnemonic & " " &
    " FROM " & tblProductionDetail & " where " & W_ProductItem & " =@ProductItem and " & ent & "='3' "
    Public Shared Function GetDataProducItem(ItemRows As String) As DataTable
        Dim strSQL As String = StrProducItemRows
        strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducItem", "strSQL = StrProducItemRows", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataProducItemDataSet(ItemRows As String) As DataSet
        Dim strSQL As String = StrProducItemRows
        strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducItemDataSet", "strSQL = StrProducItemRows", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '''<remarks> Get  Relationship to Table >> imae_t : Where   Spec  Like '?' </remarks>  
    Private Shared StrProducItemRowsLike As String = "Select " & ProductItem & "," & ProductName & "," & Specifaction & "," & Mnemonic & " " &
    " FROM " & tblProductionDetail & " where  " & ent & "='3' and " & Specifaction & "  @pSpec  "
    Public Shared Function GetDataProducItem_Like(StrSpec As String) As DataTable
        Dim strSQL As String = StrProducItemRowsLike
        strSQL = strSQL.Replace("@pSpec", " Like '%" & StrSpec & "%'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducItem_Like", "strSQL = StrProducItemRowsLike", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataProducIte_Like_DataSet(StrSpec As String) As DataSet
        Dim strSQL As String = StrProducItemRowsLike
        strSQL = strSQL.Replace("@pSpec", "'" & StrSpec & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAAL", "GetDataProducIte_Like_DataSet", "strSQL = StrProducItemRowsLike", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
