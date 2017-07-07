Imports System.Data.OracleClient
Imports System.Drawing

Public Class Scrap
    Inherits System.Web.UI.Page
    Dim Conn_sql As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim ControlForm As New ControlDataForm
    Private registerCount As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Dim TempTable As String = "TempScrap" & Session("UserName")
            'CreateTempTable.createScrap(TempTable)
            BuExcel.Visible = False
            btSelect.Visible = False
            btPrint.Visible = False
            BuExcel0.Visible = False
            btSelect0.Visible = False
            btPrint0.Visible = False
            btPrintAMP0.Visible = False
            btPrintAMP.Visible = False
            PanelTabReport.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            If e.Row.Cells(5).Text <> String.Empty Then
                If e.Row.Cells(5).Text = "C" Then
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.Green
                ElseIf e.Row.Cells(5).Text = "M" Then
                    e.Row.Cells(5).BackColor = System.Drawing.Color.Wheat
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.Black
                ElseIf e.Row.Cells(5).Text = "N" Then
                    e.Row.Cells(5).ForeColor = System.Drawing.Color.Maroon
                ElseIf e.Row.Cells(5).Text = "Y" Then
                    e.Row.Cells(5).ForeColor = System.Drawing.ColorTranslator.FromHtml("#b35900")
                End If
                e.Row.Cells(5).Text = StatusT100.MO_Normal(e.Row.Cells(5).Text)
            End If
            Dim RecardDetail As String = e.Row.Cells(10).Text
            If RecardDetail <> String.Empty Then
                If RecardDetail = "1" Then
                    e.Row.Cells(10).Text = "1 : GENERAL"
                Else
                    e.Row.Cells(10).Text = RecardDetail & " : REWORK"
                End If
            End If
        End If
            If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.BorderColor = System.Drawing.Color.Black
            e.Row.Font.Bold = True
        End If
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call GetDataTable()
    End Sub
    'Private Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        Dim colCount As Integer = e.Row.Cells.Count
    '        Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
    '        Dim Header0 As New TableCell()
    '        ' Dim FindeMO As String = lbMO.Text
    '        'Dim StrMO_No As String = "<span style='color:Blue;'>" & FindeMO & "</span>"
    '        Header0.Text = ""
    '        Header0.Font.Bold = True
    '        Header0.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        Header0.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        Header0.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        Header0.ColumnSpan = 1
    '        Header0.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(Header0)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)
    '        Dim HeaderSaleOrder As New TableCell()
    '        ' Dim FindeMO As String = lbMO.Text
    '        Dim StrSOo As String = "<span style='color:#3F3E3E;'>SaleOrder & SaleOrder Forecast (axmt500 )</span>"
    '        HeaderSaleOrder.Text = vbTab & vbTab & StrSOo
    '        HeaderSaleOrder.Font.Bold = True
    '        HeaderSaleOrder.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        HeaderSaleOrder.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        HeaderSaleOrder.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        HeaderSaleOrder.ColumnSpan = 3
    '        HeaderSaleOrder.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(HeaderSaleOrder)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)
    '        ' Dim HeaderRow1 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
    '        Dim HeaderMO As New TableCell()
    '        Dim StrMO As String = "<span style='color:#3F3E3E;'>Manufacture Order (asft300 : Maintain Work Order)</span>"
    '        HeaderMO.Text = vbTab & vbTab & StrMO
    '        HeaderMO.Font.Bold = True
    '        HeaderMO.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        HeaderMO.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        HeaderMO.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        HeaderMO.ColumnSpan = 7
    '        HeaderMO.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(HeaderMO)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

    '        Dim HeaderMO_Process As New TableCell()
    '        Dim StrMO_PO As String = "<span style='color:#3F3E3E;'>MO Operation (asft301 : WO Process Maintain)</span>"
    '        HeaderMO_Process.Text = vbTab & vbTab & StrMO_PO
    '        HeaderMO_Process.Font.Bold = True
    '        HeaderMO_Process.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        HeaderMO_Process.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        HeaderMO.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        HeaderMO_Process.ColumnSpan = 7
    '        HeaderMO_Process.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(HeaderMO_Process)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

    '        Dim HeaderRework As New TableCell()
    '        Dim StrRework As String = "<span style='color:#3F3E3E;'>Return for Rework (asft338 )</span>"
    '        HeaderRework.Text = vbTab & vbTab & StrRework
    '        HeaderRework.Font.Bold = True
    '        HeaderRework.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        HeaderRework.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        HeaderRework.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        HeaderRework.ColumnSpan = 4
    '        HeaderRework.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(HeaderRework)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

    '        Dim HeaderScarpRequest As New TableCell()
    '        Dim StrScarpRequest As String = "<span style='color:#3F3E3E;'>Scarp Request Order (aint300 )</span>"
    '        HeaderScarpRequest.Text = vbTab & vbTab & StrScarpRequest
    '        HeaderScarpRequest.Font.Bold = True
    '        HeaderScarpRequest.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
    '        HeaderScarpRequest.BackColor = ColorTranslator.FromHtml("#99CCCC")
    '        HeaderScarpRequest.BorderColor = ColorTranslator.FromHtml("#5E73DD")
    '        HeaderScarpRequest.ColumnSpan = 12
    '        HeaderScarpRequest.HorizontalAlign = HorizontalAlign.Left
    '        HeaderRow.Cells.Add(HeaderScarpRequest)
    '        gvShow.Controls(0).Controls.AddAt(0, HeaderRow)
    '    End If
    'End Sub

    Private Shared strSqlWH As String = "Select " & SFCB.WONo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " & SFBA.MasterItemNo & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " & SFBA.Position & " ," & SFBA.OperationNo & "," & SFBA.OperationSeq & "," &
        " " & SFBA.BOMitem & "," & SFBA.IssueItem & "," & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & "," &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & "," & SFCB.DirectScarp & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ",  " &
        " " & SFCB.RunCard & "," & SFIA.DocNo & "," & SFIA.RunCard & "," & SFIA.TransferOutOpSerailNo & "," & SFCB.LineNo & "," & SFCB.OperationID & ", " &
        " " & SFAA.ProductionQty & "," & SFIA.ReworkTransferOutQty & "," & SFCA.RunCardDetail & ", " &
        " " & INBI.DocNo & "," & INBI.EntryDate & "," & INBI.Applicant & "," & INBI.AppliedDepartment & "," & INBI.Status & "," & INBI.ScrapReason & ", " &
        " " & INBJ.DocNo & "," & INBJ.LineNo & "," & INBJ.ItemNo & "," & INBJ.TransferOutWH & "," & INBJ.AppliedQty & "," & INBJ.InventoryUnit & " " &
        " FROM " & SFBA.tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFAA.tblMO & "." & SFAA.DocNo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & "  " &
        " AND  " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " Left OUTER JOIN " & INBJ.tblScarpDestoryBody & " On " & INBJ.ItemNo & " = " & SFBA.BOMitem & " " &
        " Left OUTER JOIN " & INBI.tblScarpDestoryHeader & " On " & INBI.tblScarpDestoryHeader & "." & INBI.DocNo & " = " & INBJ.tblScarpDestoryBody & "." & INBJ.DocNo & "   " &
        " LEFT OUTER JOIN  " & SFIA.tblTransferReworkHead & " On " & SFIA.tblTransferReworkHead & "." & SFIA.WONo & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " " &
        " AND " & SFIA.tblTransferReworkHead & "." & SFIA.RunCard & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "  " &
        " AND " & SFIA.tblTransferReworkHead & "." & SFIA.TransferOutOpOrder & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.OperationSeq & "  " &
        " AND " & SFIA.tblTransferReworkHead & "." & SFIA.TransferOutOpSerailNo & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.OperationID & "  " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where (" & SFBA.wStandard & " AND  " & SFBA.ItemSequence & "<>'999' @pWhereCustomUsing )  " &
        " Group By " & SFCB.WONo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " & SFBA.MasterItemNo & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " & SFBA.Position & " ," & SFBA.OperationNo & "," & SFBA.OperationSeq & "," &
        " " & SFBA.BOMitem & "," & SFBA.IssueItem & "," & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & "," &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & "," & SFCB.DirectScarp & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ",  " &
        " " & SFCB.RunCard & "," & SFIA.DocNo & "," & SFIA.RunCard & "," & SFIA.TransferOutOpSerailNo & "," & SFCB.LineNo & "," & SFCB.OperationID & ", " &
        " " & SFAA.ProductionQty & "," & SFIA.ReworkTransferOutQty & "," & SFCA.RunCardDetail & ", " &
        " " & INBI.DocNo & "," & INBI.EntryDate & "," & INBI.Applicant & "," & INBI.AppliedDepartment & "," & INBI.Status & "," & INBI.ScrapReason & ", " &
        " " & INBJ.DocNo & "," & INBJ.LineNo & "," & INBJ.ItemNo & "," & INBJ.TransferOutWH & "," & INBJ.AppliedQty & "," & INBJ.InventoryUnit & " "

    Private Shared Function GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlWH
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub GetDataTable()
        Dim Where As String = String.Empty,
            strSO_No As String = txtSoNo.Text,
            SaleOrder_Date As String = SaleOrderDate.Text,
            MO_Type As String = UsingMO_Type.getObject.Text,
            MO_No As String = txtMO_WO_No.Text,
            Item_No As String = txtitem.Text,
            Spec As String = txtspec.Text,
            F_Date As String = FromDate.Text,
            T_Date As String = ToDate.Text,
            ConditionDate As String = ddlDate.SelectedValue
        Dim wstrSO_No As String = String.Empty
        Dim wSaleOrder_Date As String = String.Empty
        Dim wMO As String = String.Empty
        Dim wItem_No As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wTranDocumentDate As String = String.Empty
        Dim wScarpEntryDate As String = String.Empty
        Dim wWC As String = String.Empty

        If strSO_No <> String.Empty Then
            wstrSO_No = " and " & XMDC.SaleOrderNo & " =  '" & strSO_No & "'"
        End If
        If SaleOrder_Date <> String.Empty Then
            wSaleOrder_Date = " and " & XMDA.DocumentDate & "= TO_DATE('" & [String].Join("','", SaleOrder_Date) & "','yyyy/mm/dd')"
        End If
        If (MO_Type <> "" Or MO_Type <> "--- Select ---" Or MO_Type <> "0") And (MO_No <> String.Empty) Then
            wMO = " and " & SFBA.MODocNo & " = '" & [String].Join("','", ReplaceString.ReplaceMO(MO_Type, MO_No)) & "'"
        End If
        If Item_No <> String.Empty Then
            wItem_No = " and " & SFBA.BOMitem & " Like '" & [String].Join("','", Item_No) & "%'"
        End If
        If Spec <> String.Empty Then
            wSpec = " and " & IMAAL.Specifaction & " Like '%" & [String].Join("','", Spec) & "%'"
        End If
        If ConditionDate <> "1" Then
            If ConditionDate = "T" Then
                wTranDocumentDate = " and " & SFIA.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
            End If
            If ConditionDate = "S" Then
                wScarpEntryDate = " and " & INBI.EntryDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
            End If
        End If
        Dim iStatusRow As Integer = 0
        Dim MO_Status As List(Of [String]) = New List(Of String)()
        For Each sitem As ListItem In MO_NormalStatus.getObject.Items
            If sitem.Selected Then
                MO_Status.Add(sitem.Value)
                iStatusRow = +1
            End If
        Next
        Dim iWC_Row As Integer = 0
        Dim AaryWC_List As List(Of [String]) = New List(Of String)()
        For Each WCitem As ListItem In WC_List.getObject.Items
            If WCitem.Selected Then
                AaryWC_List.Add(WCitem.Value)
                iWC_Row = +1
            End If
        Next
        If (iStatusRow > 0) And (iWC_Row > 0) Then
            Dim StrMO_Status As String = " '" & [String].Join("' , '", MO_Status.ToArray())
            Dim StrWC As String = " '" & [String].Join("' , '", AaryWC_List.ToArray())
            Dim pMultiStatus As String = SFAA.Status & " In(" & [String].Join("','", StrMO_Status) & "')"
            Dim pMultiWorkStation As String = SFCB.WorkStation & " In(" & [String].Join("','", StrWC) & "')"
            wWC = " and " & pMultiStatus & " AND " & pMultiWorkStation
        End If

        Where = wstrSO_No & wSaleOrder_Date & wMO & wItem_No & wSpec & wTranDocumentDate & wScarpEntryDate & wWC
        If Where <> String.Empty Then
            'lblSql.Text = GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body(Where)
            Dim dt As DataTable = GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body(Where)
            If dt.Rows.Count > 0 Then
                gvShow.DataSource = dt
                gvShow.DataBind()
                Dim ChkBoxHeader As CheckBox = CType(gvShow.HeaderRow.FindControl("chkAllSelect"), CheckBox)
                If lblSql.Text = "1" Then
                    ChkBoxHeader.Checked = True
                Else
                    ChkBoxHeader.Checked = False
                End If
                For Each row As GridViewRow In gvShow.Rows
                    Dim ChkBoxRows As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                    If ChkBoxHeader.Checked = True Then
                        ChkBoxRows.Checked = True
                    Else
                        ChkBoxRows.Checked = False
                    End If
                Next
                GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
                PanelTabReport.Visible = True
                BuExcel.Visible = False
                btSelect.Visible = True
                btPrint.Visible = False
                BuExcel0.Visible = False
                btSelect0.Visible = True
                btPrint0.Visible = False
                btPrintAMP0.Visible = False
                btPrintAMP.Visible = False
            Else
                gvShow.DataSource = New List(Of String)
                gvShow.DataBind()
                GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                PanelTabReport.Visible = False
                BuExcel.Visible = False
                btSelect.Visible = False
                btPrint.Visible = False
                BuExcel0.Visible = False
                btSelect0.Visible = False
                btPrint0.Visible = False
                btPrintAMP0.Visible = False
                btPrintAMP.Visible = False
            End If
        End If
    End Sub
    Private Sub Busearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Busearch.Click
        Call CheckSelectRow()
        Call GetDataTable()
    End Sub
    Protected Sub chkboxSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim ChkBoxHeader As CheckBox = CType(gvShow.HeaderRow.FindControl("chkAllSelect"), CheckBox)
        For Each row As GridViewRow In gvShow.Rows
            Dim ChkBoxRows As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If ChkBoxHeader.Checked = True Then
                ChkBoxRows.Checked = True
                lblSql.Text = "1"
            Else
                ChkBoxRows.Checked = False
                lblSql.Text = ""
            End If
        Next
    End Sub
    Sub CheckSelectRow()
        For Each row As GridViewRow In gvShow.Rows
            Dim ChkBoxHeader As CheckBox = CType(gvShow.HeaderRow.FindControl("chkAllSelect"), CheckBox)
            If lblSql.Text = "1" Then
                ChkBoxHeader.Checked = True
            Else
                ChkBoxHeader.Checked = False
            End If
            Dim ChkBoxRows As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If ChkBoxHeader.Checked = True Then
                ChkBoxRows.Checked = True
            Else
                ChkBoxRows.Checked = False
            End If
        Next
    End Sub
    Protected Sub BuExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuExcel.Click, BuExcel0.Click
        ExportsUtility.ExportGridviewToMsExcel("Scrap" & Session("UserName"), gvSel)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Private Function gridviewToDataTable(gv As GridView) As DataTable
        Dim dtReport As New DataTable("TableReport")
        Dim dr As DataRow = Nothing
        dtReport.Columns.Add(New DataColumn("Workstation", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Scarp_ApplicatinNo", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Entry_Date", GetType(String)))
        dtReport.Columns.Add(New DataColumn("MO_DocNo", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Production_Item", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Scarp_Item", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Scarp_Spec", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Runcard", GetType(String)))
        dtReport.Columns.Add(New DataColumn("RuncardDetail", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Production_Qty", GetType(String)))
        dtReport.Columns.Add(New DataColumn("ScarpAppliedQty", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Scarp_Uint", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Defect", GetType(String)))
        dtReport.Columns.Add(New DataColumn("RootCause", GetType(String)))
        dtReport.Columns.Add(New DataColumn("Applicant", GetType(String)))
        dtReport.Columns.Add(New DataColumn("EmpWorkDefect", GetType(String)))
        For Each row As GridViewRow In gv.Rows
            Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            dr = dtReport.NewRow()
            If chkSelect.Checked Then
                Dim sWorkstation As String = row.Cells(16).Text
                Dim sScarpApplicatinNo As String = row.Cells(21).Text
                Dim sEntry_Date As String = row.Cells(22).Text
                Dim sMO_DocNo As String = row.Cells(2).Text
                Dim sProduction_Item As String = row.Cells(4).Text
                Dim sProduction_Qty As String = row.Cells(11).Text
                Dim sScarp_Item As String = row.Cells(6).Text
                Dim sScarp_Spec As String = row.Cells(8).Text
                Dim sRuncard As String = row.Cells(9).Text
                Dim sRuncardDetail As String = row.Cells(10).Text
                Dim sScarpAppliedQty As String = row.Cells(30).Text
                Dim sScarp_Uint As String = row.Cells(31).Text
                Dim sDefect As String = String.Empty
                Dim sRootCause As String = String.Empty
                Dim sApplicant As String = row.Cells(23).Text
                Dim sEmpWorkDefect As String = String.Empty

                dr("Workstation") = sWorkstation
                dr("Scarp_ApplicatinNo") = sScarpApplicatinNo
                dr("Entry_Date") = sEntry_Date
                dr("MO_DocNo") = sMO_DocNo
                dr("Production_Item") = sProduction_Item
                dr("Production_Qty") = sProduction_Qty
                dr("Scarp_Item") = sScarp_Item
                dr("Scarp_Spec") = sScarp_Spec
                dr("Runcard") = sRuncard
                dr("RuncardDetail") = sRuncardDetail
                dr("ScarpAppliedQty") = sScarpAppliedQty
                dr("Scarp_Uint") = sScarp_Uint
                dr("Defect") = sDefect
                dr("RootCause") = sRootCause
                dr("Applicant") = sApplicant
                dr("EmpWorkDefect") = sEmpWorkDefect
                'dr("date") = DateTime.Parse(row.Cells(0).Text)
                'dr("loanbalance") = Double.Parse(row.Cells(1).Text)
                dtReport.Rows.Add(dr)
            End If
        Next
        Return dtReport
    End Function
    Protected Sub btSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelect.Click, btSelect0.Click
        Dim dt As DataTable = gridviewToDataTable(gvShow)
        gvSel.DataSource = dt
        gvSel.DataBind()
        'Call RemoveDataEmptyGridSel()
        GridviewUtility.GridStyleTemplate_Std(gvSel)
        GridviewUtility.GrigOnmouseHandleAuto(gvSel)
        Call CheckSelectRow()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
        BuExcel.Visible = True
        btPrint.Visible = True
        BuExcel0.Visible = True
        btPrint0.Visible = True
        btPrintAMP0.Visible = True
        btPrintAMP.Visible = True
    End Sub
    'Private Sub RemoveDataEmptyGridSel()
    '    For Each row As GridViewRow In gvSel.Rows
    '        Dim i As Integer = 0
    '        While i < gvSel.Columns.Count
    '            'Dim header As [String] = gvSel.Columns(i).HeaderText
    '            Dim cellText As [String] = row.Cells(i).Text
    '            If cellText = "&nbsp;" Or cellText = String.Empty Then
    '                row.Cells(i).Text = String.Empty
    '            End If

    '            i += 1
    '        End While
    '    Next
    'End Sub
    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrint.Click, btPrint0.Click

        Dim tableName As String = "TempReworkOrScrap" & Session("UserName")
        Dim paraName As String = ""

        Randomize()

        paraName = "table:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ScarpRework.aspx?dbName=MIS&ReportName=ReworkOrScrap.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ScrapAndRedo.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
    End Sub

    Protected Sub btPrintAMP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintAMP.Click

        Dim tableName As String = "TempReworkOrScrap" & Session("UserName")
        Dim paraName As String = ""

        Randomize()

        paraName = "table:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ScarpRework.aspx?dbName=MIS&ReportName=ReworkOrScrapAMP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ScrapAndRedo.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
    End Sub

    Protected Sub btPrintAMP0_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btPrintAMP0.Click

        Dim tableName As String = "TempReworkOrScrap" & Session("UserName")
        Dim paraName As String = ""

        Randomize()

        paraName = "table:" & tableName
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ScarpRework.aspx?dbName=MIS&ReportName=ReworkOrScrapAMP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=ScrapAndRedo.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
    End Sub

    'Private Sub gvSel_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSel.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.Cells(0).Text = "&nbsp;" Then
    '            e.Row.Cells(0).Text.Replace("&nbsp;", "")
    '        End If
    '    End If
    'End Sub
End Class