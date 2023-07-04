using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AdminDelEncomenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                 
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        int pid = int.Parse(Request.QueryString["pid"]);
                    String deleteitem = "delete from orderitem where id_order=@id";
                    String deleteorder = "delete from orders where id=@id";
                    using (SqlCommand cmd = new SqlCommand(deleteitem, con))
                    {
                        cmd.Parameters.AddWithValue("@id", pid);
                        cmd.ExecuteNonQuery();
                        
                    }
                    
                    using (SqlCommand cmd = new SqlCommand(deleteorder, con))
                        {
                            cmd.Parameters.AddWithValue("@id", pid);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }
               
                Response.Redirect("AdminEncomendas.aspx");
            }
        }
    }
}