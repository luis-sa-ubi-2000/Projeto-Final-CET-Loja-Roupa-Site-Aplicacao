using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminAddProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            addProduto.Attributes.Add("OnClick", "insereProd");
            addProduto.Click += new EventHandler(insereProd);
        }

        protected void insereProd(object sender, System.EventArgs e)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                int idadd;
                String imgqry = "insert into imagem values (@path)";
                using (SqlCommand cmd = new SqlCommand(imgqry, con))
                {
                    cmd.Parameters.AddWithValue("@path", "/Imagens/" + imagem.FileName);
                    cmd.ExecuteNonQuery();
                }

                string idimagem = "select MAX(id) from Imagem";
                using (SqlCommand cmd2 = new SqlCommand(idimagem, con))
                {
                    idadd = (int)cmd2.ExecuteScalar();
                }

                String qry = "insert into Produto values (@nome,@desc,@preco,1,1,@idimagem,@genero)";
                try { 
                using (SqlCommand cmd3 = new SqlCommand(qry, con))
                {
                    cmd3.Parameters.AddWithValue("@nome", nome.Text);
                    cmd3.Parameters.AddWithValue("@desc", desc.Text);
                    cmd3.Parameters.AddWithValue("@preco", preco.Text);
                    cmd3.Parameters.AddWithValue("@idimagem", idadd);
                    cmd3.Parameters.AddWithValue("@genero", generodrop.SelectedValue);
                    cmd3.ExecuteNonQuery();
                }
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Erro, o produto não foi aceite! Verifique os deus dados.');", true);

                    Response.Redirect(Request.RawUrl);
                }
                con.Close();

            }
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Produto Inserido com Sucesso!');window.location='AdminAddProduto.aspx';", true);
        }

        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}