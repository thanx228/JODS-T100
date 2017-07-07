Imports System.Data.OracleClient
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports System.Linq
Imports System.Collections.Generic
Imports System
Imports System.Drawing
Imports System.Web.UI.Control
'Namespace getSortGrid
Public Class GridviewUtility
    Inherits System.Web.UI.UserControl
    Public Shared Function ShowHead(gvShow As GridView, ByVal colStart As Integer, ByVal dayOfMon As Integer)
        With gvShow
            Dim ii As Double = 0
            For i As Integer = colStart To .Columns.Count - 1
                With .Columns(i)
                    Dim ddd As String = DateTime.Now.AddDays(ii).ToString("ddd")
                    If ddd = "Sun" Or ddd = "Sat" Then
                        .ItemStyle.BackColor = Drawing.Color.Red
                    End If
                    .HeaderText = DateTime.Now.AddDays(ii).ToString("dd-MM-yyyy(ddd)")
                End With
                ii += 1
            Next
        End With
        Return gvShow
    End Function
    Public Shared Function GridStyleTemplate_Std(gv As GridView)
        With gv
            .BackColor = ColorTranslator.FromHtml("#FFFFFF")
            .BorderColor = ColorTranslator.FromHtml("#3366CC")
            .BorderStyle = BorderStyle.None
            .BorderWidth = 4
            .FooterStyle.BackColor = ColorTranslator.FromHtml("#99CCCC")
            .FooterStyle.ForeColor = ColorTranslator.FromHtml("#003399")
            .HeaderStyle.BackColor = ColorTranslator.FromHtml("#003399")
            .HeaderStyle.Font.Bold = True
            .HeaderStyle.ForeColor = ColorTranslator.FromHtml("#CCCCFF")
            .PagerStyle.BackColor = ColorTranslator.FromHtml("#99CCCC")
            .PagerStyle.ForeColor = ColorTranslator.FromHtml("#003399")
            .PagerStyle.HorizontalAlign = HorizontalAlign.Left
            .RowStyle.BackColor = ColorTranslator.FromHtml("White")
            .RowStyle.ForeColor = ColorTranslator.FromHtml("#003399")
            .SelectedRowStyle.BackColor = ColorTranslator.FromHtml("#009999")
            .SelectedRowStyle.ForeColor = ColorTranslator.FromHtml("#CCFF99")
            .SelectedRowStyle.Font.Bold = True
            .SortedAscendingCellStyle.BackColor = ColorTranslator.FromHtml("#EDF6F6")
            .SortedAscendingHeaderStyle.BackColor = ColorTranslator.FromHtml("#0D4AC4")
            .SortedDescendingCellStyle.BackColor = ColorTranslator.FromHtml("#D6DFDF")
            .SortedDescendingHeaderStyle.BackColor = ColorTranslator.FromHtml("#002876")
        End With
        Return gv
    End Function
    Public Shared Function GridStyleTemplateStyleTooling(gv As GridView)
        With gv
            .BackColor = ColorTranslator.FromHtml("White")
            .BackColor = ColorTranslator.FromHtml("#FFFFFF")
            .BorderColor = ColorTranslator.FromHtml("#CCCCCC")
            .BorderStyle = BorderStyle.None
            .BorderWidth = 5
            .CellPadding = 3
            '.Attributes.Add("class", "header")
            .FooterStyle.BackColor = ColorTranslator.FromHtml("#99CCCC")
            .FooterStyle.ForeColor = ColorTranslator.FromHtml("#003399")
            .HeaderStyle.BackColor = ColorTranslator.FromHtml("#666666")
            .HeaderStyle.Font.Bold = True
            .HeaderStyle.ForeColor = ColorTranslator.FromHtml("White")
            .PagerStyle.BackColor = ColorTranslator.FromHtml("#F5F5DC")
            .PagerStyle.ForeColor = ColorTranslator.FromHtml("#000066")
            .PagerStyle.HorizontalAlign = HorizontalAlign.Left
            .RowStyle.BackColor = ColorTranslator.FromHtml("White")
            .RowStyle.BorderColor = ColorTranslator.FromHtml("#EDF6F6")
            .RowStyle.ForeColor = ColorTranslator.FromHtml("black")
            .SelectedRowStyle.BackColor = ColorTranslator.FromHtml("#669999")
            .SelectedRowStyle.ForeColor = ColorTranslator.FromHtml("White")
            .SelectedRowStyle.Font.Bold = True
            .SortedAscendingCellStyle.BackColor = ColorTranslator.FromHtml("#EDF6F6")
            .SortedAscendingHeaderStyle.BackColor = ColorTranslator.FromHtml("#0D4AC4")
            .SortedDescendingCellStyle.BackColor = ColorTranslator.FromHtml("#D6DFDF")
            .SortedDescendingHeaderStyle.BackColor = ColorTranslator.FromHtml("#002876")
        End With
        Return gv
    End Function
    Public Shared Function GirdSelectRows(page As Page, gv As GridView)
        For Each row As GridViewRow In gv.Rows
            If row.RowType = DataControlRowType.DataRow Then
                row.Attributes("onclick") = page.ClientScript.GetPostBackClientHyperlink(gv, "Select$" & row.RowIndex)
                row.Attributes("style") = "cursor: pointer"
            End If
        Next
        Return gv
    End Function

    Public Shared Function GrigOnmouseHandleCustomer(gv As GridView, htmlColor As String)
        Dim style As String = "this.originalstyle=this."
        Dim BG As String = "style.backgroundColor;"
        Dim sBG As String = "this.style.backgroundColor="
        Dim Styleonmouseover As String = style & BG & sBG & "'" & htmlColor & "'"
        Dim Operation As String = " = "
        Dim BgOrignal As String = "this.style.backgroundColor"
        Dim Orignal As String = "this.originalstyle;"
        Dim StrOnmoueout As String = "onmouseout"
        Dim StyleOnmoueout As String = BgOrignal & Operation & Orignal
        For Each row As GridViewRow In gv.Rows
            row.Attributes.Add("onmouseover", Styleonmouseover)
            row.Attributes.Add(StrOnmoueout, StyleOnmoueout)
        Next
        Return gv
    End Function
    Public Shared Function GrigOnmouseHandleAuto(gv As GridView)
        Dim HtmlColor As String = "#FFCCFF"
        Dim style As String = "this.originalstyle=this."
        Dim BG As String = "style.backgroundColor;"
        Dim sBG As String = "this.style.backgroundColor="
        Dim Styleonmouseover As String = style & BG & sBG & "'" & htmlColor & "'"
        Dim Operation As String = " = "
        Dim BgOrignal As String = "this.style.backgroundColor"
        Dim Orignal As String = "this.originalstyle;"
        Dim StrOnmoueout As String = "onmouseout"
        Dim StyleOnmoueout As String = BgOrignal & Operation & Orignal
        For Each row As GridViewRow In gv.Rows
            row.Attributes.Add("onmouseover", Styleonmouseover)
            row.Attributes.Add(StrOnmoueout, StyleOnmoueout)
        Next
        Return gv
    End Function
    Public Shared Function GrigCreatedHeader(gv As GridView, ByRef Title As String, ByRef colTitle As Integer,
              ByRef pStr As String, ByRef colSpan2 As Integer, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderRow As New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            HeaderCell2.Text = Title
            HeaderCell2.Font.Bold = True
            HeaderCell2.ForeColor = System.Drawing.Color.Blue
            HeaderCell2.BorderColor = ColorTranslator.FromHtml("#BDBDBD")
            HeaderCell2.BackColor = ColorTranslator.FromHtml("#848484")
            HeaderCell2.ColumnSpan = colTitle
            HeaderCell2.HorizontalAlign = HorizontalAlign.Left
            HeaderRow.Cells.Add(HeaderCell2)
            gv.Controls(0).Controls.AddAt(0, HeaderRow)
            Dim HeaderRow1 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = pStr
            HeaderCell.ColumnSpan = colSpan2
            HeaderCell.Font.Bold = True
            HeaderCell.ForeColor = System.Drawing.Color.Black
            HeaderCell.BackColor = ColorTranslator.FromHtml("#BDBDBD")
            HeaderCell.BorderColor = ColorTranslator.FromHtml("#BDBDBD")
            HeaderCell.HorizontalAlign = HorizontalAlign.Left
            HeaderRow1.Cells.Add(HeaderCell)
            gv.Controls(0).Controls.AddAt(1, HeaderRow1)
        End If
        Return gv
    End Function
    '##### Grdivew AutoSortClumns and Custom sortExpression = Filed : Auto Srting Find by Primary Key  ###################################################
    Public Shared Sub GridSortColAutoGen(ByRef dt As DataTable, ByRef gv As GridView, ByRef sender As Object, ByRef e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If e.SortDirection = SortDirection.Ascending Then
            e.SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            e.SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        dt.DefaultView.Sort = sortExpression & direction
        gv.DataSource = dt
        gv.DataBind()
    End Sub
    Public Property SortDirectionColAutoGen() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            Else
                ViewState("SortDirection") = SortDirection.Descending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Shared Function MergeCells(gv As GridView)
        For i As Integer = gv.Rows.Count - 1 To 1 Step -1
            Dim row As GridViewRow = gv.Rows(i)
            Dim previousRow As GridViewRow = gv.Rows(i - 1)
            For j As Integer = 0 To row.Cells.Count - 1
                If row.Cells(j).Text = previousRow.Cells(j).Text Then
                    If previousRow.Cells(j).RowSpan = 0 Then
                        If row.Cells(j).RowSpan = 0 Then
                            previousRow.Cells(j).RowSpan += 2
                        Else
                            previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                        End If
                        row.Cells(j).Visible = False
                    End If
                End If
            Next
        Next
        Return gv
    End Function

    'Public Shared Function RangeDate(startDate As Date, endDate As Date) As IEnumerable(Of Date)
    '    Return Enumerable.Range(0, (endDate - startDate).Days + 1).[Select](Function(d) startDate.AddDays(d))
    'End Function







End Class
'End Namespace


