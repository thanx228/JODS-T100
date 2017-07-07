Public Class UsingMat_IssueType
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowMaterialIssueType()
                DL_MatIssueType.Items.Insert(0, New ListItem("--- Select Type ---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_MatIssueType
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_MatIssueType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_MatIssueType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowMaterialIssueType(value)
            If value = "" Then
                DL_MatIssueType.Items.Clear()
            Else
                setShowMaterialIssueType(value, False)
            End If
        End Set
    End Property
    Sub setShowMaterialIssueType()
        Dim ddt As DataTable = OOBXL.GetMaterailIssueType_Table
        With DL_MatIssueType
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
        End With
    End Sub
    Sub setShowMaterialIssueType(val As String, Optional showAll As Boolean = True)
        setShowMaterialIssueType()
    End Sub

End Class