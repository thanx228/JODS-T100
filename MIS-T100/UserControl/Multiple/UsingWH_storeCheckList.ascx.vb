Public Class UsingWH_storeCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowWarehourse_DataSet()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbWH_storeList
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbWH_storeList
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbWH_storeList.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbWH_storeList.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWarehourse_DataSet(value)
            If value = "" Then
                cbWH_storeList.Items.Clear()
            Else
                setShowWarehourse_DataSet(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWarehourse_DataSet(value)
            If value = "" Then
                cbWH_storeList.Items.Clear()
            Else
                setShowWarehourse_DataSet(value, True)
            End If
        End Set
    End Property
    Sub setShowWarehourse_DataSet()
        Dim ddt As DataSet = INAA.GetWarehouse_DataSet
        With cbWH_storeList
            .DataSource = ddt
            .DataValueField = INAA.WharehouseID
            .DataTextField = INAA.ShowpWarehouse
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowWarehourse_DataSet(val As String, Optional showAll As Boolean = True)
        setShowWarehourse_DataSet()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbWH_storeList.Items
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
        For Each item As ListItem In cbWH_storeList.Items
            item.Selected = True
        Next
        Return cbWH_storeList
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbWH_storeList.Items
            item.Selected = False
        Next
        Return cbWH_storeList
    End Function
End Class