Public Class updatePQC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim strSQL As String = ""


        Dim SQL As String = "select  " & SFAA.DocNo & "," & SFCB.OperationID & "," & SFCB.OperationSeq & " from " & SFCB.tblMOprocessItem_SFCB &
        " left join " & SFCA.tblMO_Detail & " On " & SFCA.ent & "=" & SFCB.ent & " And " & SFCA.DocNo & "=" & SFCB.WONo & " And " & SFCA.RunCardNo & "=" & SFCB.RunCard &
        " left join " & SFAA.tblMO & " On " & SFAA.ent & "=" & SFCB.ent & " And " & SFAA.DocNo & "=" & SFCA.DocNo &
        " left join " & ECBB.tblItemRoutingBody & " On " & ECBB.ent & "=" & SFCB.ent & " And " & ECBB.ProcessPartNo & "=" & SFAA.ProductItem & " And " & ECBB.ProcessPartNoNumber & "=" & SFCB.OperationID & " And " & ECBB.OperationSeq & "=" & SFCB.OperationSeq & " and " & ECBB.ProcessPartNoNumber & "='01' " &
        " where " & SFCB.ent & " =3 And " & SFAA.Status & " ='F' and  " & SFCB.GoodTransferOut & "=0 and " & ECBB.PQC & "='Y' " &
        SFCB.OperationID & "<>'' and " & SFCB.OperationSeq & "<>'' and " & SFCB.Site & "='JINPAO'"
        ''" group by " & SFAA.ProductItem & "," & SFCB.OperationID & "," & SFCB.OperationSeq & ""
        'Dim dt3 As DataTable = GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe)
        'For j As Integer = 0 To dt3.Rows.Count - 1
        '    With dt3.Rows(j)

        '        strSQL &= "update " & SFCB.tblMOprocessItem_SFCB & " set " & SFCB.PQC & "='Y' where " & SFCB.WONo & " = '" & .Item(SFAA.DocNo) & "'  and " & SFCB.OperationID & "='" & .Item(SFAA.DocNo) & "' and " & SFCB.OperationSeq & "=" & .Item(SFAA.DocNo) & " and " & SFCB.RunCard & "=0 ;<br/>"

        '    End With

        'Next
        'lbShow.Text = strSQL
        'Exit Sub
        SQL = "select " & SFCB.WONo & "," & SFCB.OperationID & "," & SFCB.OperationSeq & " from " & SFCB.tblMOprocessItem_SFCB & " where sfcbdocno in (select sfaadocno from sfaa_t where sfaaent=3 and sfaastus='F') and sfcb017='N'  and " & SFCB.GoodTransferOut & "=0 "
        Dim moItemHash As New Hashtable
        Dim ItemRoutingHash As New Hashtable
        Dim dt As DataTable = GetData.GetDataReaderOracle(SQL, "", GetData.WhoCalledMe)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim item As String = ""
                Dim moNumber As String = .Item(SFCB.WONo)
                Dim opCode As String = .Item(SFCB.OperationID)
                Dim opSeq As String = .Item(SFCB.OperationSeq)
                Dim routingCode As String = "01"
                Dim SQL2 As String
                Dim dt2 As DataTable
                'get item from mo
                If moItemHash.ContainsKey(moNumber) Then
                    item = moItemHash(moNumber)
                Else
                    SQL2 = "select " & SFAA.ProductItem & " from " & SFAA.tblMO & " where " & SFAA.ent & "=3 and " & SFAA.DocNo & "='" & moNumber & "' "
                    dt2 = GetData.GetDataReaderOracle(SQL2, "", GetData.WhoCalledMe)
                    If dt2.Rows.Count > 0 Then
                        item = dt2.Rows(0).Item(SFAA.ProductItem)
                        moItemHash.Add(moNumber, item)
                    End If

                End If
                    Dim strItem = item & opCode & opSeq
                Dim PQC As String = "N"
                'get routing 
                If ItemRoutingHash.ContainsKey(strItem) Then
                    PQC = ItemRoutingHash(strItem)
                Else
                    'check pqc in item routing
                    SQL2 = "select " & ECBB.PQC & " from " & ECBB.tblItemRoutingBody &
                           " where " & ECBB.ent & "=3 and " & ECBB.ProcessPartNo & "='" & item & "' and " & ECBB.ProcessPartNoNumber & "='01' " &
                           " and " & ECBB.CurrentOperation & "='" & opCode & "' and " & ECBB.OperationSeq & "='" & opSeq & "' and  " & ECBB.PQC & "='Y' "
                    dt2 = GetData.GetDataReaderOracle(SQL2, "", GetData.WhoCalledMe)
                    If dt2.Rows.Count > 0 Then
                        PQC = "Y"
                    End If
                    ItemRoutingHash.Add(strItem, PQC)
                End If
                If PQC = "Y" Then
                    strSQL &= "update " & SFCB.tblMOprocessItem_SFCB & " set " & SFCB.PQC & "='Y' where " & SFCB.WONo & " = '" & .Item(SFCB.WONo) & "'  and " & SFCB.OperationID & "='" & opCode & "' and " & SFCB.OperationSeq & "=" & opSeq & " and " & SFCB.RunCard & "=0 ;<br/>"
                End If
            End With
        Next
        lbShow.Text = strSQL


        'Dim aa As String = ""



    End Sub
End Class