Public Class WIPStatusReport
    Inherits System.Web.UI.Page

    '---------------- Production Module - WIP Status Report ----------------
    '                      Original Code Module - JODS
    '                 Version 3 by Pattavee Narumonchavalit
    '-----------------------------------------------------------------------

    Dim ECAA As New ECAA
    Dim SFCB As New SFCB
    Dim SFFB As New SFFB
    Dim SFDB As New SFDB
    Dim SFDA As New SFDA

    Dim clsConnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
        End If
    End Sub

    Public Function doGenerateReport(ByVal ext As String) As String

        Dim str As String = ""
        Dim wcds As DataSet
        Dim chkconds As DataSet
        Dim wcrow As Integer = 0
        Dim chkrow As Integer = 0
        Dim wccode As String = ""
        Dim wcname As String = ""
        Dim colorcode As String = ""
        Dim chkNulldateSQL As String = ""
        Dim moDocNo As String = ""
        Dim TODate As String = ""
        Dim PostDate As Date
        Dim TodayDate As Date = Date.Now
        Dim MIPostdate As String = ""
        'Initiate for count
        Dim zt_init As Integer = 0
        Dim fs_init As Integer = 0
        Dim efi_init As Integer = 0
        Dim tthir_init As Integer = 0
        Dim ErrorMO As Integer = 0

        Dim WCsql As String = "SELECT " & ECAA.WorkcenterID & " as WorkcenterID, " & ECAA.Workcenter & " as WorkcenterName FROM " & ECAA.tblWorkcenter & " WHERE " & ECAA.ent & "='3' " & ext & " "
        wcds = clsConnect.QueryDataSet(WCsql, clsConnect.T100)
        clsConnect.Close(clsConnect.T100)
        wcrow = wcds.Tables("DATASET").Rows.Count
        If wcrow <> 0 Then
            str = str + "<table border=1><tr align=center bgcolor=#FFB55D><th>&nbsp;WorkcenterID&nbsp;</th><th>&nbsp;WorkcenterName&nbsp;</th><th>&nbsp;0-3 Days&nbsp;</th><th>&nbsp;4-7 Days&nbsp;</th><th>&nbsp;8-15 Days&nbsp;</th><th>&nbsp;More Than 15 Days&nbsp;</th><th>&nbsp;Error MO&nbsp;</th></tr>"
            For i = 0 To wcrow - 1
                If i Mod 2 = 0 Then
                    colorcode = "#FFFFFF"
                Else
                    colorcode = "#FFDBAF"
                End If
                wccode = wcds.Tables("DATASET")(i)("WorkcenterID").ToString
                wcname = wcds.Tables("DATASET")(i)("WorkcenterName").ToString
                'Null date Record for TO will be conducted for Mat'l Issue
                chkNulldateSQL = "SELECT " & SFCB.WONo & " AS MODocNO,(SELECT MAX(" & SFFB.DocumentDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & ") AS tranferDatelatest, TRUNC(SYSDATE)-TO_DATE((SELECT MAX(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & ")) AS DIFF FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.WIP & " >0 AND " & SFCB.ent & "='3' AND " & SFCB.WorkStation & "='" & wccode & "'"
                chkconds = clsConnect.QueryDataSet(chkNulldateSQL, clsConnect.T100)
                clsConnect.Close(clsConnect.T100)
                chkrow = chkconds.Tables("DATASET").Rows.Count
                If chkrow <> 0 Then
                    For j = 0 To chkrow - 1
                        moDocNo = chkconds.Tables("DATASET")(j)("MODocNO").ToString
                        TODate = chkconds.Tables("DATASET")(j)("tranferDatelatest").ToString
                        If TODate = "" Then
                            MIPostdate = getMIPostDate(moDocNo)
                            If MIPostdate = "" Then    ' Considered as "Old Data" - Must be "Eliminate"
                                ErrorMO = ErrorMO + 1  ' Insert Error/Old MO
                            Else   'Consult On
                                PostDate = MIPostdate
                                Dim ddif As Integer = DateDiff(DateInterval.Day, TodayDate, PostDate)
                                If ddif <= 3 Then
                                    zt_init = zt_init + 1         'Plus MO with Material Issues
                                ElseIf ddif > 3 And ddif <= 7 Then
                                    fs_init = fs_init + 1
                                ElseIf ddif > 7 And ddif <= 15 Then
                                    efi_init = efi_init + 1
                                ElseIf ddif > 15 Then
                                    tthir_init = tthir_init + 1
                                End If
                            End If
                        Else
                        End If
                    Next
                Else
                End If

                Dim a As Integer = getStayLengthday("0to3", wccode)
                Dim b As Integer = getStayLengthday("4to7", wccode)
                Dim c As Integer = getStayLengthday("7to15", wccode)
                Dim d As Integer = getStayLengthday("over15", wccode)
                Dim result03 As String = FormatNumber(a, 0, , , TriState.True)
                Dim result47 As String = FormatNumber(b, 0, , , TriState.True)
                Dim result715 As String = FormatNumber(c, 0, , , TriState.True)
                Dim result15m As String = FormatNumber(d, 0, , , TriState.True)


                Dim openlink03 As String = "<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=03')>" & result03 & "</a>"
                Dim openlink47 As String = "<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=47')>" & result47 & "</a>"
                Dim openlink715 As String = "<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=715')>" & result715 & "</a>"
                Dim openlink15m As String = "<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=15m')>" & result15m & "</a>"
                Dim errormodisp As String = FormatNumber(CStr(ErrorMO), 0,,, TriState.True)
                ''Determine to send the link if got error data

                If result03 = 0 Then
                    openlink03 = ""
                End If
                If result47 = 0 Then
                    openlink47 = ""
                End If
                If result715 = 0 Then
                    openlink715 = ""
                End If
                If result15m = 0 Then
                    openlink15m = ""
                End If
                If ErrorMO = 0 Then
                    errormodisp = ""
                End If

                'str = str + "<tr bgcolor=" & colorcode & "><td align=center>&nbsp;" & wccode & "&nbsp;</td><td align=left>&nbsp;" & wcname & "&nbsp;</td><td align=right>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=03')>" & getStayLengthday("0to3", wccode) + zt_init & "</a>&nbsp;</td><td align=right>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=47')>" & getStayLengthday("4to7", wccode) + fs_init & "</a>&nbsp;</td><td align=right>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=715')>" & getStayLengthday("7to15", wccode) + efi_init & "</a>&nbsp;</td><td align=right>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('WIPStatusReportDetail.aspx?wc=" & wccode & "&f=15m')>" & getStayLengthday("over15", wccode) + tthir_init & "</a>&nbsp;</td><td align=right>&nbsp;" & ErrorMO & "&nbsp;</td></tr>"
                str = str + "<tr bgcolor=" & colorcode & "><td align=center>&nbsp;" & wccode & "&nbsp;</td><td align=left>&nbsp;" & wcname & "&nbsp;</td><td align=right>&nbsp;" & openlink03 & "&nbsp;</td><td align=right>&nbsp;" & openlink47 & "&nbsp;</td><td align=right>&nbsp;" & openlink715 & "&nbsp;</td><td align=right>&nbsp;" & openlink15m & "&nbsp;</td><td align=right>&nbsp;" & errormodisp & "&nbsp;</td></tr>"
                zt_init = 0
                fs_init = 0
                efi_init = 0
                tthir_init = 0
                ErrorMO = 0
            Next
            str = str + "</table>"
        End If
        Return str

    End Function

    Public Function getStayLengthday(ByVal condition As String, ByVal wc As String) As Integer

        Dim extStr As String = ""
        Dim ds As DataSet
        Dim baseSQLstr As String = ""
        Dim i As Integer = 0
        Dim rec As Integer = 0

        'Check totall record in WIP
        baseSQLstr = "SELECT COUNT(*) AS totalMO FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.WIP & " >0 AND " & SFCB.ent & "='3' AND " & SFCB.WorkStation & "='" & wc & "' "
        If condition = "0to3" Then
            extStr = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))<=3"
        ElseIf condition = "4to7" Then
            extStr = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))>3 AND TRUNC(SYSDATE)-TO_DATE((select max(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))<=7"
        ElseIf condition = "7to15" Then
            extStr = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))>7 AND TRUNC(SYSDATE)-TO_DATE((select max(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))<=15"
        ElseIf condition = "over15" Then
            extStr = "AND TRUNC(SYSDATE)-TO_DATE((SELECT MAX(" & SFFB.CompleteDate & ") FROM " & SFFB.tblTransferHead & " WHERE " & SFCB.WONo & "=" & SFFB.WONo & "))>15"
        Else
        End If
        baseSQLstr = baseSQLstr + extStr
        ds = clsConnect.QueryDataSet(baseSQLstr, clsConnect.T100)
        rec = ds.Tables("DATASET")(i)("totalMO")
        clsConnect.Close(clsConnect.T100)

        Return rec

    End Function

    Public Function getMIPostDate(ByVal MO As String) As String           'Get Material Issue Post Date

        Dim pdate As String = ""
        Dim ds As DataSet
        Dim i As Integer = 0
        Dim row As Integer = 0
        Dim baseSQLstr As String = "SELECT " & SFDA.PostingDate & " AS MIPostDate FROM " & SFDB.tblMatIssueSet & " LEFT JOIN " & SFDA.tblMatIssueHead & " ON " & SFDB.IssueDocNo & "=" & SFDA.IssueDocNo & " WHERE " & SFDB.ent & "='3' AND " & SFDA.ent & "=3 AND " & SFDB.IssueDocNo & "='" & MO & "' AND " & SFDA.Status & "='S' AND ROWNUM=1"
        ds = clsConnect.QueryDataSet(baseSQLstr, clsConnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            pdate = ds.Tables("DATASET")(i)("MIPostDate").ToString
        Else
            pdate = ""
        End If
        clsConnect.Close(clsConnect.T100)

        Return pdate

    End Function

    Public Function showparameWC(Optional ByVal rowsplit As Integer = 4) As String

        Dim drawstr As String = ""
        Dim linecut As Integer = 1
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim wccode As String = ""
        Dim wcname As String = ""
        ds = ECAA.GetWorkcenter_DataSet()
        rowcount = ds.Tables(0).Rows.Count
        For i = 0 To rowcount - 1
            'wccode = ds.Tables("DATASET")(i)("WC")
            'wcname = ds.Tables("DATASET")(i)("WCNAME")
            wccode = ds.Tables(0)(i)(0)
            wcname = ds.Tables(0)(i)(1)
            drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & "> " & wcname & " "
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
        Next
        Return drawstr

    End Function

    Public Function createStrWC(ByVal wcstring As String) As String
        Dim statement As String = ""
        If wcstring <> "on" Then
            statement = "'" + wcstring + "'"
        Else
        End If
        Return statement
    End Function

End Class