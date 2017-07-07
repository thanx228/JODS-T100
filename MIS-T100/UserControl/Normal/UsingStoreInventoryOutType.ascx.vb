Public Class UsingStoreInventoryOutType
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowInventoryOutType_DataTable()
                DL_StorInventoryOut.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_StorInventoryOut
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_StorInventoryOut.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_StorInventoryOut.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_StorInventoryOut.Items.Clear()
            Else
                setShowInventoryOutType_DataTable(value, False)
            End If
        End Set
    End Property
    Sub setShowInventoryOutType_DataTable()
        Dim ddt As DataTable = OOBXL.GetInventoryOutType_Table
        With DL_StorInventoryOut
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowInventoryOutType_DataTable(val As String, Optional showAll As Boolean = True)
        setShowInventoryOutType_DataTable()
    End Sub

End Class