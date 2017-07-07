Public Class UsingMO_TypeGenBatch
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowMOtypeGenBatch()
                DL_MOtypeGenBatch.Items.Insert(0, New ListItem("--- Select Type---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_MOtypeGenBatch
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_MOtypeGenBatch.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_MOtypeGenBatch.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_MOtypeGenBatch.Items.Clear()
            Else
                setShowMOtypeGenBatch(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_MOtypeGenBatch.Items.Clear()
            Else
                setShowMOtypeGenBatch(value, True)
            End If
        End Set
    End Property
    Sub setShowMOtypeGenBatch()
        Dim ddt As DataTable = OOBXL.GetMOTypeGenerateBatch_Table
        With DL_MOtypeGenBatch
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowMOtypeGenBatch(val As String, Optional showAll As Boolean = True)
        setShowMOtypeGenBatch()
    End Sub

End Class