Imports System.Globalization
Public Class SOOpenMO
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim clsDBConnect As New clsDBConnect
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "", SQL1 As String = ""
            SQL = "select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl001 like '22%'"
            ControlFormT100.showCheckboxListT100(cblSOType, SQL, "CodeName", "Code", 6, clsDBConnect.T100)

            SQL1 = "select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and SUBSTR(oobxl001,1,2) in ('51','52','53') order by oobxl001"
            ControlFormT100.showCheckboxListT100(cblMOType, SQL1, "CodeName", "Code", 5, clsDBConnect.T100)

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If

            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
            tbDateFrom.Text = DateSerial(Year(Date.Now()), Month(Date.Now()), 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            tbDateTo.Text = Date.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim WHR As String = ""
        Dim SQL As String = ""
        If tbFGItem.Text <> "" Then
            WHR = WHR & " and imae041 like '21%' and imae001 like '%" & tbFGItem.Text.Trim & "%'"
        End If

        If tbFGSpec.Text <> "" Then
            WHR = WHR & " and imae041 like '21%' and imaal003 like '%" & tbFGSpec.Text.Trim & "%'"
        End If

        If tbPurItem.Text <> "" Then
            WHR = WHR & " and imae001 like '%" & tbPurItem.Text.Trim & "%'"
        End If

        If tbPurSpec.Text <> "" Then
            WHR = WHR & " and imaal003 like '%" & tbPurSpec.Text.Trim & "%'"
        End If
        WHR = WHR & Conn_SQL.Where("SUBSTR(sfaadocno,3,4)", cblMOType)
        WHR = WHR & Conn_SQL.Where("SUBSTR(sfaadocno,8,11)", tbMoNo)

        WHR = WHR & Conn_SQL.Where("SUBSTR(sfaa006,3,4)", cblSOType)
        WHR = WHR & Conn_SQL.Where("SUBSTR(sfaa006,3,4)", tbSoNo)
        WHR = WHR & Conn_SQL.Where("xmdcseq", tbSoSeq)
        WHR = WHR & Conn_SQL.Where("TO_CHAR(sfaadocdt,'yyyy/MM/dd')", tbDateFrom.Text.Trim, tbDateTo.Text.Trim)
        Dim fld As String = "",
            colName As New ArrayList
        fld &= " oobxl003 Descriptions,"
        colName.Add("Desc:Descriptions")
        fld &= " sfaa006 SONO,"
        colName.Add("SO NO:SONO")
        fld &= " CAST(xmdc007 AS DECIMAL(16,2)) QtySO,"
        colName.Add("Qty SO:QtySO")
        fld &= " sfaadocno MONO,"
        colName.Add("MO NO:MONO")
        fld &= " TO_CHAR(sfaadocdt,'yyyy/MM/dd') DocDate, "
        colName.Add("Doc Date:DocDate")
        fld &= " CASE WHEN LENGTH(sfaa010)=16 THEN SUBSTR(sfaa010,1,14)||'-'||SUBSTR(sfaa010,15,2) ELSE sfaa010 END Item,"
        colName.Add("Item:Item")
        fld &= " imaal003 Spec,"
        colName.Add("Spec:Spec")
        fld &= " imaal004 Descritions,"
        colName.Add("Descritions:Descritions")
        fld &= " (select CAST(bmba011 AS DECIMAL(16,2)) from bmba_t where bmba003=sfaa010 and bmbaent=sfaaent and bmbasite=sfaasite and rownum=1 ) QPA,"
        colName.Add("QPA:QPA")
        fld &= " CAST(sfaa012 AS DECIMAL(16,2)) PlanQty ,"
        colName.Add("Plan Qty:PlanQty")
        fld &= " CAST(sfaa049 AS DECIMAL(16,2)) Issued_Kit_Qty,"
        colName.Add("Issued Kit Qty:Issued_Kit_Qty")
        fld &= " CAST(sfca004 AS DECIMAL(16,2)) Complete_Qty,"
        colName.Add("Complete Qty:Complete_Qty")
        fld &= " CAST(sfaa056 AS DECIMAL(16,2)) ScrapQty,"
        colName.Add("Scrap Qty:ScrapQty")
        fld &= " sfaa013 Unit,"
        colName.Add("Unit:Unit")
        fld &= " '' Remark,"
        colName.Add("Remark:Remark")
        fld &= " '' Plan_Batch_No,"
        colName.Add("Plan Batch No:Plan_Batch_No")
        fld &= " sfaa009||' : '||pmaal004 Customer,"
        colName.Add("Customer:Customer")
        fld &= " CASE imaa009 WHEN 'N' then '' ELSE imaa009||'-'||rtaxl003 END Product_Classification,"
        colName.Add("Product Classification:Product_Classification")
        fld &= " '' Customer_Item,"
        colName.Add("Customer Item:Customer_Item")

        ControlForm.GridviewColWithLinkFirst(gvShow, colName)
        Dim dt As New DataTable
        SQL = " select " & fld.Substring(0, fld.Length - 1) &
            " from sfaa_t " &
            " left join oobxl_t on SUBSTR(sfaadocno,3,4)=oobxl001 and  oobxlent=sfaaent " &
            " left join xmdc_t on xmdcdocno=sfaa006 and xmdc001=sfaa010 and xmdcent=sfaaent and xmdcsite=sfaasite " &
            " left join sfca_t on sfcadocno=sfaadocno and sfca001=sfaa001 and sfcaent=sfaaent and sfcasite=sfaasite " &
            " left join imaal_t on imaal001=sfaa010 and imaalent=sfaaent " &
            " left join imaa_t on imaa001=imaal001 and imaaent=imaalent " &
            " left join rtaxl_t on rtaxl001=imaa009 and rtaxlent=sfaaent " &
            " left join pmaal_t on pmaal001=sfaa009 and pmaalent=sfaaent " &
            " left join imae_t on imae001=sfaa010 and imaeent=sfaaent and imaesite=sfaasite " &
            " where sfaaent='3' and sfaasite='JINPAO' " & WHR & " order by oobxl001,sfaadocno ASC"
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = dt
        gvShow.DataBind()
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        btExport.Visible = True
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        gvShow.Visible = True
        ControlForm.ExportGridViewToExcel("SOOpenMO" & Session("UserName"), gvShow)
        gvShow.Visible = False
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class