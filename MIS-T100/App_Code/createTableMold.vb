Public Class createTableMold
    Dim Conn_SQL As New ConnSQL
    'add 2015-08-13 by noi
    Sub createMoldInfoHead()
        Dim table As String = "MoldInfoHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "item varchar(20) NOT NULL  ,"
        StrSQL &= "cust varchar(5) DEFAULT '',"
        StrSQL &= "partNo varchar(250) DEFAULT '',"
        StrSQL &= "revision varchar(10) DEFAULT '',"
        StrSQL &= "moldOwner varchar(10) DEFAULT '',"
        StrSQL &= "remark nvarchar(250) DEFAULT '',"
        StrSQL &= "status char(2) DEFAULT '1',"
        StrSQL &= "detail nvarchar(255) DEFAULT '',"
        StrSQL &= "Type nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createMoldInfoBody()
        Dim table As String = "MoldInfoBody"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "item varchar(20) NOT NULL  ,"
        StrSQL &= "seq varchar(4) NOT NULL ,"
        StrSQL &= "operation varchar(4)  DEFAULT '',"
        StrSQL &= "step varchar(4)  DEFAULT '',"
        StrSQL &= "loc varchar(10)  DEFAULT '',"
        StrSQL &= "power varchar(250) DEFAULT '',"
        StrSQL &= "shot nvarchar(255) DEFAULT '',"
        StrSQL &= "limitSh nvarchar(255) DEFAULT '',"
        StrSQL &= "remark nvarchar(250) DEFAULT '',"
        StrSQL &= "detail nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(item,seq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createMoldProcessRecord()
        Dim table As String = "MoldProcessRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docno char(20) NOT NULL ,"
        StrSQL &= "motype varchar(4) NOT NULL ,"
        StrSQL &= "mo varchar(25) NOT NULL ,"
        StrSQL &= "opSeq char(6) NOT NULL ,"
        StrSQL &= "item varchar(25) NOT NULL ,"
        StrSQL &= "spec varchar(255) DEFAULT '',"
        StrSQL &= "custID varchar(255) DEFAULT '',"
        StrSQL &= "oper varchar(255) DEFAULT '',"
        StrSQL &= "moldPower varchar(250) DEFAULT '',"
        StrSQL &= "machType varchar(255) DEFAULT '',"
        StrSQL &= "machName varchar(250) DEFAULT '',"
        StrSQL &= "StartDate varchar(255) DEFAULT '',"
        StrSQL &= "EndDate varchar(255) DEFAULT '',"
        StrSQL &= "status varchar(255) DEFAULT '',"
        StrSQL &= "remark varchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '',"
        StrSQL &= "CreateDate varchar(255) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '',"
        StrSQL &= "ChangeDate varchar(255) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docno,motype,mo,opSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    Sub createMoldRecordMain()
        Dim table As String = "MoldRecordMain"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20) NOT NULL ,"
        StrSQL &= "docNoRef char(20) DEFAULT '',"
        StrSQL &= "type varchar(10) NOT NULL ,"
        StrSQL &= "item varchar(25) DEFAULT '',"
        StrSQL &= "moldseq char(6) DEFAULT '',"
        StrSQL &= "machType varchar(255) DEFAULT '',"
        StrSQL &= "machName varchar(255) DEFAULT '',"
        StrSQL &= "invQty Decimal(16,4)  DEFAULT 1  ,"
        StrSQL &= "detail nvarchar(MAX) DEFAULT '',"
        StrSQL &= "remark nvarchar(MAX) DEFAULT '',"
        StrSQL &= "status varchar(10) DEFAULT '',"
        StrSQL &= "docDate varchar(20) DEFAULT '',"
        StrSQL &= "docTime varchar(20) DEFAULT '',"
        StrSQL &= "remarkCan nvarchar(MAX) DEFAULT '',"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '',"
        StrSQL &= "CreateDate varchar(20) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '',"
        StrSQL &= "ChangeDate varchar(20) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo,type)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createMoldRecordSubMain()
        Dim table As String = "MoldRecordSubMain"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo char(20) NOT NULL ,"
        StrSQL &= "docNoRef char(20) DEFAULT '',"
        StrSQL &= "type varchar(10) NOT NULL ,"
        StrSQL &= "item varchar(25) DEFAULT '',"
        StrSQL &= "moldseq char(6) DEFAULT '',"
        StrSQL &= "moType varchar(4) DEFAULT '',"
        StrSQL &= "mo varchar(25) DEFAULT '',"
        StrSQL &= "opSeq char(6) NOT NULL ,"
        StrSQL &= "status varchar(10) DEFAULT '',"
        StrSQL &= "shotQty Decimal(16,4)  DEFAULT 0,"
        StrSQL &= "remarkCan nvarchar(MAX) DEFAULT '',"
        StrSQL &= "CreateBy varchar(20)  DEFAULT '',"
        StrSQL &= "CreateDate varchar(20) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '',"
        StrSQL &= "ChangeDate varchar(20) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo,type,item,moldseq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class

