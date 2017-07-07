Imports System.Data.OracleClient
Public Class XMDC

    Private Shared AXM As String = "AXM"
    '''<reamrks>##########Table SaleOrder Body############## </reamrks>
    Public Shared tblSaleItem As String = "xmdc_t"
    Public Shared SaleOrderNo As String = "xmdcdocno"
    Public Shared ent As String = "xmdcent"
    Public Shared Site As String = "xmdcsite"
    Public Shared ItemSequence As String = "xmdcseq"
    Public Shared Item As String = "xmdc001"
    Public Shared ProductFeature As String = "xmdc002"
    Public Shared PackagingContainer As String = "xmdc003"
    Public Shared OperationNo As String = "xmdc004"
    Public Shared Process As String = "xmdc005"
    Public Shared SalesUnit As String = "xmdc006"
    Public Shared SalesQty As String = "xmdc007"
    Public Shared ReferenceUnit As String = "xmdc008"
    Public Shared ReferenceQty As String = "xmdc009"
    Public Shared PricingUnit As String = "xmdc010"
    Public Shared PricingQty As String = "xmdc011"
    Public Shared BookShippingDate As String = "xmdc012"
    Public Shared ExpectedReceiptDate As String = "xmdc013"
    Public Shared UnitPrice As String = "xmdc015"
    Public Shared TaxType As String = "xmdc016"
    Public Shared TaxRate As String = "xmdc017"
    Public Shared SubItemFeature As String = "xmdc019" 'งานที่แถ่ม
    Public Shared UrgentItem As String = "xmdc020"
    Public Shared Bond As String = "xmdc021"
    Public Shared PartialDelivery As String = "xmdc022"
    Public Shared Shippinglocation As String = "xmdcunit"
    Public Shared CollectionLocation As String = "xmdcorga"
    Public Shared CustID As String = "xmdc023"
    Public Shared MultiDeliveryPeriod As String = "xmdc024"
    Public Shared ReceiptAddressNo As String = "xmdc025"
    Public Shared BillAddressNo As String = "xmdc026"
    Public Shared CustomerItemNo As String = "xmdc027"
    Public Shared RestrictedWarehouseLocation As String = "xmdc028"
    Public Shared RestrictedStorageLocation As String = "xmdc029"
    Public Shared RestrictedBatchNumber As String = "xmdc030"
    Public Shared MeansOfTransportation As String = "xmdc031"
    Public Shared PickingMethod As String = "xmdc032"
    Public Shared RateOfSpares As String = "xmdc033"
    Public Shared OverShippingRatio As String = "xmdc034"
    Public Shared PriceDetermination As String = "xmdc035"
    Public Shared ProjectNo As String = "xmdc036"
    Public Shared WBSNo As String = "xmdc037"
    Public Shared ActivityNo As String = "xmdc038"
    Public Shared CauseOfSpending As String = "xmdc039"
    Public Shared PricingSource As String = "xmdc040"
    Public Shared PriceReferenceNo As String = "xmdc041"
    Public Shared PriceReferenceItems As String = "xmdc042"
    Public Shared AccessPrices As String = "xmdc043"
    Public Shared PercentSpread As String = "xmdc044"
    Public Shared StatusCode As String = "xmdc045"
    Public Shared AmtExclTax As String = "xmdc046"
    Public Shared AmtInclTax As String = "xmdc047"
    Public Shared Tax As String = "xmdc048"
    Public Shared ReasonCode As String = "xmdc049"
    Public Shared Notes As String = "xmdc050"
    Public Shared CustomerOrderItemNo As String = "xmdc051"
    Public Shared Inspection As String = "xmdc052"
    Public Shared SettlementReasonCode As String = "xmdc053"
    Public Shared BOMEffectiveDate As String = "xmdc054"
    Public Shared SourceDocNo As String = "xmdc055"
    Public Shared SourceItemSeq As String = "xmdc056"
    Public Shared InventoryManagmentFeature As String = "xmdc057"
    Public Shared ReturnVolume As String = "xmdc058"
    Public Shared QuantityRepaymentReferenceQty As String = "xmdc059"
    Public Shared PriceRepaymentQty As String = "xmdc060"
    Public Shared PriceRepaymentReferenceQty As String = "xmdc061"
    Public Shared BOMFeatures As String = "xmdc062"
    Public Shared CashDiscountNo As String = "xmdc200"
    Public Shared CashDiscountItemSeq As String = "xmdc201"
    Public Shared CustPONumber As String = "xmdcud003" 'add by noi on 2017-07-05

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared RowsStus As String = "xmdc045" 'สถานะ Item
    Public Shared Whr As String = ""
    Public Shared Genaral As String = RowsStus & " ='1' " 'งานที่ขายปกติ 
    Public Shared SampleGenaral As String = SubItemFeature & " ='1' " 'งานที่ขายปกติ 
    Public Shared Sample As String = SubItemFeature & " ='9' " 'งานที่แถ่ม

    '--Page CheckBOMPopupSub
    '--SOSupBomPopUp
    Private Shared SelectSupBom As String = "select " & XMDA.CustomerId & "," & SaleOrderNo & "," & XMDA.VersionNo & ",substr(" & XMDA.DataConfirmDate & ",0,10) as Cnfdt," &
        " substr(" & BookShippingDate & ",0,10) as Deldt," & SalesQty & "," & XMDD.ShippedQty & ",sum(nvl(" & SalesQty & " - " & XMDD.ShippedQty & ",0)) as BalQty," & SalesUnit & " from " & tblSaleItem & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " On " & SaleOrderNo & " = " & XMDD.SaleOrderNo & " And " & ItemSequence & " = " & XMDD.LineNo & " And " & Item & " = " & XMDD.ItemNo & "" &
        " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
        " where " & wStandard & " And " & Genaral & " And " & SampleGenaral & " And" &
        " " & XMDD.wStandard & " And " & XMDD.SampleGenaral & " And" &
        " " & XMDA.wStandard & " And " & XMDA.Approved & " And" &
        " " & Item & "='@ItemNo'" &
        " group by " & XMDA.CustomerId & "," & SaleOrderNo & "," & XMDA.VersionNo & "," & XMDA.DataConfirmDate & "," & BookShippingDate & "," & SalesQty & "," & XMDD.ShippedQty & "," & SalesUnit & " order by " & BookShippingDate & ""
    Public Shared Function SOSupBomPopUp(ByVal ItemNo As String)
        Dim Oral As String = SelectSupBom
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function



    '--Page SaleUndeliveryStatus and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Sum ReqQty
    Private Shared SelectSumSOReqQty As String = "select " & Item & ",sum(" & SalesQty & ") as SOReqQty from " & tblSaleItem & "" &
    " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
    " where " & wStandard & " and " & Genaral & " and " & SampleGenaral & "" &
    " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
    "  and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function SumSOReqQty(ByVal ItemNo As String)
        Dim Oral As String = SelectSumSOReqQty
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SaleUndeliveryStatus and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Sum DeliveryQty where ItemNo
    Private Shared SelectDeliveryQty As String = "select " & Item & ",sum(" & XMDD.ShippedQty & ") as DeliveryQty  from " & tblSaleItem & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " On " & SaleOrderNo & " = " & XMDD.SaleOrderNo & " and " & ItemSequence & " = " & XMDD.LineNo & " and " & Item & " = " & XMDD.ItemNo & "" &
        " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
        " where " & wStandard & "and " & Genaral & "and " & SampleGenaral & "" &
        " and " & XMDD.wStandard & "and " & XMDD.SampleGenaral & "" &
        " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
        " and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function SumDeliQty(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectDeliveryQty
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SaleUndeliveryStatus and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Sum SampleReqQty
    Private Shared SelectSumSampleReqQty As String = "select " & Item & ",sum(" & SalesQty & ") as SampleReqQty from " & tblSaleItem & "" &
    " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
    " where " & wStandard & " and " & Genaral & " and " & Sample & "" &
    " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
    " and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function SumSampleReqQty(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectSumSampleReqQty
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SaleUndeliveryStatus and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Select SampleDeliveryQty Where SO Approved ItemNOClose ItemSample
    Private Shared SelectSampleDeliQty As String = "select " & Item & ",sum(" & XMDD.ShippedQty & ") as sampleDeliQty  from " & tblSaleItem & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " On " & SaleOrderNo & " = " & SaleOrderNo & " and " & ItemSequence & " = " & XMDD.LineNo & " and " & Item & " = " & XMDD.ItemNo & "" &
        " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
        " where " & wStandard & "and " & Genaral & "and " & Sample & "" &
        " and " & XMDD.wStandard & "and " & XMDD.Sample & "" &
        " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
        " and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function SumSampleDeliveryQty(ByVal SOReqQtyItemNo As String)
        Dim Oral As String = SelectSampleDeliQty
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", SOReqQtyItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Shearch All DeliveryQty form where
    Private Shared SelectDelQty As String = "select " & SaleOrderNo & ", " & XMDA.DocumentDate & ", " & Item & ", " & XMDD.ShippedQty & " from " & tblSaleItem & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " On " & SaleOrderNo & " = " & XMDD.SaleOrderNo & " and " & ItemSequence & " = " & XMDD.LineNo & " and " & Item & " = " & XMDD.ItemNo & "" &
        " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
        " where " & wStandard & "and " & Genaral & "and " & SampleGenaral & "" &
        " and " & XMDD.wStandard & "and " & XMDD.SampleGenaral & "" &
        " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
        " and " & Whr & "@whr order by " & Item & ""
    Public Shared Function DeliveryQty(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectDelQty
        Oral = Oral.Replace("@whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Sum Item Where Genaral
    Private Shared SelectSOLineItemGenaral As String = "select " & SaleOrderNo & "," & ItemSequence & "," & Item & "," &
    " " & CustID & "," & SalesQty & " from " & tblSaleItem & " " &
    " where " & wStandard & " and " & Genaral & " and " & SampleGenaral & "" &
    " and " & SaleOrderNo & "='@SaleOrderNo'"
    Public Shared Function GetSOHeadItemGenaral(ByVal SOHead As String)
        Dim Oral As String = SelectSOLineItemGenaral
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SaleOrderNo", SOHead)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page CheckBOM (item Sample Only) 
    '--Sum SampleReqQty for One SO / Refresh DataTable
    Private Shared SelectsmpReqQtyForOneSO As String = "select " & Item & ",sum(" & SalesQty & ") as SampleReqQty from " & tblSaleItem & "" &
    " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
    " where " & wStandard & " and " & Genaral & " and " & Sample & "" &
    " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
    " and " & SaleOrderNo & "='@SO'and " & ItemSequence & "='@Seq'and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function smpReqForOneSO(ByVal SO As String, ByVal Seq As String, ByVal ItemNo As String)
        Dim Oral As String = SelectsmpReqQtyForOneSO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SO", SO)
        Oral = Oral.Replace("@Seq", Seq)
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page CheckBOM (item Sample Only) 
    '--Select SampleDeliveryQty  for One SO / Refresh DataTable
    Private Shared SelectsmpDelForOneSO As String = "select " & Item & ",sum(" & XMDD.ShippedQty & ") as sampleDeliQty  from " & tblSaleItem & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " On " & SaleOrderNo & " = " & SaleOrderNo & " and " & ItemSequence & " = " & XMDD.LineNo & " and " & Item & " = " & XMDD.ItemNo & "" &
        " left join " & XMDA.tblSaleHead & " on " & SaleOrderNo & " = " & XMDA.SaleOrderNo & "" &
        " where " & wStandard & "and " & Genaral & "and " & Sample & "" &
        " and " & XMDD.wStandard & "and " & XMDD.Sample & "" &
        " and " & XMDA.wStandard & "and " & XMDA.Approved & "" &
        " and " & SaleOrderNo & "='@SO'and " & ItemSequence & "='@Seq'and " & Item & "='@ItemNo' group by " & Item & " order by " & Item & ""
    Public Shared Function smpDelForOneSO(ByVal SO As String, ByVal Seq As String, ByVal ItemNo As String)
        Dim Oral As String = SelectsmpDelForOneSO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SO", SO)
        Oral = Oral.Replace("@Seq", Seq)
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function





    '######### for All Filed SaleOrder Deatil Where SaleOrder_No. = ? : Body Tab : SO Detail #########################
    Private Shared strSO_SaleDetail As String = "Select " & SaleOrderNo & " ," & ItemSequence & "," & Item & ", " & ProductFeature & ", " &
       " " & PackagingContainer & " ," & OperationNo & "," & Process & "," & SalesUnit & "," & SalesQty & ", " &
       " " & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & "," & PricingQty & "," & BookShippingDate & ", " &
       " " & ExpectedReceiptDate & "," & UnitPrice & "," & TaxType & "," & TaxRate & "," & SubItemFeature & "," &
       " " & UrgentItem & "," & Bond & "," & PartialDelivery & "," & Shippinglocation & "," & CollectionLocation & "," &
       " " & CustID & "," & MultiDeliveryPeriod & "," & ReceiptAddressNo & "," & BillAddressNo & "," & CustomerItemNo & "," &
       " " & RestrictedWarehouseLocation & "," & RestrictedStorageLocation & "," & RestrictedBatchNumber & "," & MeansOfTransportation & "," & PickingMethod & "," &
       " " & RateOfSpares & "," & OverShippingRatio & "," & PriceDetermination & "," & ProjectNo & "," & WBSNo & "," &
       " " & ActivityNo & "," & CauseOfSpending & "," & PricingSource & "," & PriceReferenceNo & "," & PriceReferenceItems & "," &
       " " & AccessPrices & "," & PercentSpread & "," & StatusCode & "," & AmtExclTax & "," & AmtInclTax & "," & Tax & ", " &
       " " & ReasonCode & "," & Notes & "," & CustomerOrderItemNo & "," & Inspection & "," & SettlementReasonCode & "," & BOMEffectiveDate & ", " &
       " " & SourceDocNo & "," & SourceItemSeq & "," & InventoryManagmentFeature & "," & ReturnVolume & "," & QuantityRepaymentReferenceQty & ", " &
       " " & PriceRepaymentQty & "," & PriceRepaymentReferenceQty & "," & BOMFeatures & "," & CashDiscountNo & "," & CashDiscountItemSeq & "  " &
       " FROM " & tblSaleItem & "  " &
       " where " & wStandard & " AND " & SaleOrderNo & " =@pSaleOrder_No "
    Public Shared Function getSO_SaleDetail(strSaleOrder_No As String) As DataTable
        Dim Sql As String = strSO_SaleDetail.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDC", "getSO_SaleDetail", "Sql = strSO_SaleDetail", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSO_SaleDetail_DataSet(strSaleOrder_No As String) As DataSet
        Dim Sql As String = strSO_SaleDetail.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDC", "getSO_SaleDetail_DataSet", "Sql = strSO_SaleDetail", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '######### for All Filed SaleOrder Deatil Where ItemNo. = ? : Body Tab : SO Detail #########################
    Private Shared strSO_SaleItemNo As String = "Select " & SaleOrderNo & " ," & ItemSequence & "," & Item & ", " & ProductFeature & ", " &
       " " & PackagingContainer & " ," & OperationNo & "," & Process & "," & SalesUnit & "," & SalesQty & ", " &
       " " & ReferenceUnit & "," & ReferenceQty & "," & PricingUnit & "," & PricingQty & "," & BookShippingDate & ", " &
       " " & ExpectedReceiptDate & "," & UnitPrice & "," & TaxType & "," & TaxRate & "," & SubItemFeature & "," &
       " " & UrgentItem & "," & Bond & "," & PartialDelivery & "," & Shippinglocation & "," & CollectionLocation & "," &
       " " & CustID & "," & MultiDeliveryPeriod & "," & ReceiptAddressNo & "," & BillAddressNo & "," & CustomerItemNo & "," &
       " " & RestrictedWarehouseLocation & "," & RestrictedStorageLocation & "," & RestrictedBatchNumber & "," & MeansOfTransportation & "," & PickingMethod & "," &
       " " & RateOfSpares & "," & OverShippingRatio & "," & PriceDetermination & "," & ProjectNo & "," & WBSNo & "," &
       " " & ActivityNo & "," & CauseOfSpending & "," & PricingSource & "," & PriceReferenceNo & "," & PriceReferenceItems & "," &
       " " & AccessPrices & "," & PercentSpread & "," & StatusCode & "," & AmtExclTax & "," & AmtInclTax & "," & Tax & ", " &
       " " & ReasonCode & "," & Notes & "," & CustomerOrderItemNo & "," & Inspection & "," & SettlementReasonCode & "," & BOMEffectiveDate & ", " &
       " " & SourceDocNo & "," & SourceItemSeq & "," & InventoryManagmentFeature & "," & ReturnVolume & "," & QuantityRepaymentReferenceQty & ", " &
       " " & PriceRepaymentQty & "," & PriceRepaymentReferenceQty & "," & BOMFeatures & "," & CashDiscountNo & "," & CashDiscountItemSeq & "  " &
       " FROM " & tblSaleItem & "  " &
       " where " & wStandard & " AND " & Item & " =@pItem "
    Public Shared Function getSO_SaleDetail_ByItemNo(strItemNo As String) As DataTable
        Dim Sql As String = strSO_SaleDetail.Replace("@pItem", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDC", "getSO_SaleDetail_ByItemNo", "Sql = strSO_SaleItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strWH_ItemNo As String = "Select " & SaleOrderNo & "," & Item & "," & SalesUnit & "  FROM " & tblSaleItem & "  where " & Item & " =@ItemNO "
    Public Shared Function GetDataMoProcessHeader(ByVal WH_ItemNo As String) As DataTable
        Dim strSQL As String = strWH_ItemNo
        strSQL = strSQL.Replace("@ItemNO", "'" & WH_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection
        Try
            With objConn
                .ConnectionString = clsDBConnect.strT100ConnectionString
                .Open()
            End With
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDC", "GetDataMoProcessHeader", "Sql = strWH_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataMoProcessHeaderDataSet(ByVal WH_ItemNo As String) As DataSet
        Dim strSQL As String = strWH_ItemNo
        strSQL = strSQL.Replace("@ItemNO", "'" & WH_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection
        Try
            With objConn
                .ConnectionString = clsDBConnect.strT100ConnectionString
                .Open()
            End With
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDC", "GetDataMoProcessHeaderDataSet", "Sql = strWH_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class
