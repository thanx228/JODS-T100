Public Class TagCardPrint
    Inherits System.Web.UI.Page

    Dim SFEB As New SFEB
    Dim SFEA As New SFEA
    Dim ECAA As New ECAA
    Dim SFCA As New SFCA
    Dim SFAA As New SFAA
    Dim IMAAL As New IMAAL
    Dim SFFB As New SFFB

    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function getWC_sel() As String

        Dim str As String = "<select><option>- All Workcenter -</option>"
        Dim ds As DataSet
        Dim wc As String = ""
        Dim wcname As String = ""
        Dim sql As String = "SELECT " & ECAA.WorkcenterID & "," & ECAA.Workcenter & " FROM " & ECAA.tblWorkcenter & " WHERE " & ECAA.ent & "='3'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            wc = ds.Tables("DATASET")(i)("ecaa001")
            wcname = ds.Tables("DATASET")(i)("ecaa002")
            str = str + "<option value=" & wc & ">" & wc & " - " & wcname & "</option>"
        Next
        str = str + "</select>"
        Return str

    End Function

    Public Function getMOSummary(ByVal specidate As String) As String

        Dim str As String = "<table border=1>"
        Dim ds As DataSet
        Dim MODocNo As String, Seq As String, WC As String, WCName As String, MasterItem As String, Description As String, speci As String
        Dim MOQty As String, FinishedQty As String, ScrapQty As String, MOStatus As String

        Dim sql As String = "SELECT " & SFCB.WONo & " AS MODocNo," & SFCB.LineNo & " AS Seq," & SFCB.WorkStation & " AS WorkcenterID," &
                            "" & ECAA.Workcenter & " AS Workcenter," & SFAA.ProductItem & " AS ItemNo," &
                            "" & IMAAL.ProductName & " AS ItemDesc," & IMAAL.Specifaction & " AS Spec, " & SFCA.ProductionQty & " AS MOQty," &
                            "" & SFCA.CompletedQty & " AS CompleteQty," & SFAA.ScarpQty & " AS ScrapQty," &
                            "" & SFAA.Status & " AS MOStatus " &
                            "FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " " &
                            "ON " & SFCB.WONo & "=" & SFCA.DocNo & " " &
                            "LEFT JOIN " & SFAA.tblMO & " ON " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFAA.ProductItem & "=" & IMAAL.ProductItem & " " &
                            "LEFT JOIN " & ECAA.tblWorkcenter & " ON " & SFCB.WorkStation & "=" & ECAA.WorkcenterID & " " &
                            "WHERE " & SFCB.ent & "='3' AND " & SFCA.ent & "='3' AND " & SFAA.ProgromCode & "='3' AND " & IMAAL.ent & "='3' AND " & ECAA.ent & "='3' " &
                            "AND " & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & specidate & "','dd/MM/yyyy') AND TO_DATE('" & specidate & "','dd/MM/yyyy') " &
                            "ORDER BY " & SFCB.WONo & "," & SFCB.LineNo & ""

        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        str = str + "<tr align=center bgcolor=#FFFFFF height=30><th>&nbsp;Option&nbsp;</th><th>&nbsp;MO DocNumber&nbsp;</th><th>&nbsp;Seq&nbsp;</th><th>&nbsp;Workcenter&nbsp;</th><th>&nbsp;Workcenter Name&nbsp;</th><th>&nbsp;Production Item&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;MO Qty&nbsp;</th><th>&nbsp;CompleteQty&nbsp;</th><th>&nbsp;ScrapQty&nbsp;</th><th>&nbsp;MO Status&nbsp;</th></tr>"
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                MODocNo = ds.Tables("DATASET")(i)("MODocNo")
                Seq = ds.Tables("DATASET")(i)("Seq")
                WC = ds.Tables("DATASET")(i)("WorkcenterID")
                WCName = ds.Tables("DATASET")(i)("Workcenter")
                MasterItem = ds.Tables("DATASET")(i)("ItemNo")
                Description = ds.Tables("DATASET")(i)("ItemDesc")
                speci = ds.Tables("DATASET")(i)("Spec")
                'MO Detail
                MOQty = ds.Tables("DATASET")(i)("MOQty")
                FinishedQty = ds.Tables("DATASET")(i)("CompleteQty")
                ScrapQty = ds.Tables("DATASET")(i)("ScrapQty")
                'End
                MOStatus = ds.Tables("DATASET")(i)("MOStatus")
                str = str + "<tr bgcolor=#FFFFFF height=25><td align=center>&nbsp;Print&nbsp;</td><td>&nbsp;" & MODocNo & "&nbsp;</td><td align=right>&nbsp;" & Seq & "&nbsp;</td><td>&nbsp;" & WC & "&nbsp;</td><td>&nbsp;" & WCName & "&nbsp;</td><td>&nbsp;" & MasterItem & "&nbsp;</td><td>&nbsp;" & Description & "&nbsp;</td><td>&nbsp;" & speci & "&nbsp;</td><td align=right>&nbsp;" & MOQty & "&nbsp;</td><td align=right>&nbsp;" & FinishedQty & "&nbsp;</td><td align=right>&nbsp;" & ScrapQty & "&nbsp;</td><td align=left>&nbsp;" & MOStatusDetail(MOStatus) & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        End If

        Return str

    End Function

    Public Function getTOSummary(ByVal specidate As String) As String

        Dim str As String = "<table border=1>"
        Dim ds As DataSet
        Dim TODocNo As String, MO As String, MasterItem As String, Description As String, speci As String
        Dim TransferQty As String, FWC As String, TWC As String, runcard As String
        Dim MOQty As String, FinishedQty As String, ScrapQty As String
        Dim NextOper As String, NextOperSeq As String

        Dim sql As String = "SELECT " & SFFB.DocNo & " AS TODocNo," & SFFB.RunCard & " AS Runcard," & SFFB.WONo & " AS MONumber," & SFFB.WorkReportItemNo & " AS ItemNo, " &
                            "" & IMAAL.ProductName & " AS ItemDesc," & IMAAL.Specifaction & " AS Spec," & SFCA.ProductionQty & " AS MOQty," & SFCA.CompletedQty & " AS CompleteQty, " &
                            "" & SFAA.ScarpQty & " AS ScrapQty," & SFFB.NoOfGoodItem & " AS TransferQty," & SFFB.Workstation & " AS FromWorkcenter, " &
                            "" & SFCB.NextOperation & " AS NextOperation, " & SFCB.NextStationOpSeq & " AS NextOperSeq " &
                            "FROM " & SFFB.tblTransferHead & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " " &
                            "ON " & SFFB.WorkReportItemNo & "=" & IMAAL.ProductItem & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " " &
                            "ON " & SFFB.WONo & "=" & SFCA.DocNo & " And " & SFFB.RunCard & "=" & SFCA.RunCardNo & " " &
                            "LEFT JOIN " & SFAA.tblMO & " " &
                            "ON " & SFFB.WONo & " = " & SFAA.DocNo & " " &
                            "LEFT JOIN " & SFCB.tblMOprocessItem_SFCB & " " &
                            "ON " & SFFB.WONo & "=" & SFCB.WONo & " And " & SFFB.RunCard & "=" & SFCB.RunCard & " And " & SFFB.OperationNo & "=" & SFCB.NextOperation & " And " & SFFB.OperationSequence & "=" & SFCB.NextStationOpSeq & " " &
                            "WHERE " & SFFB.ent & "='3' AND " & IMAAL.ent & "='3' AND " & SFCA.ent & "='3' AND " & SFAA.ProgromCode & "='3' AND " & SFCB.ent & "='3' " &
                            "AND " & SFFB.DocumentDate & " BETWEEN TO_DATE ('" & specidate & "','dd/MM/yyyy') AND TO_DATE ('" & specidate & "','dd/MM/yyyy') ORDER BY " & SFFB.WONo & ", " & SFFB.RunCard & ""

        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        str = str + "<tr align=center bgcolor=#FFFFFF height=30><th>&nbsp;Option&nbsp;</th><th>&nbsp;TO DocNumber&nbsp;</th><th>&nbsp;Run Card&nbsp;</th><th>&nbsp;MO Number&nbsp;</th><th>&nbsp;Production Item&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;MO Qty&nbsp;</th><th>&nbsp;CompleteQty&nbsp;</th><th>&nbsp;ScrapQty&nbsp;</th><th>&nbsp;TransferQty&nbsp;</th><th>&nbsp;From WorkCenter&nbsp;</th><th>&nbsp;To WorkCenter&nbsp;</th></tr>"
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                TODocNo = ds.Tables("DATASET")(i)("TODocNo")
                MO = ds.Tables("DATASET")(i)("MONumber")
                MasterItem = ds.Tables("DATASET")(i)("ItemNo")
                Description = ds.Tables("DATASET")(i)("ItemDesc")
                speci = ds.Tables("DATASET")(i)("Spec")
                'MO Detail
                MOQty = ds.Tables("DATASET")(i)("MOQty")
                FinishedQty = ds.Tables("DATASET")(i)("CompleteQty")
                ScrapQty = ds.Tables("DATASET")(i)("ScrapQty")
                'End
                TransferQty = ds.Tables("DATASET")(i)("TransferQty")
                FWC = ds.Tables("DATASET")(i)("FromWorkcenter")
                runcard = ds.Tables("DATASET")(i)("Runcard")
                'For Tracking down previous operation
                NextOper = ds.Tables("DATASET")(i)("NextOperation")
                NextOperSeq = ds.Tables("DATASET")(i)("NextOperSeq")
                TWC = getTWC_TO(MO, runcard, NextOper, NextOperSeq)
                str = str + "<tr bgcolor=#FFFFFF height=25><td align=center>&nbsp;Print&nbsp;</td><td>&nbsp;" & TODocNo & "&nbsp;</td><td align=right>&nbsp;" & runcard & "&nbsp;</td><td>&nbsp;" & MO & "&nbsp;</td><td>&nbsp;" & MasterItem & "&nbsp;</td><td>&nbsp;" & Description & "&nbsp;</td><td>&nbsp;" & speci & "&nbsp;</td><td align=right>&nbsp;" & MOQty & "&nbsp;</td><td align=right>&nbsp;" & FinishedQty & "&nbsp;</td><td align=right>&nbsp;" & ScrapQty & "&nbsp;</td><td align=right>&nbsp;" & TransferQty & "&nbsp;</td><td align=center>&nbsp;" & FWC & "&nbsp;</td><td align=center>&nbsp;" & TWC & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        End If

        Return str

    End Function

    Public Function getMORSummary(ByVal specidate As String) As String

        Dim str As String = "<table border=1>"
        Dim ds As DataSet
        Dim MORDocNo As String, MO As String, MasterItem As String, Description As String, speci As String, unit As String
        Dim FinQty As String, FWC As String = "", TWC As String, runcard As String
        Dim MOQty As String, FinishedQty As String, ScrapQty As String

        Dim sql As String = "SELECT " & SFEB.DocNo & " AS MOReceiptDocNO, " & SFEB.WONo & " AS MONumber," & SFEB.Runcard & " AS Runcard," &
                           "" & SFEB.ItemNo & " AS ItemNo," & IMAAL.ProductName & " AS ItemDesc," & IMAAL.Specifaction & " AS Spec," & SFCA.ProductionQty & " AS MOQty," & SFCA.CompletedQty & " AS CompleteQty," & SFAA.ScarpQty & " AS ScrapQty," & SFEB.Unit & " AS Unit," &
                           "" & SFEB.AppliedQty & " As FinishQty,(SELECT " & SFCB.WorkStation & " FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.NextOperation & "='END' AND " & SFEB.Runcard & "=" & SFCB.RunCard & " " &
                           "AND " & SFCB.WONo & "=" & SFEB.WONo & " AND ROWNUM=1) AS ToWorkCenter " &
                           "FROM " & SFEB.tblMOreceiptB_StockInRequisition & " " &
                           "LEFT JOIN " & SFEA.tblMOreceiptHead & " " &
                           "ON " & SFEB.DocNo & "=" & SFEA.DocNo & " " &
                           "LEFT Join " & IMAAL.tblProductionDetail & " " &
                           "ON " & SFEB.ItemNo & "=" & IMAAL.ProductItem & " " &
                           "LEFT JOIN " & SFCA.tblMO_Detail & " " &
                           "ON " & SFEB.WONo & "=" & SFCA.DocNo & " AND " & SFEB.Runcard & "=" & SFCA.RunCardNo & " " &
                           "LEFT JOIN " & SFAA.tblMO & " " &
                           "ON " & SFEB.WONo & " = " & SFAA.DocNo & " " &
                           "WHERE " & SFEB.ent & "='3' AND " & SFEA.ent & "='3' AND " & IMAAL.ent & "='3' " &
                           "AND " & SFEA.PostedDate & " BETWEEN TO_DATE ('" & specidate & "','dd/MM/yyyy') AND TO_DATE ('" & specidate & "','dd/MM/yyyy')"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        str = str + "<tr align=center bgcolor=#FFFFFF height=30><th>&nbsp;Option&nbsp;</th><th>&nbsp;MOR DocNumber&nbsp;</th><th>&nbsp;MO Number&nbsp;</th><th>&nbsp;Production Item&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Specification&nbsp;</th><th>&nbsp;MO Qty&nbsp;</th><th>&nbsp;CompleteQty&nbsp;</th><th>&nbsp;ScrapQty&nbsp;</th><th>&nbsp;Unit&nbsp;</th><th>&nbsp;Qty&nbsp;</th><th>&nbsp;From WorkCenter&nbsp;</th><th>&nbsp;To WorkCenter&nbsp;</th></tr>"
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                MORDocNo = ds.Tables("DATASET")(i)("MOReceiptDocNO")
                MO = ds.Tables("DATASET")(i)("MONumber")
                MasterItem = ds.Tables("DATASET")(i)("ItemNo")
                Description = ds.Tables("DATASET")(i)("ItemDesc")
                speci = ds.Tables("DATASET")(i)("Spec")
                'MO Detail
                MOQty = ds.Tables("DATASET")(i)("MOQty")
                FinishedQty = ds.Tables("DATASET")(i)("CompleteQty")
                ScrapQty = ds.Tables("DATASET")(i)("ScrapQty")
                'End
                unit = ds.Tables("DATASET")(i)("Unit")
                FinQty = ds.Tables("DATASET")(i)("FinishQty")
                TWC = ds.Tables("DATASET")(i)("ToWorkCenter")
                runcard = ds.Tables("DATASET")(i)("Runcard")
                FWC = getFWC_MOR(MO, runcard)
                str = str + "<tr bgcolor=#FFFFFF height=25><td align=center>&nbsp;Print&nbsp;</td><td>&nbsp;" & MORDocNo & "&nbsp;</td><td>&nbsp;" & MO & "&nbsp;</td><td>&nbsp;" & MasterItem & "&nbsp;</td><td>&nbsp;" & Description & "&nbsp;</td><td>&nbsp;" & speci & "&nbsp;</td><td align=right>&nbsp;" & MOQty & "&nbsp;</td><td align=right>&nbsp;" & FinishedQty & "&nbsp;</td><td>&nbsp;" & ScrapQty & "&nbsp;</td><td>&nbsp;" & unit & "&nbsp;</td><td align=right>&nbsp;" & FinQty & "&nbsp;</td><td align=center>&nbsp;" & FWC & "&nbsp;</td><td align=center>&nbsp;" & TWC & "&nbsp;</td></tr>"
            Next
            str = str + "</table>"
        End If

        Return str

    End Function

    Private Function getTWC_TO(ByVal MO As String, ByVal runcard As String, ByVal nextoper As String, ByVal nextoperseq As String) As String

        Dim TrfLineSeq As String = ""
        Dim NextWC As String = ""
        Dim ds2 As DataSet
        Dim ds As DataSet

        Dim sql As String = "SELECT " & SFCB.LineNo & " AS TrfLineSeq FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.ent & "='3' AND " & SFCB.WONo & "='" & MO & "' AND " & SFCB.OperationID & "='" & nextoper & "' AND " & SFCB.OperationSeq & "='" & nextoperseq & "' AND " & SFCB.RunCard & "='" & runcard & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)

        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            TrfLineSeq = ds.Tables("DATASET")(0)("TrfLineSeq").ToString
        Next

        Dim sql2 As String = "SELECT " & SFCB.WorkStation & " AS NextWC FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.ent & "='3' AND " & SFCB.WONo & "='" & MO & "' AND " & SFCB.LineNo & "> " & TrfLineSeq & " AND " & SFCB.RunCard & "='" & runcard & "' ORDER BY " & SFCB.LineNo & " ASC"
        ds2 = clsconnect.QueryDataSet(sql2, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)

        If ds2.Tables("DATASET").Rows.Count = 0 Then
            NextWC = "END"
        Else
            For i = 0 To ds2.Tables("DATASET").Rows.Count - 1
                If i = 0 Then
                    NextWC = ds2.Tables("DATASET")(0)("NextWC").ToString
                Else
                End If
            Next
        End If

        Return NextWC

    End Function

    Private Function getFWC_MOR(ByVal MO As String, ByVal runcard As String) As String

        Dim FWC As String = ""
        Dim ds As DataSet
        Dim sql As String = "Select " & SFCB.WorkStation & " As FWC FROM " & SFCB.tblMOprocessItem_SFCB & " WHERE " & SFCB.WONo & "='" & MO & "' AND " & SFCB.RunCard & "='0' AND " & SFCB.NextOperation & "!='END' ORDER BY " & SFCB.LineNo & " DESC "
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count = 0 Then
            FWC = "INIT"
        Else
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                If i = 0 Then
                    FWC = ds.Tables("DATASET")(0)("FWC").ToString
                Else
                End If
            Next
        End If

        Return FWC

    End Function

    Private Function MOStatusDetail(ByVal code As String) As String

        If code = "A" Then
            code = code + ":Approved"
        ElseIf code = "C" Then
            code = code + ":Closed"
        ElseIf code = "D" Then
            code = code + ":Withdraw"
        ElseIf code = "F" Then
            code = code + ":Released"
        ElseIf code = "M" Then
            code = code + ":Cost Close"
        ElseIf code = "N" Then
            code = code + ":Unapproved"
        ElseIf code = "R" Then
            code = code + ":Rejected"
        ElseIf code = "W" Then
            code = code + ":Approving"
        ElseIf code = "X" Then
            code = code + ":Voided"
        ElseIf code = "Y" Then
            code = code + ":Approved"
        ElseIf code = "E" Then
            code = code + ":Termination"
        Else
            code = "N/A"
        End If

        Return code

    End Function

End Class