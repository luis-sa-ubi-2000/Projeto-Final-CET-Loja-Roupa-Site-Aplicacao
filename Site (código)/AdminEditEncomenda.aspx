<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEditEncomenda.aspx.cs" Inherits="Loja.AdminEditEncomenda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"  style="background-image: linear-gradient(141deg, #1f191a 0%, #363636 71%, #46403f 100%); background-attachment: fixed;">
<head runat="server">
    <title></title>
<meta charset="utf-8" />
    <link href="stylee.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v5.3.1/js/all.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Montserrat|Orbitron:700&display=swap" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="styleflic.css" rel="stylesheet" />
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <script src="https://kit.fontawesome.com/800c4a5535.js"></script>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="EditProduto" runat="server">

       <!------------NAVBAR-------------------------->
        <section class="hero is-medium is-light is-bold" style="font-family: 'Montserrat', sans-serif;">
            <nav class="navbar is-fixed-top " role="navigation" aria-label="main navigation" style="background-color:#cc053eb0; opacity:0.9; color:white!important" >
                <div class="navbar-menu">
                    <div class="navbar-brand" style="padding:0 0 0 2em;">
                        <a href="AdminIndex.aspx"><img src="Imagens/logo_transparent2.png" style="height:92px; width:92px; " /></a>
                        <div style="font-size:26px!important; margin-top:18px; color:white!important;">Administração</div>
                    </div>
                     <div class="navbar-start" style="padding-left:0.8em; color:black;  flex-grow: 1; justify-content: center;">
                            
                         <div class="navbar-item has-dropdown is-hoverable">
                                    <a style="margin-left:-200px!important;" class="navbar-link">
                                      Gestão de Loja
                                    </a>
                                    <div style="margin-left:-200px!important; color:black!important;" class="navbar-dropdown">
                                      <a class="navbar-item" style="color:black" href="AdminAddProduto.aspx">
                                        Adicionar Produtos
                                      </a>
                                      <a class="navbar-item" style="color:black" href="AdminProdutos.aspx">
                                        Editar/Remover Produtos
                                      </a>       
                                    </div>
                          </div>
                         <div class="navbar-item has-dropdown is-hoverable">
                                    <a class="navbar-link" >
                                      Faturação
                                    </a>
                                    <div class="navbar-dropdown">
                                      <a class="navbar-item" style="color:black" href="AdminComercial.aspx">
                                        Plano Comercial
                                      </a>
                                      <a class="navbar-item" style="color:black" href="AdminEncomendas.aspx">
                                        Processamento de encomendas
                                      </a>                                        
                                    </div>
                            </div>
                         <a class="navbar-item" href="AdminContas.aspx">
                              Gestão de Contas
                          </a>
                          <div class="navbar-item has-dropdown is-hoverable">
                                    <a class="navbar-link" >
                                      Marketing
                                    </a>
                                    <div class="navbar-dropdown">
                                      <a class="navbar-item" style="color:black" href="AdminNewsletter.aspx">
                                        Enviar Newsletter
                                      </a>        
                                        <a class="navbar-item" style="color:black" href="AdminVouchers.aspx">
                                        Vouchers
                                        </a> 
                                    </div>
                            </div>
                     </div>

                     <div class="navbar-end">
                         <div style="padding-right:5px; margin-right:110px; padding-top:18px;"><asp:Button ID="logout" runat="server" Text="Logout" CssClass="button is-danger" OnClientClick="logoutbutton()" /></div>                    
                     </div>
                 </div>
            </nav>
        </section>
        <!----------------------------------------------------------------------------------->

  <section class="hero is-medium is-dark is-bold " style="font-family: 'Montserrat', sans-serif; height:100%;">
        <div class="hero-body">
            <div class="container">     
                <div class="columns">
                        <div class="column" style="margin-left:50px; "><br /><br /><br />
                            <div class="title"><h1 style="font-size:40px;  font-weight:bold; font-family: 'Orbitron', sans-serif; color:#911538;">Dados de envio:</h1></div><br /><br />
                                <div class="columns is-mobile">
                                    <div class="column" style="margin-top:-28px">
                                        <p class="bd-notification is-info" style="font-size:27px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:white; margin-bottom:-20px;">
                                                Destinatário: <asp:TextBox ID="nome" CssClass="nopadd" runat="server" MaxLength="100"></asp:TextBox><br />
                                                Morada: <asp:TextBox id="morada" TextMode="multiline" Columns="30" Rows="3" runat="server" Cssclass="textarea" MaxLength="250"/><br />
                                                NIF: <asp:TextBox id="nif" CssClass="nopadd" TextMode="singleline" runat="server" MaxLength="10" /><br />
                                                Contacto Telefónico: <asp:TextBox ID="tel" CssClass="nopadd" runat="server" MaxLength="100"></asp:TextBox><br /><br />
                                                Estado: <asp:DropDownList ID="estadodrop" runat="server">
                                     <asp:ListItem Value="0">Em processamento</asp:ListItem>
                                        <asp:ListItem Value="1">Enviada</asp:ListItem>
                                         <asp:ListItem Value="2">Concluida</asp:ListItem>
                                </asp:DropDownList><br />
                                        </p>
                                        <br />                        <asp:Button ID="edit2" runat="server" Text="Editar" CssClass="button is-large is-danger"/><br /><br />

                                    </div>
                                </div>
                        </div>


        <div class="column" style="margin-left:70px;"><br /><br /><br />
            <div class="title"><h1 style="font-size:40px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#911538;">Produtos</h1></div>
            <div class="columns is-mobile">
                <div class="column ">
                    <table class="table" style="width:700px;">
                                          <thead>
                                            <tr>
                                              <th style="border-top-left-radius:7px; "></th>
                                              <th>Nome</th>
                                              <th>Quantidade</th>
                                              <th style=" border-top-right-radius:7px;">Preço</th>
                                            </tr>
                                          </thead>
                                                <tbody>
                                                     <asp:Label ID="produtos" runat="server" Text=""></asp:Label>
                                                </tbody>
                                    </table>
                </div>
            </div>
        </div>

                    </div>
                    </div>
        </div>
      </section>    

    </form>
</body>
</html>
