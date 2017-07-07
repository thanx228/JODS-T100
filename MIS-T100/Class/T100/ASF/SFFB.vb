Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFFB
    '# Module T100 : ASF
    Private Shared ASF As String = "ASF"
    '# Table sffb_t
    '# Transfer Order : Header
    '# Example select TOP 100 = "select * from sffb_t  where rownum <= 100 "

    '''<reamrks>### Structure Table Transfer Order Header ##############</reamrks>
    Public Shared tblTransferHead As String = "sffb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared DocNo As String = "sffbdocno"
    Public Shared DocumentDate As String = "sffbdocdt"
    Public Shared ent As String = "sffbent"
    Public Shared Site As String = "sffbsite"
    Public Shared version As String = "sffbseq"
    Public Shared ReportingType As String = "sffb001"
    Public Shared DailyReportBy As String = "sffb002"
    Public Shared Dept As String = "sffb003"
    Public Shared Status As String = "sffbstus"
    '''<reamrks> Basic Data </reamrks>
    Public Shared WONo As String = "sffb005"
    Public Shared RunCard As String = "sffb006"
    Public Shared OperationNo As String = "sffb007"
    Public Shared OperationSequence As String = "sffb008"
    Public Shared WorkReportItemNo As String = "sffb029"
    Public Shared Workstation As String = "sffb009"
    Public Shared Costcenter As String = "sffb030"
    Public Shared WorkReportingClass As String = "sffb004 "
    Public Shared WorkReportingMachine As String = "sffb010"
    Public Shared WorkReportingCategory As String = "sffb024"
    Public Shared NumberOfOperations As String = "sffb011"
    Public Shared CompleteDate As String = "sffb012"
    Public Shared CompleteTime As String = "sffb013"
    Public Shared LaborHours As String = "sffb014"
    Public Shared MachineHours As String = "sffb015"
    Public Shared Unit As String = "sffb016"
    Public Shared NoOfGoodItem As String = "sffb017"
    Public Shared ScarpQty As String = "sffb018 "
    Public Shared CurrSuspenedQty As String = "sffb019"
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "sffbownid"
    Public Shared DataOwnerDept As String = "sffbowndp"
    Public Shared DataCreateBy As String = "sffbcrtid"
    Public Shared DataCreateByDept As String = "sffbcrtdp"
    Public Shared DataCreateDate As String = "sffbcrtdt"
    Public Shared DataModifyBy As String = "sffbmodid"
    Public Shared LastModifyDate As String = "sffbmoddt"
    Public Shared DataConfirmationPersonal As String = "sffbcnfid"
    Public Shared DataConfirmedDate As String = "sffbcnfdt"

    '''<reamrks> Condition Where </reamrks>
    Private Shared W_Standard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    Private Shared extention As String = ""


    '#################### Get Transfer Order Where 1 Transfer No. ='?' #################################################
    Private Shared SqlTransferBy_DocNo As String = "Select " & DocNo & "," & DocumentDate & "," & version & "," & ReportingType & ", " &
    " " & DailyReportBy & "," & Dept & "," & Status & "," & WONo & "," & RunCard & "," & OperationNo & "," & OperationSequence & "," & WorkReportItemNo & ", " &
    " " & Workstation & "," & Costcenter & "," & WorkReportingClass & "," & WorkReportingMachine & "," & WorkReportingCategory & ", " &
    " " & NumberOfOperations & "," & CompleteDate & "," & CompleteTime & "," & LaborHours & "," & MachineHours & "," & Unit & "," & NoOfGoodItem & ", " &
    " " & ScarpQty & "," & CurrSuspenedQty & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblTransferHead & "   where " & W_Standard & " AND " & DocNo & " =@DocNo "
    Public Shared Function GetTransferOrderWTrs(strDocNo As String) As DataTable
        Dim Sql As String = SqlTransferBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderWTrs", "Sql = SqlTransferBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferOrderWTrs_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlTransferBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderWTrs_DataSet", "Sql = SqlTransferBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get Transfer Order Where 1 (MO_No)  MO_No. ='?' #################################################
    Private Shared SqlTransferBy_MO_No As String = "Select " & DocNo & "," & DocumentDate & "," & version & "," & ReportingType & ", " &
    " " & DailyReportBy & "," & Dept & "," & Status & "," & WONo & "," & RunCard & "," & OperationNo & "," & OperationSequence & "," & WorkReportItemNo & ", " &
    " " & Workstation & "," & Costcenter & "," & WorkReportingClass & "," & WorkReportingMachine & "," & WorkReportingCategory & ", " &
    " " & NumberOfOperations & "," & CompleteDate & "," & CompleteTime & "," & LaborHours & "," & MachineHours & "," & Unit & "," & NoOfGoodItem & ", " &
    " " & ScarpQty & "," & CurrSuspenedQty & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
 "  FROM " & tblTransferHead & "   where " & W_Standard & " AND " & WONo & " =@pMO_No "
    Public Shared Function GetTransferOrderW_MO(strMO_No As String) As DataTable
        Dim Sql As String = SqlTransferBy_MO_No.Replace("@pMO_No", "'" & strMO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_MO", "Sql = SqlTransferBy_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferOrderW_MO_DataSet(strMO_No As String) As DataSet
        Dim Sql As String = SqlTransferBy_MO_No.Replace("@pMO_No", "'" & strMO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_MO_DataSet", "Sql = SqlTransferBy_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get Transfer Order Where 1 (Status)  Status. ='?' #################################################
    Private Shared SqlTransferBy_Status As String = "Select " & DocNo & "," & DocumentDate & "," & version & "," & ReportingType & ", " &
 " " & DailyReportBy & "," & Dept & "," & Status & "," & WONo & "," & RunCard & "," & OperationNo & "," & OperationSequence & "," & WorkReportItemNo & ", " &
 " " & Workstation & "," & Costcenter & "," & WorkReportingClass & "," & WorkReportingMachine & "," & WorkReportingCategory & ", " &
 " " & NumberOfOperations & "," & CompleteDate & "," & CompleteTime & "," & LaborHours & "," & MachineHours & "," & Unit & "," & NoOfGoodItem & ", " &
 " " & ScarpQty & "," & CurrSuspenedQty & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
 " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
 "  FROM " & tblTransferHead & "   where " & W_Standard & " AND " & Status & " =@pStatus "
    Public Shared Function GetTransferOrderW_Status(strStatus As String) As DataTable
        Dim Sql As String = SqlTransferBy_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_Status", "Sql = SqlTransferBy_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferOrderW_Status_DataSet(strStatus As String) As DataSet
        Dim Sql As String = SqlTransferBy_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_Status_DataSet", "Sql = SqlTransferBy_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '########################### for eddy Not Approved ###########################################################################    
    Public Shared Function GetTransferOrderW_StatusWC_DataSet(strStatus As String, Optional ByVal str As String = "") As DataSet
        Dim Sql As String = SqlTransferBy_Status.Replace("@pStatus", "'" & strStatus & "'")
        Sql = Sql + str
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_StatusWC_DataSet", "Sql = SqlTransferBy_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '#################### Get Transfer Order  >> Where DocumentDate. BETWEEN  StarDate To EndDate #################################################
    Private Shared SqlTransferOrderW_DocDateBetween As String = "Select " & DocNo & "," & DocumentDate & "," & version & "," & ReportingType & ", " &
 " " & DailyReportBy & "," & Dept & "," & Status & "," & WONo & "," & RunCard & "," & OperationNo & "," & OperationSequence & "," & WorkReportItemNo & ", " &
 " " & Workstation & "," & Costcenter & "," & WorkReportingClass & "," & WorkReportingMachine & "," & WorkReportingCategory & ", " &
 " " & NumberOfOperations & "," & CompleteDate & "," & CompleteTime & "," & LaborHours & "," & MachineHours & "," & Unit & "," & NoOfGoodItem & ", " &
 " " & ScarpQty & "," & CurrSuspenedQty & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
 " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
 "  FROM " & tblTransferHead & "   where " & W_Standard & " " &
 " And " & DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetTransferOrderW_DocDateBetween(sDate As String, Edate As String) As DataTable
        Dim Sql As String = SqlTransferOrderW_DocDateBetween
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_DocDateBetween", "Sql = SqlTransferOrderW_DocDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferOrderW_DocDateBetween_DataSet(sDate As String, Edate As String) As DataSet
        Dim Sql As String = SqlTransferOrderW_DocDateBetween
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFFB", "GetTransferOrderW_DocDateBetween_DataSet", "Sql = SqlTransferOrderW_DocDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function



    'add by noi on 2017-06-16
    'function list use==>FGLabel.aspx.vb at line 138
    Public Shared Function getTransferData_Process_MO_Item(fldName As ArrayList, WHR As String) As DataTable
        Dim conn_sql = New ConnSQL
        Dim VarIni As New VarIni
        Dim SQL As String
        Dim LeftJoin As String
        SQL = VarIni.S & conn_sql.getFeild(fldName) & VarIni.F & tblTransferHead

        LeftJoin = SFCB.WONo & ":" & WONo & ","
        LeftJoin &= SFCB.RunCard & ":" & RunCard & ","
        LeftJoin &= SFCB.OperationID & ":" & OperationNo & ","
        LeftJoin &= SFCB.OperationSeq & ":" & OperationSequence
        'asft301 body
        SQL &= VarIni.getLeftjoinFirst(VarIni.SFCB, VarIni.SFFB, True, LeftJoin)
        'asft301-head
        SQL &= VarIni.getLeftjoinFirst(VarIni.SFAA, VarIni.SFCB, True, SFAA.DocNo & ":" & SFCB.WONo)
        'apmt500 head
        SQL &= VarIni.getLeftjoinFirst(VarIni.XMDA, VarIni.SFAA, True, XMDA.SaleOrderNo & ":" & SFAA.OldRefereanceDocNo)
        'axmm200 customer
        SQL &= VarIni.getLeftjoinFirst(VarIni.PMAAL, VarIni.XMDA, False, PMAAL.ContactID & ":" & XMDA.CustomerId & "," & PMAAL.Langauge & ":" & VarIni.enUS_V & ":")
        'item
        SQL &= VarIni.getLeftjoinFirst(VarIni.IMAA, VarIni.SFFB, False, IMAA.ItemNo & ":" & SFAA.ProductItem)
        'item langue
        SQL &= VarIni.getLeftjoinFirst(VarIni.IMAAL, VarIni.IMAA, False, IMAAL.ProductItem & ":" & IMAA.ItemNo & "," & IMAAL.Langauge & ":" & VarIni.enUS_V & ":")
        'where and order by
        SQL &= WHR & VarIni.getOrderBy(DocNo & VarIni.C & SFCB.LineNo)

        Return GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe())
    End Function
End Class
