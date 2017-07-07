Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMEG
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmeg_t
    '# axmt510 : SaleOrder Change,SaleForcast Change : Order Detail
    ''' <remarks> Order Detail </remarks>
    Public Shared tblSaleChangeOrder As String = "xmeg_t"
    Public Shared ent As String = "xmegent"
    Public Shared Site As String = "xmegsite"
    Public Shared DocNo As String = "xmegdocno"
    Public Shared Version As String = "xmeg900"
    Public Shared ItemSequence As String = "xmegseq"
    Public Shared ItemNo As String = "xmeg001"
    Public Shared ProductFeature As String = "xmeg002"
    Public Shared PackagingContainer As String = "xmeg003"
    Public Shared OperationNo As String = "xmeg004"
    Public Shared Process As String = "xmeg005"
    Public Shared SalesUnit As String = "xmeg006"
    Public Shared SalesQty As String = "xmeg007"
    Public Shared ReferenceUnit As String = "xmeg008"
    Public Shared ReferenceQty As String = "xmeg009"
    Public Shared PricingUnit As String = "xmeg010"
    Public Shared PricingQty As String = "xmeg011"
    Public Shared AppointedDeliveryDate As String = "xmeg012"
    Public Shared ExpectedReceiptDate As String = "xmeg013"
    Public Shared UnitPrice As String = "xmeg015"
    Public Shared TaxType As String = "xmeg016"
    Public Shared TaxRate As String = "xmeg017"
    Public Shared SubItemFeature As String = "xmeg019"
    Public Shared UrgentItem As String = "xmeg020"
    Public Shared Bond As String = "xmeg021"
    Public Shared PartialDelivery As String = "xmeg022"
    Public Shared ShippingLocation As String = "xmegunit"
    Public Shared CollectionLocation As String = "xmegorga"
    Public Shared ShipTo As String = "xmeg023"
    Public Shared MultiDeliveryPeriod As String = "xmeg024"
    Public Shared ReceiptAddressNo As String = "xmeg025"
    Public Shared BillAddressNo As String = "xmeg026"
    Public Shared CustomerItemNo As String = "xmeg027"
    Public Shared RestrictedWarehouseLocation As String = "xmeg028"
    Public Shared RestrictedStorageLocation As String = "xmeg029"
    Public Shared RestrictedBatchNumber As String = "xmeg030"
    Public Shared MeansOfTransportation As String = "xmeg031"
    Public Shared PickingMethod As String = "xmeg032"
    Public Shared RateOfSpares As String = "xmeg033"
    Public Shared OverShippingRatio As String = "xmeg034"
    Public Shared PriceDetermination As String = "xmeg035"
    Public Shared ProjectNo As String = "xmeg036"
    Public Shared WBSno As String = "xmeg037"
    Public Shared ActivityNo As String = "xmeg038"
    Public Shared CauseOfSpending As String = "xmeg039"
    Public Shared PricingSource As String = "xmeg040"
    Public Shared PriceReferenceNo As String = "xmeg041"
    Public Shared PriceReferenceItems As String = "xmeg042"
    Public Shared AccessPrices As String = "xmeg043"
    Public Shared PercentSpread As String = "xmeg044"
    Public Shared RowStatus As String = "xmeg045"
    Public Shared AmtExclTax As String = "xmeg046"
    Public Shared AmtInclTax As String = "xmeg047"
    Public Shared Tax As String = "xmeg048"
    Public Shared ReasonCode As String = "xmeg049"
    Public Shared Notes As String = "xmeg050" '--PO SO Chg.
    Public Shared CustomerOrderItemNo As String = "xmeg051"
    Public Shared Inspection As String = "xmeg052"
    Public Shared SettlementReasonCode As String = "xmeg053"
    Public Shared ChangeSN As String = "xmeg900" '--Version SO Chg.
    Public Shared ChangedType As String = "xmeg901"
    Public Shared ReasonOfChange As String = "xmeg902" '--เช่น 11E :Due Date Change
    Public Shared ChangeNotes As String = "xmeg903" '--หมายเหตุที่แก้ไข
    Public Shared BOMeffectiveDate As String = "xmeg054"
    Public Shared SourceDocNo As String = "xmeg055"
    Public Shared SourceItemSeq As String = "xmeg056"
    Public Shared InventoryManagmentFeature As String = "xmeg057"
    Public Shared ReturnVolume As String = "xmeg058"
    Public Shared QuantityRepaymentReferenceQty As String = "xmeg059"
    Public Shared PriceRepaymentQty As String = "xmeg060"
    Public Shared PriceRepaymentReferenceQty As String = "xmeg061"
    Public Shared BOMFeatures As String = "xmeg062"

    ''' <remarks>Where Starndrad </remarks>
    Public Shared wStandard As String = ent & "='3' and " & Site & "='JINPAO'"

    '--Page SaleOrderChangeStus
    '--Shearch SaleOrderChaange Tap Report / Rrfesh DataTable 
    Private Shared SelectSOChangeLine As String = "select " & DocNo & "," & ChangeSN & "," & ItemSequence & "," & ItemNo & "," & SalesUnit & "," & SalesQty & "," & ReasonOfChange & "," & Notes & "," &
        " case " & RowStatus & " when '1' then 'General' when '2' then 'Normal Settlement' when '3' then 'Long Settlement' else '-' end as RosStus," &
        " " & AppointedDeliveryDate & "," & ChangeNotes & " from " & tblSaleChangeOrder & " " &
        " where " & wStandard & " and  " & DocNo & " ='@OrderNo' and " & ChangeSN & " ='@ChangeSN'"
    Public Shared Function GetSOChgHead(ByVal OrderNo As String, ByVal ChgVer As String)
        Dim Oral As String = SelectSOChangeLine
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@OrderNo", OrderNo)
        Oral = Oral.Replace("@ChangeSN", ChgVer)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function













    '######### where DocNo =?  and ItemN =? #########################
    Private Shared strSaleChangeOrder As String = "Select * from " & tblSaleChangeOrder & " " &
       " where " & wStandard & " AND " & DocNo & " =@pDocNo and " & ItemNo & " =@pItemNo order by " & DocNo & "," & Version & "," & ItemSequence & " asc "
    Public Shared Function getSaleChangeOrder(strDocNo As String, strItemNo As String) As DataTable
        Dim Sql As String = strSaleChangeOrder
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrder", "Sql = strSaleChangeOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeOrderDataSet(strDocNo As String, strItemNo As String) As DataSet
        Dim Sql As String = strSaleChangeOrder
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrderDataSet", "Sql = strSaleChangeOrder", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where DocNo =? #########################
    Private Shared strSaleChangeOrderByDocNo As String = "Select * from " & tblSaleChangeOrder & " " &
       " where " & wStandard & " AND " & DocNo & " =@pDocNo  order by " & DocNo & "," & Version & "," & ItemSequence & " asc "
    Public Shared Function getSaleChangeOrderByDocNo(strDocNo As String) As DataTable
        Dim Sql As String = strSaleChangeOrderByDocNo
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrderByDocNo", "Sql = strSaleChangeOrderByDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeOrderDataSetByDocNo(strDocNo As String) As DataSet
        Dim Sql As String = strSaleChangeOrderByDocNo
        Sql = Sql.Replace("@pDocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrderDataSetByDocNo", "Sql = strSaleChangeOrderByDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '######### where ItemN =? #########################
    Private Shared strSaleChangeOrdeByItemNo As String = "Select * from " & tblSaleChangeOrder & " " &
       " where " & wStandard & " AND " & ItemNo & " =@pItemNo  order by " & DocNo & "," & Version & "," & ItemSequence & " asc "
    Public Shared Function getSaleChangeOrderByItemNo(strItemNo As String) As DataTable
        Dim Sql As String = strSaleChangeOrdeByItemNo
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrderByItemNo", "Sql = strSaleChangeOrdeByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleChangeOrderDataSetByItemNo(strItemNo As String) As DataSet
        Dim Sql As String = strSaleChangeOrdeByItemNo
        Sql = Sql.Replace("@pItemNo", "'" & strItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMEG", "getSaleChangeOrderDataSetByItemNo", "Sql = strSaleChangeOrdeByItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


End Class
