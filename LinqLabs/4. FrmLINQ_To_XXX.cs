using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11,121};

            //IEnumerable<IGrouping<int, int>> q = from n in nums
            //                                     group n by (n % 2);

            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                    group n by n%2==0?"偶數":"奇數";

            this.dataGridView1.DataSource=  q.ToList();

            //========================

            foreach (var group in q)
            {
                TreeNode node= this.treeView1.Nodes.Add(group.Key.ToString());

                foreach (var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {

            //split => Apply => Combine
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 121 };

            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { MyKey= g.Key, MyCount = g.Count(), MyAvg= g.Average(), MyGroup=g };

            this.dataGridView1.DataSource = q.ToList();

            //========================

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            //=====================
            this.chart1.DataSource =  q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //split => Apply => Combine
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 121 };

            var q = from n in nums
                    group n by MyKey(n)   into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();

            //========================

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=====================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        string MyKey(int n)
        {
            if (n<5)
            {
                return "Small";
            }
            else if (n<10)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }
    }
}
