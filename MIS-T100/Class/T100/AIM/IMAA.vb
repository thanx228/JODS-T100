Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class IMAA
    '# Module : AIM
    Private Shared AIM As String = "AIM"
    '# aimm200 : Produt Item Property 
    '# Table : imaa_t     (Relationship to table imae_t)
    '# aimm215 : Produt Item Property : Header Detail
    '''<reamrks>### Structure Table : Product Item Property Detail ##############</reamrks>
    Public Shared tblProductItemDeatil As String = "imaa_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> # Basic Date </reamrks>
    Public Shared ent As String = "imaaent"
    Public Shared ItemNo As String = "imaa001"
    Public Shared CurrentVersion As String = "imaa002"
    Public Shared PrimaryGroupCode As String = "imaa003"
    Public Shared ItemCategory As String = "imaa004"
    Public Shared CharacteristicCategory As String = "imaa005"
    Public Shared BasicUnit As String = "imaa006"
    Public Shared LifecycleStatus As String = "imaa010"
    Public Shared ProductClassification As String = "imaa009"
    Public Shared OutputType As String = "imaa011"
    Public Shared AllowByProducts As String = "imaa012"
    Public Shared DirectoryNo As String = "imaa013"
    Public Shared ProductBarcode As String = "imaa014"
    Public Shared GrossWeight As String = "imaa016"
    Public Shared NetWeight As String = "imaa017"
    Public Shared WeightUnit As String = "imaa018"
    Public Shared Length As String = "imaa019"
    Public Shared Width As String = "imaa001"
    Public Shared Height As String = "imaa021"
    Public Shared LengthUnit As String = "imaa022"
    Public Shared SquareMeasure As String = "imaa023"
    Public Shared AreaUnit As String = "imaa024"
    Public Shared Volume As String = "imaa025"
    Public Shared VolumeUnit As String = "imaa026"
    Public Shared PackagingContainer As String = "imaa027"
    Public Shared Capacity As String = "imaa028"
    Public Shared CapacityUnit As String = "imaa029"
    Public Shared ExcessVolumeTolerance As String = "imaa030"
    Public Shared WeightLoad As String = "imaa031"
    Public Shared LoadUnit As String = "imaa032"
    Public Shared OverweightTolerance As String = "imaa033"
    Public Shared ItemSource As String = "imaa034"
    Public Shared SourceReferenceItemNo As String = "imaa035"
    Public Shared LogLocationInsert As String = "imaa036"
    Public Shared AssemblyLocationMustBeArticulated As String = "imaa037"
    Public Shared EngineeringItem As String = "imaa038"
    Public Shared ConvertToFormalItemNo As String = "imaa039"
    Public Shared TimeConverted As String = "imaa040"
    Public Shared EngineeringGraphNo As String = "imaa041"
    Public Shared MainMoldNo As String = "imaa042"
    Public Shared ComponentsAdjustableByRDLocations As String = "imaa043"
    Public Shared AVLcontrolPoint As String = "imaa044"
    Public Shared ProductionNationRegion As String = "imaa045"
    Public Shared BarcodeClassification As String = "imaa100"
    Public Shared PrimaryVendor As String = "imaa101"
    Public Shared ShelfLifeMmonth As String = "imaa102"
    Public Shared ShelfLifeDay As String = "imaa103"
    Public Shared InventoryUnit As String = "imaa104"
    Public Shared SalesUnit As String = "imaa105"
    Public Shared SalesDenominated As String = "imaa106"
    Public Shared PurchasingDept As String = "imaa107"
    Public Shared TypesOfGoods As String = "imaa108"
    Public Shared BarcodeType As String = "imaa109"
    Public Shared SeasonalProduct As String = "imaa110"
    Public Shared StartDate As String = "imaa111"
    Public Shared EndDate As String = "imaa112"
    Public Shared BiographyScaleFactor As String = "imaa113"
    Public Shared PricingCurrency As String = "imaa114"
    Public Shared EstimatedPurchasePrice As String = "imaa115"
    Public Shared EstimatedSalesPrice As String = "imaa116"
    Public Shared PurchaseSalesDifferenceRate As String = "imaa117"
    Public Shared TestSalesPeriodDays As String = "imaa118"
    Public Shared TestSalesAmount As String = "imaa119"
    Public Shared TestSalesQuantity As String = "imaa120"
    Public Shared OnlineBusiness As String = "imaa121"
    Public Shared OriginClassification As String = "imaa122"
    Public Shared OriginDescriptions As String = "imaa123"
    Public Shared PurchaseSalesTaxableItem As String = "imaa124"
    Public Shared OneTimeProduct As String = "imaa125"
    Public Shared Brand As String = "imaa126"
    Public Shared Series As String = "imaa127"
    Public Shared Model As String = "imaa128"
    Public Shared Functions As String = "imaa129"
    Public Shared PrimaryMaterial As String = "imaa130"
    Public Shared PriceRange As String = "imaa131"
    Public Shared OtherAttribute1 As String = "imaa132"
    Public Shared OtherAttribute2 As String = "imaa133"
    Public Shared OtherAttribute3 As String = "imaa134"
    Public Shared OtherAttribute4 As String = "imaa135"
    Public Shared OtherAttribute5 As String = "imaa136"
    Public Shared OtherAttribute6 As String = "imaa137"
    Public Shared OtherAttribute7 As String = "imaa138"
    Public Shared OtherAttribute8 As String = "imaa139"
    Public Shared OtherAttribute9 As String = "imaa140"
    Public Shared OtherAttribute10 As String = "imaa141"
    Public Shared CreatedByOrganization As String = "imaa142"
    Public Shared ProductGroupNumber As String = "imaa143"
    Public Shared InventoryMultipleUnits As String = "imaa144"
    Public Shared PurchasePricingUnit As String = "imaa145"
    Public Shared CostUnit As String = "imaa146"
    Public Shared Status As String = "imaastus"
    Public Shared PresetProductAdventRatio As String = "imaa147"
    Public Shared NumberOfProductAdventDays As String = "imaa148"
    Public Shared AdventControlMethod As String = "imaa149"
    Public Shared Level As String = "imaa151"
    Public Shared Color As String = "imaa152"
    Public Shared ModelNo As String = "imaa153"
    Public Shared Year As String = "imaa154"
    Public Shared OrderingQuarter As String = "imaa155"
    Public Shared Gender As String = "imaa156"
    Public Shared LabelPrice As String = "imaa157"
    Public Shared ListedDate As String = "imaa158"
    Public Shared WeightPermg As String = "imaa159"
    Public Shared FabricWidth As String = "imaa160"

    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "imaaownid"
    Public Shared DataOwnerDept As String = "imaaowndp"
    Public Shared DataCreateBy As String = "imaacrtid"
    Public Shared DataCreateByDept As String = "imaacrtdp"
    Public Shared DataCreateDate As String = "imaacrtdt"
    Public Shared DataModifyBy As String = "imaamodid"
    Public Shared LastModifyDate As String = "imaamoddt"
    Public Shared DataConfirmedBy As String = "imaacnfid "
    Public Shared DataConfirmedDate As String = "imaacnfdt "

    '''<reamrks> Condition Field </reamrks>
    Public Shared wStandard As String = ent & "='3' and " & DataOwnerDept & "='JINPAO' "
    Public Shared Approved As String = Status & "='Y'  "

    '--Page CustomsNew
    '--Shearch Industry Item
    Private Shared SelectItemDetail As String = "Select " & ItemNo & "," & ProductClassification & "," & RTAXL.Description & ",nvl(" & GrossWeight & ",0.0) as Weight," & NetWeight & ",nvl(" & VolumeUnit & ",0.0) as Packigng,case " & IMAF.SupplyStrategy & " when '1' then 'Purchase' when '2' then 'Manufacture' end as Supply," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & BasicUnit & " FROM " & tblProductItemDeatil & "" &
        " left join " & IMAAL.tblProductionDetail & " on " & ItemNo & " = " & IMAAL.ProductItem & "" &
        " left join " & RTAXL.tblClassificationSecondary & " on " & ProductClassification & " = " & RTAXL.ItemCategoryNo & "" &
        " left join " & IMAF.tblSaleItemProperty & " on " & ItemNo & " = " & IMAF.ItemNo & "" &
        " where " & ent & "='3' and " & Approved & "and " & IMAAL.WStandard & "and " & IMAAL.enUS & "and " & RTAXL.LGStandard & "and " & IMAF.wStandard & "" &
        " and " & ProductClassification & " in ('" & RTAXL.Electronic & "','" & RTAXL.Aerospace & "','" & RTAXL.Automotive & "','" & RTAXL.Telecommunic & "','" & RTAXL.Medical & "','" & RTAXL.FoodIndustry & "','" & RTAXL.Energy & "','" & RTAXL.Transportati & "')" &
        " and " & ItemNo & "='@ItemNo' order by " & ItemNo & ""
    Public Shared Function ItemDetail(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectItemDetail
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function





    '####################  Where By..  Item_No. ='?' ############################################
    Private Shared StrProducItemDeatil As String = "Select " & ItemNo & "," & CurrentVersion & "," & PrimaryGroupCode & "," & ItemCategory & ", " &
    " " & CharacteristicCategory & "," & BasicUnit & "," & LifecycleStatus & "," & ProductClassification & "," & OutputType & "," & AllowByProducts & ", " &
    " " & DirectoryNo & "," & ProductBarcode & "," & GrossWeight & "," & NetWeight & "," & WeightUnit & "," & Length & ", " &
    " " & Width & "," & Height & "," & LengthUnit & "," & SquareMeasure & "," & AreaUnit & "," & Volume & "," & VolumeUnit & ", " &
    " " & PackagingContainer & "," & Capacity & "," & CapacityUnit & "," & ExcessVolumeTolerance & "," & WeightLoad & "," & LoadUnit & ", " &
    " " & OverweightTolerance & "," & ItemSource & "," & SourceReferenceItemNo & "," & LogLocationInsert & "," & AssemblyLocationMustBeArticulated & "," & EngineeringItem & ", " &
    " " & ConvertToFormalItemNo & "," & TimeConverted & "," & EngineeringGraphNo & "," & MainMoldNo & "," & ComponentsAdjustableByRDLocations & "," & AVLcontrolPoint & ", " &
    " " & ProductionNationRegion & "," & BarcodeClassification & "," & PrimaryVendor & "," & ShelfLifeMmonth & "," & ShelfLifeDay & "," & InventoryUnit & ", " &
    " " & SalesUnit & "," & SalesDenominated & "," & PurchasingDept & "," & TypesOfGoods & "," & BarcodeType & "," & SeasonalProduct & "," & StartDate & "," & EndDate & ", " &
    " " & BiographyScaleFactor & "," & PricingCurrency & "," & EstimatedPurchasePrice & "," & EstimatedSalesPrice & "," & PurchaseSalesDifferenceRate & "," & TestSalesPeriodDays & ", " &
    " " & TestSalesAmount & "," & TestSalesQuantity & "," & OnlineBusiness & "," & OriginClassification & "," & OriginDescriptions & "," & PurchaseSalesTaxableItem & ", " &
    " " & OneTimeProduct & "," & Brand & "," & Series & "," & Model & "," & Functions & "," & PrimaryMaterial & "," & PriceRange & "," & OtherAttribute1 & "," & OtherAttribute2 & ", " &
    " " & OtherAttribute3 & "," & OtherAttribute4 & "," & OtherAttribute5 & "," & OtherAttribute6 & "," & OtherAttribute7 & "," & OtherAttribute8 & "," & OtherAttribute9 & ", " &
    " " & OtherAttribute10 & "," & CreatedByOrganization & "," & ProductGroupNumber & "," & InventoryMultipleUnits & "," & PurchasePricingUnit & "," & CostUnit & ", " &
    " " & Status & "," & PresetProductAdventRatio & "," & NumberOfProductAdventDays & "," & AdventControlMethod & "," & Level & "," & Color & "," & ModelNo & "," & Year & " " &
    " " & OrderingQuarter & "," & Gender & "," & LabelPrice & "," & ListedDate & "," & WeightPermg & "," & FabricWidth & "," & DataOwner & "," & DataOwnerDept & ", " &
    " " & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmedBy & "," & DataConfirmedDate & "  " &
    " FROM " & tblProductItemDeatil & " where " & ItemNo & " =@ProductItem and " & ent & "='3' "
    Public Shared Function GetDataProducItem(ItemRows As String) As DataTable
        Dim strSQL As String = StrProducItemDeatil
        strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAA", "GetDataProducItem", "strSQL = StrProducItemDeatil", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataProducItemDataSet(ItemRows As String) As DataSet
        Dim strSQL As String = StrProducItemDeatil
        strSQL = strSQL.Replace("@ProductItem", "'" & ItemRows & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAA", "GetDataProducItemDataSet", "strSQL = StrProducItemDeatil", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    'add by noi date 2017-06-23
    Public Shared Function getItemInfo(fldName As ArrayList, WHR As String) As DataTable
        Dim conn_sql = New ConnSQL
        Dim VarIni As New VarIni
        Dim SQL As String = ""
        SQL &= VarIni.S & conn_sql.getFeild(fldName) & VarIni.F & tblProductItemDeatil
        SQL &= WHR & VarIni.getOrderBy(ItemNo)
        Return GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe())
    End Function
End Class
