Imports System.Data

Public Class GetPageError
    Private Shared PathT100 As String = "Class/T100"
    Public Shared Sub GetClassT100(sModule As String, Files As String, Func As String, sql As String, strError As String)
        System.Web.HttpContext.Current.Session("PathVB") = PathT100 & "/" & sModule & "/" & Files & ".vb"
        System.Web.HttpContext.Current.Session("FC") = Func
        System.Web.HttpContext.Current.Session("SQL") = sql
        System.Web.HttpContext.Current.Session("Error") = strError
        System.Web.HttpContext.Current.Response.Redirect("../Class/T100/HttpErrorClassT100.aspx")
    End Sub
    Public Shared Sub GetPage(path As String, Func As String, sql As String, strError As String)
        System.Web.HttpContext.Current.Session("Path") = path
        System.Web.HttpContext.Current.Session("PathVB") = path & ".vb"
        System.Web.HttpContext.Current.Session("FC") = Func
        System.Web.HttpContext.Current.Session("SQL") = sql
        System.Web.HttpContext.Current.Session("Error") = strError
        System.Web.HttpContext.Current.Response.Redirect("../HttpErrorPage.aspx")
    End Sub
End Class
