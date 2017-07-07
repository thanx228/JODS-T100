Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.util.collections
Imports System.Data.OracleClient
Public Class LockProductionPlan
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../Login.aspx")
            End If
            clearData()
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Dim SQL As String = "",
            WHR As String = ""
        If tbBatchNo.Text.Trim <> "" Then
            WHR = WHR & " and TA001 like '%" & tbBatchNo.Text.Trim & "%' "
        End If
        If tbSaleType.Text.Trim <> "" Then
            WHR = WHR & " and TA023 = '" & tbSaleType.Text.Trim & "' "
        End If
        If tbSaleNo.Text.Trim <> "" Then
            WHR = WHR & " and TA024 = '" & tbSaleNo.Text.Trim & "' "
        End If
        If tbSaleSeq.Text.Trim <> "" Then
            WHR = WHR & " and TA025 = '" & tbSaleSeq.Text.Trim & "' "
        End If
        If tbItem.Text.Trim <> "" Then
            WHR = WHR & " and TA002 like '%" & tbItem.Text.Trim & "%' "
        End If
        If tbSpec.Text.Trim <> "" Then
            WHR = WHR & " and MB002 like '%" & tbSpec.Text.Trim & "%' "
        End If

        SQL = " select TA001 as 'Batch No.',TA050 'Versoin',TA023+'-'+TA024+'-'+TA025 as 'Sale Order'," & _
              " (SUBSTRING(TA002,1,14)+'-'+SUBSTRING(TA002,15,2)) as item,MB003 as Spec,cast(TA006 as decimal(10,2)) as 'Prod.Qty'," & _
              " (SUBSTRING(TA007,7,2)+'-'+SUBSTRING(TA007,5,2)+'-'+SUBSTRING(TA007,1,4)) as 'Plan Start Date'," & _
              " (SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Plan Complete Date'," & _
              " TA010 as 'MO Type',TA004 as 'WC',TA005 as 'WH',TA009 as 'Lock',TC012 'Industry Type'" & _
              " from LRPTA  left join LRPLA on LA001=TA001  and LA012=TA050 " & _
              "  left join INVMB on MB001=TA002 " & _
              " LEFT JOIN JINPAO80.dbo.COPTC ON TC001=TA023 and TC002=TA024 " & _
              " where TA029='1' and TA051 = 'N' and LA005='1' and LA013 = '1' and TA006>0 " & WHR & _
              " order by TA001,TA023,TA024,TA025,TA002 "
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
        Dim row As Decimal = ControlForm.rowGridview(gvShow)
        CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        If row > 0 Then
            btLock.Visible = True
        End If

        'btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub btLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLock.Click
        With gvShow
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim cbSelect As CheckBox = .FindControl("cbSelect")
                    If cbSelect.Checked Then
                        If .Cells(11).Text <> "Y" Then
                            Dim USQL As String = " update LRPTA set TA009='Y' where TA001='" & .Cells(1).Text.Trim & "' and TA050='" & .Cells(2).Text.Trim & "' and TA023+'-'+TA024+'-'+TA025='" & .Cells(3).Text.Trim & "' and TA002='" & .Cells(4).Text.Trim.Replace("-", "") & "' "
                            Conn_SQL.Exec_Sql(USQL, Conn_SQL.ERP_ConnectionString)
                        End If
                    End If
                End With
            Next
        End With
    End Sub
    Sub clearData()
        tbBatchNo.Text = ""
        tbSaleType.Text = ""
        tbSaleNo.Text = ""
        tbSaleSeq.Text = ""
        tbItem.Text = ""
        tbSpec.Text = ""
        btLock.Visible = False
        gvShow.DataSource = ""
        gvShow.DataBind()

        'Dim da As New SqlDataAdapter()
        'Dim ds As New DataSet
        'da.Fill(ds)
        'ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
        'gvShow.DataSource = ds
        'gvShow.DataBind()
        'Dim ColumnCount As Integer = gvShow.Rows(0).Cells.Count
        'gvShow.Rows(0).Cells.Clear()
        'gvShow.Rows(0).Cells.Add(New TableCell())
        'gvShow.Rows(0).Cells(0).ColumnSpan = ColumnCount
        'gvShow.Rows(0).Cells(0).Text = "No Data Found"

    End Sub

    Protected Sub btClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btClear.Click
        clearData()
    End Sub
End Class