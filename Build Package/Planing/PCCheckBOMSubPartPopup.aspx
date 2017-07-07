﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PCCheckBOMSubPartPopup.aspx.vb" Inherits="MIS_T100.PCCheckBOMSubPartPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>Detail Plan delivery stock</title>
    <style type="text/css">
        .style1
        {
            width: 124px;
        }
        .style2
        {
            width: 214px;
        }
        .auto-style2 {
            width: 325px;
        }
        .auto-style3 {
            width: 158px;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            width: 109px;
        }
        .auto-style6 {
            width: 96px;
        }
        .auto-style7 {
            width: 195px;
        }
        .auto-style8 {
            width: 246px;
        }
        .auto-style9 {
            width: 112px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg'); background-repeat: repeat">
    <form id="form1" runat="server">
    <div>
    
        <table >
            <tr>
                <td colspan="3" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label16" runat="server" Font-Size="X-Large" ForeColor="Blue" 
                        Text="BOM Detail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #b0a9a9;" 
                    class="auto-style3">
                    <asp:Label ID="Label21" runat="server" Text="MasterItemNo."></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #b0a9a9;" class="auto-style2">
                    <asp:Label ID="lbItem" runat="server" Text="ItemName"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #b0a9a9;" class="auto-style2">
                    <asp:Label ID="Label22" runat="server" Text="Specifaction"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                    class="auto-style3">
                    <asp:Label ID="lblMasterItemNo" runat="server" ForeColor="#000099" ></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;" >
                    <asp:Label ID="lbItemName" runat="server" ForeColor="#000099"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lblSpec" runat="server" ForeColor="#000099"></asp:Label>
                </td>
            </tr>
            </table>

        <asp:Label ID="Label1" runat="server" Text="จำนวนรายการ BOM SubPart Detail"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountBOMsubpart" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvBOMDetail" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">
            <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlBOMSubPart" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="bmba009" HeaderText="LineNo" >
                 <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="bmba003" HeaderText="ChildItemNo" />
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMItemName" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specifaction">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMSpecifaction" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemCategory">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMItemCategory" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supply Statregy">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMItemSupplyStatregy" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="bmba011" HeaderText="QPA" DataFormatString="{0:N3}" >
                 <HeaderStyle HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                 <asp:BoundField DataField="bmba012" HeaderText="Denominator" DataFormatString="{0:N3}" >
                 <HeaderStyle HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                 <asp:BoundField DataField="bmba010" HeaderText="IssueUnit" >
                 <HeaderStyle HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                 <asp:BoundField DataField="bmba005" HeaderText="EffectiveDateTime" DataFormatString="{0:yyyy/MM/dd}" >
                 <HeaderStyle HorizontalAlign="Center" />
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:TemplateField HeaderText="In Stock">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMinStock" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMIssue_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UniIssued Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMUnIssue_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MO Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMMO_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMPO_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PR Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMPR_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SO Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMSO_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Capacity">
                    <ItemTemplate>
                        <asp:Label ID="lblBOMCapacity" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
   
    </div>
    </form>
</body>
</html>
