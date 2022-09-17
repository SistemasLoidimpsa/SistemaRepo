<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ParametrosCorporativos.aspx.cs" Inherits="LOADIMPSA.ParametrosCorporativos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <style>
          .hideGridColumn {
            display: none;
        }

        .header {
            background-color: #1D4289;
            font-weight: bold;
            color: white;
            text-align: center;
            align-content: center;
        }

        .MiButton {
            border-color: #1D4289;
            border-style: solid;
            color: white;
            font-family: 'Montserrat';
            font-weight: bold;
            background: #243D75;
        }
        .tituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #fff;
            font-size: 20px;
        }

        .subtituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 9px;
            font-weight: bold;
        }

        .textboxNormal {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 9px;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        #overlay {
            position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: #000;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=20);
            opacity: 0.2;
        }

        #theprogress {
            padding: 10px;
            background-color: #000;
            filter: Alpha(Opacity=20);
            line-height: 30px;
            text-align: center;
        }

        #modalprogress {
            top: 40%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
            position: fixed;
            z-index: 999;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            position: center;
            left: 15%;
            right: 15%;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            width: 200px;
            height: 350px;
        }

        .auto-style1 {
            height: 25px;
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 44 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
                return true;
            }
        }

        function isNumberKeyDecimal(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
                return true;
            }
        }

        function ValidaComas(value) {
            if (value.includes(",")) {
                return false;
            }
        }
    </script>

    <link href="css/GridStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppTracking">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress" style="width: 250px; height: 250px">
                    <div id="theprogress">
                        <asp:Label ID="imgLoader" runat="server" Text=" Por favor espere......" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uppTracking" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" HorizontalAlign="Center" Width="100%">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="PARÁMETROS CORPORATIVOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <asp:Panel runat="server" HorizontalAlign="Center" Width="98%">
                    <asp:GridView runat="server" ID="dtgParametrosCorporativos" AutoGenerateColumns="False"
                        Width="90%" HorizontalAlign="Center" OnRowDeleting="dtgParametrosCorporativos_RowDeleting" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                        CssClass="mGrid"
                        PageSize="7">
                        <Columns>
                            <asp:BoundField DataField="codigoparametro" HeaderText="Codigo" />
                            <asp:BoundField DataField="nombrecodificado" HeaderText="Nombre del Parametro" />
                            <asp:BoundField DataField="nombre" HeaderText="Descripcion" />
                            <asp:BoundField DataField="valorint" HeaderText="Valor Entero" />
                            <asp:BoundField DataField="valorchar" HeaderText="Valor Cadena" />
                            <asp:BoundField DataField="valordate" HeaderText="Valor Fecha" />
                            <asp:BoundField DataField="valordecimal" HeaderText="Valor Decimal" />
                            <asp:TemplateField HeaderText="Actualizar Parámetro">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnActualizar" OnClick="btnActualizar_Click"
                                        CommandArgument='<%# Eval("codigoparametro")+";"+Eval("nombrecodificado")+";"+Eval("valorint")+";"+Eval("valorchar")+";"+Eval("valordate")+";"+Eval("valordecimal")%>'
                                        ImageUrl="~/images/update.png" ToolTip="Actualizar Parámetro" Width="20px" />
                                </ItemTemplate>
                                <ControlStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#D5D8DC" />
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <br />
            <br />
            <asp:HiddenField ID="hddCodigo" runat="server" />
            <asp:HiddenField ID="hddIdentificacion" runat="server" />
            <asp:Button ID="hButton" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlPopup" runat="server" Width="650px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr style="background-color: #1D4289">
                                <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                    <asp:Label ID="lblTituloV" Text="ACTUALIZAR PARÁMETRO CORPORATIVO" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 2em">
                                    <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Text="Ingrese los valores indicados:"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Valor Entero:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtValorEntero" runat="server" Width="35%" onkeypress="return isNumberKeyDecimal(event)" ></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="Valor Cadena:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtValorCadena" runat="server" Width="80%" Rows="2"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="Valor Fecha:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtFecha" runat="server" Width="35%" Rows="2" TextMode="Date"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label4" runat="server" Text="Valor decimal:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtValorDecimal" runat="server" Width="35%" Rows="2" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <div style="margin-left: auto; margin-right: auto; text-align: center">
                            <br />
                            <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: right">
                            <br />
                            <asp:Button ID="btnGuardarCambios" runat="server" OnClick="btnGuardarCambios_Click" Font-Size="Small" Text="Guardar Cambios" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnClose" runat="server" Text="Cancelar" BackColor="Red" Font-Size="Small" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <br />
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPnlCustomerDetail">
                    <ProgressTemplate>
                        <div id="Background"></div>
                        <div id="Progress" style="color: darkred">
                            Por favor espere ......
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="hButton" PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="modalBackground" BehaviorID="_content_mdlPopup" DynamicServicePath="" />



        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
