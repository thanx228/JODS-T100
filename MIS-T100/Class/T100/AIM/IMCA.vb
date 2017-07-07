Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class IMCA
    '# Module : AIM
    Private Shared AIM As String = "AIM"
    '# aimm200 : Main items grouping file : Item Property 
    '''<reamrks>### Structure Table ##############</reamrks>
    Public Shared tblMainItemsGrouping As String = "imca_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> # Basic Date </reamrks>
    Public Shared ent As String = "imcaent"
    Public Shared Status As String = "imcastus"
    Public Shared MainGroupCode As String = "imca001"
    Public Shared LowLevelCode As String = "imca003"
    Public Shared ItemCategory As String = "imca004"
    Public Shared CharacteristicCategory As String = "imca005"
    Public Shared BasicUnit As String = "imca006"
    Public Shared LifecycleStatus As String = "imca010"
    Public Shared OutputType As String = "imca011"
    Public Shared AllowByProductGeneration As String = "imca012"
    Public Shared PageNoInCatalogue As String = "imca013"
    Public Shared ProductSerialNo As String = "imca014"
    Public Shared WeightUnit As String = "imca018"
    Public Shared LengthUnit As String = "imca022"
    Public Shared AreaUnit As String = "imca024"
    Public Shared VolumeUnit As String = "imca026"
    Public Shared PackagingContainer As String = "imca027"
    Public Shared CapacityUnit As String = "imca029"
    Public Shared ExcessVolumeTolerance As String = "imca030"
    Public Shared LoadUnit As String = "imca032"
    Public Shared OverweightTolerance As String = "imca033"
    Public Shared LogAssemblyLocation As String = "imca036"
    Public Shared AssemblyLocationMustBeArticulated As String = "imca037"
    Public Shared EngineeringItem As String = "imca038"
    Public Shared EngineeringGraphNo As String = "imca041"
    Public Shared MainModelNo As String = "imca042"
    Public Shared LocationAdjustableResearch As String = "imca043"
    Public Shared AVLcontrolPoint As String = "imca044"
    Public Shared ManufacturingCountry As String = "imca045"
    Public Shared OriginClassification As String = "imca122"
    Public Shared OriginDescriptions As String = "imca123"
    Public Shared PurchaseSaleTaxableSubject As String = "imca124"
    Public Shared Brand As String = "imca126"
    Public Shared Series As String = "imca127"
    Public Shared Model As String = "imca128"
    Public Shared Functions As String = "imca129"
    Public Shared Stuff As String = "imca130"
    Public Shared PriceBand As String = "imca131"
    Public Shared OtherAttr1 As String = "imca132"
    Public Shared OtherAttr2 As String = "imca133"
    Public Shared OtherAttr3 As String = "imca134"
    Public Shared OtherAttr4 As String = "imca135"
    Public Shared OtherAttr5 As String = "imca136"
    Public Shared OtherAttr6 As String = "imca137"
    Public Shared OtherAttr7 As String = "imca138"
    Public Shared OtherAttr8 As String = "imca139"
    Public Shared OtherAttr9 As String = "imca140"
    Public Shared OtherAttr10 As String = "imca141"
    Public Shared InventoryGrouping As String = "imca201"
    Public Shared SalesGrouping As String = "imca202"
    Public Shared PurchaseGrouping As String = "ximca203x"
    Public Shared ManufacturingManagementGrouping As String = "imca204"
    Public Shared QualityControlGrouping As String = "imca205"
    Public Shared FinancialGrouping As String = "imca206"
    Public Shared WMSgroup As String = "imca207"
    Public Shared CustomsGrouping As String = "imca208"

    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "imcaownid"
    Public Shared DepartmentOfData As String = "imcaowndp"
    Public Shared DataCreatedBy As String = "imcacrtid"
    Public Shared DataCreatedDate As String = "imcacrtdt"
    Public Shared DateCreatedByDept As String = "imcacrtdp"
    Public Shared ModifiedBy As String = "imcamodid"
    Public Shared LastModifiedDate As String = "imcamoddt"
    '''<reamrks> Condition Field </reamrks>

    Public Shared wStandard As String = ent & "='3' and " & DepartmentOfData & "='JINPAO' "

    '####################  Where By..  Item_No. ='?' ############################################
    'Private Shared StrProducItemDeatil As String = "Select " & ItemNo & "," & CurrentVersion & "," & PrimaryGroupCode & "," & ItemCategory & ", " &
    '" " & CharacteristicCategory & "," & BasicUnit & "," & LifecycleStatus & "," & ProductClassification & "," & OutputType & "," & AllowByProducts & ", " &
    '" " & DirectoryNo & "," & ProductBarcode & "," & GrossWeight & "," & NetWeight & "," & WeightUnit & "," & Length & ", " &
    '" " & Width & "," & Height & "," & LengthUnit & "," & SquareMeasure & "," & AreaUnit & "," & Volume & "," & VolumeUnit & ", " &
    '" " & PackagingContainer & "," & Capacity & "," & CapacityUnit & "," & ExcessVolumeTolerance & "," & WeightLoad & "," & LoadUnit & ", " &
    '" " & OverweightTolerance & "," & ItemSource & "," & SourceReferenceItemNo & "," & LogLocationInsert & "," & AssemblyLocationMustBeArticulated & "," & EngineeringItem & ", " &
    '" " & ConvertToFormalItemNo & "," & TimeConverted & "," & EngineeringGraphNo & "," & MainMoldNo & "," & ComponentsAdjustableByRDLocations & "," & AVLcontrolPoint & ", " &
    '" " & ProductionNationRegion & "," & BarcodeClassification & "," & PrimaryVendor & "," & ShelfLifeMmonth & "," & ShelfLifeDay & "," & InventoryUnit & ", " &
    '" " & SalesUnit & "," & SalesDenominated & "," & PurchasingDept & "," & TypesOfGoods & "," & BarcodeType & "," & SeasonalProduct & "," & StartDate & "," & EndDate & ", " &
    '" " & BiographyScaleFactor & "," & PricingCurrency & "," & EstimatedPurchasePrice & "," & EstimatedSalesPrice & "," & PurchaseSalesDifferenceRate & "," & TestSalesPeriodDays & ", " &
    '" " & TestSalesAmount & "," & TestSalesQuantity & "," & OnlineBusiness & "," & OriginClassification & "," & OriginDescriptions & "," & PurchaseSalesTaxableItem & ", " &
    '" " & OneTimeProduct & "," & Brand & "," & Series & "," & Model & "," & Functions & "," & PrimaryMaterial & "," & PriceRange & "," & OtherAttribute1 & "," & OtherAttribute2 & ", " &
    '" " & OtherAttribute3 & "," & OtherAttribute4 & "," & OtherAttribute5 & "," & OtherAttribute6 & "," & OtherAttribute7 & "," & OtherAttribute8 & "," & OtherAttribute9 & ", " &
    '" " & OtherAttribute10 & "," & CreatedByOrganization & "," & ProductGroupNumber & "," & InventoryMultipleUnits & "," & PurchasePricingUnit & "," & CostUnit & ", " &
    '" " & Status & "," & PresetProductAdventRatio & "," & NumberOfProductAdventDays & "," & AdventControlMethod & "," & Level & "," & Color & "," & ModelNo & "," & Year & " " &
    '" " & OrderingQuarter & "," & Gender & "," & LabelPrice & "," & ListedDate & "," & WeightPermg & "," & FabricWidth & "," & DataOwner & "," & DataOwnerDept & ", " &
    '" " & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmedBy & "," & DataConfirmedDate & "  " &
    '" FROM " & tblProductItemDeatil & " where " & ItemNo & " =@ProductItem and " & ent & "='3' "
    'Public Shared Function GetDataProducItem(ItemRows As String) As DataTable
    '    Dim strSQL As String = StrProducItemDeatil
    '    strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim dt As New DataTable
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSQL, objConn)
    '        dtAdapter.Fill(dt)
    '        Return dt '*** Return DataTable ***
    '    Catch ex As Exception
    '        GetPageError.GetClassT100(AIM, "IMAA", "GetDataProducItem", "strSQL = StrProducItemDeatil", ex.Message)
    '        Return Nothing
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function
    'Public Shared Function GetDataProducItemDataSet(ItemRows As String) As DataSet
    '    Dim strSQL As String = StrProducItemDeatil
    '    strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim ds As New DataSet
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSQL, objConn)
    '        dtAdapter.Fill(ds)
    '        Return ds '*** Return DataSet ***
    '    Catch ex As Exception
    '        GetPageError.GetClassT100(AIM, "IMAA", "GetDataProducItemDataSet", "strSQL = StrProducItemDeatil", ex.Message)
    '        Return Nothing
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function
End Class
