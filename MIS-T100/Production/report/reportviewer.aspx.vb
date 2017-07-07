Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class reportviewer
    Inherits System.Web.UI.Page

    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("username") = String.Empty Then
            Response.Redirect("../../Login.aspx")
        Else
            Dim userid As String = Session("UserName")
            Dim ses_repname As String = Session("reportname")
            Dim ses_reppath As String = Session("reportpath")
            Dim fullpath As String = ses_repname

            Dim CrystalReport As New ReportDocument()
            'CrystalReport.Load(Server.MapPath("~/CustomerReport.rpt"))
            CrystalReport.Load(Server.MapPath(fullpath))
            'Dim ds As DataSet = GetData("select * from customers")
            Dim ds As DataSet = Session("datasource")
            CrystalReport.SetDataSource(ds)
            CrystalReport.SetDatabaseLogon("itprogram", "Jinpa0")
            CRViewer.ReportSource = CrystalReport
        End If

    End Sub

    'Private Function GetData(query As String) As DataSet
    '    Dim conString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    '    Dim cmd As New SqlCommand(query)
    '    Using con As New SqlConnection(conString)
    '        Using sda As New SqlDataAdapter()
    '            cmd.Connection = con
    '            sda.SelectCommand = cmd
    '            Using dsCustomers As New DataSet()
    '                sda.Fill(dsCustomers, "DataTable1")
    '                Return dsCustomers
    '            End Using
    '        End Using
    '    End Using
    'End Function

End Class