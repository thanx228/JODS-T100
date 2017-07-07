Public Class UsingStatusStore_IQC
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setStore_IQC()
                DL_Store_IQC.Items.Insert(0, New ListItem("--- Select Status---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_Store_IQC
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_Store_IQC.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_Store_IQC.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_Store_IQC.Items.Clear()
            Else
                setStore_IQC(value, False)
            End If
        End Set
    End Property
    Sub setStore_IQC()
        Dim ddt As DataTable = DTstatusT100.Store_IQC
        With DL_Store_IQC
            .DataSource = ddt
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            '.DataValueField = "StusID"
            '.DataTextField = "Stus"
            .DataBind()
        End With
    End Sub
    Sub setStore_IQC(val As String, Optional showAll As Boolean = True)
        setStore_IQC()
    End Sub

End Class