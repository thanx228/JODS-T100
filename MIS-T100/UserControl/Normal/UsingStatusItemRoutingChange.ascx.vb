Public Class UsingStatusItemRoutingChange
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setItemRoutingChange()
                DL_ItemRoutingChange.Items.Insert(0, New ListItem("--- Select Status---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_ItemRoutingChange
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_ItemRoutingChange.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_ItemRoutingChange.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_ItemRoutingChange.Items.Clear()
            Else
                setItemRoutingChange(value, False)
            End If
        End Set
    End Property
    Sub setItemRoutingChange()
        Dim ddt As DataTable = DTstatusT100.ItemRoutingChange
        With DL_ItemRoutingChange
            .DataSource = ddt
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            '.DataValueField = "StusID"
            '.DataTextField = "Stus"
            .DataBind()
        End With
    End Sub
    Sub setItemRoutingChange(val As String, Optional showAll As Boolean = True)
        setItemRoutingChange()
    End Sub

End Class