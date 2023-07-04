using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminEditProduto : System.Web.UI.Page
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
            edit.Attributes.Add("OnClick", "EditaProd");
            edit.Click += new EventHandler(EditaProd);
            apaga.Attributes.Add("OnClick", "EliminaProd");
            apaga.Click += new EventHandler(EliminaProd);

            if (!IsPostBack) { 
            int pid = int.Parse(Request.QueryString["pid"]);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();



                SqlCommand cmd = new SqlCommand("select * from produto inner join imagem on Produto.id_imagem = imagem.id where produto.id=" + pid, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    String valor = dr[3].ToString();
                    imagemprod.Text = "<img src='" + dr[9].ToString() + "' alt='Placeholder image'>";
                    nome.Text = dr[1].ToString();
                    desc.Text = dr[2].ToString();
                    preco.Text = valor.Remove(valor.Length - 2);
                    generodrop.SelectedValue = dr[7].ToString();
                }
                dr.Close();
                cmd.Dispose();
                con.Close();
                }
            }
            }
        }
        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }

        protected void EditaProd(object sender, System.EventArgs e)
        {
            int pid = int.Parse(Request.QueryString["pid"]);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                if (imagem.HasFile)
                {
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
                    try { 
                    String qryedit = "update Produto set nome=@nome,descricao=@desc,preco=@preco,genero=@genero,id_imagem=@imagem where id=@id";
                    using (SqlCommand cmd3 = new SqlCommand(qryedit, con))
                    {

                        cmd3.Parameters.AddWithValue("@id", pid);
                        cmd3.Parameters.AddWithValue("@nome", nome.Text);
                        cmd3.Parameters.AddWithValue("@desc", desc.Text);
                        cmd3.Parameters.AddWithValue("@preco", preco.Text.Replace(",","."));
                        cmd3.Parameters.AddWithValue("@imagem", idadd);
                        cmd3.Parameters.AddWithValue("@genero", generodrop.SelectedValue);
                        cmd3.ExecuteNonQuery();
                    }
                    }
                    catch (Exception)
                    {
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    try {
                        String qryedit = "update Produto set nome=@nome,descricao=@desc,preco=@preco,genero=@genero where id=@id";
                        using (SqlCommand cmd3 = new SqlCommand(qryedit, con))
                        {

                            cmd3.Parameters.AddWithValue("@id", pid);
                            cmd3.Parameters.AddWithValue("@nome", nome.Text);
                            cmd3.Parameters.AddWithValue("@desc", desc.Text);
                            cmd3.Parameters.AddWithValue("@preco", preco.Text.Replace(",","."));
                            cmd3.Parameters.AddWithValue("@genero", generodrop.SelectedValue);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    catch (Exception) {
                        Response.Redirect(Request.RawUrl);
                    }

                }
                con.Close();
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Alteração feita com sucesso!');window.location='AdminProdutos.aspx';", true);
            }
        }

        protected void EliminaProd(object sender, System.EventArgs e)
        {
            int pid = int.Parse(Request.QueryString["pid"]);
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                String qryelim = "delete from Produto where id=@id";

                using (SqlCommand cmd3 = new SqlCommand(qryelim, con))
                {
                    cmd3.Parameters.AddWithValue("@id", pid);
                    cmd3.ExecuteNonQuery();
                }
                con.Close();
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Removido com sucesso!');window.location='AdminProdutos.aspx';", true);
            }
        }
    }
}