Public Class WorkCenterSumPopup
    Inherits System.Web.UI.Page

    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function ShowWCSDetail(ByVal fdate As String, ByVal tdate As String, ByVal wc As String, ByVal mol As String, ByVal doctype As String) As String

        Dim ds As DataSet
        Dim disp As String = ""

        Dim wccode As String = ""
        Dim mo As String = ""
        Dim seq As String = ""
        Dim runcard As String = ""
        Dim item As String = ""
        Dim spec As String = ""
        Dim moqty As String = ""
        Dim compqty As String = ""
        Dim scrap As String = ""
        Dim wipqty As String = ""
        Dim startDate As String = ""
        Dim LastTransdate As String = ""
        Dim PSstartdatea As String = ""
        Dim colorcode As String = ""
        Dim doctypeliststr As String = ""
        'Doctype Specification
        If doctype <> "" Then
            Dim arrdoctype() As String = doctype.Split("-")
            For i = 0 To arrdoctype.Length - 1
                If arrdoctype(i) <> "on" Or arrdoctype(i) <> "A" Then
                    If arrdoctype.Length > 1 And i = 0 Then
                        doctypeliststr = "AND substr(sfcadocno,1,6) IN ('" & arrdoctype(i) & "',"
                    ElseIf arrdoctype.Length = 1 And i = 0 Then
                        doctypeliststr = "AND substr(sfcadocno,1,6) IN ('" & arrdoctype(i) & "')"
                    ElseIf i = arrdoctype.Length - 1 And arrdoctype.Length > 0 Then
                        doctypeliststr = doctypeliststr + "'" & arrdoctype(i) & "')"
                    Else
                        doctypeliststr = doctypeliststr + "'" & arrdoctype(i) & "',"
                    End If
                End If
            Next
        End If

        Dim sql As String = "SELECT sfcb011||'-'||ecaa002 AS WorkCenter,sfcbdocno AS MODocNo,sfcb002 AS Seq,sfcb001 AS Runcard,sfaa010 AS MasterItem,imaal004 AS Spec, sfca003 AS MOQty,sfca004 AS CompleteQty,sfaa056 AS ScrapQty,sfcb050 AS WIPQty,TO_CHAR(sfaa019,'dd/MM/yyyy') AS StartDate,
                             (SELECT MAX(sffb012) AS LastTransfDate FROM sffb_t WHERE sffb005=sfcbdocno AND sffb029=sfaa010 AND sffbent='3') AS LastTransDate     
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN ecaa_t
                             ON sfcb011=ecaa001
                             LEFT JOIN imaal_t
                             ON sfaa010=imaal001 AND imaal002='en_US'
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' AND ecaaent='3' AND imaalent='3' " & doctypeliststr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0' AND sfaa019 BETWEEN TO_DATE('" & fdate & "','yyyyMMdd') AND TO_DATE('" & tdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "'
                             ORDER BY sfcadocno ASC,sfcb002 ASC"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)

        'Disp Header
        disp = "<table border=1 cellpadding=0 cellspacing=0>"
        disp = disp + "<tr align=center bgcolor=#FFBA66 height=25><th>&nbsp;Workcenter&nbsp;</th><th>&nbsp;Number of MO&nbsp;</th><th>&nbsp;From Date&nbsp;</th><th>&nbsp;To Date&nbsp;</th>"
        disp = disp + "<tr align=center bgcolor=#FFDBAF height=25><td align=center>&nbsp;" & wc & "&nbsp;</td><td align=center>&nbsp;" & mol & "&nbsp;</td><td align=center>&nbsp;" & fdate & "&nbsp;</td><td align=center>&nbsp;" & tdate & "&nbsp;</td>"
        disp = disp + "</tr></table><br /><br />"

        'Disp Data

        disp = disp + "<table border=1 cellpadding=0 cellspacing=0>"
        disp = disp + "<tr align=center bgcolor=#FFBA66 height=25><th>&nbsp;WC-Name&nbsp;</th><th>&nbsp;MO No&nbsp;</th><th>&nbsp;Seq&nbsp;</th><th>&nbsp;Runcard&nbsp;</th><th>&nbsp;Item&nbsp;</th><th>&nbsp;Spec&nbsp;</th><th>&nbsp;MO Qty&nbsp;</th><th>&nbsp;Complete Qty&nbsp;</th><th>&nbsp;Scrap Qty&nbsp;</th><th>&nbsp;WIP Qty&nbsp;</th><th>&nbsp;Start Date&nbsp;</th><th>&nbsp;Plan Date&nbsp;</th><th>&nbsp;Last Transfer (DateTransNo)&nbsp;</th>"

        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            If i Mod 2 = 0 Then
                colorcode = "#FFE8CC"
            Else
                colorcode = "#FFDBAF"
            End If
            wccode = ds.Tables("DATASET")(i)("WorkCenter")
            mo = ds.Tables("DATASET")(i)("MODocNo")
            seq = ds.Tables("DATASET")(i)("Seq")
            runcard = ds.Tables("DATASET")(i)("Runcard")
            item = ds.Tables("DATASET")(i)("MasterItem")
            spec = ds.Tables("DATASET")(i)("Spec")
            moqty = ds.Tables("DATASET")(i)("MOQty")
            compqty = ds.Tables("DATASET")(i)("CompleteQty")
            scrap = ds.Tables("DATASET")(i)("ScrapQty")
            wipqty = ds.Tables("DATASET")(i)("WIPQty")
            startDate = ds.Tables("DATASET")(i)("StartDate")
            LastTransdate = ds.Tables("DATASET")(i)("LastTransDate").ToString
            PSstartdatea = CheckDatePlanSchedule(mo, seq)
            If LastTransdate = "" Then
                LastTransdate = ""
            Else
                LastTransdate = CDate(LastTransdate).ToString("dd/MM/yyyy")
            End If
            disp = disp + "<tr align=center bgcolor=" & colorcode & " height=25><td align=right>&nbsp;" & wccode & "&nbsp;</td><td align=center>&nbsp;" & mo & "&nbsp;</td><td align=right>&nbsp;" & seq & "&nbsp;</td><td align=right>&nbsp;" & runcard & "&nbsp;</td><td align=left>&nbsp;" & item & "&nbsp;</td><td align=left>&nbsp;" & spec & "&nbsp;</td><td align=right>&nbsp;" & FormatNumber(moqty, 0,,, TriState.True) & "&nbsp;</td><td align=right>&nbsp;" & FormatNumber(compqty, 0,,, TriState.True) & "&nbsp;</td><td align=right>&nbsp;" & FormatNumber(scrap, 0,,, TriState.True) & "&nbsp;</td><td align=right>&nbsp;" & FormatNumber(wipqty, 0,,, TriState.True) & "&nbsp;</td><td align=center>&nbsp;" & startDate & "&nbsp;</td><td align=center>&nbsp;" & PSstartdatea & "&nbsp;</td><td align=center>&nbsp;" & LastTransdate & "&nbsp;</td>"
        Next
        disp = disp + "</table>"

        Return disp

    End Function

    Public Function CheckDatePlanSchedule(ByVal T100MONum As String, ByVal T100seq As String) As String

        Dim ds As DataSet
        Dim erpPlandate As String = ""
        Dim erpMOtype As String = T100MONum.Substring(2, 4)
        Dim erpMOnum As String = T100MONum.Substring(7, 11)
        Dim erpSeq As String = ""

        Dim lz As String = ""
        Dim lcount As Integer = 0
        lcount = T100seq.Length
        If lcount = 1 Then
            lz = "000"
        ElseIf lcount = 2 Then
            lz = "00"
        ElseIf lcount = 3 Then
            lz = "0"
        Else
        End If
        erpSeq = lz + T100seq

        'FYI = For your information
        'TA001 in SQL ERP is "MO Type"
        'TA002 in SQL ERP is "MO Number"
        'TA003 in SQL ERP is "MO Sequence"

        Dim sql As String = "SELECT MAX(PlanDate) AS PlanDate
                             FROM PlanSchedule WHERE TA001='" & erpMOtype & "' AND TA002='" & erpMOnum & "' AND TA003='" & erpSeq & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.MIS)
        clsconnect.Close(clsconnect.MIS)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            erpPlandate = ds.Tables("DATASET")(i)("PlanDate").ToString
        Next

        Return ERPPlandate

    End Function

End Class