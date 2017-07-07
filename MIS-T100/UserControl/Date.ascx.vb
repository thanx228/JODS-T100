Imports System.Globalization
Public Class _Date
    Inherits System.Web.UI.UserControl
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    '###### Get format Date ERP เก่า ####################
    Public Property dateVal() As String
        Get
            Return configDate.dateFormat2(tbDate.Text.Trim)
        End Get
        Set(value As String)
            tbDate.Text = value
        End Set
    End Property

    Public Property dateValDefault() As String
        Get
            Return tbDate.Text.Trim
        End Get
        Set(ByVal value As String)
            tbDate.Text = value
        End Set
    End Property
    '###### Get format Date T100 ####################
    'Public Property dateValT100() As String
    '    Get
    '        'Return ConvertUtility.StringToDate(tbDate.Text)
    '        Return tbDate.Text
    '    End Get
    '    Set(value As String)
    '        tbDate.Text = value
    '    End Set
    'End Property

    'Public Property Text() As String
    '    Get
    '        Return tbDate.Text
    '    End Get
    '    Set(ByVal value As String)
    '        tbDate.Text = value
    '    End Set
    'End Property
End Class