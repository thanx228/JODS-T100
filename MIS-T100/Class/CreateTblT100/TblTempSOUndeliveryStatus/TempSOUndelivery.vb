Public Class TempSOUndelivery
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    '##--สร้างตาราง TempSOUnDelivery POSO
    Sub createTempSOUnDelivery(UserID As String)
        Dim SQL As String = " if exists (select * from TempSOUnDelivery where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempSOUnDelivery where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempSOUnDelivery"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "custid nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "item nvarchar(20) NOT NULL  ,"
        StrSQL &= "ItemDesc nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "Spec nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "UndelQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "Unit nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "moQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "poQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "prQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "stockQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "Industry nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "reqQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "sampleQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "deliveryQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "sampledelQty  nvarchar(250)  DEFAULT 0  ,"
        StrSQL &= "UserID nvarchar(50)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--InsertUndelStus
    Private Const InsertUndelStus As String = "insert into TempSOUnDelivery" &
        "(custid,item,ItemDesc,Spec,UndelQty,Unit,reqQty,sampleQty,deliveryQty,sampledelQty,moQty,poQty,stockQty,Industry,UserID) values" &
        "('@custid','@item','@ItemDesc','@Spec','@UndelQty','@Unit','@reqQty','@sampleQty','@deliveryQty','@sampledelQty','@moQty','@poQty','@stockQty','@Industry','@UserID')"
    Public Function InsertSLUndelStus(ByVal custid As String, ByVal item As String, ByVal ItemDesc As String, ByVal Spec As String, ByVal UndelQty As String, ByVal Unit As String,
                                      ByVal reqQty As String, ByVal sampleQty As String, ByVal deliveryQty As String, ByVal sampledelQty As String, ByVal moQty As String,
                                      ByVal poQty As String, ByVal stockQty As String, ByVal IndusDesc As String, ByVal UserID As String)
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
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '-- SelectShowDetail Where UserID 
    Private Shared SelectShowDetail As String = "select custid,item,ItemDesc,Spec,UndelQty,Unit,moQty,poQty,prQty,stockQty,Industry from TempSOUnDelivery where UserID='@UserID'" & Whr & "@Whr order by item"
    Public Shared Function ShowDetail(ByVal UserID As String, ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectShowDetail
        StrSQL = StrSQL.Replace("@UserID", UserID)
        StrSQL = StrSQL.Replace("@Whr", Whr)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '##--delete from TempSOUnDelivery Where UserID
    Sub DeleteDetailTempSOUnDelivery(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSOUnDelivery where UserID='" & UserID & "')"
        SQL = SQL & " delete from TempSOUnDelivery where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

End Class
