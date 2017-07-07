Public Class UsingMO_ReceiptType
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setMO_Receipt()
                DL_Receipt.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_Receipt
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_Receipt.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_Receipt.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setMO_Receipt(value)
            If value = "" Then
                DL_Receipt.Items.Clear()
            Else
                setMO_Receipt(value, False)
            End If
        End Set
    End Property
    Sub setMO_Receipt()
        Dim ddt As DataTable = OOBXL.GetMO_ReceiptType_Table
        With DL_Receipt
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setMO_Receipt(val As String, Optional showAll As Boolean = True)
        setMO_Receipt()
    End Sub

End Class