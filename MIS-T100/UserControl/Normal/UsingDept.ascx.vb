Public Class UsingDept
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setDept()
                DL_Dept.Items.Insert(0, New ListItem("--- Select Dept ---", "0"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Public ReadOnly Property getObject() As DropDownList
        Get
            Return DL_Dept
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return DL_Dept.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            DL_Dept.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowType(value)
            If value = "" Then
                DL_Dept.Items.Clear()
            Else
                setDept(value, False)
            End If
        End Set
    End Property
    Sub setDept()
        Dim ddt As DataTable = OOEFL.GetDepartment_Table
        With DL_Dept
            .DataSource = ddt
            '.DataValueField = "Dept_ID"
            '.DataTextField = "Department"
            .DataValueField = OOEFL.DeptID
            .DataTextField = OOEFL.ShowDeparment
            .DataBind()
        End With
    End Sub
    Sub setDept(val As String, Optional showAll As Boolean = True)
        setDept()
    End Sub

End Class