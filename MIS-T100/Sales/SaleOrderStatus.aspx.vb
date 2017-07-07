Public Class SaleOrderStatus
    Inherits System.Web.UI.Page

    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function getSOStatusDisp(ByVal fdate As String, ByVal tdate As String, Optional ByVal pSONum As String = "", Optional ByVal pCustID As String = "", Optional ByVal pItem As String = "", Optional ByVal pSpec As String = "") As String


        Dim drawstr As String = ""
        Dim ds As DataSet
        Dim ext As String = ""

        Dim rDate As Date
        Dim SO As String = ""
        Dim Item As String = ""
        Dim Spec As String = ""
        Dim UnitPrice As String = ""
        Dim Qty As String = ""
        Dim DeliveryQty As String = ""
        Dim BalanceQty As String = ""
        Dim StockQty As String = ""
        Dim DeliveryDate As Date
        Dim CustID As String = ""
        Dim CustName As String = ""
        Dim Industry As String = ""
        Dim App As String = ""
        Dim MO As String = ""
        Dim PR As String = ""
        Dim PO As String = ""

        If pSONum <> "" Then
            ext = ext + " AND xmdadocno LIKE '%" & pSONum & "%'"
        End If
        If pCustID <> "" Then
            ext = ext + " AND xmda004 ='" & pCustID & "'"
        End If
        If pItem <> "" Then
            ext = ext + " AND xmdc001 LIKE '%" & pItem & "%'"
        End If
        If pSpec <> "" Then
            ext = ext + " AND imaal004 LIKE '%" & pSpec & "%'"
        End If

        Dim sql As String = "SELECT xmdadocdt AS SaleDate,xmdadocno AS SO,xmdc001 AS itemID,imaal003 AS Descriptionm,imaal004 AS Spec,xmdc007 AS OrderQty, xmdc015 AS UnitPrice,
                             (SELECT xmdd014 AS DeliverQty FROM xmdd_t WHERE xmdadocno=xmdddocno AND xmdcseq=xmddseq) AS DeliveryQty,
                             (xmdc007-(SELECT xmdd014 AS DeliverQty FROM xmdd_t WHERE xmdadocno=xmdddocno AND xmdcseq=xmddseq)) AS BalanceQty,
                             (SELECT sum(inag009) as TotalQty FROM inag_t WHERE xmdc001=inag001 AND inag004 IN ('2101','2102','2103')) AS StockQty,
                             (SELECT xmdd011 AS DeliverDate FROM xmdd_t WHERE xmdadocno=xmdddocno AND xmdcseq=xmddseq) AS DeliverDate,
                             xmda004 AS CustomerID,pmaal004 AS CustomerName,
                             (CASE xmdc019 WHEN '1' THEN '1:General' WHEN '9' THEN '9:Sample/Jig Fixture' ELSE 'xmdc019' END) AS Itemtype,
                             (CASE xmdc045 WHEN '1' THEN '1:General' WHEN '2' THEN '2:Normal Settlement' WHEN '4' THEN '4:Brief' ELSE xmdc045 END) AS Rowstatus,
                             (CASE xmdastus WHEN 'Y' THEN 'SO Confirmed' WHEN 'C' THEN 'Closed' WHEN 'N' THEN 'Unconfirmed' ELSE xmdastus END) AS SOStatus,
                             (SELECT sfaadocno AS MODocNo FROM sfaa_t WHERE xmdadocno=sfaa006 AND sfaa005='2') AS MO,
                             (SELECT pmdbdocno AS PRDocNO FROM pmdb_t WHERE xmdadocno=pmdb001) AS PRDocNO,
                             (SELECT pmdldocno AS PODocNO FROM pmdl_t WHERE xmdadocno=pmdl008 AND pmdl007='3') AS PODocNO
                             FROM xmdc_t
                             LEFT JOIN xmda_t ON xmdcdocno=xmdadocno
                             LEFT JOIN imaal_t ON xmdc001=imaal001 AND imaal002='en_US'
                             LEFT JOIN pmaal_t ON xmda004=pmaal001 AND pmaal002='en_US'
                             WHERE xmdcent='3' AND xmdaent='3' AND imaalent='3' AND pmaalent='3' AND xmdadocdt BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy') " & ext & " ORDER BY xmdadocdt ASC,xmdcseq ASC"

        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        drawstr = " <br /><table bgcolor=FFFFFF border=1>"
        drawstr = drawstr + "<tr height=25 align=center><th>&nbsp;Date&nbsp;</th><th>&nbsp;SO&nbsp;</th><th>&nbsp;Item&nbsp;</th>" &
                       "<th>&nbsp;Spec&nbsp;</th><th>&nbsp;UnitPrice&nbsp;</th><th>&nbsp;Qty&nbsp;</th><th>&nbsp;DeliveryQty&nbsp;</th><th>&nbsp;BalanceQty&nbsp;</th><th>&nbsp;StockQty&nbsp;</th><th>&nbsp;DeliveryDate&nbsp;</th><th>&nbsp;CustID&nbsp;</th><th>&nbsp;CustName&nbsp;</th><th>&nbsp;SO Status&nbsp;</th><th>&nbsp;MO&nbsp;</th><th>&nbsp;PR&nbsp;</th><th>&nbsp;PO&nbsp;</th></tr>"
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            rDate = CDate(ds.Tables("DATASET")(i)("SaleDate").ToString)
            SO = ds.Tables("DATASET")(i)("SO").ToString
            Item = ds.Tables("DATASET")(i)("itemID").ToString
            Spec = ds.Tables("DATASET")(i)("Spec").ToString
            UnitPrice = checkzeronull(ds.Tables("DATASET")(i)("UnitPrice").ToString)
            Qty = checkzeronull(ds.Tables("DATASET")(i)("OrderQty").ToString)
            DeliveryQty = checkzeronull(ds.Tables("DATASET")(i)("DeliveryQty").ToString)
            BalanceQty = checkzeronull(ds.Tables("DATASET")(i)("BalanceQty").ToString)
            StockQty = checkzeronull(ds.Tables("DATASET")(i)("StockQty").ToString)
            DeliveryDate = CDate(ds.Tables("DATASET")(i)("DeliverDate").ToString)
            CustID = ds.Tables("DATASET")(i)("CustomerID").ToString
            CustName = ds.Tables("DATASET")(i)("CustomerName").ToString
            'Industry = ds.Tables("DATASET")(i)("")
            App = ds.Tables("DATASET")(i)("SOStatus").ToString
            MO = ds.Tables("DATASET")(i)("MO").ToString
            PR = ds.Tables("DATASET")(i)("PRDocNO").ToString
            PO = ds.Tables("DATASET")(i)("PODocNO").ToString
            drawstr = drawstr + "<tr height=25><td>&nbsp;" & rDate.ToString("dd/MM/yyyy") & "&nbsp;</td><td>&nbsp;" & SO & "&nbsp;</td><td>&nbsp;" & Item & "&nbsp;</td>" &
                   "<td align=left>&nbsp;" & Spec & "&nbsp;</td><td align=left>&nbsp;" & UnitPrice.ToString & "&nbsp;</td><td align=right>&nbsp;" & Qty & "&nbsp;</td><td align=right>&nbsp;" & DeliveryQty & "&nbsp;</td>" &
                   "<td align=right>&nbsp;" & BalanceQty & "&nbsp;</td><td align=right>&nbsp;" & StockQty.ToString & "&nbsp;</td>" &
                   "<td align=right>&nbsp;" & DeliveryDate.ToString("dd/MM/yyyy") & "&nbsp;</td><td align=right>&nbsp;" & CustID & "&nbsp;</td>" &
                   "<td align=left>&nbsp;" & CustName & "&nbsp;</td>" &
                   "<td align=center>&nbsp;" & App & "&nbsp;</td>   <td align=center>&nbsp;" & MO & "&nbsp;</td>" &
                   "<td align=center>&nbsp;" & PR & "&nbsp;</td>   <td align=center>&nbsp;" & PO & "&nbsp;</td></tr>"
        Next
        drawstr = drawstr + "</table>"
        Return drawstr

    End Function

    Public Function showparameSOType(Optional ByVal rowsplit As Integer = 4)

        Dim ds As DataSet
        Dim drawstr As String = "<table><tr>"
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim sql As String = "Select " & OOBXL.DocTypeId & " As SaleOrderType, " & OOBXL.DocType & " As Description " &
                            "FROM " & OOBXL.tblDocType & " WHERE " & OOBXL.ent & "='3' AND " & OOBXL.DocTypeId & " IN" &
                            "('2201','2202','2203','2204','2205','2206','2207','2208','2209','2210','2211','2212','2213','2214','2215','2216','2217','2218','2501','2502','2503','2504','2505')" &
                            " And " & OOBXL.Language & " ='en_US'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            scode = ds.Tables("DATASET")(i)("SaleOrderType")
            scodename = ds.Tables("DATASET")(i)("Description")
            drawstr = drawstr + "<td height=25 width=150><input type=checkbox name=stype value=JP" & scode & "> " & scode & "-" & scodename & "</td>"
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "</tr>"
            Else
            End If
            linecut = linecut + 1
        Next
        drawstr = drawstr + "</table>"
        Return drawstr

    End Function

    Public Function showparamNotApproveOption(Optional ByVal rowsplit As Integer = 3)

        Dim drawstr As String = "<table border=0><tr><td><table border=0><tr>"
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim appstus() As String = {"Y", "N"}
        Dim appstusdesc() As String = {"Approved", "Not Approve"}

        For i = 0 To appstus.Length - 1
            drawstr = drawstr + "<td height=25 width=150><input type=checkbox name=apprstus value=" & appstus(i) & ">" & appstusdesc(i) & "</td>"
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "</tr>"
            Else
            End If
            linecut = linecut + 1
        Next
        drawstr = drawstr + "</table></td>"
        Return drawstr

    End Function

    Public Function showparamSOOption(Optional ByVal rowsplit As Integer = 3)

        Dim drawstr As String = "<td><table border=0><tr><td rowspan=2>&nbsp;&nbsp;&nbsp;&nbsp;SO Status&nbsp;&nbsp;&nbsp;&nbsp;</td>"
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim SOstus() As String = {"Y", "M", "N"}
        Dim SOstusdesc() As String = {"Auto Close", "Manual Close", "Not Close"}

        For i = 0 To SOstus.Length - 1
            drawstr = drawstr + "<td height=25 width=150><input type=checkbox name=apprstus value=" & SOstus(i) & ">" & SOstusdesc(i) & "</td>"
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "</tr>"
            Else
            End If
            linecut = linecut + 1
        Next
        drawstr = drawstr + "<table></td></tr></table>"
        Return drawstr

    End Function

    Public Function checkzeronull(ByVal value As String) As String

        Dim str As String = ""
        If value = "0" Then
            str = ""
        Else
            str = value
        End If
        Return str

    End Function

End Class