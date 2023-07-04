<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddProduto.aspx.cs" Inherits="Loja.AdminAddProduto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"  style="background-image: linear-gradient(141deg, #1f191a 0%, #363636 71%, #46403f 100%); background-attachment: fixed;">
<head runat="server">
    <title>ADMIN | Adicionar Produto</title>
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
    <form id="AdminAddProduto" runat="server">
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

        <section class="hero is-medium is-dark is-bold" style="font-family: 'Montserrat', sans-serif; height:100vh;">
            <div class="hero-body">
                <div class="container">
                    <div style="text-align:center; margin-top:-70px; margin-left:40px; width:600px; height:600px; position:absolute"><asp:Label ID="imagemprod" runat="server" Text=""></asp:Label></div>
                        
                    <div class="columns">
                        <div class="column" style="margin-left:650px;">
                        <br /><br /><br />
                   
                        Nome: <asp:TextBox ID="nome" CssClass="nopadd" runat="server" required=""></asp:TextBox><br /><br /><br />
                        Descrição: <asp:TextBox id="desc" required="" TextMode="multiline" Columns="50" Rows="5"  runat="server" Cssclass="textarea is-rounded nopadd"/>
                        </div>

                        <div class="column admi" style="margin-left:40px; margin-right:-300px;">
                        <br /><br /><br />
                        Preço: <br /><asp:TextBox ID="preco" CssClass="nopadd" required="" runat="server"></asp:TextBox>  &#8364;<br />

                        Género: <br /><asp:DropDownList ID="generodrop" runat="server">
                                     <asp:ListItem Value="M">Homem</asp:ListItem>
                                        <asp:ListItem Value="F">Mulher</asp:ListItem>
                                         <asp:ListItem Value="C">Criança</asp:ListItem>
                                          
                                </asp:DropDownList><br /><br /><br /><br /><br />
                        Imagem:  <br /><asp:FileUpload ID="imagem" runat="server"  required=""/><br /><br /><br /><br /><br /><br /><br /><br />
                        <asp:Button ID="addProduto" runat="server" Text="Adicionar" CssClass="button is-large is-danger"/>
                    </div>
                </div>
            </div>
        </div>
      </section>
    </form>
</body>
</html>
