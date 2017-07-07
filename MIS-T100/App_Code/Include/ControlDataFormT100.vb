
Imports System.Data
Imports MIS_T100
Imports System.IO

Imports System.Data.SqlClient
Public Class ControlDataFormT100
    Inherits System.Web.UI.Page
    Shared clsDBConnect As New clsDBConnectT100

    Public Function showCheckboxListT100(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "") As String
        Try
            Dim dt As New DataTable
            dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
            clsDBConnect.Close(connectSting)

            With conCheckboxList
                .DataSource = dt
                .DataTextField = fldText
                .DataValueField = fldValue
                .DataBind()
                .RepeatColumns = showColumn
                .RepeatDirection = RepeatDirection.Horizontal
                .RepeatLayout = RepeatLayout.Flow
            End With
        Catch ex As Exception
        End Try
    End Function

    Public Function rowGridviewT100(ByRef gridview As GridView) As Decimal
        Dim rowGrid As Decimal = gridview.Rows.Count
        If rowGrid = 0 Then
            rowGrid = 0
        ElseIf rowGrid = 1 And gridview.Rows(0).Cells(0).Text = "No Data Found" Then
            rowGrid = 0
        End If
        Return rowGrid
    End Function

    Public Sub ShowGridViewT100(ByRef Gridview1 As GridView, dt As DataTable)


        If dt.Rows.Count > 0 Then
            Gridview1.DataSource = dt
            Gridview1.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            Gridview1.DataSource = dt
            Gridview1.DataBind()
            Dim ColumnCount As Integer = Gridview1.Rows(0).Cells.Count
            Gridview1.Rows(0).Cells.Clear()
            Gridview1.Rows(0).Cells.Add(New TableCell())
            Gridview1.Rows(0).Cells(0).ColumnSpan = ColumnCount
            Gridview1.Rows(0).Cells(0).Text = "No Data Found"

        End If
    End Sub

    Public Sub ShowGridViewT100(ByRef Gridview1 As Object, sqlCommand As String, Optional connectSting As String = "")
        If connectSting = "T100" Then
            connectSting = clsDBConnect.T100
        ElseIf connectSting = "MIS2" Then
            connectSting = clsDBConnect.MIS2
        End If
        Dim ds As New DataSet
        ds = clsDBConnect.QueryDataSet(sqlCommand, connectSting)


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
        clsDBConnect.Close(connectSting)
    End Sub

    Public Function nameHeaderT100(ByVal progName As String) As String

        Dim strHeader As String = ""
        Dim SQL As String
        Dim dt As New Data.DataTable

        SQL = "select ParentId,Name from Menu where Prog='" & progName.Substring(1, progName.Length - 1) & "' "
        Dim connection As New System.Data.SqlClient.SqlConnection(clsDBConnect.strMIS2ConnectionString)
        connection.Open()
        Dim adapter As New System.Data.SqlClient.SqlDataAdapter()
        adapter.SelectCommand = New System.Data.SqlClient.SqlCommand(SQL, connection)
        adapter.Fill(dt)
        connection.Close()

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                strHeader = .Item("Name").ToString.Trim
                strHeader = getName(.Item("ParentId"), strHeader)
            End With
        End If
        Return strHeader
    End Function

    Private Function getName(ByVal pID As Integer, ByVal str As String) As String
        Dim SQL As String
        Dim dt As New DataTable
        SQL = "select ParentId,Name from Menu where Id='" & pID & "' "
        Dim connection As New System.Data.SqlClient.SqlConnection(clsDBConnect.strMIS2ConnectionString)
        connection.Open()
        Dim adapter As New System.Data.SqlClient.SqlDataAdapter()
            adapter.SelectCommand = New System.Data.SqlClient.SqlCommand(SQL, connection)
            adapter.Fill(dt)
        connection.Close()
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                str = .Item("Name").ToString.Trim & " -> " & str
                str = getName(.Item("ParentId"), str)
            End With
        End If
        Return str
    End Function

End Class
