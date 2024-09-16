using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            //this.productPhotoTableAdapter1.Fill(this.dataSet11.ProductPhoto);
            //this.productProductPhotoTableAdapter1.Fill(this.dataSet11.ProductProductPhoto);
            //this.productTableAdapter1.Fill(this.dataSet11.Product);

            int selectedYear;
            if (int.TryParse(comboBox3.Text, out selectedYear))
            {
                int startMonth = 1;
                int endMonth = 12;

                switch (comboBox2.SelectedItem.ToString())
                {
                    case "第一季":
                        startMonth = 1;
                        endMonth = 3;
                        break;
                    case "第二季":
                        startMonth = 4;
                        endMonth = 6;
                        break;
                    case "第三季":
                        startMonth = 7;
                        endMonth = 9;
                        break;
                    case "第四季":
                        startMonth = 10;
                        endMonth = 12;
                        break;
                    default:
                        MessageBox.Show("請選擇有效的季度！");
                        return;
                }

                DateTime startDate = new DateTime(selectedYear, startMonth, 1);
                DateTime endDate = new DateTime(selectedYear, endMonth, DateTime.DaysInMonth(selectedYear, endMonth));
                var q = from p in this.dataSet11.Product
                        join pp in this.dataSet11.ProductProductPhoto
                on p.ProductID equals pp.ProductID
                        join pp1 in this.dataSet11.ProductPhoto
                        on pp.ProductPhotoID equals pp1.ProductPhotoID
                        where !p.IsProductModelIDNull()
                              && p.ModifiedDate >= startDate
                              && p.ModifiedDate <= endDate
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

        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            // 先填充資料表
            this.productTableAdapter1.Fill(this.dataSet11.Product);

            // 從資料表中抓取不重複的年份
            var years = (from p in this.dataSet11.Product
                         where !p.IsNull("ModifiedDate")   // 檢查 ModifiedDate 欄位是否為 NULL
                         select p.ModifiedDate.Year).Distinct().OrderBy(y => y);


            // 將年份填入 comboBox3
            comboBox3.Items.Clear();
            foreach (var year in years)
            {
                comboBox3.Items.Add(year);
            }

            // 設定 comboBox3 的預設選項
            if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;  // 預設選第一個年份
            }
        }
    }
}
