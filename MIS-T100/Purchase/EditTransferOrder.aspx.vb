Public Class EditTransferOrder
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnectT100
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTable As New CreateTempTableT100
    Dim TransfType As String = "'D201','D203','D204','D205','D206','D209','D212'"
    Dim tableLog As String = "TransferOutsourcLog"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If
            Dim SQL As String = ""
            SQL = "select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl002='en_US' and oobxl001 in  (" & TransfType & ")"
            showDDL(DDLType, SQL, "CodeName", "Code", True, clsDBConnect.T100)
        End If
        CreateTable.CreateTransferOutsourcLog(tableLog)
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Dim format As System.IFormatProvider = New System.Globalization.CultureInfo("en-US")
        Dim sdate As String = DateTime.Now.ToString()
        Dim newdate As DateTime = DateTime.Parse(sdate, format)
        Dim resultdate As String = newdate.ToString("dd/MM/yyyy")
        'CreateDate = resultdate
        GridView2.Visible = False
    End Sub
    Public Sub showDDL(ByRef ControlDDL As DropDownList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "", Optional ByVal headVal As String = "ALL", Optional dbType As String = "A")
        Dim dt As New DataTable
        dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
        clsDBConnect.Close(connectSting)

        With ControlDDL
            .DataSource = dt
            .DataTextField = fldText
            .DataValueField = fldValue
            .DataBind()
            If setAll = True Then
                .Items.Insert(0, headVal)
            End If
        End With
    End Sub
    Protected Sub BuSearch_Click(sender As Object, e As EventArgs) Handles BuSearch.Click
        Dim SQL As String = ""
        Dim dt As New DataTable
        If txtno.Text = "" Then
            show_message.ShowMessage(Page, "Please Insert Transfer Type/No.  ", UpdatePanel1)
            txtno.Focus()
            Exit Sub
        ElseIf txtno.Text <> "" Then
            'sfcb012 Allow Outsourcing
            SQL = "select sffbdocno DocNo,sffb008 Seq,sffb005 MO,sffb029 Item,imaal003 Description,imaal004 Spec,CAST(sffb017 AS DECIMAL(16,2)) Qty " &
                " from sffb_t " &
                " left join sfcb_t on sfcbdocno=sffb005 and sfcb001=sffb006 and sfcb003=sffb007 and sfcb004=sffb008 " &
                " left join sfca_t on sfcadocno=sfcbdocno and sfca001=sfcb001 " &
                " left join imaal_t on imaal001=sffb029 " &
                " where sffbent='3'  and sfcaent='3' and sfcbent='3' and imaalent='3' " &
                " and sfcasite='JINPAO' and sfcasite='JINPAO' and sfcbsite='JINPAO' " &
                " and sfcb012='Y' and SUBSTR(sffbdocno,3,4)='" & DDLType.SelectedValue & "' and SUBSTR(sffbdocno,8,11) = '" & txtno.Text.Trim & "' " &
                " order by sffb008 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            GridView2.DataSource = dt
            GridView2.DataBind()
        End If

        'ClearData()
        GridView2.Visible = True

    End Sub

    Private Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "OnChange" Then
            Dim i As Integer = e.CommandArgument

            Dim dType As String = GridView2.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
            'Dim dNo As String = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Ltype.Text = dType
            Lseq.Text = GridView2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
            Litem.Text = GridView2.Rows(i).Cells(4).Text.Replace("&nbsp;", "")
            Ldesc.Text = GridView2.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
            Lspec.Text = GridView2.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
            Lqty.Text = GridView2.Rows(i).Cells(7).Text.Replace("&nbsp;", "")

            'Dim sql As String = "select TH007,TH008,TH015,TH030 from MOCTH where TH001='" & dType & "' and TH002='" & dNo & "' "

            'Dim aa As New Data.DataTable
            'aa = Conn_SQL.Get_DataReader(sql, Conn_SQL.ERP_ConnectionString)
            'If aa.Rows.Count > 0 Then
            '    Lcurrency.Text = aa.Rows(0).Item("TH007")
            '    Lrate.Text = aa.Rows(0).Item("TH008")
            '    Ltaxtype.Text = aa.Rows(0).Item("TH015")
            '    Ltaxrate.Text = aa.Rows(0).Item("TH030") * 100
            'End If

            GridView2.Visible = True
        End If

    End Sub
    Private Sub BuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuSave.Click

        Dim StrSql As String
        Dim SqlSubcontact As String
        Dim qty As Double = Lqty.Text
        Dim Price As Double = txtprice.Text
        Dim Amount As Double = qty * Price

        'StrSql = "update SFCTC set TC017='" & txtprice.Text & "' , TC018='" & Amount & "' where TC001='" & Ltype.Text & "' and TC002='" & Lno.Text & "' and TC003='" & Lseq.Text & "'"
        'Conn_SQL.Exec_Sql(StrSql, Conn_SQL.ERP_ConnectionString)

        ''get rate
        'Dim rateVat As Decimal = 0,
        '    rate As Decimal = 0
        'If Lrate.Text.Trim <> "" Then
        '    rate = CDec(Lrate.Text.Trim)
        'End If
        'If Ltaxtype.Text.Trim <> "3" And Ltaxtype.Text.Trim <> "" Then
        '    rateVat = CDec(Ltaxrate.Text.Trim / 100)
        'End If

        'SqlSubcontact = "update MOCTI set TI024='" & txtprice.Text.Replace(" ", "") & "', TI025='" & Amount & "', TI044='" & Amount & "', TI046='" & Amount * rate & "', TI045='" & Amount * rateVat & "', TI047='" & Amount * rateVat * rate & "' where TI001='" & Ltype.Text & "' and TI002='" & Lno.Text & "' and TI003='" & Lseq.Text & "'"
        'Conn_SQL.Exec_Sql(SqlSubcontact, Conn_SQL.ERP_ConnectionString)

        'Dim StrTemp As String
        'StrTemp = "insert into TempTransferOrder(Type,No,Seq,Item,Dec,Spec,Qty,Price,Amount,CreateBy,CreateDate) Values('" & Ltype.Text & "','" & Ltype.Text & "','" & Lseq.Text & "','" & Litem.Text & "','" & Lqty.Text & "','" & txtprice.Text & "','" & Amount & "','" & Session("Username") & "','" & CreateDate & "')"
        'Conn_SQL.Exec_Sql(StrTemp, Conn_SQL.MIS_ConnectionString)
        'ClearData()
        'GridView2.Visible = False

    End Sub

    Private Sub ClearData()
        Ltype.Text = ""
        'Lno.Text = ""
        Lseq.Text = ""
        Litem.Text = ""
        Ldesc.Text = ""
        Lspec.Text = ""
        Lqty.Text = ""
        txtprice.Text = ""
        Ltaxtype.Text = ""
        Ltaxrate.Text = ""
        Lcurrency.Text = ""
        Lrate.Text = ""
        GridView2.DataBind()
    End Sub
End Class