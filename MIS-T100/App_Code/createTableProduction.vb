Public Class createTableProduction
    Dim Conn_SQL As New ConnSQL


    Sub createProductionElectrodUsage()
        Dim table As String = "ProductionElectrodUsage"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "electrod integer DEFAULT 0,"
        StrSQL &= "squeezeTime integer DEFAULT 0,"
        StrSQL &= "weldTime1 integer DEFAULT 0,"
        StrSQL &= "weldTime2 integer DEFAULT 0,"
        StrSQL &= "current1 integer DEFAULT 0,"
        StrSQL &= "current2 integer DEFAULT 0,"
        StrSQL &= "upSlope integer DEFAULT 0,"
        StrSQL &= "coolTime integer DEFAULT 0,"
        StrSQL &= "holdTime integer DEFAULT 0,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class
