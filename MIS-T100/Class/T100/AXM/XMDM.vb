Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMDM
    '# Module T100 : AXM
    Private Shared AXM As String = "AXM"
    '# Table : xmdm_t
    '# axmt540 : Sale Delivery : Body Tab >> Muliti Store/Location/Lot Shiping Detail
    ''' <remarks> Structure Table Sale Delivery Body Tab >> Muliti Store/Location/Lot Shiping Detail </remarks>
    Public Shared tblSaleDelivery_Body_MulitiStorel As String = "xmdm_t"
    Public Shared ent As String = "xmdment"
    Public Shared Site As String = "xmdmsite"
    Public Shared SaleDeliveryNo As String = "xmdmdocno"
    Public Shared LineNo As String = "xmdmseq"
    Public Shared ItemOrderSeq As String = "xmdmseq1"
    Public Shared ItemNo As String = "xmdm001"
    Public Shared ProductCharacteristics As String = "xmdm002"
    Public Shared OperationNo As String = "xmdm003"
    Public Shared OperationSeq As String = "xmdm004"
    Public Shared RestrictedWarehouseLocation As String = "xmdm005"
    Public Shared RestrictedStorageLocation As String = "xmdm006"
    Public Shared RestrictedBatchNumber As String = "xmdm007"
    Public Shared Unit As String = "xmdm008"
    Public Shared ShippedQty As String = "xmdm009"
    Public Shared ReferenceUnit As String = "xmdm010"
    Public Shared ReferenceQty As String = "xmdm011"
    Public Shared SigningForReceiptQty As String = "xmdm012"
    Public Shared SigningforRejectQty As String = "xmdm013"
    Public Shared SalesReturnQty As String = "xmdm014"
    Public Shared RejectedQty As String = "xmdm031"
    Public Shared CheckOutReferenceQuantity As String = "xmdm032"
    Public Shared InventoryManagementCharacteristics As String = "xmdm033"
    Public Shared AllocatedQty As String = "xmdm034"
    Public Shared PickingVolume As String = "xmdm035"

    ''' <remarks> Condition  </remarks>
    Private Shared W_Standard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '######### for All Filed Sale Delivery Where Sale Delivery Doc_No. = ? : Body Tab : Muliti Store/Location/Lot Shiping Detail #########################
    Private Shared strWH_SaleDeliveryNo As String = "Select " & SaleDeliveryNo & " ," & LineNo & "," & ItemOrderSeq & ", " & ItemNo & ", " &
       " " & ProductCharacteristics & " ," & OperationNo & "," & OperationSeq & "," & RestrictedWarehouseLocation & "," & RestrictedStorageLocation & ", " &
       " " & RestrictedBatchNumber & "," & Unit & "," & ShippedQty & "," & ReferenceUnit & "," & ReferenceQty & ", " &
       " " & SigningForReceiptQty & "," & SigningforRejectQty & "," & SalesReturnQty & "," & RejectedQty & "," & CheckOutReferenceQuantity & "," &
       " " & InventoryManagementCharacteristics & "," & AllocatedQty & "," & PickingVolume & " " &
       " FROM " & tblSaleDelivery_Body_MulitiStorel & "  " &
       " where " & W_Standard & " AND " & SaleDeliveryNo & " =@pSaleDelivery_No "
    Public Shared Function getSaleDelivery_MulitiStoreDetail(strSaleDelivery_No As String) As DataTable
        Dim Sql As String = strWH_SaleDeliveryNo.Replace("@pSaleDelivery_No", "'" & strSaleDelivery_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDM", "getSaleDelivery_MulitiStoreDetail", "Sql = strWH_SaleDeliveryNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getSaleDelivery_MulitiStoreDetail_DataSet(strSaleDelivery_No As String) As DataSet
        Dim Sql As String = strWH_SaleDeliveryNo.Replace("@pSaleDelivery_No", "'" & strSaleDelivery_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDM", "getSaleDelivery_MulitiStoreDetail_DataSet", "Sql = strWH_SaleDeliveryNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
