Imports System.Data

Public Class not_approve
    Inherits System.Web.UI.Page


    '------------------- Production Module - Not Approve -------------------
    '                      Original Code Module - JODS
    '                 Version 4 by Pattavee Narumonchavalit
    '-----------------------------------------------------------------------

    'Declare SearchType
    'attn all param will be splitted by "-" between param & param description. (Hardcode)
    'exp. Code-Code's description in case to add more or remove some element out.

    'Declare Class
    Dim ECAA As New ECAA   'WC List
    Dim SFAA As New SFAA   'WO Header Status
    Dim SFDA As New SFDA   'All Mat Header Status 
    Dim SFFB As New SFFB   'TO (Transfer Order) Header Status Separate
    Dim SFIA As New SFIA   'Rework Header Status Separate
    Dim SFEA As New SFEA   'MO Receipt Header Status Separate
    Dim QCBA As New QCBA   'QC Header Status Separate
    Dim ClsDbcon As New clsDBConnect
    Dim clstemp As New clsJODST100_temp_NAPPlist   'Connect to temptable

    'Dim paramType() As String = {"D1-Production Input", "D2-Transfer", "D3-MO Receive", "D4-Materials Issue", "D5-Inventory Transaction Order", "D6-Purchase Receipt Inspection", "D7-MO Receipt Inspection", "D8-Subcontract Receipt Inspection", "D9-Transfer Inspection", "D10-Sale Return Inspection", "D11-Sales Delivery", "D12-Purchase Receipt", "D13-Loan/Borrow Order", "D14-Loan/Borrow", "D15-Asset PR", "D16-Scrap Order"}
    'Dim paramTypeT100() As String = {"asft300-Maintain Work Order", "asft301-WO Process Maintainance", "asft311-Maintain Work Order Materials Issued in Sets", "asft321-Maintain Work Order Materials Returned in Sets", "asft312-Maintain Work Order Acquisition Surplus", "asft322-Maintain Work Order Acquisition Return/Surplus", "asft313-Maintain Work Order Shortage and Replenishment", "asft323-Maintain General Material Return for Work Orders", "asft335-Daily WO Maintenanace : Single Row Type", "asfp330-Manufacturing Immediate Work Reporting Operations", "asft338-Work Order Remanufacturing Transfer Out Operations", "aqct300-Quality Inspection Record Maintenance", "asft340-Work Order Completed Storage Operations"}
    Dim paramTypeT100() As String = {"asft300-Maintain Work Order", "asft311-Maintain Work Order Materials Issued in Sets", "asft335-Daily WO Maintenance : Single Row Type", "asft338-Work Order Remanufacturing Transfer Out Operations", "aqct300-Quality Inspection Record Maintenance", "asft340-Work Order Completed Storage Operations"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
            Dim usrnamelogged As String = Session("UserName")
            clstemp.ClearTempRec(usrnamelogged)
            Dim show As String = Request.Form("doreport")
            If show = "Show" Then
                Dim workcenterID As String = "sffb009"
                Dim rt_str() As String = Request.Form.GetValues("schtype")
                Dim wc_str() As String = Request.Form.GetValues("wc")
                Dim seldate As String = Request.Form("dateTo")
                Dim recstat As String = Request.Form("recstat")
                Dim rt_num As Integer = 0
                Dim wc_num As Integer = 0
                Dim in_wcstr As String = ""
                Dim modulecodename As String = ""
                Dim allnoneapprove_encounter As Integer = 0
                Dim noneapprove_encounter As Integer = 0
                Dim recordnum As Integer = 1
                If rt_str Is Nothing Then
                Else
                    rt_num = rt_str.Length
                End If
                If wc_str Is Nothing Then
                Else
                    wc_num = wc_str.Length
                End If
                Dim reptype As String = Request.Form("reptype")
                If reptype = 0 Then     'Summary
                    doGenerateReport(reptype)
                    If wc_num <> 0 Then
                        For z = 0 To wc_num - 1
                            If z = 0 Then
                                in_wcstr = "AND " & workcenterID & " IN("
                                in_wcstr = in_wcstr + createStrWC(wc_str(z))
                                If wc_str(z) <> "on" Then
                                    If wc_num = 1 Then
                                        in_wcstr = in_wcstr + ")"
                                    Else
                                        in_wcstr = in_wcstr + ","
                                    End If
                                Else
                                    If wc_str(z) <> "on" Then
                                        in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                                    Else
                                    End If
                                End If
                            ElseIf z = wc_num - 1 Then
                                in_wcstr = in_wcstr + createStrWC(wc_str(z))
                                in_wcstr = in_wcstr + ")"
                            Else
                                in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                            End If
                        Next
                    Else
                    End If
                    For i = 0 To rt_num - 1
                        noneapprove_encounter = getNotapproveRec(rt_str(i), recstat, in_wcstr)
                        If rt_str(i) = "asft300" Then
                            rt_str(i) = rt_str(i) + "-asft301"
                        ElseIf rt_str(i) = "asft311" Then
                            rt_str(i) = rt_str(i) + "-asft321-asft312-asft322-asft313-asft323"
                        Else
                        End If
                        modulecodename = getModulename(rt_str(i))
                        clstemp.InsertTempRecord(recordnum, rt_str(i), modulecodename, "", "", noneapprove_encounter, reptype, usrnamelogged)
                        allnoneapprove_encounter = allnoneapprove_encounter + noneapprove_encounter
                        noneapprove_encounter = 0
                        recordnum = recordnum + 1

                        If rt_str(i) = "asft300-asft301" Then
                            rt_str(i) = "asft300"
                        End If
                        If rt_str(i) = "asft311-asft321-asft312-asft322-asft313-asft323" Then
                            rt_str(i) = "asft311"
                        End If
                    Next
                    Call ShowGridNotApproved_Sum()
                Else     'Detail
                    doGenerateReport(reptype)
                    For i = 0 To rt_num - 1
                        Dim r As Integer = 0
                        Dim k As Integer = 0
                        Dim ds As DataSet
                        Dim wds As DataSet
                        Dim docNo As String = ""
                        Dim docDate As String = ""
                        Dim wc As String = ""
                        Dim wcname As String = ""
                        Dim MOno As String = ""
                        ds = getNotapproveData(rt_str(i))
                        r = ds.Tables(0).Rows.Count
                        If r <> 0 Then
                            For r = 0 To r - 1
                                If rt_str(i) = "asft300" Then
                                    rt_str(i) = rt_str(i) + "-asft301"
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(3)   'docDate
                                ElseIf rt_str(i) = "asft311" Then
                                    rt_str(i) = rt_str(i) + "-asft321-asft312-asft322-asft313-asft323"
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(1)   'docDate
                                ElseIf rt_str(i) = "asft335" Then
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(1)   'docDate
                                    MOno = ds.Tables(0)(r)(7).ToString      'MO No
                                    wc = ds.Tables(0)(r)(12).ToString         'WC Code
                                    wds = ECAA.GetFindWorkcenterDetail_DataSet(wc)
                                    k = wds.Tables(0).Rows.Count
                                    For k = 0 To k - 1
                                        wcname = wds.Tables(0)(k)(1)
                                    Next
                                ElseIf rt_str(i) = "asft338" Then
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(1)   'docDate
                                    MOno = ds.Tables(0)(r)(5)      'MO No
                                ElseIf rt_str(i) = "aqct300" Then
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(1)   'docDate    
                                ElseIf rt_str(i) = "asft340" Then
                                    docNo = ds.Tables(0)(r)(0)     'docNo      
                                    docDate = ds.Tables(0)(r)(1)   'docDate
                                End If
                                If rt_str(i) = "asft335" Then
                                    If wc_num <> 0 Then
                                        If checkInList(wc, wc_str) Then
                                            modulecodename = getModulename(rt_str(i))
                                            clstemp.InsertTempRecord(recordnum, rt_str(i), modulecodename, wc, wcname, 0, reptype, usrnamelogged, docNo, docDate, MOno)
                                            MOno = ""
                                            wc = ""
                                            recordnum = recordnum + 1
                                        Else
                                        End If
                                    End If
                                Else
                                    modulecodename = getModulename(rt_str(i))
                                    clstemp.InsertTempRecord(recordnum, rt_str(i), modulecodename, wc, wcname, 0, reptype, usrnamelogged, docNo, docDate, MOno)
                                    MOno = ""
                                    wc = ""
                                    recordnum = recordnum + 1
                                End If
                                If rt_str(i) = "asft300-asft301" Then
                                    rt_str(i) = "asft300"
                                End If
                                If rt_str(i) = "asft311-asft321-asft312-asft322-asft313-asft323" Then
                                    rt_str(i) = "asft311"
                                End If
                            Next
                        Else
                        End If
                    Next
                    Call ShowGridNotApproved_Detail()
                End If
            Else
            End If
        End If
    End Sub

    Private Sub ShowGridNotApproved_Sum()
        Dim usrnamelogged As String = Session("UserName")
        Dim ds As DataSet = clstemp.GetLogDataShow_Summary_Dataset(usrnamelogged)
        GridViewResult.DataSource = ds
        GridViewResult.DataBind()
        'GridviewUtility.GrigOnmouseHandleAuto(GridViewResult)
        'GridviewUtility.MergeCells(GridViewResult)
        GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")
    End Sub

    Private Sub ShowGridNotApproved_Detail()
        Dim usrnamelogged As String = Session("UserName")
        Dim ds As DataSet = clstemp.GetLogDataShow_Detail_Dataset(usrnamelogged)
        GridViewResult.DataSource = ds
        GridViewResult.DataBind()
        'GridviewUtility.GrigOnmouseHandleAuto(GridViewResult)
        'GridviewUtility.MergeCells(GridViewResult)
        GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    'Function section
    Private Function doGenerateReport(ByVal reptype As String)
        Dim ds As DataSet
        Dim userid As String = Session("UserName")
        If reptype = "0" Then
            Session("reportname") = "cr_notapprove_summary.rpt"
            ds = clstemp.GetLogDataShow_Summary_Dataset(userid)
            Session("datasource") = ds
        Else
            Session("reportname") = "cr_notapprove_detail.rpt"
            ds = clstemp.GetLogDataShow_Detail_Dataset(userid)
            Session("datasource") = ds
        End If
        Session("reportpath") = ""
        Return Nothing
    End Function

    Private Function checkInList(ByVal wc As String, ByVal wc_str() As String) As Boolean
        Dim result As Boolean = False
        For Each x As String In wc_str
            If x.Contains(wc) Then
                result = True
            Else
            End If
        Next
        Return result
    End Function

    Private Function createStrWC(ByVal wcstring As String) As String
        Dim statement As String = ""
        If wcstring <> "on" Then
            statement = "'" + wcstring + "'"
        Else
        End If
        Return statement
    End Function

    Private Function getModulename(ByVal modulecode As String) As String
        Dim str As String = ""
        If modulecode = "asft300-asft301" Then
            str = "MO Rounting Section"
        ElseIf modulecode = "asft311-asft321-asft312-asft322-asft313-asft323" Then
            str = "Materials Issued & Return Section"
        ElseIf modulecode = "asft335" Then
            str = "Transfer Order : MO Single Record"
        ElseIf modulecode = "asft338" Then
            str = "Re-work and transfer-out for MO process"
        ElseIf modulecode = "aqct300" Then
            str = "QC Inspection Record"
        ElseIf modulecode = "asft340" Then
            str = "MO completion and stock-in"
        Else
            str = ""
        End If
        Return str
    End Function

    Function getNotapproveRec(ByVal modulename As String, Optional ByVal statcode As String = "N", Optional ByVal extention As String = "") As Integer
        Dim num As Integer = 0
        Dim ds As DataSet
        If modulename = "asft300" Then
            ds = SFAA.GetMO_HeaderDeatilStatusDataSet(statcode)
            num = ds.Tables(0).Rows.Count
        ElseIf modulename = "asft311" Then
            ds = SFDA.GetMatIssueHeadStatus_DataSet(statcode)
            num = ds.Tables(0).Rows.Count
        ElseIf modulename = "asft335" Then
            'Please note : In TO Module, The system open only 2 option - Approved - Not Approved 
            'just change from Y to N 
            If statcode = "Y" Then
                statcode = "N"
            Else
            End If
            If extention = "" Then
                ds = SFFB.GetTransferOrderW_Status_DataSet(statcode)
            Else
                ds = SFFB.GetTransferOrderW_StatusWC_DataSet(statcode, extention)
            End If
            num = ds.Tables(0).Rows.Count
        ElseIf modulename = "asft338" Then
            'Please note : TRW Module use the same as "asft335"
            If statcode = "Y" Then
                statcode = "N"
            Else
            End If
            ds = SFIA.GetTransferRework_Status_Dataset(statcode)
            num = ds.Tables(0).Rows.Count
        ElseIf modulename = "aqct300" Then   ' --------------------- UNCERTAIN / UNSTABLE MODULE, MIGHT BE UPDATE LATER
            ds = QCBA.getQCHeader_Status_Dataset(statcode)
            num = ds.Tables(0).Rows.Count
        ElseIf modulename = "asft340" Then
            ds = SFEA.GetMOreceitpStatus_DataSet(statcode)
            num = ds.Tables(0).Rows.Count
        Else
        End If
        Return num
    End Function

    Function getNotapproveData(ByVal modulename As String, Optional ByVal statcode As String = "N") As DataSet
        Dim ds As DataSet
        If modulename = "asft300" Then
            ds = SFAA.GetMO_HeaderDeatilStatusDataSet(statcode)
            Return ds
        ElseIf modulename = "asft311" Then
            ds = SFDA.GetMatIssueHeadStatus_DataSet(statcode)
            Return ds
        ElseIf modulename = "asft335" Then
            'Please note : In TO Module, The system open only 2 option - Approved - Not Approved 
            'just change from Y to N 
            If statcode = "Y" Then
                statcode = "N"
            Else
            End If
            ds = SFFB.GetTransferOrderW_Status_DataSet(statcode)
            Return ds
        ElseIf modulename = "asft338" Then
            'Please note : TRW Module use the same as "asft335"
            If statcode = "Y" Then
                statcode = "N"
            Else
            End If
            ds = SFIA.GetTransferRework_Status_Dataset(statcode)
            Return ds
        ElseIf modulename = "aqct300" Then      ' --------------------- UNCERTAIN / UNSTABLE MODULE, MIGHT BE UPDATE LATER
            ds = QCBA.getQCHeader_Status_Dataset(statcode)
            Return ds
        ElseIf modulename = "asft340" Then
            ds = SFEA.GetMOreceitpStatus_DataSet(statcode)
            Return ds
        Else
            Return Nothing
        End If
    End Function

    Public Function showparameType(Optional ByVal rowsplit As Integer = 4, Optional mtpt As String = "") As String
        Dim drawstr As String = ""
        Dim x_code As String = ""
        Dim x_desc As String = ""
        Dim cutstr() As String
        Dim linecut As Integer = 1
        Dim ischecked As String = ""
        Dim mtptarr() As String = mtpt.Split(",")

        For Each x As String In paramTypeT100

            cutstr = Split(x, "-")
            x_code = cutstr(0)
            x_desc = cutstr(1)

            If mtptarr.Length <> 0 Then
                If mtptarr.Contains(x_code) = True Then
                    ischecked = " checked"
                Else
                End If
            End If

            drawstr = drawstr + "<input type=checkbox name=schtype value=" & x_code & "" & ischecked & ">" & x_code & "-" & x_desc & " "
            If linecut Mod rowsplit = 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
            ischecked = ""

        Next
        Return drawstr
    End Function

    Public Function showparameWC(Optional ByVal rowsplit As Integer = 4, Optional mtwc As String = "") As String
        Dim drawstr As String = ""
        Dim linecut As Integer = 1
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim wccode As String = ""
        Dim wcname As String = ""
        Dim ischecked As String = ""
        Dim mtwcarr() As String = mtwc.Split(",")

        ds = ECAA.GetWorkcenter_DataSet()
        rowcount = ds.Tables(0).Rows.Count
        For i = 0 To rowcount - 1
            'wccode = ds.Tables("DATASET")(i)("WC")
            'wcname = ds.Tables("DATASET")(i)("WCNAME")

            wccode = ds.Tables(0)(i)(0)
            wcname = ds.Tables(0)(i)(1)

            If mtwc.Length <> 0 Then
                If mtwcarr.Contains(wccode) = True Then
                    ischecked = " checked"
                Else
                End If
            End If

            drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & "" & ischecked & "> " & wcname & " "
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
            ischecked = ""
        Next
        Return drawstr
    End Function

    Public Function showselReporttype(Optional value As String = "") As String

        Dim drawstring As String = ""
        Dim v1 As String = ""
        Dim v2 As String = ""

        If value = 0 Then
            v1 = " selected"
        End If
        If value = 1 Then
            v2 = " selected"
        End If

        drawstring = "<option value=0 " & v1 & ">Summary</option><option value=1" & v2 & ">Detail</option>"

        Return drawstring

    End Function

    Public Function showselRecstatus(Optional value As String = "") As String

        Dim drawstring As String = ""

        Dim v1 As String = ""
        Dim v2 As String = ""

        If value = "N" Then
            v1 = " selected"
        End If
        If value = "Y" Then
            v2 = " selected"
        End If

        drawstring = "<option value=N " & v1 & ">Not Approve</option><option value=Y" & v2 & ">Approved(Not Posting, Or Not Released)</option>"
        Return drawstring

    End Function

End Class