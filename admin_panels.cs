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
using static LushMed.admin_login;

namespace LushMed
{
    public partial class admin_panels : Form
    {
        public admin_panels()
        {
            InitializeComponent();
            edit_panel.Visible = false;
            u_d_panel.Visible = false;
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

        private void recordbtn_Click(object sender, EventArgs e)
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

        private void editBtn_Click(object sender, EventArgs e)
        {
            edit_panel.Visible = true;
        }



        private void addPanelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("insert into med_panels values(@panelName,@panelBal)", str);
                cmnd.Parameters.AddWithValue("@panelName",addPanelName.Text);
                cmnd.Parameters.AddWithValue("@panelBal", int.Parse(addPanelActBal.Text));
                cmnd.ExecuteNonQuery();
                str.Close();
                MessageBox.Show("data added...");
                addPanelActBal.Text = string.Empty;
                addPanelName.Text = string.Empty;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void refresh_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select * from med_panels",str);
            SqlDataAdapter data = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            PanelsGrid.DataSource = dt;
            str.Close();
            edit_panel.Visible = false;
            addPanelActBal.Text = string.Empty;
            addPanelName.Text = string.Empty;
        }

        private void admin_panels_Load(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select * from med_panels", str);
            SqlDataAdapter data = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            PanelsGrid.DataSource = dt;
            str.Close();
        }

        private void UpdatePanelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("update med_panels set panelName=@panelName,panelBal=@panelBal where panelId=@panelId", str);
                cmnd.Parameters.AddWithValue("@panelId", int.Parse(SearchIdTxt.Text));
                cmnd.Parameters.AddWithValue("@panelName", editPanelName.Text);
                cmnd.Parameters.AddWithValue("@panelBal", int.Parse(editPanelBal.Text));
                cmnd.ExecuteNonQuery();
                str.Close();
                feedback.Text = "data updated";
                u_d_panel.Visible = false;
                SearchIdTxt.Text = string.Empty;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select panelName,panelBal from med_panels where panelId=@panelId", str);
            cmnd.Parameters.AddWithValue("@panelId",int.Parse(SearchIdTxt.Text));
            SqlDataReader reader = cmnd.ExecuteReader();
            if (reader.Read())
            {
                feedback.Text = string.Empty;
                u_d_panel.Visible = true;
                editPanelName.Text = reader["panelName"].ToString();
                editPanelBal.Text = reader["panelBal"].ToString();
            }
            else
            {
                feedback.Text = "invalid Id....";
                u_d_panel.Visible = false;
            }
            str.Close();


        }

        private void DeletePanelBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("delete med_panels where panelId=@panelId",str);
            cmnd.Parameters.AddWithValue("@panelId", int.Parse(SearchIdTxt.Text));
            cmnd.ExecuteNonQuery();
            str.Close();
            feedback.Text = "data Deleted";
            u_d_panel.Visible = false;
            SearchIdTxt.Text = string.Empty;
        }
    }
}
