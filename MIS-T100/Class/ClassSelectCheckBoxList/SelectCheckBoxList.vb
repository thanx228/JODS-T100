Imports System
Imports System.Data
Imports System.IO
Public Class SelectCheckBoxList
    Public Shared RowNumSelect As Integer
    Public Shared RowNum As Integer
    Public Shared Function MultipleSelect(CheckboxList As CheckBoxList) As String
        Dim iRow As Integer = 0
        Dim AaryList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In CheckboxList.Items
            If item.Selected Then
                AaryList.Add(item.Value)
                iRow = +1
                MultipleSelectNum(iRow)
            End If
        Next
        Dim Where As String = String.Empty
        Dim StrSelect As String = String.Empty
        If iRow > 0 Then
            StrSelect = " '" & [String].Join("' , '", AaryList.ToArray())
        End If
        Where = StrSelect
        Return Where
    End Function
    Private Shared Function MultipleSelectNum(RowNum As Integer) As Integer
        RowNumSelect = RowNum
        Return RowNumSelect
    End Function

    Public Shared Function Multiple(CheckboxList As CheckBoxList) As String
        Dim iRow As Integer = 0
        Dim AaryList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In CheckboxList.Items
            If item.Selected Then
                AaryList.Add(item.Value)
                iRow = +1
                MultipleSelect(iRow)
            End If
        Next
        Dim Where As String = String.Empty
        Dim StrSelect As String = String.Empty
        If iRow > 0 Then
            StrSelect = "'" & [String].Join("' , '", AaryList.ToArray())
        End If
        Where = StrSelect
        CheckboxList.ClearSelection()
        Return Where
    End Function
    Private Shared Function MultipleSelect(Row As Integer) As Integer
        RowNum = Row
        Return RowNum
    End Function
End Class
