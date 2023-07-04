using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminDelConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        int pid = int.Parse(Request.QueryString["pid"]);
                        String delete = "delete from pessoa where id=@id";
                        using (SqlCommand cmd = new SqlCommand(delete, con))
                        {
                            cmd.Parameters.AddWithValue("@id", pid);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("AdminContas.aspx");
                }
                Response.Redirect("AdminContas.aspx");
            }
        }
    }
}