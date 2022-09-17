<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Menu.Master" CodeBehind="ReporteCasilleros.aspx.cs" Inherits="LOADIMPSA.ReporteCasilleros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .wrapper {
            border: 2px;
            overflow: hidden;
        }

            .wrapper div {
                min-height: 200px;
                padding: 10px;
            }

        #one {
            float: left;
            margin-right: 40px;
            width: 450px;
            border-right: 2px;
        }

        #two {
            overflow: hidden;
            width: 630px;
        }

        #three {
            float: left;
            margin-right: 30px;
            width: 450px;
            border-right: 2px;
            overflow: hidden;
            width: 630px;
        }

        @media screen and (max-width: 400px) {
            #one {
                float: none;
                margin-right: 0;
                width: auto;
                border: 0;
                border-bottom: 2px;
            }
        }

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
            left: 20%;
            right: 20%;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            width: 200px;
            height: 350px;
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
            font-size: 9px;
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppReporteCliente">
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
    <asp:UpdatePanel ID="uppReporteCliente" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" HorizontalAlign="Center" Width="100%">
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <asp:Label runat="server" Text="REPORTE CLIENTES" Font-Bold="true" CssClass="tituloHeader" ForeColor="#1D4289"></asp:Label>
                </div>
                <br />
                <div style="text-align: center; width: 100%; margin-right: auto; margin-left: auto">
                    <table style="width: 50%">
                        <tr>
                            <td style="width: 25%; text-align: left; margin-left: auto; margin-right: auto" colspan="3">
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="Lista de Años:  " CssClass="subtituloHeader"></asp:Label>

                                <asp:DropDownList ID="ddlstrAnio" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">

                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>

                                </asp:DropDownList>
                                <br />
                                <br />
                            </td>
                            <td>
                                <asp:Button ID="Button1" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlCharCantidadClientes" runat="server" HorizontalAlign="Center" Width="100%" Height="600px" ScrollBars="Auto">
                <br />
                <br />
                <div class="wrapper">

                    <div id="one">
                        <asp:Chart ID="CharClientesAnual" runat="server" Width="505px" OnLoad="CharClientesAnual_Load" EnableViewState="True" IsMapAreaAttributesEncoded="True">
                            <Titles>
                                <asp:Title Font="Arial, 14pt, style=Bold" ForeColor="#1d4289" Name="Title1"
                                    Text="TOTAL DE CASILLEROS MENSUALES">
                                </asp:Title>
                            </Titles>
                            <Series>
                                <asp:Series Name="ReporteCasilleros" XValueMember="0" YValueMembers="2" IsValueShownAsLabel="true" IsXValueIndexed="True" CustomProperties="DrawingStyle=LightToDark, TextOrientation=TopToBottom, LabelStyle=Center, MaxPixelPointWidth=40" Palette="Pastel" XValueType="Double">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="CantidadClientes">
                                    <AxisX Interval="1"></AxisX>
                                    <%--<AxisY IntervalAutoMode="VariableCount">
                        </AxisY>--%>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                    <div id="two">
                        <asp:Chart ID="ChartPesoAnual" runat="server" Width="639px" BackImageAlignment="BottomRight" ToolTip="TOTAL DE CASILLEROS MENSUALES">
                            <Series>

                              
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="CantidadPeso">
                                    <%--<AxisY IntervalAutoMode="VariableCount">
                        </AxisY>--%>
                                    <AxisX Interval="1">
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1">
                                </asp:Legend>
                            </Legends>
                        </asp:Chart>
                    </div>

                    <div id="three">
                        <asp:Chart ID="CharClientesCategoria" runat="server" Width="639px">
                            <Series>
                               
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ClientesCategoria">
                                    <AxisX Interval="1">
                                    </AxisX>
                                    <%--<AxisY IntervalAutoMode="VariableCount">
                        </AxisY>--%>
                                </asp:ChartArea>
                            </ChartAreas>
                             <Legends>
                                <asp:Legend Name="Legend1">
                                </asp:Legend>
                            </Legends>
                        </asp:Chart>
                    </div>
                    </div>
            </asp:Panel>
            <br />
            <br />

            <asp:Panel ID="pnlPopup" runat="server" Width="650px" Height="100%" Style="display: none" CssClass="modalPopup">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr style="background-color: #1D4289">
                                <td colspan="2" style="width: 100%; height: 100%; text-align: center; background-color: #1D4289">
                                    <asp:Label ID="lblTituloV" Text="REPORTE CASILLEROS" CssClass="header" runat="server" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                    <asp:Label ID="lbltitulo2" runat="server" CssClass="header" BackColor="#1D4289" Font-Size="Larger" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: justify; padding: 2em">
                                    <asp:Label ID="Label5" runat="server" CssClass="subtituloHeader" Text="Escoja el año y de clic en buscar"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="Lista de Años:" CssClass="subtituloHeader"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Lista de Años:  " CssClass="subtituloHeader"></asp:Label>

                                    <asp:DropDownList ID="DropDownList1" class="textboxNormal" runat="server" CssClass="form-control" Width="80%">
                                        <asp:ListItem Text="Seleccione" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                        <asp:ListItem Text="2021" Value="2021"></asp:ListItem>

                                    </asp:DropDownList>

                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" Font-Size="X-Small" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg" BackColor="#1D4289" OnClick="btnBuscar_Click" />

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



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
