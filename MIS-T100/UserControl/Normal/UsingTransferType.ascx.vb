Public Class UsingTransferType
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowTransferType()
                DL_TransferType.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public ReadOnly Property getValue() As String
        Get
            Return DL_TransferType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_TransferType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowTransferType(value)
            If value = "" Then
                DL_TransferType.Items.Clear()
            Else
                setShowTransferType(value, False)
            End If
        End Set
    End Property
    Sub setShowTransferType()
        Dim dt As DataTable = OOBXL.GetDocTransferOrder_Table
        With DL_TransferType
            .DataSource = dt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowTransferType(val As String, Optional showAll As Boolean = True)
        setShowTransferType()
    End Sub

End Class