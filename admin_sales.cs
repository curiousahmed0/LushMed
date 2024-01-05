using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LushMed.admin_login;

namespace LushMed
{
    public partial class admin_sales : Form
    {
        public admin_sales()
        {
            InitializeComponent();
        }


        private void usersbtn_Click(object sender, EventArgs e)
        {
            Admin_dashboard x = new Admin_dashboard();
            x.Show();
            this.Hide();
        }

        private void salesbtn_Click(object sender, EventArgs e)
        {
            admin_sales x = new admin_sales();
            x.Show();
            this.Hide();
        }

        private void servicesbtn_Click(object sender, EventArgs e)
        {
            admin_services x = new admin_services();
            x.Show();
            this.Hide();
        }

        private void panelsbtn_Click(object sender, EventArgs e)
        {
            admin_panels x = new admin_panels();
            x.Show();
            this.Hide();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            admin_record x = new admin_record();
            x.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            admin_login x = new admin_login();
            x.Show();
            this.Hide();
        }

    
    }
}
