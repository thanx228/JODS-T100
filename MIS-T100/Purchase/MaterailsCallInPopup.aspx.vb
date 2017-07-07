Public Class MaterailsCallInPopup
    Inherits System.Web.UI.Page

    Dim clsDBConnect As New clsDBConnect
    Dim configDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim dt As New DataTable

            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbPart.Text = jpPart
            'lbSpec.Text = Conn_SQL.DecodeFrom64(Request.QueryString("JPSpec").ToString.Trim)
            lbSpec.Text = Request.QueryString("JPSpec").ToString.Trim
            lbIssue.Text = Request.QueryString("issueQty").ToString.Trim
            lbDel.Text = Request.QueryString("delQty").ToString.Trim
            lbStock.Text = Request.QueryString("stock").ToString.Trim
            lbMO.Text = Request.QueryString("moQty").ToString.Trim
            lbPO.Text = Request.QueryString("poQty").ToString.Trim
            lbPOFor.Text = Request.QueryString("poForQty").ToString.Trim
            lbPOMan.Text = Request.QueryString("poManQty").ToString.Trim
            lbPOMO.Text = Request.QueryString("poMoQty").ToString.Trim
            lbPR.Text = Request.QueryString("prQty").ToString.Trim
            lbPoInsp.Text = Request.QueryString("poRcpQty").ToString.Trim

            Dim whr As String = "", SQL As String = ""
            Dim endDate As String = Request.QueryString("endDate").ToString.Trim
            'Sale order Detail
            'SO rows status ที่ยังไม่จบ
            SQL = " select xmda004 Cust,xmdadocno||'-'||xmddseq SODetails,TO_CHAR(xmdadocdt,'yyyy/MM/dd') DocDate,TO_CHAR(xmdc012,'yyyy/MM/dd') PlanDelDate,xmdc007 SOQty," &
            " xmdd014 DeliveredQty,xmdc007-xmdd014 BalQty,xmdc006 Unit " &
                " from  xmda_t" &
                " left join xmdc_t on xmdadocno = xmdcdocno and xmdcent=xmdaent and xmdcsite=xmdasite and xmdc045='1' " &
                " left join imaal_t on imaal001 = xmdc001 and imaalent=xmdaent and imaal002= 'en_US' " &
                " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddent=xmdaent and xmddsite=xmdasite " &
                " where xmdaent='3' and xmdasite='JINPAO' and xmdastus ='Y' and xmdc001='" & jpPart & "'" &
                " order by xmdcdocno,xmdcseq "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvSO.DataSource = dt
            gvSO.DataBind()
            lbCountSO.Text = gvSO.Rows.Count

            'Materials Request Issue
            SQL = "select '' Cust,sfaadocno MONo,TO_CHAR(sfaadocdt,'yyyy/MM/dd') MODate,TO_CHAR(sfaa019,'yyyy/MM/dd') PlanIssueDate,CAST(sfba023 AS DECIMAL(16,2)) MatReqQty,CAST(sfba016+sfba025 AS DECIMAL(16,2)) MatIssueQty," &
                " CAST(sfba013-(sfba016+sfba025) AS DECIMAL(16,2))  IssueBal ,case when LENGTH(sfba005) = 16 then SUBSTR(sfba005,1,14) ||'-'|| SUBSTR(sfba005,15,2) else sfba005 end JPItem,imaal004 JPSpec " &
                " from sfba_t " &
                " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
                " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
                " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' and sfba005 ='" & jpPart & "'" &
                " order by sfaadocno "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvIssue.DataSource = dt
            gvIssue.DataBind()
            lbCountIssue.Text = gvIssue.Rows.Count

            'MO Detail
            SQL = " select sfaa026||'-'||sfaa027||'-'||sfaa028 SODetails,sfaadocno MONO,TO_CHAR(sfaadocdt,'yyyy/MM/dd') MODate," &
                  " TO_CHAR(sfaa019,'yyyy/MM/dd') PlanCompleteDate,CAST(sfaa012 AS DECIMAL(16,2)) MOQty,CAST(sfaa050 AS DECIMAL(16,2)) PRDQty,sfaa012-sfaa050 MOBal " &
                  " from sfaa_t " &
                  " left join imaal_t on imaal001 = sfaa010 and imaalent=sfaaent " &
                  " where sfaaent='3' and sfaastus ='F'and sfaa012-sfaa050 > 0 and sfaa010='" & jpPart & "'" &
                  " order by sfaa010 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvMO.DataSource = dt
            gvMO.DataBind()
            lbCountMO.Text = gvMO.Rows.Count

            ' PR Detail
            SQL = " select pmdbdocno ||'-'|| pmdbseq PR,TO_CHAR(pmdadocdt,'yyyy/MM/dd') PRDate,TO_CHAR(pmdb030,'yyyy/MM/dd') PRReqDate,CAST(pmdb006 AS DECIMAL(16,2)) PRQty,'' PlanBatch " &
                " from pmdb_t " &
                " left join pmdl_t on pmdl008=pmdbdocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
                " left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
                " where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'Y' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' and pmdb004='" & jpPart & "' " &
                " order by pmdb004 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvPR.DataSource = dt
            gvPR.DataBind()
            lbCountPR.Text = gvPR.Rows.Count

            'PO Detail
            SQL = " select  '' SODetail,pmdndocno||'-'||pmdnseq PODetail,TO_CHAR(pmdn012,'yyyy/MM/dd') PlanDelDate,'' ConfirmDelDate, " &
                " CAST(pmdn007 AS DECIMAL(16,2)) POQty,pmdo015 PODelQty,pmdn007-pmdo015 POBal,pmdl004 Supplier,pmaal004 SupllierName " &
                " from pmdn_t " &
                " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
                " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
                " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
                " left join pmaal_t on pmaal001=pmdl004 and pmaalent=pmdnent and pmaal002 = 'en_US' " &
                " where pmdnent='3' and pmdn045 = '1' and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " &
                " and pmdn001='" & jpPart & "' " &
                " order by pmdn012,pmdndocno,pmdnseq"
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvPO.DataSource = dt
            gvPO.DataBind()
            lbCountPO.Text = gvPO.Rows.Count

            'Purchase Receipt not aprove
            SQL = " select pmdt001||'-'|| pmdt002 PODetail,pmdsdocno POReceipts,pmds007 Supplier,pmdsdocdt ReceiptsDate, " &
                  " pmds052 InspectionDate,pmdt053 POReceiptsQty " &
                  " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
                  " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
                  " and SUBSTR(pmdsdocno,3,2) in ('34','37') and pmdt006 ='" & jpPart & "' " &
                  " order by pmdt006 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            gvPoInsp.DataSource = dt
            gvPoInsp.DataBind()
            lbCountPoInsp.Text = gvPoInsp.Rows.Count

        End If
    End Sub
End Class