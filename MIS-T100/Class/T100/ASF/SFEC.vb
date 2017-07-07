Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFEC
    '# Module ASF
    Private Shared ASF As String = "ASF"
    '# Table sfec_t
    '# asft340 : MO-Receipt : Body Tab Stock-in-Detail
    '# Example CommndLine = "select * from sfeb_t  where rownum <= 100 "
    '''<reamrks>### Structure Table : MO-Receipt  Body Tab Stock-in-Detail ##############</reamrks>
    Public Shared tblMOreceiptB_StockInDetail As String = "sfec_t"
    '''<reamrks> # Field </reamrks>
    Public Shared DocNo As String = "sfecdocno"
    Public Shared ent As String = "sfecent"
    Public Shared Site As String = "sfecsite"
    Public Shared Line_No As String = "sfecseq"
    Public Shared ItemOrder As String = "sfecseq1"
    Public Shared WONo As String = "sfec001"
    Public Shared Runcard As String = "sfec021"
    Public Shared FQCNo As String = "sfec002"
    Public Shared Determinat As String = "sfec003"
    Public Shared ItemNo As String = "sfec005"
    Public Shared Specifiation As String = "sfec008"
    Public Shared Quantity As String = "sfec009"
    Public Shared WH As String = "sfec012"
    Public Shared Location As String = "sfec013"
    Public Shared Lot_No As String = "sfec014"
    Public Shared InventoryManagCharater As String = "sfec015"
    Public Shared ProjectNo As String = "sfec022"
    Public Shared WBS As String = "sfec023"
    Public Shared ActivityNo As String = "sfec024"
    Public Shared ManufacturingDate As String = "sfec028"
    Public Shared ValidDate As String = "sfec016"
    Public Shared InventoryNote As String = "sfec017"

    '''<reamrks> Condition Where </reamrks>
    Private Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '#################### Get  Where 1  Doc_No. ='?' ##############################################################################################
    Private Shared SqlMOreceitpDocNo As String = "Select " & Line_No & "," & DocNo & "," & WONo & "," & Runcard & "," & FQCNo & "," & ItemNo & ", " &
    " " & Determinat & "," & Specifiation & "," & Quantity & "," & WH & "," & Location & "," & Lot_No & "," & InventoryManagCharater & ", " &
    " " & ProjectNo & "," & WBS & "," & ActivityNo & "," & ManufacturingDate & "," & ValidDate & "," & InventoryNote & " " &
    "  FROM " & tblMOreceiptB_StockInDetail & "   where " & wStandard & " AND " & DocNo & " =@DocNo "
    Public Shared Function GetMOreceitpB_StockInDetailDocNo(strDocNo As String) As DataTable
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEC", "GetMOreceitpB_StockInDetailDocNo", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpB_StockInDeatilDocNo_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEC", "GetMOreceitpB_StockInDeatilDocNo_DataSet", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  WO_No. ='?' #################################################################################################
    Private Shared SqlMOreceitp_WONo As String = "Select " & Line_No & "," & DocNo & "," & WONo & "," & Runcard & "," & FQCNo & "," & ItemNo & ", " &
    " " & Determinat & "," & Specifiation & "," & Quantity & "," & WH & "," & Location & "," & Lot_No & "," & InventoryManagCharater & ", " &
    " " & ProjectNo & "," & WBS & "," & ActivityNo & "," & ManufacturingDate & "," & ValidDate & "," & InventoryNote & " " &
    "  FROM " & tblMOreceiptB_StockInDetail & "   where " & wStandard & " AND " & WONo & " =@pWO_No "
    Public Shared Function GetMOreceitpB_StockInDetailWO_No(strWO_No As String) As DataTable
        Dim Sql As String = SqlMOreceitp_WONo.Replace("@pWO_No", "'" & strWO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEC", "GetMOreceitpB_StockInDetailWO_No", "Sql = SqlMOreceitp_WONo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpB_StockInDetailWO_No_DataSet(strWO_No As String) As DataSet
        Dim Sql As String = SqlMOreceitp_WONo.Replace("@pWO_No", "'" & strWO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEC", "GetMOreceitpB_StockInDetailWO_No_DataSet", "Sql = SqlMOreceitp_WONo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
