﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminProdutos.aspx.cs" Inherits="Loja.AdminProdutos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-image: linear-gradient(141deg, #1f191a 0%, #363636 71%, #46403f 100%); background-attachment: fixed;">
<head runat="server">
    <title>ADMIN | Produtos</title>
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
    <form id="AdminProdutos" runat="server">
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

        <!----------------------------------------PRODUTOS----------------------------------->
         <section class="hero is-medium is-dark is-bold" style="font-family: 'Montserrat', sans-serif; height:100%; padding-left: 360px; padding-bottom:140px; padding-top:120px;">
                    <asp:Label ID="listaprodutos" runat="server"></asp:Label>
         </section>
        <!------------------------------------------------------------------------------------>
    </form>
    <script>
        
        function visible(c) {
            document.getElementById('product' + c).style.backgroundColor = "white";
            document.getElementById('edit' + c).style.visibility = "visible";
            document.getElementById('dele' + c).style.visibility = "visible";
  
        }

        function invisi(c) {
            document.getElementById('product'+c).style.backgroundColor = '#F1F3F4';
            document.getElementById('edit' + c).style.visibility = "hidden";
            document.getElementById('dele' + c).style.visibility = "hidden";
        }

    </script>

</body>
</html>
