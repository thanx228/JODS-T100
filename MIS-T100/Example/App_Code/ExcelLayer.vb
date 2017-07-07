Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data
Imports System.Data.OleDb

' Written by Anurag Gandhi.
' Url: http://www.gandhisoft.com
' Contact me at: soft.gandhi@gmail.com

''' <summary>
''' Acts as a DataBase Layer
''' </summary>
Public Class ExcelLayer
    Public Sub New()
        '
        ' TODO: Add constructor logic here
        '
    End Sub
    ''' <summary>
    ''' Retireves the data from Excel Sheet to a DataTable.
    ''' </summary>
    ''' <param name="FileName">File Name along with path from the root folder.</param>
    ''' <param name="TableName">Name of the Table of the Excel Sheet. Sheet1$ if no table.</param>
    ''' <returns></returns>
    Public Shared Function GetDataTable(ByVal FileName As String, ByVal TableName As String) As DataTable
        Try
            Dim strPath As String = HttpContext.Current.Request.PhysicalApplicationPath + FileName
            Dim ds As New DataSet()
            Dim sConnectionString As [String] = ("Provider=Microsoft.Jet.OLEDB.4.0; " & "Data Source=") + strPath & "; " & "Extended Properties=Excel 8.0;"

            Dim objConn As New OleDbConnection(sConnectionString)
            objConn.Open()
            Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & TableName & "] where IsActive = 1", objConn)
            Dim objAdapter1 As New OleDbDataAdapter()
            objAdapter1.SelectCommand = objCmdSelect
            objAdapter1.Fill(ds)
            objConn.Close()
            Return ds.Tables(0)
        Catch ex As Exception
            'Log your exception here.//
            Return DirectCast(Nothing, DataTable)
        End Try
    End Function
End Class