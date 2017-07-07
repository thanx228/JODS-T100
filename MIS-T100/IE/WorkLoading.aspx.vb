Imports System.IO
Imports System.Data
Imports System.Drawing

Public Class WorkLoading
    Inherits System.Web.UI.Page


    '------------------- Production Module - Work Center Loading -------------------
    '                          Original Code Module - JODS
    '                     Version 1 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    'This Program no need any temptable to restore data
    'Declare Class

    Dim SFAA As New SFAA 'WO Header
    Dim SFCB As New SFCB 'WO Operation Step
    Dim OOCQL As New OOCQL 'Operation List Lable
    Dim ECAA As New ECAA   'WC List
    Dim clsConnect As New clsDBConnect
    Dim MOType() As String = {"5102-5102: Other NB", "5104-5104: Aero NB", "5106-5106: Auto NB", "5108-5108: FA Other", "5109-5109: FA Aero", "5192-5192: ReOther NB", "5194-5194: Re Aero NB", "5196-5196: Re Auto NB", "5198-5198: Re FA Oth", "5199-5199: Re FA Aero"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
        End If

    End Sub

    Public Function getTORecordDetail(ByVal fdate As String, ByVal tdate As String, Optional ByVal extStr As String = "") As String

        Session("sfdate") = fdate    'For WLP
        Session("stdate") = tdate    'For WLP

        Dim ds As DataSet
        Dim str As String = ""
        Dim lblfield_wc As String = "|WorkCenter|"
        lblfield_wc = lblfield_wc.Replace("|", Chr(34))
        Dim lblfield_wcname As String = "|WorkCenter Name|"
        lblfield_wcname = lblfield_wcname.Replace("|", Chr(34))
        Dim lblfield_ttitem As String = "|Total Item|"
        lblfield_ttitem = lblfield_ttitem.Replace("|", Chr(34))
        Dim lblfield_ttmo As String = "|Total MO|"
        lblfield_ttmo = lblfield_ttmo.Replace("|", Chr(34))
        Dim lblfield_ttqty As String = "|Total Qty|"
        lblfield_ttqty = lblfield_ttqty.Replace("|", Chr(34))
        Dim lblfield_mantime As String = "|Man time|"
        lblfield_mantime = lblfield_mantime.Replace("|", Chr(34))
        Dim lblfield_mctime As String = "|M/C Time|"
        lblfield_mctime = lblfield_mctime.Replace("|", Chr(34))
        Dim lblfield_mancap As String = "|Man Capacity/Day|"
        lblfield_mancap = lblfield_mancap.Replace("|", Chr(34))
        Dim lblfield_manload As String = "|Man Load|"
        lblfield_manload = lblfield_manload.Replace("|", Chr(34))
        Dim lblfield_mcload As String = "|M/C Load|"
        lblfield_mcload = lblfield_mcload.Replace("|", Chr(34))
        Dim lblfield_loadend As String = "|Load End Date|"
        lblfield_loadend = lblfield_loadend.Replace("|", Chr(34))

        Dim wc As String = ""
        Dim wcname As String = ""
        Dim ttlitem As Integer = 0
        Dim ttlmo As Integer = 0
        Dim ttlabourhour As Integer = 0
        Dim ttlmchour As Integer = 0
        'Dim ttlqty As Integer = 0
        Dim enddayofload As System.DateTime

        Dim sql As String = "Select " & SFCB.WorkStation & " As " & lblfield_wc & ", " & ECAA.Workcenter & " As " & lblfield_wcname & ", " &
        " count(" & SFCB.WONo & ") As " & lblfield_ttmo & ", nvl(sum(" & SFCB.FixedLaborHours2 & ")*60,0) As " & lblfield_mantime & ", " &
        " sum(" & SFCB.FixMachineHours & ") As " & lblfield_mctime & ", max(" & SFCB.PlannedCompletionDate & ") As " & lblfield_loadend & " " &
        " from  " & SFCB.tblMOprocessItem_SFCB & "  " &
        " Left Join " & ECAA.tblWorkcenter & " On " & SFCB.WorkStation & "=" & ECAA.WorkcenterID & " " &
        " where " & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy')" &
        " And " & SFCB.ent & " ='3' AND " & ECAA.ent & "='3' " & extStr & " Group By " & SFCB.WorkStation & "," & ECAA.Workcenter & " ORDER BY " & SFCB.WorkStation & "  "

        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        clsConnect.Close(clsConnect.T100)
        Dim row As Integer = 0
        row = ds.Tables("DATASET").Rows.Count
        If row <> 0 Then
            str = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;Detail&nbsp;</th><th>&nbsp;WorkCenter&nbsp;</th><th>&nbsp;WorkCenter Name&nbsp;</th><th>&nbsp;Total Item&nbsp;</th><th>&nbsp;Total MO&nbsp;</th><th>&nbsp;Labour Time&nbsp;</th><th>&nbsp;Machine Time&nbsp;</th><th>&nbsp;Last day of Load&nbsp;</th></tr>"
            For i = 0 To row - 1
                wc = ds.Tables("DATASET")(i)("WorkCenter").ToString
                wcname = ds.Tables("DATASET")(i)("WorkCenter Name").ToString
                ttlitem = getTotalitem(fdate, tdate, wc)
                ttlmo = getTotalMO(fdate, tdate, wc)
                'ttlitem = ds.Tables("DATASET")(i)("Total Item").ToString
                'ttlmo = ds.Tables("DATASET")(i)("Total MO").ToString
                ttlabourhour = ds.Tables("DATASET")(i)("Man time")
                ttlmchour = ds.Tables("DATASET")(i)("M/C Time")
                'ttlqty = ds.Tables("DATASET")(i)("Total Qty")
                enddayofload = ds.Tables("DATASET")(i)("Load End Date").ToString
                str = str + "<tr bgcolor=#ffffff height=25><td align=center><a href = javascript: void(0) onclick=javascript:window.open('WorkLoadingPop.aspx?wc=" & wc & "')>Detail<a/></td>" + "<td align=center>" & wc & "</td>" + "<td align=left>&nbsp;" & wcname & "&nbsp;</td>" + "<td align=right>" & FormatNumber(ttlitem, 0,,, TriState.True) & "&nbsp;</td>" + "<td align=right>" & FormatNumber(ttlmo, 0,,, TriState.True) & "&nbsp;</td>" + "<td align=right>" & FormatNumber(ttlabourhour, 0,,, TriState.True) & "&nbsp;</td>" + "<td align=right>" & FormatNumber(ttlmchour, 0,,, TriState.True) & "&nbsp;</td>" + "<td align=center>" & enddayofload.ToString("dd/MM/yyyy") & "</td></tr>"
            Next
            str = str + "</table>"
        Else
        End If

        Return str

    End Function

    Public Function createStrWC(ByVal wcstring As String) As String
        Dim statement As String = ""
        If wcstring <> "on" Then
            statement = "'" + wcstring + "'"
        Else
        End If
        Return statement
    End Function

    Private Function getTotalitem(ByVal fdate As String, ByVal tdate As String, ByVal wc As String) As Integer

        Dim ds As DataSet
        Dim row As Integer = 0
        Dim sql As String = "SELECT " & SFAA.ProductItem & " AS item FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                            "LEFT JOIN " & SFAA.tblMO & " ON " & SFCB.WONo & "=" & SFAA.DocNo & " WHERE " &
                            "" & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') " &
                            "And TO_DATE('" & tdate & "','dd/MM/yyyy') AND " & SFCB.WorkStation & "='" & wc & "' GROUP BY SFAA010"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        clsConnect.Close(clsConnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        Return row

    End Function

    Private Function getTotalMO(ByVal fdate As String, ByVal tdate As String, ByVal wc As String) As Integer

        Dim ds As DataSet
        Dim row As Integer = 0
        Dim sql As String = "SELECT DISTINCT " & SFCB.WONo & " AS item FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                            "LEFT JOIN " & SFAA.tblMO & " ON " & SFCB.WONo & "=" & SFAA.DocNo & " WHERE " &
                            "" & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') " &
                            "And TO_DATE('" & tdate & "','dd/MM/yyyy') AND " & SFCB.WorkStation & "='" & wc & "' GROUP BY " & SFCB.WONo & ""
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        clsConnect.Close(clsConnect.T100)
        row = ds.Tables("DATASET").Rows.Count
        Return row

    End Function

    'Function getDataTest() As String
    '    Dim sql1, sql2 As String
    '    sql1 = "Select  sum( sfba023)  from sfba_t where sfbadocno= sfaa_t.sfaadocno "
    '    sql2 = " Select  sfaadocno,sfaa003,(Select  count( sfba005)  from sfba_t where sfbadocno= sfaa_t.sfaadocno) As BOMitem, " &
    '     " ( " & sql1 & ") As Std_IssueQty " &
    '     "   From sfaa_t Where sfaadocno ='JP5101-20170116001'"
    '    Return sql2
    'End Function
    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Label1.Text = getDataTest()
    'End Sub

    Public Function showMOType(Optional ByVal rowsplit As Integer = 4) As String
        Dim drawstr As String = ""
        Dim x_code As String = ""
        Dim x_desc As String = ""
        Dim cutstr() As String
        Dim linecut As Integer = 1
        For Each x As String In MOType
            cutstr = Split(x, "-")
            x_code = cutstr(0)
            x_desc = cutstr(1)
            drawstr = drawstr + "<input type=checkbox name=motype value=" & x_code & "> " & x_desc & " "
            If linecut Mod rowsplit = 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
        Next
        Return drawstr
    End Function

    Public Function showparameWC(Optional ByVal rowsplit As Integer = 4, Optional mtwc As String = "") As String

        Dim drawstr As String = ""
        Dim linecut As Integer = 1
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim wccode As String = ""
        Dim wcname As String = ""

        Dim ischecked As String = ""
        Dim mtwcarr() As String = mtwc.Split(",")

        ds = ECAA.GetWorkcenter_DataSet()
        rowcount = ds.Tables(0).Rows.Count
        For i = 0 To rowcount - 1
            'wccode = ds.Tables("DATASET")(i)("WC")
            'wcname = ds.Tables("DATASET")(i)("WCNAME")
            wccode = ds.Tables(0)(i)(0)
            wcname = ds.Tables(0)(i)(1)

            If mtwc.Length <> 0 Then
                If mtwcarr.Contains(wccode) = True Then
                    ischecked = " checked"
                Else
                End If
            End If

            drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & " " & ischecked & "> " & wcname & " "
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
            ischecked = ""
        Next
        Return drawstr
    End Function

End Class