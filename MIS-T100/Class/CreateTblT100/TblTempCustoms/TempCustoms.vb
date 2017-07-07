Public Class TempCustoms
    Dim DBCONN_SQL As New clsDBConnect

    '--สร้างตารางTempCustomsโดยมีเงื่อนไขเมื่อ User Login เข้าใช้งานโปรแกรมๆจะทำงานลบข้อมูลในตารางโดยอ้างอิงจาก User ทิ้ง
    Sub CreateTempCustoms(ByVal UserID As String)
        Dim StrSQL As String = " if exists (select * from TempCustoms where UserID='" & UserID & "' )"
        StrSQL = StrSQL & " delete from TempCustoms where UserID='" & UserID & "'"
        DBCONN_SQL.QueryExecuteNonQuery(StrSQL, DBCONN_SQL.MIS2)
        Dim Temptable As String = "TempCustoms"
        Dim SQL As String = " If Not exists (Select * from sysobjects where name='" & Temptable & "' )"
        SQL &= "CREATE TABLE " & Temptable & " ("
        SQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        SQL &= "seq  Integer  DEFAULT '',"
        SQL &= "Packing  Decimal(16,2)  NULL  ,"
        SQL &= "Weht Decimal(16,2)  NULL  ,"
        SQL &= "InvQty Decimal(16,2)  NULL  ,"
        SQL &= "InvAmtTax Decimal(16,2)  NULL  ,"
        SQL &= "InvNo nvarchar(50)  DEFAULT '',"
        SQL &= "PO nvarchar(50)  DEFAULT '',"
        SQL &= "Remark nvarchar(250)  DEFAULT '',"
        SQL &= "UserID nvarchar(50)  DEFAULT '',"
        SQL &= "PRIMARY KEY(id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

    '--ลบข้อมูลในตาราง TempCustoms โดยอ้างอิงจาก UserID ที่มีการ login เข้ามาที่ฟอร์ม Customs 
    Private Const UserClear As String = "delete from TempCustoms where UserID=@UserID"
    Public Function ClearTempCustoms(ByVal UserID As String)
        Dim Oral As String = UserClear
        Oral = Oral.Replace("@UserID", "'" & UserID & "'")
        DBCONN_SQL.QueryExecuteNonQuery(Oral, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    '--บันทึกข้อมุลลงตาราง TempCustoms
    Private Const InserCustoms As String = "insert into TempCustoms (seq,Packing,Weight,InvQty,InvAmtTax,InvNo,PO,Remark,UserID) values" &
        "('@seq','@Packing','@Weight','@InvQty','@InvAmtTax',SUBSTRING('@InvNo',5,18),'@PO',N'@Remark','@UserID')"
    Public Function InserDataCustoms(ByVal seq As String, ByVal Packing As String, ByVal Weight As String, ByVal InvoiceQty As String, ByVal InvoiceAmtTax As String, ByVal InvoiceNo As String, ByVal PO As String, ByVal ItemNameRemarkType As String, ByVal UserID As String)
        Dim SQL As String = InserCustoms
        SQL = SQL.Replace("@seq", seq)
        SQL = SQL.Replace("@Packing", Packing)
        SQL = SQL.Replace("@Weight", Weight)
        SQL = SQL.Replace("@InvQty", InvoiceQty)
        SQL = SQL.Replace("@InvAmtTax", InvoiceAmtTax)
        SQL = SQL.Replace("@InvNo", InvoiceNo)
        SQL = SQL.Replace("@PO", PO)
        SQL = SQL.Replace("@Remark", ItemNameRemarkType)
        SQL = SQL.Replace("@UserID", UserID)
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
        Return Nothing
    End Function

    Sub CreateTempPrintCustoms(ByVal UserID As String)
        Dim Temptable As String = "TempPrintCustoms"
        Dim SQL As String = " If Not exists (Select * from sysobjects where name='" & Temptable & "' )"
        SQL &= "CREATE TABLE " & Temptable & " ("
        SQL &= "id Integer NOT NULL IDENTITY(1, 1) ,"
        SQL &= "seq  Integer  DEFAULT '',"
        SQL &= "Packing Decimal(16,2)  NULL  ,"
        SQL &= "Weht Decimal(16,2)  NULL  ,"
        SQL &= "InvQty Decimal(16,2)  NULL  ,"
        SQL &= "InvAmtTax Decimal(16,2)  NULL  ,"
        SQL &= "InvNo nvarchar(50)  DEFAULT '',"
        SQL &= "PO nvarchar(50)  DEFAULT '',"
        SQL &= "Remark nvarchar(250)  DEFAULT '',"
        SQL &= "PrintDt nvarchar(250)  DEFAULT '',"
        SQL &= "UserID nvarchar(50)  DEFAULT '',"
        SQL &= "PRIMARY KEY(id)) ;"
        DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
    End Sub

    '--บันทึกข้อมุลลงตาราง TempPrintCustoms
    Private Const SelectPrintCustoms As String = "select seq,Packing,Weht,InvQty,InvAmtTax,InvNo,PO,Remark,GETDATE() PrintDt,UserID from TempCustoms where UserID='@UserID'"
    Private Const InserPrintCustoms As String = "insert into TempPrintCustoms (seq,Packing,Weht,InvQty,InvAmtTax,InvNo,PO,Remark,PrintDt,UserID) values" &
        "('@seq','@Packing','@Weht','@InvQty','@InvAmtTax',SUBSTRING('@InvNo',5,18),'@PO',N'@Remark','@PrintDt','@UserID')"
    Public Function InserDataPrintCustoms(ByVal UserID As String)
        Dim dt As New DataTable
        Dim StrSQL As String = SelectPrintCustoms
        StrSQL = StrSQL.Replace("@UserID", UserID)
        GetData.Get_DataReaderSQL(StrSQL, clsDBConnect.strMIS2ConnectionString, dt)
        Dim Seq As String = "", Pack As String = "", Wgh As String = "", InvQty As String = "",
            InvQtyAmt As String = "", InvNo As String = "", PO As String = "", Remark As String = "",
            PrintDt As String = "", ID As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows.Count > 0 Then
                With dt.Rows(i)
                    Seq = .Item("seq")
                    Pack = .Item("Packing")
                    Wgh = .Item("Weht")
                    InvQty = .Item("InvQty")
                    InvQtyAmt = .Item("InvAmtTax")
                    InvNo = .Item("InvNo")
                    PO = .Item("PO")
                    Remark = .Item("Remark")
                    PrintDt = .Item("PrintDt")
                    ID = .Item("UserID")

                    Dim SQL As String = InserPrintCustoms
                    SQL = SQL.Replace("@seq", Seq)
                    SQL = SQL.Replace("@Packing", Pack)
                    SQL = SQL.Replace("@Weht", Wgh)
                    SQL = SQL.Replace("@InvQty", InvQty)
                    SQL = SQL.Replace("@InvAmtTax", InvQtyAmt)
                    SQL = SQL.Replace("@InvNo", InvNo)
                    SQL = SQL.Replace("@PO", PO)
                    SQL = SQL.Replace("@Remark", Remark)
                    SQL = SQL.Replace("@PrintDt", PrintDt)
                    SQL = SQL.Replace("@UserID", ID)
                    DBCONN_SQL.QueryExecuteNonQuery(SQL, DBCONN_SQL.MIS2)
                End With
            End If
        Next
        Return Nothing
    End Function

End Class
