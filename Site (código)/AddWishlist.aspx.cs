using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loja
{
    public partial class AddWishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                if (Session["id"] == null)
                {
                Response.Redirect("Login.aspx");
                }
                else { 
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = Conn.ligacao;
                        con.Open();
                        int pid = int.Parse(Request.QueryString["pid"]);
                            SqlCommand check = new SqlCommand("select * from wishlist where id_produto=@prod AND id_pessoa=@id", con);
                                check.Parameters.AddWithValue("@prod", pid);
                                check.Parameters.AddWithValue("@id", Session["id"]);
                                object exist = check.ExecuteScalar();
                    if (exist != null) {
                        Response.Redirect("Wishlist.aspx");
                    }
                    else { 
                        String insert = "insert into wishlist values(@produto,@pessoa)";
                        using (SqlCommand cmd2 = new SqlCommand(insert, con))
                        {
                                cmd2.Parameters.AddWithValue("@produto", pid);
                                cmd2.Parameters.AddWithValue("@pessoa", Session["id"].ToString());
                                cmd2.ExecuteNonQuery();
                        }
                        con.Close();
                        Response.Redirect("Wishlist.aspx");
                    }
                }
            }

            }
          
        }
        }
    
