Public Class SaleForecast
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim Pfunction As New Pfunction
    Dim ControlFormT100 As New ControlDataFormT100
    Dim CreateTempTable As New T100CreateTempTable
    Dim ControlForm As New ControlDataForm
    Dim clsDBConnect As New clsDBConnectT100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            btExport.Visible = False
            gvShow.Visible = False
            Dim SQL As String = ""
            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 like '22%'"
            ControlFormT100.showCheckboxListT100(cblSoTypePO, SQL, "CodeName", "Code", 4, clsDBConnect.T100)
            Dim SQL1 As String = ""
            SQL1 = "select  ooefl001 code,ooefl001||' : '||ooefl003 codename from ooefl_t where ooeflent='3' and ooefl001 in ('MKTS1','MKTS2','MKTS3','MKTS4','MKTS5')"
            ControlFormT100.showCheckboxListT100(cblForeType, SQL1, "codeName", "code", 5, clsDBConnect.T100)
        End If
        HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim WHR As String = ""
        Dim SQL As String = ""
        WHR = WHR & Conn_SQL.Where("xmda003", cblForeType) 'Section 1-5
        WHR = WHR & Conn_SQL.Where("SUBSTR(xmdadocno,3,4)", cblSoTypePO)
        WHR = WHR & Conn_SQL.Where("xmda004", tbCust)
        WHR = WHR & Conn_SQL.Where("xmdc001", tbItem)
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec)
        WHR = WHR & Conn_SQL.Where("TO_CHAR(xmdc012,'yyyy/MM/dd')", tbDateFrom.Text.Trim, tbDateTo.Text.Trim)
        WHR = WHR & Conn_SQL.Where("TO_CHAR(xmdadocdt,'yyyy/MM/dd')", tbFCDateFrom.Text.Trim, tbFCDateTo.Text.Trim)
        WHR = WHR & Conn_SQL.Where("imaf013", cblProperty)
        WHR = WHR & Conn_SQL.Where("SUBSTR(xmdadocno,8,11)", tbForNo)
        WHR = WHR & Conn_SQL.Where("xmdcseq", tbForSeq)

        If cbNoBOM.Checked Then
            WHR &= " and (select count(bmaa001) from bmaa_t where bmaa001=xmdc001 and bmaaent=xmdaent and bmaasite=xmdasite ) = 0 "
        End If

        Dim fld As String = "",
            colName As New ArrayList
        fld &= " xmdadocno||'-'|| xmdcseq ForcastNo_Seq,"
        colName.Add("ForcastNo Seq:ForcastNo_Seq")
        fld &= " xmdc001 Item ,"
        colName.Add("Item:Item")
        fld &= " imaal003 Descriptions,"
        colName.Add("Descriptions:Descriptions")
        fld &= " imaal004 Spec,"
        colName.Add("Spec:Spec")
        fld &= " CASE imaf013 WHEN '1' THEN '1:PURCHASE' WHEN '2' THEN '2:MANUFACTURING' WHEN '3' THEN '3:SUBCONTRACTING' WHEN '4' THEN '4:NONE' END ItemProprety,"
        colName.Add("Item Proprety:ItemProprety")
        fld &= " TO_CHAR(xmdadocdt,'yyyy/MM/dd') DocDate,"
        colName.Add("Doc Date:DocDate")
        fld &= " CAST(xmdc007 AS DECIMAL(16,2)) Forecast_Order_Qty,"
        colName.Add("Forecast Order Qty:Forecast_Order_Qty")
        fld &= " CAST(xmdd005-xmdd014 AS DECIMAL(16,2)) UnOrdered_Qty,"
        colName.Add("Un-Ordered Qty:UnOrdered_Qty")
        fld &= " xmda003 Forecast_TypeChannel,"
        colName.Add("Forecast Type Channel:Forecast_TypeChannel")
        fld &= " TO_CHAR(xmdc012,'yyyy/MM/dd') Due_Date,"
        colName.Add("Due Date:Due_Date")
        fld &= " CAST(xmdc015 AS DECIMAL(16,2)) UnitPrice,"
        colName.Add("Unit Price:UnitPrice")
        fld &= " xmda015 Currency,"
        colName.Add("Currency:Currency")
        fld &= " CAST(xmda016 AS DECIMAL(16,4)) Exchange_Rate,"
        colName.Add("Exchange Rate:Exchange_Rate")
        fld &= " xmda004||'-'||pmaal004 Customer,"
        colName.Add("Customer:Customer")
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)

        Dim dt As New DataTable
        SQL = " select " & fld.Substring(0, fld.Length - 1) &
            " from xmda_t " &
            " left join xmdc_t on xmdadocno=xmdcdocno and xmdaent=xmdcent and xmdasite=xmdcsite " &
            " left join xmdd_t on xmdddocno=xmdadocno and xmddent=xmdaent and xmddsite=xmdasite" &
            " left join imaal_t on imaal001=xmdc001 and imaalent=xmdaent " &
            " left join pmaal_t on pmaal001=xmda004 and pmaalent=xmdaent " &
            " left join imaf_t on imaf001=xmdc001 and imafent=xmdaent and imafsite=xmdasite " &
            " left join ooefl_t on ooefl001=xmda003 and ooeflent=xmdaent and ooefl002='en_US'" &
            " where xmdaent='3' and xmdasite='JINPAO' and xmda005='5'" & WHR &
            " order by xmdadocno,xmdcseq,xmdadocno "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = dt
        gvShow.DataBind()
        gvShow.Visible = True
        btExport.Visible = True
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        gvShow.Visible = True
        ControlForm.ExportGridViewToExcel("checkBOM" & Session("UserName"), gvShow)
        gvShow.Visible = False
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class