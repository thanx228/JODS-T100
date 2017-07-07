Public Class noBomItemBlock
    Inherits System.Web.UI.Page

    Dim clsconnect As New clsDBConnect
    Dim BMBA As New BMBA            'BOM itemchild Line
    Dim xmdc As New XMDC            'Sales Line

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function ShowItemNoBOMorItemBlock(ByVal seltype As String, ByVal sotype As String) As String

        Dim disp As String = ""
        Dim ds As DataSet

        Dim itemid As String = ""
        Dim spec As String = ""
        Dim matItemid As String = ""
        Dim matItemspec As String = ""
        Dim sql As String = ""
        Dim ext As String = ""

        If sotype <> "" Then
            ext = "AND xmdcdocno LIKE '" & sotype & "%'"
        Else
            ext = ""
        End If

        If seltype = "1" Then  'NO Bom
            sql = "SELECT  xmdc001 AS MasterItemNo,imaal004 AS MasterItemSpec
                   FROM xmdc_t
                   LEFT JOIN imaal_t
                   ON xmdc001=imaal001 AND imaal002='en_US'
                   WHERE xmdcent='3' AND imaalent='3' AND
                   (SELECT count(*) FROM bmba_t WHERE bmbaent='3' AND bmba001=xmdc001 AND bmbasite='JINPAO')=0
                   " & ext & " GROUP BY xmdc001,imaal004"
        Else  'Item Block
            sql = "SELECT xmdc001 AS MasterItemNo,I.imaal004 AS MasterItemSpec, J.imaal001 AS MatItemNo, J.imaal004 AS MatItemSpec
                   FROM xmdc_t
                   LEFT JOIN bmba_t ON xmdc001=bmba001
                   LEFT JOIN imaal_t I ON xmdc001=I.imaal001 AND I.imaal002='en_US'
                   LEFT JOIN imaal_t J ON bmba003=J.imaal001 AND J.imaal002='en_US'
                   WHERE (SELECT imaastus FROM imaa_t WHERE xmdc001=imaa001 AND imaaent='3')='X'
                   AND J.imaal001 is not null " & ext & "
                   GROUP BY xmdcdocno,xmdc001,I.imaal004,J.imaal001,J.imaal004"
        End If
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            disp = disp + "<table border=1><tr align=center bgcolor=#FFB55D><th>&nbsp;Detail&nbsp;</th><th>&nbsp;ItemID&nbsp;</th><th>&nbsp;Spec&nbsp;</th>"
            If seltype = "2" Then
                disp = disp + "<th>&nbsp;MatItemID&nbsp;</th><th>&nbsp;MatSpec&nbsp;</th>"
            Else
                disp = disp + "</tr>"
            End If
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                If seltype = "2" Then
                    itemid = ds.Tables("DATASET")(i)("MasterItemNo").ToString()
                    spec = ds.Tables("DATASET")(i)("MasterItemSpec").ToString()
                    matItemid = ds.Tables("DATASET")(i)("MatItemNo").ToString()
                    matItemspec = ds.Tables("DATASET")(i)("MatItemSpec").ToString()
                    disp = disp + "<tr bgcolor=#FFFFFF><td align=center>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('noBomItemBlockPop.aspx?mitemid=" & itemid & "')>Detail</a>&nbsp;</td><td align=center>&nbsp;" & itemid & "&nbsp;</td><td align=right>&nbsp;" & matItemid & "&nbsp;</td><td align=center>&nbsp;" & matItemspec & "&nbsp;</td></tr>"
                Else
                    itemid = ds.Tables("DATASET")(i)("MasterItemNo").ToString()
                    spec = ds.Tables("DATASET")(i)("MasterItemSpec").ToString()
                    disp = disp + "<tr bgcolor=#FFFFFF>&nbsp;<td align=center>&nbsp;<a href = javascript: void(0) onclick=javascript:window.open('noBomItemBlockPop.aspx?mitemid=" & itemid & "')>Detail</a>&nbsp;</td><td align=right>&nbsp;" & itemid & "&nbsp;</td><td align=left>&nbsp;" & spec & "&nbsp;</td></tr>"
                End If
            Next
        End If
        Return disp

    End Function

    Public Function showparameSOType_sel() As String

        Dim ds As DataSet
        Dim drawstr As String = "<Select name=sotype><option value=>All</option>"
        Dim scode As String = ""
        Dim scodename As String = ""
        Dim linecut As Integer = 1

        Dim sql As String = "Select " & OOBXL.DocTypeId & " As SaleOrderType, " & OOBXL.DocType & " As Description " &
                            "FROM " & OOBXL.tblDocType & " WHERE " & OOBXL.ent & "='3' AND " & OOBXL.DocTypeId & " IN" &
                            "('2201','2202','2203','2204','2205','2206','2207','2208','2209','2210','2211','2212','2213','2214','2215','2216','2217','2218','2501','2502','2503','2504','2505')" &
                            " And " & OOBXL.Language & " ='en_US'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            scode = ds.Tables("DATASET")(i)("SaleOrderType")
            scodename = ds.Tables("DATASET")(i)("Description")
            drawstr = drawstr + "<option value=JP" & scode & "> " & scode & "-" & scodename & "</option>"
        Next
        drawstr = drawstr + "</select>"
        Return drawstr

    End Function

End Class