Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECCB
    '# Module : AEC
    '# Table : eccb_t
    '# ect801 : Item Routing Change : Item Body 1

    '# Function for select rows Top 100 (Example)
    '# select * from Table where field ='string' AND field2 ='string'
    Private Const strItemRoutingCahngeB1_Rows100 As String = "select * from eccb_t  where rownum <= 100 "
    Shared Function ItemRoutingCahngeB1_Rows100() As String
        Return strItemRoutingCahngeB1_Rows100
    End Function


End Class

