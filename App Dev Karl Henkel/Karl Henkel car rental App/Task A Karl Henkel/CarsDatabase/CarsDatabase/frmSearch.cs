using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarsDatabase
{
    public partial class frmSearch : Form
    {

        //Insert own connectionstring here
        SqlConnection con = new SqlConnection(@"Data Source = (Local); Initial Catalog = Hire; User ID = delegate; Password=Pa55w.rd");

        DataSet ds = new DataSet();
        public frmSearch()
        {
            InitializeComponent();
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "Yes" || txtValue.Text == "yes")
            {
                txtValue.Text = "1";
            }
            else if (txtValue.Text == "No" || txtValue.Text == "no")
            {
                txtValue.Text = "0";
            }
            ds.Reset();
            String strField = cboField.Text.ToString();
            String strOperator = cboOperator.Text.ToString();
            String strValue = txtValue.Text;
            String strSearch = "" + strField + " " + strOperator + " " + "'" + strValue + "'";
            if (strField.Trim() != "" && strOperator.Trim() != "" && strValue.Trim() != "")
            {
                try
                {


                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM tblCar WHERE " + @strSearch;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    dgvList.DataSource = ds.Tables[0];
                    con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            

            /*if (cboField.Text == "Make" && cboOperator.Text == cboOperator.SelectedText)
            {
                da = new SqlDataAdapter("SELECT VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available From tblCar Where Make LIKE '" + txtValue.Text+"%'", con);
                da.Fill(dt);
                dgvList.DataSource = dt;
                
            }
            else
            {
                MessageBox.Show("please fill required fields");
            }
            if (cboField.Text == "EngineSize" && cboOperator.Text == cboOperator.Text)
            {
                da = new SqlDataAdapter("SELECT VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available From tblCar Where EngineSize LIKE '" + txtValue.Text + "%'", con);
                da.Fill(dt);
                dgvList.DataSource = dt;
                dgvList.Refresh();
            }
            if (cboField.Text == "RentalPerDay" && cboOperator.Text == cboOperator.SelectedText)
            {
                da = new SqlDataAdapter("SELECT VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available From tblCar Where RentalPerDay LIKE '" + txtValue.Text + "%'", con);
                da.Fill(dt);
                dgvList.DataSource = dt;
                
            }
            else
            {
                MessageBox.Show("please fill required fields");
            }
            if (cboField.Text == "Available" && cboOperator.Text == cboOperator.SelectedText )
            {
                da = new SqlDataAdapter("SELECT VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available From tblCar Where Available LIKE '" + txtValue.Text + "%'", con);
                da.Fill(dt);
                dgvList.DataSource = dt;
                
            }
            else
            {
                MessageBox.Show("please fill required fields");
            }*/

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
            frmCars fc = new frmCars();
            fc.Show();
            
        }
    }
}
