Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDT
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdt_t
    '''# apmt520 :Purchaes PR Receipt : Body Item :  Purchase  Receipt Detail
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PR Receipt : Body Item : Purchase  Receipt Detail ##############</reamrks>
    Public Shared tblPRreceiptDetailBody As String = "pmdt_t"
    Public Shared tableName As String = "pmdt_t"

    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdtent"
    Public Shared Site As String = "pmdtsite"
    Public Shared DocNo As String = "pmdtdocno"
    Public Shared ItemSequence As String = "pmdtseq"
    Public Shared PurchaseOrderNo As String = "pmdt001"
    Public Shared LineNo As String = "pmdt002"
    Public Shared PurchaseItemSN As String = "pmdt003"
    Public Shared PurchaseBatchOrder As String = "pmdt004"
    Public Shared SubItemFeature As String = "pmdt005"
    Public Shared ReceiptItemNo As String = "pmdt006"
    Public Shared ProductFeature As String = "pmdt007"
    Public Shared Packagingcontainer As String = "pmdt008"
    Public Shared OperationNo As String = "pmdt009"
    Public Shared OperationSeq As String = "pmdt010"
    Public Shared OffsetSeq As String = "pmdt011"
    Public Shared StoreLocation As String = "pmdt016"
    Public Shared Location As String = "pmdt017"
    Public Shared LotNo As String = "pmdt018"
    Public Shared ReceiptStockInUnit As String = "pmdt019"
    Public Shared ReceiptStockInQty As String = "pmdt020"
    Public Shared ReferenceUnit As String = "pmdt021"
    Public Shared ReferenceQty As String = "pmdt022"
    Public Shared PricingUnit As String = "pmdt023"
    Public Shared PricingQty As String = "pmdt024"
    Public Shared Urgency As String = "pmdt025"
    Public Shared Inspection As String = "pmdt026"
    Public Shared GoodsReceiptNo As String = "pmdt027"
    Public Shared ReceiptLineNo As String = "pmdt028"
    Public Shared UnitPrice As String = "pmdt036"
    Public Shared TaxRate As String = "pmdt037"
    Public Shared AmtExclTax As String = "pmdt038"
    Public Shared AmtInclTax As String = "pmdt039"
    Public Shared PriceDetermination As String = "pmdt040"
    Public Shared Bonded As String = "pmdt041"
    Public Shared PricingSource As String = "pmdt042"
    Public Shared PriceReferenceNo As String = "pmdt043"
    Public Shared RetrieveUP As String = "pmdt044"
    Public Shared PercentSpread As String = "pmdt045"
    Public Shared TaxType As String = "pmdt046"
    Public Shared Tax As String = "pmdt047"
    Public Shared ReasonCode As String = "pmdt051"
    Public Shared StatusCode As String = "pmdt052"
    Public Shared AcceptableQty As String = "pmdt053"
    Public Shared StockedQty As String = "pmdt054"
    Public Shared RejectQty As String = "pmdt055"
    Public Shared MainSetBilledQuantity As String = "pmdt056"
    Public Shared AccountSet2PaidQuantity As String = "pmdt057"
    Public Shared AccountSet3PaidQuantity As String = "pmdt058"
    Public Shared Notes As String = "pmdt059" 'pack std
    Public Shared VendorBatchNo As String = "pmdt060"
    Public Shared SupplierDeliveryQuantity As String = "pmdt061"
    Public Shared MultiStoreLocationLotReceptionAndStocking As String = "pmdt062"
    Public Shared InventoryManagmentFeature As String = "pmdt063"
    Public Shared ShippingOrderNo As String = "pmdt064"
    Public Shared ShippingLineNo As String = "pmdt065"
    Public Shared PrimaryBookSetTempEstimatedQty As String = "pmdt066"
    Public Shared AccountSet2ProvisionalEstimationsQuantity As String = "pmdt067"
    Public Shared AccountSet3ProvisionalEstimationsQuantity As String = "pmdt068"
    Public Shared InvoiceQty As String = "pmdt069"
    Public Shared QCDocNo As String = "pmdt081"
    Public Shared QCitems As String = "pmdt082"
    Public Shared Result As String = "pmdt083"
    Public Shared SelfCreatedAPRequiredYN As String = "pmdt084"
    Public Shared InterCompanyProcessNo As String = "pmdt085"
    Public Shared MultilateralTradeTypeForPurchasing As String = "pmdt086"
    Public Shared PurchaseOrderIssuanceLocation As String = "pmdt087"
    Public Shared StockNote As String = "pmdt088"
    Public Shared ValidDate As String = "pmdt089"
    Public Shared RetainFieldStart As String = "pmdt900"
    Public Shared RetainFieldEnd As String = "pmdt999"
    Public Shared UnshippedQuantity As String = "pmdtud012"
    Public Shared GoodsBarcode As String = "pmdt200"
    Public Shared ReceiptPackagingUnit As String = "pmdt201"
    Public Shared ReceiptPackagingQty As String = "pmdt202"
    Public Shared Purchasing As String = "pmdt203"
    Public Shared PurchaseCenter As String = "pmdt204"
    Public Shared RequisitionOrganization As String = "pmdt205"
    Public Shared ReservedReceptionNoteNumber As String = "pmdt206"
    Public Shared ReservedReceptionItem As String = "pmdt207"
    Public Shared PurchaseChannel As String = "pmdt208"
    Public Shared ChannelProperty As String = "pmdt209"
    Public Shared OperatingMethod As String = "pmdt210"
    Public Shared Settlement_Method As String = "pmdt211"
    Public Shared ContractNo As String = "pmdt212"
    Public Shared AgreementNo As String = "pmdt213"
    Public Shared AccountOrg As String = "pmdtorga"
    Public Shared Reference As String = "pmdt070"
    Public Shared ReferenceItemSeq As String = "pmdt071"
    Public Shared PurchaseMethod As String = "pmdt214"
    Public Shared FinalReceptionOrganization As String = "pmdt215"
    Public Shared PriceReferenceItems As String = "pmdt048"
    Public Shared ReturnRequisitionNumber As String = "pmdt216"
    Public Shared ReturnRequestItems As String = "pmdt217"
    Public Shared ProductLentQuantityByPrices As String = "pmdt090"
    Public Shared ProductLentReturnedByPricesReferenceQuantity As String = "pmdt091"
    Public Shared UntaxedCounterofferAmount As String = "pmdt092"
    Public Shared TaxedCounterofferAmount As String = "pmdt093"
    Public Shared PurchasePrice As String = "pmdt218"
    Public Shared CreatedDate As String = "pmdt219"
    Public Shared ProjectNo As String = "pmdt072"
    Public Shared WBS As String = "pmdt073"
    Public Shared ActivityNo As String = "pmdt074"
    Public Shared ReplenishmentSpecificationsDescriptions As String = "pmdt227"
    Public Shared InvoiceCode As String = "pmdt049"
    Public Shared InvoiceNo As String = "pmdt050"
    Public Shared BudgetDetails As String = "pmdt075"
    Public Shared ProductCategories As String = "pmdt220"
    Public Shared SourceDocProductCategories As String = "pmdt221"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  PR_Receipt By DocNo = ?  Purchaes PR Receipt : Body Item :  Receipt Detail (apmt520 :Purchaes PR Receipt : Body Item :  Purchase  Receipt Detail) ####
    Private Shared strWH_PR_Receipt_DocNo As String = "Select " & DocNo & " ," & ItemSequence & "," & PurchaseOrderNo & ", " & LineNo & "," &
        " " & PurchaseItemSN & "," & PurchaseBatchOrder & ", " & SubItemFeature & " ," & ReceiptItemNo & "," & ProductFeature & "," & Packagingcontainer & "," & OperationNo & "," &
        " " & OperationSeq & ", " & OffsetSeq & "," & StoreLocation & "," & Location & "," & LotNo & "," & ReceiptStockInUnit & ", " &
        " " & ReceiptStockInQty & "," & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & "," & PricingQty & "," & Urgency & ", " &
        " " & Inspection & "," & GoodsReceiptNo & "," & ReceiptLineNo & "," & UnitPrice & "," & TaxRate & "," & AmtExclTax & "," & AmtInclTax & ", " &
        " " & PriceDetermination & "," & Bonded & "," & PricingSource & "," & PriceReferenceNo & "," & RetrieveUP & "," & PercentSpread & "," & TaxType & " " &
        " " & Tax & "," & ReasonCode & "," & StatusCode & "," & AcceptableQty & "," & StockedQty & "," & RejectQty & "," & MainSetBilledQuantity & ", " &
        " " & AccountSet2PaidQuantity & "," & AccountSet3PaidQuantity & "," & Notes & "," & VendorBatchNo & "," & SupplierDeliveryQuantity & ", " &
        " " & MultiStoreLocationLotReceptionAndStocking & "," & InventoryManagmentFeature & "," & ShippingOrderNo & "," & ShippingLineNo & "," & PrimaryBookSetTempEstimatedQty & ", " &
        " " & AccountSet2ProvisionalEstimationsQuantity & "," & AccountSet3ProvisionalEstimationsQuantity & "," & InvoiceQty & "," & QCDocNo & "," & QCitems & ", " &
        " " & Result & "," & SelfCreatedAPRequiredYN & "," & InterCompanyProcessNo & "," & MultilateralTradeTypeForPurchasing & "," & PurchaseOrderIssuanceLocation & ", " &
        " " & StockNote & "," & ValidDate & "," & RetainFieldStart & "," & RetainFieldEnd & "," & UnshippedQuantity & "," & GoodsBarcode & "," & ReceiptPackagingUnit & ", " &
        " " & ReceiptPackagingQty & "," & Purchasing & "," & PurchaseCenter & "," & RequisitionOrganization & "," & ReservedReceptionNoteNumber & ", " &
        " " & ReservedReceptionItem & "," & PurchaseChannel & "," & ChannelProperty & "," & OperatingMethod & "," & Settlement_Method & "," & ContractNo & ", " &
        " " & AgreementNo & "," & AccountOrg & "," & Reference & "," & ReferenceItemSeq & "," & PurchaseMethod & "," & FinalReceptionOrganization & ", " &
        " " & PriceReferenceItems & "," & ReturnRequisitionNumber & "," & ReturnRequestItems & "," & ProductLentQuantityByPrices & "," & ProductLentReturnedByPricesReferenceQuantity & ", " &
        " " & UntaxedCounterofferAmount & "," & TaxedCounterofferAmount & "," & PurchasePrice & "," & CreatedDate & "," & ProjectNo & ", " &
        " " & WBS & "," & ActivityNo & "," & ReplenishmentSpecificationsDescriptions & "," & InvoiceCode & "," & InvoiceNo & ", " &
        " " & BudgetDetails & "," & ProductCategories & "," & SourceDocProductCategories & "  " &
        " FROM " & PMDT.tblPRreceiptDetailBody & "  " &
        " where " & wStandard & " And  " & DocNo & " = @pPR_Receipt_DocNo  "
    Public Shared Function GetPR_Receipt_By_DocNo_BodyReceiptDetail(strPR_Receipt_DocNo As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_DocNo_BodyReceiptDetail", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_DocNo_BodyReceiptDetailDataSet(strPR_Receipt_DocNo As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_DocNo_BodyReceiptDetailDataSet", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where PR_Receipt By PO_No = ?  Purchaes PR Receipt : Body Item :  Receipt Detail (apmt520 :Purchaes PR Receipt : Body Item :  Purchase  Receipt Detail) ####
    Private Shared strWH_PR_Receipt_PO_No As String = "Select " & DocNo & " ," & ItemSequence & "," & PurchaseOrderNo & ", " & LineNo & "," &
        " " & PurchaseItemSN & "," & PurchaseBatchOrder & ", " & SubItemFeature & " ," & ReceiptItemNo & "," & ProductFeature & "," & Packagingcontainer & "," & OperationNo & "," &
        " " & OperationSeq & ", " & OffsetSeq & "," & StoreLocation & "," & Location & "," & LotNo & "," & ReceiptStockInUnit & ", " &
        " " & ReceiptStockInQty & "," & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & "," & PricingQty & "," & Urgency & ", " &
        " " & Inspection & "," & GoodsReceiptNo & "," & ReceiptLineNo & "," & UnitPrice & "," & TaxRate & "," & AmtExclTax & "," & AmtInclTax & ", " &
        " " & PriceDetermination & "," & Bonded & "," & PricingSource & "," & PriceReferenceNo & "," & RetrieveUP & "," & PercentSpread & "," & TaxType & " " &
        " " & Tax & "," & ReasonCode & "," & StatusCode & "," & AcceptableQty & "," & StockedQty & "," & RejectQty & "," & MainSetBilledQuantity & ", " &
        " " & AccountSet2PaidQuantity & "," & AccountSet3PaidQuantity & "," & Notes & "," & VendorBatchNo & "," & SupplierDeliveryQuantity & ", " &
        " " & MultiStoreLocationLotReceptionAndStocking & "," & InventoryManagmentFeature & "," & ShippingOrderNo & "," & ShippingLineNo & "," & PrimaryBookSetTempEstimatedQty & ", " &
        " " & AccountSet2ProvisionalEstimationsQuantity & "," & AccountSet3ProvisionalEstimationsQuantity & "," & InvoiceQty & "," & QCDocNo & "," & QCitems & ", " &
        " " & Result & "," & SelfCreatedAPRequiredYN & "," & InterCompanyProcessNo & "," & MultilateralTradeTypeForPurchasing & "," & PurchaseOrderIssuanceLocation & ", " &
        " " & StockNote & "," & ValidDate & "," & RetainFieldStart & "," & RetainFieldEnd & "," & UnshippedQuantity & "," & GoodsBarcode & "," & ReceiptPackagingUnit & ", " &
        " " & ReceiptPackagingQty & "," & Purchasing & "," & PurchaseCenter & "," & RequisitionOrganization & "," & ReservedReceptionNoteNumber & ", " &
        " " & ReservedReceptionItem & "," & PurchaseChannel & "," & ChannelProperty & "," & OperatingMethod & "," & Settlement_Method & "," & ContractNo & ", " &
        " " & AgreementNo & "," & AccountOrg & "," & Reference & "," & ReferenceItemSeq & "," & PurchaseMethod & "," & FinalReceptionOrganization & ", " &
        " " & PriceReferenceItems & "," & ReturnRequisitionNumber & "," & ReturnRequestItems & "," & ProductLentQuantityByPrices & "," & ProductLentReturnedByPricesReferenceQuantity & ", " &
        " " & UntaxedCounterofferAmount & "," & TaxedCounterofferAmount & "," & PurchasePrice & "," & CreatedDate & "," & ProjectNo & ", " &
        " " & WBS & "," & ActivityNo & "," & ReplenishmentSpecificationsDescriptions & "," & InvoiceCode & "," & InvoiceNo & ", " &
        " " & BudgetDetails & "," & ProductCategories & "," & SourceDocProductCategories & "  " &
        " FROM " & PMDT.tblPRreceiptDetailBody & "  " &
        " where " & wStandard & " And  " & PurchaseOrderNo & " = @pPR_Receipt_PO_No  "
    Public Shared Function GetPR_Receipt_By_PO_No_BodyReceiptDetail(StrPR_Receipt_PO_No As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_PO_No
        Sql = Sql.Replace("@pPR_Receipt_PO_No", "'" & StrPR_Receipt_PO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_PO_No_BodyReceiptDetail", "Sql = strWH_PR_Receipt_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_PO_No_BodyReceiptDetailDataSet(StrPR_Receipt_PO_No As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_PO_No
        Sql = Sql.Replace("@pPR_Receipt_PO_No", "'" & StrPR_Receipt_PO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_PO_No_BodyReceiptDetailDataSet", "Sql = strWH_PR_Receipt_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where PR_Receipt By ItemNo = ?  Purchaes PR Receipt : Body Item :  Receipt Detail (apmt520 :Purchaes PR Receipt : Body Item :  Purchase  Receipt Detail) ####
    Private Shared strWH_PR_Receipt_By_ItemNo As String = "Select " & DocNo & " ," & ItemSequence & "," & PurchaseOrderNo & ", " & LineNo & "," &
        " " & PurchaseItemSN & "," & PurchaseBatchOrder & ", " & SubItemFeature & " ," & ReceiptItemNo & "," & ProductFeature & "," & Packagingcontainer & "," & OperationNo & "," &
        " " & OperationSeq & ", " & OffsetSeq & "," & StoreLocation & "," & Location & "," & LotNo & "," & ReceiptStockInUnit & ", " &
        " " & ReceiptStockInQty & "," & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & "," & PricingQty & "," & Urgency & ", " &
        " " & Inspection & "," & GoodsReceiptNo & "," & ReceiptLineNo & "," & UnitPrice & "," & TaxRate & "," & AmtExclTax & "," & AmtInclTax & ", " &
        " " & PriceDetermination & "," & Bonded & "," & PricingSource & "," & PriceReferenceNo & "," & RetrieveUP & "," & PercentSpread & "," & TaxType & " " &
        " " & Tax & "," & ReasonCode & "," & StatusCode & "," & AcceptableQty & "," & StockedQty & "," & RejectQty & "," & MainSetBilledQuantity & ", " &
        " " & AccountSet2PaidQuantity & "," & AccountSet3PaidQuantity & "," & Notes & "," & VendorBatchNo & "," & SupplierDeliveryQuantity & ", " &
        " " & MultiStoreLocationLotReceptionAndStocking & "," & InventoryManagmentFeature & "," & ShippingOrderNo & "," & ShippingLineNo & "," & PrimaryBookSetTempEstimatedQty & ", " &
        " " & AccountSet2ProvisionalEstimationsQuantity & "," & AccountSet3ProvisionalEstimationsQuantity & "," & InvoiceQty & "," & QCDocNo & "," & QCitems & ", " &
        " " & Result & "," & SelfCreatedAPRequiredYN & "," & InterCompanyProcessNo & "," & MultilateralTradeTypeForPurchasing & "," & PurchaseOrderIssuanceLocation & ", " &
        " " & StockNote & "," & ValidDate & "," & RetainFieldStart & "," & RetainFieldEnd & "," & UnshippedQuantity & "," & GoodsBarcode & "," & ReceiptPackagingUnit & ", " &
        " " & ReceiptPackagingQty & "," & Purchasing & "," & PurchaseCenter & "," & RequisitionOrganization & "," & ReservedReceptionNoteNumber & ", " &
        " " & ReservedReceptionItem & "," & PurchaseChannel & "," & ChannelProperty & "," & OperatingMethod & "," & Settlement_Method & "," & ContractNo & ", " &
        " " & AgreementNo & "," & AccountOrg & "," & Reference & "," & ReferenceItemSeq & "," & PurchaseMethod & "," & FinalReceptionOrganization & ", " &
        " " & PriceReferenceItems & "," & ReturnRequisitionNumber & "," & ReturnRequestItems & "," & ProductLentQuantityByPrices & "," & ProductLentReturnedByPricesReferenceQuantity & ", " &
        " " & UntaxedCounterofferAmount & "," & TaxedCounterofferAmount & "," & PurchasePrice & "," & CreatedDate & "," & ProjectNo & ", " &
        " " & WBS & "," & ActivityNo & "," & ReplenishmentSpecificationsDescriptions & "," & InvoiceCode & "," & InvoiceNo & ", " &
        " " & BudgetDetails & "," & ProductCategories & "," & SourceDocProductCategories & "  " &
        " FROM " & PMDT.tblPRreceiptDetailBody & "  " &
        " where " & wStandard & " And  " & ReceiptItemNo & " = @pPR_Receipt_ItemNo  "
    Public Shared Function GetPR_Receipt_By_ItemNo_BodyReceiptDetail(StrPR_Receipt_ItemNo As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_By_ItemNo
        Sql = Sql.Replace("@pPR_Receipt_ItemNo", "'" & StrPR_Receipt_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_ItemNo_BodyReceiptDetail", "Sql = strWH_PR_Receipt_By_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_ItemNo_BodyReceiptDetailDataSet(StrPR_Receipt_ItemNo As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_By_ItemNo
        Sql = Sql.Replace("@pPR_Receipt_ItemNo", "'" & StrPR_Receipt_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDT", "GetPR_Receipt_By_ItemNo_BodyReceiptDetailDataSet", "Sql = strWH_PR_Receipt_By_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function







    'add by noi on 2017-07-06 start
    Public Shared Function Get_Receipt_PO_ITEM(fldName As ArrayList, whr As String) As DataSet
        Dim SQL As String
        Dim varIni As New VarIni
        Dim Conn_SQL As New ConnSQL
        SQL = varIni.S & Conn_SQL.getFeild(fldName) & varIni.F & tableName
        'apmt520 purchase receipt head
        SQL &= varIni.getLeftjoinFirst(varIni.PMDS, varIni.PMDT, True, PMDS.DocNo & ":" & DocNo)
        'contract name
        SQL &= varIni.getLeftjoinFirst(varIni.PMAAL, varIni.PMDS, False, PMAAL.ContactID & ":" & PMDS.PurchaseVendor)
        'item lang
        SQL &= varIni.getLeftjoinFirst(varIni.IMAAL, varIni.PMDT, False, IMAAL.ProductItem & ":" & ReceiptItemNo & "," & IMAAL.Langauge & ":" & varIni.enUS_V & ":")
        'where and order by
        SQL &= whr & varIni.getOrderBy(DocNo)
        Dim dt As DataSet = GetData.GetDataReaderOracleDataSet(SQL, "", GetData.WhoCalledMe)
        Return dt
    End Function


    'add by noi on 2017-07-06 end
End Class
