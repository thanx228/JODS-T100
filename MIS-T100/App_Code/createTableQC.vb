Public Class createTableQC
    Dim Conn_SQL As New ConnSQL
    'add 2015-11-06 by noi
    Sub createDocumentQC()
        Dim table As String = "DataCollRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docDate varchar(10) DEFAULT '',"
        StrSQL &= "moType varchar(5) DEFAULT '',"
        StrSQL &= "moNo varchar(20) NOT NULL  ,"
        StrSQL &= "docType varchar(10) DEFAULT '',"
        StrSQL &= "docNo nvarchar(50) DEFAULT '',"
        StrSQL &= "remark ntext DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class

