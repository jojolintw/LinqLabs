using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
           

        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }
    }
}
