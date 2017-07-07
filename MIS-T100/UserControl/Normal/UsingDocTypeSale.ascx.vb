Public Class UsingDocTypeSale
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowDocType()
                DL_SaleDocType.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_SaleDocType
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_SaleDocType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_SaleDocType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_SaleDocType.Items.Clear()
            Else
                setShowType(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_SaleDocType.Items.Clear()
            Else
                setShowType(value, True)
            End If
        End Set
    End Property
    Sub setShowDocType()
        Dim ddt As DataTable = OOBXL.GetDocTypeSaleTable
        With DL_SaleDocType
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowType(val As String, Optional showAll As Boolean = True)
        setShowDocType()
    End Sub

End Class