Public Class UsingItemMasterGroup
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setItemMasterGroup()
                DL_ItemMasterGroup.Items.Insert(0, New ListItem("--- Select---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_ItemMasterGroup
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_ItemMasterGroup.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_ItemMasterGroup.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_ItemMasterGroup.Items.Clear()
            Else
                setItemMasterGroup(value, False)
            End If
        End Set
    End Property
    Sub setItemMasterGroup()
        Dim ddt As DataTable = OOCQL.GetDataItemMasterGroup
        With DL_ItemMasterGroup
            .DataSource = ddt
            .DataValueField = OOCQL.OperationID
            .DataTextField = OOCQL.ShowData
            .DataBind()
        End With
    End Sub
    Sub setItemMasterGroup(val As String, Optional showAll As Boolean = True)
        setItemMasterGroup()
    End Sub

End Class