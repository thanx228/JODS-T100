Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOBXL
    ''' <summary>
    ''' # Module T100 : AOO  
    ''' # Table : oobxl_t 
    ''' #   Document Type  ERP-T100
    ''' </summary>
    Private Shared AOO As String = "AOO"
    '''<reamrks>##########Table DocType ##############</reamrks>
    Public Shared tblDocType As String = "oobxl_t"
    '''<reamrks> # Field </reamrks>
    '''<remarks> '# Sale-Type, MO-Type, MO-TypeGenerateBacth, TransferOrderType,</remarks>
    Public Shared DocTypeId As String = "oobxl001"
    Public Shared DocType As String = "oobxl003"
    Public Shared ShowDocType As String = "SaleDocType"
    Public Shared Language As String = "oobxl002"
    Public Shared ent As String = "oobxlent"

    '''<reamrks> Condition Where </reamrks>
    Public Shared wStandard As String = ent & " ='3' "
    Public Shared enUS As String = Language & " ='en_US' "


    '--Page CustomsNew
    '--Invoice Type 
    Private Shared GetInvType As String = "Select  (substr(" & DocTypeId & ",3,4)) AS  DocType_Id," & DocType & ",(substr(" & DocTypeId & ",3,4)) || ' : ' || " & DocType & " AS  InvTypeDescription from " & tblDocType & "" &
        " where " & DocTypeId & " IN ('61EX','61LC') and " & wStandard & ""
    Public Shared Function Get_InvType() As DataTable
        Dim Orl As String = GetInvType
        Dim dt As New DataTable
        Dim dtAdapter = New OracleDataAdapter(Orl, clsDBConnect.strT100ConnectionString)
        dtAdapter.Fill(dt)
        Return dt '*** Return DataTable ***'
    End Function



    '############################## Sale Type #########################################
    Private Shared strSqlTypqSale As String = "select " & DocTypeId & "," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
        " from " & tblDocType & "  where " & Language & " ='en_US' and (" & DocTypeId & " like '22%') and  (" & ent & " = '3') "
    '''<remarks># Sale DocType DataTable</remarks>
    Public Shared Function GetDocTypeSaleTable() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlTypqSale, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDocTypeSaleTable", "strSqlTypqSale", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Sale DocType DataSet</remarks>
    Public Shared Function GetDocTypeSaleDataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlTypqSale, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDocTypeSaleDataSet", "strSqlTypqSale", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## MO Type #########################################
    Private Shared strSqlMOtype As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
     " from " & tblDocType & " where " & Language & "  ='en_US' And ( " & DocTypeId & " Like '51%' or  " & DocTypeId & " like '52%' or  " & DocTypeId & " like '53%') and  " & ent & " ='3' "
    '''<remarks># MO Type DataTable</remarks>
    Public Shared Function GetMOType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOtype, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMOType_Table", "strSqlMOtype", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># MO Type Data Set</remarks>
    Public Shared Function GetMOType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOtype, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMOType_DataSet", "strSqlMOtype", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '############################## Where MO Type #########################################
    Private Shared strSqlMOtypeWhere As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
     " from " & tblDocType & " where " & Language & "  ='en_US' And ( " & DocTypeId & " Like '51%' or  " & DocTypeId & " like '52%' or  " & DocTypeId & " like '53%') and  " & ent & " ='3' " &
     " and " & DocTypeId & " = @pMOtype  "
    '''<remarks># MO Type DataTable</remarks>
    Public Shared Function GetMOTypeWhere_Table(DocTypeId) As DataTable
        Dim Sql As String = strSqlMOtypeWhere
        Sql = Sql.Replace("@pMOtype", "'" & DocTypeId & "'")
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMOTypeWhere_Table", "strSqlMOtypeWhere", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '############################## Material Generate Bacth Type #########################################
    Private Shared strSqlMOtypeGenerateBatch As String = " Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
     " from " & tblDocType & " where " & Language & " ='en_US'  and  " & ent & " ='3' " &
     " And ( " & DocTypeId & " = '5' or  " & DocTypeId & " = '5104' or  " & DocTypeId & " = '5109' or  " & DocTypeId & " = '5194' or  " & DocTypeId & " = '5199' " &
     " or  " & DocTypeId & " = '5210' or  " & DocTypeId & " = '5211' or  " & DocTypeId & " = '5212' ) "

    '''<remarks># MO Type ofr GenerateBatch DataTable</remarks>
    Public Shared Function GetMOTypeGenerateBatch_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOtypeGenerateBatch, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMOTypeGenerateBatch_Table", "strSqlMOtypeGenerateBatch", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># MO Type for GenerateBatch Data Set</remarks>
    Public Shared Function GetMOTypeGenerateBatch_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOtypeGenerateBatch, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMOTypeGenerateBatch_DataSet", "strSqlMOtypeGenerateBatch", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Transfer Order Type #########################################
    Private Shared strSqlTransferOrder As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
    "  from " & tblDocType & "  where " & Language & "='en_US' and " & DocTypeId & " like 'D2%' And " & ent & " = '3' and (" & DocTypeId & " <> 'D205' and " & DocTypeId & " <> 'D209') "
    '''<remarks># DocType TransferOrder  T100 Function asft335  >> DataTable</remarks>
    Public Shared Function GetDocTransferOrder_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlTransferOrder, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDocTransferOrder_Table", "strSqlTransferOrder", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># DocType TransferOrder T100 Function asft335    >> DataSet</remarks>
    Public Shared Function GetDocTransferOrder_Dataset() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlTransferOrder, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDocTransferOrder_Dataset", "strSqlTransferOrder", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Materail Return for Rework Type #########################################
    Private Shared strSqlReworkReturn As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
    " from " & tblDocType & " where " & Language & "='en_US' " &
    " and (" & DocTypeId & " = 'D205' OR " & DocTypeId & " = 'D209') and " & ent & " = '3' "
    '''<remarks># DocType TransferOrder  T100 Rework Function asft338  >> DataTable</remarks>
    Public Shared Function GetDocReworkTable() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlReworkReturn, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDocReworkTable", "strSqlReworkReturn", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># DocType TransferOrder T100 Rework Function asft338 >> DataSet</remarks>
    Public Shared Function GetDoc_ReworkDataset() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlReworkReturn, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetDoc_ReworkDataset", "strSqlReworkReturn", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Materail Issue Type #########################################
    Private Shared strSqlMatIssue As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
     " from " & tblDocType & "  where " & Language & " ='en_US' and (" & DocTypeId & " like '54%') and  (" & ent & " = '3') "
    '''<remarks># Material Issue Type DataTable</remarks>
    Public Shared Function GetMaterailIssueType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatIssue, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMaterailIssueType_Table", "strSqlMatIssue", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Material Issue Type DataSet</remarks>
    Public Shared Function GetMaterialIssueType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatIssue, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMaterialIssueType_DataSet", "strSqlMatIssue", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## MO-Receipt Type #########################################
    Private Shared strSqlMOreceipt As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
      " from " & tblDocType & "   where " & Language & "='en_US' and " & DocTypeId & " like 'D30%' And " & ent & " = '3' "
    '''<remarks># MO-Receipt Type DataTable</remarks>
    Public Shared Function GetMO_ReceiptType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOreceipt, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMO_ReceiptType_Table", "strSqlMOreceipt", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># MO-Receipt Type DataSet</remarks>
    Public Shared Function GetMO_ReceiptType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMOreceipt, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMO_ReceiptType_DataSet", "strSqlMOreceipt", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Inventory Input Type #########################################
    Private Shared strSqlInventoryInput As String = "select " & DocTypeId & "," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
    " from " & tblDocType & " Left Join ooba_t ON " & tblDocType & "." & DocTypeId & " =  ooba_t.ooba002 " &
       " where oobastus ='Y' and oobaent='3' " &
       " and (" & DocTypeId & " = '1101' OR " & DocTypeId & " ='1103' OR " & DocTypeId & " ='1110' OR " & DocTypeId & " ='1112' " &
       " OR " & DocTypeId & " = '1113' OR " & DocTypeId & " = '1117' OR " & DocTypeId & " = '1118' OR " & DocTypeId & " = '1199') " &
       " OR " & DocType & " = 'TransectionIn'  and " & Language & "='en_US' " &
       " Group by " & DocTypeId & "," & DocType & "  Order by " & DocTypeId & " asc "
    '''<remarks># Inventory Type (Transection In) DataTable</remarks>
    Public Shared Function GetInventoryType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryInput, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryType_Table", "strSqlInventoryInput", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Inventory Type (Transection In)  DataSet</remarks>
    Public Shared Function GetInventoryType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryInput, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryType_DataSet", "strSqlInventoryInput", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Inventory Output Type #########################################
    Private Shared strSqlInventoryOut As String = "select " & DocTypeId & "," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
    " from " & tblDocType & " Left Join ooba_t ON " & tblDocType & "." & DocTypeId & " =  ooba_t.ooba002 " &
       " where oobastus ='Y' and oobaent='3' " &
       " and (" & DocTypeId & " = '1102' OR " & DocTypeId & " ='1104' OR " & DocTypeId & " ='1106' OR " & DocTypeId & " ='1109' " &
       " OR " & DocTypeId & " = '1111' OR " & DocTypeId & " = '1114' OR " & DocTypeId & " = '1116' OR " & DocTypeId & " = '1199' " &
       " OR " & DocTypeId & " = '1198') and " & Language & "='en_US' " &
       " Group by " & DocTypeId & "," & DocType & "  Order by " & DocTypeId & " asc "
    '''<remarks># Inventory Type (Transection Output) DataTable</remarks>
    Public Shared Function GetInventoryOutType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryOut, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryOutType_Table", "strSqlInventoryOut", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Inventory Type (Transection Output)  DataSet</remarks>
    Public Shared Function GetInventoryOutType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryOut, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryOutType_DataSet", "strSqlInventoryOut", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Inventory Scarp Type #########################################
    Private Shared strSqlInventoryScarpType As String = "select " & DocTypeId & "," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
    " from " & tblDocType & " Left Join ooba_t ON " & tblDocType & "." & DocTypeId & " =  ooba_t.ooba002 " &
       " where oobastus ='Y' and oobaent='3' " &
       " and (" & DocTypeId & " = '1801' OR " & DocTypeId & " ='1802' OR " & DocTypeId & " ='1803' OR " & DocTypeId & " ='1804' " &
       " OR " & DocTypeId & " = '1198') and " & Language & "='en_US' " &
       " Group by " & DocTypeId & "," & DocType & "  Order by " & DocTypeId & " asc "
    '''<remarks># Inventory Scarp Type DataTable  (Request for inventory Scarpping aint310)</remarks>
    Public Shared Function GetInventoryScarpType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryScarpType, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryScarpType_Table", "strSqlInventoryScarpType", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Inventory Scarp Type DataSet  (Request for inventory Scarpping aint310)</remarks>
    Public Shared Function GetInventoryScarpType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlInventoryScarpType, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetInventoryScarpType_DataSet", "strSqlInventoryScarpType", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Mat_Issue Over Type #########################################
    Private Shared strSqlMatIssueOver As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
  " from " & tblDocType & "   where " & Language & "='en_US' and " & DocTypeId & " = '5501' And " & ent & " = '3' "
    '''<remarks># MO-Receipt Type DataTable</remarks>
    Public Shared Function GetMatIssueOverType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatIssueOver, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMatIssueOverType_Table", "strSqlMatIssueOver", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># MO-Receipt Type DataSet</remarks>
    Public Shared Function GetMatIssueOverType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatIssueOver, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMatIssueOverType_DataSet", "strSqlMatIssueOver", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '############################## Mat Return,Rework Over Type #########################################
    Private Shared strSqlMatReworkOver As String = "Select " & DocTypeId & " ," & DocType & "," & DocTypeId & " || ' : ' || " & DocType & " as " & ShowDocType & " " &
  " from " & tblDocType & "   where " & Language & "='en_US' and " & DocTypeId & " = '5701' And " & ent & " = '3' "
    '''<remarks># MO-Receipt Type DataTable</remarks>
    Public Shared Function GetMatReworkOverType_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatReworkOver, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMatReworkOverType_Table", "strSqlMatReworkOver", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># MO-Receipt Type DataSet</remarks>
    Public Shared Function GetMatReworkOverType_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlMatReworkOver, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOBXL", "GetMatReworkOverType_DataSet", "strSqlMatReworkOver", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    Private Shared dtAdapter As OracleDataAdapter
    Private Shared objConn As OracleConnection
    Private Shared Sub T100Close()
        If objConn.State = ConnectionState.Open Then objConn.Close()
        dtAdapter = Nothing
        objConn = Nothing
    End Sub
End Class

