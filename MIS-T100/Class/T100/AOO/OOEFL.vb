Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOEFL
    '''<remarks> # Module : AOO
    '''# Table : ooefl_t
    ''' # Department  ERP-T100
    '''</remarks> 
    Private Shared AOO As String = "AOO"
    '''<reamrks>##########Table Deaprtment ##############</reamrks>
    Public Shared tblDepartment As String = "ooefl_t"
    '''<reamrks> # Field </reamrks>

    Public Shared DeptID As String = "ooefl001"
    Public Shared Dept As String = "ooefl003"
    Public Shared OrganizationInformation As String = "ooefl001"
    Public Shared FullInternalName As String = "ooefl004"
    Public Shared ShowDeparment As String = "Department"
    Public Shared Language As String = "ooefl002"
    Public Shared Site As String = "ooefl001"
    Public Shared ent As String = "ooeflent"

    '''<reamrks> Condition Field </reamrks>
    Private Shared wStandard As String = ent & "= '3' and (" & Site & " <>'JINPAO' and " & Site & " <>'SITE-01' and " & Site & " <>'ALL') "
    Public Shared Seles1 As String = "MKTS1"
    Public Shared Seles2 As String = "MKTS2"
    Public Shared Seles3 As String = "MKTS3"
    Public Shared Seles4 As String = "MKTS4"
    Public Shared Seles5 As String = "MKTS5"
    Public Shared US As String = "en_US"


    '--Show Type Sales Order
    Private Shared SalesSectionSL As String = "select " & OrganizationInformation & "," & FullInternalName & "," & OrganizationInformation & " || ' : ' || " & FullInternalName & " as SLSection from " & tblDepartment & " " &
        " where  " & ent & " ='3' and " & Language & " =('" & US & "') and " & OrganizationInformation & " in ('" & Seles1 & "','" & Seles2 & "','" & Seles3 & "','" & Seles4 & "','" & Seles5 & "') order by " & OrganizationInformation & " "
    Public Shared Function ShowSectionSL() As Data.DataTable
        Dim Oral As String = SalesSectionSL
        Dim dt As New DataTable
        Dim dtAdapter = New OracleDataAdapter(Oral, clsDBConnect.strT100ConnectionString)
        dtAdapter.Fill(dt)
        Return dt '*** Return Dataset ***'
    End Function

    '--Page SalesOrderChangeStatus
    '--Show Type Sales Order Rrfesh DataTable
    Private Shared SalesSectionRrfesh As String = "select " & OrganizationInformation & "," & FullInternalName & " from " & tblDepartment & " " &
        " where  " & ent & " ='3' and " & Language & " =('" & US & "') and " & OrganizationInformation & " ='@OrganizationInformation' order by " & OrganizationInformation & " "
    Public Shared Function GetSectionSLRrfesh(ByVal Section As String)
        Dim Oral As String = SalesSectionRrfesh
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@OrganizationInformation", Section)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function






























    '######### Show Department to select or Databind()  ####################################################
    Private Shared strDeptarment As String = "Select " & DeptID & "," & Dept & ", " &
    " " & DeptID & " || ' : ' || " & Dept & " as " & ShowDeparment & "  from " & tblDepartment & " " &
    " where " & wStandard & "  Order By ooefl001 asc"
    '''<remarks>'# Department get DataTable</remarks> 
    Public Shared Function GetDepartment_Table() As DataTable
        Dim Sql As String = strDeptarment
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOEFL", "GetDepartment_Table", "Sql = strDeptarment", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '''<remarks># Department get DataSet</remarks>
    Public Shared Function GetDepartment_DataSet() As DataSet
        Dim Sql As String = strDeptarment
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOEFL", "GetDepartment_DataSet", "Sql = strDeptarment", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '######### Find Serach Department to select or Databind()  ####################################################
    Private Shared strDeptarmentFind As String = "Select " & DeptID & "," & Dept & ", " &
" " & DeptID & " || ' : ' || " & Dept & " as " & ShowDeparment & "  from " & tblDepartment & " " &
" where " & wStandard & " and " & DeptID & "=@pDept  Order By ooefl001 asc"
    '''<remarks>'# Department get DataTable</remarks> 
    Public Shared Function GetDepartmentFind_Table(Dept As String) As DataTable
        Dim Sql As String = strDeptarmentFind.Replace("@pDept", "'" & Dept & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOEFL", "GetDepartmentFind_Table", "Sql = strDeptarmentFind", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '''<remarks># Department get DataSet</remarks>
    Public Shared Function GetDepartmentFind_DataSet(Dept As String) As DataSet
        Dim Sql As String = strDeptarmentFind.Replace("@pDept", "'" & Dept & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOEFL", "GetDepartmentFind_DataSet", "Sql = strDeptarmentFind", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
