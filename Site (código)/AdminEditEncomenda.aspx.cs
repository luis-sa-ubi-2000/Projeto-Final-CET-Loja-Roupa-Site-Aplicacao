using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminEditEncomenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                logout.Attributes.Add("OnClick", "logoutbutton");
                logout.Click += new EventHandler(logoutbutton);
                edit2.Attributes.Add("OnClick", "EditaProd");
                edit2.Click += new EventHandler(Edita);

                if (!IsPostBack)
                {
                    int pid = int.Parse(Request.QueryString["pid"]);
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();



                        SqlCommand cmd = new SqlCommand("select * from orders where id=" + pid, con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            nome.Text = dr[6].ToString();
                            morada.Text = dr[5].ToString();
                            nif.Text = dr[8].ToString();
                            tel.Text = dr[7].ToString();
                            estadodrop.SelectedValue = dr[4].ToString();
                        }
                        dr.Close();
                        cmd.Dispose();
                        con.Close();
                    }
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();

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
            }
        }
        protected void Edita(object sender, System.EventArgs e)
        {
            int pid = int.Parse(Request.QueryString["pid"]);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                try
                {
                    String qryedit = "update orders set destinatario=@nome,morada=@morada,tel=@tel,nif=@nif,estado=@estado where id=@id";
                    using (SqlCommand cmd3 = new SqlCommand(qryedit, con))
                    {
                        cmd3.Parameters.AddWithValue("@id", pid);
                        cmd3.Parameters.AddWithValue("@nome", nome.Text);
                        cmd3.Parameters.AddWithValue("@morada", morada.Text);
                        cmd3.Parameters.AddWithValue("@tel", tel.Text);
                        cmd3.Parameters.AddWithValue("@nif", nif.Text);
                        cmd3.Parameters.AddWithValue("@estado", estadodrop.SelectedValue);
                        cmd3.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect(Request.RawUrl);
                }
                con.Close();
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Alteração feita com sucesso!');window.location='AdminEncomendas.aspx';", true);
            }
        }
                protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}