Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFBA
    Public Shared strWH_MoNo As String
    Private Shared ASF As String = "ASF"
    '# Module : ASF
    '# Table : sfba_t
    '# asft300 : Maintain Work Order : Body
    '# asfp500 : Close MO : Item Body
    ''' <remarks> Table MO Body Tab >> Maintain Work Order </remarks>
    Public Shared tblManufactureOrder_Body As String = "sfba_t"
    Public Shared ent As String = "sfbaent"
    Public Shared Site As String = "sfbasite"
    Public Shared MODocNo As String = "sfbadocno"
    Public Shared ItemSequence As String = "sfbaseq"
    Public Shared LineSequence As String = "sfbaseq1"
    Public Shared MasterItemNo As String = "sfba001"
    Public Shared Position As String = "sfba002"
    Public Shared OperationNo As String = "sfba003"
    Public Shared OperationSeq As String = "sfba004"
    Public Shared BOMitem As String = "sfba005"
    Public Shared IssueItem As String = "sfba006"
    Public Shared IssueTimeBucket As String = "sfba007"
    Public Shared EssentialCharacteristics As String = "sfba008"
    Public Shared RecoilMaterial As String = "sfba009"
    Public Shared StandardQPAnumerator As String = "sfba010"
    Public Shared StandardQPAdenominator As String = "sfba011"
    Public Shared AllowErrorRate As String = "sfba012"
    Public Shared RequiredQty As String = "sfba013"
    Public Shared Unit As String = "sfba014"
    Public Shared ConsPurchase As String = "sfba015"
    Public Shared IssuedQty As String = "sfba016"
    Public Shared UnIssuedQty As String = "UnIssuedQty"
    Public Shared SUMUnIssuedQty As String = "SUMUnIssuedQty"
    Public Shared SUMIssuedQty As String = "SUM_Issued_Qty"
    Public Shared SUMUnplannedIssued As String = "SUMUnplannedIssued" ' Sum Issue Over Qty
    Public Shared ScrapQty As String = "sfba017"
    Public Shared CountinglossQuantity As String = "sfba018"
    Public Shared DesignatedIssuanceWarehouse As String = "sfba019"
    Public Shared DesignatedIssuanceLocation As String = "sfba020"
    Public Shared ProductFeature As String = "sfba021"
    Public Shared UsageProb As String = "sfba022"
    Public Shared StandardIssuanceQuantity As String = "sfba023"
    Public Shared SUMStandardIssuanceQuantity As String = "Std_issuance_qty"
    Public Shared AdjustIssuanceQuantity As String = "sfba024"
    Public Shared UnplannedIssued As String = "sfba025"  '########### Issue Over Qty
    Public Shared SETsubstitutionStatus As String = "sfba026"
    Public Shared SETSubstituteGroup As String = "sfba027"
    Public Shared ConsignedMaterial As String = "sfba028"
    Public Shared DesignatedIssuanceBatchNo As String = "sfba029"
    Public Shared DesignatedInventoryManagementFeatures As String = "sfba030"
    Public Shared AllocatedQty As String = "sfba031"
    Public Shared PreparationReasonCode As String = "sfba032"
    Public Shared Bonded As String = "sfba033"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where WO MO_No. = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWH_MO_No As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & MODocNo & " =@MO_No " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_Body(strMoNo As String) As DataTable
        Dim Sql As String = strWH_MO_No.Replace("@MO_No", "'" & strMoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_Body", "Sql = strWH_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_BodyDataSet(strMoNo As String) As DataSet
        Dim Sql As String = strWH_MO_No.Replace("@MO_No", "'" & strMoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BodyDataSet", "Sql = strWH_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where WO MO_No. BETWEEN From = ? and To = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strBETWEEN_MO_No As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & MODocNo & " BETWEEN @FromMO_No  AND @To_MO_No " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_BeetweenMO_No_Body(strFromMoNo As String, strTo_MoNo As String) As DataTable
        Dim Sql As String = strBETWEEN_MO_No
        Sql = Sql.Replace("@FromMO_No", "'" & strFromMoNo & "'")
        Sql = Sql.Replace("@To_MO_No", "'" & strTo_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BeetweenMO_No_Body", "Sql = strBETWEEN_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_BeetweenMO_No_BodyDataSet(strFromMoNo As String, strTo_MoNo As String) As DataSet
        Dim Sql As String = strBETWEEN_MO_No
        Sql = Sql.Replace("@FromMO_No", "'" & strFromMoNo & "'")
        Sql = Sql.Replace("@To_MO_No", "'" & strTo_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BeetweenMO_No_BodyDataSet", "Sql = strBETWEEN_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where Plan StartDate. BETWEEN FromDate = ? and ToDate = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strBETWEEN_PlanStartDate As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & SFAA.PlanStartDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_BETWEEN_PlanStartDate_Body(sDate As String, Edate As String) As DataTable
        Dim Sql As String = strBETWEEN_PlanStartDate
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BETWEEN_PlanStartDate_Body", "Sql = strBETWEEN_PlanStartDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_BETWEEN_PlanStartDate_BodyDataSet(sDate As String, Edate As String) As DataSet
        Dim Sql As String = strBETWEEN_PlanStartDate
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BETWEEN_PlanStartDate_BodyDataSet", "Sql = strBETWEEN_PlanStartDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '####### where WO BOM_Item. = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWH_BOM_Item As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & BOMitem & " =@pBOM_Item " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_BOM_Item_Body(strBOM_Item As String) As DataTable
        Dim Sql As String = strWH_BOM_Item.Replace("@pBOM_Item", "'" & strBOM_Item & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOM_Item_Body", "Sql = strWH_BOM_Item", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_BOM_Item_BodyDataSet(strBOM_Item As String) As DataSet
        Dim Sql As String = strWH_BOM_Item.Replace("@pBOM_Item", "'" & strBOM_Item & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOM_Item_BodyDataSet", "Sql = strWH_BOM_Item", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '####### where Where Custom Parameter. = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWH_Custom As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3'   AND  @pWhereCustom  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & " " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_WhereCustom_Body(strWhereCustom As String) As DataTable
        Dim Sql As String = strWH_Custom
        Dim pWhereCustom As String = strWhereCustom
        Sql = Sql.Replace("@pWhereCustom", pWhereCustom)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_WhereCustom_Body", "Sql = strWH_Custom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '####### where Multiplace WorkStation. = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWH_WorkStation As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3'   AND  @pWorkSttion  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCB.WorkStation & " " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_WorkStation_Body(strWorkStation As String) As DataTable
        Dim Sql As String = strWH_WorkStation
        Dim pWCStation As String = SFCB.WorkStation & " In(" & [String].Join("','", strWorkStation) & "')"
        Sql = Sql.Replace("@pWorkSttion", pWCStation)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_WorkStation_Body", "Sql = strWH_WorkStation", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared strWH_MultiStatus As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND    @pMultiStatus AND @pMultiWorkCenter " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " Order By " & MODocNo & "," & ItemSequence & " "
    Public Shared Function GetManufactureOrder_MultiStatus_And_WC_Body(strMultiStatus As String, stWC As String) As DataTable
        Dim Sql As String = strWH_MultiStatus
        Dim pMultiStatus As String = SFAA.Status & " In(" & [String].Join("','", strMultiStatus) & "')"
        Dim pMultiWorkStation As String = SFCB.WorkStation & " In(" & [String].Join("','", stWC) & "')"
        Sql = Sql.Replace("@pMultiStatus", pMultiStatus)
        Sql = Sql.Replace("@pMultiWorkCenter", pMultiWorkStation)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_MultiStatus_And_WC_Body", "Sql = strWH_MultiStatus", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#######  Multiplace Checkboxlist where BOM_Item. = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWHMultiplace_BOM_Item As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " AND " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3'  " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_MultiplaceBOM_Item_Body(strBOM_Item As String) As DataTable
        Dim Sql As String = strWHMultiplace_BOM_Item
        Sql = Sql & " and SUBSTR(" & BOMitem & ", 3, 1) In(" & [String].Join("','", strBOM_Item) & "')"
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_MultiplaceBOM_Item_Body", "Sql = strWHMultiplace_BOM_Item", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_MultiplaceBOM_Item_BodyDataSet(strBOM_Item As String) As DataSet
        Dim Sql As String = strWHMultiplace_BOM_Item
        Sql = Sql & " and SUBSTR(" & BOMitem & ", 3, 1) In(" & [String].Join("','", strBOM_Item) & "')"
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_MultiplaceBOM_Item_BodyDataSet", "Sql = strWHMultiplace_BOM_Item", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  Spec Like % ? %  (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strWH_BOM_SpecLike As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " where " & wStandard & " And " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & IMAAL.Specifaction & "  @pSpec  " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_BOM_ItemSpec_Body(StrSpec As String) As DataTable
        Dim Sql As String = strWH_BOM_SpecLike
        Sql = Sql.Replace("@pSpec", " Like '%" & StrSpec & "%'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOM_ItemSpec_Body", "Sql = strWH_BOM_SpecLike", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_BOM_ItemSpec_BodyDataSet(StrSpec As String) As DataSet
        Dim Sql As String = strWH_BOM_SpecLike
        Sql = Sql.Replace("@pSpec", " Like '%" & StrSpec & "%'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOM_ItemSpec_BodyDataSet", "Sql = strWH_BOM_SpecLike", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  SaleOrder DocNo = ?   (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strSqlWH_SaleOrder_No As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND " & XMDC.SaleOrderNo & " = @pSaleOrder_No  " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_SaleOrder_No_Body(strSaleOrder_No As String) As DataTable
        Dim Sql As String = strSqlWH_SaleOrder_No
        Sql = Sql.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_No_Body", "Sql = strSqlWH_SaleOrder_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_SaleOrder_No_BodyDataSet(strSaleOrder_No As String) As DataSet
        Dim Sql As String = strSqlWH_SaleOrder_No
        Sql = Sql.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_No_BodyDataSet", "Sql = strSqlWH_SaleOrder_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strSqlWH_SaleOrder_Seq As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND " & XMDC.SaleOrderNo & " = @pSaleOrder_No  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " Order By " & MODocNo & "," & ItemSequence & " "
    Public Shared Function GetManufactureOrder_SaleOrder_Seq_Body(strSaleOrder_No As String, strSaleOrder_Seq As String) As DataTable
        Dim Sql As String = strSqlWH_SaleOrder_Seq
        Sql = Sql.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Sql = Sql.Replace("@pSaleOrder_Seq", "'" & strSaleOrder_Seq & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_Seq_Body", "Sql = strSqlWH_SaleOrder_Seq", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_SaleOrder_Seq_BodyDataSet(strSaleOrder_No As String, strSaleOrder_Seq As String) As DataSet
        Dim Sql As String = strSqlWH_SaleOrder_Seq
        Sql = Sql.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Sql = Sql.Replace("@pSaleOrder_Seq", "'" & strSaleOrder_Seq & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_Seq_BodyDataSet", "Sql = strSqlWH_SaleOrder_Seq", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared strSqlWH_MODocNo_To_ScarpWHCustom As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & "," & SFAA.ScarpQty & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND  @pWhereCustomUsing  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & "," & SFAA.ScarpQty & " " &
        " Order By " & MODocNo & "," & ItemSequence & " "
    Public Shared Function GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlWH_MODocNo_To_ScarpWHCustom
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_MO_Doc_No_To_ScarpWHCustom_Body", "Sql = strSqlWH_MODocNo_To_ScarpWHCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared strSqlWH_SaleOrder_No_To_Scarp As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND " & XMDC.SaleOrderNo & " = @pSaleOrder_No  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " Order By " & MODocNo & "," & ItemSequence & " "
    Public Shared Function GetManufactureOrder_SaleOrder_No_To_Scarp_Body(strSaleOrder_No As String) As DataTable
        Dim Sql As String = strSqlWH_SaleOrder_No_To_Scarp
        Sql = Sql.Replace("@pSaleOrder_No", "'" & strSaleOrder_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_No_To_Scarp_Body", "Sql = strSqlWH_SaleOrder_No_To_Scarp", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '####### where  SaleOrder DocumentDate  = ? (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strSqlWH_SaleOrder_DocumentDate As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
        " where " & XMDA.wStandard & " AND " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND " & XMDA.DocumentDate & " = TO_DATE (@pSaleOrderDocumentDate, 'yyyy/mm/dd')  " &
        " Group By " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDA.SaleOrderNo & "," & XMDA.DocumentDate & ", " &
        " " & SFCB.WorkStation & "," & XMDC.SalesQty & " " &
        " Order By " & MODocNo & "," & ItemSequence & " "
    Public Shared Function GetManufactureOrder_SaleOrderDocumentDate__Body(strSaleOrderDocumentDate As String) As DataTable
        Dim Sql As String = strSqlWH_SaleOrder_DocumentDate
        Sql = Sql.Replace("@pSaleOrderDocumentDate", "'" & strSaleOrderDocumentDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrderDocumentDate__Body", "Sql = strSqlWH_SaleOrder_DocumentDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  Customer No.=?   (for asft300 : MO Body Tab >> Maintain Work Order) #########################
    Private Shared strSqlWH_SaleOrder_ByCustomer As String = "Select " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDC.SaleOrderNo & " " &
        " FROM " & tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & "  " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFBA.tblManufactureOrder_Body & "." & SFBA.MasterItemNo & " " &
        " where " & XMDC.wStandard & " AND " & SFBA.ItemSequence & "<>'999' " &
        " AND " & wStandard & " And " & IMAAL.ent & "='3' AND " & XMDC.CustID & " = @pCustomerId  " &
       " Group by " & MODocNo & " ," & ItemSequence & "," & LineSequence & ", " & MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & Position & " ," & OperationNo & "," & OperationSeq & "," & BOMitem & "," & IssueItem & "," & IssueTimeBucket & ", " &
        " " & EssentialCharacteristics & "," & RecoilMaterial & "," & StandardQPAnumerator & "," & StandardQPAdenominator & "," & AllowErrorRate & ", " &
        " " & RequiredQty & "," & Unit & "," & ConsPurchase & "," & IssuedQty & "," & ScrapQty & "," & CountinglossQuantity & ", " &
        " " & DesignatedIssuanceWarehouse & "," & DesignatedIssuanceLocation & "," & ProductFeature & "," & UsageProb & "," & StandardIssuanceQuantity & ", " &
        " " & AdjustIssuanceQuantity & "," & UnplannedIssued & "," & SETsubstitutionStatus & "," & SETSubstituteGroup & "," & ConsignedMaterial & ", " &
        " " & DesignatedIssuanceBatchNo & "," & DesignatedInventoryManagementFeatures & "," & AllocatedQty & "," & PreparationReasonCode & "," & Bonded & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & XMDC.Item & "," & XMDC.SaleOrderNo & " " &
        " Order By " & ItemSequence & " Asc "
    Public Shared Function GetManufactureOrder_SaleOrder_ByCustomer_Body(strCustomerId As String) As DataTable
        Dim Sql As String = strSqlWH_SaleOrder_ByCustomer
        Sql = Sql.Replace("@pCustomerId", "'" & strCustomerId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_ByCustomer_Body", "Sql = strSqlWH_SaleOrder_ByCustomer", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetManufactureOrder_SaleOrder_ByCustomer_BodyDataSet(strCustomerId As String) As DataSet
        Dim Sql As String = strSqlWH_SaleOrder_ByCustomer
        Sql = Sql.Replace("@pCustomerId", "'" & strCustomerId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_SaleOrder_ByCustomer_BodyDataSet", "Sql = strSqlWH_SaleOrder_ByCustomer", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strSqlWH_BOMitemNo As String = " select sum(" & StandardIssuanceQuantity & ") as " & SUMStandardIssuanceQuantity & ", " &
        " sum(" & IssuedQty & ") as " & SUMIssuedQty & ", sum(" & StandardIssuanceQuantity & ")-sum(" & IssuedQty & ") as " & SUMUnIssuedQty & ", " &
        " sum(" & UnplannedIssued & ") as " & SUMUnplannedIssued & " " &
        "  FROM " & tblManufactureOrder_Body & "  " &
        " where " & wStandard & " AND " & SFBA.BOMitem & " = @pBOMitemNo  "
    Public Shared Function GetManufactureOrder_BOMitemNo_SumStdIssuanceSumIssueQty_Body(strBOMitemNo As String) As DataTable
        Dim Sql As String = strSqlWH_BOMitemNo
        Sql = Sql.Replace("@pBOMitemNo", "'" & strBOMitemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOMitemNo_SumStdIssuanceSumIssueQty_Body", "Sql = strSqlWH_BOMitemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '--Page CheckBOMPopupSub
    '--Sum Undelivery issue / Rrfesh DataTable
    Private Shared SelectSumUnissue As String = "select " & MODocNo & ",substr(" & SFAA.DataConfrmedDate & ",0,10) as Cnfdt,substr(" & SFAA.PlanStartDate & ",0,10) as PlanStrt,substr(" & SFAA.PlanedCompletionDate & ",0,10) as PlanCmp," &
        " " & SFAA.ProductionQty & "," & SFAA.StoredPassQuantity & "," & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & RequiredQty & "," & IssuedQty & "," &
        " sum(nvl(" & RequiredQty & " - " & IssuedQty & ",0)) As IssueQty,sum(nvl(" & SFAA.ProductionQty & " - " & SFAA.StoredPassQuantity & ",0)) As BalMOQty from " & tblManufactureOrder_Body & "" &
        " left join " & SFAA.tblMO & " On " & MODocNo & " = " & SFAA.DocNo & " And " & MasterItemNo & " = " & SFAA.ProductItem & "" &
        " left join " & IMAAL.tblProductionDetail & " On " & BOMitem & " = " & IMAAL.ProductItem & "" &
        " where  " & wStandard & " And" &
        " " & SFAA.wStandard & " And " & SFAA.Released & " And " &
        " " & IMAAL.WStandard & " and " & IMAAL.enUS & " And " &
        " " & BOMitem & " ='@BOMitem'" &
        " group by " & MODocNo & "," & SFAA.DataConfrmedDate & "," & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.ProductionQty & "," & SFAA.StoredPassQuantity & "," &
        " " & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & RequiredQty & "," & IssuedQty & " order by " & MODocNo & ""
    Public Shared Function SumUnissue(ByVal Item As String)
        Dim Oral As String = SelectSumUnissue
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@BOMitem", Item)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page CheckBOMPopupSub
    '--Sum Undelivery MO / Rrfesh DataTable
    Private Shared SelectSumUnMO As String = "select " & MODocNo & ",substr(" & SFAA.DataConfrmedDate & ",0,10) as Cnfdt,substr(" & SFAA.PlanStartDate & ",0,10) as PlanStrt,substr(" & SFAA.PlanedCompletionDate & ",0,10) as PlanCmp," &
        " " & SFAA.ProductionQty & "," & SFAA.StoredPassQuantity & "," & SFAA.ScarpQty & "," & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," &
        " sum(nvl(" & SFAA.ProductionQty & " - " & SFAA.StoredPassQuantity & "-" & SFAA.ScarpQty & ",0)) As BalMOQty from " & tblManufactureOrder_Body & "" &
        " left join " & SFAA.tblMO & " On " & MODocNo & " = " & SFAA.DocNo & " And " & MasterItemNo & " = " & SFAA.ProductItem & "" &
        " left join " & IMAAL.tblProductionDetail & " On " & BOMitem & " = " & IMAAL.ProductItem & "" &
        " where  " & wStandard & " And" &
        " " & SFAA.wStandard & " And " & SFAA.Released & " And " &
        " " & IMAAL.WStandard & " and " & IMAAL.enUS & " And " &
        " " & SFAA.ProductItem & " ='@BOMitem'" &
        " group by " & MODocNo & "," & SFAA.DataConfrmedDate & "," & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.ProductionQty & "," & SFAA.StoredPassQuantity & "," &
        " " & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFAA.ScarpQty & " order by " & MODocNo & ""
    Public Shared Function SumUnMO(ByVal Item As String)
        Dim Oral As String = SelectSumUnMO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@BOMitem", Item)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page  Page CheckBOMPopUp
    '--Sum Undelivery issue / Rrfesh DataTable
    Private Shared SelectSumIssue As String = "select " & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", sum(nvl(" & RequiredQty & " - " & IssuedQty & ",0)) As BalIssueQty from " & tblManufactureOrder_Body & "" &
        " left join " & SFAA.tblMO & " On " & MODocNo & " = " & SFAA.DocNo & " And " & MasterItemNo & " = " & SFAA.ProductItem & "" &
        " left join " & IMAAL.tblProductionDetail & " On " & BOMitem & " = " & IMAAL.ProductItem & "" &
        " where  " & wStandard & " And" &
        " " & SFAA.wStandard & " And " & SFAA.Released & " And " &
        " " & IMAAL.WStandard & " And " & IMAAL.enUS & " And " &
        " " & BOMitem & " ='@BOMitem'" &
        " group by " & BOMitem & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & " order by " & BOMitem & ""
    Public Shared Function SumIssue(ByVal Item As String)
        Dim Oral As String = SelectSumIssue
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@BOMitem", Item)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

End Class

