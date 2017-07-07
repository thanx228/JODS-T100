Public Class UsingReworkTypeCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowReworkTrype_Dataset()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbReworkType
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbReworkType
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbReworkType.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbReworkType.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbReworkType.Items.Clear()
            Else
                setShowReworkTrype_Dataset(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbReworkType.Items.Clear()
            Else
                setShowReworkTrype_Dataset(value, True)
            End If
        End Set
    End Property
    Sub setShowReworkTrype_Dataset()
        Dim ddt As DataSet = OOBXL.GetDoc_ReworkDataset
        With cbReworkType
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowReworkTrype_Dataset(val As String, Optional showAll As Boolean = True)
        setShowReworkTrype_Dataset()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbReworkType.Items
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
        For Each item As ListItem In cbReworkType.Items
            item.Selected = True
        Next
        Return cbReworkType
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbReworkType.Items
            item.Selected = False
        Next
        Return cbReworkType
    End Function
End Class