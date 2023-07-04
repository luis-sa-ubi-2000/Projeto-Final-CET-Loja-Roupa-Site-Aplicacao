using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminProdutos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else { 
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();



                SqlCommand cmd = new SqlCommand("select * from produto inner join imagem on Produto.id_imagem = imagem.id ", con);
                SqlDataReader dr = cmd.ExecuteReader();

                    int c = 0;
                    while (dr.Read())
                    {

                        String preco = dr[3].ToString();
                        listaprodutos.Text += "" +
                            "<a href='AdminEditProduto.aspx?pid=" + dr[0] + "'>" +
                                "<div id='product" + c + "' class='product product" + c + "' onmouseover='visible(" + c + ")' onmouseout='invisi(" + c + ")'>" +
                                    "<img src='" + dr[9].ToString() + "' alt='Placeholder image'>" +
                                    "<div class='info'>" + dr[1].ToString() + "<br>" +
                                         preco.Remove(preco.Length - 2) + "&#8364;<i id='edit" + c + "' class='far fa-edit edit'></i>" +
                                    "</div>" +
                                "</div>" +
                            "</a>" +
                            "";
                        c++;


                    }

                    dr.Close();

                cmd.Dispose();






                con.Close();
                }
            }
        }
        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}