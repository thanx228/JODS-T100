Imports System
Imports System.Data
Imports System.Data.OracleClient
Imports System.IO
Partial Class Tos_test
    Inherits System.Web.UI.Page
    Dim _Separator As String = "."
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGridView()
        End If
    End Sub


    'Binds all the GridView used in the page.//
    Private Sub BindGridView()
        ' Retrieve the data table from Excel Data Source.
        'Dim dt As DataTable = ExcelLayer.GetDataTable("_Data\DataForPivot.xls", "Sheet1$")
        Dim dt As DataTable = ExcelLayer.GetDataTable("Example\_Data\DataForPivot.xls", "Sheet1$")
        Dim pvt As New Pivot(dt)

        grdRawData.DataSource = dt
        grdRawData.DataBind()

        grdCompanyYear.DataSource = Pivot.PivotData("Company", "CTC", AggregateFunction.Count, "Year")
        grdCompanyYear.DataBind()
        grdLeastCompanyYear.DataSource = Pivot.PivotData("Company", "CTC", AggregateFunction.Min, "Year")
        grdLeastCompanyYear.DataBind()

        grdDesignationCompanyYear.DataSource = Pivot.PivotData("Designation", "CTC", AggregateFunction.Max, "Company", "Year")
        grdDesignationCompanyYear.DataBind()
        grdDesignationCompanyYearAvg.DataSource = Pivot.PivotData("Designation", "CTC", AggregateFunction.Average, "Company", "Year")
        grdDesignationCompanyYearAvg.DataBind()

        grdPivot.DataSource = Pivot.PivotData("Designation", "CTC", AggregateFunction.Max, "Company", "Department", "Year")
        grdPivot.DataBind()
    End Sub

    Protected Sub grdPivot2_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            MergeHeader(DirectCast(sender, GridView), e.Row, 2)
        End If
    End Sub

    Protected Sub grdPivot3_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            MergeHeader(DirectCast(sender, GridView), e.Row, 3)
        End If
    End Sub
    ''' <summary>
    ''' Function used to Create and Merge the Header Cells based on the Pivot conditions.
    ''' </summary>
    ''' <param name="gv">GridView</param>
    ''' <param name="row">Header Row of the GridView</param>
    ''' <param name="PivotLevel">The no. of ColumnFields used to Pivot the data</param>
    Private Sub MergeHeader(ByVal gv As GridView, ByVal row As GridViewRow, ByVal PivotLevel As Integer)
        For iCount As Integer = 1 To PivotLevel
            Dim oGridViewRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim Header = (row.Cells.Cast(Of TableCell)().[Select](Function(x) GetHeaderText(x.Text, iCount, PivotLevel))).GroupBy(Function(x) x)

            For Each v In Header
                Dim cell As New TableHeaderCell()
                cell.Text = v.Key.Substring(v.Key.LastIndexOf(_Separator) + 1)
                cell.ColumnSpan = v.Count()
                oGridViewRow.Cells.Add(cell)
            Next
            gv.Controls(0).Controls.AddAt(row.RowIndex, oGridViewRow)
        Next
        row.Visible = False
    End Sub

    Private Function GetHeaderText(ByVal s As String, ByVal i As Integer, ByVal PivotLevel As Integer) As String
        If Not s.Contains(_Separator) AndAlso i <> PivotLevel Then
            Return String.Empty
        Else
            Dim Index As Integer = NthIndexOf(s, _Separator, i)
            If Index = -1 Then
                Return s
            End If
            Return s.Substring(0, Index)
        End If
    End Function
    ''' <summary>
    ''' Returns the nth occurance of the SubString from string str
    ''' </summary>
    ''' <param name="str">source string</param>
    ''' <param name="SubString">SubString whose nth occurance to be found</param>
    ''' <param name="n">n</param>
    ''' <returns>Index of nth occurance of SubString if found else -1</returns>
    Private Function NthIndexOf(ByVal str As String, ByVal SubString As String, ByVal n As Integer) As Integer
        Dim x As Integer = -1
        For i As Integer = 0 To n - 1
            x = str.IndexOf(SubString, x + 1)
            If x = -1 Then
                Return x
            End If
        Next
        Return x
    End Function
End Class
