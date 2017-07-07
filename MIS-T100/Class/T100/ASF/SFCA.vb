Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFCA
    ''' <reamrks>
    ''' # Module : ASF
    ''' # Table : sfca_t
    ''' # asft301 : Maintain MO Operation : Header
    ''' # asft004 : Workstation WIP Status  >> Tab1:  MO-No.
    ''' </reamrks>
    Private Shared ASF As String = "ASF"
    ''' <remarks> # Structure Table MO Header Preocess / Operation </remarks>
    Public Shared tblMO_Detail As String = "sfca_t"
    ''' <remarks> # Field </remarks>
    Public Shared ent As String = "sfcaent"
    Public Shared Site As String = "sfcasite"
    Public Shared DocNo As String = "sfcadocno"
    Public Shared Company As String = "sfcasite"
    Public Shared RunCardNo As String = "sfca001"
    Public Shared RunCardDetail As String = "sfca005"
    Public Shared ProductionQty As String = "sfca003"
    Public Shared CompletedQty As String = "sfca004"
    Public Shared VersionMOChange As String = "sfca002" '--VersionMOChange

    '''<reamrks> Condition Field </reamrks>
    Public Shared Whr As String = ""
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '--Page SalesOrderChangeStatus
    '--SelectdocNoLine where DocNo  / NO Rrfesh DataTable
    Private Shared SelectdocNoLine As String = "select " & DocNo & "," & VersionMOChange & "," & RunCardNo & "," & ProductionQty & "," & CompletedQty & " from " & tblMO_Detail & " " &
        " where " & wStandard & " and " & DocNo & " ='@docNo'"
    Public Shared Function GetDocNoMOLine(ByVal DocNoMOLine As String)
        Dim Oral As String = SelectdocNoLine
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@docNo", DocNoMOLine)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--SelectdocNoLine where DocNo RunCardNo VersionDocNo  from  Rrfesh DataTable
    Private Shared SelectdocNoLineRrfesh As String = "select " & DocNo & "," & VersionMOChange & "," & RunCardNo & "," & ProductionQty & "," & CompletedQty & " from " & tblMO_Detail & " " &
        " where " & wStandard & " and " & DocNo & " ='@docNo'"
    Public Shared Function GetDocNoMOLineRrfesh(ByVal DocNoMOLine As String)
        Dim Oral As String = SelectdocNoLineRrfesh
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@docNo", DocNoMOLine)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function























    Private Shared strWH_MoNoProcess As String = "Select " & DocNo & "," & ProductionQty & "," & CompletedQty & " FROM " & tblMO_Detail & "  where " & DocNo & " =@MoNO "
    Public Shared Function GetDataMoProcessHeader(ByVal WH_MoNo As String) As DataTable
        Dim strSQL As String = strWH_MoNoProcess
        strSQL = strSQL.Replace("@MoNO", "'" & WH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCA", "GetDataMoProcessHeader", "strSQL = strWH_MoNoProcess", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataMoProcessHeaderDataSet(ByVal WH_MoNo As String) As DataSet
        Dim strSQL As String = strWH_MoNoProcess
        strSQL = strSQL.Replace("@MoNO", "'" & WH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCA", "GetDataMoProcessHeaderDataSet", "strSQL = strWH_MoNoProcess", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
