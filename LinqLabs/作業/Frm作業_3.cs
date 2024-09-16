using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; } 
            public int Eng { get; set; } 
            public int Math { get; set; }
            public string Gender { get; set; }
        }
        private List<Student> _stuScores;
        public Frm作業_3()
        {
            InitializeComponent();
            _stuScores = new List<Student>()
                    {
                    new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                    new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                    new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                    new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                    new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                    new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                    };
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績
            var sb = new StringBuilder();


            // 共幾個 學員成績 ?		
            var q = _stuScores.Count();
            sb.AppendLine($"Total students: {q}");

            // 找出 前面三個 的學員所有科目成績					
            var firstThreeStudents = _stuScores.Take(3);
            sb.AppendLine("前面三個學員所有科目成績:");
            foreach (var student in firstThreeStudents)
            {
                sb.AppendLine($"Name: {student.Name}, Chi: {student.Chi}, Eng: {student.Eng}, Math: {student.Math}");
            }


            // 找出 後面兩個 的學員所有科目成績			
            var lastTwo = _stuScores.Skip(_stuScores.Count() - 2);
            foreach (var student in lastTwo)
            {
                sb.AppendLine($"{student.Name} - Chi: {student.Chi}, Eng: {student.Eng}, Math: {student.Math}");
            }

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績	
            var selectedStudents1 = _stuScores.Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc");
            foreach (var student in selectedStudents1)
            {
                sb.AppendLine($"{student.Name} - Chi: {student.Chi}, Eng: {student.Eng}");
            }

            // 找出學員 'bbb' 的成績
            var bbbScore = _stuScores.FirstOrDefault(s => s.Name == "bbb");
            if (bbbScore != null)
            {
                sb.AppendLine($"bbb's Scores - Chi: {bbbScore.Chi}, Eng: {bbbScore.Eng}, Math: {bbbScore.Math}");
            }

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            var otherStudents = _stuScores.Where(s => s.Name != "bbb");
            sb.AppendLine("除了 'bbb' 學員的所有成績:");
            foreach (var student in otherStudents)
            {
                sb.AppendLine($"Name: {student.Name}, Chi: {student.Chi}, Eng: {student.Eng}, Math: {student.Math}");
            }

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |	
            var specificStudentsScores = _stuScores
                                        .Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc")
                                        .Select(s => new
                                        {
                                        s.Name,
                                        s.Chi,
                                        s.Math
                                        });

            sb.AppendLine("Name 為 'aaa', 'bbb', 'ccc' 的學員國文和數學科目成績:");
            foreach (var student in specificStudentsScores)
            {
                sb.AppendLine($"Name: {student.Name}, 國文: {student.Chi}, 數學: {student.Math}");
            }
            // 數學不及格 ... 是誰 
            var studentsFailingMath = _stuScores
                                        .Where(s => s.Math < 60)
                                        .Select(s => new
                                        {
                                            s.Name,
                                            s.Math
                                        });

            sb.AppendLine("數學不及格的學員:");
            foreach (var student in studentsFailingMath)
            {
                sb.AppendLine($"Name: {student.Name}, 數學成績: {student.Math}");
            }
            #endregion

            MessageBox.Show(sb.ToString(), "學員成績結果");

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            // 統計 每個學生個人成績 並排序
            var studentAnalysis = _stuScores.Select(s => new
            {
                s.Name,
                Sum = s.Chi + s.Eng + s.Math,
                Avg = Math.Round((decimal)((s.Chi + s.Eng + s.Math) / 3), 1),
                Max = new[] { s.Chi, s.Eng, s.Math }.Max(),
                Min = new[] { s.Chi, s.Eng, s.Math }.Min(),
            }).OrderByDescending(s => s.Avg);

            var sb = new StringBuilder();
            sb.AppendLine("學生成績統計:");
            foreach (var student in studentAnalysis)
            {
                sb.AppendLine($"Name: {student.Name}, 總分: {student.Sum}, 平均: {student.Avg}, 最高分: {student.Max}, 最低分: {student.Min}");
            }

            // 顯示結果
            MessageBox.Show(sb.ToString(), "學生成績分析結果");

        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            var groups = _stuScores.Select(s => new
            {
                s.Name,
                Avg = (s.Chi + s.Eng + s.Math) / 3.0
            }).GroupBy(s =>
            {
                if (s.Avg >= 90) return "優良";
                if (s.Avg >= 70) return "佳";
                return "待加強";
            }).ToList();

            // 排序並顯示在 treeView1
            treeView1.Nodes.Clear();

            foreach (var group in groups.OrderBy(g => g.Key))
            {
                var groupNode = new TreeNode(group.Key);

                foreach (var student in group.OrderByDescending(s => s.Avg))
                {
                    var studentNode = new TreeNode($"{student.Name}: {student.Avg:F1}");
                    groupNode.Nodes.Add(studentNode);
                }

                treeView1.Nodes.Add(groupNode);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            // 取得指定目錄下的檔案
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            // 分組檔案，根據檔案大小分類
            var groupedFiles = from f in files
                               group f by f.Length > 10000 ? "大檔案" : "小檔案" into g
                               select new
                               {
                                   FileType = g.Key,
                                   Files = g.OrderByDescending(file => file.Length).ToList() 
                               };

            // 顯示在 DataGridView
            this.dataGridView1.DataSource = groupedFiles.ToList();

            // 清除 TreeView 中現有的節點
            this.treeView1.Nodes.Clear();

            // 將分組結果顯示在 TreeView
            foreach (var group in groupedFiles)
            {
                // 為每個檔案類別建立一個節點
                TreeNode parentNode = new TreeNode(group.FileType);

                // 將檔案列表添加為子節點
                foreach (var file in group.Files)
                {
                    parentNode.Nodes.Add(new TreeNode(file.Name));
                }

                // 將每個檔案類別節點添加到 TreeView
                this.treeView1.Nodes.Add(parentNode);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var groupedYears = from f in files
                               group f by f.CreationTime.Year into g
                               select new
                               {
                                   Year = g.Key,
                                   FileCount = g.Count(),
                                   AvgFileSize = g.Average(f => f.Length),
                                   Files = g.OrderByDescending(f => f.Length).ToList()
                               };
            this.dataGridView1.DataSource = groupedYears.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in groupedYears.OrderByDescending(g => g.Year))
            {
                // 為每個年份建立一個節點
                TreeNode parentNode = new TreeNode($"{group.Year} ({group.FileCount} files)");

                // 將檔案列表添加為子節點
                foreach (var file in group.Files)
                {
                    parentNode.Nodes.Add(new TreeNode($"{file.Name} ({file.Length / 1024} KB)")); // 顯示檔案名稱和大小（以KB為單位）
                }

                // 將每個年份節點添加到 TreeView
                this.treeView1.Nodes.Add(parentNode);
            }

        }
        NorthwindEntities dbContext = new NorthwindEntities();

        private void button8_Click(object sender, EventArgs e)
        {
            // 先從資料庫中獲取所有產品
            var products = dbContext.Products.ToList();

            // 在記憶體中進行分組
            var q = from p in products
                    group p by price(p.UnitPrice) into g
                    select new
                    {
                        PriceGroup = g.Key,
                        Products = g.ToList()
                    };

            // 設置 DataGridView 的資料來源
            this.dataGridView1.DataSource = q.ToList();

            // 清空 TreeView 的節點
            this.treeView1.Nodes.Clear();

            // 將分組結果顯示在 TreeView 中
            foreach (var group in q)
            {
                TreeNode parentNode = new TreeNode(group.PriceGroup);
                foreach (var product in group.Products)
                {
                    parentNode.Nodes.Add($"{product.ProductName} ({product.UnitPrice:C})");
                }
                this.treeView1.Nodes.Add(parentNode);
            }
        }

        private string price(decimal? unitPrice)
        {
            // 檢查 unitPrice 是否為 null，並根據值返回對應的價格區間
            if (unitPrice == null)
                return "未知";

            if (unitPrice < 20)
                return "低價";
            else if (unitPrice >= 20 && unitPrice < 40)
                return "中價";
            else
                return "高價";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var order = dbContext.Orders.ToList();
            var q = from p in order
                    group p by p.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        Orders = g.ToList()
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                // 創建年份節點
                TreeNode yearNode = new TreeNode(group.Year.ToString());

                foreach (var orders in group.Orders)
                {
                    // 將訂單信息添加到年份節點下
                    yearNode.Nodes.Add($"Order ID: {orders.OrderID}, Order Date: {orders.OrderDate}");
                }

                // 將年份節點添加到 TreeView
                this.treeView1.Nodes.Add(yearNode);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var orders = dbContext.Orders.ToList();

            // 根據年份和月份分組
            var groupedOrders = from o in orders
                                group o by new { Year = o.OrderDate.Value.Year, Month = o.OrderDate.Value.Month } into g
                                orderby g.Key.Year, g.Key.Month // 按年份和月份排序
                                select new
                                {
                                    Year = g.Key.Year,
                                    Month = g.Key.Month,
                                    Orders = g.ToList()
                                };

            // 將結果綁定到 DataGridView
            this.dataGridView1.DataSource = groupedOrders.ToList();

            // 清空 TreeView 的節點
            this.treeView1.Nodes.Clear();

            // 先按年份分組
            var years = groupedOrders.GroupBy(g => g.Year).OrderBy(g => g.Key);

            foreach (var yearGroup in years)
            {
                // 創建年份節點
                TreeNode yearNode = new TreeNode($"{yearGroup.Key}年");

                // 在年份節點下按月份分組
                var months = yearGroup.GroupBy(g => g.Month).OrderBy(g => g.Key);

                foreach (var monthGroup in months)
                {
                    // 創建月份節點
                    TreeNode monthNode = new TreeNode($"{monthGroup.Key}月");

                    foreach (var order in monthGroup.SelectMany(g => g.Orders))
                    {
                        // 將訂單信息添加到月份節點下
                        monthNode.Nodes.Add($"Order ID: {order.OrderID}, Order Date: {order.OrderDate}");
                    }

                    // 將月份節點添加到年份節點下
                    yearNode.Nodes.Add(monthNode);
                }

                // 將年份節點添加到 TreeView
                this.treeView1.Nodes.Add(yearNode);
            }
        }

        private void button2_Click(object sender, EventArgs e)
            {
                // 取得 Orders, Order_Details, Products 和 Employees 的資料
                var orders = dbContext.Orders.ToList();
                var orderDetails = dbContext.Order_Details.ToList();
                var products = dbContext.Products.ToList();
                var employees = dbContext.Employees.ToList();

                // 計算每筆訂單的金額
                var orderAmounts = from o in orders
                                   join od in orderDetails on o.OrderID equals od.OrderID
                                   join p in products on od.ProductID equals p.ProductID
                                   join emp in employees on o.EmployeeID equals emp.EmployeeID
                                   group new { o, od, p, emp } by new { emp.LastName, emp.FirstName } into g
                                   select new
                                   {
                                       EmployeeName = g.Key.LastName + " " + g.Key.FirstName, 
                                       Products = g.Select(x => new
                                       {
                                           x.o.OrderID,
                                           x.p.ProductName,
                                           Amount = Math.Round((decimal)x.od.UnitPrice * x.od.Quantity * (1 - (decimal)x.od.Discount), 2) // 金額取到小數點2位
                                       }).ToList(),
                                       TotalAmount = Math.Round(g.Sum(x => (decimal)x.od.UnitPrice * x.od.Quantity * (1 - (decimal)x.od.Discount)), 2) // 總金額取到小數點2位
                                   };

                // 將結果綁定到 DataGridView
                this.dataGridView1.DataSource = orderAmounts.ToList();

                // 清空 TreeView 的節點
                this.treeView1.Nodes.Clear();

                foreach (var employeeGroup in orderAmounts)
                {
                    // 創建 EmployeeName 節點
                    TreeNode employeeNode = new TreeNode($"Employee: {employeeGroup.EmployeeName}, Total Amount: {employeeGroup.TotalAmount:C}");

                    foreach (var product in employeeGroup.Products)
                    {
                        // 將產品資訊添加到 Employee 節點下
                        employeeNode.Nodes.Add($"Order ID: {product.OrderID}, Product: {product.ProductName}, Amount: {product.Amount:C}");
                    }

                    // 將 Employee 節點添加到 TreeView
                    this.treeView1.Nodes.Add(employeeNode);
                }
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 取得 Orders, Order_Details, Products 和 Employees 的資料
            var orders = dbContext.Orders.ToList();
            var orderDetails = dbContext.Order_Details.ToList();
            var products = dbContext.Products.ToList();
            var employees = dbContext.Employees.ToList();

            // 計算每筆訂單的金額
            var orderAmounts = from o in orders
                               join od in orderDetails on o.OrderID equals od.OrderID
                               join p in products on od.ProductID equals p.ProductID
                               join emp in employees on o.EmployeeID equals emp.EmployeeID
                               group new { o, od, p, emp } by new { emp.LastName, emp.FirstName } into g
                               select new
                               {
                                   EmployeeName = g.Key.LastName + " " + g.Key.FirstName,  // 使用員工的全名
                                   TotalAmount = Math.Round(g.Sum(x => (decimal)x.od.UnitPrice * x.od.Quantity * (1 - (decimal)x.od.Discount)), 2) // 總金額取到小數點2位
                               };

            // 將員工按照總金額排序，並取出前5個
            var top5Employees = orderAmounts
                                .OrderByDescending(emp => emp.TotalAmount)
                                .Take(5)
                                .ToList();

            // 將結果綁定到 DataGridView
            this.dataGridView1.DataSource = top5Employees;

            // 清空 TreeView 的節點
            this.treeView1.Nodes.Clear();

            // 在 TreeView 中顯示前5名員工及其總金額
            foreach (var employee in top5Employees)
            {
                // 添加員工及其總金額到 TreeView
                this.treeView1.Nodes.Add($"Employee: {employee.EmployeeName}, Total Amount: {employee.TotalAmount:C}");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var orders = dbContext.Orders.ToList();
            var orderDetails = dbContext.Order_Details.ToList();
            var products = dbContext.Products.ToList();
            var employees = dbContext.Employees.ToList();
            var categories = dbContext.Categories.ToList();

            var q = from o in orders
                    join od in orderDetails on o.OrderID equals od.OrderID
                    join p in products on od.ProductID equals p.ProductID
                    join c in categories on p.CategoryID equals c.CategoryID
                    select new
                    {
                        o.OrderID,
                        o.OrderDate,
                        p.ProductName,
                        c.CategoryName,
                        OrderAmount = Math.Round((decimal)od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount), 2)
                    };
            var T5=  q.OrderByDescending(o => o.OrderAmount)
                     .Take(5)
                     .ToList();
            this.dataGridView1.DataSource = T5;
            this.treeView1.Nodes.Clear();
            foreach (var order in T5)
            {
                TreeNode orderNode = new TreeNode($"Order ID: {order.OrderID}, Amount: {order.OrderAmount:C}, Category: {order.CategoryName}");

                orderNode.Nodes.Add($"Product: {order.ProductName}");

                this.treeView1.Nodes.Add(orderNode);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            var orders = dbContext.Orders.ToList();
            var orderDetails = dbContext.Order_Details.ToList();
            var products = dbContext.Products.ToList();
            var categories = dbContext.Categories.ToList();

            var orderAmounts = from o in orders
                               join od in orderDetails on o.OrderID equals od.OrderID
                               join p in products on od.ProductID equals p.ProductID
                               join c in categories on p.CategoryID equals c.CategoryID
                               select new
                               {
                                   o.OrderID,
                                   o.OrderDate,
                                   p.ProductName,
                                   c.CategoryName,
                                   OrderAmount = Math.Round((decimal)od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount), 2) // 計算訂單金額並取到小數點2位
                               };
            var ordersAbove300 = orderAmounts
                         .GroupBy(o => o.OrderID)
                         .Select(g => new
                         {
                             OrderID = g.Key,
                             OrderDate = g.First().OrderDate, // 取第一筆訂單日期
                             TotalAmount = g.Sum(o => o.OrderAmount), // 計算同一個 OrderID 的總金額
                             Products = g.ToList() // 將所有產品放入列表
                         })
                         .Where(o => o.TotalAmount > 300) // 只篩選金額大於300的訂單
                         .OrderByDescending(o => o.TotalAmount) // 以總金額降序排列
                         .ToList();

            this.dataGridView1.DataSource = ordersAbove300.Select(o => new
            {
                o.OrderID,
                o.OrderDate,
                TotalAmount = o.TotalAmount.ToString("C") // 格式化金額顯示
            }).ToList();
            this.treeView1.Nodes.Clear();
            foreach (var order in ordersAbove300)
            {
                // 創建以 OrderID 分組的節點
                TreeNode orderNode = new TreeNode($"Order ID: {order.OrderID}, Total Amount: {order.TotalAmount:C}");

                foreach (var product in order.Products)
                {
                    // 將產品信息添加到訂單節點下
                    orderNode.Nodes.Add($"Product: {product.ProductName}, Amount: {product.OrderAmount:C}, Category: {product.CategoryName}");
                }

                // 將訂單節點添加到 TreeView
                this.treeView1.Nodes.Add(orderNode);
            }
        }
    }
}
