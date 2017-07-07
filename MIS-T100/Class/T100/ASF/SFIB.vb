Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFIB
    '# Module T100 : ASF
    Private Shared ASF As String = "ASF"
    '# Table sfib_t
    '# Transfer Return for Rework : Item Rework To 
    '# Tansection Code asft338
    '''<reamrks>### Structure Table : Transfer Return for Rework : Body ##############</reamrks>
    Public Shared tblTransferReworkBody As String = "sfib_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "sfibent"
    Public Shared Site As String = "sfibsite"
    Public Shared DocNo As String = "sfibdocno"
    Public Shared ItemSequence As String = "sfibseq"
    Public Shared OperationID As String = "sfib001"
    Public Shared OperationSeq As String = "sfib002"
    Public Shared GroupProperties As String = "sfib003"
    Public Shared Groups As String = "sfib004"
    Public Shared PreviousOperation As String = "sfib005"
    Public Shared PrevSequence As String = "sfib006"
    Public Shared NextOperation As String = "sfib007"
    Public Shared NextSequence As String = "sfib008"
    Public Shared Workstation As String = "sfib009"
    Public Shared AllowOutsourcing As String = "sfib010"
    Public Shared MainProcessingPlant As String = "sfib011"
    Public Shared MoveIn As String = "sfib012"
    Public Shared CheckIn As String = "sfib013"
    Public Shared WorkReportStation As String = "sfib014"
    Public Shared PQC As String = "sfib015"
    Public Shared CheckOut As String = "sfib016"
    Public Shared MoveOut As String = "sfib017"
    Public Shared TransferOutUnit As String = "sfib018"
    Public Shared TransferOutUitConversionNumerator As String = "sfib019"
    Public Shared TransferOutUnitConversionDenominator As String = "sfib020"
    Public Shared FixedLaborHours As String = "sfib021"
    Public Shared StandardLaborHours As String = "sfib022"
    Public Shared FixMachineHours As String = "sfib023"
    Public Shared StandardMachineHours As String = "sfib024"
    Public Shared StandardOutput As String = "sfib025"
    Public Shared PlannedStartDate As String = "sfib026"
    Public Shared PlannedCompletionDate As String = "sfib027"
    Public Shared TransferInUnit As String = "sfib028"
    Public Shared TransferToUnitConversionRateNumerator As String = "sfib029"
    Public Shared TransferToUnitConversionRateDenominator As String = "sfib030"
    Public Shared Headcount As String = "sfibud011"
    Public Shared FixedLaborHours2 As String = "sfibua001"
    Public Shared StandardLaborHours2 As String = "sfibua002"
    Public Shared FixedMachineHours As String = "sfibua003"
    Public Shared StandardMachineHours2 As String = "sfibua004"
    Public Shared ProductionPlan As String = "sfib031"
    Public Shared ProductionItem As String = "sfib032"
    Public Shared BOMcharacteristics As String = "sfib033"
    Public Shared ProductFeature As String = "sfib034"

    '''<reamrks> Condition Where </reamrks>
    Public Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '#################### Get Transfer Return Reworf Where 1  Doc_No. ='?' #################################################
    Private Shared SqlTransferReworkBy_DocNo As String = "Select " & DocNo & "," & ItemSequence & "," & OperationID & "," & OperationSeq & "," & GroupProperties & ", " &
    " " & Groups & "," & PreviousOperation & "," & PrevSequence & "," & NextOperation & "," & NextSequence & "," & Workstation & ", " &
    " " & AllowOutsourcing & "," & MainProcessingPlant & "," & MoveIn & "," & CheckIn & "," & WorkReportStation & "," & PQC & "," & CheckOut & ", " &
    " " & MoveOut & "," & TransferOutUnit & "," & TransferOutUitConversionNumerator & "," & TransferOutUnitConversionDenominator & "," & FixedLaborHours & "," & StandardLaborHours & "," &
    " " & FixMachineHours & "," & StandardMachineHours & "," & StandardOutput & "," & PlannedStartDate & "," & PlannedCompletionDate & "," & TransferInUnit & "," & TransferToUnitConversionRateNumerator & ", " &
    " " & TransferToUnitConversionRateDenominator & "," & Headcount & "," & FixedLaborHours2 & "," & StandardLaborHours2 & "," & FixedMachineHours & "," & StandardMachineHours2 & ", " &
    " " & ProductionPlan & "," & ProductionItem & "," & BOMcharacteristics & "," & ProductFeature & " " &
    "  FROM " & tblTransferReworkBody & "   where " & wStandard & " AND " & DocNo & " =@DocNo "
    Public Shared Function GetTransferReworkWTrsNo(strDocNo As String) As DataTable
        Dim Sql As String = SqlTransferReworkBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIB", "GetTransferReworkWTrsNo", "Sql = SqlTransferReworkBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetTransferReworkWTrsNo_DataSet(strDocNo As String) As DataSet
        Dim Sql As String = SqlTransferReworkBy_DocNo.Replace("@DocNo", "'" & strDocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFIB", "GetTransferReworkWTrsNo_DataSet", "Sql = SqlTransferReworkBy_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


End Class
