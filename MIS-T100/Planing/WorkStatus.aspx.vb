Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Globalization
Imports System.Data.OracleClient
Imports System.Drawing

Public Class WorkStatus
    Inherits System.Web.UI.Page

    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    ' Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Private Shared _SourceTable As New DataTable()
    Dim _Separator As String = "."
    Private Shared strWhere As String
    'Public Sub New(ByVal SourceTable As DataTable)
    '    _SourceTable = SourceTable
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Private Shared strSqlDataWorkstatus As String = "Select " & SFAA.DocNo & ", " & SFAA.ProductItem & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFCB.WorkStation & "," & SFCB.OperationID & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & SFCA.RunCardDetail & ", " &
        " " & SFCA.RunCardNo & "," & SFAA.ProductionQty & ", " & SFCB.LineNo & "," & SFCB.WONo & ", " &
        " " & SFCB.WIP & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & ", " &
        " " & SFCB.DirectScarp & " " &
        " FROM " & SFAA.tblMO & " " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.ent & " = " & SFAA.ent & " On " & SFCA.DocNo & " = " & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.ent & "=" & SFCA.ent & " AND  " & SFCB.WONo & "=" & SFCA.DocNo & " AND  " & SFCB.RunCard & "=" & SFCA.RunCardNo & "  " &
        " LEFT OUTER JOIN  " & SFBA.tblManufactureOrder_Body & " On " & SFBA.ent & " = " & SFAA.ent & " " & " and " & SFBA.MODocNo & " = " & SFAA.DocNo &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ent & "=" & SFAA.ent & " and " & IMAAL.ProductItem & "=" & SFAA.ProductItem &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.ent & "=" & SFBA.ent & " and " & XMDC.Item & "=" & SFBA.MasterItemNo &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.ent & "=" & XMDA.ent & " and " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo &
        " where ( @pWhereCustomUsing  AND " & SFBA.wStandard & " AND  " & SFBA.ItemSequence & "<>'999')  " &
        " Order By " & SFAA.DocNo & "," & SFCA.RunCardNo & "," & SFCB.LineNo

    '" Left OUTER JOIN " & XMDM.tblSaleDelivery_Body_MulitiStorel & " On " & XMDM.ent & " = " & XMDC.ent & " and " & XMDM.ItemNo & " = " & XMDC.Item &
    '    " LEFT OUTER JOIN " & XMDK.tblSaleDelivery_Head & " On " & XMDK.SaleDeliveryNo & " = " & XMDM.SaleDeliveryNo & " " &
    '" Group By " & SFAA.DocNo & ", " & SFAA.ProductItem & "," &
    '    " " & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFCB.WorkStation & "," & SFCB.OperationID & ", " &
    '    " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & ", " &
    '     " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & SFCA.RunCardDetail & ", " &
    '    " " & SFCA.RunCardNo & "," & SFAA.ProductionQty & ", " & SFCB.LineNo & "," & SFCB.WONo & ", " &
    '    " " & SFCB.WIP & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & ", " &
    '    " " & SFCB.DirectScarp & " " &

    Private Shared Function GetWorkStatusDataTable(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlDataWorkstatus
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
    Private Shared strSqlDataWorkstatusStrPvt As String = "Select  " & SFAA.DocNo & ", " & SFAA.ProductItem & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & "," & SFCA.RunCardDetail & ", " &
        " " & SFCA.RunCardNo & "," & SFAA.ProductionQty & "," & XMDA.CustomerId & " ,  " &
        " " & SFAA.ScarpQty & ",(select min(" & SFCB.LineNo & ") from " & SFCB.tblMOprocessItem_SFCB & " " &
        " where " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFAA.DocNo & " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.RunCardNo & ") as StartProcess, " &
        " (select max(" & SFCB.LineNo & ") from " & SFCB.tblMOprocessItem_SFCB & " " &
        " where " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFAA.DocNo & " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.RunCardNo & ") as EndProcess, " &
        " (select count(" & SFCB.LineNo & ") from " & SFCB.tblMOprocessItem_SFCB & " " &
        " where " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFAA.DocNo & " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.RunCardNo & ") as CountProcess " &
        " FROM " & SFAA.tblMO & " " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFCA.tblMO_Detail & "." & SFCA.DocNo & "  " &
        " AND  " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & "  " &
        " LEFT OUTER JOIN  " & SFBA.tblManufactureOrder_Body & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFAA.tblMO & "." & SFAA.ProductItem & "  " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " Left OUTER JOIN " & XMDM.tblSaleDelivery_Body_MulitiStorel & " On " & XMDM.tblSaleDelivery_Body_MulitiStorel & "." & XMDM.ItemNo & " = " & XMDC.tblSaleItem & "." & XMDC.Item & " " &
        " LEFT OUTER JOIN " & XMDK.tblSaleDelivery_Head & " On " & XMDK.tblSaleDelivery_Head & "." & XMDK.SaleDeliveryNo & " = " & XMDM.tblSaleDelivery_Body_MulitiStorel & "." & XMDM.SaleDeliveryNo & " " &
        " where ( @pWhereCustom  And " & SFBA.wStandard & " And  " & SFBA.ItemSequence & "<>'999')  " &
        " Group By " & SFAA.DocNo & ", " & SFAA.ProductItem & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & "," & SFCA.RunCardDetail & ", " &
        " " & SFCA.RunCardNo & "," & SFAA.ProductionQty & "," & XMDA.CustomerId & " ,  " &
        " " & SFAA.ScarpQty & " " &
        " Order By " & XMDA.SaleOrderNo & "," & SFAA.DocNo & "," & SFCA.RunCardNo & " ASC "
    Private Shared Function GetWorkStatusPvtDataTable(strWhereCustom As String) As DataTable
        Dim Sql As String = strSqlDataWorkstatusStrPvt
        Dim pWhereCustomUsing As String = strWhereCustom
        Sql = Sql.Replace("@pWhereCustom", pWhereCustomUsing)
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

    Private Sub GetSubDataWorkstatus()
        Dim Where As String = String.Empty
        Dim strSO_Type As String = UsingDocTypeSale.getObject.Text
        Dim strSON_No As String = txtSaleNo.Text
        Dim SaleSeq As String = txtSaleSeq.Text
        Dim CustomerID As String = txtCust.Text
        Dim MO_Type As String = UsingMO_Type.getObject.Text
        Dim MO_No As String = txtWorkNo.Text
        Dim Item_No As String = txtProductionItem.Text
        Dim Spec As String = txtSpec.Text
        Dim F_Date As String = FormDate.Text
        Dim T_Date As String = ToDate.Text
        Dim ConditionDate As String = ddlDate.SelectedValue
        Dim status As String = UsingStatusMO_Normal.getObject.Text

        If (strSO_Type <> "--- Select ---" Or strSO_Type <> "0") And (strSON_No <> String.Empty) Then
            Dim pstrSaleNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, txtSaleNo.Text)
            Where = XMDA.SaleOrderNo & "= '" & [String].Join("','", pstrSaleNo) & "'"
        ElseIf (SaleSeq <> String.Empty) And (strSO_Type <> "--- Select ---" Or strSO_Type <> "0") And (strSON_No <> String.Empty) Then
            Dim pstrSaleNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, txtSaleNo.Text)
            Dim pSaleNo As String = XMDA.SaleOrderNo & "= '" & [String].Join("','", pstrSaleNo) & "'"
            Dim pSaleSeq As String = XMDC.ItemSequence & "= '" & [String].Join("','", SaleSeq) & "'"
            Where = pSaleNo & " AND " & pSaleSeq
        End If
        If (MO_Type <> "" Or MO_Type <> "--- Select ---" Or MO_Type <> "0") And (MO_No <> String.Empty) Then
            Where = SFBA.MODocNo & " = '" & [String].Join("','", ReplaceString.ReplaceMO(MO_Type, MO_No)) & "'"
        End If
        If Item_No <> String.Empty Then
            Where = SFBA.MasterItemNo & " Like '" & [String].Join("','", Item_No) & "%'"
        End If
        If Spec <> String.Empty Then
            Where = IMAAL.Specifaction & " Like '%" & [String].Join("','", Spec) & "%'"
        End If
        If CustomerID <> String.Empty Then
            Where = XMDA.CustomerId & " = '" & [String].Join("','", CustomerID) & "'"
        End If
        If ConditionDate <> "0" And (status <> "" Or status <> "--- Select ---" Or status <> "0") Then
            If ConditionDate = "DS" Then ' Delivery Date + Status
                Dim SaleDeliveryDate As String = XMDK.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
                Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
                Where = SaleDeliveryDate & " AND " & pStatus
            ElseIf ConditionDate = "SD" Then ' Sale Date + Status
                Dim SaleDate As String = XMDA.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
                Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
                Where = SaleDate & " AND " & pStatus
            ElseIf ConditionDate = "MOD" Then ' MO Date + Status
                Dim MODate As String = SFCB.PlanStartDate & " >= TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND " & SFCB.PlannedCompletionDate & " <= TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
                Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
                Where = MODate & " AND " & pStatus
            ElseIf ConditionDate = "PSD" Then ' Plan Start Date + Status
                Dim PlanStartDate As String = SFCB.PlanStartDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
                Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
                Where = PlanStartDate & " AND " & pStatus
            ElseIf ConditionDate = "PFD" Then ' Plan Finish Date + Status
                Dim PlanCompleteDate As String = SFCB.PlannedCompletionDate & " BETWEEN TO_DATE('" & [String].Join("','", F_Date) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", T_Date) & "','yyyy/mm/dd')"
                Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
                Where = PlanCompleteDate & " AND " & pStatus
            End If
        End If
        ' lblSql.Text = GetWorkStatusDataTable(Where)
        If Where <> String.Empty Then
            strWhere = Where
            Dim dtA As DataTable = GetWorkStatusDataTable(Where)
            Dim dtPvt As DataTable = GetWorkStatusPvtDataTable(Where)
            If dtPvt.Rows.Count > 0 Then
                gvShowPiVot.DataSource = dtPvt
                gvShowPiVot.DataBind()

            End If

            If dtA.Rows.Count > 0 Then
                gvShow.DataSource = dtA
                gvShow.DataBind()
                ' GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                GridviewUtility.GridStyleTemplate_Std(gvShow)
                '' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewShow", "gridviewShow();", True)
                CountRow1.RowCount = dtA.Rows.Count.ToString
                btExport.Visible = True
            Else
                gvShow.DataSource = New List(Of String)
                gvShow.DataBind()
                GridviewUtility.GridStyleTemplate_Std(gvShow)
                ''GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                btExport.Visible = False
            End If
        Else
            gvShow.DataSource = New List(Of String)
            gvShow.DataBind()
            GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        'ExportsUtility.ExportGridviewToMsExcel("WorkStatus" & Session("UserName"), gvShow)
        Dim sfileName As String = "WorkStatus" & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".xls"
        Dim sAttachfiels As String = "attachment;filename=" & sfileName
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", sAttachfiels)

        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gvShow.AllowPaging = False
            Me.GetSubDataWorkstatus()

            gvShow.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gvShow.HeaderRow.Cells
                cell.BackColor = gvShow.HeaderStyle.BackColor
                cell.BorderStyle = BorderStyle.Inset
            Next
            For Each row As GridViewRow In gvShow.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gvShow.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gvShow.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                    cell.BorderStyle = BorderStyle.Inset
                    cell.Wrap = False
                    Dim controls As New List(Of Control)()

                    'Add controls to be removed to Generic List
                    For Each control As Control In cell.Controls
                        controls.Add(control)
                    Next

                    'Loop through the controls to be removed and replace then with Literal
                    For Each control As Control In controls
                        Select Case control.GetType().Name
                            Case "HyperLink"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, HyperLink).Text.ToString()
                                    })
                                Exit Select
                            Case "Label"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, Label).Text.ToString()
                                    })
                                Exit Select
                            Case "TextBox"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, TextBox).Text.ToString()
                                    })
                                Exit Select
                            Case "LinkButton"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, LinkButton).Text.ToString()
                                    })
                                Exit Select
                            Case "CheckBox"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, CheckBox).Text.ToString()
                                    })
                                Exit Select
                            Case "RadioButton"
                                cell.Controls.Add(New Literal() With {
                                     .Text = TryCast(control, RadioButton).Text.ToString()
                                    })
                                Exit Select
                        End Select
                        cell.Controls.Remove(control)
                    Next
                Next
            Next

            gvShow.RenderControl(hw)

            'style to format numbers to string
            Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
            ' Dim style As String = "<style> .textmode {' } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()
        End Using
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call GetSubDataWorkstatus()
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            If e.Row.Cells(4).Text <> String.Empty Then 'Satus
                If e.Row.Cells(4).Text = "C" Then
                    e.Row.ForeColor = System.Drawing.Color.Green
                ElseIf e.Row.Cells(4).Text = "M" Then
                    e.Row.BackColor = System.Drawing.Color.Wheat
                    e.Row.ForeColor = System.Drawing.Color.Black
                ElseIf e.Row.Cells(4).Text = "N" Then
                    e.Row.ForeColor = System.Drawing.Color.Maroon
                ElseIf e.Row.Cells(4).Text = "Y" Then
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#b35900")
                End If
                e.Row.Cells(4).Text = StatusT100.MO_Normal(e.Row.Cells(4).Text)
            End If
            If e.Row.Cells(9).Text <> String.Empty Then 'RecardDeatil
                If e.Row.Cells(9).Text = "1" Then
                    e.Row.Cells(9).Text = "1 : GENERAL"
                End If
                If e.Row.Cells(9).Text = "2" Then
                    e.Row.Cells(9).Text = "2 : REWORK"
                    e.Row.ForeColor = ColorTranslator.FromHtml("black")
                End If
            End If
            'If e.Row.Cells(16).Text <> String.Empty Then ' Operation
            '    Dim dtOP As DataTable = OOCQL.GetDataOperation(e.Row.Cells(16).Text)
            '    If dtOP.Rows.Count > 0 Then
            '        e.Row.Cells(16).Text = dtRowsFormat.FormatString(dtOP, OOCQL.OperationID) & " : " & dtRowsFormat.FormatString(dtOP, OOCQL.Operation)
            '    End If
            'End If
            'If e.Row.Cells(17).Text <> String.Empty Then ' WC
            '    Dim dtWC As DataTable = ECAA.GetFindWorkcenterDetail_Table(e.Row.Cells(17).Text)
            '    If dtWC.Rows.Count > 0 Then
            '        e.Row.Cells(17).Text = dtRowsFormat.FormatString(dtWC, ECAA.WorkcenterID) & " : " & dtRowsFormat.FormatString(dtWC, ECAA.Workcenter)
            '    End If
            'End If
        End If
    End Sub

    'Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        e.Row.Cells(3).Text = "MO DocNo."
    '    End If
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        'Dim lblSaleOrderNo As Label = CType((e.Row.FindControl("lblSaleOrderNo")), Label)
    '        'Dim lblSaleItemNo As Label = CType((e.Row.FindControl("lblSaleItemNo")), Label) ' 
    '        'If e.Row.Cells(3).Text <> String.Empty Then
    '        '    Dim SaleItemDT As DataTable = GetWorkStatusDataTable(strWhere)
    '        '    If SaleItemDT.Rows.Count > 0 Then
    '        '        lblSaleItemNo.Text = dtRowsFormat.FormatString(SaleItemDT, SFAA.ProductItem)
    '        '    End If
    '        '    If lblSaleItemNo.Text <> String.Empty Then
    '        '        Dim SaleNoDT As DataTable = XMDC.GetDataMoProcessHeader(lblSaleItemNo.Text)
    '        '        lblSaleOrderNo.Text = dtRowsFormat.FormatString(SaleNoDT, XMDC.SaleOrderNo)
    '        '    End If
    '        'End If
    '    End If
    'End Sub
    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Call GetSubDataWorkstatus()
        Call CheckDataProcess()
    End Sub

    Private Sub gvShowPiVot_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShowPiVot.RowDataBound
        Dim CountStep, FindStep1, FindStep2, FindStep3, FindStep4, FindStep5, FindStep6, FindStep7, FindStep8,
            FindStep9, FindStep10, FindStep11, FindStep12, FindStep13, FindStep14, FindStep15, FindStep16,
            FindStep17, FindStep18, FindStep19, FindStep20, FindStep21, FindStep22, FindStep23, FindStep24,
            FindStep25, FindStep26, FindStep27, FindStep28, FindStep29, FindStep30 As Integer
        '#### Header Control #######################################
        If e.Row.RowType = DataControlRowType.Header Then
            Dim lblHStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep1"), System.Web.UI.WebControls.Label)
            Dim lblHStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep2"), System.Web.UI.WebControls.Label)
            Dim lblHStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep3"), System.Web.UI.WebControls.Label)
            Dim lblHStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep4"), System.Web.UI.WebControls.Label)
            Dim lblHStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep5"), System.Web.UI.WebControls.Label)
            Dim lblHStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep6"), System.Web.UI.WebControls.Label)
            Dim lblHStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep7"), System.Web.UI.WebControls.Label)
            Dim lblHStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep8"), System.Web.UI.WebControls.Label)
            Dim lblHStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep9"), System.Web.UI.WebControls.Label)
            Dim lblHStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep10"), System.Web.UI.WebControls.Label)
            Dim lblHStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep11"), System.Web.UI.WebControls.Label)
            Dim lblHStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep12"), System.Web.UI.WebControls.Label)
            Dim lblHStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep13"), System.Web.UI.WebControls.Label)
            Dim lblHStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep14"), System.Web.UI.WebControls.Label)
            Dim lblHStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep15"), System.Web.UI.WebControls.Label)
            Dim lblHStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep16"), System.Web.UI.WebControls.Label)
            Dim lblHStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep17"), System.Web.UI.WebControls.Label)
            Dim lblHStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep18"), System.Web.UI.WebControls.Label)
            Dim lblHStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep19"), System.Web.UI.WebControls.Label)
            Dim lblHStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep20"), System.Web.UI.WebControls.Label)
            Dim lblHStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep21"), System.Web.UI.WebControls.Label)
            Dim lblHStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep22"), System.Web.UI.WebControls.Label)
            Dim lblHStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep23"), System.Web.UI.WebControls.Label)
            Dim lblHStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep24"), System.Web.UI.WebControls.Label)
            Dim lblHStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep25"), System.Web.UI.WebControls.Label)
            Dim lblHStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep26"), System.Web.UI.WebControls.Label)
            Dim lblHStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep27"), System.Web.UI.WebControls.Label)
            Dim lblHStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep28"), System.Web.UI.WebControls.Label)
            Dim lblHStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep29"), System.Web.UI.WebControls.Label)
            Dim lblHStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblHStep30"), System.Web.UI.WebControls.Label)
        End If
        '### DataRows Control #######################################
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.BackColor = ColorTranslator.FromHtml("#ffffff")
            e.Row.Cells(0).BackColor = ColorTranslator.FromHtml("#DDDDDD")
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Font.Size = 8
            e.Row.Cells(0).ForeColor = ColorTranslator.FromHtml("#444444")
            Dim lblStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep1"), System.Web.UI.WebControls.Label)
            Dim lblStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep2"), System.Web.UI.WebControls.Label)
            Dim lblStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep3"), System.Web.UI.WebControls.Label)
            Dim lblStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep4"), System.Web.UI.WebControls.Label)
            Dim lblStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep5"), System.Web.UI.WebControls.Label)
            Dim lblStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep6"), System.Web.UI.WebControls.Label)
            Dim lblStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep7"), System.Web.UI.WebControls.Label)
            Dim lblStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep8"), System.Web.UI.WebControls.Label)
            Dim lblStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep9"), System.Web.UI.WebControls.Label)
            Dim lblStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep10"), System.Web.UI.WebControls.Label)
            Dim lblStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep11"), System.Web.UI.WebControls.Label)
            Dim lblStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep12"), System.Web.UI.WebControls.Label)
            Dim lblStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep13"), System.Web.UI.WebControls.Label)
            Dim lblStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep14"), System.Web.UI.WebControls.Label)
            Dim lblStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep15"), System.Web.UI.WebControls.Label)
            Dim lblStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep16"), System.Web.UI.WebControls.Label)
            Dim lblStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep17"), System.Web.UI.WebControls.Label)
            Dim lblStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep18"), System.Web.UI.WebControls.Label)
            Dim lblStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep19"), System.Web.UI.WebControls.Label)
            Dim lblStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep20"), System.Web.UI.WebControls.Label)
            Dim lblStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep21"), System.Web.UI.WebControls.Label)
            Dim lblStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep22"), System.Web.UI.WebControls.Label)
            Dim lblStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep23"), System.Web.UI.WebControls.Label)
            Dim lblStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep24"), System.Web.UI.WebControls.Label)
            Dim lblStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep25"), System.Web.UI.WebControls.Label)
            Dim lblStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep26"), System.Web.UI.WebControls.Label)
            Dim lblStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep27"), System.Web.UI.WebControls.Label)
            Dim lblStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep28"), System.Web.UI.WebControls.Label)
            Dim lblStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep29"), System.Web.UI.WebControls.Label)
            Dim lblStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStep30"), System.Web.UI.WebControls.Label)
            'Dim lblStepCount As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblStepCount"), System.Web.UI.WebControls.Label)

            Dim lblOPStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep1"), System.Web.UI.WebControls.Label)
            Dim lblOPStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep2"), System.Web.UI.WebControls.Label)
            Dim lblOPStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep3"), System.Web.UI.WebControls.Label)
            Dim lblOPStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep4"), System.Web.UI.WebControls.Label)
            Dim lblOPStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep5"), System.Web.UI.WebControls.Label)
            Dim lblOPStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep6"), System.Web.UI.WebControls.Label)
            Dim lblOPStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep7"), System.Web.UI.WebControls.Label)
            Dim lblOPStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep8"), System.Web.UI.WebControls.Label)
            Dim lblOPStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep9"), System.Web.UI.WebControls.Label)
            Dim lblOPStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep10"), System.Web.UI.WebControls.Label)
            Dim lblOPStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep11"), System.Web.UI.WebControls.Label)
            Dim lblOPStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep12"), System.Web.UI.WebControls.Label)
            Dim lblOPStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep13"), System.Web.UI.WebControls.Label)
            Dim lblOPStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep14"), System.Web.UI.WebControls.Label)
            Dim lblOPStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep15"), System.Web.UI.WebControls.Label)
            Dim lblOPStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep16"), System.Web.UI.WebControls.Label)
            Dim lblOPStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep17"), System.Web.UI.WebControls.Label)
            Dim lblOPStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep18"), System.Web.UI.WebControls.Label)
            Dim lblOPStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep19"), System.Web.UI.WebControls.Label)
            Dim lblOPStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep20"), System.Web.UI.WebControls.Label)
            Dim lblOPStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep21"), System.Web.UI.WebControls.Label)
            Dim lblOPStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep22"), System.Web.UI.WebControls.Label)
            Dim lblOPStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep23"), System.Web.UI.WebControls.Label)
            Dim lblOPStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep24"), System.Web.UI.WebControls.Label)
            Dim lblOPStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep25"), System.Web.UI.WebControls.Label)
            Dim lblOPStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep26"), System.Web.UI.WebControls.Label)
            Dim lblOPStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep27"), System.Web.UI.WebControls.Label)
            Dim lblOPStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep28"), System.Web.UI.WebControls.Label)
            Dim lblOPStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep29"), System.Web.UI.WebControls.Label)
            Dim lblOPStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblOPStep30"), System.Web.UI.WebControls.Label)

            Dim lblWCStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep1"), System.Web.UI.WebControls.Label)
            Dim lblWCStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep2"), System.Web.UI.WebControls.Label)
            Dim lblWCStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep3"), System.Web.UI.WebControls.Label)
            Dim lblWCStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep4"), System.Web.UI.WebControls.Label)
            Dim lblWCStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep5"), System.Web.UI.WebControls.Label)
            Dim lblWCStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep6"), System.Web.UI.WebControls.Label)
            Dim lblWCStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep7"), System.Web.UI.WebControls.Label)
            Dim lblWCStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep8"), System.Web.UI.WebControls.Label)
            Dim lblWCStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep9"), System.Web.UI.WebControls.Label)
            Dim lblWCStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep10"), System.Web.UI.WebControls.Label)
            Dim lblWCStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep11"), System.Web.UI.WebControls.Label)
            Dim lblWCStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep1"), System.Web.UI.WebControls.Label)
            Dim lblWCStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep13"), System.Web.UI.WebControls.Label)
            Dim lblWCStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep14"), System.Web.UI.WebControls.Label)
            Dim lblWCStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep15"), System.Web.UI.WebControls.Label)
            Dim lblWCStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep16"), System.Web.UI.WebControls.Label)
            Dim lblWCStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep17"), System.Web.UI.WebControls.Label)
            Dim lblWCStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep18"), System.Web.UI.WebControls.Label)
            Dim lblWCStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep19"), System.Web.UI.WebControls.Label)
            Dim lblWCStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep20"), System.Web.UI.WebControls.Label)
            Dim lblWCStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep21"), System.Web.UI.WebControls.Label)
            Dim lblWCStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep22"), System.Web.UI.WebControls.Label)
            Dim lblWCStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep23"), System.Web.UI.WebControls.Label)
            Dim lblWCStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep24"), System.Web.UI.WebControls.Label)
            Dim lblWCStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep25"), System.Web.UI.WebControls.Label)
            Dim lblWCStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep26"), System.Web.UI.WebControls.Label)
            Dim lblWCStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep27"), System.Web.UI.WebControls.Label)
            Dim lblWCStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep28"), System.Web.UI.WebControls.Label)
            Dim lblWCStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep29"), System.Web.UI.WebControls.Label)
            Dim lblWCStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWCStep30"), System.Web.UI.WebControls.Label)


            Dim lblTrsInStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep1"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep2"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep3"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep4"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep5"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep6"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep7"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep8"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep9"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep10"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep11"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep12"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep13"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep14"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep15"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep16"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep17"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep18"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep19"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep20"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep21"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep22"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep23"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep24"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep25"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep26"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep27"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep28"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep29"), System.Web.UI.WebControls.Label)
            Dim lblTrsInStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsInStep30"), System.Web.UI.WebControls.Label)


            Dim lblWIPStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep1"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep2"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep3"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep4"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep5"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep6"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep7"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep8"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep9"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep10"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep11"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep12"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep13"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep14"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep15"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep16"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep17"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep18"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep19"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep20"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep21"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep22"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep23"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep24"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep25"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep26"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep27"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep28"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep29"), System.Web.UI.WebControls.Label)
            Dim lblWIPStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblWIPStep30"), System.Web.UI.WebControls.Label)

            Dim lblTrsOutStep1 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep1"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep2 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep2"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep3 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep3"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep4 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep4"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep5 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep5"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep6 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep6"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep7 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep7"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep8 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep8"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep9 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep9"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep10 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep10"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep11 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep11"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep12 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep12"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep13 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep13"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep14 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep14"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep15 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep15"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep16 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep16"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep17 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep17"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep18 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep18"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep19 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep19"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep20 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep20"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep21 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep21"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep22 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep22"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep23 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep23"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep24 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep24"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep25 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep25"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep26 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep26"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep27 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep27"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep28 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep28"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep29 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep29"), System.Web.UI.WebControls.Label)
            Dim lblTrsOutStep30 As System.Web.UI.WebControls.Label = CType(e.Row.FindControl("lblTrsOutStep30"), System.Web.UI.WebControls.Label)

            'CountStep = CInt(lblStepCount.Text)
            Dim MO_DocNo As String = e.Row.Cells(3).Text
            Dim MO_Runcard As String = e.Row.Cells(8).Text
            Dim P1 As String = String.Empty
            Dim P2 As String = String.Empty
            Dim P3 As String = String.Empty
            Dim P4 As String = String.Empty
            Dim P5 As String = String.Empty
            Dim P6 As String = String.Empty
            Dim P7 As String = String.Empty
            Dim P8 As String = String.Empty
            Dim P9 As String = String.Empty
            Dim P10 As String = String.Empty
            Dim P11 As String = String.Empty
            Dim P12 As String = String.Empty
            Dim P13 As String = String.Empty
            Dim P14 As String = String.Empty
            Dim P15 As String = String.Empty
            Dim P16 As String = String.Empty
            Dim P17 As String = String.Empty
            Dim P18 As String = String.Empty
            Dim P19 As String = String.Empty
            Dim P20 As String = String.Empty
            Dim P21 As String = String.Empty
            Dim P22 As String = String.Empty
            Dim P23 As String = String.Empty
            Dim P24 As String = String.Empty
            Dim P25 As String = String.Empty
            Dim P26 As String = String.Empty
            Dim P27 As String = String.Empty
            Dim P28 As String = String.Empty
            Dim P29 As String = String.Empty
            Dim P30 As String = String.Empty
            Dim Item_Seq As String = "ItemSeq: "
            Dim ItemOP As String = "Operation: "
            Dim ssTrsInP As String = "GoodTransfer-in: "
            Dim ssWIP_P As String = "WIP: "
            Dim ssTrsOutP As String = "GoodTransfer-Out: "
            P1 = lblStep1.Text
            lblStep1.Text = Item_Seq & "<span style='color:Blue;'>" & P1 & "</span>"

            Dim lblStep1_C As Label = CType(e.Row.FindControl("lblStep1_C"), Label)
            lblStep1_C.Text = P1
            If MO_DocNo <> String.Empty Then
                Dim StrSQLPF1 As String = "select " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WorkStation & "," &
            " " & SFCB.GoodTransferIn & "," & SFCB.WIP & "," & SFCB.GoodTransferOut & " " &
            " from " & SFCB.tblMOprocessItem_SFCB & " " &
                " where rownum = 1  And " & SFCB.WONo & "='" & MO_DocNo & "' and " & SFCB.RunCard & "='" & MO_Runcard & "' " &
                 " And " & SFCB.LineNo & "= " & P1 & " "
                Dim dtFP1 As DataTable = GetQueryDataTable(StrSQLPF1)
                If dtFP1.Rows.Count > 0 Then
                    Dim P_OP1 As String = dtRowsFormat.FormatString(dtFP1, SFCB.OperationID)
                    Dim P_WC1 As String = dtRowsFormat.FormatString(dtFP1, SFCB.WorkStation)
                    Dim P_TrsIn1 As String = dtRowsFormat.FormatString(dtFP1, SFCB.GoodTransferIn)
                    Dim P_WIP1 As String = dtRowsFormat.FormatString(dtFP1, SFCB.WIP)
                    Dim P_TrsOut1 As String = dtRowsFormat.FormatString(dtFP1, SFCB.GoodTransferOut)
                    If P_WC1 <> String.Empty Then
                        Dim dtWC1 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC1)
                        If dtWC1.Rows.Count > 0 Then
                            P_WC1 = dtRowsFormat.FormatSumString(dtWC1, ECAA.WorkcenterID, ECAA.Workcenter)
                        End If
                    End If
                    lblOPStep1.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP1 & "</span>"
                    lblWCStep1.Text = "<span style='color:indigo;'>" & P_WC1 & "</span>"
                    lblTrsInStep1.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn1 & "</span>"
                    lblWIPStep1.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP1 & "</span>"
                    lblTrsOutStep1.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut1 & "</span>"
                End If

                Dim StrSQLP As String = "select " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WorkStation & ", " &
            " " & SFCB.GoodTransferIn & "," & SFCB.WIP & "," & SFCB.GoodTransferOut & " " &
            " from " & SFCB.tblMOprocessItem_SFCB & " " &
            " where rownum = 1  And " & SFCB.WONo & "='" & MO_DocNo & "' and " & SFCB.RunCard & "='" & MO_Runcard & "' " &
            " And " & SFCB.LineNo & " > @pWhereCustom "
                '######### P2 #############################
                If P1 <> String.Empty Then
                    FindStep1 = CInt(P1)
                    Dim StrSQLP2 As String = StrSQLP.Replace("@pWhereCustom", FindStep1)
                    Dim dtP2 As DataTable = GetQueryDataTable(StrSQLP2)
                    If dtP2.Rows.Count > 0 Then
                        Dim lblStep2_C As Label = CType(e.Row.FindControl("lblStep2_C"), Label)
                        P2 = dtRowsFormat.FormatString(dtP2, SFCB.LineNo)
                        lblStep2_C.Text = P2
                        lblStep2.Text = Item_Seq & "<span style='color:Blue;'>" & P2 & "</span>"
                        Dim P_OP2 As String = dtRowsFormat.FormatString(dtP2, SFCB.OperationID)
                        Dim P_WC2 As String = dtRowsFormat.FormatString(dtP2, SFCB.WorkStation)
                        Dim P_TrsIn2 As String = dtRowsFormat.FormatString(dtP2, SFCB.GoodTransferIn)
                        Dim P_WIP2 As String = dtRowsFormat.FormatString(dtP2, SFCB.WIP)
                        Dim P_TrsOut2 As String = dtRowsFormat.FormatString(dtP2, SFCB.GoodTransferOut)
                        If P_WC2 <> String.Empty Then
                            Dim dtWC2 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC2)
                            If dtWC2.Rows.Count > 0 Then
                                P_WC2 = dtRowsFormat.FormatSumString(dtWC2, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep2.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP2 & "</span>"
                        lblWCStep2.Text = "<span style='color:indigo;'>" & P_WC2 & "</span>"
                        lblTrsInStep2.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn2 & "</span>"
                        lblWIPStep2.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP2 & "</span>"
                        lblTrsOutStep2.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut2 & "</span>"
                    End If
                End If
                '######### P3 #############################
                If P2 <> String.Empty Then
                    FindStep2 = CInt(P2)
                    Dim StrSQLP3 As String = StrSQLP.Replace("@pWhereCustom", FindStep2)
                    Dim dtP3 As DataTable = GetQueryDataTable(StrSQLP3)
                    If dtP3.Rows.Count > 0 Then
                        Dim lblStep3_C As Label = CType(e.Row.FindControl("lblStep3_C"), Label)
                        P3 = dtRowsFormat.FormatString(dtP3, SFCB.LineNo)
                        lblStep3_C.Text = P3
                        lblStep3.Text = Item_Seq & "<span style='color:Blue;'>" & P3 & "</span>"
                        Dim P_OP3 As String = dtRowsFormat.FormatString(dtP3, SFCB.OperationID)
                        Dim P_WC3 As String = dtRowsFormat.FormatString(dtP3, SFCB.WorkStation)
                        Dim P_TrsIn3 As String = dtRowsFormat.FormatString(dtP3, SFCB.GoodTransferIn)
                        Dim P_WIP3 As String = dtRowsFormat.FormatString(dtP3, SFCB.WIP)
                        Dim P_TrsOut3 As String = dtRowsFormat.FormatString(dtP3, SFCB.GoodTransferOut)
                        If P_WC3 <> String.Empty Then
                            Dim dtWC3 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC3)
                            If dtWC3.Rows.Count > 0 Then
                                P_WC3 = dtRowsFormat.FormatSumString(dtWC3, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep3.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP3 & "</span>"
                        lblWCStep3.Text = "<span style='color:indigo;'>" & P_WC3 & "</span>"
                        lblTrsInStep3.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn3 & "</span>"
                        lblWIPStep3.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP3 & "</span>"
                        lblTrsOutStep3.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut3 & "</span>"
                    End If
                End If
                ''######### P4 #############################
                If P3 <> String.Empty Then
                    FindStep3 = CInt(P3)
                    Dim StrSQLP4 As String = StrSQLP.Replace("@pWhereCustom", FindStep3)
                    Dim dtP4 As DataTable = GetQueryDataTable(StrSQLP4)
                    If dtP4.Rows.Count > 0 Then
                        Dim lblStep4_C As Label = CType(e.Row.FindControl("lblStep4_C"), Label)
                        P4 = dtRowsFormat.FormatString(dtP4, SFCB.LineNo)
                        lblStep4_C.Text = P4
                        lblStep4.Text = Item_Seq & "<span style='color:Blue;'>" & P4 & "</span>"
                        Dim P_OP4 As String = dtRowsFormat.FormatString(dtP4, SFCB.OperationID)
                        Dim P_WC4 As String = dtRowsFormat.FormatString(dtP4, SFCB.WorkStation)
                        Dim P_TrsIn4 As String = dtRowsFormat.FormatString(dtP4, SFCB.GoodTransferIn)
                        Dim P_WIP4 As String = dtRowsFormat.FormatString(dtP4, SFCB.WIP)
                        Dim P_TrsOut4 As String = dtRowsFormat.FormatString(dtP4, SFCB.GoodTransferOut)
                        If P_WC4 <> String.Empty Then
                            Dim dtWC4 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC4)
                            If dtWC4.Rows.Count > 0 Then
                                P_WC4 = dtRowsFormat.FormatSumString(dtWC4, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep4.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP4 & "</span>"
                        lblWCStep4.Text = "<span style='color:indigo;'>" & P_WC4 & "</span>"
                        lblTrsInStep4.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn4 & "</span>"
                        lblWIPStep4.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP4 & "</span>"
                        lblTrsOutStep4.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut4 & "</span>"
                    End If
                End If
                ''######### P5 #############################
                If P4 <> String.Empty Then
                    FindStep4 = CInt(P4)
                    Dim StrSQLP5 As String = StrSQLP.Replace("@pWhereCustom", FindStep4)
                    Dim dtP5 As DataTable = GetQueryDataTable(StrSQLP5)
                    If dtP5.Rows.Count > 0 Then
                        Dim lblStep5_C As Label = CType(e.Row.FindControl("lblStep5_C"), Label)
                        P5 = dtRowsFormat.FormatString(dtP5, SFCB.LineNo)
                        lblStep5_C.Text = P5
                        lblStep5.Text = Item_Seq & "<span style='color:Blue;'>" & P5 & "</span>"
                        Dim P_OP5 As String = dtRowsFormat.FormatString(dtP5, SFCB.OperationID)
                        Dim P_WC5 As String = dtRowsFormat.FormatString(dtP5, SFCB.WorkStation)
                        Dim P_TrsIn5 As String = dtRowsFormat.FormatString(dtP5, SFCB.GoodTransferIn)
                        Dim P_WIP5 As String = dtRowsFormat.FormatString(dtP5, SFCB.WIP)
                        Dim P_TrsOut5 As String = dtRowsFormat.FormatString(dtP5, SFCB.GoodTransferOut)
                        If P_WC5 <> String.Empty Then
                            Dim dtWC5 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC5)
                            If dtWC5.Rows.Count > 0 Then
                                P_WC5 = dtRowsFormat.FormatSumString(dtWC5, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep5.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP5 & "</span>"
                        lblWCStep5.Text = "<span style='color:indigo;'>" & P_WC5 & "</span>"
                        lblTrsInStep5.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn5 & "</span>"
                        lblWIPStep5.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP5 & "</span>"
                        lblTrsOutStep5.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut5 & "</span>"
                    End If
                End If
                ''######### P6 #############################
                If P5 <> String.Empty Then
                    FindStep5 = CInt(P5)
                    Dim StrSQLP6 As String = StrSQLP.Replace("@pWhereCustom", FindStep5)
                    Dim dtP6 As DataTable = GetQueryDataTable(StrSQLP6)
                    If dtP6.Rows.Count > 0 Then
                        Dim lblStep6_C As Label = CType(e.Row.FindControl("lblStep6_C"), Label)
                        P6 = dtRowsFormat.FormatString(dtP6, SFCB.LineNo)
                        lblStep6_C.Text = P6
                        lblStep6.Text = Item_Seq & "<span style='color:Blue;'>" & P6 & "</span>"
                        Dim P_OP6 As String = dtRowsFormat.FormatString(dtP6, SFCB.OperationID)
                        Dim P_WC6 As String = dtRowsFormat.FormatString(dtP6, SFCB.WorkStation)
                        Dim P_TrsIn6 As String = dtRowsFormat.FormatString(dtP6, SFCB.GoodTransferIn)
                        Dim P_WIP6 As String = dtRowsFormat.FormatString(dtP6, SFCB.WIP)
                        Dim P_TrsOut6 As String = dtRowsFormat.FormatString(dtP6, SFCB.GoodTransferOut)
                        If P_WC6 <> String.Empty Then
                            Dim dtWC6 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC6)
                            If dtWC6.Rows.Count > 0 Then
                                P_WC6 = dtRowsFormat.FormatSumString(dtWC6, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep6.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP6 & "</span>"
                        lblWCStep6.Text = "<span style='color:indigo;'>" & P_WC6 & "</span>"
                        lblTrsInStep6.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn6 & "</span>"
                        lblWIPStep6.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP6 & "</span>"
                        lblTrsOutStep6.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut6 & "</span>"
                    End If
                End If
                ''######### P7 #############################
                If P6 <> String.Empty Then
                    FindStep6 = CInt(P6)
                    Dim StrSQLP7 As String = StrSQLP.Replace("@pWhereCustom", FindStep6)
                    Dim dtP7 As DataTable = GetQueryDataTable(StrSQLP7)
                    If dtP7.Rows.Count > 0 Then
                        Dim lblStep7_C As Label = CType(e.Row.FindControl("lblStep7_C"), Label)
                        P7 = dtRowsFormat.FormatString(dtP7, SFCB.LineNo)
                        lblStep7_C.Text = P7
                        lblStep7.Text = Item_Seq & "<span style='color:Blue;'>" & P7 & "</span>"
                        Dim P_OP7 As String = dtRowsFormat.FormatString(dtP7, SFCB.OperationID)
                        Dim P_WC7 As String = dtRowsFormat.FormatString(dtP7, SFCB.WorkStation)
                        Dim P_TrsIn7 As String = dtRowsFormat.FormatString(dtP7, SFCB.GoodTransferIn)
                        Dim P_WIP7 As String = dtRowsFormat.FormatString(dtP7, SFCB.WIP)
                        Dim P_TrsOut7 As String = dtRowsFormat.FormatString(dtP7, SFCB.GoodTransferOut)
                        If P_WC7 <> String.Empty Then
                            Dim dtWC7 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC7)
                            If dtWC7.Rows.Count > 0 Then
                                P_WC7 = dtRowsFormat.FormatSumString(dtWC7, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep7.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP7 & "</span>"
                        lblWCStep7.Text = "<span style='color:indigo;'>" & P_WC7 & "</span>"
                        lblTrsInStep7.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn7 & "</span>"
                        lblWIPStep7.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP7 & "</span>"
                        lblTrsOutStep7.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut7 & "</span>"
                    End If
                End If
                ''######### P8 #############################
                If P7 <> String.Empty Then
                    FindStep7 = CInt(P7)
                    Dim StrSQLP8 As String = StrSQLP.Replace("@pWhereCustom", FindStep7)
                    Dim dtP8 As DataTable = GetQueryDataTable(StrSQLP8)
                    If dtP8.Rows.Count > 0 Then
                        Dim lblStep8_C As Label = CType(e.Row.FindControl("lblStep8_C"), Label)
                        P8 = dtRowsFormat.FormatString(dtP8, SFCB.LineNo)
                        lblStep8_C.Text = P8
                        lblStep8.Text = Item_Seq & "<span style='color:Blue;'>" & P8 & "</span>"
                        Dim P_OP8 As String = dtRowsFormat.FormatString(dtP8, SFCB.OperationID)
                        Dim P_WC8 As String = dtRowsFormat.FormatString(dtP8, SFCB.WorkStation)
                        Dim P_TrsIn8 As String = dtRowsFormat.FormatString(dtP8, SFCB.GoodTransferIn)
                        Dim P_WIP8 As String = dtRowsFormat.FormatString(dtP8, SFCB.WIP)
                        Dim P_TrsOut8 As String = dtRowsFormat.FormatString(dtP8, SFCB.GoodTransferOut)
                        If P_WC8 <> String.Empty Then
                            Dim dtWC8 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC8)
                            If dtWC8.Rows.Count > 0 Then
                                P_WC8 = dtRowsFormat.FormatSumString(dtWC8, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep8.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP8 & "</span>"
                        lblWCStep8.Text = "<span style='color:indigo;'>" & P_WC8 & "</span>"
                        lblTrsInStep8.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn8 & "</span>"
                        lblWIPStep8.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP8 & "</span>"
                        lblTrsOutStep8.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut8 & "</span>"
                    End If
                End If
                ''######### P9 #############################
                If P8 <> String.Empty Then
                    FindStep8 = CInt(P8)
                    Dim StrSQLP9 As String = StrSQLP.Replace("@pWhereCustom", FindStep8)
                    Dim dtP9 As DataTable = GetQueryDataTable(StrSQLP9)
                    If dtP9.Rows.Count > 0 Then
                        Dim lblStep9_C As Label = CType(e.Row.FindControl("lblStep9_C"), Label)
                        P9 = dtRowsFormat.FormatString(dtP9, SFCB.LineNo)
                        lblStep9_C.Text = P9
                        lblStep9.Text = Item_Seq & "<span style='color:Blue;'>" & P9 & "</span>"
                        Dim P_OP9 As String = dtRowsFormat.FormatString(dtP9, SFCB.OperationID)
                        Dim P_WC9 As String = dtRowsFormat.FormatString(dtP9, SFCB.WorkStation)
                        Dim P_TrsIn9 As String = dtRowsFormat.FormatString(dtP9, SFCB.GoodTransferIn)
                        Dim P_WIP9 As String = dtRowsFormat.FormatString(dtP9, SFCB.WIP)
                        Dim P_TrsOut9 As String = dtRowsFormat.FormatString(dtP9, SFCB.GoodTransferOut)
                        If P_WC9 <> String.Empty Then
                            Dim dtWC9 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC9)
                            If dtWC9.Rows.Count > 0 Then
                                P_WC9 = dtRowsFormat.FormatSumString(dtWC9, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep9.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP9 & "</span>"
                        lblWCStep9.Text = "<span style='color:indigo;'>" & P_WC9 & "</span>"
                        lblTrsInStep9.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn9 & "</span>"
                        lblWIPStep9.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP9 & "</span>"
                        lblTrsOutStep9.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut9 & "</span>"
                    End If
                End If
                ''######### P10 #############################
                If P9 <> String.Empty Then
                    FindStep9 = CInt(P9)
                    Dim StrSQLP10 As String = StrSQLP.Replace("@pWhereCustom", FindStep9)
                    Dim dtP10 As DataTable = GetQueryDataTable(StrSQLP10)
                    If dtP10.Rows.Count > 0 Then
                        Dim lblStep10_C As Label = CType(e.Row.FindControl("lblStep10_C"), Label)
                        P10 = dtRowsFormat.FormatString(dtP10, SFCB.LineNo)
                        lblStep10_C.Text = P10
                        lblStep10.Text = Item_Seq & "<span style='color:Blue;'>" & P10 & "</span>"
                        Dim P_OP10 As String = dtRowsFormat.FormatString(dtP10, SFCB.OperationID)
                        Dim P_WC10 As String = dtRowsFormat.FormatString(dtP10, SFCB.WorkStation)
                        Dim P_TrsIn10 As String = dtRowsFormat.FormatString(dtP10, SFCB.GoodTransferIn)
                        Dim P_WIP10 As String = dtRowsFormat.FormatString(dtP10, SFCB.WIP)
                        Dim P_TrsOut10 As String = dtRowsFormat.FormatString(dtP10, SFCB.GoodTransferOut)
                        If P_WC10 <> String.Empty Then
                            Dim dtWC10 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC10)
                            If dtWC10.Rows.Count > 0 Then
                                P_WC10 = dtRowsFormat.FormatSumString(dtWC10, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep10.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP10 & "</span>"
                        lblWCStep10.Text = "<span style='color:indigo;'>" & P_WC10 & "</span>"
                        lblTrsInStep10.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn10 & "</span>"
                        lblWIPStep10.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP10 & "</span>"
                        lblTrsOutStep10.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut10 & "</span>"
                    End If
                End If
                ''######### P11 #############################
                If P10 <> String.Empty Then
                    FindStep10 = CInt(P10)
                    Dim StrSQLP11 As String = StrSQLP.Replace("@pWhereCustom", FindStep10)
                    Dim dtP11 As DataTable = GetQueryDataTable(StrSQLP11)
                    If dtP11.Rows.Count > 0 Then
                        Dim lblStep11_C As Label = CType(e.Row.FindControl("lblStep11_C"), Label)
                        P11 = dtRowsFormat.FormatString(dtP11, SFCB.LineNo)
                        lblStep11_C.Text = P11
                        lblStep11.Text = Item_Seq & "<span style='color:Blue;'>" & P11 & "</span>"
                        Dim P_OP11 As String = dtRowsFormat.FormatString(dtP11, SFCB.OperationID)
                        Dim P_WC11 As String = dtRowsFormat.FormatString(dtP11, SFCB.WorkStation)
                        Dim P_TrsIn11 As String = dtRowsFormat.FormatString(dtP11, SFCB.GoodTransferIn)
                        Dim P_WIP11 As String = dtRowsFormat.FormatString(dtP11, SFCB.WIP)
                        Dim P_TrsOut11 As String = dtRowsFormat.FormatString(dtP11, SFCB.GoodTransferOut)
                        If P_WC11 <> String.Empty Then
                            Dim dtWC11 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC11)
                            If dtWC11.Rows.Count > 0 Then
                                P_WC11 = dtRowsFormat.FormatSumString(dtWC11, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep11.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP11 & "</span>"
                        lblWCStep11.Text = "<span style='color:indigo;'>" & P_WC11 & "</span>"
                        lblTrsInStep11.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn11 & "</span>"
                        lblWIPStep11.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP11 & "</span>"
                        lblTrsOutStep11.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut11 & "</span>"
                    End If
                End If
                ''######### P12 #############################
                If P11 <> String.Empty Then
                    FindStep11 = CInt(P11)
                    Dim StrSQLP12 As String = StrSQLP.Replace("@pWhereCustom", FindStep11)
                    Dim dtP12 As DataTable = GetQueryDataTable(StrSQLP12)
                    If dtP12.Rows.Count > 0 Then
                        Dim lblStep12_C As Label = CType(e.Row.FindControl("lblStep12_C"), Label)
                        P12 = dtRowsFormat.FormatString(dtP12, SFCB.LineNo)
                        lblStep12_C.Text = P12
                        lblStep12.Text = Item_Seq & "<span style='color:Blue;'>" & P12 & "</span>"
                        Dim P_OP12 As String = dtRowsFormat.FormatString(dtP12, SFCB.OperationID)
                        Dim P_WC12 As String = dtRowsFormat.FormatString(dtP12, SFCB.WorkStation)
                        Dim P_TrsIn12 As String = dtRowsFormat.FormatString(dtP12, SFCB.GoodTransferIn)
                        Dim P_WIP12 As String = dtRowsFormat.FormatString(dtP12, SFCB.WIP)
                        Dim P_TrsOut12 As String = dtRowsFormat.FormatString(dtP12, SFCB.GoodTransferOut)
                        If P_WC12 <> String.Empty Then
                            Dim dtWC12 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC12)
                            If dtWC12.Rows.Count > 0 Then
                                P_WC12 = dtRowsFormat.FormatSumString(dtWC12, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep12.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP12 & "</span>"
                        lblWCStep12.Text = "<span style='color:indigo;'>" & P_WC12 & "</span>"
                        lblTrsInStep12.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn12 & "</span>"
                        lblWIPStep12.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP12 & "</span>"
                        lblTrsOutStep12.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut12 & "</span>"
                    End If
                End If
                ''######### P13 #############################
                If P12 <> String.Empty Then
                    FindStep12 = CInt(P12)
                    Dim StrSQLP13 As String = StrSQLP.Replace("@pWhereCustom", FindStep12)
                    Dim dtP13 As DataTable = GetQueryDataTable(StrSQLP13)
                    If dtP13.Rows.Count > 0 Then
                        Dim lblStep13_C As Label = CType(e.Row.FindControl("lblStep13_C"), Label)
                        P13 = dtRowsFormat.FormatString(dtP13, SFCB.LineNo)
                        lblStep13_C.Text = P13
                        lblStep13.Text = Item_Seq & "<span style='color:Blue;'>" & P14 & "</span>"
                        Dim P_OP13 As String = dtRowsFormat.FormatString(dtP13, SFCB.OperationID)
                        Dim P_WC13 As String = dtRowsFormat.FormatString(dtP13, SFCB.WorkStation)
                        Dim P_TrsIn13 As String = dtRowsFormat.FormatString(dtP13, SFCB.GoodTransferIn)
                        Dim P_WIP13 As String = dtRowsFormat.FormatString(dtP13, SFCB.WIP)
                        Dim P_TrsOut13 As String = dtRowsFormat.FormatString(dtP13, SFCB.GoodTransferOut)
                        If P_WC13 <> String.Empty Then
                            Dim dtWC13 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC13)
                            If dtWC13.Rows.Count > 0 Then
                                P_WC13 = dtRowsFormat.FormatSumString(dtWC13, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep13.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP13 & "</span>"
                        lblWCStep13.Text = "<span style='color:indigo;'>" & P_WC13 & "</span>"
                        lblTrsInStep13.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn13 & "</span>"
                        lblWIPStep13.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP13 & "</span>"
                        lblTrsOutStep13.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut13 & "</span>"
                    End If
                End If
                ''######### P14 #############################
                If P13 <> String.Empty Then
                    FindStep13 = CInt(P13)
                    Dim StrSQLP14 As String = StrSQLP.Replace("@pWhereCustom", FindStep13)
                    Dim dtP14 As DataTable = GetQueryDataTable(StrSQLP14)
                    If dtP14.Rows.Count > 0 Then
                        Dim lblStep14_C As Label = CType(e.Row.FindControl("lblStep14_C"), Label)
                        P14 = dtRowsFormat.FormatString(dtP14, SFCB.LineNo)
                        lblStep14_C.Text = P14
                        lblStep14.Text = Item_Seq & "<span style='color:Blue;'>" & P14 & "</span>"
                        Dim P_OP14 As String = dtRowsFormat.FormatString(dtP14, SFCB.OperationID)
                        Dim P_WC14 As String = dtRowsFormat.FormatString(dtP14, SFCB.WorkStation)
                        Dim P_TrsIn14 As String = dtRowsFormat.FormatString(dtP14, SFCB.GoodTransferIn)
                        Dim P_WIP14 As String = dtRowsFormat.FormatString(dtP14, SFCB.WIP)
                        Dim P_TrsOut14 As String = dtRowsFormat.FormatString(dtP14, SFCB.GoodTransferOut)
                        If P_WC14 <> String.Empty Then
                            Dim dtWC14 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC14)
                            If dtWC14.Rows.Count > 0 Then
                                P_WC14 = dtRowsFormat.FormatSumString(dtWC14, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep14.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP14 & "</span>"
                        lblWCStep14.Text = "<span style='color:indigo;'>" & P_WC14 & "</span>"
                        lblTrsInStep14.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn14 & "</span>"
                        lblWIPStep14.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP14 & "</span>"
                        lblTrsOutStep14.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut14 & "</span>"
                    End If
                End If
                ''######### P15 #############################
                If P14 <> String.Empty Then
                    FindStep14 = CInt(P14)
                    Dim StrSQLP15 As String = StrSQLP.Replace("@pWhereCustom", FindStep14)
                    Dim dtP15 As DataTable = GetQueryDataTable(StrSQLP15)
                    If dtP15.Rows.Count > 0 Then
                        Dim lblStep15_C As Label = CType(e.Row.FindControl("lblStep15_C"), Label)
                        P15 = dtRowsFormat.FormatString(dtP15, SFCB.LineNo)
                        lblStep15_C.Text = P15
                        lblStep15.Text = Item_Seq & "<span style='color:Blue;'>" & P15 & "</span>"
                        Dim P_OP15 As String = dtRowsFormat.FormatString(dtP15, SFCB.OperationID)
                        Dim P_WC15 As String = dtRowsFormat.FormatString(dtP15, SFCB.WorkStation)
                        Dim P_TrsIn15 As String = dtRowsFormat.FormatString(dtP15, SFCB.GoodTransferIn)
                        Dim P_WIP15 As String = dtRowsFormat.FormatString(dtP15, SFCB.WIP)
                        Dim P_TrsOut15 As String = dtRowsFormat.FormatString(dtP15, SFCB.GoodTransferOut)
                        If P_WC15 <> String.Empty Then
                            Dim dtWC15 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC15)
                            If dtWC15.Rows.Count > 0 Then
                                P_WC15 = dtRowsFormat.FormatSumString(dtWC15, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep15.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP15 & "</span>"
                        lblWCStep15.Text = "<span style='color:indigo;'>" & P_WC15 & "</span>"
                        lblTrsInStep15.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn15 & "</span>"
                        lblWIPStep15.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP15 & "</span>"
                        lblTrsOutStep15.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut15 & "</span>"
                    End If
                End If
                ''######### P16 #############################
                If P15 <> String.Empty Then
                    FindStep15 = CInt(P15)
                    Dim StrSQLP16 As String = StrSQLP.Replace("@pWhereCustom", FindStep15)
                    Dim dtP16 As DataTable = GetQueryDataTable(StrSQLP16)
                    If dtP16.Rows.Count > 0 Then
                        Dim lblStep16_C As Label = CType(e.Row.FindControl("lblStep16_C"), Label)
                        P16 = dtRowsFormat.FormatString(dtP16, SFCB.LineNo)
                        lblStep16_C.Text = P16
                        lblStep16.Text = Item_Seq & "<span style='color:Blue;'>" & P16 & "</span>"
                        Dim P_OP16 As String = dtRowsFormat.FormatString(dtP16, SFCB.OperationID)
                        Dim P_WC16 As String = dtRowsFormat.FormatString(dtP16, SFCB.WorkStation)
                        Dim P_TrsIn16 As String = dtRowsFormat.FormatString(dtP16, SFCB.GoodTransferIn)
                        Dim P_WIP16 As String = dtRowsFormat.FormatString(dtP16, SFCB.WIP)
                        Dim P_TrsOut16 As String = dtRowsFormat.FormatString(dtP16, SFCB.GoodTransferOut)
                        If P_WC16 <> String.Empty Then
                            Dim dtWC16 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC16)
                            If dtWC16.Rows.Count > 0 Then
                                P_WC16 = dtRowsFormat.FormatSumString(dtWC16, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep16.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP16 & "</span>"
                        lblWCStep16.Text = "<span style='color:indigo;'>" & P_WC16 & "</span>"
                        lblTrsInStep16.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn16 & "</span>"
                        lblWIPStep16.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP16 & "</span>"
                        lblTrsOutStep16.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut16 & "</span>"
                    End If
                End If
                ''######### P17 #############################
                If P16 <> String.Empty Then
                    FindStep16 = CInt(P16)
                    Dim StrSQLP17 As String = StrSQLP.Replace("@pWhereCustom", FindStep16)
                    Dim dtP17 As DataTable = GetQueryDataTable(StrSQLP17)
                    If dtP17.Rows.Count > 0 Then
                        Dim lblStep17_C As Label = CType(e.Row.FindControl("lblStep17_C"), Label)
                        P17 = dtRowsFormat.FormatString(dtP17, SFCB.LineNo)
                        lblStep17_C.Text = P17
                        lblStep17.Text = Item_Seq & "<span style='color:Blue;'>" & P17 & "</span>"
                        Dim P_OP17 As String = dtRowsFormat.FormatString(dtP17, SFCB.OperationID)
                        Dim P_WC17 As String = dtRowsFormat.FormatString(dtP17, SFCB.WorkStation)
                        Dim P_TrsIn17 As String = dtRowsFormat.FormatString(dtP17, SFCB.GoodTransferIn)
                        Dim P_WIP17 As String = dtRowsFormat.FormatString(dtP17, SFCB.WIP)
                        Dim P_TrsOut17 As String = dtRowsFormat.FormatString(dtP17, SFCB.GoodTransferOut)
                        If P_WC17 <> String.Empty Then
                            Dim dtWC17 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC17)
                            If dtWC17.Rows.Count > 0 Then
                                P_WC17 = dtRowsFormat.FormatSumString(dtWC17, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep17.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP17 & "</span>"
                        lblWCStep17.Text = "<span style='color:indigo;'>" & P_WC17 & "</span>"
                        lblTrsInStep17.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn17 & "</span>"
                        lblWIPStep17.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP17 & "</span>"
                        lblTrsOutStep17.Text = ssTrsOutP & "<span style='color:indigo;'>" & P_TrsOut17 & "</span>"
                    End If
                End If
                ''######### P18 #############################
                If P17 <> String.Empty Then
                    FindStep17 = CInt(P17)
                    Dim StrSQLP18 As String = StrSQLP.Replace("@pWhereCustom", FindStep17)
                    Dim dtP18 As DataTable = GetQueryDataTable(StrSQLP18)
                    If dtP18.Rows.Count > 0 Then
                        Dim lblStep18_C As Label = CType(e.Row.FindControl("lblStep18_C"), Label)
                        P18 = dtRowsFormat.FormatString(dtP18, SFCB.LineNo)
                        lblStep18_C.Text = P18
                        lblStep18.Text = Item_Seq & "<span style='color:Blue;'>" & P18 & "</span>"
                        Dim P_OP18 As String = dtRowsFormat.FormatString(dtP18, SFCB.OperationID)
                        Dim P_WC18 As String = dtRowsFormat.FormatString(dtP18, SFCB.WorkStation)
                        Dim P_TrsIn18 As String = dtRowsFormat.FormatString(dtP18, SFCB.GoodTransferIn)
                        Dim P_WIP18 As String = dtRowsFormat.FormatString(dtP18, SFCB.WIP)
                        Dim P_TrsOut18 As String = dtRowsFormat.FormatString(dtP18, SFCB.GoodTransferOut)
                        If P_WC18 <> String.Empty Then
                            Dim dtWC18 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC18)
                            If dtWC18.Rows.Count > 0 Then
                                P_WC18 = dtRowsFormat.FormatSumString(dtWC18, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep18.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP18 & "</span>"
                        lblWCStep18.Text = "<span style='color:indigo;'>" & P_WC18 & "</span>"
                        lblTrsInStep18.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn18 & "</span>"
                        lblWIPStep18.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP18 & "</span>"
                    End If
                End If
                ''######### P19 #############################
                If P18 <> String.Empty Then
                    FindStep18 = CInt(P18)
                    Dim StrSQLP19 As String = StrSQLP.Replace("@pWhereCustom", FindStep18)
                    Dim dtP19 As DataTable = GetQueryDataTable(StrSQLP19)
                    If dtP19.Rows.Count > 0 Then
                        Dim lblStep19_C As Label = CType(e.Row.FindControl("lblStep19_C"), Label)
                        P19 = dtRowsFormat.FormatString(dtP19, SFCB.LineNo)
                        lblStep19_C.Text = P19
                        lblStep19.Text = Item_Seq & "<span style='color:Blue;'>" & P19 & "</span>"
                        Dim P_OP19 As String = dtRowsFormat.FormatString(dtP19, SFCB.OperationID)
                        Dim P_WC19 As String = dtRowsFormat.FormatString(dtP19, SFCB.WorkStation)
                        Dim P_TrsIn19 As String = dtRowsFormat.FormatString(dtP19, SFCB.GoodTransferIn)
                        Dim P_WIP19 As String = dtRowsFormat.FormatString(dtP19, SFCB.WIP)
                        Dim P_TrsOut19 As String = dtRowsFormat.FormatString(dtP19, SFCB.GoodTransferOut)
                        If P_WC19 <> String.Empty Then
                            Dim dtWC19 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC19)
                            If dtWC19.Rows.Count > 0 Then
                                P_WC19 = dtRowsFormat.FormatSumString(dtWC19, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep19.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP19 & "</span>"
                        lblWCStep19.Text = "<span style='color:indigo;'>" & P_WC19 & "</span>"
                        lblTrsInStep19.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn19 & "</span>"
                        lblWIPStep19.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP19 & "</span>"
                    End If
                End If
                ''######### P20 #############################
                If P19 <> String.Empty Then
                    FindStep19 = CInt(P19)
                    Dim StrSQLP20 As String = StrSQLP.Replace("@pWhereCustom", FindStep19)
                    Dim dtP20 As DataTable = GetQueryDataTable(StrSQLP20)
                    If dtP20.Rows.Count > 0 Then
                        Dim lblStep20_C As Label = CType(e.Row.FindControl("lblStep20_C"), Label)
                        P20 = dtRowsFormat.FormatString(dtP20, SFCB.LineNo)
                        lblStep20_C.Text = P20
                        lblStep20.Text = Item_Seq & "<span style='color:Blue;'>" & P20 & "</span>"
                        Dim P_OP20 As String = dtRowsFormat.FormatString(dtP20, SFCB.OperationID)
                        Dim P_WC20 As String = dtRowsFormat.FormatString(dtP20, SFCB.WorkStation)
                        Dim P_TrsIn20 As String = dtRowsFormat.FormatString(dtP20, SFCB.GoodTransferIn)
                        Dim P_WIP20 As String = dtRowsFormat.FormatString(dtP20, SFCB.WIP)
                        Dim P_TrsOut20 As String = dtRowsFormat.FormatString(dtP20, SFCB.GoodTransferOut)
                        If P_WC20 <> String.Empty Then
                            Dim dtWC20 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC20)
                            If dtWC20.Rows.Count > 0 Then
                                P_WC20 = dtRowsFormat.FormatSumString(dtWC20, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep20.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP20 & "</span>"
                        lblWCStep20.Text = "<span style='color:indigo;'>" & P_WC20 & "</span>"
                        lblTrsInStep20.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn20 & "</span>"
                        lblWIPStep20.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP20 & "</span>"
                    End If
                End If
                ''######### P21 #############################
                If P20 <> String.Empty Then
                    FindStep20 = CInt(P20)
                    Dim StrSQLP21 As String = StrSQLP.Replace("@pWhereCustom", FindStep20)
                    Dim dtP21 As DataTable = GetQueryDataTable(StrSQLP21)
                    If dtP21.Rows.Count > 0 Then
                        Dim lblStep21_C As Label = CType(e.Row.FindControl("lblStep21_C"), Label)
                        P21 = dtRowsFormat.FormatString(dtP21, SFCB.LineNo)
                        lblStep21_C.Text = P21
                        lblStep21.Text = Item_Seq & "<span style='color:Blue;'>" & P21 & "</span>"
                        Dim P_OP21 As String = dtRowsFormat.FormatString(dtP21, SFCB.OperationID)
                        Dim P_WC21 As String = dtRowsFormat.FormatString(dtP21, SFCB.WorkStation)
                        Dim P_TrsIn21 As String = dtRowsFormat.FormatString(dtP21, SFCB.GoodTransferIn)
                        Dim P_WIP21 As String = dtRowsFormat.FormatString(dtP21, SFCB.WIP)
                        Dim P_TrsOut21 As String = dtRowsFormat.FormatString(dtP21, SFCB.GoodTransferOut)
                        If P_WC21 <> String.Empty Then
                            Dim dtWC21 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC21)
                            If dtWC21.Rows.Count > 0 Then
                                P_WC21 = dtRowsFormat.FormatSumString(dtWC21, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep21.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP21 & "</span>"
                        lblWCStep21.Text = "<span style='color:indigo;'>" & P_WC21 & "</span>"
                        lblTrsInStep21.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn21 & "</span>"
                        lblWIPStep21.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP21 & "</span>"
                    End If
                End If
                ''######### P22 #############################
                If P21 <> String.Empty Then
                    FindStep21 = CInt(P21)
                    Dim StrSQLP22 As String = StrSQLP.Replace("@pWhereCustom", FindStep21)
                    Dim dtP22 As DataTable = GetQueryDataTable(StrSQLP22)
                    If dtP22.Rows.Count > 0 Then
                        Dim lblStep22_C As Label = CType(e.Row.FindControl("lblStep22_C"), Label)
                        P22 = dtRowsFormat.FormatString(dtP22, SFCB.LineNo)
                        lblStep22_C.Text = P22
                        lblStep22.Text = Item_Seq & "<span style='color:Blue;'>" & P22 & "</span>"
                        Dim P_OP22 As String = dtRowsFormat.FormatString(dtP22, SFCB.OperationID)
                        Dim P_WC22 As String = dtRowsFormat.FormatString(dtP22, SFCB.WorkStation)
                        Dim P_TrsIn22 As String = dtRowsFormat.FormatString(dtP22, SFCB.GoodTransferIn)
                        Dim P_WIP22 As String = dtRowsFormat.FormatString(dtP22, SFCB.WIP)
                        Dim P_TrsOut22 As String = dtRowsFormat.FormatString(dtP22, SFCB.GoodTransferOut)
                        If P_WC22 <> String.Empty Then
                            Dim dtWC22 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC22)
                            If dtWC22.Rows.Count > 0 Then
                                P_WC22 = dtRowsFormat.FormatSumString(dtWC22, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep22.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP22 & "</span>"
                        lblWCStep22.Text = "<span style='color:indigo;'>" & P_WC22 & "</span>"
                        lblTrsInStep22.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn22 & "</span>"
                        lblWIPStep22.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP22 & "</span>"
                    End If
                End If
                ''######### P23 #############################
                If P22 <> String.Empty Then
                    FindStep22 = CInt(P22)
                    Dim StrSQLP23 As String = StrSQLP.Replace("@pWhereCustom", FindStep22)
                    Dim dtP23 As DataTable = GetQueryDataTable(StrSQLP23)
                    If dtP23.Rows.Count > 0 Then
                        Dim lblStep23_C As Label = CType(e.Row.FindControl("lblStep23_C"), Label)
                        P23 = dtRowsFormat.FormatString(dtP23, SFCB.LineNo)
                        lblStep23_C.Text = P23
                        lblStep23.Text = Item_Seq & "<span style='color:Blue;'>" & P23 & "</span>"
                        Dim P_OP23 As String = dtRowsFormat.FormatString(dtP23, SFCB.OperationID)
                        Dim P_WC23 As String = dtRowsFormat.FormatString(dtP23, SFCB.WorkStation)
                        Dim P_TrsIn23 As String = dtRowsFormat.FormatString(dtP23, SFCB.GoodTransferIn)
                        Dim P_WIP23 As String = dtRowsFormat.FormatString(dtP23, SFCB.WIP)
                        Dim P_TrsOut23 As String = dtRowsFormat.FormatString(dtP23, SFCB.GoodTransferOut)
                        If P_WC23 <> String.Empty Then
                            Dim dtWC23 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC23)
                            If dtWC23.Rows.Count > 0 Then
                                P_WC23 = dtRowsFormat.FormatSumString(dtWC23, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep23.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP23 & "</span>"
                        lblWCStep23.Text = "<span style='color:indigo;'>" & P_WC23 & "</span>"
                        lblTrsInStep23.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn23 & "</span>"
                        lblWIPStep23.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP23 & "</span>"
                    End If
                End If
                ''######### P24 #############################
                If P23 <> String.Empty Then
                    FindStep23 = CInt(P23)
                    Dim StrSQLP24 As String = StrSQLP.Replace("@pWhereCustom", FindStep23)
                    Dim dtP24 As DataTable = GetQueryDataTable(StrSQLP24)
                    If dtP24.Rows.Count > 0 Then
                        Dim lblStep24_C As Label = CType(e.Row.FindControl("lblStep24_C"), Label)
                        P24 = dtRowsFormat.FormatString(dtP24, SFCB.LineNo)
                        lblStep24_C.Text = P24
                        lblStep24.Text = Item_Seq & "<span style='color:Blue;'>" & P24 & "</span>"
                        Dim P_OP24 As String = dtRowsFormat.FormatString(dtP24, SFCB.OperationID)
                        Dim P_WC24 As String = dtRowsFormat.FormatString(dtP24, SFCB.WorkStation)
                        Dim P_TrsIn24 As String = dtRowsFormat.FormatString(dtP24, SFCB.GoodTransferIn)
                        Dim P_WIP24 As String = dtRowsFormat.FormatString(dtP24, SFCB.WIP)
                        Dim P_TrsOut24 As String = dtRowsFormat.FormatString(dtP24, SFCB.GoodTransferOut)
                        If P_WC24 <> String.Empty Then
                            Dim dtWC24 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC24)
                            If dtWC24.Rows.Count > 0 Then
                                P_WC24 = dtRowsFormat.FormatSumString(dtWC24, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep24.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP24 & "</span>"
                        lblWCStep24.Text = "<span style='color:indigo;'>" & P_WC24 & "</span>"
                        lblTrsInStep24.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn24 & "</span>"
                        lblWIPStep24.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP24 & "</span>"
                    End If
                End If
                ''######### P25 #############################
                If P24 <> String.Empty Then
                    FindStep24 = CInt(P24)
                    Dim StrSQLP25 As String = StrSQLP.Replace("@pWhereCustom", FindStep24)
                    Dim dtP25 As DataTable = GetQueryDataTable(StrSQLP25)
                    If dtP25.Rows.Count > 0 Then
                        Dim lblStep25_C As Label = CType(e.Row.FindControl("lblStep25_C"), Label)
                        P25 = dtRowsFormat.FormatString(dtP25, SFCB.LineNo)
                        lblStep25_C.Text = P25
                        lblStep25.Text = Item_Seq & "<span style='color:Blue;'>" & P25 & "</span>"
                        Dim P_OP25 As String = dtRowsFormat.FormatString(dtP25, SFCB.OperationID)
                        Dim P_WC25 As String = dtRowsFormat.FormatString(dtP25, SFCB.WorkStation)
                        Dim P_TrsIn25 As String = dtRowsFormat.FormatString(dtP25, SFCB.GoodTransferIn)
                        Dim P_WIP25 As String = dtRowsFormat.FormatString(dtP25, SFCB.WIP)
                        Dim P_TrsOut25 As String = dtRowsFormat.FormatString(dtP25, SFCB.GoodTransferOut)
                        If P_WC25 <> String.Empty Then
                            Dim dtWC25 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC25)
                            If dtWC25.Rows.Count > 0 Then
                                P_WC25 = dtRowsFormat.FormatSumString(dtWC25, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep25.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP25 & "</span>"
                        lblWCStep25.Text = "<span style='color:indigo;'>" & P_WC25 & "</span>"
                        lblTrsInStep25.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn25 & "</span>"
                        lblWIPStep25.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP25 & "</span>"
                    End If
                End If
                ''######### P26 #############################
                If P25 <> String.Empty Then
                    FindStep25 = CInt(P25)
                    Dim StrSQLP26 As String = StrSQLP.Replace("@pWhereCustom", FindStep25)
                    Dim dtP26 As DataTable = GetQueryDataTable(StrSQLP26)
                    If dtP26.Rows.Count > 0 Then
                        Dim lblStep26_C As Label = CType(e.Row.FindControl("lblStep26_C"), Label)
                        P26 = dtRowsFormat.FormatString(dtP26, SFCB.LineNo)
                        lblStep26_C.Text = P26
                        lblStep26.Text = Item_Seq & "<span style='color:Blue;'>" & P26 & "</span>"
                        Dim P_OP26 As String = dtRowsFormat.FormatString(dtP26, SFCB.OperationID)
                        Dim P_WC26 As String = dtRowsFormat.FormatString(dtP26, SFCB.WorkStation)
                        Dim P_TrsIn26 As String = dtRowsFormat.FormatString(dtP26, SFCB.GoodTransferIn)
                        Dim P_WIP26 As String = dtRowsFormat.FormatString(dtP26, SFCB.WIP)
                        Dim P_TrsOut26 As String = dtRowsFormat.FormatString(dtP26, SFCB.GoodTransferOut)
                        If P_WC26 <> String.Empty Then
                            Dim dtWC26 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC26)
                            If dtWC26.Rows.Count > 0 Then
                                P_WC26 = dtRowsFormat.FormatSumString(dtWC26, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep26.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP26 & "</span>"
                        lblWCStep26.Text = "<span style='color:indigo;'>" & P_WC26 & "</span>"
                        lblTrsInStep26.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn26 & "</span>"
                        lblWIPStep26.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP26 & "</span>"
                    End If
                End If
                ''######### P27 #############################
                If P26 <> String.Empty Then
                    FindStep26 = CInt(P26)
                    Dim StrSQLP27 As String = StrSQLP.Replace("@pWhereCustom", FindStep26)
                    Dim dtP27 As DataTable = GetQueryDataTable(StrSQLP27)
                    If dtP27.Rows.Count > 0 Then
                        Dim lblStep27_C As Label = CType(e.Row.FindControl("lblStep27_C"), Label)
                        P27 = dtRowsFormat.FormatString(dtP27, SFCB.LineNo)
                        lblStep27_C.Text = P27
                        lblStep27.Text = Item_Seq & "<span style='color:Blue;'>" & P27 & "</span>"
                        Dim P_OP27 As String = dtRowsFormat.FormatString(dtP27, SFCB.OperationID)
                        Dim P_WC27 As String = dtRowsFormat.FormatString(dtP27, SFCB.WorkStation)
                        Dim P_TrsIn27 As String = dtRowsFormat.FormatString(dtP27, SFCB.GoodTransferIn)
                        Dim P_WIP27 As String = dtRowsFormat.FormatString(dtP27, SFCB.WIP)
                        Dim P_TrsOut27 As String = dtRowsFormat.FormatString(dtP27, SFCB.GoodTransferOut)
                        If P_WC27 <> String.Empty Then
                            Dim dtWC27 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC27)
                            If dtWC27.Rows.Count > 0 Then
                                P_WC27 = dtRowsFormat.FormatSumString(dtWC27, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep27.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP27 & "</span>"
                        lblWCStep27.Text = "<span style='color:indigo;'>" & P_WC27 & "</span>"
                        lblTrsInStep27.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn27 & "</span>"
                        lblWIPStep27.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP27 & "</span>"
                    End If
                End If
                ''######### P28 #############################
                If P27 <> String.Empty Then
                    FindStep27 = CInt(P27)
                    Dim StrSQLP28 As String = StrSQLP.Replace("@pWhereCustom", FindStep27)
                    Dim dtP28 As DataTable = GetQueryDataTable(StrSQLP28)
                    If dtP28.Rows.Count > 0 Then
                        Dim lblStep28_C As Label = CType(e.Row.FindControl("lblStep28_C"), Label)
                        P28 = dtRowsFormat.FormatString(dtP28, SFCB.LineNo)
                        lblStep28_C.Text = P28
                        lblStep28.Text = Item_Seq & "<span style='color:Blue;'>" & P28 & "</span>"
                        Dim P_OP28 As String = dtRowsFormat.FormatString(dtP28, SFCB.OperationID)
                        Dim P_WC28 As String = dtRowsFormat.FormatString(dtP28, SFCB.WorkStation)
                        Dim P_TrsIn28 As String = dtRowsFormat.FormatString(dtP28, SFCB.GoodTransferIn)
                        Dim P_WIP28 As String = dtRowsFormat.FormatString(dtP28, SFCB.WIP)
                        Dim P_TrsOut28 As String = dtRowsFormat.FormatString(dtP28, SFCB.GoodTransferOut)
                        If P_WC28 <> String.Empty Then
                            Dim dtWC28 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC28)
                            If dtWC28.Rows.Count > 0 Then
                                P_WC28 = dtRowsFormat.FormatSumString(dtWC28, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep28.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP28 & "</span>"
                        lblWCStep28.Text = "<span style='color:indigo;'>" & P_WC28 & "</span>"
                        lblTrsInStep28.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn28 & "</span>"
                        lblWIPStep28.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP28 & "</span>"
                    End If
                End If
                ''######### P29 #############################
                If P28 <> String.Empty Then
                    FindStep28 = CInt(P28)
                    Dim StrSQLP29 As String = StrSQLP.Replace("@pWhereCustom", FindStep28)
                    Dim dtP29 As DataTable = GetQueryDataTable(StrSQLP29)
                    If dtP29.Rows.Count > 0 Then
                        Dim lblStep29_C As Label = CType(e.Row.FindControl("lblStep29_C"), Label)
                        P29 = dtRowsFormat.FormatString(dtP29, SFCB.LineNo)
                        lblStep29_C.Text = P29
                        lblStep29.Text = Item_Seq & "<span style='color:Blue;'>" & P29 & "</span>"
                        Dim P_OP29 As String = dtRowsFormat.FormatString(dtP29, SFCB.OperationID)
                        Dim P_WC29 As String = dtRowsFormat.FormatString(dtP29, SFCB.WorkStation)
                        Dim P_TrsIn29 As String = dtRowsFormat.FormatString(dtP29, SFCB.GoodTransferIn)
                        Dim P_WIP29 As String = dtRowsFormat.FormatString(dtP29, SFCB.WIP)
                        Dim P_TrsOut29 As String = dtRowsFormat.FormatString(dtP29, SFCB.GoodTransferOut)
                        If P_WC29 <> String.Empty Then
                            Dim dtWC29 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC29)
                            If dtWC29.Rows.Count > 0 Then
                                P_WC29 = dtRowsFormat.FormatSumString(dtWC29, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep29.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP29 & "</span>"
                        lblWCStep29.Text = "<span style='color:indigo;'>" & P_WC29 & "</span>"
                        lblTrsInStep29.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn29 & "</span>"
                        lblWIPStep29.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP29 & "</span>"
                    End If
                End If
                ''######### P30 #############################
                If P29 <> String.Empty Then
                    FindStep29 = CInt(P29)
                    Dim StrSQLP30 As String = StrSQLP.Replace("@pWhereCustom", FindStep29)
                    Dim dtP30 As DataTable = GetQueryDataTable(StrSQLP30)
                    If dtP30.Rows.Count > 0 Then
                        Dim lblStep30_C As Label = CType(e.Row.FindControl("lblStep30_C"), Label)
                        P30 = dtRowsFormat.FormatString(dtP30, SFCB.LineNo)
                        lblStep30_C.Text = P30
                        lblStep30.Text = Item_Seq & "<span style='color:Blue;'>" & P30 & "</span>"
                        Dim P_OP30 As String = dtRowsFormat.FormatString(dtP30, SFCB.OperationID)
                        Dim P_WC30 As String = dtRowsFormat.FormatString(dtP30, SFCB.WorkStation)
                        Dim P_TrsIn30 As String = dtRowsFormat.FormatString(dtP30, SFCB.GoodTransferIn)
                        Dim P_WIP30 As String = dtRowsFormat.FormatString(dtP30, SFCB.WIP)
                        Dim P_TrsOut30 As String = dtRowsFormat.FormatString(dtP30, SFCB.GoodTransferOut)
                        If P_WC30 <> String.Empty Then
                            Dim dtWC30 As DataTable = ECAA.GetFindWorkcenterDetail_Table(P_WC30)
                            If dtWC30.Rows.Count > 0 Then
                                P_WC30 = dtRowsFormat.FormatSumString(dtWC30, ECAA.WorkcenterID, ECAA.Workcenter)
                            End If
                        End If
                        lblOPStep30.Text = ItemOP & "<span style='color:slateblue;'>" & P_OP30 & "</span>"
                        lblWCStep30.Text = "<span style='color:indigo;'>" & P_WC30 & "</span>"
                        lblTrsInStep30.Text = ssTrsInP & "<span style='color:indigo;'>" & P_TrsIn30 & "</span>"
                        lblWIPStep30.Text = ssWIP_P & "<span style='color:indigo;'>" & P_WIP30 & "</span>"
                    End If
                End If
                'FindStep30 = CInt(lblStep30.Text)
            End If

            If e.Row.Cells(9).Text <> String.Empty Then 'RecardDeatil
                If e.Row.Cells(9).Text = "1" Then
                    e.Row.Cells(9).Text = "1 : GENERAL"
                End If
                If e.Row.Cells(9).Text = "2" Then
                    e.Row.Cells(9).Text = "2 : REWORK"
                    ' e.Row.BackColor = ColorTranslator.FromHtml("#e6e6e6")

                End If
            End If
            If e.Row.Cells(4).Text <> String.Empty Then 'Satus
                If e.Row.Cells(4).Text = "C" Then
                    'e.Row.ForeColor = System.Drawing.Color.Green
                ElseIf e.Row.Cells(4).Text = "M" Then
                    e.Row.BackColor = System.Drawing.Color.Wheat
                    'e.Row.ForeColor = System.Drawing.Color.Black
                ElseIf e.Row.Cells(4).Text = "N" Then
                    'e.Row.ForeColor = System.Drawing.Color.Maroon
                ElseIf e.Row.Cells(4).Text = "Y" Then
                    'e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#b35900")
                End If
                e.Row.Cells(4).Text = StatusT100.MO_Normal(e.Row.Cells(4).Text)
            End If

            '########################## Check Cell Datat Empty 
            e.Row.Cells(1).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(1).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(2).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(2).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(3).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(3).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(4).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(4).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(5).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(5).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(6).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(6).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(7).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(7).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(8).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(8).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(9).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(9).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(10).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(10).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(11).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(11).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(12).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(12).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(13).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(13).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Cells(14).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            e.Row.Cells(14).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            'e.Row.Cells(15).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
            'e.Row.Cells(15).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            If lblStep1.Text = String.Empty Then
                e.Row.Cells(15).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(15).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(15).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(16).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep2.Text = String.Empty Then
                e.Row.Cells(16).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(16).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(16).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                ' e.Row.Cells(17).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep3.Text = String.Empty Then
                e.Row.Cells(17).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(17).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(17).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(18).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep4.Text = String.Empty Then
                e.Row.Cells(18).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(18).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(18).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(19).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep5.Text = String.Empty Then
                e.Row.Cells(19).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(19).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(19).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                ' e.Row.Cells(20).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep6.Text = String.Empty Then
                e.Row.Cells(20).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(20).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(20).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(21).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep7.Text = String.Empty Then
                e.Row.Cells(21).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(21).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(21).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                ' e.Row.Cells(22).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep8.Text = String.Empty Then
                e.Row.Cells(22).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(22).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(22).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(23).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep9.Text = String.Empty Then
                e.Row.Cells(23).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(23).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(23).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                ' e.Row.Cells(24).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep10.Text = String.Empty Then
                e.Row.Cells(24).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(24).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(24).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(25).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep11.Text = String.Empty Then
                e.Row.Cells(25).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(25).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(25).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                'e.Row.Cells(26).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep12.Text = String.Empty Then
                e.Row.Cells(26).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(26).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(26).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                ' e.Row.Cells(27).BackColor = ColorTranslator.FromHtml("#FBEFFB")
            End If
            If lblStep13.Text = String.Empty Then
                e.Row.Cells(27).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(27).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(27).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep14.Text = String.Empty Then
                e.Row.Cells(28).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(28).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(28).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep15.Text = String.Empty Then
                e.Row.Cells(29).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(29).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(29).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep16.Text = String.Empty Then
                e.Row.Cells(30).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(30).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(30).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep17.Text = String.Empty Then
                e.Row.Cells(31).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(31).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(31).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep18.Text = String.Empty Then
                e.Row.Cells(32).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(32).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(32).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep19.Text = String.Empty Then
                e.Row.Cells(33).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(33).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(33).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep20.Text = String.Empty Then
                e.Row.Cells(34).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(34).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(34).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep21.Text = String.Empty Then
                e.Row.Cells(35).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(35).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(35).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep22.Text = String.Empty Then
                e.Row.Cells(36).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(36).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(36).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep23.Text = String.Empty Then
                e.Row.Cells(37).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(37).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(37).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep24.Text = String.Empty Then
                e.Row.Cells(38).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(38).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(38).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep25.Text = String.Empty Then
                e.Row.Cells(39).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(39).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(39).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep26.Text = String.Empty Then
                e.Row.Cells(40).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(40).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(40).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep27.Text = String.Empty Then
                e.Row.Cells(41).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(41).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(41).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep28.Text = String.Empty Then
                e.Row.Cells(42).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(42).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(42).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep29.Text = String.Empty Then
                e.Row.Cells(43).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(43).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(43).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            If lblStep30.Text = String.Empty Then
                e.Row.Cells(44).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            Else
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F6CEEC'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                e.Row.Cells(44).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                e.Row.Cells(44).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If
            e.Row.Cells(45).BackColor = ColorTranslator.FromHtml("#D8D8D8")
            e.Row.Cells(45).ForeColor = System.Drawing.Color.BlueViolet
            e.Row.Cells(45).Font.Bold = True
            'lblStep1.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep2.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep3.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep4.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep5.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep6.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep7.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep8.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep9.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep10.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep11.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep12.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep13.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep14.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep15.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep16.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep17.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep18.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep19.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep20.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep21.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep22.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep23.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep24.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep25.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep26.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep27.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep28.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep29.ForeColor = ColorTranslator.FromHtml("#013ADF")
            'lblStep30.ForeColor = ColorTranslator.FromHtml("#013ADF")

            'lblOPStep1.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep2.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep3.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep4.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep5.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep6.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep7.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep8.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep9.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep10.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep11.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep12.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep13.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep14.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep15.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep16.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep17.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep18.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep19.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep20.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep21.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep22.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep23.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep24.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep25.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep26.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep27.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep28.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep29.ForeColor = System.Drawing.Color.BlueViolet
            'lblOPStep30.ForeColor = System.Drawing.Color.BlueViolet

            'lblWCStep1.ForeColor = System.Drawing.Color.Green
            'lblWCStep2.ForeColor = System.Drawing.Color.Green
            'lblWCStep3.ForeColor = System.Drawing.Color.Green
            'lblWCStep4.ForeColor = System.Drawing.Color.Green
            'lblWCStep5.ForeColor = System.Drawing.Color.Green
            'lblWCStep6.ForeColor = System.Drawing.Color.Green
            'lblWCStep7.ForeColor = System.Drawing.Color.Green
            'lblWCStep8.ForeColor = System.Drawing.Color.Green
            'lblWCStep9.ForeColor = System.Drawing.Color.Green
            'lblWCStep10.ForeColor = System.Drawing.Color.Green
            'lblWCStep11.ForeColor = System.Drawing.Color.Green
            'lblWCStep12.ForeColor = System.Drawing.Color.Green
            'lblWCStep13.ForeColor = System.Drawing.Color.Green
            'lblWCStep14.ForeColor = System.Drawing.Color.Green
            'lblWCStep15.ForeColor = System.Drawing.Color.Green
            'lblWCStep16.ForeColor = System.Drawing.Color.Green
            'lblWCStep17.ForeColor = System.Drawing.Color.Green
            'lblWCStep18.ForeColor = System.Drawing.Color.Green
            'lblWCStep19.ForeColor = System.Drawing.Color.Green
            'lblWCStep20.ForeColor = System.Drawing.Color.Green
            'lblWCStep21.ForeColor = System.Drawing.Color.Green
            'lblWCStep22.ForeColor = System.Drawing.Color.Green
            'lblWCStep23.ForeColor = System.Drawing.Color.Green
            'lblWCStep24.ForeColor = System.Drawing.Color.Green
            'lblWCStep25.ForeColor = System.Drawing.Color.Green
            'lblWCStep26.ForeColor = System.Drawing.Color.Green
            'lblWCStep27.ForeColor = System.Drawing.Color.Green
            'lblWCStep28.ForeColor = System.Drawing.Color.Green
            'lblWCStep29.ForeColor = System.Drawing.Color.Green
            'lblWCStep30.ForeColor = System.Drawing.Color.Green
        End If
    End Sub
    Private Function GetQueryDataTable(ByVal strSQL As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection
        With objConn
            .ConnectionString = clsDBConnect.strT100ConnectionString
            .Open()
        End With
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***'
        Catch ex As Exception
            'GetPageError.GetClassT100(AIN, "INBJ", "GetBody_Scarp_Destory_DocNo_DataSet", "Sql = strWH_Doc_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Function CallStringToColor(strParaeamter As String) As String
        Return "<span style='color:Blue;'>" & strParaeamter & "</span>"
    End Function
    Private Sub CheckDataProcess()
        Dim Process1 As Integer = 0
        Dim Process2 As Integer = 0
        Dim Process3 As Integer = 0
        Dim Process4 As Integer = 0
        Dim Process5 As Integer = 0
        Dim Process6 As Integer = 0
        Dim Process7 As Integer = 0
        Dim Process8 As Integer = 0
        Dim Process9 As Integer = 0
        Dim Process10 As Integer = 0
        Dim Process11 As Integer = 0
        Dim Process12 As Integer = 0
        Dim Process13 As Integer = 0
        Dim Process14 As Integer = 0
        Dim Process15 As Integer = 0
        Dim Process16 As Integer = 0
        Dim Process17 As Integer = 0
        Dim Process18 As Integer = 0
        Dim Process19 As Integer = 0
        Dim Process20 As Integer = 0
        Dim Process21 As Integer = 0
        Dim Process22 As Integer = 0
        Dim Process23 As Integer = 0
        Dim Process24 As Integer = 0
        Dim Process25 As Integer = 0
        Dim Process26 As Integer = 0
        Dim Process27 As Integer = 0
        Dim Process28 As Integer = 0
        Dim Process29 As Integer = 0
        Dim Process30 As Integer = 0
        Dim iP1 As Double = 0
        Dim iP2 As Double = 0
        Dim iP3 As Double = 0
        Dim iP4 As Double = 0
        Dim iP5 As Double = 0
        Dim iP6 As Double = 0
        Dim iP7 As Double = 0
        Dim iP8 As Double = 0
        Dim iP9 As Double = 0
        Dim iP10 As Double = 0
        Dim iP11 As Double = 0
        Dim iP12 As Double = 0
        Dim iP13 As Double = 0
        Dim iP14 As Double = 0
        Dim iP15 As Double = 0
        Dim iP16 As Double = 0
        Dim iP17 As Double = 0
        Dim iP18 As Double = 0
        Dim iP19 As Double = 0
        Dim iP20 As Double = 0
        Dim iP21 As Double = 0
        Dim iP22 As Double = 0
        Dim iP23 As Double = 0
        Dim iP24 As Double = 0
        Dim iP25 As Double = 0
        Dim iP26 As Double = 0
        Dim iP27 As Double = 0
        Dim iP28 As Double = 0
        Dim iP29 As Double = 0
        Dim iP30 As Double = 0
        Dim dbCountPP1 As Double = 0
        Dim dbCountPP2 As Double = 0
        Dim dbCountPP3 As Double = 0
        Dim dbCountPP4 As Double = 0
        Dim dbCountPP5 As Double = 0
        Dim dbCountPP6 As Double = 0
        Dim dbCountPP7 As Double = 0
        Dim dbCountPP8 As Double = 0
        Dim dbCountPP9 As Double = 0
        Dim dbCountPP10 As Double = 0
        Dim dbCountPP11 As Double = 0
        Dim dbCountPP12 As Double = 0
        Dim dbCountPP13 As Double = 0
        Dim dbCountPP14 As Double = 0
        Dim dbCountPP15 As Double = 0
        Dim dbCountPP16 As Double = 0
        Dim dbCountPP17 As Double = 0
        Dim dbCountPP18 As Double = 0
        Dim dbCountPP19 As Double = 0
        Dim dbCountPP20 As Double = 0
        Dim dbCountPP21 As Double = 0
        Dim dbCountPP22 As Double = 0
        Dim dbCountPP23 As Double = 0
        Dim dbCountPP24 As Double = 0
        Dim dbCountPP25 As Double = 0
        Dim dbCountPP26 As Double = 0
        Dim dbCountPP27 As Double = 0
        Dim dbCountPP28 As Double = 0
        Dim dbCountPP29 As Double = 0
        Dim dbCountPP30 As Double = 0

        For Each row As GridViewRow In gvShowPiVot.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim lblStep1_C As Label = CType(row.FindControl("lblStep1_C"), Label)
                Dim lblStep2_C As Label = CType(row.FindControl("lblStep2_C"), Label)
                Dim lblStep3_C As Label = CType(row.FindControl("lblStep3_C"), Label)
                Dim lblStep4_C As Label = CType(row.FindControl("lblStep4_C"), Label)
                Dim lblStep5_C As Label = CType(row.FindControl("lblStep5_C"), Label)
                Dim lblStep6_C As Label = CType(row.FindControl("lblStep6_C"), Label)
                Dim lblStep7_C As Label = CType(row.FindControl("lblStep7_C"), Label)
                Dim lblStep8_C As Label = CType(row.FindControl("lblStep8_C"), Label)
                Dim lblStep9_C As Label = CType(row.FindControl("lblStep9_C"), Label)
                Dim lblStep10_C As Label = CType(row.FindControl("lblStep10_C"), Label)
                Dim lblStep11_C As Label = CType(row.FindControl("lblStep11_C"), Label)
                Dim lblStep12_C As Label = CType(row.FindControl("lblStep12_C"), Label)
                Dim lblStep13_C As Label = CType(row.FindControl("lblStep13_C"), Label)
                Dim lblStep14_C As Label = CType(row.FindControl("lblStep14_C"), Label)
                Dim lblStep15_C As Label = CType(row.FindControl("lblStep15_C"), Label)
                Dim lblStep16_C As Label = CType(row.FindControl("lblStep16_C"), Label)
                Dim lblStep17_C As Label = CType(row.FindControl("lblStep17_C"), Label)
                Dim lblStep18_C As Label = CType(row.FindControl("lblStep18_C"), Label)
                Dim lblStep19_C As Label = CType(row.FindControl("lblStep19_C"), Label)
                Dim lblStep20_C As Label = CType(row.FindControl("lblStep20_C"), Label)
                Dim lblStep21_C As Label = CType(row.FindControl("lblStep21_C"), Label)
                Dim lblStep22_C As Label = CType(row.FindControl("lblStep22_C"), Label)
                Dim lblStep23_C As Label = CType(row.FindControl("lblStep23_C"), Label)
                Dim lblStep24_C As Label = CType(row.FindControl("lblStep24_C"), Label)
                Dim lblStep25_C As Label = CType(row.FindControl("lblStep25_C"), Label)
                Dim lblStep26_C As Label = CType(row.FindControl("lblStep26_C"), Label)
                Dim lblStep27_C As Label = CType(row.FindControl("lblStep27_C"), Label)
                Dim lblStep28_C As Label = CType(row.FindControl("lblStep28_C"), Label)
                Dim lblStep29_C As Label = CType(row.FindControl("lblStep29_C"), Label)
                Dim lblStep30_C As Label = CType(row.FindControl("lblStep30_C"), Label)
                'row.Cells(i).Text = ""
                If lblStep1_C.Text <> String.Empty Then
                    dbCountPP1 = Convert.ToDouble(lblStep1_C.Text).ToString()
                    While iP1 <= dbCountPP1
                        iP1 += 1
                    End While
                End If
                Process1 = iP1
                If lblStep2_C.Text <> String.Empty Then
                    dbCountPP2 = Convert.ToDouble(lblStep2_C.Text).ToString()
                    While iP2 <= dbCountPP2
                        iP2 += 1
                    End While
                End If
                Process2 = iP2
                If lblStep3_C.Text <> String.Empty Then
                    dbCountPP3 = Convert.ToDouble(lblStep3_C.Text).ToString()
                    While iP3 <= dbCountPP3
                        iP3 += 1
                    End While
                End If
                Process3 = iP3
                If lblStep4_C.Text <> String.Empty Then
                    dbCountPP4 = Convert.ToDouble(lblStep4_C.Text).ToString()
                    While iP4 <= dbCountPP4
                        iP4 += 1
                    End While
                End If
                Process4 = iP4
                If lblStep5_C.Text <> String.Empty Then
                    dbCountPP5 = Convert.ToDouble(lblStep5_C.Text).ToString()
                    While iP5 <= dbCountPP5
                        iP5 += 1
                    End While
                End If
                Process5 = iP5
                If lblStep6_C.Text <> String.Empty Then
                    dbCountPP6 = Convert.ToDouble(lblStep6_C.Text).ToString()
                    While iP6 <= dbCountPP6
                        iP6 += 1
                    End While
                End If
                Process6 = iP6
                If lblStep7_C.Text <> String.Empty Then
                    dbCountPP7 = Convert.ToDouble(lblStep7_C.Text).ToString()
                    While iP7 <= dbCountPP7
                        iP7 += 1
                    End While
                End If
                Process7 = iP7
                If lblStep8_C.Text <> String.Empty Then
                    dbCountPP8 = Convert.ToDouble(lblStep8_C.Text).ToString()
                    While iP8 <= dbCountPP8
                        iP8 += 1
                    End While
                End If
                Process8 = iP8
                If lblStep9_C.Text <> String.Empty Then
                    dbCountPP9 = Convert.ToDouble(lblStep9_C.Text).ToString()
                    While iP9 <= dbCountPP9
                        iP9 += 1
                    End While
                End If
                Process9 = iP9
                If lblStep10_C.Text <> String.Empty Then
                    dbCountPP10 = Convert.ToDouble(lblStep10_C.Text).ToString()
                    While iP10 <= dbCountPP10
                        iP10 += 1
                    End While
                End If
                Process10 = iP10
                If lblStep11_C.Text <> String.Empty Then
                    dbCountPP11 = Convert.ToDouble(lblStep11_C.Text).ToString()
                    While iP11 <= dbCountPP11
                        iP11 += 1
                    End While
                End If
                Process11 = iP11
                If lblStep12_C.Text <> String.Empty Then
                    dbCountPP12 = Convert.ToDouble(lblStep12_C.Text).ToString()
                    While iP12 <= dbCountPP12
                        iP12 += 1
                    End While
                End If
                Process12 = iP12
                If lblStep13_C.Text <> String.Empty Then
                    dbCountPP13 = Convert.ToDouble(lblStep13_C.Text).ToString()
                    While iP13 <= dbCountPP13
                        iP13 += 1
                    End While
                End If
                Process13 = iP13
                If lblStep14_C.Text <> String.Empty Then
                    dbCountPP14 = Convert.ToDouble(lblStep14_C.Text).ToString()
                    While iP14 <= dbCountPP14
                        iP14 += 1
                    End While
                End If
                Process14 = iP14
                If lblStep15_C.Text <> String.Empty Then
                    dbCountPP15 = Convert.ToDouble(lblStep15_C.Text).ToString()
                    While iP15 <= dbCountPP15
                        iP15 += 1
                    End While
                End If
                Process15 = iP15
                If lblStep16_C.Text <> String.Empty Then
                    dbCountPP16 = Convert.ToDouble(lblStep16_C.Text).ToString()
                    While iP16 <= dbCountPP16
                        iP16 += 1
                    End While
                End If
                Process16 = iP16
                If lblStep17_C.Text <> String.Empty Then
                    dbCountPP17 = Convert.ToDouble(lblStep17_C.Text).ToString()
                    While iP17 <= dbCountPP17
                        iP17 += 1
                    End While
                End If
                Process17 = iP17
                If lblStep18_C.Text <> String.Empty Then
                    dbCountPP18 = Convert.ToDouble(lblStep18_C.Text).ToString()
                    While iP18 <= dbCountPP18
                        iP18 += 1
                    End While
                End If
                Process18 = iP18
                If lblStep19_C.Text <> String.Empty Then
                    dbCountPP19 = Convert.ToDouble(lblStep19_C.Text).ToString()
                    While iP19 <= dbCountPP19
                        iP19 += 1
                    End While
                End If
                Process19 = iP19
                If lblStep20_C.Text <> String.Empty Then
                    dbCountPP20 = Convert.ToDouble(lblStep20_C.Text).ToString()
                    While iP20 <= dbCountPP20
                        iP20 += 1
                    End While
                End If
                Process20 = iP20
                If lblStep21_C.Text <> String.Empty Then
                    dbCountPP21 = Convert.ToDouble(lblStep21_C.Text).ToString()
                    While iP21 <= dbCountPP21
                        iP21 += 1
                    End While
                End If
                Process21 = iP21
                If lblStep22_C.Text <> String.Empty Then
                    dbCountPP22 = Convert.ToDouble(lblStep22_C.Text).ToString()
                    While iP22 <= dbCountPP22
                        iP22 += 1
                    End While
                End If
                Process22 = iP22
                If lblStep23_C.Text <> String.Empty Then
                    dbCountPP23 = Convert.ToDouble(lblStep23_C.Text).ToString()
                    While iP23 <= dbCountPP23
                        iP23 += 1
                    End While
                End If
                Process23 = iP23
                If lblStep24_C.Text <> String.Empty Then
                    dbCountPP24 = Convert.ToDouble(lblStep24_C.Text).ToString()
                    While iP24 <= dbCountPP24
                        iP24 += 1
                    End While
                End If
                Process24 = iP24
                If lblStep25_C.Text <> String.Empty Then
                    dbCountPP25 = Convert.ToDouble(lblStep25_C.Text).ToString()
                    While iP25 <= dbCountPP25
                        iP25 += 1
                    End While
                End If
                Process25 = iP25
                If lblStep26_C.Text <> String.Empty Then
                    dbCountPP26 = Convert.ToDouble(lblStep26_C.Text).ToString()
                    While iP26 <= dbCountPP26
                        iP26 += 1
                    End While
                End If
                Process26 = iP26
                If lblStep27_C.Text <> String.Empty Then
                    dbCountPP27 = Convert.ToDouble(lblStep27_C.Text).ToString()
                    While iP27 <= dbCountPP27
                        iP27 += 1
                    End While
                End If
                Process27 = iP27
                If lblStep28_C.Text <> String.Empty Then
                    dbCountPP28 = Convert.ToDouble(lblStep28_C.Text).ToString()
                    While iP28 <= dbCountPP28
                        iP28 += 1
                    End While
                End If
                Process28 = iP28
                If lblStep29_C.Text <> String.Empty Then
                    dbCountPP29 = Convert.ToDouble(lblStep29_C.Text).ToString()
                    While iP29 <= dbCountPP29
                        iP29 += 1
                    End While
                End If
                Process29 = iP29
                If lblStep30_C.Text <> String.Empty Then
                    dbCountPP30 = Convert.ToDouble(lblStep30_C.Text).ToString()
                    While iP30 <= dbCountPP30
                        iP30 += 1
                    End While
                End If
                Process30 = iP30
                ' lblSql.Text = Process17
            End If
        Next
        If Process1 <= 1 Then
            gvShowPiVot.Columns(15).Visible = False
        End If
        If Process2 <= 1 Then
            gvShowPiVot.Columns(16).Visible = False
        End If
        If Process3 <= 1 Then
            gvShowPiVot.Columns(17).Visible = False
        End If
        If Process4 <= 1 Then
            gvShowPiVot.Columns(18).Visible = False
        End If
        If Process5 <= 1 Then
            gvShowPiVot.Columns(19).Visible = False
        End If
        If Process6 <= 1 Then
            gvShowPiVot.Columns(20).Visible = False
        End If
        If Process7 <= 1 Then
            gvShowPiVot.Columns(21).Visible = False
        End If
        If Process8 <= 1 Then
            gvShowPiVot.Columns(22).Visible = False
        End If
        If Process9 <= 1 Then
            gvShowPiVot.Columns(23).Visible = False
        End If
        If Process10 <= 1 Then
            gvShowPiVot.Columns(24).Visible = False
        End If
        If Process11 <= 1 Then
            gvShowPiVot.Columns(25).Visible = False
        End If
        If Process12 <= 1 Then
            gvShowPiVot.Columns(26).Visible = False
        End If
        If Process13 <= 1 Then
            gvShowPiVot.Columns(27).Visible = False
        End If
        If Process14 <= 1 Then
            gvShowPiVot.Columns(28).Visible = False
        End If
        If Process15 <= 1 Then
            gvShowPiVot.Columns(29).Visible = False
        End If
        If Process16 <= 1 Then
            gvShowPiVot.Columns(30).Visible = False
        End If
        If Process17 <= 1 Then
            gvShowPiVot.Columns(31).Visible = False
        End If
        If Process18 <= 1 Then
            gvShowPiVot.Columns(32).Visible = False
        End If
        If Process19 <= 1 Then
            gvShowPiVot.Columns(33).Visible = False
        End If
        If Process20 <= 1 Then
            gvShowPiVot.Columns(34).Visible = False
        End If
        If Process21 <= 1 Then
            gvShowPiVot.Columns(35).Visible = False
        End If
        If Process22 <= 1 Then
            gvShowPiVot.Columns(36).Visible = False
        End If
        If Process23 <= 1 Then
            gvShowPiVot.Columns(37).Visible = False
        End If
        If Process24 <= 1 Then
            gvShowPiVot.Columns(38).Visible = False
        End If
        If Process25 <= 1 Then
            gvShowPiVot.Columns(39).Visible = False
        End If
        If Process26 <= 1 Then
            gvShowPiVot.Columns(40).Visible = False
        End If
        If Process27 <= 1 Then
            gvShowPiVot.Columns(41).Visible = False
        End If
        If Process28 <= 1 Then
            gvShowPiVot.Columns(42).Visible = False
        End If
        If Process29 <= 1 Then
            gvShowPiVot.Columns(43).Visible = False
        End If
        If Process30 <= 1 Then
            gvShowPiVot.Columns(44).Visible = False
        End If

        'gvShowPiVot.Columns(45).Visible = False
        'gvShowPiVot.Columns(46).Visible = False
        'gvShowPiVot.Columns(47).Visible = False
    End Sub
End Class