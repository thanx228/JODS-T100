Public Class UsingMO_Type
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowMOtype()
                DL_MOtype.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_MOtype
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_MOtype.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_MOtype.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_MOtype.Items.Clear()
            Else
                setShowMOtype(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_MOtype.Items.Clear()
            Else
                setShowMOtype(value, True)
            End If
        End Set
    End Property
    Sub setShowMOtype()
        Dim ddt As DataTable = OOBXL.GetMOType_Table
        With DL_MOtype
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowMOtype(val As String, Optional showAll As Boolean = True)
        setShowMOtype()
    End Sub

End Class