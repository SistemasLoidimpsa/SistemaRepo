<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="LOADIMPSA.MenuPrincipal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat' rel='stylesheet' type='text/css' />
    <meta name="facebook-domain-verification" content="x4ugnfcntyhxy6hctzi4ni9m66h4d4" />

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        .wrapper {
            border: 2px;
            overflow: hidden;
        }

            .wrapper div {
                min-height: 200px;
                padding: 10px;
            }

        #one {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="UpdateProgress" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="uppMenu">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress" style="width: 250px; height: 250px">
                    <div id="theprogress">
                        <asp:Image ID="imgWaitIcon1" Width="40%" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/logoPrincipal.png" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uppMenu" runat="server">
        <ContentTemplate>
            <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="cssHeader" HeaderSelectedCssClass="cssHeaderSelected"
                ContentCssClass="cssContent" FadeTransitions="True" SuppressHeaderPostbacks="True" TransitionDuration="250" FramesPerSecond="40"
                RequireOpenedPane="False">
                <Panes>
                    <cc1:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="" HeaderCssClass="">
                        <Header>
                            <table>
                                <tr>
                                    <td style="width: 5%; padding-left: 1em; margin: auto">&nbsp&nbsp&nbsp
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/Direccion.png" Width="60%" ImageAlign="Left" />
                                    </td>
                                    <td style="width: 85%; text-align: left">&nbsp&nbsp&nbsp
                                            <asp:Label ID="Label18" runat="server" Text="DIRECCIONES" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </Header>
                        <Content>
                            <br />
                            <table style="width: 90%; margin-left: auto; margin-right: auto">
                                <tr>
                                    <td style="width: 50%">
                                        <div>
                                            <div style="border: 1px solid #061c2f; box-shadow: inset 0 -3em 3em rgba(0,0,0,0.1), 0 0 0 2px rgb(255,255,255), 0.3em 0.3em 1em rgba(0,0,0,0.3); text-align: left; width: 500px; margin-top: auto; height: 250px; padding: 1em">

                                                <br />
                                                <div style="margin-left: auto; margin-right: auto; text-align: center">
                                                    <asp:Label ID="Label19" runat="server" Text="DIRECCIÓN MIAMI" Font-Size="Medium" Style="text-shadow: 1px 1px 2px gray; font-family: 'Montserrat', sans-serif;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:TextBox ID="txtDireccionMiami" Enabled="false" runat="server" TextMode="MultiLine" Width="80%" Rows="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 50%">
                                        <div>
                                            <div style="border: 1px solid #061c2f; box-shadow: inset 0 -3em 3em rgba(0,0,0,0.1), 0 0 0 2px rgb(255,255,255), 0.3em 0.3em 1em rgba(0,0,0,0.3); text-align: left; width: 500px; margin-top: auto; height: 250px; padding: 1em">

                                                <br />
                                                <div style="margin-left: auto; margin-right: auto; text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text="DIRECCIÓN ECUADOR" Font-Size="Medium" Style="text-shadow: 1px 1px 2px gray; font-family: 'Montserrat', sans-serif;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:TextBox ID="txtDireccionEcuador" runat="server" TextMode="MultiLine" Width="80%" Rows="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="padding-right: 2em; width: 100%; text-align: right">
                                <br />
                                <br />
                                <asp:Button ID="btnActualizarDireccion" Text="Actualizar Dirección" runat="server" CssClass="btn btn-primary btn-lg" Font-Size="9px" Style="margin-left: 10px" BackColor="#1D4289" OnClick="btnActualizarDireccion_Click" />
                                <br />
                                <br />
                            </div>

                        </Content>
                    </cc1:AccordionPane>
                </Panes>
            </cc1:Accordion>
            <asp:Panel ID="pnlCharCantidadClientes" runat="server" HorizontalAlign="Center" Width="100%" Height="600px" ScrollBars="Auto">
                <br />
                <br />
                <div class="wrapper">

                    <div id="one">
                        <asp:Chart ID="CharClientesAnual" runat="server" Width="505px" OnLoad="CharClientesAnual_Load" EnableViewState="True" IsMapAreaAttributesEncoded="True">
                            <Titles>
                                <asp:Title Font="Arial, 14pt, style=Bold" ForeColor="#1d4289" Name="Title1"
                                    Text="TIEMPO PROMEDIO DE ATENCIÓN EN DÍAS">
                                </asp:Title>
                            </Titles>
                            <Series>
                                <asp:Series Name="ReportePromedio" XValueMember="0" YValueMembers="2" IsValueShownAsLabel="true" IsXValueIndexed="True" CustomProperties="DrawingStyle=LightToDark, TextOrientation=TopToBottom, LabelStyle=Center, MaxPixelPointWidth=40" Palette="Pastel" XValueType="Double">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="CantidadDias">
                                    <AxisX Interval="1"></AxisX>
                                    <%--<AxisY IntervalAutoMode="VariableCount">
                        </AxisY>--%>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                </div>
            </asp:Panel>
             <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
<a href="https://wa.me/593992282734/?text=Hola,%20tengo%20una%20consulta." class="float" target="_blank">
<i class="fa fa-whatsapp my-float"></i>
</a>
        </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>
