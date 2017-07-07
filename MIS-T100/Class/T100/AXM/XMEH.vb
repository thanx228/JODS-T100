Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMEH
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmeh_t
    '# axmt510 : SaleOrder Change,SaleForcast Change : Delevery Detail
    ''' <remarks> Delevery Detail </remarks>
    Public Shared tblSaleChangeDeliveryDeatil As String = "xmeh_t"
    Public Shared ent As String = "xmehent"
    Public Shared Site As String = "xmehsite"
    Public Shared DocNo As String = "xmehdocno"
    Public Shared Version As String = "xmeh900"
    Public Shared SOline As String = "xmehseq"
    Public Shared LineSequence As String = "xmehseq1"
    Public Shared BatchOrder As String = "xmehseq2"
    Public Shared ItemNo As String = "xmeh001"
    Public Shared ProductFeature As String = "xmeh002"
    Public Shared SubItemFeature As String = "xmeh003"
    Public Shared SalesUnit As String = "xmeh004"
    Public Shared OverallPurchaseQuantity As String = "xmeh005"
    Public Shared BatchOrderQuantity As String = "xmeh006"
    Public Shared ConvertMasterItemQuantity As String = "xmeh007"
    Public Shared QPA As String = "xmeh008"
    Public Shared DeliveryDateType As String = "xmeh009"
    Public Shared ArrivalPeriod As String = "xmeh010"
    Public Shared AppointedDeliveryDate As String = "xmeh011"
    Public Shared MRPdeliveryDateFrozen As String = "xmeh013"
    Public Shared ShippedQty As String = "xmeh014"
    Public Shared QtyforItemsSoldAndReturned As String = "xmeh015"
    Public Shared SalesReturnExchangeQuantity As String = "xmeh016"
    Public Shared ShippingStatus As String = "xmeh017"
    Public Shared ReferencePrice As String = "xmeh018"
    Public Shared TaxType As String = "xmeh019"
    Public Shared TaxRate As String = "xmeh020"
    Public Shared DigitalPurchaseOrderNumber As String = "xmeh021"
    Public Shared RecentEditor As String = "xmeh022"
    Public Shared RecentEditTime As String = "xmeh023"
    Public Shared BatchReferenceUnit As String = "xmeh024"
    Public Shared BatchReferenceQuantity As String = "xmeh025"
    Public Shared BatchValuationUnit As String = "xmeh026"
    Public Shared BatchValuationQuantity As String = "xmeh027"
    Public Shared BatchAmountExcludingTax As String = "xmeh028"
    Public Shared TaxedBatchAmount As String = "xmeh029"
    Public Shared BatchTax As String = "xmeh030"
    Public Shared TransferredOutDistributionQuantity As String = "xmeh031"
    Public Shared ChangeSN As String = "xmeh900"
    Public Shared ChangedType As String = "xmeh901"
    Public Shared PromotionalPrograms As String = "xmeh200"
    Public Shared BatchPackagingUnit As String = "xmeh201"
    Public Shared BatchPackagingQuantity As String = "xmeh202"
    Public Shared StandardPrice As String = "xmeh203"
    Public Shared PromotionPrice As String = "xmeh204"
    Public Shared TradingPrice As String = "xmeh205"
    Public Shared DiscountAmount As String = "xmeh206"
    Public Shared ApplicationOrg As String = "xmehunit"
    Public Shared ReceiptBranch As String = "xmeh207"
    Public Shared ShipAddressCode As String = "xmeh208"
    Public Shared DeliveryStation As String = "xmeh209"
    Public Shared DeliveryTime As String = "xmeh210"
    Public Shared ShipTo As String = "xmeh211"
    Public Shared FrozenByMRP As String = "xmeh212"
    Public Shared LocationStore As String = "xmeh213"
    Public Shared Store As String = "xmeh214"
    Public Shared LotNo As String = "xmeh215"
    Public Shared InventoryLockingGrade As String = "xmeh216"
    Public Shared InventoryBalance As String = "xmeh217"
    Public Shared SalesChannel As String = "xmeh218"
    Public Shared ProductGroupNumber As String = "xmeh219"
    Public Shared SalesOfNumber As String = "xmeh220"
    Public Shared Notes As String = "xmeh221"
    Public Shared Office As String = "xmeh222"
    Public Shared SalesPerson As String = "xmeh223"
    Public Shared SalesDepartment As String = "xmeh224"
    Public Shared MainContractSerialNo As String = "xmeh225"
    Public Shared OperatingMethod As String = "xmeh226"
    Public Shared SettlementType As String = "xmeh227"
    Public Shared SettlementMethod As String = "xmeh228"
    Public Shared AccountOrg As String = "xmehorga"
    Public Shared TransactionType As String = "xmeh229"

    ''' <remarks>Where Starndrad </remarks>
    Public Shared wStandard As String = ent & "='3' and " & Site & "='JINPAO'"

    '######### where DocNo =?  and ItemN =? #########################
    Private Shared strSaleChangeDeliveryDeatil As String = "Select * from " & tblSaleChangeDeliveryDeatil & " " &
       " where " & wStandard & " AND " & DocNo & " =@pDocNo and " & ItemNo & " =@pItemNo order by " & DocNo & "," & Version & "," & SOline & " asc "
    Public Shared Function getSaleChangeDeliveryDeatil(strDocNo As String, strItemNo As String) As DataTable
        Dim Sql As String = strSaleChangeDeliveryDeatil
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatil", "Sql = strSaleChangeDeliveryDeatil", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeDeliveryDeatilDataSet(strDocNo As String, strItemNo As String) As DataSet
        Dim Sql As String = strSaleChangeDeliveryDeatil
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilDataSet", "Sql = strSaleChangeDeliveryDeatil", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where DocNo =? #########################
    Private Shared strSaleChangeDeliveryDeatilByDocNo As String = "Select * from " & tblSaleChangeDeliveryDeatil & " " &
       " where " & wStandard & " AND " & DocNo & " =@pDocNo order by " & DocNo & "," & Version & "," & SOline & " asc "
    Public Shared Function getSaleChangeDeliveryDeatilByDocNo(strDocNo As String) As DataTable
        Dim Sql As String = strSaleChangeDeliveryDeatilByDocNo
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilByDocNo", "Sql = strSaleChangeDeliveryDeatilByDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeDeliveryDeatilByDocNoDataSet(strDocNo As String) As DataSet
        Dim Sql As String = strSaleChangeDeliveryDeatilByDocNo
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilByDocNoDataSet", "Sql = strSaleChangeDeliveryDeatilByDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where ItemN =? #########################
    Private Shared strSaleChangeDeliveryDeatilByItemNo As String = "Select * from " & tblSaleChangeDeliveryDeatil & " " &
       " where " & wStandard & " AND " & ItemNo & " =@pItemNo  order by " & DocNo & "," & Version & "," & SOline & " asc "
    Public Shared Function getSaleChangeDeliveryDeatilByItemNo(strItemNo As String) As DataTable
        Dim Sql As String = strSaleChangeDeliveryDeatilByItemNo
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatil", "Sql = strSaleChangeDeliveryDeatilByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeDeliveryDeatilByItemNoDataSet(strItemNo As String) As DataSet
        Dim Sql As String = strSaleChangeDeliveryDeatilByItemNo
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilDataSet", "Sql = strSaleChangeDeliveryDeatilByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where Custom  #########################
    Private Shared strstrSaleChangeDeliveryDeatilCustom As String = "Select * from " & tblSaleChangeDeliveryDeatil & " " &
       " where " & wStandard & " AND @pCustomWhere order by " & DocNo & "," & Version & "," & SOline & " asc "
    Public Shared Function getSaleChangeDeliveryDeatilCustom(strCustomWhere As String) As DataTable
        Dim Sql As String = strstrSaleChangeDeliveryDeatilCustom
        Sql = Sql.Replace("@pCustomWhere", strCustomWhere)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilCustom", "Sql = strstrSaleChangeDeliveryDeatilCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeDeliveryDeatilCustomDataSet(strCustomWhere As String) As DataSet
        Dim Sql As String = strstrSaleChangeDeliveryDeatilCustom
        Sql = Sql.Replace("@pCustomWhere", strCustomWhere)
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEH", "getSaleChangeDeliveryDeatilCustomDataSet", "Sql = strstrSaleChangeDeliveryDeatilCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
