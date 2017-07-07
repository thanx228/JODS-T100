Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Public Class ConfigCrystalReport
    Public Shared Sub Sub_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        'CrystalReport1' must be the name the CrystalReport

        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo
        'Crystal Report Properties

        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        ' Then, use following code sample to add the logic in the Page_Load method of your Web Form

        crConnectionInfo.ServerName = "192.168.50.1"
        'crConnectionInfo.DatabaseName = "DBMIS"

        crConnectionInfo.DatabaseName = "DBMIST100"
        crConnectionInfo.UserID = "mis"
        crConnectionInfo.Password = "Mis2012"
        crConnectionInfo.UserID = "sa"
        crConnectionInfo.Password = "Alex0717"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next
    End Sub

    Public Shared Sub SubERP_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)
        'CrystalReport1' must be the name the CrystalReport
        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo
        'Crystal Report Properties
        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        'Then, use following code sample to add the logic in the Page_Load method of your Web Form

        crConnectionInfo.ServerName = "192.168.50.1"
        'crConnectionInfo.DatabaseName = "JINPAO80"
        'crConnectionInfo.DatabaseName = "DBMIS"
        crConnectionInfo.DatabaseName = "DBMIST100"
        crConnectionInfo.UserID = "sa"
        crConnectionInfo.Password = "Alex0717"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next
    End Sub

    Public Shared Sub SubJPP_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        'CrystalReport1' must be the name the CrystalReport
        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo
        ' Crystal Report Properties
        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        ' Then, use following code sample to add the logic in the Page_Load method of your Web Form

        crConnectionInfo.ServerName = "192.168.1.13"
        crConnectionInfo.DatabaseName = "JPPSchool"
        crConnectionInfo.UserID = "kung"
        crConnectionInfo.Password = "kung"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next
    End Sub
    Public Shared Sub SubTrace_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        'CrystalReport1' must be the name the CrystalReport

        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo
        'Crystal Report Properties

        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        'Then, use following code sample to add the logic in the Page_Load method of your Web Form

        crConnectionInfo.ServerName = "192.168.1.13"
        crConnectionInfo.DatabaseName = "tracesystem"
        crConnectionInfo.UserID = "mis"
        crConnectionInfo.Password = "Mis2012"
        crConnectionInfo.UserID = "kung"
        crConnectionInfo.Password = "kung"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next
    End Sub

    '### New T100
    Public Shared Sub SubT100_CRCrystalLogon(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        'CrystalReport1' must be the name the CrystalReport

        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo
        'Crystal Report Properties

        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        'Then, use following code sample to add the logic in the Page_Load method of your Web Form

        crConnectionInfo.ServerName = "JP"
        crConnectionInfo.DatabaseName = "JP"
        crConnectionInfo.UserID = "dsdemo"
        crConnectionInfo.Password = "dsdemo"
        crDatabase = rpt.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)
        Next
    End Sub
End Class
