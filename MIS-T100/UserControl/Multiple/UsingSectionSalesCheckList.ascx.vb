Imports System.Web.DynamicData

Partial Class UsingSectionSalesCheckList
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
        Return ChkbSectionSales
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return ChkbSectionSales
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return ChkbSectionSales.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            ChkbSectionSales.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                ChkbSectionSales.Items.Clear()
            Else
                setShowDocTypeSale_Dataset(value, False)
            End If
        End Set
    End Property
    Sub setShowDocTypeSale_Dataset()
        Dim Dt As DataTable = OOEFL.ShowSectionSL
        With ChkbSectionSales
            .DataSource = Dt
            .DataValueField = "ooefl001"
            .DataTextField = "SLSection"
            .DataBind()
            .RepeatColumns = 5 '--เรียกเป็นแนวนอน
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
        For Each item As ListItem In ChkbSectionSales.Items
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
        For Each item As ListItem In ChkbSectionSales.Items
            item.Selected = True
        Next
        Return ChkbSectionSales
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In ChkbSectionSales.Items
            item.Selected = False
        Next
        Return ChkbSectionSales
    End Function
    Public Function CheckBoxArray1() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In ChkbSectionSales.Items
            If item.Selected Then
                YrStrList.Add(item.Value)
            End If
        Next
        If YrStrList.ToArray() Is Nothing Then
            YrStr = String.Empty
        Else
            YrStr = [String].Join(" OR ", YrStrList.ToArray()) & ""
        End If
        Return YrStr
    End Function
End Class
