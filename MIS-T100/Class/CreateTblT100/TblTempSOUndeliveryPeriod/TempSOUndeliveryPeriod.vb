Public Class TempSOUndeliveryPeriod
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""

    '##--สร้างตาราง TempSOUnDeliveryPeriod
    Sub createTempSOUnDelivery(Temptable As String, ByVal fdate As Date, ByVal amtMonth As Integer)
        Dim SelSQL As String = " if exists (select * from sysobjects where name='" & Temptable & "' )"
        SelSQL = SelSQL & "Drop Table " & Temptable
        DBCONN_SQL.QueryExecuteNonQuery(SelSQL, DBCONN_SQL.MIS2)

        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "custid nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "item nvarchar(20) NOT NULL  ,"
        StrSQL &= "ItemDesc nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "Spec nvarchar(250)  DEFAULT ''  ,"
        For i As Integer = 0 To amtMonth
            Dim tdate As String = fdate.AddMonths(i).ToString("yyyyMM", System.Globalization.CultureInfo.InvariantCulture)
            StrSQL &= "D" & tdate & " nvarchar(50)  DEFAULT 0  ,"
        Next
        StrSQL &= "UndelQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "Unit nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "moQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "poQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "prQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "stockQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "Industry nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "reqQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "sampleQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "deliveryQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "sampledelQty nvarchar(50)  DEFAULT 0  ,"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--InsertUndelStus
    Private Const InsertUndelStus As String = "insert into @tempTable" &
        "(custid,item,ItemDesc,Spec,UndelQty,Unit,reqQty,sampleQty,deliveryQty,sampledelQty,moQty,poQty,stockQty,Industry) values" &
        "('@custid','@item','@ItemDesc','@Spec','@UndelQty','@Unit','@reqQty','@sampleQty','@deliveryQty','@sampledelQty','@moQty','@poQty','@stockQty','@Industry')"
    Public Function InsertSLUndelStus(ByVal custid As String, ByVal item As String, ByVal ItemDesc As String, ByVal Spec As String, ByVal UndelQty As String, ByVal Unit As String,
                                      ByVal reqQty As String, ByVal sampleQty As String, ByVal deliveryQty As String, ByVal sampledelQty As String, ByVal moQty As String,
                                      ByVal poQty As String, ByVal stockQty As String, ByVal IndusDesc As String, ByVal tempTable As String)
        Dim StrSQL As String = InsertUndelStus
        StrSQL = StrSQL.Replace("@custid", custid)
        StrSQL = StrSQL.Replace("@item", item)
        StrSQL = StrSQL.Replace("@ItemDesc", ItemDesc)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@UndelQty", UndelQty)
        StrSQL = StrSQL.Replace("@Unit", Unit)
        StrSQL = StrSQL.Replace("@reqQty", reqQty)
        StrSQL = StrSQL.Replace("@sampleQty", sampleQty)
        StrSQL = StrSQL.Replace("@deliveryQty", deliveryQty)
        StrSQL = StrSQL.Replace("@sampledelQty", sampledelQty)
        StrSQL = StrSQL.Replace("@moQty", moQty)
        StrSQL = StrSQL.Replace("@poQty", poQty)
        StrSQL = StrSQL.Replace("@stockQty", stockQty)
        StrSQL = StrSQL.Replace("@Industry", IndusDesc)
        StrSQL = StrSQL.Replace("@tempTable", tempTable)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--Update Period
    Private Const sqlPeriod As String = "Update @tempTable set @Period = '@UndelQty' where item ='@item'"
    Public Function UpdatePeriod(ByVal tempTable As String, ByVal Period As String, ByVal UndelQty As String, ByVal ItemNo As String)
        Dim StrSQL As String = sqlPeriod
        StrSQL = StrSQL.Replace("@tempTable", tempTable)
        StrSQL = StrSQL.Replace("@Period", Period)
        StrSQL = StrSQL.Replace("@UndelQty", UndelQty)
        StrSQL = StrSQL.Replace("@item", ItemNo)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Function

End Class
