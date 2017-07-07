Public Class UsingDept_checkList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowDocTypeSale_Dataset()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbDeptList
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbDeptList
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbDeptList.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbDeptList.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbDeptList.Items.Clear()
            Else
                setShowDocTypeSale_Dataset(value, False)
            End If
        End Set
    End Property
    Sub setShowDocTypeSale_Dataset()
        Dim ddt As DataSet = OOEFL.GetDepartment_DataSet
        With cbDeptList
            .DataSource = ddt
            '.DataValueField = "Dept_ID"
            '.DataTextField = "Department"
            .DataValueField = OOEFL.DeptID
            .DataTextField = OOEFL.ShowDeparment
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowDocTypeSale_Dataset(val As String, Optional showAll As Boolean = True)
        setShowDocTypeSale_Dataset()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbDeptList.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            ' YrStr = ParameterField & "= '" & [String].Join("'  OR " & ParameterField & "= '", YrStrList.ToArray()) & "'"
            YrStr = " '" & [String].Join("' , '", YrStrList.ToArray())
        End If
        Return YrStr
    End Function
    Public Function CheckedListBox_CheckAll()
        For Each item As ListItem In cbDeptList.Items
            item.Selected = True
        Next
        Return cbDeptList
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbDeptList.Items
            item.Selected = False
        Next
        Return cbDeptList
    End Function
End Class