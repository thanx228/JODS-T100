Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Public Class ContDtFormOrl
    Inherits System.Web.UI.Page
    Shared Conn_ORL As New clsDBConnect
    Shared DBCONN_SQL As New clsDBConnect
    '--โชว์ข้อมูลใน Gridview
    Public Sub OrlShowGridView(ByRef Gridview1 As Object, OrlCommand As String, Optional OrlconnectSting As String = "")
        If OrlconnectSting = "" Then
            OrlconnectSting = clsDBConnect.strT100ConnectionString
        End If

        Dim da As New OracleDataAdapter(OrlCommand, OrlconnectSting)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Gridview1.DataSource = ds
            Gridview1.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            Gridview1.DataSource = ds
            Gridview1.DataBind()
            Dim ColumnCount As Integer = Gridview1.Rows(0).Cells.Count
            Gridview1.Rows(0).Cells.Clear()
            Gridview1.Rows(0).Cells.Add(New TableCell())
            Gridview1.Rows(0).Cells(0).ColumnSpan = ColumnCount
            Gridview1.Rows(0).Cells(0).Text = "No Data Found"

        End If
    End Sub

    '--โชว์ข้อมูลใน Dropdown
    Public Sub OrlShowDDL(ByRef ControlDDL As DropDownList, ByVal OrlCommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "", Optional ByVal headVal As String = "ALL")
        If connectSting = "" Then
            connectSting = clsDBConnect.strT100ConnectionString
        End If

        Dim da As New OracleDataAdapter(OrlCommand, connectSting)
        Dim ds As New DataSet
        da.Fill(ds)

        With ControlDDL
            .DataSource = ds
            .DataTextField = fldText
            .DataValueField = fldValue
            .DataBind()
            If setAll = True Then
                .Items.Insert(0, headVal)
            End If
        End With
    End Sub

    '--Set ค่าหัวคอลมน์ ของคิวรี่ โชว์ข้อมูลใน Gridview
    Shared Function DataTableTranf(ByVal DataTableFiledName() As String, ByRef DT3 As Data.DataTable) As Boolean
        DT3.Clear()
        For L_count As Integer = 0 To DataTableFiledName.Length - 1
            Dim myColumn As New Data.DataColumn
            myColumn.DataType = System.Type.GetType("System.String")
            myColumn.ColumnName = DataTableFiledName(L_count)
            DT3.Columns.Add(myColumn)
        Next
    End Function

    '--นับแถวตาราง
    Public Function RowGridview(ByRef gridview As GridView) As Decimal
        Dim rowGrid As Decimal = gridview.Rows.Count
        If rowGrid = 0 Then
            rowGrid = 0
        ElseIf rowGrid = 1 And gridview.Rows(0).Cells(0).Text = "No Data Found" Then
            rowGrid = 0
        End If
        Return rowGrid
    End Function

    '--โชว์ข้อมูลใน Gridview
    Public Sub ShowGridViewSQL(ByRef Gridview1 As Object, ByRef tempDataTable As Data.DataTable)

        If tempDataTable.Rows.Count > 0 Then
            Gridview1.DataSource = tempDataTable
            Gridview1.DataBind()
        Else
            tempDataTable.Rows.Add(tempDataTable.NewRow())
            'dt.NewRow()
            Gridview1.DataSource = tempDataTable
            Gridview1.DataBind()
            Dim ColumnCount As Integer = Gridview1.Rows(0).Cells.Count
            Gridview1.Rows(0).Cells.Clear()
            Gridview1.Rows(0).Cells.Add(New TableCell())
            Gridview1.Rows(0).Cells(0).ColumnSpan = ColumnCount
            Gridview1.Rows(0).Cells(0).Text = "No Data Found"

        End If
    End Sub

    '--ExportGridViewToExcel
    Public Sub ExportGridViewToExcel(ByVal FileName As String, ByVal gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" +
        HttpContext.Current.Server.UrlEncode(FileName) & ".xls")

        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        Dim StringWriter As New System.IO.StringWriter
        Dim HtmlTextWriter As New HtmlTextWriter(StringWriter)
        gv.AllowSorting = False
        gv.AllowPaging = False
        gv.EnableViewState = False
        gv.AutoGenerateColumns = False
        gv.RenderControl(HtmlTextWriter)
        HttpContext.Current.Response.Write(StringWriter.ToString())
        HttpContext.Current.Response.End()
    End Sub

    '--Header Form
    Public Function nameHeader(ByVal progName As String) As String
        Dim strHeader As String = ""

        Dim SQL As String,
            dt As New DataTable
        SQL = "select ParentId,Name from Menu where Prog='" & progName.Substring(1, progName.Length - 1) & "' "
        GetData.Get_DataReaderSQL(SQL, clsDBConnect.strMIS2ConnectionString, dt)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                strHeader = .Item("Name").ToString.Trim
                strHeader = getName(.Item("ParentId"), strHeader)
            End With
        End If
        Return strHeader
    End Function

    '--Header Form
    Private Function getName(ByVal pID As Integer, ByVal str As String) As String
        Dim SQL As String,
           dt As New DataTable
        SQL = "select ParentId,Name from Menu where Id='" & pID & "' "
        GetData.Get_DataReaderSQL(SQL, clsDBConnect.strMIS2ConnectionString, dt)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                str = .Item("Name").ToString.Trim & " -> " & str
                str = getName(.Item("ParentId"), str)
            End With
        End If
        Return str
    End Function
End Class
