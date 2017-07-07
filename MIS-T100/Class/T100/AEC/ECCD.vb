Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECCD
    '# Module : AEC
    '# Table : eccd_t
    '# ect801 : Item Routing Change : Item Body Right

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strItemRoutingCahngeBright_Rows100 As String = "select * from eccd_t  where rownum <= 100 "
    Shared Function ItemRoutingCahngeBright_Rows100() As String
        Return strItemRoutingCahngeBright_Rows100
    End Function

End Class

