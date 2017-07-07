Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMDD
    ' # Module AXM
    Private Shared AXM As String = "AXM"
    '# Table xmdd_t
    '# TansectionCode >> axmt500
    '''<reamrks>##########Table SaleOrder Body : Tab Delivery Details ##############</reamrks>
    Public Shared tblSaleItemDeliveryDetail As String = "xmdd_t"
    '''<reamrks> # Field </reamrks>
    Public Shared SaleOrderNo As String = "xmdddocno"
    Public Shared ent As String = "xmddent"
    Public Shared Site As String = "xmddsite"
    Public Shared LineNo As String = "xmddseq"
    Public Shared ItemOrder As String = "xmddseq1"
    Public Shared BatchOrder As String = "xmddseq2"
    Public Shared ItemNo As String = "xmdd001"
    Public Shared ProductCharacteristics As String = "xmdd002"
    Public Shared SubPartFeatures As String = "xmdd003"
    Public Shared SalesUnit As String = "xmdd004"
    Public Shared OverallPurchaseQuantity As String = "xmdd005"
    Public Shared BatchOrderQuantity As String = "xmdd006"
    Public Shared ConvertMasterItemQuantity As String = "xmdd007"
    Public Shared QPA As String = "xmdd008"
    Public Shared DeliveryDateType As String = "xmdd009"
    Public Shared ShippingTimes As String = "xmdd010"
    Public Shared AgreedShippingDate As String = "xmdd011"
    Public Shared EstimatedReceptionDate As String = "xmdd012"
    Public Shared MRPdeliveryDateFrozen As String = "xmdd013"
    Public Shared ShippedQty As String = "xmdd014" 'DeliveryQty
    Public Shared SalesReturnSmountAlready As String = "xmdd015"
    Public Shared SalesReturnExchangeQuantity As String = "xmdd016"
    Public Shared ShippingStatus As String = "xmdd017"
    Public Shared ReferencePrice As String = "xmdd018"
    Public Shared TaxCode As String = "xmdd019"
    Public Shared TaxRate As String = "xmdd020"
    Public Shared DigitalPurchaseOrderNumber As String = "xmdd021"
    Public Shared RecentEditor As String = "xmdd022"
    Public Shared RecentEditTime As String = "xmdd023"
    Public Shared BatchReferenceUnit As String = "xmdd024"
    Public Shared BatchReferenceQuantity As String = "xmdd025"
    Public Shared BatchValuationUnit As String = "xmdd026"
    Public Shared BatchValuationQuantity As String = "xmdd027"
    Public Shared BatchAmountExcludingTax As String = "xmdd028"
    Public Shared TaxedBatchAmount As String = "xmdd029"
    Public Shared BatchTax As String = "xmdd030"
    Public Shared TransferredOutDistributionQuantity As String = "xmdd031"
    Public Shared AllocatedQty As String = "xmdd032"
    Public Shared PreparationReason As String = "xmdd033"
    Public Shared SigningForRejectQty As String = "xmdd034"
    Public Shared QuantityAssigned As String = "xmdd035"
    Public Shared PromotionalPrograms As String = "xmdd200"
    Public Shared BatchPackagingUnit As String = "xmdd201"
    Public Shared BatchPackagingQuantity As String = "xmdd202"
    Public Shared StandardPrice As String = "xmdd203"
    Public Shared PromotionPrice As String = "xmdd204"
    Public Shared TradingPrice As String = "xmdd205"
    Public Shared DiscountAmount As String = "xmdd206"
    Public Shared ApplicationOrg As String = "xmddunit"
    Public Shared ReceiptBranch As String = "xmdd207"
    Public Shared ShipAddressCode As String = "xmdd208"
    Public Shared DeliveryPoints As String = "xmdd209"
    Public Shared DeliveryTime As String = "xmdd210"
    Public Shared ShipTo As String = "xmdd211"
    Public Shared FreezeMRP As String = "xmdd212"
    Public Shared LocationStore As String = "xmdd213"
    Public Shared Location As String = "xmdd214"
    Public Shared LotNo As String = "xmdd215"
    Public Shared InventoryLockingGrade As String = "xmdd216"
    Public Shared InventoryBalance As String = "xmdd217"
    Public Shared SalesChannel As String = "xmdd218"
    Public Shared ProductGroupNumber As String = "xmdd219"
    Public Shared SalesOfNumber As String = "xmdd220"
    Public Shared Memo As String = "xmdd221"
    Public Shared Office As String = "xmdd222"
    Public Shared Salesman As String = "xmdd223"
    Public Shared SalesDept As String = "xmdd224"
    Public Shared MainContractSerialNo As String = "xmdd225"
    Public Shared OperatingMethod As String = "xmdd226"
    Public Shared SettlementType As String = "xmdd227"
    Public Shared BalanceType As String = "xmdd228"
    Public Shared AccountOrg As String = "xmddorga"
    Public Shared TransactionType As String = "xmdd229"
    Public Shared BatchPackagingSalesReturnExchangeQuantity As String = "xmdd230"
    Public Shared ReturnVolume As String = "xmdd036"
    Public Shared AlsoTheAmountOfReferenceQty As String = "xmdd037"
    Public Shared CounterofferQty As String = "xmdd038"
    Public Shared CounterofferReferenceQty As String = "xmdd039"
    Public Shared InventoryManagementCharacteristics As String = "xmdd231"
    Public Shared BOMfeatures As String = "xmdd040"
    Public Shared PlaningGeneralConfrimDateSaleOrder As String = "xmddud010"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared SampleGenaral As String = SubPartFeatures & " ='1' " 'งานที่ขายปกติ 
    Public Shared Sample As String = SubPartFeatures & " ='9' " 'งานที่แถ่ม

    '######### Where SaleOrder_No. = ? for All Filed SaleOrder Body : Tab Delivery Details   #########################
    Private Shared strSO_BodyDeliveryDetail_BySaleOrderNo As String = "Select " & SaleOrderNo & " ," & LineNo & "," & ItemOrder & ", " & BatchOrder & ", " &
       " " & ItemNo & " ," & ProductCharacteristics & "," & SubPartFeatures & "," & SalesUnit & "," & OverallPurchaseQuantity & ", " &
       " " & BatchOrderQuantity & "," & ConvertMasterItemQuantity & "," & QPA & "," & DeliveryDateType & "," & ShippingTimes & ", " &
       " " & AgreedShippingDate & "," & EstimatedReceptionDate & "," & MRPdeliveryDateFrozen & "," & ShippedQty & "," & SalesReturnSmountAlready & "," &
       " " & SalesReturnExchangeQuantity & "," & ShippingStatus & "," & ReferencePrice & "," & TaxCode & "," & TaxRate & "," &
       " " & DigitalPurchaseOrderNumber & "," & RecentEditor & "," & RecentEditTime & "," & BatchReferenceUnit & "," & BatchReferenceQuantity & "," &
       " " & BatchValuationUnit & "," & BatchValuationQuantity & "," & BatchAmountExcludingTax & "," & TaxedBatchAmount & "," & BatchTax & "," &
       " " & TransferredOutDistributionQuantity & "," & AllocatedQty & "," & PreparationReason & "," & SigningForRejectQty & "," & QuantityAssigned & "," &
       " " & PromotionalPrograms & "," & BatchPackagingUnit & "," & BatchPackagingQuantity & "," & StandardPrice & "," & PromotionPrice & "," &
       " " & TradingPrice & "," & DiscountAmount & "," & ApplicationOrg & "," & ReceiptBranch & "," & ShipAddressCode & "," & DeliveryPoints & ", " &
       " " & DeliveryTime & "," & ShipTo & "," & FreezeMRP & "," & LocationStore & "," & Location & ", " &
       " " & LotNo & "," & InventoryLockingGrade & "," & InventoryBalance & "," & SalesChannel & "," & ProductGroupNumber & ", " &
       " " & SalesOfNumber & "," & Memo & "," & Office & "," & Salesman & "," & SalesDept & "," & MainContractSerialNo & ", " &
       " " & OperatingMethod & "," & SettlementType & "," & BalanceType & "," & AccountOrg & "," & TransactionType & ", " &
       " " & BatchPackagingSalesReturnExchangeQuantity & "," & ReturnVolume & "," & AlsoTheAmountOfReferenceQty & "," & CounterofferQty & "," & CounterofferReferenceQty & ",  " &
       " " & InventoryManagementCharacteristics & "," & BOMfeatures & "  " &
       " FROM " & tblSaleItemDeliveryDetail & " " &
       " where " & wStandard & " AND " & SaleOrderNo & " =@pSaleOrder_No "
    Public Shared Function GetSO_BodyDeliveryDetailBySaleOrderNo(strSaleOrder_No As String) As DataTable
        Dim Sql As String = strSO_BodyDeliveryDetail_BySaleOrderNo.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDD", "GetSO_BodyDeliveryDetailBySaleOrderNo", "Sql = strSO_BodyDeliveryDetail_BySaleOrderNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSO_BodyDeliveryDetailBySaleOrderNo_Dataset(strSaleOrder_No As String) As DataSet
        Dim Sql As String = strSO_BodyDeliveryDetail_BySaleOrderNo.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDD", "GetSO_BodyDeliveryDetailBySaleOrderNo_Dataset", "Sql = strSO_BodyDeliveryDetail_BySaleOrderNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#########  Where ItemNo. = ? for All Filed ItemNo Body : Tab Delivery Details #########################
    Private Shared strSO_BodyDeliveryDetailByItemNo As String = "Select " & SaleOrderNo & " ," & LineNo & "," & ItemOrder & ", " & BatchOrder & ", " &
       " " & ItemNo & " ," & ProductCharacteristics & "," & SubPartFeatures & "," & SalesUnit & "," & OverallPurchaseQuantity & ", " &
       " " & BatchOrderQuantity & "," & ConvertMasterItemQuantity & "," & QPA & "," & DeliveryDateType & "," & ShippingTimes & ", " &
       " " & AgreedShippingDate & "," & EstimatedReceptionDate & "," & MRPdeliveryDateFrozen & "," & ShippedQty & "," & SalesReturnSmountAlready & "," &
       " " & SalesReturnExchangeQuantity & "," & ShippingStatus & "," & ReferencePrice & "," & TaxCode & "," & TaxRate & "," &
       " " & DigitalPurchaseOrderNumber & "," & RecentEditor & "," & RecentEditTime & "," & BatchReferenceUnit & "," & BatchReferenceQuantity & "," &
       " " & BatchValuationUnit & "," & BatchValuationQuantity & "," & BatchAmountExcludingTax & "," & TaxedBatchAmount & "," & BatchTax & "," &
       " " & TransferredOutDistributionQuantity & "," & AllocatedQty & "," & PreparationReason & "," & SigningForRejectQty & "," & QuantityAssigned & "," &
       " " & PromotionalPrograms & "," & BatchPackagingUnit & "," & BatchPackagingQuantity & "," & StandardPrice & "," & PromotionPrice & "," &
       " " & TradingPrice & "," & DiscountAmount & "," & ApplicationOrg & "," & ReceiptBranch & "," & ShipAddressCode & "," & DeliveryPoints & ", " &
       " " & DeliveryTime & "," & ShipTo & "," & FreezeMRP & "," & LocationStore & "," & Location & ", " &
       " " & LotNo & "," & InventoryLockingGrade & "," & InventoryBalance & "," & SalesChannel & "," & ProductGroupNumber & ", " &
       " " & SalesOfNumber & "," & Memo & "," & Office & "," & Salesman & "," & SalesDept & "," & MainContractSerialNo & ", " &
       " " & OperatingMethod & "," & SettlementType & "," & BalanceType & "," & AccountOrg & "," & TransactionType & ", " &
       " " & BatchPackagingSalesReturnExchangeQuantity & "," & ReturnVolume & "," & AlsoTheAmountOfReferenceQty & "," & CounterofferQty & "," & CounterofferReferenceQty & ",  " &
       " " & InventoryManagementCharacteristics & "," & BOMfeatures & "  " &
       " FROM " & tblSaleItemDeliveryDetail & " " &
       " where " & wStandard & " AND " & ItemNo & " =@pItem_No "
    Public Shared Function GetSO_BodyDeliveryDetailByItemNo(strItem_No As String) As DataTable
        Dim Sql As String = strSO_BodyDeliveryDetailByItemNo.Replace("@pItem_No", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDD", "GetSO_BodyDeliveryDetailByItemNo", "Sql = strSO_BodyDeliveryDetailByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSO_BodyDeliveryDetailByItemNo_Dataset(strItem_No As String) As DataSet
        Dim Sql As String = strSO_BodyDeliveryDetailByItemNo.Replace("@pItem_No", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDD", "GetSO_BodyDeliveryDetailByItemNo_Dataset", "Sql = strSO_BodyDeliveryDetailByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class