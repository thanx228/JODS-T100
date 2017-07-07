Imports System.Data
Imports System.Data.OracleClient

' Written by Anurag Gandhi.
' Url: http://www.gandhisoft.com
' Contact me at: soft.gandhi@gmail.com
Partial Class Sample
    Inherits System.Web.UI.Page

    Dim _Separator As String = "."

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGridView()
        End If
    End Sub
    Private Shared strSqlRowProcessJoinDataTable As String = "SELECT " & SFCB.WONo & " as MO_DocNo," & SFCB.LineNo & "," & SFCB.WorkStation & "," & SFCB.WIP & ", " &
" " & SFCB.OperationID & " " &
" FROM  " & SFCB.tblMOprocessItem_SFCB & "  where " & SFCB.WONo & " =@pMoNO  Order by " & SFCB.LineNo & " "
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


    'Binds all the GridView used in the page.//
    Private Sub BindGridView()
        ' Retrieve the data table from Excel Data Source.
        ' Dim dt As DataTable = ExcelLayer.GetDataTable("_Data\DataForPivot.xls", "Sheet1$")
        Dim dt As DataTable = GetProcessJoinDataTable("JP5102-20151104168")
        Dim pvt As New Pivot(dt)

        grdRawData.DataSource = dt
        grdRawData.DataBind()

        ' Public Function PivotData(ByVal RowField As String, ByVal DataField As String, ByVal Aggregate As AggregateFunction, ByVal ParamArray ColumnFields As String()) As DataTable
        'grdCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.LineNo, AggregateFunction.Sum, SFCB.LineNo)
        'grdCompanyYear.DataBind()

        'grdLeastCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Min, SFCB.LineNo)
        'grdLeastCompanyYear.DataBind()

        'grdDesignationCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Max, SFCB.WorkStation, SFCB.LineNo)
        'grdDesignationCompanyYear.DataBind()

        'grdDesignationCompanyYearAvg.DataSource = pvt.PivotData("MO_DocNo", SFCB.LineNo, AggregateFunction.Average, SFCB.LineNo, SFCB.WorkStation)
        'grdDesignationCompanyYearAvg.DataBind()

        'grdPivot.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Max, SFCB.WorkStation, SFCB.OperationID, SFCB.LineNo)
        'grdPivot.DataBind()

        'grdCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.LineNo, AggregateFunction.Sum, SFCB.WorkStation)
        'grdCompanyYear.DataBind()

        ''grdLeastCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Min, SFCB.WorkStation)
        ''grdLeastCompanyYear.DataBind()

        ' Public Function PivotData(ByVal RowField As String, ByVal DataField As String, ByVal OrderByField As String, ByVal Aggregate As AggregateFunction, ByVal ParamArray ColumnFields As String()) As DataTable

        grdDesignationCompanyYear.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Max, SFCB.LineNo, SFCB.WorkStation)
        grdDesignationCompanyYear.DataBind()


        'grdPivot.DataSource = pvt.PivotData("MO_DocNo", SFCB.WIP, AggregateFunction.Max, SFCB.LineNo, SFCB.OperationID, SFCB.WorkStation)
        'grdPivot.DataBind()

        'grdLeastCompanyYear.DataSource = pvt.PivotData("Company", "CTC", AggregateFunction.Min, "Year")
        'grdLeastCompanyYear.DataBind()

        'grdDesignationCompanyYear.DataSource = pvt.PivotData("Designation", "CTC", AggregateFunction.Max, "Company", "Year")
        'grdDesignationCompanyYear.DataBind()
        'grdDesignationCompanyYearAvg.DataSource = pvt.PivotData("Designation", "CTC", AggregateFunction.Average, "Company", "Year")
        'grdDesignationCompanyYearAvg.DataBind()

        'grdPivot.DataSource = pvt.PivotData("Designation", "CTC", AggregateFunction.Max, "Company", "Department", "Year")
        'grdPivot.DataBind()
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
