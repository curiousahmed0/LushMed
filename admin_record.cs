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
    public partial class admin_record : Form
    {
        public admin_record()
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

        private void record_Click(object sender, EventArgs e)
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

        private void admin_record_Load(object sender, EventArgs e)
        {
            Action_panel.Visible = false;
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select * from med_patients", str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            patient_grid.DataSource = dt;
            str.Close();
        }

        private void refresh_Click(object sender, EventArgs e)
        {

            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select * from med_patients", str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            patient_grid.DataSource = dt;
            str.Close();
        }

        private void searchForAction_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("select * from med_patients where patId=@patId", str);
                cmnd.Parameters.AddWithValue("@patId", int.Parse(idForAction.Text));
                SqlDataReader reader = cmnd.ExecuteReader();
                if (reader.Read())
                {
                    nameTxt.Text = reader["patName"].ToString();
                    ageTxt.Text = reader["patAge"].ToString();
                    mbnTxt.Text = reader["patMbn"].ToString();
                    genderTxt.Text = reader["patGender"].ToString();
                    feedbackLbl.Text = string.Empty;
                    Action_panel.Visible = true;
                }
                else
                {
                    feedbackLbl.Text = "invalid id....";
                }
                str.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("delete med_patients where patId=@patId", str);
            cmnd.Parameters.AddWithValue("@padId",int.Parse(idForAction.Text));
            cmnd.ExecuteNonQuery();
            feedbackLbl.Text = "data seccesfully deleted";
            Action_panel.Visible= false;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("Update med_patients set patName=@patName,patAge=@patAge,patMbn=@patMbn,patGender=@patGender where patId=@patId", str);
                cmnd.Parameters.AddWithValue("@patId", int.Parse(idForAction.Text));
                cmnd.Parameters.AddWithValue("@patName", nameTxt.Text);
                cmnd.Parameters.AddWithValue("@patAge", ageTxt.Text);
                cmnd.Parameters.AddWithValue("@patMbn", mbnTxt.Text);
                cmnd.Parameters.AddWithValue("patGender", genderTxt.Text);
                cmnd.ExecuteNonQuery();
                str.Close();
                Action_panel.Visible = false;
                feedbackLbl.Text = "Record Updated...";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
