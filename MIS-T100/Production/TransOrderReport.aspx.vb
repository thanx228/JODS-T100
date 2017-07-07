Imports System.IO
Imports System.Data
Imports System.Drawing

Public Class TransOrderReport
    Inherits System.Web.UI.Page

    '------------------- Production Module - Trans& MO Rec Report -------------------
    '                          Original Code Module - JODS
    '                     Version 1 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    'This Program no need any temptable to restore data

    'Declare Class

    Dim SFFB As New SFFB  'Transfer Order
    Dim SFCA As New SFCA  'Operation Flow for BOM Item
    Dim OOCQL As New OOCQL 'Operation List Lable
    Dim ECAA As New ECAA   'WC List
    Dim clsConnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
            getData()
        End If
    End Sub

    Private Sub getData()
        Dim ds As DataSet
        'Form Variable
        Dim f_workcenterID As String = "sffb009"
        Dim f_MOnum As String = "sffb005"
        Dim f_item As String = "imaal001"
        Dim f_spec As String = "imaal004"
        Dim r As Integer = 0
        Dim show As String = Request.Form("doreport")
        Dim wc_str() As String = Request.Form.GetValues("wc")
        Dim wc_num As Integer = 0
        Dim expand_str As String = ""
        Dim in_wcstr As String = ""
        Dim in_mostr As String = ""
        Dim in_itemstr As String = ""
        Dim in_specstr As String = ""
        Dim rt_str As String = ""
        Dim rectype As String = Request.Form("rectype")
        Dim fromdate As String = Request.Form("fromdate")
        Dim todate As String = Request.Form("todate")
        Dim monum As String = Request.Form("monum")
        Dim item As String = Request.Form("item")
        Dim spec As String = Request.Form("spec")
        Dim reptype As String = Request.Form("reptype")
        If show = "Show" Then
            If reptype = "0" Or reptype = "1" Then  'summary

                If wc_str Is Nothing Then
                    wc_num = 0
                Else
                    wc_num = wc_str.Length
                End If
                If wc_num <> 0 Then
                    For z = 0 To wc_num - 1
                        If z = 0 Then
                            in_wcstr = "AND " & f_workcenterID & " IN("
                            in_wcstr = in_wcstr + createStrWC(wc_str(z))
                            If wc_str(z) <> "on" Then
                                If wc_num = 1 Then
                                    in_wcstr = in_wcstr + ")"
                                Else
                                    in_wcstr = in_wcstr + ","
                                End If
                            Else
                                If wc_str(z) <> "on" Then
                                    in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                                Else
                                End If
                            End If
                        ElseIf z = wc_num - 1 Then
                            in_wcstr = in_wcstr + createStrWC(wc_str(z))
                            in_wcstr = in_wcstr + ")"
                        Else
                            in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                        End If
                    Next
                Else
                End If
                expand_str = expand_str + in_wcstr
                '--------------- get wc string END
                '--------------- get moNum
                If monum <> "" Then
                    in_mostr = " AND " & f_MOnum & " LIKE '%" & monum & "%' "
                    expand_str = expand_str + in_mostr
                Else
                End If
                '--------------- get moNum END
                '--------------- get item 
                If item <> "" Then
                    in_itemstr = " AND " & f_item & " LIKE '%" & item & "%' "
                    expand_str = expand_str + in_itemstr
                Else
                End If
                '--------------- get item END
                '--------------- get spec
                If spec <> "" Then
                    in_specstr = " AND " & f_spec & " LIKE '%" & spec & "%' "
                    expand_str = expand_str + in_specstr
                Else
                End If
                '--------------- get spec END
                '--------------- report general or rework
                If reptype = "0" Then
                    rt_str = " AND sffb006=0"
                    expand_str = expand_str + rt_str
                Else
                    rt_str = " AND sffb006<>0"
                    expand_str = expand_str + rt_str
                End If
                '--------------- report general or rework END
                ds = getTORecordSummary(fromdate, todate, expand_str)
                GridViewResult.DataSource = ds
                GridViewResult.DataBind()

            Else     'detail
                '--------------- get wc string
                If wc_str Is Nothing Then
                    wc_num = 0
                Else
                    wc_num = wc_str.Length
                End If
                If wc_num <> 0 Then
                    For z = 0 To wc_num - 1
                        If z = 0 Then
                            in_wcstr = "AND " & f_workcenterID & " IN("
                            in_wcstr = in_wcstr + createStrWC(wc_str(z))
                            If wc_str(z) <> "on" Then
                                If wc_num = 1 Then
                                    in_wcstr = in_wcstr + ")"
                                Else
                                    in_wcstr = in_wcstr + ","
                                End If
                            Else
                                If wc_str(z) <> "on" Then
                                    in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                                Else
                                End If
                            End If
                        ElseIf z = wc_num - 1 Then
                            in_wcstr = in_wcstr + createStrWC(wc_str(z))
                            in_wcstr = in_wcstr + ")"
                        Else
                            in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                        End If
                    Next
                Else
                End If
                expand_str = expand_str + in_wcstr
                '--------------- get wc string END
                '--------------- get moNum
                If monum <> "" Then
                    in_mostr = " AND " & f_MOnum & " LIKE '%" & monum & "%' "
                    expand_str = expand_str + in_mostr
                Else
                End If
                '--------------- get moNum END
                '--------------- get item 
                If item <> "" Then
                    in_itemstr = " AND " & f_item & " LIKE '%" & item & "%' "
                    expand_str = expand_str + in_itemstr
                Else
                End If
                '--------------- get item END
                '--------------- get spec
                If spec <> "" Then
                    in_specstr = " AND " & f_spec & " LIKE '%" & spec & "%' "
                    expand_str = expand_str + in_specstr
                Else
                End If
                '--------------- get spec END
                '--------------- report general or rework
                If reptype = "2" Then
                    rt_str = " AND sffb006=0"
                    expand_str = expand_str + rt_str
                Else
                    rt_str = " AND sffb006<>0"
                    expand_str = expand_str + rt_str
                End If
                '--------------- report general or rework END

                ds = getTORecordDetail(fromdate, todate, expand_str)
                'r = ds.Tables("DATASET").Rows.Count
                GridViewResult.DataSource = ds
                GridViewResult.DataBind()
            End If
        Else
        End If
    End Sub

    Public Function getTORecordSummary(ByVal fdate As String, ByVal tdate As String, Optional ByVal extStr As String = "") As DataSet

        Dim ds As DataSet
        Dim lblfield_fdocudate As String = "|From Date|"
        lblfield_fdocudate = lblfield_fdocudate.Replace("|", Chr(34))
        Dim lblfield_tdocudate As String = "|To Date|"
        lblfield_tdocudate = lblfield_tdocudate.Replace("|", Chr(34))
        Dim lblfield_wc As String = "|Workcenter|"
        lblfield_wc = lblfield_wc.Replace("|", Chr(34))
        Dim lblfield_sum As String = "|Summary Time(s) of Transfer|"
        lblfield_sum = lblfield_sum.Replace("|", Chr(34))

        'oracle command us " As identifier column use "Chr(34) as " 

        Dim sql As String = "SELECT TO_CHAR(MIN(sffbdocdt),'dd/MM/yyyy') AS " & lblfield_fdocudate & ", TO_CHAR(MAX(sffbdocdt),'dd/MM/yyyy') AS " & lblfield_tdocudate & ",sffb009 AS " & lblfield_wc & ",COUNT(sffbdocno) AS " & lblfield_sum & "
                             FROM sffb_t
                             LEFT JOIN sfaa_t
                             ON sffb005=sfaadocno
                             LEFT JOIN imaal_t
                             ON sffb029=imaal001 AND imaal002='en_US' 
                             WHERE sffbdocdt BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy')
                             AND (SELECT sfaastus FROM sfaa_t WHERE sffb005=sfaadocno AND sfaaent='3')='F'                             
                             AND sffbent='3' " & extStr & " 
                             GROUP BY sffb009
                             ORDER BY sffb009"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Return ds

    End Function

    Public Function getTORecordDetail(ByVal fdate As String, ByVal tdate As String, Optional ByVal extStr As String = "") As DataSet

        Dim ds As DataSet
        Dim lblfield_docudate As String = "|Document Date|"
        lblfield_docudate = lblfield_docudate.Replace("|", Chr(34))
        Dim lblfield_transacid As String = "|To DocNo|"
        lblfield_transacid = lblfield_transacid.Replace("|", Chr(34))
        Dim lblfield_monum As String = "|MO Number|"
        lblfield_monum = lblfield_monum.Replace("|", Chr(34))
        Dim lblfield_MasterItem As String = "|MasterItem|"
        lblfield_MasterItem = lblfield_MasterItem.Replace("|", Chr(34))
        Dim lblfield_runcard As String = "|Runcard|"
        lblfield_runcard = lblfield_runcard.Replace("|", Chr(34))
        Dim lblfield_spec As String = "|Specification|"
        lblfield_spec = lblfield_spec.Replace("|", Chr(34))
        Dim lblfield_qty As String = "|Qty|"
        lblfield_qty = lblfield_qty.Replace("|", Chr(34))
        Dim lblfield_accptqty As String = "|Accepted Qty|"
        lblfield_accptqty = lblfield_accptqty.Replace("|", Chr(34))
        Dim lblfield_scrapqty As String = "|Scrap Qty|"
        lblfield_scrapqty = lblfield_scrapqty.Replace("|", Chr(34))
        Dim lblfield_unit As String = "|Unit|"
        lblfield_unit = lblfield_unit.Replace("|", Chr(34))
        Dim lblfield_workcenter As String = "|Issuer Work Center|"
        lblfield_workcenter = lblfield_workcenter.Replace("|", Chr(34))
        Dim lblfield_receiveworkcenter As String = "|Receive Work Center|"
        lblfield_receiveworkcenter = lblfield_receiveworkcenter.Replace("|", Chr(34))
        Dim lblfield_issueOp As String = "|Issue Operation|"
        lblfield_issueOp = lblfield_issueOp.Replace("|", Chr(34))
        Dim lblfield_receiptOp As String = "|Receive Operation|"
        lblfield_receiptOp = lblfield_receiptOp.Replace("|", Chr(34))
        Dim lblfield_approvestat As String = "|Approve Status|"
        lblfield_approvestat = lblfield_approvestat.Replace("|", Chr(34))
        Dim lblfield_approver As String = "|Approver|"
        lblfield_approver = lblfield_approver.Replace("|", Chr(34))
        Dim lblfield_reporter As String = "|Reporter|"
        lblfield_reporter = lblfield_reporter.Replace("|", Chr(34))
        Dim lblfield_assgtosto As String = "|Assigned To Store|"
        lblfield_assgtosto = lblfield_assgtosto.Replace("|", Chr(34))

        'oracle command us " As identifier column use "Chr(34) as " 

        'Dim sql As String = "SELECT TO_CHAR(" & SFFB.DocumentDate & ",'dd/MM/yyyy') AS " & lblfield_docudate & ", " & SFFB.DocNo & " As " & lblfield_transacid & ", " &
        '" " & SFFB.WONo & " As " & lblfield_monum & ", " & IMAAL.Specifaction & " As " & lblfield_spec & ", " & SFFB.NoOfGoodItem & " As " & lblfield_qty & ", " &
        '" " & SFFB.NoOfGoodItem & " As " & lblfield_accptqty & ", " & SFFB.ScarpQty & " As " & lblfield_scrapqty & "," & SFFB.Unit & " As " & lblfield_unit & ", " &
        '" " & SFFB.Workstation & " As " & lblfield_workcenter & ", " & SFFB.Status & " as " & lblfield_approvestat & ", " & SFFB.WorkReportItemNo & " as " & lblfield_item & ", " &
        '" " & SFFB.OperationNo & "  As " & lblfield_operationnum & "  from  " & SFFB.tblTransferHead & "  " &
        '" Left Join " & OOCQL.tblOperation & " On " & SFFB.OperationNo & "=" & OOCQL.OperationID & " " &
        '" Left Join " & ECAA.tblWorkcenter & " On " & SFFB.Workstation & "=" & ECAA.WorkcenterID & " " &
        '" Left join " & IMAAL.tblProductionDetail & " On " & SFFB.WorkReportItemNo & "=" & IMAAL.ProductItem & " " &
        '"  where " & SFFB.DocumentDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy')" &
        '" And " & SFFB.ent & " ='3' AND " & OOCQL.ent & "='3' AND " & ECAA.ent & "='3' " + extStr

        Dim sql As String = "SELECT TO_CHAR(sffbdocdt,'dd/MM/yyyy') AS " & lblfield_docudate & ", sffbdocno AS " & lblfield_transacid & ",sffb005 AS " & lblfield_monum & ",sffb006 AS " & lblfield_runcard & ",sffb029 AS " & lblfield_MasterItem & ",imaal004 AS " & lblfield_spec & ",sffb017 As " & lblfield_qty & ",
                sffb017 AS " & lblfield_accptqty & ",sffb018  AS " & lblfield_scrapqty & ",sffb016 AS " & lblfield_unit & ",sffb002 AS " & lblfield_reporter & ",
                sffbcnfid AS " & lblfield_approver & ",
                CASE sffbstus WHEN 'Y' THEN 'Y:Approved' WHEN 'X' THEN 'X:Voided' ELSE 'N:Unapproved' END AS " & lblfield_approvestat & ",
                sffb009||'-'||ecaa002 AS " & lblfield_workcenter & ",
                CASE WHEN (SELECT sfcb011 FROM sfcb_t WHERE sfcb007=sffb007 AND sfcb008=sffb008 AND sfcb001=sffb006 AND sfcbdocno=sffb005) is null THEN 'END' ELSE (SELECT sfcb011||'-'||ecaa002 FROM sfcb_t LEFT JOIN ecaa_t ON ecaa001=sfcb011 WHERE sfcb007=sffb007 AND sfcb008=sffb008 AND sfcb001=sffb006 AND sfcbdocno=sffb005 AND ecaaent='3') END AS " & lblfield_receiveworkcenter & ",
                sffb007||'-'||oocql004 AS " & lblfield_issueOp & ",
                CASE WHEN (SELECT sfcb003 FROM sfcb_t WHERE sfcb007=sffb007 AND sfcb008=sffb008 AND sfcb001=sffb006 AND sfcbdocno=sffb005) is null THEN 'END' ELSE (SELECT sfcb003||'-'||oocql004 FROM sfcb_t LEFT JOIN oocql_t ON oocql002=sfcb003 WHERE sfcb007=sffb007 AND sfcb008=sffb008 AND sfcb001=sffb006 AND sfcbdocno=sffb005 AND oocqlent='3' AND oocql003='en_US' AND oocql001='221') END AS " & lblfield_receiptOp & ",  
                CASE WHEN (SELECT sfcb003 FROM sfcb_t WHERE sfcb007=sffb007 AND sfcb008=sffb008 AND sfcb001=sffb006 AND sfcbdocno=sffb005) is null THEN (SELECT imae041 AS assignedstore FROM imae_t WHERE sffb029=imae001 AND imaesite='JINPAO' AND imaeent=sffbent) ELSE '' END AS " & lblfield_assgtosto & "
                FROM sffb_t
                LEFT JOIN imaal_t
                ON  sffb029=imaal001 AND imaal002='en_US'
                LEFT JOIN ecaa_t
                ON sffb009=ecaa001
                LEFT JOIN oocql_t
                ON  sffb007=oocql002 AND oocql003='en_US' AND oocql001='221'
                LEFT JOIN sfaa_t
                ON sffb005=sfaadocno
                WHERE sffbdocdt BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy') " & extStr & "
                AND sffbent='3' AND imaalent='3' AND ecaaent='3' AND oocqlent='3' AND sfaaent='3' AND sfaastus='F'
                ORDER BY sffbdocdt"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        clsConnect.Close(clsConnect.T100)

        Return ds

    End Function

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'GridViewResult.AllowPaging = False
            'Me.BindGrid()

            GridViewResult.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridViewResult.HeaderRow.Cells
                cell.BackColor = GridViewResult.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridViewResult.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridViewResult.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridViewResult.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridViewResult.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()

        End Using
    End Sub

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

    Private Function createStrWC(ByVal wcstring As String) As String
        Dim statement As String = ""
        If wcstring <> "on" Then
            statement = "'" + wcstring + "'"
        Else
        End If
        Return statement
    End Function

    Public Function showselReporttype(Optional value As String = "") As String

        Dim drawstring As String = ""
        Dim v1 As String = ""
        Dim v2 As String = ""
        Dim v3 As String = ""
        Dim v4 As String = ""

        If value = 0 Then
            v1 = " selected"
        End If
        If value = 1 Then
            v2 = " selected"
        End If
        If value = 2 Then
            v3 = " selected"
        End If
        If value = 3 Then
            v3 = " selected"
        End If

        drawstring = "<option value=0 " & v1 & ">Summary(Runcard = 0)</option><option value=1" & v2 & ">Summary(Runcard Is Not 0)</option><option value=2" & v3 & ">Detail(Runcard = 0)</option><option value=3" & v4 & ">Detail(Runcard Is Not 0)</option>"

        Return drawstring

    End Function

    'Private Sub GridViewResult_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewResult.PageIndexChanging
    '    GridViewResult.PageIndex = e.NewPageIndex
    '    Call getData()
    'End Sub
End Class