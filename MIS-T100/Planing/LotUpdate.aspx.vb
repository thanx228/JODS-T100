Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.util.collections
Imports System.Data.OracleClient
Public Class LotUpdate
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ConForm As New ControlDataForm
    Dim ConSQL As New ConnSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            ucHeaderForm.HeaderLable = ConForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            ' clear()
        End If
    End Sub

    Sub clear()
        'ucDocTypeSO.setObjectWithAll = "22"
        'ucDocTypeMO.setObjectWithAll = "51,52"
        'tbSO.Text = ""
        'tbSoSeq.Text = ""
        'tbMoNo.Text = ""
        'ucDate.dateVal = ""
    End Sub

    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        clear()
    End Sub

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        'Dim SQL As String
        'Dim colName() As String = {"SO:A",
        '                           "MO:B",
        '                           "Item:C",
        '                           "Spec:D",
        '                           "Raw Mat:E",
        '                           "Control Plan:F",
        '                           "Cust Model:G"}

        'SQL = " select  TA026+'-'+TA027+'-'+TA028 A,TA001+'-'+TA002 B ,TA006 C,TA035 D,isnull(UDF02,'') G,isnull(UDF03,'') E,isnull(UDF04,'') F" &
        '      " from MOCTA " &
        '      " where TA011 not in ('Y','y') " & getWhr() &
        '      " order by TA001,TA002,TA003 "
        'ConForm.GridviewColWithLinkFirst(gvShow, colName, False)
        'ConForm.ShowGridView(gvShow, SQL, ConSQL.ERP_ConnectionString)
        'ucCountRow.RowCount = ConForm.rowGridview(gvShow)
        'System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub
    Function getWhr() As String
        'Dim WHR As String
        'WHR = ConSQL.Where("TA001", ucDocTypeMO.getObject)
        'WHR &= ConSQL.Where("TA002", tbMoNo, False)
        'WHR &= ConSQL.Where("TA003", ucDate.dateVal, ucDate.dateVal)
        'WHR &= ConSQL.Where("TA026", ucDocTypeSO.getObject)
        'WHR &= ConSQL.Where("TA027", tbSO, False)
        'WHR &= ConSQL.Where("TA028", tbSoSeq, False)
        'Return WHR
    End Function
    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        'Dim SQL As String,
        '    dt As DataTable,
        '    USQL As String = ""
        'SQL = " select TA006,MB010,MB011,count(*) cnt from MOCTA left join INVMB on MB001=TA006  where TA011 not in ('Y','y') " & getWhr() & "  group by TA006,MB010,MB011 order by TA006,MB010,MB011 "
        'dt = ConSQL.Get_DataReader(SQL, ConSQL.ERP_ConnectionString)
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    With dt.Rows(i)
        '        Dim sSQL As String,
        '            sdt As DataTable,
        '            save As Boolean = False,
        '            custModel As String = "",
        '            controlPlan As String = "",
        '            rawMaterials As String = "",
        '            item As String = Trim(.Item("MB010")),
        '            itemRout As String = Trim(.Item("MB011"))

        '        'get from item routing
        '        sSQL = "select isnull(UDF01,'') UDF01,isnull(UDF02,'') UDF02 from BOMME where ME001='" & item & "' and ME002='" & itemRout & "' "
        '        sdt = ConSQL.Get_DataReader(sSQL, ConSQL.ERP_ConnectionString)
        '        If sdt.Rows.Count > 0 Then
        '            'save = True
        '            With sdt.Rows(0)
        '                custModel = Trim(.Item("UDF01"))
        '                controlPlan = Trim(.Item("UDF02"))
        '            End With
        '        End If
        '        'get from BOM
        '        sSQL = "select rtrim(MB002)+''+rtrim(MB003) MB002 from BOMMD left join INVMB on MB001=MD003  where MD001='" & item & "' and substring(MD003,3,1)='1' order by MD002 "
        '        sdt = ConSQL.Get_DataReader(sSQL, ConSQL.ERP_ConnectionString)
        '        If sdt.Rows.Count > 0 Then
        '            'save = True
        '            With sdt.Rows(0)
        '                rawMaterials = Trim(.Item("MB002"))
        '            End With
        '        End If
        '        'If save Then
        '        USQL = "update MOCTA set UDF02='" & custModel & "',UDF03='" & rawMaterials & "',UDF04='" & controlPlan & "' where TA006='" & item & "' and TA011 not in ('Y','y');  "
        '        'End If
        '        'update QC From 
        '        If cbQC.Checked Then

        '            SQL = "select isnull(UDF01,'') UDF01,MF003 from BOMMF where MF001='" & item & "' and MF002='" & itemRout & "' and isnull(UDF01,'')<>''  order by MF003 "
        '            sdt = ConSQL.Get_DataReader(SQL, ConSQL.ERP_ConnectionString)
        '            If sdt.Rows.Count > 0 Then
        '                For j As Integer = 0 To sdt.Rows.Count - 1
        '                    Dim QC As String = sdt.Rows(j).Item(0),
        '                        routSeq As String = sdt.Rows(j).Item(1)
        '                    SQL = "select TA001,TA002 from MOCTA where TA006='" & item & "' and TA011 not in ('Y','y') " & getWhr() & " order by TA001,TA002"
        '                    Dim sdt2 As DataTable = ConSQL.Get_DataReader(SQL, ConSQL.ERP_ConnectionString)
        '                    If sdt2.Rows.Count > 0 Then
        '                        For k As Integer = 0 To sdt2.Rows.Count - 1
        '                            Dim moType As String = sdt2.Rows(k).Item(0),
        '                                moNo As String = sdt2.Rows(k).Item(1)
        '                            USQL &= " update SFCTA set SFCTA.UDF01='" & QC & "' where TA001='" & moType & "' and TA002='" & moNo & "' and TA003='" & routSeq & "';  "
        '                        Next
        '                    End If
        '                Next
        '            End If
        '        End If
        '        'Dim aa As String = ""
        '        ConSQL.Exec_Sql(USQL, ConSQL.ERP_ConnectionString)
        '    End With
        'Next
        'show_message.ShowMessage(Page, "update " & dt.Rows.Count & " row!!!", UpdatePanel1)
        'btSearch_Click(sender, e)
        'clear()
    End Sub
End Class