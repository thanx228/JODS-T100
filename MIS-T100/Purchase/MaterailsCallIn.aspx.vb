Public Class MaterailsCallIn
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnect
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New T100CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If
            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
        End If
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

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMaterialsCallIn" & Session("UserName")
        CreateTempTable.createTempMaterialShortage(tempTable)
        If tbSup.Text.Trim <> "" Then
            SearchBySupplier(tempTable)
        Else
            SearchBy(tempTable)
        End If
        'Stock
        Dim CodeType As String = cblCodeType.Text,
            WHR As String = "",
            SQL As String = "",
            Program As New DataTable,
            item As String = "",
            Titem As String = "",
            Qty As Integer = 0,
            USQL As String = "",
            whList As String = "",
            ValueChkType As String = "",
            ValueChkMat As Integer = 0, ValueChkFG As Integer = 0, ValueChkSemiFG As Integer = 0, ValueChkSparPart As Integer = 0

        For Each Val As ListItem In cblCodeType.Items
            Dim boxVal As String = CStr(Val.Value.Trim)
            If Val.Selected Then
                If boxVal = "1" Then
                    ValueChkMat = 1
                ElseIf boxVal = "2" Then
                    ValueChkFG = 2
                ElseIf boxVal = "3" Then
                    ValueChkSemiFG = 3
                Else
                    ValueChkSparPart = 4
                End If
            End If
        Next

        If ValueChkFG = 2 Or ValueChkSemiFG = 3 Then
            whList = " and inag004 not in ('8888','9999','2600','2700','5000','5001','5002') "
        ElseIf ValueChkSparPart = 3 Or ValueChkSparPart = 4 Then
            whList = " and inag004 in ('2101','2201','2202','2204','2205','2206','2209','2300','2301','2400','2900','2901','3000','3200','3300','3400','3700','3800','3333','3100','3600','3900') "
        End If

        SQL = " select  item from " & tempTable & ""
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim SSQL As String = ""
            Dim dt As New DataTable
            Titem = Trim(Program.Rows(i).Item("item"))
            SSQL = "select inag001 item,sum(inag008) stock,NVL('',0) saveStock from inag_t where inagent='3' and inag008 > 0 " & whList & " and inag001 ='" & Titem & "'" &
                " group by inag001 "
            dt = clsDBConnect.QueryDataTable(SSQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("stock")
                    Dim saveQty As Decimal = .Item("saveStock").ToString
                    USQL = " if exists(select item,stockQty from " & tempTable & " where item='" & item & "' ) " &
                           " update " & tempTable & " set stockQty='" & Qty & "',saveQty='" & saveQty & "' where item='" & item & "' else " &
                           " insert into " & tempTable & "(item,stockQty,saveQty)values ('" & item & "','" & Qty & "','" & saveQty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If
        Next

        'Show Data
        Dim strDemand As String = " T.issueQty+T.delQty  "
        Dim strSupply As String = " T.stockQty+T.poRcpQty " '+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty
        Dim strSupply2 As String = " T.stockQty+T.moQty+T.poQty+T.poManQty+T.poForQty+T.poMoQty+T.prQty+T.poRcpQty " '
        Dim valConsel As String = ddlCondition.SelectedValue.ToString
        If valConsel <> "0" Then
            WHR = " where "
            Select Case valConsel
                Case "1" 'Call In
                    WHR = WHR & strDemand & ">" & strSupply & " and " & strDemand & ">0  "
                Case "2" 'Shortage
                    WHR = WHR & strDemand & ">" & strSupply2 & " and " & strDemand & ">0  "
            End Select
        End If

        Dim dtShow As New DataTable
        dtShow.Columns.Add(New DataColumn("JP Item"))
        dtShow.Columns.Add(New DataColumn("JP Spec"))
        dtShow.Columns.Add(New DataColumn("Industry type"))
        dtShow.Columns.Add(New DataColumn("Call In"))
        dtShow.Columns.Add(New DataColumn("Shortage"))
        dtShow.Columns.Add(New DataColumn("MO Issue(-)"))
        dtShow.Columns.Add(New DataColumn("Lead Time(Day)"))
        dtShow.Columns.Add(New DataColumn("Safety"))
        dtShow.Columns.Add(New DataColumn("Stock(+)"))
        dtShow.Columns.Add(New DataColumn("PO Insp.(+)"))
        dtShow.Columns.Add(New DataColumn("PO(+)"))
        dtShow.Columns.Add(New DataColumn("PO Manual(+)"))
        dtShow.Columns.Add(New DataColumn("PO Forcast(+)"))
        dtShow.Columns.Add(New DataColumn("PO MO(+)"))
        dtShow.Columns.Add(New DataColumn("PR(+)App"))
        dtShow.Columns.Add(New DataColumn("PR(+) Not App"))
        dtShow.Columns.Add(New DataColumn("MO Rcv(+)"))
        dtShow.Columns.Add(New DataColumn("SO(-)"))
        dtShow.Columns.Add(New DataColumn("Main Supp."))
        dtShow.Columns.Add(New DataColumn("Confirm Delivery Date"))
        dtShow.Columns.Add(New DataColumn("Plan Delivery Date"))
        dtShow.Columns.Add(New DataColumn("Main W/H"))

        SQL = " select T.item as 'C01',cast(case when (" & strSupply & ")-(" & strDemand & ")>='0' then '0' else (" & strSupply & ")-(" & strDemand & ") end as decimal(10,2)) as 'C03'," &
            " cast(case when (" & strSupply2 & ")-(" & strDemand & ")>='0' then '0' else (" & strSupply2 & ")-(" & strDemand & ") end as decimal(10,2)) as 'C04'," &
            " T.issueQty as 'C05',T.saveQty as 'C07',T.stockQty as 'C08',T.poRcpQty as 'C09',T.poQty as 'C10',T.poManQty as 'C11',T.poForQty as 'C12',T.poMoQty as 'C13', " &
            " T.prQty as 'C14',T.prQtyNot as 'C15',T.moQty as 'C16',T.delQty as 'C17'," &
            " (case when T.confirmdate='' then ' ' else (substring(T.confirmdate,1,4)+'-'+substring(T.confirmdate,5,2)+'-'+substring(T.confirmdate,7,2)) end  ) as 'C19'," &
            " (case when T.plandate='' then ' ' else (substring(T.plandate,1,4)+'-'+substring(T.plandate,5,2)+'-'+substring(T.plandate,7,2)) end  ) as 'C21' " &
            " from " & tempTable & " T " & WHR & " order by T.item "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim jpitem As String = "",'C01
                callin As String = "",'C03
                shortage As String = "",'C04
                moissues As String = "",'C05
                safety As String = "",'C07
                stock As String = "",'C08
                poinsp As String = "",'C09
                po As String = "",'C10
                pomanual As String = "",'C11
                poforcast As String = "",'C12
                pomo As String = "",'C13
                prapp As String = "",'C14
                prnotapp As String = "",'C15
                morcv As String = "",'C16
                so As String = "",'C17
                confirmdeldate As String = "",'C19
                plandeldate As String = "" 'C21
                Dim QL As String = ""
                Dim dt As New DataSet
                Dim jpspec As String = "",
                    industrytype As String = "",
                    leadtime As String = "",
                    mainsupp As String = "",
                    mainwh As String = ""

                jpitem = .Item("C01")
                callin = .Item("C03")
                shortage = .Item("C04")
                moissues = .Item("C05")
                safety = .Item("C07")
                stock = .Item("C08")
                poinsp = .Item("C09")
                po = .Item("C10")
                pomanual = .Item("C11")
                poforcast = .Item("C12")
                pomo = .Item("C13")
                prapp = .Item("C14")
                prnotapp = .Item("C15")
                morcv = .Item("C16")
                so = .Item("C17")
                confirmdeldate = .Item("C19")
                plandeldate = .Item("C21")

                Dim dr1 As DataRow

                QL = "select imaf001,imaf171 leadtime,NVL(imaf153,'-') mainsupp,imaal003 descr,imaal004 spec,imaa009,NVL(rtaxl003,'-') industrytype,NVL(imae041,'-') MainWHR  from imaf_t " &
                    " left join imae_t on imae001=imaf001 and imaeent=imafent and imaesite=imafsite " &
                    " left join imaa_t on imaa001=imae001 and imaaent=imaeent " &
                    " left join imaal_t on imaal001=imaa001 and imaalent=imafent and imaal002='en_US' " &
                    " left join rtaxl_t on rtaxl001=imaa009 and rtaxlent=imafent and rtaxl002='en_US' and rtaxl001 in ('1','2','3','4','5','6','7','8') " &
                    " where imafent='3' and imafsite='JINPAO' and imaf001='" & jpitem.Trim & "'" &
                    " order by imaf001 "
                dt = clsDBConnect.QueryDataSet(QL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Tables(0).Rows.Count > 0 Then
                    With dt.Tables(0).Rows(0)
                        jpspec = .Item("descr")
                        industrytype = .Item("industrytype")
                        leadtime = .Item("leadtime")
                        mainsupp = .Item("mainsupp")
                        mainwh = .Item("MainWHR")
                    End With
                End If
                dr1 = dtShow.NewRow()
                dr1("JP Item") = jpitem
                dr1("JP Spec") = jpspec
                dr1("Industry type") = industrytype
                dr1("Call In") = callin
                dr1("shortage") = shortage
                dr1("MO Issue(-)") = moissues
                dr1("Lead Time(Day)") = leadtime
                dr1("Safety") = safety
                dr1("Stock(+)") = stock
                dr1("PO Insp.(+)") = poinsp
                dr1("PO(+)") = po
                dr1("PO Manual(+)") = pomanual
                dr1("PO Forcast(+)") = poforcast
                dr1("PO MO(+)") = pomo
                dr1("PR(+)App") = prapp
                dr1("PR(+) Not App") = prnotapp
                dr1("MO Rcv(+)") = morcv
                dr1("SO(-)") = so
                dr1("Main Supp.") = mainsupp
                dr1("Confirm Delivery Date") = confirmdeldate
                dr1("Plan Delivery Date") = plandeldate
                dr1("Main W/H") = mainwh
                dtShow.Rows.Add(dr1)
            End With
        Next

        ControlFormT100.ShowGridViewT100(gvShow, dtShow)
        CountRow1.RowCount = ControlFormT100.rowGridviewT100(gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        btExport.Visible = True

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

    Protected Sub SearchBy(ByVal tempTable As String)

        Dim SQL As String = "",
            WHR As String = "",
            ISQL As String = "",
            USQL As String = ""
        Dim CodeType As String = cblCodeType.Text,
            Condition As String = ddlCondition.Text,
            Code As String = tbCode.Text.Trim,
            Spec As String = tbSpec.Text.Trim,
            Desc As String = tbDesc.Text.Trim,
            EndDate As String = configDate.dateFormat2(tbEndDate.Text)
        Dim Program As New DataTable
        Dim item As String = "",
            Qty As Decimal = 0,
            Mdate As String = ""
        Dim CHK As String = ""
        CHK = valPower(cblCodeType)
        'MO issue mat 
        WHR = Conn_SQL.Where("TO_CHAR(sfaadocdt,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfba005,1,1) in ('G','M','A') AND SUBSTR(sfba005,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfba005,1,1) not in ('G','M','A') AND SUBSTR(sfba005,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("sfba005", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec
        'MO issue mat
        'BOM item /(MO)sum(RequiredQty -(Issue+Unplaned))
        Dim dt As New DataTable
        SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR &
              " group by sfba005"
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1

            ISQL = "insert into " & tempTable & "(item,issueQty) values ('" & dt.Rows(i).Item(0).ToString & "','" & dt.Rows(i).Item(1).ToString & "')"
            clsDBConnect.QueryExecuteScalar(ISQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)

        Next

        ''MO 
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(sfaa010,1,1) in ('G','M','A') AND SUBSTR(sfaa010,2,1) in (" & CHK & ")) OR  (( SUBSTR(sfaa010,1,1) not in ('G','M','A') AND SUBSTR(sfaa010,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("sfaa010", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        '(MO Operation) /ProductionQty-PlanCompleted
        SQL = " select sfaa010 item,SUM(sfaa012-sfaa050) moQty " &
              " from sfaa_t " &
              " left join imaal_t on imaal001 = sfaa010 and imaalent=sfaaent " &
              " where sfaaent='3' and sfaastus ='F'and sfaa012-sfaa050 > 0 " & WHR &
              " group by sfaa010 order by sfaa010 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For J As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(J)
                item = Program.Rows(J).Item("item").ToString
                Qty = Program.Rows(J).Item("moQty").ToString
                USQL = " if exists(select item,moQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
                clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'Sale Order
        WHR = ""
        WHR = configDate.DateWhere("TO_CHAR(xmdd011,'yyyy/MM/dd')", "", EndDate)
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(xmdc001,1,1) in ('G','M','A') AND SUBSTR(xmdc001,2,1) in (" & CHK & ")) OR  (( SUBSTR(xmdc001,1,1) not in ('G','M','A') AND SUBSTR(xmdc001,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("xmdc001", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        SQL = " select xmdc001 item,sum(xmdc007-xmdd014) so " &
            " from  xmda_t " &
            " left join xmdc_t on xmdadocno = xmdcdocno and xmdcent=xmdaent and xmdcsite=xmdasite " &
            " left join imaal_t on imaal001 = xmdc001 and imaalent=xmdaent and imaal002= 'en_US' " &
            " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddent=xmdaent and xmddsite=xmdasite " &
            " where xmdaent='3' and xmdasite='JINPAO' and xmda005='1' and xmdc045 not in ('2','3') and xmdastus ='Y' " & WHR & "" &
            " group by xmdc001 order by xmdc001 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("so")
            USQL = " if exists(select item,delQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'purchase request Appove 
        'Status = Approved
        'Closed Indicator = Not Cloesd(2=Auto Closed,3=Manual Closed)
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdb004,1,1) in ('G','M','A') AND SUBSTR(pmdb004,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdb004,1,1) not in ('G','M','A') AND SUBSTR(pmdb004,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdb004", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec
        SQL = " select pmdb004 item,sum(pmdb006) pr " &
              "  from pmdb_t " &
              "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
              "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
              "  left join imaa_t on imaa001=pmdb004 and imaaent=pmdbent " &
              "  left join imaal_t on imaal001=pmdb004 and imaalent=pmdbent" &
              "  where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'Y' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' " & WHR &
              "  group by pmdb004 order by pmdb004 "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select item,prQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        ''purchase request Not Appove 
        SQL = " select pmdb004 item,sum(pmdb006) pr " &
              "  from pmdb_t " &
              "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
              "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
              "  left join imaa_t on imaa001=pmdb004 and imaaent=pmdbent " &
              "  left join imaal_t on imaal001=pmdb004 and imaalent=pmdbent" &
              "  where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'N' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' " & WHR &
              "  group by pmdb004 order by pmdb004 "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("pr")
            USQL = " if exists(select item,prQtyNot from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set prQtyNot='" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,prQtyNot)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'purchase order
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdn001,1,1) in ('G','M','A') AND SUBSTR(pmdn001,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdn001,1,1) not in ('G','M','A') AND SUBSTR(pmdn001,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdn001", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        '(PO)
        SQL = "  Select  pmdn001 item,'2' poType,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='3' and pmdn045 = '1' " &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
            " group by pmdn001   order by pmdn001 "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")
            Dim fldPO As String = "poManQty"
            Select Case Program.Rows(i).Item("poType")
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
            End Select

            USQL = " if exists(select item,poQty,poForQty,poMoQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        'PO ConfirmDate 
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdn001,1,1) in ('G','M','A') AND SUBSTR(pmdn001,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdn001,1,1) not in ('G','M','A') AND SUBSTR(pmdn001,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdn001", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        SQL = "select distinct pmdn001 item,TO_CHAR(pmdn012,'yyyyMMdd') confirmdate " &
           " from pmdn_t " &
           " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
           " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
           " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
           " where pmdnent='3' and pmdn045 = '1' " &
           " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
           " order by pmdn001 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Mdate = Program.Rows(i).Item("confirmdate").ToString.Replace("'", " ")
            USQL = " if exists(select item,confirmdate from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set confirmdate='" & Mdate & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,confirmdate)values ('" & item & "','" & Mdate & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        ''PO PlanDate 
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdn001,1,1) in ('G','M','A') AND SUBSTR(pmdn001,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdn001,1,1) not in ('G','M','A') AND SUBSTR(pmdn001,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdn001", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        SQL = " select distinct pmdn001 item,TO_CHAR(pmdn014,'yyyyMMdd') plandate " &
           " from pmdn_t " &
           " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
           " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
           " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
           " where pmdnent='3' and pmdn045 = '1' " &
           " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
           " order by pmdn001 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Mdate = Program.Rows(i).Item("plandate")
            USQL = " if exists(select item,plandate from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set plandate='" & Mdate & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,plandate)values ('" & item & "','" & Mdate & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next
        'Purchase receipt inspection
        WHR = ""
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdt006,1,1) in ('G','M','A') AND SUBSTR(pmdt006,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdt006,1,1) not in ('G','M','A') AND SUBSTR(pmdt006,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdt006", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec

        SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') " & WHR &
            " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "

        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po_insp")
            USQL = " if exists(select item,poRcpQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                   " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

    End Sub

    Protected Sub SearchBySupplier(ByVal tempTable As String)
        Dim CHK As String = ""
        CHK = valPower(cblCodeType)
        Dim SQL As String = "",
        WHR As String = "",
        ISQL As String = "",
        USQL As String = "",
        CodeType As String = cblCodeType.Text,
        Condition As String = ddlCondition.Text,
        Code As String = tbCode.Text.Trim,
        Spec As String = tbSpec.Text.Trim,
        Desc As String = tbDesc.Text.Trim,
        EndDate As String = configDate.dateFormat2(tbEndDate.Text)
        Dim Program As New DataTable
        Dim Titem As String = "",
            item As String = "",
            Qty As Integer = 0

        'purchase order
        If CodeType <> "" Then
            WHR = WHR & ("and ((SUBSTR(pmdn001,1,1) in ('G','M','A') AND SUBSTR(pmdn001,2,1) in (" & CHK & ")) OR  (( SUBSTR(pmdn001,1,1) not in ('G','M','A') AND SUBSTR(pmdn001,3,1) in (" & CHK & ") )))")
        End If
        WHR = WHR & Conn_SQL.Where("pmdn001", tbCode) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", tbDesc) 'Desc
        WHR = WHR & Conn_SQL.Where("imaal004", tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("pmdl004", tbSup) 'Supplier
        'Default จากจำนวน PO อย่างเดียว( select case= 2)
        SQL = " select  pmdn001 item,'2' poType,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='3' and pmdn045 = '1' and pmdlstus <> 'C'" &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
            " group by pmdn001   order by pmdn001 "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            item = Program.Rows(i).Item("item")
            Qty = Program.Rows(i).Item("po")

            Dim fldPO As String = "poManQty"
            Select Case Program.Rows(i).Item("poType")
                Case "2"
                    fldPO = "poQty"
                Case "4"
                    fldPO = "poForQty"
                Case "5"
                    fldPO = "poMoQty"
            End Select

            USQL = " if exists(select itme,poQty,poForQty,poMoQty from " & tempTable & " where item='" & item & "' ) " &
                   " update " & tempTable & " set " & fldPO & "= " & fldPO & "+'" & Qty & "' where item='" & item & "' else " &
                   " insert into " & tempTable & "(item," & fldPO & ")values ('" & item & "','" & Qty & "')"
            clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        Next

        SQL = " select  item from " & tempTable & ""
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.Rows.Count - 1
            Titem = Trim(Program.Rows(i).Item("item"))
            'MO issue mat 
            WHR = Conn_SQL.Where("TO_CHAR(sfaa019,'yyyyMMdd')", "", EndDate) 'Planed start date
            Dim dt As New DataTable
            SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issueQty from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " where sfbaent = '3' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' " & WHR & " and sfba005 ='" & Titem & "'" &
              " group by sfba005"
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("issueQty")
                    USQL = " if exists(select item,issueQty from " & tempTable & " where item='" & item & "' ) " &
                           " update " & tempTable & " set issueQty='" & Qty & "' where item='" & item & "' else " &
                           " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'MO 
            SQL = " select sfaa010 item,SUM(sfaa012-sfaa050) moQty " &
              " from sfaa_t " &
              " left join imaal_t on imaal001 = sfaa010 and imaalent=sfaaent " &
              " where sfaaent='3' and sfaastus ='F'and sfaa012-sfaa050 > 0 and sfaa010 ='" & Titem & "'" &
              " group by sfaa010 order by sfaa010 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("issueQty")
                    USQL = " if exists(select item,moQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set moQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,moQty)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'Sale Order
            WHR = ""
            WHR = Conn_SQL.Where("TO_CHAR(xmdd011,'yyyyMMdd')", "", EndDate)
            SQL = " select xmdc001 item,sum(xmdc007-xmdd014) so " &
           " from  xmda_t " &
           " left join xmdc_t on xmdadocno = xmdcdocno and xmdcent=xmdaent and xmdcsite=xmdasite " &
           " left join imaal_t on imaal001 = xmdc001 and imaalent=xmdaent and imaal002= 'en_US' " &
           " left join xmdd_t on xmdddocno = xmdcdocno and xmdd001 = xmdc001 and xmddent=xmdaent and xmddsite=xmdasite " &
           " where xmdaent='3' and xmdasite='JINPAO' and xmdc045 not in ('2','3') and xmdastus ='Y' " & WHR & " and xmdc001 ='" & Titem & "'" &
           " group by xmdc001 order by xmdc001 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("so")
                    USQL = " if exists(select item,delQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set delQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,delQty)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'purchase request Appove 
            SQL = " select pmdb004 item,sum(pmdb006) pr " &
              "  from pmdb_t " &
              "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
              "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
              "  where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'Y' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' and pmdb004 ='" & Titem & "'" &
              "  group by pmdb004 order by pmdb004 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("pr")
                    USQL = " if exists(select item,prQty from " & tempTable & " where item='" & item & "' ) " &
                       " update " & tempTable & " set prQty='" & Qty & "' where item='" & item & "' else " &
                       " insert into " & tempTable & "(item,prQty)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'purchase request Not Appove 
            SQL = " select pmdb004 item,sum(pmdb006) pr " &
             "  from pmdb_t " &
             "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
             "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
             "  where pmdbent='3' and pmdbsite='JINPAO'and pmdastus= 'N' and pmdb032 not in ('2','4') and SUBSTR(pmdadocno,3,2) like '31%' and pmdb004 ='" & Titem & "'" &
             "  group by pmdb004 order by pmdb004 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("pr")
                    USQL = " if exists(select item,prQtyNot from " & tempTable & " where item='" & item & "' ) " &
                           " update " & tempTable & " set prQtyNot='" & Qty & "' where item='" & item & "' else " &
                           " insert into " & tempTable & "(item,prQtyNot)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'Purchase receipt inspection
            SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
            " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            " and SUBSTR(pmdsdocno,3,2) in ('34','37') and pmdt006 ='" & Titem & "'" &
            " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "
            dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    item = .Item("item")
                    Qty = .Item("po_insp")
                    USQL = " if exists(select item,poRcpQty from " & tempTable & " where item='" & item & "' ) " &
                           " update " & tempTable & " set poRcpQty='" & Qty & "',poQty=poQty-" & Qty & " where item='" & item & "' else " &
                           " insert into " & tempTable & "(item,poRcpQty)values ('" & item & "','" & Qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If
        Next
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("hplShow"), HyperLink)
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("JP Item")) Then
                    Dim link As String = ""
                    Dim jpPart As String = .DataItem("JP Item")
                    link = link & "&JPPart= " & jpPart
                    link = link & "&JPSpec= " & Conn_SQL.EncodeTo64UTF8(.DataItem("JP Spec")) 'JP Spec
                    link = link & "&issueQty= " & .DataItem("MO Issue(-)") 'MO Issue(-)
                    link = link & "&stock= " & .DataItem("Stock(+)") 'Stock(+)
                    link = link & "&poRcpQty= " & .DataItem("PO Insp.(+)") 'PO Insp.(+)
                    link = link & "&poQty= " & .DataItem("PO(+)") 'PO(+)
                    link = link & "&poManQty= " & .DataItem("PO Manual(+)") 'PO Manual(+)
                    link = link & "&poForQty= " & .DataItem("PO Forcast(+)") 'PO Forcast(+)
                    link = link & "&poMoQty= " & .DataItem("PO MO(+)") 'PO MO(+)
                    link = link & "&prQty= " & .DataItem("PR(+)App") 'PR(+)App
                    link = link & "&moQty= " & .DataItem("MO Rcv(+)") 'MO Rcv(+)
                    link = link & "&delQty= " & .DataItem("SO(-)") 'SO(-)
                    link = link & "&mainsup= " & .DataItem("Main Supp.") 'Main Supp.
                    link = link & "&fixtime= " & .DataItem("Lead Time(Day)") 'Fixed Lead Time
                    link = link & "&endDate= " & configDate.dateFormat2(tbEndDate.Text)
                    hplDetail.NavigateUrl = "MaterailsCallInPopup.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", jpPart)

                End If
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsCallIn" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class