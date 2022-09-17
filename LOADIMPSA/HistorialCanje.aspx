<%@ Page Language="C#"  Async="true"  AutoEventWireup="true" MasterPageFile="~/Menu.Master" CodeBehind="HistorialCanje.aspx.cs" Inherits="LOADIMPSA.HistorialCanje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            top: 30%;
            left: 30%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 14px;
            position: fixed;
            z-index: 999;
        }

        .grdSearchResultbreakword {
            word-wrap: break-word;
            word-break: break-all;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            position: center;
            left: 5%;
            right: 55%;
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
    <link href="css/GridStyle.css" rel="stylesheet" />
    <style>
        .tituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #fff;
            font-size: 20px;
        }

        .subtituloHeader {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 11px;
            font-weight: bold;
        }

        .textboxNormal {
            font-family: 'Montserrat', sans-serif;
            color: #1D4289;
            font-size: 9px;
            font-weight: bold;
        }


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
    </style>
    <style type="text/css">
        .cssContent {
            background-color: #D3DEEF;
            font-family: 'Montserrat', sans-serif;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 98%;
            height: 100%;
        }

        .cssHeaderSelected {
            background-color: #1D4289;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 50px;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_minus.png);
            background-repeat: no-repeat;
            background-position: right;
        }

        .cssHeader {
            background-color: #009CDE;
            border: 1px solid #2F4F4F;
            font-family: 'Montserrat', sans-serif;
            color: white;
            cursor: pointer;
            font-size: medium;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 98%;
            height: 30PX%;
            background-image: url(http://cdn1.iconfinder.com/data/icons/fatcow/32/bullet_toggle_plus.png);
            background-repeat: no-repeat;
            background-position: right;
        }
         .float {
            color: #fff;
            position: fixed;
            width: 60px;
            height: 60px;
            bottom: 40px;
            right: 40px;
            background-color: #1D4289;
            
            border-radius: 50px;
            text-align: center;
            font-size: 30px;
            box-shadow: 2px 2px 3px #999;
            z-index: 100;
        }

            .float:hover {
                text-decoration: none;
               
                background-color:#1D4289;
                 color: #fff;
            }

        .my-float {
            margin-top: 16px;   
            color: #fff;
             
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

        function ValidaComas(value) {
            if (value.includes(",")) {
                return false;
            }
        }

        function MuestraOculta(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "images/close.gif";
                }
                else {
                    img.src = "images/close.gif";
                }
                img.alt = "Ocultar detalles";
            }
            else {
                //const strMaterias = document.getElementById('_materias').value;
                //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "images/detail.gif";
                }
                else {
                    img.src = "images/detail.gif";
                }
                img.alt = "Mostrar detalles";
            }
        }


        function VerFile(nameFile) {
            console.log(cedula + nameFile);

            /*   var div = document.getElementById(obj);
               var img = document.getElementById('img' + obj);
   
               if (div.style.display == "none") {
                   //document.getElementById('_materias').value = document.getElementById('_materias').value + ";" + img.title;
                   div.style.display = "block";
                   if (row == 'alt') {
                       img.src = "images/close.gif";
                   }
                   else {
                       img.src = "images/close.gif";
                   }
                   img.alt = "Ocultar detalles";
               }
               else {
                   //const strMaterias = document.getElementById('_materias').value;
                   //document.getElementById('_materias').value = strMaterias.replace(";" + img.title, '');
                   div.style.display = "none";
                   if (row == 'alt') {
                       img.src = "images/detail.gif";
                   }
                   else {
                       img.src = "images/detail.gif";
                   }
                   img.alt = "Mostrar detalles";
               }*/
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppListadoTracking">
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
    <asp:UpdatePanel ID="uppListadoTracking" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="HISTORIAL DE PRODUCTOS CANJEADOS" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                      <br />
                <br />
                <asp:Panel ID="pnlBusquedaClientesCanjeo" runat="server" HorizontalAlign="Center">
                    <asp:Panel runat="server" DefaultButton="btnBuscar" HorizontalAlign="Center">
                        <asp:TextBox runat="server" ID="txtCliente" Width="250px" class="textboxNormal" placeholder="Identificación - Nombres" />
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary btn-lg" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnBuscar_Click" />
                    </asp:Panel>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel runat="server" ID="pnlClientes" Width="100%" HorizontalAlign="Center" ScrollBars="Vertical">
                    <asp:Panel runat="server" Width="50%" HorizontalAlign="Center" Style="margin-left: auto; margin-right: auto">
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid"
                            BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="numeroIdentificacion" HeaderText="Identificación">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="cod_usu" HeaderText="Usuario Cliente">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#D5D8DC" />
                            <AlternatingRowStyle CssClass="alt" />
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <br />
                 <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hddIdentificacion" runat="server" />
                <asp:Panel ID="pnlListadoHistorial" runat="server" HorizontalAlign="Center" Height="800px" Width="100%" Visible="false" ScrollBars="Vertical">
                     <br />
          
                      <asp:Label ID="lblPuntos" runat="server" CssClass="subtituloHeader" Font-Size="10px"></asp:Label> &nbsp &nbsp &nbsp         
                    <asp:Button ID="Button1" Text="Credito/Debio" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" Style="margin-left: 10px" Visible="false" BackColor="#1D4289" OnClick="btnDebiCredi" />
                    <br />
                    <br />
                      <asp:Panel runat="server" Width="70%" HorizontalAlign="Center" Style="margin-left: auto; margin-right: auto">
                    <asp:GridView ID="gvClientesHist" runat="server" HorizontalAlign="Center"
                        Font-Size="X-Small"
                        AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="dtgHistorial_RowDataBound"
                        ShowHeaderWhenEmpty="True"
                        CssClass="mGrid"
                        EmptyDataText="No hay Historial a Presentar.">
                        <Columns>

                            <asp:BoundField DataField="clasePuntos" HeaderText="MOVIMIENTOS PUNTOS" />

                            <asp:BoundField DataField="descripHist" HeaderText="DESCRIPCION" />

                            <asp:BoundField DataField="puntosHist" HeaderText="PUNTOS" />

                            <asp:BoundField DataField="fechaCanjeoHist" HeaderText="FECHA REALIZADO" />

                        </Columns>
                        <FooterStyle BackColor="#D5D8DC" />
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                              </asp:Panel>
                </asp:Panel>

                
            </asp:Panel>
                        <asp:Button ID="hButton" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:Panel ID="pnlRevisar" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%">
                                <tr style="background-color: #1D4289">
                                    <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                        <asp:Label ID="Label7" Text="DEBITO/CREDITO:" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                        <asp:Label ID="lblorden" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label11" runat="server" CssClass="subtituloHeader" Text="Accion a Realizar:"></asp:Label>


                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAccion" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="pagoConfir_SelectedIndexChanged">
                                            <asp:ListItem Text="DEBITO" Value="DEBITO"></asp:ListItem>
                                            <asp:ListItem Text="CREDITO" Value="CREDITO"></asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label9" runat="server" CssClass="subtituloHeader" Text="Valor:"></asp:Label>
                                     
                                        
                                        <br />
                                    </td>
                                      <td>
                                            <asp:TextBox ID="txtValor" runat="server" Width="50%" ></asp:TextBox>
                                      </td>

                                </tr>
                                <tr>
                                    <td style="text-align: justify; padding: 1em">
                                        <asp:Label ID="Label8" runat="server" CssClass="subtituloHeader" Text="Comentario para Cliente:"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtObservacion2" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                                    </td>

                                </tr>

                            </table>
                        </asp:Panel>


                        <div style="margin-left: auto; margin-right: auto; text-align: center">
                            <br />
                            <asp:Label ID="lblErrores" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: right">
                            <br />

                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Font-Size="Small" Text="Guardar" BackColor="#1D4289" CssClass="btn btn-primary btn-lg" Visible="true" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnClose" runat="server" Text="Cancelar" BackColor="Black" Font-Size="Small" CssClass="btn btn-primary btn-lg" Visible="true" />
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

               <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
<a href="https://wa.me/593992282734/?text=Hola,%20tengo%20una%20consulta." class="float" target="_blank">
<i class="fa fa-whatsapp my-float"></i>
</a>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
