using System;
using System.Data.SqlClient;

namespace Loja
{
    public partial class Conta : System.Web.UI.Page
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
                Response.Redirect("Login.aspx");
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
                String qry = "select * from Pessoa where id='" + Session["id"] + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    nomepessoa.Text = dr[1].ToString();
                    emaill.Text = dr[4].ToString();
                    niff.Text = dr[3].ToString();
                    telef.Text = dr[5].ToString();
                    String formatar = dr[7].ToString();
                    datanascimento.Text = formatar.Remove(formatar.Length-12);
                }
                dr.Close();
                String qry2 = "select * from orders where pessoaid='" + Session["id"] + "' order by id desc";
                SqlCommand cmd2 = new SqlCommand(qry2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                String estado = "";
                while (dr2.Read())
                {
                    switch (dr2[4].ToString())
                    {
                        case "0":
                            estado = "Em processamento";
                            break;
                        case "1":
                            estado = "Enviada";
                            break;
                    }
                    encomendas.Text+= "<p class='bd - notification is -danger' id='encomenda' style='font - size:20px; font - weight:bold; font - family: 'Orbitron', sans - serif; color: black; '>";
                    encomendas.Text += "Nº: <a href='Encomenda.aspx?pid="+dr2[0].ToString()+ "'>"+ dr2[0].ToString() + "</a><br>  ";
                    encomendas.Text+= "Data de compra: "+dr2[2].ToString()+"<br>Valor: "+dr2[3].ToString()+ "&#8364; <strong>Estado: "+estado+"</strong></p>";
                }



                con.Close();

            }
            //Provavelmente meter orders, nome morada etc aqui
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