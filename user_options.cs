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
    public partial class user_options : Form
    {
        public user_options(string profile_name)
        {
            InitializeComponent();
           profile.Text = profile_name;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            mainForm x = new mainForm();
            x.Show();
            this.Hide();
        }

        private void receptionbtn_Click(object sender, EventArgs e)
        {
            user_dashboard x = new user_dashboard(profile.Text);

            x.Show();
            this.Hide();
        }

        private void optionsBtn_Click(object sender, EventArgs e)
        {
            user_options x = new user_options(profile.Text);
            x.Show();
            this.Hide();
        }

        private void Save_pass_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True; MultipleActiveResultSets=true;");
                str.Open();
                SqlCommand cmnd = new SqlCommand("select userPass from med_Users where userUserName=@userUserName", str);
                cmnd.Parameters.AddWithValue("@userUserName", profile.Text);
                SqlDataReader reader = cmnd.ExecuteReader();
                if (reader.Read())
                {
                    if (new_pass.Text != confirm_pass.Text)
                    {
                        FeedbackLbl.Text = " Passwords are not Same...";
                    }
                    else
                    {
                        SqlCommand cmnd1 = new SqlCommand("update med_Users set userPass=@userPass where userUserName=@userUserName", str);
                        cmnd1.Parameters.AddWithValue("@userUserName", profile.Text);
                        cmnd1.Parameters.AddWithValue("@userPass", confirm_pass.Text);
                        cmnd1.ExecuteNonQuery();
                        FeedbackLbl.Text = "Password Succesfully Saved...";
                        present_pass.Text = string.Empty;
                        new_pass.Text = string.Empty;
                        confirm_pass.Text = string.Empty;
                    }
                }
                else
                {
                    FeedbackLbl.Text = "Wrong Password...";
                }
                str.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Cash_tally_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True; MultipleActiveResultSets=true;");
                str.Open();
                SqlCommand cmnd = new SqlCommand("select * from med_slips where userName=@userName", str);
                cmnd.Parameters.AddWithValue("@userName", profile.Text);
                SqlDataAdapter data = new SqlDataAdapter(cmnd);
                DataTable dt = new DataTable();
                data.Fill(dt);
                data_Grid.DataSource = dt;
                SqlCommand cmnd1 = new SqlCommand("Select userSale from med_Users where userUserName=@userUserName", str);
                cmnd1.Parameters.AddWithValue("@userUserName", profile.Text);
                SqlDataReader reader = cmnd1.ExecuteReader();
                if (reader.Read())
                {
                    Total_Sale.Text = reader["userSale"].ToString();
                }
                str.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
