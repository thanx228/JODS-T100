Namespace Language
    Public Structure TransectionCode
        ''' <summary>
        ''' #### Transection Code T100 ##############################
        ''' </summary>
        ''' ### Production , Planing  
        Public Shared asft300 As String = "asft300"  ' Manufacture Order
        Public Shared asft301 As String = "asft301"  ' MO Operation
        Public Shared asft311 As String = "asft311"  ' Material Issue
        Public Shared asft321 As String = "asft321"  ' Materail Return
        Public Shared asft312 As String = "asft312"  ' Materail Issue (Over Issue) 
        Public Shared asft322 As String = "asft322"  ' Material Return (Over Issue)
        Public Shared asft313 As String = "asft313"  ' Maitain Work Order Shortage and Replenishment
        Public Shared asft323 As String = "asft323"  ' Maintain General Material Return for Work Orders
        Public Shared asft335 As String = "asft335"  ' Transfer Order
        Public Shared asft330 As String = "asft330"  ' Transfer Order(Multi)
        Public Shared asft338 As String = "asft338"  ' Transfer for Return,Rework
        Public Shared aqct300 As String = "aqct300"  ' QC Item Inspection
        Public Shared asft340 As String = "asft340"  ' MO-Receipt
        Public Shared asft800 As String = "asft800"  ' MO  Change 
        Public Shared asft801 As String = "asft801"  ' MO Operation Change
        Public Shared asfp500 As String = "asfp500"  ' MO Close
        Public Shared asfp501 As String = "asfp501"  ' Cancel MO Close
        Public Shared asfp301 As String = "asfp301"  ' Order transfer work order operation
        '#### IE
        Public Shared aeci001 As String = "aeci001"  ' Workcenter
        Public Shared aeci004 As String = "aeci004"  ' OPeration Process
        Public Shared aecm200 As String = "aecm002"  ' Item Routings
        Public Shared aect801 As String = "aect008"  ' Item Routing Change
        Public Shared aimm215 As String = "aimm215"  ' Item Property(Production)
        '#### Store
        Public Shared aini005 As String = "aini005"  ' Warehouse(2-1)
        Public Shared aini001 As String = "aini001"  ' Warehouse(2-2)
        Public Shared aini002 As String = "aini002"  ' BN(Location)
        Public Shared aini212 As String = "aini212"  ' Lot Control
        Public Shared aini211 As String = "aini211"  ' Sheft Control

    End Structure
    ''' <summary>
    ''' Structure Language Thai
    ''' </summary> 
    Public Structure ThaiERP
        Public Class SwictherLanguage
            Private Shared asft300 = TransectionCode.asft300,
                asft301 = TransectionCode.asft301,
                asft311 = TransectionCode.asft311,
                asft321 = TransectionCode.asft321,
                asft312 = TransectionCode.asft321,
                asft322 = TransectionCode.asft321,
                asft313 = TransectionCode.asft321,
                asft323 = TransectionCode.asft321,
                asft335 = TransectionCode.asft321,
                asft330 = TransectionCode.asft321,
                asft338 = TransectionCode.asft321,
                aqct300 = TransectionCode.asft321,
                asft340 = TransectionCode.asft321,
                asft800 = TransectionCode.asft321,
                asft801 = TransectionCode.asft321,
                asfp500 = TransectionCode.asft321,
                asfp501 = TransectionCode.asft321,
                asfp301 = TransectionCode.asft321,
                aeci001 = TransectionCode.asft321,
                aeci004 = TransectionCode.asft321,
                aecm200 = TransectionCode.asft321,
                aect801 = TransectionCode.asft321,
                aimm215 = TransectionCode.asft321,
                aini005 = TransectionCode.asft321,
                aini001 = TransectionCode.asft321,
                aini002 = TransectionCode.asft321,
                aini212 = TransectionCode.asft321,
                aini211 = TransectionCode.asft321
        End Class
    End Structure
    ''' <summary>
    ''' Sturcture Language English
    ''' </summary>
    Public Structure EnglishERP
        Private Shared asft300 = TransectionCode.asft300,
                asft301 = TransectionCode.asft301,
                asft311 = TransectionCode.asft311,
                asft321 = TransectionCode.asft321,
                asft312 = TransectionCode.asft321,
                asft322 = TransectionCode.asft321,
                asft313 = TransectionCode.asft321,
                asft323 = TransectionCode.asft321,
                asft335 = TransectionCode.asft321,
                asft330 = TransectionCode.asft321,
                asft338 = TransectionCode.asft321,
                aqct300 = TransectionCode.asft321,
                asft340 = TransectionCode.asft321,
                asft800 = TransectionCode.asft321,
                asft801 = TransectionCode.asft321,
                asfp500 = TransectionCode.asft321,
                asfp501 = TransectionCode.asft321,
                asfp301 = TransectionCode.asft321,
                aeci001 = TransectionCode.asft321,
                aeci004 = TransectionCode.asft321,
                aecm200 = TransectionCode.asft321,
                aect801 = TransectionCode.asft321,
                aimm215 = TransectionCode.asft321,
                aini005 = TransectionCode.asft321,
                aini001 = TransectionCode.asft321,
                aini002 = TransectionCode.asft321,
                aini212 = TransectionCode.asft321,
                aini211 = TransectionCode.asft321
        Public Class SwictherLanguage


        End Class
    End Structure



End Namespace
