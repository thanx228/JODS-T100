Public Class UsingStatusStore_IQC_checkList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setStore_IQC()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbStore_IQC
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbStore_IQC
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbStore_IQC.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbStore_IQC.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbStore_IQC.Items.Clear()
            Else
                setStore_IQC(value, False)
            End If
        End Set
    End Property
    Sub setStore_IQC()
        Dim ddt As DataTable = DTstatusT100.Store_IQC
        Dim ds As New DataSet
        ds.Tables.Add(ddt)
        With cbStore_IQC
            .DataSource = ds
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setStore_IQC(val As String, Optional showAll As Boolean = True)
        setStore_IQC()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbStore_IQC.Items
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
        For Each item As ListItem In cbStore_IQC.Items
            item.Selected = True
        Next
        Return cbStore_IQC
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbStore_IQC.Items
            item.Selected = False
        Next
        Return cbStore_IQC
    End Function
End Class