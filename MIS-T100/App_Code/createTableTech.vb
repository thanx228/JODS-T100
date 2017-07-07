Public Class createTableTech
    Dim Conn_SQL As New ConnSQL
    'add 2015-07-14 by noi
    Sub CreateTaskRequest()
        Dim table As String = "taskRequestTech"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo varchar(15) NOT NULL  ,"
        StrSQL &= "docDate varchar(10) DEFAULT '',"
        StrSQL &= "dept varchar(5) DEFAULT '',"
        StrSQL &= "empId varchar(10) DEFAULT '',"
        StrSQL &= "telExt varchar(20) DEFAULT '',"
        StrSQL &= "taskGrp varchar(10) DEFAULT '',"
        StrSQL &= "mchType varchar(10) DEFAULT ''," 'mch/tool type
        StrSQL &= "assetNo varchar(150) DEFAULT '',"
        StrSQL &= "mchNo varchar(150) DEFAULT '',"
        StrSQL &= "problem varchar(10) DEFAULT '',"
        StrSQL &= "detial ntext DEFAULT '',"
        StrSQL &= "status char(2) DEFAULT '1',"
        StrSQL &= "actualDate varchar(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateTaskAssign()
        Dim table As String = "taskAssign"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo varchar(15) NOT NULL  ,"
        StrSQL &= "docReq varchar(20) DEFAULT '',"
        StrSQL &= "planDate varchar(10) DEFAULT '',"
        StrSQL &= "techId varchar(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
End Class
