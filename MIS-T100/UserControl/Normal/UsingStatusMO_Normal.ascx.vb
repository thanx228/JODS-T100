Public Class UsingStatusMO_Normal
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setDL_MO_Normal()
                DL_MO_Normal.Items.Insert(0, New ListItem("--- Select Status---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_MO_Normal
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_MO_Normal.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_MO_Normal.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_MO_Normal.Items.Clear()
            Else
                setDL_MO_Normal(value, False)
            End If
        End Set
    End Property
    Sub setDL_MO_Normal()
        Dim ddt As DataTable = DTstatusT100.MO_Normal
        With DL_MO_Normal
            .DataSource = ddt
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            '.DataValueField = "StusID"
            '.DataTextField = "Stus"
            .DataBind()
        End With
    End Sub
    Sub setDL_MO_Normal(val As String, Optional showAll As Boolean = True)
        setDL_MO_Normal()
    End Sub

End Class