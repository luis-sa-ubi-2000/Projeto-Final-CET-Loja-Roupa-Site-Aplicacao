using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminEncomendas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();



                SqlCommand cmd = new SqlCommand("select * from orders inner join Pessoa on orders.pessoaid = pessoa.id ", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    String preco = dr[9].ToString();
                    String data = dr[2].ToString();
                    String estado ="";
                    switch (dr[4].ToString())
                    {
                        case "0":
                            estado = "Em processamento";
                            break;
                        case "1":
                            estado = "Enviada";
                            break;
                        case "2":
                            estado = "Concluida";
                            break;

                    }



                    listaEncomendas.Text += "<tr><td>" + dr[0].ToString() + "</td><td>" + dr[11].ToString() + "</td><td>" + data.Remove(data.Length - 9) + "</td><td>" + preco + "&#8364;</td><td>" + estado + "</td><td><a class='button is-small' href='AdminEditEncomenda.aspx?pid="+dr[0].ToString()+ "'>Alterar</a><a onclick='return confirm(`Tem a certeza que quer eliminar esta encomenda?`)' class='button is-small is-danger' style='margin-left:10px;' href='AdminDelEncomenda.aspx?pid=" + dr[0].ToString()+"'>Remover</a></tr>";

                }

                dr.Close();

                cmd.Dispose();






                con.Close();
            }
        }
        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}