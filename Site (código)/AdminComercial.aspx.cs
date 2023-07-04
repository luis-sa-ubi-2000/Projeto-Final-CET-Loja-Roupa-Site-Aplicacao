using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminComercial : System.Web.UI.Page
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

                SqlCommand cmd = new SqlCommand("select sum(desconto) from orders",con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    vendas.Text = dr[0].ToString();
                }
                dr.Close();
                cmd.Dispose();
                SqlCommand cmd2 = new SqlCommand("select count(id) from orders", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                   encomendas.Text = dr2[0].ToString();
                }
                dr2.Close();
                cmd2.Dispose();


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