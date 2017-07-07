Public Class MaterialsOverIssue
    Inherits System.Web.UI.Page

    '----------------------- Production Module - Mat Over Issue ---------------------
    '                          Original Code Module - JODS
    '                       Version 2 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    'This Program no need any temptable to restore data
    'Declare Class

    Dim SFDC As New SFDC
    Dim SFBA As New SFBA
    Dim SFAA As New SFAA
    Dim IMAAL As New IMAAL
    Dim clsConnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
        End If
    End Sub

    Public Function getMatOverIssue(ByVal MOdateFrom As String, MOdateTo As String, ByVal cond As String) As String

        'Document Number 5501 is for "Over Issue Only"

        Dim str As String = ""
        Dim ds As DataSet
        Dim issueno As String = ""
        Dim mono As String = ""
        Dim DocDate As System.DateTime
        Dim rmItem As String = ""
        Dim spec As String = ""
        Dim stdreqQty As String = ""
        Dim issueQty As String = ""
        Dim different As String = 0
        Dim unit As String = ""
        Dim condStr As String = ""
        Dim overpercent As String = ""
        condStr = getCondition(cond)

        'Version2
        Dim sql As String = "SELECT " & SFDC.IssueDocNo & " AS MOI_Number, " & SFDC.WONo & " AS MO_Number, " & SFAA.DocumentDate & " AS DocumentDate," &
                                    "" & SFDC.RequirementItem_No & " AS RawMaterialItemCode, " & IMAAL.Specifaction & " AS Spec, " & SFBA.StandardIssuanceQuantity & " AS RequireQty," &
                                    "(" & SFBA.IssuedQty & "+" & SFBA.UnplannedIssued & ") AS OverallIssueQty,ROUND((" & SFBA.IssuedQty & "+" & SFBA.UnplannedIssued & ")-" & SFBA.StandardIssuanceQuantity & ",2) AS Different," &
                                    "" & SFBA.Unit & " AS Unit, ROUND((((" & SFBA.IssuedQty & "+" & SFBA.UnplannedIssued & ")-" & SFBA.StandardIssuanceQuantity & ")/" & SFBA.StandardIssuanceQuantity & ")*100,2) AS Overpercent " &
                                    "FROM " & SFDC.tblMatIssueDistribution & " " &
                                    "LEFT JOIN " & SFBA.tblManufactureOrder_Body & " On " & SFDC.RequirementItem_No & "=" & SFBA.IssueItem & " And " & SFDC.WONo & "=" & SFBA.MODocNo & " " &
                                    "LEFT JOIN " & SFAA.tblMO & " ON " & SFDC.WONo & "=" & SFAA.DocNo & " " &
                                    "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFDC.RequirementItem_No & "=" & IMAAL.W_ProductItem & " " &
                                    "WHERE " & SFDC.ent & "='3' AND " & SFAA.ProgromCode & "='3'  AND " & IMAAL.ent & "='3' AND " & SFDC.IssueDocNo & "  LIKE 'JP5501%' " &
                                    "AND " & SFAA.DocumentDate & " BETWEEN TO_DATE('" & MOdateFrom & "','dd/MM/yyyy') AND TO_DATE('" & MOdateTo & "','dd/MM/yyyy') " &
                                    "" & condStr & " AND " & SFBA.StandardIssuanceQuantity & "<>0 ORDER BY " & SFDC.IssueDocNo & ""
        'Version 1
        'Dim sql As String = "SELECT " & SFDC.IssueDoc_No & " AS MOI_Number, " & SFDC.WO_No & " AS MO_Number, " & SFAA.DocumentDate & " " &
        '                    "AS DocumentDate, " & SFDC.RequirementItem_No & " AS RawMaterialItemCode, " & IMAAL.Specifaction & " AS Spec, " & SFBA.Standard_issuance_quantity & " AS RequireQty" &
        '                    ", " & SFBA.Issued_Qty & " AS IssueQty, (" & SFBA.Issued_Qty & "-" & SFBA.Standard_issuance_quantity & ") AS Different, " & SFBA.Unit & " AS Unit" &
        '                    ", CASE WHEN TRUNC((" & SFBA.Issued_Qty & "-" & SFBA.Standard_issuance_quantity & ") / " & SFBA.Standard_issuance_quantity & " *100,2) < 0 THEN 0 ELSE TRUNC((" & SFBA.Issued_Qty & "-" & SFBA.Standard_issuance_quantity & ") / " & SFBA.Standard_issuance_quantity & " *100,2) end AS Overpercent " &
        '                    "FROM " & SFDC.tblMatIssueDistribution & " " &
        '                    "LEFT JOIN " & SFBA.tblManufactureOrder_Body & " On " & SFDC.RequirementItem_No & "=" & SFBA.BOM_Item & " And " & SFDC.WO_No & "=" & SFBA.MO_DocNo & " " &
        '                    "LEFT JOIN " & SFAA.tblMO & " ON " & SFDC.WO_No & "=" & SFAA.DocNo & " " &
        '                    "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFDC.RequirementItem_No & "=" & IMAAL.W_ProductItem & " " &
        '                    "WHERE " & SFDC.ent & "='3' AND " & SFAA.ProgromCode & "='3'  AND " & IMAAL.ent & "='3' AND " & SFDC.IssueDoc_No & "  LIKE 'JP5501%' " &
        '                    "AND sfaadocdt BETWEEN TO_DATE('" & MOdateFrom & "','dd/MM/yyyy')  AND  TO_DATE('" & MOdateTo & "','dd/MM/yyyy') " &
        '"" & condStr & " ORDER BY sfdcdocno"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Dim row As Integer = 0
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            str = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;Detail&nbsp;</th><th>&nbsp;Mat Issue Number&nbsp;</th><th>&nbsp;MO Number&nbsp;</th><th>&nbsp;Document Date&nbsp;</th><th>&nbsp;Raw Material&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;RequireQty&nbsp;</th><th>&nbsp;IssueQty&nbsp;</th><th>&nbsp;Different&nbsp;</th><th>&nbsp;Unit&nbsp;</th><th>&nbsp;Over(%)&nbsp;</th></tr>"
            For i = 0 To row - 1
                issueno = ds.Tables("DATASET")(i)("MOI_Number").ToString
                mono = ds.Tables("DATASET")(i)("MO_Number").ToString
                DocDate = ds.Tables("DATASET")(i)("DocumentDate").ToString
                rmItem = ds.Tables("DATASET")(i)("RawMaterialItemCode").ToString
                spec = ds.Tables("DATASET")(i)("Spec").ToString
                stdreqQty = ds.Tables("DATASET")(i)("RequireQty").ToString
                issueQty = ds.Tables("DATASET")(i)("OverallIssueQty").ToString
                different = ds.Tables("DATASET")(i)("Different").ToString
                unit = ds.Tables("DATASET")(i)("Unit").ToString
                overpercent = ds.Tables("DATASET")(i)("Overpercent").ToString
                str = str + "<tr bgcolor=#ffffff height=25><td align=center>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('MaterialsOverIssuePop.aspx?mo=" & mono & "&rm=" & rmItem & "&issueno=" & issueno & "')>Detail<a/>&nbsp;</td>" + "<td align=center>&nbsp;" & issueno & "&nbsp;</td>" + "<td align=left>&nbsp;" & mono & "&nbsp;</td>" + "<td align=center>&nbsp;" & DocDate.ToString("dd/MM/yyyy") & "&nbsp;</td>" + "<td align=left>&nbsp;" & rmItem & "&nbsp;</td>" + "<td align=left>&nbsp;" & spec & "&nbsp;</td>" + "<td align=right>&nbsp;" & stdreqQty & "&nbsp;</td>&nbsp;" + "<td align=right>" & issueQty & "&nbsp;</td>" + "<td align=right>&nbsp;" & different & "&nbsp;</td>" + "<td align=center>&nbsp;" & unit & "&nbsp;</td>" + "<td align=right>&nbsp;" & overpercent & "&nbsp;</tr>"
            Next
            str = str + "</table>"
        Else
        End If

        Return str

    End Function

    Public Function getCondition(ByVal cond As String) As String

        Dim str As String = ""

        'Version 2

        If cond = "a" Then    'More than 0% to 5%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >0 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=5"
        ElseIf cond = "b" Then 'More than 5% to 10%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >5 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=10"
        ElseIf cond = "c" Then 'More than 10% to 15%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >10 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=15"
        ElseIf cond = "d" Then 'More than 15% to 20%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >15 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=20"
        ElseIf cond = "e" Then 'More than 20% to 25%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >20 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=25"
        ElseIf cond = "f" Then 'More than 25% to 30%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >25 AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) <=30"
        ElseIf cond = "g" Then 'More than 30%
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >30"
        Else  'All Case (Over Issues)
            str = "AND ROUND((((sfba016+sfba025)-sfba023)/sfba023)*100,2) >0"
        End If

        'Version 1
        'If cond = "a" Then    'More than 0% to 5%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 0 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 5"
        'ElseIf cond = "b" Then 'More than 5% to 10%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 5 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 10"
        'ElseIf cond = "c" Then 'More than 10% to 15%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 10 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 15"
        'ElseIf cond = "d" Then 'More than 15% to 20%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 15 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 20"
        'ElseIf cond = "e" Then 'More than 20% to 25%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 20 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 25"
        'ElseIf cond = "f" Then 'More than 25% to 30%
        '    str = "AND TRUNC((sfba016-sfba023) / sfba023 *100,2) > 25 AND TRUNC((sfba016-sfba023) / sfba023 *100,2) < 30"
        'ElseIf cond = "g" Then 'More than 30%
        '    str = "AND (sfba016-sfba023) / sfba023 *100 > 30"
        'Else  'All Case (Over Issues)
        '    str = ""
        'End If
        Return str

    End Function

End Class