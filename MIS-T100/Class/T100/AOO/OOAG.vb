Imports System
Imports System.Data.OracleClient
Public Class OOAG
    Private Shared AOO As String = "AOO"

    Public Shared tblUserT100 As String = "ooag_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "ooagent"
    Public Shared Site As String = "ooag004"
    Public Shared Status As String = "ooagstus"
    Public Shared EmployeeNo As String = "ooag001"
    Public Shared ContactTargetID As String = "ooag002"
    Public Shared Department As String = "ooag003"
    Public Shared JobTitle As String = "ooag005"
    Public Shared BankNoPostOfficeNo As String = "ooag006"
    Public Shared AccountNo As String = "ooag007"
    Public Shared Surname As String = "ooag008"
    Public Shared MiddleName As String = "ooag009"
    Public Shared Name As String = "ooag010"
    Public Shared FullName As String = "ooag011"
    Public Shared ReferenceName As String = "ooag012"
    Public Shared Nickname As String = "ooag013"
    Public Shared MnemonicCode As String = "ooag014"
    Public Shared EmployeeDecisionLevels As String = "ooag015"
    Public Shared ActivateAgentMechanism As String = "ooag016"
    Public Shared AgentEmployeeID As String = "ooag017"
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "ooagownid"
    Public Shared DataOwnerDept As String = "ooagowndp"
    Public Shared DataCreatedBy As String = "ooagcrtid"
    Public Shared DataCreatedByDept As String = "ooagcrtdp"
    Public Shared DataCreatedDate As String = "ooagcrtdt"
    Public Shared ModifiedBy As String = "ooagmodid"
    Public Shared LastModifiedDate As String = "ooagmoddt"
    '''<reamrks> Condition Where </reamrks>
    Private Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "
    Public Shared SLSect1 As String = "MKTS1"
    Public Shared SLSect2 As String = "MKTS2"
    Public Shared SLSect3 As String = "MKTS3"
    Public Shared SLSect4 As String = "MKTS4"
    Public Shared SLSect5 As String = "MKTS5"

    '--Page BillInvoice
    '--Show EmployeeID Section Sales
    Private Shared EmployeeIDOral As String = "select " & EmployeeNo & "," & Name & "," & EmployeeNo & " || ' : ' || " & Name & " as EmpID , " & Department & " from " & tblUserT100 & " " &
        " where " & ent & "='3' and " & Department & " in ( '" & SLSect1 & "', '" & SLSect2 & "','" & SLSect3 & "','" & SLSect4 & "','" & SLSect5 & "') order by " & EmployeeNo & " "
    Public Shared Function GetEmployeeIDOralddl() As Data.DataTable
        Dim Oral As String = EmployeeIDOral
        Dim dt As New DataTable
        Dim dtAdapter = New OracleDataAdapter(Oral, clsDBConnect.strT100ConnectionString)
        dtAdapter.Fill(dt)
        Return dt '*** Return DataTable ***'
    End Function

    '--Page BillInvoice
    '--Checking EmployeeID Section Sales
    Private Shared EmployeeIDOral1 As String = "select " & EmployeeNo & "," & Name & "," & Department & " from " & tblUserT100 & " " &
        " where " & ent & "='3' and " & Department & " in ( '" & SLSect1 & "', '" & SLSect2 & "','" & SLSect3 & "','" & SLSect4 & "','" & SLSect5 & "')  and " & EmployeeNo & "='@EmployeeID' order by " & EmployeeNo & " "
    Public Shared Function GetEmployeeIDOral(ByVal EmployeeID As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = EmployeeIDOral1
        Oral = Oral.Replace("@EmployeeID", EmployeeID)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function














































    Private Shared strqlEmployeeNo As String = "Select " & EmployeeNo & "," & ContactTargetID & "," & Department & ", " &
    " " & JobTitle & "," & BankNoPostOfficeNo & "," & AccountNo & "," & Surname & "," & MiddleName & ", " &
    " " & Name & "," & FullName & "," & ReferenceName & "," & Nickname & "," & MnemonicCode & "," & EmployeeDecisionLevels & ", " &
    " " & ActivateAgentMechanism & "," & AgentEmployeeID & "," & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & ", " &
    " " & DataCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & ", " & LastModifiedDate & "  " &
    " from " & tblUserT100 & " where " & wStandard & " AND " & EmployeeNo & "=@pEmpNo "
    Public Shared Function GetEmployeeNo(srtEmpNo As String) As DataTable
        Dim Sql As String = strqlEmployeeNo.Replace("@pEmpNo", "'" & srtEmpNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOAG", "GetEmployeeNo", "Sql = strqlEmployeeNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetEmployeeNo_Dataset(srtEmpNo As String) As DataSet
        Dim Sql As String = strqlEmployeeNo.Replace("@pEmpNo", "'" & srtEmpNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOAG", "GetEmployeeNo_Dataset", "Sql = strqlEmployeeNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strqlEmpDept As String = "Select " & EmployeeNo & "," & ContactTargetID & "," & Department & ", " &
" " & JobTitle & "," & BankNoPostOfficeNo & "," & AccountNo & "," & Surname & "," & MiddleName & ", " &
" " & Name & "," & FullName & "," & ReferenceName & "," & Nickname & "," & MnemonicCode & "," & EmployeeDecisionLevels & ", " &
" " & ActivateAgentMechanism & "," & AgentEmployeeID & "," & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & ", " &
" " & DataCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & ", " & LastModifiedDate & "  " &
" from " & tblUserT100 & " where " & wStandard & " AND " & Department & "=@pDeptNo "
    Public Shared Function GetEmpDepartment(srtDeptNo As String) As DataTable
        Dim Sql As String = strqlEmpDept.Replace("@pDeptNo", "'" & srtDeptNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOAG", "GetEmpDepartment", "Sql = strqlEmpDept", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetEmpDepartment_Dataset(srtDeptNo As String) As DataSet
        Dim Sql As String = strqlEmpDept.Replace("@pDeptNo", "'" & srtDeptNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOAG", "GetEmpDepartment_Dataset", "Sql = strqlEmpDept", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
