Public Class CheckBOM
    Inherits System.Web.UI.Page
    Dim ContDtFormOrl As New ContDtFormOrl
    Dim connect As New clsDBConnect
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") Is Nothing Or Session("UserName") = "" Then
                'Response.Redirect("../Login.aspx")
            Else

            End If
            HeaderFormT1001.HeaderLable = ContDtFormOrl.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
        Scollbar()
    End Sub
    '--'Save To Excel File
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    '--ShowScollbar สกอบาร์
    Private Sub Scollbar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GvChkBomScrollbar", "GvChkBomScrollbar();", True)
    End Sub

    '--Export Excel
    Protected Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        ContDtFormOrl.ExportGridViewToExcel("GvChkBom" & Session("UserName"), GvChkBom)
    End Sub

    '--Search
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ShowGvChkBom(GvChkBom, True, NowPageIndex.Text)
        CountRow1.RowCount = ContDtFormOrl.RowGridview(GvChkBom)
    End Sub

    '--ShowGvChkBom
    Private Sub ShowGvChkBom(ByRef GridName As GridView, ByVal HasShow As Boolean, ByVal PageIndex As Integer)
        Dim Whr As String = "",
            CustNo As String = txtCustID.Text.Trim,
            SONo As String = txtSONo.Text.Trim,
            Ver As String = txtVersion.Text.Trim,
            Item As String = txtItemNo.Text.Trim,
            Spac As String = txtSpec.Text.Trim,
            Condition As String = ddlCondition.SelectedValue,
            DateFrom As String = DateT1001.Text,
            DateTo As String = DateT1002.Text,
            SOTypeCheckList As String = SelectCheckBoxList.MultipleSelect(UsingTypeSaleCheckList1.getObject),
            SOTypeRows As Integer = SelectCheckBoxList.RowNumSelect

        If (SOTypeRows > 0) Then
            Whr = Whr & " and substr(" & XMDA.SaleOrderNo & ",3,4)" & " in(" & [String].Join("','", SOTypeCheckList) & "')"
        End If
        If Condition = "Y" Then
            Whr = Whr & " and " & XMDA.Approved & ""
        ElseIf Condition = "N" Then
            Whr = Whr & " and " & XMDA.Unconfirmed & ""
            Whr = Whr & " and case when (select count(*) from " & BMAA.tblBOMheader & " where " & BMAA.Approved & "and " & BMAA.WStandard & " and " & BMAA.MasterItemNo & "=" & XMDC.Item & ")=0 "
        End If
        If CustNo <> "" Then
            Whr = Whr & " and " & XMDA.CustomerId & "='" & CustNo & "'"
        End If
        If SONo <> "" And Ver <> "" Then
            Whr = Whr & " and " & XMDA.SaleOrderNo & "= '" & SONo & "' and " & XMDA.VersionNo & "= '" & Ver & "'"
        End If
        If Item <> "" And Spac <> "" Then
            Whr = Whr & " and " & XMDC.Item & "= '" & Item & "' and " & IMAAL.Specifaction & "= '" & Spac & "'"
        End If
        If DateFrom <> "" And DateTo <> "" Then
            Whr = Whr & " and " & XMDC.BookShippingDate & " BETWEEN TO_DATE ('" & DateFrom & "', 'yyyy/mm/dd') and TO_DATE ('" & DateTo & "', 'yyyy/mm/dd')"
        End If

        'ชื่อคอลัมน์หัวตาราง  
        Dim ShowFiled As String = "SONo,ItemNo,Desc,Spec,Category,SOQty,DelQty,BalacneQty,Status,Industry,Cust,DelDate,SLReqDueDate,BOM,PRQty,LastBOMUpdate"
        'ตัด , ออก
        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")
        'seting column
        Dim DtShow, dt, dt1, dt2, dt3, dt4 As New Data.DataTable
        If DtShow.Rows.Count = 0 Then
            ContDtFormOrl.DataTableTranf(ArrayShowFiled, DtShow)
        End If

        '--Shearch SaleOrder  where whr /not Refresh DataTable
        XMDA.WhrChkBOM(Whr, dt) '1
        For f As Integer = 0 To dt.Rows.Count - 1
            Dim ShearchSLOrder As String = "", ShearchDocDate As String = "",
                ShearchItemNo As String = "", ShearchItemName As String = "",
                ShearchItemSpec As String = "", ShearchCategory As String = "",
                ShearchReqQty As String = "0", ShearchDelQty As String = "0",
                ShearchStus As String = "-", ShearchClssfctDesc As String = "",'Classification Description
                ShearchCustid As String = "", ShearchDelDate As String = "",
                ShearchSLReqDueDate As String = "-", ShearchBOM As String = "",
                ShearchLastBOMUpdate As String = "-"
            Dim KeySO As String = "", KeySeq As String = ""
            If dt.Rows.Count > 0 Then
                ShearchSLOrder = dt.Rows(f).Item("SO") '*
                ShearchItemNo = dt.Rows(f).Item("xmdc001") '*
                ShearchItemName = dt.Rows(f).Item("imaal003") '*
                ShearchItemSpec = dt.Rows(f).Item("imaal004") '*
                ShearchCategory = dt.Rows(f).Item("ItemCategory") '*

                ShearchReqQty = dt.Rows(f).Item("xmdc007") '*
                Dim Comma As Decimal = CDec(ShearchReqQty)
                ShearchReqQty = String.Format("{0:n0}", Comma)

                ShearchDelQty = dt.Rows(f).Item("xmdd014") '*
                Dim Comma1 As Decimal = CDec(ShearchDelQty)
                ShearchDelQty = String.Format("{0:n0}", Comma1)

                'ShearchStus = dt.Rows(f).Item("")'*
                ShearchClssfctDesc = dt.Rows(f).Item("rtaxl003") '*
                ShearchCustid = dt.Rows(f).Item("Cust") '* 
                ShearchDelDate = dt.Rows(f).Item("xmdc012") '*
                'ShearchSLReqDueDate = dt.Rows(f).Item("")'*
                ShearchBOM = dt.Rows(f).Item("BOM") '*
                ShearchLastBOMUpdate = dt.Rows(f).Item("LastBOMUpdate") '*
                KeySO = dt.Rows(f).Item("xmdadocno")
                KeySeq = dt.Rows(f).Item("xmdcseq")
            Else
                Exit Sub
            End If
            '--Sum SampleReqQty for One SO
            dt2 = XMDC.smpReqForOneSO(KeySO, KeySeq, ShearchItemNo)
            Dim ReqQtySample As Integer = 0
            If dt2.Rows.Count > 0 Then
                ReqQtySample = dt2.Rows(0).Item("SampleReqQty") '*
            End If
            '--Sum SampleDeliveryQty for One SO
            dt3 = XMDC.smpDelForOneSO(KeySO, KeySeq, ShearchItemNo)
            Dim sampledelQty As Integer = 0
            If dt3.Rows.Count > 0 Then
                sampledelQty = dt3.Rows(0).Item("sampleDeliQty") '*
            End If
            '--Sum UnDeliveryQty
            Dim SumUndelQty As Integer = 0,
                UndelQty As String = "0"
            SumUndelQty = (ShearchReqQty + ReqQtySample - ShearchDelQty - sampledelQty)
            If SumUndelQty > 0 Then
                UndelQty = SumUndelQty
                Dim Comma As Decimal = CDec(UndelQty)
                UndelQty = String.Format("{0:n0}", Comma)
            End If
            Dim PR As String = "0"
            DtShow.Rows.Add(New Object() {ShearchSLOrder, ShearchItemNo, ShearchItemName, ShearchItemSpec, ShearchCategory, ShearchReqQty, ShearchDelQty, UndelQty, ShearchStus, ShearchClssfctDesc, ShearchCustid, ShearchDelDate, ShearchSLReqDueDate, ShearchBOM, PR, ShearchLastBOMUpdate})
        Next

        'Input Gridview 
        GvChkBom.DataSource = DtShow
        GvChkBom.DataBind()

        Dim a As Integer = 0
        a = GvChkBom.Rows.Count
        If a = 0 Then
        Else
            '--Gridview row color Onmouse
            GridviewUtility.GrigOnmouseHandleCustomer(GvChkBom, "#FFCC99")
        End If
    End Sub

    '--HyperLink GvChkBom 
    Protected Sub GvChkBom_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvChkBom.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim hplDetail As HyperLink = CType(.FindControl("ShowDetail"), HyperLink)
                Dim item As String = .DataItem("ItemNo")
                If Not IsNothing(hplDetail) And Not IsDBNull(.DataItem("ItemNo")) Then
                    Dim link As String = ""
                    link = link & "&item=" & .DataItem("ItemNo")
                    link = link & "&Spec=" & .DataItem("Spec")
                    hplDetail.NavigateUrl = "CheckBOMPopUp.aspx?height=150&width=350" & link
                    hplDetail.Attributes.Add("title", item)
                    hplDetail.Target = "_blank"
                End If
            End If
        End With
    End Sub
End Class