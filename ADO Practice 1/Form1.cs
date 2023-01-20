﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ADO_Practice_1
{
    public partial class Form1 : Form
    {
        SqlDataAdapter adapterCategory = null;
        SqlDataAdapter adapterGoods = null;
        SqlConnection connection = new SqlConnection();
        SqlCommandBuilder builder = null;
        SqlDataReader reader = null;
        DataSet dataSetGoods = new DataSet();
        DataSet dataSetCategory = new DataSet();
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void подключитьсяКБдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;
            try 
            {
                adapterCategory = new SqlDataAdapter("select * from Category", connection.ConnectionString);
                adapterCategory.Fill(dataSetCategory);
                dataGridView1.DataSource = dataSetCategory.Tables[0];
                adapterGoods = new SqlDataAdapter("select * from Goods", connection.ConnectionString);
                adapterGoods.Fill(dataSetGoods);
                dataGridView2.DataSource = dataSetGoods.Tables[0];                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                ts_status.Text = "All data read success";
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                int lastid = 1;
                if (dataSetCategory.Tables[0].Rows.Count > 0)
                {
                    lastid = (int)dataSetCategory.Tables[0].Rows[dataSetCategory.Tables[0].Rows.Count - 1][0] + 1;
                }
                AddCategory ac = new AddCategory(lastid);
                if (ac.ShowDialog() == DialogResult.OK)
                {
                    dataSetCategory.Tables[0].Rows.Add(Int32.Parse(ac.tb_id.Text), ac.tb_name.Text);                                     
                }
                ac.Dispose();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                int lastid = 1;
                if (dataSetGoods.Tables[0].Rows.Count > 0)
                {
                    lastid = (int)dataSetGoods.Tables[0].Rows[dataSetGoods.Tables[0].Rows.Count - 1][0] + 1;
                }
                AddGoods ag = new AddGoods(lastid);
                if (ag.ShowDialog() == DialogResult.OK)
                {
                    dataSetGoods.Tables[0].Rows.Add(Int32.Parse(ag.tb_id.Text), ag.tb_name.Text);
                }
                ag.Dispose();
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1 && dataGridView1.SelectedRows.Count > 0)
            {               
                DataRow editRow = dataSetCategory.Tables[0].Rows[dataGridView1.SelectedRows[0].Index];
                EditCategory ec = new EditCategory((int)editRow[0], (string)editRow[1]);                
                if (ec.ShowDialog() == DialogResult.OK)
                {
                    dataSetCategory.Tables[0].Rows[dataGridView1.SelectedRows[0].Index].SetField(0, Int32.Parse(ec.tb_id.Text));
                    dataSetCategory.Tables[0].Rows[dataGridView1.SelectedRows[0].Index].SetField(1, ec.tb_name.Text);                    
                }
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {

            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Cells[0].Value != null)
                    dataSetCategory.Tables[0].Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);                
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            builder = new SqlCommandBuilder(adapterCategory);
            adapterCategory.DeleteCommand = builder.GetDeleteCommand();
            adapterCategory.InsertCommand = builder.GetInsertCommand();
            adapterCategory.UpdateCommand = builder.GetUpdateCommand();
            adapterCategory.Update(dataSetCategory);
        }
    }
}
