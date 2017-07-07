Public Class WIPStatusReportDetail
    Inherits System.Web.UI.Page

    '--------------------- Production Module - WIP Status Report -------------------
    '                          Original Code Module - JODS
    '                      Version 2 by Pattavee Narumonchavalit
    '-------------------------------------------------------------------------------


    Dim SFCB As New SFCB
    Dim SFAA As New SFAA
    Dim IMAAL As New IMAAL
    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function getherDetailofWIPStay(ByVal wc As String, ByVal f As String) As String

        Dim dispstr As String = ""
        Dim cond As String = ""
        Dim rec As Integer = 0
        Dim ds As DataSet
        Dim rowcount As Integer = 0

        Dim mo As String = ""
        Dim itemnumber As String = ""
        Dim itemname As String = ""
        Dim spec As String = ""
        Dim wipqty As String = ""
        Dim seq As String = ""
        Dim runcard As String = ""
        Dim processID As String = ""
        Dim datediff As String = ""
        Dim refTransfo As String = ""
        Dim transfdocno As String = ""

        If f = "03" Then
            cond = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))<=3"
        ElseIf f = "47" Then
            cond = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))>3 AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))<=7"
        ElseIf f = "715" Then
            cond = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))>7 AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))<=15"
        Else
            cond = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005))>15"
        End If

        'Dim sql As String = "SELECT sfcbdocno AS MODocNo,imaal001 AS ItemNumber,imaal003 AS ItemName,imaal004 AS Spec,
        '                     sfcb050 AS WIPRemainQty,sfcb002 AS Seq,sfcb003 AS ProcessID,
        '                     TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffbdocdt) FROM sffb_t WHERE sfcbdocno=sffb005)) AS DateDiff,
        '                     TO_CHAR((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005),'dd/MM/yyyy') AS RefCompleteTransferDocNo
        '                     FROM sfcb_t
        '                     LEFT JOIN sfaa_t
        '                     ON sfcbdocno=sfaadocno
        '                     LEFT JOIN imaal_t
        '                     ON sfaa010=imaal001 AND imaal002='en_US'
        '                     WHERE sfcbent='3' AND sfaaent='3' AND sfcb050>0 AND sfcb011='" & wc & "' " & cond & "
        '                     GROUP BY sfcbdocno,imaal001,imaal003,imaal004,sfcb050,sfcb002,sfcb003
        '                     ORDER BY TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005)) DESC"

        Dim sql As String = "SELECT sfcbdocno AS MODocNo,imaal001 AS ItemNumber,imaal003 AS ItemName,imaal004 AS Spec,
                              sfcb050 AS WIPRemainQty,sfcb002 AS Seq,sfcb001 AS Runcard,sfcb003 AS ProcessID,
                              TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffbdocdt) FROM sffb_t WHERE sfcbdocno=sffb005 AND sffbent='3')) AS DateDiff,
                              TO_CHAR((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005),'dd/MM/yyyy') AS RefLastCompleteTransferDate,
                              (SELECT MAX(sffbdocno) FROM sffb_t WHERE sfcbdocno=sffb005) AS RefLastCompleteTransferDocNo
                              FROM sfcb_t
                              LEFT JOIN sfaa_t
                              ON sfcbdocno=sfaadocno
                              LEFT JOIN imaal_t
                              ON sfaa010=imaal001 AND imaal002='en_US'
                              WHERE sfcbent='3' AND sfaaent='3' AND imaalent='3' AND sfcb050>0 AND sfcb011='" & wc & "' " & cond & "
                              GROUP BY sfcbdocno,imaal001,imaal003,imaal004,sfcb050,sfcb002,sfcb001,sfcb003
                              ORDER BY TRUNC(SYSDATE)-TO_DATE((SELECT MAX(sffb012) FROM sffb_t WHERE sfcbdocno=sffb005)) DESC"

        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        rowcount = ds.Tables("DATASET").Rows.Count
        dispstr = dispstr + "<table border=1 cellpadding=0 cellspacing=0><tr align=center bgcolor=#ffffff height=30><td width=200 align=right><b>&nbsp;Amount of row(s)&nbsp;</b></td><td width=150 align=righ><b>&nbsp;" & rowcount & " Rows&nbsp;</b></td></tr></table><br />"
        dispstr = dispstr + "<table border=1 cellpadding=0 cellspacing=0><tr align=center bgcolor=#FFB55D><th>&nbsp;No.&nbsp;</th><th>&nbsp;MO Number&nbsp;</th><th>&nbsp;Seq&nbsp;</th><th>&nbsp;Runcard&nbsp;</th><th>&nbsp;Process ID&nbsp;</th><th>&nbsp;Item&nbsp;</th><th>&nbsp;Item Name&nbsp;</th><th>&nbsp;Spec&nbsp;</th><th>&nbsp;WIP Qty&nbsp;</th><th>&nbsp;Date Diff&nbsp;</th><th>&nbsp;Last Transfer Date&nbsp;</th><th>&nbsp;Last Transfer DocNo&nbsp;</th></tr>"
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                mo = ds.Tables("DATASET")(i)("MODocNo").ToString
                itemnumber = ds.Tables("DATASET")(i)("ItemNumber").ToString
                itemname = ds.Tables("DATASET")(i)("ItemName").ToString
                spec = ds.Tables("DATASET")(i)("Spec").ToString
                wipqty = FormatNumber(ds.Tables("DATASET")(i)("WIPRemainQty").ToString, 0,,, TriState.True)
                seq = ds.Tables("DATASET")(i)("Seq").ToString
                runcard = ds.Tables("DATASET")(i)("Runcard").ToString
                processID = ds.Tables("DATASET")(i)("ProcessID").ToString
                datediff = ds.Tables("DATASET")(i)("DateDiff").ToString
                refTransfo = ds.Tables("DATASET")(i)("RefLastCompleteTransferDate").ToString
                transfdocno = ds.Tables("DATASET")(i)("RefLastCompleteTransferDocNo").ToString
                dispstr = dispstr + "<tr bgcolor=#ffffff><td align=right>&nbsp;" & i + 1 & "&nbsp;</td><td align=center>&nbsp;" & mo & "&nbsp;</td><td align=right>&nbsp;" & seq & "&nbsp;</td><td align=right>&nbsp;" & runcard & "&nbsp;</td><td align=center>&nbsp;" & processID & "&nbsp;</td><td align=right>&nbsp;" & itemnumber & "&nbsp;</td><td align=left>&nbsp;" & itemname & "&nbsp;</td><td align=left>&nbsp;" & spec & "&nbsp;</td><td align=right>&nbsp;" & wipqty & "&nbsp;</td><td align=right>&nbsp;" & datediff & "&nbsp;</td><td align=left>&nbsp;" & refTransfo & "&nbsp;</td><td align=left>&nbsp;" & transfdocno & "&nbsp;</td> </tr>"
            Next
            dispstr = dispstr + "</table>"
        Else
        End If

        Return dispstr

    End Function

End Class