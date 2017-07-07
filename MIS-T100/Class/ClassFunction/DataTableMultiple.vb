Imports System.Data.OracleClient
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports System.Linq
Imports System.Collections.Generic
Imports System
Imports System.Drawing
Imports System.Web.UI.Control
Public Class DataTableMultiple
    '###################### Function for Add DateBetween Join To DataTable #############################################
    Public Shared Function AddDateBetweenToDataTable(dt As DataTable, SDate As String, EDate As String) As DataSet
        Dim StartDate As Date
        Dim EndDate As Date
        'If SDate <> String.Empty AndAlso EDate <> String.Empty Then
        StartDate = ConvertUtility.StringToDate(SDate)
        EndDate = ConvertUtility.StringToDate(EDate)
        Dim tbl As New DataTable
        Dim dr As DataRow = Nothing
        Dim columns As DataColumnCollection = dt.Columns
        Dim column As DataColumn
        For Each column In columns
            tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
        Next
        Dim eRow As DataRowCollection = dt.Rows
        For Each drs As DataRow In eRow
            tbl.ImportRow(drs)
        Next
        For offset = 0 To (EndDate - StartDate).Days
            tbl.Columns.Add(New DataColumn(StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
        Next

        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
        ' End If
    End Function
    '###################### Function for Add DateBetween Join To DataTable #############################################
    Public Shared Function AddDaysBetweenMonth(dt As DataTable, SDay As Integer, EDay As Integer) As DataSet
        'If SDate <> String.Empty AndAlso EDate <> String.Empty Then
        Dim tbl As New DataTable
        Dim dr As DataRow = Nothing
        Dim columns As DataColumnCollection = dt.Columns
        Dim column As DataColumn
        For Each column In columns
            tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
        Next
        Dim eRow As DataRowCollection = dt.Rows
        For Each drs As DataRow In eRow
            tbl.ImportRow(drs)
        Next
        For offset = 1 To 31
            tbl.Columns.Add(New DataColumn(offset))
        Next

        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
        ' End If
    End Function
    '#################### Function for DataTable1 Join DataTable2 to New_DataTable ########################################
    'Public Shared Function DataTable_Join_DataTable(dtA As DataTable, dtB As DataTable) As DataTable
    '    Dim dtNew As New DataTable
    '    Dim dr As DataRow = Nothing
    '    Dim coldtA As DataColumnCollection = dtA.Columns
    '    Dim columnA As DataColumn
    '    For Each columnA In coldtA
    '        dtNew.Columns.Add(New DataColumn(columnA.ColumnName, columnA.DataType))
    '    Next
    '    Dim coldtB As DataColumnCollection = dtB.Columns
    '    For Each columnB In coldtB
    '        dtNew.Columns.Add(New DataColumn(columnB.ColumnName, columnB.DataType))
    '    Next
    '    Dim RowA As DataRowCollection = dtA.Rows
    '    For Each drA As DataRow In RowA
    '        dtNew.ImportRow(drA)
    '    Next
    '    Dim RowB As DataRowCollection = dtA.Rows
    '    For Each drB As DataRow In RowB
    '        dtNew.ImportRow(drB)
    '    Next

    '    Dim dsA As New DataSet()
    '    dsA.Tables.Add(dtNew)
    '    Dim dtAddnew As DataTable = dsA.Tables(0)

    '    Return dtAddnew
    'End Function
End Class
