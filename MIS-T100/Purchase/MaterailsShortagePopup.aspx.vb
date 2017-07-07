Public Class MaterailsShortagePopup
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnect
    Dim configDate As New ConfigDate
    Dim ControlFormT100 As New ControlDataFormT100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim jpPart As String = Request.QueryString("JPPart").ToString.Trim
            lbPart.Text = jpPart
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
            lbUnit.Text = Request.QueryString("Unit").ToString.Trim
            Dim whr As String = "", SQL As String = "", dt As New DataTable
            Dim endDate As String = Request.QueryString("endDate").ToString.Trim
            Dim ds As New DataSet

            'Sale order Detail
            SQL = " select xmda004 Cust,xmdadocno||'-'||xmddseq SODetails,TO_CHAR(xmdadocdt,'yyyy/MM/dd') DocDate,TO_CHAR(xmdc012,'yyyy/MM/dd') PlanDelDate," &
            " xmdc007 SOQty,xmdd014 DeliveredQty,xmdc007-xmdd014 BalQty,xmdc006 Unit  " &
            " from  xmda_t " &
            " left join xmdc_t on xmdadocno = xmdcdocno And xmdcent=xmdaent And xmdcsite=xmdasite " &
            " left join imaal_t on imaal001 = xmdc001 And imaalent=xmdaent And imaal002= 'en_US' " &
            " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddseq=xmdcseq and xmddent=xmdaent and xmddsite=xmdasite " &
            " where xmdaent='3' and xmdasite='JINPAO' and xmda005='1' and xmdc045 not in ('2','3') and xmdastus ='Y' and xmdc001='" & jpPart & "'" &
            " order by xmdcdocno,xmdcseq "
            ControlFormT100.ShowGridViewT100(gvSO, SQL, clsDBConnect.T100)

            'Materials Request Issue
            SQL = " select sfaadocno MONo,sfaadocdt MODate,sfaa015 PlanIssueDate,sfba023 MatReqQty,sfba016 MatIssueQty, " &
            " sfba023-sfba016 IssueBal ,case when LENGTH(sfba005) = 16 then SUBSTR(sfba005,1,14) ||'-'|| SUBSTR(sfba005,15,2) else sfba005 end JPItem,imaal004 JPSpec " &
            " from sfba_t  " &
            " left join imaal_t on imaal001=sfba005 And imaalent=sfbaent " &
            " left join sfaa_t on sfbadocno=sfaadocno And sfaaent=sfbaent " &
            " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " &
            " and sfba005='" & jpPart & "' " & configDate.DateWhere("TO_CHAR(sfaadocdt,'yyyyMMdd')", "", endDate) &
            " order by sfaa015,sfaadocno "
            ControlFormT100.ShowGridViewT100(gvIssue, SQL, clsDBConnect.T100)

            'MO Detail
            SQL = " select sfaa026||'-'||sfaa027||'-'||sfaa028 SODetails,sfaadocno MONO,sfaa015 MODate, " &
              " sfaa020 PlanCompleteDate,sfaa012 MOQty,sfaa050 PRDQty,sfaa012-sfaa050 MOBal " &
              " from sfaa_t " &
              " left join imaal_t on imaal001 = sfaa010 and imaalent=sfaaent " &
              " where sfaaent='3' and sfaastus ='F'and sfaa012-sfaa050 > 0 " &
              "  and sfaa010='" & jpPart & "' " &
              " " & configDate.DateWhere("TO_CHAR(sfaa020,'yyyyMMdd')", "", endDate) &
              "  order by sfaa020,sfaadocno"
            ControlFormT100.ShowGridViewT100(gvMO, SQL, clsDBConnect.T100)

            'PR Detail
            SQL = " select pmdbdocno ||'-'|| pmdbseq PR,pmdadocdt PRDate,pmdb030 PRReqDate,pmdb006 PRQty,'' PlanBatch " &
                  " from pmdb_t  " &
                  " left join pmda_t on pmdbdocno=pmdadocno " &
                  " where pmdbent='3' and pmdaent='3' and pmdastus = 'Y' " &
                  " and pmdb004='" & jpPart & "' " &
                  " order by pmdbdocno,pmdbseq "
            ControlFormT100.ShowGridViewT100(gvPR, SQL, clsDBConnect.T100)

            'PO Detail
            SQL = " select '' SODetail,pmdndocno||'-'||pmdnseq PODetail,pmdn012 PlanDelDate,'' ConfirmDelDate,pmdn007 POQty, " &
                 " pmdo015 PODelQty,pmdn007-pmdo015 POBal,pmdl004 Supplier,pmaal004 SupllierName " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " left join pmaal_t on pmaal001=pmdl004 " &
            " where pmdnent='3' and pmdn045 = '1' " &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " &
            " and pmdn001='" & jpPart & "' " &
            "  order by pmdn012,pmdndocno,pmdnseq "
            ControlFormT100.ShowGridViewT100(gvPO, SQL, clsDBConnect.T100)

            'Purchase Receipt not aprove
            SQL = " select pmdt001||'-'|| pmdt002 PODetail,pmdsdocno POReceipts,pmds007 Supplier,pmdsdocdt ReceiptsDate, " &
            " pmds052 InspectionDate,pmdt053 POReceiptsQty " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') " &
            " And  pmdt006 ='" & jpPart & "' " & configDate.DateWhere("TO_CHAR(pmds052,'yyyyMMdd')", "", endDate) &
            " order by pmdt006 "
            ControlFormT100.ShowGridViewT100(gvPoInsp, SQL, clsDBConnect.T100)
        End If
    End Sub

End Class