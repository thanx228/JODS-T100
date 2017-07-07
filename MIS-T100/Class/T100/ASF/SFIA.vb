Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFIA
    '# Module T100 : ASF
    Private Shared ASF As String = "ASF"
    '# Table sfia_t
    '# Transfer Return for Rework : Header
    '# Tansection Code asft338
    '# Example select TOP 100 = "select * from sfia_t  where rownum <= 100 "
    '''<reamrks>### Structure Table : Transfer Return for Rework : Header ##############</reamrks>
    Public Shared tblTransferReworkHead As String = "sfia_t"
    '''<reamrks> # Field </reamrks>
    Public Shared DocNo As String = "sfiadocno"
    Public Shared ent As String = "sfiaent"
    Public Shared Site As String = "sfiasite"
    Public Shared DocumentDate As String = "sfiadocdt"
    Public Shared Applicant As String = "sfia001"  'BY EmpID
    Public Shared Dept As String = "sfia002"
    Public Shared Status As String = "sfiastus"
    Public Shared RunCard As String = "sfia004"
    '''<reamrks> Basic Data </reamrks>
    Public Shared WONo As String = "sfia003"
    Public Shared Version As String = "sfia003"
    Public Shared ProductionItem As String = "sfia010"
    Public Shared TransferOutOpSerailNo As String = "sfia005"
    Public Shared TransferOutOpOrder As String = "sfia006"
    Public Shared ReworkTransferOutQty As String = "sfia007"
    Public Shared TransferinRuncard As String = "sfia008"
    Public Shared Memo As String = "sfia009"
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "sfiaownid"
    Public Shared DataOwnerDept As String = "sfiaowndp"
    Public Shared DataCreateBy As String = "sfiacrtid"
    Public Shared DataCreateByDept As String = "sfiacrtdp"
    Public Shared DataCreateDate As String = "sfiacrtdt"
    Public Shared DataModifyBy As String = "sfiamodid"
    Public Shared LastModifyDate As String = "sfiamoddt"
    Public Shared DataConfirmationPersonal As String = "sfiacnfid"
    Public Shared DataConfirmedDate As String = "sfiacnfdt"
    '''<reamrks> Condition Where </reamrks>
    Public Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '#################### Get Transfer Return Reworf Where 1  Doc_No. ='?' #################################################
    Private Shared SqlTransferReworkBy_DocNo As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & WONo & "," & Version & "," & ProductionItem & "," & TransferOutOpSerailNo & "," & TransferOutOpOrder & "," & ReworkTransferOutQty & ", " &
    " " & TransferinRuncard & "," & Memo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblTransferReworkHead & "   where " & wStandard & " AND " & DocNo & " =@DocNo "
    Public Shared Function GetTransferReworkWTrsNo(strDocNo As String) As DataTable
        Dim Sql As String = SqlTransferReworkBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkWTrsNo", "Sql = SqlTransferReworkBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferReworkWTrsNo_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlTransferReworkBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkWTrsNo_DataSet", "Sql = SqlTransferReworkBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get Transfer Return Reworf Where 1  MO_No. ='?' #################################################
    Private Shared SqlTransferReworkBy_MO_No As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & WONo & "," & Version & "," & ProductionItem & "," & TransferOutOpSerailNo & "," & TransferOutOpOrder & "," & ReworkTransferOutQty & ", " &
    " " & TransferinRuncard & "," & Memo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblTransferReworkHead & "   where " & wStandard & " AND " & WONo & " =@MO_No "
    Public Shared Function GetTransferReworkWMO_No(strMO_No As String) As DataTable
        Dim Sql As String = SqlTransferReworkBy_MO_No.Replace("@MO_No", "'" & strMO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkWMO_No", "Sql = SqlTransferReworkBy_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferReworkWMO_No_DataSet(strMO_No As String) As DataSet
        Dim Sql As String = SqlTransferReworkBy_MO_No.Replace("@MO_No", "'" & strMO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkWMO_No_DataSet", "Sql = SqlTransferReworkBy_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '#################### Get Transfer Return Reworf Where   DocumentDate. BETWEEN  StarDate? To EndDate #################################################
    Private Shared SqlTransferReworkBy_DocDateBetween As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & WONo & "," & Version & "," & ProductionItem & "," & TransferOutOpSerailNo & "," & TransferOutOpOrder & "," & ReworkTransferOutQty & ", " &
    " " & TransferinRuncard & "," & Memo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblTransferReworkHead & "   where " & wStandard & " " &
    "  And " & DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetTransferReworkDocDateBetween(sDate As String, Edate As String) As DataTable
        Dim Sql As String = SqlTransferReworkBy_DocDateBetween
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
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkDocDateBetween", "Sql = SqlTransferReworkBy_DocDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferReworkDocDateBetween_DataSet(sDate As String, Edate As String) As DataSet
        Dim Sql As String = SqlTransferReworkBy_DocDateBetween
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
            GetPageError.GetClassT100(ASF, "SFIA", "GetTransferReworkDocDateBetween_DataSet", "Sql = SqlTransferReworkBy_DocDateBetween", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '#################### Get Transfer Return Status #################################################
    Private Shared SqlTransferReworkStatus As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & WONo & "," & Version & "," & ProductionItem & "," & TransferOutOpSerailNo & "," & TransferOutOpOrder & "," & ReworkTransferOutQty & ", " &
    " " & TransferinRuncard & "," & Memo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblTransferReworkHead & "   where " & wStandard & " AND " & Status & " =@status "
    Public Shared Function GetTransferRework_Status_Dataset(strstatus As String, Optional ByVal wc As String = "") As DataSet
        Dim Sql As String = SqlTransferReworkStatus.Replace("@status", "'" & strstatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
