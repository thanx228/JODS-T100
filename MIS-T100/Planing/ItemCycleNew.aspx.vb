Imports System.IO
Imports System.Data
Imports System.Globalization
Imports System
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class ItemCycleNew
    Inherits System.Web.UI.Page
    Dim Conn_sql As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("../LoginT100.aspx")
        End If
        'gvShow.Visible = False
        LCount.Text = 0
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click
        'gvShow.Visible = True
        'GridView1.AllowPaging = False
        ' SqlDataSource1.SelectCommand = "select MB001,MB002,MB003,MB004,MC007,MC002 from INVMC C left join INVMB B on(B.MB001 =C.MC001) where MC002='" & DDLWh.SelectedValue & "' and MC007 > 0"
        'SqlDataSource1.SelectCommand = "select RunNo,Wid+'-'+WName as Expr1,Item,[Desc],Spec,Qty,Unit from ItemCycle where Wid='" & DDLWh.SelectedValue & "' order by RunNo "
        'GridView1.DataBind()
        Dim SQL As String = "select WName as WH,Wid as Bin,RunNo,Item,[Desc],Spec,cast(Qty as decimal(15,3)) as Qty,Unit from ItemCycle where WName like '" & DDLWh.SelectedValue.Trim & "%' and RunNo like '" & ddlPreroid.SelectedValue.Trim & "%' order by RunNo "
        Dim dtItemCyccle As DataTable = GetDataSQLserverMIS(SQL)
        If dtItemCyccle.Rows.Count > 0 Then
            gvShow.DataSource = dtItemCyccle
            gvShow.DataBind()
            LCount.Text = dtItemCyccle.Rows.Count.ToString
        End If
        'System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub Busave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Busave.Click
        'InsertItemCycle()
        generateItemCycle()
        Dim SQL As String = "select WName as WH,Wid as Bin,RunNo,Item,[Desc],Spec,cast(Qty as decimal(15,3)) as Qty,Unit from ItemCycle where WName like '" & DDLWh.SelectedValue.Trim & "%' and RunNo like '" & ddlPreroid.SelectedValue.Trim & "%' order by RunNo "
        ControlForm.ShowGridView(gvShow, SQL)
        LCount.Text = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub generateItemCycle()
        '###### Delete #############################
        Dim docstr As String = Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo),
            wh As String = DDLWh.Text.Trim

        Dim DSQL As String = " delete from ItemCycle where WName like'" & wh & "%' and RunNo like '" & docstr & "%' "
        Conn_sql.Exec_Sql(DSQL, Conn_sql.MIS_ConnectionString)


        '###### GenerateItemCycle #############################
        Dim CreateDate As Date = Date.Now.Date.ToString("dd-MMM-yyyy")

        Dim Program As New DataTable
        Dim RNo As Integer = docstr & "0001"
        Dim NoShow As String = ""
        Dim WHName As String = Conn_sql.Get_value("select MC002 from [JINPAO80].[dbo].[CMSMC] where MC001='" & wh & "'", Conn_sql.ERP_ConnectionString)

        Dim SQL As String = "select MB001,MB002,MB003,MB004,MC007,MC015 from INVMC C left join INVMB B on(B.MB001 =C.MC001) where MC002='" & DDLWh.SelectedValue.Trim & "' and (MC007 <> 0 or MC008 <> 0)  order by MB001"

        Program = Conn_sql.Get_DataReader(SQL, Conn_sql.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable

                NoShow = wh & "-" & RNo
                whrHash.Add("RunNo", RNo) 'NUMBER RUNNING
                whrHash.Add("WName", wh & ":" & WHName) 'WAREHORE NAME
                'Insert
                fldInsHash.Add("Item", .Item("MB001").ToString.Trim) 'ITEM
                fldInsHash.Add("[Desc]", .Item("MB002").ToString.Trim) 'DESCRIPTION
                fldInsHash.Add("Spec", .Item("MB003").ToString.Trim) 'SPEC
                fldInsHash.Add("Wid", .Item("MC015").ToString.Trim) 'Bin ID
                fldInsHash.Add("Qty", .Item("MC007")) 'QUANTITY
                fldInsHash.Add("Unit", .Item("MB004").ToString.Trim) 'UNIT
                fldInsHash.Add("UserID", Session("UserName")) 'USER ID GENERATE
                fldInsHash.Add("CreateDate", CreateDate) 'CREATE DATE
                'fldInsHash.Add("PrintNo", .Item("TA004")) 'PRINTE NO
                fldInsHash.Add("NoShow", wh & "-" & RNo) 'NUMBER SHOW
                Conn_sql.Exec_Sql(Conn_sql.GetSQL("ItemCycle", fldInsHash, whrHash, "I"), Conn_sql.MIS_ConnectionString)
                RNo = CInt(RNo) + 1

            End With
        Next
        show_message.ShowMessage(Page, "Generate warehouse='" & wh & "-" & WHName & "' is completed !!!", UpdatePanel1)
    End Sub

    'Private Sub InsertItemCycle()
    '    Dim CreateDate As Date
    '    CreateDate = Date.Now.Date.ToString("dd-MMM-yyyy")
    '    Dim PNo As String
    '    Dim PrintNo As New Data.DataTable
    '    PrintNo = Conn_sql.Get_DataReader("  select isnull(MAX(PrintNo),0) from ItemCycle e", Conn_sql.MIS_ConnectionString)
    '    If PrintNo.Rows(0).Item(0) = 0 Then
    '        PNo = 1
    '    Else
    '        PNo = CInt(PrintNo.Rows(0).Item(0)) + 1
    '    End If
    '    If gvShow.Rows.Count > 0 Then
    '        For M_count As Integer = 0 To gvShow.Rows.Count - 1

    '            Dim Item As String = gvShow.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Desc As String = gvShow.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Spec As String = gvShow.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "").Replace("&#39;", "").Replace("&quot;", "")
    '            Dim Qty As String = gvShow.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Unit As String = gvShow.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Wh As String = gvShow.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim WHName As String = Conn_sql.Get_value("select MC002 from [JINPAO80].[dbo].[CMSMC] where MC001='" & Wh & "'", Conn_sql.ERP_ConnectionString)

    '            Dim program As New Data.DataTable
    '            Dim RNo As Integer
    '            Dim NoShow As String
    '            Dim docstr As String = Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo)
    '            program = Conn_sql.Get_DataReader("SELECT isnull(max(RunNo),0) FROM ItemCycle where RunNo like '%" & docstr & "%' ", Conn_sql.MIS_ConnectionString)
    '            If program.Rows(0).Item(0) = 0 Then
    '                RNo = docstr & "0001"
    '                NoShow = DDLWh.SelectedValue.Replace(" ", "") & "-" & RNo
    '            Else
    '                RNo = CInt(program.Rows(0).Item(0)) + 1
    '                NoShow = DDLWh.SelectedValue.Replace(" ", "") & "-" & RNo
    '            End If

    '            Dim InSQL As String = "Insert into ItemCycle(Item,[Desc],Spec,Wid,WName,Qty,Unit,UserID,RunNo,CreateDate,PrintNo,NoShow)"
    '            InSQL = InSQL & " Values('" & Item & "','" & Desc & "',"
    '            InSQL = InSQL & "'" & Spec & "','" & Wh & "',"
    '            InSQL = InSQL & "'" & WHName & "','" & Qty & "',"
    '            InSQL = InSQL & "'" & Unit & "',"
    '            InSQL = InSQL & "'" & Session("UserName") & "','" & RNo & "','" & CreateDate & "','" & PNo & "','" & NoShow & "')"
    '            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
    '        Next
    '    End If

    '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ItemCycle1.aspx?PNo=" + PNo + "');", True)
    'End Sub

    Protected Sub BuExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuExcel.Click
        'ItemList()
        Dim typeFor As String = ddlFor.Text.Trim,
            txtQty As String = "Audit Qty",
            txtBy As String = "Audit By"

        If typeFor = "1" Then
            txtQty = "Check Qty"
            txtBy = "Check By"
        End If
        Dim paraName As String = "wh:" & DDLWh.SelectedValue.Trim & ",period:" & ddlPreroid.SelectedValue.Trim & ",txtQty:" & txtQty & ",txtBy:" & txtBy
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ShowCrystalReportPlan.aspx?dbName=MIS&ReportName=ItemList.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    'Private Sub ItemList()

    '    Dim DelItemList As String = "delete from ItemList"
    '    Conn_sql.Exec_Sql(DelItemList, Conn_sql.MIS_ConnectionString)

    '    If gvShow.Rows.Count > 0 Then
    '        For M_count As Integer = 0 To gvShow.Rows.Count - 1

    '            Dim Item As String = gvShow.Rows(M_count).Cells(0).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Desc As String = gvShow.Rows(M_count).Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Spec As String = gvShow.Rows(M_count).Cells(2).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Qty As String = gvShow.Rows(M_count).Cells(3).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Unit As String = gvShow.Rows(M_count).Cells(4).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim Wh As String = gvShow.Rows(M_count).Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim CountQty As String = gvShow.Rows(M_count).Cells(6).Text.Replace(" ", "").Replace("&nbsp;", "")
    '            Dim CountBy As String = gvShow.Rows(M_count).Cells(7).Text.Replace(" ", "").Replace("&nbsp;", "")

    '            Dim InSQL As String = "Insert into ItemList(Item,[Desc],Spec,Qty,Unit,Wh,CountQty,CountBy)"
    '            InSQL = InSQL & " Values('" & Item & "','" & Desc & "',"
    '            InSQL = InSQL & "'" & Spec & "','" & Qty & "',"
    '            InSQL = InSQL & "'" & Unit & "','" & Wh & "',"
    '            InSQL = InSQL & "'" & CountQty & "','" & CountBy & "')"
    '            Conn_sql.Exec_Sql(InSQL, Conn_sql.MIS_ConnectionString)
    '        Next
    '    End If

    '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('ItemCycle2.aspx?');", True)

    'End Sub

    Protected Sub BuPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuPrint.Click
        Dim whr As String = ""
        If cbQty.Checked Then
            whr = " and Qty<>'0.000000' "
        End If
        Dim paraName As String = "userPrint:" & Session("UserName") & ",wh:" & DDLWh.SelectedValue.Trim & ",period:" & ddlPreroid.SelectedValue.Trim & ",Qty:" & whr
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ShowCrystalReportPlanaspx?dbName=MIS&ReportName=ItemCycle.rpt&paraName=" & Server.UrlEncode(paraName) & "&encode=1');", True)
    End Sub

    Protected Sub BuEx_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuEx.Click
        ControlForm.ExportGridViewToExcel("ItemCycle" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Private Shared Function GetDataOracleDate(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataOracleDate", "strSQL", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared Function GetDataSQLserverMIS(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataSQLserverMIS", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
End Class