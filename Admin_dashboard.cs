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
    public partial class Admin_dashboard : Form
    {
        public Admin_dashboard()
        {
            InitializeComponent();
           updatePanel.Visible = false;
            deletePanel.Visible = false;
        }

        public static string id_forUpdation;
        public static string id_forDeletion;



        private void salesbtn_Click(object sender, EventArgs e)
        {
            admin_sales x = new admin_sales();
            x.Show();
            this.Hide();
        }

        private void servicesbtn_Click(object sender, EventArgs e)
        {
            admin_services x = new admin_services();
            x .Show();
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

        private void usersbtn_Click(object sender, EventArgs e)
        {
            Admin_dashboard x = new Admin_dashboard();
            x.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            admin_login x = new admin_login();
            x.Show();
            this.Hide();
        }

        private void Admin_dashboard_Load(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select * from med_Users",str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            allUsers.DataSource = dt;
        }

        private void searchUpdatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("Select userId,userName,userAge,userMbn,userSal,userUserName,userPass,userSale from med_Users where userId=@userId", str);
                cmnd.Parameters.AddWithValue("@userId", int.Parse(idForupdate.Text));
                SqlDataReader reader = cmnd.ExecuteReader();
                if (reader.Read())
                {
                    updatefeedback.Text = string.Empty;
                    id_forUpdation = reader["userId"].ToString();
                    nameUpdatetxt.Text = reader["userName"].ToString();
                    ageUpdatetxt.Text = reader["userAge"].ToString();
                    mbnUpdatetxt.Text = reader["userMbn"].ToString();
                    salUpdatetxt.Text = reader["userSal"].ToString();
                    usernameUpdatetxt.Text = reader["userUserName"].ToString();
                    passUpdatetxt.Text = reader["userPass"].ToString();
                    salesUpdateTxt.Text = reader["userSale"].ToString();
                    updatePanel.Visible = true;
                }
                else
                {
                    updatefeedback.Text = "invalid...";
                    updatePanel.Visible = false;
                    idForupdate.Text = string.Empty;
                }
                str.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
          
        }

        private void idFordelete_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select userId,userName,userAge,userMbn,userSal,userUserName,userPass from  med_Users where userId=@userId", str);
            cmnd.Parameters.AddWithValue("@userId",int.Parse(idDeltxt.Text));
            SqlDataReader reader = cmnd.ExecuteReader();
            if(reader.Read())
            {
                delFeedback.Text = string.Empty;
                id_forDeletion = reader["userId"].ToString();
                delName.Text = reader["userName"].ToString();
                delAge.Text = reader["userAge"].ToString();
                delMbn.Text = reader["userMbn"].ToString();
                delSal.Text = reader["userSal"].ToString();
                delUsername.Text = reader["userUserName"].ToString();
                delPass.Text = reader["userPass"].ToString();
                deletePanel.Visible = true;
            }
            else
            {
                delFeedback.Text = "Invalid Input....!";
                deletePanel.Visible = false;
                idDeltxt.Text = string.Empty;
            }

            str.Close();

        }

        private void addUserbtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("insert into med_Users values(@userName,@userAge,@userMbn,@userSal,@userUserName,@userPass,@userSale)", str);
                cmnd.Parameters.AddWithValue("@userName", nametxt.Text);
                cmnd.Parameters.AddWithValue("@userAge", int.Parse(agetxt.Text));
                cmnd.Parameters.AddWithValue("@userMbn", mbntxt.Text);
                cmnd.Parameters.AddWithValue("@userSal", saltxt.Text);
                cmnd.Parameters.AddWithValue("@userUserName", usernametxt.Text);
                cmnd.Parameters.AddWithValue("@userPass", passtxt.Text);
                cmnd.Parameters.AddWithValue("@userSale", int.Parse(salestxt.Text));
                cmnd.ExecuteNonQuery();
                str.Close();
                MessageBox.Show("data has been succesfully added...!");
                nametxt.Text = string.Empty;
                agetxt.Text = string.Empty;
                mbntxt.Text = string.Empty;
                saltxt.Text = string.Empty;
                usernametxt.Text = string.Empty;
                passtxt.Text = string.Empty;
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmnd = new SqlCommand("Update med_Users set userName=@userName,userAge=@userAge,userMbn=@userMbn,userSal=@userSal,userUserName=@userUserName,userPass=@userPass,userSale=@userSale  where userId=@userId", str);
                cmnd.Parameters.AddWithValue("@userId", int.Parse(id_forUpdation));
                cmnd.Parameters.AddWithValue("@userName", nameUpdatetxt.Text);
                cmnd.Parameters.AddWithValue("@userAge", int.Parse(ageUpdatetxt.Text));
                cmnd.Parameters.AddWithValue("@userMbn", salUpdatetxt.Text);
                cmnd.Parameters.AddWithValue("@userSal", mbnUpdatetxt.Text);
                cmnd.Parameters.AddWithValue("@userUserName", usernameUpdatetxt.Text);
                cmnd.Parameters.AddWithValue("@userPass", passUpdatetxt.Text);
                cmnd.Parameters.AddWithValue("@userSale", int.Parse(salesUpdateTxt.Text));
                cmnd.ExecuteNonQuery();
                str.Close();
                updatePanel.Visible = false;
                updatefeedback.Text = "Data at id = " + id_forUpdation + " has been Succesfully Updated";
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Delete from med_Users where userId=@userId",str);
            cmnd.Parameters.AddWithValue("@userId",int.Parse(id_forDeletion));
            cmnd.ExecuteNonQuery();
            str.Close();
            deletePanel.Visible = false;
            delFeedback.Text = "Data at id = " + id_forDeletion + " has been Succesfully Deleted";
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
            str.Open();
            SqlCommand cmnd = new SqlCommand("Select * from med_Users", str);
            SqlDataAdapter dat = new SqlDataAdapter(cmnd);
            DataTable dt = new DataTable();
            dat.Fill(dt);
            allUsers.DataSource = dt;
            nametxt.Text = string.Empty;
            agetxt.Text = string.Empty;
            mbntxt.Text = string.Empty;
            saltxt.Text = string.Empty;
            usernametxt.Text = string.Empty;
            passtxt.Text = string.Empty;
            deletePanel.Visible = false;
            updatePanel.Visible = false;
            idDeltxt.Text = string.Empty;
            idForupdate.Text = string.Empty;
            delFeedback.Text = string.Empty;
            updatefeedback.Text = string.Empty;
        }


    }
}
