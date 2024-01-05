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
    public partial class admin_services : Form
    {
        
        public admin_services()
        {
            InitializeComponent();
            deletePanel.Visible = false;
            updatePanel.Visible = false;
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

      

        private void addServiceBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("insert into med_services values(@serviceId,@serviceName,@servicePrice,@serviceAvail)", str);
                cmnd.Parameters.AddWithValue("@serviceId", addServiceId.Text);
                cmnd.Parameters.AddWithValue("@serviceName", addServiceName.Text);
                cmnd.Parameters.AddWithValue("@servicePrice", int.Parse(addServicePrice.Text));
                if (addServicesSwitch.Checked == true)
                {
                    cmnd.Parameters.AddWithValue("@serviceAvail", 1);
                }
                else
                {
                    cmnd.Parameters.AddWithValue("@serviceAvail", 0);
                }
                cmnd.ExecuteNonQuery();
                str.Close();
                MessageBox.Show("Service Succesfully added....!");
                addServiceName.Text = string.Empty;
                addServiceId.Text = string.Empty;
                addServicePrice.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void updateSearchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select serviceName,servicePrice,serviceAvail from med_services where serviceId=@serviceId",str);
            cmnd.Parameters.AddWithValue("@serviceId",updateIdTxt.Text);
            SqlDataReader reader = cmnd.ExecuteReader();
            if(reader.Read())
            {
                UpdateServiceName.Text = reader["serviceName"].ToString();
                updateServicePrice.Text = reader["servicePrice"].ToString();
                string a = reader["serviceAvail"].ToString();
                if (int.Parse(a) == 1)
                {
                    UpdateSwitch.Checked = true;
                }
                else
                {
                    UpdateSwitch.Checked= false;
                }

                updateFeedback.Text = string.Empty;
                updatePanel.Visible = true;
            }
            else
            {
                updateFeedback.Text = "invalid id ";
                updatePanel.Visible = false;
            }
            str.Close();

        }

        private void UpdateServiceBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Update med_services set serviceName=@serviceName,servicePrice=@servicePrice,serviceAvail=@serviceAvail where serviceId=@serviceId",str);
            cmnd.Parameters.AddWithValue("@serviceId",updateIdTxt.Text);
            cmnd.Parameters.AddWithValue("@serviceName",UpdateServiceName.Text);
            cmnd.Parameters.AddWithValue("@servicePrice",int.Parse(updateServicePrice.Text));
            if(UpdateSwitch.Checked==true)
            {
                cmnd.Parameters.AddWithValue("@serviceAvail", 1);
            }
            else
            {
                cmnd.Parameters.AddWithValue("@serviceAvail", 0);
            }
            cmnd.ExecuteNonQuery();
            str.Close();
            updatePanel.Visible = false;
            updateFeedback.Text = "Data successfully updated";
            updateIdTxt.Text = string.Empty;
        }

        private void delSearchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select serviceId,serviceName,servicePrice,serviceAvail from med_services where serviceId=@serviceId", str);
            cmnd.Parameters.AddWithValue("@serviceId",delIdTxt.Text);
            SqlDataReader reader = cmnd.ExecuteReader();
            if(reader.Read())
            {
                deletePanel.Visible = true;
                delFeedback.Text = string.Empty;
                delName.Text = reader["serviceName"].ToString();
                delPrice.Text = reader["servicePrice"].ToString();
                delId.Text = reader["serviceId"].ToString();
                string a = reader["serviceAvail"].ToString();
                if(int.Parse(a)==0)
                {
                    delSwitch.Checked = false;
                }
                else
                {
                    delSwitch.Checked = true;
                }

            }
            else
            {
                delFeedback.Text = "invalid Id...";
                deletePanel.Visible= false;
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Delete med_services where serviceId=@serviceId", str);
            cmnd.Parameters.AddWithValue("@serviceId", delIdTxt.Text);
            cmnd.ExecuteNonQuery();
            str.Close();
            deletePanel.Visible = false;
            delFeedback.Text = "Data succesfully deleted";
            delIdTxt.Text = string.Empty;
        }

        private void admin_services_Load(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select * from med_services",str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            ServicesGrid.DataSource = dt;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select * from med_services", str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            ServicesGrid.DataSource = dt;

        }
    }
}
