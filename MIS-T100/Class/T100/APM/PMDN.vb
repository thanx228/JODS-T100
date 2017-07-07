Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDN
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdn_t
    '''# apmt500 :Purchaes PO : Body Item : Tab Purchase Detail
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PO : Body Item : Tab Purchase Detail ##############</reamrks>
    Public Shared tblPObody As String = "pmdn_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdnent"
    Public Shared Site As String = "pmdnsite"
    Public Shared Application_Org As String = "pmdnunit"
    Public Shared PurchaseNo As String = "pmdndocno"
    Public Shared ItemSequence As String = "pmdnseq"
    Public Shared ItemNo As String = "pmdn001"
    Public Shared ProductFeature As String = "pmdn002"
    Public Shared PackagingContainer As String = "pmdn003"
    Public Shared OperationNo As String = "pmdn004"
    Public Shared OperationSeq As String = "pmdn005"
    Public Shared PurchasingDept As String = "pmdn006"
    Public Shared PurchaseQty As String = "pmdn007"
    Public Shared SumPurchaseQty As String = "Sum_Purchase_Qty"
    Public Shared ReferenceUnit As String = "pmdn008"
    Public Shared ReferenceQty As String = "pmdn009"
    Public Shared PricingUnit As String = "pmdn010"
    Public Shared PricingQty As String = "pmdn011"
    Public Shared ShippingDate As String = "pmdn012"
    Public Shared ArrivalDate As String = "pmdn013"
    Public Shared StockInDate As String = "pmdn014"
    Public Shared UnitPrice As String = "pmdn015"
    Public Shared TaxType As String = "pmdn016"
    Public Shared TaxRate As String = "pmdn017"
    Public Shared SubItemFeature As String = "pmdn019"
    Public Shared Urgency As String = "pmdn020"
    Public Shared Bond As String = "pmdn021"
    Public Shared PartialDelivery As String = "pmdn022"
    Public Shared PaymentLocation As String = "pmdnorga"
    Public Shared ShippingVendor As String = "pmdn023"
    Public Shared MultiDeliveryPeriod As String = "pmdn024"
    Public Shared ReceiptAddressNo As String = "pmdn025"
    Public Shared BillAddressNo As String = "pmdn026"
    Public Shared VendorItemNo As String = "pmdn027"
    Public Shared ReceiptWarehouseLocation As String = "pmdn028"
    Public Shared ReceiptLocation As String = "pmdn029"
    Public Shared ReceiptBatchNo As String = "pmdn030"
    Public Shared MeansOfTransportation As String = "pmdn031"
    Public Shared PickingMethod As String = "pmdn032"
    Public Shared RateOfSpares As String = "pmdn033"
    Public Shared NoUse As String = "pmdn034"
    Public Shared PriceDetermination As String = "pmdn035"
    Public Shared ProjectNo As String = "pmdn036"
    Public Shared WBSno As String = "pmdn037"
    Public Shared ActivityNo As String = "pmdn038"
    Public Shared CauseOfSpending As String = "pmdn039"
    Public Shared PricingSource As String = "pmdn040"
    Public Shared PriceReferenceNo As String = "pmdn041"
    Public Shared PriceReferenceItems As String = "pmdn042"
    Public Shared AccessPrices As String = "pmdn043"
    Public Shared PercentSpread As String = "pmdn044"
    Public Shared RowStatus As String = "pmdn045"
    Public Shared AmtExclTax As String = "pmdn046"
    Public Shared AmtInclTax As String = "pmdn047"
    Public Shared Tax As String = "pmdn048"
    Public Shared ReasonCode As String = "pmdn049"
    Public Shared Notes As String = "pmdn050"
    Public Shared RetentionClosureReasonCode As String = "pmdn051"
    Public Shared Inspection As String = "pmdn052"
    Public Shared InventoryManagmentFeature As String = "pmdn053"
    Public Shared GoodsBarcode As String = "pmdn200"
    Public Shared PackingUnit As String = "pmdn201"
    Public Shared PackingQty As String = "pmdn202"
    Public Shared ReceiptDepartment As String = "pmdn203"
    Public Shared NoUse2 As String = "pmdn204"
    Public Shared RequisitionOrganization As String = "pmdn205"
    Public Shared Stock As String = "pmdn206"
    Public Shared PurchaseInTransitQty As String = "pmdn207"
    Public Shared PreviousDaySalesVolume As String = "pmdn208"
    Public Shared SalesVolumeInThePreviousMonth As String = "pmdn209"
    Public Shared s1stWeekSalesVolume As String = "pmdn210"
    Public Shared s2ndWeekSalesVolume As String = "pmdn211"
    Public Shared s3rdWeekSalesVolume As String = "pmdn212"
    Public Shared s4thWeekSalesVolume As String = "pmdn213"
    Public Shared PurchaseChannel As String = "pmdn214"
    Public Shared ChannelProperty As String = "pmdn215"
    Public Shared OperatingMethod As String = "pmdn216"
    Public Shared SettlementMethod As String = "pmdn217"
    Public Shared ContractNo As String = "pmdn218"
    Public Shared AgreementNo As String = "pmdn219"
    Public Shared Purchaser As String = "pmdn220"
    Public Shared PurchaseDepartment As String = "pmdn221"
    Public Shared PurchaseCenter As String = "pmdn222"
    Public Shared DistributionCenter As String = "pmdn223"
    Public Shared PurchaseInvalidDate As String = "pmdn224"
    Public Shared RetainFieldStrat As String = "pmdn900"
    Public Shared RetainFieldEnd As String = "pmdn999"
    Public Shared Unit As String = "pmdnud004"
    Public Shared FinalReceptionOrganization As String = "pmdn225"
    Public Shared MaterialsReturnedQty As String = "pmdn054"
    Public Shared QuantityRepaymentReferenceQty As String = "pmdn055"
    Public Shared PriceRepaymentQty As String = "pmdn056"
    Public Shared PriceRepaymentReferenceQty As String = "pmdn057"
    Public Shared DeliveryVolumeForLongValidPeriod As String = "pmdn226"
    Public Shared ReplenishmentSpecificationsDescriptions As String = "pmdn227"
    Public Shared BudgetAC As String = "pmdn058"
    Public Shared ProductCategories As String = "pmdn228"
    Public Shared UnitPrice2 As String = "pmdnua001"
    Public Shared ConversionRatePercentageRatio As String = "pmdnua002"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared RowStusGenaral As String = RowStatus & " ='1' " 'สถานะ  Item ที่ยังไม่จบ
    Public Shared SampleGenaral As String = SubItemFeature & " ='1' " 'ประเภท Item ปกติ 
    Public Shared Sample As String = SubItemFeature & " ='9' " 'ประเภท Item ที่แถ่ม

    '--Page SaleUndeliveryStatus and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Sum POQty
    Private Shared SelectSumPOQty As String = "Select " & ItemNo & " ,sum(" & PMDO.OverallPurchaseQuantity & "-" & PMDO.ReceivedVolume & ") As POQty from " & tblPObody & "" &
        " left join " & PMDL.tblPOHeader & " On " & PurchaseNo & " = " & PMDL.PONo & "" &
        " left join " & PMDO.tblPObodyDelivery & " On " & ItemSequence & " = " & PMDO.LineNo & " and " & ItemNo & " = " & PMDO.ItemNo & "" &
        " where " & wStandard & "and " & RowStusGenaral & "and " & SampleGenaral & "" &
        " and " & PMDO.wStandard & "" &
        " and " & PMDL.wStandard & "and " & PMDL.Approved & "" &
        " and " & ItemNo & "='@PuDetItemNo' group by " & ItemNo & " order by " & ItemNo & ""
    Public Shared Function SumPOQty(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectSumPOQty
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@PuDetItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page Sales Undelivery Status PopUp and Page SLUndelStusAmountPopUp and Page SaleUndelivery Status Period 
    '--Shearch PO
    Private Shared SelectPO As String = "Select " & PMDL.PONo & "," & ItemNo & " ," & PMDO.OverallPurchaseQuantity & "," & PMDO.ReceivedVolume & "," & PMDO.ShippingDate & ",substr(" & PMDL.DataConfirmationDate & ",0,10) as CnfDate from " & tblPObody & "" &
        " left join " & PMDL.tblPOHeader & " On " & PurchaseNo & " = " & PMDL.PONo & "" &
        " left join " & PMDO.tblPObodyDelivery & " On " & ItemSequence & " = " & PMDO.LineNo & " and " & ItemNo & " = " & PMDO.ItemNo & "" &
        " where " & wStandard & "and " & RowStusGenaral & "and " & SampleGenaral & "" &
        " and " & PMDO.wStandard & "" &
        " and " & PMDL.wStandard & "and " & PMDL.Approved & "" &
        " and " & ItemNo & "='@PuDetItemNo' order by " & ItemNo & ""
    Public Shared Function ShearchPO(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectPO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@PuDetItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page CheckBOMPopupSub
    '--Shearch PO
    Private Shared SelectSupPopUPPO As String = "Select (" & PurchaseNo & " || '-' || " & PMDL.Version & ") as PONo,substr(" & PMDL.DataConfirmationDate & ",0,10) As Cnfdt ,substr(" & PMDO.ShippingDate & ",0,10) As DelQty ," & PMDO.OverallPurchaseQuantity & "," & PMDO.ReceivedVolume & "," &
        " sum(nvl(" & PMDO.OverallPurchaseQuantity & "-" & PMDO.ReceivedVolume & ",0)) As BalQty,(" & ShippingVendor & "  || ' : ' || " & PMAAL.ContactName & " ) as Vendor from " & tblPObody & "" &
        " left join " & PMDL.tblPOHeader & " On " & PurchaseNo & " = " & PMDL.PONo & "" &
        " left join " & PMDO.tblPObodyDelivery & " On " & ItemSequence & " = " & PMDO.LineNo & " and " & ItemNo & " = " & PMDO.ItemNo & "" &
        " left join " & PMAA.tbCustommerMain & " On " & ShippingVendor & " = " & PMAA.ContactID & "" &
        " left join " & PMAAL.tblCustomerName & " On " & PMAA.ContactID & " = " & PMAAL.ContactID & "" &
        " where 
        " & wStandard & "and " & RowStusGenaral & "and " & SampleGenaral & " and " &
        " " & PMDO.wStandard & " and " &
        " " & PMAA.wStandard & " and " & PMAA.Approved & " and " &
        " " & PMAAL.WStandard & " and " & PMAAL.enUS & " and " &
        " " & PMDL.wStandard & " and " & PMDL.Approved & " and " &
        " " & ItemNo & "='@ItemNo'" &
        " group by " & PurchaseNo & "," & PMDL.Version & "," & PMDL.DataConfirmationDate & "," & PMDO.ShippingDate & "," & PMDO.OverallPurchaseQuantity & "," & PMDO.ReceivedVolume & "," & ShippingVendor & "," & PMAAL.ContactName & "" &
        " order by " & PurchaseNo & ""
    Public Shared Function SupBomPopUPPO(ByVal ItemNo As String)
        Dim Oral As String = SelectSupPopUPPO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function


































    '####### where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes PO : Body Item) #########################
    Private Shared strWH_PO_No As String = "Select " & PurchaseNo & " ," & ItemSequence & "," & ItemNo & ", " & ProductFeature & "," & PackagingContainer & "," & OperationNo & ", " &
        " " & OperationSeq & " ," & PurchasingDept & "," & PurchaseQty & "," & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & ", " &
        " " & PricingQty & "," & ShippingDate & "," & ArrivalDate & "," & StockInDate & "," & UnitPrice & ", " &
        " " & TaxType & "," & TaxRate & "," & SubItemFeature & "," & Urgency & "," & Bond & "," & PartialDelivery & ", " &
        " " & PaymentLocation & "," & ShippingVendor & "," & MultiDeliveryPeriod & "," & ReceiptAddressNo & "," & BillAddressNo & ", " &
        " " & VendorItemNo & "," & ReceiptWarehouseLocation & "," & ReceiptLocation & "," & ReceiptBatchNo & "," & MeansOfTransportation & ", " &
        " " & PickingMethod & "," & RateOfSpares & "," & NoUse & "," & PriceDetermination & "," & ProjectNo & ", " &
        " " & WBSno & "," & ActivityNo & "," & CauseOfSpending & "," & PricingSource & "," & PriceReferenceNo & ", " &
        " " & PriceReferenceItems & "," & AccessPrices & "," & PercentSpread & "," & RowStatus & "," & AmtExclTax & ", " &
        " " & AmtInclTax & "," & Tax & "," & ReasonCode & "," & Notes & "," & RetentionClosureReasonCode & ", " &
        " " & Inspection & "," & InventoryManagmentFeature & "," & GoodsBarcode & "," & PackingUnit & "," & PackingQty & ", " &
        " " & ReceiptDepartment & "," & NoUse2 & "," & RequisitionOrganization & "," & Stock & "," & PurchaseInTransitQty & ", " &
        " " & PreviousDaySalesVolume & "," & SalesVolumeInThePreviousMonth & "," & s1stWeekSalesVolume & "," & s2ndWeekSalesVolume & ", " &
        " " & s3rdWeekSalesVolume & "," & s4thWeekSalesVolume & "," & PurchaseChannel & "," & ChannelProperty & "," & OperatingMethod & ", " &
        " " & SettlementMethod & "," & ContractNo & "," & AgreementNo & "," & Purchaser & "," & PurchaseDepartment & ", " &
        " " & PurchaseCenter & "," & DistributionCenter & "," & PurchaseInvalidDate & "," & RetainFieldStrat & "," & RetainFieldEnd & ", " &
        " " & FinalReceptionOrganization & "," & MaterialsReturnedQty & "," & QuantityRepaymentReferenceQty & "," & PriceRepaymentQty & "," & PriceRepaymentReferenceQty & ", " &
        " " & DeliveryVolumeForLongValidPeriod & "," & ReplenishmentSpecificationsDescriptions & "," & BudgetAC & ", " &
        " " & ProductCategories & "," & UnitPrice2 & "," & ConversionRatePercentageRatio & "  " &
        " FROM " & PMDN.tblPObody & "  " &
        " where " & wStandard & " And  " & PurchaseNo & " = @pPO_No  "
    Public Shared Function GetPO_Body(StrPO_No As String) As DataTable
        Dim Sql As String = strWH_PO_No
        Sql = Sql.Replace("@pPO_No", "'" & StrPO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDN", "GetPO_Body", "Sql = strWH_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPO_BodyDataSet(StrPO_No As String) As DataSet
        Dim Sql As String = strWH_PO_No
        Sql = Sql.Replace("@pPO_No", "'" & StrPO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDN", "GetPO_BodyDataSet", "Sql = strWH_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  Item_No = ? Tab: Purchase Detail  (for apmt500 :Purchaes PO : Body Item) #########################
    Private Shared strWH_ItemNO As String = "Select " & PurchaseNo & " ," & ItemSequence & "," & ItemNo & ", " & ProductFeature & "," & PackagingContainer & "," & OperationNo & ", " &
        " " & OperationSeq & " ," & PurchasingDept & "," & PurchaseQty & "," & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & ", " &
        " " & PricingQty & "," & ShippingDate & "," & ArrivalDate & "," & StockInDate & "," & UnitPrice & ", " &
        " " & TaxType & "," & TaxRate & "," & SubItemFeature & "," & Urgency & "," & Bond & "," & PartialDelivery & ", " &
        " " & PaymentLocation & "," & ShippingVendor & "," & MultiDeliveryPeriod & "," & ReceiptAddressNo & "," & BillAddressNo & ", " &
        " " & VendorItemNo & "," & ReceiptWarehouseLocation & "," & ReceiptLocation & "," & ReceiptBatchNo & "," & MeansOfTransportation & ", " &
        " " & PickingMethod & "," & RateOfSpares & "," & NoUse & "," & PriceDetermination & "," & ProjectNo & ", " &
        " " & WBSno & "," & ActivityNo & "," & CauseOfSpending & "," & PricingSource & "," & PriceReferenceNo & ", " &
        " " & PriceReferenceItems & "," & AccessPrices & "," & PercentSpread & "," & RowStatus & "," & AmtExclTax & ", " &
        " " & AmtInclTax & "," & Tax & "," & ReasonCode & "," & Notes & "," & RetentionClosureReasonCode & ", " &
        " " & Inspection & "," & InventoryManagmentFeature & "," & GoodsBarcode & "," & PackingUnit & "," & PackingQty & ", " &
        " " & ReceiptDepartment & "," & NoUse2 & "," & RequisitionOrganization & "," & Stock & "," & PurchaseInTransitQty & ", " &
        " " & PreviousDaySalesVolume & "," & SalesVolumeInThePreviousMonth & "," & s1stWeekSalesVolume & "," & s2ndWeekSalesVolume & ", " &
        " " & s3rdWeekSalesVolume & "," & s4thWeekSalesVolume & "," & PurchaseChannel & "," & ChannelProperty & "," & OperatingMethod & ", " &
        " " & SettlementMethod & "," & ContractNo & "," & AgreementNo & "," & Purchaser & "," & PurchaseDepartment & ", " &
        " " & PurchaseCenter & "," & DistributionCenter & "," & PurchaseInvalidDate & "," & RetainFieldStrat & "," & RetainFieldEnd & ", " &
        " " & FinalReceptionOrganization & "," & MaterialsReturnedQty & "," & QuantityRepaymentReferenceQty & "," & PriceRepaymentQty & "," & PriceRepaymentReferenceQty & ", " &
        " " & DeliveryVolumeForLongValidPeriod & "," & ReplenishmentSpecificationsDescriptions & "," & BudgetAC & ", " &
        " " & ProductCategories & "," & UnitPrice2 & "," & ConversionRatePercentageRatio & "  " &
        " FROM " & PMDN.tblPObody & "  " &
        " where " & wStandard & " And  " & ItemNo & " = @pItem_No  "
    Public Shared Function GetPO_Body_By_ItemNo(StrItemNO As String) As DataTable
        Dim Sql As String = strWH_ItemNO
        Sql = Sql.Replace("@pItem_No", "'" & StrItemNO & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDN", "GetPO_Body_By_ItemNo", "Sql = strWH_ItemNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPO_Body_ByItemNo_DataSet(StrItemNO As String) As DataSet
        Dim Sql As String = strWH_ItemNO
        Sql = Sql.Replace("@pItem_No", "'" & StrItemNO & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDN", "GetPO_Body_ByItemNo_DataSet", "Sql = strWH_ItemNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### Sum(Stock-in-Qty), SUM(Received_volume) where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes Body Item : Tab Delivery Detail) #########################
    Private Shared strSum_POqty_WH_Item_No As String = "Select SUM(" & PurchaseQty & ") as " & SumPurchaseQty & " " &
    " FROM " & PMDN.tblPObody & "  " &
    " where " & wStandard & " And  " & ItemNo & " = @POqty_WH_Item_No  "
    Public Shared Function GetPO_Body_SumStockInQty_ByItemNo_Delivery(strPOqty_WH_Item_No As String) As DataTable
        Dim Sql As String = strSum_POqty_WH_Item_No
        Sql = Sql.Replace("@POqty_WH_Item_No", "'" & strPOqty_WH_Item_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDN", "GetPO_Body_SumStockInQty_ByItemNo_Delivery", "Sql = strSum_POqty_WH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
