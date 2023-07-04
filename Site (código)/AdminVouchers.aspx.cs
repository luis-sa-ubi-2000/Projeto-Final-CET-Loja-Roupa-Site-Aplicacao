using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminVouchers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            listaVouchers.Text = "";
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from voucher", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    String data = dr[2].ToString();
                    listaVouchers.Text += "<tr><td>" + dr[1].ToString() + "</td><td>" + data.Remove(data.Length-9) + "</td><td>" + dr[3].ToString() + "%</td><td><a class='button is-small is-danger' style='margin-left:10px;' href='AdminDelVoucher.aspx?pid=" + dr[0].ToString() + "'>Remover</a></tr>";
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
        protected void criavoucher(object sender, System.EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    String criavoucher = "insert into voucher values (@codigo,getDate(),@desconto)";
                    using (SqlCommand cmd = new SqlCommand(criavoucher, con))
                    {
                        cmd.Parameters.AddWithValue("@codigo",codigo.Text);
                        cmd.Parameters.AddWithValue("@desconto", desconto.Text.Replace(",", "."));
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();

                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Erro na criação, verifique os dados!');", true);

            }
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Voucher criado com sucesso!');window.location='AdminVouchers.aspx';", true);

        }

    }
}
