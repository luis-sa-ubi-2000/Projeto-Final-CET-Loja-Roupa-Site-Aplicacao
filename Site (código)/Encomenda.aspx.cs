using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class Encomenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                int pid = int.Parse(Request.QueryString["pid"]);



                SqlCommand cmd = new SqlCommand("select * from orders where id="+pid, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    nomepessoa.Text = dr[6].ToString();
                    nif.Text = dr[8].ToString();
                    tel.Text = dr[7].ToString();
                    morada.Text = dr[5].ToString();
                    valorsemdesconto.Text = dr[3].ToString();
                    total.Text = dr[9].ToString();

                }

                dr.Close();

                cmd.Dispose();







                con.Close();

            }
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                int pid = int.Parse(Request.QueryString["pid"]);


                SqlCommand cmd = new SqlCommand("select * from orderitem inner join Produto on orderitem.id_produto = produto.id inner join imagem on produto.id_imagem=imagem.id where orderitem.id_order=" + pid, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    String preco = dr[4].ToString();
                    produtos.Text += "<tr><td><img src='" + dr[14].ToString() + "' style='width:25px; height:25px;'/></td><td>" + dr[6].ToString() + "</td><td>" + dr[3].ToString() + "</td><td>" + preco + "&#8364;</td></tr>";

                }

                dr.Close();

                cmd.Dispose();






                con.Close();

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