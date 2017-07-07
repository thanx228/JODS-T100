Public Class UsingMOTypeCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowMOtypeDataset()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbMOtypeList
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbMOtypeList
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbMOtypeList.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbMOtypeList.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowMOtypeDataset(value)
            If value = "" Then
                cbMOtypeList.Items.Clear()
            Else
                setShowMOtypeDataset(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowMOtypeDataset(value)
            If value = "" Then
                cbMOtypeList.Items.Clear()
            Else
                setShowMOtypeDataset(value, True)
            End If
        End Set
    End Property
    Sub setShowMOtypeDataset()
        Dim ddt As DataSet = OOBXL.GetMOType_DataSet
        With cbMOtypeList
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowMOtypeDataset(val As String, Optional showAll As Boolean = True)
        setShowMOtypeDataset()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbMOtypeList.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            'YrStr = ParameterField & "= '" & [String].Join("'  OR " & ParameterField & "= '", YrStrList.ToArray()) & "'"
            YrStr = " '" & [String].Join("' , '", YrStrList.ToArray())
        End If
        Return YrStr
    End Function
    Public Function CheckedListBox_CheckAll()
        For Each item As ListItem In cbMOtypeList.Items
            item.Selected = True
        Next
        Return cbMOtypeList
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbMOtypeList.Items
            item.Selected = False
        Next
        Return cbMOtypeList
    End Function
End Class