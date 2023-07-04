using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class DelWishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Conn.ligacao;
                    con.Open();
                    int pid = int.Parse(Request.QueryString["pid"]);
                    String delete = "delete from wishlist where id_produto=@id AND id_pessoa=@idpessoa";
                    using (SqlCommand cmd = new SqlCommand(delete, con))
                    {
                        cmd.Parameters.AddWithValue("@id", pid);
                        cmd.Parameters.AddWithValue("@idpessoa", Session["id"]);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception)
            {
                Response.Redirect("Wishlist.aspx");
            }
            Response.Redirect("Wishlist.aspx");
        }
    }
}