Public Class UsingStatusBOM_ItemMaster
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setBOM_ItemMaster()
                DL_BOM_ItemMaster.Items.Insert(0, New ListItem("--- Select Status---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_BOM_ItemMaster
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_BOM_ItemMaster.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_BOM_ItemMaster.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_BOM_ItemMaster.Items.Clear()
            Else
                setBOM_ItemMaster(value, False)
            End If
        End Set
    End Property
    Sub setBOM_ItemMaster()
        Dim ddt As DataTable = DTstatusT100.BOM_ItemMaster
        With DL_BOM_ItemMaster
            .DataSource = ddt
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            '.DataValueField = "StusID"
            '.DataTextField = "Stus"
            .DataBind()
        End With
    End Sub
    Sub setBOM_ItemMaster(val As String, Optional showAll As Boolean = True)
        setBOM_ItemMaster()
    End Sub

End Class