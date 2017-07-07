Imports System.Globalization
Public Class MaterialsReview
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnect
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New T100CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Function strToDate(ByVal strDate As String, Optional ByVal dateFormat As String = "yyyyMMdd") As Date
        Return DateTime.ParseExact(strDate, dateFormat, New CultureInfo("en-US"))
    End Function

    Function getFirstWeek(ByVal selDate As Date) As Date
        Return selDate.AddDays(-(selDate.DayOfWeek - DayOfWeek.Sunday))
    End Function

    Function getLastWeek(ByVal selDate As Date) As Date
        Return selDate.AddDays(-(selDate.DayOfWeek - DayOfWeek.Saturday))
    End Function

    Function getWeek(ByVal selDate As Date) As Integer
        Return DatePart("ww", selDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
    End Function

    Sub addDataWeek(ByRef selWeek As Hashtable, ByVal strWeek As String, ByVal endWeek As String, ByVal strYear As String)
        For i As Integer = strWeek To endWeek
            selWeek.Add(strYear & i, strYear & "-" & i)
        Next
    End Sub

    Private Function strFormat(ByVal val As Object) As String
        Dim show As String = ""
        If CDec(val) <> 0 Then
            show = String.Format("{0:n2}", val)
        Else
            show = String.Format("", val)
        End If
        Return show
    End Function

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "tempMaterialReview" & Session("UserName"),
           tempTable2 As String = "tempBOMReview" & Session("UserName")

        Dim date1 As String = tbDateFrom.Text.Replace("/", "").Trim,
            date2 As String = tbDateTo.Text.Replace("/", "").Trim,
            strDate As String = "",
            endDate As String = ""

        'Begin date
        If date1 <> "" Then
            strDate = date1
        Else
            strDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        'strDate = strDate & "01"

        'End date
        If date2 <> "" Then
            endDate = date2
        Else
            endDate = Date.Today.ToString("yyyyMMdd", New CultureInfo("en-US"))
        End If
        ' endDate = endDate & "31"

        Dim beginDate As Date = getFirstWeek(strToDate(strDate))
        tbDateFrom.Text = beginDate.ToString("yyyy/MM/dd", New CultureInfo("en-US"))

        Dim lastDate As Date = getLastWeek(strToDate(endDate))
        tbDateTo.Text = lastDate.ToString("yyyy/MM/dd", New CultureInfo("en-US"))

        'get fld week
        Dim selWeek As Hashtable = New Hashtable,
            strYear As String = beginDate.ToString("yyyy", New CultureInfo("en-US")),
            endYear As String = lastDate.ToString("yyyy", New CultureInfo("en-US")),
            strWeek As Integer = 0,
            endWeek As Integer = 0

        Dim beforeDate As Date = New Date,
            afterDate As Date = New Date
        For i As Integer = strYear To endYear
            If i = strYear Then
                beforeDate = beginDate
            Else
                beforeDate = strToDate(i & "0101")
            End If
            If i = endYear Then
                afterDate = lastDate
            Else
                afterDate = strToDate(i & "1231")
            End If
            addDataWeek(selWeek, getWeek(beforeDate), getWeek(afterDate), i)
        Next

        CreateTempTable.createTempMatReview(tempTable, selWeek)
        CreateTempTable.createtempBOMReview(tempTable2)

        If tbItem.Text <> "" Or tbSpec.Text <> "" Then
            genDataItem(tempTable, tempTable2, beginDate.ToString("yyyyMMdd", New CultureInfo("en-US")), lastDate.ToString("yyyyMMdd", New CultureInfo("en-US")))
        Else
            genDataWeek(tempTable, beginDate.ToString("yyyyMMdd", New CultureInfo("en-US")), lastDate.ToString("yyyyMMdd", New CultureInfo("en-US")))
        End If
        'show Data
        Dim dtShow As Data.DataTable = New DataTable
        dtShow.Columns.Add(New DataColumn("Msg1"))
        dtShow.Columns.Add(New DataColumn("Mat Detail"))
        dtShow.Columns.Add(New DataColumn("Msg4"))
        dtShow.Columns.Add(New DataColumn("Detail"))
        dtShow.Columns.Add(New DataColumn("Msg2"))
        dtShow.Columns.Add(New DataColumn("Qty"))
        dtShow.Columns.Add(New DataColumn("Msg3"))
        dtShow.Columns.Add(New DataColumn("Before"))

        Dim fldSumIssue As String = "issueQty",
            fldSumPlan As String = "planQty",
            fldSumPo As String = "poQty-poRcpQty",
            fldSumPoCon As String = "poConQty"

        For i As Integer = strYear To endYear
            If i = strYear Then
                beforeDate = beginDate
            Else
                beforeDate = strToDate(i & "0101")
            End If
            If i = endYear Then
                afterDate = lastDate
            Else
                afterDate = strToDate(i & "1231")
            End If
            Dim firstWeek As Integer = getWeek(beforeDate)
            Dim lastWeek As Integer = getWeek(afterDate)
            For j As Integer = firstWeek To lastWeek
                dtShow.Columns.Add(New DataColumn("W " & j & "/" & i))
                fldSumIssue = fldSumIssue & "+issue" & i & j
                fldSumPlan = fldSumPlan & "+plan" & i & j
                fldSumPo = fldSumPo & "+po" & i & j
                fldSumPoCon = fldSumPoCon & "+poCon" & i & j
            Next
        Next
        dtShow.Columns.Add(New DataColumn("sum"))
        Dim fld As String = "," & fldSumIssue & " as sumIssue," & fldSumPlan & " as sumPlan ," & fldSumPo & " as sumPo ," & fldSumPoCon & " as sumPoCon "
        Dim dr1 As DataRow,
            dr2 As DataRow,
            dr3 As DataRow,
            dr4 As DataRow,
            dr5 As DataRow
        Dim whr As String = " "
        If cbSum.Checked Then
            Dim fld_whr As String = "stockQty+poRcpQty"
            If cbPR.Checked Then
                fld_whr = fld_whr & "+prQty"
            End If
            fld_whr = fld_whr & "-(" & fldSumIssue & ")-(" & fldSumPlan & ")"
            If cbPO.Checked Then
                fld_whr = fld_whr & "+(" & fldSumPo & ")"
            End If
            whr = " where " & fld_whr & "<0"
        End If

        Dim SQL As String = ""
        Dim dt As New DataTable
        Dim dc As New DataTable

        SQL = "select *" & fld & " from " & tempTable & ""
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        With dt
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim fgcode As String = ""
                    Dim fgDesc As String = ""
                    Dim fgSpec As String = ""
                    Dim venders As String = ""
                    Dim fixlead As String = ""
                    Dim lastPur As String = ""
                    Dim fgunit As String = ""
                    Dim sSQL As String = ""

                    sSQL = "select imaa001 item,imaal003 descr,imaal004 spec,imaa006 unit,imaf153 sup,pmaal004 supdesc,imaa006 unit,imaf171 fixlead,NVL(imai021,0) lastpurchase from imaal_t " &
                    " left join imaa_t on imaa001=imaal001 left join imaf_t on imaf001=imaa001 left join pmaal_t on pmaal001= imaf153 " &
                    " left join imai_t on imai001=imaa001" &
                    " where imaalent='3' and imaaent='3' and imafent='3' and pmaalent='3' and imaient='3' and imaisite='JINPAO' and imafsite='JINPAO' and imaal001='" & Trim(.Item(0)) & "' order by imaal001"
                    dc = clsDBConnect.QueryDataTable(sSQL, clsDBConnect.T100)
                    clsDBConnect.Close(clsDBConnect.T100)
                    If dc.Rows.Count > 0 Then
                        fgcode = dc.Rows(0).Item("item") 'item
                        fgDesc = dc.Rows(0).Item("descr") 'Desc
                        fgSpec = dc.Rows(0).Item("spec") 'Spec
                        venders = dc.Rows(0).Item("sup").ToString
                        fixlead = dc.Rows(0).Item("fixlead")  'fix lead time
                        lastPur = dc.Rows(0).Item("lastpurchase") 'Last Purchase Price-Price(O/C)
                        fgunit = dc.Rows(0).Item("unit")  'unit

                        'item
                        dr1 = dtShow.NewRow()
                        dr1("Msg1") = "Item"
                        dr1("Mat Detail") = fgcode
                        dr1("Msg4") = "Vendors Code"
                        dr1("Detail") = venders
                        dr1("Msg2") = "Stock Mat (MRB = " & .Item("stockMRBQty") & ")"
                        dr1("Qty") = strFormat(.Item("stockQty") + .Item("stockMRBQty"))
                        dr1("Msg3") = "Issue(MO)"
                        dr1("Before") = strFormat(.Item("issueQty"))

                        'desc
                        dr2 = dtShow.NewRow()
                        dr2("Msg1") = "Desc"
                        dr2("Mat Detail") = fgDesc
                        dr2("Msg4") = "Fixed Lead Time"
                        dr2("Detail") = fixlead 'Fix Lead Time
                        dr2("Msg2") = "PR"
                        dr2("Qty") = strFormat(.Item("prQty"))
                        dr2("Msg3") = "Plan(Forecast)"
                        dr2("Before") = strFormat(.Item("planQty"))

                        'spec
                        dr3 = dtShow.NewRow()
                        dr3("Msg1") = "Spec"
                        dr3("Mat Detail") = fgSpec
                        dr3("Msg4") = ""
                        dr3("Detail") = ""
                        dr3("Msg2") = "Inspection Qty"
                        dr3("Qty") = strFormat(.Item("poRcpQty"))
                        dr3("Msg3") = "Plan PO"
                        dr3("Before") = strFormat(.Item("poQty"))
                        Dim bal As Decimal = .Item("stockQty") + .Item("poRcpQty") - .Item("issueQty") - .Item("planQty")
                        If cbPR.Checked Then
                            bal = bal + .Item("prQty")
                        End If
                        If cbPO.Checked Then
                            bal = bal + .Item("poQty") - .Item("poRcpQty")
                        End If
                        dr4 = dtShow.NewRow()
                        dr4("Msg1") = "Unit"
                        dr4("Mat Detail") = fgunit
                        dr4("Msg4") = ""
                        dr4("Detail") = ""
                        dr4("Msg2") = "Last Price"
                        dr4("Qty") = strFormat(lastPur) 'Last Price
                        dr4("Msg3") = "Confirmed PO" '"Shortage"
                        dr4("Before") = "" 'strFormat(bal)

                        Dim StatusItem As String = ""
                        SQL = " select pmdj002 from pmdi_t left join pmdj_t on pmdidocno=pmdjdocno where pmdient='3' and pmdjent='3' and  " &
                                " pmdisite='JINPAO' and pmdjsite='JINPAO' and SUBSTR(pmdidocno,3,2) in ('32') and pmdistus = 'Y' and " &
                                " pmdj002  = '" & Trim(.Item("item")) & "'"
                        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                        clsDBConnect.Close(clsDBConnect.T100)
                        If dt.Rows.Count = 0 Then
                            StatusItem = "NPI"
                        End If

                        dr5 = dtShow.NewRow()

                        dr5("Msg1") = "Status"
                        dr5("Mat Detail") = StatusItem
                        dr5("Msg4") = ""
                        dr5("Detail") = ""
                        dr5("Msg2") = "Bal"
                        dr5("Qty") = strFormat(.Item("stockQty") + .Item("poRcpQty") + .Item("prQty"))
                        dr5("Msg3") = "Shortage"
                        dr5("Before") = strFormat(bal)

                        For year As Integer = strYear To endYear
                            If year = strYear Then
                                beforeDate = beginDate
                            Else
                                beforeDate = strToDate(year & "0101")
                            End If
                            If year = endYear Then
                                afterDate = lastDate
                            Else
                                afterDate = strToDate(year & "1231")
                            End If
                            Dim firstWeek As Integer = getWeek(beforeDate)
                            Dim lastWeek As Integer = getWeek(afterDate)
                            For week As Integer = firstWeek To lastWeek
                                Dim colName As String = "W " & week & "/" & year
                                Dim fldName As String = year & week
                                bal = bal - .Item("issue" & fldName) - .Item("plan" & fldName)
                                If cbPO.Checked Then
                                    bal = bal + .Item("po" & fldName)
                                End If
                                dr1(colName) = strFormat(.Item("issue" & fldName))
                                dr2(colName) = strFormat(.Item("plan" & fldName))
                                dr3(colName) = strFormat(.Item("po" & fldName))
                                dr4(colName) = strFormat(.Item("poCon" & fldName))
                                dr5(colName) = strFormat(bal)
                            Next
                        Next
                        dr1("Sum") = strFormat(.Item("sumIssue"))
                        dr2("Sum") = strFormat(.Item("sumPlan"))
                        dr3("Sum") = strFormat(.Item("sumPo"))
                        dr4("Sum") = strFormat(.Item("sumPoCon"))
                        dr5("Sum") = ""
                        dtShow.Rows.Add(dr1)
                        dtShow.Rows.Add(dr2)
                        dtShow.Rows.Add(dr3)
                        dtShow.Rows.Add(dr4)
                        dtShow.Rows.Add(dr5)
                        bal = 0
                    End If
                End With
            Next
            gvShow.DataSource = dtShow
            gvShow.DataBind()
        End With
        lbCount.Text = gvShow.Rows.Count / 5
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub genDataItem(ByVal tempTable As String, ByVal tempTable2 As String, ByVal strDate As String, ByVal endDate As String)
        'gent from BOM
        Dim SQL As String = "",
            WHR As String = "",
            USQL As String = ""
        If tbItem.Text.Trim <> "" Then
            WHR = WHR & " and bmba001 like '" & tbItem.Text.Trim & "%'"
        End If
        If tbSpec.Text.Trim <> "" Then
            WHR = WHR & " and imaal004 like '" & tbSpec.Text.Trim & "%'"
        End If

        SQL = " select bmba001 from bmba_t left join imaf_t on imaf001=bmba001 left join imaal_t on imaal001=imaf001" &
            " where bmbaent='3' and bmbasite='JINPAO' and imafent='3' and imafsite='JINPAO' and imaalent='3'" &
            " and imaf013='2' " & WHR

        Dim Program As New DataTable
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            CodeBOM(tempTable, tempTable2, Program.Rows(i).Item("bmba001"))
        Next

        Dim TSQL As String = ""
        TSQL = " Select item from " & tempTable & " "
        Program = clsDBConnect.QueryDataTable(TSQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.rows.count - 1
            With Program.Rows(i)
                Dim Item As String = ""
                Dim qty As Integer = 0
                Item = Trim(.Item("item"))
                'PR
                SQL = " select pmdb004 item,sum(pmdb006) pr " &
                      "  from pmdb_t " &
                      "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
                      "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
                      "  where pmdbent='" & clsDBConnect.entcode & "' and pmdbsite='" & clsDBConnect.site & "'and pmdastus= 'Y' and pmdb032 not in ('2','4')" &
                      "  and SUBSTR(pmdadocno,3,2) Like '31%' and pmdb004='" & Item & "'" &
                      "  group by pmdb004 order by pmdb004 "

                Dim dt As New DataTable
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set prQty='" & dt.Rows(0).Item("pr") & "' where item='" & dt.Rows(0).Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'Stock
                SQL = " select inag001 item,sum(inag008) stock from inag_t " &
                    " where inagent='" & clsDBConnect.entcode & "'  and inag008 > 0 " &
                    " and (SUBSTR(inag001,3,1) = '1' and inag004 in ('2201','2202','2203','2204','2209','2206') and inag001='" & Item & "') " &
                    " or (SUBSTR(inag001,3,1) = '4' and inag004 in ('2205','2206','2900','2901','2209') and inag001='" & Item & "') " &
                    " group by inag001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set stockQty='" & dt.Rows(0).Item("stock") & "' where item='" & .Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'stockMRBQty
                SQL = " select inag001 item,sum(inag008) stockMRBQty from inag_t " &
                   " where inagent='" & clsDBConnect.entcode & "'  and inag008 > 0 " &
                   " and (SUBSTR(inag001,3,1) = '1' and inag004 in ('2600') and inag001='" & Item & "') " &
                   " Or (SUBSTR(inag001,3,1) = '4' and inag004 in ('2600') and inag001='" & Item & "')" &
                   " group by inag001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set stockMRBQty='" & dt.Rows(0).Item("stockMRBQty") & "' where item='" & dt.Rows(0).Item("item") & "'"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'Purchase receipt inspection
                SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
                      " from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent " &
                      " where pmdsent='3' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
                      " and SUBSTR(pmdsdocno,3,2) in ('34','37') and pmdt006='" & Item & "'" &
                      " group by pmdt006 having sum(pmdt020) > 0 order by pmdt006 "

                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    Item = dt.Rows(0).Item("item")
                    qty = dt.Rows(0).Item("po_insp")
                    USQL = " if exists(select item,poRcpQty from " & tempTable & " where item='" & Item & "' ) " &
                           " update " & tempTable & " set poRcpQty='" & qty & "'  where item='" & Item & "' else " &
                           " insert into " & tempTable & "(item,poRcpQty)values ('" & Item & "','" & qty & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If
                'Plan Receive
                'po All and TD012 < '" & strDate & "'/* ใช้ Shipping Date แทนรอข้อมูล ConfirmDate
                SQL = " select  pmdn001 item, sum(pmdn007-pmdo019) po" &
                      " from pmdn_t " &
                      " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
                      " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
                      " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
                      " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
                      " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 and pmdn001='" & Item & "'" &
                      " and TO_Char(pmdn012,'yyyyMMdd') < '" & strDate & "' " &
                      " group by pmdn001   order by pmdn001 "

                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set poQty='" & dt.Rows(0).Item("po") & "' where item='" & dt.Rows(0).Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'po between date select
                SQL = " select pmdn001 item,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW')  weekNo,TO_CHAR(pmdn012,'yyyy') selYear,sum(pmdn007-pmdo015) po " &
                      " from pmdn_t " &
                      " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
                      " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
                      " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
                      " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
                      " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 and pmdn001='" & Item & "'" &
                      " " & Conn_SQL.Where("TO_CHAR(pmdn012,'yyyyMMdd')", strDate, endDate) &
                      " group by pmdn001,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW'),TO_CHAR(pmdn012,'yyyy') order by pmdn001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set po" & dt.Rows(0).Item("selYear") & dt.Rows(0).Item("weekNo") & "='" & dt.Rows(0).Item("po") & "' where item='" & .Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'Confirm Receive
                'po All and TD012 < '" & strDate & "'/* ใช้ Shipping Date แทนรอข้อมูล ConfirmDate
                SQL = " select  pmdn001 item, sum(pmdn007-pmdo019) po" &
                      " from pmdn_t " &
                      " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
                      " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
                      " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
                      " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
                      " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 and pmdn001='" & Item & "'" &
                      " and TO_Char(pmdn012,'yyyyMMdd') < '" & strDate & "' " &
                      " group by pmdn001   order by pmdn001 "
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set poConQty='" & dt.Rows(0).Item("po") & "' where item='" & dt.Rows(0).Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'po between date select
                SQL = " select pmdn001 item,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW')  weekNo,TO_CHAR(pmdn012,'yyyy') selYear,sum(pmdn007-pmdo015) po " &
                      " from pmdn_t " &
                      " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
                      " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
                      " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
                      " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
                      " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 and pmdn001='" & Item & "'" &
                      " " & Conn_SQL.Where("TO_CHAR(pmdn012,'yyyyMMdd')", strDate, endDate) &
                      " group by pmdn001,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW'),TO_CHAR(pmdn012,'yyyy') order by pmdn001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set poCon" & dt.Rows(0).Item("selYear") & dt.Rows(0).Item("weekNo") & "='" & dt.Rows(0).Item("po") & "' where item='" & dt.Rows(0).Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If
                ''Plan Usage
                ''<statr date
                'SQL = " select itemMAT as item,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from DBMIS.dbo." & tempTable2 & " T " &
                '      " left join LRPTA on  TA002=itemParent " &
                '      " left join LRPLA on TA001=LA001  and TA050=LA012 " &
                '      " where TA007<'" & strDate & "' and  TA006>0  and TA051='N' and LA005='1'  and LA013 = '1' " &
                '      " group by itemMAT "
                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                'For i As Integer = 0 To dt.Rows.Count - 1
                '    With dt.Rows(i)
                '        USQL = " update " & tempTable & " set planQty='" & .Item("planQty") & "' where item='" & .Item("item") & "'  "
                '        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                '    End With
                'Next
                ''between start date and end date
                'SQL = " select itemMAT as item,DATEPART(week,TA007) as weekNo,DATEPART(yyyy,TA007) as selYear,sum((isnull(TA006,0)*qty)+CEILING(isnull(TA006,0)*qty*scrapRatio)) as planQty from DBMIS.dbo." & tempTable2 & " T " &
                '      " left join LRPTA on  TA002=itemParent " &
                '      " left join LRPLA on TA001=LA001  and TA050=LA012 " &
                '      " where TA006>0   and TA051='N' and LA005='1' and LA013 = '1' and TA007 between  '" & strDate & "' and '" & endDate & "'   " &
                '      " group by itemMAT,DATEPART(week,TA007),DATEPART(yyyy,TA007) "

                'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
                'For i As Integer = 0 To dt.Rows.Count - 1
                '    With dt.Rows(i)
                '        USQL = " update " & tempTable & " set plan" & .Item("selYear") & .Item("weekNo") & "='" & .Item("planQty") & "' where item='" & .Item("item") & "'  "
                '        Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                '    End With
                'Next

                'plan Issue
                '< from date
                SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issue from sfba_t  " &
                      " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
                      " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
                      " where sfbaent = '" & clsDBConnect.entcode & "' and (sfba016+sfba025) < sfba013 and sfaastus = 'F' and sfba005='" & Item & "'" &
                      " group by sfba005"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    USQL = " update " & tempTable & " set issueQty='" & dt.Rows(0).Item("issue") & "' where item='" & dt.Rows(0).Item("item") & "'  "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If

                'between from date and to date
                SQL = " select sfba005 item,to_char(to_date(sfaa015,'yyyy/MM/dd'),'WW')  weekNo,TO_CHAR(sfaa015,'yyyy') selYear,sum(sfba013-(sfba016+sfba025)) issue from sfba_t  " &
                      " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
                      " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
                      " where sfbaent = '" & clsDBConnect.entcode & "' and (sfba016+sfba025) < sfba013 and sfaastus = 'F'" &
                      " and TO_CHAR(sfaa015,'yyyyMMdd') between  '" & strDate & "' and '" & endDate & "' and sfba005='" & Item & "'" &
                      " group by sfba005,to_char(to_date(sfaa015,'yyyy/MM/dd'),'WW'),TO_CHAR(sfaa015,'yyyy') order by sfba005 "
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If dt.Rows.Count > 0 Then
                    Dim fldName As String = "issue" & dt.Rows(0).Item("selYear") & dt.Rows(0).Item("weekNo")
                    USQL = " update " & tempTable & " set issue" & .Item("selYear") & dt.Rows(0).Item("weekNo") & " ='" & dt.Rows(0).Item("issue") & "' where item='" & dt.Rows(0).Item("item") & "' "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If
            End With
        Next

    End Sub

    Protected Sub genDataWeek(ByVal tempTable As String, ByVal strDate As String, ByVal endDate As String)

        Dim SQL As String = "",
            USQL As String = "",
            WHR As String = "",
            dt As New DataTable,
           item As String = "",
           qty As Decimal = 0,
           fld As String = "",
           lstItem As String = "",
           fldInsHash As Hashtable = New Hashtable,
           whrHash As Hashtable = New Hashtable,
           fldUpdHash As Hashtable = New Hashtable

        'PR
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and pmdb004 like '" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and imaal003 like '" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and imaal004 like '" & tbMatSpec.Text.Trim & "%' "
        End If
        If ddlTypeMat.Text <> "0" Then
            WHR = WHR & " and LENGTH(pmdb004)>10 and SUBSTR(pmdb004,3,1) like '" & ddlTypeMat.Text & "%' "
        Else
            WHR = WHR & " and LENGTH(pmdb004)>10 and SUBSTR(pmdb004,3,1) in ('1','4') "
        End If

        SQL = " select pmdb004 item,sum(pmdb006) pr " &
              "  from pmdb_t " &
              "  left join pmda_t on pmdbdocno=pmdadocno and pmdaent=pmdbent and pmdasite=pmdbsite " &
              "  left join pmdl_t on pmdl008=pmdadocno and pmdlent=pmdbent and pmdlsite=pmdbsite and pmdlstus not in ('Y','C') " &
              "  where pmdbent='" & clsDBConnect.entcode & "' and pmdbsite='" & clsDBConnect.site & "' and pmdastus= 'Y' and pmdb032 not in ('2','4') " &
              "  and SUBSTR(pmdadocno,3,2) Like '31%' " & WHR &
              "  group by pmdb004 order by pmdb004 "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item(0).ToString.Trim 'item
                qty = .Item(1).ToString.Trim 'pr
                whrHash.Add("item", item)
                fldInsHash.Add("prQty", qty) ' fg item
                fldUpdHash.Add("prQty", "'" & qty & "'") ' fg item
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'Purchase receipt inspection
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and pmdt006 like '" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and imaal003 like '" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and imaal004 like '" & tbMatSpec.Text.Trim & "%' "
        End If
        If ddlTypeMat.Text <> "0" Then
            WHR = WHR & " and LENGTH(pmdt006)>10 and SUBSTR(pmdt006,3,1) like '" & ddlTypeMat.Text & "%' "
        Else
            WHR = WHR & " and LENGTH(pmdt006)>10 and SUBSTR(pmdt006,3,1) in ('1','4') "
        End If


        SQL = " select pmdt006 item,sum(pmdt020) po_insp " &
            "  from pmds_t left join pmdt_t on pmdsdocno=pmdtdocno and pmdsent=pmdtent" &
            "  where pmdsent= '" & clsDBConnect.entcode & "' and ((pmds000=1 and pmdsstus<>'Y') or (pmds000=6 and pmdsstus<>'S')) " &
            "  and SUBSTR(pmdsdocno,3,2) in ('34','37')" & WHR &
            " " & Conn_SQL.Where("TO_CHAR(pmdsdocdt,'yyyyMMdd')", strDate, endDate) &
            "  group by pmdt006 HAVING SUM(PMDT020) > 0 " &
            "  order by pmdt006"

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item(0).ToString.Trim
                qty = .Item(1).ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("poRcpQty", qty) ' fg item
                fldUpdHash.Add("poRcpQty", "'" & qty & "'") ' fg item
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'Plan Receive
        'po All and TD012 < '" & strDate & "'
        'PO สถานะ Not Cloesd
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and pmdn001 like '" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and imaal003 like '" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and imaal004 like '" & tbMatSpec.Text.Trim & "%' "
        End If
        If ddlTypeMat.Text <> "0" Then
            WHR = WHR & " and LENGTH(pmdn001)>10 and SUBSTR(pmdn001,3,1) like '" & ddlTypeMat.Text & "%' "
        Else
            WHR = WHR & " and LENGTH(pmdn001)>10  and SUBSTR(pmdn001,3,1) in('1','4') "
        End If

        SQL = " select pmdn001 item,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' and pmdlstus <> 'C'" &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
            " and TO_Char(pmdn012,'yyyyMMdd') < '" & strDate & "' " &
            " group by pmdn001   order by pmdn001 "

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item(0).ToString.Trim
                qty = .Item(1).ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("poQty", qty) ' fg item
                fldUpdHash.Add("poQty", "'" & qty & "'") ' fg item
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'po between date select
        lstItem = ""
        SQL = " select pmdn001 item,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW')  weekNo,TO_CHAR(pmdn012,'yyyy') selYear,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' and pmdlstus <> 'C'" &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " &
             " " & Conn_SQL.Where("TO_CHAR(pmdn012,'yyyyMMdd')", strDate, endDate) & " " & WHR &
            " group by pmdn001,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW'),TO_CHAR(pmdn012,'yyyy')   order by pmdn001 "

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item(0).ToString.Trim
                qty = .Item(3).ToString.Trim
                fld = "po" & .Item(2) & .Item(1)
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) ' fg item
                fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        End If
        lstItem = ""
        ' replace(TD014,'-','')
        'Confirm PO Date
        'add confirm po date <> ''

        SQL = " select pmdn001 item,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " & WHR &
            " and TO_Char(pmdn012,'yyyyMMdd') < '" & strDate & "' " &
            " group by pmdn001   order by pmdn001 "
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item(0).ToString.Trim
                qty = .Item(1).ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("poConQty", qty) ' fg item
                fldUpdHash.Add("poConQty", "'" & qty & "'") ' fg item
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next
        'po between date select
        'add confirm po date <> ''
        lstItem = ""
        SQL = " select pmdn001 item,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW')  weekNo,TO_CHAR(pmdn012,'yyyy') selYear,sum(pmdn007-pmdo019) po " &
            " from pmdn_t " &
            " left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 and pmdoent=pmdnent " &
            " left join pmdl_t on pmdldocno=pmdndocno and pmdlent=pmdoent " &
            " left join imaal_t on imaal001=pmdn001 and imaalent=pmdnent " &
            " where pmdnent='" & clsDBConnect.entcode & "' and pmdn045 = '1' " &
            " and SUBSTR(pmdndocno,3,2) = '33' and (pmdn007-pmdo019) > 0 " &
            " " & Conn_SQL.Where("TO_CHAR(pmdn012,'yyyyMMdd')", strDate, endDate) & " " & WHR &
            " group by pmdn001,to_char(to_date(pmdn012,'yyyy/MM/dd'),'WW'),TO_CHAR(pmdn012,'yyyy')   order by pmdn001 "

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item(0).ToString.Trim
                qty = .Item(3).ToString.Trim
                fld = "poCon" & .Item(2) & .Item(1)
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) ' fg item
                fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        End If
        'lstItem = ""

        ''Plan Usage
        ''<statr date   on  TA002=itemParent
        'WHR = ""
        'If tbMatItem.Text.Trim <> "" Then
        '    WHR = WHR & " and MD003 like '" & tbMatItem.Text.Trim & "%' "
        'End If
        'If tbMatDesc.Text.Trim <> "" Then
        '    WHR = WHR & " and MB002 like '" & tbMatDesc.Text.Trim & "%' "
        'End If
        'If tbMatSpec.Text.Trim <> "" Then
        '    WHR = WHR & " and MB003 like '" & tbMatSpec.Text.Trim & "%' "
        'End If
        'If ddlTypeMat.Text <> "0" Then
        '    WHR = WHR & " and substring(MD003,3,1) like '" & ddlTypeMat.Text & "%' "
        'Else
        '    WHR = WHR & " and SUBSTRING(MD003,3,1) in('1','4') "
        'End If

        'SQL = " select MD003,sum((isnull(TA006,0)* isnull(MD006,0))+CEILING(isnull(TA006,0)* isnull(MD006,0)*MD008) ) as planQty from LRPTA " &
        '      " right join BOMMD on MD001=TA002  " &
        '      " left join INVMB on MB001=MD003 " &
        '      " left join LRPLA on TA001=LA001  and TA050=LA012 " &
        '      " where MB025='P' and  TA006>0 and TA051 = 'N'   and LA005='1' and LA013 = '1'  and TA007<'" & strDate & "' " & WHR &
        '      " group by MD003 having sum( isnull(TA006,0)* isnull(MD006,0) )>0 "

        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    With dt.Rows(i)
        '        fldInsHash = New Hashtable
        '        whrHash = New Hashtable
        '        fldUpdHash = New Hashtable
        '        'whr of condition
        '        item = .Item("MD003").ToString.Trim
        '        qty = .Item("planQty").ToString.Trim
        '        whrHash.Add("item", item)
        '        fldInsHash.Add("planQty", qty) ' fg item
        '        fldUpdHash.Add("planQty", "'" & qty & "'") ' fg item
        '        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
        '    End With
        'Next
        ''between start date and end date
        'SQL = " select MD003,DATEPART(week,TA007) as weekNo,DATEPART(yyyy,TA007) as selYear,sum((isnull(TA006,0)* isnull(MD006,0))+CEILING(isnull(TA006,0)* isnull(MD006,0)*MD008) ) as planQty from LRPTA " &
        '      " right join BOMMD on MD001=TA002  " &
        '      " left join INVMB on MB001=MD003 " &
        '      " left join LRPLA on TA001=LA001  and TA050=LA012 " &
        '      " where MB025='P' and TA051 = 'N' and LA005='1' and LA013 = '1'  and  TA006>0 " & Conn_SQL.Where("TA007", strDate, endDate) & WHR &
        '      " group by MD003,DATEPART(week,TA007),DATEPART(yyyy,TA007) having sum( isnull(TA006,0)* isnull(MD006,0) )>0 "

        'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    With dt.Rows(i)
        '        item = .Item("MD003").ToString.Trim
        '        qty = .Item("planQty").ToString.Trim
        '        fld = "plan" & .Item("selYear") & .Item("weekNo")
        '        'whr of condition
        '        If lstItem <> item Then
        '            If lstItem <> "" Then
        '                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
        '            End If
        '            fldInsHash = New Hashtable
        '            whrHash = New Hashtable
        '            fldUpdHash = New Hashtable
        '            whrHash.Add("item", item)
        '        End If
        '        fldInsHash.Add(fld, qty) ' fg item
        '        fldUpdHash.Add(fld, "'" & qty & "'") ' fg item
        '        lstItem = item
        '    End With
        'Next
        'If lstItem <> "" Then
        '    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), Conn_SQL.MIS_ConnectionString)
        'End If

        lstItem = ""
        'plan Issue
        '< from date
        WHR = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR = WHR & " and sfba005 like '" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR = WHR & " and imaal003 like '" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR = WHR & " and imaal004 like '" & tbMatSpec.Text.Trim & "%' "
        End If
        If ddlTypeMat.Text <> "0" Then
            WHR = WHR & " and SUBSTR(sfba005,3,1) like '" & ddlTypeMat.Text & "%' "
        Else
            WHR = WHR & " and SUBSTR(sfba005,3,1) in('1','4') "
        End If

        'Mat issue สถานะ Not Cloesd 
        SQL = " select sfba005 item,sum(sfba013-(sfba016+sfba025)) issue from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " left join imaf_t on imaf001=sfba005 and imafent=sfbaent and imafsite=sfbasite " &
              " where sfbaent ='" & clsDBConnect.entcode & "' and sfbasite='" & clsDBConnect.site & "'" &
              " and (sfba016+sfba025) < sfba013 And sfaastus = 'F' " & WHR &
              " group by sfba005 order by sfba005 "

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                fldInsHash = New Hashtable
                whrHash = New Hashtable
                fldUpdHash = New Hashtable
                'whr of condition
                item = .Item(0).ToString.Trim
                qty = .Item(1).ToString.Trim
                whrHash.Add("item", item)
                fldInsHash.Add("issueQty", qty) ' fg item
                fldUpdHash.Add("issueQty", "'" & qty & "'") ' fg item
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next

        'between from date and to date
        'สถานะ Not Closed เลือกช่วงเวลา
        SQL = " select sfba005 item,to_char(to_date(sfaa019,'yyyy/MM/dd'),'WW') As weekNo,TO_CHAR(sfaa019,'yyyy') As selYear,sum(sfba013-(sfba016+sfba025)) issue from sfba_t  " &
              " left join imaal_t on imaal001=sfba005 and imaalent=sfbaent " &
              " left join sfaa_t on sfbadocno=sfaadocno and sfaaent=sfbaent " &
              " left join imaf_t on imaf001=sfba005 and imafent=sfbaent and imafsite=sfbasite " &
              " where sfbaent ='" & clsDBConnect.entcode & "' and sfbasite='JINPAO' and imaf013='1'" &
              " and (sfba016+sfba025) < sfba013 And sfaastus = 'F' " &
              " and TO_CHAR(sfaa019,'yyyyMMdd') between  '" & strDate & "' and '" & endDate & "' " & WHR &
              " group by sfba005,to_char(to_date(sfaa019,'yyyy/MM/dd'),'WW'),TO_CHAR(sfaa019,'yyyy') order by sfba005 "

        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                item = .Item(0).ToString.Trim
                qty = .Item(3).ToString.Trim
                fld = "issue" & .Item(2) & .Item(1)
                'whr of condition
                If lstItem <> item Then
                    If lstItem <> "" Then
                        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End If
                    fldInsHash = New Hashtable
                    whrHash = New Hashtable
                    fldUpdHash = New Hashtable
                    whrHash.Add("item", item)
                End If
                fldInsHash.Add(fld, qty) 'fg item
                fldUpdHash.Add(fld, "'" & qty & "'") 'fg item
                lstItem = item
            End With
        Next
        If lstItem <> "" Then
            clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)
        End If

        'Stock
        Dim TSQL As String = ""
        Dim program As New DataTable
        TSQL = " select T.item from " & tempTable & " T "
        program = clsDBConnect.QueryDataTable(TSQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.rows.count - 1
            With program.Rows(i)

                SQL = " select inag001 item,sum(inag008) stock from inag_t where inagent='" & clsDBConnect.entcode & "' and inag008 > 0 " &
                      " and (SUBSTR(inag001,3,1) = '1' and inag004 in ('2201','2202','2203','2204','2209','3400','2206') and inag001='" & item & "') " &
                      " Or (SUBSTR(inag001,3,1) = '4' and inag004 in ('2205','2206','2900','2901','2209','3400') and inag001='" & item & "')" &
                      " group by inag001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For j As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(j)
                        fldInsHash = New Hashtable
                        whrHash = New Hashtable
                        fldUpdHash = New Hashtable
                        'whr of condition
                        item = .Item("item").ToString.Trim
                        qty = .Item("stock").ToString.Trim
                        whrHash.Add("item", item)
                        fldInsHash.Add("stockQty", qty) ' fg item
                        fldUpdHash.Add("stockQty", "'" & qty & "'") ' fg item
                        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End With
                Next
                SQL = " select inag001 item,sum(inag008) stockMRBQty from inag_t where inagent='" & clsDBConnect.entcode & "' and inag008 > 0 " &
                      " and (SUBSTR(inag001,3,1) = '1' and inag004 in ('2600') and inag001='" & item & "') " &
                      " Or (SUBSTR(inag001,3,1) = '4' and inag004 in ('2600') and inag001='" & item & "') " &
                      " group by inag001"
                dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                For j As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(j)
                        fldInsHash = New Hashtable
                        whrHash = New Hashtable
                        fldUpdHash = New Hashtable
                        'whr of condition
                        item = .Item("item").ToString.Trim
                        qty = .Item("stockMRBQty").ToString.Trim
                        whrHash.Add("item", item)
                        fldInsHash.Add("stockMRBQty", qty) ' fg item
                        fldUpdHash.Add("stockMRBQty", "'" & qty & "'") ' fg item
                        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                        clsDBConnect.Close(clsDBConnect.MIS2)
                    End With
                Next
            End With
        Next
    End Sub

    Protected Sub CodeBOM(ByVal tempTable As String, ByVal tempTable2 As String, ByVal code As String, Optional ByVal codeParent As String = "")
        If codeParent = "" Then
            codeParent = code
        End If
        Dim SQL As String = "",
            WHR As String = ""
        If tbMatItem.Text.Trim <> "" Then
            WHR &= " and bmba003 like '" & tbMatItem.Text.Trim & "%' "
        End If
        If tbMatDesc.Text.Trim <> "" Then
            WHR &= " and imaal003 like '" & tbMatDesc.Text.Trim & "%' "
        End If
        If tbMatSpec.Text.Trim <> "" Then
            WHR &= " and imaal004 like '" & tbMatSpec.Text.Trim & "%' "
        End If
        If WHR <> "" Then
            WHR = " and (imaf013='2' or (imaf013='1' " & WHR & ")) "
        End If

        SQL = " select bmba003,bmba011,imaf013,bmba012 " &
            " from bmba_t left join imaf_t on imaf001=bmba001 " &
            " left join imaal_t on bmba001=imaal001 " &
            " where bmbaent='3' and imaalent='3' and bmbasite='JINPAO' and imafent='3' and imafsite='JINPAO' " &
            " and bmba001='" & code & "' " & WHR &
            " order by bmba003 "

        Dim Program As New DataTable
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim item As String = .Item("bmba003"), 'ParentItem
                    qty As Decimal = .Item("bmba011"), 'Qty Per
                    ScrapRatio As Decimal = .Item("bmba012") 'ScrapRatio
                If .Item("imaf013") = "1" Then 'Manufuring
                    CodeBOM(tempTable, tempTable2, item, codeParent)
                ElseIf chkCode(item) Then '2:Purchase
                    Dim fldInsHash As Hashtable = New Hashtable,
                        whrHash As Hashtable = New Hashtable,
                        fldUpdHash As Hashtable = New Hashtable
                    'whr of condition
                    Dim matCode As String = .Item("bmba003").ToString.Trim
                    whrHash.Add("item", matCode)
                    fldInsHash.Add("poQty", "0") ' fg item
                    fldUpdHash.Add("poQty", "'0'") ' fg item
                    clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, fldUpdHash, whrHash), clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)

                    Dim USQL As String = " if not exists(select itemFG,itemParent,itemMat,Qty,scrapRatio from " & tempTable2 & " where itemFG='" & codeParent & "' and itemParent='" & code & "' and itemMAT='" & matCode & "' ) " &
                                         " insert into " & tempTable2 & "(itemFG,itemParent,itemMAT,qty,scrapRatio)values ('" & codeParent & "','" & code & "','" & matCode & "','" & qty & "','" & ScrapRatio & "')"
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End If
            End With
        Next
    End Sub

    Function chkCode(ByVal code As String) As Boolean
        Dim c1 As String = code.Substring(1, 1)
        Dim res As Boolean = False
        If ddlTypeMat.Text = "0" Then
            If c1 = "1" Or c1 = "4" Then
                res = True
            End If
        Else
            Dim tm As String = ddlTypeMat.Text
            If c1 = tm Then
                res = True
            End If
        End If
        Return res
    End Function

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsReview" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class