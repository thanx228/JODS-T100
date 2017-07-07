Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class XMDQ
    ' # Module AXM
    Private Shared AXM As String = "AXM"
    '# Table xmdq_t
    '# TansectionCode >> axmt500
    '''<reamrks>##########Table SaleOrder Body : Tab Optional Details ##############</reamrks>
    Public Shared tblSaleItemOptionalDetail As String = "xmdq_t"
    '''<reamrks> # Field </reamrks>
    Public Shared SaleOrderNo As String = "xmdqdocno"
    Public Shared ent As String = "xmdqent"
    Public Shared Site As String = "xmdqsite"
    Public Shared LineNo As String = "xmdqseq"
    Public Shared ItemOrder As String = "xmdqseq1"
    Public Shared AccessoriesItemNo As String = "xmdq001"
    Public Shared MasterItemNo As String = "xmdq002"
    Public Shared PartCode As String = "xmdq003"
    Public Shared OperationNo As String = "xmdq004"
    Public Shared OperationSequence As String = "xmdq005"
    Public Shared QPA As String = "xmdq006"
    Public Shared Denominator As String = "xmdq007"
    Public Shared Unit As String = "xmdq008"
    Public Shared RequestQty As String = "xmdq009"
    Public Shared StandardConstituteUsageVolume As String = "xmdq010"
    Public Shared StandardMasterItemBase As String = "xmdq011"
    Public Shared SubPartFeatures As String = "xmdq012"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '######### Where SaleOrder_No. = ? for All Filed SaleOrder Body : Tab Optional Deatil   #########################
    Private Shared strSO_BodyOptionalDeatil_BySaleOrderNo As String = "Select " & SaleOrderNo & " ," & LineNo & "," & ItemOrder & ", " & AccessoriesItemNo & ", " &
       " " & MasterItemNo & " ," & PartCode & "," & OperationNo & "," & OperationSequence & "," & QPA & "," & Denominator & ", " &
       " " & Unit & "," & RequestQty & "," & StandardConstituteUsageVolume & "," & StandardMasterItemBase & "," & SubPartFeatures & "  " &
       " FROM " & tblSaleItemOptionalDetail & " " &
       " where " & wStandard & " AND " & SaleOrderNo & " =@pSaleOrder_No "
    Public Shared Function GetSO_BodyOptionalDetailBySaleOrderNo(strSaleOrder_No As String) As DataTable
        Dim Sql As String = strSO_BodyOptionalDeatil_BySaleOrderNo.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDQ", "GetSO_BodyOptionalDetailBySaleOrderNo", "Sql = strSO_BodyOptionalDeatil_BySaleOrderNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSO_BodyOptionalDetailBySaleOrderNo_Dataset(strSaleOrder_No As String) As DataSet
        Dim Sql As String = strSO_BodyOptionalDeatil_BySaleOrderNo.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDQ", "GetSO_BodyOptionalDetailBySaleOrderNo_Dataset", "Sql = strSO_BodyOptionalDeatil_BySaleOrderNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#########  Where ItemNo. = ? for All Filed MaterItemNo Body : Tab Delivery Deatil #########################
    Private Shared strSO_BodyOptionalDeatilByMaterItemNo As String = "Select " & SaleOrderNo & " ," & LineNo & "," & ItemOrder & ", " & AccessoriesItemNo & ", " &
       " " & MasterItemNo & " ," & PartCode & "," & OperationNo & "," & OperationSequence & "," & QPA & "," & Denominator & ", " &
       " " & Unit & "," & RequestQty & "," & StandardConstituteUsageVolume & "," & StandardMasterItemBase & "," & SubPartFeatures & "  " &
       " FROM " & tblSaleItemOptionalDetail & " " &
       " where " & wStandard & " AND " & MasterItemNo & " =@pMasterItemNo "
    Public Shared Function GetSO_BodyOptionalDetailByMaterItemNo(strMasterItemNo As String) As DataTable
        Dim Sql As String = strSO_BodyOptionalDeatilByMaterItemNo.Replace("@pMasterItemNo", "'" & strMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDQ", "GetSO_BodyOptionalDetailByMaterItemNo", "Sql = strSO_BodyOptionalDeatilByMaterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetSO_BodyOptionalDetailByMaterItemNo_Dataset(strMasterItemNo As String) As DataSet
        Dim Sql As String = strSO_BodyOptionalDeatilByMaterItemNo.Replace("@pMasterItemNo", "'" & strMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AXM, "XMDQ", "GetSO_BodyOptionalDetailByMaterItemNo_Dataset", "Sql = strSO_BodyOptionalDeatilByMaterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class