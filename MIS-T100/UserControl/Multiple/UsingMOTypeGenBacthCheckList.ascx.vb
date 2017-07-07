Public Class UsingMOTypeGenBacthCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowMOtypeGenBacthDataset()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbMOtypeGenBacthList
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbMOtypeGenBacthList
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbMOtypeGenBacthList.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbMOtypeGenBacthList.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowMOtypeDataset(value)
            If value = "" Then
                cbMOtypeGenBacthList.Items.Clear()
            Else
                setShowMOtypeGenBacthDataset(value, False)
            End If
        End Set
    End Property

    Public WriteOnly Property setObjectWithAll() As String
        Set(value As String)
            'setShowMOtypeDataset(value)
            If value = "" Then
                cbMOtypeGenBacthList.Items.Clear()
            Else
                setShowMOtypeGenBacthDataset(value, True)
            End If
        End Set
    End Property
    Sub setShowMOtypeGenBacthDataset()
        Dim ddt As DataSet = OOBXL.GetMOTypeGenerateBatch_DataSet
        With cbMOtypeGenBacthList
            .DataSource = ddt
            .DataValueField = OOBXL.DocTypeId
            .DataTextField = OOBXL.ShowDocType
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowMOtypeGenBacthDataset(val As String, Optional showAll As Boolean = True)
        setShowMOtypeGenBacthDataset()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbMOtypeGenBacthList.Items
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
        For Each item As ListItem In cbMOtypeGenBacthList.Items
            item.Selected = True
        Next
        Return cbMOtypeGenBacthList
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbMOtypeGenBacthList.Items
            item.Selected = False
        Next
        Return cbMOtypeGenBacthList
    End Function
End Class