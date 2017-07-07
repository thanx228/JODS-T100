Public Class MaterialsNotIssue
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim clsDBConnect As New clsDBConnectT100
    Dim configDate As New ConfigDate
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTableT100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = ""
            SQL = " select oobx001 Type ,oobx001||' : '||oobxl003 Name from oobx_t left join oobxl_t on oobx001=oobxl001 and oobxent=oobxlent and oobxl002='en_US' where oobxent='3' and SUBSTR(oobx001,1,2) in ('51','52','53') order by oobx001 "
            showCheckboxList(clWorkType, SQL, "Name", "Type", 4, clsDBConnect.T100)

            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If

        End If
        HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
    End Sub
    Function showCheckboxList(ByRef conCheckboxList As CheckBoxList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal showColumn As Decimal = 0, Optional ByVal connectSting As String = "") As String
        Try
            Dim dt As New DataTable
            dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
            clsDBConnect.Close(connectSting)

            With conCheckboxList
                .DataSource = dt
                .DataTextField = fldText
                .DataValueField = fldValue
                .DataBind()
                .RepeatColumns = showColumn
                .RepeatDirection = RepeatDirection.Horizontal
                .RepeatLayout = RepeatLayout.Flow
            End With
        Catch ex As Exception
        End Try
    End Function
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Dim SQL As String = "",
        SSQL As String = "",
        WHR As String = "",
        ISQL As String = "",
        codeType As String = ddlCodeType.Text,
        condition As String = ddlCondition.Text.Trim,
        program As New DataTable,
        program1 As New DataTable,
        dt As New DataTable,
        dt2 As New Data.DataTable,
        item As String = "",
        qty As Decimal = 0,
        USQL As String = "",
        SQLMain As String = "",
        SQLSUB As String = "",
        BomItem As String = "",
        MONO As String = "",
        PlanStartDate As String = "",
        ItemSpec As String = "",
        MatItem As String = "",
        MatSpec As String = "",
        RequestQty As String = "",
        IssueQty As String = "",
        BalQty As String = "",
        Unit As String = "",
        PRQty As String = "",
        POQty As String = "",
        POInspectQty As String = "",
        InventoryQty As String = ""

        Dim dtShow As Data.DataTable = New DataTable
        dtShow.Columns.Add(New DataColumn("MONO"))
        dtShow.Columns.Add(New DataColumn("PlanStartDate"))
        dtShow.Columns.Add(New DataColumn("ItemSpec"))
        dtShow.Columns.Add(New DataColumn("MatItem"))
        dtShow.Columns.Add(New DataColumn("MatSpec"))
        dtShow.Columns.Add(New DataColumn("RequestQty"))
        dtShow.Columns.Add(New DataColumn("IssueQty"))
        dtShow.Columns.Add(New DataColumn("BalQty"))
        dtShow.Columns.Add(New DataColumn("Unit"))
        dtShow.Columns.Add(New DataColumn("PRQty"))
        dtShow.Columns.Add(New DataColumn("POQty"))
        dtShow.Columns.Add(New DataColumn("POInspectQty"))
        dtShow.Columns.Add(New DataColumn("InventoryQty"))

        'Dim ShowFiled As String = "MONO,PlanStartDate, ItemSpec,MatItem,MatSpec,RequestQty,IssueQty,BalQty,Unit,PRQty,POQty,POInspectQty,InventoryQty"
        'Dim ArrayShowFiled() As String = Split(ShowFiled, ",")

        'DataTableTranf(ArrayShowFiled, dt2)
        Dim tempTable As String = "tempMaterialsNotIssue" & Session("UserName")
        CreateTempTable.createTempMatNotIssue(tempTable)

        Dim txtCodeType As String = "'1','4'"
        If codeType <> "0" Then
            txtCodeType = "'" & codeType & "'"
        End If

        'get code from Manufactor Order
        WHR = ""
        WHR = WHR & " and SUBSTR(sfba005,3,1) in (" & txtCodeType & ") " 'code Type
        WHR = WHR & Conn_SQL.Where("SUBSTR(sfbadocno,3,4)", clWorkType) 'Work Type
        WHR = WHR & Conn_SQL.Where("SUBSTR(sfbadocno,8,11)", tbWorkNo) 'Work No
        WHR = WHR & Conn_SQL.Where("sfba005", tbItem) 'Item
        WHR = WHR & Conn_SQL.Where("imaal003", "imaal004", tbSpec) 'Spec
        WHR = WHR & Conn_SQL.Where("TO_CHAR(sfaa019,'yyyyMMdd')", tbDateFrom.Text.Replace("/", "").Trim, tbDateTo.Text.Replace("/", "").Trim)
        If condition <> "0" Then 'check condition
            Select Case condition
                Case "1" ' not issue =0
                    WHR = WHR & Conn_SQL.Where("sfba016", "=0")
                Case "2" ' issue < required,issue>0
                    WHR = WHR & Conn_SQL.Where("sfba023", "> sfba016")
                    WHR = WHR & Conn_SQL.Where("sfba016", ">0")
            End Select
        End If

        'MO
        SQL = " select distinct sfba005 item " &
            " from sfba_t " &
            " left join sfaa_t on sfbadocno=sfaadocno " &
            " left join imaal_t on sfba005=imaal001 " &
            " where sfbaent='3' and sfaaent='3'and imaalent='3' and sfba023-sfba016 > 0 and sfaastus not in ('C') and sfaastus='F' " & WHR
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To dt.Rows.Count - 1
            item = dt.Rows(i).Item("item")
            ISQL = "insert into " & tempTable & " (item) values ('" & item & "')"
            clsDBConnect.QueryExecuteScalar(ISQL, clsDBConnect.MIS2)
            clsDBConnect.Close(clsDBConnect.MIS2)


            'get PR
            SSQL = " select pmdb004 item,sum(pmdb006) pr from pmdb_t left join pmda_t on pmdbdocno=pmdadocno" &
                " where pmdbent='3' and pmdaent='3' and pmdastus = 'Y' and pmdb004= '" & item & "'group by pmdb004 "
            program = clsDBConnect.QueryDataTable(SSQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If program.Rows.Count > 0 Then
                With program.Rows(0)
                    item = .Item("item")
                    qty = .Item("pr")
                    USQL = " update " & tempTable & " set prQty='" & qty & "' where item='" & item & "' "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If


            'get PO 
            SSQL = " select pmdn001 item ,sum(pmdn007-pmdo015) po from pmdn_t left join pmdo_t on pmdodocno=pmdndocno and pmdo001=pmdn001 " &
                   " left join pmdl_t on pmdldocno=pmdndocno where pmdnent='3'and pmdoent='3' and pmdlent='3' and pmdlstus='Y' and SUBSTR(pmdn001,3,1) not in ('2') and pmdn001= '" & item & "' group by pmdn001 "
            program = clsDBConnect.QueryDataTable(SSQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If program.Rows.Count > 0 Then
                With program.Rows(0)
                    item = .Item("item")
                    qty = .Item("po")
                    USQL = " update " & tempTable & " set poQty='" & qty & "' where item='" & item & "' "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'get IPQC Purchase Receipts
            SSQL = " select  pmdt006 item,sum(pmdt020) po_insp from pmdt_t left join pmds_t on pmds006=pmdtdocno " &
                   " where pmdtent='3' and pmdsent='3' and  pmdsstus not in 'S' and pmdt006='" & item & "'" &
                   " group by pmdt006 having sum(pmdt020) > 0"
            program = clsDBConnect.QueryDataTable(SSQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If program.Rows.Count > 0 Then
                With program.Rows(0)
                    item = .Item("item")
                    qty = .Item("po_insp")
                    USQL = " update " & tempTable & " set poRcpQty='" & qty & "' where item='" & item & "' "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            'get Stock
            SSQL = " select inag001 item,sum(inag008) stockQty from inag_t where inagent='3' and inag008 > 0 and " &
                   " inag004 in ('2201','2202','2204','2205','2206','2900','2901','3333') and inag001= '" & item & "' group by inag001 "
            program = clsDBConnect.QueryDataTable(SSQL, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            If program.Rows.Count > 0 Then
                With program.Rows(0)
                    item = .Item("item")
                    qty = .Item("stockQty")
                    USQL = " update " & tempTable & " set stockQty='" & qty & "' where item='" & item & "' "
                    clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                End With
            End If

            Dim WHR1 As String = ""
            Dim WHR2 As String = ""
            WHR1 = WHR1 & Conn_SQL.Where("sfba005", tbItem) 'Item
            WHR1 = WHR1 & " and SUBSTR(sfba005,3,1) in (" & txtCodeType & ") " 'code Type
            WHR1 = WHR1 & Conn_SQL.Where("SUBSTR(sfbadocno,3,4)", clWorkType) 'Work Type
            WHR1 = WHR1 & Conn_SQL.Where("SUBSTR(sfbadocno,8,11)", tbWorkNo) 'Work No
            WHR1 = WHR1 & Conn_SQL.Where("TO_CHAR(sfaa019,'yyyyMMdd')", tbDateFrom.Text.Replace("/", "").Trim, tbDateTo.Text.Replace("/", "").Trim)

            If condition <> "0" Then 'check condition
                Select Case condition
                    Case "1" ' not issue =0
                        WHR1 = WHR1 & Conn_SQL.Where("sfba016", "=0")
                    Case "2" ' issue < required,issue>0
                        WHR1 = WHR1 & Conn_SQL.Where("sfba023", "> sfba016")
                        WHR1 = WHR1 & Conn_SQL.Where("sfba016", ">0")
                End Select
            End If
            WHR2 = WHR2 & Conn_SQL.Where("imaal003", tbSpec) 'Spec
            'show data



            SQLMain = " select sfba005 Item,sfbadocno MONO,TO_CHAR(sfaa019,'yyyy/MM/dd') PlanStartDate,(select imaal004 from imaal_t where sfaa010=imaal001 and imaalent=sfbaent and  ROWNUM=1) ItemSpec, " &
                " (select imaal001 from imaal_t where sfba005=imaal001 and imaalent=sfbaent and  ROWNUM=1) MatItem, " &
                " (select imaal003 from imaal_t where sfba005=imaal001 and imaalent=sfbaent and  ROWNUM=1 " & WHR2 & ") MatSpec, " &
                " CAST(sfba023 AS DECIMAL(16,2)) RequestQty,CAST(sfba016 AS DECIMAL(16,2)) IssueQty,CAST(sfba023-sfba016 AS DECIMAL(16,2)) BalQty, sfba014 Unit" &
                " from sfba_t left join sfaa_t on sfbadocno=sfaadocno where sfbaent='3' and sfaaent='3' and sfba023-sfba016 > 0 and sfaastus not in ('C') and sfaastus='F' " &
                " " & WHR1 & ""
            program = clsDBConnect.QueryDataTable(SQLMain, clsDBConnect.T100)
            clsDBConnect.Close(clsDBConnect.T100)
            For j As Integer = 0 To program.Rows.Count - 1
                With program.Rows(j)
                    BomItem = Trim(.Item(0))
                    MONO = Trim(.Item(1))
                    PlanStartDate = Trim(.Item(2))
                    ItemSpec = Trim(.Item(3))
                    MatItem = Trim(.Item(4))
                    MatSpec = Trim(.Item(5))
                    RequestQty = Trim(.Item(6))
                    IssueQty = Trim(.Item(7))
                    BalQty = Trim(.Item(8))
                    Unit = Trim(.Item(9))

                    SQLSUB = "select prQty,poQty,poRcpQty,stockQty from " & tempTable & " where item='" & BomItem & "'"
                    'program1 = clsDBConnect.QueryDataTable(SQLSUB, clsDBConnect.MIS2)
                    'clsDBConnect.Close(clsDBConnect.MIS2)
                    'For l As Integer = 0 To program1.Rows.Count - 1
                    '    With program1.Rows(l)
                    '        PRQty = Trim(.Item("prQty"))
                    '        POQty = Trim(.Item("poQty"))
                    '        POInspectQty = Trim(.Item("poRcpQty"))
                    '        InventoryQty = Trim(.Item("stockQty"))
                    '    End With
                    'Next
                    Dim dc As New DataSet
                    dc = clsDBConnect.QueryDataSet(SQLSUB, clsDBConnect.MIS2)
                    clsDBConnect.Close(clsDBConnect.MIS2)
                    If dc.Tables(0).Rows.Count > 0 Then
                        With dc.Tables(0).Rows(0)
                            PRQty = Trim(.Item("prQty"))
                            POQty = Trim(.Item("poQty"))
                            POInspectQty = Trim(.Item("poRcpQty"))
                            InventoryQty = Trim(.Item("stockQty"))
                            Dim dr1 As DataRow
                            dr1 = dtShow.NewRow()
                            dr1("MONO") = MONO
                            dr1("PlanStartDate") = PlanStartDate
                            dr1("ItemSpec") = ItemSpec
                            dr1("MatItem") = MatItem
                            dr1("MatSpec") = MatSpec
                            dr1("RequestQty") = RequestQty
                            dr1("IssueQty") = IssueQty
                            dr1("BalQty") = BalQty
                            dr1("Unit") = Unit
                            dr1("PRQty") = PRQty
                            dr1("POQty") = POQty
                            dr1("POInspectQty") = POInspectQty
                            dr1("InventoryQty") = InventoryQty
                            dtShow.Rows.Add(dr1)
                        End With
                    End If
                    'dt2.Rows.Add(New Object() {MONO, PlanStartDate, ItemSpec, MatItem, MatSpec, RequestQty, IssueQty, BalQty, Unit, PRQty, POQty, POInspectQty, InventoryQty})
                End With
            Next
        Next
        If dtShow.Rows.Count < 0 Then
            dtShow.Clear()
        End If
        gvShow.DataSource = dtShow
        gvShow.DataBind()
        lbCount.Text = gvShow.Rows.Count
        btExport.Visible = True
        'System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("MaterialsNotIssue" & Session("UserName"), gvShow)
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
End Class