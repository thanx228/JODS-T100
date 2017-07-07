Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web
Public Class BillingReceipts2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CrystalReportViewer1.ReportSourceID = CrystalReportSource1.ClientID
        CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        CrystalReportSource1.Report.FileName = Server.MapPath("~/App_Data/Purchase_Report/BillingMonth.rpt")
        ConfigCrystalReport.Sub_CRCrystalLogon(CrystalReportSource1.ReportDocument)
        Dim paraName As String = Request.QueryString("paraName")
        Dim Arry() As String = paraName.Split(":")
        With CrystalReportSource1.ReportDocument

            .SetParameterValue("BillDateS", Request("BillDateS"))
            .SetParameterValue("BillDateE", Request("BillDateE"))
        End With
    End Sub

End Class