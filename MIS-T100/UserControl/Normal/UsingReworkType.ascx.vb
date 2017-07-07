Public Class UsingReworkType
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowReworkType()
                DL_ReworkType.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public ReadOnly Property getValue() As String
        Get
            Return DL_ReworkType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_ReworkType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowTransferType(value)
            If value = "" Then
                DL_ReworkType.Items.Clear()
            Else
                setShowReworkType(value, False)
            End If
        End Set
    End Property
    Sub setShowReworkType()
        Dim dt As DataTable = OOBXL.GetDocReworkTable
        With DL_ReworkType
            .DataSource = dt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowReworkType(val As String, Optional showAll As Boolean = True)
        setShowReworkType()
    End Sub

End Class