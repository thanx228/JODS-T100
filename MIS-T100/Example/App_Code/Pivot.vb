﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data

' Written by Anurag Gandhi.
' Url: http://www.gandhisoft.com
' Contact me at: soft.gandhi@gmail.com

''' <summary>
''' Pivots the data
''' </summary>
Public Class Pivot
    Private Shared _SourceTable As New DataTable()

    Public Sub New(ByVal SourceTable As DataTable)
        _SourceTable = SourceTable
    End Sub
    ''' <summary>
    ''' Pivots the DataTable based on provided RowField, DataField, Aggregate Function and ColumnFields.//
    ''' </summary>
    ''' <param name="RowField">The column name of the Source Table which you want to spread into rows</param>
    ''' <param name="DataField">The column name of the Source Table which you want to spread into Data Part</param>
    ''' <param name="Aggregate">The Aggregate function which you want to apply in case matching data found more than once</param>
    ''' <param name="ColumnFields">The List of column names which you want to spread as columns</param>
    ''' <returns>A DataTable containing the Pivoted Data</returns>
    Public Shared Function PivotData(ByVal RowField As String, ByVal DataField As String, ByVal Aggregate As AggregateFunction, ByVal ParamArray ColumnFields As String()) As DataTable
        Dim dt As New DataTable()
        Dim Separator As String = "."
        Dim RawRowList = (From x In _SourceTable.AsEnumerable() Select New With {.Name = x.Field(Of Object)(RowField).ToString()}).Distinct()
        Dim RowListParam As String() = (From s In RawRowList Select s.Name).ToArray()
        Dim RowList = GetDistinct(RowListParam)

        'Gets the list of columns .(dot) separated.
        ' Order By LineNo = Integer
        Dim RawColList = (From x In _SourceTable.AsEnumerable()
                          Select New With {.Name = ColumnFields.Select(Function(n) x.Field(Of Object)(n).ToString()) _
                                                 .Aggregate(Function(a, b) (a & Separator & b.ToString()))}).Distinct() _
                                                 .OrderBy(Function(x) x.Name)

        Dim ColListParam As String() = (From s In RawColList Select s.Name).ToArray()
        Dim ColList = GetDistinct(ColListParam)


        dt.Columns.Add(RowField)
        For Each col In ColList
            ' Cretes the result columns.//
            If Not dt.Columns.Contains(col.ToString()) Then
                dt.Columns.Add(col.ToString())
            End If
        Next

        For Each RowName In RowList
            Dim row As DataRow = dt.NewRow()
            row(RowField) = RowName.ToString()
            For Each col In ColList
                Dim strFilter As String = (RowField & " = '") + RowName & "'"
                Dim strColValues As String() = col.ToString().Split(Separator.ToCharArray(), StringSplitOptions.None)
                For i As Integer = 0 To (ColumnFields.Length - 1)
                    strFilter = strFilter & " and " & ColumnFields(i) & " = '" & strColValues(i) & "'"
                Next
                'row(col.ToString()) = GetData(strFilter, DataField, Aggregate)
                row(col.ToString()) = GetDataRows(strFilter, DataField)
            Next
            dt.Rows.Add(row)
        Next
        Return dt
    End Function

    'Private Function GetColList(ByVal ColumnFields As String()) As String()
    '    Dim RawColList = (From x In _SourceTable.AsEnumerable() _
    '                   Select New With {.Name = ColumnFields.Select(Function(n) x.Field(Of Object)(n).ToString()) _
    '                       .Aggregate(Function(a, b) a = a & Separator & b.ToString())}).OrderBy(Function(x) x.Name)

    '    Dim ColList1 As String() = (From s In RawColList Select s.Name).ToArray()
    '    Dim ColList = GetDistinct(ColList1)
    'End Function

    Private Shared Function GetDistinct(ByVal strList As String()) As String()
        Dim NewList As List(Of String) = New List(Of String)()
        For Each myStr In strList
            If Not NewList.Contains(myStr) Then
                NewList.Add(myStr)
            End If
        Next
        GetDistinct = NewList.ToArray()
    End Function

    ''' <summary>
    ''' Retrives the data for matching RowField value and ColumnFields values with Aggregate function applied on them.
    ''' </summary>
    ''' <param name="Filter">DataTable Filter condition as a string</param>
    ''' <param name="DataField">The column name which needs to spread out in Data Part of the Pivoted table</param>
    ''' <param name="Aggregate">Enumeration to determine which function to apply to aggregate the data</param>
    ''' <returns></returns>
    Private Shared Function GetData(ByVal Filter As String, ByVal DataField As String, ByVal Aggregate As AggregateFunction) As Object
        Try
            Dim FilteredRows As DataRow() = _SourceTable.[Select](Filter)
            Dim objList As Object() = FilteredRows.[Select](Function(x) x.Field(Of Object)(DataField)).ToArray()

            Select Case Aggregate
                Case AggregateFunction.Average
                    Return GetAverage(objList)
                Case AggregateFunction.Count
                    Return objList.Count()
                Case AggregateFunction.Exists
                    Return If((objList.Count() = 0), "False", "True")
                Case AggregateFunction.First
                    Return GetFirst(objList)
                Case AggregateFunction.Last
                    Return GetLast(objList)
                Case AggregateFunction.Max
                    Return GetMax(objList)
                Case AggregateFunction.Min
                    Return GetMin(objList)
                Case AggregateFunction.Sum
                    Return GetSum(objList)
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            Return "#Error"
        End Try
        Return Nothing
    End Function

    Private Shared Function GetAverage(ByVal objList As Object()) As Object
        Return If(objList.Count() = 0, Nothing, DirectCast((Convert.ToDecimal(GetSum(objList)) / objList.Count()), Object))
    End Function

    Private Shared Function GetSum(ByVal objList As Object()) As Object
        Return If(objList.Count() = 0, Nothing, DirectCast((objList.Aggregate(New Decimal(), Function(x, y) x + Convert.ToDecimal(y))), Object))
    End Function

    Private Shared Function GetFirst(ByVal objList As Object()) As Object
        Return If((objList.Count() = 0), Nothing, objList.First())
    End Function

    Private Shared Function GetLast(ByVal objList As Object()) As Object
        Return If((objList.Count() = 0), Nothing, objList.Last())
    End Function

    Private Shared Function GetMax(ByVal objList As Object()) As Object
        Return If((objList.Count() = 0), Nothing, objList.Max())
    End Function

    Private Shared Function GetMin(ByVal objList As Object()) As Object
        Return If((objList.Count() = 0), Nothing, objList.Min())
    End Function
    Private Shared Function GetDataRows(ByVal Filter As String, ByVal DataField As String) As Object
        Try
            Dim FilteredRows As DataRow() = _SourceTable.[Select](Filter)
            Dim objList As Object() = FilteredRows.[Select](Function(x) x.Field(Of Object)(DataField)).ToArray()
            Return If((objList.Count() = 0), Nothing, objList.First())
        Catch ex As Exception
            Return "#Error"
        End Try
        Return Nothing
    End Function

End Class

Public Enum AggregateFunction
    Count = 1
    Sum = 2
    First = 3
    Last = 4
    Average = 5
    Max = 6
    Min = 7
    Exists = 8
End Enum

