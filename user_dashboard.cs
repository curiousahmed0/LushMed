﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LushMed
{
    public partial class user_dashboard : Form
    {
        public user_dashboard(string profile_name)
        {
            InitializeComponent();
            profile.Text = profile_name;
        }

        public int patient_id_for_save;


        private void Logout_Click(object sender, EventArgs e)
        {
            mainForm x = new mainForm();
            x.Show();
            this.Hide();
        }

        private void optionsBtn_Click(object sender, EventArgs e)
        {
            user_options x = new user_options(profile.Text);
            x.Show();
            this.Hide();
        }

        private void receptionbtn_Click(object sender, EventArgs e)
        {
            
            user_dashboard x = new user_dashboard(profile.Text);
            x.Show();
            this.Hide();
        }

        private void user_dashboard_Load(object sender, EventArgs e)
        {
            grid_Panel.Visible = false;
            add_patient_panel.Visible = false;
            punch_panel.Visible = false;
            FeedbackLbl.Text = string.Empty;
        }

        private void add_new_patientBtn_Click(object sender, EventArgs e)
        {
            //s_or_a_panel.Visible = false;
            //add_patient_panel.Visible = true;
            //FeedbackLbl.Text = string.Empty;

            patient_add_interface x = new patient_add_interface();
            x.Show();
        }





        // slip print
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("***************************************************", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(180,10));
            e.Graphics.DrawString("Lush Med", new Font("Arial",20, FontStyle.Bold),Brushes.Black, new Point(180,60));
            e.Graphics.DrawString("***************************************************", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(180,110));
            e.Graphics.DrawString("Name", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 200));
            e.Graphics.DrawString(Name.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 200));
            e.Graphics.DrawString("Age", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180,250));
            e.Graphics.DrawString(Age.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480,250));
            e.Graphics.DrawString("Mobile Number", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 300));
            e.Graphics.DrawString(mobile.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 300));
            e.Graphics.DrawString("Gender", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180,350));
            e.Graphics.DrawString(Gender.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 350));
            e.Graphics.DrawString("___________________________________________________", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 380));
            e.Graphics.DrawString("Service", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 430));
            e.Graphics.DrawString(service_name.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 430));
            e.Graphics.DrawString("Panel", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 480));
            e.Graphics.DrawString(Paneltxt.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 480));
            e.Graphics.DrawString("___________________________________________________", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 530));
            e.Graphics.DrawString("Total Amount", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(180, 580));
            e.Graphics.DrawString(service_price.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(480, 580));
            e.Graphics.DrawString("Generated By", new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(450, 850));
            e.Graphics.DrawString(profile.Text, new Font("Arial", 16, FontStyle.Regular), Brushes.Black, new Point(700, 850));
        }








        private void search_patientBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("select patId,patName,patAge,patMbn,patGender from med_patients where patMbn=@patMbn ", str);
                cmnd.Parameters.AddWithValue("@patMbn", int.Parse(patient_mbn_for_search.Text));
                SqlDataReader reader = cmnd.ExecuteReader();
                if (reader.Read())
                {
                    FeedbackLbl.Text = string.Empty;
                    patient_mbn_for_search.Text = string.Empty;
                    string x = reader["patId"].ToString();
                    patient_id_for_save = int.Parse(x);
                    Name.Text = reader["patName"].ToString();
                    Age.Text = reader["patAge"].ToString();
                    mobile.Text = reader["patMbn"].ToString();
                    Gender.Text = reader["patGender"].ToString();
                    add_patient_panel.Visible = true;
                    punch_panel.Visible = false;
                    str.Close();
                }
                else
                {
                    FeedbackLbl.Text = "no data exists for this mobile number....";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void id_service_btn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("select serviceId,serviceName,servicePrice,serviceAvail from med_services where serviceId=@serviceId", str);
                cmnd.Parameters.AddWithValue("@serviceId", id_service_search.Text);
                SqlDataReader reader = cmnd.ExecuteReader();
                if (reader.Read())
                {
                    FeedbackLbl.Text = string.Empty;
                    service_name.Text = reader["serviceName"].ToString();
                    service_price.Text = reader["servicePrice"].ToString();
                    string x = reader["serviceAvail"].ToString();
                    if (int.Parse(x) == 1)
                    {
                        service_avail.Text = "Yes";
                    }
                    else
                    {
                        service_avail.Text = "No";
                    }
                    punch_panel.Visible = true;
                }
                else
                {
                    FeedbackLbl.Text = "no service Exist with this Id..";
                }
                str.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }




        private void save_slip_Click(object sender, EventArgs e)
        {
            if (service_avail.Text == "Yes")
            {
                try
                {

                    SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True; MultipleActiveResultSets=true;");
                    str.Open();
                    SqlCommand cmnd = new SqlCommand("insert into med_slips values(@patId,@serviceId,@panelId,@userName)", str);
                    cmnd.Parameters.AddWithValue("@patId", patient_id_for_save);
                    cmnd.Parameters.AddWithValue("@serviceId", id_service_search.Text);
                    int panel_id;
                    SqlCommand cmnd1 = new SqlCommand("select panelId,panelBal from med_panels where panelName=@panelName", str);
                    cmnd1.Parameters.AddWithValue("@panelName", Paneltxt.Text);
                    SqlDataReader reader = cmnd1.ExecuteReader();
                    if (reader.Read())
                    {
                        string x = reader["panelId"].ToString();
                        panel_id = int.Parse(x);
                        cmnd.Parameters.AddWithValue("@panelId", panel_id);
                        cmnd.Parameters.AddWithValue("@userName", profile.Text);
                        cmnd.ExecuteNonQuery();
                        
                        
                        SqlCommand cmnd2 = new SqlCommand("select userSale from med_Users where userName=@userName", str);
                        cmnd2.Parameters.AddWithValue("@userName",profile.Text);
                        SqlDataReader reader1 = cmnd2.ExecuteReader();
                        if (reader1.Read())
                        {
                            x = reader1["userSale"].ToString();
                            int a;
                            a = int.Parse(service_price.Text);
                            a = a + int.Parse(x);
                            SqlCommand cmnd3 = new SqlCommand("update med_Users set userSale=@userSale where userName=@userName ", str);
                            cmnd3.Parameters.AddWithValue("@userSale", a);
                            cmnd3.Parameters.AddWithValue("@userName", profile.Text);
                            cmnd3.ExecuteNonQuery();
                        }

                       x = reader["panelBal"].ToString();
                        int temp;
                        temp = int.Parse(service_price.Text);
                        temp = temp + int.Parse(x);

                        SqlCommand cmnd4 = new SqlCommand("update med_panels set panelBal=@panelBal where panelId=@panelId", str);
                        cmnd4.Parameters.AddWithValue("@panelId", panel_id);
                        cmnd4.Parameters.AddWithValue("panelBal", temp);
                        cmnd4.ExecuteNonQuery();
                        





                        FeedbackLbl.Text = "Slip is succesfully saved....!";
                    }
                    else
                    {
                        MessageBox.Show("No panel exists with this Name try Puting valid Name");
                    }

                    str.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                FeedbackLbl.Text = "This service is currently not available...";
            }


        }

        private void print_slip_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
            add_patient_panel.Visible = false;
            punch_panel.Visible = false;
            FeedbackLbl.Text = string.Empty;
        }

        private void all_services_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select * from med_services", str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            data_grid.DataSource = dt;
            str.Close();
            grid_Panel.Visible = true;
        }

        private void all_panels_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("select panelId,panelName from med_panels", str);
            SqlDataAdapter data = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            data_grid.DataSource = dt;
            str.Close();
            grid_Panel.Visible = true;
        }
    }
}
