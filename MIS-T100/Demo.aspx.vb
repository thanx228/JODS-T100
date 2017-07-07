Imports System
Imports System.Data
Public Class Demo
    Inherits System.Web.UI.Page
    Dim clsDB As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Private Sub btnBetween_Click(sender As Object, e As EventArgs) Handles btnBetween.Click
        Dim dt As DataTable = SFBA.GetManufactureOrder_BeetweenMO_No_Body(txtForm.Text, txtTo.Text)
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call SaleDeliveryHeader()
    End Sub

    Private Sub SaleDeliveryHeader()
        'Dim dt As DataTable = XMDM.getSaleDelivery_MulitiStoreDetail(txtSaleOrcerNo.Text)
        'Dim ds As DataSet = ECAA.GetFindWorkcenterDetail_DataSet(txtSaleOrcerNo.Text)
        'Dim dt As DataTable = SFBA.GetManufactureOrder_Body(txtSaleOrcerNo.Text)
        'Dim ds As DataSet = SFBA.GetManufactureOrder_BodyDataSet(txtSaleOrcerNo.Text)
        'Dim dt As DataTable = XMDC.getSO_SaleDetail(txtSaleOrcerNo.Text)
        'Dim ds As DataSet = XMDC.getSO_SaleDetail_DataSet(txtSaleOrcerNo.Text)
        'Dim dt As DataTable = SFBA.GetManufactureOrder_BOM_Item_Body(txtSaleOrcerNo.Text)
        'Dim dt As DataTable = IMAAL.GetDataProducItem(txtSaleOrcerNo.Text)

        'Dim dt As DataTable = ECAA.GetWorkcenterDetailMultiAll_Table("WC01,WC02")
        'GridView1.DataSource = dt
        'GridView1.DataBind()


        'MsgBox(DateForm.dateText & "************" & DateTo.dateText)
        'Dim dt As DataTable = XMDK.getSaleDelivery_HeaderDetailAll_DateBetween(DateForm.dateText, DateTo.dateText)
        'GridView1.DataSource = dt
        'GridView1.DataBind()
    End Sub


End Class