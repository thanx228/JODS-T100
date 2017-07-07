Public Class PCConfSOPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            Dim WHR As String = ""
            Dim SoType As String = Request.QueryString("SoType").ToString.Trim
            Dim SoNo As String = Request.QueryString("SoNo").ToString.Trim
            Dim SoSeq As String = Request.QueryString("SoSeq").ToString.Trim


            If SoType <> "" Then
                WHR = WHR & " and soType = '" & SoType & "' "
            End If

            If SoNo <> "" Then
                WHR = WHR & " and soNo like '" & SoNo & "%' "
            End If

            If SoSeq <> "" Then
                WHR = WHR & " and soSeq like '" & SoSeq & "%' "
            End If

            Dim SQL As String = ""

            SQL = "select DocNo as 'Doc. NO' , DocType as 'Doc Type','JP'+soType+'-'+Rtrim(soNo) as 'SaleOrderNo.',convert(int,soSeq) as 'Seq', " &
                " qty as 'SO Qty', PlanDelDate as 'Plan Delivery Date', SOReqDate as 'Sale Request Due Date'," &
                " PURRemark as 'PUR Remark',PURConf as 'PUR Confirm', PCConf as 'PC Confirm',PURConf1 as 'PUR Confirm 1', PCConf1 as 'PC Confirm 1',CreateBy ,CreateDate " &
                " from SOConfirmDate " &
                " where DocType <> 'SALE'" & WHR &
                " order by DocNo "

            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
            CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        End If
    End Sub


End Class