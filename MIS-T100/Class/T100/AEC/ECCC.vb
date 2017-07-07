Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECCC
    '# Module : AEC
    '# Table : eccc_t
    '# ect801 : Item Routing Change : Item Body Left

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strItemRoutingCahngeBleft_Rows100 As String = "select * from eccc_t  where rownum <= 100 "
    Shared Function ItemRoutingCahngeBleft_Rows100() As String
        Return strItemRoutingCahngeBleft_Rows100
    End Function

End Class

