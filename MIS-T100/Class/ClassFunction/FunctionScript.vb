Public Class FunctionScript
    Public Shared Sub CloseWinForm(page As Page)
        Dim sb As New StringBuilder()
        sb.Append("<script type=""text/javascript"">")
        sb.Append("self.close();</")
        sb.Append("script>")
        page.ClientScript.RegisterClientScriptBlock(page.[GetType](), "MyCloseScript", sb.ToString())
    End Sub

    Public Shared Sub CloseForChrome(page As Page)
        'Dim sb As New StringBuilder()
        'sb.Append("<script type=""text/javascript"">")
        'sb.Append(" window.open('','_self',''); ")
        'sb.Append("self.close();</")
        'sb.Append("script>")
        'page.ClientScript.RegisterClientScriptBlock(page.[GetType](), "javascript", " window.close();")
    End Sub
End Class


