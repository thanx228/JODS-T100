Public Class UsingReworkOverType
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowReworkOverType()
                DL_ReworkOverType.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public ReadOnly Property getValue() As String
        Get
            Return DL_ReworkOverType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_ReworkOverType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowTransferType(value)
            If value = "" Then
                DL_ReworkOverType.Items.Clear()
            Else
                setShowReworkOverType(value, False)
            End If
        End Set
    End Property
    Sub setShowReworkOverType()
        Dim dt As DataTable = OOBXL.GetMatReworkOverType_Table
        With DL_ReworkOverType
            .DataSource = dt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowReworkOverType(val As String, Optional showAll As Boolean = True)
        setShowReworkOverType()
    End Sub

End Class