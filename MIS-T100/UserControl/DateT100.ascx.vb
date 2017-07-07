Imports System.Globalization
Public Class DateT100
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    '###### Get format Date T100 ####################

    Public Property dateText() As String
        Get
            Return tbDate.Text
        End Get
        Set(value As String)
            tbDate.Text = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return tbDate.Text
        End Get
        Set(ByVal value As String)
            tbDate.Text = value
        End Set
    End Property
End Class