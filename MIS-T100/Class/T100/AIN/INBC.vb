Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class INBC
    '# Module : ABM
    '# Table : inbc_t
    Private Shared AAP As String = "AAP"
    '# aint302 : Store Inventory Receipt
    '''<reamrks>## Table BOM MasterItemNo : Header  ##############</reamrks>
    Public Shared tblStockInDetail As String = "inbc_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "inbcent"
    Public Shared DocNo As String = "inbcdocno"
    Public Shared LineNo As String = "inbcseq"
    Public Shared ItemOrder As String = "inbcseq1"
    Public Shared ItemNo As String = "inbc001"
    Public Shared ProductCharacteristics As String = "inbc002"
    Public Shared InventoryManagementCharacteristics As String = "inbc003"
    Public Shared PackagingContainerNo As String = "inbc004"
    Public Shared WH As String = "inbc005"
    Public Shared Location As String = "inbc006"
    Public Shared LotNo As String = "inbc007"
    Public Shared TradingUnit As String = "inbc009"
    Public Shared Quantity As String = "inbc010"
    Public Shared ReferenceUnit As String = "inbc011"
    Public Shared ReferenceQty As String = "inbc015"
    Public Shared ValidDate As String = "inbc016"
    Public Shared StockNote As String = "inbc017"
    Public Shared QCdocNo As String = "inbc018"
    Public Shared QCitems As String = "inbc019"
    Public Shared InspectionResult As String = "inbc020"
    Public Shared GoodsBarcode As String = "inbc200"
    Public Shared PackingUnit As String = "inbc201"
    Public Shared PackingQty As String = "inbc202"
    Public Shared ApplicationOrg As String = "inbcunit"
    Public Shared ManufacturingDate As String = "inbc203"
    Public Shared ProjectNo As String = "inbc021"
    Public Shared WBS As String = "inbc022"
    Public Shared ActivityNo As String = "inbc023"
    Public Shared PickUpRejectionPrice As String = "inbc204"
    Public Shared PickUpRejectionAmount As String = "inbc205"
    Public Shared UnitCost As String = "inbc206"
    Public Shared CostAmt As String = "inbc207"
    Public Shared ExpenseDocNo As String = "inbc208"
    Public Shared LineNoOfSourceDoc As String = "inbc209"
    Public Shared SourceReceiptItemSequence As String = "inbc210"
    Public Shared PricingUnit As String = "inbc211"
    Public Shared PricingQty As String = "inbc212"

    '''<reamrks> Condition Field </reamrks>
    Public Shared Site As String = "inbcsite"
    Public Shared WStandard As String = Site & "='JINPAO' and " & ent & "='3' "


    '''<remarks> Get BOM MasterItemNo where MasterItemNo. ='?' </remarks> 
    Private Shared StrBOMItemNo As String = "Select " & DocNo & "," & LineNo & "," & ItemOrder & "," & ItemNo & ", " &
    " " & ProductCharacteristics & "," & InventoryManagementCharacteristics & "," & PackagingContainerNo & "," & WH & "," & Location & ", " &
    " " & LotNo & "," & TradingUnit & "," & Quantity & "," & ReferenceUnit & "," & ReferenceQty & "," & ValidDate & "," & StockNote & ", " &
    " " & QCdocNo & "," & QCitems & "," & InspectionResult & "," & GoodsBarcode & "," & PackingUnit & "," & PackingQty & "," & ApplicationOrg & ",  " &
    " " & ManufacturingDate & "," & ProjectNo & "," & WBS & "," & ActivityNo & "," & PickUpRejectionPrice & "," & PickUpRejectionAmount & ",  " &
     " " & UnitCost & "," & CostAmt & "," & ExpenseDocNo & "," & LineNoOfSourceDoc & "," & SourceReceiptItemSequence & "," & PricingUnit & ",  " &
    " FROM " & tblStockInDetail & " where " & WStandard & " AND " & ItemNo & " =@pBOMItemNo "
    Public Shared Function GetBOMItemNo(strBOMItemNo As String) As DataTable
        Dim strSQL As String = strBOMItemNo
        strSQL = strSQL.Replace("@pBOMItemNo", "'" & strBOMItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AAP, "INBC", "GetBOMItemNo", "Sql = StrBOMItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetBOMItemNo_DataSet(strBOMItemNo As String) As DataSet
        Dim strSQL As String = strBOMItemNo
        strSQL = strSQL.Replace("@pBOMItemNo", "'" & strBOMItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AAP, "INBC", "GetBOMItemNo_DataSet", "Sql = StrBOMItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

