Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class PMAA
    '# Module : APM
    '# Table : pmaa_t
    '# azmm200 : Customer Info 
    '''<reamrks>Table Custommer Main</reamrks>
    Public Shared tbCustommerMain As String = "pmaa_t"
    Public Shared ContactID As String = "pmaa001"
    Public Shared KeyCustMain As String = "pmaa027" '--KeyCustMain ใช้สำหรับ ชนกับ Key Column OOFB002 Table OOFB_t #เพื่อดึงเอาที่อยู่ลูกค้า
    Public Shared Status As String = "pmaastus"
    Public Shared ent As String = "pmaaent"

    '''<reamrks> Condition Field </reamrks>
    Public Shared wStandard As String = ent & "='3'"
    Public Shared Approved As String = Status & "='Y'"

    '--Page BillInvoice
    '--Checking EmployeeID Section Sales
    Private Shared CustAddressOral As String = "select " & ContactID & "," & KeyCustMain & " from " & tbCustommerMain & " " &
        " where " & wStandard & " and " & ContactID & "='@ContactID'"
    Public Shared Function GetCustAddressOral(ByVal CustID As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = CustAddressOral
        Oral = Oral.Replace("@ContactID", CustID)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function






























    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strCustInfo_Rows100 As String = "select * from pmaa_t  where rownum <= 100 "
    Shared Function CustInfo_Rows100() As String
        Return strCustInfo_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function



End Class
