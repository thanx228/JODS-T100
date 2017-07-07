Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class RTAXL
    '# Module : ART
    '# T100 : aimi010
    '''<reamrks>##########Table Basic profile multi-language category stalls ##############</reamrks>
    Public Shared tblClassificationSecondary As String = "rtaxl_t"
    Public Shared ItemCategoryNo As String = "rtaxl001"
    Public Shared Description As String = "rtaxl003"
    Public Shared MnemonicCode As String = "rtaxl04"
    Public Shared Langauge As String = "rtaxl002"
    Public Shared ent As String = "rtaxlent"
    '''<reamrks> Condition Field </reamrks>
    Public Shared WStandard As String = ent & " ='3' "
    Public Shared LGStandard As String = Langauge & "='en_US'"
    Public Shared Electronic As String = "1"
    Public Shared Aerospace As String = "2"
    Public Shared Automotive As String = "3"
    Public Shared Telecommunic As String = "4"
    Public Shared Medical As String = "5"
    Public Shared FoodIndustry As String = "6"
    Public Shared Energy As String = "7"
    Public Shared Transportati As String = "8"
    Public Shared enUS As String = Langauge & "='en_US'"

    '--UsingControl CheckBoxlist Classification
    Private Shared SelectProductClassification As String = "select " & ItemCategoryNo & "," & Description & "," & ItemCategoryNo & " || ' : ' || " & Description & " as ProductClassification from " & tblClassificationSecondary & " " &
        " where " & WStandard & "and " & LGStandard & "and " & ItemCategoryNo & " in ('" & Electronic & "','" & Aerospace & "','" & Telecommunic & "','" & Medical & "','" & FoodIndustry & "','" & Energy & "','" & Transportati & "')"
    Public Shared Function ProductClassification() As Data.DataTable
        Dim Oral As String = SelectProductClassification
        Dim dt As New DataTable
        Dim dtAdapter = New OracleDataAdapter(Oral, clsDBConnect.strT100ConnectionString)
        dtAdapter.Fill(dt)
        Return dt
    End Function

    '--Select Classification Description where  ItemCategoryNo / no Refresh DataTable
    Private Shared SelectIndustry As String = "select " & ItemCategoryNo & "," & Description & " from " & tblClassificationSecondary & " " &
        " where " & WStandard & "and " & LGStandard & "and " & ItemCategoryNo & " in ('" & Electronic & "','" & Aerospace & "','" & Telecommunic & "','" & Medical & "','" & FoodIndustry & "','" & Energy & "','" & Transportati & "')and " & ItemCategoryNo & " ='@ItemCategoryNo'"
    Public Shared Function Industry(ByVal ItemCategoryNo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectIndustry
        Oral = Oral.Replace("@ItemCategoryNo", ItemCategoryNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function
End Class
