Public Class createTableHealth
     Dim Conn_SQL As New ConnSQL
    
    Sub createHealthPersonalHistory()
        Dim table As String = "PersonalHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "EmpCode varchar(20) NOT NULL  ,"
        StrSQL &= "FullNameEng nvarchar(250) DEFAULT '',"
        StrSQL &= "Name nvarchar(250) DEFAULT '',"
        StrSQL &= "SurName nvarchar(250) DEFAULT '',"
        StrSQL &= "Gender nvarchar(15) DEFAULT '',"
        StrSQL &= "Birth varchar(10) DEFAULT '',"
        StrSQL &= "StartDate varchar(10) DEFAULT '',"
        StrSQL &= "IdCardNo varchar(25) DEFAULT '',"
        StrSQL &= "Presentno nvarchar(15) DEFAULT '',"
        StrSQL &= "Presentmoo nvarchar(10) DEFAULT '',"
        StrSQL &= "Presentsoi nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Presentroad nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Presentsubdir nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Presentdir nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Presentprovince nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Presentpost nvarchar(MAX) DEFAULT '',"
        StrSQL &= "PresentTel varchar(20) DEFAULT '',"
        StrSQL &= "Contactno nvarchar(15) DEFAULT '',"
        StrSQL &= "Contactmoo nvarchar(10) DEFAULT '',"
        StrSQL &= "Contactsoi nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Contactroad nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Contactsubdir nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Contactdir nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Contactprovince nvarchar(MAX) DEFAULT '',"
        StrSQL &= "Contactpost nvarchar(MAX) DEFAULT '',"
        StrSQL &= "ContactTel varchar(20) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthWorkHistory()
        Dim table As String = "WorkHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "No char(20) NOT NULL  ,"
        StrSQL &= "EmpCode nvarchar(25) NOT NULL  ,"
        StrSQL &= "CompanyName nvarchar(255) DEFAULT '',"
        StrSQL &= "TypeBus nvarchar(255) DEFAULT '',"
        StrSQL &= "JobDesc nvarchar(255) DEFAULT '',"
        StrSQL &= "startDate nvarchar(25) DEFAULT '',"
        StrSQL &= "endDate nvarchar(25) DEFAULT '',"
        StrSQL &= "HealthRisks nvarchar(255) DEFAULT '',"
        StrSQL &= "RiskManage nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(No,EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthAccidentHistory()
        Dim table As String = "Accidentlists"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "EmpCode nvarchar(255)NOT NULL  ,"
        StrSQL &= "Acc2 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc21 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc3 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc31 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc4 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc41 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc5 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc6 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc61 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc7 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc71 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc10 nvarchar(10) DEFAULT '',"
        StrSQL &= "Acc101 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc11 nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthPastHistory()
        Dim table As String = "PastHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "No char(20)  NOT NULL,"
        StrSQL &= "EmpCode nvarchar(25) NOT NULL  ,"
        StrSQL &= "Accident nvarchar(255) DEFAULT '',"
        StrSQL &= "Year nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode,No)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthFamilyHistory()
        Dim table As String = "FamilyHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "No char(20) NOT NULL  ,"
        StrSQL &= "EmpCode nvarchar(25) NOT NULL  ,"
        StrSQL &= "Relat nvarchar(255) DEFAULT '',"
        StrSQL &= "beSick nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(No,EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthSmokeInfo()
        Dim table As String = "SmokeInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "EmpCode nvarchar(25) NOT NULL  ,"
        StrSQL &= "Acc8 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc81 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc82 nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthDrinkInfo()
        Dim table As String = "DrinkInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "EmpCode nvarchar(25) NOT NULL  ,"
        StrSQL &= "Acc9 nvarchar(255) DEFAULT '',"
        StrSQL &= "Acc91 nvarchar(255) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthCheckUp()
        Dim table As String = "HealthCheckUp"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & " ("
        StrSQL &= "EmpCode nvarchar(25) NOT NULL ,"
        StrSQL &= "HealthType nvarchar(15) NOT NULL ,"
        StrSQL &= "Num nvarchar(10) NOT NULL ,"
        StrSQL &= "DocNo nvarchar(30) DEFAULT '',"
        StrSQL &= "ChkDate nvarchar(255) DEFAULT '',"
        StrSQL &= "weight nvarchar(15) DEFAULT '',"
        StrSQL &= "height nvarchar(15) DEFAULT '',"
        StrSQL &= "BMI nvarchar(255) DEFAULT ''," 'ดัชนีมวลกาย
        StrSQL &= "BP nvarchar(255) DEFAULT ''," 'ความดันโลหิต
        StrSQL &= "Pulse nvarchar(15) DEFAULT ''," 'ชีพจร
        StrSQL &= "Result nvarchar(15) DEFAULT '',"
        StrSQL &= "Remark ntext DEFAULT '',"
        StrSQL &= "Laboratory ntext DEFAULT '',"
        StrSQL &= "Remark1 ntext DEFAULT '',"
        StrSQL &= "Status nvarchar(15) DEFAULT '',"
        StrSQL &= "Year nvarchar(5) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(EmpCode,HealthType,Num)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createSubHealthCheckUp()
        Dim table As String = "SubHealthCheckUp"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & " ("
        StrSQL &= "DocNo nvarchar(30) NOT NULL ,"
        StrSQL &= "RefNo nvarchar(30) NOT NULL ,"
        StrSQL &= "riskFactor nvarchar(30) DEFAULT '',"
        StrSQL &= "Result ntext DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub createSubHealthCheckUp1() 'ตารางสำรองข้อมูล
        Dim table As String = "SubHealthCheckUp1"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & " ("
        StrSQL &= "DocNo nvarchar(30) NOT NULL ,"
        StrSQL &= "RefNo nvarchar(30) NOT NULL ,"
        StrSQL &= "riskFactor nvarchar(30) DEFAULT '',"
        StrSQL &= "Result ntext DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createCodeHealth()
        Dim table As String = "CodeHealth"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & " ("
        StrSQL &= "CodeType nvarchar(30) NOT NULL ,"
        StrSQL &= "Code nvarchar(30) NOT NULL ,"
        StrSQL &= "Name nvarchar(255) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(CodeType,Code)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createPersonalAccidentRecord()
        Dim table As String = "PersonalAccidentRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & " ("
        StrSQL &= "Docno nvarchar(25) NOT NULL ,"
        StrSQL &= "EmpCode nvarchar(25) NOT NULL ,"
        StrSQL &= "DocDate nvarchar(25) DEFAULT '',"
        StrSQL &= "Accident nvarchar(255) DEFAULT '',"
        StrSQL &= "Reason nvarchar(255) DEFAULT '',"
        StrSQL &= "Remark ntext DEFAULT '',"
        StrSQL &= "Infirm nvarchar(10) DEFAULT '',"
        StrSQL &= "LossOflims nvarchar(10) DEFAULT '',"
        StrSQL &= "less3day nvarchar(10) DEFAULT '',"
        StrSQL &= "more3day nvarchar(10) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Docno,EmpCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createCompanyAddress()
        Dim table As String = "CompanyAddress"
        Dim StrSQL As String = "if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & "("
        StrSQL &= "CodeID nvarchar(25) NOT NULL ,"
        StrSQL &= "Name nvarchar(255) DEFAULT '',"
        StrSQL &= "No nvarchar(25) DEFAULT '',"
        StrSQL &= "Moo nvarchar(25) DEFAULT '',"
        StrSQL &= "Soi nvarchar(255) DEFAULT '',"
        StrSQL &= "Road nvarchar(255) DEFAULT '',"
        StrSQL &= "Tambon nvarchar(255) DEFAULT '',"
        StrSQL &= "District nvarchar(255) DEFAULT '',"
        StrSQL &= "Province nvarchar(255) DEFAULT '',"
        StrSQL &= "Post nvarchar(10) DEFAULT '',"
        StrSQL &= "Tel nvarchar(25) DEFAULT '',"
        StrSQL &= "Fax nvarchar(255) DEFAULT '',"
        StrSQL &= "Email nvarchar(255) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(CodeID)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub createHealthRecord()
        Dim table As String = "HealthRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "')"
        StrSQL &= "Create TABLE " & table & "("
        StrSQL &= "DocDate nvarchar(25) NOT NULL ,"
        StrSQL &= "Org nvarchar(255) NOT NULL ,"
        StrSQL &= "Name nvarchar(255) NOT NULL ,"
        StrSQL &= "DocNum nvarchar(25) DEFAULT '',"
        StrSQL &= "NumMedical nvarchar(255) DEFAULT '',"
        StrSQL &= "No nvarchar(15) DEFAULT '',"
        StrSQL &= "Moo nvarchar(15) DEFAULT '',"
        StrSQL &= "Road nvarchar(255) DEFAULT '',"
        StrSQL &= "Tambon nvarchar(255) DEFAULT '',"
        StrSQL &= "District nvarchar(255) DEFAULT '',"
        StrSQL &= "Province nvarchar(255) DEFAULT '',"
        StrSQL &= "Tel nvarchar(15) DEFAULT '',"
        StrSQL &= "Status nvarchar(15) DEFAULT '',"
        StrSQL &= "CreateBy varchar(25) DEFAULT '',"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(DocDate,Org,Name)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class
