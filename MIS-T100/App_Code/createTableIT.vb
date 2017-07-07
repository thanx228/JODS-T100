Public Class createTableIT
    Dim Conn_SQL As New ConnSQL

    Sub createChangeCorrectionRecord()
        Dim table As String = "ChangeCorrectionRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "id varchar(12) NOT NULL ,"
        StrSQL &= "recordDate varchar(8) DEFAULT ''  ,"
        StrSQL &= "deptCode varchar(5) DEFAULT ''   ,"
        StrSQL &= "userCode varchar(10) DEFAULT ''   ,"
        StrSQL &= "funcCode varchar(10) DEFAULT ''   ,"
        StrSQL &= "docType varchar(5) DEFAULT ''  ,"
        StrSQL &= "docNo varchar(20) DEFAULT ''  ,"
        StrSQL &= "docSeq varchar(5) DEFAULT ''  ,"
        StrSQL &= "corrCode varchar(5) DEFAULT ''   ," 'correction
        StrSQL &= "causeCode varchar(5) DEFAULT ''   ,"
        StrSQL &= "sourceFrom varchar(5) DEFAULT ''   ,"
        StrSQL &= "remark nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


End Class
