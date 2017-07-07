Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDU
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdt_t
    '''# apmt520 :Purchaes PR Receipt : Body Item :Multi Store Location Detail
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PR Receipt : Body Item : Multi Store Location Detail ##############</reamrks>
    Public Shared tblPR_MultiStoreLotcationBody As String = "pmdu_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmduent"
    Public Shared Site As String = "pmdusite"
    Public Shared DocNo As String = "pmdudocno"
    Public Shared LineNo As String = "pmduseq"
    Public Shared ItemOrder As String = "pmduseq1"
    Public Shared ItemNo As String = "pmdu001"
    Public Shared ProductCharacteristics As String = "pmdu002"
    Public Shared OperationNo As String = "pmdu003"
    Public Shared OperationSequence As String = "pmdu004"
    Public Shared InventoryManagementCharacteristics As String = "pmdu005"
    Public Shared WH As String = "pmdu006"
    Public Shared Location As String = "pmdu007"
    Public Shared LotNo As String = "pmdu008"
    Public Shared Unit As String = "pmdu009"
    Public Shared Quantity As String = "pmdu010"
    Public Shared ReferenceUnit As String = "pmdu011"
    Public Shared ReferenceQty As String = "pmdu012"
    Public Shared AcceptableQty As String = "pmdu013"
    Public Shared StockInQty As String = "pmdu014"
    Public Shared WithdrawalQuantity As String = "pmdu015"
    Public Shared StockNote As String = "pmdu016"
    Public Shared ValidDate As String = "pmdu017"
    Public Shared PackingUnit As String = "pmdu200"
    Public Shared PackingQty As String = "pmdu201"
    Public Shared ManufacturingDate As String = "pmdu202"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  PR_Receipt By DocNo = ?  Purchaes PR Receipt : Body Item : Multi Store Location Detail ####
    Private Shared strWH_PR_Receipt_DocNo As String = "Select " & DocNo & " ," & LineNo & "," & ItemOrder & ", " & ItemNo & "," &
        " " & ProductCharacteristics & "," & OperationNo & ", " & OperationSequence & " ," & InventoryManagementCharacteristics & ", " &
        " " & WH & ", " & Location & "," & LotNo & "," & Unit & "," & Quantity & "," & ReferenceUnit & ", " &
        " " & ReferenceQty & "," & AcceptableQty & "," & StockInQty & "," & WithdrawalQuantity & "," & StockNote & "," & ValidDate & ", " &
        " " & PackingUnit & "," & PackingQty & "," & ManufacturingDate & " " &
        " FROM " & PMDU.tblPR_MultiStoreLotcationBody & "  " &
        " where " & wStandard & " And  " & DocNo & " = @pPR_Receipt_DocNo  "
    Public Shared Function GetPR_Receipt_By_DocNo_BodyReceiptDetail(strPR_Receipt_DocNo As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDU", "GetPR_Receipt_By_DocNo_BodyReceiptDetail", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_DocNo_BodyReceiptDetailDataSet(strPR_Receipt_DocNo As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDU", "GetPR_Receipt_By_DocNo_BodyReceiptDetailDataSet", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  PR_Receipt By Item_No = ?  Purchaes PR Receipt : Body Item : Multi Store Location Detail ####
    Private Shared strWH_PR_Receipt_ItemNo As String = "Select " & DocNo & " ," & LineNo & "," & ItemOrder & ", " & ItemNo & "," &
        " " & ProductCharacteristics & "," & OperationNo & ", " & OperationSequence & " ," & InventoryManagementCharacteristics & ", " &
        " " & WH & ", " & Location & "," & LotNo & "," & Unit & "," & Quantity & "," & ReferenceUnit & ", " &
        " " & ReferenceQty & "," & AcceptableQty & "," & StockInQty & "," & WithdrawalQuantity & "," & StockNote & "," & ValidDate & ", " &
        " " & PackingUnit & "," & PackingQty & "," & ManufacturingDate & " " &
        " FROM " & PMDU.tblPR_MultiStoreLotcationBody & "  " &
        " where " & wStandard & " And  " & ItemNo & " = @pPR_Receipt_ItemNo  "
    Public Shared Function GetPR_Receipt_By_ItemNo_BodyReceiptDetail(strPR_Receipt_DocNo As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_ItemNo
        Sql = Sql.Replace("@pPR_Receipt_ItemNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDU", "GetPR_Receipt_By_ItemNo_BodyReceiptDetail", "Sql = strWH_PR_Receipt_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_ItemNo_BodyReceiptDetailDataSet(strPR_Receipt_DocNo As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_ItemNo
        Sql = Sql.Replace("@pPR_Receipt_ItemNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDU", "GetPR_Receipt_By_ItemNo_BodyReceiptDetailDataSet", "Sql = strWH_PR_Receipt_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
