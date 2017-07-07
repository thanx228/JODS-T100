Public Class CreateTempTableT100
    Dim DBCONN_SQL As New clsDBConnectT100
    Sub T100CreateTempCustoms(tempTable As String)
        Dim SQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SQL = SQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryDataSet(SQL, DBCONN_SQL.MIS2)

        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & tempTable & " ("
        StrSQL = StrSQL & "seq int NOT NULL  ,"
        StrSQL = StrSQL & "po  Char (30) NOT NULL ,"
        StrSQL = StrSQL & "inv  Char (30) NOT NULL ,"
        StrSQL = StrSQL & "qty Decimal(16,6)  NULL  ,"
        StrSQL = StrSQL & "amt Decimal(16,6)  NULL  ,"
        StrSQL = StrSQL & "pack  Decimal(16,6)  NULL  ,"
        StrSQL = StrSQL & "wgh  Decimal(16,6)  NULL  ,"
        StrSQL = StrSQL & "Remark nvarchar(255)  NULL  ,"
        StrSQL = StrSQL & "PRIMARY KEY(seq)) ;"
        DBCONN_SQL.QueryDataSet(StrSQL, DBCONN_SQL.MIS2)
    End Sub
    Public Sub CreateBillPurHead()
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
        StrSQL = StrSQL & "Remark varchar (250)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "ChangeBy varchar(20)  DEFAULT '' ,"
        StrSQL = StrSQL & "ChangeDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(BillNo)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub CreateBillPurLine()
        Dim table As String = "BillPurLine"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & table & "' )"
        StrSQL = StrSQL & "CREATE TABLE " & table & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "BillNo varchar (10) NOT NULL ,"
        StrSQL = StrSQL & "InvoiceNo varchar (25)  NOT NULL ,"
        StrSQL = StrSQL & "SupID varchar (20)  DEFAULT '' ,"
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
        StrSQL = StrSQL & "CreateBy varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "CreateDate varchar(25) DEFAULT '',"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    Sub CreateTempBillPurMonth(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL = StrSQL & "BillShow varchar (20)  NOT NULL ,"
        StrSQL = StrSQL & "SupID varchar (10)  DEFAULT '' ,"
        StrSQL = StrSQL & "Payment varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "BillDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "AmountBalance Decimal(16,2) DEFAULT(0) ,"
        StrSQL = StrSQL & "DueDate varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(BillShow)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
    End Sub
    Sub createTempMaterialShortage(ByVal tempTable As String)
        Dim StrSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL = StrSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim SelSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL &= "CREATE TABLE " & tempTable & " ("
        SelSQL &= "item Char (20) NOT NULL  ,"
        SelSQL &= "delQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "moQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "prQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "prQtyNot Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "poQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "poManQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "poForQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "poMoQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "poRcpQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "saveQty Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "MoqQty Char(10)  DEFAULT 0  ,"
        SelSQL &= "callIn Decimal(16,3)  DEFAULT 0  ,"
        SelSQL &= "confirmdate Char(250)  DEFAULT ''  ,"
        SelSQL &= "plandate Char(50)  DEFAULT ''  ,"
        SelSQL &= "PRIMARY KEY(item)) ;"
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempPlanCallIn(tempTable As String, ByVal fdate As Date, ByVal amtDate As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,3)  DEFAULT 0  ,"
        For i As Integer = 0 To amtDate
            Dim tdate As String = fdate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "issueQty" & tdate & " Decimal(16,3)  DEFAULT 0  ,"
        Next
        StrSQL &= "PRIMARY KEY(item));"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub CreateTempJPItem(ByVal Temptable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL = StrSQL & "ID Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL = StrSQL & "Item nvarchar (20) DEFAULT ''  ,"
        StrSQL = StrSQL & "specification nvarchar (255)  DEFAULT '' ,"
        StrSQL = StrSQL & "Descriptions nvarchar (255)  DEFAULT '' ,"
        StrSQL = StrSQL & "Unit nvarchar (255)  DEFAULT '' ,"
        StrSQL = StrSQL & "PRIMARY KEY(ID)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempMatForcast(ByVal tempTable As String, ByVal fdate As Date, ByVal amtMonth As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stockMRBQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "moQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poConQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "supplier char(250)  DEFAULT ''  ,"
        StrSQL &= "leadtime Decimal(16,2)  DEFAULT 0  ,"
        For i As Integer = 0 To amtMonth
            Dim tdate As String = fdate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "plan" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "issue" & tdate & " Decimal(16,2)  DEFAULT 0  ,"
            StrSQL &= "po" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poRcp" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poCon" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "pr" & tdate & " Decimal(16,2)   DEFAULT 0   ,"
        Next
        'StrSQL &= "fgItem text DEFAULT ''  ,"
        'StrSQL &= "fgDesc text DEFAULT ''  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempBomMat(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        'StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "scrapRatio Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemParent,itemMAT)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempBomShow(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemFG,itemParent,itemMAT)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

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
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub
    Sub CreateTransferOutsourcLog(ByVal Table As String)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Table & "' )"
        StrSQL &= "CREATE TABLE " & Table & " ("
        StrSQL &= "docNo Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "trsType varchar (30)  DEFAULT '' ,"
        StrSQL &= "trsNo varchar (30)  DEFAULT '' ,"
        StrSQL &= "seq varchar (255)  DEFAULT '' ,"
        StrSQL &= "Item varchar (255)  DEFAULT '' ,"
        StrSQL &= "Qty varchar (255)  DEFAULT '' ,"
        StrSQL &= "Price varchar (255)  DEFAULT '' ,"
        StrSQL &= "Amount varchar (255)  DEFAULT '' ,"
        StrSQL &= "CreateBy varchar (20)  DEFAULT '' ,"
        StrSQL &= "CreateDate varchar(25) DEFAULT '',"
        StrSQL &= "PRIMARY KEY(docNo)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempMatNotIssue(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "prQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "stockQty Decimal(16,4)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(item)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub
    '--------------------------------------------------------------------------------------------------------------end update 20170516
    Sub createTempMatForcastOut(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "ym Char (5) NOT NULL  ,"
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "supplier Char (5) NOT NULL  ,"
        StrSQL &= "optCode Char (5) NOT NULL  ,"
        StrSQL &= "sType Char (1) NOT NULL  ," '1=MO,2=SO and 3=forcast
        StrSQL &= "qty Decimal(16,3)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(section,itemType,period,isOldItem)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub

    Sub createTempMatReview(ByVal tempTable As String, ByVal listWeek As Hashtable)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "item Char (20) NOT NULL  ,"
        StrSQL &= "stockQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "stockMRBQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poRcpQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "prQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "poConQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "planQty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "issueQty Decimal(16,2)  DEFAULT 0  ,"
        For Each weekName As String In listWeek.Keys
            StrSQL &= "po" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "poCon" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "plan" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
            StrSQL &= "issue" & weekName & " Decimal(16,2)   DEFAULT 0   ,"
        Next
        StrSQL &= "PRIMARY KEY(item)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub
    Sub createtempBOMReview(ByVal tempTable As String)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & tempTable & "' )"
        SelSQL = SelSQL & "Drop Table " & tempTable
        DBCONN_SQL.QueryExecuteScalar(SelSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & tempTable & "' )"
        StrSQL &= "CREATE TABLE " & tempTable & " ("
        StrSQL &= "itemFG Char (20) NOT NULL  ,"
        StrSQL &= "itemParent Char (20) NOT NULL  ,"
        StrSQL &= "itemMAT Char (20) NOT NULL  ,"
        StrSQL &= "qty Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "scrapRatio Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(itemFG,itemParent,itemMAT)) ;"
        DBCONN_SQL.QueryExecuteScalar(StrSQL, DBCONN_SQL.MIS2)
        DBCONN_SQL.Close(DBCONN_SQL.MIS2)
    End Sub
    'end 2017-06-05

End Class
