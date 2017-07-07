Public Class TempSLQuotation
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '##--สร้างตาราง TempSLQuotion
    Sub createTempSLQuotion(UserID As String)
        Dim SQL As String = " if exists (select * from TempSLQuotion where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempSLQuotion where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempSLQuotion"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "SaleOrderNo nvarchar(30) DEFAULT '',"
        StrSQL &= "VerNo nvarchar(20) DEFAULT '',"
        StrSQL &= "ItemNo nvarchar(30) DEFAULT '',"
        StrSQL &= "Spec nvarchar(250) DEFAULT '',"
        StrSQL &= "CustID nvarchar(20) DEFAULT '',"
        StrSQL &= "CustName nvarchar(250)  DEFAULT '',"
        StrSQL &= "OrderQty nvarchar(250)  DEFAULT '',"
        StrSQL &= "SampleQty nvarchar(250)  DEFAULT '',"
        StrSQL &= "SOPrice nvarchar(250)  DEFAULT '',"
        StrSQL &= "UnitPrice nvarchar(250)  DEFAULT '',"
        StrSQL &= "DeffPrice nvarchar(250)  DEFAULT '',"
        StrSQL &= "DocDate nvarchar(20) DEFAULT '',"
        StrSQL &= "UserID nvarchar(10)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempSLQuotion
    Private Const InserSOQuotion As String = "insert into TempSLQuotion (SaleOrderNo,VerNo,ItemNo,Spec,CustID,CustName,OrderQty,SampleQty,SOPrice,UnitPrice,DeffPrice,DocDate,UserID) values" &
        "('@SaleOrderNo','@VerNo','@ItemNo','@Spec','@CustID','@CustName','@OrderQty','@SampleQty','@SOPrice','@UnitPrice','@DeffPrice','@DocDate','@UserID')"
    Public Function InserSOQuotionDetail(ByVal SaleOrderNo As String, ByVal Version As String, ByVal ItemNo As String, ByVal Spec As String,
                                        ByVal CustID As String, ByVal CustName As String, ByVal OrderQty As String, ByVal SampleQty As String,
                                        ByVal SOPrice As String, ByVal UnitPrice As String, ByVal DeffPrice As String, ByVal DocumentDate As String, ByVal UserID As String)
        Dim StrSQL As String = InserSOQuotion
        StrSQL = StrSQL.Replace("@SaleOrderNo", SaleOrderNo)
        StrSQL = StrSQL.Replace("@VerNo", Version)
        StrSQL = StrSQL.Replace("@ItemNo", ItemNo)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@CustID", CustID)
        StrSQL = StrSQL.Replace("@CustName", CustName)
        StrSQL = StrSQL.Replace("@OrderQty", OrderQty)
        StrSQL = StrSQL.Replace("@SampleQty", SampleQty)
        StrSQL = StrSQL.Replace("@SOPrice", SOPrice)
        StrSQL = StrSQL.Replace("@UnitPrice", UnitPrice)
        StrSQL = StrSQL.Replace("@DeffPrice", DeffPrice)
        StrSQL = StrSQL.Replace("@DocDate", DocumentDate)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--SelectTempSLQuotion
    Private Const SelectSOQuotion As String = "Select SaleOrderNo,VerNo,ItemNo,Spec,CustID,CustName,OrderQty,SampleQty,SOPrice,UnitPrice,DeffPrice,DocDate from TempSLQuotion where UserID='@UserID'@Whr"
    Public Function SelectTempSLQuotion(ByVal Whr As String, ByVal UserID As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SelectSOQuotion
        StrSQL = StrSQL.Replace("@Whr", Whr)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '##--delete from TempSLQuotion Where UserID
    Sub DeleteDetailTempSLQuotion(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSLQuotion where UserID='" & UserID & "')"
        SQL = SQL & " delete from TempSLQuotion where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub
End Class
