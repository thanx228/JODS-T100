Public Class validationCheck
    Public Shared strControl As String = "txtEmp"
    Public Shared msgAlert As String = "msg test"
    Public Shared jsScript As String
    Public Shared JavaScript As String
    Public Shared cstype As Type
    Public Shared cs As ClientScriptManager
    '######################## page.aspx อยุ่ใน Path Folder ../Production/Page.aspx ##############################
    Public Shared Sub CommandLoadInPath(page As Page)
        Dim s As New HtmlLink
        s.Attributes.Add("type", "text/css")
        s.Attributes.Add("rel", "stylesheet")
        s.Href = page.ResolveUrl("../js/jquery-ui.css")
        page.Header.Controls.Add(s)
        Dim sLoad As New HtmlLink
        sLoad.Attributes.Add("type", "text/css")
        sLoad.Attributes.Add("rel", "stylesheet")
        sLoad.Href = page.ResolveUrl("../Styles/PopupLoad.css")
        page.Header.Controls.Add(sLoad)

        Dim jsResource As New LiteralControl()
        jsResource.Text = "<script type=""text/javascript"" src=""../js/jquery-1.12.4.js""></script>"
        page.Header.Controls.Add(jsResource)

        Dim jsResource2 As New LiteralControl()
        jsResource2.Text = "<script type=""text/javascript"" src=""../js/jquery-ui.js""></script>"
        page.Header.Controls.Add(jsResource2)

        'Dim jsCss As New LiteralControl()
        'jsCss.Text = "<style type ="" text/css "" >"
        'jsCss.Text += " #divMsg {"
        'jsCss.Text += " position: absolute;"
        'jsCss.Text += "top:0px; "
        'jsCss.Text += "right:0px;"
        'jsCss.Text += " width:100%;"
        'jsCss.Text += " height:100%;"
        'jsCss.Text += " background-color: #000;"
        'jsCss.Text += " background - repeat: no-repeat;"
        'jsCss.Text += "background-position: center;"
        'jsCss.Text += " z-Index: 10000000;"
        'jsCss.Text += " opacity: 0.7;"
        'jsCss.Text += "filter: alpha(opacity = 40);"
        'jsCss.Text += "color: white;"
        'jsCss.Text += " }"
        'jsCss.Text += "</style>"
        'page.Header.Controls.Add(jsCss)



        'Dim jsScript As New LiteralControl()
        'jsScript.Text = "<script type=""text/javascript"">"
        'jsScript.Text += " $(function(){"
        'jsScript.Text += "$('#submitter').one('click', function(){ "
        'jsScript.Text += "$('#divMsg').show(); "
        'jsScript.Text += "});>"
        'jsScript.Text += " });"
        'jsScript.Text += "</script>"
        'page.Header.Controls.Add(jsScript)


    End Sub
    '######################## page.aspx อยุ่นอก Path Folder ##############################
    Public Shared Sub CommandLoadOutPath(page As Page)
        Dim s As New HtmlLink
        s.Attributes.Add("type", "text/css")
        s.Attributes.Add("rel", "stylesheet")
        s.Href = page.ResolveUrl("/js/jquery-ui.css")
        page.Header.Controls.Add(s)
        Dim sLoad As New HtmlLink
        sLoad.Attributes.Add("type", "text/css")
        sLoad.Attributes.Add("rel", "stylesheet")
        sLoad.Href = page.ResolveUrl("/Styles/PopupLoad.css")
        page.Header.Controls.Add(sLoad)


        Dim jsResource As New LiteralControl()
        jsResource.Text = "<script type=""text/javascript"" src=""/js/jquery-1.12.4.js""></script>"
        page.Header.Controls.Add(jsResource)

        Dim jsResource2 As New LiteralControl()
        jsResource2.Text = "<script type=""text/javascript"" src=""/js/jquery-ui.js""></script>"
        page.Header.Controls.Add(jsResource2)

        'Dim jsScript As New LiteralControl()
        'jsScript.Text = "<script type=""text/javascript"">"
        'jsScript.Text += " $(function(){"
        'jsScript.Text += "$('#submitter').one('click', function(){ "
        'jsScript.Text += "$('#divMsg').show(); "
        'jsScript.Text += "});>"
        'jsScript.Text += " });"
        'jsScript.Text += "</script>"
        'page.Header.Controls.Add(jsScript)



        '     <div id = "divMsg" style="display:none" ondrag="return false;">
        '      <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        '      <br /><br />
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '          <img src = "../pic/loading.gif" alt="Please wait.." /><br />
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        '                    Please Wait....processing the request.
        '      </div>
        '<div>

        '</div>


    End Sub



    Public Shared Function ControlCheck() As String
        jsScript = "<SCRIPT Language='JavaScript'>"
        jsScript += "Function() validationCheck() {"
        jsScript += "var summary = "";"
        jsScript += "summary += isvalidsdate();"
        jsScript += "If (summary!= "") {"
        jsScript += "alert(summary);"
        jsScript += "Return False;}"
        jsScript += "Else { Return True; } }"
        jsScript += "Function isvalidsdate() {"
        jsScript += "var id;"
        jsScript += "var temp = document.getElementById(' <%='" & strControl & "'.ClientID%>');"
        jsScript += "id = temp.value;"
        jsScript += "If (id == "") Then {"
        jsScript += "Return ('" & msgAlert & "' + ' \ n'); }"
        jsScript += "Else {"
        jsScript += "Return ""; }"
        jsScript += "var id;  }"
        jsScript += "</script>"

        Return jsScript
    End Function

    Public Shared Function Script(ByVal Page As System.Web.UI.Page)
        cstype = Page.GetType()
        cs = Page.ClientScript
        Return cstype
        Return cs
    End Function
    Public Shared Sub ShowMessage(ByVal msg As String)
        msgAlert = msg
        cs.RegisterStartupScript(cstype, "validation", "<script language='javascript'>alert('" & msgAlert & "');</script>")
    End Sub
    Public Shared Function Alert() As String
        Return strControl
        Return msgAlert
        Return JavaScript
        Return jsScript
    End Function
End Class
