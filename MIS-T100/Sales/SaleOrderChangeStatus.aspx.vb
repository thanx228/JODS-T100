Imports System.Globalization
Imports System.Drawing
Public Class SaleOrderChangeStatus
    Inherits System.Web.UI.Page
    Dim ChkbSelect As WebControls.CheckBox
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    Dim dtshow As New DataTable
    Dim TempSOChacgeStatus As New TempSOChangeStatus

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            Else
                '--ล้างตารางโดยอ้างอิงจาก User ที่ Login เข้าใช้งานหน้านี้ 
                Dim UserID As String = Session("UserName")
                TempSOChacgeStatus.CreateTempSOChacgeStatus(UserID)
            End If
            TabContainer1.ActiveTabIndex = 0
            Panel4.Visible = False
            Panel5.Visible = False
            Panel1.Visible = False
            ReptSOType()
            HeaderFormT1001.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
            HeaderFormT1002.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        Scollbar()
    End Sub

    'Save To Excel File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    '--Seles Order  Type
    Private Sub ReptSOType()
        Dim Dt As DataTable = OOBX.SalesOrderTypeOral()
        With ReptdllSOType
            .DataSource = Dt
            .DataValueField = "oobx001"
            .DataTextField = "SOType"
            .DataBind()
        End With
    End Sub

    '--ShowScollbar สกอบาร์
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvReportHeadScrollbar", "GvReportHeadScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvReportLineScrollbar", "GvReportLineScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvPrintHeadScrollbar", "GvPrintHeadScrollbar();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvPrintLineScrollbar", "GvPrintLineScrollbar();", True)
    End Sub

    '--Control Tab
    Protected Sub TabContainer1_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer1.ActiveTabChanged

        If TabContainer1.ActiveTabIndex = 0 Then
            Panel2.Visible = True
            Panel3.Visible = True
            Panel4.Visible = False
            Panel5.Visible = False

        ElseIf TabContainer1.ActiveTabIndex = 1 Then
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = True
            Panel5.Visible = True
            'DateT1001.Text = Date.Now.ToString("dd/MM/yyyy")
            'DateT1002.Text = Date.Now.ToString("dd/MM/yyyy")
        End If
    End Sub

    '--Search ReptbtnSearch
    Protected Sub ReptbtnSearch_Click(sender As Object, e As EventArgs) Handles ReptbtnSearch.Click
        If TabContainer1.ActiveTabIndex = 0 Then
            '--ShowGvReportHead
            ShowGvReportHead(GvReportHead, True, NowPageIndex.Text)
            CountRow5.RowCount = ContDtFormOrl.RowGridview(GvReportHead)
            '--ShowGvReportLine
            ShowGvReportLine(GvReportLine, True, NowPageIndex.Text)
            CountRow6.RowCount = ContDtFormOrl.RowGridview(GvReportLine)
            Panel4.Visible = False
            Panel5.Visible = False
        End If
    End Sub

    '--ShowGvReportHead
    Private Sub ShowGvReportHead(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)

        If RepttxtSONo.Text = "" Then
            Exit Sub
        End If
        If RepttxtChgVer.Text = "" Then
        End If

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "SalesOrderNo,Rev,Seq,Item,Des,Spec,Qty,Unit,WH"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dtshow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12 As New Data.DataTable
        If dtshow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, dtshow)
        End If

        '--Checking SOChg Header
        XMEE.GetSOChg(ReptdllSOType.SelectedValue, RepttxtSONo.Text, RepttxtChgVer.Text, dt1)
        'วนเช็คข้อมูล ที่ต้องการนำมาโชว์
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim OrderNoHead As String = "",
                ChgVer As String = ""
            If dt1.Rows.Count > 0 Then '
                OrderNoHead = dt1.Rows(j).Item("xmeedocno") '*
                ChgVer = dt1.Rows(j).Item("xmee001") '* จริงๆต้องใช้ตัวนี้
            ElseIf dt1.Rows.Count = 0 Then
                Exit Sub
            End If

            dt2 = XMEG.GetSOChgHead(OrderNoHead, ChgVer)
            Dim OrderNoLine As String = "",
                ItemNoLine As String = "",
                ReqiestQty As String = ""
            If dt2.Rows.Count > 0 Then
                OrderNoLine = dt2.Rows(0).Item("xmegdocno") '*
                ItemNoLine = dt2.Rows(0).Item("xmeg001") '*
                ReqiestQty = dt2.Rows(0).Item("xmeg007") '--8
                Dim Comma As Decimal = CDec(ReqiestQty)
                ReqiestQty = String.Format("{0:n0}", Comma)
            End If

            SFAA.GetOrderNoSOChgLine(OrderNoLine, dt3) '*
            Dim DocNoMOHead As String = "",
                ProductionItemMOHead As String = "",
                SOChg As String = "",
                Version As String = ""
            If dt3.Rows.Count > 0 Then
                DocNoMOHead = dt3.Rows(0).Item("sfaadocno") '*
                ProductionItemMOHead = dt3.Rows(0).Item("sfaa010") '*
                SOChg = dt3.Rows(0).Item("sfaa006") '--1
                Version = dt3.Rows(0).Item("sfaa007") '--2 รองใช้ไปก่อน
            End If

            dt4 = SFAC.GetDocNoMOHead(DocNoMOHead, ProductionItemMOHead)
            Dim DocNoMOLine As String = "",
                ItemNoMOLine As String = "",
                Unit As String = ""
            If dt4.Rows.Count > 0 Then
                DocNoMOLine = dt4.Rows(0).Item("sfacdocno")
                ItemNoMOLine = dt4.Rows(0).Item("sfac001") '--4
                Unit = dt4.Rows(0).Item("sfac004") '--9
            End If

            dt5 = IMAF.GetItemNoMOLine(ItemNoMOLine)
            Dim DefaultWH As String = ""
            If dt5.Rows.Count > 0 Then
                DefaultWH = dt5.Rows(0).Item("WareHouse") '--7
            End If

            dt6 = IMAAL.GetItemNoMOLine(ItemNoMOLine) '*
            Dim ProductItemName As String = "",
                Specifaction As String = ""
            If dt6.Rows.Count > 0 Then
                ProductItemName = dt6.Rows(0).Item("imaal003") '--5
                Specifaction = dt6.Rows(0).Item("imaal004") '--6
            End If

            dt12 = SFAB.GetDocNoMOLine(DocNoMOLine)
            Dim SoureItemSeq As String = ""
            If dt12.Rows.Count > 0 Then
                SoureItemSeq = dt12.Rows(0).Item("sfab004") '--SeqSO --3
            End If

            '--Add data into rows 
            dtshow.Rows.Add(New Object() {SOChg, Version, SoureItemSeq, ItemNoMOLine, ProductItemName, Specifaction, ReqiestQty, Unit, DefaultWH})
        Next

        'เอาเข้า Gridview 
        GvReportHead.DataSource = dtshow
        GvReportHead.DataBind()

        Dim a As Integer = 0
        a = GvReportHead.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvReportHead, "#FFCC99")
        End If
    End Sub

    '--ShowGvReportLine
    Private Sub ShowGvReportLine(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        If RepttxtSONo.Text = "" Then
            Exit Sub
        End If
        If RepttxtChgVer.Text = "" Then
            Exit Sub
        End If

        'ชื่อคอลัมน์หัวตาราง
        Dim ShowFiled As String = "Ststus, DocNoMO, Opreation, OpreationDes, WorkStation, WorkStationName, ItemNoMO, ItemName, Spec, CompletedQty, ScrapQty, DefaultWH"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dtshow, dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13 As New Data.DataTable
        If dtshow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, dtshow)
        End If

        '--Checking SOChg Header
        XMEE.GetSOChg(ReptdllSOType.SelectedValue, RepttxtSONo.Text, RepttxtChgVer.Text, dt1)
        'วนเช็คข้อมูล ที่ต้องการนำมาโชว์
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim OrderNoHead As String = "",
                ChgVer As String = ""
            If dt1.Rows.Count > 0 Then '
                OrderNoHead = dt1.Rows(j).Item("xmeedocno") '*
                ChgVer = dt1.Rows(j).Item("xmee001") '*
            ElseIf dt1.Rows.Count = 0 Then
                Exit Sub
            End If

            dt2 = XMEG.GetSOChgHead(OrderNoHead, ChgVer)
            Dim OrderNoLine As String = "", ItemNoLine As String = "",
                Rowtus As String = ""
            If dt2.Rows.Count > 0 Then
                OrderNoLine = dt2.Rows(0).Item("xmegdocno") '*
                ItemNoLine = dt2.Rows(0).Item("xmeg001") '*
                Rowtus = dt2.Rows(0).Item("RosStus")
            End If

            SFAA.GetOrderNoSOChgLine(OrderNoLine, dt3)
            Dim DocNoMOHead As String = "",
                ProductionItemMOHead As String = "",
                ScrapQty As String = "",
                StusMOHead As String = ""
            If dt3.Rows.Count > 0 Then
                DocNoMOHead = dt3.Rows(0).Item("sfaadocno") '*
                ProductionItemMOHead = dt3.Rows(0).Item("sfaa010") '*
                ScrapQty = dt3.Rows(0).Item("sfaa056") '-11
                Dim Comma As Decimal = CDec(ScrapQty)
                ScrapQty = String.Format("{0:n0}", Comma)
                StusMOHead = dt3.Rows(0).Item("sfaastus") '-1
            End If

            dt4 = SFAC.GetDocNoMOHead(DocNoMOHead, ProductionItemMOHead)
            Dim DocNoMOLine As String = "",
                ItemNoMOLine As String = ""
            If dt4.Rows.Count > 0 Then
                DocNoMOLine = dt4.Rows(0).Item("sfacdocno") '-2
                ItemNoMOLine = dt4.Rows(0).Item("sfac001") '-7
            End If

            dt5 = IMAF.GetItemNoMOLine(ItemNoLine)
            Dim DefaultWH As String = ""
            If dt5.Rows.Count > 0 Then
                DefaultWH = dt5.Rows(0).Item("WareHouse") '-12
            End If

            dt6 = IMAAL.GetItemNoMOLine(ItemNoMOLine)
            Dim ProductItemName As String = "",
                Specifaction As String = ""
            If dt6.Rows.Count > 0 Then
                ProductItemName = dt6.Rows(0).Item("imaal003") '-8
                Specifaction = dt6.Rows(0).Item("imaal004") '-9
            End If

            dt7 = SFCA.GetDocNoMOLine(DocNoMOLine)
            Dim DocNoMOOperatHead As String = "",
                CompletedQty As String = ""
            If dt7.Rows.Count > 0 Then
                DocNoMOOperatHead = dt7.Rows(0).Item("sfcadocno") '*
                CompletedQty = dt7.Rows(0).Item("sfca004") '-10
                Dim Comma As Decimal = CDec(CompletedQty)
                CompletedQty = String.Format("{0:n0}", Comma)
            End If

            dt8 = SFCB.GetDocNoMOOperatHead(DocNoMOOperatHead)
            Dim WorkStation As String = "",
                    Opreation As String = "",
                    DocNoMO As String = ""
            For i As Integer = 0 To dt8.Rows.Count - 1
                If dt8.Rows.Count > 0 Then
                    DocNoMO = dt8.Rows(i).Item("sfcbdocno") '-5
                    WorkStation = dt8.Rows(i).Item("sfcb011") '-5
                    Opreation = dt8.Rows(i).Item("sfcb003") '-3
                End If

                dt9 = ECAA.GetWorkcenter(WorkStation)
                Dim WorkStationName As String = ""
                If dt9.Rows.Count > 0 Then
                    WorkStationName = dt9.Rows(0).Item("ecaa002") '-6
                End If

                dt10 = OOCQL.GetOpreationMOOperatHead(Opreation)
                Dim OperDescription As String = ""
                If dt10.Rows.Count > 0 Then
                    OperDescription = dt10.Rows(0).Item("oocql004") '-4
                End If
                dtshow.Rows.Add(New Object() {StusMOHead, DocNoMO, Opreation, OperDescription, WorkStation, WorkStationName, ItemNoMOLine, ProductItemName, Specifaction, CompletedQty, ScrapQty, DefaultWH})
            Next


        Next

        'เอาเข้า Gridview 
        GvReportLine.DataSource = dtshow
        GvReportLine.DataBind()

        Dim a As Integer = 0
        a = GvReportLine.Rows.Count
        If a = 0 Then
        Else

            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvReportLine, "#FFCC99")
        End If
    End Sub

    '--Search PrtbtnShearch
    Protected Sub PrtbtnShearch_Click(sender As Object, e As EventArgs) Handles PrtbtnShearch.Click
        If TabContainer1.ActiveTabIndex = 1 Then
            '--ShowGvPrintHead
            ShowGvPrintHead(GvPrintHead, True, NowPageIndex.Text)
            CountRow3.RowCount = ContDtFormOrl.RowGridview(GvPrintHead)

            dtshow.Clear()
            GvPrintLine.DataBind()
            CountRow4.RowCount = String.Empty

            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = True
            Panel5.Visible = True
        End If
    End Sub

    '--ShowGvPrintHead
    Private Sub ShowGvPrintHead(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim Whr As String = "",
          Status As String = PrtddlStus.SelectedValue,
          DateFrom As String = DateT1001.Text,
          DateTo As String = DateT1002.Text

        Dim TypeSection As Integer = 0
        Dim SectionList As List(Of [String]) = New List(Of String)()
        For Each sitem As ListItem In UsingSectionSalesCheckList.getObject.Items
            If sitem.Selected Then
                SectionList.Add(sitem.Value)
                TypeSection = +1
            End If
        Next
        Dim TypeSale As Integer = 0
        Dim TypeSaleList As List(Of [String]) = New List(Of String)()
        For Each WCitem As ListItem In UsingTypeSaleCheckList1.getObject.Items
            If WCitem.Selected Then
                TypeSaleList.Add(WCitem.Value)
                TypeSale = +1
            End If
        Next

        Dim pStatus As String = ""
        If Status <> "" Then
            pStatus = XMEE.Status & " = '" & Status.Trim & "'"
            Whr = pStatus
        End If

        If PrttxtCust.Text.Trim <> "" Then
            Dim Cust As TextBox = PrttxtCust
            Whr = Whr & " And " & XMEE.CustomerNo & " in('" & GetData.GetCust(Cust) & "')"
        End If

        Whr = Whr & " And " & GetData.ora_dateselector(XMEE.ChangedDate, DateFrom, DateTo) & ""

        If (TypeSale > 0) And (TypeSection <= 0) Then
            Dim TypeSL As String = " '" & [String].Join("' , '", TypeSaleList.ToArray())
            Whr = Whr & " And " & "substr(" & XMEE.DocNo & ",3,4)" & " in(" & [String].Join("','", TypeSL) & "')"
        End If

        If (TypeSection > 0) And (TypeSale <= 0) Then
            Dim Section As String = " '" & [String].Join("' , '", SectionList.ToArray())
            Whr = Whr & " And " & XMEE.SalesDepartment & " in(" & [String].Join("','", Section) & "')"
        End If

        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "Section,CustID,CustName,SalesOrderChg,Version,ChgDate,Reason,Status"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim dtshow, dt, dt1, dt2, dt3, dt4, dt5 As New Data.DataTable
        If dtshow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, dtshow)
        End If

        '--Checking SOChg Header
        XMEE.GetWhrSOChgHead(Whr, dt1)
        'วนเช็คข้อมูล ที่ต้องการนำมาโชว์
        For j As Integer = 0 To dt1.Rows.Count - 1
            Dim OrderNoHead As String = "",
                ChgVer As String = "",
                ChgDate As String = "",
                CustID As String = "",
                Section As String = "",
                Stus As String = "",
                ReasonofChange As String = ""
            If dt1.Rows.Count > 0 Then '
                OrderNoHead = dt1.Rows(j).Item("xmeedocno") 'Key
                ChgVer = dt1.Rows(j).Item("xmee001") 'Key
                ChgDate = dt1.Rows(j).Item("xmee902") '*
                Section = dt1.Rows(j).Item("xmee003") 'Key
                CustID = dt1.Rows(j).Item("xmee004") 'key
                Stus = dt1.Rows(j).Item("xmeestus") '*
                ReasonofChange = dt1.Rows(j).Item("xmee903").ToString '*
            ElseIf dt1.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim Statuss As String = ""
            If Stus = "Y" Then
                Statuss = "Approved"
            ElseIf Stus = "U" Then
                Statuss = "UnApproved"
            End If

            dt2 = OOEFL.GetSectionSLRrfesh(Section)
            Dim SectionID As String = "",
                SectionName As String = ""
            If dt2.Rows.Count > 0 Then
                SectionID = dt2.Rows(0).Item("ooefl001")
                SectionName = dt2.Rows(0).Item("ooefl004").ToString '*
            End If

            dt3 = PMAAL.GetDataCustomerRefresh(CustID)
            Dim CustomerID As String = "",
                CustomerName As String = ""
            If dt3.Rows.Count > 0 Then
                CustomerID = dt3.Rows(0).Item("pmaal001") '*
                CustomerName = dt3.Rows(0).Item("pmaal004") '*
            End If

            If dt1.Rows.Count > 0 Then
                '--Add data into rows
                dtshow.Rows.Add(New Object() {SectionName, CustomerID, CustomerName, OrderNoHead, ChgVer, ChgDate, ReasonofChange, Statuss})
            End If
        Next

        'เอาเข้า Gridview 
        GvPrintHead.DataSource = dtshow
        GvPrintHead.DataBind()

        Dim a As Integer = 0
        a = GvPrintHead.Rows.Count
        If a = 0 Then
            GvPrintHead.Visible = False
            GvPrintHead.DataBind()
        Else
            GvPrintHead.Visible = True
            GvPrintHead.DataBind()
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvPrintHead, "#FFCC99")
        End If
    End Sub

    '--Checkbox Select All
    Protected Sub cbAll_CheckedChanged(sender As Object, e As EventArgs)
        If TabContainer1.ActiveTabIndex = 1 Then
            Dim ChkBoxHeader As CheckBox = CType(GvPrintHead.HeaderRow.FindControl("cbAll"), CheckBox)
            For Each row As GridViewRow In GvPrintHead.Rows
                Dim ChkBoxRows As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                If ChkBoxHeader.Checked = True Then
                    ChkBoxRows.Checked = True
                Else
                    ChkBoxRows.Checked = False
                End If
            Next
        End If
    End Sub

    '--BtnShow
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        ShowGvPrintLine(GvReportLine, True, NowPageIndex.Text)
        CountRow4.RowCount = ContDtFormOrl.RowGridview(GvPrintLine)
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = True
        Panel5.Visible = True
    End Sub

    '--ShowGvPrintLine
    Private Sub ShowGvPrintLine(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)

        Dim seq As Integer = 1
        Dim UserID As String = Session("UserName")
        Dim dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8 As New Data.DataTable

        Dim ShowFiled As String = "Section,CustID,CustName,SalesOrderChg,Version,ChgDate,Item,Spec,ItemDes,QtyOld,QtyNew,PlanDelDateOld,PlanDelDateNew,CustPONo,Reason,Remark,Status"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        If dtshow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, dtshow)
        End If

        For l As Integer = 0 To GvPrintHead.Rows.Count - 1
                Dim Section As String = GvPrintHead.Rows(l).Cells(1).Text,
                    CustID As String = GvPrintHead.Rows(l).Cells(2).Text,
                    CustName As String = GvPrintHead.Rows(l).Cells(3).Text,
                    SalesOrderChg As String = GvPrintHead.Rows(l).Cells(4).Text,
                    Version As String = GvPrintHead.Rows(l).Cells(5).Text,
                    ChgDate As String = GvPrintHead.Rows(l).Cells(6).Text,
                    Status As String = GvPrintHead.Rows(l).Cells(8).Text

                ChkbSelect = GvPrintHead.Rows(l).FindControl("chkSelect")

            If ChkbSelect.Checked = True Then
                dt4 = XMEG.GetSOChgHead(SalesOrderChg, Version)
                Dim OrderNoLine As String = "", ChgVerLine As String = ""
                For i As Integer = 0 To dt4.Rows.Count - 1
                    Dim ItemSeq As String = "", ItemNoLine As String = "",
                        ChangeReasonCode As String = "", Unit As String = "",
                        ReqiestQty As String = "", POSOChg As String = "",
                        OldAgreedDeliveryDate As String = "", SOChgVerLine As String = "",
                        ReqiestQtyOld As String = "", PlanDelDateOld As String = "",
                        ChangeNotes As String = "", ProductItemName As String = "",
                        Specifaction As String = ""
                    Dim OperDescription As String = ""
                    If dt4.Rows.Count > 0 Then
                        OrderNoLine = dt4.Rows(i).Item("xmegdocno")  '*
                        ChgVerLine = dt4.Rows(i).Item("xmeg900") '*
                        ItemSeq = dt4.Rows(i).Item("xmegseq")
                        ItemNoLine = dt4.Rows(i).Item("xmeg001") '*
                        Unit = dt4.Rows(i).Item("xmeg006")
                        ReqiestQty = dt4.Rows(i).Item("xmeg007") '*
                        Dim Comma As Decimal = CDec(ReqiestQty)
                        ReqiestQty = String.Format("{0:n0}", Comma)
                        OldAgreedDeliveryDate = dt4.Rows(i).Item("xmeg012") '*
                        ChangeReasonCode = dt4.Rows(i).Item("xmeg902").ToString '*
                        POSOChg = dt4.Rows(i).Item("xmeg050").ToString '*
                        ChangeNotes = dt4.Rows(i).Item("xmeg903").ToString '*
                    Else
                        If ChangeNotes = "" Then
                            ChangeNotes = "-"
                        End If
                        If POSOChg = "" Then
                            POSOChg = "-"
                        End If
                        If ChangeReasonCode = "" Then
                            ChangeReasonCode = "-"
                        End If
                    End If

                    If ChgVerLine > 1 Then
                        SOChgVerLine = ChgVerLine - 1
                        dt6 = XMEG.GetSOChgHead(OrderNoLine, SOChgVerLine)
                        ReqiestQtyOld = dt6.Rows(0).Item("xmeg007") '*'
                        Dim Comma As Decimal = CDec(ReqiestQtyOld)
                        ReqiestQtyOld = String.Format("{0:n0}", Comma)
                        If ReqiestQtyOld = ReqiestQty Then
                            ReqiestQtyOld = "-"
                        End If
                        PlanDelDateOld = dt6.Rows(0).Item("xmeg012") '*
                        If PlanDelDateOld = OldAgreedDeliveryDate Then
                            PlanDelDateOld = "-"
                        End If
                    Else
                        PlanDelDateOld = "-"
                        ReqiestQtyOld = "-"
                    End If

                    dt5 = IMAAL.GetItemNoMOLineRrfesh(ItemNoLine) '*
                    If dt5.Rows.Count > 0 Then
                        ProductItemName = dt5.Rows(0).Item("imaal003") '*
                        Specifaction = dt5.Rows(0).Item("imaal004") '*
                    End If

                    dt7 = OOCQL.GetOpreationMOOperatHeadRefesh(ChangeReasonCode)
                    If dt7.Rows.Count > 0 Then
                        OperDescription = dt7.Rows(0).Item("oocql004").ToString
                    Else
                        OperDescription = "-"
                    End If
                    '--บันทึกข้อมุลลง TempSOChacgeStatus
                    TempSOChacgeStatus.InserSOChacgeStatus(seq, Section, CustID, CustName, OrderNoLine, ChgVerLine, ChgDate, ItemNoLine, Specifaction, ProductItemName, ReqiestQtyOld, ReqiestQty, PlanDelDateOld, OldAgreedDeliveryDate, POSOChg, OperDescription, ChangeNotes, Status, UserID)
                    seq += 1
                Next

                TempSOChacgeStatus.SelectTempSOChacgeStus(OrderNoLine, ChgVerLine, UserID, dt8)
                For x As Integer = 0 To dt8.Rows.Count - 1
                    With dt8.Rows(x)
                        Dim Sections As String = "", CustomerID As String = "",
                            CustomerName As String = "", SalesOrder As String = "",
                            Ver As String = "", ChangeDate As String = "",
                            Item As String = "", Spec As String = "",
                            ItemDes As String = "", QtyOld As String = "",
                            QtyNew As String = "", DelDateOld As String = "",
                            DelDateNew As String = "", PONo As String = "",
                            ForecastNo As String = "", Reason As String = "",
                            Remark As String = "", Stus As String = ""
                        If dt8.Rows.Count > 0 Then
                            Sections = .Item("Section")
                            CustomerID = .Item("CustID")
                            CustomerName = .Item("CustName")
                            SalesOrder = .Item("SalesOrder")
                            Ver = .Item("Ver")
                            ChangeDate = .Item("ChgDate")
                            Item = .Item("Item")
                            Spec = .Item("Spec")
                            ItemDes = .Item("ItemDes")
                            QtyOld = .Item("QtyOld")
                            QtyNew = .Item("QtyNew")
                            DelDateOld = .Item("PlanDelDateOld")
                            DelDateNew = .Item("PlanDelDateNew")
                            PONo = .Item("CustPONo")
                            Reason = .Item("Reason")
                            Remark = .Item("Remark")
                            Stus = .Item("Stus")
                            dtshow.Rows.Add(New Object() {Sections, CustomerID, CustomerName, SalesOrder, Ver, ChangeDate, Item, ItemDes, Spec, QtyOld, QtyNew, DelDateOld, DelDateNew, PONo, Reason, Remark, Stus})
                        End If
                    End With
                Next
            End If

            'Input Gridview 
            GvPrintLine.DataSource = dtshow
                GvPrintLine.DataBind()

                Dim a As Integer = 0
                a = GvPrintLine.Rows.Count
                If a = 0 Then
                    Panel1.Visible = False
                Else
                    Panel1.Visible = True
                    '--Gridview row color Onmouse
                    GridviewUtility.GrigOnmouseHandleCustomer(GvPrintLine, "#FFCC99")
                End If
            Next

    End Sub

    '--Print
    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim Username As String = Session("UserName")
        Dim paraName As String = ""
        paraName = "username:  " & Username
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&f=SLReport&ReportName=SOChangeStatus.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    '--ExcelExport
    Protected Sub PrinttbtnExport_Click(sender As Object, e As EventArgs) Handles PrinttbtnExport.Click
        PrtbtnShearch_Click(sender, e)
        ReptbtnSearch_Click(sender, e)
        GvPrintLine.Visible = True
        GvReportLine.Visible = True
        If TabContainer1.ActiveTabIndex = 0 Then
            ContDtFormOrl.ExportGridViewToExcel("SaleOrderChangeStatusReport", GvReportLine)
        Else
            ContDtFormOrl.ExportGridViewToExcel("SaleOrderChangeStatusReport", GvPrintLine)
        End If
    End Sub


End Class