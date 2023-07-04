using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class Registar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            submeter.Attributes.Add("OnClick", "registar");
            submeter.Click += new EventHandler(registar);
        }

        protected void registar(object sender, System.EventArgs e)
        {
            if(email.Text==confirmemail.Text && password.Text == confirmpassword.Text) { 
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Conn.ligacao;
                con.Open();
                String sql = "insert into Pessoa(nome,nif,email,telefone,senha,datanascimento,id_tipoutilizador)  values (@nome,@nif,@email,@tel,@pass,@datanasc,1)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@nif", nif.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@email", email.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@tel", tel.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@pass", password.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@datanasc", datanasc.Text.Replace("'", "''"));
                    cmd.ExecuteNonQuery();
                }

                con.Close();

                    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Registou-se com sucesso!');window.location='Login.aspx';", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Os dados inseridos estão incorretos, tente novamente');window.location='Registar.aspx';", true);

            }
        }
    }
}