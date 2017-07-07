Public Class HistoricalReceipt
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim clsDBConnect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btExport.Visible = False
        If Session("UserName") = "" Then
            Response.Redirect("../LoginT100.aspx")
        End If
        HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
        btExport.Visible = False
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click

        Dim SQL As String = ""
        Dim WHR As String = ""
        Dim dt As New DataTable
        WHR = WHR & Conn_SQL.Where("imaal001", tbItem)

        Dim TypeSO As String = ""

        'If ddlSO.Text.ToString = "2213" Then
        '    WHR = WHR & "and isnull(case when PURTD.TD013 = '' then LRPTC.TC026 else PURTD.TD013 end,'') = '2213'"
        'ElseIf ddlSO.Text.ToString = "Other" Then
        '    WHR = WHR & "and isnull(case when PURTD.TD013 = '' then LRPTC.TC026 else PURTD.TD013 end,'') <> '2213'"
        'End If

        If ddlStatus.SelectedValue = "S" Then
            WHR = WHR & " and pmdsstus = 'S'"
        ElseIf ddlStatus.SelectedValue = "Y" Then
            WHR = WHR & " and pmdsstus = 'Y'"
        ElseIf ddlStatus.SelectedValue = "N" Then
            WHR = WHR & " and pmdsstus = 'N'"
        End If

        WHR = WHR & configDate.DateWhere("TO_Char(pmdsdocdt,'yyyy/MM/dd')", tbDateFrom.Text.Trim, tbDateTo.Text.Trim)

        'SQL = " select distinct TG005 as 'Supplier' , TG021 as 'Supplier Short Desc.' ,COPTC.TC012 as 'Industry', " &
        '    " PURTH.TH011+'-'+PURTH.TH012+'-'+PURTH.TH013 as 'PO NO', " &
        '    " substring(PURTC.TC003,1,4)+'-'+substring(PURTC.TC003,5,2)+'-'+substring(PURTC.TC003,7,2) as 'Purchase Date', " &
        '    " case when len(TH004)=16 then substring(TH004,1,14)+'-'+substring(TH004,15,2) else TH004 end as 'Item', TH005 as 'Item Desc' , TH006 as 'Spec' , " &
        '    " PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003 as 'PUR Receipt No', substring(TG003,1,4)+'-'+substring(TG003,5,2)+'-'+substring(TG003,7,2) as 'Accept/Return Date', " &
        '    " case when TD014 = '' then substring(PURTD.TD012,1,4)+'-'+substring(PURTD.TD012,5,2)+'-'+substring(PURTD.TD012,7,2) else TD014 end as 'Plan Confirm Delivery', " &
        '    " cast(TH015 as decimal(16,2)) AS 'Accept/Return Qty',PURTH.TH008 as 'Unit',cast(PURTH.TH018 as decimal(16,2)) as 'Price', " &
        '    " cast(PURTG.TG008 as decimal(16,2)) as 'Exchange Rate', PURTG.TG007 as 'Currency',cast(PURTH.TH045 as decimal(16,2)) as 'Receipt/Return Total(O/C)', " &
        '    " cast(PURTH.TH046 as decimal(16,2)) as 'Receipt/Return Tax Total(O/C)', " &
        '    " cast(PURTH.TH045+PURTH.TH046 as decimal(16,2)) as 'Receipt/Return Total(O/C)', CMSMC.MC002 as 'Delivery Warehouse'," &
        '    " isnull(case when PURTD.TD013 = '' then LRPTC.TC026+'-'+LRPTC.TC027 else PURTD.TD013+'-'+PURTD.TD021 end,'') as 'SO', " &
        '    " case when PURTH.TH030='Y' then 'Y:Approved' when PURTH.TH030='N' then 'N:Not Approved' else 'V:Cancel' end as 'Approved Status', " &
        '    " CMSMV.MV002 as 'Buyer','Inspection Receipt' as 'Action',TH033 as 'Remark' " &
        '    " from PURTD " &
        '    '" left join PURTH on PURTH.TH011(receipt line) = PURTD.TD001(PO Line) and PURTH.TH012=PURTD.TD002 and PURTH.TH013=PURTD.TD003 " &
        '    '" left join PURTC on PURTC.TC001(PO Header) = (PO Line)PURTD.TD001 and PURTC.TC002=PURTD.TD002 " &
        '    '" left join PURTG on PURTG.TG001(receipt Header) =(receipt line) PURTH.TH001 and PURTG.TG002 = PURTH.TH002 " &
        '    '" left join CMSMC on CMSMC.MC001 = PURTH.TH009 " &
        '    'pass" left join CMSMV on CMSMV.MV001 = PURTC.TC011 " &
        '    'pass" left join PURTB on PURTB.TB001(request Line) = (PO Line)PURTD.TD026 and PURTB.TB002 = PURTD.TD027 and PURTB.TB003 = PURTD.TD028 " &
        '    'pass" left join LRPTC on LRPTC.TC001 = TB057 and LRPTC.TC002 = TB004 and LRPTC.TC046 = TB058 " &
        '    " left join COPTC on Rtrim(PURTD.TD013) <> '' and COPTC.TC001 = PURTD.TD013 and COPTC.TC002 =PURTD.TD021 or Rtrim(PURTD.TD013) = '' and COPTC.TC001 = LRPTC.TC026 and COPTC.TC002 =LRPTC.TC027" &
        '    " where 1=1 " & WHR & " order by substring(TG003,1,4)+'-'+substring(TG003,5,2)+'-'+substring(TG003,7,2),PURTH.TH001+'-'+PURTH.TH002+'-'+PURTH.TH003"
        SQL = "select pmdl004 Supplier,pmaal004 SupplierDescription,pmdldocno PO,TO_Char(pmdo027,'yyyy/MM/dd') PurchaseDate,pmdo001 Item,imaal003 Description ,imaal004 Spec," &
            " pmdsdocno PurchaseReceiptsNo,TO_CHAR(pmdsdocdt,'yyyy/MM/dd')  Accept_Return_Date,TO_Char(pmdn012,'yyyy/MM/dd') Plan_Confirm_Delivery,CAST(pmdt020 as DECIMAL(16,4)) Qty,pmdt019 Unit, " &
            " CAST(pmdt024 AS DECIMAL(16,4)) Price,pmds038 Exchange_Rate,pmds037 Currency,pmdt016 Delivery_Warehouse,'' SO, " &
            " case pmdsstus when 'Y' then 'Y:Comfirmed' when 'N' then 'N:UnConfirmed' when 'S' then 'S:Posted' else '' end  Approved_Status,ooag010||' '||ooag008 Buyer,'Inspection Receipt' Action,'' Remark " &
            " from pmdl_t " &
            " left join pmdn_t on pmdldocno=pmdndocno " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 " &
            " left join pmdt_t on pmdt001=pmdodocno and pmdt006=pmdo001 " &
            " left join pmds_t on pmds006=pmdtdocno " &
            " left join ooag_t on ooag001=pmdl002 " &
            " left join pmaal_t on pmaal001=pmdl004 " &
            " left join imaal_t on imaal001=pmdt006 " &
            " where pmdlent='3' and pmdnent='3'  and pmdoent='3' and pmdtent='3' and pmdsent='3' and pmaalent='3' and imaalent='3' and ooagent='3' " &
            " and pmdlsite='JINPAO' and pmdnsite='JINPAO' and pmdosite='JINPAO' and pmdtsite='JINPAO' and pmdssite='JINPAO'" &
            " and SUBSTR(pmdldocno,3,2) in ('33') and pmdlstus='Y' " & WHR & "" &
            " order by pmdldocno "
        '/*left join pmdb_t on pmdbdocno=pmdl008*/ so no
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = dt
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
        ControlForm.ExportGridViewToExcel("HistoricalReceipt" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

End Class