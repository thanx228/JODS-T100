Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class INBI
    ''' <remarks>
    '''# Module T100 : AIN 
    ''' # Table : inbi_t
    ''' </remarks>
    Private Shared AIN As String = "AIN"
    '''<reamrks>## Table  ##############</reamrks>
    Public Shared tblScarpDestoryHeader As String = "inbi_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "inbient"
    Public Shared Site As String = "inbisite"
    Public Shared DocNo As String = "inbidocno"
    Public Shared EntryDate As String = "inbidocdt"
    Public Shared DocType As String = "inbi000"
    Public Shared Applicant As String = "inbi001"
    Public Shared AppliedDepartment As String = "inbi002"
    Public Shared Status As String = "inbistus"
    Public Shared ScrapReason As String = "inbi003"
    Public Shared Inspection As String = "inbi004"
    Public Shared InTransitCostWarehouseLocation As String = "inbi005"
    Public Shared InTransitNonCostStore As String = "inbi006"
    Public Shared RequestConfirmationDate As String = "inbi007"
    Public Shared ScrappingConfirmationDate As String = "inbi008"
    Public Shared TransferDoc As String = "inbi009"
    Public Shared Memo As String = "inbi021"
    Public Shared ScrapRequisitionNo As String = "inbi031"
    Public Shared ScrapForInventoryManagement As String = "inbi032"
    Public Shared DataOwner As String = "inbiownid"
    Public Shared DepartmentOfData As String = "inbiowndp"
    Public Shared DataCreatedBy As String = "inbicrtid"
    Public Shared DataCreatedByDept As String = "inbicrtdp"
    Public Shared DataCreatedDate As String = "inbicrtdt"
    Public Shared ModifiedBy As String = "inbimodid"
    Public Shared LastModifiedDate As String = "inbimoddt"
    Public Shared DataPonfirmationPersonnel As String = "inbicnfid"
    Public Shared DataConfirmationDate As String = "inbicnfdt"
    Public Shared DataPoster As String = "inbipstid"
    Public Shared DataPostedDate As String = "inbipstdt"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"


    '####### where  Doc_No = ?  Header #########################
    Private Shared strWH_Doc_No As String = "Select " & DocNo & " ," & EntryDate & "," & DocType & ", " & Applicant & "," &
        " " & AppliedDepartment & "," & Status & ", " & ScrapReason & " ," & Inspection & "," & InTransitCostWarehouseLocation & "," &
        " " & InTransitNonCostStore & ", " & RequestConfirmationDate & "," & ScrappingConfirmationDate & "," & TransferDoc & "," & Memo & ", " &
        " " & ScrapRequisitionNo & "," & ScrapForInventoryManagement & "," & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & ", " &
        " " & DataCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataPonfirmationPersonnel & "," &
        " " & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & " " &
        " FROM " & INBI.tblScarpDestoryHeader & "  " &
        " where " & wStandard & " And  " & DocNo & " = @pDoc_No  "
    Public Shared Function GetHeader_Scarp_Destory_DocNo(strDoc_No As String) As DataTable
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
            GetPageError.GetClassT100(AIN, "INBI", "GetHeader_Scarp_Destory_DocNo", "Sql = strWH_Doc_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetHeader_Scarp_Destory_DocNo_DataSet(strDoc_No As String) As DataSet
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
            GetPageError.GetClassT100(AIN, "INBI", "GetHeader_Scarp_Destory_DocNo_DataSet", "Sql = strWH_Doc_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  EntryDate BETWEEN StartDate= ?  ToDate = ? Header #########################
    Private Shared strWH_EntryDate_BETWEEN As String = "Select " & DocNo & " ," & EntryDate & "," & DocType & ", " & Applicant & "," &
        " " & AppliedDepartment & "," & Status & ", " & ScrapReason & " ," & Inspection & "," & InTransitCostWarehouseLocation & "," &
        " " & InTransitNonCostStore & ", " & RequestConfirmationDate & "," & ScrappingConfirmationDate & "," & TransferDoc & "," & Memo & ", " &
        " " & ScrapRequisitionNo & "," & ScrapForInventoryManagement & "," & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & ", " &
        " " & DataCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataPonfirmationPersonnel & "," &
        " " & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & " " &
        " FROM " & INBI.tblScarpDestoryHeader & "  " &
        " where " & wStandard & " And  " & EntryDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetHeader_Scarp_Destory_EntryDate_BETWEEN(sDate As String, eDate As String) As DataTable
        Dim Sql As String = strWH_EntryDate_BETWEEN
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
            GetPageError.GetClassT100(AIN, "INBI", "GetHeader_Scarp_Destory_EntryDate_BETWEEN", "Sql = strWH_EntryDate_BETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetHeader_Scarp_Destory_EntryDate_BETWEEN_DataSet(sDate As String, eDate As String) As DataSet
        Dim Sql As String = strWH_EntryDate_BETWEEN
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
            GetPageError.GetClassT100(AIN, "INBI", "GetHeader_Scarp_Destory_EntryDate_BETWEEN_DataSet", "Sql = strWH_EntryDate_BETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
