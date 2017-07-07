Public Class UsingWH_Store
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowWHStore()
                DL_WHstore.Items.Insert(0, New ListItem("--- Select ---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_WHstore
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_WHstore.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_WHstore.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_WHstore.Items.Clear()
            Else
                setShowWHstore(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_WHstore.Items.Clear()
            Else
                setShowWHstore(value, True)
            End If
        End Set
    End Property
    Sub setShowWHStore()
        Dim ddt As DataTable = INAA.GetWarehouse_Table
        With DL_WHstore
            .DataSource = ddt
            .DataValueField = INAA.WharehouseID
            .DataTextField = INAA.ShowpWarehouse
            .DataBind()
        End With
    End Sub
    Sub setShowWHstore(val As String, Optional showAll As Boolean = True)
        setShowWHStore()
    End Sub

End Class