using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Loja
{
    public partial class AdminIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            Random rnd = new Random();
            
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP(5) * from orders inner join Pessoa on orders.pessoaid = pessoa.id order by orders.id desc ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    String preco = dr[9].ToString();
                    String data = dr[2].ToString();
                    listaEncomendas.Text += "<tr><td>" + dr[0].ToString() + "</td><td>" + dr[11].ToString() + "</td><td>" + data.Remove(data.Length-9) + "</td><td>" + preco + "&#8364;</td></tr>";
                    
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