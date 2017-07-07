Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMEE
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmee_t
    '# axmt510 : SaleOrder Change,SaleForcast Change : Header
    ''' <remarks> Header </remarks>
    Public Shared tblHeaderSaleChangeOrder As String = "xmee_t"
    Public Shared ent As String = "xmeeent"
    Public Shared Site As String = "xmeesite"
    Public Shared DocNo As String = "xmeedocno"
    Public Shared Version As String = "xmee001"
    Public Shared Status As String = "xmeestus"
    Public Shared ChangedDate As String = "xmee902"
    Public Shared Salesperson As String = "xmee002"
    Public Shared SalesDepartment As String = "xmee003"
    Public Shared CustomerNo As String = "xmee004"
    Public Shared OrderType As String = "xmee005"
    Public Shared InterCompany As String = "xmee006"
    Public Shared Source As String = "xmee007"
    Public Shared SourceDocNo As String = "xmee008"
    Public Shared CollectionTerm As String = "xmee009"
    Public Shared TradeTerm As String = "xmee010"
    Public Shared TaxType As String = "xmee011"
    Public Shared TaxRate As String = "xmee012"
    Public Shared TaxIncludedInUP As String = "xmee013"
    Public Shared Currency As String = "xmee015"
    Public Shared ExchRate As String = "xmee016"
    Public Shared PricingMethod As String = "xmee017"
    Public Shared CollectionPreference As String = "xmee018"
    Public Shared IncludeToAPS As String = "xmee019"
    Public Shared ShipBy As String = "xmee020"
    Public Shared BilledToCustomer As String = "xmee021"
    Public Shared ShipTo As String = "xmee022"
    Public Shared SalesChannel As String = "xmee023"
    Public Shared SalesClass2 As String = "xmee024"
    Public Shared ShippingAddress As String = "xmee025"
    Public Shared BillAddress As String = "xmee026"
    Public Shared ContactPerson As String = "xmee027"
    Public Shared OneTimeCounterpartyID As String = "xmee028"
    Public Shared ShippingDepartment As String = "xmee029"
    Public Shared InterCompanyTradeTransferred As String = "xmee030"
    Public Shared MultilateralSourceDocNo As String = "xmee031"
    Public Shared RetentionReason As String = "xmee032"
    Public Shared CustomerPONo As String = "xmee033"
    Public Shared FinalCustomer As String = "xmee034"
    Public Shared InvoiceType As String = "xmee035"
    Public Shared ShippingVendor As String = "xmee036"
    Public Shared Departure As String = "xmee037"
    Public Shared Destination As String = "xmee038"
    Public Shared PreCollectionInvoiceCreationMethod As String = "xmee039"
    Public Shared TotalAmtExclTax As String = "xmee041"
    Public Shared TotalAmtInclTax As String = "xmee042"
    Public Shared OverallOrderTax As String = "xmee043"
    Public Shared ShippingMarkCode As String = "xmee044"
    Public Shared ClosureByLogistics As String = "xmee045"
    Public Shared ClosureByAccounts As String = "xmee046"
    Public Shared ClosureByCash As String = "xmee047"
    Public Shared OrderType2 As String = "xmee048"
    Public Shared ExchangeCalcBasis As String = "xmee049"
    Public Shared InterCompanyProcessNo As String = "xmee050"
    Public Shared MultilateralFinalDestination As String = "xmee051"
    Public Shared Notes As String = "xmee071"
    Public Shared ChangeSN As String = "xmee900"
    Public Shared ChangedType As String = "xmee901"
    Public Shared ReasonOfChange As String = "xmee903"
    Public Shared ChangeNotes As String = "xmee904"
    Public Shared PurchaseOrderedSettledYN As String = "xmeeacti"
    Public Shared TransferRetailerSerialNo As String = "xmee200"
    Public Shared DeliveryAgentNo As String = "xmee201"
    Public Shared SalesOffice As String = "xmee202"
    Public Shared InvoicedTo As String = "xmee203"
    Public Shared PromotionProjectNo As String = "xmee204"
    Public Shared TheEntireDiscount As String = "xmee205"
    Public Shared DeliveryPointsSerialNo As String = "xmee206"
    Public Shared TransportRouteSerialNo As String = "xmee207"
    Public Shared Region As String = "xmee208"
    Public Shared NumberCounties As String = "xmee209"
    Public Shared ProvinceRegionSerialNo As String = "xmee210"
    Public Shared Region2 As String = "xmee211"
    Public Shared ApplicationOrgUint As String = "xmeeunit"
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "xmeeownid"
    Public Shared DataOwnerDept As String = "xmeeowndp"
    Public Shared DataCreatedBy As String = "xmeecrtid"
    Public Shared DataCreatedByDept As String = "xmeecrtdp"
    Public Shared DataCreatedDate As String = "xmeecrtdt"
    Public Shared ModifiedBy As String = "xmeemodid"
    Public Shared LastModifiedDate As String = "xmeemoddt"
    Public Shared DataConfirmedBy As String = "xmeecnfid"
    Public Shared DataConfirmedDate As String = "xmeecnfdt"

    Public Shared UnaAppoved As String = "N"
    Public Shared Appoved As String = "Y"
    Public Shared Whr As String = ""
    Public Shared Seles1 As String = "MKTS1"
    Public Shared Seles2 As String = "MKTS2"
    Public Shared Seles3 As String = "MKTS3"
    Public Shared Seles4 As String = "MKTS4"
    Public Shared Seles5 As String = "MKTS5"
    Public Shared US As String = "en_US"

    ''' <remarks>Where Starndrad </remarks>
    Public Shared wStandard As String = ent & "='3' and " & Site & "='JINPAO'"

    '--Page SaleOrderChangeStus
    '--WHR SaleOrderChaange Tap Report  / No Rrfesh DataTable 
    Private Shared SelectSOChangeHead As String = "select " & DocNo & "," & Version & "," & ChangedDate & "," & ReasonOfChange & " from " & tblHeaderSaleChangeOrder & " " &
        " where " & wStandard & " and  " & DocNo & " ='@OrderNo' and " & Version & " ='@Version'"
    Public Shared Function GetSOChg(ByVal SOType As String, ByVal SONo As String, ByVal ChgVer As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectSOChangeHead,
            Conv As String = "JP" & SOType & "-" & SONo & ""
        Oral = Oral.Replace("@OrderNo", Conv)
        Oral = Oral.Replace("@Version", ChgVer)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Page SaleOrderChangeStus 
    '--WHR SaleOrderChaange Tap Report  / No Rrfesh DataTable 
    Private Shared SelectWhrSOChgHead As String = "select " & Status & "," &
        " " & DocNo & "," & Version & "," & ChangedDate & "," & SalesDepartment & "," & CustomerNo & "," & ReasonOfChange & " from " & tblHeaderSaleChangeOrder & " " &
        " where " & wStandard & "and " & SalesDepartment & " not in ('JINPAO') and " & Whr & "@Whr"
    Public Shared Function GetWhrSOChgHead(ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectWhrSOChgHead
        Oral = Oral.Replace("@Whr", Whr)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '######### where DocNo =? #########################
    Private Shared strHeaderSaleChangeOrder As String = "Select * from " & tblHeaderSaleChangeOrder & " " &
       " where " & wStandard & " AND " & DocNo & " =@pDocNo "
    Public Shared Function getHeaderSaleChangeOrderByDocNo(strDocNo As String) As DataTable
        Dim Sql As String = strHeaderSaleChangeOrder
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEE", "getHeaderSaleChangeOrderByDocNo", "Sql = strHeaderSaleChangeOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getHeaderSaleChangeOrderDataSetByDocNo(strDocNo As String) As DataSet
        Dim Sql As String = strHeaderSaleChangeOrder
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEE", "getHeaderSaleChangeOrderDataSetByDocNo", "Sql = strHeaderSaleChangeOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where Custom  #########################
    Private Shared strHeaderSaleChangeOrderCustom As String = "Select * from " & tblHeaderSaleChangeOrder & " " &
       " where " & wStandard & " AND @pCustomWhere "
    Public Shared Function getHeaderSaleChangeOrderCustom(strCustomWhere As String) As DataTable
        Dim Sql As String = strHeaderSaleChangeOrderCustom
        Sql = Sql.Replace("@pCustomWhere", strCustomWhere)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEE", "getHeaderSaleChangeOrderCustom", "Sql = strHeaderSaleChangeOrderCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getHeaderSaleChangeOrderCustomDataSet(strCustomWhere As String) As DataSet
        Dim Sql As String = strHeaderSaleChangeOrderCustom
        Sql = Sql.Replace("@pCustomWhere", strCustomWhere)
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEE", "getHeaderSaleChangeOrderCustomDataSet", "Sql = strHeaderSaleChangeOrderCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
