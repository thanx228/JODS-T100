Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECCA
    '# Module : AEC
    '# Table : ecca_t
    '# ect801 : Item Routing Change : Header

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strItemRoutingCahngeH_Rows100 As String = "select * from ecca_t  where rownum <= 100 "
    Shared Function ItemRoutingCahngeH_Rows100() As String
        Return strItemRoutingCahngeH_Rows100
    End Function

End Class

