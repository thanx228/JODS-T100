Imports System.Data
Imports System
Public Class DemoCheckBoxList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnCallData_Click(sender As Object, e As EventArgs) Handles btnCallData.Click

        Dim SelectMulti_WC As String = UsingWorkstationCheckList.CheckBoxArray
        ' Dim dt As DataTable = SFBA.GetManufactureOrder_WorkStation_Body(SelectMulti_WC)

        'Dim dt2 As DataTable
        'Dim BOM_Str As String
        'Dim YrStrList As List(Of [String]) = New List(Of String)()
        'For Each sitem As ListItem In cblCodeType.Items
        '    If sitem.Selected Then
        '        YrStrList.Add(sitem.Value)
        '    End If
        'Next
        'If YrStrList.ToArray() Is Nothing Then
        '    BOM_Str = String.Empty
        'Else
        '    BOM_Str = " '" & [String].Join("' , '", YrStrList.ToArray())

        'End If
        'UsingStatusMO_Normal_checkList.CheckBoxArray()
        'dt2 = SFBA.GetManufactureOrder_MultiplaceBOM_Item_Body(BOM_Str)
        'lblselect.Text = INBJ.GetBody_Scarp_Destory_ItemNo("8021301204900001")
        'GridView1.DataSource = dt
        'GridView1.DataBind()
        Dim xx As String = "5106-20170220033"
        ''Dim Where_Custom As String = SFBA.MO_DocNo & " =  '" & xx & "'"
        'Dim str_Lineitem As String = "60"
        'Dim Where As String = SFCB.WO_No & "=" & "'" & xx & "' AND " & SFCB.LineNo & "=" & "'" & str_Lineitem & "'"
        'lblselect.Text = SFCB.GetDataRowsProcessItemWhereCustom(Where)
        ' lblselect.Text = SFBA.GetManufactureOrder_SaleOrderDocumentDate__Body(" 05/19/2014")
        Dim Where_Custom As String = SFBA.MODocNo & " =  '" & ReplaceString.ReplaceMO("5106", "20170220033") & "'"
        'lblselect.Text = SFBA.GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body(Where_Custom)
    End Sub

    Private Sub btnCheckAll_Click(sender As Object, e As EventArgs) Handles btnCheckAll.Click
        UsingWorkstationCheckList.CheckedListBox_CheckAll()
    End Sub

    Private Sub btnUncheckAll_Click(sender As Object, e As EventArgs) Handles btnUncheckAll.Click
        UsingWorkstationCheckList.CheckedListBox_UnCheckAll()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'lblsql.Text = SFDA.GetMatIssueHead_Status(TextBox1.Text)
        'Dim dtxx As DataTable = SFDA.GetMatIssueHead_Status(TextBox1.Text)
        'GridView2.DataSource = dtxx
        'GridView2.DataBind()
    End Sub

End Class