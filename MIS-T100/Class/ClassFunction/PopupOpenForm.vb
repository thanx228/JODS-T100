Imports System.Net
Public Class PopupOpenForm
    Public Shared Sub OpenPopup(page As Page, url As String)
        'url = "subWebForm5.aspx"
        Dim S_Paper As String = "A3"
        Dim Data4 As String = "DailyOrder"
        Dim StrUrl As String = "subWebForm5.aspx"
        Dim SendData As String = "RuningID='000000'&yyyy='2017'"
        ScriptManager.RegisterStartupScript(page, page.GetType(), "popup", "window.open('" & url & "'?" & SendData & "','_blank')", True)
    End Sub
    '    <Script language = "javascript" type="text/javascript">
    'Function popitup(url) {
    '	newwindow = window.open(url,'name','height=200,width=150');
    '    If(window.focus) {newwindow.focus()}
    '	Return False;
    '}

    '</script>

    Public Shared Sub WebRequest(url As String)
        ' url = "subWebForm5.aspx"
        Dim siteUri As New Uri(url)
        Dim myReq As HttpWebRequest = HttpWebRequest.Create(siteUri)
    End Sub
End Class
