Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.IO
Imports System.Drawing

Public Class PlanMatIssue
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable
    Private Shared Pathfiles As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            btExport.Visible = False
        End If

    End Sub
    Private Shared strSqlDataPlanMatIssue As String = "Select " & SFAA.DocNo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " & SFBA.MasterItemNo & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFBA.OperationNo & "," & SFBA.OperationSeq & "," &
        " " & SFBA.BOMitem & "," & SFBA.IssueItem & "," & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & "," &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFBA.RecoilMaterial & "," & SFBA.ConsPurchase & ", " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & ", " &
        " " & SFAA.ProductionQty & "," & XMDA.CustomerId & "," & SFDA.Status & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & ", " &
        " " & SFDC.IssueDocNo & "," & SFDA.ProductionDept & "," & SFDA.DocumentDate & "," & SFDA.PostingDate & "," & SFDA.Applicant & "," & SFDB.Expectes_Sets & "," & SFDB.Actual_Sets & ", " &
        " " & SFAA.Unit & "," & SFBA.StandardIssuanceQuantity & "," & SFBA.IssuedQty & "," & SFBA.Unit & "," & SFBA.StandardIssuanceQuantity & "-" & SFBA.IssuedQty & " as " & SFBA.UnIssuedQty & " " &
        " FROM " & SFBA.tblManufactureOrder_Body & " " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFAA.tblMO & "." & SFAA.DocNo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFCA.tblMO_Detail & "." & SFCA.DocNo & "  " &
        " AND  " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & "  " &
        " LEFT OUTER JOIN  " & SFDC.tblMatIssueDistribution & " On " & SFDC.tblMatIssueDistribution & "." & SFDC.WONo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " AND  " & SFDC.tblMatIssueDistribution & "." & SFDC.RequirementItem_No & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " " &
        " LEFT OUTER JOIN  " & SFDB.tblMatIssueSet & " On " & SFDB.tblMatIssueSet & "." & SFDB.IssueDocNo & " = " & SFDC.tblMatIssueDistribution & "." & SFDC.IssueDocNo & " " &
        " AND  " & SFDB.tblMatIssueSet & "." & SFDB.WONo & " = " & SFDC.tblMatIssueDistribution & "." & SFDC.WONo & " " &
        " LEFT OUTER JOIN  " & SFDA.tblMatIssueHead & " On " & SFDA.tblMatIssueHead & "." & SFDA.IssueDocNo & " = " & SFDC.tblMatIssueDistribution & "." & SFDC.IssueDocNo & " " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & "  " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " Left OUTER JOIN " & XMDM.tblSaleDelivery_Body_MulitiStorel & " On " & XMDM.tblSaleDelivery_Body_MulitiStorel & "." & XMDM.ItemNo & " = " & XMDC.tblSaleItem & "." & XMDC.Item & " " &
        " LEFT OUTER JOIN " & XMDK.tblSaleDelivery_Head & " On " & XMDK.tblSaleDelivery_Head & "." & XMDK.SaleDeliveryNo & " = " & XMDM.tblSaleDelivery_Body_MulitiStorel & "." & XMDM.SaleDeliveryNo & " " &
        " where (" & SFBA.wStandard & "  @pWhereCustomUsing )  " &
        " Group By " & SFAA.DocNo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " & SFBA.MasterItemNo & "," &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFBA.OperationNo & "," & SFBA.OperationSeq & "," &
        " " & SFBA.BOMitem & "," & SFBA.IssueItem & "," & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & "," &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFBA.RecoilMaterial & "," & SFBA.ConsPurchase & ",  " &
         " " & SFAA.ScarpQty & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & ", " &
        " " & SFAA.ProductionQty & "," & XMDA.CustomerId & "," & SFDA.Status & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & ", " &
        " " & SFDC.IssueDocNo & "," & SFDA.ProductionDept & "," & SFDA.DocumentDate & "," & SFDA.PostingDate & "," & SFDA.Applicant & "," & SFDB.Expectes_Sets & "," & SFDB.Actual_Sets & ", " &
        " " & SFAA.Unit & "," & SFBA.StandardIssuanceQuantity & "," & SFBA.IssuedQty & "," & SFBA.Unit & " " &
        " Order By " & SFAA.DocNo & "," & SFBA.LineSequence & "," & SFBA.OperationNo & "," & SFDC.IssueDocNo & ", " &
        " " & SFBA.BOMitem & "," & SFCA.RunCardNo & " ASC "
    Private Shared Function GetPlanMatIssueDataTable(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlDataPlanMatIssue
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
            Dim getFunction As String = "GetPlanMatIssueDataTable"
            Dim getsql As String = "strSqlDataPlanMatIssue"
            GetPageError.GetPage(Pathfiles, getFunction, getsql, ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub GetSubDataPlanMatIssue()
        Dim Where As String = String.Empty
        Dim strSO_Type As String = UsingDocTypeSale.getObject.Text
        Dim strSON_No As String = txtSaleNo.Text
        Dim SaleSeq As String = txtSaleSeq.Text
        Dim CustomerID As String = txtCust.Text
        Dim saleDate As String = txtSaleDate.Text
        Dim MO_Type As String = UsingMO_Type.getObject.Text
        Dim MO_NoFrom As String = txtWorkNoFrom.Text
        Dim MO_NoTo As String = txtWorkNoTo.Text
        Dim Item_No As String = txtItem.Text
        Dim Spec As String = txtSpec.Text
        Dim MO_F_Date As String = MO_FormDate.Text
        Dim MO_T_Date As String = MO_ToDate.Text
        Dim status As String = UsingStatusMO_Normal.getObject.Text

        Dim IssuesType As String = UsingMat_IssueType.getObject.Text
        Dim IssueDocNo As String = txtIssueDocNo.Text
        Dim Issuestatus As String = DLIssueType.SelectedValue

        Dim wSaleOrderNo As String = String.Empty
        Dim wSaleOrderNoSeq As String = String.Empty
        Dim wMO_Between As String = String.Empty
        Dim wItem_No As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wCustomerID As String = String.Empty
        Dim wSaleDate As String = String.Empty
        Dim wMO_Date As String = String.Empty
        Dim wMODate_Staus As String = String.Empty
        Dim wIssueDocNo As String = String.Empty
        Dim wRecoilMaterial As String = String.Empty
        Dim wstatus As String = String.Empty

        If (strSO_Type <> "0") And (strSON_No <> String.Empty) And CheckRecoilMat.Checked = False Then
            Dim pstrSaleNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, txtSaleNo.Text)
            wSaleOrderNo = " and " & XMDA.SaleOrderNo & "= '" & [String].Join("','", pstrSaleNo) & "'"
        End If
        If (strSO_Type <> "0") And (strSON_No <> String.Empty) And SaleSeq <> String.Empty And CheckRecoilMat.Checked = False Then
            Dim pstrSaleNo As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, txtSaleNo.Text)
            Dim pSaleNo As String = XMDA.SaleOrderNo & "= '" & [String].Join("','", pstrSaleNo) & "'"
            Dim pSaleSeq As String = XMDC.ItemSequence & "= '" & [String].Join("','", SaleSeq) & "'"
            wSaleOrderNoSeq = " and " & pSaleNo & " AND " & pSaleSeq
        End If
        If MO_Type <> "0" And MO_NoFrom <> String.Empty And MO_NoTo <> String.Empty Then
            Dim MO_From As String = " '" & [String].Join("','", ReplaceString.ReplaceMO(MO_Type, MO_NoFrom)) & "'"
            Dim MO_To As String = " '" & [String].Join("','", ReplaceString.ReplaceMO(MO_Type, MO_NoTo)) & "'"
            wMO_Between = " and " & SFAA.DocNo & " BETWEEN " & MO_From & " AND " & MO_To
        End If
        If Item_No <> String.Empty And CheckRecoilMat.Checked = False Then
            wItem_No = " and " & SFBA.BOMitem & " Like '" & [String].Join("','", Item_No) & "%'"
        End If
        If Spec <> String.Empty And CheckRecoilMat.Checked = False Then
            wSpec = " and " & IMAAL.Specifaction & " Like '%" & [String].Join("','", Spec) & "%'"
        End If
        If CustomerID <> String.Empty And CheckRecoilMat.Checked = False Then
            wCustomerID = " and " & XMDA.CustomerId & " = '" & [String].Join("','", CustomerID) & "'"
        End If
        If saleDate <> String.Empty And CheckRecoilMat.Checked = False Then
            wSaleDate = " and " & XMDA.DocumentDate & " = TO_DATE('" & [String].Join("','", saleDate) & "','yyyy/mm/dd') "
        End If
        If (MO_F_Date <> String.Empty And MO_T_Date <> String.Empty) And status = "0" And CheckRecoilMat.Checked = False Then
            wMO_Date = " and " & SFAA.PlanStartDate & " >= TO_DATE('" & [String].Join("','", MO_F_Date) & "','yyyy/mm/dd') AND " & SFAA.PlanedCompletionDate & " <= TO_DATE('" & [String].Join("','", MO_T_Date) & "','yyyy/mm/dd')"
        End If
        If (MO_F_Date <> String.Empty And MO_T_Date <> String.Empty) And status <> "0" And CheckRecoilMat.Checked = False Then
            Dim MODate As String = SFAA.PlanStartDate & " >= TO_DATE('" & [String].Join("','", MO_F_Date) & "','yyyy/mm/dd') AND " & SFAA.PlanedCompletionDate & " <= TO_DATE('" & [String].Join("','", MO_T_Date) & "','yyyy/mm/dd')"
            Dim pStatus As String = SFAA.Status & " = '" & [String].Join("','", status) & "'"
            wMODate_Staus = " and " & MODate & " AND " & pStatus
        End If
        If status <> "0" And (MO_F_Date = String.Empty And MO_T_Date = String.Empty) And (MO_Type = "0" Or MO_NoFrom = String.Empty Or MO_NoTo = String.Empty) And CheckRecoilMat.Checked = False Then
            wstatus = " and " & SFAA.Status & " = '" & [String].Join("','", status) & "'"
        End If
        Dim pStrIssueDocNo As String = String.Empty
        If (UsingMat_IssueType.getObject.Text <> "0") And (IssueDocNo <> String.Empty) And CheckRecoilMat.Checked = False And Issuestatus = "0" Then
            pStrIssueDocNo = ReplaceString.ReplaceJP(UsingMat_IssueType.getObject.Text, IssueDocNo)
            wIssueDocNo = " and " & SFDC.IssueDocNo & "= '" & [String].Join("','", pStrIssueDocNo) & "'"
        ElseIf (UsingMat_IssueType.getObject.Text <> "0") And (IssueDocNo <> String.Empty) And CheckRecoilMat.Checked = False And Issuestatus <> "0" Then
            pStrIssueDocNo = ReplaceString.ReplaceJP(UsingMat_IssueType.getObject.Text, IssueDocNo)
            Dim strIssueDocNo As String = SFDC.IssueDocNo & "= '" & [String].Join("','", pStrIssueDocNo) & "'"
            Dim pIssuestatus As String = SFDA.Status & "= '" & [String].Join("','", Issuestatus) & "'"
            wIssueDocNo = " and " & strIssueDocNo & " AND " & pIssuestatus
        ElseIf (UsingMat_IssueType.getObject.Text = "0") And (IssueDocNo = String.Empty) And CheckRecoilMat.Checked = False And Issuestatus <> "0" Then
            wIssueDocNo = " and " & SFDA.Status & "= '" & [String].Join("','", Issuestatus) & "'"
        End If
        If (UsingMat_IssueType.getObject.Text = "0" And IssueDocNo = String.Empty) And
            (MO_Type = "0" And MO_NoFrom = String.Empty And MO_NoTo = String.Empty) And (Item_No = String.Empty) And (Spec = String.Empty) And
            (CustomerID = String.Empty) And (saleDate = String.Empty) And ((strSO_Type = "0") And (strSON_No = String.Empty)) And
            ((MO_F_Date = String.Empty And MO_T_Date = String.Empty) And status = "0") And (SaleSeq = String.Empty) Then

            If CheckRecoilMat.Checked = True And DLConsPurchase.SelectedValue = "0" And Issuestatus = "0" Then
                wRecoilMaterial = " and " & SFBA.RecoilMaterial & "= '" & [String].Join("','", "Y") & "'"
            End If
            If CheckRecoilMat.Checked = False And DLConsPurchase.SelectedValue = "0" And Issuestatus = "0" Then
                wRecoilMaterial = " and " & SFBA.RecoilMaterial & "= '" & [String].Join("','", "N") & "'"
            ElseIf CheckRecoilMat.Checked = False And DLConsPurchase.SelectedValue <> "0" And Issuestatus = "0" Then
                Dim pConPur As String = String.Empty
                Dim pRecoilMat As String = String.Empty
                pRecoilMat = SFBA.RecoilMaterial & "= '" & [String].Join("','", "N") & "'"
                If DLConsPurchase.SelectedValue = "1" Then
                    pConPur = SFBA.ConsPurchase & " > '" & [String].Join("','", "0") & "'"
                End If
                If DLConsPurchase.SelectedValue = "2" Then
                    pConPur = SFBA.ConsPurchase & " = '" & [String].Join("','", "0") & "'"
                End If
                wRecoilMaterial = " and " & pRecoilMat & " AND " & pConPur
            ElseIf CheckRecoilMat.Checked = True And DLConsPurchase.SelectedValue <> "0" And Issuestatus = "0" Then
                Dim pConPur As String = String.Empty
                Dim pRecoilMat As String = String.Empty
                pRecoilMat = SFBA.RecoilMaterial & "= '" & [String].Join("','", "Y") & "'"
                If DLConsPurchase.SelectedValue = "1" Then
                    pConPur = SFBA.ConsPurchase & " > '" & [String].Join("','", "0") & "'"
                End If
                If DLConsPurchase.SelectedValue = "2" Then
                    pConPur = SFBA.ConsPurchase & " = '" & [String].Join("','", "0") & "'"
                End If
                wRecoilMaterial = " and " & pRecoilMat & " AND " & pConPur
            End If
        End If

        Where = wSaleOrderNo & wSaleOrderNoSeq & wMO_Between & wItem_No & wSpec & wCustomerID & wSaleDate & wMO_Date & wMODate_Staus & wIssueDocNo & wRecoilMaterial & wstatus
        'lblSql.Text = GetPlanMatIssueDataTable(Where)
        If Where <> String.Empty Then
            Dim dt As DataTable = GetPlanMatIssueDataTable(Where)
            If dt.Rows.Count > 0 Then
                gvShow.DataSource = dt
                gvShow.DataBind()
                ' GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                'GridviewUtility.GridStyleTemplate_Std(gvShow)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "gridviewScrollShow();", True)
                CountRow1.RowCount = dt.Rows.Count.ToString
                btExport.Visible = True
            Else
                gvShow.DataSource = New List(Of String)
                gvShow.DataBind()
                'GridviewUtility.GridStyleTemplate_Std(gvShow)
                'GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
                btExport.Visible = False
            End If
        Else
            gvShow.DataSource = New List(Of String)
            gvShow.DataBind()
            GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
        End If
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("MaterialsIssueStatus" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call GetSubDataPlanMatIssue()
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblAppliedQty2 As Label = CType(e.Row.FindControl("lblAppliedQty2"), Label)
            Dim lblActualQty2 As Label = CType(e.Row.FindControl("lblActualQty2"), Label)
            Dim lblUnit2 As Label = CType(e.Row.FindControl("lblUnit2"), Label)
            Dim lblWH2 As Label = CType(e.Row.FindControl("lblWH2"), Label)
            Dim lblRecoilMat As Label = CType(e.Row.FindControl("lblRecoilMat"), Label)
            Dim ChkRecoilMat As CheckBox = CType(e.Row.FindControl("ChkRecoilMat"), CheckBox)
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim S_cellIssueDocNo = e.Row.Cells(1).Text
            Dim S_cellSO = e.Row.Cells(15).Text
            Dim S_cellMO = e.Row.Cells(13).Text
            Dim S_cellCustomer = e.Row.Cells(14).Text
            Dim S_cellMasterItem = e.Row.Cells(17).Text
            Dim S_cellBOM = e.Row.Cells(27).Text
            e.Row.ToolTip = "Click to select this row." & vbNewLine & "Issue DocNo: " & S_cellIssueDocNo & vbNewLine & "SO No : " & S_cellSO & vbNewLine &
               "Customer: : " & S_cellCustomer & vbNewLine & "MO DocNo : " & S_cellMO & vbNewLine & "ProductionItemNo: " & S_cellMasterItem & vbNewLine & "BOM ItemNo: " & S_cellBOM
            Dim MO_DocNo As String = e.Row.Cells(15).Text
            Dim IssueDocNo As String = e.Row.Cells(1).Text
            Dim BOM_ItemNo As String = e.Row.Cells(27).Text
            Dim W_MO As String = SFDC.WONo & " ='" & [String].Join("','", MO_DocNo) & "'"
            Dim W_IssueDocNo As String = SFDC.IssueDocNo & " = '" & [String].Join("','", IssueDocNo) & "'"
            Dim W_BOM_ItemNo As String = SFDC.RequirementItem_No & " = '" & [String].Join("','", BOM_ItemNo) & "'"
            Dim Where As String = W_IssueDocNo & " AND " & W_MO & " AND " & W_BOM_ItemNo
            If MO_DocNo <> String.Empty And IssueDocNo <> String.Empty And BOM_ItemNo <> String.Empty Then
                Dim dtSFDC1 As DataTable = SFDC.GetMatIssueBy_Multi(Where)
                If dtSFDC1.Rows.Count > 0 Then
                    lblAppliedQty2.Text = dtRowsFormat.FormatString(dtSFDC1, SFDC.AppliciedQty)
                    If lblAppliedQty2.Text <> String.Empty Then
                        Dim dblNumberAppliedQty2 As Double = CDbl(lblAppliedQty2.Text)
                        lblAppliedQty2.Text = String.Format("{0:n3}", dblNumberAppliedQty2)
                    End If
                    lblActualQty2.Text = dtRowsFormat.FormatString(dtSFDC1, SFDC.ActualQty)
                    If lblActualQty2.Text <> String.Empty Then
                        Dim dblNumberActualQty As Double = CDbl(lblActualQty2.Text)
                        lblActualQty2.Text = String.Format("{0:n3}", dblNumberActualQty)
                    End If
                    lblUnit2.Text = dtRowsFormat.FormatString(dtSFDC1, SFDC.Unit)
                    lblWH2.Text = dtRowsFormat.FormatString(dtSFDC1, SFDC.SpecifiesWH)
                    If lblWH2.Text <> String.Empty Then
                        Dim dtWH2 As DataTable = INAA.GetWarehouseFind_Table(lblWH2.Text)
                        If dtWH2.Rows.Count > 0 Then
                            lblWH2.Text = dtRowsFormat.FormatSumString(dtWH2, INAA.WharehouseID, INAA.Warehouse)
                        End If
                    End If
                End If
            End If
            Dim sApplicant As String = e.Row.Cells(5).Text
            If sApplicant <> String.Empty Then
                Dim dtApplicant As DataTable = OOAG.GetEmployeeNo(sApplicant)
                If dtApplicant.Rows.Count > 0 Then
                    Dim ApplicantName = dtRowsFormat.FormatString(dtApplicant, OOAG.Name) & " " & dtRowsFormat.FormatString(dtApplicant, OOAG.Surname)
                    e.Row.Cells(5).Text = dtRowsFormat.FormatString(dtApplicant, OOAG.EmployeeNo) & " : " & ApplicantName
                End If
            End If
            Dim sProDept As String = e.Row.Cells(4).Text
            If sProDept <> String.Empty Then
                Dim dtDept As DataTable = OOEFL.GetDepartmentFind_Table(sProDept)
                If dtDept.Rows.Count > 0 Then
                    Dim strDept = dtRowsFormat.FormatString(dtDept, OOEFL.Dept)
                    e.Row.Cells(4).Text = dtRowsFormat.FormatString(dtDept, OOEFL.DeptID) & " : " & strDept
                End If
            End If
            Dim IssueStatusT As String = e.Row.Cells(12).Text
            If IssueStatusT <> String.Empty Then
                If IssueStatusT = "Y" Or IssueStatusT = "y" Then
                    e.Row.Cells(12).ForeColor = System.Drawing.ColorTranslator.FromHtml("#003300")
                ElseIf IssueStatusT = "s" Or IssueStatusT = "S" Then
                    e.Row.Cells(12).ForeColor = System.Drawing.ColorTranslator.FromHtml("#b38f00")
                ElseIf IssueStatusT = "N" Or IssueStatusT = "n" Then
                    e.Row.Cells(12).ForeColor = System.Drawing.Color.Maroon
                End If
                e.Row.Cells(12).Text = StatusT100.MaterailIssue(e.Row.Cells(12).Text)
            End If
            Dim pStatusT As String = e.Row.Cells(16).Text
            If pStatusT <> String.Empty Then
                If pStatusT = "C" Then
                    e.Row.Cells(16).ForeColor = System.Drawing.Color.Green
                ElseIf pStatusT = "M" Then
                    e.Row.Cells(16).ForeColor = System.Drawing.Color.YellowGreen
                ElseIf pStatusT = "N" Then
                    e.Row.Cells(16).ForeColor = System.Drawing.Color.Maroon
                ElseIf pStatusT = "F" Then
                    e.Row.Cells(16).ForeColor = System.Drawing.ColorTranslator.FromHtml("#008ae6")
                ElseIf pStatusT = "Y" Then
                    e.Row.Cells(16).ForeColor = System.Drawing.ColorTranslator.FromHtml("#b35900")
                End If
                e.Row.Cells(16).Text = StatusT100.MO_Normal(e.Row.Cells(16).Text)
            End If
            Dim RecoilMat As String = lblRecoilMat.Text
            If RecoilMat <> String.Empty Then
                If RecoilMat = "N" Then
                    ChkRecoilMat.Checked = False
                ElseIf RecoilMat = "Y" Then
                    ChkRecoilMat.Checked = True
                End If
            End If
            Dim RecardDetail As String = e.Row.Cells(23).Text
            If RecardDetail <> String.Empty Then
                If RecardDetail = "1" Then
                    e.Row.Cells(23).Text = "1 : GENERAL"
                ElseIf RecardDetail = "2" Then
                    e.Row.Cells(23).Text = "2 : REWORK"
                End If
            End If
        End If
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Call GetSubDataPlanMatIssue()
        'btExport.Visible = True
        'System.Threading.Thread.Sleep(1000)
    End Sub

End Class