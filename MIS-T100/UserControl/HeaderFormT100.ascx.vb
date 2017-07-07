Public Class HeaderFormT100
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public WriteOnly Property HeaderLable() As String
        Set(value As String)
            lblHeader.Text = value
        End Set
    End Property
End Class