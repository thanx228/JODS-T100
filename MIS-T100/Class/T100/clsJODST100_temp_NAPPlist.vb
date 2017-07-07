Public Class clsJODST100_temp_NAPPlist

    Dim obj_clsconnectdb As New clsDBConnect

    Private Const strTempInsert As String = "INSERT INTO temp_NAPPlist (linenum,module_code,module_name,wc_code,wc_description,notapprove_num,reptype,docNo,docDate,mono,loginID) VALUES (@linenum,@module_code,@module_name,@wc_code,@wc_description,@notapprove_num,@reptype,@docNo,@docDate,@mono,@loginID)"
    Public Function InsertTempRecord(ByVal linenum As Integer, ByVal mod_id As String, ByVal mod_name As String, ByVal wc As String, ByVal wcname As String, ByVal nap As Integer, ByVal reptype As Integer, ByVal loginId As String, Optional ByVal docNo As String = "", Optional ByVal docDate As String = "", Optional ByVal mono As String = "")

        Dim strSQL As String = strTempInsert
        strSQL = strSQL.Replace("@linenum", "'" & linenum & "'")
        strSQL = strSQL.Replace("@module_code", "'" & mod_id & "'")
        strSQL = strSQL.Replace("@module_name", "'" & mod_name & "'")
        strSQL = strSQL.Replace("@wc_code", "'" & wc & "'")        '
        strSQL = strSQL.Replace("@wc_description", "'" & wcname & "'")    '
        strSQL = strSQL.Replace("@notapprove_num", nap)
        strSQL = strSQL.Replace("@reptype", reptype)
        strSQL = strSQL.Replace("@loginID", "'" & loginId & "'")
        strSQL = strSQL.Replace("@docNo", "'" & docNo & "'")
        strSQL = strSQL.Replace("@docDate", "'" & docDate & "'")
        strSQL = strSQL.Replace("@mono", "'" & mono & "'")
        obj_clsconnectdb.QueryExecuteNonQuery(strSQL, obj_clsconnectdb.MIS2)
        Return Nothing

    End Function

    Private Const strGetLogDataShow_Summary As String = "SELECT linenum as 'Line No.', module_code as 'Module Code', module_name as 'Module Name', notapprove_num as 'Not Approved' FROM temp_NAPPlist WHERE loginID=@loginId ORDER BY linenum"
    Public Function GetLogDataShow_Summary_Dataset(ByVal loginId As String) As DataSet

        Dim strSQL As String = strGetLogDataShow_Summary
        Dim ds As DataSet
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        ds = obj_clsconnectdb.QueryDataSet(strSQL, obj_clsconnectdb.MIS2)
        Return ds

    End Function

    Private Const strGetLogDataShow_Detail As String = "SELECT linenum as 'Line No.', module_code as 'Module Code', module_name as 'Module Name', wc_code as 'Workcenter Code', wc_description as 'Workcenter Name', docNo as 'Document Number', docDate as 'Document Date',mono as 'MO Number' FROM temp_NAPPlist WHERE loginID=@loginId ORDER BY linenum"
    Public Function GetLogDataShow_Detail_Dataset(ByVal loginId As String) As DataSet

        Dim strSQL As String = strGetLogDataShow_Detail
        Dim ds As DataSet
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        ds = obj_clsconnectdb.QueryDataSet(strSQL, obj_clsconnectdb.MIS2)
        Return ds

    End Function

    Private Const strLogClear As String = "DELETE FROM temp_NAPPlist WHERE loginID=@loginId"
    Public Function ClearTempRec(ByVal loginId As String)

        Dim strSQL As String = strLogClear
        strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
        obj_clsconnectdb.QueryExecuteNonQuery(strSQL, obj_clsconnectdb.MIS2)
        Return Nothing

    End Function

    'Public Function GetLogDataShow_Summary_DataTable(ByVal loginId As String) As DataTable

    '    Dim strSQL As String = strGetLogDataShow_Summary
    '    Dim dt As DataTable
    '    strSQL = strSQL.Replace("@loginId", "'" & loginId & "'")
    '    dt = obj_clsconnectdb.QueryDataTable(strSQL, obj_clsconnectdb.MIS2)
    '    Return dt

    'End Function

End Class
