using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows;

namespace Loja
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Search.Attributes.Add("OnClick", "pesquisa");
            Search.Click += new EventHandler(pesquisa);
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            insereemail.Attributes.Add("OnClick", "newsletter");
            insereemail.Click += new EventHandler(newsletter);

           

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
                    SqlCommand cmd = new SqlCommand("select * from carrinho where pessoaid="+Session["id"], con);
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
                else { 
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

          

            }
        protected void newsletter(object sender, System.EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {

                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    string insere = "insert into newsletter values (@email)";
                    using (SqlCommand cmd = new SqlCommand(insere, con))
                    {
                        cmd.Parameters.AddWithValue("@email", newsletteremail.Text );
                        cmd.ExecuteNonQuery();
                    }
                        con.Close();
                }
                }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('O email inserido já está registado!');window.location='Index.aspx';", true);

            }
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('O email foi inserido, nunca mais vais perder as nossas novidades!');window.location='Index.aspx';", true);

        }



        protected void pesquisa(object sender, System.EventArgs e)
        {
            String procura = TextBox1.Text;
            Response.Redirect("Procurar.aspx?procura=" + procura);
        }

        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
    }
