Public Class OutsourceStatus
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnectT100
    Dim outsTrnType As String = "'D203','D204','D205','D206','D209'"
    Dim configDate As New ConfigDate
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SQL As String = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 in(" & outsTrnType & ") order by oobxl001"
            showCheckboxList(cblTrnType, SQL, "CodeName", "Code", 4, clsDBConnect.T100)

            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl001 in (select oobxl001 from oobxl_t where oobxl001 like '51%' or oobxl001 like '52%' )"
            showCheckboxList(cblmoType, SQL, "CodeName", "Code", 4, clsDBConnect.T100)

            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If

        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)
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
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String = ""
        Dim SQL1 As String = ""
        Dim WHR As String = ""
        Dim FLD As String = ""
        Dim WHR1 As String = ""
        Dim FLD1 As String = ""
        Dim CntSel As Integer = 0
        Dim cnt As Integer = 0
        Dim DT As New DataTable

        WHR = Conn_SQL.Where("SUBSTR(" & SFFB.WONo & ",3,4)", cblmoType)
        WHR = WHR & Conn_SQL.Where("SUBSTR(" & SFFB.WONo & ",8,11)", tbMoNo)
        WHR = WHR & Conn_SQL.Where("" & SFAA.ProductItem & "", tbItem)
        WHR = WHR & Conn_SQL.Where("" & IMAAL.Specifaction & "", tbSpec)
        WHR = WHR & Conn_SQL.Where("SUBSTR(" & SFFB.DocNo & ",3,4)", cblTrnType)

        Dim status As String = ddlStatus.Text.ToString
        If status <> "A" Then
            WHR = WHR & " and " & SFFB.Status & "='" & status & "'  "
        End If

        'check Type Rework
        For Each box As ListItem In cblTrnType.Items
            If box.Selected Then
                If box.Value = "D205" Then
                    cnt = 1
                End If
            End If
        Next

        'check Type Transfer
        For Each box As ListItem In cblTrnType.Items
            If box.Selected Then
                CntSel = 1
            End If
        Next

        '????
        'Dim sup As String = tbSup.Text.ToString
        'If sup <> "" Then
        '    WHR = WHR & " and (TB.TB005='" & sup & "' or TB.TB008='" & sup & "') "
        'Else
        '    WHR = WHR & " and (TB.TB004='2' or TB.TB007='2') "
        'End If


        WHR = WHR & configDate.DateWhere("TO_CHAR(" & SFFB.DocumentDate & ",'YYYYMMDD')", configDate.dateFormat2(tbDateFrom.Text.ToString), configDate.dateFormat2(tbDateTo.Text.ToString))
        FLD = FLD & "CASE LENGTH(" & SFAA.ProductItem & ") when 16 then SUBSTR(" & SFAA.ProductItem & ",1,14)||'-'||SUBSTR(" & SFAA.ProductItem & ",15,2) else " & SFAA.ProductItem & " end Item," 'Part
        FLD = FLD & "" & IMAAL.Specifaction & " as Spec," 'Spec
        FLD = FLD & "" & IMAAL.ProductName & " as Descrition," 'Desc
        FLD = FLD & "'' as DescSpec," 'Desc/Spec
        FLD = FLD & "" & SFAA.DocNo & " as MODetail," 'MO Detail
        FLD = FLD & "cast(" & SFAA.ProductionQty & " as decimal(15,2)) as MOQty," 'MO Qty
        FLD = FLD & "" & SFFB.DocNo & "||'-'||" & SFFB.OperationSequence & " as TransferDetail," 'Transfer Detail
        FLD = FLD & "TO_CHAR(" & SFFB.DocumentDate & ",'YYYY-MM-DD') as TransferDate," 'Transfer Date
        FLD = FLD & "cast(" & SFFB.NoOfGoodItem & " as decimal(15,2)) as TransferQty," 'Transfer Qty
        '-----FLD = FLD & "" 'WIP
        'FLD = FLD & "TC.TC009+'-'+MT.MW002 as 'Transfer To'," 'Transfer to
        FLD = FLD & "'' as OperationTo," 'Outs to Location
        FLD = FLD & "'' as TransferTo," 'Outs to Location

        'FLD = FLD & "TC.TC007+'-'+MF.MW002 as 'Transfer From'," 'Transfer From
        FLD = FLD & "" & SFFB.OperationNo & " as OperationFrom," 'Outs to Location
        FLD = FLD & "" & SFFB.Workstation & " as TransferFrom," 'Outs to Location
        '-----FLD = FLD & "" 'Transfer From Descreiption
        '-----FLD = FLD & "" 'Transfer to Descreiption
        FLD = FLD & "" & SFFB.Status & " as TransferStatus " 'Transfer Status
        '''SFIA D205 

        If cnt > 0 Or CntSel > 0 Then
            WHR1 = Conn_SQL.Where("SUBSTR(sfaadocno,3,4)", cblmoType)
            WHR1 = WHR1 & Conn_SQL.Where("SUBSTR(sfaadocno,8,11)", tbMoNo)
            WHR1 = WHR1 & Conn_SQL.Where("sfaa010", tbItem)
            WHR1 = WHR1 & Conn_SQL.Where("" & IMAAL.Specifaction & "", tbSpec)
            WHR1 = WHR1 & Conn_SQL.Where("SUBSTR(" & SFIA.DocNo & ",3,4)", cblTrnType)
            Dim status1 As String = ddlStatus.Text.ToString
            If status <> "A" Then
                WHR1 = WHR1 & " and " & SFIA.Status & "='" & status & "'  "
            End If

            WHR1 = WHR1 & configDate.DateWhere("TO_CHAR(" & SFIA.DocumentDate & ",'YYYYMMDD')", configDate.dateFormat2(tbDateFrom.Text.ToString), configDate.dateFormat2(tbDateTo.Text.ToString))

            FLD1 = FLD1 & "CASE LENGTH(" & SFAA.ProductItem & ") when 16 then SUBSTR(" & SFAA.ProductItem & ",1,14)||'-'||SUBSTR(" & SFAA.ProductItem & ",15,2) else " & SFAA.ProductItem & " end Item," 'Part
            FLD1 = FLD1 & "" & IMAAL.Specifaction & " as Spec," 'Spec
            FLD1 = FLD1 & "" & IMAAL.ProductName & " as Descrition," 'Desc
            FLD1 = FLD1 & "'' as DescSpec," 'Desc/Spec
            FLD1 = FLD1 & "" & SFAA.DocNo & " as MODetail," 'MO Detail
            FLD1 = FLD1 & "cast(" & SFAA.ProductionQty & " as decimal(15,2)) as MOQty," 'MO Qty
            FLD1 = FLD1 & "" & SFIA.DocNo & " as TransferDetail," 'Transfer Detail


            FLD1 = FLD1 & "TO_CHAR(" & SFIA.DocumentDate & ",'YYYY-MM-DD') as TransferDate," 'Transfer Date
            FLD1 = FLD1 & "cast(" & SFIA.ReworkTransferOutQty & " as decimal(15,2)) as TransferQty," 'Transfer Qty
            '-----FLD = FLD & "" 'WIP
            'FLD = FLD & "TC.TC009+'-'+MT.MW002 as 'Transfer To'," 'Transfer to
            FLD1 = FLD1 & "'' as OperationTo," 'Outs to Location
            FLD1 = FLD1 & "'' as TransferTo," 'Outs to Location

            'FLD = FLD & "TC.TC007+'-'+MF.MW002 as 'Transfer From'," 'Transfer From
            FLD1 = FLD1 & "" & SFIA.TransferOutOpSerailNo & " as OperationFrom," 'Outs to Location
            FLD1 = FLD1 & "'' as TransferFrom," 'Outs to Location
            '-----FLD = FLD & "" 'Transfer From Descreiption
            '-----FLD = FLD & "" 'Transfer to Descreiption
            FLD1 = FLD1 & "" & SFIA.Status & " as TransferStatus " 'Transfer Status

            SQL1 = " UNION  select " & FLD1 & " from " & SFIA.tblTransferReworkHead & " " &
           " left join oocq_t on oocq002=sfia005 " &
           " left join sfaa_t on sfaadocno=sfia003 " &
           " left join imaal_t on imaal001=sfaa010 " &
           " where sfiaent='3' and sfiasite='JINPAO' and oocqent='3' and sfaaent='3' and imaalent='3' and sfaaent='3' " &
           " " & WHR1
            '" order by " & SFFB.DocNo & "," & SFFB.OperationSequence & ""

        End If

        SQL = " select " & FLD & " from " & SFFB.tblTransferHead & " " &
             " left join " & OOCQ.tblOperationSummary & " on oocq002=" & SFFB.OperationNo & " and " & SFFB.Workstation & "=oocqua001 " &
             " left join " & SFAA.tblMO & " on " & SFAA.DocNo & "=" & SFFB.WONo & " " &
             " left join " & IMAAL.tblProductionDetail & " on " & IMAAL.ProductItem & "=" & SFAA.ProductItem & " " &
             " where sffbent='3' and oocqent='3' and sfaaent='3' and imaalent='3'and sffbsite='JINPAO' " &
             " " & WHR & SQL1 & ""
        DT = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = DT
        gvShow.DataBind()
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("OutsourceStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class