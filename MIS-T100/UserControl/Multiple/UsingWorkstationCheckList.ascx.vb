Public Class UsingWorkstationCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String
    Public Shared WC_Auten As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                setShowWorkstationCheckboxList()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Public Function getCheckboxArray() As CheckBoxList
        Return cbwc
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbwc
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbwc.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbwc.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbwc.Items.Clear()
            Else
                setShowWorkstationCheckboxList(value, False)
            End If
        End Set
    End Property
    Sub setShowWorkstationCheckboxList()
        Dim ddt As New DataTable
        ddt = ECAA.GetWorkcenter_Table
        'If WC_Auten = String.Empty Then
        '    ddt = ECAA.GetWorkcenter_Table
        'ElseIf WC_Auten <> String.Empty Then
        '    ddt = ECAA.GetWorkcenterWhere_Table(WC_Auten)
        'End If
        With cbwc
            .DataSource = ddt
            .DataValueField = ECAA.WorkcenterID
            .DataTextField = ECAA.ShowWorkstation
            .DataBind()
            .RepeatColumns = 5
            .RepeatDirection = RepeatDirection.Horizontal
            .RepeatLayout = RepeatLayout.Table
        End With
    End Sub
    Sub setShowWorkstationCheckboxList(val As String, Optional showAll As Boolean = True)
        setShowWorkstationCheckboxList()
    End Sub

    Public Function CheckBoxArray() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbwc.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        '   YrStr = ParameterField & "= '" & [String].Join("'  OR " & ParameterField & "= '", YrStrList.ToArray()) & "'"
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            YrStr = " '" & [String].Join("' , '", YrStrList.ToArray())
        End If
        Return YrStr
    End Function
    Public Function CheckedListBox_CheckAll()
        For Each item As ListItem In cbwc.Items
            item.Selected = True
        Next
        Return cbwc
    End Function
    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbwc.Items
            item.Selected = False
        Next
        Return cbwc
    End Function
    Public Function SeacrhBox_ChekTure()
        Dim StrU As String = String.Empty
        For Each item As ListItem In cbwc.Items
            StrU = item.Text
        Next
        Return StrU
    End Function






















    'Public Function CheckedListBox_CheckAll() Handles cbwc.SelectedIndexChanged
    '    For Each item As ListItem In cbwc.Items
    '        item.Selected = True
    '    Next
    '    Return cbwc
    'End Function

    'Public Function CheckedListBox_UnCheckAll() Handles cbwc.SelectedIndexChanged
    '    For Each item As ListItem In cbwc.Items
    '        item.Selected = False
    '    Next
    '    Return cbwc
    'End Function
End Class