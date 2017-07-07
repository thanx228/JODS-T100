Public Class UsingTypeSaleCheckList
    Inherits System.Web.UI.UserControl
    Public Shared YrStr As [String]
    Public Shared ParameterField As String = "ParameterField"
    Public Shared JP As String = "JP"
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
        Return cbSaleDocTypeList
    End Function
    Public ReadOnly Property getObject() As CheckBoxList
        Get
            Return cbSaleDocTypeList
        End Get
    End Property
    Public ReadOnly Property getValue() As String
        Get
            Return cbSaleDocTypeList.Text.Trim
        End Get
    End Property

    Public WriteOnly Property setValue() As String
        Set(value As String)
            cbSaleDocTypeList.Text = value
        End Set
    End Property

    Public WriteOnly Property setObject() As String
        Set(value As String)
            'setShowWorkstationCheckboxList(value)
            If value = "" Then
                cbSaleDocTypeList.Items.Clear()
            Else
                setShowDocTypeSale_Dataset(value, False)
            End If
        End Set
    End Property
    Sub setShowDocTypeSale_Dataset()
        Dim Dt As DataTable = OOBX.SalesOrderTypeOral()
        With cbSaleDocTypeList
            .DataSource = Dt
            .DataValueField = "oobx001"
            .DataTextField = "SOType"
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
        For Each item As ListItem In cbSaleDocTypeList.Items
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
        For Each item As ListItem In cbSaleDocTypeList.Items
            item.Selected = True
        Next
        Return cbSaleDocTypeList
    End Function

    Public Function CheckedListBox_UnCheckAll()
        For Each item As ListItem In cbSaleDocTypeList.Items
            item.Selected = False
        Next
        Return cbSaleDocTypeList
    End Function

    Public Function CheckBoxArray1() As String
        ' Create the list to store.
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In cbSaleDocTypeList.Items
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