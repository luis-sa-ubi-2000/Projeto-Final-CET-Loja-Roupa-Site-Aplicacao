using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows;

namespace Loja
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            //Usar session["id"] 
            Search.Attributes.Add("OnClick", "pesquisa");
            Search.Click += new EventHandler(pesquisa);
            logout.Attributes.Add("OnClick", "logoutbutton");
            logout.Click += new EventHandler(logoutbutton);
            checkout.Attributes.Add("OnClick", "confirmar");
            checkout.Click += new EventHandler(confirmar);
            inserir.Attributes.Add("OnClick", "aplicadesconto");
            inserir.Click += new EventHandler(aplicadesconto);
            if (Session["id"] != null)
            {
                log.Text = "A minha conta";
                log.NavigateUrl = "Conta.aspx";
                minhaconta.NavigateUrl = "Conta.aspx";
                logout.Visible = true;
                wishlist.Visible = true;
                if (!IsPostBack)
                {
                    //mostrar produtos no carrinho e confirmar dados/pagamento. PODE ALTERAR MORADA E CONTACTO
                    using (SqlConnection con = new SqlConnection())
                    {

                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        String qry = "select * from carrinho inner join produto on produto.id=carrinho.id_produto inner join imagem on produto.id_imagem=imagem.id  where pessoaid='" + Session["id"] + "'";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            prodcarrinho.Text += "<br><p style='margin-left: 12%;'>";
                            prodcarrinho.Text += "<img src='" + dr[17].ToString() + "' style='width:90px;height:90px; position:absolute; margin-left:-140px;'/><br>";
                            prodcarrinho.Text += "Nome:  ";
                            prodcarrinho.Text += dr[6].ToString();
                            prodcarrinho.Text += "  |  Quantidade: ";
                            prodcarrinho.Text += dr[1].ToString();
                            prodcarrinho.Text += "   | Tamanho: ";
                            prodcarrinho.Text += dr[5].ToString();
                            prodcarrinho.Text += "<br>Preço:  ";
                            prodcarrinho.Text += dr[3].ToString();
                            prodcarrinho.Text += " &#8364";
                            prodcarrinho.Text += "<br><a class='button is-small is-danger' href='DelCarrinho.aspx?pid=" + dr[0].ToString() + "'>Remover</a><br><br>";
                        }
                        dr.Close();
                        String qry2 = "select sum(total) from carrinho where pessoaid='" + Session["id"] + "'";
                        SqlCommand cmd2 = new SqlCommand(qry2, con);
                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        if (dr2.Read())
                        {
                            valorsemdesconto.Text = dr2[0].ToString();
                            valortotal.Text = dr2[0].ToString();
                        }
                        dr2.Close();
                        con.Close();

                    }
                }
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
                if (!IsPostBack)
                {
                    //mostrar produtos no carrinho e confirmar dados/pagamento. PODE ALTERAR MORADA E CONTACTO
                    using (SqlConnection con = new SqlConnection())
                    {

                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        String qry = "select * from carrinhotemporario inner join produto on produto.id=carrinhotemporario.idproduto inner join imagem on produto.id_imagem=imagem.id  where sessionid='" + Session["temporario"] + "'";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            prodcarrinho.Text += "<br><p>";
                            prodcarrinho.Text += "<img src='" + dr[17].ToString() + "' style='width:90px;height:90px; position:absolute; margin-left:-440px;'/><br>";
                            prodcarrinho.Text += "Nome:  ";
                            prodcarrinho.Text += dr[6].ToString();
                            prodcarrinho.Text += "  |  Quantidade: ";
                            prodcarrinho.Text += dr[1].ToString();
                            prodcarrinho.Text += "   | Tamanho: ";
                            prodcarrinho.Text += dr[5].ToString();
                            prodcarrinho.Text += "<br>Preço:  ";
                            prodcarrinho.Text += dr[3].ToString();
                            prodcarrinho.Text += " &#8364";
                            prodcarrinho.Text += "<br><a onclick='return confirm(`Prentende mesmo eliminar este item do carrinho?`)' class='button is-small is-danger' href='DelCarrinho.aspx?pid=" + dr[0].ToString() + "'>Remover</a><br><br>";
                        }
                        dr.Close();
                        String qry2 = "select sum(total) from carrinho where pessoaid='" + Session["id"] + "'";
                        SqlCommand cmd2 = new SqlCommand(qry2, con);
                        SqlDataReader dr2 = cmd2.ExecuteReader();

                        if (dr2.Read())
                        {
                            valorsemdesconto.Text = dr2[0].ToString();
                            valortotal.Text = dr2[0].ToString();
                        }
                        dr2.Close();
                        con.Close();

                    }
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
           
            


        }
        private void pesquisa(object sender, System.EventArgs e)
        {
            String roberto = TextBox1.Text;
            Response.Redirect("Procurar.aspx?procura=" + roberto);
        }

        protected void aplicadesconto(object sender, System.EventArgs e)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    decimal desconto;
                    string percentagem = "select max(desconto) from voucher where codigo=@id";
                    using (SqlCommand cmd3 = new SqlCommand(percentagem, con))
                    {
                        cmd3.Parameters.AddWithValue("@id", voucher.Text);

                        desconto = (decimal)cmd3.ExecuteScalar();
                    }
                    decimal preco = Convert.ToDecimal(valortotal.Text) - (Convert.ToDecimal(valortotal.Text) * (desconto / 100));
                    String descontoformat = (Convert.ToDecimal(valortotal.Text) * (desconto / 100)).ToString();
                    menos.Text = descontoformat.Remove(descontoformat.Length - 2);
                    //MessageBox.Show(preco.ToString());
                    String format = preco.ToString();
                    valortotal.Text = format.Remove(format.Length - 2);
                    voucher.Enabled = false;
                }
                   
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Código invalido! Tente novamente, decerteza que existe algum!');", true);
                
                }
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Código aplicado com sucesso!');", true);
                
            }
        }
        private void confirmar(object sender, System.EventArgs e)
        {
            if (Session["id"] != null) {
                using (SqlConnection con = new SqlConnection())
                {
                    try {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        String qry = "insert into orders values (@id,GETDATE(),@total,0,@morada,@destinatario,@tel,@nif,@desconto)";
                        using (SqlCommand cmd2 = new SqlCommand(qry, con))
                        {
                            cmd2.Parameters.AddWithValue("@id", Session["id"]);
                            cmd2.Parameters.AddWithValue("@total", valorsemdesconto.Text.Replace(',', '.'));
                            cmd2.Parameters.AddWithValue("@morada", morada.Text);
                            cmd2.Parameters.AddWithValue("@destinatario", nomepessoa.Text);
                            cmd2.Parameters.AddWithValue("@tel", telefone.Text);
                            cmd2.Parameters.AddWithValue("@nif", nif.Text);
                            cmd2.Parameters.AddWithValue("@desconto", valortotal.Text.Replace(',', '.'));
                            cmd2.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Encomenda finalizada com sucesso');window.location='Index.aspx';", true);
                        }

                        int idorder;
                        string orderid = "select MAX(id) from orders";
                        using (SqlCommand cmd3 = new SqlCommand(orderid, con))
                        {
                            idorder = (int)cmd3.ExecuteScalar();
                        }

                        String cartitem = "select * from carrinho inner join produto on produto.id=carrinho.id_produto inner join imagem on produto.id_imagem=imagem.id  where pessoaid='" + Session["id"] + "'";
                        SqlCommand cmd = new SqlCommand(cartitem, con);
                        List<int> idproduto = new List<int>();
                        List<int> qtd = new List<int>();
                        List<decimal> price = new List<decimal>();

                        SqlDataReader dr = cmd.ExecuteReader();
                        String orderitem = "insert into orderitem values (@idorder,@idprod,@qtd,@preco)";
                        while (dr.Read())
                        {

                            idproduto.Add(Convert.ToInt32(dr[7]));
                            qtd.Add(Convert.ToInt32(dr[1]));
                            price.Add(Convert.ToDecimal(dr[3]));
                        }
                        dr.Close();

                        for (int i = 0; i < idproduto.Count; i++)
                        {
                            SqlCommand cmd2 = new SqlCommand(orderitem, con);
                            cmd2.Parameters.AddWithValue("@idorder", idorder);
                            cmd2.Parameters.AddWithValue("@idprod", idproduto[i]);
                            cmd2.Parameters.AddWithValue("@qtd", qtd[i]);
                            cmd2.Parameters.AddWithValue("@preco", price[i]);
                            cmd2.ExecuteNonQuery();
                        }

                        String delete = "delete from carrinho where pessoaid=@id";
                        using (SqlCommand cmd2 = new SqlCommand(delete, con))
                        {
                            cmd2.Parameters.AddWithValue("@id", Session["id"]);
                            cmd2.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Encomenda finalizada com sucesso');window.location='Index.aspx';", true);
                        }
                    }
                    catch (Exception)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('ERRO! Verifique os seus dados para finalizar a compra!');window.location='Carrinho.aspx';", true);
                    }
                }
            }
            else { Response.Redirect("Login.aspx"); }
            }

        protected void logoutbutton(object sender, System.EventArgs e)
        {
            Session.Remove("id");
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Obrigado pela visita!');window.location='Login.aspx';", true);
        }
    }
}