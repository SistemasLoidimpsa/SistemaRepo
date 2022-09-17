<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Prueba1.aspx.cs" Inherits="LOADIMPSA.Prueba1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            width:340px;
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
    
      <table cellpadding="5">

         <tr>

            <th>

               Repeat direction:

            </th>

            <th>

               Repeat layout:

            </th>

            <th>

               Repeat columns:

            </th>

            <th>

               <asp:CheckBox id="ShowBorderCheckBox"
                    Text="Show border"
                    Checked="False" 
                    runat="server" />

            </th>

         </tr>

         <tr>

            <td>

               <asp:DropDownList id="DirectionList" 
                    runat="server">

                  <asp:ListItem>Horizontal</asp:ListItem>
                  <asp:ListItem Selected="True">Vertical</asp:ListItem>

               </asp:DropDownList>

            </td>

            <td>

               <asp:DropDownList id="LayoutList" 
                    runat="server">

                  <asp:ListItem Selected="True">Table</asp:ListItem>
                  <asp:ListItem>Flow</asp:ListItem>

               </asp:DropDownList>

            </td>

            <td>

               <asp:DropDownList id="ColumnsList" 
                    runat="server">

                  <asp:ListItem Selected="True">0</asp:ListItem>
                  <asp:ListItem>1</asp:ListItem>
                  <asp:ListItem>2</asp:ListItem>
                  <asp:ListItem>3</asp:ListItem>
                  <asp:ListItem>4</asp:ListItem>
                  <asp:ListItem>5</asp:ListItem>

               </asp:DropDownList>

            </td>

            <td>

                

            </td>


         </tr>

      </table>     
         
      <asp:LinkButton id="RefreshButton" 
           Text="Refresh DataList" 
           OnClick="Button_Click" 
           runat="server"/>
     <asp:HiddenField ID="hddCodigoUsuario" runat="server" />
                                <asp:HiddenField ID="hddIdentificacion" runat="server" />
     <asp:Label ID="lbl1" Text="" CssClass="header" runat="server" Font-Size="Larger" Width="100%"></asp:Label>
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
            <div style="width: 360px; height: contain">
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
                                        <div class="text-center"><asp:Button  ID="btnCanjeo" class="button" Text="Canjear" runat="server" BackColor="#1D4289" ForeColor="White"  OnCommand="btnRegistroCheckOut_Click"
                                                CommandArgument=' <%# Eval( "IntegerValue")+";"+Eval( "puntos") %>'/></div>
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ItemTemplate>

    </asp:DataList>





</asp:Content>
