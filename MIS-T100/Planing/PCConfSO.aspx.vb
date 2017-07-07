Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Web.HttpException
Imports System.Data.SqlClient

Public Class PCConfSO
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    ' Dim Pfunction As New Pfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                ' Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002"
            'ControlForm.showCheckboxList(cblSaleType, SQL, "MQ002", "MQ001", 5, Conn_SQL.ERP_ConnectionString)
            'btExport.Visible = False
            'CreateTable.CreateSOConfirmDate()
            'gvShow.Visible = False
            HeaderForm1.HeaderLable = Request.CurrentExecutionFilePath.ToString
        End If
    End Sub

    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Call ShowData()
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call ShowData()
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim lblSaleOrderNo As Label = CType(e.Row.FindControl("lblSaleOrderNo"), Label)
            Dim lblProductItemNo As Label = CType(e.Row.FindControl("lblProductItemNo"), Label)
            Dim lblProductItemName As Label = CType(e.Row.FindControl("lblProductItemName"), Label)
            Dim lblSpecifaction As Label = CType(e.Row.FindControl("lblSpecifaction"), Label)
            Dim lblBOM As Label = CType(e.Row.FindControl("lblBOM"), Label)

            Dim lblSaleRequestDueDate As Label = CType(e.Row.FindControl("lblSaleRequestDueDate"), Label)
            Dim lblPURConfirmDate As Label = CType(e.Row.FindControl("lblPURConfirmDate"), Label)
            Dim lblPCConfirmDate As Label = CType(e.Row.FindControl("lblPCConfirmDate"), Label)

            Dim lblPURRemark As Label = CType(e.Row.FindControl("lblPURRemark"), Label)
            Dim lblSaleConfirm As Label = CType(e.Row.FindControl("lblSaleConfirm"), Label)
            Dim lblPURConfirmDate1 As Label = CType(e.Row.FindControl("lblPURConfirmDate1"), Label)
            Dim lblPCConfirmDate1 As Label = CType(e.Row.FindControl("lblPCConfirmDate1"), Label)
            Dim lblSaleConfirm1 As Label = CType(e.Row.FindControl("lblSaleConfirm1"), Label)
            Dim lblSOconfrimLogNO As Label = CType(e.Row.FindControl("lblSOconfrimLogNO"), Label)
            If lblSaleOrderNo.Text <> String.Empty Then
                Dim pSaleDocNO = lblSaleOrderNo.Text.Split("-")
                Dim SaleNo As String = pSaleDocNO(1)
                Dim SaleType As String = pSaleDocNO(0).Replace("JP", "")
                Dim SaleSeq As String = pSaleDocNO(2)
                Dim PCwhere As String = " soType='" & SaleType & "' and soNo='" & SaleNo & "' and soSeq='" & CInt(SaleSeq).ToString("0000") & "' "
                Dim SqlPcconfrim As String = "select SOReqDate,PURConf,PCConf,PURRemark,SaleConf,PURConf1,PCConf1,SaleConf1,DocNo from SOConfirmDate  where  " & PCwhere & " "
                Dim dtPCconfrim As DataTable = GetdateSQLserver(SqlPcconfrim)
                If dtPCconfrim.Rows.Count > 0 Then
                    lblSaleRequestDueDate.Text = dtRowsFormat.FormatString(dtPCconfrim, "SOReqDate")
                    If lblSaleRequestDueDate.Text <> String.Empty Then
                        lblSaleRequestDueDate.Text = CDate(lblSaleRequestDueDate.Text).ToString("yyyy/MM/dd")
                    End If
                    lblPURConfirmDate.Text = dtRowsFormat.FormatString(dtPCconfrim, "PURConf")
                    If lblPURConfirmDate.Text <> String.Empty Then
                        lblPURConfirmDate.Text = CDate(lblPURConfirmDate.Text).ToString("yyyy/MM/dd")
                    End If
                    lblPCConfirmDate.Text = dtRowsFormat.FormatString(dtPCconfrim, "PCConf")
                    lblPURRemark.Text = dtRowsFormat.FormatString(dtPCconfrim, "PURRemark")
                    lblSaleConfirm.Text = dtRowsFormat.FormatString(dtPCconfrim, "SaleConf")
                    lblPURConfirmDate1.Text = dtRowsFormat.FormatString(dtPCconfrim, "PURConf1")
                    lblPCConfirmDate1.Text = dtRowsFormat.FormatString(dtPCconfrim, "PCConf1")
                    lblSaleConfirm1.Text = dtRowsFormat.FormatString(dtPCconfrim, "SaleConf1")
                    lblSOconfrimLogNO.Text = dtRowsFormat.FormatString(dtPCconfrim, "DocNo")
                End If
            End If
            If lblProductItemNo.Text <> String.Empty Then
                Dim dtSpec As DataTable = IMAAL.GetDataProducItem(lblProductItemNo.Text)
                If dtSpec.Rows.Count > 0 Then
                    lblProductItemName.Text = dtRowsFormat.FormatString(dtSpec, IMAAL.ProductName)
                    lblSpecifaction.Text = dtRowsFormat.FormatString(dtSpec, IMAAL.Specifaction)
                End If

                Dim SqlBOM As String = "select count(" & BMBA.ChildItemNo & ") as cuntBom from " & BMBA.tblBOMdetail & " " &
                " where " & BMBA.wStandard & "  and " & BMBA.MasterItemNo & "='" & lblProductItemNo.Text & "'"
                Dim dtBOM As DataTable = GetdateOracleBaase(SqlBOM)
                If dtBOM.Rows.Count > 0 Then
                    lblBOM.Text = dtRowsFormat.FormatString(dtBOM, "cuntBom")
                    If lblBOM.Text <> "0" Then
                        lblBOM.Text = "Yes"
                    Else
                        lblBOM.Text = "No"
                    End If
                End If
            End If
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            If lblStatus.Text <> String.Empty Then
                lblStatus.Text = StatusT100.MO_Normal(lblStatus.Text)
            End If
            Dim lblOrderType As Label = CType(e.Row.FindControl("lblOrderType"), Label)
            If lblOrderType.Text <> String.Empty Then
                If lblOrderType.Text = "1" Then
                    lblOrderType.Text = "1:GENERAL SO"
                ElseIf lblOrderType.Text = "2" Then
                    lblOrderType.Text = "2:EXCHANGE SO"
                ElseIf lblOrderType.Text = "3" Then
                    lblOrderType.Text = "3:RECEPTION ORDER"
                ElseIf lblOrderType.Text = "4" Then
                    lblOrderType.Text = "4:TRANSFER ORDER"
                ElseIf lblOrderType.Text = "5" Then
                    lblOrderType.Text = "5:ADVANCED ORDER"
                End If
            End If
            Dim lblItemCategory As Label = CType(e.Row.FindControl("lblItemCategory"), Label)
            If lblItemCategory.Text <> String.Empty Then
                If lblItemCategory.Text = "A" Then
                    lblItemCategory.Text = "A:COMBINED/PROCESSED PRODUCT"
                ElseIf lblItemCategory.Text = "E" Then
                    lblItemCategory.Text = "E:COST/SOFTWARE"
                ElseIf lblItemCategory.Text = "F" Then
                    lblItemCategory.Text = "F:OFFICE SUPPLIES"
                ElseIf lblItemCategory.Text = "M" Then
                    lblItemCategory.Text = "M:MATERIAL/PART/PRODUCT"
                ElseIf lblItemCategory.Text = "T" Then
                    lblItemCategory.Text = "T:TEMPLATE"
                ElseIf lblItemCategory.Text = "X" Then
                    lblItemCategory.Text = "X:VIRTUAL PRODUCTS"
                End If
            End If
            Dim lblCustomer As Label = CType(e.Row.FindControl("lblCustomer"), Label)
            If lblCustomer.Text <> String.Empty Then
                Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(lblCustomer.Text)
                If dtCustName.Rows.Count > 0 Then
                    lblCustomer.Text = dtRowsFormat.FormatSumString(dtCustName, PMAAL.CustomerID, PMAAL.CustomerName)
                End If
            End If

            Dim hplBOMDetail As HyperLink = CType(e.Row.FindControl("hlBOM"), HyperLink)
            If Not IsNothing(hplBOMDetail) Then
                Dim link As String = "&item= " & lblProductItemNo.Text
                link = link & "&spec=" & lblSpecifaction.Text
                hplBOMDetail.NavigateUrl = "PCCheckBOMPopup.aspx?height=150&width=350" & link
                hplBOMDetail.Attributes.Add("title", lblSpecifaction.Text)
            End If
            Dim hplShowDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplShowDetail) Then
                Dim link As String = ""
                Dim SO As String = String.Empty
                Dim SoType As String = String.Empty
                Dim SoSeq As String = String.Empty
                If lblSaleOrderNo.Text <> String.Empty Then
                    Dim pSO = lblSaleOrderNo.Text.Split("-")
                    SO = pSO(1)
                    Dim pType As String = pSO(0)
                    Dim Type As String = pType.Replace("JP", "")
                    SoType = Type
                    Dim iSeq As Integer = CInt(pSO(2))
                    SoSeq = iSeq.ToString("0000")
                End If
                link = link & "&SoType= " & SoType
                link = link & "&SoNo= " & SO
                link = link & "&SoSeq= " & SoSeq
                hplShowDetail.NavigateUrl = "PCConfSOPopup.aspx?height=150&width=350" & link
                hplShowDetail.Attributes.Add("title", SO)
            End If
        End If
    End Sub
    Private Shared Function GetdateOracleBaase(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared Function GetdateSQLserver(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateSQLserver", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        'gvPrint.Visible = True
        If gvShow.Rows.Count > 0 Then
            ExportsUtility.ExportGridviewToMsExcel("checkBOM" & Session("UserName"), gvShow)
        Else
            show_message.ShowMessage(Page, "Not Data Found! ", UpdatePanel1)
        End If
        'ControlForm.ExportGridViewToExcel("checkBOM" & Session("UserName"), gvPrint)
        ' gvPrint.Visible = False
    End Sub
    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim pSaleOrderNo = lbSO.Text.Split("-")
        Dim SOoracle As String = pSaleOrderNo(0) & "-" & pSaleOrderNo(1)
        Dim SoType As String = pSaleOrderNo(0).Replace("JP", "")
        Dim SoNo As String = pSaleOrderNo(1)
        Dim SoSeq As String = CInt(pSaleOrderNo(2)).ToString("0000")
        Dim DocNo As String = lbDateRec.Text
        Dim Item As String = lbItem.Text.Trim
        Dim Spec As String = lbSpec.Text.Trim
        Dim Qty As String = lbQty.Text
        Dim SaleReqDate As String = lbSaleReqDate.Text.Trim
        Dim DelDate As String = lbDelDate.Text.Trim
        Dim ConfDate As String = txtPCDate.Text
        Dim PCInDate As String = ConfDate
        Dim DocType As String = "PC"
        Dim UpPlan As String = ConfDate
        Dim CrBy As String = Session("UserName").ToString
        Dim CrDate As String = Date.Now.ToString("yyyyMMdd HH:mm")

        If ConfDate <> "" And lbPURCon.Text <> "" Then
            CDate(txtPCDate.Text).ToString("yyyy-MM-dd")
        ElseIf ConfDate = String.Empty Then
            ConfDate = " "
        End If

        If lbSaleCon.Text = "Reject" And ConfDate < lbPURCon1.Text And ConfDate <> "" Then
            show_message.ShowMessage(Page, "PC Confirm Date 1 Less Than PUR Confirm Date 1! ", UpdatePanel1)
            txtPCDate.Focus()
        ElseIf lbSaleCon.Text <> "Reject" And ConfDate < lbPURCon.Text And ConfDate <> "" Then
            show_message.ShowMessage(Page, "PC Confirm Date Less Than PUR Confirm Date! ", UpdatePanel1)
            txtPCDate.Focus()
        Else

            Dim PCUp As String = ""
            Dim PCIn As String = ""
            If lbSaleCon.Text = "Reject" Then
                PCIn = "PCConf1"
            Else
                PCIn = "PCConf"
            End If

            Dim strSqlup As String = " insert into SOConfirmDate (DocNo,DocType,soType,soNo,soSeq,Item,Spec,qty,PlanDelDate,SOReqDate," & PCIn & ",CreateBy,CreateDate)values " &
            " ('" & DocNo & "','" & DocType & "','" & SoType & "','" & SoNo & "','" & SoSeq & "','" & Item & "','" & Spec & "','" & Qty & "','" & DelDate & "','" & SaleReqDate & "','" & ConfDate & "','" & CrBy & "','" & CrDate & "')"
            Conn_SQL.Exec_Sql(strSqlup, Conn_SQL.MIS_ConnectionString)

            'Dim SQL As String = "update COPTD set " & PCUp & " = '" & ConfDate & "' where COPTD.TD001='" & SoType & "' and COPTD.TD002='" & SoNo & "' and COPTD.TD003='" & SoSeq & "' "
            'Conn_SQL.Exec_Sql(Sql, Conn_SQL.ERP_ConnectionString)
            Dim conOracle As New clsDBConnect
            Dim SqlOracle As String = " UPDATE " & XMDD.tblSaleItemDeliveryDetail & " " &
                " " & XMDD.PlaningGeneralConfrimDateSaleOrder & "='" & txtPCDate.Text & "' " &
                " where " & XMDD.wStandard & " and " & XMDD.SaleOrderNo & "='" & SOoracle & "' and " & XMDD.ItemNo & "='" & lbItem.Text & "'  "
            Try
                conOracle.QueryExecuteNonQuery(SqlOracle, conOracle.T100)
                conOracle.Close(conOracle.T100)
                Call ShowData()
                Call clear()
                show_message.ShowMessage(Page, "Update Complete", UpdatePanel1)
                If MultiView1.ActiveViewIndex <> 0 Then
                    MultiView1.SetActiveView(View1)
                End If
            Catch ex As Exception
                show_message.ShowMessage(Page, "Check Data to Save! ", UpdatePanel1)
            End Try
        End If
    End Sub
    Protected Sub gvShow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim Index = Convert.ToInt32(e.CommandArgument)
        Dim row = gvShow.Rows(Index)
        Dim lblSaleOrderNo As Label = CType(row.FindControl("lblSaleOrderNo"), Label)
        Dim lblProductItemNo As Label = CType(row.FindControl("lblProductItemNo"), Label)
        Dim lblProductItemName As Label = CType(row.FindControl("lblProductItemName"), Label)
        Dim lblSpecifaction As Label = CType(row.FindControl("lblSpecifaction"), Label)
        Dim lblCustomer As Label = CType(row.FindControl("lblCustomer"), Label)
        Dim lblSOqty As Label = CType(row.FindControl("lblSOqty"), Label)
        Dim lblSaleOrderDate As Label = CType(row.FindControl("lblSaleOrderDate"), Label)
        Dim lblPlanDeliveryDate As Label = CType(row.FindControl("lblPlanDeliveryDate"), Label)
        Dim lblSaleRequestDueDate As Label = CType(row.FindControl("lblSaleRequestDueDate"), Label)
        Dim lblPURConfirmDate As Label = CType(row.FindControl("lblPURConfirmDate"), Label)
        Dim lblPCConfirmDate As Label = CType(row.FindControl("lblPCConfirmDate"), Label)

        Dim lblPURRemark As Label = CType(row.FindControl("lblPURRemark"), Label)
        Dim lblSaleConfirm As Label = CType(row.FindControl("lblSaleConfirm"), Label)
        Dim lblPURConfirmDate1 As Label = CType(row.FindControl("lblPURConfirmDate1"), Label)
        If e.CommandName = "OnEdit" Then
            If MultiView1.ActiveViewIndex = 0 Then
                MultiView1.SetActiveView(View2)
            End If
            Dim Cust = lblCustomer.Text.Split(":")
            lbSO.Text = lblSaleOrderNo.Text
            lbItem.Text = lblProductItemNo.Text
            lbSpec.Text = lblSpecifaction.Text
            lbCust.Text = Cust(0)
            lbQty.Text = lblSOqty.Text
            lbSODate.Text = lblSaleOrderDate.Text
            lbDelDate.Text = lblPlanDeliveryDate.Text
            lbSaleReqDate.Text = lblSaleRequestDueDate.Text
            lbPURCon.Text = lblPURConfirmDate.Text
            lbPCCon.Text = lblPCConfirmDate.Text
            lbSaleCon.Text = lblSaleConfirm.Text
            lbRemark.Text = lblPURRemark.Text
            lbPURCon1.Text = lblPURConfirmDate1.Text
            txtPCDate.Focus()
        End If
    End Sub
    Private Sub ShowData()
        Dim Where As String = String.Empty
        Dim wWC As String = String.Empty
        Dim wSelectProperty As String = String.Empty
        Dim wSaleOrderNo As String = String.Empty
        Dim wwSaleOrderNoSeq As String = String.Empty
        Dim wCust As String = String.Empty
        Dim wItem As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wDocumentDateSaleDelivery As String = String.Empty
        Dim wDocumentDateSaleOrder As String = String.Empty
        '### SaleDocType 
        Dim SelectSaleTypeWhere As String = SelectCheckBoxList.MultipleSelect(UsingDocTypeSaleCheckList.getObject)
        Dim SelectSaleType As Integer = SelectCheckBoxList.RowNumSelect
        If SelectSaleType > 0 Then
            wWC = " and substr(" & XMDC.SaleOrderNo & " ,3,4 ) in(" & [String].Join("','", SelectSaleTypeWhere) & "')"
        End If
        '### ItemProperty 
        Dim iRowProperty As Integer = 0
        Dim AaryList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cblProperty.Items
            If item.Selected Then
                AaryList.Add(item.Value)
                iRowProperty = +1
            End If
        Next
        If iRowProperty > 0 Then
            Dim SelectProperty As String = " '" & [String].Join("' , '", AaryList.ToArray())
            SelectProperty = " and " & IMAA.ItemCategory & " in(" & [String].Join("','", SelectProperty) & "')"
        End If

        If tbSaleNo.Text <> String.Empty And tbSOSeq.Text <> String.Empty Then
            Dim SaleOrderNo As String = XMDC.SaleOrderNo & " ='JP" & [String].Join("','", tbSaleNo.Text) & "'"
            Dim SaleSeq As String = XMDC.ItemSequence & " ='" & [String].Join("','", tbSOSeq.Text) & "'"
            wSaleOrderNo = " and " & SaleOrderNo & " AND " & SaleSeq
        ElseIf tbSaleNo.Text <> String.Empty And tbSOSeq.Text = String.Empty Then
            wwSaleOrderNoSeq = " and " & XMDC.SaleOrderNo & "='JP" & [String].Join("','", tbSaleNo.Text) & "'"
        End If
        If tbCust.Text <> String.Empty Then
            wCust = " and " & XMDA.CustomerId & " ='" & [String].Join("','", tbCust.Text) & "'"
        End If
        If tbItem.Text <> String.Empty Then
            wItem = " and " & XMDC.Item & " Like '" & [String].Join("','", tbItem.Text) & "%'"
        End If
        If tbSpec.Text <> String.Empty Then
            wSpec = " and " & IMAAL.Specifaction & " Like '" & [String].Join("','", tbSpec.Text) & "%'"
        End If
        If tbFromDelivDate.Text <> String.Empty AndAlso tbToDelivDate.Text <> String.Empty Then
            wDocumentDateSaleDelivery = " and " & XMDK.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", tbFromDelivDate.Text) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", tbToDelivDate.Text) & "','yyyy/mm/dd')"
        End If
        If txtsFromDate.Text <> String.Empty AndAlso txtsToDate.Text <> String.Empty Then
            wDocumentDateSaleOrder = " and " & XMDA.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", txtsFromDate.Text) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", txtsToDate.Text) & "','yyyy/mm/dd')"
        End If

        Where = wWC & wSelectProperty & wSaleOrderNo & wwSaleOrderNoSeq & wCust & wItem & wSpec & wDocumentDateSaleDelivery & wDocumentDateSaleOrder
        Dim dtSaleOrder As DataTable = GetDataSaleOrder(Where)
        If dtSaleOrder.Rows.Count > 0 Then
            gvShow.DataSource = dtSaleOrder
            gvShow.DataBind()
            CountRow1.RowCount = dtSaleOrder.Rows.Count.ToString
        Else
            MessageAlert.Show(Me, "Not Data Found")
        End If
        'gvPrint.DataBind()
        'gvShow.Visible = True
        'btExport.Visible = True
        'System.Threading.Thread.Sleep(1000)
    End Sub
    Private Shared strSqlDataSaleOrder As String = "select  " & XMDC.SaleOrderNo & "||'-'||" & XMDC.ItemSequence & " as SaleOrderSeq, " &
        " " & XMDC.SaleOrderNo & "," & XMDC.ItemSequence & "," & XMDC.Item & "," & XMDA.DocumentDate & ", " &
        " " & XMDC.SalesQty & "," & XMDA.CustomerId & "," & XMDA.DocStatus & "," & XMDM.SaleDeliveryNo & "," & XMDM.ShippedQty & ",   " &
        " " & XMDC.SalesQty & "-" & XMDM.ShippedQty & " as BalacneQty," & XMDA.OrderType & "," & XMDK.DocumentDate & "," & IMAA.ItemCategory & " " &
        " from " & XMDC.tblSaleItem & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " LEFT OUTER JOIN " & XMDM.tblSaleDelivery_Body_MulitiStorel & " On " & XMDM.ItemNo & " = " & XMDC.Item & " " &
        " LEFT OUTER JOIN " & XMDK.tblSaleDelivery_Head & " On " & XMDK.SaleDeliveryNo & " = " & XMDM.SaleDeliveryNo & " " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & XMDC.Item & "  " &
        " LEFT OUTER JOIN  " & IMAA.tblProductItemDeatil & " On  " & IMAA.ItemNo & "=" & XMDC.Item & " And " & IMAA.wStandard & "  " &
        " where " & XMDA.OrderType & " in('1','2') @pWhereCustomUsing " &
        " Group By " & XMDC.SaleOrderNo & "," & XMDC.ItemSequence & "," & XMDC.Item & "," & XMDA.DocumentDate & ", " &
        " " & XMDC.SalesQty & "," & XMDA.CustomerId & "," & XMDA.DocStatus & "," & XMDM.SaleDeliveryNo & "," & XMDM.ShippedQty & ", " &
        " " & XMDA.OrderType & "," & XMDK.DocumentDate & "," & IMAA.ItemCategory & " " &
        " Order By " & XMDC.SaleOrderNo & "," & XMDC.ItemSequence & " ASC "
    Private Shared Function GetDataSaleOrder(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlDataSaleOrder
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
        clear()
        If MultiView1.ActiveViewIndex <> 0 Then
            MultiView1.SetActiveView(View1)
        End If
    End Sub

    Private Sub clear()

        lbCust.Text = ""
        lbDateRec.Text = ""
        lbDelDate.Text = ""
        lbItem.Text = ""
        lbQty.Text = ""
        lbSaleReqDate.Text = ""
        lbSO.Text = ""
        lbSpec.Text = ""

    End Sub
End Class