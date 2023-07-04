<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="Loja.Produto" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gaming Lobby | Produto</title>
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
    <form id="form5" runat="server">
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

      <section class="hero is-medium is-light is-bold" style="font-family: 'Montserrat', sans-serif;">
        <div class="hero-body">
            <div class="container">
                <div class="columns">
                    <div class="column is-two-thirds">
                    <br /><br /><br />
                    <div style="text-align:center;"><asp:Label ID="imagem" runat="server" Text=""></asp:Label></div>
                    </div>
                    <div class="column">
                    <div style="font-size:40px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;"> <asp:Label ID="nomee" runat="server" Text=""></asp:Label></div><br />
                    <div style="font-size:18px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:black;; "> <asp:Label ID="descricacao" runat="server" Text=""></asp:Label></div>
                        
                     <br />   <br />
                    <div style="font-size:20px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:black;"> Preço:  <asp:Label ID="preco" runat="server" Text=""></asp:Label>&#8364;</div><br />
                    <div class="qtd" style="font-size:20px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:black;">
                        Quantidade: <asp:TextBox ID="quantidade" runat="server" Text="1" AutoPostBack="true" CssClass="input is-rounded" MaxLength="3"></asp:TextBox>

                    </div><br />
                    
                    <div class="qtd" style="font-size:20px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:black;">
                        Tamanho:  
                        <asp:DropDownList ID="tamanho" runat="server" >
                            <asp:ListItem Text="XS" Value="XS"></asp:ListItem>
                            <asp:ListItem Text="S"  Value="S"></asp:ListItem>
                            <asp:ListItem Text="M"  Value="M"></asp:ListItem>
                            <asp:ListItem Text="L"  Value="L"></asp:ListItem>
                            <asp:ListItem Text="XL" Value="XL"></asp:ListItem>
                        </asp:DropDownList>
                    </div><br /><br />

                    <div style="font-size:25px; font-weight:bold; font-family: 'Orbitron', sans-serif; color:#5F021F;"> Total:  <asp:Label ID="total" runat="server" Text=""></asp:Label>&#8364;</div><br /><br /><br /><br />
                    
                        <asp:Button ID="addCart" runat="server" Text="Adicionar ao carrinho" CssClass="button is-large is-danger"/>
                    
                        
                    </div>
                </div>
            </div>
        </div>



        <!--------------------------RODAPE------------------------------------------------------------------>
        <br /><br /><br /><br /><br /><br /><br />
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
    <script>
        function noti() {
            document.getElementById('noti').style.visibility = "visible";  
        }

    </script>

</body>
</html>
