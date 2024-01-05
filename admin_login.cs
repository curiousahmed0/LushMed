using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LushMed
{
    

    public partial class admin_login : Form
    {
        public admin_login()
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

        private void userloginbtn_Click(object sender, EventArgs e)
        {
            mainForm x = new mainForm();
            x.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if(nametxt.Text=="lush" && passtxt.Text=="6969")
            {
                Admin_dashboard x = new Admin_dashboard();
            
                x.Show();
                this.Hide();
            }
            else
            {
                feedback.Text = "invalid username or password...!";
                nametxt.Text = string.Empty;
                passtxt.Text = string.Empty;
            }
        }

   
    }
}
