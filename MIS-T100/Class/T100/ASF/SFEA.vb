Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFEA
    '# Module ASF
    Private Shared ASF As String = "ASF"
    '# Table sfea_t
    '# asft340 : MO-Receipt : Header
    '# Example CommndLine = "select * from sfea_t  where rownum <= 100 "
    '''<reamrks>### Structure Table : MO-Receipt  Header ##############</reamrks>
    Public Shared tblMOreceiptHead As String = "sfea_t"
    '''<reamrks> # Field </reamrks>
    Public Shared DocNo As String = "sfeadocno"
    Public Shared ent As String = "sfeaent"
    Public Shared Site As String = "sfeasite"
    Public Shared DocumentDate As String = "sfeadocdt"
    Public Shared PostedDate As String = "sfea001"
    Public Shared Applicant As String = "sfea002"
    Public Shared Dept As String = "sfea002"
    Public Shared Status As String = "sfeastus"
    Public Shared PBINo As String = "sfea004"
    Public Shared RecoiledMatPickingNo As String = "sfea005 "
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "sfeaownid"
    Public Shared DataOwnerDept As String = "sfeaowndp"
    Public Shared DataCreateBy As String = "sfeacrtid"
    Public Shared DataCreateByDept As String = "sfeacrtdp"
    Public Shared DataCreateDate As String = "sfeacrtdt"
    Public Shared DataModifyBy As String = "sfeamodid"
    Public Shared LastModifyDate As String = "sfeamoddt"
    Public Shared DataConfirmationPersonal As String = "sfeacnfid"
    Public Shared DataConfirmedDate As String = "sfeacnfdt"
    Public Shared DataPoster As String = "sfeapstid"
    Public Shared PostingDate As String = "sfeapstdt"

    '''<reamrks> Condition Where </reamrks>
    Private Shared W_Standard As String = Site & " = 'JINPAO' AND " & ent & "='3' "
    Private Shared W_DocNo As String = "sfeadocno"
    Private Shared W_DocumentDate As String = "sfeadocdt"
    Private Shared W_PostedDate As String = "sfea001"
    Private Shared W_Applicant As String = "sfea002"
    Private Shared W_Dept As String = "sfea002"
    Private Shared W_Status As String = "sfeastus"


    '#################### Get  Where 1  Doc_No. ='?' #################################################
    Private Shared SqlMOreceitpDocNo As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & PostedDate & "," & PBINo & "," & RecoiledMatPickingNo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & ", " &
    " " & DataPoster & "," & PostingDate & "   " &
    "  FROM " & tblMOreceiptHead & "   where " & W_Standard & " AND " & W_DocNo & " =@DocNo "
    Public Shared Function GetMOreceitpDocNo(strDocNo As String) As DataTable
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpDocNo", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpDocNo_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlMOreceitpDocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpDocNo_DataSet", "Sql = SqlMOreceitpDocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  Document Date BETWEEN. ='?' #################################################
    Private Shared SqlMOreceitpDocDateBETWEEN As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & PostedDate & "," & PBINo & "," & RecoiledMatPickingNo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & ", " &
    " " & DataPoster & "," & PostingDate & "   " &
    "  FROM " & tblMOreceiptHead & " " &
    " where " & W_Standard & " And " & W_DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetMOreceitpDocDateBETWEEN(Sdate As String, Edate As String) As DataTable
        Dim Sql As String = SqlMOreceitpDocDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpDocDateBETWEEN", "Sql = SqlMOreceitpDocDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpDocDateBETWEEN_DataSet(Sdate As String, Edate As String) As DataSet
        Dim Sql As String = SqlMOreceitpDocDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpDocDateBETWEEN_DataSet", "Sql = SqlMOreceitpDocDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  Psoted Date BETWEEN. ='?' #################################################
    Private Shared SqlMOreceitpPostedDateBETWEEN As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & PostedDate & "," & PBINo & "," & RecoiledMatPickingNo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & ", " &
    " " & DataPoster & "," & PostingDate & "   " &
    "  FROM " & tblMOreceiptHead & " " &
    " where " & W_Standard & " And " & W_PostedDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetMOreceitpPostedDateBETWEEN(Sdate As String, Edate As String) As DataTable
        Dim Sql As String = SqlMOreceitpPostedDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpPostedDateBETWEEN", "Sql = SqlMOreceitpPostedDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpPostedDateBETWEEN_DataSet(Sdate As String, Edate As String) As DataSet
        Dim Sql As String = SqlMOreceitpPostedDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpPostedDateBETWEEN_DataSet", "Sql = SqlMOreceitpPostedDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  Status. ='?' #################################################
    Private Shared SqlMOreceitpStatus As String = "Select " & DocNo & "," & DocumentDate & "," & Applicant & "," & Dept & "," & Status & ", " &
    " " & PostedDate & "," & PBINo & "," & RecoiledMatPickingNo & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & ", " &
    " " & DataPoster & "," & PostingDate & "   " &
    "  FROM " & tblMOreceiptHead & "   where " & W_Standard & " AND " & W_Status & " =@pStatus "
    Public Shared Function GetMOreceitpStatus(strStatus As String) As DataTable
        Dim Sql As String = SqlMOreceitpStatus.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpStatus", "Sql = SqlMOreceitpStatus", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMOreceitpStatus_DataSet(strStatus As String) As DataSet
        Dim Sql As String = SqlMOreceitpStatus.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFEA", "GetMOreceitpStatus_DataSet", "Sql = SqlMOreceitpStatus", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

