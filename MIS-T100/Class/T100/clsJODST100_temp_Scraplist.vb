Public Class clsJODST100_temp_Scraplist

    Dim obj_clsconnectdb As New clsDBConnect


    'clstemp.InsertTempRecord(scrapdocNo, docDate, item, spec, approvestat)

    Private strTempInsert As String = "INSERT INTO temp_SCRAPlist (linenum,reptype,wc_code,wc_description,scrap_docNo,docDate,mono,proditem_code,proditem_spec,mo_qty,scrap_qty,scrap_percent,rework_qty,approvestat,datefrom,dateto,loginID) VALUES (@linenum,@reptype_code,@wc_code,@wc_description,@scrap_docNo,@docDate,@mono,@proditem_code,@proditem_spec,@mo_qty,@scrap_qty,@scrap_percent,@rework_qty,@approvestat,@datefrom,@dateto,@loginID)"
    Public Function InsertTempRecord(ByVal linenum As Integer, ByVal reptype As String, ByVal docNo As String, ByVal docDate As String, ByVal item As String, ByVal spec As String, ByVal approvestat As String, ByVal loginID As String)

        Dim strSQL As String = strTempInsert
        strSQL = strSQL.Replace("@linenum", linenum)
        strSQL = strSQL.Replace("@reptype_code", reptype)
        strSQL = strSQL.Replace("@wc_code", "''") '        strSQL = strSQL.Replace("@wc_code", "'" & reptype & "'")
        strSQL = strSQL.Replace("@wc_description", "''")  '        strSQL = strSQL.Replace("@wc_description", "'" & reptype & "'")
        strSQL = strSQL.Replace("@scrap_docNo", "'" & docNo & "'")
        strSQL = strSQL.Replace("@docDate", "'" & docDate & "'")
        strSQL = strSQL.Replace("@mono", "''")   '        strSQL = strSQL.Replace("@mono", "'" & reptype & "'")
        strSQL = strSQL.Replace("@proditem_code", "'" & item & "'")
        strSQL = strSQL.Replace("@proditem_spec", "'" & spec & "'")
        strSQL = strSQL.Replace("@mo_qty", 0)   '        strSQL = strSQL.Replace("@mo_qty", "'" & reptype & "'")
        strSQL = strSQL.Replace("@scrap_qty", 0) '        strSQL = strSQL.Replace("@scrap_qty", "'" & reptype & "'")
        strSQL = strSQL.Replace("@scrap_percent", 0)   '        strSQL = strSQL.Replace("@scrap_percent", "'" & reptype & "'")
        strSQL = strSQL.Replace("@rework_qty", 0)
        strSQL = strSQL.Replace("@approvestat", "'" & approvestat & "'")
        strSQL = strSQL.Replace("@datefrom", "''")   '        strSQL = strSQL.Replace("@datefrom", "'" & reptype & "'")
        strSQL = strSQL.Replace("@dateto", "''")  '        strSQL = strSQL.Replace("@dateto", "'" & reptype & "'")
        strSQL = strSQL.Replace("@loginID", "'" & loginID & "'")
        obj_clsconnectdb.QueryExecuteNonQuery(strSQL, obj_clsconnectdb.MIS2)
        Return Nothing

    End Function

    Private Shared strGetLogDataShow_Summary As String = ""
    Public Function GetLogDataShow_Summary_Dataset(ByVal loginId As String) As DataSet

        Dim strSQL As String = strGetLogDataShow_Summary
        Dim ds As DataSet
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        ds = obj_clsconnectdb.QueryDataSet(strSQL, obj_clsconnectdb.MIS2)
        Return ds

    End Function

    Private Const strGetLogDataShow_Detail As String = "SELECT scrap_docNo as 'Document No.', docDate as 'Document Date', proditem_code as 'Item Code', proditem_spec as 'Specification', approvestat as 'Approve Status' FROM temp_SCRAPlist WHERE loginID=@loginId ORDER BY linenum"
    Public Function GetLogDataShow_Detail_Dataset(ByVal loginId As String) As DataSet

        Dim strSQL As String = strGetLogDataShow_Detail
        Dim ds As DataSet
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        ds = obj_clsconnectdb.QueryDataSet(strSQL, obj_clsconnectdb.MIS2)
        Return ds

    End Function

    Private Const strLogClear As String = "DELETE FROM temp_SCRAPlist WHERE loginID=@loginId"
    Public Function ClearTempRec(ByVal loginId As String)

        Dim strSQL As String = strLogClear
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        obj_clsconnectdb.QueryExecuteNonQuery(strSQL, obj_clsconnectdb.MIS2)
        Return Nothing

    End Function

End Class
