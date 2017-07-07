Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class IMAE
    '# Module : AIM
    Private Shared AIM As String = "AIM"
    '# Table : imae_t     (Header Relationship to Deatil table imaa_t , imaal_t)
    '# aimm215 : Produt Item Property : Header
    '''<reamrks>### Structure Table : Product Item Property Detail ##############</reamrks>
    Public Shared tblProductItem As String = "imae_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> # Basic Date </reamrks>
    Public Shared ItemNo As String = "imae001"
    Public Shared ManufManagGroup As String = "imae011"
    Public Shared Planner As String = "imae012"
    '''<reamrks> # Production Management </reamrks>
    Public Shared ProductType As String = "imae013"
    Public Shared ReceptionIssuenceMech As String = "imae014"
    Public Shared ProdLossRate As String = "imae015"
    Public Shared EssentialProperties As String = "imae023"
    Public Shared ProductUnit As String = "imae016"
    Public Shared ProductionBacthSize As String = "imae017"
    Public Shared MinimumManufQuantity As String = "imae018"
    Public Shared ManufBacthControlmethod As String = "imae019"
    Public Shared ManufExcessDeliverRate As String = "imae020"
    Public Shared ManufCommandExpOption As String = "imae021"
    '''<reamrks> # Work Order PreSet </reamrks>
    Public Shared PresetWorkOrdeType As String = "imae031"
    Public Shared DefaulfBOMfeature As String = "imae037"
    Public Shared ProcessPartNo As String = "imae032"
    Public Shared DefaultProcessNo As String = "imae033"
    Public Shared PresetDeptSupplier As String = "imae034"
    Public Shared PresetCostCenter As String = "imae035"
    Public Shared WorkOrderSepaBacthVolume As String = "imae022"
    Public Shared AllowDemand_Manuf As String = "imae036"
    Public Shared PresetStorageWH As String = "imae041"
    Public Shared PresetStorageLocation As String = "imae042"
    Public Shared StandardLaborHours As String = "imae051"
    Public Shared StandardMachineHours As String = "imae052"
    '''<reamrks> # Production Plans </reamrks>
    Public Shared ExpectedInvalidDays As String = "imae062"
    Public Shared SupplySummaryTimeInterval As String = "imae064"
    Public Shared ProcessBacthTransferVolume As String = "imae077"
    Public Shared WorkOrderPlaningTransferTime As String = "imae078"
    Public Shared MianItemRequirementRetentionDays As String = "imae079"
    Public Shared KeyMaterial As String = "imae080"
    Public Shared FixedManufacturingLeadTime As String = "imae071"
    Public Shared AdjustmentManufacturingLeadTime As String = "imae072"
    Public Shared AdjustmentBacth As String = "imae073"
    Public Shared QCLeadTime As String = "imae074"
    '''<reamrks> # Production Control </reamrks>
    Public Shared IssueUnit As String = "imae081"
    Public Shared IssueBacthSize As String = "imae082"
    Public Shared MinimumIssuanceQuantity As String = "imae083"
    Public Shared IssuanceBacthControlMethod As String = "imae084"
    Public Shared PresetFeedTime_Distance As String = "imae085"
    Public Shared RecoilMaterail As String = "imae091"
    Public Shared TransferBeforeIssuing As String = "imae092"
    Public Shared PresetIssuanceWH As String = "imae101"
    Public Shared PresetIssuanceStorageLocation As String = "imae102"
    Public Shared PresetIssuanceStorageWH As String = "imae013"
    Public Shared PresetReturnStorageLocation As String = "imae014"
    '''<reamrks> Adjustment information </reamrks>
    Public Shared DataOwner As String = "imaeownid"
    Public Shared DataOwnerDept As String = "imaeowndp"
    Public Shared DataCreateBy As String = "imaecrtid"
    Public Shared DataCreateByDept As String = "imaecrtdp"
    Public Shared DataCreateDate As String = "imaecrtdt"
    Public Shared DataModifyBy As String = "imaemodid"
    Public Shared LastModifyDate As String = "imaemoddt"
    Public Shared DataConfirmationPersonal As String = "imaecnfid"
    Public Shared DataConfirmedDate As String = "imaecnfdt"
    ' Public Shared wStandard As String = "imaecnfdt"


    '''<reamrks> Condition Field </reamrks>
    Private Shared Site As String = "imaesite"
    Public Shared ent As String = "imaeent"
    Public Shared wStandard As String = Site & " = 'JINPAO' AND " & ent & "='3' "

    '#################### Get  Where 1  Production Item_No. ='?' ##############################################################################################
    Private Shared SqlProductItemNo As String = "Select " & ItemNo & "," & ManufManagGroup & "," & Planner & "," & ProductType & "," & ReceptionIssuenceMech & "," & ProdLossRate & ", " &
    "  " & EssentialProperties & "," & ProductUnit & "," & ProductionBacthSize & "," & MinimumManufQuantity & "," & ManufBacthControlmethod & "," & ManufExcessDeliverRate & ", " &
    "  " & ManufCommandExpOption & "," & PresetWorkOrdeType & "," & DefaulfBOMfeature & "," & ProcessPartNo & "," & DefaultProcessNo & "," & PresetDeptSupplier & "," & PresetCostCenter & ", " &
    "  " & WorkOrderSepaBacthVolume & "," & AllowDemand_Manuf & "," & PresetStorageWH & "," & PresetStorageLocation & "," & StandardLaborHours & "," & StandardMachineHours & ", " &
    "  " & ExpectedInvalidDays & "," & SupplySummaryTimeInterval & "," & ProcessBacthTransferVolume & "," & WorkOrderPlaningTransferTime & "," & MianItemRequirementRetentionDays & "," & KeyMaterial & ", " &
    "  " & FixedManufacturingLeadTime & "," & AdjustmentManufacturingLeadTime & "," & AdjustmentBacth & "," & QCLeadTime & "," & IssueUnit & "," & IssueBacthSize & "," & MinimumIssuanceQuantity & ", " &
    "  " & IssuanceBacthControlMethod & "," & PresetFeedTime_Distance & "," & RecoilMaterail & "," & TransferBeforeIssuing & "," & PresetIssuanceWH & "," & PresetIssuanceStorageLocation & ", " &
    "  " & PresetIssuanceStorageWH & "," & PresetReturnStorageLocation & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    "  " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblProductItem & "   where " & wStandard & " AND " & ItemNo & " =@pItemNo "
    Public Shared Function GetProductItemNo(strItem_No As String) As DataTable
        Dim Sql As String = SqlProductItemNo.Replace("@pItemNo", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNo", "Sql = SqlProductItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetProductItemNo_DataSet(strItem_No As String) As DataSet
        Dim Sql As String = SqlProductItemNo.Replace("@pItemNo", "'" & strItem_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNo_DataSet", "Sql = SqlProductItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  Create By EpmId. ='?' ##############################################################################################
    Private Shared SqlProductItemNoByEpmId As String = "Select " & ItemNo & "," & ManufManagGroup & "," & Planner & "," & ProductType & "," & ReceptionIssuenceMech & "," & ProdLossRate & ", " &
    "  " & EssentialProperties & "," & ProductUnit & "," & ProductionBacthSize & "," & MinimumManufQuantity & "," & ManufBacthControlmethod & "," & ManufExcessDeliverRate & ", " &
    "  " & ManufCommandExpOption & "," & PresetWorkOrdeType & "," & DefaulfBOMfeature & "," & ProcessPartNo & "," & DefaultProcessNo & "," & PresetDeptSupplier & "," & PresetCostCenter & ", " &
    "  " & WorkOrderSepaBacthVolume & "," & AllowDemand_Manuf & "," & PresetStorageWH & "," & PresetStorageLocation & "," & StandardLaborHours & "," & StandardMachineHours & ", " &
    "  " & ExpectedInvalidDays & "," & SupplySummaryTimeInterval & "," & ProcessBacthTransferVolume & "," & WorkOrderPlaningTransferTime & "," & MianItemRequirementRetentionDays & "," & KeyMaterial & ", " &
    "  " & FixedManufacturingLeadTime & "," & AdjustmentManufacturingLeadTime & "," & AdjustmentBacth & "," & QCLeadTime & "," & IssueUnit & "," & IssueBacthSize & "," & MinimumIssuanceQuantity & ", " &
    "  " & IssuanceBacthControlMethod & "," & PresetFeedTime_Distance & "," & RecoilMaterail & "," & TransferBeforeIssuing & "," & PresetIssuanceWH & "," & PresetIssuanceStorageLocation & ", " &
    "  " & PresetIssuanceStorageWH & "," & PresetReturnStorageLocation & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    "  " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblProductItem & "   where " & wStandard & " AND " & DataCreateBy & " =@pEpmId "
    Public Shared Function GetProductItemNoByEpmId(strEpmId As String) As DataTable
        Dim Sql As String = SqlProductItemNoByEpmId.Replace("@pEpmId", "'" & strEpmId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNoByEpmId", "Sql = SqlProductItemNoByEpmId", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetProductItemNoByEpmId_DataSet(strEpmId As String) As DataSet
        Dim Sql As String = SqlProductItemNoByEpmId.Replace("@pEpmId", "'" & strEpmId & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNoByEpmId_DataSet", "Sql = SqlProductItemNoByEpmId", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '#################### Get  Where 1  Create Date BETWEEN. StartDate='?' and EnsDate= '?'  ##############################################################################################
    Private Shared SqlProductItemNoCreateDate As String = "Select " & ItemNo & "," & ManufManagGroup & "," & Planner & "," & ProductType & "," & ReceptionIssuenceMech & "," & ProdLossRate & ", " &
    "  " & EssentialProperties & "," & ProductUnit & "," & ProductionBacthSize & "," & MinimumManufQuantity & "," & ManufBacthControlmethod & "," & ManufExcessDeliverRate & ", " &
    "  " & ManufCommandExpOption & "," & PresetWorkOrdeType & "," & DefaulfBOMfeature & "," & ProcessPartNo & "," & DefaultProcessNo & "," & PresetDeptSupplier & "," & PresetCostCenter & ", " &
    "  " & WorkOrderSepaBacthVolume & "," & AllowDemand_Manuf & "," & PresetStorageWH & "," & PresetStorageLocation & "," & StandardLaborHours & "," & StandardMachineHours & ", " &
    "  " & ExpectedInvalidDays & "," & SupplySummaryTimeInterval & "," & ProcessBacthTransferVolume & "," & WorkOrderPlaningTransferTime & "," & MianItemRequirementRetentionDays & "," & KeyMaterial & ", " &
    "  " & FixedManufacturingLeadTime & "," & AdjustmentManufacturingLeadTime & "," & AdjustmentBacth & "," & QCLeadTime & "," & IssueUnit & "," & IssueBacthSize & "," & MinimumIssuanceQuantity & ", " &
    "  " & IssuanceBacthControlMethod & "," & PresetFeedTime_Distance & "," & RecoilMaterail & "," & TransferBeforeIssuing & "," & PresetIssuanceWH & "," & PresetIssuanceStorageLocation & ", " &
    "  " & PresetIssuanceStorageWH & "," & PresetReturnStorageLocation & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & ", " &
    "  " & DataModifyBy & "," & LastModifyDate & "," & DataConfirmationPersonal & "," & DataConfirmedDate & "  " &
    "  FROM " & tblProductItem & "   where " & wStandard & " AND " & DataCreateDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetProductItemNoByCreateDate(Sdate As String, Edate As String) As DataTable
        Dim Sql As String = SqlProductItemNoCreateDate
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNoByCreateDate", "Sql = SqlProductItemNoCreateDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetProductItemNoByCreateDate_DataSet(Sdate As String, Edate As String) As DataSet
        Dim Sql As String = SqlProductItemNoCreateDate
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIM, "IMAE", "GetProductItemNoByCreateDate_DataSet", "Sql = SqlProductItemNoCreateDate", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
