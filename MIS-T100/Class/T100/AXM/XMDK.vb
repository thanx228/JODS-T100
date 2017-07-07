Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Imports System.Exception
Public Class XMDK
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmdk_t
    '# axmt540 : Sale Delivery : Header
    ''' <remarks> Structure Table Sale Delivery Header </remarks>
    Public Shared tblSaleDelivery_Head As String = "xmdk_t"
    Public Shared ent As String = "xmdkent"
    Public Shared Site As String = "xmdksite"
    Public Shared SaleDeliveryNo As String = "xmdkdocno"
    Public Shared DocumentDate As String = "xmdkdocdt"
    Public Shared DocType As String = "xmdk000"
    Public Shared ApplicationOrg As String = "xmdkunit"
    Public Shared DebitDate As String = "xmdk001"
    Public Shared SalesMan As String = "xmdk003"
    Public Shared SalesDept As String = "xmdk004"
    Public Shared Status As String = "xmdkstus"
    ''' <remarks> Shipping information </remarks>
    Public Shared ShippingNoticeShippingOrderNo As String = "xmdk005"
    Public Shared OrderNo As String = "xmdk006"
    Public Shared OrderCustomer As String = "xmdk007"
    Public Shared CustomerNo As String = "xmdk008"
    Public Shared ReceivingCustomer As String = "xmdk009"
    Public Shared InvoiceTo As String = "xmdk202"
    Public Shared ShippingProperties As String = "xmdk002"
    Public Shared MultilateralProperties As String = "xmdk045"
    Public Shared ClearanceDate As String = "xmdk032"
    Public Shared SalesChannel As String = "xmdk030"
    Public Shared PackagingOrderProduction As String = "xmdk028"
    Public Shared CreateInvoices As String = "xmdk029"
    Public Shared InvoiceNo As String = "xmdk037"
    Public Shared HoldReason As String = "xmdk034"
    Public Shared ShipingNote As String = "xmdk054"
    Public Shared InTransitCostWarehouseLocation As String = "xmdk039"
    Public Shared InTransitNonCostStore As String = "xmdk040"
    ''' <remarks> Trasnport information </remarks>
    Public Shared ShippingToAddress As String = "xmdk021"
    Public Shared TransportationType As String = "xmdk022"
    Public Shared PlaceOfDepature As String = "xmdk023"
    Public Shared DeliveryDestination As String = "xmdk024"
    Public Shared DeliveryVendor As String = "xmdk020"
    Public Shared FlightNoFlightCabinNo As String = "xmdk025"
    Public Shared ShipmentStartDate As String = "xmdk026"
    Public Shared ShippingMarkCode As String = "xmdk027"
    Public Shared TransportStatus As String = "xmdk038"
    Public Shared CustomerReceptionDay As String = "xmdk055"
    ''' <remarks> Account information </remarks>
    Public Shared PaymentTermSales As String = "xmdk010"
    Public Shared TradeTerms As String = "xmdk011"
    Public Shared TaxCode As String = "xmdk012"
    Public Shared TaxRate As String = "xmdk013"
    Public Shared TaxIncludedInUP As String = "xmdk014"
    Public Shared InvoiceType As String = "xmdk015"
    Public Shared Currency As String = "xmdk016"
    Public Shared ExchRate As String = "xmdk017"
    Public Shared PricingMethod As String = "xmdk018"
    Public Shared PreferenceConditions As String = "xmdk019"
    Public Shared OrderType As String = "xmdk042"
    Public Shared ExchangeRateCalculationBasis As String = "xmdk043"
    ''' <remarks> Other information </remarks>
    Public Shared SalesClass As String = "xmdk031"
    Public Shared AdditionalItemNameSpec As String = "xmdk033"
    Public Shared IntercompanyTradingSN As String = "xmdk035"
    Public Shared IntegrateSource As String = "xmdk046"
    Public Shared IntegrationOrderNo As String = "xmdk036"
    Public Shared MultilateralTradeSwitched As String = "xmdk083"

    ''' <remarks> Else </remarks>
    Public Shared IntercompanyTradingProcessID As String = "xmdk044"
    Public Shared OverallUntaxedAmount As String = "xmdk051"
    Public Shared OverallTaxedAmount As String = "xmdk052"
    Public Shared TotalTax As String = "xmdk053"
    Public Shared CorrespondingReceiptNo As String = "xmdk081"
    Public Shared SalesReturnWay As String = "xmdk082"
    Public Shared IssueAllowanceEvidence As String = "xmdk084"
    Public Shared TransferRetailerSerialNo As String = "xmdk200"
    Public Shared DeliveryAgentNo As String = "xmdk201"
    Public Shared PromotionProjectNo As String = "xmdk203"
    Public Shared TheEntireDiscount As String = "xmdk204"
    Public Shared DeliveryPointsSerialNo As String = "xmdk205"
    Public Shared TransportRouteSerialNo As String = "xmdk206"
    Public Shared SalesOrganization As String = "xmdk207"
    Public Shared AgentDeliverNo As String = "xmdk208"
    Public Shared NoUse As String = "xmdk209"
    Public Shared NoUse2 As String = "xmdk210"
    Public Shared NoUse3 As String = "xmdk211"
    Public Shared NoUse4 As String = "xmdk212"
    Public Shared TaxedOverallStandardCurrAmount As String = "sfaa002"
    Public Shared CollectionCompletion As String = "xmdk214"
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "xmdkownid"
    Public Shared DataOwnerDept As String = "xmdkowndp"
    Public Shared DataCreatedBy As String = "xmdkcrtid"
    Public Shared DateCreatedByDept As String = "xmdkcrtdp"
    Public Shared DataCreatedDate As String = "xmdkcrtdt"
    Public Shared ModifiedBy As String = "xmdkmodid"
    Public Shared LastModifiedDate As String = "xmdkmoddt"
    Public Shared DataConfirmationPersonnel As String = "xmdkcnfid"
    Public Shared DataConfirmationDate As String = "xmdkcnfdt"
    Public Shared DataPoster As String = "xmdkpstid"
    Public Shared DataPostedDate As String = "xmdkpstdt"
    ''' <remarks> Condition  </remarks>
    Private Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '###### for All Field >> Sale Delivery : Header Where Sale Doc No. = ? ############################################################
    Private Shared strWHAll_Sale_No As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & SaleDeliveryNo & " =@Sale_No "
    Public Shared Function getSaleDelivery_HeaderDetailAll_SaleNo(strSale_No As String) As DataTable
        Dim Sql As String = strWHAll_Sale_No.Replace("@Sale_No", "'" & strSale_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_SaleNo", "Sql = strWHAll_Sale_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetailAll_SaleNo_DataSet(strSale_No As String) As DataSet
        Dim Sql As String = strWHAll_Sale_No.Replace("@Sale_No", "'" & strSale_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_SaleNo_DataSet", "Sql = strWHAll_Sale_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '#### for All Field >> Sale Delivery : Header Where Status  = ? ############################################################
    Private Shared strWHAll_Status As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & Status & " =@pStatus "
    Public Shared Function getSaleDelivery_HeaderDetailAll_Status(strStatus As String) As DataTable
        Dim Sql As String = strWHAll_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_Status", "Sql = strWHAll_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetailAll_Status_DataSet(strStatus As String) As DataSet
        Dim Sql As String = strWHAll_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_Status_DataSet", "Sql = strWHAll_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '##### for All Field >> Sale Delivery : Header Where Customer No.  = ? ############################################################
    Private Shared strWHAll_CustomerNo As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & CustomerNo & " =@pCustomerNo "
    Public Shared Function getSaleDelivery_HeaderDetailAll_CustomerNo(strCustomerNo As String) As DataTable
        Dim Sql As String = strWHAll_CustomerNo.Replace("@pCustomerNo", "'" & strCustomerNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_CustomerNo", "Sql = strWHAll_CustomerNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetailAll_CustomerNo_DataSet(strCustomerNo As String) As DataSet
        Dim Sql As String = strWHAll_CustomerNo.Replace("@pCustomerNo", "'" & strCustomerNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_CustomerNo_DataSet", "Sql = strWHAll_CustomerNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '##### for All Field >> Sale Delivery : Header Where DocumentDate Between StartDate = ? and EndDate = ? #####################
    Private Shared strWHAll_DocumentDateBetween As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & DocumentDate & " BETWEEN TO_DATE (@pStartDate, 'yyyy/mm/dd') AND TO_DATE (@pEndDate, 'yyyy/mm/dd') "
    Public Shared Function getSaleDelivery_HeaderDetailAll_DateBetween(StartDate As String, EndDate As String) As DataTable
        Dim Sql As String = strWHAll_DocumentDateBetween
        Sql = Sql.Replace("@pStartDate", "'" & StartDate & "'")
        Sql = Sql.Replace("@pEndDate", "'" & EndDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_DateBetween", "Sql = strWHAll_DocumentDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetailAll_DateBetween_DataSet(StartDate As String, EndDate As String) As DataSet
        Dim Sql As String = strWHAll_DocumentDateBetween
        Sql = Sql.Replace("@pStartDate", "'" & StartDate & "'")
        Sql = Sql.Replace("@pEndDate", "'" & EndDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetailAll_DateBetween_DataSet", "Sql = strWHAll_DocumentDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '####### for axmt540 : Sale Delivery : Header Where Sale Doc No. = ? ############################################################
    Private Shared strWH_Sale_No As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & SaleDeliveryNo & " =@Sale_No "
    Public Shared Function getSaleDelivery_HeaderDetail_SaleNo(strSale_No As String) As DataTable
        Dim Sql As String = strWH_Sale_No.Replace("@Sale_No", "'" & strSale_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_SaleNo", "Sql = strWH_Sale_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetail_SaleNo_DataSet(strSale_No As String) As DataSet
        Dim Sql As String = strWH_Sale_No.Replace("@Sale_No", "'" & strSale_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_SaleNo_DataSet", "Sql = strWH_Sale_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '############## for axmt540 : Sale Delivery : Header Where DocumentDate Between StartDate = ? and EndDate = ? ##############################
    Private Shared strWH_DocumentDate_Between As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & DocumentDate & " BETWEEN TO_DATE (@pStartDate, 'yyyy/mm/dd') AND TO_DATE (@pEndDate, 'yyyy/mm/dd') "
    Public Shared Function getSaleDelivery_HeaderDetail_DateBetween(StartDate As String, EndDate As String) As DataTable
        Dim Sql As String = strWH_DocumentDate_Between
        Sql = Sql.Replace("@pStartDate", "'" & StartDate & "'")
        Sql = Sql.Replace("@pEndDate", "'" & EndDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_DateBetween", "Sql = strWH_DocumentDate_Between", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetail_DateBetween_DataSet(StartDate As String, EndDate As String) As DataSet
        Dim Sql As String = strWH_DocumentDate_Between
        Sql = Sql.Replace("@pStartDate", "'" & StartDate & "'")
        Sql = Sql.Replace("@pEndDate", "'" & EndDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_DateBetween_DataSet", "Sql = strWH_DocumentDate_Between", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '##### for axmt540 : Sale Delivery : Header Where Status = ? ############################################################
    Private Shared strWH_Status As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & Status & " =@pStatus "
    Public Shared Function getSaleDelivery_HeaderDetail_Status(strStatus As String) As DataTable
        Dim Sql As String = strWH_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_Status", "Sql = strWH_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetail_Status_DataSet(strStatus As String) As DataSet
        Dim Sql As String = strWH_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_Status_DataSet", "Sql = strWH_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '##### for axmt540 : Sale Delivery : Header Where Customer No. = ? ############################################################
    Private Shared strWH_CustomerNo As String = "Select " & SaleDeliveryNo & " ," & DocumentDate & "," & DocType & ", " & ApplicationOrg & ", " &
       " " & DebitDate & " ," & SalesMan & "," & SalesDept & "," & Status & "," & ShippingNoticeShippingOrderNo & "," & OrderNo & ", " &
       " " & OrderCustomer & "," & CustomerNo & "," & ReceivingCustomer & "," & InvoiceTo & "," & ShippingProperties & ", " &
       " " & MultilateralProperties & "," & ClearanceDate & "," & SalesChannel & "," & PackagingOrderProduction & "," & CreateInvoices & "," &
       " " & InvoiceNo & "," & HoldReason & "," & ShipingNote & "," & InTransitCostWarehouseLocation & "," & InTransitNonCostStore & "," &
       " " & ShippingToAddress & "," & TransportationType & "," & PlaceOfDepature & "," & DeliveryDestination & "," & DeliveryVendor & "," &
       " " & FlightNoFlightCabinNo & "," & ShipmentStartDate & "," & ShippingMarkCode & "," & TransportStatus & "," & CustomerReceptionDay & ", " &
       " " & PaymentTermSales & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & "," & InvoiceType & "," &
       " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PreferenceConditions & "," & OrderType & "," & ExchangeRateCalculationBasis & ", " &
       " " & SalesClass & "," & AdditionalItemNameSpec & "," & IntercompanyTradingSN & "," & IntegrateSource & "," & IntegrationOrderNo & ", " &
       " " & MultilateralTradeSwitched & "," & IntercompanyTradingProcessID & "," & OverallUntaxedAmount & "," & OverallTaxedAmount & "," & TotalTax & "," &
       " " & CorrespondingReceiptNo & "," & SalesReturnWay & "," & IssueAllowanceEvidence & "," & TransferRetailerSerialNo & "," & DeliveryAgentNo & ", " &
       " " & PromotionProjectNo & "," & TheEntireDiscount & "," & DeliveryPointsSerialNo & "," & TransportRouteSerialNo & "," & SalesOrganization & ", " &
       " " & AgentDeliverNo & "," & NoUse & "," & NoUse2 & "," & NoUse3 & "," & NoUse4 & "," & CollectionCompletion & "," &
       " " & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & ", " &
       " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & ", " &
       " " & DataPoster & "," & DataPostedDate & " " &
       " FROM " & tblSaleDelivery_Head & "  " &
       " where " & wStandard & " AND " & CustomerNo & " =@pCustomerNo "
    Public Shared Function getSaleDelivery_HeaderDetail_CustomerNo(strCustomerNo As String) As DataTable
        Dim Sql As String = strWH_CustomerNo.Replace("@pCustomerNo", "'" & strCustomerNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_CustomerNo", "Sql = strWH_CustomerNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_HeaderDetail_CustomerNo_DataSet(strCustomerNo As String) As DataSet
        Dim Sql As String = strWH_CustomerNo.Replace("@pCustomerNo", "'" & strCustomerNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDK", "getSaleDelivery_HeaderDetail_CustomerNo_DataSet", "Sql = strWH_CustomerNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
