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
    public partial class patient_add_interface : Form
    {
        public patient_add_interface()
        {
            InitializeComponent();
        }

        private void SavePatient_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection str = new SqlConnection("Data Source=LUSHTOP\\SQLEXPRESS;Initial Catalog=lushmed;Integrated Security=True");
                str.Open();
                SqlCommand cmd = new SqlCommand("insert into med_patients values(@patName,@patAge,@patMbn,@patGender)", str);
                cmd.Parameters.AddWithValue("@patName",patient_name.Text);
                cmd.Parameters.AddWithValue("@patAge",int.Parse(patient_age.Text));
                cmd.Parameters.AddWithValue("@patMbn", int.Parse(patient_mbn.Text));
                cmd.Parameters.AddWithValue("@patGender", patient_gender.Text);
                cmd.ExecuteNonQuery();
                str.Close();
                MessageBox.Show("Patient Succesfully added...!");
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
