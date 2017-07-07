Public Class ISAG
    '''<reamrks>Table Invoice</reamrks>
    Public Shared tblInvoiceTapDetail As String = "isag_t"
    '''<reamrks> Column </reamrks>
    Public Shared Invoice As String = "isagdocno"
    Public Shared ShipOrderNo As String = "isag002"
    Public Shared SeqSalesOrderfromInv As String = "isag003"
    Public Shared InvoiceQty As String = "isag004"
    Public Shared InvoiceAmtTax As String = "isag105"
    Public Shared Item As String = "isag009"
    Public Shared ItemName As String = "isag010"
    Public Shared Langauge As String = "isagent"
    Public Shared ent As String = "isagent"
    Public Shared AmtBfTaxInLocalCurr As String = "isag115"

    Public Shared WStandard As String = ent & " ='3' "


    '--Page CustomsNew
    '--Shaerch InvoiceDetail Where Item ana ItemName
    Private Shared SelectInvoice As String = "select " & SeqSalesOrderfromInv & "," & ShipOrderNo & "," & Invoice & "," & InvoiceQty & "," & InvoiceAmtTax & "," & AmtBfTaxInLocalCurr & "," & ItemName & "," & Item & "" &
        " from " & tblInvoiceTapDetail & "  where " & WStandard & "and " &
        "" & Invoice & " ='@isagdocno' order by " & Invoice & ""
    Public Shared Function InvoiceDetail(ByVal InvoiceNo As String, ByVal ItemName As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectInvoice
        Oral = Oral.Replace("@isagdocno", InvoiceNo)
        If ItemName = "Computer" Then
            Oral = Oral.Replace("@isag010", "'" & ItemName & "'")
        ElseIf ItemName = "Appliances" Then
            Oral = Oral.Replace("@isag010", "'" & ItemName & "'")
        End If
        Return GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Page CustomsNew
    '--Check ShipOrderNo from Sales Invoice same ShipOrderNo from Sales Delivery
    Private Shared CheckShipOrderNoOral As String = "select " & SeqSalesOrderfromInv & "," & ShipOrderNo & " from " & tblInvoiceTapDetail & "  where " & WStandard & ""
    Public Shared Function Get_ShipOrderNoOral(ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = CheckShipOrderNoOral
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function


End Class
