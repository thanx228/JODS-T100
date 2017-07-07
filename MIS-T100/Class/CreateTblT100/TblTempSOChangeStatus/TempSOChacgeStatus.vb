Public Class TempSOChacgeStatus
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '##--สร้างตาราง TempSOChacgeStatus
    Sub CreateTempSOChacgeStatus(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSOChacgeStatus where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempSOChacgeStatus where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempSOChacgeStatus"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "seq  Integer  DEFAULT '',"
        StrSQL &= "Section  nvarchar(255)  DEFAULT '',"
        StrSQL &= "CustID  nvarchar(255)  DEFAULT '',"
        StrSQL &= "CustName  nvarchar(255) DEFAULT '',"
        StrSQL &= "SalesOrder  nvarchar(255) DEFAULT '',"
        StrSQL &= "Ver  nvarchar(255)  DEFAULT '',"
        StrSQL &= "ChgDate   varchar (20)  DEFAULT '',"
        StrSQL &= "Item  nvarchar(255)  DEFAULT '',"
        StrSQL &= "Spec  nvarchar(255)  DEFAULT '',"
        StrSQL &= "ItemDes  nvarchar(255)  DEFAULT '',"
        StrSQL &= "QtyOld  nvarchar(255)  DEFAULT '',"
        StrSQL &= "QtyNew  nvarchar(255)  DEFAULT '',"
        StrSQL &= "PlanDelDateOld  nvarchar(255) DEFAULT '',"
        StrSQL &= "PlanDelDateNew  nvarchar(255)  DEFAULT '',"
        StrSQL &= "CustPONo  nvarchar(255)  DEFAULT '',"
        StrSQL &= "ForecastNo  nvarchar(255)  DEFAULT '',"
        StrSQL &= "Reason  nvarchar(255)  DEFAULT '',"
        StrSQL &= "Remark  nvarchar(255)  DEFAULT '',"
        StrSQL &= "Stus  nvarchar(255)  DEFAULT '',"
        StrSQL &= "UserID nvarchar(50)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)

    End Sub

    '##--สร้างตาราง TempSOChacgeStatus
    Sub DeleteSOChacgeStatus(ByVal SalesOrderChg As String, ByVal Version As String, ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSOChacgeStatus where UserID='" & UserID & "' and SalesOrder='" & SalesOrderChg & "'and Ver='" & Version & "' )"
        SQL = SQL & " delete from TempSOChacgeStatus where UserID='" & UserID & "' and SalesOrder='" & SalesOrderChg & "'and Ver='" & Version & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempCustoms
    Private Const InserCustoms As String = "insert into TempSOChacgeStatus (seq,Section,CustID,CustName,SalesOrder,Ver,ChgDate,Item,Spec,ItemDes,QtyOld,QtyNew,PlanDelDateOld,PlanDelDateNew,CustPONo,Reason,Remark,Stus,UserID) values" &
        "('@seq','@Section','@CustID','@CustName','@SalesOrder','@Ver','@ChgDate','@Item','@Spec','@ItemDes','@QtyOld','@QtyNew','@PlanDelDateOld','@PlanDelDateNew','@CustPONo','@Reason','@Remark','@Stus','@UserID')"
    Public Function InserSOChacgeStatus(ByVal seq As String, ByVal Section As String, ByVal CustID As String, ByVal CustName As String, ByVal SalesOrder As String,
                                        ByVal Version As String, ByVal ChgDate As String, ByVal Item As String, ByVal Spec As String, ByVal ItemDes As String,
                                        ByVal QtyOld As String, ByVal QtyNew As String, ByVal PlanDelDateOld As String, ByVal PlanDelDateNew As String, ByVal CustPONo As String,
                                        ByVal Reason As String, ByVal Remark As String, ByVal Status As String, ByVal UserID As String)
        Dim StrSQL As String = InserCustoms
        StrSQL = StrSQL.Replace("@seq", seq)
        StrSQL = StrSQL.Replace("@Section", Section)
        StrSQL = StrSQL.Replace("@CustID", CustID)
        StrSQL = StrSQL.Replace("@CustName", CustName)
        StrSQL = StrSQL.Replace("@SalesOrder", SalesOrder)
        StrSQL = StrSQL.Replace("@Ver", Version)
        StrSQL = StrSQL.Replace("@ChgDate", ChgDate)
        StrSQL = StrSQL.Replace("@Item", Item)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@ItemDes", ItemDes)
        StrSQL = StrSQL.Replace("@QtyOld", QtyOld)
        StrSQL = StrSQL.Replace("@QtyNew", QtyNew)
        StrSQL = StrSQL.Replace("@PlanDelDateOld", PlanDelDateOld)
        StrSQL = StrSQL.Replace("@PlanDelDateNew", PlanDelDateNew)
        StrSQL = StrSQL.Replace("@CustPONo", CustPONo)
        StrSQL = StrSQL.Replace("@Reason", Reason)
        StrSQL = StrSQL.Replace("@Remark", Remark)
        StrSQL = StrSQL.Replace("@Stus", Status)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--SelectTempSOChacgeStus 
    Private Const TempSOChacgeStus As String = "select seq, Section, CustID, CustName,SalesOrder,Ver,ChgDate,Item,Spec,ItemDes,QtyOld,QtyNew,PlanDelDateOld,PlanDelDateNew,CustPONo,ForecastNo,Reason,Remark,Stus from TempSOChacgeStatus where UserID='@UserID'"
    Public Function SelectTempSOChacgeStus(ByVal UserID As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = TempSOChacgeStus
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function


End Class
