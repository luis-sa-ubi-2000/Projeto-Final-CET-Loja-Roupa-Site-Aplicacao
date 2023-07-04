using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminContas : System.Web.UI.Page
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



                SqlCommand cmd = new SqlCommand("select * from pessoa", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (dr[9].ToString() != "99") {
                    listaContas.Text += "<tr><td>" + dr[0].ToString() + "</td><td>" + dr[1].ToString() + "</td><td>" + dr[4].ToString() + "</td><td><a onclick='return confirm(`Tem a certeza que quer eliminar esta conta? Esta ação é irreversível.`)' class='button is-small is-danger' href='AdminDelConta.aspx?pid=" + dr[0].ToString()+"'>Remover</a></tr>";
                    }
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