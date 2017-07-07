Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class QCBA
    '# Module : AQC
    Private Shared AQC As String = "AQC"
    '# Table : qcba_t
    '# aqct300 : Maintain QC Inspection Record : Header
    Public Shared tbl_IQC_inSpectionItem As String = "qcba_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "qcbaent"
    Public Shared Site As String = "qcbasite"
    Public Shared DocNo As String = "qcbadocno"
    Public Shared DocumentDate As String = "qcbadocdt"
    Public Shared Status As String = "qcbastus"
    Public Shared Testtype As String = "qcba000"
    Public Shared SourceDocNo As String = "qcba001"
    Public Shared SourceLineNo As String = "qcba002"
    Public Shared Reference As String = "qcba003"
    Public Shared ReferenceDocLineNo As String = "qcba004"
    Public Shared TransactionPartyID As String = "qcba005"
    Public Shared OperationNo As String = "qcba006"
    Public Shared ProcessingOrder As String = "qcba007"
    Public Shared SourceQty As String = "qcba008"
    Public Shared SourceUnit As String = "qcba009"
    Public Shared ItemNo As String = "qcba010"
    Public Shared Version As String = "qcba011"
    Public Shared ProductCharacteristics As String = "qcba012"
    Public Shared QualityControlGrouping As String = "qcba013"
    Public Shared InspectionDate As String = "qcba014"
    Public Shared InspectionTime As String = "qcba015"
    Public Shared InspectionUnit As String = "qcba016"
    Public Shared InspectionQty As String = "qcba017"
    Public Shared InspectionDegree As String = "qcba018"
    Public Shared TestSeries As String = "qcba019"
    Public Shared ApprovalNo As String = "qcba020"
    Public Shared Urgency As String = "qcba021"
    Public Shared InspectionResult As String = "qcba022"
    Public Shared QtyPassed As String = "qcba023"
    Public Shared Inspector As String = "qcba024"
    Public Shared TestProjectIdCode As String = "qcba030"
    Public Shared DocumentIssuanceStaff As String = "qcba900"
    Public Shared DocumentIssuanceDepartment As String = "qcba901"
    Public Shared CompletionTestTime As String = "qcba025"
    Public Shared InspectionTime2 As String = "qcba026"
    Public Shared QtyDefected As String = "qcba027"
    Public Shared CompletionTestDate As String = "qcba028"
    Public Shared StyleClassification As String = "qcba031"
    Public Shared RunCard As String = "qcba029"
    Public Shared QualityAbnormalityApplicationFormNo As String = "qcba032"
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "qcbaownid"
    Public Shared DepartmentOfData As String = "qcbaowndp"
    Public Shared DataCreatedBy As String = "qcbacrtid"
    Public Shared DateCreatedByDept As String = "qcbacrtdp"
    Public Shared DataCreatedDate As String = "qcbacrtdt"
    Public Shared ModifiedBy As String = "qcbamodid"
    Public Shared LastModifiedDate As String = "qcbamoddt"
    Public Shared DataConfirmationPersonnel As String = "qcbacnfid"
    Public Shared DataConfirmationDate As String = "qcbacnfdt"
    Public Shared DataPoster As String = "qcbapstid"
    Public Shared DataPostedDate As String = "qcbapstdt"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared SUMSourceQty As String = "SUM_Source_Qty"
    Public Shared SUMInspectionQty As String = "SUM_Inspection_Qty"
    Public Shared SUMQtyPassed As String = "SUM_Qty_Passed"
    Public Shared SUMQtyDefected As String = "SUM_Qty_Defected"

    '####### IQC inspection where DocNo. = ? ####################################################################
    Private Shared strIQC_InSpection_DocNo As String = "Select " & DocNo & " ," & DocumentDate & "," & Status & ", " & Testtype & ", " &
      " " & SourceDocNo & " ," & SourceLineNo & "," & Reference & "," & ReferenceDocLineNo & "," & TransactionPartyID & ", " &
      " " & OperationNo & "," & ProcessingOrder & "," & SourceQty & "," & SourceUnit & "," & ItemNo & "," & Version & ", " &
      " " & ProductCharacteristics & "," & QualityControlGrouping & "," & InspectionDate & "," & InspectionTime & "," & InspectionUnit & ", " &
      " " & InspectionQty & "," & InspectionDegree & "," & TestSeries & "," & ApprovalNo & "," & Urgency & "," & InspectionResult & ", " &
      " " & QtyPassed & "," & Inspector & "," & TestProjectIdCode & "," & DocumentIssuanceStaff & "," & DocumentIssuanceDepartment & ", " &
      " " & CompletionTestTime & "," & InspectionTime2 & "," & QtyDefected & "," & CompletionTestDate & "," & StyleClassification & ", " &
      " " & RunCard & "," & QualityAbnormalityApplicationFormNo & "," & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & ", " &
      " " & DateCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & ", " &
      " " & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & "  " &
      " FROM " & QCBA.tbl_IQC_inSpectionItem & "  " &
      " where " & wStandard & "  AND " & DocNo & " =@pDocNo "
    Public Shared Function GetIQC_inSpectionHeader(strInSpectionDocNo As String) As DataTable
        Dim Sql As String = strIQC_InSpection_DocNo.Replace("@pDocNo", "'" & strInSpectionDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_inSpectionHeader", "Sql = strIQC_InSpection_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetIQC_inSpectionHeader_DateSet(strInSpectionDocNo As String) As DataSet
        Dim Sql As String = strIQC_InSpection_DocNo.Replace("@pDocNo", "'" & strInSpectionDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_inSpectionHeader_DateSet", "Sql = strIQC_InSpection_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '####### IQC inspection where ItemNo. = ? ####################################################################
    Private Shared strIQC_InSpection_ItemNo As String = "Select " & DocNo & " ," & DocumentDate & "," & Status & ", " & Testtype & ", " &
      " " & SourceDocNo & " ," & SourceLineNo & "," & Reference & "," & ReferenceDocLineNo & "," & TransactionPartyID & ", " &
      " " & OperationNo & "," & ProcessingOrder & "," & SourceQty & "," & SourceUnit & "," & ItemNo & "," & Version & ", " &
      " " & ProductCharacteristics & "," & QualityControlGrouping & "," & InspectionDate & "," & InspectionTime & "," & InspectionUnit & ", " &
      " " & InspectionQty & "," & InspectionDegree & "," & TestSeries & "," & ApprovalNo & "," & Urgency & "," & InspectionResult & ", " &
      " " & QtyPassed & "," & Inspector & "," & TestProjectIdCode & "," & DocumentIssuanceStaff & "," & DocumentIssuanceDepartment & ", " &
      " " & CompletionTestTime & "," & InspectionTime2 & "," & QtyDefected & "," & CompletionTestDate & "," & StyleClassification & ", " &
      " " & RunCard & "," & QualityAbnormalityApplicationFormNo & "," & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & ", " &
      " " & DateCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & ", " &
      " " & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & "  " &
      " FROM " & QCBA.tbl_IQC_inSpectionItem & "  " &
     " where " & wStandard & "  AND " & ItemNo & " =@pItemNo "
    Public Shared Function GetIQC_ItemNo_inSpectionHeader(strInSpectionItemNo As String) As DataTable
        Dim Sql As String = strIQC_InSpection_ItemNo.Replace("@pItemNo", "'" & strInSpectionItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_ItemNo_inSpectionHeader", "Sql = strIQC_InSpection_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetIQC_ItemNo_inSpectionHeader_DateSet(strInSpectionItemNo As String) As DataSet
        Dim Sql As String = strIQC_InSpection_ItemNo.Replace("@pItemNo", "'" & strInSpectionItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_ItemNo_inSpectionHeader_DateSet", "Sql = strIQC_InSpection_ItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### IQC inspection where DocumentDate BETWEEN StartDate. = ?  and ToDate = ? #################################################
    Private Shared strIQC_InSpection_DocumentDate_BETWEEN As String = "Select " & DocNo & " ," & DocumentDate & "," & Status & ", " & Testtype & ", " &
      " " & SourceDocNo & " ," & SourceLineNo & "," & Reference & "," & ReferenceDocLineNo & "," & TransactionPartyID & ", " &
      " " & OperationNo & "," & ProcessingOrder & "," & SourceQty & "," & SourceUnit & "," & ItemNo & "," & Version & ", " &
      " " & ProductCharacteristics & "," & QualityControlGrouping & "," & InspectionDate & "," & InspectionTime & "," & InspectionUnit & ", " &
      " " & InspectionQty & "," & InspectionDegree & "," & TestSeries & "," & ApprovalNo & "," & Urgency & "," & InspectionResult & ", " &
      " " & QtyPassed & "," & Inspector & "," & TestProjectIdCode & "," & DocumentIssuanceStaff & "," & DocumentIssuanceDepartment & ", " &
      " " & CompletionTestTime & "," & InspectionTime2 & "," & QtyDefected & "," & CompletionTestDate & "," & StyleClassification & ", " &
      " " & RunCard & "," & QualityAbnormalityApplicationFormNo & "," & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & ", " &
      " " & DateCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & ", " &
      " " & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & "  " &
      " FROM " & QCBA.tbl_IQC_inSpectionItem & "  " &
     " where " & wStandard & "  AND " & DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetIQC_DocumentDateBETWEEN_inSpectionHeader(sDate As String, eDate As String) As DataTable
        Dim Sql As String = strIQC_InSpection_DocumentDate_BETWEEN
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & eDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_DocumentDateBETWEEN_inSpectionHeader", "Sql = strIQC_InSpection_DocumentDate_BETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetIQC_DocumentDateBETWEEN_inSpectionHeader_DateSet(sDate As String, eDate As String) As DataSet
        Dim Sql As String = strIQC_InSpection_DocumentDate_BETWEEN
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & eDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_DocumentDateBETWEEN_inSpectionHeader_DateSet", "Sql = strIQC_InSpection_DocumentDate_BETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '  For Not Approve  Eddy ''
    '###################### where QC Header Status ##############################################
    Private Shared strQCHeader_Status As String = "Select " & DocNo & " ," & DocumentDate & "," & Testtype & ", " & SourceDocNo & ", " &
        " " & SourceLineNo & " ," & Reference & "," & ReferenceDocLineNo & "," & TransactionPartyID & "," & Status & "," & OperationNo & ", " &
        " " & ProcessingOrder & "," & SourceQty & "," & SourceUnit & "," & ItemNo & "," & QualityControlGrouping & ", " &
        " " & InspectionDate & "," & InspectionTime & "," & InspectionUnit & "," & InspectionQty & "," & InspectionDegree & "," & TestSeries & ", " &
        " " & ApprovalNo & "," & Urgency & "," & InspectionResult & "," & QtyPassed & "," & Inspector & "" &
        " FROM " & tbl_IQC_inSpectionItem & "  " &
        " where " & wStandard & " AND " & Status & " =@pStatus "
    Public Shared Function getQCHeader_Status(strStatus As String) As DataTable
        Dim Sql As String = strQCHeader_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "getQCHeader_Status", "Sql = strQCHeader_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function getQCHeader_Status_Dataset(strStatus As String) As DataSet
        Dim Sql As String = strQCHeader_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "getQCHeader_Status_Dataset", "Sql = strQCHeader_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### IQC inspection Summary Qty #################################################
    Private Shared strIQC_InSpection_ItemNo_SUMqty As String = "Select sum(" & SourceQty & ") as " & SUMSourceQty & " ,sum(" & InspectionQty & ") as " & SUMInspectionQty & ", " &
     " sum(" & QtyPassed & ") as " & SUMQtyPassed & ",sum(" & QtyDefected & ") as " & SUMQtyDefected & "  " &
     " FROM " & QCBA.tbl_IQC_inSpectionItem & "  " &
     " where " & wStandard & "  AND " & ItemNo & " =@pItemNo "
    Public Shared Function GetIQC_SUMqty_inSpectionHeader(strInSpectionItemNo As String) As DataTable
        Dim Sql As String = strIQC_InSpection_ItemNo_SUMqty.Replace("@pItemNo", "'" & strInSpectionItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AQC, "QCBA", "GetIQC_SUMqty_inSpectionHeader", "Sql = strIQC_InSpection_ItemNo_SUMqty", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

