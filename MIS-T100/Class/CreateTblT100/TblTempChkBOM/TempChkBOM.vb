Public Class TempChkBOM
    Dim DBCONN_SQL As New clsDBConnect
    Dim GetData As New GetData
    '''<reamrks> Velue </reamrks>'''
    Public Shared Whr As String = ""
    '##--สร้างตาราง TempChkBOM 
    Sub createTempChkBOM(UserID As String)
        Dim SQL As String = " if exists (select * from TempChkBOM where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempChkBOM where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempChkBOM"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Level nvarchar(255)  DEFAULT ''  ,"
        StrSQL &= "Seq nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "Item nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "Descr nvarchar(150)  DEFAULT ''  ,"
        StrSQL &= "Spec nvarchar(150)  DEFAULT ''  ,"
        StrSQL &= "QPA Decimal(16,2)  DEFAULT 0  ,"
        StrSQL &= "Unit nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "Sply nvarchar(20) DEFAULT '',"
        StrSQL &= "UserID nvarchar(50)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '##--สร้างตาราง TempSubChkBOM 
    Sub createTempSubChkBOM(UserID As String)
        Dim SQL As String = " if exists (select * from TempSubChkBOM where UserID='" & UserID & "' )"
        SQL = SQL & " delete from TempSubChkBOM where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempSubChkBOM"
        Dim StrSQL As String = " if not exists (select * from sysobjects where name='" & Temptable & "' )"
        StrSQL &= "CREATE TABLE " & Temptable & " ("
        StrSQL &= "Id Integer NOT NULL IDENTITY(1, 1) ,"
        StrSQL &= "Item nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "Descr nvarchar(150)  DEFAULT ''  ,"
        StrSQL &= "Spec nvarchar(150)  DEFAULT ''  ,"
        StrSQL &= "STQty nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "IsQty nvarchar(30) DEFAULT '',"
        StrSQL &= "MOQty nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "POQty nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "PRQty nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "SOQty nvarchar(30)  DEFAULT ''  ,"
        StrSQL &= "LeadTiem nvarchar(30)  DEFAULT '',"
        StrSQL &= "UserID nvarchar(50)  DEFAULT '',"
        StrSQL &= "PRIMARY KEY(Id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
    End Sub

    '--Page SubChkBOMPopUp
    '--Insert BOM Detail
    Private Const SubBom As String = "insert into TempSubChkBOM (Item,Descr,Spec,STQty,IsQty,MOQty,POQty,PRQty,SOQty,LeadTiem,UserID) values" &
        " ('@Item',N'@Descr',N'@Spec','@STQty','@IsQty','@MOQty','@POQty','@PRQty','@SOQty','@LeadTiem','@UserID')"
    Public Function InsertSubBom(ByVal Item As String, ByVal Desc As String, ByVal Spec As String,
                                 ByVal STQty As String, ByVal IsQty As String, ByVal MOQty As String,
                                 ByVal POQty As String, ByVal PRQty As String, ByVal SOQty As String,
                                 ByVal LeadTiem As String, ByVal UserID As String)
        Dim StrSQL As String = SubBom
        StrSQL = StrSQL.Replace("@Item", Item)
        StrSQL = StrSQL.Replace("@Descr", Desc)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@STQty", STQty)
        StrSQL = StrSQL.Replace("@IsQty", IsQty)
        StrSQL = StrSQL.Replace("@MOQty", MOQty)
        StrSQL = StrSQL.Replace("@POQty", POQty)
        StrSQL = StrSQL.Replace("@PRQty", PRQty)
        StrSQL = StrSQL.Replace("@SOQty", SOQty)
        StrSQL = StrSQL.Replace("@LeadTiem", LeadTiem)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--Page ChkBOMPopUp
    '--Insert BOM Detail
    Private Const itembom As String = "insert into TempChkBOM (Item,Descr,Spec,Unit,Sply,UserID) values" &
        " ('@Item',N'@Descr',N'@Spec','@Unit','@Sply','@UserID')"
    Public Function Insertitembom(ByVal Item As String, ByVal Desc As String, ByVal Spec As String,
                                       ByVal Unit As String, ByVal Sply As String, ByVal UserID As String)
        Dim StrSQL As String = itembom
        StrSQL = StrSQL.Replace("@Item", Item)
        StrSQL = StrSQL.Replace("@Descr", Desc)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@Unit", Unit)
        StrSQL = StrSQL.Replace("@Sply", Sply)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--Page ChkBOMPopUp
    '--Insert BOM Detail
    Private Const BOMDetail As String = "insert into TempChkBOM (Seq,Item,Descr,Spec,QPA,Unit,Sply,UserID) values" &
        " ('@Seq','@Item',N'@Descr',N'@Spec','@QPA','@Unit','@Sply','@UserID')"
    Public Function InsertBOMDetail(ByVal Seq As String, ByVal Item As String, ByVal Desc As String, ByVal Spec As String,
                                        ByVal QPA As String, ByVal Unit As String, ByVal Sply As String, ByVal UserID As String)
        Dim StrSQL As String = BOMDetail
        StrSQL = StrSQL.Replace("@Seq", Seq)
        StrSQL = StrSQL.Replace("@Item", Item)
        StrSQL = StrSQL.Replace("@Descr", Desc)
        StrSQL = StrSQL.Replace("@Spec", Spec)
        StrSQL = StrSQL.Replace("@QPA", QPA)
        StrSQL = StrSQL.Replace("@Unit", Unit)
        StrSQL = StrSQL.Replace("@Sply", Sply)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--Page ChkBOMPopUp
    '-- UpdateMixItemPopUp Where Level item UserID 
    Private Const UdMixItemPopUp As String = "update TempChkBOM set  Level ='@Level' where UserID='@UserID' and Item='@Item'"
    Public Function UpdateMixItemPopUp(ByVal MixItem As String, ByVal SearchItem As String, ByVal UserID As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = UdMixItemPopUp
        StrSQL = StrSQL.Replace("@Level", MixItem)
        StrSQL = StrSQL.Replace("@Item", SearchItem)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
        Return Nothing
    End Function

    '--Page ChkBOMPopUp
    '--Search Detail BOMPopUp
    Private Const SeltBomPopUp As String = "select len(Level)-len(replace(Level,'" & Chr(8) & "','')) as Level,Seq,Item,Descr,Spec,QPA,Unit,Sply,UserID from TempChkBOM where UserID='@UserID'"
    Public Function SearchBOMPopUp(ByVal UserID As String, ByRef tempDataTable As Data.DataTable)
        Dim StrSQL As String = SeltBomPopUp
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
    End Function

    '--Page SubChkBOMPopUp
    '--Search Detail SubBOMPopUp
    Private Const SeltSubBOMPopUp As String = "select Item,Descr,Spec,STQty,IsQty,MOQty,POQty,PRQty,SOQty from TempSubChkBOM where Item='@Item' and  UserID='@UserID'"
    Public Function SearchSubBOMPopUp(ByVal Item As String, ByVal UserID As String)
        Dim StrSQL As String = SeltSubBOMPopUp
        Dim tempDataTable As New DataTable
        StrSQL = StrSQL.Replace("@Item", Item)
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page ChkBOMPopUp
    '##--delete from TempSubChkBOM Where UserID
    Sub DeleteTempChkBOM(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempChkBOM where UserID='" & UserID & "')"
        SQL = SQL & " delete from TempChkBOM where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

    '--Page SubChkBOMPopUp 
    '##--delete from TempChkBOM Where UserID
    Sub DeleteTempSubChkBOM(ByVal UserID As String)
        Dim SQL As String = " if exists (select * from TempSubChkBOM where UserID='" & UserID & "')"
        SQL = SQL & " delete from TempSubChkBOM where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

End Class
