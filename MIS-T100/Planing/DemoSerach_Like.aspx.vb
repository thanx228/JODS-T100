Imports System
Imports System.Data
Imports System.Data.OracleClient
Imports System.IO

Public Class DemoSerach_Like
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'gvEmp.DataSource = SFBA.GetManufactureOrder_Body("JP5102-20170214003")
        'gvEmp.DataBind()

        'gvDept.DataSource = IMAAL.GetDataProducItemAll
        'gvDept.DataBind()


        'showTestDTgetJoin_FunctionData()

        ' showTestDTgetJoin()
    End Sub
    Private Shared strSqlRowProcessJoinDataTable As String = "SELECT " & SFCB.WONo & "," & SFCB.LineNo & "," & SFCB.WorkStation & " " &
" FROM  " & SFCB.tblMOprocessItem_SFCB & "  where " & SFCB.WONo & " =@pMoNO  "
    Private Function GetProcessJoinDataTable(strWH_MoNo As String) As DataTable
        Dim strSQL As String = strSqlRowProcessJoinDataTable
        strSQL = strSqlRowProcessJoinDataTable.Replace("@pMoNO", "'" & strWH_MoNo & "'")
        ' strSQL = strSqlRowProcessJoinDataTable.Replace("@pRuncard", "'" & strRuncard & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim Pathfiles As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString()
            GetPageError.GetPage(Pathfiles, "GetProcessJoinDataTable", "strSQL = strSqlRowProcessJoinDataTable", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub btnSeacth_Click(sender As Object, e As EventArgs) Handles btnSeacth.Click

        'For Each foundFile As String In My.Computer.FileSystem.GetFiles(
        '   My.Computer.FileSystem.SpecialDirectories.MyDocuments,
        '   Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.dll")

        '    ListBox1.Items.Add(foundFile)
        'Next
        'Dim fi As FileInfo() = FileLocation.GetFiles("*data*.csv")
        'Dim fi As List(Of FileInfo) = New List(Of FileInfo)
        'For Each File In FileLocation.GetFiles()
        '    If (File IsNot Nothing) Then
        '        If (Path.GetExtension(File.ToString.ToLower) = ".csv") Then
        '            If (File.ToString.ToLower.Contains("data")) Then fi.Add(File)
        '        End If
        '    End If
        'Next
        'ListBox1.Items.Clear()
        'Dim fileNames = My.Computer.FileSystem.GetFiles(
        '    folderPath, FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
        'For Each fileName As String In fileNames
        '    ListBox1.Items.Add(fileName)
        'Next

        'Dim dt As DataTable = GetProcessJoinDataTable(txtSearch.Text)
        'If dt.Rows.Count > 0 Then
        '    GridView1.DataSource = dt
        '    GridView1.DataBind()

        '    Dim pivotedTable As DataTable = PivotTable(dt)
        '    GridView2.DataSource = pivotedTable
        '    GridView2.DataBind()
        'End If
        Dim PO_NO As String = txtSearch.Text
        Dim ItemNO As String = txtSearch2.Text
        Dim dtPODelivery As DataTable = PMDO.GetPO_Body_ByPNO_ItemNo_Delivery(PO_NO, ItemNO)
        If dtPODelivery.Rows.Count > 0 Then
            GridView1.DataSource = dtPODelivery
            GridView1.DataBind()
        End If
    End Sub
    Private Function PivotTable(origTable As DataTable) As DataTable
        Dim newTable As New DataTable()
        Dim dr As DataRow = Nothing
        'Add Columns to new Table
        Dim i As Integer = 0
        While i <= origTable.Rows.Count
            newTable.Columns.Add(New DataColumn(origTable.Columns(i).ColumnName, GetType([String])))
            i += 1
        End While

        'Execute the Pivot Method
        Dim cols As Integer = 0
        While cols < origTable.Columns.Count
            dr = newTable.NewRow()
            Dim rows As Integer = 0
            While rows < origTable.Rows.Count
                If rows < origTable.Columns.Count Then
                    dr(0) = origTable.Columns(cols).ColumnName
                    ' Add the Column Name in the first Column
                    dr(rows + 1) = origTable.Rows(rows)(cols)
                End If
                rows += 1
            End While
            'add the DataRow to the new Table rows collection
            newTable.Rows.Add(dr)
            cols += 1
        End While
        Return newTable
    End Function
    'Public Function gridviewToDataTable(dt As DataTable) As DataTable
    '    'Dim dtReport As New DataTable("TableReport")
    '    Dim tbl As New DataTable
    '    Dim dr As DataRow = Nothing
    '    Dim columns As DataColumnCollection = dt.Columns
    '    Dim column As DataColumn
    '    For Each column In columns
    '        tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
    '    Next
    '    Dim eRow As DataRowCollection = dt.Rows
    '    For Each drs As DataRow In eRow
    '        tbl.ImportRow(drs)
    '    Next

    '    'dtReport.Columns.Add(New DataColumn("Workstation", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Scarp_ApplicatinNo", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Entry_Date", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("MO_DocNo", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Production_Item", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Scarp_Item", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Scarp_Spec", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Production_Qty", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("ScarpAppliedQty", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Scarp_Uint", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Defect", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("RootCause", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("Applicant", GetType(String)))
    '    'dtReport.Columns.Add(New DataColumn("EmpWorkDefect", GetType(String)))
    '    'For Each row As GridViewRow In gv.Rows
    '    '    Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
    '    '    dr = dtReport.NewRow()
    '    '    If chkSelect.Checked Then
    '    '        dr("Workstation") = row.Cells(17).Text
    '    '        dr("Scarp_ApplicatinNo") = row.Cells(22).Text
    '    '        dr("Entry_Date") = row.Cells(23).Text
    '    '        dr("MO_DocNo") = row.Cells(4).Text
    '    '        dr("Production_Item") = row.Cells(6).Text
    '    '        dr("Production_Qty") = row.Cells(12).Text
    '    '        dr("Scarp_Item") = row.Cells(8).Text
    '    '        dr("Scarp_Spec") = row.Cells(9).Text
    '    '        dr("ScarpAppliedQty") = row.Cells(31).Text
    '    '        dr("Scarp_Uint") = row.Cells(32).Text
    '    '        dr("Defect") = ""
    '    '        dr("RootCause") = ""
    '    '        dr("Applicant") = row.Cells(24).Text
    '    '        dr("EmpWorkDefect") = ""
    '    '        'dr("date") = DateTime.Parse(row.Cells(0).Text)
    '    '        'dr("loanbalance") = Double.Parse(row.Cells(1).Text)
    '    '        dtReport.Rows.Add(dr)
    '    '    End If
    '    'Next
    '    'Return dtReport
    '    Return tbl
    'End Function
    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.Cells(1).Value = True Then
        '    Dim col As New DataColumn(e.Row.Cells(1).Value, Type.GetType("System.String"))
        '    dt.Columns.Add(e.Row.Cells(1).Value)
        'End If
    End Sub
    Private Sub showTestDTgetJoin_FunctionData()
        'Dim dtMO_Body As DataTable = SFBA.GetManufactureOrder_Body("JP5102-20170214003")
        'Dim dtBody_item As DataTable = IMAAL.GetDataProducItemAll

        'Dim obj_ParentBOM_ID As DataColumn, obj_ChildItem_ID As DataColumn
        'Dim ds As New DataSet()
        'ds.Tables.Add(dtMO_Body)
        'ds.Tables.Add(dtBody_item)
        'obj_ParentBOM_ID = ds.Tables("sfba_t").Columns(("sfba005"))
        'obj_ChildItem_ID = ds.Tables("imaal_t").Columns("imaal001")

        'dtMO_Body.Columns.Add("imaal001")
        'Dim obj_DataRelation As New DataRelation("item_reln", obj_ParentBOM_ID, obj_ChildItem_ID)
        'ds.Relations.Add(obj_DataRelation)
        'For Each dr_Employee As DataRow In ds.Tables("sfba_t").Rows
        '    Dim dr_Department As DataRow = dr_Employee.GetParentRow(obj_DataRelation)
        '    dr_Employee("sfba005") = dr_Department("imaal001")
        'Next
        'Dim dtResult As DataTable = ds.Tables("sfba_t").DefaultView.ToTable(False, "sfba005", "imaal001")
        'gvJoin.DataSource = dtResult
        'gvJoin.DataBind()
    End Sub
    Private Sub showTestDTgetJoin()
        Dim dtEmployee As DataTable = getEmployeeRecords()
        Dim dtDepartment As DataTable = getDepartmentRecords()
        '#Region "One solution using Data Relations"
        Dim obj_ParentDepartmentID As DataColumn, obj_ChildDepartmentID As DataColumn
        Dim ds As New DataSet()
        ds.Tables.Add(dtDepartment)
        ds.Tables.Add(dtEmployee)
        obj_ParentDepartmentID = ds.Tables("Department").Columns("DeptID")
        obj_ChildDepartmentID = ds.Tables("Employee").Columns("DeptID")
        ' To show the output in a Datatable I have added one more column in Employee table.
        ' I will keep the Department Name in new column
        dtEmployee.Columns.Add("DepartmentName")
        Dim obj_DataRelation As New DataRelation("dept_reln", obj_ParentDepartmentID, obj_ChildDepartmentID)
        ds.Relations.Add(obj_DataRelation)
        ' Here I have created relation between two data columns of two different Data Tables
        For Each dr_Employee As DataRow In ds.Tables("Employee").Rows
            Dim dr_Department As DataRow = dr_Employee.GetParentRow(obj_DataRelation)
            ' Here I got parent data means Department table records from each employee record
            dr_Employee("DepartmentName") = dr_Department("DeptName")
        Next
        Dim dtResult As DataTable = ds.Tables("Employee").DefaultView.ToTable(False, "EmployeeID", "EmployeeName", "FatherName", "DepartmentName")
        gvJoin.DataSource = dtResult
        gvJoin.DataBind()
    End Sub
    Private Function getDepartmentRecords() As DataTable
        Dim dtDepartment As New DataTable("Department")
        dtDepartment.Columns.Add("DeptID", GetType(Integer))
        dtDepartment.Columns.Add("DeptName", GetType(String))
        ' I have added 3 Department Details in below code.
        Dim dr As DataRow = dtDepartment.NewRow()
        dr("DeptID") = 1
        dr("DeptName") = "IT"
        dtDepartment.Rows.Add(dr)
        dr = dtDepartment.NewRow()
        dr("DeptID") = 2
        dr("DeptName") = "Marketing"
        dtDepartment.Rows.Add(dr)
        dr = dtDepartment.NewRow()
        dr("DeptID") = 3
        dr("DeptName") = "Development"
        dtDepartment.Rows.Add(dr)
        Return dtDepartment
    End Function
    Private Function getEmployeeRecords() As DataTable
        Dim dtEmployee As New DataTable("Employee")
        dtEmployee.Columns.Add("EmployeeID", GetType(Integer))
        dtEmployee.Columns.Add("EmployeeName", GetType(String))
        dtEmployee.Columns.Add("FatherName", GetType(String))
        dtEmployee.Columns.Add("Address", GetType(String))
        dtEmployee.Columns.Add("DeptID", GetType(Integer))
        ' I have added 5 Employees' records in below code.
        Dim dr As DataRow = dtEmployee.NewRow()
        dr("EmployeeID") = 1
        dr("EmployeeName") = "A1"
        dr("FatherName") = "F1"
        dr("Address") = "Bangalore"
        dr("DeptID") = 1
        dtEmployee.Rows.Add(dr)

        dr = dtEmployee.NewRow()
        dr("EmployeeID") = 2
        dr("EmployeeName") = "A2"
        dr("FatherName") = "F2"
        dr("Address") = "Bangalore"
        dr("DeptID") = 1
        dtEmployee.Rows.Add(dr)

        dr = dtEmployee.NewRow()
        dr("EmployeeID") = 3
        dr("EmployeeName") = "A3"
        dr("FatherName") = "F3"
        dr("Address") = "Bangalore"
        dr("DeptID") = 2
        dtEmployee.Rows.Add(dr)
        dr = dtEmployee.NewRow()

        dr("EmployeeID") = 4
        dr("EmployeeName") = "A4"
        dr("FatherName") = "F4"
        dr("Address") = "Bangalore"
        dr("DeptID") = 1
        dtEmployee.Rows.Add(dr)
        dr = dtEmployee.NewRow()

        dr("EmployeeID") = 5
        dr("EmployeeName") = "A5"
        dr("FatherName") = "F5"
        dr("Address") = "Bangalore"
        dr("DeptID") = 3
        dtEmployee.Rows.Add(dr)
        Return dtEmployee
    End Function

    Sub DTjoin()
        Dim myDataTable As New DataTable()
        Dim PartNumber As New DataColumn("PartNumber")
        Dim RequiredQuantity As New DataColumn("RequiredQuantity")
        myDataTable.Columns.Add(PartNumber)
        myDataTable.Columns.Add(RequiredQuantity)

        Dim dataRowPN1 As DataRow = myDataTable.NewRow()
        Dim dataRowPN2 As DataRow = myDataTable.NewRow()
        Dim dataRowPN3 As DataRow = myDataTable.NewRow()

        dataRowPN1("PartNumber") = "A"
        dataRowPN2("PartNumber") = "B"
        dataRowPN3("PartNumber") = "C"
        dataRowPN1("RequiredQuantity") = "2"
        dataRowPN2("RequiredQuantity") = "1"
        dataRowPN3("RequiredQuantity") = "3"
        myDataTable.Rows.Add(dataRowPN1)
        myDataTable.Rows.Add(dataRowPN2)
        myDataTable.Rows.Add(dataRowPN3)
        Dim i As Integer = myDataTable.Rows.Count

        Dim joinDataTable As New DataTable()
        Dim SerialNumber As New DataColumn("SerialNumber")
        Dim JoinPartNumber As New DataColumn("PartNumber")
        joinDataTable.Columns.Add(SerialNumber)
        joinDataTable.Columns.Add(JoinPartNumber)
        For Each dr As DataRow In myDataTable.Rows
            Dim count As Integer = 1
            While count <= Convert.ToInt16(dr("RequiredQuantity"))
                Dim joindataRow As DataRow = joinDataTable.NewRow()
                joindataRow("SerialNumber") = count.ToString().Trim()
                joindataRow("PartNumber") = dr("PartNumber").ToString().Trim()
                joinDataTable.Rows.Add(joindataRow)
                count += 1
            End While
        Next
        GridView1.DataSource = joinDataTable
        GridView1.DataBind()
        Response.Write(joinDataTable.Rows.Count)
    End Sub
End Class