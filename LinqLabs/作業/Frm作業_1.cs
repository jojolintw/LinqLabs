using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

            var q1 = from o in this.nwDataSet1.Orders
                     select o.OrderDate.Year;

            //NOTE Distinct()
            this.comboBox1.DataSource = q1.Distinct().ToList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        int page = -1;
        int countPerPage = 10;

        private void button13_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out countPerPage);

            page += 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * page).Take(countPerPage).ToList();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            //files[0].CreationTime
            var q = from f in files
                    where f.Extension == ".log"
                    orderby f.Length descending
                    select new
                    {
                        檔案名稱 = f.Name,
                        建立時間 = f.CreationTime,
                        大小 = f.Length
                    };
            this.dataGridView1.DataSource = q.ToList();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products); //Auto conn.open()=>command.ExecuteXXX()..=> while( sqlDataRader Read...)=conn.Close()

            this.dataGridView1.DataSource = this.nwDataSet1.Products;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            var q1 = from f in files
                     where f.CreationTime.Year==2017
                     select f;
            this.dataGridView1.DataSource = q1.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            var q2 = from f in files
                     where f.Length>100000
                     select f;
            this.dataGridView1.DataSource = q2.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year;// = 1997;
            int.TryParse(this.comboBox1.Text, out year);

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

            var q3=from p in this.nwDataSet1.Orders
                    where p.OrderDate.Year == year
                  select p;
            this.bindingSource1.DataSource = q3.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;


        }

        private void button12_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out countPerPage);

            page -= 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * page).Take(countPerPage).ToList();

        }
    }
}
