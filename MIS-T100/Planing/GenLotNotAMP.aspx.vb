Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Public Class GenLotNotAMP
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            ucMoType.setObjectFull = "5102,5108,5192,5198"
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub


    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim SQL As String = "",
            WHR As String = ""

        WHR &= Conn_SQL.Where("TA006", tbItem)
        WHR &= Conn_SQL.Where("TB035", tbSpec)
        WHR &= Conn_SQL.Where("TA001", ucMoType.getObject)
        WHR &= Conn_SQL.Where("TA002", tbMoNo)
        WHR &= Conn_SQL.Where("TA009", ucDate.dateVal, ucDate.dateVal)

        SQL = " select TA001,TA002,TA009 from MOCTA left join INVMB on MB001=TA006 " &
              " where substring(TA057,1,1)='*' and TA013 = 'Y' and MB006 <> '2' and TA011 < 'Y' and MB022 <> 'N' " & WHR &
              " order by TA009 "
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        Dim txt As String = "No Data Update(ไม่มีข้อมูลที่ได้ปรับปรุง)"
        If dt.Rows.Count > 0 Then
            Dim strSQL As String = ""
            Dim fldWhr As Hashtable,
                fldVal As Hashtable

            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    fldWhr = New Hashtable
                    fldVal = New Hashtable

                    fldWhr.Add("TA001", Trim(.Item("TA001")))
                    fldWhr.Add("TA002", Trim(.Item("TA002")))
                    fldVal.Add("TA057 ", changeLotNumToText(Trim(.Item("TA009"))))
                    strSQL &= Conn_SQL.GetSQL("MOCTA", fldVal, fldWhr, "U")
                End With
            Next
            If strSQL <> "" Then
                Conn_SQL.Exec_Sql(strSQL, Conn_SQL.ERP_ConnectionString)
                txt = "Update " & dt.Rows.Count & " rows (มีรายการปรับปรุง " & dt.Rows.Count & " แถว)"
            End If
        End If
        lbUpdate.Text = txt
    End Sub

    Function changeLotNumToText(pDate As String) As String
        Dim txtYear As String = pDate.Substring(2, 2),
            txtMonth As String = pDate.Substring(4, 2),
            chrMonth As String = ""
        If IsNumeric(txtMonth) Then
            Select Case CInt(txtMonth)
                Case 1
                    chrMonth = "A"
                Case 2
                    chrMonth = "B"
                Case 3
                    chrMonth = "C"
                Case 4
                    chrMonth = "D"
                Case 5
                    chrMonth = "E"
                Case 6
                    chrMonth = "F"
                Case 7
                    chrMonth = "G"
                Case 8
                    chrMonth = "H"
                Case 9
                    chrMonth = "I"
                Case 10
                    chrMonth = "J"
                Case 11
                    chrMonth = "K"
                Case 12
                    chrMonth = "L"
            End Select
        End If

        Return txtYear & chrMonth
    End Function

End Class