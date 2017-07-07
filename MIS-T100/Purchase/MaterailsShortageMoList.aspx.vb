Public Class MaterailsShortageMoList
    Inherits System.Web.UI.Page
    Dim configDate As New ConfigDate
    Dim clsDBConnect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim whr As String = "",
                SQL As String = "",
                 dt As New DataTable
            Dim item As String = Request.QueryString("item").ToString.Trim
            Dim fDate As String = Request.QueryString("fDate").ToString.Trim
            Dim tDate As String = Request.QueryString("tDate").ToString.Trim
            Dim conDate As Boolean = False
            Dim addDate As Integer = Request.QueryString("addDate").ToString.Trim
            'Dim tempDate As Date
            'If tDate = "" Then
            '    tempDate = DateTime.ParseExact(fDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '    fDate = tempDate.AddDays(addDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '    conDate = True
            'ElseIf fDate = "" Then
            '    tempDate = DateTime.ParseExact(tDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '    tDate = tempDate.AddDays(addDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            'End If

            'Materials Request Issue
            SQL = " select sfaadocno MONo,sfaadocdt MODate,sfaa015 PlanIssueDate,sfba023 MatReqQty,sfba016 MatIssueQty, " &
                  " sfba023-sfba016 IssueBal ,case when LENGTH(sfba005) = 16 then SUBSTR(sfba005,1,14) ||'-'|| SUBSTR(sfba005,15,2) else sfba005 end JPItem,imaal004 JPSpec " &
                  " from sfba_t " &
                  " left join sfaa_t on sfbadocno=sfaadocno " &
                  " left join imaal_t on sfba005=imaal001 " &
                  " where sfbaent='3' and sfaaent='3'and imaalent='3' and sfba023-sfba016 > 0 and sfaastus not in ('C') and sfaastus='F' " &
                  " and sfba005='" & item & "'" &
                  " order by sfaa015,sfaadocno "
            '" & configDate.DateWhere("sfaa015", fDate, tDate, conDate) &
            'sfaa015 Plan issue Date ไม่มีใน/ T100(BOM Date)
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            gvIssue.DataSource = dt
            gvIssue.DataBind()
            clsDBConnect.Close(clsDBConnect.T100)



        End If
    End Sub

End Class