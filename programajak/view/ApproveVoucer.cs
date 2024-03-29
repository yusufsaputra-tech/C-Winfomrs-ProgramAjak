﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using programajak.Db;

namespace programajak
{
    public partial class ApproveVoucer : Form
    {

        public ApproveVoucer()
        {
            InitializeComponent();
        }

        private void ClearData()
        {
            txtId.Text = "";
            txtCIF.Text = "";
            txtNama.Text = "";
            txtVoucer.Text = "";
        }
        private void ApproveVoucer_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getAllDataNasabah();
            dataGridView1.Refresh();
            
            getAllData();
            dataGridView2.Refresh();
        }
       
        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private DataTable getAllDataNasabah()
        {
            ConnectionManager Mycon = new ConnectionManager();
            try
            {
                using (MySqlConnection conn = Mycon.ConnectionDb)
                {
                    conn.Open();
                    using (DataTable dt = new DataTable())
                    {
                        string query = "SELECT CIF,customer_name FROM tblcustomer_tm";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Mycon = null;
            }
        }

        private DataTable getDataNasabahName()
        {
            ConnectionManager Mycon = new ConnectionManager();
            try
            {
                using (MySqlConnection conn = Mycon.ConnectionDb)
                {
                    conn.Open();
                    using (DataTable dt = new DataTable())
                    {
                        string query = "SELECT CIF,customer_name FROM tblcustomer_tm WHERE customer_name = @name";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@name", txtSearch.Text);

                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Mycon = null;
            }
        }

        private DataTable getAllData()
        {
            ConnectionManager Mycon = new ConnectionManager();
            try
            {
                using (MySqlConnection conn = Mycon.ConnectionDb)
                {
                    conn.Open();
                    using (DataTable dt = new DataTable())
                    {
                        string query = "SELECT * FROM tblapprovevoucer";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Mycon = null;
            }
        }

        private DataTable getDataVoucerDebitur()
        {
            ConnectionManager Mycon = new ConnectionManager();
            try
            {
                using (MySqlConnection conn = Mycon.ConnectionDb)
                {
                    conn.Open();
                    using (DataTable dt = new DataTable())
                    {
                        string query = "SELECT * FROM tblapprovevoucer WHERE name_customer = @name";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@name", txtSearch2.Text);

                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Mycon = null;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Id Voucer Belum Di Isi !!!");
                txtId.Select();
            }
            else if (string.IsNullOrWhiteSpace(txtCIF.Text))
            {
                MessageBox.Show("CIF Belum Di Isi !!!");
                txtCIF.Select();
            }
            else if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama Belum Di Isi !!!");
                txtNama.Select();
            }
            else if (string.IsNullOrWhiteSpace(txtVoucer.Text))
            {
                MessageBox.Show("Voucer Belum Di Isi !!!");
                txtVoucer.Select();
            }
            else
            {
                try
                {
                    ConnectionManager Mycon = new ConnectionManager();

                    using (MySqlConnection conn = Mycon.ConnectionDb)
                    {
                        conn.Open();
                        using(MySqlCommand cmd = new MySqlCommand("ApproveVoucer", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("_IdVoucer", txtId.Text);
                            cmd.Parameters.AddWithValue("_CIF", txtCIF.Text);
                            cmd.Parameters.AddWithValue("_NameCustomer", txtNama.Text);
                            cmd.Parameters.AddWithValue("_TotalQty", txtVoucer.Text);
                            cmd.Parameters.AddWithValue("_ActionType", "1");
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Voucer Berhasil Ditambahkan !!!");
                                ClearData();
                                getAllData();
                                this.dataGridView2.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Gagal Menambahkan Voucer !!!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            getDataVoucerDebitur();
            dataGridView2.Refresh();

        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCIF.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNama.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtVoucer.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            button2.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtCIF.Text = "";
            txtNama.Text = "";;
            txtVoucer.Text = "";
            button2.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            ConnectionManager Mycon = new ConnectionManager();
            try
            {
                using (MySqlConnection conn = Mycon.ConnectionDb)
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("ApproveVoucer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_IdVoucer", txtId.Text);
                        cmd.Parameters.AddWithValue("_CIF", txtCIF.Text);
                        cmd.Parameters.AddWithValue("_NameCustomer", txtNama.Text);
                        cmd.Parameters.AddWithValue("_TotalQty", txtVoucer.Text);
                        cmd.Parameters.AddWithValue("_ActionType", "2");
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Voucer Berhasil Dirubah !!!");
                            ClearData();
                            this.dataGridView2.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Gagal Update Data !!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mycon = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCIF.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNama.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getDataNasabahName();
            dataGridView1.Refresh();
        }
    }
}
