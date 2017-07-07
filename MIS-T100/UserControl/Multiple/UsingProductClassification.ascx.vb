Public Class WebUserControl1
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Public Shared JP As String = "JP"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowProductClassification_Dataset()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbProductClassification
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbProductClassification
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbProductClassification.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbProductClassification.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbProductClassification.Items.Clear()
            Else
                setShowDocTypeSale_Dataset(value, False)
            End If
        End Set
    End Property
    Sub setShowProductClassification_Dataset()
        Dim Dt As DataTable = RTAXL.ProductClassification()
        With cbProductClassification
            .DataSource = Dt
            .DataValueField = "rtaxl001"
            .DataTextField = "ProductClassification"
            .DataBind()
            .RepeatColumns = 5 '--เรียกเป็นแนวนอน
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowDocTypeSale_Dataset(val As String, Optional showAll As Boolean = True)
        setShowProductClassification_Dataset()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbProductClassification.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            YrStr = ParameterField & "= '" & [String].Join("'  OR " & ParameterField & "= '", YrStrList.ToArray()) & "'"
        End If
        Return YrStr
    End Function
    Public Function CheckedListBox_CheckAll()
        For Each item As ListItem In cbProductClassification.Items
            item.Selected = True
        Next
        Return cbProductClassification
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbProductClassification.Items
            item.Selected = False
        Next
        Return cbProductClassification
    End Function

    Public Function CheckBoxArray1() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbProductClassification.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            YrStr = [String].Join(" OR ", "JP", YrStrList.ToArray()) & ""
        End If
        Return YrStr
    End Function
End Class