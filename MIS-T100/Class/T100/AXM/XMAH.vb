﻿Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class XMAH
    Public Shared strParameter1 As String
    Public Shared strParameter2 As String
    Public Shared strParameter3 As String
    Public Shared strParameter4 As String
    '# Module : AXM
    '# Table : xmah_t
    '# axmi130 : Customer Price Method : Header     Customer
    '# axmi120 : Item Code By Customer
    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strCustPriceMethodH_Rows100 As String = "select * from xmah_t  where rownum <= 100 "
    Shared Function CutsPriceMethodH_Rows100() As String
        Return strCustPriceMethodH_Rows100
    End Function

    Public Shared Function Parameter() As String
        Return strParameter1
        Return strParameter2
        Return strParameter3
        Return strParameter4
    End Function

End Class
