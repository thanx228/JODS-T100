Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class INAG
    ''' <remarks>
    '''# Module T100 : AIN 
    ''' # Table : inag_t
    ''' Store >> 6).Query  Report : Tansection Code : aing100 (Query Inventories)
    ''' Inventory Details 
    ''' </remarks>
    Private Shared AIN As String = "AIN"
    '''<reamrks>## Table Inventory Deatils  ##############</reamrks>
    Public Shared tblInventoryDeatil As String = "inag_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "inagent"
    Public Shared Site As String = "inagsite"
    Public Shared ItemNo As String = "inag001"
    Public Shared ProductCharacteristics As String = "inag002"
    Public Shared InventoryManagementCharacteristics As String = "inag003"
    Public Shared WH As String = "inag004"
    Public Shared LocationNumber As String = "inag005" '(ERP = Store,Bin)
    Public Shared LotNo As String = "inag006"
    Public Shared InventoryUnit As String = "inag007"
    Public Shared BookInventory As String = "inag008"
    Public Shared ActualStock As String = "inag009"
    Public Shared InventoryAvailable As String = "inag010"
    Public Shared MRPavailable As String = "inag011"
    Public Shared CostWarehouseYN As String = "inag012"
    Public Shared PickupPriority As String = "inag013"
    Public Shared LastCountingDate As String = "inag014"
    Public Shared LastTransactionDate As String = "inag015"
    Public Shared IdleDate As String = "inag016"
    Public Shared FirstStockInDate As String = "inag017"
    Public Shared NoUse As String = "inag018"
    Public Shared RetentionYN As String = "inag019"
    Public Shared HoldReason As String = "inag020"
    Public Shared AllocatedQuantity As String = "inag021"
    Public Shared NoUse2 As String = "inag022"
    Public Shared TagBinaryCode As String = "inag023"
    Public Shared ReferenceUnit As String = "inag024"
    Public Shared ReferenceQty As String = "inag025"
    Public Shared RecentTestDate As String = "inag026"
    Public Shared NextInspectionDate As String = "inag027"
    Public Shared RetentionDate As String = "inag028"
    Public Shared Retainer As String = "inag029"
    Public Shared RetentionDepartment As String = "inag030"
    Public Shared RetentionDocumentNumber As String = "inag031"
    Public Shared BasicUnit As String = "inag032"
    Public Shared xx As String = "inag"
    Public Shared BaseUnitQuantity As String = "inag033"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared STCustomerHub As String = "2301"
    Public Shared STCustomerSupport As String = "2400"
    Public Shared STMRB_NG As String = "2600"
    Public Shared STSCRAP As String = "2700"
    Public Shared STBorrow As String = "2800"

    '####### Inventory where  Item_No = ?   #########################
    Private Shared strInventoryBy_ItemNo As String = "Select " & ItemNo & " ," & ProductCharacteristics & "," & InventoryManagementCharacteristics & ", " & WH & "," &
        " " & LocationNumber & "," & LotNo & ", " & InventoryUnit & " ," & BookInventory & "," & ActualStock & "," &
        " " & InventoryAvailable & ", " & MRPavailable & "," & CostWarehouseYN & "," & PickupPriority & "," & LastCountingDate & ", " &
        " " & LastTransactionDate & "," & IdleDate & "," & FirstStockInDate & "," & NoUse & "," & RetentionYN & ", " &
        " " & HoldReason & "," & AllocatedQuantity & "," & NoUse2 & "," & TagBinaryCode & "," & ReferenceUnit & "," &
        " " & ReferenceQty & "," & RecentTestDate & "," & NextInspectionDate & "," & RetentionDate & "," & Retainer & ", " &
        " " & RetentionDepartment & "," & RetentionDocumentNumber & " " &
        " FROM " & tblInventoryDeatil & "," & BasicUnit & "," & BaseUnitQuantity & "  " &
        " where " & wStandard & " And  " & ItemNo & " = @pItemNo  "
    Public Shared Function GetInventoryBy_ItemNo(InventoryBy_ItemNo As String) As DataTable
        Dim Sql As String = strInventoryBy_ItemNo
        Sql = Sql.Replace("@pItemNo", "'" & InventoryBy_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAG", "GetInventoryBy_ItemNo", "Sql = strInventoryBy_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetInventoryBy_ItemNo_Dataset(InventoryBy_ItemNo As String) As DataSet
        Dim Sql As String = strInventoryBy_ItemNo
        Sql = Sql.Replace("@pItemNo", "'" & InventoryBy_ItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAG", "GetInventoryBy_ItemNo_Dataset", "Sql = strInventoryBy_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### Inventory where  Warehouse_No = ?   #########################
    Private Shared strInventoryBy_Warehouse_No As String = "Select " & ItemNo & " ," & ProductCharacteristics & "," & InventoryManagementCharacteristics & ", " & WH & "," &
        " " & LocationNumber & "," & LotNo & ", " & InventoryUnit & " ," & BookInventory & "," & ActualStock & "," &
        " " & InventoryAvailable & ", " & MRPavailable & "," & CostWarehouseYN & "," & PickupPriority & "," & LastCountingDate & ", " &
        " " & LastTransactionDate & "," & IdleDate & "," & FirstStockInDate & "," & NoUse & "," & RetentionYN & ", " &
        " " & HoldReason & "," & AllocatedQuantity & "," & NoUse2 & "," & TagBinaryCode & "," & ReferenceUnit & "," &
        " " & ReferenceQty & "," & RecentTestDate & "," & NextInspectionDate & "," & RetentionDate & "," & Retainer & ", " &
        " " & RetentionDepartment & "," & RetentionDocumentNumber & " " &
        " FROM " & tblInventoryDeatil & "," & BasicUnit & "," & BaseUnitQuantity & "  " &
        " where " & wStandard & " And  " & WH & " = @pWH_No  "
    Public Shared Function GetInventoryBy_WarehouseNo(InventoryBy_WarehouseNo As String) As DataTable
        Dim Sql As String = strInventoryBy_Warehouse_No
        Sql = Sql.Replace("@pWH_No", "'" & InventoryBy_WarehouseNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAG", "GetInventoryBy_WarehouseNo", "Sql = strInventoryBy_Warehouse_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetInventoryBy_WarehouseNo_Dataset(InventoryBy_WarehouseNo As String) As DataSet
        Dim Sql As String = strInventoryBy_Warehouse_No
        Sql = Sql.Replace("@pWH_No", "'" & InventoryBy_WarehouseNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAG", "GetInventoryBy_WarehouseNo_Dataset", "Sql = strInventoryBy_Warehouse_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
