Imports Microsoft.VisualBasic
'Imports System.Runtime.CompilerServices.Extension
Namespace Css
    Public Class StyleSheetToPage
        ''' <summary>
        ''' Add Css and Include jQuery
        ''' </summary>
        ''' <param name="page"></param>
        Public Shared Sub AddStyleSheetToPage(page As Page)
            Dim s As New HtmlLink
            s.Attributes.Add("type", "text/css")
            s.Attributes.Add("rel", "stylesheet")
            s.Href = page.ResolveUrl("../../Styles/Site.css")
            page.Header.Controls.Add(s)
            Dim s2 As New HtmlLink
            s2.Attributes.Add("type", "text/css")
            s2.Attributes.Add("rel", "stylesheet")
            s2.Href = page.ResolveUrl("../../Scripts/gridviewScroll.css")
            page.Header.Controls.Add(s2)

            Dim jsResource As New LiteralControl()
            jsResource.Text = "<script type=""text/javascript"" src=""../../Scripts/jquery.js""></script>"
            page.Header.Controls.Add(jsResource)

            Dim jsResource2 As New LiteralControl()
            jsResource2.Text = "<script type=""text/javascript"" src=""../../Scripts/jquery-ui/jquery-ui.min.js""></script>"
            page.Header.Controls.Add(jsResource2)

            Dim jsResource3 As New LiteralControl()
            jsResource3.Text = "<script type=""text/javascript"" src=""../../Scripts/gridviewScroll.min.js""></script>"
            page.Header.Controls.Add(jsResource3)
            ' # Call Function AddStyleSheetToPage
            ' 1.  >>   Css.StyleSheetToPage.AddStyleSheetToPage( Me)
            ' 2.  >>   StyleSheetToPage.AddStyleSheetToPage( Me)

            ' 1.  >>   Css.StyleSheetToPage.AddStyleSheetToPage("StyleSheet.css", Me)
            ' 2.  >>   StyleSheetToPage.AddStyleSheetToPage("StyleSheet.css", Me)
        End Sub

        ''' <summary>
        ''' ### Wait........
        ''' </summary>
        ''' <param name="page"></param>
        ''' <param name="gv"></param>
        Public Shared Sub GridScrollBar(page As Page, ByVal gv As String)
            'Dim Script As System.Web.UI.ScriptManager
            'Dim cs As ClientScriptManager = page.ClientScript
            Dim JavaScript As New LiteralControl()
            JavaScript.Text = "<div>"
            JavaScript.Text += " <script type =""text/javascript"">"
            JavaScript.Text += " function gridviewScrollShow() { "
            JavaScript.Text += " gridView1 = $('#<%=" & gv & ".ClientID %>').gridviewScroll({ "
            JavaScript.Text += " width: 150,"
            JavaScript.Text += " height: 200,"
            JavaScript.Text += " railcolor:'#F0F0F0', "
            JavaScript.Text += "barcolor: '#CDCDCD',"
            JavaScript.Text += "barhovercolor: '#606060',"
            JavaScript.Text += "bgcolor: '#F0F0F0',"
            JavaScript.Text += "arrowsize: 30,"
            JavaScript.Text += " varrowtopimg:'../../Images/arrowvt.png',"
            JavaScript.Text += "varrowbottomimg:'../../Images/arrowvb.png',"
            JavaScript.Text += "harrowleftimg:'../../Images/arrowhl.png',"
            JavaScript.Text += "harrowrightimg:'../../Images/arrowhr.png',"
            JavaScript.Text += "headerrowcount: 1,"
            JavaScript.Text += "railsize: 16,"
            JavaScript.Text += "barsize: 8"
            JavaScript.Text += "  }); "
            JavaScript.Text += "  } "
            JavaScript.Text += " </script> "
            JavaScript.Text += " </div>"
            'page.Controls.Add(JavaScript)
            'page.Controls.Add(JavaScript)
            'Return JavaScript
            ' page.ClientScript.RegisterStartupScript(page.[GetType](), "Javascript", JavaScript.Text)
            'Script.RegisterClientScriptInclude(page.GetType(), "Javascript", "javascript:gridviewScrollShow(); ", True)
            'ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "gridviewScrollShow();", True)
        End Sub

        Public Shared Sub GenerateJsTag(page As Page)
            Dim jsCode As String = ""
            Dim gv As String = "GridView1"
            jsCode = " function gridviewScrollShow() { "
            jsCode += " gridView1 = $('#<%=" & gv & ".ClientID %>').gridviewScroll({ "
            jsCode += " width: 150,"
            jsCode += " height: 200,"
            jsCode += " railcolor:'#F0F0F0', "
            jsCode += "barcolor: '#CDCDCD',"
            jsCode += "barhovercolor: '#606060',"
            jsCode += "bgcolor: '#F0F0F0',"
            jsCode += "arrowsize: 30,"
            jsCode += " varrowtopimg:'../../Images/arrowvt.png',"
            jsCode += "varrowbottomimg:'../../Images/arrowvb.png',"
            jsCode += "harrowleftimg:'../../Images/arrowhl.png',"
            jsCode += "harrowrightimg:'../../Images/arrowhr.png',"
            jsCode += "headerrowcount: 1,"
            jsCode += "railsize: 16,"
            jsCode += "barsize: 8"
            jsCode += "  }); "
            jsCode += "  } "

            ' ClientScript.RegisterStartupScript(Me.GetType(), jsCode.ToString, "javaScript")
            'page.RegisterStartupScript(GetType(), "UniqueKeyForThisScript", jsCode, True)
            'Dim jsLink = New HtmlGenericControl() With {
            '    .TagName = "script",
            '    .InnerHtml = jsCode.ToString
            '}
            'jsLink.Attributes.Add("type", "text/javascript")
            'Page.Header.Controls.Add(jsLink)
            ' Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), Nothing, jsCode.ToString)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "gridviewScrollShow();", True)
            ' Master.BottomScriptsPlaceHolder.Controls.Add(Helper.CreateJavaScriptLink("~/Scripts/master.js"))



            'Dim scriptText As String = ""
            'scriptText &= "function DisplayCharCount(){"
            'scriptText &= "   getElementByID("spanCounter").innerText = " & _
            '    "document.forms[0].TextBox1.value.length"
            'scriptText &= "}"
            'ClientScriptManager.RegisterClientScriptBlock(Me.GetType(),
            '    "CounterScript", scriptText, True)
            'TextBox1.Attributes.Add("onkeyup", "DisplayCharCount()")
            'Dim spanLiteral As New LiteralControl()
            'spanLiteral.Text = "<span id=""spanCounter""></span>"
            'PlaceHolder1.Controls.Add(spanLiteral)




            'form1.Attributes("script") = jsCode
            ''create Google Maps API library script tag
            'Dim objLibrary As New HtmlGenericControl("script")

            ''add attributes
            'objLibrary.Attributes.Add("type", "text/javascript")
            'objLibrary.Attributes.Add("src", "http://maps.google.com/maps?file=api&v=2&sensor=false&key=ABQIAAAAynoIQZ5YX-BdZ9UvBsREmBRZz1l8SlBkcLc8c82DcC4MDIosshQLjjGtypnNhjFkfxXwjfObtAgcyw")

            ''add to master page
            'Page.Header.Controls.Add(objLibrary)
        End Sub

    End Class
End Namespace



