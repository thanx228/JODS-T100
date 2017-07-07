Public Class MaterialsPrepareStatus
    Inherits System.Web.UI.Page

    '--------------------- Production Module - Mat Prepare Status -------------------
    '                          Original Code Module - JODS
    '                       Version 2 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    Dim ECAA As New ECAA     'WC List
    Dim INAG As New INAG     'Stock Inventory Detail
    Dim SFBA As New SFBA     'Material Body
    Dim IMAAL As New IMAAL   'Item Master Table
    Dim SFCB As New SFCB     'WO Process Line (MO Operation)
    Dim OOBXL As New OOBXL   'Document Type
    Dim clsconnect As New clsDBConnect
    Dim SpWC() As String = {"WC04-WC04 : Spot Welding", "WC05-WC05 : Reprocess", "WC09-WC09 : Aerospace", "WC12-WC12 : Stamping", "WC13-WC13 : Assembly", "WC14-WC14 : Welding", "WC15-WC15 : Welding (Robot)", "WC56-WC56 : AMP Welding"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
        End If

    End Sub

    Public Function getWIPDataProcess(ByVal planstartdate As String, ByVal plantodate As String, ByVal wc As String, ByVal cond As String, Optional ByVal ext As String = "") As String

        Dim str = ""
        Dim ds As DataSet
        Dim MOnum As String = ""
        Dim OpId As String = ""
        Dim OpDesc As String = ""
        Dim PlanCmptDate As System.DateTime
        Dim RMitem As String = ""
        Dim RMSpec As String = ""
        Dim ReqQty As String = ""
        Dim IssueQty As String = ""
        Dim NotIssue As String = ""
        Dim StockQty As String = ""
        Dim Unit As String = ""
        Dim row As Integer = 0
        Dim plnschDate As String = ""
        Dim WebDateFormate As String = ""
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        Dim extCond As String = ""

        'Checkcondition
        If cond = "2" Then     'Not Issue
            extCond = "AND (sfba016+sfba025)=0"
        ElseIf cond = "3" Then  'Issue < Require
            extCond = "AND (sfba023-sfba016)<sfba023 AND (sfba023-sfba016)>0"
        Else     'None of above
        End If

        Dim sql As String = "SELECT " & SFCB.WorkStation & " AS Workcenter," & SFCB.WONo & " AS MONumber," & SFCB.OperationID & " AS OperationID," & OOCQL.Operation & " AS OperationDescription," &
                             "" & SFCB.PlannedCompletionDate & " AS PlanCompleteDate," & SFBA.IssueItem & " AS RawMatItem," & IMAAL.Specifaction & " AS Spec," & SFBA.StandardIssuanceQuantity & " AS ReqQty," &
                             "" & SFBA.IssuedQty & " AS IssueQty,(" & SFBA.StandardIssuanceQuantity & "-" & SFBA.IssuedQty & ") AS NotIssue," &
                             "" & SFBA.Unit & " AS Unit,ROUND((SELECT SUM(" & INAG.ActualStock & ") FROM " & INAG.tblInventoryDeatil & " WHERE " & IMAAL.ProductItem & "=" & INAG.ItemNo & "),2) AS StockQty " &
                             "FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                             "Left JOIN " & SFBA.tblManufactureOrder_Body & " " &
                             "ON " & SFCB.WONo & "=" & SFBA.MODocNo & " " &
                             "LEFT JOIN " & OOCQL.tblOperation & " " &
                             "ON " & SFCB.OperationID & "=" & OOCQL.OperationID & " " &
                             "LEFT JOIN " & IMAAL.tblProductionDetail & " " &
                             "ON " & SFBA.IssueItem & "=" & IMAAL.ProductItem & " " &
                             "WHERE " &
                             "" & SFCB.ent & "='3' AND " & OOCQL.ent & "='3' AND " & OOCQL.IssueSite & "='221' AND " & OOCQL.Language & "='en_US' " &
                             "AND " & IMAAL.ent & "='3' " &
                             "AND " & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & planstartdate & "','dd/MM/yyyy') AND TO_DATE('" & plantodate & "','dd/MM/yyyy') " &
                             "" & ext & " " &
                             "AND " & SFCB.WorkStation & " ='" & wc & "' " & extCond & " " &
                             "ORDER BY " & SFCB.WONo & "," & SFCB.LineNo & ""
        '"AND " & SFCB.WO_No & " LIKE 'JP5102%' " 
        '"" & ext & " " &

        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            str = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;Detail&nbsp;</th><th>&nbsp;WorkCenter&nbsp;</th><th>&nbsp;MO&nbsp;</th><th>&nbsp;Process&nbsp;</th><th>&nbsp;ProcessName&nbsp;</th><th>&nbsp;PlanCompletionDate&nbsp;</th><th>&nbsp;RM Item&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;ReqQty&nbsp;</th><th>&nbsp;IssueQty&nbsp;</th><th>&nbsp;NotIssueQty&nbsp;</th><th>&nbsp;StockQty&nbsp;</th><th>&nbsp;Unit&nbsp;</th><th>&nbsp;DailyPlanDate&nbsp;</th></tr>"
            For i = 0 To row - 1
                wc = ds.Tables("DATASET")(i)("WorkCenter").ToString
                MOnum = ds.Tables("DATASET")(i)("MONumber").ToString
                OpId = ds.Tables("DATASET")(i)("OperationID").ToString
                OpDesc = ds.Tables("DATASET")(i)("OperationDescription").ToString
                PlanCmptDate = ds.Tables("DATASET")(i)("PlanCompleteDate").ToString
                RMitem = ds.Tables("DATASET")(i)("RawMatItem").ToString
                RMSpec = ds.Tables("DATASET")(i)("Spec").ToString
                ReqQty = ds.Tables("DATASET")(i)("ReqQty").ToString
                IssueQty = ds.Tables("DATASET")(i)("IssueQty").ToString
                NotIssue = ds.Tables("DATASET")(i)("NotIssue").ToString
                Unit = ds.Tables("DATASET")(i)("Unit").ToString
                StockQty = ds.Tables("DATASET")(i)("StockQty").ToString
                plnschDate = getPlanScheduleLastDate(MOnum)
                If plnschDate <> "" Then
                    d = plnschDate.Substring(6, 2)
                    m = plnschDate.Substring(4, 2)
                    y = plnschDate.Substring(0, 4)
                    WebDateFormate = d + "/" + m + "/" + y
                Else
                    WebDateFormate = ""
                End If
                '20170301

                str = str + "<tr bgcolor=#ffffff height=25><td align=center><a href = javascript: void(0) onclick=javascript:window.open('PlanScheduleAddPop.aspx?mo=" & MOnum & "')>Detail<a/></td>" + "<td align=center>" & wc & "</td>" + "<td align=left>&nbsp;" & MOnum & "&nbsp;</td>" + "<td align=right>" & OpId & "&nbsp;</td>" + "<td align=right>" & OpDesc & "&nbsp;</td>" + "<td align=right>" & PlanCmptDate.ToString("dd/MM/yyyy") & "&nbsp;</td>" + "<td align=right>&nbsp;" & RMitem & "&nbsp;</td>" + "<td align=left>&nbsp;" & RMSpec & "&nbsp;</td>" + "<td align=right>&nbsp;" & ReqQty & "&nbsp;</td>" + "<td align=right>&nbsp;" & IssueQty & "&nbsp;</td>" + "<td align=right>&nbsp;" & NotIssue & "&nbsp;</td>" + "<td align=right>&nbsp;" & StockQty & "&nbsp;</td>" + "<td align=center>&nbsp;" & Unit & "&nbsp;</td>" + "<td align=center>&nbsp;" & WebDateFormate & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        Else
        End If

        Return str

    End Function

    Public Function getPlanScheduleLastDate(ByVal MO As String) As String

        Dim planscheDate As String = ""
        Dim i As Integer = 0
        Dim numrow As Integer = 0
        Dim MISsql As String = "SELECT TOP 1 PlanDate FROM PlanSchedule WHERE MO_T100 LIKE '%" & MO & "%' ORDER BY PlanDate DESC"
        Dim ds As DataSet
        ds = clsconnect.QueryDataSet(MISsql, clsconnect.MIS)
        clsconnect.Close(clsconnect.MIS)
        If ds.Tables("DATASET").Rows.Count = 0 Then
            planscheDate = ""
        Else
            planscheDate = ds.Tables("DATASET")(i)("PlanDate").ToString
        End If

        Return planscheDate

    End Function

    Public Function getDocType(Optional ByVal rowsplit As Integer = 3, Optional ByVal mtdoctype As String = "") As String

        Dim str As String = ""
        Dim row As Integer = 0
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim docTypeCode As String = ""
        Dim docTypeName As String = ""
        Dim linecut As Integer = 1
        Dim prefixStr As String = "JP"
        Dim connector As String = "||"

        Dim ischecked As String = ""
        Dim mtdoctypearr() As String = mtdoctype.Split(",")

        'Dim sql As String = "SELECT '" & prefixStr & "'" & connector & "" & OOBXL.DocType_Id & "" & connector & "' : '||" & OOBXL.DocType & " AS Doctype," & OOBXL.DocType & " AS DocDescription " &
        '"FROM " & OOBXL.tblDocType & "  WHERE " & OOBXL.ent & "='3' AND (" & OOBXL.DocType_Id & " LIKE '51%' OR " & OOBXL.DocType_Id & " LIKE '52%');"
        Dim sql As String = "SELECT " & OOBXL.DocTypeId & " AS Doctype, '" & prefixStr & "'" & connector & "" & OOBXL.DocTypeId & "" & connector & "' : '||" & OOBXL.DocType & " AS DocDescription " &
                            "FROM " & OOBXL.tblDocType & "  WHERE " & OOBXL.ent & "='3' AND (" & OOBXL.DocTypeId & " LIKE '51%' OR " & OOBXL.DocTypeId & " LIKE '52%')"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        rowcount = ds.Tables("DATASET").Rows.Count
        If rowcount <> 0 Then
            For i = 0 To rowcount - 1
                docTypeCode = ds.Tables("DATASET")(i)("Doctype")
                docTypeName = ds.Tables("DATASET")(i)("DocDescription")

                If mtdoctypearr.Length <> 0 Then
                    If mtdoctypearr.Contains(docTypeCode) = True Then
                        ischecked = " checked"
                    Else
                    End If
                End If

                str = str + "<input type=checkbox name=doctype value=" & docTypeCode & " " & ischecked & "> " & docTypeName & " "
                If i Mod rowsplit = 0 And i <> 0 Then
                    str = str + "<br />"
                Else
                End If
                linecut = linecut + 1
                ischecked = ""
            Next
        End If

        Return str

    End Function

    Public Function createStrDoctype(ByVal word As String) As String
        Dim statement As String = ""
        If word <> "on" Then
            statement = "'JP" + word + "'"
        Else
        End If
        Return statement
    End Function

    Public Function CheckstatusRegenerate(ByVal value As String) As String

        Dim drawstring As String = ""
        Dim v1 As String = ""
        Dim v2 As String = ""
        Dim v3 As String = ""

        If value = 1 Then
            v1 = " selected"
        End If
        If value = 2 Then
            v2 = " selected"
        End If
        If value = 3 Then
            v3 = " selected"
        End If

        drawstring = "<option value=0 " & v1 & ">Summary (Runcard = 0)</option><option value=1" & v2 & ">Summary (Runcard is not 0)</option><option value=2" & v3 & ">Detail</option>"

        Return drawstring

    End Function

End Class