Public Class OutsourcePending
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnectT100
    Dim outsTrnType As String = "'D203','D204','D206','D209'"
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim SQL As String = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 in(" & outsTrnType & ") order by oobxl001"
            showCheckboxList(cblTrnType, SQL, "CodeName", "Code", 3, clsDBConnect.T100)

            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl001 in (select oobxl001 from oobxl_t where oobxl001 like '51%' or oobxl001 like '52%' )"
            showDDL(ddlMoType, SQL, "CodeName", "Code", 4, clsDBConnect.T100)

            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If

        End If
        tbSup.Enabled = False
    End Sub
    Public Sub showCheckboxList(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "")
        Try
            Dim dt As New DataTable
            dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
            clsDBConnect.Close(connectSting)

            With conCheckboxList
                .DataSource = dt
                .DataTextField = fldText
                .DataValueField = fldValue
                .DataBind()
                .RepeatColumns = showColumn
                .RepeatDirection = RepeatDirection.Horizontal
                .RepeatLayout = RepeatLayout.Flow
            End With
        Catch ex As Exception
        End Try
    End Sub
    Public Sub showDDL(ByRef ControlDDL As DropDownList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "", Optional ByVal headVal As String = "ALL", Optional dbType As String = "A")
        Dim dt As New DataTable
        dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
        clsDBConnect.Close(connectSting)

        With ControlDDL
            .DataSource = dt
            .DataTextField = fldText
            .DataValueField = fldValue
            .DataBind()
            If setAll = True Then
                .Items.Insert(0, headVal)
            End If
        End With
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim WHR As String = ""
        Dim FLD As String = ""
        Dim DTB As New DataTable
        Dim CntSel As Integer = 0

        WHR = WHR & Conn_SQL.Where("SUBSTR(" & SFFB.WONo & ",3,4)", ddlMoType)
        WHR = WHR & Conn_SQL.Where("SUBSTR(" & SFFB.WONo & ",8,11)", tbMoNo)
        WHR = WHR & Conn_SQL.Where("" & SFAA.ProductItem & "", tbItem)
        WHR = WHR & Conn_SQL.Where("" & IMAAL.Specifaction & "", tbSpec)

        'Not Select
        For Each box As ListItem In cblTrnType.Items
            If box.Selected Then
                CntSel = CntSel + 1
            End If
        Next
        If CntSel > 0 Then
            WHR = WHR & Conn_SQL.Where("SUBSTR(" & SFFB.DocNo & ",3,4)", cblTrnType)
        Else
            WHR = WHR & "and SUBSTR(" & SFFB.DocNo & ",3,4) in ('D203','D204','D206','D209')"
        End If
        'Dim sup As String = tbSup.Text.ToString
        'If sup <> "" Then
        '    WHR = WHR & " and (SFCTB.TB005='" & sup & "' or SFCTB.TB008='" & sup & "') "
        'Else
        '    WHR = WHR & " and (SFCTB.TB004='2' or SFCTB.TB007='2') "
        'End If

        Dim status As String = ddlStatus.Text.ToString
        If status <> "A" Then
            WHR = WHR & " and " & SFFB.Status & "='" & status & "'  "
        End If

        WHR = WHR & configDate.DateWhere("TO_CHAR(" & SFFB.DocumentDate & ",'YYYYMMDD')", configDate.dateFormat2(tbDateFrom.Text.ToString), configDate.dateFormat2(tbDateTo.Text.ToString))

        FLD = FLD & "case LENGTH(" & SFAA.ProductItem & ") when 10 then SUBSTR(" & SFAA.ProductItem & ",1,14)||'-'||SUBSTR(" & SFAA.ProductItem & ",15,2) else " & SFAA.ProductItem & " end ItemCode,"
        FLD = FLD & "" & IMAAL.Specifaction & " ItemSpec,"
        FLD = FLD & "" & SFFB.WONo & " MO,"
        FLD = FLD & "CAST(" & SFAA.ProductionQty & " AS DECIMAL(16,2)) PlanQty,"
        FLD = FLD & " " & SFFB.DocNo & "||'-'||" & SFFB.OperationSequence & " TransferDetail,"
        FLD = FLD & "'' TransferDate,"
        FLD = FLD & "" & SFFB.NoOfGoodItem & " TransferQty,"
        FLD = FLD & "CAST(" & SFFB.ScarpQty & " AS DECIMAL(15,2)) ReceiptQty,"
        FLD = FLD & "CAST( (" & SFFB.NoOfGoodItem & "-" & SFFB.ScarpQty & ") AS DECIMAL(15,2)) NotReceiptsQty,"
        FLD = FLD & "'' AcceptedQty,"
        FLD = FLD & "" & SFFB.ScarpQty & " ScrapQty,"
        FLD = FLD & "'' DestroyedQty,"
        FLD = FLD & "'' InspectionReturnQty,"
        FLD = FLD & "" & SFFB.Workstation & " TransferFrom,"
        FLD = FLD & "(select  " & ECAA.Workcenter & " from  " & ECAA.tblWorkcenter & "  where " & ECAA.ent & "='3' and " & ECAA.WorkcenterID & "=" & SFFB.Workstation & " and rownum = '1') TransferToDesc,"
        FLD = FLD & "CAST(" & SFCA.CompletedQty & " AS DECIMAL(16,2)) MOCompletedQty,"
        FLD = FLD & "CAST(" & SFCB.DirectScarp & " AS DECIMAL(16,2)) MOScrapQty,"
        FLD = FLD & "" & SFAA.SourceDocNo & "||'-'|| sfaa007 SO,"
        FLD = FLD & "sfaa009 CustomerOrder,"
        FLD = FLD & "'' Cutomer,"
        FLD = FLD & "'' CustomerName,"
        FLD = FLD & "" & SFCB.LineNo & " IssueOperation,"
        FLD = FLD & "'' ReceiptOperation,"
        FLD = FLD & "TO_CHAR(" & SFFB.DocumentDate & ",'YYYY/MM/DD') ReceiptDate,"
        FLD = FLD & "" & SFFB.Status & " TransferStatus,"
        FLD = FLD & "" & SFFB.Workstation & "||'-'||(select  " & ECAA.Workcenter & " from  " & ECAA.tblWorkcenter & " where " & ECAA.ent & "='3' and " & ECAA.WorkcenterID & "=" & SFFB.Workstation & " and rownum = '1') WC,"
        FLD = FLD & "" & OOCQL.Operation & " ProcessName,"
        FLD = FLD & "'' RemarkInventory,"
        FLD = FLD & "CAST(" & SFCB.ReworkTrsIn & " AS DECIMAL(16,2)) ReturnQty,"
        FLD = FLD & "CAST(" & SFCB.TransferOutforRework & " AS DECIMAL(16,2)) FinishReturnQty,"
        FLD = FLD & "CAST(" & SFCB.DirectScarp & " AS DECIMAL(16,2)) ScrapQty,"
        FLD = FLD & "'' MOStatus"

        SQL = " select " & FLD & "" &
            " from " & SFFB.tblTransferHead & " " &
            " left join " & SFAA.tblMO & " on " & SFFB.WONo & "=" & SFAA.DocNo & " and " & SFFB.RunCard & "=sfaa001 " &
            " left join " & IMAAL.tblProductionDetail & " on " & IMAAL.ProductItem & "= " & SFAA.ProductItem & " " &
            " left join " & SFCB.tblMOprocessItem_SFCB & " on " & SFCB.WONo & " = " & SFFB.WONo & " and " & SFCB.OperationID & "=" & SFFB.OperationNo & " and " & SFCB.OperationSeq & "= " & SFFB.OperationSequence & " and " & SFFB.RunCard & "=" & SFCB.FixedLaborHours & "" &
            " left join " & SFCA.tblMO_Detail & " on " & SFCA.DocNo & " = " & SFFB.WONo & " and " & SFCA.RunCardNo & "= sffb006 " &
            " left join " & OOCQL.tblOperation & " on " & OOCQL.OperationID & "= " & SFCB.OperationID & " " &
            " where " & SFFB.NoOfGoodItem & "<>" & SFFB.ScarpQty & "" &
            " and " & SFFB.ent & "='3' and sfaaent='3' and imaalent='3' and " & SFCB.ent & "='3' and sfcaent='3' and " & OOCQL.ent & "='3' and " & SFFB.Site & "='JINPAO' and sfaasite='JINPAO' and " & SFCB.Site & "='JINPAO' and sfcasite='JINPAO' " &
            " " & WHR & " " &
            " order by " & SFFB.WONo & ""

        DTB = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = DTB
        gvShow.DataBind()
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OutsourcePending" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class