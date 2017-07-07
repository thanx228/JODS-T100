<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Demo.aspx.vb" Inherits="MIS_T100.Demo" %>
<%@ Register Src="~/UserControl/Normal/UsingDept.ascx" TagPrefix="uc1" TagName="UsingDept" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/Normal/UsingInventoryScarpType .ascx" TagPrefix="uc1" TagName="UsingInventoryScarpType" %>
<%@ Register Src="~/UserControl/Normal/UsingMat_IssueOverType.ascx" TagPrefix="uc1" TagName="UsingMat_IssueOverType" %>
<%@ Register Src="~/UserControl/Normal/UsingMat_IssueType.ascx" TagPrefix="uc1" TagName="UsingMat_IssueType" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_ReceiptType.ascx" TagPrefix="uc1" TagName="UsingMO_ReceiptType" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_TypeGenBatch.ascx" TagPrefix="uc1" TagName="UsingMO_TypeGenBatch" %>
<%@ Register Src="~/UserControl/Normal/UsingReworkOverType.ascx" TagPrefix="uc1" TagName="UsingReworkOverType" %>
<%@ Register Src="~/UserControl/Normal/UsingReworkType.ascx" TagPrefix="uc1" TagName="UsingReworkType" %>
<%@ Register Src="~/UserControl/Normal/UsingStoreInventoryOutType.ascx" TagPrefix="uc1" TagName="UsingStoreInventoryOutType" %>
<%@ Register Src="~/UserControl/Normal/UsingStoreInventoryType.ascx" TagPrefix="uc1" TagName="UsingStoreInventoryType" %>
<%@ Register Src="~/UserControl/Normal/UsingTransferType.ascx" TagPrefix="uc1" TagName="UsingTransferType" %>
<%@ Register Src="~/UserControl/Normal/UsingWH_Store.ascx" TagPrefix="uc1" TagName="UsingWH_Store" %>
<%@ Register Src="~/UserControl/Normal/UsingWorkstation.ascx" TagPrefix="uc1" TagName="UsingWorkstation" %>
<%@ Register Src="~/UserControl/Multiple/UsingDept_checkList.ascx" TagPrefix="uc1" TagName="UsingDept_checkList" %>
<%@ Register Src="~/UserControl/Multiple/UsingDocTypeSaleCheckList.ascx" TagPrefix="uc1" TagName="UsingDocTypeSaleCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingInventoryScarpTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingInventoryScarpTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingMat_IssueTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingMat_IssueTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingMO_ReceiptTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingMO_ReceiptTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingMOTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingMOTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingMOTypeGenBacthCheckList.ascx" TagPrefix="uc1" TagName="UsingMOTypeGenBacthCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingReworkTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingReworkTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStoreInventoryCheckList.ascx" TagPrefix="uc1" TagName="UsingStoreInventoryCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStoreInventoryOutCheckList .ascx" TagPrefix="uc1" TagName="UsingStoreInventoryOutCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingTransferTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingTransferTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingWH_storeCheckList.ascx" TagPrefix="uc1" TagName="UsingWH_storeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusMO_Normal.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusBOM_ItemMaster.ascx" TagPrefix="uc1" TagName="UsingStatusBOM_ItemMaster" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusItemRoutingChange.ascx" TagPrefix="uc1" TagName="UsingStatusItemRoutingChange" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusStore_IQC.ascx" TagPrefix="uc1" TagName="UsingStatusStore_IQC" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusMO_Normal_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal_checkList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusBOM_ItemMaster_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusBOM_ItemMaster_checkList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusItemRoutingChange_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusItemRoutingChange_checkList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusStore_IQC_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusStore_IQC_checkList" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>
<%@ Register Src="~/UserControl/Normal/UsingItemMasterGroup.ascx" TagPrefix="uc2" TagName="UsingItemMasterGroup" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <uc2:UsingItemMasterGroup runat="server" ID="UsingItemMasterGroup" />
        </div>
        <div>

            <uc1:DateT100 runat="server" ID="DateForm" /><br />
            <uc1:DateT100 runat="server" ID="DateTo" />
        </div>
        <div>
            <asp:TextBox ID="txtSaleOrcerNo" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Call SaleDelivery Header" />
            <asp:Label ID="lblCkeckCommandSql" runat="server" Text=""></asp:Label>
            <hr />
            <h1>Between</h1>
            <asp:TextBox ID="txtForm" runat="server"></asp:TextBox> to <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
            <asp:Button ID="btnBetween" runat="server" Text="Call Data" />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
<div style="background-color:beige;">
       Status MO Normal: <uc1:UsingStatusMO_Normal runat="server" id="UsingStatusMO_Normal" /><br />
       Status BOM_ItemMaster : <uc1:UsingStatusBOM_ItemMaster runat="server" id="UsingStatusBOM_ItemMaster" /><br />
       Status ItemRoutingChange : <uc1:UsingStatusItemRoutingChange runat="server" id="UsingStatusItemRoutingChange" /><br />
       Status Store_IQC : <uc1:UsingStatusStore_IQC runat="server" id="UsingStatusStore_IQC" /><hr />
    <uc1:UsingStatusMO_Normal_checkList runat="server" id="UsingStatusMO_Normal_checkList" /><hr />
    <uc1:UsingStatusBOM_ItemMaster_checkList runat="server" id="UsingStatusBOM_ItemMaster_checkList" /><hr />
    <uc1:UsingStatusItemRoutingChange_checkList runat="server" id="UsingStatusItemRoutingChange_checkList" /><hr />
    <uc1:UsingStatusStore_IQC_checkList runat="server" id="UsingStatusStore_IQC_checkList" />
</div>
        <hr />
    <div>
        <uc1:UsingDept runat="server" ID="UsingDept" /><br />
        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" /><br />
        <uc1:UsingInventoryScarpType runat="server" ID="UsingInventoryScarpType" /><br />
        <uc1:UsingMat_IssueOverType runat="server" ID="UsingMat_IssueOverType" /><br />
        <uc1:UsingMat_IssueType runat="server" ID="UsingMat_IssueType" /><br />
        <uc1:UsingMO_ReceiptType runat="server" ID="UsingMO_ReceiptType" /><br />
        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" /><br />
        <uc1:UsingMO_TypeGenBatch runat="server" ID="UsingMO_TypeGenBatch" /><br />
        <uc1:UsingReworkOverType runat="server" ID="UsingReworkOverType" /><br />
        <uc1:UsingReworkType runat="server" ID="UsingReworkType" /><br />
        <uc1:UsingStoreInventoryOutType runat="server" ID="UsingStoreInventoryOutType" /><br />
        <uc1:UsingStoreInventoryType runat="server" ID="UsingStoreInventoryType" /><br />
        <uc1:UsingTransferType runat="server" ID="UsingTransferType" /><br />
        <uc1:UsingWH_Store runat="server" ID="UsingWH_Store" /><br />
        <uc1:UsingWorkstation runat="server" ID="UsingWorkstation" />
<hr />
        <uc1:UsingDept_checkList runat="server" id="UsingDept_checkList" /><br />
        <uc1:UsingDocTypeSaleCheckList runat="server" id="UsingDocTypeSaleCheckList" /><br />
        <uc1:UsingInventoryScarpTypeCheckList runat="server" id="UsingInventoryScarpTypeCheckList" /><br />
        <uc1:UsingMat_IssueTypeCheckList runat="server" id="UsingMat_IssueTypeCheckList" /><br />
        <uc1:UsingMO_ReceiptTypeCheckList runat="server" id="UsingMO_ReceiptTypeCheckList" /><br />
        <uc1:UsingMOTypeCheckList runat="server" id="UsingMOTypeCheckList" /><br />
        <uc1:UsingMOTypeGenBacthCheckList runat="server" id="UsingMOTypeGenBacthCheckList" /><br />
        <uc1:UsingReworkTypeCheckList runat="server" id="UsingReworkTypeCheckList" /><br />
        <uc1:UsingStoreInventoryCheckList runat="server" id="UsingStoreInventoryCheckList" /><br />
        <uc1:UsingStoreInventoryOutCheckList runat="server" id="UsingStoreInventoryOutCheckList" /><br />
        <uc1:UsingTransferTypeCheckList runat="server" id="UsingTransferTypeCheckList" /><br />
        <uc1:UsingWH_storeCheckList runat="server" id="UsingWH_storeCheckList" /><br />
        <uc1:UsingWorkstationCheckList runat="server" id="UsingWorkstationCheckList" />
    </div>
    </form>
</body>
</html>
