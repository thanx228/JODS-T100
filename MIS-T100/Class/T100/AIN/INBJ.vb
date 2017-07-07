Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class INBJ
    ''' <remarks>
    '''# Module T100 : AIN 
    ''' # Table : inbi_t
    ''' </remarks>
    Private Shared AIN As String = "AIN"
    '''<reamrks>## Table  ##############</reamrks>
    Public Shared tblScarpDestoryBody As String = "inbj_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "inbjent"
    Public Shared Site As String = "inbjsite"
    Public Shared DocNo As String = "inbjdocno"
    Public Shared LineNo As String = "inbjseq"
    Public Shared ItemNo As String = "inbj001"
    Public Shared ProductCharacteristics As String = "inbj002"
    Public Shared InventoryManagementCharacteristics As String = "inbj003"
    Public Shared PackagingContainerNo As String = "inbj004"
    Public Shared TransferOutWH As String = "inbj005"
    Public Shared TransferOutLocation As String = "inbj006"
    Public Shared LotNo As String = "inbj007"
    Public Shared InventoryUnit As String = "inbj008"
    Public Shared AppliedQty As String = "inbj009"
    Public Shared ActualTransactionQty As String = "inbj010"
    Public Shared ReferenceUnit As String = "inbj011"
    Public Shared ReferenceUnitRequestQty As String = "inbj012"
    Public Shared ReferenceUnitActualQty As String = "inbj013"
    Public Shared IncomingWasteStore As String = "inbj014"
    Public Shared IncomingWasteLocation As String = "inbj015"
    Public Shared ScrapReason As String = "inbj016"
    Public Shared Department As String = "inbj017"
    Public Shared DocumentNo As String = "inbj018"
    Public Shared Memo As String = "inbj031"
    Public Shared ProjectNo As String = "inbj019"
    Public Shared WBS As String = "inbj020"
    Public Shared ActivityNo As String = "inbj021"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  Doc_No = ?  Body #########################
    Private Shared strWH_Doc_No As String = "Select " & DocNo & " ," & LineNo & "," & ItemNo & ", " & ProductCharacteristics & "," &
        " " & InventoryManagementCharacteristics & "," & PackagingContainerNo & ", " & TransferOutWH & " ," & TransferOutLocation & "," &
        " " & LotNo & ", " & InventoryUnit & "," & AppliedQty & "," & ActualTransactionQty & "," & ReferenceUnit & ", " &
        " " & ReferenceUnitRequestQty & "," & ReferenceUnitActualQty & "," & IncomingWasteStore & "," & IncomingWasteLocation & "," &
        " " & ScrapReason & "," & Department & "," & DocumentNo & "," & Memo & "," & ProjectNo & "," & WBS & "," & ActivityNo & "  " &
        " FROM " & INBJ.tblScarpDestoryBody & "  " &
        " where " & wStandard & " And  " & DocNo & " = @pDoc_No  "
    Public Shared Function GetBody_Scarp_Destory_DocNo(strDoc_No As String) As DataTable
        Dim Sql As String = strWH_Doc_No
        Sql = Sql.Replace("@pDoc_No", "'" & strDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetBody_Scarp_Destory_DocNo", "Sql = strWH_Doc_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetBody_Scarp_Destory_DocNo_DataSet(strDoc_No As String) As DataSet
        Dim Sql As String = strWH_Doc_No
        Sql = Sql.Replace("@pDoc_No", "'" & strDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetBody_Scarp_Destory_DocNo_DataSet", "Sql = strWH_Doc_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  Item_No = ?  Body #########################
    Private Shared strWH_Item_No As String = "Select " & DocNo & " ," & LineNo & "," & ItemNo & ", " & ProductCharacteristics & "," &
        " " & InventoryManagementCharacteristics & "," & PackagingContainerNo & ", " & TransferOutWH & " ," & TransferOutLocation & "," &
        " " & LotNo & ", " & InventoryUnit & "," & AppliedQty & "," & ActualTransactionQty & "," & ReferenceUnit & ", " &
        " " & ReferenceUnitRequestQty & "," & ReferenceUnitActualQty & "," & IncomingWasteStore & "," & IncomingWasteLocation & "," &
        " " & ScrapReason & "," & Department & "," & DocumentNo & "," & Memo & "," & ProjectNo & "," & WBS & "," & ActivityNo & "  " &
        " FROM " & INBJ.tblScarpDestoryBody & "  " &
        " where " & WStandard & " And  " & ItemNo & " = @pItem_No  "
    Public Shared Function GetBody_Scarp_Destory_ItemNo(strItem_No As String) As DataTable
        Dim Sql As String = strWH_Item_No
        Sql = Sql.Replace("@pItem_No", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetBody_Scarp_Destory_ItemNo", "Sql = strWH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetBody_Scarp_Destory_ITemNo_DataSet(strItem_No As String) As DataSet
        Dim Sql As String = strWH_Item_No
        Sql = Sql.Replace("@pItem_No", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetBody_Scarp_Destory_ITemNo_DataSet", "Sql = strWH_Item_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####################  EddyGet Joined DATA FROM OTHER TABLE #################################################
    Private Shared SqlGetRecorddata_Condition As String = "SELECT " & Site & "," & DocNo & "," & LineNo & "," &
    " " & ItemNo & "," & InventoryManagementCharacteristics & "," & TransferOutWH & "," & TransferOutLocation & "," & LotNo & "," & InventoryUnit & "," & AppliedQty & "," & ActualTransactionQty & "," &
    " " & IncomingWasteStore & "," & ScrapReason & "," & Department & "," & DocumentNo & "," & ProjectNo & "," &
    " " & WBS & "," & ActivityNo & "," & Memo & "," & IMAAL.ProductItem & "," & IMAAL.Specifaction & "," & INBI.DocNo & "," & INBI.EntryDate & "," & INBI.Status &
    " FROM " & INBJ.tblScarpDestoryBody & " LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & ItemNo & "=" & IMAAL.ProductItem & "" &
    " LEFT JOIN " & INBI.tblScarpDestoryHeader & " ON " & DocNo & "=" & INBI.DocNo & " WHERE " & ent & "='3' AND " & IMAAL.ent & "='3' AND " & INBI.ent & "=3"

    Public Shared Function GetScrapDetail() As DataTable
        'Dim Sql As String = SqlGetRecorddata_Condition.Replace("@DocNo", "'" & strDocNo & "'")
        Dim Sql As String = SqlGetRecorddata_Condition
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetScrapDetail", "Sql = SqlGetRecorddata_Condition", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Public Shared Function GetScrapDetail_DataSet() As DataSet
        'Dim Sql As String = SqlGetRecorddata_Condition.Replace("@DocNo", "'" & strDocNo & "'")
        Dim Sql As String = SqlGetRecorddata_Condition
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INBJ", "GetScrapDetail_DataSet", "Sql = SqlGetRecorddata_Condition", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
