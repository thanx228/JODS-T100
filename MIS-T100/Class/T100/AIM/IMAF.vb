Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class IMAF
    '# Module : AIM
    Private Shared AIM As String = "AIM"
    '# Table : imaf_t
    '# aimm213 : Item Property(Sale)  :  Item Property(Sale)
    '''<reamrks>## Table Item Property(Sale)  ##############</reamrks>
    Public Shared tblSaleItemProperty As String = "imaf_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> Basic Data </reamrks>
    Public Shared ItemNo As String = "imaf001"
    Public Shared MainGroup As String = "imaf011"
    Public Shared StockControlMethod As String = "imaf012"
    Public Shared SupplyStrategy As String = "imaf013"
    Public Shared DemandCalculationMethod As String = "imaf014"
    Public Shared ReferenceUnit As String = "imaf015"
    Public Shared LocationLifecycle As String = "imaf016"
    Public Shared TaxCode As String = "imaf017"
    Public Shared UseAccessories As String = "imaf018"
    Public Shared MonthsInPurchasePeriod As String = "imaf021"
    Public Shared DaysInPurchasePeriod As String = "imaf022"
    Public Shared SupplementedVolumeForThePeriod As String = "imaf023"
    Public Shared ReorderPoint As String = "imaf024"
    Public Shared ReorderPointQuantity As String = "imaf025"
    Public Shared SafetyStock As String = "imaf026"
    Public Shared SafeInventoryQuantity As String = "imaf027"
    Public Shared NumberOfMonthsInValidPeriod As String = "imaf031"
    Public Shared AddedDaysInValidPeriod As String = "imaf032"
    Public Shared QuarantineIsolationDays As String = "imaf033"
    Public Shared BondedItem As String = "imaf034"
    Public Shared CorrespondingNonBondedItemNo As String = "imaf035"
    Public Shared InventoryGrouping As String = "imaf051"
    Public Shared StoreKeeper As String = "imaf052"
    Public Shared LocationInventoryUnit As String = "imaf053"
    Public Shared InventoryMultipleUnits As String = "imaf054"
    Public Shared InventoryManagementCharacteristics As String = "imaf055"
    Public Shared NoUse As String = "imaf056"
    Public Shared ABCclassification As String = "imaf057"
    Public Shared StockPreparationStrategy As String = "imaf058"
    Public Shared PickingStrategy As String = "imaf059"
    Public Shared InventoryBatchNumberControlMethod As String = "imaf061"
    Public Shared AutomaticCodingOfInventoryBatchNumber As String = "imaf062"
    Public Shared InventoryBatchNumberCodingMethod As String = "imaf063"
    Public Shared InventoryBatchNoUniquenessCheckingAndControl As String = "imaf064"
    Public Shared ManufacturingBatchNumberControlMethod As String = "imaf071"
    Public Shared LotSerialNoAutomaticCoding As String = "imaf072"
    Public Shared ManufacturingBatchNumberCodingMethod As String = "imaf073"
    Public Shared ManufacturingBatchNumberUniquenessCheckingAndControl As String = "imaf074"
    Public Shared SNcontrolMethod As String = "imaf081"
    Public Shared AutoGenerateSN As String = "imaf082"
    Public Shared SNcodingMethod As String = "imaf083"
    Public Shared SNuniquenessCheckAndControl As String = "imaf084"
    Public Shared DefaultWH As String = "imaf091"
    Public Shared DefaultLocation As String = "imaf092"
    Public Shared NoUse2 As String = "imaf093"
    Public Shared CountingToleranceNumber As String = "imaf094"
    Public Shared CountingToleranceRate As String = "imaf095"
    Public Shared BillingIdleDate As String = "imaf096"
    Public Shared NoUse3 As String = "imaf097"
    Public Shared TransferBatch As String = "imaf101"
    Public Shared MinimumTransforQuantity As String = "imaf102"
    Public Shared SalesGrouping As String = "imaf111"
    Public Shared SalesUnit As String = "imaf112"
    Public Shared SalesDenominated As String = "imaf113"
    Public Shared SalesBatch As String = "imaf114"
    Public Shared MinimumSalesQuantity As String = "imaf115"
    Public Shared SalesBatchControlMethod As String = "imaf116"
    Public Shared GuaranteeWarrantyMonths As String = "imaf117"
    Public Shared GuaranteeWarrantyDays As String = "imaf118"
    Public Shared PresetDomesticInternationalSales As String = "imaf121"
    Public Shared OrderSubpartDisassemblyMethod As String = "imaf122"
    Public Shared PreferredPackagingContainer As String = "imaf123"
    Public Shared SalesStockDaysInAdvance As String = "imaf124 "
    Public Shared ForecastItemNo As String = "imaf125"
    Public Shared ShippingSubstitution As String = "imaf126"
    Public Shared CalculateExceptionProduct As String = "imaf127"
    Public Shared ExcessiveSalesDeliveryRate As String = "imaf128"
    Public Shared PurchaseGrouping As String = "imaf141"
    Public Shared Purchaser As String = "imaf142"
    Public Shared PurchasingDept As String = "imaf143"
    Public Shared PurchasePricingUnit As String = "imaf144"
    Public Shared PurchaseBatchSize As String = "imaf145"
    Public Shared MinimumPurchase As String = "imaf146"
    Public Shared PurchasingBatchModeControlManager As String = "imaf147"
    Public Shared EconomicOrderQuantity As String = "imaf148"
    Public Shared AvgOrderQty As String = "imaf149"
    Public Shared PresetInternalExternalPurchase As String = "imaf151"
    Public Shared SupplierSelectionMethod As String = "imaf152"
    Public Shared MainVendor As String = "imaf153"
    Public Shared MainSupplierAssignmentLimit As String = "imaf154"
    Public Shared AllocationPositionalNotationMultiples As String = "imaf155"
    Public Shared SupplyMode As String = "imaf156"
    Public Shared PreferredPackagingContainer2 As String = "imaf157"
    Public Shared OrderDisassemblyMethodPurchase As String = "imaf158"
    Public Shared PurchaseSubstitute As String = "imaf161"
    Public Shared PurchaseReceptionSubstitute As String = "imaf162"
    Public Shared PurchaseContractOffset As String = "imaf163"
    Public Shared PurchaseLossRate As String = "imaf164"
    Public Shared SpareRateDuringPurchase As String = "imaf165"
    Public Shared PurchasingSuperCrossRate As String = "imaf166"
    Public Shared PurchaseFileLeadTime As String = "imaf171"
    Public Shared PurchaseDeliveryLeadTime As String = "imaf172"
    Public Shared PurchaseArrivalLeadTime As String = "imaf173"
    Public Shared PurchaseStockInLeadTime As String = "imaf174"
    Public Shared StrictlyAdhereToDeliveryDateLeadTime As String = "imaf175"
    Public Shared ReceiptTime As String = "imaf176"
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "imafownid"
    Public Shared DataOwnerDept As String = "imafowndp"
    Public Shared DataCreatedBy As String = "imafcrtid"
    Public Shared DateCreatedByDept As String = "imafcrtdp"
    Public Shared DataCreatedDate As String = "imafcrtdt"
    Public Shared ModifiedBy As String = "imafmodid"
    Public Shared LastModifiedDate As String = "imafmoddt"
    Public Shared DataConfirmationPersonnel As String = "imafcnfid"
    Public Shared DataConfirmationDate As String = "imafcnfdt"
    Public Shared Status As String = "imafstus"
    Public Shared BarcodeCodingMethod As String = "imaf178"
    Public Shared BarcodePackagingQuantity As String = "imaf179"

    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "imafent"
    Private Shared Site As String = "imafsite"
    Public Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "

    '--Page SalesOrderChangeStatus
    '--Select WareHouse where ItemNo  / No Refresh DataTable
    Private Shared SelectItemNoMOLine As String = "select " & ItemNo & ",nvl(" & DefaultWH & ",'No WH') as WareHouse from " & tblSaleItemProperty & " " &
    " where " & wStandard & " and  " & ItemNo & " ='@ItemNo'"
    Public Shared Function GetItemNoMOLine(ByVal ItemNoMOLine As String)
        Dim Oral As String = SelectItemNoMOLine
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNoMOLine)
        tempDataTable = GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function

    '--Select WareHouse where ItemNo  / Refresh DataTable
    Private Shared SelectWH As String = "select " & ItemNo & ",nvl(" & DefaultWH & ",'No WH') as WareHouse from " & tblSaleItemProperty & " " &
    " where " & wStandard & " and  " & ItemNo & " ='@ItemNo'"
    Public Shared Function GetItem(ByVal Item As String)
        Dim Oral As String = SelectWH
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", Item)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function




    '''<remarks> Item Property(Sale) All Field : where ItemNo. ='?' </remarks> 
    Private Shared SqlSiteSalePropertyAllField_ItemNo As String = "Select " & ItemNo & "," & MainGroup & "," & StockControlMethod & "," & SupplyStrategy & "," & DemandCalculationMethod & ", " &
    "  " & ReferenceUnit & "," & LocationLifecycle & "," & TaxCode & "," & UseAccessories & "," & MonthsInPurchasePeriod & "," & DaysInPurchasePeriod & "," & SupplementedVolumeForThePeriod & ", " &
    "  " & ReorderPoint & "," & ReorderPointQuantity & "," & SafetyStock & "," & SafeInventoryQuantity & "," & NumberOfMonthsInValidPeriod & "," & AddedDaysInValidPeriod & ", " &
    "  " & QuarantineIsolationDays & "," & BondedItem & "," & CorrespondingNonBondedItemNo & "," & InventoryGrouping & "," & StoreKeeper & "," & LocationInventoryUnit & ", " &
    "  " & InventoryMultipleUnits & "," & InventoryManagementCharacteristics & "," & NoUse & "," & ABCclassification & "," & StockPreparationStrategy & "," & PickingStrategy & ", " &
    "  " & InventoryBatchNumberControlMethod & "," & AutomaticCodingOfInventoryBatchNumber & "," & InventoryBatchNumberCodingMethod & ", " &
    "  " & InventoryBatchNoUniquenessCheckingAndControl & "," & ManufacturingBatchNumberControlMethod & "," & LotSerialNoAutomaticCoding & ", " &
    "  " & ManufacturingBatchNumberCodingMethod & "," & ManufacturingBatchNumberUniquenessCheckingAndControl & "," & SNcontrolMethod & "," & AutoGenerateSN & ", " &
    "  " & SNcodingMethod & "," & SNuniquenessCheckAndControl & "," & DefaultWH & "," & DefaultLocation & "," & NoUse2 & "," & CountingToleranceNumber & ",  " &
    "  " & CountingToleranceRate & "," & BillingIdleDate & "," & NoUse3 & "," & TransferBatch & "," & MinimumTransforQuantity & "," & SalesGrouping & "," & SalesUnit & ", " &
    "  " & SalesDenominated & "," & SalesBatch & "," & MinimumSalesQuantity & "," & SalesBatchControlMethod & "," & GuaranteeWarrantyMonths & "," & GuaranteeWarrantyDays & ", " &
    "  " & PresetDomesticInternationalSales & "," & OrderSubpartDisassemblyMethod & "," & PreferredPackagingContainer & "," & SalesStockDaysInAdvance & ",  " &
    "  " & ForecastItemNo & "," & ShippingSubstitution & "," & CalculateExceptionProduct & "," & ExcessiveSalesDeliveryRate & "," & PurchaseGrouping & "," & Purchaser & ", " &
    "  " & PurchasingDept & "," & PurchasePricingUnit & "," & PurchaseBatchSize & "," & MinimumPurchase & "," & PurchasingBatchModeControlManager & "," & EconomicOrderQuantity & ", " &
    "  " & AvgOrderQty & "," & PresetInternalExternalPurchase & "," & SupplierSelectionMethod & "," & MainVendor & "," & MainSupplierAssignmentLimit & ", " &
    "  " & AllocationPositionalNotationMultiples & "," & SupplyMode & "," & PreferredPackagingContainer2 & "," & OrderDisassemblyMethodPurchase & "," & PurchaseSubstitute & ", " &
    "  " & PurchaseReceptionSubstitute & "," & PurchaseContractOffset & "," & PurchaseLossRate & "," & SpareRateDuringPurchase & "," & PurchasingSuperCrossRate & ", " &
    "  " & PurchaseFileLeadTime & "," & PurchaseDeliveryLeadTime & "," & PurchaseArrivalLeadTime & "," & PurchaseStockInLeadTime & "," & StrictlyAdhereToDeliveryDateLeadTime & ", " &
    "  " & ReceiptTime & "," & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DateCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & ",  " &
    "  " & DataConfirmationPersonnel & "," & DataConfirmationDate & "," & Status & "," & BarcodeCodingMethod & "," & BarcodePackagingQuantity & "  " &
    "  FROM " & tblSaleItemProperty & "   where " & wStandard & " AND " & ItemNo & " =@pItemNo "
    Public Shared Function GetSiteSalePropertyAllField_By_ItemNo(sItemNo As String) As DataTable
        Dim strSQL As String = SqlSiteSalePropertyAllField_ItemNo
        strSQL = strSQL.Replace("@pItemNo", "'" & sItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAF", "GetSiteSalePropertyAllField_By_ItemNo", "Sql = SqlSiteSalePropertyAllField_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSiteSalePropertyAllFiled_By_ItemNo_DataSet(sItemNo As String) As DataSet
        Dim strSQL As String = SqlSiteSalePropertyAllField_ItemNo
        strSQL = strSQL.Replace("@pItemNo", "'" & sItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAF", "GetSiteSalePropertyAllFiled_By_ItemNo_DataSet", "Sql = SqlSiteSalePropertyAllField_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '''<remarks> Item Sale Property(for Transection Code >> aimm213)  where ItemNo. ='?' </remarks> 
    Private Shared SqlSiteSaleProperty_ItemNo As String = "Select " & ItemNo & "," & Status & "," & SalesGrouping & "," & SalesUnit & "," & SalesDenominated & ", " &
    "  " & SalesBatch & "," & MinimumSalesQuantity & "," & SalesBatchControlMethod & "," & GuaranteeWarrantyMonths & "," & GuaranteeWarrantyDays & ", " &
    "  " & PresetDomesticInternationalSales & "," & OrderSubpartDisassemblyMethod & "," & PreferredPackagingContainer & "," & SalesStockDaysInAdvance & ", " &
    "  " & ForecastItemNo & "," & ShippingSubstitution & "," & CalculateExceptionProduct & "," & ExcessiveSalesDeliveryRate & " " &
     "  FROM " & tblSaleItemProperty & "   where " & wStandard & " AND " & ItemNo & " =@pItemNo "
    Public Shared Function GetSiteSaleProperty_By_ItemNo(sItemNo As String) As DataTable
        Dim strSQL As String = SqlSiteSaleProperty_ItemNo
        strSQL = strSQL.Replace("@pItemNo", "'" & sItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAF", "GetSiteSaleProperty_By_ItemNo", "Sql = SqlSiteSaleProperty_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSiteSaleProperty_By_ItemNo_DataSet(sItemNo As String) As DataSet
        Dim strSQL As String = SqlSiteSaleProperty_ItemNo
        strSQL = strSQL.Replace("@pItemNo", "'" & sItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAF", "GetSiteSaleProperty_By_ItemNo_DataSet", "Sql = SqlSiteSaleProperty_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '--Page CheckBOMPopUp
    '--Select FixedLeadTiem where ItemNo  / Refresh DataTable
    Private Shared SelectSupply As String = "select " & SupplyStrategy & " from " & tblSaleItemProperty & "" &
        " left join " & IMAE.tblProductItem & " on " & IMAE.ItemNo & " = " & ItemNo & "" &
        " where " & wStandard & " and  " & IMAE.wStandard & " and  " & ItemNo & " ='@ItemNo'"

    Private Shared SelectFixedLeadTiem As String = "select @LeadTime as LeadTime from " & tblSaleItemProperty & "" &
        " left join " & IMAE.tblProductItem & " on " & IMAE.ItemNo & " = " & ItemNo & "" &
        " where " &
        " " & wStandard & " and  " & IMAE.wStandard & " and  " & ItemNo & " ='@ItemNo'"
    Public Shared Function FixedLeadTiem(ByVal ItemNo As String)
        Dim Oral As String = SelectSupply
        Dim dt As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, dt)
        Dim Supply As String = ""
        If dt.Rows.Count > 0 Then
            Supply = dt.Rows(0).Item("imaf013")
        End If

        Dim Ora As String = SelectFixedLeadTiem
        Dim tempDataTable As New DataTable
        Dim LeadTime As String = ""
        If Supply = 1 Then
            LeadTime = "" & IMAE.FixedManufacturingLeadTime & ""
        ElseIf Supply = 2 Then
            LeadTime = "" & PurchaseFileLeadTime & ""
        Else
            LeadTime = "" & IMAE.FixedManufacturingLeadTime & ""
        End If
        Ora = Ora.Replace("@ItemNo", ItemNo)
        Ora = Ora.Replace("@LeadTime", LeadTime)
        GetData.Get_DataReaderOracle(Ora, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function
End Class
