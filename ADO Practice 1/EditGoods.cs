using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_Practice_1
{
    public partial class EditGoods : Form
    {
        int _id;
        string _name;
        int _category_id;
        int _price;
        int _count;
        public EditGoods(int id, string name, int category_id, int price, int count)
        {
            InitializeComponent();
            _id = id;
            _name = name;
            _category_id = category_id;
            _price = price;
            _count = count;
        }      
        private void EditGoods_Load(object sender, EventArgs e)
        {
            tb_id.Text = _id.ToString();
            tb_name.Text = _name.ToString();
            tb_cat_id.Text= _category_id.ToString();
            tb_price.Text = _price.ToString();
            tb_count.Text = _count.ToString();
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
