using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class Produto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["pid"] != null) { 
            Search.Attributes.Add("OnClick", "pesquisa");
            Search.Click += new EventHandler(pesquisa);
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            addCart.Attributes.Add("OnClick", "addCarrinho");
            addCart.Click += new EventHandler(addCarrinho);
                if (Session["id"] != null)
                {
                    log.Text = "A minha conta";
                    log.NavigateUrl = "Conta.aspx";
                    minhaconta.NavigateUrl = "Conta.aspx";
                    logout.Visible = true;
                    wishlist.Visible = true;
                    using (SqlConnection con = new SqlConnection())
                    {

                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select * from carrinho where pessoaid=" + Session["id"], con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            notix.Visible = true;
                        }
                        else { notix.Visible = false; }
                        dr.Close();
                        cmd.Dispose();
                    }
                }
                else
                {
                    minhaconta.Attributes.Add("onclick", "this.disabled=true");
                    minhaconta.NavigateUrl = "Login.aspx";
                    logout.Visible = false;
                    if (Session["temporario"] == null)
                    {
                        Random rnd = new Random();
                        Session["temporario"] = rnd.Next(1, 100);

                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection())
                        {

                            con.ConnectionString = Conn.ligacao;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select * from carrinhotemporario where sessionid=" + Session["temporario"], con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                notix.Visible = true;
                            }
                            else { notix.Visible = false; }
                            dr.Close();
                            cmd.Dispose();
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection())
            {
                int pid = int.Parse(Request.QueryString["pid"]);

                con.ConnectionString = Conn.ligacao;
                con.Open();


                SqlCommand cmd = new SqlCommand("select * from Produto inner join imagem on produto.id_imagem = imagem.id where produto.id=" + pid, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    imagem.Text = "<img src = '"+dr[9].ToString()+"' alt='rip' style='width:550px;height:550px;' /> ";
                        descricacao.Text = dr[2].ToString();
                    nomee.Text = dr[1].ToString();
                    String formatar = dr[3].ToString();
                    //Console.WriteLine(formatar);
                    preco.Text = formatar.Remove(formatar.Length -2);
                    
                    //Imagem -> Arranjar maneira de meter na base da dos sem alterar!!!!
                    total.Text = precototal(quantidade.Text, preco.Text);

                }

                dr.Close();

                cmd.Dispose();

                con.Close();
            }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void addCarrinho(object sender, EventArgs e)
        {

            if (Session["id"] == null)
            {
                

                try { 
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    String sql = "insert into carrinhotemporario(qtd,preco,total,sessionid,tamanho,nome,idproduto)  values (@qtd,@prc,@tot,@pessoaid,@size,@produto,@id)";
                    int pid = int.Parse(Request.QueryString["pid"]);
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@produto", nomee.Text);
                        cmd.Parameters.AddWithValue("@qtd", quantidade.Text);
                        cmd.Parameters.AddWithValue("@prc", preco.Text.Replace(',', '.'));
                        cmd.Parameters.AddWithValue("@tot", total.Text.Replace(',', '.'));
                        cmd.Parameters.AddWithValue("@pessoaid", Session["temporario"]);
                        cmd.Parameters.AddWithValue("@size", tamanho.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@id", pid);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    Response.Redirect("Carrinho.aspx");
                }
                }
                catch (Exception) { Response.Redirect("Index.aspx"); }
            }
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                String sql = "insert into carrinho(qtd,preco,total,pessoaid,tamanho,nome,id_produto)  values (@qtd,@prc,@tot,@pessoaid,@size,@produto,@id)";
                int pid = int.Parse(Request.QueryString["pid"]);
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@produto", nomee.Text);
                    cmd.Parameters.AddWithValue("@qtd", quantidade.Text);
                    cmd.Parameters.AddWithValue("@prc", preco.Text.Replace(',','.'));
                    cmd.Parameters.AddWithValue("@tot", total.Text.Replace(',','.'));
                    cmd.Parameters.AddWithValue("@pessoaid", Session["id"]);
                    cmd.Parameters.AddWithValue("@size", tamanho.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@id", pid);
                    cmd.ExecuteNonQuery();
                }

                con.Close();

                Response.Redirect("Carrinho.aspx");
            }
        }

        protected void alteracaoqtd(object sender, EventArgs e)
        {
            total.Text = precototal(quantidade.Text, preco.Text);
        }

        private string precototal(string qtd, string preco)
        {
            try { 
            int qty = int.Parse(qtd);
            Decimal prc = Convert.ToDecimal(preco);

            Decimal totalidade = qty * prc;

            return totalidade.ToString();
            }
            catch (Exception)
            {
                Response.Redirect(Request.RawUrl);
            }

            String teste="";
            return teste;
        }

        protected void pesquisa(object sender, System.EventArgs e)
        {
            String procura = TextBox1.Text;
            Response.Redirect("Procurar.aspx?procura=" + procura);
        }
        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}