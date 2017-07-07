Public Class CheckBOMPopupSub
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim connect As New clsDBConnect
    Dim TempChkBOM As New TempChkBOM
    Dim Item As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserID As String = Session("UserName")
        Dim dt, dt1, dt2, dt3, dt4, dt5 As New DataTable
        Item = Request.QueryString("item").ToString.Trim
        lblItem.Text = Item

        '--SearchSubBOMPopUp
        dt = TempChkBOM.SearchSubBOMPopUp(Item, UserID)
        If dt.Rows.Count > 0 Then
            lblItem.Text = dt.Rows(0).Item("Item")
            lblSpec.Text = dt.Rows(0).Item("Spec")
            lblIssueQty.Text = dt.Rows(0).Item("IsQty")
            lblSTQty.Text = dt.Rows(0).Item("STQty")
            lblSOQty.Text = dt.Rows(0).Item("SOQty")
            lblMOQty.Text = dt.Rows(0).Item("MOQty")
            lblPOQty.Text = dt.Rows(0).Item("POQty")
            lblPRQty.Text = dt.Rows(0).Item("PRQty")
        End If
        Scollbar()
        ShowGvSO(GvSO, True, NowPageIndex.Text)
        ShowGvMatIssue(GvMatIssue, True, NowPageIndex.Text)
        ShowGvMO(GvMO, True, NowPageIndex.Text)
        ShowGvPO(GvPO, True, NowPageIndex.Text)
    End Sub

    '--ShowScollbar สกอบาร์ 
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvSOScrollbar", "GvSOScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvMatIssueScrollbar", "GvMatIssueScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvMOScrollbar", "GvMOScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvPOScrollbar", "GvPOScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvPRScrollbar", "GvPRScrollbar();", True)
    End Sub

    '--ShowGvSO
    Private Sub ShowGvSO(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = " CustID, SONo, Ver, Cnfdt, Deldt, SOQty, DelQty, BalacneQty, Unit"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        dt1 = XMDC.SOSupBomPopUp(Item)
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim CustID As String = "", SO As String = "-",
                Ver As String = "-", Cnfdt As String = "",
                Deldt As String = "", SOQty As String = "",
                DelQty As String = "-", BalacneQty As String = "", Unit As String = ""
            If dt1.Rows.Count > 0 Then
                CustID = dt1.Rows(j).Item("xmda004")
                SO = dt1.Rows(j).Item("xmdcdocno")
                Ver = dt1.Rows(j).Item("xmda001")
                Cnfdt = dt1.Rows(j).Item("Cnfdt")
                Deldt = dt1.Rows(j).Item("Deldt")

                SOQty = dt1.Rows(j).Item("xmdc007")
                Dim Comma As Decimal = CDec(SOQty)
                SOQty = String.Format("{0:n0}", Comma)

                DelQty = dt1.Rows(j).Item("xmdd014")
                Dim Comma1 As Decimal = CDec(DelQty)
                DelQty = String.Format("{0:n0}", Comma1)

                BalacneQty = dt1.Rows(j).Item("BalQty")
                Dim Comma2 As Decimal = CDec(BalacneQty)
                BalacneQty = String.Format("{0:n0}", Comma2)

                Unit = dt1.Rows(j).Item("xmdc006")
            Else
                Exit Sub
            End If


            DtShow.Rows.Add(New Object() {CustID, SO, Ver, Cnfdt, Deldt, SOQty, DelQty, BalacneQty, Unit})
        Next

        'เอาเข้า Gridview 
        GvSO.DataSource = DtShow
        GvSO.DataBind()

        Dim a As Integer = 0
        a = GvSO.Rows.Count
        If a = 0 Then

        Else
            CountRow1.RowCount = ContDtFormOrl.RowGridview(GvSO)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvSO, "#FFCC99")
        End If
    End Sub

    '--ShowGvIssue
    Private Sub ShowGvMatIssue(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "CustID, SONo, Ver, MONo, PlanStart, MOReqQty, PlanMatdt, MatReqQty, IssueQty,BalacneQty,SubItem,Desc"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        dt1 = SFBA.SumUnissue(Item)
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim CustID As String = "-", SO As String = "-",
                Ver As String = "-", MO As String = "",
                PlanStrt As String = "", MOReqQty As String = "",
                PlanMatdt As String = "-", MatReqQty As String = "",
                IsQty As String = "", BalIsQty As String = "",
                SubBom As String = "", Desc As String = ""
            If dt1.Rows.Count > 0 Then
                MO = dt1.Rows(j).Item("sfbadocno")
                PlanStrt = dt1.Rows(j).Item("PlanStrt")

                MOReqQty = dt1.Rows(j).Item("sfaa012")
                Dim Comma As Decimal = CDec(MOReqQty)
                MOReqQty = String.Format("{0:n0}", Comma)

                MatReqQty = dt1.Rows(j).Item("sfba013")
                Dim Comma1 As Decimal = CDec(MatReqQty)
                MatReqQty = String.Format("{0:n0}", Comma1)

                IsQty = dt1.Rows(j).Item("sfba016")
                Dim Comma3 As Decimal = CDec(IsQty)
                IsQty = String.Format("{0:n0}", Comma3)

                BalIsQty = dt1.Rows(j).Item("IssueQty")
                Dim Comma2 As Decimal = CDec(BalIsQty)
                BalIsQty = String.Format("{0:n0}", Comma2)

                SubBom = dt1.Rows(j).Item("sfba005")
                Desc = dt1.Rows(j).Item("imaal004")
            Else
                Exit Sub
            End If


            DtShow.Rows.Add(New Object() {CustID, SO, Ver, MO, PlanStrt, MOReqQty, PlanMatdt, MatReqQty, IsQty, BalIsQty, SubBom, Desc})

        Next

        'เอาเข้า Gridview 
        GvMatIssue.DataSource = DtShow
        GvMatIssue.DataBind()

        Dim a As Integer = 0
        a = GvMatIssue.Rows.Count
        If a = 0 Then

        Else
            CountRow2.RowCount = ContDtFormOrl.RowGridview(GvMatIssue)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvMatIssue, "#FFCC99")
        End If
    End Sub

    '--ShowGvMO
    Private Sub ShowGvMO(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = " SONo, Ver, MONo, Cnfdt, PlanCmp, MOReqQty, CmpQty, ScarpQty, BalacneQty"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        dt1 = SFBA.SumUnMO(Item)
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim SO As String = "-", Ver As String = "-",
                Cnfdt As String = "", MO As String = "",
                PlanCmp As String = "", MOReqQty As String = "",
                CmpQty As String = "-", BalacneQty As String = "", ScarpQty As String = ""
            If dt1.Rows.Count > 0 Then
                MO = dt1.Rows(j).Item("sfbadocno")
                Cnfdt = dt1.Rows(j).Item("Cnfdt")
                PlanCmp = dt1.Rows(j).Item("PlanCmp")

                MOReqQty = dt1.Rows(j).Item("sfaa012")
                Dim Comma As Decimal = CDec(MOReqQty)
                MOReqQty = String.Format("{0:n0}", Comma)

                CmpQty = dt1.Rows(j).Item("sfaa050")
                Dim Comma1 As Decimal = CDec(CmpQty)
                CmpQty = String.Format("{0:n0}", Comma1)

                ScarpQty = dt1.Rows(j).Item("sfaa056")
                Dim Comma3 As Decimal = CDec(CmpQty)
                CmpQty = String.Format("{0:n0}", Comma3)

                BalacneQty = dt1.Rows(j).Item("BalMOQty")
                Dim Comma2 As Decimal = CDec(BalacneQty)
                BalacneQty = String.Format("{0:n0}", Comma2)
            Else
                Exit Sub
            End If


            DtShow.Rows.Add(New Object() {SO, Ver, MO, Cnfdt, PlanCmp, MOReqQty, CmpQty, ScarpQty, BalacneQty})

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
        Dim ShowFiled As String = " SONo, Ver, PONo, Cnfdt, Deldt, POQty, DelQty, BalacneQty, Vendor"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        dt = PMDN.SupBomPopUPPO(Item)
        For j As Integer = 0 To dt.Rows.Count - 1
            Dim SO As String = "-", Ver As String = "-",
                PONo As String = "", Cnfdt As String = "",
                Deldt As String = "", POQty As String = "",
                DelQty As String = "-", BalacneQty As String = "",
                Vendor As String = ""
            If dt.Rows.Count > 0 Then
                'SO = dt.Rows(j).Item("")
                ' Ver = dt.Rows(j).Item("")
                PONo = dt.Rows(j).Item("PONo")
                Cnfdt = dt.Rows(j).Item("Cnfdt")
                Deldt = dt.Rows(j).Item("DelQty")
                Vendor = dt.Rows(j).Item("Vendor")

                POQty = dt.Rows(j).Item("pmdo005")
                Dim Comma As Decimal = CDec(POQty)
                POQty = String.Format("{0:n0}", Comma)

                DelQty = dt.Rows(j).Item("pmdo015")
                Dim Comma1 As Decimal = CDec(DelQty)
                DelQty = String.Format("{0:n0}", Comma1)

                BalacneQty = dt.Rows(j).Item("BalQty")
                Dim Comma2 As Decimal = CDec(BalacneQty)
                BalacneQty = String.Format("{0:n0}", Comma2)
            Else
                Exit Sub
            End If


            DtShow.Rows.Add(New Object() {SO, Ver, PONo, Cnfdt, Deldt, POQty, DelQty, BalacneQty, Vendor})

        Next

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