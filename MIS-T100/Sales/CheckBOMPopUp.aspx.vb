Imports System.Globalization
Imports System.Drawing
Public Class CheckBOMPopUp
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim connect As New clsDBConnect
    Dim TempChkBOM As New TempChkBOM
    Dim Item As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else

            End If
        End If
        Item = Request.QueryString("item").ToString.Trim
        Dim Spec As String = Request.QueryString("Spec").ToString.Trim

        lblItem.Text = Item
        lblSpec.Text = Spec
        Dim UserID As String = Session("UserName")

        TempChkBOM.createTempChkBOM(UserID)
        TempChkBOM.createTempSubChkBOM(UserID)

        ShowGvBomDetail(GvBomDetail, True, NowPageIndex.Text)
        ShowGvBomStock(GvBomStock, True, NowPageIndex.Text)
    End Sub

    '--ShowGvBomDetail
    Private Sub ShowGvBomDetail(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "Level,Seq,Item,Desc,Spec,QPA,Unit,SupplyStrategy"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        If Item <> "" Then
            dt = IMAA.ItemDetail(Item)
            Dim Itembom As String = "", ItemName As String = "",
                Spec As String = "", Unit As String = "",
                SupplyStrategy As String = ""
            If dt.Rows.Count > 0 Then
                Itembom = dt.Rows(0).Item("imaa001")
                ItemName = dt.Rows(0).Item("imaal003")
                Spec = dt.Rows(0).Item("imaal004")
                Unit = dt.Rows(0).Item("imaa006")
                SupplyStrategy = dt.Rows(0).Item("Supply")

                TempChkBOM.Insertitembom(Itembom, ItemName, Spec, Unit, SupplyStrategy, UserID)
            End If
        Else
            Exit Sub
        End If

        Level(Item)

        TempChkBOM.SearchBOMPopUp(UserID, dt1)
        For i As Integer = 0 To dt1.Rows.Count - 1
            Dim ShowLevel As String = "", ShowSeq As String = "",
                ShowItem As String = "", ShowDesc As String = "",
                ShowSpec As String = "", ShowQPA As String = "",
                ShowUnit As String = "", ShowSupplyStrategy As String = ""
            If dt1.Rows.Count > 0 Then
                ShowLevel = dt1.Rows(i).Item("Level")
                ShowSeq = dt1.Rows(i).Item("Seq")
                ShowItem = dt1.Rows(i).Item("Item")
                ShowDesc = dt1.Rows(i).Item("Descr")
                ShowSpec = dt1.Rows(i).Item("Spec")
                ShowQPA = dt1.Rows(i).Item("QPA")
                ShowUnit = dt1.Rows(i).Item("Unit")
                ShowSupplyStrategy = dt1.Rows(i).Item("Sply")
                DtShow.Rows.Add(New Object() {ShowLevel, ShowSeq, ShowItem, ShowDesc, ShowSpec, ShowQPA, ShowUnit, ShowSupplyStrategy})
            End If
        Next


        'เอาเข้า Gridview 
        GvBomDetail.DataSource = DtShow
        GvBomDetail.DataBind()

        Dim a As Integer = 0
        a = GvBomDetail.Rows.Count
        If a = 0 Then
        Else
            CountRow1.RowCount = ContDtFormOrl.RowGridview(GvBomDetail)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvBomDetail, "#FFCC99")
        End If
    End Sub

    '--Chaeck sub part Item BOM
    Protected Sub Level(code As String, Optional l As String = "")
        Dim UserID As String = Session("UserName")
        Dim dt, dt1, dt2, dt3 As New Data.DataTable

        If l = "" Then 
            dt1 = BMAA.BomDetl(code)
            For e As Integer = 0 To dt1.Rows.Count - 1
                Dim ShowLevel As String = "", ShowSeq As String = "",
                    ShowDesc As String = "", ShowSpec As String = "",
                    ShowQPA As String = "", ShowUnit As String = "",
                    ShowSupplyStrategy As String = "", ShowItem As String = ""
                If dt1.Rows.Count > 0 Then
                    ShowSeq = dt1.Rows(e).Item("bmba009")
                    ShowItem = dt1.Rows(e).Item("bmba003")
                    ShowDesc = dt1.Rows(e).Item("imaal003")
                    ShowSpec = dt1.Rows(e).Item("imaal004")
                    ShowQPA = dt1.Rows(e).Item("bmba011")
                    ShowUnit = dt1.Rows(e).Item("bmba010")
                    ShowSupplyStrategy = dt1.Rows(e).Item("Supply")
                    TempChkBOM.InsertBOMDetail(ShowSeq, ShowItem, ShowDesc, ShowSpec, ShowQPA, ShowUnit, ShowSupplyStrategy, UserID)
                End If
            Next
            l = code
        End If

        dt2 = BMAA.BomDetl(code)
        For i As Integer = 0 To dt2.Rows.Count - 1
            With dt2.Rows(i)
                Dim SearchItem As String = "", MixItem As String = "",
                SearchSply As String = ""
                If dt2.Rows.Count > 0 Then
                    SearchItem = dt2.Rows(i).Item("bmba003")
                    SearchSply = dt2.Rows(i).Item("Supply")
                    MixItem = l & Chr(8) & SearchItem

                    TempChkBOM.UpdateMixItemPopUp(MixItem, SearchItem, UserID, dt3)

                    If SearchSply = "2" Then
                        Level(SearchItem)
                    End If
                End If
            End With
        Next
    End Sub

    '--ShowGvBomStock
    Private Sub ShowGvBomStock(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim UserID As String = Session("UserName")
        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "Item, Desc, Spec, StockQty, IssueQty, MOQty, POQty, PRQty, SOQty, FixedLeadTiem"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If
        '--SearcgSubitemBOM
        TempChkBOM.SearchBOMPopUp(UserID, dt)
        For j As Integer = 0 To dt.Rows.Count - 1
            Dim SubitemBOM As String = ""
            If dt.Rows.Count > 0 Then
                SubitemBOM = dt.Rows(j).Item("Item")
            Else
                Exit Sub
            End If

            '-- Sum ItemStock  where Item
            dt1 = IMAAL.SumSTChkBom(SubitemBOM)
            Dim StockQty As String = ""
            Dim DefaultWH As String = ""
            If dt1.Rows.Count > 0 Then
                StockQty = dt1.Rows(0).Item("StockQty")
                DefaultWH = dt1.Rows(0).Item("inag004")
            End If

            '--Sum POQty  where Item
            dt2 = PMDN.SumPOQty(SubitemBOM)
            Dim POQty As String = "0"
            If dt2.Rows.Count > 0 Then
                POQty = dt2.Rows(0).Item("POQty")
                Dim Comma As Decimal = CDec(POQty)
                POQty = String.Format("{0:n0}", Comma)
            End If

            '--Sum MO  where Item
            dt3 = SFAA.SumMO(SubitemBOM)
            Dim MOQty As String = "0"
            If dt3.Rows.Count > 0 Then
                MOQty = dt3.Rows(0).Item("MOQty")
                Dim Comma As Decimal = CDec(MOQty)
                MOQty = String.Format("{0:n0}", Comma)
            End If

            '--Sum SO RequestQty
            dt4 = XMDC.SumSOReqQty(SubitemBOM)
            Dim SOReqQty As Integer = 0
            If dt4.Rows.Count > 0 Then
                SOReqQty = dt4.Rows(0).Item("SOReqQty")
                Dim Comma As Decimal = CDec(SOReqQty)
                SOReqQty = String.Format("{0:n0}", Comma)
            End If

            '--Sum DeliveryQty where ItemNo
            dt5 = XMDC.SumDeliQty(SubitemBOM)
            Dim DeliQty As Integer = 0
            If dt5.Rows.Count > 0 Then
                DeliQty = dt5.Rows(0).Item("DeliveryQty")
                Dim Comma As Decimal = CDec(DeliQty)
                DeliQty = String.Format("{0:n0}", Comma)
            End If

            '--Sum UnDeliveryQty
            Dim SumUndelQty As Integer = 0, UndelQty As String = ""
            SumUndelQty = (SOReqQty - DeliQty)
            If SumUndelQty > 0 Then
                UndelQty = SumUndelQty
                Dim Comma As Decimal = CDec(UndelQty)
                UndelQty = String.Format("{0:n0}", Comma)
            Else
                If UndelQty = String.Empty Then
                    UndelQty = 0
                End If
            End If

            Dim PRQty As String = "0"

            '--Search Desc Spec Where Item
            dt6 = IMAAL.GetItem(SubitemBOM)
            Dim ProductItemName As String = "", Specifaction As String = ""
            If dt6.Rows.Count > 0 Then
                ProductItemName = dt6.Rows(0).Item("imaal003")
                Specifaction = dt6.Rows(0).Item("imaal004")
            End If

            '--Sum IssueQty Where Item
            dt7 = SFBA.SumIssue(SubitemBOM)
            Dim IssueQty As String = "0"
            If dt7.Rows.Count > 0 Then
                IssueQty = dt7.Rows(0).Item("BalIssueQty")
            End If

            dt8 = IMAF.FixedLeadTiem(SubitemBOM)
            Dim LeadTiem As String = ""
            If dt8.Rows.Count > 0 Then
                LeadTiem = dt8.Rows(0).Item("LeadTime")
            End If


            DtShow.Rows.Add(New Object() {SubitemBOM, ProductItemName, Specifaction, StockQty, IssueQty, MOQty, POQty, PRQty, UndelQty, LeadTiem})

            TempChkBOM.InsertSubBom(SubitemBOM, ProductItemName, Specifaction, StockQty, IssueQty, MOQty, POQty, PRQty, UndelQty, LeadTiem, UserID)
        Next

        'เอาเข้า Gridview 
        GvBomStock.DataSource = DtShow
        GvBomStock.DataBind()

        Dim a As Integer = 0
        a = GvBomStock.Rows.Count
        If a = 0 Then

        Else
            CountRow1.RowCount = ContDtFormOrl.RowGridview(GvBomStock)
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvBomStock, "#FFCC99")
            '##--delete from TempChkBOM Where UserID
            TempChkBOM.DeleteTempChkBOM(UserID)
        End If
    End Sub

    '--HyperLink GvBomStock
    Protected Sub GvBomStock_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvBomStock.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                Dim item As String = .DataItem("Item")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("Item")) Then
                    Dim link As String = ""
                    link = link & "&Item=" & .DataItem("Item")
                    hplDetail.NavigateUrl = "CheckBOMPopupSub.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                    hplDetail.Target = "_blank"
                End If
            End If
        End With
    End Sub
End Class