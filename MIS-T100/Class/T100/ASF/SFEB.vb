Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFEB
    '# Module ASF
    Private Shared ASF As String = "ASF"
    '# Table sfeb_t
    '# asft340 : MO-Receipt : Body Tab Stock-in-requisition
    '# Example CommndLine = "select * from sfeb_t  where rownum <= 100 "
    '''<reamrks>### Structure Table : MO-Receipt  Body Tab Stock-in-requisition ##############</reamrks>
    Public Shared tblMOreceiptB_StockInRequisition As String = "sfeb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared DocNo As String = "sfebdocno"
    Public Shared ent As String = "sfebent"
    Public Shared Site As String = "sfebsite"
    Public Shared LineNo As String = "sfebseq"
    Public Shared WONo As String = "sfeb001"
    Public Shared Runcard As String = "sfeb026"
    Public Shared FQC As String = "sfeb002"
    Public Shared ItemNo As String = "sfeb004"
    Public Shared Unit As String = "sfeb007"
    Public Shared AppliedQty As String = "sfeb008"
    Public Shared TestPassQty As String = "sfeb027"
    Public Shared ActualQty As String = "sfeb009"
    Public Shared SpecifiesWH As String = "sfeb013"
    Public Shared SpecifiesLocation As String = "sfeb014"
    Public Shared AssignLotNo As String = "sfeb015"
    Public Shared InventoryManag As String = "sfeb016"
    Public Shared ProjectNo As String = "sfeb017"
    Public Shared WBS As String = "sfeb018"
    Public Shared ActivityNo As String = "sfeb019"
    Public Shared ReasonCode As String = "sfeb020"
    Public Shared ManufacturingDate As String = "sfeb028"
    Public Shared InventoryExpiry As String = "sfeb021"
    Public Shared InventoryNote As String = "sfeb022"

    '''<reamrks> Condition Where </reamrks>
    Private Shared W_Standard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '#################### Get  Where 1  Doc_No. ='?' #################################################
    Private Shared SqlMOreceitpDocNo As String = "Select " & LineNo & "," & DocNo & "," & WONo & "," & Runcard & "," & FQC & "," & ItemNo & ", " &
    " " & Unit & "," & AppliedQty & "," & TestPassQty & "," & ActualQty & "," & SpecifiesWH & "," & SpecifiesLocation & ", " &
    " " & AssignLotNo & "," & InventoryManag & "," & ProjectNo & "," & WBS & "," & ActivityNo & "," & ReasonCode & ", " &
    " " & ReasonCode & "," & ManufacturingDate & "," & InventoryExpiry & "," & InventoryNote & " " &
    "  FROM " & tblMOreceiptB_StockInRequisition & "   where " & W_Standard & " AND " & DocNo & " =@DocNo "
    Public Shared Function GetMOreceitpB_StockInReqDocNo(strDocNo As String) As DataTable
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEB", "GetMOreceitpB_StockInReqDocNo", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpB_StockInReqDocNo_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEB", "GetMOreceitpB_StockInReqDocNo_DataSet", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  WO_No. ='?' #################################################
    Private Shared SqlMOreceitp_WONo As String = "Select " & LineNo & "," & DocNo & "," & WONo & "," & Runcard & "," & FQC & "," & ItemNo & ", " &
    " " & Unit & "," & AppliedQty & "," & TestPassQty & "," & ActualQty & "," & SpecifiesWH & "," & SpecifiesLocation & ", " &
    " " & AssignLotNo & "," & InventoryManag & "," & ProjectNo & "," & WBS & "," & ActivityNo & "," & ReasonCode & ", " &
    " " & ReasonCode & "," & ManufacturingDate & "," & InventoryExpiry & "," & InventoryNote & " " &
    "  FROM " & tblMOreceiptB_StockInRequisition & "   where " & W_Standard & " AND " & WONo & " =@pWO_No "
    Public Shared Function GetMOreceitpB_StockInReqWO_No(strWO_No As String) As DataTable
        Dim Sql As String = SqlMOreceitp_WONo.Replace("@pWO_No", "'" & strWO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEB", "GetMOreceitpB_StockInReqWO_No", "Sql = SqlMOreceitp_WONo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpB_StockInReqWO_No_DataSet(strWO_No As String) As DataSet
        Dim Sql As String = SqlMOreceitp_WONo.Replace("@pWO_No", "'" & strWO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEB", "GetMOreceitpB_StockInReqWO_No_DataSet", "Sql = SqlMOreceitp_WONo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    'add by noi on 2017-06-16
    'function list use==>FGLabel.aspx.vb at line XXX
    Public Shared Function getMoReceiptData_Process_MO_Item(fldName As ArrayList, WHR As String) As DataTable
        Dim conn_sql = New ConnSQL
        Dim VarIni As New VarIni
        Dim SQL As String
        'sfea 
        SQL = VarIni.S & conn_sql.getFeild(fldName) & VarIni.F & tblMOreceiptB_StockInRequisition

        SQL &= VarIni.getLeftjoinFirst(VarIni.SFEA, VarIni.SFEB, True, SFEA.DocNo & ":" & DocNo)

        SQL &= VarIni.getLeftjoinFirst(VarIni.SFAA, VarIni.SFEB, True, SFAA.DocNo & ":" & WONo)
        'apmt500 head
        SQL &= VarIni.getLeftjoinFirst(VarIni.XMDA, VarIni.SFAA, True, XMDA.SaleOrderNo & ":" & SFAA.OldRefereanceDocNo)
        'axmm200 customer
        SQL &= VarIni.getLeftjoinFirst(VarIni.PMAAL, VarIni.XMDA, False, PMAAL.ContactID & ":" & XMDA.CustomerId & "," & PMAAL.Langauge & ":" & VarIni.enUS_V & ":")

        SQL &= VarIni.getLeftjoinFirst(VarIni.IMAA, VarIni.SFEB, False, IMAA.ItemNo & ":" & ItemNo)

        SQL &= VarIni.getLeftjoinFirst(VarIni.IMAAL, VarIni.SFEB, False, IMAAL.ProductItem & ":" & ItemNo & "," & IMAAL.Langauge & ":" & VarIni.enUS_V & ":")

        SQL &= WHR & VarIni.getOrderBy(DocNo & VarIni.C & SFEB.LineNo)

        Return GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe())
    End Function

End Class

