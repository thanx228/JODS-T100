Public Class CostBom
    Inherits System.Web.UI.Page
    Dim ControlDataFormT100 As New ControlDataFormT100
    Dim CreateTempTable As New T100CreateTempTable
    Dim clsDBConnect As New clsDBConnect
    Dim Conn_SQL As New ConnSQL
    Dim ControlFormT100 As New ControlDataFormT100
    Dim ControlForm As New ControlDataForm
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btExport.Visible = False
            If Session("UserName") = "" Then
                Response.Redirect("../LoginT100.aspx")
            End If
            HeaderForm1.HeaderLable = ControlDataFormT100.nameHeaderT100(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim tempTable As String = "CostBOM" & Session("UserName")
        GenData(tempTable)

        Dim qty As Decimal = 1
        Dim fldQty As String = tbQty.Text.ToString.Trim
        If fldQty <> "" Then
            qty = CDec(fldQty)
            If qty = 0 Then
                qty = 1
            End If
        End If

        Dim Program As New DataTable
        Dim SQL As String = ""
        Dim ParItem As String = "", SubItem As String = "", Descr As String = "", Spec As String = "", Levelbom As String = "", Prty As String = "", QPA As String = "",
            Usage As String = "", Unit As String = "", Curr As String = "", PricOgr As String = "", ExRate As String = "", PriceTHB As String = "", Sup As String = "", SupDesc As String = "", AmtTHB As String = ""
        Dim dr1 As DataRow
        Dim dtShow As New DataTable
        dtShow.Columns.Add(New DataColumn("Parent Item")) 'ParItem
        dtShow.Columns.Add(New DataColumn("Sub Item")) 'SubItem
        dtShow.Columns.Add(New DataColumn("Description")) ' Descr
        dtShow.Columns.Add(New DataColumn("Specification")) 'Spec
        dtShow.Columns.Add(New DataColumn("Level Bom")) 'Levelbom
        dtShow.Columns.Add(New DataColumn("Property")) 'Prty
        dtShow.Columns.Add(New DataColumn("QPA")) 'QPA
        dtShow.Columns.Add(New DataColumn("Usage")) 'Usage
        dtShow.Columns.Add(New DataColumn("Unit")) 'Unit
        dtShow.Columns.Add(New DataColumn("Currency")) 'Curr
        dtShow.Columns.Add(New DataColumn("Price Original")) 'PricOgr 
        dtShow.Columns.Add(New DataColumn("Exchang Rate")) 'ExRate
        dtShow.Columns.Add(New DataColumn("Price THB")) 'PriceTHB
        dtShow.Columns.Add(New DataColumn("Supplier")) 'Sup
        dtShow.Columns.Add(New DataColumn("Supplier Description")) 'SupDesc
        dtShow.Columns.Add(New DataColumn("Amount THB")) 'AmtTHB

        SQL = "  select substring(BomItem,1,16) ParentItem,SubItem SubItem,(LEN(BomItem) - LEN(REPLACE(BomItem, ':', ''))) LevelBom, " &
            " case Property when '1' then 'Purchase'  when '2' then 'Manufacture' when '3' then 'Subcontract'when '4' then 'None'when 'O' then 'Outsource'else 'Configuration' end Property," &
            " QtyPerPcs QPA,cast(QtyPerPcs*1 as decimal(16,4)) Usage,Unit,Currency,Price PriceOriginal,ExchangRate ExchangRate," &
            " cast(Price*ExchangRate as decimal(16,4)) PriceTHB, Supplier," &
            " cast(QtyPerPcs*1*Price*ExchangRate as decimal(16,4)) AmountTHB" &
            " from " & tempTable & " " &
            " order by BomItem "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)

                dr1 = dtShow.NewRow()
                dr1("Parent Item") = .Item("ParentItem")
                dr1("Sub Item") = .Item("SubItem")

                Dim ASQL As String = ""
                Dim AProgram As New DataTable
                ASQL = "select imaal001,imaal003,imaal004 from imaal_t where imaalent='3' and imaal002='en_US' and imaal001='" & Trim(.Item("SubItem")) & "'"
                AProgram = clsDBConnect.QueryDataTable(ASQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)

                If AProgram.Rows.Count > 0 Then
                    With AProgram.Rows(0)
                        dr1("Description") = .Item("imaal003")
                        dr1("Specification") = .Item("imaal004")
                    End With
                End If

                dr1("Level Bom") = .Item("LevelBom")
                dr1("Property") = .Item("Property")
                dr1("QPA") = .Item("QPA")
                dr1("Usage") = .Item("Usage")
                dr1("Unit") = .Item("Unit")
                dr1("Currency") = .Item("Currency")
                dr1("Price Original") = .Item("PriceOriginal")
                dr1("Exchang Rate") = .Item("ExchangRate")
                dr1("Price THB") = .Item("PriceTHB")
                dr1("Supplier") = .Item("Supplier")

                Dim BSQL As String = "", BProgram As New DataTable
                BSQL = "select pmaal001,pmaal004 from pmaal_t where pmaalent='3' and pmaal002='en_US' and pmaal001='" & Trim(.Item("Supplier")) & "'"
                BProgram = clsDBConnect.QueryDataTable(BSQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                If BProgram.Rows.Count > 0 Then
                    With BProgram.Rows(0)
                        dr1("Supplier Description") = .Item("pmaal004")
                    End With
                End If

                dr1("Amount THB") = .Item("AmountTHB")
                dtShow.Rows.Add(dr1)
            End With
        Next
        ControlFormT100.ShowGridViewT100(gvShow, dtShow)
        CountRow1.RowCount = ControlFormT100.rowGridviewT100(gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        btExport.Visible = True
    End Sub

    Protected Sub GenData(tempTable As String)
        CreateTempTable.createTempCostBOM(tempTable)
        Dim fldInsHash As Hashtable = New Hashtable,
            whrHash As Hashtable = New Hashtable,
            codePrd As String = tbCode.Text.Trim
        whrHash.Add("BomItem", codePrd) ' parent item
        'insert Zone
        fldInsHash.Add("ParentItem", codePrd) ' parent item
        fldInsHash.Add("SubItem", codePrd) ' sub item
        fldInsHash.Add("Unit", "PC") 'Unit
        fldInsHash.Add("QtyPerPcs", 1) 'qty per pcs
        fldInsHash.Add("Property", "2") 'property 1:Manufu,2:Pur
        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        codeOuts(tempTable, codePrd, codePrd, codePrd)
        CodeBOM(tempTable, codePrd, codePrd)
        '1=M,2=P
        Dim SQL As String = "",
            sSQL As String = "",
            ssSQL As String = "",
            Program As New DataTable,
            sProgram As New DataTable,
            ssProgram As New DataTable,
            ExchangeRateList = New Hashtable
        'get curency export value to local value
        ExchangeRateList.Add("THB", 1)
        Dim yearMonth As String = DateTime.Today.ToString("yyyyMM")
        ssSQL = " select ooan002,CAST(ooan007 AS DECIMAL(16,8)) exch from ooam_t " &
            " left join ooan_t on ooanent=ooament and ooan004=ooam004 " &
            " where ooament='3' and ooam001='TH' and ooamstus='Y' and TO_CHAR(ooam004,'yyyyMM') like '" & yearMonth & "%' order by ooam004 desc "
        ssProgram = clsDBConnect.QueryDataTable(ssSQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To ssProgram.Rows.Count - 1
            With ssProgram.Rows(i)
                Dim currency As String = .Item("ooan002").ToString.Trim
                If ExchangeRateList.ContainsKey(currency) = False Then
                    ExchangeRateList.Add(currency, .Item("exch")) 'value is exchang rate for currency
                End If
            End With
        Next
        '2:MANUFACTURING
        SQL = "select SubItem,Property from " & tempTable & " where Property not in ('2')  group by SubItem,Property "
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim supplier As String = "",
                    price As Decimal = 99999,
                    currency As String = "",
                    exchangRate As Decimal = 1,
                    code As String = .Item("SubItem").ToString.Trim,
                    propertyCode As String = .Item("Property").ToString.Trim

                'select price last update each supplier
                Dim priceList As Hashtable = New Hashtable,
                    currencyList As Hashtable = New Hashtable
                'o:outsourcing
                If propertyCode = "O" Then
                    Dim temp As String() = code.Split("-")
                    sSQL = " select pmdf004 sup,pmdf005 cur,pmdg010 prc from pmdf_t " &
                        " left join pmdg_t on pmdgent=pmdfent and pmdgsite=pmdfsite and pmdgdocno=pmdfdocno " &
                        " where pmdfent='3' and pmdfsite='JINPAO' and pmdfstus='Y' and pmdg003='" & Trim(temp(0)) & "' and pmdg014='" & Trim(temp(1)) & "'"
                Else
                    sSQL = " select pmdf004 sup,pmdf005 cur,pmdg010 prc from pmdf_t " &
                       " left join pmdg_t on pmdgent=pmdfent and pmdgsite=pmdfsite and pmdgdocno=pmdfdocno " &
                       " where pmdfent='3' and pmdfsite='JINPAO' and pmdfstus='Y' and pmdg003='" & Trim(code) & "'"
                End If
                sProgram = clsDBConnect.QueryDataTable(sSQL, clsDBConnect.T100)
                clsDBConnect.Close(clsDBConnect.T100)
                'หา Subcon
                For j As Integer = 0 To sProgram.Rows.Count - 1
                    With sProgram.Rows(j)
                        Dim supCode As String = .Item("sup") 'supplier code
                        If priceList.ContainsKey(supCode) = False Then
                            priceList.Add(supCode, .Item("prc")) 'price per unit for supplier
                            currencyList.Add(supCode, .Item("cur")) 'currency for supplier
                        End If
                    End With
                Next

                'select price is cheapest and currency
                If priceList.Count > 0 Then
                    For Each sCode As String In priceList.Keys
                        Dim temp_cur As String = currencyList.Item(sCode).ToString.Trim
                        Dim temp_prc_ori As Decimal = CDec(priceList.Item(sCode))
                        Dim temp_rate As Decimal = CDec(ExchangeRateList.Item(temp_cur))
                        Dim temp_prc As Decimal = temp_prc_ori * temp_rate
                        If temp_prc < price Then
                            supplier = sCode
                            price = temp_prc_ori
                            currency = temp_cur
                            exchangRate = temp_rate
                        End If
                    Next
                Else
                    price = 0
                    currency = ""
                    exchangRate = 1
                    supplier = ""
                End If

                fldInsHash = New Hashtable
                Dim fldUpdHash As Hashtable = New Hashtable
                Dim USQL As String = ""
                whrHash = New Hashtable
                'whr of condition
                whrHash.Add("SubItem", code) ' Sub item
                'Update Zone
                fldUpdHash.Add("Supplier", supplier) ' Supplier
                fldUpdHash.Add("Price", price) ' Price
                fldUpdHash.Add("Currency", currency) ' Currency
                fldUpdHash.Add("ExchangRate", exchangRate) ' Exchange Rate
                USQL = Conn_SQL.GetSQL(tempTable, fldUpdHash, whrHash, "U")
                clsDBConnect.QueryExecuteScalar(USQL, clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next
    End Sub

    Protected Sub codeOuts(ByVal temptable As String, ByVal parentItem As String, ByVal parentCode As String, ByVal code As String, Optional ByVal qpa As Decimal = 1)
        Dim sSQL As String = "",
            sProgram As DataTable

        'select data from outs
        sSQL = "Select ecbb004,ecbb030,count(ecbb004) cnt from ecba_t " &
        " left join ecbb_t On ecbbent=ecbaent And ecbbsite=ecbasite And ecbb001=ecba001 And ecbb002=ecba002 " &
        " where ecbaent='3' and ecbasite='JINPAO' and ecba001='" & code & "' and ecbb013='Y' group by ecbb004,ecbb030 "
        sProgram = clsDBConnect.QueryDataTable(sSQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)

        For j As Integer = 0 To sProgram.Rows.Count - 1
            With sProgram.Rows(j)
                Dim xOper As String = .Item("ecbb004")
                Dim operation As String = code & "-" & xOper
                Dim fldInsHash = New Hashtable
                Dim whrHash = New Hashtable
                'whr of condition
                whrHash.Add("BomItem", parentItem & ":" & operation) ' parent item
                'insert Zone
                fldInsHash.Add("ParentItem", parentCode) ' parent item
                fldInsHash.Add("SubItem", operation) ' sub item
                fldInsHash.Add("Unit", .Item("ecbb030")) 'Unit
                fldInsHash.Add("QtyPerPcs", .Item("cnt") * qpa) 'qty per pcs
                fldInsHash.Add("Property", "O") 'property
                fldInsHash.Add("Operation", xOper) 'Operation
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(temptable, fldInsHash, whrHash, "I"), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
            End With
        Next
    End Sub

    Protected Sub CodeBOM(ByVal tempTable As String, ByVal code As String, ByVal parentItem As String, Optional qpa As Decimal = 1)
        Dim SQL As String = "",
            USQL As String = "",
            sSQL As String = ""

        SQL = " select bmba001,bmba003,bmba011,bmba010,imaf013 from bmba_t " &
              " left join imaf_t on imaf001=bmba003 and imafent=bmbaent and imafsite=bmbasite" &
              " left join imaal_t on imaal001=imaf001 and imaalent=bmbaent and imaal002='en_US' " &
              " where bmbaent='3' and bmbasite='JINPAO' and bmba001='" & code & "'"
        'imaf013=2:Purchase
        'imaf013=1:Manufuring
        Dim Program As New DataTable,
            sProgram As New DataTable
        Program = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                Dim parentCode As String = ""
                parentCode = .Item("bmba001")
                Dim subCode As String = ""
                subCode = .Item("bmba003").ToString.Trim
                'whr of condition
                whrHash.Add("BomItem", parentItem & ":" & subCode) ' parent item
                'insert Zone
                fldInsHash.Add("ParentItem", parentCode) ' parent item
                fldInsHash.Add("SubItem", subCode) ' sub item
                fldInsHash.Add("Unit", .Item("bmba010")) 'Unit
                fldInsHash.Add("QtyPerPcs", .Item("bmba011") * qpa) 'qty per pcs
                fldInsHash.Add("Property", .Item("imaf013")) 'property
                clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash, "I"), clsDBConnect.MIS2)
                clsDBConnect.Close(clsDBConnect.MIS2)
                'select data from outs
                codeOuts(tempTable, parentItem, parentCode, subCode, qpa)
                If .Item("imaf013") = "2" Then '2:MANUFACTURING
                    CodeBOM(tempTable, subCode, parentItem & ":" & subCode, .Item("bmba011"))
                End If

            End With
        Next
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("CostBOM" & Session("UserName"), gvShow)
    End Sub
End Class