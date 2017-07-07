Public Class PURConfSOPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack() Then

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

            SQL = "select DocNo as 'Doc. NO' , DocType as 'Doc Type',soType+'-'+Rtrim(soNo)+'-'+soSeq as 'SO Type-No', " & _
                " qty as 'SO Qty', PlanDelDate as 'Plan Delivery Date', SOReqDate as 'Sale Request Due Date', " & _
                " PURRemark as 'PUR Remark', PURConf as 'PUR Confirm',PURConf1 as 'PUR Confirm', CreateBy ,CreateDate " & _
                " from SOConfirmDate " & _
                " where DocType = 'PUR'" & WHR & _
                " order by DocNo "
             
            ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
            CountRow1.RowCount = ControlForm.rowGridview(gvShow)

        End If
    End Sub


End Class