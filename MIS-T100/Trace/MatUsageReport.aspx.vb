Public Class MatUsageReport
    Inherits System.Web.UI.Page

    Dim SFAA As New SFAA
    Dim SFBA As New SFBA
    Dim IMAAL As New IMAAL
    Dim clsconnect As New clsDBConnect

    Dim rec_prefixconst As String = "CJ"      ' Custom Journal JinPao
    Dim tblCollection As String = "T100ProductionMatUsage"    'Table Collection Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Public Function getMODetail(ByVal motype As String, ByVal DocNum As String) As String

        Dim fullmo As String = motype + "-" + DocNum
        Dim disp As String = "<table>"
        Dim sfaads As DataSet
        Dim rownum As Integer = 0
        Dim monum As String = "", itemid As String = "", moqty As String = "", unit As String = "", itemdesc As String = "", spec As String = ""
        Dim sql As String = "SELECT " & SFAA.DocNo & " AS MONumber," & SFAA.ProductItem & " AS ItemNo," & SFAA.ProductionQty & " AS MOQty," &
                            "" & SFAA.Unit & " AS Unit," & IMAAL.ProductName & " AS ItemDesc," & IMAAL.Specifaction & " AS Spec " &
                            "FROM " & SFAA.tblMO & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " " &
                            "ON " & SFAA.ProductItem & "=" & IMAAL.ProductItem & " " &
                            "WHERE " & SFAA.ProgromCode & "='3' AND " & IMAAL.ent & "='3' AND " & SFAA.DocNo & " ='" & fullmo & "' AND (" & SFAA.Status & "='F' OR " & SFAA.Status & "='A' OR " & SFAA.Status & "='Y') "
        sfaads = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If sfaads.Tables("DATASET").Rows.Count = 0 Then
        Else
            For i = 0 To sfaads.Tables("DATASET").Rows.Count - 1
                monum = sfaads.Tables("DATASET")(i)("MONumber").ToString
                itemid = sfaads.Tables("DATASET")(i)("ItemNo").ToString
                moqty = sfaads.Tables("DATASET")(i)("MOQty").ToString
                unit = sfaads.Tables("DATASET")(i)("Unit").ToString
                itemdesc = sfaads.Tables("DATASET")(i)("ItemDesc").ToString
                spec = sfaads.Tables("DATASET")(i)("Spec").ToString
                disp = disp + "<tr ><td>MO Number : " & monum & "</td></tr><tr ><td>ItemID : " & itemid & "</td></tr><tr ><td>Description : " & itemdesc & "</td></tr><tr ><td>Spec : " & spec & "</td></tr>" &
                       "<tr ><td>MO Qty : " & moqty & "</td></tr><tr ><td>Unit : " & unit & "</td></tr><tr ><td>MO Batch Number : </td></tr>"
            Next
            disp = disp + "</table>"
        End If

        Return disp

    End Function

    Public Function getItemBOMList(ByVal motype As String, ByVal DocNum As String) As String

        Dim fullmo As String = motype + "-" + DocNum
        Dim disp As String = ""
        Dim sfbads As DataSet
        Dim rownum As Integer = 0
        Dim monum As String = "", BOMitemid As String = "", StandardQPA As String = "", unit As String = "", BOMitemdesc As String = "", spec As String = ""
        Dim sql As String = "SELECT " & SFBA.BOMitem & " AS BOMItem," & IMAAL.ProductName & " AS BOMItemDescription," &
                             "" & IMAAL.Specifaction & " AS Spec," & SFBA.StandardIssuanceQuantity & " AS StandardQPA," &
                             "" & SFBA.Unit & " AS Unit " &
                             "FROM " & SFBA.tblManufactureOrder_Body & " " &
                             "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFBA.BOMitem & "=" & IMAAL.ProductItem & " " &
                             "LEFT JOIN " & SFAA.tblMO & " ON " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
                             "WHERE " & SFBA.MODocNo & "='" & fullmo & "' and " & SFBA.ent & "='3' AND " & IMAAL.ent & "='3' AND " & SFAA.ProgromCode & "='3'" &
                             "AND (" & SFAA.Status & "='Y' OR " & SFAA.Status & "='F' OR " & SFAA.Status & "='A') ORDER BY " & SFBA.ItemSequence & " ASC"
        sfbads = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If sfbads.Tables("DATASET").Rows.Count = 0 Then
        Else
            disp = "<br /><table bgcolor=FFFFFF border=1>"
            disp = disp + "<tr height=25 align=center><th>&nbsp;BOM Item&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Spec&nbsp;</th>" &
                       "<th>&nbsp;Standard QPA&nbsp;</th><th>&nbsp;Unit&nbsp;</th><th>&nbsp;MatUsageQty&nbsp;</th><th>&nbsp;LotName&nbsp;</th></tr>"
            For i = 0 To sfbads.Tables("DATASET").Rows.Count - 1
                BOMitemid = sfbads.Tables("DATASET")(i)("BOMItem").ToString
                BOMitemdesc = sfbads.Tables("DATASET")(i)("BOMItemDescription").ToString
                spec = sfbads.Tables("DATASET")(i)("Spec").ToString
                StandardQPA = sfbads.Tables("DATASET")(i)("StandardQPA").ToString
                unit = sfbads.Tables("DATASET")(i)("Unit").ToString
                disp = disp + "<tr height=25><td>&nbsp;" & BOMitemid & "&nbsp;</td><td>&nbsp;" & BOMitemdesc & "&nbsp;</td><td>&nbsp;" & spec & "&nbsp;</td>" &
                       "<td align=right>&nbsp;" & StandardQPA & "&nbsp;</td><td align=center>&nbsp;" & unit & "&nbsp;</td><td align=center><input type=text name=matuseqty maxlength=10 size=10 value=" & checkIfOldrecordExist_mat(motype, DocNum, BOMitemid) & "></td><td align=center><input type=text name=matlot maxlength=30 size=30 value=" & checkIfOldrecordExist_lot(motype, DocNum, BOMitemid) & "></td></tr>"
            Next
            disp = disp + "<tr><td colspan=7 align=center><br /><br /><input id=submitter type=submit name=Savedata value=Save /><br /><br /></td></tr></table>"
        End If

        Return disp

    End Function

    Public Function isExist(ByVal motype As String, ByVal DocNum As String) As Boolean

        Dim result As Boolean = False
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim sql As String = "SELECT Recid FROM " & tblCollection & " WHERE moType='" & motype & "' AND moNo='" & DocNum & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.MIS2)
        rowcount = ds.Tables("DATASET").Rows.Count
        clsconnect.Close(clsconnect.MIS2)
        If rowcount <> 0 Then
            result = True
        Else
        End If

        Return result

    End Function

    Public Function UpdateData(ByVal motype As String, ByVal DocNum As String, ByVal matQty() As String, ByVal lotName() As String) As Boolean

        Dim result As Boolean = False
        Dim fullmo As String = motype + "-" + DocNum
        Dim sfbads As DataSet
        Dim journalnum As String = ""
        Dim BOMItem As String = ""
        Dim Desc As String = ""
        Dim Spec As String = ""
        Dim MOQty As String = ""
        Dim DatetimeNow = System.DateTime.Now()
        Dim itemseq As String = ""
        Dim matQqy_data As String = ""
        Dim lotname_data As String = ""

        Dim sql As String = "SELECT " & SFBA.BOMitem & " AS BOMItem," & IMAAL.ProductName & " AS BOMItemDescription," &
                             "" & IMAAL.Specifaction & " AS Spec," & SFBA.StandardIssuanceQuantity & " AS StandardQPA," &
                             "" & SFBA.Unit & " AS Unit," & SFBA.ItemSequence & " AS itemsequence, " & SFAA.ProductionQty & " AS MOQty " &
                             "FROM " & SFBA.tblManufactureOrder_Body & " " &
                             "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFBA.BOMitem & "=" & IMAAL.ProductItem & " " &
                             "LEFT JOIN " & SFAA.tblMO & " ON " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
                             "WHERE " & SFBA.MODocNo & "='" & fullmo & "' and " & SFBA.ent & "='3' AND " & IMAAL.ent & "='3' AND " & SFAA.ProgromCode & "='3'" &
                             "AND (" & SFAA.Status & "='Y' OR " & SFAA.Status & "='F' OR " & SFAA.Status & "='A') ORDER BY " & SFBA.ItemSequence & " ASC"
        sfbads = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If sfbads.Tables("DATASET").Rows.Count = 0 Then
        Else
            For i = 0 To sfbads.Tables("DATASET").Rows.Count - 1
                itemseq = sfbads.Tables("DATASET")(i)("itemsequence").ToString()
                BOMItem = sfbads.Tables("DATASET")(i)("BOMItem").ToString()
                MOQty = sfbads.Tables("DATASET")(i)("MOQty").ToString()
                journalnum = rec_prefixconst + motype + DocNum + itemseq
                matQqy_data = matQty(i)
                lotname_data = lotName(i)
                Dim Updsql As String = "UPDATE " & tblCollection & " SET matQty='" & matQqy_data & "', lot='" & lotname_data & "' WHERE item='" & BOMItem & "' AND moType='" & motype & "' AND moNo='" & DocNum & "'"
                clsconnect.QueryExecuteNonQuery(Updsql, clsconnect.MIS2)
            Next
        End If

        Return True

    End Function

    Public Function WriteData(ByVal motype As String, ByVal DocNum As String, ByVal matQty() As String, ByVal lotName() As String) As Boolean

        Dim result As Boolean = False
        Dim fullmo As String = motype + "-" + DocNum
        Dim sfbads As DataSet
        Dim journalnum As String = ""
        Dim BOMItem As String = ""
        Dim Desc As String = ""
        Dim Spec As String = ""
        Dim MOQty As String = ""
        Dim DatetimeNow = System.DateTime.Now()
        Dim itemseq As String = ""
        Dim matQqy_data As String = ""
        Dim lotname_data As String = ""

        Dim sql As String = "SELECT " & SFBA.BOMitem & " AS BOMItem," & IMAAL.ProductName & " AS BOMItemDescription," &
                             "" & IMAAL.Specifaction & " AS Spec," & SFBA.StandardIssuanceQuantity & " AS StandardQPA," &
                             "" & SFBA.Unit & " AS Unit," & SFBA.ItemSequence & " AS itemsequence, " & SFAA.ProductionQty & " AS MOQty " &
                             "FROM " & SFBA.tblManufactureOrder_Body & " " &
                             "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFBA.BOMitem & "=" & IMAAL.ProductItem & " " &
                             "LEFT JOIN " & SFAA.tblMO & " ON " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
                             "WHERE " & SFBA.MODocNo & "='" & fullmo & "' and " & SFBA.ent & "='3' AND " & IMAAL.ent & "='3' AND " & SFAA.ProgromCode & "='3'" &
                             "AND (" & SFAA.Status & "='Y' OR " & SFAA.Status & "='F' OR " & SFAA.Status & "='A') ORDER BY " & SFBA.ItemSequence & " ASC"
        sfbads = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If sfbads.Tables("DATASET").Rows.Count = 0 Then
        Else
            For i = 0 To sfbads.Tables("DATASET").Rows.Count - 1
                itemseq = sfbads.Tables("DATASET")(i)("itemsequence").ToString()
                BOMItem = sfbads.Tables("DATASET")(i)("BOMItem").ToString()
                MOQty = sfbads.Tables("DATASET")(i)("MOQty").ToString()
                journalnum = rec_prefixconst + motype + DocNum + itemseq
                matQqy_data = matQty(i)
                lotname_data = lotName(i)
                'Dim Inssql As String = "INSERT INTO " & tblCollection & " (Recid,moType,moNo,item,lot,moQty,qtyPer,CreateBy,CreateDate,docStart,matQty) VALUES('" & journalnum & "','" & motype & "','" & DocNum & "','" & BOMItem & "','" & lotname_data & "'," & MOQty & "," & matQqy_data & ",'" & Session("UserId") & "','" & DatetimeNow.ToString("yyyyMMdd hhmmss") & "',0," & matQqy_data & ")"
                Dim Inssql As String = "INSERT INTO " & tblCollection & " (Recid,moType,moNo,item,lot,moQty,qtyPer,CreateBy,CreateDate,docStart,matQty) VALUES('" & journalnum & "','" & motype & "','" & DocNum & "','" & BOMItem & "','" & lotname_data & "'," & MOQty & "," & matQqy_data & ",'TEST','" & DatetimeNow.ToString("yyyyMMdd hhmmss") & "',0," & matQqy_data & ")"
                clsconnect.QueryExecuteNonQuery(Inssql, clsconnect.MIS2)
            Next
        End If

        Return True

    End Function

    Private Function checkIfOldrecordExist_mat(ByVal motype As String, ByVal mono As String, ByVal BOMItem As String) As String

        Dim result As String = ""
        Dim ds As DataSet
        Dim sql As String = "SELECT matQty FROM " & tblCollection & " WHERE moType='" & motype & "' AND moNo='" & mono & "' AND item='" & BOMItem & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.MIS2)
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            result = ds.Tables("DATASET")(0)("matQty")
        Else
            result = ""
        End If
        Return result

    End Function

    Private Function checkIfOldrecordExist_lot(ByVal motype As String, ByVal mono As String, ByVal BOMItem As String) As String

        Dim result As String = ""
        Dim ds As DataSet
        Dim sql As String = "SELECT lot FROM " & tblCollection & " WHERE moType='" & motype & "' AND moNo='" & mono & "' AND item='" & BOMItem & "'"
        ds = clsconnect.QueryDataSet(sql, clsconnect.MIS2)
        If ds.Tables("DATASET").Rows.Count <> 0 Then
            result = ds.Tables("DATASET")(0)("lot")
        Else
            result = ""
        End If

        Return result

    End Function

    Public Function ShowReport(ByVal motype As String, ByVal mono As String, Optional ByVal lotname As String = "", Optional ByVal fromdate As String = "", Optional ByVal todate As String = "") As String

        Dim disp As String = ""
        Dim ds As DataSet
        '
        Dim recno As String = ""
        Dim showMOtype As String = ""
        Dim showMOno As String = ""
        Dim showLotname As String = ""
        Dim showMOQty As String = ""
        Dim showDate As String = ""
        Dim creator As String = ""


        Dim sql As String = "SELECT Recid as DN, moType,moNo,lot AS LotName,moQty,CreateDate,CreateBy FROM T100ProductionMatUsage"
        ds = clsconnect.QueryDataSet(sql, clsconnect.MIS2)
        If ds.Tables("DATASET").Rows.Count = 0 Then
        Else
            disp = "<br /><table bgcolor=FFFFFF border=1>"
            disp = disp + "<tr height=25 align=center><th>&nbsp;DocNo&nbsp;</th><th>&nbsp;MOType&nbsp;</th><th>&nbsp;MONumber&nbsp;</th>" &
                           "<th>&nbsp;LotName&nbsp;</th><th>&nbsp;CreateBy&nbsp;</th><th>&nbsp;CreateDate&nbsp;</th></tr>"
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                recno = ds.Tables("DATASET")(i)("DN").ToString()
                showMOtype = ds.Tables("DATASET")(i)("moType").ToString()
                showMOno = ds.Tables("DATASET")(i)("moNo").ToString()
                showLotname = ds.Tables("DATASET")(i)("LotName").ToString()
                showMOQty = ds.Tables("DATASET")(i)("moQty").ToString()
                creator = ds.Tables("DATASET")(i)("CreateBy").ToString()
                showDate = ds.Tables("DATASET")(i)("CreateDate").ToString()
                disp = disp + "<tr height=25><td align=left>&nbsp;" & recno & "&nbsp;</td><td align=center>&nbsp;" & showMOtype & "&nbsp;</td><td align=center>&nbsp;" & showMOno & "&nbsp;</td>" &
                       "<td align=center>&nbsp;" & showLotname & "&nbsp;</td><td align=center>&nbsp;" & creator & "&nbsp;</td><td align=center>&nbsp;" & showDate & "&nbsp;</td></tr>"
            Next
            disp = disp + "</table>"

        End If

        Return disp

    End Function

End Class