<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="Loja.Carrinho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gaming Lobby | Carrinho</title>
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
    <form id="form1" runat="server">
        <section class="hero is-medium is-light is-bold" style="font-family: 'Montserrat', sans-serif;">
            <nav class="navbar is-fixed-top " role="navigation" aria-label="main navigation" style="background-color:#cc053eb0; opacity:0.9; color:white!important" >
                <div class="navbar-menu">
                    <div class="navbar-brand" style="padding:0 0 0 2em;">
                        <img src="Imagens/logo_transparent2.png" style="height:92px; width:92px; " />
                    </div>

                     <div class="navbar-start" style="padding-left:0.8em; color:white!important;">
                         <a class="navbar-item" href="Index.aspx">Início</a>
                         <a class="navbar-item" href="Homem.aspx">Homem</a>
                         <a class="navbar-item " href="Mulher.aspx">Mulher</a>
                         <a class="navbar-item " href="Crianca.aspx">Criança</a>
                        <asp:HyperLink ID="wishlist" runat="server" CssClass="navbar-item" Visible="false" NavigateUrl="~/Wishlist.aspx">Wishlist</asp:HyperLink>

                     </div>

                     <div style="padding:0.9em 14em 0.8em 0.8em; margin-left:-2%;">
                        <p class="control has-icons-left has-icons-right" >
                            <asp:TextBox ID="TextBox1" runat="server" Cssclass="input is-rounded is-small" type="text" placeholder=" " style="width:390px; background-color:white; height:38px; margin-top:6px; padding-left: 40px; font-size: 18px;"></asp:TextBox>
                                <span class="icon is-small is-left is-dark" style="font-size: 18px;margin-top: 4px;">
                                    <i class="fas fa-search " style="color:black"></i>
                                </span>
                            <a style="position:absolute; border:none; margin-top:-40px; margin-left:74%;">
                                <asp:Button ID="Search" runat="server" Text="Pesquisar" CssClass="button is-small is-light" runtat="server" BackColor="Transparent" Font-Size="18px"  OnClientClick="pesquisa()" />
                            </a>
                        </p>
                     </div>

                     <div class="navbar-end">
                         <asp:HyperLink ID="log" runat="server" CssClass="navbar-item" Text="Login" NavigateUrl="~/Login.aspx"></asp:HyperLink>
                        
                         <span style="padding-left:1em;"></span>
                         <a class="navbar-item " href="Carrinho.aspx"><i class="fas fa-shopping-cart " style="width:55px; font-size:20px;"></i></a>
                         <span style="padding-right:1.5em;"></span>
                         <div style="padding-right:5px; margin-right:110px; padding-top:18px;"><asp:Button ID="logout" runat="server" Text="Logout" CssClass="button is-danger" OnClientClick="logoutbutton()" /></div>
                         <div class="noti" runat="server" id="notix" visible="false">
                         </div>
                     </div>
                 </div>
            </nav>
        </section>
        <!--------------------------END-MENU------------------------------------------------------------------>


         <section class="hero is-medium is-dark is-bold" style="font-family: 'Montserrat', sans-serif;">
             <div class="hero-body" style="text-align:center; padding-top:5em;">
                 <div style="text-align:center;" >
                      <img src="logo_transparent.png" style="width:400px; height:400px;" />
                  </div>
             </div>      
             </section>
         
        <section class="hero is-medium is-light is-bold carrin" style="font-family: 'Montserrat', sans-serif;">
                <div class="hero-body" style="text-align:center; padding-top:30px;padding-bottom:1px;">
                    <h1 style="font-size:40px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;">Carrinho</h1><br /><br /> <br />
               
                    <div class="columns" style="margin-left:33%; margin-bottom:40px; ">
                          <div class="column is-half"><h2 style="font-size:25px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F; margin-top:-30px; ">Dados de Envio</h2><br />
                              <p><asp:TextBox ID="nomepessoa" runat="server" placeholder="Destinatário"  MaxLength="100" required=""></asp:TextBox></p><br />
                              <p><asp:TextBox ID="morada" runat="server" placeholder="Morada" MaxLength="250" required=""></asp:TextBox></p><br />
                              <p><asp:TextBox ID="telefone"  runat="server" placeholder="Telefone" MaxLength="15" required="" ></asp:TextBox></p><br />
                              <p><asp:TextBox ID="nif" runat="server" placeholder="NIF" MaxLength="10" required=""></asp:TextBox></p>
                              <i class="fas fa-exclamation-triangle"></i>   Os seus dados estão seguros connosco.
                              <p><br /><asp:TextBox ID="voucher" runat="server" placeholder="Código Promocional"></asp:TextBox><br /><asp:Button ID="inserir" runat="server" CssClass="button is-small" Text="Adicionar Código" AutoPostBack="false" />
                          </div>
                          </div>
                    </div>
                    <div class="columns" style="margin-left:33%;">
                        <div class="column is-half"><h2 style="font-size:25px; font-weight:bold;margin-left: 40%; font-family: 'Orbitron', sans-serif; color:#5F021F;">Produtos</h2><br /><div style="font-size:17px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:black;" ><asp:Literal ID="prodcarrinho" runat="server"></asp:Literal></div></div>
                    </div>
                <div class="dadoscar">
                     <br /><br /><br />
                     <h2 style="font-size:18px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;">Valor: </h2>
                     <div style="font-size:13px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;" ><asp:Label ID="valorsemdesconto" runat="server" Text="" ></asp:Label>&#8364;</div>
                     <h2 style="font-size:18px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;">Desconto aplicado: </h2>
                     <div style="font-size:13px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;" >-<asp:Label ID="menos" runat="server" Text="0" ></asp:Label>&#8364;</div>
                     <br /><h2 style="font-size:25px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;">Valor Total: </h2>
                     <div style="font-size:20px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;" ><asp:Label ID="valortotal" runat="server" Text="" ></asp:Label>&#8364;</div>
            <br /><br /><br />
                     <asp:Button ID="checkout" runat="server" Text="CHECKOUT" CssClass="button is-danger is-large" />
                 </div>
 
        <!--------------------------RODAPE------------------------------------------------------------------>
        <br /><br /><br />
        <hr class="style1" style="width:83%; margin-left:auto; margin-right:auto" />
        <br /><br /><br /><br />

            <div class="rodape">
                <div class="tile is-parent is-8">
                  <div class="content"">
                   <img src="Imagens/pagamentos.png" style="width:334px; height:252px; margin-right:340px; margin-left:100px;" />
                  </div>
                    <div class="columns">
                      <div class="column">
                        <h3>Produtos</h3>
                          <hr style="width:150px;"/>
                          <a href="Homem.aspx"><p>Homem</p></a>
                          <a href="Mulher.aspx"><p>Mulher</p></a>
                          <a href="Crianca.aspx"> <p>Criança</p></a>
                      </div>
                      <div class="column">
                         <h3>Links Uteis</h3>
                          <hr style="width:150px;"/>
                          <asp:HyperLink ID="minhaconta" runat="server"><p>Minha conta</p></asp:HyperLink>
                          <a href="https://www.atec.pt/"><p>ATEC</p></a>
                      </div>
                      <div class="column" style="margin-right:-750px;">
                         <h3>Contacte-nos</h3>
                          <hr style="width:300px;"/>
                          <p><i class="fas fa-phone" style="margin-right:20px; font-size:21px;"></i>+351  928 852 496</p>
                          <p><i class="fas fa-at" style="margin-right:20px; font-size:21px;"></i>gaminglobby@gmail.com</p>
                          <p><i class="fas fa-home" style="margin-right:20px; font-size:21px;"></i>ATEC - Palmela</p>
                     </div>
                    </div>
                 </div>
            </div>
        </section>


    </form>
</body>
</html>
