<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Menu.Master" CodeBehind="Prueba.aspx.cs" Inherits="LOADIMPSA.Prueba" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         <asp:Label ID="lblPrueba" runat="server" Font-Bold="true" />
<asp:DataList ID="dlProducts" runat="server" RepeatColumns="4" CellSpacing="3" RepeatLayout="Table">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hfId" runat="server" />
                                <asp:Label ID="lblCatagory" Text='<%# Eval("Catagory")%>' runat="server" /><br />
                                <asp:Label ID="lblDetails" Text='<%# Eval("Details")%>' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="imgImage" ImageUrl='<%# Eval("Image")%>' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrice" Text='<%# Eval("Price")%>' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTime" runat="server" Font-Bold="true" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>

</asp:Content>