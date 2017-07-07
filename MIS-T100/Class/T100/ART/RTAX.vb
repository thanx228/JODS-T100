Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class RTAX
    '# Module : ART
    '# T100 : aimi010
    '''<reamrks>##########Table Basic profile category ##############</reamrks>
    Public Shared tblClassificationMain As String = "rtax_t"
    '''<reamrks>  Field </reamrks>
    Public Shared ItemCategoryNo As String = "rtax003"

    '''<reamrks> Condition Field </reamrks>
    Public Shared Langauge As String = "rtax002"
    Public Shared ent As String = "rtaxent"

    '''<reamrks> Velue Field </reamrks>'''
    Private Shared WStandard As String = ent & " ='3' "
    Public Shared LGStandard As String = Langauge & "='en_US'"
    Public Shared Electronic As String = "1"
    Public Shared Aerospace As String = "2"
    Public Shared Automotive As String = "3"
    Public Shared Telecommunic As String = "4"
    Public Shared Medical As String = "5"
    Public Shared FoodIndustry As String = "6"
    Public Shared Energy As String = "7"
    Public Shared Transportati As String = "8"


End Class
