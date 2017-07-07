Imports System.Data.OracleClient
Imports System.Drawing
Imports System.IO

Public Class WorkCenterStatus
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            btExportExcel.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Call WorkcenterDataBind()
        'CountRow1.RowCount = ControlForm.rowGridview(gvShow)
    End Sub
    Private Sub WorkcenterDataBind()
        Dim where As String = String.Empty
        Dim wWC As String = String.Empty
        Dim wwwMOType As String = String.Empty
        Dim wwSaleType As String = String.Empty
        Dim wwSaleOrderNo As String = String.Empty
        Dim wwSaleOrderNoSeq As String = String.Empty
        Dim wwSaleOrder As String = String.Empty
        Dim wwSaleOrderSeq As String = String.Empty
        Dim wCust As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wMObetweenPlanDate As String = String.Empty

        'If UsingWorkstation.getObject.Text <> "0" Then
        '    Dim WC As String = UsingWorkstation.getObject.Text
        '    wWC = " and " & SFCB.WorkStation & " ='" & [String].Join("','", WC) & "'"
        'End If
        'If UsingMO_Type.getObject.Text <> "0" Then
        '    Dim WMOType As String = UsingMO_Type.getObject.Text
        '    wwwMOType = " and substr(" & SFCB.WONo & ",3,4) = '" & [String].Join("','", WMOType) & "'"
        'End If
        'If UsingDocTypeSale.getObject.Text <> "0" Then
        '    If tbSaleNo.Text = "" And tbSaleSeq.Text = "" Then
        '        Dim SaleType As String = UsingDocTypeSale.getObject.Text
        '        SaleType = " and substr(" & XMDA.SaleOrderNo & ",3,4) = '" & [String].Join("','", SaleType) & "'"
        '    ElseIf tbSaleNo.Text <> "" And tbSaleSeq.Text = "" Then
        '        Dim SaleOrderNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, tbSaleNo.Text)
        '        wwSaleOrderNo = " and " & XMDA.SaleOrderNo & " ='" & [String].Join("','", SaleOrderNo) & "'"
        '    ElseIf tbSaleNo.Text <> "" And tbSaleSeq.Text <> "" Then
        '        Dim SaleOrderNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, tbSaleNo.Text)
        '        Dim WsaleOrdeNo As String = XMDA.SaleOrderNo & " ='" & [String].Join("','", SaleOrderNo) & "'"
        '        Dim SaleSeq As String = XMDC.ItemSequence & " ='" & [String].Join("','", tbSaleSeq.Text) & "'"
        '        wwSaleOrderNoSeq = " and " & WsaleOrdeNo & " AND " & SaleSeq
        '    End If
        'ElseIf UsingDocTypeSale.getObject.Text = "0" Then
        '    If tbSaleNo.Text <> "" And tbSaleSeq.Text = "" Then
        '        wwSaleOrder = " and substr(" & XMDA.SaleOrderNo & ",8,11) = '" & [String].Join("','", tbSaleNo.Text) & "'"
        '    ElseIf tbSaleNo.Text <> "" And tbSaleSeq.Text <> "" Then
        '        Dim SaleNo As String = "substr(" & XMDA.SaleOrderNo & ",8,11) = '" & [String].Join("','", tbSaleNo.Text) & "'"
        '        Dim SaleSeq As String = XMDC.ItemSequence & " ='" & [String].Join("','", tbSaleSeq.Text) & "'"
        '        wwSaleOrderSeq = " and " & SaleNo & " AND " & SaleSeq
        '    End If
        'End If
        'If tbCust.Text <> "" Then
        '    wCust = " and " & XMDA.CustomerId & "='" & [String].Join("','", tbCust.Text) & "'"
        'End If
        'If tbSpec.Text <> "" Then
        '    wSpec = " and " & IMAAL.Specifaction & " Like '" & [String].Join("','", tbSpec.Text) & "%'"
        'End If
        'If DateFrom.Text <> "" And DateTo.Text <> "" Then
        '    wMObetweenPlanDate = " and " & SFAA.PlanStartDate & " BETWEEN TO_DATE('" & [String].Join("','", DateFrom.Text) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", DateTo.Text) & "','yyyy/mm/dd')"
        'End If
        'where = wWC & wwwMOType & wwSaleType & wwSaleOrderNo & wwSaleOrderNoSeq & wwSaleOrder & wwSaleOrderSeq & wCust & wSpec & wMObetweenPlanDate

        'edit by noi start
        Dim varIni As New VarIni
        Dim WHR As String = ""
        WHR = varIni.getWhrFirst(varIni.SFCB)
        WHR &= Conn_SQL.Where(SFCB.WorkStation, UsingWorkstation.getObject)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(SFCB.WONo), UsingMO_Type.getObject)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(XMDA.SaleOrderNo), UsingDocTypeSale.getObject)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(XMDA.SaleOrderNo, True), tbSaleNo)
        WHR &= Conn_SQL.Where(XMDC.ItemSequence, tbSaleSeq)
        WHR &= Conn_SQL.Where(XMDA.CustomerId, tbCust)
        WHR &= Conn_SQL.Where(IMAAL.ProductItem, tbItem)
        WHR &= Conn_SQL.Where(IMAAL.Specifaction, tbSpec)
        WHR &= Conn_SQL.Where(SFAA.Status, " in ('" & ddlStatus.Text.Trim.Replace(",", "','") & "')")
        WHR &= Conn_SQL.Where("to_char(" & SFCB.PlanStartDate & ",'yyyy/mm/dd')", DateFrom.Text, DateTo.Text)
        'edit by noi end

        Dim dtWC As DataTable = GetWorkStationDataT100(WHR)
        If dtWC.Rows.Count > 0 Then
            gvShow.DataSource = dtWC
            gvShow.DataBind()
            CountRow1.RowCount = dtWC.Rows.Count.ToString()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            btExportExcel.Visible = True
        Else
            MessageAlert.Show(Me, "Not Data ")
        End If
    End Sub
    '############# function DataReturn for rework T100 ############################################################
    Private Shared SqlDataWorkstationDataT100 As String = "select " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WorkStation & "," & SFAA.DocNo & ", " &
    " " & XMDA.CustomerId & "||'-'||" & PMAAL.ContactName & " " & XMDA.CustomerId & "," & XMDA.SaleOrderNo & "," & SFAA.ProductItem & "," & IMAAL.Specifaction & " , " &
    " " & SFAA.ProductionQty & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & ", " &
    " " & SFCA.RunCardNo & "," & SFCA.RunCardDetail & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & ",  " &
    " " & SFCB.WIP & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & ",  " &
    " " & SFCB.StardardOutput & "," & SFCB.DirectScarp & "  " &
    " from " & SFCB.tblMOprocessItem_SFCB & "  " &
        " LEFT OUTER JOIN  " & ECAA.tblWorkcenter & " On " & ECAA.tblWorkcenter & "." & ECAA.ent & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.ent &
                     " and " & ECAA.tblWorkcenter & "." & ECAA.WorkcenterID & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WorkStation & " " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFAA.tblMO & "." & SFAA.ent & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.ent &
                     " and " & SFAA.tblMO & "." & SFAA.DocNo & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ent & "=" & SFAA.tblMO & "." & SFAA.ent &
                     " and  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFAA.tblMO & "." & SFAA.ProductItem & "  " &
                     " and  " & IMAAL.tblProductionDetail & "." & IMAAL.Langauge & "='en_US'  " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.ent & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.ent &
                     " and " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo &
                     " and " & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDA.tblSaleHead & "." & XMDA.ent & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.ent &
                    "  and " & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & "=" & SFAA.tblMO & "." & SFAA.OldRefereanceDocNo & " " &
        " LEFT OUTER JOIN  " & PMAAL.tblCustomerName & " On " & PMAAL.tblCustomerName & "." & PMAAL.ent & "=" & XMDA.tblSaleHead & "." & XMDA.ent &
                    "  and " & PMAAL.tblCustomerName & "." & PMAAL.ContactID & "=" & XMDA.tblSaleHead & "." & XMDA.CustomerId & " " &
                     " and  " & PMAAL.tblCustomerName & "." & PMAAL.Langauge & "='en_US'  " &
        " @pWhereCustomUsing  Order By " & SFCB.RunCard & "," & SFAA.DocNo & "," & SFCB.LineNo


    '" LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.ent & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.ent &
    '                 " and " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & SFAA.tblMO & "." & SFAA.OldRefereanceDocNo & " " &
    '                 " and " & XMDC.tblSaleItem & "." & XMDC.ItemSequence & "=" & SFAA.tblMO & "." & SFAA.OldRefereanceDocLineNo & " " &

    '" LEFT OUTER JOIN  " & SFBA.tblManufactureOrder_Body & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
    '" Group by  " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WorkStation & "," & SFAA.DocNo & ", " &
    '" " & XMDA.CustomerId & "," & XMDA.SaleOrderNo & "," & SFAA.ProductItem & "," & IMAAL.Specifaction & " , " &
    '" " & SFAA.ProductionQty & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & ", " &
    '" " & SFCA.RunCardNo & "," & SFCA.RunCardDetail & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & ",  " &
    '" " & SFCB.WIP & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & ",  " &
    '" " & SFCB.StardardOutput & "," & SFCB.DirectScarp & "  " &

    Public Shared Function GetWorkStationDataT100(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataWorkstationDataT100
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
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetWorkStationDataT100", Sql, ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function

    Protected Sub btExportExcel_Click(sender As Object, e As EventArgs) Handles btExportExcel.Click
        'ControlForm.ExportGridViewToExcel("WorkCenterStatus" & Session("UserName"), gvShow)
        Dim sfileName As String = "WorkCenterStatus" & DateTime.Now.ToString("yyyyMMdd:hh:mm:ss") & ".xls"
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
            Me.WorkcenterDataBind()

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
        Call WorkcenterDataBind()
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            'Dim Cust_Id As String = e.Row.Cells(1).Text
            'Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(Cust_Id)
            'If dtCustName.Rows.Count > 0 Then
            '    e.Row.Cells(2).Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerName)
            'End If
            Dim OP_Id As String = e.Row.Cells(14).Text
            If OP_Id <> String.Empty Then
                Dim dtOp As DataTable = OOCQL.GetDataOperation(OP_Id)
                If dtOp.Rows.Count > 0 Then
                    e.Row.Cells(15).Text = dtRowsFormat.FormatString(dtOp, OOCQL.Operation)
                End If
            End If
            Dim FindWorkcenter As String = e.Row.Cells(16).Text
            If FindWorkcenter <> String.Empty Then
                Dim dtWc As DataTable = ECAA.GetFindWorkcenterDetail_Table(FindWorkcenter)
                If dtWc.Rows.Count > 0 Then
                    e.Row.Cells(17).Text = dtRowsFormat.FormatString(dtWc, ECAA.Workcenter)
                End If
            End If
            Dim RuncardDeatil As String = e.Row.Cells(12).Text
            If RuncardDeatil <> String.Empty Then
                If RuncardDeatil = "1" Then
                    e.Row.Cells(12).Text = "1 : GENERAL"
                Else
                    e.Row.Cells(12).Text = RuncardDeatil & " : REWORK"
                End If
            End If
        End If

        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblItemSeq As Label = CType(e.Row.FindControl("lblItemSeq"), Label)
                Dim iMOseq As Integer = CInt(lblItemSeq.Text.Trim)
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplMO"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem(SFAA.DocNo)) Then
                    Dim LikeMO As String = "&MO= " & .DataItem(SFAA.DocNo).ToString.Trim
                    Dim LikeMOseq As String = "&Moseq= " & iMOseq.ToString("0000")
                    Dim LikeProducTionItem As String = "&ProductionItem= " & .DataItem(SFAA.ProductItem).ToString.Trim
                    hplDetail.NavigateUrl = "PlanScheduleAddPop.aspx?height=150&width=350&" & LikeMO & "&" & LikeProducTionItem & LikeMOseq
                    hplDetail.Attributes.Add("title", .DataItem(SFAA.ProductItem))
                End If

                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")

            End If
        End With

    End Sub

    Protected Sub btLabel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLabel.Click
        'Dim paraName As String = "whr:" & getWhr(False)
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ItemCycleWIP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    Protected Sub btList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btList.Click
        'Dim typeFor As String = ddlFor.Text.Trim,
        '    txtQty As String = "Audit Qty",
        '    txtBy As String = "Audit By"
        'If typeFor = "1" Then
        '    txtQty = "Check Qty"
        '    txtBy = "Check By"
        'End If
        'Dim paraName As String = "whr:" & getWhr(False) & ",txtQty:" & txtQty & ",txtBy:" & txtBy
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=ItemListWIP.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)

    End Sub
    ' Protected Function getWhr(Optional ByVal showStatus As Boolean = True) As String
    'Dim selProperty As String = ddlProperty.Text,
    '    wc As String = ddlWC.Text,
    '    code As String = tbCode.Text,
    '    spec As String = tbSpec.Text,
    '    whr As String = "",
    '    moType As String = ddlMoType.Text,
    '    codeStatus As String = ddlStatus.Text,
    '    cust As String = tbCust.Text.Trim,
    '    saleType As String = ddlSaleType.Text,
    '    saleNo As String = tbSaleNo.Text.Trim,
    '    saleSeq As String = tbSaleSeq.Text.Trim

    'If selProperty = "2" Then
    '    wc = tbWC.Text
    'End If

    'If wc <> "ALL" Then
    '    If selProperty = "1" Then '1
    '        whr = whr & " and SFC.TA006='" & wc & "' "
    '    Else '2
    '        whr = whr & " and SFC.TA006 like '%" & wc & "%' "
    '    End If
    'End If

    'If code <> "" Then
    '    whr = whr & " and MOC.TA006 like '%" & code & "%' "
    'End If
    'If spec <> "" Then
    '    whr = whr & " and MOC.TA035 like '%" & spec & "%' "
    'End If
    'If moType <> "ALL" Then
    '    whr = whr & " and SFC.TA001 = '" & moType & "' "
    'End If

    'If showStatus Then
    '    If codeStatus = "0" Then
    '        whr = whr & " and MOC.TA011 in('y','Y','1','2','3') "
    '    ElseIf codeStatus = "123" Then
    '        whr = whr & " and MOC.TA011 in('1','2','3') "
    '    ElseIf codeStatus = "Yy" Then
    '        whr = whr & " and MOC.TA011 in('y','Y') "
    '    Else
    '        whr = whr & " and MOC.TA011 = '" & codeStatus & "' "
    '    End If
    'End If

    'If saleType <> "ALL" Then
    '    whr = whr & " and MOC.TA026='" & saleType & "' "
    'End If
    'If saleNo <> "" Then
    '    whr = whr & " and MOC.TA027 like '%" & saleNo & "%' "
    'End If
    'If saleSeq <> "" Then
    '    whr = whr & " and MOC.TA028 like '%" & saleSeq & "' "
    'End If
    'If cust <> "" Then
    '    whr = whr & " and COPTC.TC004 = '" & cust.ToUpper() & "' "
    'End If
    'whr &= configDate.DateWhere("SFC.TA008", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))

    'Dim fld As String = "(select top 1 TB003 from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 where TC004=SFC.TA001 and TC005 = SFC.TA002 and TC008= SFC.TA003 order by TB003 desc,SFCTC.CREATE_DATE desc)"
    'whr &= configDate.DateWhere(fld, ucDateFrom.dateVal, ucDateTo.dateVal)



    'Return whr
    ' End Function
End Class