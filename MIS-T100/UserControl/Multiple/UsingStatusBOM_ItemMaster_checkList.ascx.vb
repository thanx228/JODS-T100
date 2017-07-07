Public Class UsingStatusBOM_ItemMaster_checkList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setBOM_ItemMaster()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbBOM_ItemMaster
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbBOM_ItemMaster
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbBOM_ItemMaster.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbBOM_ItemMaster.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbBOM_ItemMaster.Items.Clear()
            Else
                setBOM_ItemMaster(value, False)
            End If
        End Set
    End Property
    Sub setBOM_ItemMaster()
        Dim ddt As DataTable = DTstatusT100.BOM_ItemMaster
        Dim ds As New DataSet
        ds.Tables.Add(ddt)
        With cbBOM_ItemMaster
            .DataSource = ds
            .DataValueField = DTstatusT100.StatusID
            .DataTextField = DTstatusT100.ShowStatus
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setBOM_ItemMaster(val As String, Optional showAll As Boolean = True)
        setBOM_ItemMaster()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbBOM_ItemMaster.Items
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
        For Each item As ListItem In cbBOM_ItemMaster.Items
            item.Selected = True
        Next
        Return cbBOM_ItemMaster
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbBOM_ItemMaster.Items
            item.Selected = False
        Next
        Return cbBOM_ItemMaster
    End Function
End Class