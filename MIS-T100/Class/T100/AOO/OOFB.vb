Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOFB
    '# Module : AOO
    '# Table : oofb_t
    '# azmm200 : Customer Info : Tab Address

    '# Function for select rows Top 100 (Example)
    '''<reamrks>Table BodyCustommer</reamrks>
    Public Shared tbBodyCustommer As String = "oofb_t"
    '''<reamrks> # Column </reamrks>
    Public Shared KeyBodyCust As String = "oofb002" '--KeyBodyCust ใช้สำหรับ ชนกับ KeyCustMain #เพื่อดึงเอาที่อยู่ลูกค้า
    Public Shared AddressCust As String = "oofb017"
    Public Shared AddressType As String = "oofb008"
    Public Shared ent As String = "oofbent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared RegisterAddress As String = "1"
    Public Shared Address As String = "2"
    Public Shared ShippingtoAddress As String = "3"
    Public Shared OtherAddress As String = "4"
    Public Shared BillAddress As String = "5"


    '--Page BillInvoice
    '--Checking EmployeeID Section Sales
    Private Shared AddressCustOral As String = "select " & KeyBodyCust & "," & AddressCust & "," & AddressType & " from " & tbBodyCustommer & " " &
        " where " & WStandard & " and " & KeyBodyCust & "='@KeyBodyCust' and " & AddressType & " in('" & BillAddress & "')"
    Public Shared Function GetAddressCustOral(ByVal KeyBodyCust As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = AddressCustOral
        Oral = Oral.Replace("@KeyBodyCust", KeyBodyCust)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function











































    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String

    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strCustInfoTabAddr_Rows100 As String = "select * from oofb_t  where rownum <= 100 "
    Shared Function CustInfoTaAddr_Rows100() As String
        Return strCustInfoTabAddr_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
