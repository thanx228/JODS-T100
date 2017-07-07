Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECBB
    '# Module : AEC
    Private Shared AEC As String = "AEC"
    '# Table : ecbb_t
    '# aecm200 : Item Routing : Item - Body Tab Detail
    '# aecm200 : Item Routing : Header
    '''<reamrks>## Table Item Routing : Header  ##############</reamrks>
    Public Shared tblItemRoutingBody As String = "ecbb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ProcessPartNo As String = "ecbb001"
    Public Shared ProcessPartNoNumber As String = "ecbb002"

    Public Shared ItemSequence As String = "ecbb003"
    Public Shared CurrentOperation As String = "ecbb004"
    Public Shared OperationSeq As String = "ecbb005"
    Public Shared Groups_ As String = "ecbb007"
    Public Shared PerviuosOperation As String = "ecbb008"
    Public Shared Sequence As String = "ecbb009"
    Public Shared NextOperation As String = "ecbb010"
    Public Shared NextSequence As String = "ecbb011"
    Public Shared Workstation As String = "ecbb012"
    Public Shared ResourceGroup As String = "ecbb037"
    Public Shared FixedLaborHours As String = "ecbbua001"
    Public Shared FixedLaborHours_m As String = "ecbb024"
    Public Shared StandardLaborHours As String = "ecbbua002"
    Public Shared StandardLaborHoursh As String = "ecbbua005"
    Public Shared StandardLaborHoursm1 As String = "ecbbua006"
    Public Shared StandardLaborHoursm2 As String = "ecbb025"
    Public Shared FixedMachineHours As String = "ecbbua003"
    Public Shared FixedMachineHoursm As String = "ecbb026"
    Public Shared StandardMachineHours As String = "ecbbua004"
    Public Shared StandardMachineHoursh As String = "ecbbua007"
    Public Shared StandardMachineHoursm As String = "ecbbua008"
    Public Shared StandardMachineHoursm2 As String = "ecbb027"
    Public Shared PostpositionTime As String = "ecbb034"
    Public Shared Subcontracting As String = "ecbb013"
    Public Shared MainProcessPlant As String = "ecbb014"
    Public Shared MoveIn As String = "ecbb015"
    Public Shared CheckIn As String = "ecbb016"
    Public Shared WorkReportStation As String = "ecbb017"
    Public Shared PQC As String = "ecbb018"
    Public Shared CheckOut As String = "ecbb019"
    Public Shared MoveOut As String = "ecbb020"
    Public Shared TransferInUnit As String = "ecbb030"
    Public Shared TransferInUnitConversionNumberRator As String = "ecbb031"
    Public Shared TransferInUnitConversionDenaminator As String = "ecbb032"
    Public Shared TrasnferOutUnit As String = "ecbb021"
    Public Shared TransferOutUnitConversionNumberrator As String = "ecbb022"
    Public Shared TransferOutUnitConversionDenaminator As String = "ecbb023"
    Public Shared RecylingStation As String = "ecbb033"
    Public Shared CompletionRate As String = "ecbb028"
    Public Shared StrandardUnitPrice As String = "ecbb029"
    Public Shared Xaxis As String = "ecbb035"
    Public Shared Yaxis As String = "ecbb036"

    '''<reamrks> Condition Field </reamrks>
    Public Shared ent As String = "ecbbent"
    Public Shared Site As String = "ecbbsite"
    Public Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "


    '''<remarks> Get ItemRouting Body where ProcessPartNo. ='?' </remarks>  
    Private Shared StrItemRoutingHeaderItemNo As String = "Select " & ItemSequence & "," & CurrentOperation & "," & OperationSeq & ", " &
    " " & Groups_ & "," & PerviuosOperation & "," & Sequence & "," & NextOperation & "," & NextSequence & "," & Workstation & "," & ResourceGroup & ", " &
    " " & FixedLaborHours & "," & FixedLaborHours_m & "," & StandardLaborHours & "," & StandardLaborHoursh & "," & StandardLaborHoursm1 & "," & StandardLaborHoursm2 & ", " &
    " " & FixedMachineHours & "," & FixedMachineHoursm & "," & StandardMachineHours & "," & StandardMachineHoursh & "," & StandardMachineHoursm & "," & StandardMachineHoursm2 & ", " &
    " " & PostpositionTime & "," & Subcontracting & "," & MainProcessPlant & "," & MoveIn & "," & CheckIn & "," & WorkReportStation & "," & PQC & ", " &
    " " & CheckOut & "," & MoveOut & "," & TransferInUnit & "," & TransferInUnitConversionNumberRator & "," & TransferInUnitConversionDenaminator & ", " &
    " " & TrasnferOutUnit & "," & TransferOutUnitConversionNumberrator & "," & TransferOutUnitConversionDenaminator & "," & RecylingStation & ", " &
    " " & CompletionRate & "," & StrandardUnitPrice & "," & Xaxis & "," & Yaxis & " " &
    " FROM " & tblItemRoutingBody & " where " & ProcessPartNo & " =@ProcessPartNo And " & wStandard & " "
    Public Shared Function GetItemRoutingHeaderItemNo(ItemRotingNo As String) As DataTable
        Dim strSQL As String = StrItemRoutingHeaderItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBB", "GetItemRoutingHeaderItemNo", "Sql = StrItemRoutingHeaderItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetItemRoutingHeaderItemNo_DataSet(ItemRotingNo As String) As DataSet
        Dim strSQL As String = StrItemRoutingHeaderItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBB", "GetItemRoutingHeaderItemNo_DataSet", "Sql = StrItemRoutingHeaderItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

