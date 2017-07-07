Public Class moGenBatch
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim createTableTrace As New createTableTrace
    Const tableBatch As String = "BatchRecordLog"
    Const moTypeList As String = "5104,5109,5194,5199,5210,5211,5212"
    Const moTypeListFA As String = "5109,5199"

    Dim varIni As New VarIni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            createTableTrace.createBatchRecord()
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            reset()
            cbNewBatch_CheckedChanged(sender, e)
            'ddlDocType_SelectedIndexChanged(sender, e)
            'cbFA_CheckedChanged(sender, e)
        End If
    End Sub

    Function getMO(whr As String) As DataTable

        Dim fldName As New ArrayList

        'TA006 or TD004 = item
        fldName.Add(SFAA.ProductItem)
        'TD005 or TA034 = desc
        fldName.Add(IMAAL.ProductName)
        'TD006 or TA035 = spec
        fldName.Add(IMAAL.Specifaction)
        'TA026,TA027,TA028 = source sale order
        fldName.Add(SFAA.OldRefereanceDocNo)
        fldName.Add(SFAA.OldRefereanceDocLineNo)
        fldName.Add(SFAA.OldRefereanceDocLineSeq)
        'TA001,TA002 mo type and mo number
        fldName.Add(SFAA.DocNo)
        'TA011 status mo
        fldName.Add(SFAA.Status)
        'TA057 batch number
        fldName.Add("nvl(" & SFAA.EstimatedStorgeBacthNo & ",'*'):" & SFAA.EstimatedStorgeBacthNo)
        'mo qty
        fldName.Add(SFAA.ProductionQty)
        'cust po
        fldName.Add(XMDA.CustomerPONo)
        'product class
        fldName.Add(IMAA.ProductClassification)
        'cust po
        fldName.Add("nvl(" & XMDC.CustPONumber & ",'NO CUST PO'):" & XMDC.CustPONumber)

        'asft300 mo head
        Dim SQL As String = varIni.S & Conn_SQL.getFeild(fldName) & varIni.F & SFAA.tblMO
        'axmt500 sale order head
        SQL &= varIni.getLeftjoinFirst(varIni.XMDA, varIni.SFAA, True, XMDA.SaleOrderNo & ":" & SFAA.OldRefereanceDocNo)
        'axmt500 sale body
        SQL &= varIni.getLeftjoinFirst(varIni.XMDC, varIni.SFAA, True, XMDC.SaleOrderNo & ":" & SFAA.OldRefereanceDocNo & "," & XMDC.ItemSequence & ":" & SFAA.OldRefereanceDocLineNo)
        'item
        SQL &= varIni.getLeftjoinFirst(varIni.IMAA, varIni.SFAA, False, IMAA.ItemNo & ":" & SFAA.ProductItem)
        'item lang
        SQL &= varIni.getLeftjoinFirst(varIni.IMAAL, varIni.SFAA, False, IMAAL.ProductItem & ":" & SFAA.ProductItem & "," & IMAAL.Langauge & ":" & varIni.enUS_V & ":")
        'where and order by
        SQL &= whr & varIni.getOrderBy(SFAA.DocNo)

        Return GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe)
    End Function

    Function get_SO_MO(whr As String) As DataTable
        Dim fldName As New ArrayList
        'TA006 or TD004 = item
        fldName.Add(SFAA.ProductItem)
        'TD005 or TA034 = desc
        fldName.Add(IMAAL.ProductName)
        'TD006 or TA035 = spec
        fldName.Add(IMAAL.Specifaction)
        'TA026,TA027,TA028 = source sale order
        fldName.Add(SFAA.OldRefereanceDocNo)
        fldName.Add("nvl(" & SFAA.OldRefereanceDocLineNo & ",'0'):" & SFAA.OldRefereanceDocLineNo)
        fldName.Add(SFAA.OldRefereanceDocLineSeq)
        'TA001,TA002 mo type and mo number
        fldName.Add(SFAA.DocNo)
        'TA011 status mo
        fldName.Add(SFAA.Status)
        'TA057 batch number
        fldName.Add("nvl(" & SFAA.EstimatedStorgeBacthNo & ",'*'):" & SFAA.EstimatedStorgeBacthNo)
        'mo qty
        fldName.Add(SFAA.ProductionQty)
        'cust po
        'fldName.Add(XMDA.CustomerPONo)
        'product class
        fldName.Add(IMAA.ProductClassification)
        'asft300 mo head
        Dim SQL As String = varIni.S & Conn_SQL.getFeild(fldName) & varIni.F & SFAA.tblMO
        'item
        SQL &= varIni.getLeftjoinFirst(varIni.IMAA, varIni.SFAA, False, IMAA.ItemNo & ":" & SFAA.ProductItem)
        'item lang
        SQL &= varIni.getLeftjoinFirst(varIni.IMAAL, varIni.SFAA, False, IMAAL.ProductItem & ":" & SFAA.ProductItem & "," & IMAAL.Langauge & ":" & varIni.enUS_V & ":")
        'where and order by
        SQL &= whr & varIni.getOrderBy(SFAA.DocNo)
        Return GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe)
    End Function

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        If tbMoNo.Text = "" Then
            show_message.ShowMessage(Page, "MO No is empty!!,Please check it again.", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        End If

        Dim SQL As String,
            dt As DataTable,
            WHR As String,
            soType As String = "",
            soNo As String = "",
            item As String = "",
            moType As String = "",
            moNo As String = "",
            SaleOrderNumber As String = "",
            SaleOrderLineNo As String = "",
            SaleOrderLineSeq As String = ""

        Dim custPoNumber As String = ""
        Dim soSeq As String = ""
        Dim dtShowMO As DataTable
        'Dim dtShowSO As DataTable

        If tbItemOld.Text.Trim <> "" Then
            SQL = "select " & IMAA.ItemNo & " from " & IMAA.tblProductItemDeatil & " where " & IMAA.ItemNo & "='" & tbItemOld.Text.Trim & "' and  " & IMAA.ProductClassification & "='2' "
            dt = GetData.Get_DataReaderOracle(SQL, GetData.WhoCalledMe())
            If dt.Rows.Count = 0 Then
                show_message.ShowMessage(Page, "item Old is not exist!!,Please check it again.", UpdatePanel1)
                tbItemOld.Focus()
                Exit Sub
            End If
        End If

        WHR = varIni.getWhrFirst(varIni.SFAA)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(SFAA.DocNo), ucDocType.getObject)
        WHR &= Conn_SQL.Where(varIni.getFldDoc(SFAA.DocNo, True), tbMoNo, False)
        WHR &= Conn_SQL.Where(IMAA.ProductClassification, "2",, False)
        'WHR &= Conn_SQL.Where(SFAA.WorkOrderSource, "3", , False)

        dt = get_SO_MO(WHR)
        If dt.Rows.Count = 0 Then
            show_message.ShowMessage(Page, "Please check MO again!!!", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        Else
            With dt.Rows(0)
                lbSO.Text = .Item(SFAA.OldRefereanceDocNo)
                lbBatch.Text = .Item(SFAA.EstimatedStorgeBacthNo)

                item = Trim(.Item(SFAA.ProductItem))
                Dim fldName As New ArrayList
                WHR = varIni.getWhrFirst(varIni.IMAA, False)
                WHR &= Conn_SQL.Where(IMAA.ItemNo, item,, False)
                WHR &= Conn_SQL.Where(IMAA.ProductClassification, "2",, False)

                fldName.Add(IMAA.ItemNo)
                fldName.Add(IMAA.ProductClassification)
                Dim dt1 As DataTable = IMAA.getItemInfo(fldName, WHR)
                If dt1.Rows.Count = 0 Then
                    show_message.ShowMessage(Page, "Please Control Lot for this item before get it!!!", UpdatePanel1)
                    tbMoNo.Focus()
                    lbSO.Text = ""
                    lbBatch.Text = ""
                    Exit Sub
                End If
                lbItem.Text = item
                lbDesc.Text = Trim(.Item(IMAAL.ProductName))
                lbSpec.Text = Trim(.Item(IMAAL.Specifaction))
                lbMoStatus.Text = Trim(.Item(SFAA.Status))
                moNo = Trim(.Item(SFAA.OldRefereanceDocNo))
                SaleOrderNumber = Trim(.Item(SFAA.OldRefereanceDocNo))
                SaleOrderLineNo = Trim(.Item(SFAA.OldRefereanceDocLineNo))
                SaleOrderLineSeq = Trim(.Item(SFAA.OldRefereanceDocLineSeq))
                lbItemOld.Text = tbItemOld.Text.Trim
                lbHardTool.Text = If(cbHardTool.Checked, "1", "0")
            End With
        End If

        Dim colName As New ArrayList
        '<asp:BoundField DataField = "A1" HeaderText="MO Type" />
        '<asp:BoundField DataField = "A2" HeaderText="MO No" />
        colName.Add(":A")
        '<asp:BoundField DataField = "B" HeaderText="Item" />
        colName.Add(":B")
        '<asp:BoundField DataField = "C" HeaderText="Desc" />
        colName.Add(":C")
        '<asp:BoundField DataField = "D" HeaderText="Spec" />
        colName.Add(":D")
        '<asp:BoundField DataField = "E" DataFormatString="{0:N0}" HeaderText="Plan Qty" />
        colName.Add(":E:0")
        '<asp:BoundField DataField = "F" HeaderText="Batch No" />
        colName.Add(":F")
        '<asp:BoundField DataField = "G" HeaderText="Cust PO" />
        colName.Add(":G")
        '<asp:BoundField DataField = "H" HeaderText="SO Seq" />
        colName.Add(":H")
        '<asp:BoundField DataField = "I" HeaderText="Lot Control" />
        colName.Add(":I")

        dtShowMO = ControlForm.setColDatatable(colName)

        'colName = New ArrayList

        ''<asp:BoundField DataField = "A" HeaderText="So Seq" />
        'colName.Add(":A")
        ''<asp:BoundField DataField = "B" HeaderText="Cust PO" />
        'colName.Add(":B:0")
        ''<asp:BoundField DataField = "C" DataFormatString="{0:N0}" HeaderText="SO Qty"> <ItemStyle HorizontalAlign = "Right" /> </asp: BoundField>
        'colName.Add(":C")
        ''<asp:BoundField DataField = "D" HeaderText="Del Date" />
        'colName.Add(":D")

        'dtShowSO = ControlForm.setColDatatable(colName)

        WHR = varIni.getWhrFirst(varIni.SFAA)
        WHR &= Conn_SQL.Where(SFAA.OldRefereanceDocNo, SaleOrderNumber,, False)
        WHR &= Conn_SQL.Where(SFAA.OldRefereanceDocLineNo, SaleOrderLineNo,, False)
        'WHR &= Conn_SQL.Where(SFAA.OldRefereanceDocLineSeq, SaleOrderLineSeq,, False)
        'WHR &= Conn_SQL.Where(IMAA.ProductClassification, "2",, False)
        'WHR &= Conn_SQL.Where(SFAA.WorkOrderSource, "4", , False)

        dt = getMO(WHR)
        For i As Integer = 0 To dt.Rows.Count - 1

            With dt.Rows(i)
                Dim dataHash As New Hashtable
                '<asp:BoundField DataField = "A" HeaderText="MO" />
                dataHash.Add("A", .Item(SFAA.DocNo))
                '<asp:BoundField DataField = "B" HeaderText="Item" />
                dataHash.Add("B", .Item(SFAA.ProductItem))
                '<asp:BoundField DataField = "C" HeaderText="Desc" />
                dataHash.Add("C", .Item(IMAAL.ProductName))
                '<asp:BoundField DataField = "D" HeaderText="Spec" />
                dataHash.Add("D", .Item(IMAAL.Specifaction))
                '<asp:BoundField DataField = "E" DataFormatString="{0:N0}" HeaderText="Plan Qty" />
                dataHash.Add("E", .Item(SFAA.ProductionQty))
                '<asp:BoundField DataField = "F" HeaderText="Batch No" />
                dataHash.Add("F", .Item(SFAA.EstimatedStorgeBacthNo))
                '<asp:BoundField DataField = "G" HeaderText="Cust PO" />
                dataHash.Add("G", .Item(XMDC.CustPONumber))
                '<asp:BoundField DataField = "H" HeaderText="SO Seq" />
                dataHash.Add("H", .Item(SFAA.OldRefereanceDocLineNo))
                '<asp:BoundField DataField = "I" HeaderText="Lot Control" />
                dataHash.Add("I", .Item(IMAA.ProductClassification))

                ControlForm.addDataRow(dtShowMO, dataHash)
            End With

        Next
        ControlForm.ShowGridView(gvShow, dtShowMO)
        ucCountRowShow.RowCount = ControlForm.rowGridview(gvShow)

        ''show po and so seq
        'SQL = "select TD003 A,TD027 B,TD008 C,TD013 D from COPTD left join COPTC on TC001=TD001 and TC002=TD002 where  TD001='" & soType & "' and TD002='" & soNo & "' and TD004='" & item & "' order by TD003 "
        'ControlForm.ShowGridView(gvSO, SQL, Conn_SQL.ERP_ConnectionString)
        'ucCountRowSO.RowCount = ControlForm.rowGridview(gvSO)

        Dim batchVisible As Boolean = False,
            updateVisible As Boolean = False
        If lbMoStatus.Text.Trim <> "C" And lbMoStatus.Text.Trim <> "M" And gvShow.Rows.Count > 0 Then
            If lbBatch.Text.Trim = "*" Then
                batchVisible = True
            End If
            If gvSO.Rows.Count > 0 Then
                updateVisible = True
            End If
        End If

        btGen.Visible = batchVisible
        'btUpdate.Visible = updateVisible
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Function GetBatchNo() As String
        Dim strSql As String,
            BarNo As String,
            item As String = lbItem.Text
        
        If tbItemOld.Text.Trim <> "" Then
            item &= "," & tbItemOld.Text.Trim
        End If
        'gen new batch no
        'substring(TA057,1,2) = 'FA' or substring(TA057,1,1)='*'
        'strSql = "select top 1 isnull(TA057,'') from MOCTA where TA006 in ('" & item.Replace(",", "','") & "')  and substring(TA057,1,2) <> 'FA'  order by substring(TA057,2,1)+substring(TA057,1,1) desc "

        strSql = "select nvl(" & SFAA.EstimatedStorgeBacthNo & ",'') " & SFAA.EstimatedStorgeBacthNo & " from " & SFAA.tblMO
        strSql &= varIni.getWhrFirst(varIni.SFAA) & " and " & SFAA.ProductItem & " in ('" & item.Replace(",", "','") & "') "
        strSql &= "and  substr(" & SFAA.EstimatedStorgeBacthNo & ",1,2)<>'FA' "
        strSql &= "order by substr(" & SFAA.EstimatedStorgeBacthNo & ",2,1)||substr(" & SFAA.EstimatedStorgeBacthNo & ",1,1) desc FETCH FIRST ROW ONLY "

        ' TA001 not in('" & moTypeListFA.Replace(",", "','") & "')
        Dim dt As DataTable = GetData.GetDataReaderOracle(strSql, "", GetData.WhoCalledMe)
        If dt.Rows.Count > 0 Then
            BarNo = dt.Rows(0).Item(SFAA.EstimatedStorgeBacthNo)
        Else
            BarNo = ""
        End If

        If BarNo = "NULL" Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "0A"
        Else
            Dim strL As String = BarNo.Substring(0, 1) 'left char
            Dim strR As String = BarNo.Substring(1, 1) 'right char
            If strL = "9" And strR = "Z" Then
                strL = "A"
                strR = "0"
            Else
                'check numberic for char left
                If IsNumeric(strL) Then 'left isnumric = true
                    If strL = "9" Then 'new char right and zero
                        strL = "0"
                        strR = nextChar(strR)
                    Else 'run number continue
                        strL = nextChar(strL)
                    End If
                Else 'right isnumberic= true
                    If strR = "9" Then
                        strL = nextChar(strL)
                        strR = "0"
                    Else
                        strR = nextChar(strR)
                    End If
                End If
            End If
            BarNo = strL & strR
        End If

        If cbHardTool.Checked Then
            BarNo &= "H"
        End If
        Return BarNo
    End Function

    Function getBatchNoFA()
        Dim strSql As String,
            BarNo As String,
            item As String = lbItem.Text
        If tbItemOld.Text.Trim <> "" Then
            item &= "," & tbItemOld.Text.Trim
        End If

        'gen new batch no
        'substring(TA057,1,2) = 'FA' or substring(TA057,1,1)='*'  TA001 in('" & moTypeListFA.Replace(",", "','") & "')
        'strSql = "select top 1 isnull(TA057,'') from MOCTA where TA006 in ('" & item.Replace(",", "','") & "')  and substring(TA057,1,2) = 'FA' or substring(TA057,1,1)='*' order by substring(TA057,2,1)+substring(TA057,1,1) desc "

        strSql = "select nvl(" & SFAA.EstimatedStorgeBacthNo & ",'') " & SFAA.EstimatedStorgeBacthNo & " from " & SFAA.tblMO
        strSql &= varIni.getWhrFirst(varIni.SFAA) & " and " & SFAA.ProductItem & " in ('" & item.Replace(",", "','") & "') "
        strSql &= "and  (substr(" & SFAA.EstimatedStorgeBacthNo & ",1,2)='FA' or substr(" & SFAA.EstimatedStorgeBacthNo & ",1,1)='')"
        strSql &= "order by substr(" & SFAA.EstimatedStorgeBacthNo & ",2,1)||substr(" & SFAA.EstimatedStorgeBacthNo & ",1,1) desc FETCH FIRST ROW ONLY "

        ' TA001 not in('" & moTypeListFA.Replace(",", "','") & "')
        Dim dt As DataTable = GetData.GetDataReaderOracle(strSql, "", GetData.WhoCalledMe)
        If dt.Rows.Count > 0 Then
            BarNo = dt.Rows(0).Item(SFAA.EstimatedStorgeBacthNo)
        Else
            BarNo = ""
        End If

        'BarNo = Conn_SQL.Get_value(strSql, Conn_SQL.ERP_ConnectionString)
        If BarNo = "NULL" Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "FA1"
        Else
            Dim lastNo As Integer = CInt(BarNo.Replace("FA", "").Replace("H", "")) + 1
            BarNo = "FA" & lastNo.ToString
        End If
        If cbHardTool.Checked Then
            BarNo &= "H"
        End If
        Return BarNo

    End Function

    Function nextChar(strR As String) As String
        Return Chr(Asc(strR) + 1)
    End Function

    Protected Sub reset() Handles btReset.Click

        'Dim SQL As String
        'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ001 in ('" & moTypeList.Replace(",", "','") & "') order by MQ001"
        'ControlForm.showDDL(ddlDocType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

        ucDocType.setShowTypeFull(moTypeList)

        tbMoNo.Text = ""
        lbBatch.Text = ""
        lbSO.Text = ""
        lbSpec.Text = ""
        lbItem.Text = ""
        lbDesc.Text = ""
        lbMoStatus.Text = ""
        gvSO.DataSource = ""
        gvSO.DataBind()
        gvShow.DataSource = ""
        gvShow.DataBind()
        btGen.Visible = False
        btUpdate.Visible = False

        lbItemOld.Text = ""
        lbRedo.Text = ""
        lbHardTool.Text = ""

        ucCountRowShow.RowCount = ControlForm.rowGridview(gvShow)
        ucCountRowSO.RowCount = ControlForm.rowGridview(gvSO)

    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim cnt As Integer = 0,
            custPO As SortedList = New SortedList,
            soSeq As String = "",
            custPO2 As String = ""

        With gvSO
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim cbChk As CheckBox = .Cells(0).FindControl("cbChk")
                    If cbChk.Checked Then
                        Dim keyVal As String = .Cells(2).Text.Trim
                        Dim qty As Decimal = CDec(.Cells(3).Text.Trim)
                        Dim valSoSeq As String = .Cells(1).Text.Trim
                        soSeq &= .Cells(1).Text.Trim & "=" & qty.ToString & ","
                        If custPO.ContainsKey(keyVal) Then
                            'get old val
                            qty += CDec(custPO.Item(keyVal))
                            'remove
                            custPO.Remove(keyVal)
                        End If
                        custPO.Add(keyVal, qty)
                        cnt += 1
                    End If
                End With
            Next
            If cnt = 0 Then
                show_message.ShowMessage(Page, "Please select once for SO ", UpdatePanel1)
                Exit Sub
            End If
            For Each Val As DictionaryEntry In custPO
                custPO2 &= Val.Key.Trim & "=" & Val.Value.ToString & ","
            Next
        End With

        Dim fld As Hashtable = New Hashtable,
            whr As Hashtable,
            strUSQL As String = "",
            fldLog As Hashtable,
            whrLog As Hashtable,
            cp As String = custPO2.Substring(0, custPO2.Length - 1),
            ss As String = soSeq.Substring(0, soSeq.Length - 1)

        fld.Add("UDF01", cp)
        fld.Add("UDF05", ss)
        With gvShow
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim moType As String = .Cells(0).Text.Trim,
                        moNo As String = .Cells(1).Text.Trim
                    whr = New Hashtable
                    whr.Add("TA001", moType)
                    whr.Add("TA002", moNo)
                    strUSQL &= Conn_SQL.GetSQL("MOCTA", fld, whr, "U")
                    fldLog = New Hashtable
                    whrLog = New Hashtable
                    fldLog.Add("moType", moType)
                    fldLog.Add("moNo", moNo)
                    fldLog.Add("poNo", cp)
                    fldLog.Add("soSeq", ss)
                    fldLog.Add("CreateBy", Session("UserName"))
                    fldLog.Add("CreateDate", DateTime.Now.ToString("yyyyMMdd HHmmss"))
                    strUSQL &= Conn_SQL.GetSQL(Conn_SQL.DBReport & ".." & tableBatch, fldLog, whrLog, "I")
                End With
            Next
        End With
        If strUSQL <> "" Then
            Conn_SQL.Exec_Sql(strUSQL, Conn_SQL.ERP_ConnectionString)
        End If
        show_message.ShowMessage(Page, "Update Complete!!", UpdatePanel1)
        btShow_Click(sender, e)

    End Sub

    Protected Sub btGen_Click(sender As Object, e As EventArgs) Handles btGen.Click

        If cbRedo.Checked And tbBatchNo.Text = "" Then
            show_message.ShowMessage(Page, "Batch No is empty!!,Please check it again.", UpdatePanel1)
            tbBatchNo.Focus()
            Exit Sub
        End If

        With gvShow
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim item As String = .Cells(1).Text.Trim
                    Dim SQL As String = "select " & IMAA.ItemNo & " from " & IMAA.tblProductItemDeatil & " where " & IMAA.ItemNo & "='" & item & "' and  " & IMAA.ProductClassification & "='2' "

                    Dim dt As DataTable = GetData.Get_DataReaderOracle(SQL, GetData.WhoCalledMe())
                    If dt.Rows.Count = 0 Then
                        show_message.ShowMessage(Page, "Please Control Lot for this item " & item & " before get it!!!", UpdatePanel1)
                        Exit Sub
                    End If
                End With
            Next
        End With

        Dim barNo As String = If(cbRedo.Checked, tbBatchNo.Text, GetBatchNo())
        If cbRedo.Checked Then
            barNo = tbBatchNo.Text
        Else
            If cbFA.Checked Then 'ddlDocType.Text = "5109" Or ddlDocType.Text = "5199"
                barNo = getBatchNoFA()
            Else
                barNo = GetBatchNo()
            End If
        End If

        Dim fld As Hashtable = New Hashtable,
            whr As Hashtable,
            strUSQL As String = "",
            strISQL As String = "",
            fldLog As Hashtable,
            whrLog As Hashtable

        fld.Add(SFAA.EstimatedStorgeBacthNo, barNo)
        Dim sqlHash As New ArrayList
        With gvShow
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim moNumber As String = .Cells(0).Text.Trim

                    whr = New Hashtable
                    whr.Add(SFAA.DocNo, moNumber)
                    'whr.Add("TA002", moNo)
                    whr.Add("nvl(" & SFAA.EstimatedStorgeBacthNo & ",'*')", "*")
                    sqlHash.Add(Conn_SQL.GetSQL(SFAA.tblMO, fld, whr, "U", False))
                    'strUSQL &= Conn_SQL.GetSQL(SFAA.tblMO, fld, whr, "U")
                    fldLog = New Hashtable
                    whrLog = New Hashtable
                    'fldLog.Add("moType", moType)
                    fldLog.Add("moNo", moNumber)
                    fldLog.Add("barNo", barNo)
                    fldLog.Add("itemOld", tbItemOld.Text.Trim)
                    fldLog.Add("conRedo", lbRedo.Text)
                    fldLog.Add("conHardTool", lbHardTool.Text)
                    fldLog.Add("conFA", If(cbFA.Checked, "1", "0"))
                    fldLog.Add("BatchRef", tbBatchNo.Text.Trim)
                    fldLog.Add("CreateBy", Session("UserName"))
                    fldLog.Add("CreateDate", DateTime.Now.ToString("yyyyMMdd HHmmss"))
                    strUSQL &= Conn_SQL.GetSQL(tableBatch, fldLog, whrLog, "I")
                End With
            Next
        End With
        If strUSQL <> "" Then
            Conn_SQL.Exec_Sql(strUSQL, Conn_SQL.MIS_ConnectionString)
        End If
        If sqlHash.Count > 0 Then

            GetData.RunOracleTransaction(sqlHash)

            'For Each sql As String In sqlHash
            '    GetData.RunOracleTransaction(sql)
            '    'If Not GetData.ExecuteOracle(sql, "", GetData.WhoCalledMe) Then
            '    '    show_message.ShowMessage(Page, "Update Not Complete!!", UpdatePanel1)
            '    '    Exit Sub
            '    'End If
            'Next

        End If
        'sqlHash.Clear()
        show_message.ShowMessage(Page, "Update Complete!!", UpdatePanel1)
        btShow_Click(sender, e)
    End Sub

    Protected Sub cbNewBatch_CheckedChanged(sender As Object, e As EventArgs) Handles cbRedo.CheckedChanged
        'If (ddlDocType.Text = "5104" Or ddlDocType.Text = "5109") And cbRedo.Checked Then
        '    cbRedo.Checked = False
        'End If
        'If (ddlDocType.Text <> "5104" Or ddlDocType.Text <> "5109") And cbRedo.Enabled And cbRedo.Checked Then
        '    tbBatchNo.Text = ""
        '    tbBatchNo.Enabled = False
        '    Dim SQL As String,
        '        dt As DataTable,
        '        WHR As String

        '    'check mo
        '    If tbMoNo.Text = "" Then
        '        show_message.ShowMessage(Page, "MO No is empty!!,Please check it again.", UpdatePanel1)
        '        tbMoNo.Focus()
        '        cbRedo.Checked = False
        '        Exit Sub
        '    End If

        '    WHR = Conn_SQL.Where("TA001", ddlDocType)
        '    WHR &= Conn_SQL.Where("TA002", tbMoNo)
        '    SQL = " select TA015,isnull(TA026,'') TA026,isnull(TA027,'') TA027,isnull(TA028,'') TA028, " &
        '          " case when isnull(TA026,'')='' then '' else rtrim(TA026)+'-'+rtrim(TA027)+'-'+rtrim(TA028) end so, " &
        '          " TA057 from MOCTA where 1=1 " & WHR
        '    dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

        '    If dt.Rows.Count > 0 Then
        '        With dt.Rows(0)
        '            Dim soType As String = Trim(.Item("TA026"))
        '            Dim soNo As String = Trim(.Item("TA027"))
        '            Dim soSeq As String = Trim(.Item("TA028"))
        '            SQL = " select top 1 isnull(TA057,'') from MOCTA left join COPTD on TD001=TA026 and TD002=TA027 and TD003=TA028 where TA026='" & soType & "' and TA027='" & soNo & "' and TA028='" & soSeq & "' and substring(TA057,1,2) = 'FA' or substring(TA057,1,1)='*'  order by TA006" ' TA001 in ('" & moTypeListFA.Replace(",", "','") & "')
        '            Dim val As String = Conn_SQL.Get_value(SQL, Conn_SQL.ERP_ConnectionString)
        '            If val Is Nothing Then
        '                val = ""
        '            End If
        '            tbBatchNo.Text = val.Replace("*", "")
        '        End With
        '    End If
        '    If tbBatchNo.Text.Trim = "" Then
        '        tbBatchNo.Enabled = True
        '    End If
        'Else
        '    tbBatchNo.Text = ""
        '    tbBatchNo.Enabled = False
        'End If
        'lbRedo.Text = If(cbRedo.Checked, "1", "0")
    End Sub

    'Protected Sub ddlDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocType.SelectedIndexChanged
    '    cbRedo.Checked = False
    '    If ddlDocType.Text = "5104" Or ddlDocType.Text = "5109" Then
    '        cbRedo.Enabled = False
    '        tbItemOld.Enabled = True
    '    Else
    '        cbRedo.Enabled = True
    '        tbItemOld.Enabled = False
    '        cbNewBatch_CheckedChanged(sender, e)
    '    End If
    'End Sub
    'test system
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lbItemOld.Text = getBatchNoFA2()
    End Sub
    Function GetBatchNo2() As String
        Dim BarNo As String = tbMoNo.Text
        If BarNo = "NULL" Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "0A"
        Else
            Dim strL As String = BarNo.Substring(0, 1) 'left char
            Dim strR As String = BarNo.Substring(1, 1) 'right char

            If strL = "9" And strR = "Z" Then
                strL = "A"
                strR = "0"
            Else
                'check numberic for char left
                If IsNumeric(strL) Then 'left isnumric = true
                    If strL = "9" Then 'new char right and zero
                        strL = "0"
                        strR = nextChar(strR)
                    Else 'run number continue
                        strL = nextChar(strL)
                    End If
                Else 'right isnumberic= true
                    If strR = "9" Then
                        strL = nextChar(strL)
                        strR = "0"
                    Else
                        strR = nextChar(strR)
                    End If
                End If
            End If
            BarNo = strL & strR
        End If

        If cbHardTool.Checked Then
            BarNo &= "H"
        End If
        Return BarNo
    End Function

    Function getBatchNoFA2()
        Dim strSql As String = "",
            BarNo As String,
            item As String = lbItem.Text
        If tbItemOld.Text.Trim <> "" Then
            item &= "," & tbItemOld.Text.Trim
        End If
        BarNo = tbMoNo.Text
        If BarNo = "NULL" Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "FA1"
        Else
            Dim lastNo As Integer = CInt(BarNo.Replace("FA", "").Replace("H", "")) + 1
            BarNo = "FA" & lastNo.ToString
        End If
        If cbHardTool.Checked Then
            BarNo &= "H"
        End If
        Return BarNo
    End Function

    Protected Sub cbFA_CheckedChanged(sender As Object, e As EventArgs) Handles cbFA.CheckedChanged
        'cbRedo.Checked = False
        'If cbFA.Checked Then
        '    cbRedo.Enabled = False
        '    tbItemOld.Enabled = True
        'Else
        '    cbRedo.Enabled = True
        '    tbItemOld.Enabled = False
        '    cbNewBatch_CheckedChanged(sender, e)
        'End If
    End Sub
End Class