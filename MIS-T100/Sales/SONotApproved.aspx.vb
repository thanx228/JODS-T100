Public Class SONotApproved
    Inherits System.Web.UI.Page

    Dim OOBXL As New OOBXL         'Sales Type
    Dim XMDA As New XMDA           'Sales Order
    Dim BMAA As New BMAA           'BOM MasterItem
    Dim BMBA As New BMBA           'BOM ChildItem
    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function ShowNotApproveSO(ByVal condition As String) As String

        Dim ds As DataSet
        Dim drawstr As String = ""
        '
        Dim SO As String = ""
        Dim ItemID As String = ""
        Dim ItemDesc As String = ""
        Dim ItemSpec As String = ""
        Dim OrderQty As String = ""
        Dim IndusCode As String = ""
        Dim IndusCodeDesc As String = ""
        Dim CustID As String = ""
        Dim CustName As String = ""
        Dim PlanDelivDate As Date
        Dim SalesDueDate As Date

        Dim sql As String = "SELECT xmdcdocno AS SO,xmdcseq AS Seq,xmdc001 AS itemid,imaal003 AS ItemDescription,imaal004 AS Spec, xmdc007 AS OrderQty,xmdcua001 AS IndustryTypeCode,xmda004 AS CustomerID, pmaal004 AS CustomerName,
                             xmdc012 AS PlanDeliveryDate,xmdd011 AS SaleRequestDueDate
                             FROM xmdc_t
                             LEFT JOIN xmda_t
                             ON xmdcdocno=xmdadocno
                             LEFT JOIN imaal_t
                             ON xmdc001=imaal001 AND imaal002='en_US'
                             LEFT JOIN pmaal_t
                             ON xmda004=pmaal001 AND pmaal002='en_US'
                             LEFT JOIN xmdd_t
                             ON xmdcdocno=xmdddocno AND xmdcseq=xmddseq
                             WHERE
                             xmdcent='3' AND xmdaent=3 AND imaalent='3' AND pmaalent='3' AND xmddent='3' AND xmdastus='N' " & condition & " ORDER BY xmdcdocno ASC, xmdcseq ASC"
        'AND substr(xmdcdocno,1,6) IN('JP2201','JP2202','JP2203','JP2204','JP2205','JP2206','JP2207','JP2208','JP2209','JP2210','JP2211','JP2212','JP2213','JP2214','JP2215','JP2216','JP2217','JP2218','JP2501','JP2502','JP2503','JP2504','JP2505')
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        drawstr = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;SO&nbsp;</th><th>&nbsp;ItemID&nbsp;</th><th>&nbsp;ItemDescription&nbsp;</th><th>&nbsp;Spec&nbsp;</th><th>&nbsp;OrderQty&nbsp;</th><th>&nbsp;IndustryCode&nbsp;</th><th>&nbsp;IndustryDescription&nbsp;</th><th>&nbsp;CustomerID/CustomerName &nbsp;</th><th>&nbsp;PlanDelivery&nbsp;</th><th>&nbsp;SaleRequestDueDate&nbsp;</th><th>&nbsp;BOMCreate&nbsp;</th></tr>"
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            SO = ds.Tables("DATASET")(i)("SO").ToString
            ItemID = ds.Tables("DATASET")(i)("itemid").ToString
            ItemDesc = ds.Tables("DATASET")(i)("ItemDescription").ToString
            ItemSpec = ds.Tables("DATASET")(i)("Spec").ToString
            OrderQty = ds.Tables("DATASET")(i)("OrderQty").ToString
            IndusCode = ds.Tables("DATASET")(i)("IndustryTypeCode").ToString
            IndusCodeDesc = getIndusTypeDesc(IndusCode)
            CustID = ds.Tables("DATASET")(i)("CustomerID").ToString
            CustName = ds.Tables("DATASET")(i)("CustomerName").ToString
            PlanDelivDate = CDate(ds.Tables("DATASET")(i)("PlanDeliveryDate"))
            SalesDueDate = CDate(ds.Tables("DATASET")(i)("SaleRequestDueDate"))
            drawstr = drawstr + "<td align=center>&nbsp;" & SO & "&nbsp;</td>" + "<td align=left>&nbsp;" & ItemID & "&nbsp;</td>" + "<td align=left>&nbsp;" & ItemDesc & "&nbsp;</td>" + "<td align=left>&nbsp;" & ItemSpec & "&nbsp;</td>" + "<td align=right>" & OrderQty & "&nbsp;</td>" + "<td align=right>" & IndusCode & "&nbsp;</td>" + "<td align=left>&nbsp;" & IndusCodeDesc & "</td>" + "<td align=left>&nbsp;" & CustID & " - " & CustName & "</td>" + "<td align=right>" & PlanDelivDate.ToString("dd/MM/yyyy") & "&nbsp;</td>" + "<td align=right>" & SalesDueDate.ToString("dd/MM/yyyy") & "&nbsp;</td>" + "<td align=right>" & CheckBOMCreation(ItemID) & "&nbsp;</td></tr>"
            '        drawstr = "<table border=1><tr height=30 align=center bgcolor=#ffffff><th>&nbsp;Detail&nbsp;</th><th>&nbsp;SO&nbsp;</th><th>&nbsp;ItemID&nbsp;</th><th>&nbsp;ItemDescription&nbsp;</th><th>&nbsp;Spec&nbsp;</th><th>&nbsp;OrderQty&nbsp;</th><th>&nbsp;IndustryCode&nbsp;</th><th>&nbsp;IndustryDescription&nbsp;</th><th>&nbsp;CustomerID/CustomerName &nbsp;</th><th>&nbsp;PlanDelivery&nbsp;</th><th>&nbsp;SaleRequestDueDate&nbsp;</th></tr>"
            '"<tr bgcolor=#ffffff height=25><td align=center><a href = javascript: void(0) onclick=javascript:window.open('WorkLoadingPop.aspx?wc=" & ItemID & "')>BOM<a/></td>" +
        Next
        Return drawstr

    End Function

    Public Function showparameSOType(Optional ByVal rowsplit As Integer = 4)

        Dim ds As DataSet
        Dim drawstr As String = ""
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim sql As String = "SELECT " & OOBXL.DocTypeId & " AS SaleOrderType, " & OOBXL.DocType & " As Description " &
                            "FROM " & OOBXL.tblDocType & " WHERE " & OOBXL.ent & "='3' AND " & OOBXL.DocTypeId & " IN" &
                            "('2201','2202','2203','2204','2205','2206','2207','2208','2209','2210','2211','2212','2213','2214','2215','2216','2217','2218','2501','2502','2503','2504','2505')" &
                            " And " & OOBXL.Language & " ='en_US'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            scode = ds.Tables("DATASET")(i)("SaleOrderType")
            scodename = ds.Tables("DATASET")(i)("Description")
            drawstr = drawstr + "<input type=checkbox name=stype value=JP" & scode & "> " & scode & "-" & scodename & " "
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
        Next
        Return drawstr

    End Function

    Public Function createStrSType(ByVal ststring As String)

        Dim statement As String = ""
        If ststring <> "on" Then
            statement = "'" + ststring + "'"
        Else
        End If
        Return statement

    End Function

    Private Function getIndusTypeDesc(ByVal code As String) As String

        Dim strReturn As String = ""
        Dim ref As Integer = 0

        If code = "" Then
            ref = 0
        Else
            ref = CInt(code)
        End If

        If ref = 1 Then
            strReturn = "Electronic"
        ElseIf ref = 2 Then
            strReturn = "Aerospace"
        ElseIf ref = 3 Then
            strReturn = "Automotive"
        ElseIf ref = 4 Then
            strReturn = "Telecommunication"
        ElseIf ref = 5 Then
            strReturn = "Medical"
        ElseIf ref = 6 Then
            strReturn = "FoodIndustry"
        ElseIf ref = 7 Then
            strReturn = "Energy"
        ElseIf ref = 8 Then
            strReturn = "Transportation"
        Else
            strReturn = ""
        End If

        Return strReturn

    End Function

    Private Function CheckBOMCreation(ByVal MitemID As String) As String

        Dim result As String = "Yes"
        Dim ds As DataSet
        Dim sql As String = "SELECT * FROM bmba_t WHERE bmba001='" & MitemID & "' AND bmbasite='JINPAO' ORDER BY bmba009 ASC"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count = 0 Then
            result = "No"
        Else
        End If

        Return result

    End Function

End Class