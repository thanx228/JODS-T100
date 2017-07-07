Imports System
Imports System.Data
Public Class DTstatusT100
    Public Shared StatusID As String = "StusID"
    Public Shared Status As String = "Stus"
    Public Shared ShowStatus As String = "ShowStatus"
    Public Shared Function MO_Normal() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Number", GetType(Integer))
        dt.Columns.Add("StusID", GetType(String))
        dt.Columns.Add("Stus", GetType(String))
        dt.Columns.Add("ShowStatus", GetType(String))
        'dt.Rows.Add(1, "A", "Approved", "A : Approved")
        dt.Rows.Add(2, "C", "Closed", "C : Closed")
        dt.Rows.Add(3, "D", "Widthdraw", "D : Widthdraw")
        dt.Rows.Add(4, "E", "termanation", "E : termanation")
        dt.Rows.Add(5, "F", "Released", "F : Released")
        dt.Rows.Add(6, "H", "Hold", "H : Hold")
        dt.Rows.Add(7, "M", "Cost Close", "M : Cost Close")
        dt.Rows.Add(8, "N", "Unapproved", "N : Unapproved")
        dt.Rows.Add(9, "O", "Confrim Transfer-Out", "O : Confrim Transfer-Out")
        dt.Rows.Add(10, "P", "Confirmation for Transfer-in", "P : Confirmation for Transfer-in")
        dt.Rows.Add(11, "R", "Rejected", "R : Rejected")
        dt.Rows.Add(12, "W", "Approving", "W : Approving")
        dt.Rows.Add(13, "X", "Voided", "X : Voided")
        dt.Rows.Add(14, "Y", "Approved", "Y : Approved")
        dt.Rows.Add(15, "Z", "Restore Deduction", "Z : Restore Deduction")
        Return dt
    End Function
    Public Shared Function BOM_ItemMaster() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Number", GetType(Integer))
        dt.Columns.Add("StusID", GetType(String))
        dt.Columns.Add("Stus", GetType(String))
        dt.Columns.Add("ShowStatus", GetType(String))
        dt.Rows.Add(1, "N", "Unapproved", "N : Unapproved")
        dt.Rows.Add(2, "X", "Invaild", "X : Invaild")
        dt.Rows.Add(3, "Y", "Approved", "Y : Approved")
        Return dt
    End Function
    Public Shared Function ItemRoutingChange() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Number", GetType(Integer))
        dt.Columns.Add("StusID", GetType(String))
        dt.Columns.Add("Stus", GetType(String))
        dt.Columns.Add("ShowStatus", GetType(String))
        'dt.Rows.Add(1, "A", "Approved", "A : Approved")
        dt.Rows.Add(2, "D", "Widthdraw", "D : Widthdraw")
        dt.Rows.Add(3, "N", "Unapproved", "N : Unapproved")
        dt.Rows.Add(4, "R", "Rejected", "R : Rejected")
        dt.Rows.Add(5, "W", "Approving", "W : Approving")
        dt.Rows.Add(6, "X", "Voided", "X : Voided")
        dt.Rows.Add(7, "Y", "Approved", "Y : Approved")
        'Dim dt2 As DataTable = dt.Clone
        'Dim dr2 As DataRow() = dt.Select("StusID", "Stus", "StusID" & " : " & "Stus")
        'For Each row As DataRow In dr2
        '    dt2.ImportRow(row)
        'Next
        Return dt
    End Function
    Public Shared Function Store_IQC() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Number", GetType(Integer))
        dt.Columns.Add("StusID", GetType(String))
        dt.Columns.Add("Stus", GetType(String))
        dt.Columns.Add("ShowStatus", GetType(String))
        dt.Rows.Add(1, "Y", "Invaild", "Y : Invaild")
        dt.Rows.Add(2, "Z", "Validity", "Z : Validity")
        Return dt
    End Function
End Class
