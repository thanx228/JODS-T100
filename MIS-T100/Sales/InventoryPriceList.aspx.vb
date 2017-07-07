Public Class InventoryPriceList
    Inherits System.Web.UI.Page

    Dim IMAAL As New IMAAL
    Dim INAG As New INAG
    Dim clsconnect As New clsDBConnect
    'DBF Define

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function ShowSalesWHtype(Optional ByVal code As String = "") As String

        Dim disp As String = ""
        Dim whcode() As String = {"0", "2101", "2102", "2103"}
        Dim whname() As String = {"All Warehouse", "2101 FG Metal Part", "2102 FG BOI 1", "2103 FG BOI 2"}

        For i = 0 To whcode.Length - 1
            If code = "" Then
                disp = disp + "<option value=" & whcode(i) & ">" & whname(i) & "</option>"
            Else
            End If
        Next
        Return disp

    End Function

    Public Function getDesignateWHReport(ByVal wha As String, Optional ByVal item As String = "", Optional ByVal spec As String = "") As String


        Dim disp As String = ""
        Dim ds As DataSet
        Dim schwhstr As String = ""
        Dim schitemstr As String = ""
        Dim schspecstr As String = ""
        'ShowFields
        Dim Sitem As String = ""
        Dim Sdescription As String = ""
        Dim Sspecification As String = ""
        Dim Swarehouseid As String = ""
        Dim Swarehousename As String = ""
        Dim Slotname As String = ""
        Dim Sstockqty As String = ""
        Dim SpriceUnit As Decimal = 0
        Dim Scurrency As String = ""
        Dim Svalue As Decimal = 0
        Dim result_curr_price As String = ""
        Dim ExchRateUSD As Decimal = Decimal.Parse(getExchRate("USD"))
        Dim ExchRateEUR As Decimal = Decimal.Parse(getExchRate("EUR"))
        Dim ExchRateJPY As Decimal = Decimal.Parse(getExchRate("JPY"))

        If wha = "0" Then
            schwhstr = " AND inag004 in('2101','2102','2103')"
        Else
            schwhstr = " AND inag004 ='" & wha & "'"
        End If
        If item <> "" Then
            schitemstr = " AND inag001 LIKE '%" & item & "%'"
        End If
        If schspecstr <> "" Then
            schspecstr = " AND imaal004 LIKE '%" & spec & "%'"
        End If

        Dim sql As String = "SELECT inag001 AS item,imaal003 AS itemdescription,imaal004 AS itemspecification,
                             inag004 as warehouseid, inayl003 AS warehousename, inag006 AS LotName, inag009 as stockqty
                             FROM inag_t
                             LEFT JOIN imaal_t ON inag001=imaal001
                             LEFT JOIN inayl_t ON inag004=inayl001
                             WHERE inagent='3'
                             AND imaalent='3' AND imaal002='en_US'
                             AND inaylent='3' AND inayl002='en_US'" + schwhstr + schitemstr + schspecstr
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count = 0 Then
        Else
            disp = "<br /><table bgcolor=FFFFFF border=1>"
            disp = disp + "<tr height=25 align=center><th>&nbsp;Item&nbsp;</th><th>&nbsp;Description&nbsp;</th><th>&nbsp;Spec&nbsp;</th>" &
                       "<th>&nbsp;Warehouse&nbsp;</th><th>&nbsp;WarehouseName&nbsp;</th><th>&nbsp;LotName&nbsp;</th><th>&nbsp;StokQty&nbsp;</th><th>&nbsp;ItemPrice&nbsp;</th><th>&nbsp;Currency&nbsp;</th><th>&nbsp;ValuesAmount&nbsp;</th></tr>"
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                Sitem = ds.Tables("DATASET")(i)("item").ToString
                Sdescription = ds.Tables("DATASET")(i)("itemdescription").ToString
                Sspecification = ds.Tables("DATASET")(i)("itemspecification").ToString
                Swarehouseid = ds.Tables("DATASET")(i)("warehouseid").ToString
                Swarehousename = ds.Tables("DATASET")(i)("warehousename").ToString
                Slotname = ds.Tables("DATASET")(i)("LotName").ToString
                Sstockqty = ds.Tables("DATASET")(i)("stockqty").ToString
                result_curr_price = getItemPriceCurrency(Sitem)
                SpriceUnit = extractPrice(result_curr_price)
                Scurrency = extractCurrency(result_curr_price)
                If Scurrency = "USD" Then
                    Svalue = Decimal.Parse(Sstockqty) * ((SpriceUnit) * Decimal.Parse(ExchRateUSD))
                ElseIf Scurrency = "EUR" Then
                    Svalue = Decimal.Parse(Sstockqty) * ((SpriceUnit) * Decimal.Parse(ExchRateEUR))
                ElseIf Scurrency = "JPY" Then
                    Svalue = Decimal.Parse(Sstockqty) * ((SpriceUnit) * Decimal.Parse(ExchRateJPY))
                Else
                    Svalue = Decimal.Parse(Sstockqty) * SpriceUnit
                End If
                disp = disp + "<tr height=25><td>&nbsp;" & Sitem & "&nbsp;</td><td>&nbsp;" & Sdescription & "&nbsp;</td><td>&nbsp;" & Sspecification & "&nbsp;</td>" &
                       "<td align=right>&nbsp;" & Swarehouseid & "&nbsp;</td><td align=left>&nbsp;" & Swarehousename & "&nbsp;</td><td align=left>&nbsp;" & Slotname & "&nbsp;</td><td align=right>&nbsp;" & Sstockqty & "&nbsp;</td>" &
                       "<td align=right>&nbsp;" & SpriceUnit & "&nbsp;</td>   <td align=center>&nbsp;" & Scurrency & "&nbsp;</td>" &
                       "<td align=right>&nbsp;" & Svalue & "&nbsp;</td></tr>"
            Next
            disp = disp + "</table>"
        End If

        Return disp

    End Function

    Public Function extractPrice(ByVal data As String) As Decimal

        Dim p As Decimal
        Dim str() As String = data.Split("|")
        p = Decimal.Parse(str(1))
        Return p

    End Function

    Public Function extractCurrency(ByVal data As String) As String

        Dim c As String
        Dim str() As String = data.Split("|")
        c = str(0)
        Return c

    End Function

    Public Function getItemPriceCurrency(ByVal item As String) As String

        Dim disp As String = ""
        Dim ds As DataSet
        Dim Seq As String = ""
        Dim PVDoc As String = ""
        Dim itemid As String = ""
        Dim curr As String = ""
        Dim PRE As String = ""
        Dim defprice As String = ""
        Dim finalprice As String = ""
        Dim cmb_curr_price As String = ""

        Dim sql As String = "Select xmduseq As Seq,xmdudocno As PVDoc,xmdu002 As ItemID,xmdt015 As PricevalidStart,xmdt016 As PricevalidExpired,xmdt005 As Currency,
                             xmdu009 AS pricerateEnable,xmdu011 AS DefaultPrice
                             FROM xmdt_t
                             LEFT JOIN xmdu_t ON xmdtdocno=xmdudocno
                             WHERE xmdtent='3' AND xmduent='3' AND xmdu002='" & item & "' AND xmdt016 is null ORDER BY xmdt015 DESC"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        If ds.Tables("DATASET").Rows.Count = 0 Then
            'This Item never be used before. Can't set the price so we will define price as 0 with no currency (N/A)
            cmb_curr_price = "N/A" + "|" + "0"
        Else
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                If i = 0 Then
                    Seq = ds.Tables("DATASET")(i)("Seq").ToString
                    PVDoc = ds.Tables("DATASET")(i)("PVDoc").ToString
                    itemid = ds.Tables("DATASET")(i)("ItemID").ToString
                    curr = ds.Tables("DATASET")(i)("Currency").ToString
                    PRE = ds.Tables("DATASET")(i)("pricerateEnable").ToString
                    defprice = ds.Tables("DATASET")(i)("DefaultPrice").ToString
                    ' -------- Condition Valuation Section  -------- 
                    If PRE = "N" Then     'This Item is no price negotiation by Qty so pick defprice as price to calculate
                        finalprice = defprice
                    Else                  'Seek for pricerate negotiation by top value data
                        finalprice = getItemPriceByQty(PVDoc, Seq)
                    End If
                    ' -------- Condition Valuation Section END -------- 
                    cmb_curr_price = curr + "|" + defprice
                Else
                End If
            Next
        End If
        Return cmb_curr_price

    End Function

    Public Function getItemPriceByQty(ByVal pvDoc As String, ByVal seq As String) As String

        Dim pricebyQty As String = ""
        Dim ds As DataSet
        Dim sql As String = "SELECT xmdv003 AS PriceByQty FROM xmdv_t WHERE xmdvdocno='" & pvDoc & "' AND xmdvseq='" & seq & "' ORDER BY xmdv003 DESC"
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        For i = 0 To ds.Tables("DATASET").Rows.Count - 1
            If i = 0 Then
                pricebyQty = ds.Tables("DATASET")(i)("PriceByQty").ToString
            Else
            End If
        Next
        Return pricebyQty

    End Function

    'Special Function
    Private Function getExchRate(ByVal currency As String) As String

        Dim Exchrate As String = ""
        If currency = "N/A" Or currency = "THB" Then
            Exchrate = "1"
        Else
            Dim ds As DataSet
            Dim sql As String = "SELECT ooao003 AS BaseCurrency,ooao002 AS ToCurrency,ooao004 AS ExchPeriod,ooao011 AS ExchRate FROM ooao_t WHERE ooaoent='3' AND ooao002='" & currency & "' ORDER BY ooao004 DESC"
            ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
            clsconnect.Close(clsconnect.T100)
            For i = 0 To ds.Tables("DATASET").Rows.Count - 1
                If i = 0 Then
                    Exchrate = ds.Tables("DATASET")(i)("ExchRate").ToString
                Else
                End If
            Next

        End If
        Return Exchrate

    End Function

End Class