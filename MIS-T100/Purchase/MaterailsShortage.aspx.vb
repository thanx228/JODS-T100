Imports System.Drawing
Imports System.Globalization

Public Class MaterailsShortage
    Inherits System.Web.UI.Page
    Dim CreateTempTable As New T100CreateTempTable
    Dim Conn_SQL As New ConnSQL
    Dim clsDBConnect As New clsDBConnect
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("../LoginT100.aspx")
        End If
        btExport.Visible = False
        HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
    End Sub

    Function valPower(ByRef cbl As CheckBoxList) As String

        Dim val As String = ""
        For Each box As ListItem In cbl.Items
            Dim boxVal As String = CStr(box.Value.Trim)
            If box.Selected Then
                If box.Text.Trim <> "" Then
                    val &= "'" & boxVal & "',"
                ElseIf box.Text.Trim = "" Then
                    val &= ""
                End If
            End If
        Next
        If val <> "" Then
            Return val.Substring(0, val.Length - 1)
        Else
            Return val.Substring(0, val.Length)
        End If

    End Function

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        'Chk WH
        Dim ValueChkType As String = "", SuplyProduct As Integer = 0, SuplyOther As Integer = 0
        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "5" Then 'Suply Product
                    SuplyProduct = 5
                ElseIf boxVal = "6" Then 'Suply Other
                    SuplyOther = 2
                End If
            End If
        Next

        gvShow.Visible = True
        gvPoCallIn.Visible = False
        gvPlanCallIn.Visible = False

        Dim tempTable As String = "tempMaterialsShortage" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)

        Dim SQL As String = "", WHR As String = "", ISQL As String = "", USQL As String = ""
        Dim TempProgram As New DataTable,
            CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            EndDate As String = tbDateTo.Text.Trim,
            Program As New DataTable,
            Program1 As New DataTable,
            item As String = "",
            Qty As Integer = 0,
        cnt As Integer = 0, cntnum As Integer = 0, CHK As String = ""

        CHK = valPower(cblCodeType)
        'MO issue mat 
        WHR = configDate.DateWhere("TO_CHAR(sfaadocdt,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfba005,1,1) in ('G','M','A') AND SUBSTR(sfba005,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfba005,1,1) not in ('G','M','A') AND SUBSTR(sfba005,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " And sfba005 Like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and imaal004 like '%" & Spec & "%' "
        End If

        Dim Temp As New DataTable
        SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR &
              " group by sfba005"
        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                ISQL = "insert into " & tempTable & "(item,issueQty) values ('" & Temp.Rows(i).Item(0) & "','" & Temp.Rows(i).Item(1) & "')"
                clsDBConnect.QueryExecuteScalar(ISQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'MO 
        WHR = configDate.DateWhere("TO_CHAR(sfaa019,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfaa010,1,1) in ('G','M','A') AND SUBSTR(sfaa010,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfaa010,1,1) not in ('G','M','A') AND SUBSTR(sfaa010,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " And sfaa010 Like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and imaal004 like '%" & Spec & "%' "
        End If
        SQL = " select sfaa010 item,SUM(sfaa012-sfaa050) moQty " &
              " from sfaa_t " &
              " left join imaal_t on imaal001 = sfaa010 and imaalent=sfaaent " &
              " where sfaaent='3' and sfaastus ='F'and sfaa012-sfaa050 > 0 " & WHR &
              " group by sfaa010 order by sfaa010 "
        TempProgram = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For j As Integer = 0 To TempProgram.Rows.Count - 1
            With TempProgram.Rows(j)
                item = TempProgram.Rows(j).Item("item")
                Qty = TempProgram.Rows(j).Item("moQty")
                USQL = " if exists(select item,moQty from " & tempTable & " where item='" & item & "' ) " &
                           " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                           " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
                clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next


        'Sale Order
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(xmdc001,1,1) in ('G','M','A') AND SUBSTR(xmdc001,2,1) in (" & CHK & ")) OR  (( SUBSTR(xmdc001,1,1) not in ('G','M','A') AND SUBSTR(xmdc001,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " and xmdc001 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If
        SQL = " select xmdc001 item,sum(xmdc007-xmdd014) so " &
            " from  xmda_t " &
            " left join xmdc_t on xmdadocno = xmdcdocno and xmdcent=xmdaent and xmdcsite=xmdasite " &
            " left join imaal_t on imaal001 = xmdc001 and imaalent=xmdaent and imaal002= 'en_US' " &
            " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddent=xmdaent and xmddsite=xmdasite " &
            " where xmdaent='3' and xmdasite='JINPAO' and xmda005='1' and xmdc045 not in ('2','3') and xmdastus ='Y' " & WHR & "" &
            " group by xmdc001 order by xmdc001 "

        TempProgram = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To TempProgram.Rows.Count - 1
            item = TempProgram.Rows(i).Item("item")
            Qty = TempProgram.Rows(i).Item("so")
            USQL = " if exists(select item,delQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next


        'purchase request
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdb004,1,1) in ('G','M','A') AND SUBSTR(pmdb004,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdb004,1,1) not in ('G','M','A') AND SUBSTR(pmdb004,3,1) in (" & CHK & ") )))")
        End If
        If SuplyProduct = 5 Then
            WHR = WHR & " and imaa003 = '1401' "
        ElseIf SuplyOther = 6 Then
            WHR = WHR & " and imaa003= '1402' "
        Else
            WHR = WHR & " and imaa003 not in ('1401','1402') "
        End If

        If Code <> "" Then
            WHR = WHR & " and pmdb004 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and (imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If
        SQL = " select pmdb004 item,sum(pmdb006) pr " &
              "  from pmdb_t " &
              "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
              "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
              "  left join imaa_t on imaa001=pmdb004 and imaaent=pmdbent " &
              "  left join imaal_t on imaal001=pmdb004 and imaalent=pmdbent" &
              "  where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'Y' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' " & WHR &
              "  group by pmdb004 order by pmdb004 "
        TempProgram = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To TempProgram.Rows.Count - 1
            item = TempProgram.Rows(i).Item("item")
            Qty = TempProgram.Rows(i).Item("pr")
            USQL = " if exists(select item,prQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        ''purchase order
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdn001,1,1) in ('G','M','A') AND SUBSTR(pmdn001,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdn001,1,1) not in ('G','M','A') AND SUBSTR(pmdn001,3,1) in (" & CHK & ") )))")
        End If

        If SuplyProduct = 5 Then
            WHR = WHR & " and imaa003 = '1401' "
        ElseIf SuplyOther = 6 Then
            WHR = WHR & " and imaa003= '1402' "
        Else
            WHR = WHR & " and imaa003 not in ('1401','1402') "
        End If

        If Code <> "" Then
            WHR = WHR & " and pmdn001 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If
        SQL = "  Select  pmdn001 item,'2' poType,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " left join imaa_t on imaa001=pmdn001 and imaaent=pmdnent" &
            " where pmdnent='3' and pmdn045 = '1' " &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
            " group by pmdn001   order by pmdn001 "
        TempProgram = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To TempProgram.Rows.Count - 1
            item = TempProgram.Rows(i).Item("item")
            Qty = TempProgram.Rows(i).Item("po")

            Dim fldPO As String = "poManQty"
            Select Case TempProgram.Rows(i).Item("poType").ToString
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
                Case ""

            End Select

            USQL = " if exists(select item,poQty,poForQty,poMoQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'purchase receipt inspection
        WHR = ""
        WHR = configDate.DateWhere("TO_CHAR(pmdsdocdt,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdt006,1,1) in ('G','M','A') AND SUBSTR(pmdt006,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdt006,1,1) not in ('G','M','A') AND SUBSTR(pmdt006,3,1) in (" & CHK & ") )))")
        End If
        If SuplyProduct = 5 Then
            WHR = WHR & " and imaa003 = '1401' "
        ElseIf SuplyOther = 6 Then
            WHR = WHR & " and imaa003= '1402' "
        Else
            WHR = WHR & " and imaa003 not in ('1401','1402') "
        End If
        If Code <> "" Then
            WHR = WHR & " and pmdt006 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If

        SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " left join imaa_t on imaa001=pmdt006 and imaaent=pmdsent" &
            " left join imaal_t on imaal001=pmdt006 and imaalent=pmdtent " &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') " & WHR &
            " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "

        TempProgram = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To TempProgram.Rows.Count - 1
            item = TempProgram.Rows(i).Item("item")
            Qty = TempProgram.Rows(i).Item("po_insp") '
            USQL = " if exists(select item,poRcpQty,poQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'Stock
        Dim ValueChkMat As Integer = 0, ValueChkFG As Integer = 0, ValueChkSemiFG As Integer = 0, ValueChkSparPart As Integer = 0, ValueSuplyProduct As Integer = 0, ValueSuplyOther As Integer = 0
        Dim whList As String = ""
        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "1" Then
                    ValueChkMat = 1
                ElseIf boxVal = "2" Then
                    ValueChkFG = 2
                ElseIf boxVal = "3" Then
                    ValueChkSemiFG = 3
                ElseIf boxVal = "4" Then
                    ValueChkSparPart = 4
                ElseIf boxVal = "5" Then
                    ValueSuplyProduct = 5
                ElseIf boxVal = "6" Then
                    ValueSuplyOther = 6
                End If
            End If
        Next

        If ValueChkMat = 1 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901','3333') "
        ElseIf ValueChkFG = 2 Then
            whList = " and inag004 not in ('8888','9999','2600','2700') "
        ElseIf ValueChkSemiFG = 3 Then
            whList = " and inag004 not in ('8888','9999','2600','2700') "
        ElseIf ValueChkSparPart = 4 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901') "
        ElseIf ValueSuplyProduct = 5 Then
            whList = " and inag004 in ('4100') "
        ElseIf ValueSuplyOther = 6 Then
            whList = " and inag004 in ('4200') "
        End If

        'SaveStock ยังไม่มีใน T100
        SQL = "select item,stockQty from " & tempTable & ""
        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                item = Trim(Temp.Rows(i).Item("item"))
                Dim UTempSQL As New DataTable
                USQL = "select inag001 item,sum(inag008) stock from inag_t where inagent='3' and inag008 > 0  " & whList & " and inag001='" & item & "' group by inag001"
                UTempSQL = clsDBConnect.QueryDataTable(USQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If UTempSQL.Rows.Count > 0 Then
                    With UTempSQL.Rows(0)
                        item = .Item("item")
                        Qty = .Item("stock")
                        USQL = " if exists(select item,stockQty from " & tempTable & " where item='" & item & "' ) " &
                                " update " & tempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " &
                                " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
                        clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End With
                End If
            End With
        Next

        'MOQ QTY - Skip....>>
        'Dim dateToday As String = DateTime.Now.ToString("yyyyMMdd")
        'SQL = "select item from " & tempTable & " group by item"
        'Program = clsDBConnect.QueryDataTable(USQL, clsDBConnect.MIS2)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    Dim xcode As String = Program.Rows(i).Item("item")

        '    SQL = " select top 1 PURTM.UDF01 from PURTM  " &
        '          "   left join PURTL on TL001=TM001 and TL002=TM002 " &
        '          "  where TL006='Y' and TM004='" & xcode.TrimEnd & "' " &
        '          "    and (TM015='' or TM015 <='" & dateToday & "' ) order by TL010 desc"
        '    Program1 = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        '    If Program1.Rows.Count > 0 Then
        '        Dim moq = Program1.Rows(0).Item("UDF01")
        '        If moq <> "" Then
        '            USQL = " update " & tempTable & " set MoqQty='" & moq & "'  where item='" & xcode & "' "
        '            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        '        End If
        '    End If
        'Next

        'Show Data
        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty "
        Dim strSupply1 As String = " T.stockQty+T.poRcpQty "
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        'WHR = strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
        WHR = ""
        If valConsel <> "0" Then
            WHR = " where "
            If valConsel = "1" Then   'Shortage
                WHR = WHR & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
            ElseIf valConsel = "2" Then 'call In
                WHR = WHR & strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
            End If
        End If


        Dim ShowFiled As String = "JP Part,JP Spec,Unit,Call In,Stock(+),PO Insp.(+),PO(+),PO Manual(+),PO Forcast(+),PO MO(+),PR(+),MOQ Qty,Safe Stock,MO Rcv(+),SO(-),MO Issue(-),Shortage"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        Dim DT3 As New DataTable
        DataTableTranf(ArrayShowFiled, DT3)

        SQL = " select case when len(T.item) > 16 then (SUBSTRING(T.item,1,14)+'-'+SUBSTRING(T.item,15,2)) else T.item end as 'JP Part'," &
              " cast(case when (" & strSupply1 & ")-(" & strDemand & ")>=0 then '0' else (" & strSupply1 & ")-(" & strDemand & ") end as decimal(10,3)  ) as 'Call In', " &
              " T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)'," &
              " T.poQty as 'PO(+)',T.poManQty as 'PO Manual(+)',T.poForQty as 'PO Forcast(+)',T.poMoQty as 'PO MO(+)', " &
              " T.prQty as 'PR(+)',MoqQty as 'MOQ Qty',saveQty as 'Safe Stock'," &
              " T.moQty as 'MO Rcv(+)',T.delQty as 'SO(-)',T.issueQty as 'MO Issue(-)', " &
              " (" & strSupply & ")-(" & strDemand & ")  as Shortage ,T.item " &
              " from " & tempTable & " T " &
                WHR & " order by T.item "

        Dim dg As New DataTable
        dg = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim JPPart As String = ""
            Dim JPItem = ""
            Dim CallIn As String = ""
            Dim Stock As String = ""
            Dim POInsp As String = ""
            Dim PO As String = ""
            Dim POManual As String = ""
            Dim POForcast As String = ""
            Dim POMO As String = ""
            Dim PR As String = ""
            Dim MOQ As String = ""
            Dim SafeStock As String = ""
            Dim MORcv As String = ""
            Dim SO As String = ""
            Dim MOIssue As String = ""
            Dim Shortage As String = ""

            JPPart = dg.Rows(i).Item(0)
            CallIn = dg.Rows(i).Item(1)
            Stock = dg.Rows(i).Item(2)
            POInsp = dg.Rows(i).Item(3)
            PO = dg.Rows(i).Item(4)
            POManual = dg.Rows(i).Item(5)
            POForcast = dg.Rows(i).Item(6)
            POMO = dg.Rows(i).Item(7)
            PR = dg.Rows(i).Item(8)
            MOQ = dg.Rows(i).Item(9)
            SafeStock = dg.Rows(i).Item(10)
            MORcv = dg.Rows(i).Item(11)
            SO = dg.Rows(i).Item(12)
            MOIssue = dg.Rows(i).Item(13)
            Shortage = dg.Rows(i).Item(14)
            JPItem = dg.Rows(i).Item(15)
            Dim dz As New DataTable
            SQL = "Select imaa001,imaa006,imaal003,imaal004 from imaa_t left join imaal_t On imaal001= imaa001 and imaalent=imaaent where  imaa001='" & JPPart.Trim & "' and imaal002='en_US'"
            dz = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)

            Dim specification As String = ""
            Dim Description As String = ""
            Dim Unit As String = ""

            For z As Integer = 0 To dz.Rows.Count - 1
                Unit = dz.Rows(z).Item("imaa006")
                specification = dz.Rows(z).Item("imaal003").ToString
                Description = dz.Rows(z).Item("imaal004").ToString
            Next
            DT3.Rows.Add(New Object() {JPPart, specification, Unit, CallIn, Stock, POInsp, PO, POManual, POForcast, POMO, PR, MOQ, SafeStock, MORcv, SO, MOIssue, Shortage})
        Next
        ControlFormT100.ShowGridViewT100(gvShow, DT3)
        CountRow1.RowCount = ControlFormT100.rowGridviewT100(gvShow)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow;", True)

    End Sub

    Function DataTableTranf(ByVal DataTableFiledName() As String, ByRef DT3 As Data.DataTable) As Boolean
        DT3.Clear()
        For L_count As Integer = 0 To DataTableFiledName.Length - 1
            Dim myColumn As New Data.DataColumn
            myColumn.DataType = System.Type.GetType("System.String")
            myColumn.ColumnName = DataTableFiledName(L_count)
            DT3.Columns.Add(myColumn)
        Next

    End Function

    Protected Sub btPlanCallIn_Click(sender As Object, e As EventArgs) Handles btPlanCallIn.Click
        'Chk WH
        Dim ValueChkType As String = "", SuplyProduct As Integer = 0, SuplyOther As Integer = 0
        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "5" Then 'Suply Product
                    SuplyProduct = 5
                ElseIf boxVal = "6" Then 'Suply Other
                    SuplyOther = 2
                End If
            End If
        Next

        Dim CHK As String
        CHK = valPower(cblCodeType)
        gvPlanCallIn.Visible = True
        gvShow.Visible = False
        gvPoCallIn.Visible = False
        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""

        Dim CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            Program As New DataTable,
            Program1 As New DataTable,
            item As String = "",
            Qty As Integer = 0

        Dim TempTable As String = "TempPlanCallIn" & Session("UserName")
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = tbDateFrom.Text.Replace("/", "").Trim 'Begin date
        Dim endDate As String = tbDateTo.Text.Replace("/", "").Trim 'End date

        Dim xd As String = ""
        Dim xm As String = ""
        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

        Dim dateWork As Short = DateDiff(DateInterval.Day, beginDate, lastDate)
        CreateTempTable.createTempPlanCallIn(TempTable, beginDate, dateWork)

        'MO issue Mat
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfba005,1,1) in ('G','M','A') AND SUBSTR(sfba005,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfba005,1,1) not in ('G','M','A') AND SUBSTR(sfba005,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " And " & SFAA.ProductItem & " Like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and " & IMAAL.Specifaction & " like '%" & Spec & "%' "
        End If
        'before select 
        Dim whrDate As String = ""
        ''BOMDate(SFAA.BOMEffectiveDate) ยังไม่มีข้อมูล
        'whrDate = " and " & SFAA.BOMEffectiveDate & " <'" & strDate & "'"

        whrDate = " and TO_CHAR(sfaa019,'yyyy/MM/dd') <'" & strDate & "'"
        Dim Temp As New DataTable
        SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR & whrDate &
              " group by sfba005"
        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                ISQL = "insert into " & TempTable & "(item,issueQty) values ('" & Temp.Rows(i).Item(0) & "','" & Temp.Rows(i).Item(1) & "')"
                clsDBConnect.QueryExecuteScalar(ISQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        ' in range select
        ' BOMDate(SFAA.BOMEffectiveDate) ยังไม่มีข้อมูล
        ' whrDate = configDate.DateWhere("" & SFAA.BOMEffectiveDate & "", strDate, endDate)

        whrDate = configDate.DateWhere("TO_CHAR(sfaa019,'yyyy/MM/dd')", strDate, endDate)

        SQL = " select sfba005 item,TO_CHAR(sfaa019,'yyyy/MM/dd') issueDate,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR & whrDate &
              " group by sfba005,TO_CHAR(sfaa019,'yyyy/MM/dd') order by sfba005,TO_CHAR(sfaa019,'yyyy/MM/dd') "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)

        Dim lastItem As String = "",
        selDate As String = "",
        fldInsHash As Hashtable = New Hashtable,
        whrHash As Hashtable = New Hashtable,
        fldUpdHash As Hashtable = New Hashtable

        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("issueQty")
            selDate = Program.Rows(i).Item("issueDate").ToString
            If lastItem <> item Then
                If lastItem <> "" Then
                    clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(TempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                whrHash.Add("item", item)
            End If
            Dim fld As String = "issueQty" & selDate
            fldInsHash.Add(fld, Qty)
            fldUpdHash.Add(fld, fld & "+" & Qty)
            lastItem = item
        Next
        If Program.Rows.Count > 0 Then
            clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(TempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        End If

        'purchase receipt inspection
        Dim enddateforpur As String = tbDateTo.Text.Trim
        WHR = configDate.DateWhere("TO_CHAR(pmdsdocdt,'yyyy/MM/dd')", "", enddateforpur)

        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdt006,1,1) in ('G','M','A') AND SUBSTR(pmdt006,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdt006,1,1) not in ('G','M','A') AND SUBSTR(pmdt006,3,1) in (" & CHK & ") )))")
        End If

        If SuplyProduct = 5 Then
            WHR = WHR & " and imaa003 = '1401' "
        ElseIf SuplyOther = 6 Then
            WHR = WHR & " and imaa003= '1402' "
        Else
            WHR = WHR & " and imaa003 not in ('1401','1402') "
        End If

        If Code <> "" Then
            WHR = WHR & " and pmdt006 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If

        SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " left join imaa_t on imaa001=pmdt006 and imaaent=pmdsent" &
            " left join imaal_t on imaal001=pmdt006 and imaalent=pmdtent" &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') " & WHR &
            " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp") '
            USQL = " if exists(select item,poRcpQty from " & TempTable & " where item='" & item & "' ) " &
                   " update " & TempTable & " set poRcpQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & TempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'Stock
        Dim ValueChkMat As Integer = 0, ValueChkFG As Integer = 0, ValueChkSemiFG As Integer = 0, ValueChkSparPart As Integer = 0, ValueSuplyProduct As Integer = 0, ValueSuplyOther As Integer = 0
        Dim whList As String = ""
        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "1" Then
                    ValueChkMat = 1
                ElseIf boxVal = "2" Then
                    ValueChkFG = 2
                ElseIf boxVal = "3" Then
                    ValueChkSemiFG = 3
                ElseIf boxVal = "4" Then
                    ValueChkSparPart = 4
                ElseIf boxVal = "5" Then
                    ValueSuplyProduct = 5
                ElseIf boxVal = "6" Then
                    ValueSuplyOther = 6
                End If
            End If
        Next

        If ValueChkMat = 1 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901','3333') "
        ElseIf ValueChkFG = 2 Then
            whList = " and inag004 not in ('8888','9999','2600','2700') "
        ElseIf ValueChkSemiFG = 3 Then
            whList = " and inag004 not in ('8888','9999','2600','2700') "
        ElseIf ValueChkSparPart = 4 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901') "
        ElseIf ValueSuplyProduct = 5 Then
            whList = " and inag004 in ('4100') "
        ElseIf ValueSuplyOther = 6 Then
            whList = " and inag004 in ('4200') "
        End If

        SQL = "select item,stockQty from " & TempTable & ""
        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                item = Trim(Temp.Rows(i).Item("item"))
                Dim UTempSQL As New DataTable
                USQL = "select inag001 item,sum(inag008) stock from inag_t where inagent='3' and inag008 > 0  " & whList & " and inag001='" & item & "' group by inag001"
                UTempSQL = clsDBConnect.QueryDataTable(USQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If UTempSQL.Rows.Count > 0 Then
                    With UTempSQL.Rows(0)
                        item = .Item("item")
                        Qty = .Item("stock")
                        USQL = " if exists(select item,stockQty from " & TempTable & " where item='" & item & "' ) " &
                                " update " & TempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " &
                                " insert into " & TempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
                        clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End With
                End If
            End With
        Next

        'Show Data
        Dim TempJPItem As String = "TempJPItem" & Session("UserName")
        CreateTempTable.CreateTempJPItem(TempJPItem)
        WHR = ""
        Dim fldSupply As String = "(T.stockQty+T.poRcpQty)"
        Dim fldIssue As String = "T.issueQty"
        Dim fldShow As String = ",T.issueQty as 'Before Issue' " 'cast(case when " & fldSupply & "- T.issueQty>=0 then '0' else " & fldSupply & "- T.issueQty end as varchar) as 'Before Call In'

        Dim Col As String = ""
        For i As Integer = 0 To dateWork
            Dim tdate As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim sdate As String = beginDate.AddDays(i).ToString(" dd/MM", System.Globalization.CultureInfo.InvariantCulture)
            Dim fld As String = "T.issueQty" & tdate
            fldIssue = fldIssue & "+" & fld
            fldShow = fldShow & "," & fld & " as 'Q" & sdate & "' ,cast(case when " & fldSupply & "- (" & fldIssue & ")>=0 then '0' else " & fldSupply & "- (" & fldIssue & ") end as varchar) as 'C" & sdate & "' "

        Next

        fldShow = fldShow & "," & fldIssue & " as 'Issue Qty Sum' ,cast(case when " & fldSupply & "- (" & fldIssue & ")>=0 then '0' else " & fldSupply & "- (" & fldIssue & ") end as varchar) as 'Call In' "

        Dim dt As New DataTable
        Dim dz As New DataTable
        Dim dc As New DataTable
        Dim Unit As String = ""

        SQL = " select case when len(T.item)=16 then (SUBSTRING(T.item,1,14)+'-'+SUBSTRING(T.item,15,2)) else T.item end as 'JP Part'," &
              " T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)'" & fldShow &
              " from " & TempTable & " T " &
               " where " & fldSupply & "- (" & fldIssue & ")< 0  order by T.item "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        Dim JPPart As String = ""
        Dim JPItem = ""
        Dim CallIn As String = ""
        Dim Stock As String = ""
        Dim POInsp As String = ""
        Dim BeforeIssue As String = ""
        Dim Q As String = ""
        Dim C As String = ""
        Dim IssueQtySum As String = ""
        Dim specification As String = ""
        Dim Description As String = ""
        Dim SQLsel As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1

            JPItem = Replace(dt.Rows(i).Item(0), "-", "").Trim
            Dim SQLtest As String = "select imaa006,imaal003,imaal004 from imaa_t left join imaal_t On imaal001= imaa001 where imaaent='3'and imaalent='3' and imaal002='en_US' and imaa001='" & JPItem & "'"
            dz = clsDBConnect.QueryDataTable(SQLtest, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)

            For z As Integer = 0 To dz.Rows.Count - 1
                specification = dz.Rows(z).Item("imaal003")
                Description = dz.Rows(z).Item("imaal004")
                Unit = dz.Rows(z).Item("imaa006")
                SQL = " insert into " & TempJPItem & "(Item,specification,Descriptions,Unit) " &
               " values('" & JPItem.Trim & "','" & specification.Trim & "','" & Description.Trim & "','" & Unit.Trim & "')"
                clsDBConnect.QueryExecuteScalar(SQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            Next
        Next

        SQL = " select case when len(T.item)=16 then (SUBSTRING(T.item,1,14)+'-'+SUBSTRING(T.item,15,2)) else T.item end as 'JP Part',specification+'-'+Descriptions JPSpec,Unit, " &
              " T.stockQty as 'Stock(+)',T.poRcpQty as 'PO Insp.(+)'" & fldShow &
              " from " & TempTable & " T " &
              " left join " & TempJPItem & " T1 on T.item=T1.Item" &
               " where " & fldSupply & "- (" & fldIssue & ")< 0  order by T.item "
        ControlFormT100.ShowGridViewT100(gvPlanCallIn, SQL, clsDBConnect.MIS2)
        CountRow1.RowCount = ControlFormT100.rowGridviewT100(gvPlanCallIn)
        btExport.Visible = True
    End Sub

    Private Function returnDate(dateVal As String) As String
        Dim dateToday As Date = DateTime.Today,
            strDate As String = "",
            xd As String = "",
            xm As String = ""
        If dateVal <> "" Then
            strDate = configDate.dateFormat2(dateVal)
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = dateToday.Year & xm & xd
        End If
        Return strDate
    End Function

    Protected Sub btPOCallIn_Click(sender As Object, e As EventArgs) Handles btPOCallIn.Click
        'Chk WH
        Dim ValueChkType As String = "", SuplyProduct As Integer = 0, SuplyOther As Integer = 0, CHK As String
        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "5" Then 'Suply Product
                    SuplyProduct = 5
                ElseIf boxVal = "6" Then 'Suply Other
                    SuplyOther = 2
                End If
            End If
        Next

        CHK = valPower(cblCodeType)
        gvPoCallIn.Visible = True
        gvShow.Visible = False
        gvPlanCallIn.Visible = False

        Dim tempTable As String = "tempMaterialsShortage" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)

        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = "",
            CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text,
            Spec As String = tbSpec.Text,
            EndDate As String = tbDateTo.Text.Trim,
            Program As New DataTable,
            Program1 As New DataTable,
            Temp As New DataTable,
            item As String = "",
            Qty As Integer = 0

        'MO issue mat 
        WHR = configDate.DateWhere("TO_CHAR(sfaadocdt,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfba005,1,1) in ('G','M','A') AND SUBSTR(sfba005,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfba005,1,1) not in ('G','M','A') AND SUBSTR(sfba005,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " and sfba005 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and imaal004 like '%" & Spec & "%' "
        End If

        SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR &
              " group by sfba005"

        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                ISQL = "insert into " & tempTable & "(item,issueQty) values ('" & Temp.Rows(i).Item(0) & "','" & Temp.Rows(i).Item(1) & "')"
                clsDBConnect.QueryExecuteScalar(ISQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'Sale Order
        'WHR = configDate.DateWhere(XMDC.AppointedDeliveryDate, "", EndDate)*
        WHR = configDate.DateWhere("TO_CHAR(xmdd011,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(xmdc001,1,1) in ('G','M','A') AND SUBSTR(xmdc001,2,1) in (" & CHK & ")) OR  (( SUBSTR(xmdc001,1,1) not in ('G','M','A') AND SUBSTR(xmdc001,3,1) in (" & CHK & ") )))")
        End If
        If Code <> "" Then
            WHR = WHR & " and xmdc001 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If
        SQL = " select xmdc001 item,sum(xmdc007-xmdd014) so " &
            " from  xmda_t " &
            " left join xmdc_t on xmdadocno = xmdcdocno and xmdcent=xmdaent and xmdcsite=xmdasite " &
            " left join imaal_t on imaal001 = xmdc001 and imaalent=xmdaent and imaal002= 'en_US' " &
            " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddent=xmdaent and xmddsite=xmdasite " &
            " where xmdaent='3' and xmdasite='JINPAO' and xmda005='1' and xmdc045 not in ('2','3') and xmdastus ='Y' " & WHR & "" &
            " group by xmdc001 order by xmdc001 "

        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Temp.Rows.Count - 1
            item = Temp.Rows(i).Item("item")
            Qty = Temp.Rows(i).Item("so")
            USQL = " if exists(select item,delQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'purchase receipt inspection
        WHR = configDate.DateWhere("TO_CHAR(pmdsdocdt,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdt006,1,1) in ('G','M','A') AND SUBSTR(pmdt006,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdt006,1,1) not in ('G','M','A') AND SUBSTR(pmdt006,3,1) in (" & CHK & ") )))")
        End If

        If SuplyProduct = 5 Then
            WHR = WHR & " and imaa003 = '1401' "
        ElseIf SuplyOther = 6 Then
            WHR = WHR & " and imaa003= '1402' "
        Else
            WHR = WHR & " and imaa003 not in ('1401','1402') "
        End If

        If Code <> "" Then
            WHR = WHR & " and pmdt006 like '%" & Code & "%' "
        End If
        If Spec <> "" Then
            WHR = WHR & " and ( imaal003 like '%" & Spec & "%' or imaal004 like '%" & Spec & "%') "
        End If

        SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " left join imaa_t on imaa001=pmdt006 and imaaent=pmdsent" &
            " left join imaal_t on imaal001=pmdt006 and imaalent=pmdtent" &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') " & WHR &
            " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "

        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Temp.Rows.Count - 1
            item = Temp.Rows(i).Item("item")
            Qty = Temp.Rows(i).Item("po_insp") '
            USQL = " if exists(select item,poRcpQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'Stock 'SaveStock ยังไม่มีใน T100
        Dim whList As String = ""
        Dim ValueChkMat As Integer = 0, ValueChkFG As Integer = 0, ValueChkSemiFG As Integer = 0, ValueChkSparPart As Integer = 0, ValueSuplyProduct As Integer = 0, ValueSuplyOther As Integer = 0

        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "1" Then
                    ValueChkMat = 1
                ElseIf boxVal = "2" Then
                    ValueChkFG = 2
                ElseIf boxVal = "3" Then
                    ValueChkSemiFG = 3
                ElseIf boxVal = "4" Then
                    ValueChkSparPart = 4
                ElseIf boxVal = "5" Then
                    ValueSuplyProduct = 5
                ElseIf boxVal = "6" Then
                    ValueSuplyOther = 6
                End If
            End If
        Next

        If ValueChkMat = 1 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901','3333')"
        ElseIf ValueChkFG = 2 Then
            whList = " and inag004 not in ('8888','9999','2600','2700')"
        ElseIf ValueChkSemiFG = 3 Then
            whList = " and inag004 not in ('8888','9999','2600','2700')"
        ElseIf ValueChkSparPart = 4 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2900','2901')"
        ElseIf ValueSuplyProduct = 5 Then
            whList = " and inag004 in ('4100')"
        ElseIf ValueSuplyOther = 6 Then
            whList = " and inag004 in ('4200')"
        End If

        SQL = "select item,stockQty from " & tempTable & ""
        Temp = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Temp.Rows.Count - 1
            With Temp.Rows(i)
                item = Trim(Temp.Rows(i).Item("item"))
                Dim UTempSQL As New DataTable
                USQL = "select inag001 item,sum(inag008) stock from inag_t where inagent='3' and inag008 > 0  " & whList & " and inag001='" & item & "' group by inag001"
                UTempSQL = clsDBConnect.QueryDataTable(USQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If UTempSQL.Rows.Count > 0 Then
                    With UTempSQL.Rows(0)
                        item = .Item("item")
                        Qty = .Item("stock")
                        USQL = " if exists(select item,stockQty from " & tempTable & " where item='" & item & "' ) " &
                                " update " & tempTable & " set stockQty='" & Qty & "' where item='" & item & "' else " &
                                " insert into " & tempTable & "(item,stockQty)values ('" & item & "','" & Qty & "')"
                        clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End With
                End If
            End With
        Next


        'Show Data
        Dim DT3 As New DataTable
        Dim ShowFiled As String = "Item,Desc,Spec,PO Number,Supplier,SupplierName,Plan Delivery Date,PO Bal,Call In"
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")

        DataTableTranf(ArrayShowFiled, DT3)

        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty "
        Dim strSupply1 As String = " T.stockQty+T.poRcpQty "
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        WHR = ""
        If valConsel <> "0" Then
            WHR = " where "
            If valConsel = "1" Then   'Shortage
                WHR = WHR & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
            ElseIf valConsel = "2" Then 'call In
                WHR = WHR & strDemand & ">" & strSupply1 & " and " & strDemand & ">0  "
            End If
        End If
        Dim dt As New DataTable
        Dim dr As New DataTable
        Dim StrSQL As String = ""
        Dim JPItem As String = "",
            CallIn As String = "",
            PONo As String = "",
            ItemSpec As String = "",
            ItemDesc As String = "",
            SupID As String = "",
            SupDesc As String = "",
            PlanDelDate As String = "",
            POBal As String = ""

        SQL = "select T.item as 'Item',cast((T.stockQty+T.poRcpQty)-(T.issueQty+T.delQty) as decimal(15,3)) as 'Call In'" &
              " from " & tempTable & " T " &
              " where 1=1 and T.issueQty+T.delQty>T.stockQty+T.poRcpQty and T.issueQty+T.delQty>0 "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        For i As Integer = 0 To dt.Rows.Count - 1
            item = Trim(dt.Rows(i).Item(0))
            CallIn = dt.Rows(i).Item(1)

            SQL = " select pmdn001 Item,pmdndocno||'-'||pmdnseq PONo,imaal003 spec,imaal004 Description,pmdl004 SupplierID,pmaal004 SupDesc," &
            " pmdn012 PlanDeliveryDate,pmdn007-pmdo015 POBalance" &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " left join pmaal_t on pmdl004 = pmaal001 and pmaalent=pmdnent " &
            " where pmdnent='3' and pmdn045 = '1' and pmdlstus <> 'C'" &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 and pmdn001='" & item & "'" &
            " order by pmdn001,pmdndocno,pmdnseq"

            dr = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            For j As Integer = 0 To dr.Rows.Count - 1
                With dr.Rows(j)
                    JPItem = .Item(0)
                    PONo = .Item(1)
                    ItemSpec = .Item(2)
                    ItemDesc = .Item(3)
                    SupID = .Item(4)
                    SupDesc = .Item(5)
                    PlanDelDate = .Item(6)
                    POBal = .Item(7)
                    DT3.Rows.Add(New Object() {JPItem, ItemSpec, ItemDesc, PONo, SupID, SupDesc, PlanDelDate, POBal, CallIn})
                End With
            Next
        Next
        ControlFormT100.ShowGridViewT100(gvPoCallIn, DT3)
        CountRow1.RowCount = ControlFormT100.rowGridviewT100(gvPoCallIn)
        btExport.Visible = True
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click

        Dim gv As GridView = gvPoCallIn
        If gvShow.Visible Then
            gv = gvShow
        ElseIf gvPlanCallIn.Visible Then
            gv = gvPlanCallIn
        End If

        ControlForm.ExportGridViewToExcel("MaterialsShortage" & Session("UserName"), gv)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("JP Part")) Then
                    Dim link As String = ""

                    Dim jpPart As String = .DataItem("JP Part")
                    link = link & "&JPPart= " & jpPart.ToString
                    link = link & "&JPSpec= " & .DataItem("JP SPEC")
                    link = link & "&Unit= " & .DataItem("Unit")
                    link = link & "&stock= " & .DataItem("Stock(+)")
                    link = link & "&poQty= " & .DataItem("PO(+)")
                    link = link & "&poManQty= " & .DataItem("PO Manual(+)")
                    link = link & "&poForQty= " & .DataItem("PO Forcast(+)")
                    link = link & "&poMoQty= " & .DataItem("PO MO(+)")
                    link = link & "&prQty= " & .DataItem("PR(+)")
                    link = link & "&moQty= " & .DataItem("MO Rcv(+)")
                    link = link & "&delQty= " & .DataItem("SO(-)")
                    link = link & "&issueQty= " & .DataItem("MO Issue(-)")
                    link = link & "&poRcpQty= " & .DataItem("PO Insp.(+)")
                    link = link & "&endDate= " & configDate.dateFormat2(tbDateTo.Text)
                    hplDetail.NavigateUrl = "MaterailsShortagePopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)
                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Private Sub gvPlanCallIn_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlanCallIn.RowDataBound
        With e.Row
            Dim fDate As String = DateTime.Parse(tbDateFrom.Text).ToString("dd-MMM-yy")
            Dim tDate As String = DateTime.Parse(tbDateTo.Text).ToString("dd-MMM-yy")
            If .RowType = DataControlRowType.DataRow Then
                Dim item As String = .Cells(0).Text.Trim.Replace("-", "")
                With .Cells(5)
                    If CDec(.Text.Trim) > 0 Then
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=&tdate=" & tDate & "&addDate=-1','moList',800,500,'yes')")
                    End If
                End With

                Dim j As Integer = 0
                For i As Decimal = 6 To gvPlanCallIn.HeaderRow.Cells.Count - 3 Step 2
                    With .Cells(i)
                        Dim qty As Decimal = CDec(.Text.Trim)
                        If qty > 0 Then
                            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                            .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=" & fDate & "&tdate=&addDate=" & j & "','moList',800,500,'yes')")
                            j = j + 1
                        End If
                    End With
                Next
                With .Cells(gvPlanCallIn.HeaderRow.Cells.Count - 2)
                    If CDec(.Text.Trim) > 0 Then
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        .Attributes.Add("onclick", "NewWindow('MaterailsShortageMoList.aspx?item=" & item & "&fdate=" & fDate & "&tdate=" & tDate & "&addDate=0','moList',800,500,'yes')")
                    End If
                End With
            End If
        End With

    End Sub
End Class