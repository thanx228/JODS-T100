Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web.Security
Imports System.Web
Imports System.Configuration
Imports System.Data
Imports System
Public Class InsertTable
    Inherits System.Web.UI.Page
    Private numOfColumns As Integer = 12
    Public Shared ctr As Integer = 0
    Shared table As New Table()
    Protected Sub Page_Load(sender As Object, e As EventArgs)
        table.ID = "table1"
        Panel1.Controls.Add(table)
    End Sub
    Protected Sub btnAddNewRow_Click(sender As Object, e As EventArgs)
        Dim tbl As New Table()
        Dim tr As New TableRow()
        Dim tc As New TableCell()
        tc.Text = "Tab1"
        tc.BorderColor = System.Drawing.Color.Black
        tc.BackColor = System.Drawing.Color.Green
        'Dim btn = New Button()
        'btn.Text = "Add "
        'btn.Height = Unit.Pixel(30)
        'btn.Width = Unit.Pixel(100)
        'tc.Controls.Add(btn)
        tr.Controls.Add(tc)
        Dim tc2 As New TableCell()
        tc2.Text = "Tab2"
        tc2.BorderColor = System.Drawing.Color.Black
        tc2.BackColor = System.Drawing.Color.Green
        tr.Controls.Add(tc2)
        tbl.Controls.Add(tr)
        'etc..
        tbl.BackColor = System.Drawing.Color.Gainsboro
        tbl.Width = 1024
        Panel1.Controls.Add(tbl)
    End Sub
    Private Sub GenerateTable(colsCount As Integer)

    End Sub
End Class