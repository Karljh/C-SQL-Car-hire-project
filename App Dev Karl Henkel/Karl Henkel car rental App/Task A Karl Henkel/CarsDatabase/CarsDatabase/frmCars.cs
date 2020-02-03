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
    
           
            
    public partial class frmCars : Form
    {

        //Insert own connectionstring here
        String con = @"Data Source=(Local);Initial Catalog=Hire;User ID=delegate;Password=Pa55w.rd";
        
        DataTable dt = new DataTable();
        int R = 0;

        public frmCars()
        {
            InitializeComponent();

            this.Text = "Task A - Karl Henkel " + DateTime.Now.ToShortDateString();

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            string message = "Do you want to Exit?";
            string caption = "Exit";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string query = "INSERT INTO tblCar(VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available) VALUES(@VehicleRegNo, @Make, @EngineSize, @DateRegistered, @RentalPerDay, @Available)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleRegNo", txtVehicleRegNo.Text);
                        cmd.Parameters.AddWithValue("@Make", txtMake.Text);
                        cmd.Parameters.AddWithValue("@EngineSize", txtEngineSize.Text);
                        cmd.Parameters.AddWithValue("@DateRegistered", txtDateReg.Text);
                        cmd.Parameters.AddWithValue("@RentalPerDay", txtRentalPerDay.Text);
                        cmd.Parameters.AddWithValue("@Available", chkAvailable.Checked);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                dt.Reset();
                FrmCars_Load(null, null);
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            


        }

        private void FrmCars_Load(object sender, EventArgs e)
        {


            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select * from tblCar";
                    sqlCommand.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GetData(R);
                    }
                    else
                    {
                        MessageBox.Show("There are no records in the database.");
                    }
                }
                txtVehicleRegNo.ReadOnly = true;
                txtMake.ReadOnly = true;
                txtEngineSize.ReadOnly = true;
                txtDateReg.ReadOnly = true;
                txtRentalPerDay.ReadOnly = true;
                chkAvailable.Enabled = false;
                txtRecords.ReadOnly = true;
                lblR.BackColor = SystemColors.Control;
                lblR.Visible = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GetData(int index)
        {
            txtVehicleRegNo.Text = dt.Rows[index][0].ToString();
            txtMake.Text = dt.Rows[index][1].ToString();
            txtEngineSize.Text = dt.Rows[index][2].ToString();
            txtDateReg.Text = Convert.ToDateTime(dt.Rows[index][3]).ToString("MM/dd/yyyy");
            txtRentalPerDay.Text = dt.Rows[index][4].ToString();
            chkAvailable.Checked = Convert.ToBoolean(dt.Rows[index][5]);
            RecordCount();
        }

        private void RecordCount()
        {
            if (R == 0)
            {
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            else if (R >= dt.Rows.Count - 1)
            {
                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            txtRecords.Text = (R + 1) + " of " + dt.Rows.Count;
        }
        
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            R = 0;
            GetData(R);
           
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            R++;
            GetData(R);

        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            R--;

            GetData(R);
          
            
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            R = dt.Rows.Count - 1;
            GetData(R);
            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            frmSearch fs = new frmSearch();
            fs.Show();
            this.Hide();
            
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            txtVehicleRegNo.ReadOnly = false;
            txtMake.ReadOnly = false;
            txtEngineSize.ReadOnly = false;
            txtDateReg.ReadOnly = false;
            txtRentalPerDay.ReadOnly = false;
            chkAvailable.Enabled = true;
            lblR.BackColor = Color.White;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {

            dt.Reset();
            FrmCars_Load(null, null);
            
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            
                try
                {
                DialogResult result = MessageBox.Show(null, "Are you sure you want to delete this record?", "WARNING!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                    
                        using (SqlConnection conn = new SqlConnection(con))
                        {
                            conn.Open();
                            string query = "DELETE FROM tblCar WHERE VehicleRegNo = @VehicleRegNo";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@VehicleRegNo", txtVehicleRegNo.Text);

                                cmd.ExecuteNonQuery();
                            }
                        MessageBox.Show("Record  Deleted");
                            conn.Close();
                        }
                    dt.Reset();
                    FrmCars_Load(null, null);

                    }
                    
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            
            
            
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtVehicleRegNo.ReadOnly = false;
            txtMake.ReadOnly = false;
            txtEngineSize.ReadOnly = false;
            txtDateReg.ReadOnly = false;
            txtRentalPerDay.ReadOnly = false;
            chkAvailable.Enabled = true;
            txtVehicleRegNo.Clear();
            txtMake.Clear();
            txtEngineSize.Clear();
            txtDateReg.Clear();
            txtRentalPerDay.Clear();
            chkAvailable.Checked = false;
            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            
            lblR.Visible = false;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string query = "UPDATE tblCar SET VehicleRegNo = @VehicleRegNo, Make = @Make, EngineSize = @EngineSize," +
                            "DateRegistered = @DateRegistered, RentalPerDay = @RentalPerDay, Available=@Available " + "WHERE VehicleRegNo = @OldVehicleRegNo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OldVehicleRegNo", dt.Rows[R]["VehicleRegNo"]);
                        cmd.Parameters.AddWithValue("@VehicleRegNo", txtVehicleRegNo.Text);
                        cmd.Parameters.AddWithValue("@Make", txtMake.Text);
                        cmd.Parameters.AddWithValue("@EngineSize", txtEngineSize.Text);
                        cmd.Parameters.AddWithValue("@DateRegistered", txtDateReg.Text);
                        cmd.Parameters.AddWithValue("@RentalPerDay", Convert.ToDecimal(txtRentalPerDay.Text));
                        cmd.Parameters.AddWithValue("@Available", chkAvailable.Checked);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Details Updated in Database.");
                    }
                    conn.Close();
                }
                dt.Reset();
                FrmCars_Load(null, null);

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    }
}
