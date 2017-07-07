Imports System.Globalization
Imports System.Drawing
Public Class SLUndelStusAmountPopUp
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim TempSOUndeliveryAmount As New TempSOUndeliveryAmount
    Dim connect As New clsDBConnect
    Dim Item As String = ""
    Dim DateFrom As String = ""
    Dim DateTo As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Item = Request.QueryString("item").ToString.Trim
        Dim Spec As String = Request.QueryString("Spec").ToString.Trim
        Dim UndelQty As String = Request.QueryString("UndelQty").ToString.Trim
        Dim MOQty As String = Request.QueryString("moQty").ToString.Trim
        Dim POQty As String = Request.QueryString("poQty").ToString.Trim
        Dim StockInOut As String = Request.QueryString("StockInOut").ToString.Trim
        DateFrom = Request.QueryString("DateFrom").ToString.Trim
        DateTo = Request.QueryString("DateTo").ToString.Trim
        Dim whr As String = ""

        lblItem.Text = Item
        lblSpec.Text = Spec
        lblUnDelQty.Text = UndelQty
        lblMOQty.Text = MOQty
        lblPOQty.Text = POQty
        lblPRQty.Text = "0"
        lblStockQty.Text = StockInOut

        ShowGvStockQty(GvStockQty, True, NowPageIndex.Text)
        ShowgvUndelivery(GvSO, True, NowPageIndex.Text)
        ShowGvMO(GvMO, True, NowPageIndex.Text)
        ShowGvPO(GvPO, True, NowPageIndex.Text)
    End Sub
    '--ShowGvStockQty
    Private Sub ShowGvStockQty(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)

        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "StockNo,StockName,StockQty"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        '-- Sum ItemStock  where Item
        IMAAL.SumItemStock(Item, dt1)
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim StockQty As String = ""
            Dim DefaultWH As String = ""
            If dt1.Rows.Count > 0 Then
                StockQty = dt1.Rows(j).Item("StockQty")
                Dim Comma As Decimal = CDec(StockQty)
                StockQty = String.Format("{0:n0}", Comma)

                If StockQty = 0 Then
                    StockQty = String.Empty
                End If

                DefaultWH = dt1.Rows(j).Item("inag004")
            End If
            dt3 = INAYL.StockName(DefaultWH)
            Dim StockName As String = ""
            If dt3.Rows.Count > 0 Then
                StockName = dt3.Rows(0).Item("inayl003")
            End If
            DtShow.Rows.Add(New Object() {DefaultWH, StockName, StockQty})
        Next

        'เอาเข้า Gridview 
        GvStockQty.DataSource = DtShow
        GvStockQty.DataBind()

        Dim a As Integer = 0
        a = GvStockQty.Rows.Count
        If a = 0 Then
            'CountRow1.RowCount = False
        Else
            CountRow1.RowCount = ContDtFormOrl.RowGridview(GvStockQty)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvStockQty, "#FFCC99")
        End If
    End Sub

    '--ShowgvUndelivery
    Private Sub ShowgvUndelivery(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "CustID,SalesOrderNo,ConfirmedDate,DeliveryDate,OrderedQty,DeliveryQty,SampleQty,SampleDeliveryQty,BalanceQty,Unit,CustomerPO"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt1, dt2 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If
        XMDA.SOAmountPopUpG(Item, DateFrom, DateTo, dt1)
        For e As Integer = 0 To dt1.Rows.Count - 1
            Dim ShowCustID As String = "", ShowSalesOrder As String = "",
                Ver As String = "", ShowDateCnfid As String = "",
                ShowDelDate As String = "", ShowReqQty As Integer = 0,
                ShowDelQty As String = 0, ShowUndelQty As String = "",
                ShowUnit As String = "", ShowPOSO As String = ""
            If dt1.Rows.Count > 0 Then
                ShowCustID = dt1.Rows(e).Item("xmda004")
                ShowSalesOrder = dt1.Rows(e).Item("xmdadocno")
                Ver = dt1.Rows(e).Item("xmda001")
                ShowDateCnfid = dt1.Rows(e).Item("Cnfidt")
                ShowDelDate = dt1.Rows(e).Item("xmdc012")
                ShowReqQty = dt1.Rows(e).Item("xmdc007")
                ShowDelQty = dt1.Rows(e).Item("xmdd014")
                ShowUnit = dt1.Rows(e).Item("xmdc006")
                ShowPOSO = dt1.Rows(e).Item("POSO")

                XMDA.SOAmountPopUpS(Item, ShowSalesOrder, Ver, dt2)
                Dim ShowSampleQty As String = 0, ShowSampledelQty As String = 0, SONo As String = ""
                If dt2.Rows.Count > 0 Then
                    ShowSampleQty = dt2.Rows(0).Item("xmdc007")
                    ShowSampledelQty = dt2.Rows(0).Item("xmdd014")
                End If

                SONo = ShowSalesOrder & "-" & Ver

                '-- Sum UnDeliveryQty
                Dim SumBalanceQty As Integer = 0,
                    BalanceQty As String = "0"
                SumBalanceQty = (ShowReqQty + ShowSampleQty - ShowDelQty - ShowSampledelQty)
                If SumBalanceQty > 0 Then
                    BalanceQty = SumBalanceQty
                    Dim Comm3 As Decimal = CDec(BalanceQty)
                    BalanceQty = String.Format("{0:n0}", Comm3)
                    If BalanceQty = String.Empty Then
                        BalanceQty = 0
                    End If
                End If
                Dim ReqQty As String = "", DelQty As String = "", SampleQty As String = "", SampledelQty As String = ""
                ReqQty = FormatNumber(ShowReqQty, 0,,, TriState.True)
                DelQty = FormatNumber(ShowDelQty, 0,,, TriState.True)
                SampleQty = FormatNumber(ShowSampleQty, 0,,, TriState.True)
                SampledelQty = FormatNumber(ShowSampledelQty, 0,,, TriState.True)


                DtShow.Rows.Add(New Object() {ShowCustID, SONo, ShowDateCnfid, ShowDelDate, ReqQty, DelQty, SampleQty, SampledelQty, BalanceQty, ShowUnit, ShowPOSO})
            End If
        Next

        'เอาเข้า Gridview 
        GvSO.DataSource = DtShow
        GvSO.DataBind()

        Dim a As Integer = 0
        a = GvSO.Rows.Count
        If a = 0 Then
        Else
            CountRow2.RowCount = ContDtFormOrl.RowGridview(GvSO)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvSO, "#FFCC99")
        End If
    End Sub

    '--ShowGvMO
    Private Sub ShowGvMO(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "SalesOrderNo,MOOrderNo,ComfirmedDate,PlanStartDate,PlanCompletDate,ProductionQty,CompletedQty,ScarpQty,BalanceQty"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt1, dt2 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        '--Show Detail MO PopUp Where item         
        dt1 = SFAA.GetItemRefresh(Item)
        For e As Integer = 0 To dt1.Rows.Count - 1
            Dim ShowSalesOrder As String = "-", ShowVer As String = "-",
                ShowMONo As String = "", ShowCmfidMO As String = "",
                ShowMOQty As String = "", ShowCmpleQty As String = "",
                ShowScarpQty As String = "", ShowBalanceQtyMO As Integer = "0",
                ShowPlanCmple As String = "", BalanceQty As String = ""
            If dt1.Rows.Count > 0 Then
                'ShowSalesOrder = dt1.Rows(e).Item("SONo")
                ShowMONo = dt1.Rows(e).Item("sfaadocno")
                ShowCmfidMO = dt1.Rows(e).Item("ConfrmedDate")
                ShowPlanCmple = dt1.Rows(e).Item("sfaa020")

                ShowMOQty = dt1.Rows(e).Item("sfaa012")
                Dim Comma As Decimal = CDec(ShowMOQty)
                ShowMOQty = String.Format("{0:n0}", Comma)

                ShowCmpleQty = dt1.Rows(e).Item("sfaa050")
                Dim Comma1 As Decimal = CDec(ShowCmpleQty)
                ShowCmpleQty = String.Format("{0:n0}", Comma1)

                ShowScarpQty = dt1.Rows(e).Item("sfaa056")
                Dim Comma3 As Decimal = CDec(ShowScarpQty)
                ShowScarpQty = String.Format("{0:n0}", Comma3)

                ShowBalanceQtyMO = ShowMOQty - ShowCmpleQty - ShowScarpQty
                If ShowBalanceQtyMO > 0 Then
                    BalanceQty = ShowBalanceQtyMO
                    Dim Comma4 As Decimal = CDec(BalanceQty)
                    BalanceQty = String.Format("{0:n0}", Comma4)
                Else
                    If BalanceQty = String.Empty Then
                        BalanceQty = 0
                    End If
                End If
            End If
            DtShow.Rows.Add(New Object() {ShowSalesOrder, ShowVer, ShowMONo, ShowCmfidMO, ShowPlanCmple, ShowMOQty, ShowCmpleQty, ShowScarpQty, BalanceQty})
        Next
        'เอาเข้า Gridview 
        GvMO.DataSource = DtShow
        GvMO.DataBind()

        Dim a As Integer = 0
        a = GvMO.Rows.Count
        If a = 0 Then
        Else
            CountRow3.RowCount = ContDtFormOrl.RowGridview(GvMO)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvMO, "#FFCC99")
        End If
    End Sub

    '--ShowGvPO
    Private Sub ShowGvPO(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "SalesOrderNo,POOrderNo,ComfirmedDate,DeliveryDate,PurchaseQty,RecelvedVolumeQty,BalanceQty"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt1, dt2 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If
        dt2 = PMDN.ShearchPO(Item)
        For l As Integer = 0 To dt2.Rows.Count - 1
            Dim ShowSalesOrder As String = "-", ShowPOOrderNo As String = "", ShowComfirmedDatePO As String = "", ShowDeliveryDatePO As String = "",
                ShowPurchaseQty As String = "", ShowRecelvedVolumeQty As String = "", ShowBalanceQtyMO As String = ""
            If dt2.Rows.Count > 0 Then
                ShowPOOrderNo = dt2.Rows(l).Item("pmdldocno")
                ShowComfirmedDatePO = dt2.Rows(l).Item("CnfDate")
                ShowDeliveryDatePO = dt2.Rows(l).Item("pmdo011")

                ShowPurchaseQty = dt2.Rows(l).Item("pmdo005")
                Dim Comma As Decimal = CDec(ShowPurchaseQty)
                ShowPurchaseQty = String.Format("{0:n0}", Comma)

                ShowRecelvedVolumeQty = dt2.Rows(l).Item("pmdo015")
                Dim Comma1 As Decimal = CDec(ShowRecelvedVolumeQty)
                ShowRecelvedVolumeQty = String.Format("{0:n0}", Comma1)

                '-- Sum UnDeliveryQty
                Dim SumBalanceQty As Integer = 0,
                        BalanceQty As String = "0"
                SumBalanceQty = ShowPurchaseQty - ShowRecelvedVolumeQty
                If SumBalanceQty > 0 Then
                    BalanceQty = SumBalanceQty
                    Dim Comm3 As Decimal = CDec(BalanceQty)
                    BalanceQty = String.Format("{0:n0}", Comm3)
                    If BalanceQty = String.Empty Then
                        BalanceQty = 0
                    End If
                End If
                DtShow.Rows.Add(New Object() {ShowSalesOrder, ShowPOOrderNo, ShowComfirmedDatePO, ShowDeliveryDatePO, ShowPurchaseQty, ShowRecelvedVolumeQty, BalanceQty})
            End If
        Next
        'Next
        'เอาเข้า Gridview 
        GvPO.DataSource = DtShow
        GvPO.DataBind()

        Dim a As Integer = 0
        a = GvPO.Rows.Count
        If a = 0 Then
        Else
            CountRow4.RowCount = ContDtFormOrl.RowGridview(GvPO)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvPO, "#FFCC99")
        End If
    End Sub

End Class