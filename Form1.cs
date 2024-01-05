using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LushMed
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/lush_ahmed/");
        }

        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://web.facebook.com/profile.php?id=100014623038543");
        }

        private void bunifuPictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ahlush.000webhostapp.com/");
        }

        private void adminloginbtn_Click(object sender, EventArgs e)
        {
            admin_login x = new admin_login();
            x.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmd = new SqlCommand("select userPass from med_Users where userUserName=@userUserName",str);
            cmd.Parameters.AddWithValue("@userUserName",nametxt.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                string a = reader["userPass"].ToString();
                if(passtxt.Text == a) {
                    user_dashboard x = new user_dashboard(nametxt.Text);
                    x.Show();
                    this.Hide();
                }
                else
                {
                    feedback.Text = "invalid password...";
                }
            }
            else
            {
                feedback.Text = "invalid username....";
            }

            str.Close();
        }
    }
}
