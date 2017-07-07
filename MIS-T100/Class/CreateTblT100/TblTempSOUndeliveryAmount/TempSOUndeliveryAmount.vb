Public Class TempSOUndeliveryAmount
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    '##--สร้างตาราง TempSOUnDeliveryAmount
    Sub TempSOUndeliveryAmount(UserID As String)
        Dim SQL As String = " If exists (Select * from TempSOUnDeliveryAmount where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempSOUnDeliveryAmount where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempSOUnDeliveryAmount"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "item nvarchar(20) NOT NULL  ,"
        StrSQL &= "ItemDesc nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "Spec nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "UndelQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "Unit nvarchar(250)  DEFAULT ''  ,"
        StrSQL &= "delAmt nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "moQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "poQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "prQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "stockQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "stockAmt nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "reqQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "sampleQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "deliveryQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "sampledelQty nvarchar(30)  DEFAULT 0  ,"
        StrSQL &= "UserID nvarchar(50)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--InserUndelAmt
    Private Const Insert As String = "insert into TempSOUnDeliveryAmount (item,ItemDesc,Spec,Unit,reqQty,sampleQty,deliveryQty,sampledelQty,UndelQty,delAmt,moQty,poQty,stockQty,stockAmt,UserID) values" &
       " ('@item','@ItemDesc','@Spec','@Unit','@reqQty','@sampleQty','@deliveryQty','@sampledelQty','@UndelQty','@delAmt','@moQty','@poQty','@stockQty','@stockAmt','@UserID')"
    Public Function InserUndelAmt(ByVal item As String, ByVal ItemDesc As String, ByVal Spec As String, ByVal Unit As String, ByVal reqQty As String, ByVal sampleQty As String,
                              ByVal deliveryQty As String, ByVal sampledelQty As String, ByVal UndelQty As String, ByVal UnDelQtyAmt As String, ByVal MOQty As String,
                              ByVal poQty As String, ByVal StockInOut As String, ByVal StockAmount As String, ByVal UserID As String)
        Dim StrSQL As String = Insert
        StrSQL = StrSQL.Replace("@item", item)
        StrSQL = StrSQL.Replace("@ItemDesc", ItemDesc)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@Unit", Unit)
        StrSQL = StrSQL.Replace("@reqQty", reqQty)
        StrSQL = StrSQL.Replace("@sampleQty", sampleQty)
        StrSQL = StrSQL.Replace("@deliveryQty", deliveryQty)
        StrSQL = StrSQL.Replace("@sampledelQty", sampledelQty)
        StrSQL = StrSQL.Replace("@UndelQty", UndelQty)
        StrSQL = StrSQL.Replace("@delAmt", UnDelQtyAmt)
        StrSQL = StrSQL.Replace("@moQty", MOQty)
        StrSQL = StrSQL.Replace("@poQty", poQty)
        StrSQL = StrSQL.Replace("@stockQty", StockInOut)
        StrSQL = StrSQL.Replace("@stockAmt", StockAmount)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '-- SelectShowDetail Where UserID 
    Private Shared SelectShowDetail As String = "select item,ItemDesc,Spec,UndelQty,delAmt,moQty,poQty,prQty,stockQty,stockAmt from TempSOUnDeliveryAmount where UserID='@UserID'" & Whr & "@Whr" &
       " group by item,ItemDesc,Spec,UndelQty,delAmt,moQty,poQty,prQty,stockQty,stockAmt  order by item"
    Public Shared Function ShowDetail(ByVal UserID As String, ByVal Whr As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectShowDetail
        StrSQL = StrSQL.Replace("@UserID", UserID)
        StrSQL = StrSQL.Replace("@Whr", Whr)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '##--delete from TempSOUnDeliveryAmount Where UserID
    Sub DeleteDetailTempSOUnDeliveryAmount(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSOUnDeliveryAmount where UserID='" & UserID & "')"
        SQL = SQL & " delete from TempSOUnDeliveryAmount where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

End Class
