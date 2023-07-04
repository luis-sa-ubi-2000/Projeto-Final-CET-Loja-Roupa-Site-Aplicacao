using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Loja
{
    public partial class Procurar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String g = Request.QueryString["procura"];
            procurou.Text = "   "+ g;
            Search.Attributes.Add("OnClick", "pesquisa");
            Search.Click += new EventHandler(pesquisa);
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);

            if (Session["id"] != null)
            {
                log.Text = "A minha conta";
                log.NavigateUrl = "Conta.aspx";
                minhaconta.NavigateUrl = "Conta.aspx";
                logout.Visible = true;
                wishlist.Visible = true;
                using (SqlConnection con = new SqlConnection())
                {

                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from carrinho where pessoaid=" + Session["id"], con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        notix.Visible = true;
                    }
                    else { notix.Visible = false; }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            else
            {
                minhaconta.Attributes.Add("onclick", "this.disabled=true");
                minhaconta.NavigateUrl = "Login.aspx";
                logout.Visible = false;
                if (Session["temporario"] == null)
                {
                    Random rnd = new Random();
                    Session["temporario"] = rnd.Next(1, 100);

                }
                else
                {
                    using (SqlConnection con = new SqlConnection())
                    {

                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select * from carrinhotemporario where sessionid=" + Session["temporario"], con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            notix.Visible = true;
                        }
                        else { notix.Visible = false; }
                        dr.Close();
                        cmd.Dispose();
                    }
                }
            }
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                


                SqlCommand cmd = new SqlCommand("select * from Produto inner join imagem on Produto.id_imagem = imagem.id where nome LIKE '"+g+"%'", con);

                int c = 0;
                SqlDataReader dr = cmd.ExecuteReader();
               
                while (dr.Read())
                {
                   
                        String preco = dr[3].ToString();
                        produtos.Text += "" +
                                "<div id='product" + c + "' class='product product" + c + "' onmouseover='visible(" + c + ")' onmouseout='invisi(" + c + ")'>" +
                                    "<img src='" + dr[9].ToString() + "' alt='Placeholder image'>" +
                                    "<div class='info'>" + dr[1].ToString() + "<br>" +
                                        "<a href='Produto.aspx?pid=" + dr[0] + "'><i id='addcar" + c + "' class='fas fa-cart-plus addcar'></i></a>" + preco.Remove(preco.Length - 2) + "&#8364;<a href='AddWishlist.aspx?pid=" + dr[0].ToString() + "'><i id='coracao" + c + "' class='fas fa-heart coracao'></i></a>" +
                                    "</div>" +
                                "</div>" +
                            "</a>" +
                            "";
                        c++;
                    
                }
            
            dr.Close();

                cmd.Dispose();
               

            }
            }

        private void pesquisa(object sender, System.EventArgs e)
        {
            String roberto = TextBox1.Text;
            Response.Redirect("Procurar.aspx?procura=" + roberto);
        }
        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}