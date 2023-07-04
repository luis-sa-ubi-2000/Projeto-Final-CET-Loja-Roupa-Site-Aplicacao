using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            submeter.Attributes.Add("OnClick", "submit");
            submeter.Click += new EventHandler(submit);
        }

        protected void submit(object sender, EventArgs e)
        {
            String mail = email.Text.Replace("'","''");
            String pass = password.Text.Replace("'", "''");

            //Refazer con com base de dados original
            using(SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                String qry = "select * from Pessoa where email='" + mail + "' and senha='" + pass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[9].ToString()=="99")
                    {
                        Session["admin"] = dr[9];
                        ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Bem-vindo administrador!');window.location='AdminIndex.aspx';", true);
                    }
                    else { 
                    Session["id"] = dr[0];
                    dr.Close();
                    cmd.Dispose();
                        try { 
                            String trocacart = "select * from carrinhotemporario inner join produto on produto.id=carrinhotemporario.idproduto inner join imagem on produto.id_imagem=imagem.id  where sessionid='" + Session["temporario"] + "'";
                            SqlCommand cmd2 = new SqlCommand(trocacart, con);
                            List<int> idproduto = new List<int>();
                            List<int> qtd = new List<int>();
                            List<String> tamanho = new List<String>();
                            List<String> nome = new List<String>();
                            List<decimal> preco = new List<decimal>();
                            List<decimal> total = new List<decimal>();
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {

                                idproduto.Add(Convert.ToInt32(dr2[7]));
                                qtd.Add(Convert.ToInt32(dr2[1]));
                                preco.Add(Convert.ToDecimal(dr2[2]));
                                total.Add(Convert.ToDecimal(dr2[3]));
                                tamanho.Add(dr2[5].ToString());
                                nome.Add(dr2[6].ToString());
                            }
                            dr2.Close();
                            String cartnormal = "insert into carrinho values (@qtd,@preco,@total,@id,@tamanho,@nome,@idprod)";
                            for (int i = 0; i < idproduto.Count; i++)
                            {
                                SqlCommand cmd3 = new SqlCommand(cartnormal, con);
                                cmd3.Parameters.AddWithValue("@id",Session["id"] );
                                cmd3.Parameters.AddWithValue("@idprod", idproduto[i]);
                                cmd3.Parameters.AddWithValue("@preco", preco[i]);
                                cmd3.Parameters.AddWithValue("@total", total[i]);
                                cmd3.Parameters.AddWithValue("@qtd", qtd[i]);
                                cmd3.Parameters.AddWithValue("@tamanho", tamanho[i]);
                                cmd3.Parameters.AddWithValue("@nome", nome[i]);
                                cmd3.ExecuteNonQuery();
                            }
                            Session.Remove("temporario");
                            String limpacarttemp = "delete from carrinhotemporario";
                            SqlCommand cmd4 = new SqlCommand(limpacarttemp, con);
                            cmd4.ExecuteNonQuery();
                            
                        }
                        catch (Exception)
                        {
                            Response.Redirect("Login.aspx");
                        }
                                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Bem-vindo!');window.location='Index.aspx';", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Falhou a autentificação, tente novamente.');window.location='Login.aspx';", true);
                }
                
                con.Close();

            }

}
}
}
 
 