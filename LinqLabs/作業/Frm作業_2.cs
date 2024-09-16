using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.productPhotoTableAdapter1.Fill(this.dataSet11.ProductPhoto);
            this.productProductPhotoTableAdapter1.Fill(this.dataSet11.ProductProductPhoto);
            this.productTableAdapter1.Fill(this.dataSet11.Product);
            var q = from p in this.dataSet11.Product
                    join pp in this.dataSet11.ProductProductPhoto
                    on p.ProductID equals pp.ProductID
                    join pp1 in this.dataSet11.ProductPhoto
                    on pp.ProductPhotoID equals pp1.ProductPhotoID
                    where !p.IsProductModelIDNull()
                    select new
                            {
                                p.ProductID,
                                p.Name,
                                pp1.LargePhoto,
                                p.ModifiedDate
                    };
            //var q2=from p in q
            //       where q.
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.productPhotoTableAdapter1.Fill(this.dataSet11.ProductPhoto);
            this.productProductPhotoTableAdapter1.Fill(this.dataSet11.ProductProductPhoto);
            this.productTableAdapter1.Fill(this.dataSet11.Product);
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            var q = from p in this.dataSet11.Product
                    join pp in this.dataSet11.ProductProductPhoto
                    on p.ProductID equals pp.ProductID
                    join pp1 in this.dataSet11.ProductPhoto
                    on pp.ProductPhotoID equals pp1.ProductPhotoID
                    where !p.IsProductModelIDNull() && p.ModifiedDate >= startDate && p.ModifiedDate <= endDate
                    select new
                    {
                        p.ProductID,
                        p.Name,
                        pp1.LargePhoto,
                        p.ModifiedDate
                    };
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.productPhotoTableAdapter1.Fill(this.dataSet11.ProductPhoto);
            this.productProductPhotoTableAdapter1.Fill(this.dataSet11.ProductProductPhoto);
            this.productTableAdapter1.Fill(this.dataSet11.Product);
            int selectedYear;
            if (int.TryParse(comboBox3.Text, out selectedYear))
            {
                var q = from p in this.dataSet11.Product
                        join pp in this.dataSet11.ProductProductPhoto
                        on p.ProductID equals pp.ProductID
                        join pp1 in this.dataSet11.ProductPhoto
                        on pp.ProductPhotoID equals pp1.ProductPhotoID
                        where !p.IsProductModelIDNull() && p.ModifiedDate.Year == selectedYear
                        select new
                        {
                            p.ProductID,
                            p.Name,
                            pp1.LargePhoto,
                            p.ModifiedDate
                        };

                // 將結果綁定到 DataGridView
                this.dataGridView1.DataSource = q.ToList();
            }
            else
            {
                // 如果無法轉換年份，顯示錯誤訊息
                MessageBox.Show("請輸入有效的年份！");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
