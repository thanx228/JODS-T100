Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECBA
    '# Module : AEC
    '# Table : ecba_t
    '# aecm200 : Item Routing : Header
    Private Shared AEC As String = "AEC"
    '''<reamrks>## Table Item Routing : Header  ##############</reamrks>
    Public Shared tblItemRoutingHeader As String = "ecba_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ProcessPartNo As String = "ecba001"
    ' Public Shared ItemName As String = "" >> Join : imaal_t
    ' Public Shared Specification As String = "" >> Join  : imaal_t
    Public Shared RoutingCode As String = "ecba002"
    Public Shared Description As String = "ecba003"
    Public Shared Status As String = "ecbastus"
    ' Public Shared Memo As String =  >> Jion Table ooeb_t field ooeb003
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "ecbaownid"
    Public Shared DataOwnerDept As String = "ecbaowndp"
    Public Shared DataCreateBy As String = "ecbacrtid"
    Public Shared DataCreateByDept As String = "ecbacrtdp"
    Public Shared DataCreateDate As String = "ecbacrtdt"
    Public Shared DataModifyBy As String = "ecbamodid"
    Public Shared LastModifyDate As String = "ecbamoddt"
    Public Shared DataConfirmationPersonal As String = "ecbacnfid"
    Public Shared DateConfrimDate As String = "ecbacnfdt"
    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "ecbaent"
    Private Shared Site As String = "ecbasite"
    Public Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "

    '''<remarks> Get ItemRoutin Header where ProcessPartNo. ='?' </remarks>  
    Private Shared StrItemRoutingHeaderItemNo As String = "Select " & ProcessPartNo & "," & RoutingCode & "," & Description & "," & Status & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DateConfrimDate & "  " &
    " FROM " & tblItemRoutingHeader & " where " & ProcessPartNo & " =@ProcessPartNo and " & wStandard & " "
    Public Shared Function GetItemRoutingHeaderItemNo(ItemRotingNo As String) As DataTable
        Dim strSQL As String = StrItemRoutingHeaderItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBA", "GetItemRoutingHeaderItemNo", "Sql = StrItemRoutingHeaderItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetItemRoutingHeaderItemNo_DataSet(ItemRotingNo As String) As DataSet
        Dim strSQL As String = StrItemRoutingHeaderItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBA", "GetItemRoutingHeaderItemNo_DataSet", "Sql = StrItemRoutingHeaderItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '''<remarks> Get ItemRoutin Header where Create by BETWEEN Date. ='?' </remarks>  
    Private Shared StrItemRoutingHeaderCreatebyEmpID As String = "Select " & ProcessPartNo & "," & RoutingCode & "," & Description & "," & Status & ", " &
    " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    " " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DateConfrimDate & "  " &
    " FROM " & tblItemRoutingHeader & " where " & wStandard & " " &
    " And " & DataCreateDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetItemRoutingHeaderItemNoCreateBetweenDate(Sdate As String, Edate As String) As DataTable
        Dim strSQL As String = StrItemRoutingHeaderCreatebyEmpID
        strSQL = strSQL.Replace("@Sdate", "'" & Sdate & "'")
        strSQL = strSQL.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBA", "GetItemRoutingHeaderItemNoCreateBetweenDate", "Sql = StrItemRoutingHeaderCreatebyEmpID", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetItemRoutingHeaderItemNoCreateBetweenDate_DataSet(Sdate As String, Edate As String) As DataSet
        Dim strSQL As String = StrItemRoutingHeaderCreatebyEmpID
        strSQL = strSQL.Replace("@Sdate", "'" & Sdate & "'")
        strSQL = strSQL.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBA", "GetItemRoutingHeaderItemNoCreateBetweenDate_DataSet", "Sql = StrItemRoutingHeaderCreatebyEmpID", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

