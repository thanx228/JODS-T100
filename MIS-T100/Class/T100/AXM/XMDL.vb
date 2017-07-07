Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMDL
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmdl_t
    '# axmt540 : Sale Delivery : Body Tab >> Shippinh Detail
    ''' <remarks> Structure Table Sale Delivery Body Tab >> Shippinh Detailr </remarks>
    Public Shared tblSaleDelivery_Body_ShipingDetail As String = "xmdl_t"
    Public Shared ent As String = "xmdlent"
    Public Shared Site As String = "xmdlsite"
    Public Shared SaleDeliveryNo As String = "xmdldocno"
    Public Shared LinNo As String = "xmdlseq"
    Public Shared ShipingNoteNo As String = "xmdl001"
    Public Shared NoteItemNo As String = "xmdl002"
    Public Shared SaleOrderNo As String = "xmdl003"
    Public Shared SOLine As String = "xmdl004"
    Public Shared OrderLineSN As String = "xmdl005"
    Public Shared BacthOrder As String = "xmdl006"
    Public Shared SubPartFeatures As String = "xmdl007"
    Public Shared itemNo As String = "xmdl008"
    Public Shared ProductCharacteristics As String = "xmdl009"
    Public Shared PackagingContainer As String = "xmdl010"
    Public Shared OperationNo As String = "xmdl011"
    Public Shared OperationSeq As String = "xmdl012"
    Public Shared MultiStoreLocationLotShipping As String = "xmdl013"
    Public Shared WH As String = "xmdl014"
    Public Shared Location As String = "xmdl015"
    Public Shared LotNo As String = "xmdl016"
    Public Shared ShippingUnit As String = "xmdl017"
    Public Shared Quantity As String = "xmdl018"
    Public Shared ReferenceUnit As String = "xmdl019"
    Public Shared ReferenceQty As String = "xmdl020"
    Public Shared PricingUnit As String = "xmdl021"
    Public Shared PricingQty As String = "xmdl022"
    Public Shared Inspection As String = "xmdl023"
    Public Shared UnitPrice As String = "xmdl024"
    Public Shared TaxCode As String = "xmdl025"
    Public Shared TaxRate As String = "xmdl026"
    Public Shared AmountBeforeTax As String = "xmdl027"
    Public Shared AmountAfterTax As String = "xmdl028"
    Public Shared UnitPriceBeforeTax As String = "xmdl029"
    Public Shared ProjectNo As String = "xmdl030"
    Public Shared WBSNo As String = "xmdl031"
    Public Shared ActivityNo As String = "xmdl032"
    Public Shared CustomersItemNo As String = "xmdl033"
    Public Shared QPA As String = "xmdl034"
    Public Shared SigningForReceiptQty As String = "xmdl035"
    Public Shared SigningForRejectQty As String = "xmdl036"
    Public Shared SalesReturnAmountAlready As String = "xmdl037"
    Public Shared MainSetJournalizedQuantity As String = "xmdl038"
    Public Shared AccountSet2PostedQuantity As String = "xmdl039"
    Public Shared AccountSet3PostedQuantity As String = "xmdl040"
    Public Shared Bonded As String = "xmdl041"
    Public Shared PricingSource As String = "xmdl042"
    Public Shared PriceSourceReferenceNo As String = "xmdl043"
    Public Shared PriceSourceReferenceItems As String = "xmdl044"
    Public Shared AccessPrices As String = "xmdl045"
    Public Shared PriceDifferenceRatio As String = "xmdl046"
    Public Shared InvoiceQty As String = "xmdl047"
    Public Shared InvoiceNo As String = "xmdl048"
    Public Shared ReasonCode As String = "xmdl050"
    Public Shared Memo As String = "xmdl051"
    Public Shared InventoryManagementCharacteristics As String = "xmdl052"
    Public Shared PrimaryBookSetTempEstimatedQty As String = "xmdl053"
    Public Shared AccountSet2ProvisionalEstimationsQuantity As String = "xmdl054"
    Public Shared AccountSet3ProvisionalEstimationsQuantity As String = "xmdl055"
    Public Shared CheckOutQuantity As String = "xmdl081"
    Public Shared CheckOutValuationQuantity As String = "xmdl083"
    Public Shared CheckOutReasonCode As String = "xmdl084"
    Public Shared OrderIssuanceLocation As String = "xmdl085"
    Public Shared MultilateralOrderProperties As String = "xmdl086"
    Public Shared SelfCreatedReceivablesRequiredYN As String = "xmdl087"
    Public Shared InterCompanyTradingProcessID As String = "xmdl088"
    Public Shared QCDocNo As String = "xmdl089"
    Public Shared Determinant As String = "xmdl090"
    Public Shared InspectionResult As String = "xmdl091"
    Public Shared ProductLentQuantityByQuantities As String = "xmdl092"
    Public Shared ProductLentReturnedByQuantitiesReferenceQuantity As String = "xmdl093"
    Public Shared SalesChannel As String = "xmdl200"
    Public Shared ProductGroupCode As String = "xmdl201"
    Public Shared SalesScopeCode As String = "xmdl202"
    Public Shared SalesOffice As String = "xmdl203"
    Public Shared ShippingPackagingUnit As String = "xmdl204"
    Public Shared ShippingPackagingQuantity As String = "xmdl205"
    Public Shared CheckOutPackagingQuantity As String = "xmdl206"
    Public Shared InventoryLockingGrade As String = "xmdl207"
    Public Shared StandardPrice As String = "xmdl208"
    Public Shared PromotionPrice As String = "xmdl209"
    Public Shared Tradingprice As String = "xmdl210"
    Public Shared DiscountAmount As String = "xmdl211"
    Public Shared SalesOrganization As String = "xmdl212"
    Public Shared Salesman As String = "xmdl213"
    Public Shared Salesdepartment As String = "xmdl214"
    Public Shared ContractNo As String = "xmdl215"
    Public Shared OperatingMethod As String = "xmdl216"
    Public Shared SettlementType As String = "xmdl217"
    Public Shared BalanceType As String = "xmdl218"
    Public Shared TransactionType As String = "xmdl219"
    Public Shared ConsignWrittenOffQuantity As String = "xmdl220"
    Public Shared Region As String = "xmdl222"
    Public Shared NumberCounties As String = "xmdl223"
    Public Shared ProvinceRegionserialNo As String = "xmdl224"
    Public Shared Region2 As String = "xmdl225"
    Public Shared GoodsBarcode As String = "xmdl226"
    Public Shared ApplicationOrg As String = "xmdlunit"
    Public Shared AccountOrg As String = "xmdlorga"
    Public Shared TestPassQuantity As String = "xmdl056"
    Public Shared SourceDocNoSalesReturns As String = "xmdl094"
    Public Shared ItemsSalesReturn As String = "xmdl095"

    ''' <remarks> Condition  </remarks>
    Private Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '--Page CustomsNew
    '--Check ShipOrderNo from Sales Invoice same ShipOrderNo from Sales Delivery
    Private Shared CheckShipOrderNoOral As String =
        "select " & SOLine & "," & SaleDeliveryNo & "," & ShipingNoteNo & " from " & tblSaleDelivery_Body_ShipingDetail & " where " & wStandard & " and " &
        " " & SOLine & " ='@SeqSOformDelivery' and " & SaleDeliveryNo & " ='@ShipOrderNo'"
    Public Shared Function SODelDetail(ByVal SeqSOfromSLInv As String, ByVal ShipOrderNo As String)
        Dim Oral As String = CheckShipOrderNoOral
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SeqSOformDelivery", SeqSOfromSLInv)
        Oral = Oral.Replace("@ShipOrderNo", ShipOrderNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function




    '######### for All Filed Sale Delivery Where Sale Delivery Doc_No. = ? : Body Tab : Shippinh Detail #########################
    Private Shared strWH_SaleDeliveryNo As String = "Select " & SaleDeliveryNo & " ," & LinNo & "," & ShipingNoteNo & ", " & NoteItemNo & ", " &
       " " & SaleOrderNo & " ," & SOLine & "," & OrderLineSN & "," & BacthOrder & "," & SubPartFeatures & "," & itemNo & ", " &
       " " & ProductCharacteristics & "," & PackagingContainer & "," & OperationNo & "," & OperationSeq & "," & MultiStoreLocationLotShipping & ", " &
       " " & WH & "," & Location & "," & LotNo & "," & ShippingUnit & "," & Quantity & "," & ReferenceUnit & "," & ReferenceQty & "," &
       " " & PricingUnit & "," & PricingQty & "," & Inspection & "," & UnitPrice & "," & TaxCode & "," & TaxRate & "," & AmountBeforeTax & "," &
       " " & AmountAfterTax & "," & UnitPriceBeforeTax & "," & ProjectNo & "," & WBSNo & "," & ActivityNo & "," & CustomersItemNo & "," &
       " " & SigningForReceiptQty & "," & SigningForRejectQty & "," & SalesReturnAmountAlready & "," & MainSetJournalizedQuantity & ", " &
       " " & AccountSet2PostedQuantity & "," & AccountSet3PostedQuantity & "," & Bonded & "," & PricingSource & "," & PriceSourceReferenceNo & "," &
       " " & PriceSourceReferenceItems & "," & AccessPrices & "," & PriceDifferenceRatio & "," & InvoiceQty & "," & InvoiceNo & "," & ReasonCode & "," & Memo & ", " &
       " " & InventoryManagementCharacteristics & "," & PrimaryBookSetTempEstimatedQty & "," & AccountSet2ProvisionalEstimationsQuantity & "," &
       " " & AccountSet3ProvisionalEstimationsQuantity & "," & CheckOutQuantity & "," & CheckOutValuationQuantity & "," & CheckOutReasonCode & "," &
       " " & OrderIssuanceLocation & "," & MultilateralOrderProperties & "," & SelfCreatedReceivablesRequiredYN & "," & InterCompanyTradingProcessID & "," &
       " " & Determinant & "," & InspectionResult & "," & ProductLentQuantityByQuantities & "," & ProductLentReturnedByQuantitiesReferenceQuantity & "," &
       " " & SalesChannel & "," & ProductGroupCode & "," & SalesScopeCode & "," & SalesOffice & "," & ShippingPackagingUnit & "," & ShippingPackagingQuantity & ", " &
       " " & CheckOutPackagingQuantity & "," & InventoryLockingGrade & "," & StandardPrice & "," & PromotionPrice & "," & Tradingprice & "," & DiscountAmount & "," &
       " " & SalesOrganization & "," & Salesman & "," & Salesdepartment & "," & ContractNo & "," & OperatingMethod & "," & SettlementType & "," & BalanceType & "," &
       " " & TransactionType & "," & ConsignWrittenOffQuantity & "," & Region & "," & NumberCounties & "," & ProvinceRegionserialNo & "," & Region2 & "," &
       " " & GoodsBarcode & "," & ApplicationOrg & "," & AccountOrg & "," & TestPassQuantity & "," & SourceDocNoSalesReturns & "," & ItemsSalesReturn & " " &
       " FROM " & tblSaleDelivery_Body_ShipingDetail & "  " &
       " where " & wStandard & " AND " & SaleDeliveryNo & " =@pSaleDelivery_No "
    Public Shared Function getSaleDelivery_ShippinhDetail(strSaleDelivery_No As String) As DataTable
        Dim Sql As String = strWH_SaleDeliveryNo.Replace("@pSaleDelivery_No", "'" & strSaleDelivery_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDL", "getSaleDelivery_ShippinhDetail", "Sql = strWH_SaleDeliveryNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_ShippinhDetail_DataSet(strSaleDelivery_No As String) As DataSet
        Dim Sql As String = strWH_SaleDeliveryNo.Replace("@pSaleDelivery_No", "'" & strSaleDelivery_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDL", "getSaleDelivery_ShippinhDetail_DataSet", "Sql = strWH_SaleDeliveryNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class
