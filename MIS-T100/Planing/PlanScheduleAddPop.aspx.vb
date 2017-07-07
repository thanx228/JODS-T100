Public Class PlanScheduleAddPop
    Inherits System.Web.UI.Page

    Dim SFAA As New SFAA
    Dim SFCB As New SFCB
    Dim IMAAL As New IMAAL
    Dim OOCQL As New OOCQL
    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function showMODetail(ByVal docNO As String) As String

        Dim str As String = ""
        Dim row As Integer = 0
        Dim ds As DataSet
        'fields
        Dim mo As String = "", qty As String = "", spec As String = ""

        Dim sql As String = "SELECT " & SFAA.DocNo & " As MONumber, " & SFAA.ProductionQty & " AS MOQTY, " & IMAAL.Specifaction & " AS Spec " &
            "FROM " & SFAA.tblMO & " " &
            "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFAA.ProductItem & "=" & IMAAL.ProductItem & " " &
            "WHERE " & SFAA.ProgromCode & "='3' AND " & IMAAL.ent & "='3' AND " & SFAA.DocNo & "='" & docNO & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            str = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;MO Document Number&nbsp;</th><th>&nbsp;Qty&nbsp;</th><th>&nbsp;Specification&nbsp;</th></tr>"
            For i = 0 To row - 1
                mo = ds.Tables("DATASET")(i)("MONumber").ToString
                qty = ds.Tables("DATASET")(i)("MOQTY").ToString
                spec = ds.Tables("DATASET")(i)("SPEC").ToString
                str = str + "<tr bgcolor=#ffffff height=25><td align=center>&nbsp;" & mo & "&nbsp;</td>" + "<td align=center>&nbsp;" & qty & "&nbsp;</td>" + "<td align=left>&nbsp;" & spec & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        End If

        Return str

    End Function

    Public Function getRelateOperationMO(ByVal docNO As String) As String

        Dim str As String = ""
        Dim row As Integer = 0
        Dim ds As DataSet
        'fields
        Dim item As String = "", descrp As String = "", spec As String = "", reqQty As String = "", issueQty As String = ""
        Dim notissue As String = "", unit As String = "", wh As String = "", stQty As String = ""

        Dim sql As String = "SELECT " & SFBA.IssueItem & " AS IssuedItem, " & IMAAL.ProductName & " AS ItemDescription, " & IMAAL.Specifaction & " AS Spec, " &
                            "" & SFBA.StandardIssuanceQuantity & " AS RequiredQty, " & SFBA.IssuedQty & " As IssuedQty, (" & SFBA.StandardIssuanceQuantity & "-" & SFBA.IssuedQty & ") As NotIssuedQty, " &
                            "" & SFBA.Unit & " As Unit, " & SFBA.DesignatedIssuanceWarehouse & " As Warehouse, (SELECT SUM(" & INAG.ActualStock & ") AS StockQty FROM " & INAG.tblInventoryDeatil & " WHERE " & SFBA.IssueItem & "=" & INAG.ItemNo & ") AS StockQty " &
                            "FROM " & SFBA.tblManufactureOrder_Body & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFBA.IssueItem & "=" & IMAAL.ProductItem & " " &
                            "WHERE " & SFBA.ent & "='3' AND " & IMAAL.ent & "='3'AND " & SFBA.MODocNo & "='" & docNO & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            str = "<br /><table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;IssuedItem&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;RequiredQty&nbsp;</th><th>&nbsp;IssuedQty&nbsp;</th><th>&nbsp;NotIssue&nbsp;</th><th>&nbsp;Unit&nbsp;</th><th>&nbsp;Werehouse&nbsp;</th><th>&nbsp;StockQty&nbsp;</th></tr>"
            For i = 0 To row - 1
                item = ds.Tables("DATASET")(i)("IssuedItem").ToString
                descrp = ds.Tables("DATASET")(i)("ItemDescription").ToString
                spec = ds.Tables("DATASET")(i)("Spec").ToString
                reqQty = ds.Tables("DATASET")(i)("RequiredQty").ToString
                issueQty = ds.Tables("DATASET")(i)("IssuedQty").ToString
                notissue = ds.Tables("DATASET")(i)("NotIssuedQty").ToString
                unit = ds.Tables("DATASET")(i)("Unit").ToString
                wh = ds.Tables("DATASET")(i)("Warehouse").ToString
                stQty = ds.Tables("DATASET")(i)("StockQty").ToString
                str = str + "<tr bgcolor=#ffffff height=25><td align=center>&nbsp;" & item & "&nbsp;</td>" + "<td align=left>&nbsp;" & descrp & "&nbsp;</td>" + "<td align=left>&nbsp;" & spec & "&nbsp;</td>" + "<td align=right>&nbsp;" & reqQty & "&nbsp;</td>" + "<td align=right>&nbsp;" & issueQty & "&nbsp;</td>" + "<td align=right>&nbsp;" & notissue & "&nbsp;</td>" + "<td align=center>&nbsp;" & unit & "&nbsp;</td>" + "<td align=center>&nbsp;" & wh & "&nbsp;</td>" + "<td align=right>&nbsp;" & stQty & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        End If

        Return str

    End Function

End Class