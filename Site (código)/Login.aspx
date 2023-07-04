<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Loja.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden">
<head runat="server">
    <title>Gaming Lobby | Login</title>
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
    <form id="login" runat="server">
        <nav class="navbar is-dark" role="navigation" aria-label="main navigation">
            <div class="navbar-brand">
                <a class="navbar-item" href="Index.aspx">
                    <i class="fas fa-arrow-left" style="color:white; width:55px; font-size:24px;" ></i>
                </a>
            </div>
        </nav>
       <!--------------------------END-MENU------------------------------------------------------------------>


        <section class="hero is-fullheight is-medium is-bold">
            <div class="hero-body" style="  background-color:#8d0f33; background: #8d0f33;
                                            background: -webkit-linear-gradient(to bottom, #8d0f33, #631529); 
                                            background: linear-gradient(to bottom, #8d0f33, #631529); padding-top:0px;">
                <div class="container">
                    <div class="columns is-centered">
                        <article class="card is-rounded" style="height:475px; background:#2b2828; border-radius:4px; margin-top:-40px; box-shadow: 0px 1px 4px 1px black; padding-left: 15px; padding-bottom:40px;">
                            <div class="card-content" style="width:340px; height:500px; padding-bottom:25px;">
                                <h1 class="title" style="text-align:center;">
                                    <img src="Imagens/logo_transparent.png" style="height:240px; width:240px; text-align:center; margin-top:-15px;" />                     
                                </h1>

                                <p class="control has-icon" style="margin-top:-40px; margin-bottom:20px;">
                                    <i class="fa fa-envelope" style=" position:absolute; margin-left:4%; margin-top:19px; z-index:99; font-size:18px;"></i>
                                    <asp:TextBox TextMode="email" ID="email" CssClass="input" runat="server" placeholder="Email" MaxLength="100"></asp:TextBox>
                                </p>

                                <p class="control has-icon">
                                    <i class="fa fa-lock" style="position:absolute; margin-left:4%; margin-top:19px; z-index:99; font-size:18px;" ></i>
                                    <asp:TextBox ID="password" CssClass="input" runat="server" TextMode="Password" placeholder="Password" MaxLength="50"></asp:TextBox>
                                </p>
                                <p style="margin-top:4px; color:#a0a0a0;">
                                    Não tem Conta? <a class="reg" href="Registar.aspx">Pode criar aqui!</a>
                                </p>
                                <br /><br />
                                <p class="control" >
                                    <asp:Button ID="submeter" runat="server" Text="Login"  CssClass="button is-danger is-medium is-fullwidth" />
                                </p>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
