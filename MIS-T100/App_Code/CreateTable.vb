Public Class CreateTable
    Dim Conn_SQL As New ConnSQL

    Sub CreateMenuTable()
        'and xtype='U'
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='Menu' )"
        StrSQL = StrSQL & "CREATE TABLE Menu ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "ParentId  Integer NOT NULL  ,"
        StrSQL = StrSQL & "Line  Integer NOT NULL  ,"
        StrSQL = StrSQL & "Name  Char (50)  NULL ,"
        StrSQL = StrSQL & "Prog  Char (100)  NULL,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserGroupTable()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='UserGroup' )"
        StrSQL = StrSQL & "CREATE TABLE UserGroup ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "UserGroup Char(20) NOT NULL ,"
        StrSQL = StrSQL & "IdMenu  Char (10)  NOT NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateUserInfoTable()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='UserInfo' )"
        StrSQL = StrSQL & "CREATE TABLE UserInfo ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "UserName Char(10) NOT NULL ,"
        StrSQL = StrSQL & "UserPassWord  Char (20) NOT NULL ,"
        StrSQL = StrSQL & "UserGroup Char(20) NOT NULL ,"
        StrSQL = StrSQL & "NameSurname  Char (50)  NULL ,"
        StrSQL = StrSQL & "Dept  Char (50)  NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateMachineCapacityTable()
        Dim tableName As String = "MachineCapacity"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tableName & " ("
        StrSQL = StrSQL & "wc Char(10) NOT NULL,"
        StrSQL = StrSQL & "capacity Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "mancapacity Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "PRIMARY KEY(wc)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    Sub CreateProcessCostTable()
        Dim tableName As String = "ProcessCost"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tableName & " ("
        StrSQL = StrSQL & "wc Char(10) NOT NULL,"
        StrSQL = StrSQL & "wcName Char(50) NULL DEFAULT '',"
        StrSQL = StrSQL & "DLCost Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "MachineCost Decimal(16,0) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "PRIMARY KEY(wc)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateMoUrgentTable()
        Dim tableName As String = "MoUrgent"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tableName & " ("
        StrSQL = StrSQL & "TA001 Char(4) NOT NULL," 'MO TYPE
        StrSQL = StrSQL & "TA002 Char(20) NOT NULL," 'MO NO
        StrSQL = StrSQL & "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreatePlanScheduleTable()
        Dim tableName As String = "PlanSchedule"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tableName & " ("
        StrSQL = StrSQL & "PlanDate Char(8) NOT NULL,"
        StrSQL = StrSQL & "TA006 Char(10) NOT NULL," 'Work Center
        StrSQL = StrSQL & "PlanSeq Integer NOT NULL,"
        StrSQL = StrSQL & "PlanSeqSet Char(4) NULL DEFAULT '',"
        StrSQL = StrSQL & "TA001 Char(4) NULL DEFAULT ''," 'MO type
        StrSQL = StrSQL & "TA002 Char(20) NULL DEFAULT ''," 'MO No
        StrSQL = StrSQL & "TA003 Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL = StrSQL & "TA004 Char(4) NULL DEFAULT ''," 'Operation
        StrSQL = StrSQL & "PlanedQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "PlanQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "Mch Char(50) NULL DEFAULT ''," 'Machine
        StrSQL = StrSQL & "Urgent Char(1) NULL DEFAULT '0'," 'Urgent
        StrSQL = StrSQL & "Cancled Char(1) NULL DEFAULT '0'," 'Cancle Plan this item
        'StrSQL = StrSQL & "ReasonCan Char(150) NULL DEFAULT ''," 'Reason for cancle
        StrSQL = StrSQL & "tranNo Char(40) DEFAULT ''," 'Transfer No
        StrSQL = StrSQL & "ap100 Integer DEFAULT(0) ," 'ap 100 time
        StrSQL = StrSQL & "CreateBy  Char (10)  NULL  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(PlanDate,TA006,PlanSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateMoBalanceTable()
        Dim tableName As String = "MoBalance"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tableName & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tableName & " ("
        StrSQL = StrSQL & "TA001 Char(4) NOT NULL," 'MO type
        StrSQL = StrSQL & "TA002 Char(20) NOT NULL," 'MO No
        StrSQL = StrSQL & "TA003 Char(4) NOT NULL," 'MO seq
        StrSQL = StrSQL & "TA004 Char(4) NOT NULL," 'Operation
        StrSQL = StrSQL & "TA006 Char(10) NOT NULL," 'Work Center
        StrSQL = StrSQL & "PlanedQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "PRIMARY KEY(TA001,TA002,TA003,TA004,TA006)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateLogHistoryTable()
        Dim table As String = "LogHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "UserId Char(20) NOT NULL ,"
        StrSQL = StrSQL & "MenuId  Char (10)  NOT NULL ,"
        StrSQL = StrSQL & "ComName Char(30) DEFAULT '' ,"
        StrSQL = StrSQL & "IpAddr Char(20) DEFAULT '' ,"
        StrSQL = StrSQL & "InDateTime Char(25) DEFAULT '',"
        StrSQL = StrSQL & "outDateTime Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    Sub CreateLogInHistoryTable()
        Dim table As String = "LogInHistory"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "UserId Char(20) DEFAULT '' ,"
        StrSQL = StrSQL & "ComName Char(30) DEFAULT '' ,"
        StrSQL = StrSQL & "IpAddr Char(20) DEFAULT '' ,"
        StrSQL = StrSQL & "LogInDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemFollowLotTable()
        Dim table As String = "ItemFollowLot"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "item Char(20) DEFAULT '' ,"
        StrSQL = StrSQL & "dateStart Char(30) DEFAULT '' ,"
        StrSQL = StrSQL & "LotCheck Integer DEFAULT 0   ,"
        StrSQL = StrSQL & "status Char(2) DEFAULT '10' ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemTable()
        Dim table As String = "Item"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "item Char(20) DEFAULT '' ,"
        StrSQL = StrSQL & "Wgh Decimal(16,5) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBoxTable()
        Dim table As String = "Box"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "boxDesc Char(250) DEFAULT '',"
        StrSQL = StrSQL & "boxSpec Char(25) DEFAULT '',"
        StrSQL = StrSQL & "boxWgh Decimal(16,3) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemBoxTable()
        Dim table As String = "ItemBox"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "IdItem Integer NOT NULL  ,"
        StrSQL = StrSQL & "IdBox Integer NOT NULL  ,"
        StrSQL = StrSQL & "QPB Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "selItem Char(1) DEFAULT 'N'," 'say yes='Y',no='N'
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(IdItem,IdBox)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvHead()
        Dim table As String = "InvHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "InvType Char(4) NOT NULL,"
        StrSQL = StrSQL & "InvNumber Char(20) NOT NULL,"
        StrSQL = StrSQL & "InvDate Char(8) DEFAULT '',"
        StrSQL = StrSQL & "InvCust Char(15) DEFAULT '',"
        StrSQL = StrSQL & "InvCustName1 Char(250) DEFAULT '',"
        StrSQL = StrSQL & "InvCustName2 Char(250) DEFAULT '',"
        StrSQL = StrSQL & "InvCustAdd1 text DEFAULT '',"
        StrSQL = StrSQL & "InvCustAdd2 text DEFAULT '',"
        StrSQL = StrSQL & "InvCurrency Char(5) DEFAULT '',"
        StrSQL = StrSQL & "InvContent Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvShiperPer Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvAirWayBill Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvFrom Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvSailingOn Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvAttn Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvTel Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvType2 Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvType3 Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvPrc Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvAmt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvNetWgh Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvGrsWgh Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvCarton int NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "FileNamePic Char(50) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(InvType,InvNumber)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvBody()
        Dim table As String = "InvBody"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "InvType Char(4) DEFAULT '',"
        StrSQL = StrSQL & "InvNumber Char(20) DEFAULT '',"
        StrSQL = StrSQL & "InvOrType Char(4) NOT NULL,"
        StrSQL = StrSQL & "InvOrNumber Char(20) NOT NULL,"
        StrSQL = StrSQL & "InvOrSeq Char(4) NOT NULL,"
        StrSQL = StrSQL & "ShipType Char(4) DEFAULT '',"
        StrSQL = StrSQL & "ShipNumber Char(20) DEFAULT '',"
        StrSQL = StrSQL & "ShipSeq Char(4) DEFAULT '',"
        StrSQL = StrSQL & "InvItemId Char(20) DEFAULT '',"
        StrSQL = StrSQL & "InvItemDesc Char(150) DEFAULT '',"
        StrSQL = StrSQL & "InvCustPO Char(50) DEFAULT '',"
        StrSQL = StrSQL & "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvUnit Char(10) DEFAULT '',"
        StrSQL = StrSQL & "InvPrc Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvAmt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "InvBoxId Integer DEFAULT '0' ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(InvOrType,InvOrNumber,InvOrSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateInvPackingList()
        Dim table As String = "InvPackingList"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "InvType Char(4) NOT NULL,"
        StrSQL = StrSQL & "InvNumber Char(20) NOT NULL,"
        StrSQL = StrSQL & "InvSeq Integer NOT NULL  ,"
        StrSQL = StrSQL & "CartonType Char(1) NOT NULL,"
        StrSQL = StrSQL & "PalNumber Char(10) DEFAULT '',"
        StrSQL = StrSQL & "InvQty Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "CartonCnt Decimal(16,2) NULL DEFAULT(0) ,"
        StrSQL = StrSQL & "CreateBy Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(InvType,InvNumber,InvSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateNCTSpecial()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='NCTSpecial' )"
        StrSQL = StrSQL & "CREATE TABLE NCTSpecial ("
        StrSQL = StrSQL & "Item varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "AP_ID varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "Dim1 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim2 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim3 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim4 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Area Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Area_Acc Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "FileNamePic  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "FileNameCAD  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy  Char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeBy  Char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateNCTSpecialNew()
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='NCTSpecial' )"
        StrSQL = StrSQL & "CREATE TABLE NCTSpecial ("
        StrSQL = StrSQL & "Item varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "AP_ID varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "Type  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "SPType  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "Dim1 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim2 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim3 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Dim4 Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Area Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "Area_Acc Decimal (15,3)  DEFAULT 0 ,"
        StrSQL = StrSQL & "FileNamePic  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "FileNameCAD  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy  Char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeBy  Char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(Item)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillPurHead()
        'BillNo,SupID,SupName,Address1,Address2,Date,Payment,BillShow,CreateBy,Remark,EditBy
        Dim table As String = "BillPurHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "SupID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "SupName varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address1 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address2 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Date varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Payment varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillShow varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Remark varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "EditBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillPurLine()
        Dim table As String = "BillPurLine"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "InvoiceH varchar (5)  NOT NULL ,"
        StrSQL = StrSQL & "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "SupID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "OrderDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Tax Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Paid Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowInvoice varchar (150)  DEFAULT '' ,"
        StrSQL = StrSQL & "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "RemarkInvoice varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "TypeNo varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "OrderType varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub
    'BillPurMonth(BillShow,SupID,Payment,Date,AmountBalance,DueDate,CreateBy)
    Sub CreateBillPurMonth()
        Dim table As String = "BillPurMonth"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "BillShow varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "SupID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "Payment varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Date varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillShow)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateItemCycle()
        Dim table As String = "ItemCycle"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "RunNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "WName varchar (50)  NOT NULL ,"
        StrSQL = StrSQL & "Item varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "[Desc] varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Spec varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Wid varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Unit varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "NoShow varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "UserID varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(RunNo,WName)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateReportCheq()
        Dim table As String = "ReportCheq"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "PayType varchar (10)  NOT NULL ,"
        StrSQL = StrSQL & "PayNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "SuppNo varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "SupName varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "CheqNo varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CheqAmout Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "PRIMARY KEY(PayType,PayNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillTotal()
        Dim table As String = "BillTotal"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "BillNo varchar (10)  NOT NULL ,"
        StrSQL = StrSQL & "Date varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Cid varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CName varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Invoice varchar (150)  DEFAULT '' ,"
        StrSQL = StrSQL & "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Payment Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'select * from Billhead H left join BillLine L on (H.BillNo = L.BillNo) where H.BillNo = {?BillNo} order by InvoiceH desc,InvoiceNo 

    Sub CreateBillhead()
        ' BillNo,CustID,CustName,Address1,Address2,Date,Payment,BillShow,CreateBy,BillBy,BeDate
        Dim table As String = "Billhead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "CustID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "CustName varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address1 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Address2 varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Date varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Payment varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillShow varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "EditBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BeDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateBillLine()
        Dim table As String = "BillLine"
        'BillNo-1,InvoiceH-1,InvoiceNo-1,CustID-1,OrderDate-1,DueDate-1,Amount-1,Balance*-1,ShowAmount,ShowBalance,ShowPaid,Paid
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "InvoiceH varchar (5)  NOT NULL ,"
        StrSQL = StrSQL & "InvoiceNo varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "CustID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "OrderDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Amount Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowAmount nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Balance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowBalance nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "Paid Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "ShowPaid varchar (150)  DEFAULT '' ,"
        StrSQL = StrSQL & "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "AmountText nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "CustPO varchar (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "Remark varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateControlStore()
        Dim table As String = "ControlStore"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "TA001 char(5)  NOT NULL ,"
        StrSQL = StrSQL & "TA002 char(20) NOT NULL ,"
        StrSQL = StrSQL & "TA004 char(10)  DEFAULT '' ,"
        StrSQL = StrSQL & "TA038 char(150)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillNo char(10) DEFAULT '',"
        StrSQL = StrSQL & "StoreInvoice char(5) DEFAULT '',"
        StrSQL = StrSQL & "StoreInDate char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "StoreDO char(5) DEFAULT '',"
        StrSQL = StrSQL & "StoreDoDate char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "StoreBy char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "StoreDate char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Status char(10) DEFAULT '',"
        StrSQL = StrSQL & "RemarkStore varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateControlSales()
        Dim table As String = "ControlSales"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "TA001 char(5)  NOT NULL ,"
        StrSQL = StrSQL & "TA002 char(20) NOT NULL ,"
        StrSQL = StrSQL & "TA004 char(10)  DEFAULT '' ,"
        StrSQL = StrSQL & "TA038 char(150)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillNo char(10) DEFAULT '',"
        StrSQL = StrSQL & "SalesReInDate char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "SalesReDoDate char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "SalesBy char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "SalesDate char(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Status char(10) DEFAULT '',"
        StrSQL = StrSQL & "RemarkSales varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(TA001,TA002)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserPlanAuthority()
        Dim table As String = "UserPlanAuthority"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL  ,"
        StrSQL = StrSQL & "WC  Char (250)  NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateSalesInvoice()
        Dim table As String = "SalesInvoice"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Invoice Char (100) NOT NULL ,"
        StrSQL = StrSQL & "TA038  Char (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "TA004 Char (250) DEFAULT ''  ,"
        StrSQL = StrSQL & "MA002  Char (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(Invoice)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordHead()
        Dim table As String = "WorkRecordHead"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "docNo char(20)  NOT NULL ,"
        StrSQL = StrSQL & "docDate char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "wc char(10) DEFAULT '',"
        StrSQL = StrSQL & "mchCode char(30) DEFAULT '',"
        StrSQL = StrSQL & "shift char(1) DEFAULT 'D',"
        StrSQL = StrSQL & "jobType char(1) DEFAULT '',"
        StrSQL = StrSQL & "LeadCode char(10) DEFAULT '',"
        StrSQL = StrSQL & "opCode char(10) DEFAULT '',"
        StrSQL = StrSQL & "manPower integer DEFAULT 0,"
        StrSQL = StrSQL & "sDateStart char(8) DEFAULT '',"
        StrSQL = StrSQL & "sTimeStart char(5) DEFAULT '',"
        StrSQL = StrSQL & "sDateEnd char(8) DEFAULT '',"
        StrSQL = StrSQL & "sTimeEnd char(5) DEFAULT '',"
        StrSQL = StrSQL & "rDateStart char(8) DEFAULT '',"
        StrSQL = StrSQL & "rTimeStart char(5) DEFAULT '',"
        StrSQL = StrSQL & "rDateEnd char(8) DEFAULT '',"
        StrSQL = StrSQL & "rTimeEnd char(5) DEFAULT '',"
        StrSQL = StrSQL & "setTime integer DEFAULT 0,"
        StrSQL = StrSQL & "workTime integer DEFAULT 0,"
        StrSQL = StrSQL & "LossTime integer DEFAULT 0,"
        StrSQL = StrSQL & "remark nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecord()
        Dim table As String = "WorkRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "docNo char(20)  NOT NULL ,"
        StrSQL = StrSQL & "docSeq integer  NOT NULL ,"
        StrSQL = StrSQL & "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL = StrSQL & "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL = StrSQL & "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL = StrSQL & "inputQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "scrapQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "returnQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "scrapCode char(10) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateManfOrderRecord()
        Dim table As String = "ManfOrderRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "moType Char(4) NOT NULL," 'MO type
        StrSQL = StrSQL & "moNo Char(20) NOT NULL," 'MO No
        StrSQL = StrSQL & "moSeq Char(4) NOT NULL," 'MO seq
        StrSQL = StrSQL & "inputQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "scrapQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "returnQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "PRIMARY KEY(moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordLoss()
        Dim table As String = "WorkRecordLoss"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "docNo char(20)  NOT NULL ,"
        StrSQL = StrSQL & "docSeq integer  NOT NULL ,"
        StrSQL = StrSQL & "lossCode char(10) DEFAULT '',"
        StrSQL = StrSQL & "lDateStart char(8) DEFAULT '',"
        StrSQL = StrSQL & "lTimeStart char(5) DEFAULT '',"
        StrSQL = StrSQL & "lDateEnd char(8) DEFAULT '',"
        StrSQL = StrSQL & "lTimeEnd char(5) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateCodeInfo()
        Dim table As String = "CodeInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "CodeType char(20)  NOT NULL ,"
        StrSQL = StrSQL & "Code char(20)  NOT NULL ,"
        StrSQL = StrSQL & "Name nvarchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "WC char (30)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(CodeType,Code)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserDailyAuthority()
        Dim table As String = "UserDailyAuthority"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL  ,"
        StrSQL = StrSQL & "WC  Char (250)  NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateWorkRecordOper()
        Dim table As String = "WorkRecordOper"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "docNo char(20)  NOT NULL ,"
        StrSQL = StrSQL & "docSeq integer  NOT NULL ,"
        StrSQL = StrSQL & "opCode char(10) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(docNo,docSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateFGLabel()
        Dim table As String = "FGLabel"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "moType Char(4) NOT NULL," 'MO Receipt type
        StrSQL = StrSQL & "moNo Char(20) NOT NULL," 'MO Receipt No
        StrSQL = StrSQL & "moSeq Char(4) NOT NULL," 'MO Receipt seq
        StrSQL = StrSQL & "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "qtyCtn Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "CtnNo Char(250) DEFAULT '' ,"
        StrSQL = StrSQL & "CtnSpec Char(250) DEFAULT '' ,"
        StrSQL = StrSQL & "CtnWgh Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "PackBy varchar (200)  DEFAULT '' ,"
        StrSQL = StrSQL & "custPO Char(150) DEFAULT '' ,"
        StrSQL = StrSQL & "serailNo Char(150) DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo,moType,moNo,moSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateSOConfirmDate()
        Dim table As String = "SOConfirmDate"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "DocType Char(4) NOT NULL,"
        StrSQL = StrSQL & "soType Char(4) NOT NULL,"
        StrSQL = StrSQL & "soNo Char(20) NOT NULL,"
        StrSQL = StrSQL & "soSeq Char(4) NOT NULL,"
        StrSQL = StrSQL & "Item varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Spec varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "qty Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "PlanDelDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "SOReqDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PURRemark varchar(MAX) DEFAULT '',"
        StrSQL = StrSQL & "PURConf Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PCConf Char(25) DEFAULT '',"
        StrSQL = StrSQL & "SaleConf Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PURConf1 Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PCConf1 Char(25) DEFAULT '',"
        StrSQL = StrSQL & "SaleConf1 Char(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate Char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo,soType,soNo,soSeq)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessRecord()
        Dim table As String = "ProductionProcessRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "wc char(10) DEFAULT '',"
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectCode char(10) DEFAULT '',"
        StrSQL &= "dateCode Char (20)  DEFAULT '' ,"
        StrSQL &= "timeCode Char (20)  DEFAULT '' ,"
        StrSQL &= "breakTime char(1) DEFAULT 'N',"
        StrSQL &= "createBy varchar(20)  DEFAULT '' ,"
        StrSQL &= "isSetTime char(1) DEFAULT 'N',"
        StrSQL &= "isMulti char(1) DEFAULT 'N',"
        StrSQL &= "tranNo char(40) DEFAULT '',"
        StrSQL &= "shift char(40) DEFAULT 'D',"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCode char(10) DEFAULT '',"
        StrSQL &= "isTeam char(1) DEFAULT '',"
        StrSQL &= "processType char(1) DEFAULT '',"
        StrSQL &= "processCode char(20) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessSum()
        Dim table As String = "ProductionProcessSum"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "docEnd Integer DEFAULT 0,"
        StrSQL &= "workTime integer DEFAULT 0,"
        StrSQL &= "manPower Integer DEFAULT 0,"
        StrSQL &= "setTime integer DEFAULT 0,"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessOperator()
        Dim table As String = "ProductionProcessOperator"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "moType Char(4) NULL DEFAULT ''," 'MO type
        StrSQL &= "moNo Char(20) NULL DEFAULT ''," 'MO No
        StrSQL &= "moSeq Char(4) NULL DEFAULT ''," 'MO seq
        StrSQL &= "wc char(10) DEFAULT '',"
        StrSQL &= "mc char(30) DEFAULT '',"
        StrSQL &= "dateCode Char (20)  DEFAULT '' ,"
        StrSQL &= "timeCode Char (20)  DEFAULT '' ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateProductionProcessBOM()
        Dim table As String = "ProductionProcessBOM"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "bomItem char(20) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateCOCRecord()
        Dim table As String = "cocRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo varchar(20) NOT NULL ,"
        StrSQL &= "docRev varchar(5) DEFAULT '',"
        StrSQL &= "shipType varchar(5) DEFAULT '',"
        StrSQL &= "shipNo varchar(20) DEFAULT '',"
        StrSQL &= "shipDate varchar(10) DEFAULT '',"
        StrSQL &= "item varchar(20) DEFAULT '',"
        StrSQL &= "custPo varchar(40) DEFAULT '',"
        StrSQL &= "custPoShow varchar(100) DEFAULT '',"
        StrSQL &= "dateCode varchar(100) DEFAULT '',"
        StrSQL &= "qty varchar(100) DEFAULT '',"
        StrSQL &= "drawNo varchar(40) DEFAULT '',"
        StrSQL &= "drawRev varchar(10) DEFAULT '',"
        StrSQL &= "partName varchar(100) DEFAULT '',"
        StrSQL &= "partNo varchar(100) DEFAULT '',"
        StrSQL &= "partRev varchar(10) DEFAULT '',"
        StrSQL &= "rawDesc text DEFAULT '',"
        StrSQL &= "rawManu text DEFAULT '',"
        StrSQL &= "finished text DEFAULT '',"
        StrSQL &= "serialNo varchar(100) DEFAULT '',"
        StrSQL &= "reportRef varchar(40) DEFAULT '',"
        StrSQL &= "waiver varchar(40) DEFAULT '',"
        StrSQL &= "docShow varchar(20) DEFAULT '',"
        StrSQL &= "expdate varchar(40) DEFAULT '',"
        StrSQL &= "ulno varchar(100) DEFAULT '',"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"

        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateOTRecord()
        Dim table As String = "OTRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "EmpNo char(10) NOT NULL,"
        StrSQL = StrSQL & "EmpName char(250) NOT NULL,"
        StrSQL = StrSQL & "Dept  Char (10)  NOT NULL,"
        StrSQL = StrSQL & "Line char(50) DEFAULT '',"
        StrSQL = StrSQL & "Work char(10) DEFAULT '',"
        StrSQL = StrSQL & "Holiday char(10) DEFAULT '',"
        StrSQL = StrSQL & "Absence char(10) DEFAULT '',"
        StrSQL = StrSQL & "AbsenceTime Char (10) DEFAULT '',"
        StrSQL = StrSQL & "Shift char(10) DEFAULT '',"
        StrSQL = StrSQL & "ShiftDay char(10) DEFAULT '',"
        StrSQL = StrSQL & "OTStartDate char(20) DEFAULT '',"
        StrSQL = StrSQL & "OTStartTime char(10) DEFAULT '',"
        StrSQL = StrSQL & "OTEndTime char(10) DEFAULT '',"
        StrSQL = StrSQL & "DateofOT char(20) DEFAULT '',"
        StrSQL = StrSQL & "BusLine char(250) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '',"
        StrSQL = StrSQL & "CreateDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateNormalSatOT()
        Dim table As String = "NormalSatOT"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL  ,"
        StrSQL = StrSQL & "DateSat  char(10)  NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateUserOTRecord()
        Dim table As String = "UserOTRecord"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "Id Integer NOT NULL  ,"
        StrSQL = StrSQL & "Dept  Char (250)  NULL ,"
        StrSQL = StrSQL & "PRIMARY KEY(Id)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

 Sub CreateEmployeeInfo()
        Dim table As String = "employeeInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "empName nvarchar(250) DEFAULT '',"
        StrSQL &= "empNameEng varchar(250) DEFAULT '',"
        StrSQL &= "startDate varchar(10) DEFAULT '',"
        StrSQL &= "probDate varchar(10) DEFAULT '',"
        StrSQL &= "plant varchar(10) DEFAULT '',"
        StrSQL &= "department varchar(10) DEFAULT '',"
        StrSQL &= "position varchar(10) DEFAULT '',"
        StrSQL &= "groupWork varchar(10) DEFAULT '',"
        StrSQL &= "posLevel varchar(10) DEFAULT ''," 'for bonus
        StrSQL &= "statusEmp char(1) DEFAULT 'Y'," '(Y,N)
        StrSQL &= "empPic char(20) DEFAULT '',"
        StrSQL &= "salary Decimal(16,2) DEFAULT(0),"
        StrSQL &= "salPos Decimal(16,2) DEFAULT(0),"
        StrSQL &= "salSpe Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(empCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateEmployeeAttendence()
        Dim table As String = "employeeAttendence"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "year varchar(4) NOT NULL ,"
        StrSQL &= "evalType varchar(5) NOT NULL ,"
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "endDate varchar(10) DEFAULT '',"
        StrSQL &= "C1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "C2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "B Decimal(16,2) DEFAULT(0),"
        StrSQL &= "K Decimal(16,2) DEFAULT(0),"
        StrSQL &= "E Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "P3 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "A3 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "arriveLate Decimal(16,2) DEFAULT(0)," 'add by noi 17-06-2015
        StrSQL &= "priestHood Decimal(16,2) DEFAULT(0)," 'add by noi 07-07-2015
        StrSQL &= "dayAttend Decimal(16,2) DEFAULT(0),"
        StrSQL &= "ageWork Decimal(16,2) DEFAULT(0),"
        StrSQL &= "dayBonus Decimal(16,2) DEFAULT(0),"
        StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
        StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateEmployeeEval()
        Dim table As String = "employeeEval"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "year varchar(4) NOT NULL ,"
        StrSQL &= "evalType varchar(5) NOT NULL ,"
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "endDate varchar(10) DEFAULT '',"
        StrSQL &= "gradeType char(5) DEFAULT '',"
        StrSQL &= "E1 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "E2 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "E3 Decimal(16,2) DEFAULT(0),"
        StrSQL &= "S Decimal(16,2) DEFAULT(0),"
        StrSQL &= "T Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scoreEval Decimal(16,2) DEFAULT(0),"
        StrSQL &= "ageWork Decimal(16,2) DEFAULT(0),"
        StrSQL &= "groupBonus Decimal(16,2) DEFAULT(0),"
        StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
        StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateEmployeeSpecial()
        Dim table As String = "employeeSpecial"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "year varchar(4) NOT NULL ,"
        StrSQL &= "evalType varchar(5) NOT NULL ,"
        StrSQL &= "empCode varchar(10) NOT NULL ,"
        StrSQL &= "endDate varchar(10) DEFAULT '',"
        StrSQL &= "byDay Decimal(16,2) DEFAULT(0),"
        StrSQL &= "byMoney Decimal(16,2) DEFAULT(0),"
        StrSQL &= "wagMin Decimal(16,2) DEFAULT(0),"
        StrSQL &= "bonusAmt Decimal(16,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(year,evalType,empCode)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateEmployeeWeightScore()
        Dim table As String = "employeeWeightScore"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "plant varchar(10) NOT NULL,"
        StrSQL &= "department varchar(10) NOT NULL,"
        StrSQL &= "position varchar(10) NOT NULL,"
        StrSQL &= "groupWork varchar(10) NOT NULL,"
        StrSQL &= "wghScore Decimal(5,2) DEFAULT(0),"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "ChangeDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(plant,department,position,groupWork)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Sub CreateEmployeeTransfer()
        Dim table As String = "employeeTransfer"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "EmpNo char(10) NOT NULL,"
        StrSQL = StrSQL & "EmpName char(250) NOT NULL,"
        StrSQL = StrSQL & "Line char(50) DEFAULT '',"
        StrSQL = StrSQL & "Shift char(10) DEFAULT '',"
        StrSQL = StrSQL & "DeptFrom  Char (10)  NOT NULL,"
        StrSQL = StrSQL & "DeptTo  Char (10)  NOT NULL,"
        StrSQL = StrSQL & "StartTime time(0) DEFAULT '',"
        StrSQL = StrSQL & "EndTime time(0) DEFAULT '',"
        StrSQL = StrSQL & "TotalTime integer DEFAULT '',"
        StrSQL = StrSQL & "DateofTransfer char(10) DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '',"
        StrSQL = StrSQL & "CreateDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    'add new by noi --->>>>>------2015-06-29---->>>>----record loss time 
    Sub CreateProductionProcessLoss()
        Dim table As String = "ProductionProcessLoss"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "lossCode char(10) DEFAULT '',"
        StrSQL &= "dateStart Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStart Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "lossTime integer DEFAULT 0,"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add new by Gift --->>>>>------2015-07-27---->>>>----OTEmpList 
    Sub CreateEmpInfo()
        Dim table As String = "ChangeEmpInfo"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "Dept char(10) NOT NULL,"
        StrSQL = StrSQL & "EmpNo char(10) NOT NULL,"
        StrSQL = StrSQL & "EmpName char(250) NOT NULL,"
        StrSQL = StrSQL & "Line char(50) DEFAULT '',"
        StrSQL = StrSQL & "LineNew char(50) DEFAULT '',"
        StrSQL = StrSQL & "Shift char(10) DEFAULT '',"
        StrSQL = StrSQL & "ShiftNew char(10) DEFAULT '',"
        StrSQL = StrSQL & "BusLine char(250) DEFAULT '',"
        StrSQL = StrSQL & "BusLineNew char(250) DEFAULT '',"
        StrSQL = StrSQL & "Position char(250) DEFAULT '',"
        StrSQL = StrSQL & "PositionNew char(250) DEFAULT '',"
        StrSQL = StrSQL & "ShiftDay char(10) DEFAULT '',"
        StrSQL = StrSQL & "ShiftDayNew char(10) DEFAULT '',"
        StrSQL = StrSQL & "StatusNew char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add new by noi --->>>>>------2015-07-18---->>>>----record log 
    Sub CreateProductionProcessLog()
        Dim table As String = "ProductionProcessLog"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "editCode char(1) DEFAULT ''," '1=update,0=Del
        StrSQL &= "docStart Integer DEFAULT 0,"
        StrSQL &= "opCodeOld char(10) DEFAULT '',"
        StrSQL &= "dateStartOld Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStartOld Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEndOld Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEndOld Char (20)  DEFAULT '' ,"
        StrSQL &= "shiftOld char(40) DEFAULT 'D',"
        StrSQL &= "acceptQtyOld Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQtyOld Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQtyOld Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCodeOld char(10) DEFAULT '',"
        StrSQL &= "isSetOld char(1) DEFAULT '',"
        StrSQL &= "moOld Char (30)  DEFAULT '' ,"
        StrSQL &= "opCode char(10) DEFAULT '',"
        StrSQL &= "dateStart Char (20)  DEFAULT '' ,"
        StrSQL &= "timeStart Char (20)  DEFAULT '' ,"
        StrSQL &= "dateEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "timeEnd Char (20)  DEFAULT '' ,"
        StrSQL &= "shift char(40) DEFAULT 'D',"
        StrSQL &= "acceptQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "defectQty Decimal(16,2) DEFAULT(0) ,"
        StrSQL &= "scrapQty Decimal(16,2) DEFAULT(0),"
        StrSQL &= "scrapCode char(10) DEFAULT '',"
        StrSQL &= "isSet char(1) DEFAULT '',"
        StrSQL &= "mo Char (30)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add new by Gift --->>>>>------2015-09-04---->>>>----QtyMngSys
    Sub CreateQtyMngSys()
        Dim table As String = "QtyMngSys"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "Supplier char(10) NOT NULL,"
        StrSQL = StrSQL & "Certificate char(250) NOT NULL,"
        StrSQL = StrSQL & "Buyer char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "IssueDate char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "ExpDate char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "RecDate char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "RevDate char (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "Status char (2)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateDate char(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate char(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2015-09-16 by noi
    Sub CreateRequirePrToPoLog()
        Dim table As String = "RequirePrToPoLog"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL &= "CREATE TABLE " & table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "prNo varchar (30)  DEFAULT '' ,"
        StrSQL &= "poNo varchar (30)  DEFAULT '' ,"
        StrSQL &= "require1 varchar (255)  DEFAULT '' ,"
        StrSQL &= "require2 varchar (255)  DEFAULT '' ,"
        StrSQL &= "require3 varchar (255)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    'add 2016-02-03 by Gift
    Sub CreateTaskRequest()
        Dim table As String = "TaskRequest"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "DocNo char(20) NOT NULL,"
        StrSQL = StrSQL & "DocDate char(20) NOT NULL,"
        StrSQL = StrSQL & "EmpNo char(10) DEFAULT '',"
        StrSQL = StrSQL & "Dept  nchar(100)  DEFAULT '',"
        StrSQL = StrSQL & "Tel  nchar(10)  DEFAULT '',"
        StrSQL = StrSQL & "MchNo  nchar(100)  DEFAULT '',"
        StrSQL = StrSQL & "AssetNo  nchar(100)  DEFAULT '',"
        StrSQL = StrSQL & "DescProblem  nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "Cause  nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "CorAct  nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "PreAct  nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "ReqDate char(20) DEFAULT '',"
        StrSQL = StrSQL & "CompleteDate char(20) DEFAULT '',"
        StrSQL = StrSQL & "FileNamePic  Char (50)  DEFAULT '' ,"
        StrSQL = StrSQL & "TechNo char(100) DEFAULT '',"
        StrSQL = StrSQL & "Result nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "Remark nchar(250)  DEFAULT '',"
        StrSQL = StrSQL & "Status char(5)   DEFAULT '',"
        StrSQL = StrSQL & "PM char(5)   DEFAULT '',"
        StrSQL = StrSQL & "CM char(5)   DEFAULT '',"
        StrSQL = StrSQL & "BM char(5)   DEFAULT '',"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '',"
        StrSQL = StrSQL & "CreateDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(DocNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub


    'add new by Gift --->>>>>------2016-03-05---->>>>----AssetIT
    Sub CreateAssetIT()
        Dim table As String = "AssetIT"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "AssetNo char(20) NOT NULL,"
        StrSQL = StrSQL & "Name nchar (150)  DEFAULT '' ,"
        StrSQL = StrSQL & "Status char (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "Brand char (100)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(AssetNo)) ;"
        Conn_SQL.Exec_Sql(StrSQL, Conn_SQL.MIS_ConnectionString)
    End Sub

End Class
