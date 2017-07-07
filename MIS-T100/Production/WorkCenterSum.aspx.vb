Public Class WorkCenterSum
    Inherits System.Web.UI.Page

    '------------------- Production Module - Workcenter Sum -------------------
    '                      Original Code Module - JODS
    '                  Version 2 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------

    Dim clsconnect As New clsDBConnect
    Dim ECAA As New ECAA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function ShowDataWCSumTable(ByVal wc As String, ByVal wcstring As String, ByVal pfdate As String, ByVal ptdate As String, Optional ByVal motypestr As String = "", Optional ByVal motypeparam As String = "", Optional ByVal totalparam As String = "") As String

        Dim cellheight As String = "25"
        Dim cellwidth As String = "80"
        Dim dispdate As String = ""
        Dim ds As DataSet
        Dim daterange As Integer = 0
        Dim olddateFrom As String = pfdate
        Dim olddateTo As String = ptdate
        Dim difdate As Integer = 0
        Dim colorcode As String = ""
        Dim shdate As System.DateTime = pfdate
        Dim incretime As System.TimeSpan    'Incrementor
        incretime = New System.TimeSpan(1, 0, 0, 0)  'Add 1 Day
        olddateFrom = dateconversion(pfdate)
        olddateTo = dateconversion(ptdate)
        Dim dispwcid As String = ""
        Dim dispwcname As String = ""
        '1st thing to try, Get Date gap first (MSSQL - DBMIS - TARGET PlanSchedule TB)
        daterange = DateDiff(DateInterval.Day, CDate(pfdate), CDate(ptdate)) 'Get Date Range
        Dim arrdate(daterange) As String
        'Auto Adjustment table panel
        Dim disp As String
        If daterange < 10 Then
            disp = "<table border=1 cellpadding=0 cellspacing=0>"
        ElseIf daterange > 10 And daterange < 20 Then
            disp = "<table border=1 cellpadding=0 cellspacing=0 width=2000>"
        Else
            disp = "<table border=1 cellpadding=0 cellspacing=0 width=4000>"
        End If

        Dim sql As String
        If wc = "" Then
            sql = "SELECT ecaa001 AS Workcenter,ecaa002 AS WorkcenterName FROM ecaa_t WHERE ecaaent='3'"
        Else
            sql = "SELECT ecaa001 AS Workcenter,ecaa002 AS WorkcenterName FROM ecaa_t WHERE ecaaent='3' AND ecaa001='" & wc & "'"
        End If
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        'Now we get, wc and daterange
        '2nd thing, Draw Data to TableGrid
        Dim totalmonum As Integer = 0
        'Dim daysepartemonum As Integer = 0
        Dim daysepartemoqty As Integer = 0
        Dim mobalance As Integer = 0
        Dim mobefore As Integer = 0
        Dim moafter As String = 0
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            'Def Table ColumnFix
            disp = disp + "<tr align=center bgcolor=#FFBA66 height=" & cellheight & "><th>&nbsp;Detail&nbsp;</th><th>&nbsp;WorkcenterID&nbsp;</th><th>&nbsp;WorkcenterName&nbsp;</th><th>&nbsp;Total MO(s)&nbsp;</th><th>&nbsp;MOQtyBalance(s)&nbsp;</th><th>&nbsp;MOQtyBalance (Before)&nbsp;</th>"
            'Def Table ColumnVariable
            For colrep = 0 To daterange
                dispdate = dateconversion(CStr(shdate))
                arrdate(colrep) = dispdate     'insert date to array
                disp = disp + "<th>&nbsp; " & dispdate & " &nbsp;</th>"
                shdate = shdate.AddDays(1)
            Next
            disp = disp + "<th>&nbsp;MOQtyBalance (After)&nbsp;</th></tr>"
            'displaydata
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                If i Mod 2 = 0 Then
                    colorcode = "#FFE8CC"
                Else
                    colorcode = "#FFDBAF"
                End If
                'shdate = pfdate               'reset date
                dispwcid = ds.Tables("DATASET")(i)("Workcenter").ToString
                'dispwcid = dispwcid.Replace("W", "WC")    'Change W (Old ERP WC) to WC
                dispwcname = getWCName(dispwcid)
                totalmonum = getCountTotalMO(dispwcid, olddateFrom, olddateTo, motypestr)
                mobefore = getCountSummaryBalanceMOQty(dispwcid, olddateFrom, olddateTo, "BB", motypestr, totalparam)
                moafter = getCountSummaryBalanceMOQty(dispwcid, olddateFrom, olddateTo, "BA", motypestr, totalparam)
                mobalance = getCountSummaryBalanceMOQty(dispwcid, olddateFrom, olddateTo, motypestr, totalparam)
                disp = disp + "<tr align=center bgcolor=" & colorcode & " height=" & cellheight & "><td>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('WorkCenterSumPopup.aspx?wc=" & dispwcid & "&mow=" & totalmonum & "&fdate=" & olddateFrom & "&tdate=" & olddateTo & "&doctype=" & motypeparam & "')>Detail</a>&nbsp;</td><td align=right>&nbsp;" & dispwcid & "&nbsp;</td><td align=left>&nbsp;" & dispwcname & "&nbsp;</td><td align=right>&nbsp;" & BlindZeroValue(totalmonum) & "&nbsp;</td><td align=right>&nbsp;" & BlindZeroValue(mobalance) & "&nbsp;</td><td align=right>&nbsp;" & BlindZeroValue(mobefore) & "&nbsp;</td>"
                For colrep = 0 To daterange
                    daysepartemoqty = getCountSeparateByDayMOQty(dispwcid, arrdate(colrep), motypestr, totalparam)     'get TotalMOQty Perday
                    'daysepartemonum = getCountSeparateByDayMO(dispwcid, arrdate(colrep))       'get TotalMO Perday
                    disp = disp + "<td align=right>&nbsp; " & BlindZeroValue(daysepartemoqty) & " &nbsp;</td>"
                Next
                disp = disp + "<td align=right>&nbsp;" & BlindZeroValue(moafter) & "&nbsp;</td></tr>"
            Next
            disp = disp + "</table>"
        Else
        End If
        Return disp

    End Function

    Private Function BlindZeroValue(ByVal value As Integer) As String

        Dim datavalue As String = ""
        If value = 0 Then
            datavalue = ""
        Else
            datavalue = FormatNumber(value, 0,,, TriState.True)
        End If

        Return datavalue

    End Function

    Private Function getCountSummaryBalanceMOQty(ByVal wc As String, ByVal fdate As String, ByVal tdate As String, Optional ByVal QtyType As String = "", Optional ByVal motypestr As String = "", Optional ByVal totalparam As String = "") As Integer

        Dim ds As DataSet
        Dim cumu As Integer = 0
        Dim result As Integer = 0
        Dim execsql As String = ""

        If QtyType = "BB" Then   'MOQtyBalanceBefore
            execsql = "SELECT SUM(DISTINCT sfca003) AS MOQty
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0'  AND sfaa019 < TO_DATE('" & fdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "' " & totalparam & " AND imaalent='3' AND imaal002='en_US'
                             GROUP BY sfcb011,sfcadocno"
        ElseIf QtyType = "BA" Then  'MOQtyBalanceAfter
            execsql = "SELECT SUM(DISTINCT sfca003) AS MOQty
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & " AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0'  AND sfaa019 > TO_DATE('" & tdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "' " & totalparam & " AND imaalent='3' AND imaal002='en_US'
                             GROUP BY sfcb011,sfcadocno"
        Else     'MOQtyBalanceAll
            execsql = "SELECT SUM(DISTINCT sfca003) AS MOQty
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0'
                             AND sfcb011='" & wc & "' " & totalparam & " AND imaalent='3' AND imaal002='en_US'
                             GROUP BY sfcb011,sfcadocno"
        End If

        ds = clsconnect.QueryDataSet(execsql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                cumu = CInt(ds.Tables("DATASET")(i)("MOQty"))
                result = result + cumu
            Next
        Else
        End If
        Return result

    End Function

    Private Function getCountSeparateByDayMOQty(ByVal wc As String, ByVal vdate As String, Optional ByVal motypestr As String = "", Optional ByVal totalparam As String = "") As Integer

        Dim ds As DataSet
        Dim cumu As Integer = 0
        Dim result As Integer = 0

        Dim sql As String = "SELECT SUM(DISTINCT sfca003) AS MOQty
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0'  AND sfaa019 BETWEEN TO_DATE('" & vdate & "','yyyyMMdd') AND TO_DATE('" & vdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "' " & totalparam & " AND imaalent='3' AND imaal002='en_US'
                             GROUP BY sfcb011,sfcadocno"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                cumu = CInt(ds.Tables("DATASET")(i)("MOQty"))
                result = result + cumu
            Next
        Else
        End If
        Return result

    End Function

    Private Function getCountSeparateByDayMO(ByVal wc As String, ByVal vdate As String, Optional ByVal motypestr As String = "", Optional ByVal totalparam As String = "") As Integer

        'AND substr(sfcadocno,1,6) IN ('JP5102')

        Dim ds As DataSet
        Dim result As Integer = 0
        Dim sql As String = "SELECT COUNT(DISTINCT sfcadocno) AS TotalWorkMO
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0' AND sfaa019 BETWEEN TO_DATE('" & vdate & "','yyyyMMdd') AND TO_DATE('" & vdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "' " & totalparam & " AND imaalent='3' AND imaal002='en_US'
                             ORDER BY sfcadocno"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)

        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                result = CInt(ds.Tables("DATASET")(i)("TotalWorkMO").ToString)
            Next
        Else
        End If

        Return result

    End Function

    Private Function getCountTotalMO(ByVal wc As String, ByVal fdate As String, ByVal tdate As String, Optional ByVal motypestr As String = "", Optional ByVal totalparam As String = "") As Integer

        Dim ds As DataSet
        Dim result As Integer = 0
        Dim sql As String = "SELECT COUNT(DISTINCT sfcadocno) AS TotalWorkMO
                             FROM sfca_t
                             LEFT JOIN sfaa_t
                             ON sfcadocno=sfaadocno
                             LEFT JOIN sfcb_t
                             ON sfcadocno=sfcbdocno AND sfca001=sfcb001
                             LEFT JOIN imaal_t 
                             ON imaal001=sfaa010
                             WHERE sfcaent='3' AND sfaaent='3' AND sfcbent='3' " & motypestr & "  AND sfaastus='F'
                             AND sfca004<sfca003 AND sfca001='0' AND sfaa019 BETWEEN TO_DATE('" & fdate & "','yyyyMMdd') AND TO_DATE('" & tdate & "','yyyyMMdd')
                             AND sfcb011='" & wc & "' AND imaalent='3' AND imaal002='en_US' " & totalparam & "
                             ORDER BY sfcadocno"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)

        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                result = CInt(ds.Tables("DATASET")(i)("TotalWorkMO").ToString)
            Next

        Else
        End If

        Return result

    End Function

    Private Function getWCName(ByVal wc As String) As String

        Dim ds As DataSet
        Dim wcname As String = ""
        Dim sql As String = "SELECT " & ECAA.Workcenter & " AS WorkcenterName FROM " & ECAA.tblWorkcenter & " " &
                            "WHERE " & ECAA.WorkcenterID & "='" & wc & "' AND " & ECAA.ent & "='3' "
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            wcname = ds.Tables("DATASET")(i)("WorkcenterName").ToString
        Next

        Return wcname

    End Function

    Private Function dateconversion(ByVal vdate As String) As String

        Dim newdatefmt As String = ""
        Dim tmpDate() As String = vdate.Split("/")
        Dim dd As String = tmpDate(0)
        Dim mm As String = tmpDate(1)
        Dim yyyy As String = tmpDate(2)
        newdatefmt = yyyy + mm + dd
        Return newdatefmt

    End Function

    Public Function ShowWCS() As String

        Dim str As String = ""
        Dim fromdate As Date
        Dim todate As Date
        Dim ddif As Integer
        fromdate = Request.Form("fromdate")
        todate = Request.Form("todate")
        ddif = DateDiff(DateInterval.Day, fromdate, todate)
        str = CStr(ddif)

        Return str

    End Function

    Public Function showparameWCType_sel() As String

        Dim ds As DataSet
        Dim drawstr As String = "<Select name=wc><option value=>All</option>"
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim sql As String = "Select " & ECAA.WorkcenterID & " As WorkcenterID, " & ECAA.Workcenter & " As WCDescription " &
                            "FROM " & ECAA.tblWorkcenter & " WHERE " & ECAA.ent & "='3'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            scode = ds.Tables("DATASET")(i)("WorkcenterID")
            scodename = ds.Tables("DATASET")(i)("WCDescription")
            drawstr = drawstr + "<option value=" & scode & "> " & scode & "-" & scodename & "</option>"
        Next
        drawstr = drawstr + "</select>"
        Return drawstr

    End Function

    Public Function getDocType(Optional ByVal rowsplit As Integer = 3) As String

        Dim str As String = ""
        Dim row As Integer = 0
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim docTypeCode As String = ""
        Dim docTypeName As String = ""
        Dim linecut As Integer = 1
        Dim prefixStr As String = "JP"
        Dim connector As String = "||"
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
                str = str + "<input type=checkbox name=motype value=JP" & docTypeCode & "> " & docTypeName & " "
                If i Mod rowsplit = 0 And i <> 0 Then
                    str = str + "<br />"
                Else
                End If
                linecut = linecut + 1
            Next
        End If

        Return str

    End Function

    Public Function createStrMotype(ByVal mostring As String) As String
        Dim statement As String = ""
        If mostring <> "on" Then
            statement = "'" + mostring + "'"
        Else
        End If
        Return statement
    End Function

    'Public Function showparameWC(Optional ByVal rowsplit As Integer = 4) As String

    '    Dim drawstr As String = ""
    '    Dim linecut As Integer = 1
    '    Dim rowcount As Integer = 0
    '    Dim ds As DataSet
    '    Dim wccode As String = ""
    '    Dim wcname As String = ""
    '    ds = ECAA.GetWorkcenter_DataSet()
    '    rowcount = ds.Tables(0).Rows.Count
    '    For i = 0 To rowcount - 1
    '        'wccode = ds.Tables("DATASET")(i)("WC")
    '        'wcname = ds.Tables("DATASET")(i)("WCNAME")
    '        wccode = ds.Tables(0)(i)(0)
    '        wcname = ds.Tables(0)(i)(1)
    '        drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & "> " & wcname & " "
    '        If i Mod rowsplit = 0 And i <> 0 Then
    '            drawstr = drawstr + "<br />"
    '        Else
    '        End If
    '        linecut = linecut + 1
    '    Next
    '    Return drawstr
    'End Function



End Class