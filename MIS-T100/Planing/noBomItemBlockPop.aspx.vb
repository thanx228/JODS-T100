Public Class noBomItemBlockPop
    Inherits System.Web.UI.Page

    Dim IMAAL As New IMAAL
    Dim clsconnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function getItemInfo(ByVal itemid As String) As String

        Dim disp As String = ""
        Dim ds As DataSet
        Dim name As String = ""
        Dim spec As String = ""
        Dim sql As String = "SELECT  imaal001 AS ItemID,imaal003 AS ItemName,imaal004 AS Spec
                             FROM imaal_t
                             WHERE imaalent='3' AND imaal001='" & itemid & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            name = ds.Tables("DATASET")(i)("ItemName").ToString
            spec = ds.Tables("DATASET")(i)("Spec").ToString
        Next
        disp = "<table border=1><tr bgcolor=#FFFFFF><td align=center>Master ItemID</td><td align=left>" & itemid & "</td></tr><tr bgcolor=#FFFFFF><td align=center>Master ItemName</td><td align=left>" & name & "</td></tr><tr bgcolor=#FFFFFF><td align=center>Specification</td><td align=left>" & spec & "</td></tr></table>"

        Return disp

    End Function

    Public Function getSODeliveryLine(ByVal itemid As String) As String

        Dim disp As String = "<table border=1>"
        Dim ds As DataSet

        Dim SO As String = ""
        Dim TotalOrderQty As String = ""
        Dim BalanceQTy As String = ""
        Dim sql As String = "SELECT xmdddocno AS SO,xmdd031 AS TotalOrderQty,(xmdd031-xmdd014) AS BalanceQTy
                             FROM xmdd_t
                             LEFT JOIN imaal_t ON xmdd001=imaal001 AND imaal002='en_US'
                             WHERE xmddent='3' AND imaalent='3' AND xmdd001='" & itemid & "'
                             AND (xmdd031-xmdd014) <>0"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        disp = disp + "<tr bgcolor=#FFFFFF><th align=center>&nbsp;SONo.&nbsp;</th><th align=center>&nbsp;SO&nbsp;</th><th align=center>&nbsp;TotalOrderQty&nbsp;</th><th align=center>&nbsp;Balance&nbsp;</th></tr>"
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                SO = ds.Tables("DATASET")(i)("SO").ToString
                TotalOrderQty = ds.Tables("DATASET")(i)("TotalOrderQty").ToString
                BalanceQTy = ds.Tables("DATASET")(i)("BalanceQTy").ToString
                disp = disp + "<tr bgcolor=#FFFFFF><td align=right>&nbsp;" & i + 1 & "&nbsp;</td><td align=left>&nbsp;" & SO & "&nbsp;</td><td align=right>&nbsp;" & TotalOrderQty & "&nbsp;</td><td align=right>&nbsp;" & BalanceQTy & "&nbsp;</td></tr>"
            Next
            disp = disp + "</table>"
        Else
            disp = disp + "<tr bgcolor=#FFFFFF><td align=center colspan=4>No Data Found</td></tr>"
        End If

        Return disp

    End Function

End Class