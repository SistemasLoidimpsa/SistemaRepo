<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="LugarCanje.aspx.cs" Inherits="LOADIMPSA.LugarCange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
            'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '1111161526407188');
        fbq('track', 'AddToCart');
    </script>
    <noscript>
        <img height="1" width="1"
            src="https://www.facebook.com/tr?id=1111161526407188&ev=PageView
&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->
    <style type="text/css">
        .px-sm-6 {
            padding-right: 3rem !important;
            padding-left: 10rem !important;
        }

        .bg-yellow {
            --bs-bg-opacity: 1;
            background-color: rgb(255, 205, 0) !important;
        }

        .container {
            width: 340px;
        }

        .row-cols-md-auto1 {
            flex: 0 0 auto;
            width: 340px;
        }

        .button {
            display: inline-block;
            padding: 10px 25px;
            font-size: 80%;
            cursor: pointer;
            text-align: center;
            text-decoration: none;
            outline: none;
            color: #fff;
            border: none;
            border-radius: 13px;
            width: 70%;
            background-color: #3e65ad;
        }

            .button:hover {
                background-color: #3e65ad;
            }

            .button:active {
                background-color: #0e3c91;
                box-shadow: 0 5px #666;
                transform: translateY(4px);
            }

        .card-img-top {
            height: 226px;
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

    <!-- Favicon-->

    <!-- Bootstrap icons-->

    <!-- Core theme CSS (includes Bootstrap)-->
    <link href="css/stylese.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="bg-yellow py-4 px-sm-6">
        <img src="images/recompensaBanner.png" alt="Recompensa Banner" />

    </header>
    <asp:HiddenField ID="hddCodigoUsuario" runat="server" />
    <asp:HiddenField ID="hddIdentificacion" runat="server" />
    <center><asp:Label ID="lbl1" Text="" CssClass="header" runat="server" Font-Size="Larger" Width="100%"></asp:Label>
       <asp:Label ID="lblOrderby" runat="server" Text="Ordenar Por:" CssClass="subtituloHeader"></asp:Label>
                                    &nbsp
                                    <asp:DropDownList ID="ddlOrdenar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged">
                                           <asp:ListItem Text="Seleccione" Value="*" Selected ="true"></asp:ListItem>
                                        <asp:ListItem Text="Menor a mayor" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Mayor a menor" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Nuevos" Value="2"></asp:ListItem>

                                    </asp:DropDownList>
        </center>
    <asp:DataList ID="ItemsList"
        BorderColor="black"
        CellPadding="5"
        CellSpacing="5"
        RepeatDirection="Horizontal"
        RepeatLayout="Table"
        RepeatColumns="3"
        BorderWidth="0"
        runat="server"
        Height="50px"
        Width="10px">

        <HeaderStyle BackColor="#aaaadd"></HeaderStyle>




        <ItemTemplate>
            <div style="width: 10%; height: auto">
                <section class="py-5">
                    <div class="px-4 px-lg-5 mt-5">
                        <div class="row-cols-md-auto1">
                            <div class="col mb-5">
                                <div class="card h-1000">
                                    <!-- Product image-->
                                    <img class="card-img-top" src='<%# DataBinder.Eval(Container.DataItem, "imagen") %>' alt="<%# DataBinder.Eval(Container.DataItem, "nombre") %>" />
                                    <!-- Product details-->
                                    <div class="card-body p-4">
                                        <div class="text-center">
                                            <div class="badge bg-dark text-white position-absolute" style="top: 0.5rem; right: 0.5rem"><%#  ((string)Eval( "nombre")).Split(' ').First() %></div>
                                            <!-- Product name-->
                                            <h5 class="fw-bolder"><%# DataBinder.Eval(Container.DataItem, "nombre") %></h5>
                                            <!-- Product price-->
                                            + <%# DataBinder.Eval(Container.DataItem, "puntos") %>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "estado") %>
                                        </div>
                                    </div>
                                    <!-- Product actions-->
                                    <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
                                        <div class="text-center">
                                            <asp:Button ID="btnCanjeo" class="button" Text="Canjear" runat="server" BackColor="#1D4289" ForeColor="White" OnCommand="btnRegistroCheckOut_Click"
                                                CommandArgument=' <%# Eval( "IntegerValue")+";"+Eval( "puntos") %>' />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ItemTemplate>

    </asp:DataList>

       <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
<a href="https://wa.me/593992282734/?text=Hola,%20tengo%20una%20consulta." class="float" target="_blank">
<i class="fa fa-whatsapp my-float"></i>
</a>



</asp:Content>
