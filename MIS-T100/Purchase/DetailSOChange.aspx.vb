Imports System.Data.OracleClient

Public Class DetailSOChange
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            lblSOtype.Text = Request.QueryString("SoType").ToString.Trim
            lblSONo.Text = Request.QueryString("SoNo").ToString.Trim
            lblSOseq.Text = Request.QueryString("SoSeq").ToString.Trim
            lblItemNo.Text = Request.QueryString("item").ToString.Trim

            Call DataBindShowDetailSOchange()
            Call DataBindShowDetailSOforecastChange()
        End If
    End Sub
    Private Sub DataBindShowDetailSOchange()
        Dim WhereSOdocNo As String = XMEE.DocNo & "='JP" & lblSOtype.Text.Trim & "-" & lblSONo.Text.Trim & "'"
        ' Dim WhereSOdocOrderType As String = XMEE.OrderType & " in('1','2') "
        ' Dim SOchangeDetailWhere As String = WhereSOdocNo & " and " & WhereSOdocOrderType
        'Dim dtSOheader As DataTable = XMEE.getHeaderSaleChangeOrderCustom(WhereSOdocNo)
        'If dtSOheader.Rows.Count > 0 Then
        '    Dim sOrderType As String = dtRowsFormat.FormatString(dtSOheader, XMEE.OrderType)
        '    If sOrderType <> String.Empty Then
        Dim SOchangeDetailShow As String = "JP" & lblSOtype.Text.Trim & "-" & lblSONo.Text.Trim
        Dim dtSOchange As DataTable = XMEH.getSaleChangeDeliveryDeatilByDocNo(SOchangeDetailShow)
        If dtSOchange.Rows.Count > 0 Then
            gvShow.DataSource = dtSOchange
            gvShow.DataBind()
            CountRow1.RowCount = dtSOchange.Rows.Count.ToString
        Else
            gvShow.DataSource = New List(Of String)
            gvShow.DataBind()
        End If
        '    End If
        'End If
    End Sub
    Private Sub DataBindShowDetailSOforecastChange()
        Dim WhereSOdocNo As String = XMEE.DocNo & "='JP" & lblSOtype.Text.Trim & "-" & lblSONo.Text.Trim & "'"
        'Dim WhereSOdocOrderType As String = XMEE.OrderType & " not in('1','2') "
        'Dim SOchangeDetailWhere As String = WhereSOdocNo & " and " & WhereSOdocOrderType
        'Dim dtSOheader As DataTable = XMEE.getHeaderSaleChangeOrderCustom(SOchangeDetailWhere)
        'If dtSOheader.Rows.Count > 0 Then
        '    Dim sOrderType As String = dtRowsFormat.FormatString(dtSOheader, XMEE.OrderType)
        '    If sOrderType <> String.Empty Then
        Dim SOchangeDetailShow As String = "JP" & lblSOtype.Text.Trim & "-" & lblSONo.Text.Trim
        Dim dtSOchange As DataTable = XMEH.getSaleChangeDeliveryDeatilByDocNo(SOchangeDetailShow)
        If dtSOchange.Rows.Count > 0 Then
            gvShowFore.DataSource = dtSOchange
            gvShowFore.DataBind()
            CountRow2.RowCount = dtSOchange.Rows.Count.ToString
        Else
            gvShowFore.DataSource = New List(Of String)
            gvShowFore.DataBind()
        End If
        '    End If
        'End If
    End Sub
    Private Shared Function GetdateOracleBaase(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateOracleBaase", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblSONochange As Label = CType(e.Row.FindControl("lblSONochange"), Label)
            Dim lblSOVersion As Label = CType(e.Row.FindControl("lblSOVersion"), Label)
            Dim lblOldAgreedDelivery As Label = CType(e.Row.FindControl("lblOldAgreedDelivery"), Label)
            Dim lblAppointedDeliveryDate As Label = CType(e.Row.FindControl("lblAppointedDeliveryDate"), Label)
            Dim lblSOorderType As Label = CType(e.Row.FindControl("lblSOorderType"), Label)
            Dim lblSOChangeDate As Label = CType(e.Row.FindControl("lblSOChangeDate"), Label)
            If lblSONochange.Text <> String.Empty Then
                Dim dtSaleChangeOrder As DataTable = XMEG.getSaleChangeOrderByDocNo(lblSONochange.Text)
                If dtSaleChangeOrder.Rows.Count > 0 Then
                    lblOldAgreedDelivery.Text = dtRowsFormat.FormatString(dtSaleChangeOrder, XMEG.AppointedDeliveryDate)
                End If
                Dim WhereHeadDocNo As String = XMEE.DocNo & "='" & lblSONochange.Text & "'"
                Dim WhereHeadVersion As String = XMEE.Version & "='" & lblSOVersion.Text & "'"
                Dim where As String = WhereHeadDocNo & " and " & WhereHeadVersion
                Dim dtSaleChangeOrderType As DataTable = XMEE.getHeaderSaleChangeOrderCustom(where)
                If dtSaleChangeOrderType.Rows.Count > 0 Then
                    lblSOorderType.Text = dtRowsFormat.FormatString(dtSaleChangeOrderType, XMEE.OrderType)
                    lblSOChangeDate.Text = dtRowsFormat.FormatString(dtSaleChangeOrderType, XMEE.ChangedDate)
                End If
            End If
            If lblSOorderType.Text = "1" Then
                lblSOorderType.Text = "1:GENERAL SO"
            ElseIf lblSOorderType.Text = "2" Then
                lblSOorderType.Text = "2:EXCHANGE SO"
            ElseIf lblSOorderType.Text = "3" Then
                lblSOorderType.Text = "3:RECEPTION ORDER"
            ElseIf lblSOorderType.Text = "4" Then
                lblSOorderType.Text = "4:TRANSFER ORDER"
            ElseIf lblSOorderType.Text = "5" Then
                lblSOorderType.Text = "5:ADVANCED ORDER"
            ElseIf lblSOorderType.Text = "6" Then
                lblSOorderType.Text = "6:SHIP FROM AGENT"
            ElseIf lblSOorderType.Text = "7" Then
                lblSOorderType.Text = "7:CONSIGNMENT OFFICIALLY SHIPPING \ SALES RETURN"
            ElseIf lblSOorderType.Text = "8" Then
                lblSOorderType.Text = "8:ORDERS FOR BORROWING ITEMS"
            End If
        End If
    End Sub
    Private Sub gvShowFore_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShowFore.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblSONochange As Label = CType(e.Row.FindControl("lblSONochange"), Label)
            Dim lblOldAgreedDelivery As Label = CType(e.Row.FindControl("lblOldAgreedDelivery"), Label)
            Dim lblAppointedDeliveryDate As Label = CType(e.Row.FindControl("lblAppointedDeliveryDate"), Label)
            Dim lblSOorderType As Label = CType(e.Row.FindControl("lblSOorderType"), Label)
            If lblSONochange.Text <> String.Empty Then
                Dim dtSaleChangeOrder As DataTable = XMEG.getSaleChangeOrderByDocNo(lblSONochange.Text)
                If dtSaleChangeOrder.Rows.Count > 0 Then
                    lblOldAgreedDelivery.Text = dtRowsFormat.FormatString(dtSaleChangeOrder, XMEG.AppointedDeliveryDate)
                End If
                Dim dtSaleChangeOrderType As DataTable = XMEE.getHeaderSaleChangeOrderByDocNo(lblSONochange.Text)
                If dtSaleChangeOrderType.Rows.Count > 0 Then
                    lblSOorderType.Text = dtRowsFormat.FormatString(dtSaleChangeOrderType, XMEE.OrderType)
                End If
            End If
            If lblSOorderType.Text = "1" Then
                lblSOorderType.Text = "1:GENERAL SO"
            ElseIf lblSOorderType.Text = "2" Then
                lblSOorderType.Text = "2:EXCHANGE SO"
            ElseIf lblSOorderType.Text = "3" Then
                lblSOorderType.Text = "3:RECEPTION ORDER"
            ElseIf lblSOorderType.Text = "4" Then
                lblSOorderType.Text = "4:TRANSFER ORDER"
            ElseIf lblSOorderType.Text = "5" Then
                lblSOorderType.Text = "5:ADVANCED ORDER"
            ElseIf lblSOorderType.Text = "6" Then
                lblSOorderType.Text = "6:SHIP FROM AGENT"
            ElseIf lblSOorderType.Text = "7" Then
                lblSOorderType.Text = "7:CONSIGNMENT OFFICIALLY SHIPPING \ SALES RETURN"
            ElseIf lblSOorderType.Text = "8" Then
                lblSOorderType.Text = "8:ORDERS FOR BORROWING ITEMS"
            End If
        End If
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    If Not Page.IsPostBack() Then

    '        Dim WHR As String = ""
    '        Dim SoType As String = Request.QueryString("SoType").ToString.Trim
    '        Dim SoNo As String = Request.QueryString("SoNo").ToString.Trim
    '        Dim SoSeq As String = Request.QueryString("SoSeq").ToString.Trim


    '        Dim tempTable As String = "tempForecastChange" & Session("UserName")
    '        CreateTempTable.CreateTempForecastChange(tempTable)


    '        If SoType <> "" Then
    '            WHR = WHR & " and COPTE.TE001 = '" & SoType & "' "
    '        End If

    '        If SoNo <> "" Then
    '            WHR = WHR & " and COPTE.TE002 like '" & SoNo & "%' "
    '        End If

    '        If SoSeq <> "" Then
    '            WHR = WHR & " and COPTF.TF104 like '" & SoSeq & "%' "
    '        End If

    '        Dim SQL As String = ""

    '        SQL = "select COPTE.TE006 as 'Reason',COPTE.TE001+'-'+COPTE.TE002+'-'+COPTE.TE003 as 'SO Change',COPTF.TF104 as 'SO Orig. Seq', " & _
    '            " cast(COPTF.TF009 as decimal(20,2)) as 'SO Chang Qty', " & _
    '            " SUBSTRING(COPTF.TF015,7,2)+'-'+SUBSTRING(COPTF.TF015,5,2)+'-'+SUBSTRING(COPTF.TF015,1,4)  as 'Plan Del. Date', " & _
    '            " COPTF.TF036 as 'Forcast No' ,COPTF.TF035 as 'Forcast Seq', cast(COPMF.MF008 as decimal(20,2)) as 'Forcast Qty', " & _
    '            " case when len(COPTF.TF005) = 16 then SUBSTRING(COPTF.TF005,1,14)+'-'+SUBSTRING(COPTF.TF005,15,2) else COPTF.TF005 end as 'Item', " & _
    '            " COPTF.TF006 as 'Desc',COPTF.TF007 as 'Spec',Rtrim(COPTE.CREATOR)+'-'+(select MF002 from ADMMF where COPTE.CREATOR =MF001 ) as 'Input', " & _
    '            " SUBSTRING(COPTE.CREATE_DATE,7,2)+'-'+SUBSTRING(COPTE.CREATE_DATE,5,2)+'-'+SUBSTRING(COPTE.CREATE_DATE,1,4)  as 'Create Date' " & _
    '            " from COPTE " & _
    '            " left join COPTF on COPTF.TF001 = COPTE.TE001 and COPTF.TF002 = COPTE.TE002 and COPTF.TF003 = COPTE.TE003 " & _
    '            " left join COPME on COPME.ME001 = COPTF.TF036 " & _
    '            " left join COPMF on COPMF.MF001 = COPTF.TF036 and COPMF.MF002 = COPTF.TF035 and COPMF.MF003 = COPTF.TF005 " & _
    '            " where 1=1 " & WHR & _
    '            " order by COPTE.TE001,COPTE.TE002,COPTE.TE003 "

    '        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
    '        CountRow1.RowCount = ControlForm.rowGridview(gvShow)


    '        Dim Program As New DataTable
    '        Dim fore As String = "",
    '            chgDate As String = "",
    '            user As String = "",
    '            ISQL As String = ""

    '        WHR = ""
    '        Dim Forecast As String = Request.QueryString("Forecast").ToString.Trim
    '        If Request.QueryString("Forecast").ToString.Trim <> "" Then
    '            WHR = WHR & " and TB007 like '%" & Forecast & "%' "
    '        Else
    '            WHR = WHR & " and TB007 like ' ' "
    '        End If

    '        SQL = ""
    '        SQL = "select TB007 as forecastNo,CONVERT (datetime,TB006) as chgdate,TB004 as 'user',TB005 from ADMTB where ADMTB.TB003 = 'COPI04'" & _
    '            " AND ADMTB.TB002 in ('0') and ADMTB.TB005 like '%MF008%' " & WHR
    '        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

    '        For i As Integer = 0 To Program.Rows.Count - 1
    '            Dim TB005 As String = Program.Rows(i).Item("TB005")
    '            fore = Program.Rows(i).Item("forecastNo")
    '            chgDate = Program.Rows(i).Item("chgdate")
    '            User = Program.Rows(i).Item("user")

    '            Dim getStr() As String = Split(TB005, "DETAIL:")
    '            Dim getCom() As String = Split(getStr(1), ",")
    '            For j = 0 To getCom.Length - 1
    '                Dim getEqual() As String = Split(getCom(j), "=")

    '                If getEqual(0).Trim = "MF008" Then
    '                    'MsgBox(getEqual(0).Trim)
    '                    'Dim field As String = (getEqual(0).Trim)
    '                    Dim getFrom() As String = Split(getEqual(1), "] To [")
    '                    'MsgBox(getFrom(0).Replace("From [", "").Trim)
    '                    Dim chgfrom As String = (getFrom(0).Replace("From [", "").Trim)
    '                    'MsgBox(getFrom(1).Replace("]", "").Trim)
    '                    Dim chgTo As String = (getFrom(1).Replace("]", "").Trim)
    '                    ISQL = " insert into " & tempTable & "(forecastNo,chgQtyfrom,chgQtyto,chgDate,userchg)values " & _
    '                        " ('" & fore & "','" & chgfrom & "','" & chgTo & "','" & chgDate & "','" & user & "')"
    '                    Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
    '                End If
    '            Next
    '        Next

    '        'WHR = ""
    '        'If Request.QueryString("Forecast").ToString.Trim <> "" Then
    '        '    WHR = WHR & " and T.forecastNo like '%" & Forecast & "%' "
    '        'End If
    '        SQL = ""
    '        SQL = "select T.forecastNo as 'Froecast No-Seq', COPMF.MF003 as 'Item', COPMF.MF004 as 'Desc', COPMF.MF005 as 'Spec', " & _
    '            " cast (T.chgQtyfrom as decimal(16,2)) as 'Change Qty From', cast (T.chgQtyto as decimal(16,2)) as 'Change Qty To', " & _
    '            " CONVERT (datetime,T.chgDate) as 'Change Date (M/D/Y)', cast (COPMF.MF008 as decimal(16,2)) as 'Forecast Qty', cast (COPMF.MF009 as decimal(16,2)) as 'Forecast Order Qty' " & _
    '            " from " & tempTable & " T " & _
    '            " left join JINPAO80.dbo.COPMF COPMF on Rtrim(COPMF.MF001)+'-'+COPMF.MF002 = T.forecastNo " & _
    '            " where Rtrim(COPMF.MF001)+'-'+COPMF.MF002 = T.forecastNo " & _
    '            " Order by T.chgDate "

    '        ControlForm.ShowGridView(gvShowFore, SQL, Conn_SQL.MIS_ConnectionString)
    '        CountRow2.RowCount = ControlForm.rowGridview(gvShowFore)

    '    End If
    'End Sub

End Class