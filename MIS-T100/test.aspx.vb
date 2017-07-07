Imports System.Data
Public Class test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As DataTable = SFAA.GetMO_HeaderDeatil(txtSearch.Text)
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dt As DataTable = gridviewToDataTable(GridView1)
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub
    Public Function gridviewToDataTable(gv As GridView) As DataTable
        Dim dtReport As New DataTable("TableReport")
        Dim dr As DataRow = Nothing
        dtReport.Columns.Add(New DataColumn("MO_No", GetType(String)))

        For Each row As GridViewRow In gv.Rows
            dr = dtReport.NewRow()
            dr("MO_No") = row.Cells(0).Text
            'dr("date") = DateTime.Parse(row.Cells(0).Text)
            'dr("loanbalance") = Double.Parse(row.Cells(1).Text)
            'dr("offsetbalance") = Double.Parse(row.Cells(2).Text)
            'dr("netloan") = Double.Parse(row.Cells(3).Text)
            'dr("interestrate") = Double.Parse(row.Cells(4).Text)
            'dr("interestrateperday") = Double.Parse(row.Cells(5).Text)
            dtReport.Rows.Add(dr)
        Next

        Return dtReport
    End Function
End Class