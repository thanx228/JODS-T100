Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDA
    '# Module T100 : ASF
    '# Table : sfda_t
    '# Material Issue Over Issue : Header
    Private Shared ASF As String = "ASF"
    '''<reamrks>### Table
    ''' 1.Material Issue Header ##############
    ''' 2.Material Return fro Rework Header : Rework DocNo= JP205-xxxxxxxxx ############## </reamrks>
    Public Shared tblMatIssueHead As String = "sfda_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> Basic Data </reamrks>
    Public Shared IssueDocNo As String = "sfdadocno"
    Public Shared DocumentDate As String = "sfdadocdt"
    Public Shared PostingDate As String = "sfda001"
    Public Shared ent As String = "sfdaent"
    Public Shared Site As String = "sfdasite"
    Public Shared IssueType As String = "sfda002"
    Public Shared SourceType As String = "sfda015"
    Public Shared SourceDocNo As String = "sfda014"
    Public Shared Status As String = "sfdastus"
    Public Shared ProductionDept As String = "sfda003"
    Public Shared Applicant As String = "sfda004" ' Epm_Id Issue Material
    Public Shared PBINo As String = "sfda005"

    Public Shared ProductionItem As String = "sfda006"
    Public Shared BOMFeatures As String = "sfda007"
    Public Shared ProductCharacteristics As String = "sfda008"
    Public Shared ManufacturingControlGroup As String = "sfda009"
    Public Shared OperationNo As String = "sfda010"
    Public Shared OperationSequence As String = "sfda011"
    Public Shared WH As String = "sfda012"
    Public Shared Sets As String = "sfda013"

    '''<reamrks> Adjustment Information </reamrks>
    Public Shared DataOwner As String = "sfdaownid"
    Public Shared DataOwnerDept As String = "sfdaowndp"
    Public Shared DataCreateBy As String = "sfdacrtid"
    Public Shared DataCreateByDept As String = "sfdacrtdp"
    Public Shared DataCreateDate As String = "sfdacrtdt"
    Public Shared DataModifiedBy As String = "sfdamodid"
    Public Shared LastModifiedDate As String = "sfdamoddt"
    Public Shared DataConfrimationPersonal As String = "sfdacnfid"
    Public Shared DataConfrimedDate As String = "sfdacnfdt"
    Public Shared DataPosted As String = "sfdapstid"
    Public Shared DataPostedDate As String = "sfdapstdt"

    '''<reamrks> Condition Where Filed </reamrks>
    Public Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '##############################  where Issue_DocNo. ="" ###############################################
    Private Shared strqlMatIssueHead_DocNO As String = "Select " & IssueDocNo & "," & DocumentDate & "," & PostingDate & ", " &
    " " & IssueType & "," & SourceType & "," & SourceDocNo & "," & Status & "," & ProductionDept & "," & Applicant & "," & PBINo & ", " &
    " " & ProductionItem & "," & BOMFeatures & "," & ProductCharacteristics & "," & ManufacturingControlGroup & "," &
    " " & OperationNo & "," & OperationSequence & "," & WH & "," & Sets & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifiedBy & "," & LastModifiedDate & "," & DataConfrimationPersonal & ", " &
    " " & DataConfrimedDate & "," & DataPosted & "," & DataPostedDate & " " &
    " from " & tblMatIssueHead & " where " & wStandard & " AND  " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssueHead_DocNO(strIssueDoc_No As String) As DataTable
        Dim Sql As String = strqlMatIssueHead_DocNO.Replace("@IssueDocNo", "'" & strIssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHead_DocNO", "Sql = strqlMatIssueHead_DocNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueHeadDocNO_DataSet(strIssueDoc_No As String) As DataSet
        Dim Sql As String = strqlMatIssueHead_DocNO.Replace("@IssueDocNo", "'" & strIssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadDocNO_DataSet", "Sql = strqlMatIssueHead_DocNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '##############################  where Issue_Status. ="" ###############################################
    Private Shared strqlMatIssueHead_Status As String = "Select " & IssueDocNo & "," & DocumentDate & "," & PostingDate & ", " &
    " " & IssueType & "," & SourceType & "," & SourceDocNo & "," & Status & "," & ProductionDept & "," & Applicant & "," & PBINo & ", " &
    " " & ProductionItem & "," & BOMFeatures & "," & ProductCharacteristics & "," & ManufacturingControlGroup & "," &
    " " & OperationNo & "," & OperationSequence & "," & WH & "," & Sets & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifiedBy & "," & LastModifiedDate & "," & DataConfrimationPersonal & ", " &
    " " & DataConfrimedDate & "," & DataPosted & "," & DataPostedDate & " " &
    " from " & tblMatIssueHead & " where " & wStandard & " AND " & Status & "=@IssueStatusNo "
    Public Shared Function GetMatIssueHead_Status(strIssueStatus As String) As DataTable
        Dim Sql As String = strqlMatIssueHead_Status.Replace("@IssueStatusNo", "'" & strIssueStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            'GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHead_Status", "Sql = strqlMatIssueHead_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Public Shared Function GetMatIssueHeadStatus_DataSet(strIssueStatus As String) As DataSet
        Dim Sql As String = strqlMatIssueHead_Status.Replace("@IssueStatusNo", "'" & strIssueStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadStatus_DataSet", "Sql = strqlMatIssueHead_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '##############################  Where DocumentDate. BETWEEN  StarDate? To EndDate ###############################################
    Private Shared strqlMatIssueHead_DocDate As String = "Select " & IssueDocNo & "," & DocumentDate & "," & PostingDate & ", " &
    " " & IssueType & "," & SourceType & "," & SourceDocNo & "," & Status & "," & ProductionDept & "," & Applicant & "," & PBINo & ", " &
    " " & ProductionItem & "," & BOMFeatures & "," & ProductCharacteristics & "," & ManufacturingControlGroup & "," &
    " " & OperationNo & "," & OperationSequence & "," & WH & "," & Sets & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifiedBy & "," & LastModifiedDate & "," & DataConfrimationPersonal & ", " &
    " " & DataConfrimedDate & "," & DataPosted & "," & DataPostedDate & " " &
    " from " & tblMatIssueHead & " where " & wStandard & " AND " & DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetMatIssueHeadDocDate(sDate As String, Edate As String) As DataTable
        Dim Sql As String = strqlMatIssueHead_DocDate
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
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadDocDate", "Sql = strqlMatIssueHead_DocDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueHeadDocDate_DataSet(sDate As String, Edate As String) As DataSet
        Dim Sql As String = strqlMatIssueHead_DocDate
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
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadDocDate_DataSet", "Sql = strqlMatIssueHead_DocDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '##############################  Where POsted Date. BETWEEN  StarDate? To EndDate ###############################################
    Private Shared strqlMatIssueHead_PstedDate As String = "Select " & IssueDocNo & "," & DocumentDate & "," & PostingDate & ", " &
    " " & IssueType & "," & SourceType & "," & SourceDocNo & "," & Status & "," & ProductionDept & "," & Applicant & "," & PBINo & ", " &
    " " & ProductionItem & "," & BOMFeatures & "," & ProductCharacteristics & "," & ManufacturingControlGroup & "," &
    " " & OperationNo & "," & OperationSequence & "," & WH & "," & Sets & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & ", " &
    " " & DataCreateDate & "," & DataModifiedBy & "," & LastModifiedDate & "," & DataConfrimationPersonal & ", " &
    " " & DataConfrimedDate & "," & DataPosted & "," & DataPostedDate & " " &
    " from " & tblMatIssueHead & " where " & wStandard & " AND " & PostingDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetMatIssueHeadPostDate(sDate As String, Edate As String) As DataTable
        Dim Sql As String = strqlMatIssueHead_PstedDate
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
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadPostDate", "Sql = strqlMatIssueHead_PstedDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueHeadPostDate_DataSet(sDate As String, Edate As String) As DataSet
        Dim Sql As String = strqlMatIssueHead_PstedDate
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
            GetPageError.GetClassT100(ASF, "SFDA", "GetMatIssueHeadPostDate_DataSet", "Sql = strqlMatIssueHead_PstedDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class

