Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDO
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdo_t
    '''# apmt500 :Purchaes PO : Body Item : Tab Delivery Detail
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PO : Body Item : Tab Delivery Detail ##############</reamrks>
    Public Shared tblPObodyDelivery As String = "pmdo_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdoent"
    Public Shared Site As String = "pmdosite"
    Public Shared PurchaseNo As String = "pmdodocno"
    Public Shared LineNo As String = "pmdoseq"
    Public Shared ItemOrder As String = "pmdoseq1"
    Public Shared BatchOrder As String = "pmdoseq2"
    Public Shared ItemNo As String = "pmdo001"
    Public Shared ProductCharacteristics As String = "pmdo002"
    Public Shared SubPartFeatures As String = "pmdo003"
    Public Shared PurchasingDept As String = "pmdo004"
    Public Shared OverallPurchaseQuantity As String = "pmdo005"
    Public Shared BatchPurchaseQuantity As String = "pmdo006"
    Public Shared ConvertMasterItemQuantity As String = "pmdo007"
    Public Shared QPA As String = "pmdo008"
    Public Shared DeliveryDateType As String = "pmdo009"
    Public Shared ReceiptTime As String = "pmdo010"
    Public Shared ShippingDate As String = "pmdo011"
    Public Shared ArrivalDate As String = "pmdo012"
    Public Shared StockInDate As String = "pmdo013"
    Public Shared MRPdeliveryDateFrozen As String = "pmdo014"
    Public Shared ReceivedVolume As String = "pmdo015"
    Public Shared SumReceivedVolume As String = "Sum_Received_volume"
    Public Shared RejectQty As String = "pmdo016"
    Public Shared StoreReturnAndExchangeVolume As String = "pmdo017"
    Public Shared StockInQty As String = "pmdo019"
    Public Shared SumStockInQty As String = "Sum_Stock_in_Qty"
    Public Shared VMIAPQty As String = "pmdo020"
    Public Shared DeliveryStatus As String = "pmdo021"
    Public Shared ReferencePrice As String = "pmdo022"
    Public Shared TaxCode As String = "pmdo023"
    Public Shared TaxRate As String = "pmdo024"
    Public Shared ElectronicProcurementNo As String = "pmdo025"
    Public Shared RecentEditor As String = "pmdo026"
    Public Shared RecentEditTime As String = "pmdo027"
    Public Shared BatchReferenceUnit As String = "pmdo028"
    Public Shared BatchReferenceQuantity As String = "pmdo029"
    Public Shared BatchValuationUnit As String = "pmdo030"
    Public Shared BatchValuationQuantity As String = "pmdo031"
    Public Shared BatchAmountExcludingTax As String = "pmdo032"
    Public Shared TaxedBatchAmount As String = "pmdo033"
    Public Shared BatchTax As String = "pmdo034"
    Public Shared InitialOperatingLocation As String = "pmdo035"
    Public Shared InitialSourceDocNo As String = "pmdo036"
    Public Shared InitialSourceItems As String = "pmdo037"
    Public Shared InitialItemOrder As String = "pmdo038"
    Public Shared InitialBatchOrder As String = "pmdo039"
    Public Shared SaleReturnQty As String = "pmdo040"
    Public Shared BatchPackagingUnit As String = "pmdo200"
    Public Shared BatchPackagingQuantity As String = "pmdo201"
    Public Shared RetainFieldStart As String = "pmdo900"
    Public Shared RetainFieldEnd As String = "pmdo999"
    Public Shared MaintenanceUnit As String = "pmdoud004"
    Public Shared RetrunQty As String = "pmdo041"
    Public Shared AlsoTheAmountOfReferenceQty As String = "pmdo042"
    Public Shared CounterofferQty As String = "pmdo043"
    Public Shared CounterofferReferenceQty As String = "pmdo044"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes Body Item : Tab Delivery Detail) #########################
    Private Shared strDeliveryWH_PO_No As String = "Select " & PurchaseNo & " ," & LineNo & "," & ItemOrder & ", " & BatchOrder & "," & ItemNo & "," & ProductCharacteristics & ", " &
        " " & SubPartFeatures & " ," & PurchasingDept & "," & OverallPurchaseQuantity & "," & BatchPurchaseQuantity & "," & ConvertMasterItemQuantity & "," & QPA & ", " &
        " " & DeliveryDateType & "," & ReceiptTime & "," & ShippingDate & "," & ArrivalDate & "," & StockInDate & ", " &
        " " & MRPdeliveryDateFrozen & "," & MRPdeliveryDateFrozen & "," & ReceivedVolume & "," & RejectQty & "," & StoreReturnAndExchangeVolume & "," & StockInQty & ", " &
        " " & VMIAPQty & "," & DeliveryStatus & "," & ReferencePrice & "," & TaxCode & "," & TaxRate & "," & ElectronicProcurementNo & " " &
        " " & RecentEditor & "," & RecentEditTime & "," & BatchReferenceUnit & "," & BatchReferenceQuantity & "," & BatchValuationUnit & ", " &
        " " & BatchValuationQuantity & "," & BatchAmountExcludingTax & "," & TaxedBatchAmount & "," & BatchTax & "," & InitialOperatingLocation & ", " &
        " " & InitialSourceDocNo & "," & InitialSourceItems & "," & InitialItemOrder & "," & InitialBatchOrder & "," & SaleReturnQty & ", " &
        " " & BatchPackagingUnit & "," & BatchPackagingQuantity & "," & RetainFieldStart & "," & RetainFieldEnd & "," & MaintenanceUnit & ", " &
        " " & RetrunQty & "," & AlsoTheAmountOfReferenceQty & "," & CounterofferQty & "," & CounterofferReferenceQty & "  " &
        " FROM " & PMDO.tblPObodyDelivery & "  " &
        " where " & wStandard & " And  " & PurchaseNo & " = @pPO_No  "
    Public Shared Function GetPO_BodyDelivery(StrPO_No As String) As DataTable
        Dim Sql As String = strDeliveryWH_PO_No
        Sql = Sql.Replace("@pPO_No", "'" & StrPO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_BodyDelivery", "Sql = strDeliveryWH_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPO_BodyDeliveryDataSet(StrPO_No As String) As DataSet
        Dim Sql As String = strDeliveryWH_PO_No
        Sql = Sql.Replace("@pPO_No", "'" & StrPO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_BodyDeliveryDataSet", "Sql = strDeliveryWH_PO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes Body Item : Tab Delivery Detail) #########################
    Private Shared strDeliveryWH_Item_No As String = "Select " & PurchaseNo & " ," & LineNo & "," & ItemOrder & ", " & BatchOrder & "," & ItemNo & "," & ProductCharacteristics & ", " &
        " " & SubPartFeatures & " ," & PurchasingDept & "," & OverallPurchaseQuantity & "," & BatchPurchaseQuantity & "," & ConvertMasterItemQuantity & "," & QPA & ", " &
        " " & DeliveryDateType & "," & ReceiptTime & "," & ShippingDate & "," & ArrivalDate & "," & StockInDate & ", " &
        " " & MRPdeliveryDateFrozen & "," & MRPdeliveryDateFrozen & "," & ReceivedVolume & "," & RejectQty & "," & StoreReturnAndExchangeVolume & "," & StockInQty & ", " &
        " " & VMIAPQty & "," & DeliveryStatus & "," & ReferencePrice & "," & TaxCode & "," & TaxRate & "," & ElectronicProcurementNo & " " &
        " " & RecentEditor & "," & RecentEditTime & "," & BatchReferenceUnit & "," & BatchReferenceQuantity & "," & BatchValuationUnit & ", " &
        " " & BatchValuationQuantity & "," & BatchAmountExcludingTax & "," & TaxedBatchAmount & "," & BatchTax & "," & InitialOperatingLocation & ", " &
        " " & InitialSourceDocNo & "," & InitialSourceItems & "," & InitialItemOrder & "," & InitialBatchOrder & "," & SaleReturnQty & ", " &
        " " & BatchPackagingUnit & "," & BatchPackagingQuantity & "," & RetainFieldStart & "," & RetainFieldEnd & "," & MaintenanceUnit & ", " &
        " " & RetrunQty & "," & AlsoTheAmountOfReferenceQty & "," & CounterofferQty & "," & CounterofferReferenceQty & "  " &
        " FROM " & PMDO.tblPObodyDelivery & "  " &
    " where " & wStandard & " And  " & ItemNo & " = @pDeliveryItem_No  "
    Public Shared Function GetPO_Body_ByItemNo_Delivery(DeliveryItem_No As String) As DataTable
        Dim Sql As String = strDeliveryWH_Item_No
        Sql = Sql.Replace("@pDeliveryItem_No", "'" & DeliveryItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_Body_ByItemNo_Delivery", "Sql = strDeliveryWH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPO_Body_By_ItemNo_DeliveryDataSet(DeliveryItem_No As String) As DataSet
        Dim Sql As String = strDeliveryWH_Item_No
        Sql = Sql.Replace("@pDeliveryItem_No", "'" & DeliveryItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_Body_By_ItemNo_DeliveryDataSet", "Sql = strDeliveryWH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  PO_No = ?  and ItemNo=? Tab: Purchase Detail (for apmt500 :Purchaes Body Item : Tab Delivery Detail) #########################
    Private Shared strDeliveryWH_PO_Item_No As String = "Select " & PurchaseNo & " ," & LineNo & "," & ItemOrder & ", " & BatchOrder & "," & ItemNo & "," & ProductCharacteristics & ", " &
        " " & SubPartFeatures & " ," & PurchasingDept & "," & OverallPurchaseQuantity & "," & BatchPurchaseQuantity & "," & ConvertMasterItemQuantity & "," & QPA & ", " &
        " " & DeliveryDateType & "," & ReceiptTime & "," & ShippingDate & "," & ArrivalDate & "," & StockInDate & ", " &
        " " & MRPdeliveryDateFrozen & "," & MRPdeliveryDateFrozen & "," & ReceivedVolume & "," & RejectQty & "," & StoreReturnAndExchangeVolume & "," & StockInQty & ", " &
        " " & VMIAPQty & "," & DeliveryStatus & "," & ReferencePrice & "," & TaxCode & "," & TaxRate & "," & ElectronicProcurementNo & " " &
        " " & RecentEditor & "," & RecentEditTime & "," & BatchReferenceUnit & "," & BatchReferenceQuantity & "," & BatchValuationUnit & ", " &
        " " & BatchValuationQuantity & "," & BatchAmountExcludingTax & "," & TaxedBatchAmount & "," & BatchTax & "," & InitialOperatingLocation & ", " &
        " " & InitialSourceDocNo & "," & InitialSourceItems & "," & InitialItemOrder & "," & InitialBatchOrder & "," & SaleReturnQty & ", " &
        " " & BatchPackagingUnit & "," & BatchPackagingQuantity & "," & RetainFieldStart & "," & RetainFieldEnd & "," & MaintenanceUnit & ", " &
        " " & RetrunQty & "," & AlsoTheAmountOfReferenceQty & "," & CounterofferQty & "," & CounterofferReferenceQty & "  " &
        " FROM " & PMDO.tblPObodyDelivery & "  " &
    " where " & wStandard & " And " & PurchaseNo & "=@pPO_No  And  " & ItemNo & " = @pDeliveryItem_No  "
    Public Shared Function GetPO_Body_ByPNO_ItemNo_Delivery(strPO_No As String, DeliveryItem_No As String) As DataTable
        Dim Sql As String = strDeliveryWH_PO_Item_No
        Sql = Sql.Replace("@pDeliveryItem_No", "'" & DeliveryItem_No & "'")
        Sql = Sql.Replace("@pPO_No", "'" & strPO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_Body_ByPNO_ItemNo_Delivery", "Sql = strDeliveryWH_PO_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### Sum(Stock-in-Qty), SUM(Received_volume) where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes Body Item : Tab Delivery Detail) #########################
    Private Shared strSum_DeliveryWH_Item_No As String = "Select SUM(" & StockInQty & ") as " & SumStockInQty & ", " &
    " SUM(" & ReceivedVolume & ") as " & SumReceivedVolume & " " &
    " FROM " & PMDO.tblPObodyDelivery & "  " &
    " where " & wStandard & " And  " & ItemNo & " = @pDeliveryItem_No  "
    Public Shared Function GetPO_Body_SumStockInQty_ByItemNo_Delivery(strDeliveryItem_No As String) As DataTable
        Dim Sql As String = strSum_DeliveryWH_Item_No
        Sql = Sql.Replace("@pDeliveryItem_No", "'" & strDeliveryItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDO", "GetPO_Body_SumStockInQty_ByItemNo_Delivery", "Sql = strSum_DeliveryWH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
