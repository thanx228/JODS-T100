

Public Class FixAsset
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnect
    Dim ControlFormT100 As New ControlDataFormT100
    Dim Conn_sql As New ConnSQL
    Dim configDate As New ConfigDate

    Dim AssetType As String = "'CB01','CB02','CB03','CB04','CB05','CB06'"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            Dim SQL As String = ""
            Dim Program As New DataTable
            SQL = " select ooba002 Type,ooba002||' : '||oobxl003 Name from ooba_t " &
                " left join oobxl_t on ooba002=oobxl001 " &
                " where oobaent='3' and oobxlent='3' and ooba002 in  (" & AssetType & ")"
            showCheckboxList(cblType, SQL, "Name", "Type", 4, clsDBConnect.T100)
            HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)

        End If
    End Sub

    Function showCheckboxList(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "") As String
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
    End Function
    Protected Sub BuSearch_Click(sender As Object, e As EventArgs) Handles BuSearch.Click

        Dim SQL As String = "",
            WHR As String = "",
            dt As New DataTable

        If DDLCondition.SelectedValue = "Incomplete" Then
            WHR = WHR & " and pmdlstus='Y' "
        ElseIf DDLCondition.SelectedValue = "Complete" Then
            WHR = WHR & " and pmdlstus = 'C' "
        Else
            WHR = WHR & " and pmdlstus in ('Y','C','U')"
        End If

        WHR = WHR & Conn_sql.Where(ddlDate.Text, configDate.dateFormat2(FDate.Text.Trim), configDate.dateFormat2(TDate.Text.Trim))
        WHR = WHR & Conn_sql.Where("pmdnud003", Spec)
        WHR = WHR & Conn_sql.Where("" & PMDN.ConversionRatePercentageRatio & "", Asset)
        WHR = WHR & Conn_sql.Where("pmdl004", tbSup)
        WHR = WHR & Conn_sql.Where("SUBSTR(pmdldocno,3,4) ", cblType)
        WHR = WHR & Conn_sql.Where("pmdl004", tbBuyer)

        '" left join ooefl_t on ooefl001=pmdn006 " & Department
        SQL = " select pmdldocno ||'-'|| pmdnseq POAsset,TO_CHAR(pmdldocdt,'yyyy/MM/dd') DocDate,TO_CHAR(pmdn012,'yyyy/MM/dd') PlanDelDate, " &
            " TO_char(sysdate ,'YYYYMMDD')- TO_char(pmdn012,'YYYYMMDD') Days,pmdl004 Supplier,pmdl015 Currency,pmdnud002 AssetName,pmdnud003 AssetSpec, " &
            " pmdn006 Unit,pmdn007 Qty,TO_CHAR(pmdn012,'yyyy/MM/dd') DelQty,pmdn007-pmdo015 DelBal,pmdl003  PRDept,CASE pmdlstus WHEN 'Y' then 'Confirmed' WHEN 'U' then 'UnConfirmed' WHEN 'C' then 'Closed' end Status,pmdl002||'-'||ooag011 Buyer " &
            " from pmdl_t " &
            " left join pmdn_t on pmdldocno=pmdndocno and pmdnent=pmdlent and pmdnsite=pmdlsite" &
            " left join pmaal_t on pmaal001=pmdl004 and pmaalent=pmdlent" &
            " left join pmab_t on pmdl004=pmab001 and pmabent=pmdlent and pmabsite=pmdlsite" &
            " left join pmdo_t on pmdndocno = pmdodocno and pmdn001=pmdo001 and pmdoseq=pmdnseq and pmdoent=pmdlent and pmdosite=pmdlsite" &
            " left join ooag_t on ooag001=pmdl002 and pmdlent=ooagent" &
            " where pmdlent='3' and pmdlsite='JINPAO' " &
            " and SUBSTR(pmdldocno,1,4) like '%JPCB%' " &
            " " & WHR &
            " order by pmdldocno,pmdnseq "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        gvShow.DataSource = dt
        gvShow.DataBind()
        lbrowsCount.Text = gvShow.Rows.Count
        btExport.Visible = True

    End Sub

End Class