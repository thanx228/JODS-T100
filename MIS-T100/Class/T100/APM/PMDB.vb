Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDB
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdb_t
    '''# apmt500 :Purchaes PR Request : Body Item :  Purchase  Request Detail
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PO : Body Item : Purchase  Request Detail ##############</reamrks>
    Public Shared tblPRrequestBody As String = "pmdb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdbent"
    Public Shared Site As String = "pmdbsite"
    Public Shared PRrquestNo As String = "pmdbdocno"
    Public Shared ItemSequence As String = "pmdbseq"
    Public Shared SourceDocNo As String = "pmdb001"
    Public Shared SourceItemSeq2 As String = "pmdb002"
    Public Shared SourceItemSeq3 As String = "pmdb003"
    Public Shared ItemNo As String = "pmdb004"
    Public Shared ProductFeature As String = "pmdb005"
    Public Shared RequestQty As String = "pmdb006"
    Public Shared Unit As String = "pmdb007"
    Public Shared ReferenceQty As String = "pmdb008"
    Public Shared ReferenceUnit As String = "pmdb009"
    Public Shared PricingQty As String = "pmdb010"
    Public Shared PricingUnit As String = "pmdb011"
    Public Shared Packagingcontainer As String = "pmdb012"
    Public Shared Supplierselection As String = "pmdb014"
    Public Shared VendorCode As String = "pmdb015"
    Public Shared PaymentTerm As String = "pmdb016"
    Public Shared TradeTerm As String = "pmdb017"
    Public Shared TaxRate As String = "pmdb018"
    Public Shared ReferenceUP As String = "pmdb019"
    Public Shared UntaxedReferenceAmount As String = "pmdb020"
    Public Shared ReferenceAmtInclTax As String = "pmdb021"
    Public Shared RequestDate As String = "pmdb030"
    Public Shared ReasonCode As String = "pmdb031"
    Public Shared RowStatus As String = "pmdb032"
    Public Shared Urgency As String = "pmdb033"
    Public Shared ProjectNo As String = "pmdb034"
    Public Shared WBS As String = "pmdb035"
    Public Shared ActivityNo As String = "pmdb036"
    Public Shared ReceiptLocation37 As String = "pmdb037"
    Public Shared ReceiptWarehouseLocation As String = "pmdb038"
    Public Shared ReceiptLocation39 As String = "pmdb039"
    Public Shared NoUse As String = "pmdb040"
    Public Shared AllowPartialDelivery As String = "pmdb041"
    Public Shared AllowDeliveryInAdvance As String = "pmdb042"
    Public Shared Bond As String = "pmdb043"
    Public Shared IncludeToAPS As String = "pmdb044"
    Public Shared DeliveryFrozen As String = "pmdb045"
    Public Shared ExpenseDept As String = "pmdb046"
    Public Shared ReceiptTime As String = "pmdb048"
    Public Shared TransferredToPO As String = "pmdb049"
    Public Shared Notes As String = "pmdb050"
    Public Shared SettlementRetentionReasonCode As String = "pmdb051"
    Public Shared GoodsBarcode As String = "pmdb200"
    Public Shared PackingUnit As String = "pmdb201"
    Public Shared Pieces As String = "pmdb202"
    Public Shared DistributionCenter As String = "pmdb203"
    Public Shared DistributionStore As String = "pmdb204"
    Public Shared PurchaseCenter As String = "pmdb205"
    Public Shared Purchaser As String = "pmdb206"
    Public Shared PurchaseMethod As String = "pmdb207"
    Public Shared OperatingMethod As String = "pmdb208"
    Public Shared SettlementMethod As String = "pmdb209"
    Public Shared PromotionsStartDate As String = "pmdb210"
    Public Shared PromotionEndDate As String = "pmdb211"
    Public Shared RequisitionItemQuantity As String = "pmdb212"
    Public Shared ReasonableInventories As String = "pmdb250"
    Public Shared MaxInventories As String = "pmdb251"
    Public Shared InventoryOnHand As String = "pmdb252"
    Public Shared StorageTransitReceipts As String = "pmdb253"
    Public Shared SalesVolume1 As String = "pmdb254"
    Public Shared SalesVolume2 As String = "pmdb255"
    Public Shared SalesVolume3 As String = "pmdb256"
    Public Shared SalesInPreviousFourWeeks As String = "pmdb257"
    Public Shared RequisitionTransitReceipts As String = "pmdb258"
    Public Shared WeeklyAverageSalesVolume As String = "pmdb259"
    Public Shared RetainFieldStart As String = "pmdb900"
    Public Shared RetainFieldEnd As String = "pmdb999"
    Public Shared ReceiptDepartment As String = "pmdb260"
    Public Shared SourceBatchSequence As String = "pmdb052"
    Public Shared ReplenishmentSpecificationsDesc As String = "pmdb227"
    Public Shared BudgetDetails As String = "pmdb053"
    Public Shared ReferencePurchasingPrice As String = "pmdb213"
    Public Shared InventoryManagmentFeature As String = "pmdb054"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"


    '####### where  PO_No = ?  Tab: Purchase Detail (for apmt500 :Purchaes PO : Body Item) #########################
    Private Shared strWH_PR_Request_No As String = "Select " & PRrquestNo & " ," & ItemSequence & "," & SourceDocNo & ", " & SourceItemSeq2 & "," &
        " " & SourceItemSeq3 & "," & ItemNo & ", " & ProductFeature & " ," & RequestQty & "," & Unit & "," & ReferenceQty & "," & ReferenceUnit & "," &
        " " & PricingQty & ", " & PricingUnit & "," & Packagingcontainer & "," & Supplierselection & "," & VendorCode & "," & PaymentTerm & ", " &
        " " & TradeTerm & "," & TaxRate & "," & ReferenceUP & "," & UntaxedReferenceAmount & "," & ReferenceAmtInclTax & "," & RequestDate & ", " &
        " " & ReasonCode & "," & RowStatus & "," & Urgency & "," & ProjectNo & "," & WBS & "," & ActivityNo & "," & ReceiptLocation37 & ", " &
        " " & ReceiptWarehouseLocation & "," & ReceiptLocation39 & "," & NoUse & "," & AllowPartialDelivery & "," & AllowDeliveryInAdvance & ", " &
        " " & Bond & "," & IncludeToAPS & "," & DeliveryFrozen & "," & ExpenseDept & "," & ReceiptTime & "," & TransferredToPO & "," & Notes & ", " &
        " " & SettlementRetentionReasonCode & "," & GoodsBarcode & "," & PackingUnit & "," & Pieces & "," & DistributionCenter & ", " &
        " " & DistributionStore & "," & PurchaseCenter & "," & Purchaser & "," & PurchaseMethod & "," & OperatingMethod & "," & SettlementMethod & ", " &
        " " & PromotionsStartDate & "," & PromotionEndDate & "," & RequisitionItemQuantity & "," & ReasonableInventories & "," & MaxInventories & ", " &
        " " & InventoryOnHand & "," & StorageTransitReceipts & "," & SalesVolume1 & "," & SalesVolume2 & "," & SalesVolume3 & ", " &
        " " & SalesInPreviousFourWeeks & "," & RequisitionTransitReceipts & "," & WeeklyAverageSalesVolume & "," & RetainFieldStart & "," & RetainFieldEnd & ", " &
        " " & ReceiptDepartment & "," & SourceBatchSequence & "," & ReplenishmentSpecificationsDesc & "," & BudgetDetails & "," & ReferencePurchasingPrice & ", " &
        " " & InventoryManagmentFeature & " " &
        " FROM " & PMDB.tblPRrequestBody & "  " &
        " where " & wStandard & " And  " & PRrquestNo & " = @pPR_Reqeust_No  "
    Public Shared Function GetPR_Request_No_Body(StrPR_Reqeust_No As String) As DataTable
        Dim Sql As String = strWH_PR_Request_No
        Sql = Sql.Replace("@pPR_Reqeust_No", "'" & StrPR_Reqeust_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDB", "GetPR_Request_No_Body", "Sql = strWH_PR_Request_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Request_No_BodyDataSet(StrPR_Reqeust_No As String) As DataSet
        Dim Sql As String = strWH_PR_Request_No
        Sql = Sql.Replace("@pPR_Reqeust_No", "'" & StrPR_Reqeust_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDB", "GetPR_Request_No_BodyDataSet", "Sql = strWH_PR_Request_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  ItemNo = ?  Tab: Purchase Detail (for apmt500 :Purchaes PO : Body Item) #########################
    Private Shared strWH_PR_Request_By_ItemNo As String = "Select " & PRrquestNo & " ," & ItemSequence & "," & SourceDocNo & ", " & SourceItemSeq2 & "," &
        " " & SourceItemSeq3 & "," & ItemNo & ", " & ProductFeature & " ," & RequestQty & "," & Unit & "," & ReferenceQty & "," & ReferenceUnit & "," &
        " " & PricingQty & ", " & PricingUnit & "," & Packagingcontainer & "," & Supplierselection & "," & VendorCode & "," & PaymentTerm & ", " &
        " " & TradeTerm & "," & TaxRate & "," & ReferenceUP & "," & UntaxedReferenceAmount & "," & ReferenceAmtInclTax & "," & RequestDate & ", " &
        " " & ReasonCode & "," & RowStatus & "," & Urgency & "," & ProjectNo & "," & WBS & "," & ActivityNo & "," & ReceiptLocation37 & ", " &
        " " & ReceiptWarehouseLocation & "," & ReceiptLocation39 & "," & NoUse & "," & AllowPartialDelivery & "," & AllowDeliveryInAdvance & ", " &
        " " & Bond & "," & IncludeToAPS & "," & DeliveryFrozen & "," & ExpenseDept & "," & ReceiptTime & "," & TransferredToPO & "," & Notes & ", " &
        " " & SettlementRetentionReasonCode & "," & GoodsBarcode & "," & PackingUnit & "," & Pieces & "," & DistributionCenter & ", " &
        " " & DistributionStore & "," & PurchaseCenter & "," & Purchaser & "," & PurchaseMethod & "," & OperatingMethod & "," & SettlementMethod & ", " &
        " " & PromotionsStartDate & "," & PromotionEndDate & "," & RequisitionItemQuantity & "," & ReasonableInventories & "," & MaxInventories & ", " &
        " " & InventoryOnHand & "," & StorageTransitReceipts & "," & SalesVolume1 & "," & SalesVolume2 & "," & SalesVolume3 & ", " &
        " " & SalesInPreviousFourWeeks & "," & RequisitionTransitReceipts & "," & WeeklyAverageSalesVolume & "," & RetainFieldStart & "," & RetainFieldEnd & ", " &
        " " & ReceiptDepartment & "," & SourceBatchSequence & "," & ReplenishmentSpecificationsDesc & "," & BudgetDetails & "," & ReferencePurchasingPrice & ", " &
        " " & InventoryManagmentFeature & " " &
        " FROM " & PMDB.tblPRrequestBody & "  " &
        " where " & wStandard & " And  " & ItemNo & " = @PR_ByTemNo  "
    Public Shared Function GetPR_Request_ByItemNo_Body(StrPR_ReqeustByitemNo As String) As DataTable
        Dim Sql As String = strWH_PR_Request_By_ItemNo
        Sql = Sql.Replace("@PR_ByTemNo", "'" & StrPR_ReqeustByitemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDB", "GetPR_Request_ByItemNo_Body", "Sql = strWH_PR_Request_By_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Request_ByItemNo_BodyDataSet(StrPR_ReqeustByitemNo As String) As DataSet
        Dim Sql As String = strWH_PR_Request_By_ItemNo
        Sql = Sql.Replace("@PR_ByTemNo", "'" & StrPR_ReqeustByitemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDB", "GetPR_Request_ByItemNo_BodyDataSet", "Sql = strWH_PR_Request_By_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
