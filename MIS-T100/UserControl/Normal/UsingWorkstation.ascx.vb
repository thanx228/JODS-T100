Public Class UsingWorkstation
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowWorkstation()
                DL_Workstation.Items.Insert(0, New ListItem("--- Select ---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_Workstation
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_Workstation.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_Workstation.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_Workstation.Items.Clear()
            Else
                setShowWorkstation(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWorkstation(value)
            If value = "" Then
                DL_Workstation.Items.Clear()
            Else
                setShowWorkstation(value, True)
            End If
        End Set
    End Property
    Sub setShowWorkstation()
        Dim ddt As DataTable = ECAA.GetWorkcenter_Table
        With DL_Workstation
            .DataSource = ddt
            .DataValueField = ECAA.WorkcenterID
            .DataTextField = ECAA.ShowWorkstation
            .DataBind()
        End With
    End Sub
    Sub setShowWorkstation(val As String, Optional showAll As Boolean = True)
        setShowWorkstation()
    End Sub

End Class