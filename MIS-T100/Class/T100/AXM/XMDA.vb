Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class XMDA
    ''' <remarks>
    ''' # Module : XMDA
    ''' # Table : xmda_t
    ''' #***** SaleOrder & SaleOrder Forecast Header ***************************
    ''' # Function for select rows Top 100 (Example)
    ''' # select * from Table where field ='string' AND field2 ='string'
    ''' sselect * from xmda_t where xmdasite ='JINPAO' and  rownum <= 100 
    ''' </remarks>
    Private Shared AXM As String = "AXM"
    '''<reamrks>##########Table SaleOrder Header##############</reamrks>
    Public Shared tblSaleHead As String = "xmda_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "xmdaent"
    Public Shared Site As String = "xmdasite"
    '''<reamrks> # Trading Information</reamrks>
    Public Shared SaleOrderNo As String = "xmdadocno"
    Public Shared VersionNo As String = "xmda001"
    Public Shared DocumentDate As String = "xmdadocdt"
    Public Shared SaleEpmID As String = "xmda002"
    Public Shared SaleDeptId As String = "xmda003"
    Public Shared CustomerId As String = "xmda004"
    Public Shared OrderType As String = "xmda005"
    Public Shared MultiliProperty As String = "xmda006"
    Public Shared Source As String = "xmda007"
    Public Shared SourceDocNo As String = "xmda008"
    Public Shared CustPurchaseOrderNo As String = "xmda008"
    Public Shared CustomerContacts As String = "xmda027"
    Public Shared PaymentTerm As String = "xmda009"
    Public Shared TradeTerms As String = "xmda010"
    Public Shared TaxCode As String = "xmda011"
    Public Shared Tax As String = "xmda012"
    Public Shared TaxIncludedUP As String = "xmda013"
    Public Shared InvoiceType As String = "xmda035"
    Public Shared Currency As String = "xmda015"
    Public Shared ExchangeReate As String = "xmda016"
    Public Shared PricingMethod As String = "xmda017"
    Public Shared DiscountCondition As String = "xmda018"
    Public Shared IncludeMRS As String = "xmda019"
    Public Shared SaleChannel As String = "xmda023"
    Public Shared HoldReson As String = "xmda032"
    Public Shared OrderNote As String = "xmda071"
    Public Shared IntercompanyTPid As String = "xmda050"
    Public Shared DocStatus As String = "xmdastus"

    Public Shared CustomerPONo As String = "xmda033" 'add by noi on 2017-07-05



    '''<reamrks> # Order Information</reamrks>
    Public Shared CustomerIdOrder As String = "xmda021"
    Public Shared ReceivingCustomerID As String = "xmda022"
    Public Shared FinalCustomerId As String = "xmda034"
    Public Shared InvoiceTo As String = "xmda203"
    Public Shared SacleClass As String = "xmda024"
    Public Shared OrderType2 As String = "xmda048"
    Public Shared ExchangeRateCalu As String = "xmda049"
    Public Shared ShippingMarkCode As String = "xmda044"
    Public Shared MultiTradeSwicth As String = "xmda030"
    Public Shared IntercompayTradeSN As String = "xmda031"
    Public Shared LogisticsSetting As String = "xmda045"
    Public Shared AccountFlowSettelment As String = "xmda046"
    Public Shared CashSettelment As String = "xmda047"
    '''<reamrks> # Transport Information</reamrks>
    Public Shared RecieptAddress As String = "xmda025"
    Public Shared BillAddres As String = "xmda026"
    Public Shared DeliveryMethod As String = "xmda020"
    Public Shared ShipmentStartLoction As String = "xmda037"
    Public Shared Destination As String = "xmda038"
    Public Shared DeliveryVendor As String = "xmda036"
    '''<reamrks> # Multi Account</reamrks>
    Public Shared PI_InvoiceTheWay As String = "xmda039"
    Public Shared TotalAmtExclTax As String = "xmda041"
    Public Shared TotalOrderAmonyIncTax As String = "xmda042"
    '''<reamrks> # Adjusment Information</reamrks>
    '''<reamrks> # Data Belonging </reamrks>
    Public Shared DataOwner As String = "xmdawnid"
    Public Shared DataOwner_Dept As String = "xmdawndp"
    Public Shared DataCreateBy As String = "xmdacrtid"
    Public Shared DateCreateByDept As String = "xmdacrtdp"
    Public Shared DataCreateDate As String = "xmdacrtdt"
    '''<reamrks> # Data Changes </reamrks>
    Public Shared DataModifyBy As String = "xmdamodid"
    Public Shared LastModifyDate As String = "xmdamoddt"
    Public Shared DataConfirmedPersonal As String = "xmdacnfid"
    Public Shared DataConfirmDate As String = "xmdacnfdt"




    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared Whr As String = ""
    Public Shared Approved As String = DocStatus & "='Y'"
    Public Shared Unconfirmed As String = DocStatus & "='N'"
    Public Shared StockInOut As String = ""
    '--Page SaleUndeliveryStatusAmount
    '--Shearch SaleOrder  where whr / no Refresh DataTable
    Private Shared SelectUndeliveryStusAmount1 As String = "Select " & XMDC.Item & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & XMDC.SalesUnit & ",max(" & XMDC.UnitPrice & ") as Price," & ExchangeReate & " from " & tblSaleHead & "" &
    " left join  " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join  " & IMAA.tblProductItemDeatil & " On " & XMDC.Item & " = " & IMAA.ItemNo & "" &
    " left join  " & IMAAL.tblProductionDetail & " On " & IMAA.ItemNo & " = " & IMAAL.ProductItem & "" &
    " where 
    " & wStandard & " And " & Approved & " and " &
    " " & XMDC.wStandard & " and " & XMDC.Genaral & " and " & XMDC.SampleGenaral & " and " &
    " " & IMAAL.WStandard & " and " & IMAAL.enUS & " and " &
    " " & IMAA.wStandard & " and " & IMAA.ProductClassification & " in ('" & RTAXL.Electronic & "','" & RTAXL.Aerospace & "','" & RTAXL.Automotive & "','" & RTAXL.Telecommunic & "','" & RTAXL.Medical & "','" & RTAXL.FoodIndustry & "','" & RTAXL.Energy & "','" & RTAXL.Transportati & "')" &
    " " & Whr & "@Whr group by " & XMDC.Item & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & XMDC.SalesUnit & "," & ExchangeReate & " order by " & XMDC.Item & ""
    Public Shared Function GetWhrUndelStusAmt(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectUndeliveryStusAmount1
        Oral = Oral.Replace("@Whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SLUndelStusAmountPopUp Page SaleUndeliveryStatusPopUp
    '--Shearch SaleOrder Genaral / Refresh DataTable
    Private Shared SelectSOAmountPopUpG As String = "Select " & CustomerId & "," & SaleOrderNo & "," & VersionNo & ",substr(" & DataConfirmDate & ",0,10) As Cnfidt," & XMDC.BookShippingDate & "," &
    " " & XMDC.SalesQty & "," & XMDD.ShippedQty & "," & XMDC.SalesUnit & ",nvl(" & XMDC.Notes & ",'-') as POSO" &
    " from " & tblSaleHead & "" &
    " left join " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join " & XMDD.tblSaleItemDeliveryDetail & " on " & XMDC.SaleOrderNo & "=" & XMDD.SaleOrderNo & " And " & XMDC.ItemSequence & "=" & XMDD.LineNo & " And " & XMDC.Item & "=" & XMDD.ItemNo & "" &
    " where 
    " & wStandard & " And " & Approved & " and " &
    " " & XMDC.wStandard & " and " & XMDC.Genaral & " and " & XMDC.SampleGenaral & " and " &
    " " & XMDD.wStandard & " And " & XMDD.SampleGenaral & " and " &
    " " & XMDC.Item & "='@Item'"
    Public Shared Function SOAmountPopUpG(ByVal Item As String, ByVal DateFrom As String, ByVal DateTo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectSOAmountPopUpG
        Oral = Oral.Replace("@Item", Item)
        Oral = Oral.Replace("@DateFrom", DateFrom)
        Oral = Oral.Replace("@DateTo", DateTo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SLUndelStusAmountPopUp and Page SaleUndeliveryStatusPopUp
    '--Shearch SaleOrder Sample / Refresh DataTable
    Private Shared SelectSOAmountPopUpS As String = "Select " & CustomerId & "," & SaleOrderNo & "," & VersionNo & ",substr(" & DataConfirmDate & ",0,10) As Cnfidt," & XMDC.BookShippingDate & "," &
    " " & XMDC.SalesQty & "," & XMDD.ShippedQty & "," & XMDC.SalesUnit & ",nvl(" & XMDC.Notes & ",'-') as POSO" &
    " from " & tblSaleHead & "" &
    " left join " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join " & XMDD.tblSaleItemDeliveryDetail & " on " & XMDC.SaleOrderNo & "=" & XMDD.SaleOrderNo & " And " & XMDC.ItemSequence & "=" & XMDD.LineNo & " And " & XMDC.Item & "=" & XMDD.ItemNo & "" &
    " where 
    " & wStandard & " And " & Approved & " and " &
    " " & XMDC.wStandard & " and " & XMDC.Genaral & " and " & XMDC.Sample & " and " &
    " " & XMDD.wStandard & " And " & XMDD.SampleGenaral & " and " &
    " " & XMDC.Item & "='@Item' And " & SaleOrderNo & "='@SaleOrderNo' and " & VersionNo & "='@VersionNo'"
    Public Shared Function SOAmountPopUpS(ByVal Item As String, ByVal SaleOrderNo As String, ByVal VersionNo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectSOAmountPopUpS
        Oral = Oral.Replace("@Item", Item)
        Oral = Oral.Replace("@SaleOrderNo", SaleOrderNo)
        Oral = Oral.Replace("@VersionNo", VersionNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SaleUndeliveryStatus and Page SaleUndelivery Status Period
    '--Shearch SaleOrder where whr /no Refresh DataTable
    Private Shared SelectWhrUndelStusAmt As String = "Select " & CustomerId & "," & XMDC.Item & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," &
    " " & XMDC.SalesUnit & "," & RTAXL.Description & " from " & tblSaleHead & "" &
    " left join " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join " & PMAAL.tblCustomerName & " On " & CustomerId & " = " & PMAAL.CustomerID & "" &
    " left join " & IMAA.tblProductItemDeatil & " On " & XMDC.Item & " = " & IMAA.ItemNo & "" &
    " left join " & IMAAL.tblProductionDetail & " On " & IMAA.ItemNo & " = " & IMAAL.ProductItem & "" &
    " left join " & RTAXL.tblClassificationSecondary & " on " & IMAA.ProductClassification & " = " & RTAXL.ItemCategoryNo & "" &
    " where 
    " & wStandard & " And " & Approved & " And " &
    " " & XMDC.wStandard & " And " & XMDC.Genaral & " And " & XMDC.SampleGenaral & " And " &
    " " & IMAAL.WStandard & " And " & IMAAL.enUS & " And " &
    " " & RTAXL.WStandard & " And " & RTAXL.enUS & " And " &
    " " & PMAAL.WStandard & " And " & PMAAL.enUS & " And " &
    " " & IMAA.wStandard & " And " & IMAA.ProductClassification & " in ('" & RTAXL.Electronic & "','" & RTAXL.Aerospace & "','" & RTAXL.Automotive & "','" & RTAXL.Telecommunic & "','" & RTAXL.Medical & "','" & RTAXL.FoodIndustry & "','" & RTAXL.Energy & "','" & RTAXL.Transportati & "')" &
    " " & Whr & "@Whr group by " & CustomerId & "," & XMDC.Item & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & XMDC.SalesUnit & "," & RTAXL.Description & " order by " & XMDC.Item & ""
    Public Shared Function GetWhrUndelStus(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectWhrUndelStusAmt
        Oral = Oral.Replace("@Whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page CheckBOM (item Genaral Only) 
    '--Shearch SaleOrder  where whr /no Refresh DataTable
    Private Shared GetWhrChkBOM As String = "select " & SaleOrderNo & " || ' - ' || " & XMDC.ItemSequence & " as SO," &
        " " & SaleOrderNo & "," & VersionNo & "," & XMDC.ItemSequence & "," & XMDC.Item & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," &
        " case " & IMAA.ItemCategory & " when 'A' then 'Manufacturing' when 'M' then 'Purchase' else '-' end as ItemCategory," &
        " " & CustomerId & " || ' - ' || " & PMAAL.CustomerName & " as Cust," &
        " " & XMDC.SalesQty & "," & XMDD.ShippedQty & "," & XMDC.BookShippingDate & "," & RTAXL.Description & "," &
        " case when (select count(*) from " & BMAA.tblBOMheader & " where " & BMAA.Approved & "and " & BMAA.WStandard & " and " & BMAA.MasterItemNo & "=" & XMDC.Item & ")=0 then 'N' else 'Y' end as BOM," &
        " (select case when " & BMAA.DataModifyBy & "='00000' then substr(" & BMAA.LastModifyDate & ",0,10) else substr(" & BMAA.DataCreateDate & ",0,10) end from " & BMAA.tblBOMheader & "  where " & BMAA.Approved & "and " & BMAA.WStandard & " and rownum <=1) as LastBOMUpdate" &
        "  from " & tblSaleHead & "" &
        " left join " & XMDC.tblSaleItem & " on " & SaleOrderNo & "=" & XMDC.SaleOrderNo & "" &
        " left join " & XMDD.tblSaleItemDeliveryDetail & " on " & XMDC.SaleOrderNo & "=" & XMDD.SaleOrderNo & " And " & XMDC.ItemSequence & "=" & XMDD.LineNo & " And " & XMDC.Item & "=" & XMDD.ItemNo & "" &
        " left join " & PMAAL.tblCustomerName & " On " & CustomerId & " = " & PMAAL.CustomerID & "" &
        " left join " & IMAA.tblProductItemDeatil & " On " & XMDC.Item & " = " & IMAA.ItemNo & "" &
        " left join " & IMAAL.tblProductionDetail & " On " & IMAA.ItemNo & " = " & IMAAL.ProductItem & "" &
        " left join " & RTAXL.tblClassificationSecondary & " on " & IMAA.ProductClassification & " = " & RTAXL.ItemCategoryNo & " where" &
        " " & wStandard & " And " &
        " " & XMDC.wStandard & " And " & XMDC.Genaral & " And " & XMDC.SampleGenaral & "And " &
        " " & XMDD.wStandard & " And " & XMDD.SampleGenaral & "And " &
        " " & PMAAL.WStandard & " And " & PMAAL.enUS & "And " &
        " " & IMAA.wStandard & " And " & IMAA.ProductClassification & " in ('" & RTAXL.Electronic & "','" & RTAXL.Aerospace & "','" & RTAXL.Automotive & "','" & RTAXL.Telecommunic & "','" & RTAXL.Medical & "','" & RTAXL.FoodIndustry & "','" & RTAXL.Energy & "','" & RTAXL.Transportati & "')And " &
        " " & IMAAL.WStandard & " And  " & IMAAL.enUS & "And " &
        " " & RTAXL.LGStandard & " And  " & RTAXL.WStandard & "" &
        " " & Whr & "@Whr order by SO desc"
    Public Shared Function WhrChkBOM(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = GetWhrChkBOM
        Oral = Oral.Replace("@Whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page UndeliveryStusAmount
    '--Sum Stock Amount / Refresh DataTable
    Private Shared SelectSumStockAmount As String = "select sum(" & StockInOut & "'@StockInOut'*" & XMDC.UnitPrice & "*" & ExchangeReate & ") as StockAmount from " & tblSaleHead & " " &
    " left join  " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join  " & PMAAL.tblCustomerName & " On " & CustomerId & " = " & PMAAL.CustomerID & "" &
    " left join  " & IMAAL.tblProductionDetail & " On " & XMDC.Item & " = " & IMAAL.ProductItem & "" &
    " where " & wStandard & " and " & Approved & "" &
    " and " & XMDC.wStandard & " and " & XMDC.Genaral & " and " & XMDC.SampleGenaral & "" &
    " and " & PMAAL.WStandard & " and " & PMAAL.enUS & "" &
    " and " & IMAAL.WStandard & " and " & IMAAL.enUS & "" &
    " and " & XMDC.Item & "='@ItemNo'" & Whr & "@Whr order by " & DataConfirmDate & " desc"
    Public Shared Function SumStockAmount(ByVal ItemNo As String, ByVal Whr As String, ByVal StockInOut As String)
        Dim Oral As String = SelectSumStockAmount
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        Oral = Oral.Replace("@Whr", Whr)
        Oral = Oral.Replace("@StockInOut", StockInOut)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page Sales Quotation (item Genaral Only)
    '--Shearch SaleOrder SUM RequestQty where whr / no Refresh DataTable
    Private Shared SelectWhrQuotation As String = "Select " & SaleOrderNo & "," & VersionNo & ",to_char(" & DocumentDate & ",'yyyymmdd') " & DocumentDate & "," & CustomerId & "," & PMAAL.ContactName & "," &
    " " & XMDC.Item & "," & IMAAL.Specifaction & ",sum(" & XMDC.SalesQty & ") as RequestQty," & XMDC.UnitPrice & "," & XMDC.RowsStus & "  from " & tblSaleHead & "" &
    " left join  " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join  " & PMAAL.tblCustomerName & " On " & CustomerId & " = " & PMAAL.ContactID & "" &
    " left join  " & IMAAL.tblProductionDetail & " On " & XMDC.Item & " = " & IMAAL.ProductItem & "" &
    " where " & wStandard & " and " & Approved & "" &
    " and " & XMDC.wStandard & " and " & XMDC.SampleGenaral & "" &
    " and " & PMAAL.WStandard & " and " & PMAAL.enUS & "" &
    " and " & IMAAL.WStandard & " and " & IMAAL.enUS & "" & Whr & "@Whr" &
    " group by " & SaleOrderNo & "," & VersionNo & "," & DocumentDate & "," & CustomerId & "," & PMAAL.ContactName & "," & XMDC.Item & "," & IMAAL.Specifaction & "," & XMDC.UnitPrice & "," & XMDC.RowsStus & "  order by " & XMDC.Item & ""
    Public Shared Function GetWhrQuotation(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectWhrQuotation
        Oral = Oral.Replace("@Whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page Sales Quotation (item Sample Only)
    '--Shearch SaleOrder SUM SampleRequestQty where ItemNo / Refresh DataTable
    Private Shared SelectWhrQuotationSample As String = "Select " & SaleOrderNo & "," & VersionNo & "," & DocumentDate & "," & CustomerId & "," & PMAAL.CustomerName & "," &
    " " & XMDC.Item & "," & IMAAL.Specifaction & ",sum(" & XMDC.SalesQty & ") as SampleRequestQty," & XMDC.RowsStus & "  from " & tblSaleHead & "" &
    " left join  " & XMDC.tblSaleItem & " On " & SaleOrderNo & " = " & XMDC.SaleOrderNo & "" &
    " left join  " & PMAAL.tblCustomerName & " On " & CustomerId & " = " & PMAAL.CustomerID & "" &
    " left join  " & IMAAL.tblProductionDetail & " On " & XMDC.Item & " = " & IMAAL.ProductItem & "" &
    " where " & wStandard & " and " & Approved & "" &
    " and " & XMDC.wStandard & " and " & XMDC.Sample & "" &
    " and " & PMAAL.WStandard & " and " & PMAAL.enUS & "" &
    " and " & IMAAL.WStandard & " and " & IMAAL.enUS & " and " & XMDC.Item & "='@ItemNo' and " & SaleOrderNo & "='@SaleOrderNo' and " & VersionNo & "='@VersionNo' and " & XMDC.RowsStus & "='@RowsStus'" &
    " group by " & SaleOrderNo & "," & VersionNo & "," & DocumentDate & "," & CustomerId & "," & PMAAL.CustomerName & "," & XMDC.Item & "," & IMAAL.Specifaction & "," & SaleOrderNo & "," & VersionNo & "," & XMDC.RowsStus & ""
    Public Shared Function SheachQuotationSample(ByVal SaleOrderNo As String, ByVal VersionNo As String, ByVal ItemNo As String, ByVal RowsStatus As String)
        Dim Oral As String = SelectWhrQuotationSample
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@SaleOrderNo", SaleOrderNo)
        Oral = Oral.Replace("@VersionNo", VersionNo)
        Oral = Oral.Replace("@ItemNo", ItemNo)
        Oral = Oral.Replace("@RowsStus", RowsStatus)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function



    'tos
    Private Shared strCustomerItemSale As String = "select " & CustomerId & " from " & tblSaleHead & "  where  " & SaleOrderNo & " =@DocNo"
    '''<remarks># Sale Order Header DataTable</remarks>
    Public Shared Function GetCustomerItemSale(ByVal SaleOrderId As String) As DataTable
        Dim strSql As String = strCustomerItemSale
        strSql = strSql.Replace("@DocNo", "'" & SaleOrderId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDA", "GetCustomerItemSale", "strSql = strCustomerItemSale", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '''<remarks># Sale Order Header DataSet</remarks>
    Public Shared Function GetCustomerItemSaleDataSet(ByVal SaleOrderId As String) As DataSet
        Dim strSql As String = strCustomerItemSale
        strSql = strSql.Replace("@DocNo", "'" & SaleOrderId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDA", "GetCustomerItemSaleDataSet", "strSql = strCustomerItemSale", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    Private Shared strSaleOrder As String = "select * from '" & tblSaleHead & "'  where '" & Site & "' ='JINPAO' and '" & SaleOrderNo & "' =@SaleOrder"
    '''<remarks># Sale Order Header DataTable</remarks>
    Public Shared Function GetDataSlaeOrder(ByVal SaleOrderId As String) As DataTable
        Dim strSql As String = strSaleOrder
        strSql = strSql.Replace("@SaleOrder", "'" & SaleOrderId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDA", "GetDataSlaeOrder", "strSql = strSaleOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '''<remarks># Sale Order Header DataSet</remarks>
    Public Shared Function GetDocTypeSaleDataSet(ByVal SaleOrderId As String) As DataSet
        Dim strSql As String = strSaleOrder
        strSql = strSql.Replace("@SaleOrder", "'" & SaleOrderId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDA", "GetDocTypeSaleDataSet", "strSql = strSaleOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
