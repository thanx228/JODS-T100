Public Class PRNotOpenPO2
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnect
    Dim Conn_SQL As New ConnSQL
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Dim CreateTable As New T100CreateTempTable
    Dim configDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            Dim SQL As String = ""
            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 like '22%' order by oobxl001"
            showCheckboxList(cblSoType, SQL, "CodeName", "Code", 4, clsDBConnect.T100)

            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 like '22%' order by oobxl001"
            showCheckboxList(cblSoTypePO, SQL, "CodeName", "Code", 4, clsDBConnect.T100)

            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and SUBSTR(oobxl001,1,2) in ('31','CA') order by oobxl001 "
            showCheckboxList(cblPrType, SQL, "CodeName", "Code", 3, clsDBConnect.T100)

            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and  SUBSTR(oobxl001,1,2) in ('33','CB') order by oobxl001 "
            showCheckboxList(cblPOType, SQL, "CodeName", "Code", 3, clsDBConnect.T100)

            btExport.Visible = False

        End If
        TabContainer1.ActiveTabIndex = 0
    End Sub
    Function showCheckboxList(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "") As String

        Try
            Dim dt As DataTable
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
            GetPageError.GetPage(Request.CurrentExecutionFilePath.ToString, GetData.WhoCalledMe(), str_sqlcommand, ex.ToString)
        End Try
    End Function

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
            WHRAll As String = "",
            WHR As String = "",
            cnt As Decimal = 0,
            notIn As Boolean = False,
            selAll As Boolean = True,
            dt As New DataTable,
            PricePO As String = "",
            PricePR As String = ""

        'If Session("UserGroup").ToString.Substring(0, 3).Trim = "PUR" Or Session("UserGroup").ToString.Trim = "SYS" Then
        '    PricePO = " , CAST(pmdn011 AS DECIMAL(16,2)) Price "
        '    PricePR = " , CAST(pmdb010 AS DECIMAL(16,2)) Price "

        'End If

        If TabContainer1.ActiveTabIndex = 0 Then

            For Each boxItem As ListItem In cblSoType.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If boxItem.Selected Then
                    cnt = cnt + 1
                End If
            Next

            If cnt = 0 And cbNoneSO.Checked Then
                notIn = True
            ElseIf cnt = 0 And cbAll.Checked And cbNoneSO.Checked = False Then
                selAll = False
                'notIn = True
            End If

            WHR = WHR & Conn_SQL.Where("pmdastus", CblApp) 'code type
            'WHR = WHR & Conn_SQL.Where("substring(H.TA006,1,4) ", cblSoType, notIn, "", selAll) 'Sale Order type(Col Remark) /*ไม่มีข้อมูล
            WHR = WHR & Conn_SQL.Where("SUBSTR(pmdb004,3,1)", cblCodeType) 'Code Type
            WHR = WHR & Conn_SQL.Where("pmdb004", txtitem) 'item
            WHR = WHR & Conn_SQL.Where("imaal004", txtspec) 'spec
            WHR = WHR & Conn_SQL.Where("SUBSTR(pmdadocno,3,4)", cblPrType) 'pr type
            WHR = WHR & Conn_SQL.Where("SUBSTR(pmdadocno,8,11)", txtno) 'pr NO

            WHR = WHR & Conn_SQL.Where("pmdastus", cblCloseStatus) 'pr type /*ไม่มีข้อมูล
            'WHR = WHR & Conn_SQL.Where("PURTB.TB030", txtSONo) /* ไม่มีข้อมูล
            'WHR = WHR & Conn_SQL.Where("PURTB.TB057", txtPlanBatch) /* ไม่มีข้อมูล

            WHR = WHR & Conn_SQL.Where("TO_CHAR(pmdadocdt,'yyyy/MM/dd')", txtdateissue.Text.Trim, txtdateissueTo.Text.Trim)
            WHR = WHR & Conn_SQL.Where("TO_CHAR(pmdb030,'yyyy/MM/dd')", txtrequest.Text.Trim, txtrequestTo.Text.Trim)

            SQL = " select pmdadocno,pmdbseq,pmdb004 Item,imaal003 Description,imaal004 Spec,pmdb007 Unit,'' SONO,'' PlanBatchNo,pmdb006 PRQuantity,pmdb014 CustID, " &
                " TO_CHAR(pmdb030,'yyyy/MM/dd') RequiredDate,TO_CHAR(pmdadocdt,'yyyy/MM/dd') IssueDate,pmda003 PRDept,pmdastus ApprovedStatus,pmdldocno PONO, " &
                " pmdn007 POQty,pmdl004 Supplier,'' ConfirmDeliveryDate,'' PURConfirmDate,'' PCConfirmDate,'' SaleConfirm,CAST(pmdb010 AS DECIMAL(16,2)) Price, " &
                " '' Remark,TO_CHAR(pmdadocdt,'yyyy/MM/dd') PRDate,'' PurDate " &
                " from pmdb_t" &
                " left join pmda_t on pmdadocno=pmdbdocno and pmdaent=pmdbent" &
                " left join pmdl_t on pmdl008=pmdadocno and pmdadocno=pmdl008 and pmdlent=pmdbent" &
                " left join imaal_t on imaal001=pmdb004 and imaalent=pmdbent" &
                " left join pmdn_t  on pmdldocno=pmdndocno " &
                " where pmdbent='3' " & WHR &
                " order by pmdadocno "

        ElseIf TabContainer1.ActiveTabIndex = 1 Then

            'WHR = WHR & Conn_SQL.Where("SUBSTR(pmdadocno,3,2)", cblAsPrType)
            'WHR = WHR & Conn_SQL.Where("SUBSTR(pmdadocno,8,11)", txtnoAsset)
            'WHR = WHR & Conn_SQL.Where("imaal004", txtspecAsset)
            'WHR = WHR & Conn_SQL.Where("pmdastus", CblAppAsset)
            'WHR = WHR & Conn_SQL.Where("TO_CHAR(pmdadocdt,'yyyy/MM/dd')", txtdateissueAsset.Text.Trim, txtdateissueToAsset.Text.Trim)
            'WHR = WHR & Conn_SQL.Where("TO_CHAR(pmdb030,'yyyy/MM/dd')", txtrequestAsset.Text.Trim, txtrequestToAsset.Text.Trim)

            ''If cbNo.Checked = True Then
            ''    WHR = WHR & " And ASTTJ.TJ021 = 'N' "  /* ไม่มีข้อมูล
            ''End If

            'SQL = " select pmdadocno,pmdbseq,pmdb004 Item,imaal003 Description,imaal004 Spec,pmdb007 Unit,'' SONO,'' PlanBatchNo,pmdb006 PRQuantity,pmdb014 CustID, " &
            '    " TO_CHAR(pmdb030,'yyyy/MM/dd') RequiredDate,TO_CHAR(pmdadocdt,'yyyy/MM/dd') IssueDate,pmda003 PRDept,pmdastus ApprovedStatus,pmdldocno AssetPO, " &
            '    " '' Remark,TO_CHAR(pmdadocdt,'yyyy/MM/dd') PRDate,'' CloseStatus " &
            '    " from pmdb_t " &
            '    " left join pmda_t on pmdadocno=pmdbdocno  " &
            '    " left join pmdl_t on pmdl008=pmdadocno and pmdadocno=pmdl008 " &
            '    " left join imaal_t on imaal001=pmdb004 " &
            '    " left join pmdn_t  on pmdldocno=pmdndocno " &
            '    " where pmdbent='3' and pmdaent='3' and pmdlent='3' and imaalent='3' and SUBSTR(pmdadocno,3,2) <> '31' " &
            '    " " & WHR &
            '    " order by pmdadocno "
            Dim SSQL As String = ""
            For Each boxItem As ListItem In cblSoTypePO.Items
                Dim boxVal As String = CStr(boxItem.Value.Trim)
                If boxItem.Selected Then
                    cnt = cnt + 1
                End If
            Next

            WHR = WHR & Conn_SQL.Where("SUBSTR(pmdldocno,3,4)", cblPOType)
            WHR = WHR & Conn_SQL.Where("SUBSTR(pmdldocno,8,11)", tbPoNo)
            'WHR = WHR & Conn_SQL.Where("substring(TA006,1,4)", cblSoTypePO) /*ไม่มี Col อ้างอิง
            WHR = WHR & Conn_SQL.Where("pmdl004", tbSup, False)
            WHR = WHR & Conn_SQL.Where("pmdn001", txtitemPO)
            WHR = WHR & Conn_SQL.Where("imaal004", txtspecPO)
            WHR = WHR & Conn_SQL.Where("pmdlstus", cblCloseStatusPO)

            Select Case ddlDate.SelectedValue
                Case "ConDate" '/*ConfirmDelDate ไม่มีข้อมูล
                    'WHR = WHR & Conn_SQL.Where("", configDate.dateFormat4(tbDateFrom.Text.Trim), configDate.dateFormat4(tbDateTo.Text.Trim))
                Case "PODate" ' yyyyMMdd (dateFormat2)
                    WHR = WHR & Conn_SQL.Where("TO_CHAR(pmdldocdt,'yyyy/MM/dd')", tbDateFrom.Text.Trim, tbDateTo.Text.Trim)
            End Select

            SSQL = " ( Select pmdt020 from pmdt_t where  pmdt001= pmdodocno  and pmdt006=pmdo001 and SUBSTR(pmdtdocno,3,2) <> '37' and pmdtent=pmdlent and pmdtsite=pmdlsite and rownum=1 ) WaitReceiptApproved,"

            SQL = " select pmdldocno PONO,TO_CHAR(pmdldocdt,'yyyy/MM/dd') PODate,pmdl004||'-'||pmaal004 Supplier,pmdn001 	Item, " &
                " imaal003 Description ,imaal004 Spec,pmdadocno PRNO,'' SourceRef,pmdn006 Unit,CAST(pmdn007 AS DECIMAL(16,2)) PurchaseQty, " &
                " CAST(pmdo015 AS DECIMAL(16,2)) DeliveryQty,pmdn007-pmdo015 BalanceQty ," & SSQL &
                " '' ConfirmDelDate,TO_CHAR(pmdn012,'yyyy/MM/dd') PlanDeliveryDate  " & PricePO &
                " from pmdl_t " &
                " left join pmdn_t on pmdldocno=pmdndocno and pmdnent=pmdlent and pmdnsite=pmdlsite " &
                " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdlent and pmdosite=pmdlsite" &
                " left join pmaal_t on pmaal001=pmdl004 and pmaalent=pmdlent" &
                " left join imaal_t on imaal001=pmdn001 and imaalent=pmdlent" &
                " left join pmda_t on pmdl008 = pmdadocno and pmdadocno = pmdl008 and pmdaent=pmdlent " &
                " left join pmdb_t on pmdadocno=pmdbdocno and pmdbent=pmdlent" &
                " where pmdlent='3' and pmdlsite='JINPAO' and SUBSTR(pmdldocno,3,2) not in  ('14') " &
                " " & WHR &
                " order by pmdldocno "

        End If

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = dt
        gvShow.DataBind()
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click

        If TabContainer1.ActiveTabIndex = 0 Then
            ControlForm.ExportGridViewToExcel("PRStatus" & Session("UserName"), gvShow)
        ElseIf TabContainer1.ActiveTabIndex = 1 Then
            ControlForm.ExportGridViewToExcel("AssetPR" & Session("UserName"), gvShow)
        Else
            ControlForm.ExportGridViewToExcel("OpenPO" & Session("UserName"), gvShow)
        End If

    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class
