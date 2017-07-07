Public Class MatReceiveLable
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableTrace As New createTableTrace
    'Const tableMat As String = "ProductionMatUsage"
    Dim varIni As New VarIni
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            reset()
        End If
    End Sub

    Protected Sub reset() Handles btReset.Click
        ucReceiptType.setShowTypeFromPageCode("apmt520")
        tbReceiptNo.Text = ""
        ucReceiptDate.dateVal = ""
        tbLot.Text = ""
        gvShow.DataSource = ""
        gvShow.DataBind()

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim SQL As String,
            WHR As String
        Dim fldName As New ArrayList
        Dim colName As New ArrayList

        'purchase receipt number
        fldName.Add(PMDT.DocNo)
        colName.Add("Receipt number:" & PMDT.DocNo)

        'purchase receipt Seq
        fldName.Add(PMDT.ItemSequence)
        colName.Add("Receipt Seq:" & PMDT.ItemSequence)

        'receipt date
        fldName.Add("to_char(" & PMDS.DocumentDate & ",'" & ConfigDate.FormatShowOracle & "'):" & PMDS.DocumentDate)
        colName.Add("Receive Date:" & PMDS.DocumentDate)

        'purchase order number
        fldName.Add(PMDT.PurchaseOrderNo & "||'-'||" & PMDT.LineNo & ":" & PMDT.PurchaseOrderNo)
        colName.Add("PO Number:" & PMDT.PurchaseOrderNo)

        'vendor 
        fldName.Add(PMDS.PurchaseVendor & "||'-'||" & PMAAL.ContactName & ":" & PMDS.PurchaseVendor)
        colName.Add("Vendor:" & PMDS.PurchaseVendor)

        'item
        fldName.Add(PMDT.ReceiptItemNo)
        colName.Add("Item:" & PMDT.ReceiptItemNo)

        'desc
        fldName.Add(IMAAL.ProductName)
        colName.Add("Desc:" & IMAAL.ProductName)

        'spec   
        fldName.Add(IMAAL.Specifaction)
        colName.Add("Spec:" & IMAAL.Specifaction)

        'WH
        fldName.Add(PMDT.StoreLocation)
        colName.Add("Warehouse:" & PMDT.StoreLocation)

        'Bin
        fldName.Add(PMDT.Location)
        colName.Add("Bin:" & PMDT.Location)

        'Lot
        fldName.Add("nvl(" & PMDT.LotNo & ",'NO LOT')" & PMDT.LotNo)
        colName.Add("Lot No:" & PMDT.LotNo)

        'receipt qty
        fldName.Add(PMDT.ReceiptStockInQty)
        colName.Add("Receive Qty:" & PMDT.ReceiptStockInQty & ":3")

        'unit
        fldName.Add(PMDT.ReceiptStockInUnit)
        colName.Add("Unit:" & PMDT.ReceiptStockInUnit)

        'pack std
        fldName.Add(PMDT.Notes)
        colName.Add("Pack Std:" & PMDT.Notes)


        'Dim colName() As String = {"Receive Type:TH001",
        '                           "Receive No:TH002",
        '                           "Receive Seq:TH003",
        '                           "Receive Date:TG014",
        '                           "PO:TH011",
        '                           "Vender:TG005",
        '                           "Item:TH004",
        '                           "Desc:TH005",
        '                           "Spec:TH006",
        '                           "WH:TH009",
        '                           "Bin:TH072",
        '                           "Lot:TH010",
        '                           "Receive Qty:TH007:3",
        '                           "Unit:TH008",
        '                           "Pack Std:UDF52:2",
        '                           "Print Time:UDF51:0"}

        WHR = varIni.getWhrFirst(varIni.PMDT)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(PMDT.DocNo), ucReceiptType.getObject)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(PMDT.DocNo, True), tbReceiptNo, False)
        WHR &= Conn_SQL.Where(PMDS.DocumentDate, ucReceiptDate.dateVal, ucReceiptDate.dateVal, True)
        WHR &= Conn_SQL.Where("nvl(" & PMDT.LotNo & ",'NO LOT')", tbLot)
        WHR &= Conn_SQL.Where(PMDS.Status, ddlApp)


        'SQL = " select TH001,TH002,TH003,TG014,rtrim(TH011)+'-'+rtrim(TH012)+'-'+rtrim(TH013) TH011," &
        '      " rtrim(TG005)+'-'+rtrim(MA002) TG005,TH004,TH005,TH006,TH009,TH072,replace(TH010,'*','') TH010,TH007,TH008,PURTH.UDF51," &
        '      " case when isnull(PURTH.UDF52,0)=0 then TH007 else PURTH.UDF52 end UDF52 " &
        '      " from PURTH left join PURTG on TG001=TH001 and TG002=TH002 left join PURMA on MA001=TG005 where 1=1 " & WHR &
        '      " order by TH001,TH002,TH003"

        SQL = varIni.S & "" & varIni.F & PMDT.tableName
        'apmt520 purchase receipt head
        SQL &= varIni.getLeftjoinFirst(varIni.PMDS, varIni.PMDT, True, PMDS.DocNo & ":" & PMDT.DocNo)
        'contract name
        SQL &= varIni.getLeftjoinFirst(varIni.PMAAL, varIni.PMDS, True, PMAAL.ContactID & ":" & PMDS.PurchaseVendor)
        'item lang
        SQL &= varIni.getLeftjoinFirst(varIni.IMAAL, varIni.SFAA, False, IMAAL.ProductItem & ":" & PMDT.ReceiptItemNo & "," & IMAAL.Langauge & ":" & varIni.enUS_V & ":")
        'where and order by
        SQL &= WHR & varIni.getOrderBy(SFAA.DocNo)

        'Dim dt As DataSet = PMDT.Get_Receipt_PO_ITEM(fldName, WHR)

        ControlForm.GridviewColWithLinkFirst(gvShow, colName, True, "Print")
        ControlForm.ShowGridView(gvShow, PMDT.Get_Receipt_PO_ITEM(fldName, WHR))
        ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink)
                If Not IsNothing(hplDetail) Then
                    If Not IsDBNull(.DataItem(PMDT.DocNo)) Then
                        hplDetail.NavigateUrl = "~/PDF/labelMatReceive.aspx?height=150&width=350&iNo=" & .DataItem(PMDT.DocNo).ToString.Trim & "&iSeq=" & .DataItem(PMDT.ItemSequence).ToString.Trim
                        hplDetail.Attributes.Add("title", .DataItem("TG005"))
                    End If
                End If
            End If
        End With
    End Sub

    'Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
    '    If ucReceiptType.getValue = "ALL" Then
    '        ucReceiptType.Focus()
    '        Exit Sub
    '    End If
    '    If tbReceiptNo.Text = "" Then
    '        tbReceiptNo.Focus()
    '        Exit Sub
    '    End If
    '    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelMatIssue.aspx?iType=" & ucReceiptType.getValue.Trim & "&iNo=" & tbReceiptNo.Text.Trim & "&iSeq=');", True)
    'End Sub
End Class