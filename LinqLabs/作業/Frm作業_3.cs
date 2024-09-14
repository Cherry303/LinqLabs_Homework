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
        List<Student> _studentScores = new List<Student>
        {
                new Student { Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                new Student { Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                new Student { Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                new Student { Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                new Student { Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                new Student { Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

        };

    public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            // 數學不及格 ... 是誰 
            #endregion
            listBox1.Items.Add($"共有{_studentScores.Count()}個學員成績");



            List<Student> topThree = _studentScores.Take(3).ToList();
            listBox1.Items.Add($"前面第一個學員是{topThree[0].Name}，國文成績是{topThree[0].Chi}，英文成績是{topThree[0].Eng}，數學成績是{topThree[0].Math}");
            listBox1.Items.Add($"前面第二個學員是{topThree[1].Name}，國文成績是{topThree[1].Chi}，英文成績是{topThree[1].Eng}，數學成績是{topThree[1].Math}");
            listBox1.Items.Add($"前面第三個學員是{topThree[2].Name}，國文成績是{topThree[2].Chi}，英文成績是{topThree[2].Eng}，數學成績是{topThree[2].Math}");

            //總分（國文、英文、數學三科加起來的總和）最高的三個同學的成績
            var yy = _studentScores.Select(s => new
            {
                Name = s.Name,
                TotalScore = s.Chi + s.Eng + s.Math
            }); 

            var topScoreThree = (from i in yy
                    orderby i.TotalScore descending
                    select i).Take(3).ToList();
            listBox1.Items.Add($"總分第一名是{topScoreThree[0].Name}，總分為{topScoreThree[0].TotalScore}");
            listBox1.Items.Add($"總分第二名是{topScoreThree[1].Name}，總分為{topScoreThree[1].TotalScore}");
            listBox1.Items.Add($"總分第三名是{topScoreThree[2].Name}，總分為{topScoreThree[2].TotalScore}");

            //總分（國文、英文、數學三科加起來的總和）最低的三個同學的成績
            var lastScoreThree = (from i in yy
                                 orderby i.TotalScore 
                                 select i).Take(2).ToList();
            listBox1.Items.Add($"總分倒數第一名是{topScoreThree[0].Name}，總分為{topScoreThree[0].TotalScore}");
            listBox1.Items.Add($"總分倒數第二名是{topScoreThree[1].Name}，總分為{topScoreThree[1].TotalScore}");

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            var aa = _studentScores.Where(name => name.Name == "aaa")
                                     .Select(s =>
                                     new
                                     {
                                         s.Chi,
                                         s.Eng
                                     });
            var bb = _studentScores.Where(name => name.Name == "bbb")
                                     .Select(s =>
                                     new
                                     {
                                         s.Chi,
                                         s.Eng
                                     });
            var cc = _studentScores.Where(name => name.Name == "ccc")
                                     .Select(s =>
                                     new
                                     {
                                         s.Chi,
                                         s.Eng
                                     });
            listBox1.Items.Add($"aaa學員的中文成績是{aa.FirstOrDefault().Chi}，英文成績為{aa.FirstOrDefault().Eng}");
            listBox1.Items.Add($"bbb學員的中文成績是{bb.FirstOrDefault().Chi}，英文成績為{bb.FirstOrDefault().Eng}");
            listBox1.Items.Add($"ccc學員的中文成績是{cc.FirstOrDefault().Chi}，英文成績為{cc.FirstOrDefault().Eng}");

            //數學不及格的人
            var mathNoPass = _studentScores.Where(n => n.Math < 60).Select(n => new { n.Name, n.Math}); //Where接受一個委託
            foreach (var n in mathNoPass)
            {
                listBox1.Items.Add($"數學不及格的人 : {n.Name}，數學成績 : {n.Math}");
            }


            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            this.labelchange.Text = "除了 'bbb' 學員的學員的所有成績";
            var others = _studentScores.Where(name => name.Name != "bbb"); //Select()不用寫
            dataGridView1.DataSource = others.ToList();




        }

        private void buttonCalculator_Click(object sender, EventArgs e)
        {
            this.labelchange.Text = "每個學生個人成績，並按照總分高至低排序";
            this.dataGridView1.DataSource = null; //清理dataGridView

            //個人 sum, min, max, avg
            // 統計 每個學生個人成績 並排序
            var cal = (from o in _studentScores
                       select new
                       {
                           o.Name, // 學生姓名
                           Sum = o.Chi + o.Eng + o.Math,  // 三科總分
                           Min = Math.Min(o.Chi, Math.Min(o.Eng, o.Math)),  // 找出三科中的最低分
                           Max = Math.Max(o.Chi, Math.Max(o.Eng, o.Math)),  // 找出三科中的最高分
                           Average = Math.Round(((o.Chi + o.Eng + o.Math) / 3.0 ),2) // 計算三科的平均分，並且四捨五入到小數點後2位
                       }).OrderByDescending(n => n.Sum); //按照全部學生的總分高至低排序

            dataGridView1 .DataSource = cal.ToList(); //填裝dataGridView

        }

        private void btn100students_Click(object sender, EventArgs e)
        {

            // 中文科成績的表現分組
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            this.listBox1.Items.Add("6位學生以成績分成 三群 : '待加強'(60~69) '佳'(70~89) '優良'(90~100)");

            var q = (from n in _studentScores
                    group n by groupCompare(n.Chi) into g
                    select new
                    {
                        Level = g.Key,
                        Name = string.Join(",",g.OrderByDescending(s=> s.Chi).Select(s=>s.Name))

                    }).Select(n => new {n.Level, n.Name});

            foreach (var item in q)
            {
                listBox1.Items.Add($"成績等級為 {item.Level} 的學生為: {item.Name}");
            }

            //========================隨機100位學生的成績，並分成三群=========================================
            this.labelchange.Text = "隨機100位學生的成績，並按照成績分級";
            this.dataGridView1.DataSource = null;

            

            // 生成100個隨機學生
            List<Student> students100 = new List<Student>(); //裝學生的List
            Random random = new Random();
            int studentnumb = 100; //要生成幾個學生

            for (int i = 0; i < studentnumb; i++)
            {
                string[] names = { "aaa", "bbb", "ccc", "ddd", "eee", "fff", "ggg", "hhh", "iii", "jjj" }; // 學生姓名範例
                string[] classes = { "CS_101", "CS_102", "CS_103", "CS_104" }; // 班級範例
                string[] genders = { "Male", "Female" }; // 性別範例

                // 隨機生成學生姓名、班級和性別
                string name = names[random.Next(names.Length)] + (i + 1); 
                string studentClass = classes[random.Next(classes.Length)]; //隨機取出classes字串中的一個隨機位置的內容值
                string gender = genders[random.Next(genders.Length)]; //當隨機數為 0 時，genders[random.Next(2)] 返回 "Male"；當隨機數為 1 時，genders[random.Next(2)] 返回 "Female"

                // 隨機生成學生分數（範圍為 0~100）
                int chiScore = random.Next(60, 101); // 國文分數
                int engScore = random.Next(60, 101); // 英文分數
                int mathScore = random.Next(60, 101); // 數學分數

                // 建立學生物件並加入列表
                students100.Add(new Student
                {
                    Name = name,
                    Class = studentClass,
                    Chi = chiScore,
                    Eng = engScore,
                    Math = mathScore,
                    Gender = gender
                });

            }

            //======按照英文成績分成三群=========

            var qs = from y in students100
                     group y by groupCompare(y.Eng) into g1
                     select new
                     {
                         Level = g1.Key,
                         MyGroup = g1
                     };
            dataGridView1.DataSource = qs.ToList();

            foreach (var studentlv in qs)
            {

                TreeNode node = this.treeView1.Nodes.Add($"{studentlv.Level}");

                foreach (var lv in studentlv.MyGroup)
                {
                    node.Nodes.Add($"{lv.Name}的英文成績為{lv.Eng}分");
                }
            }


        }

        public string groupCompare(int n)
        {
            string result = "";
            if(n>=90)
            {
                result = "優良";
            }
            else if (n>=70 && n<=89)
            {
                result = "佳";
            }
            else
            {
                result = "待加強";
            }
            return result;
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void Frm作業_3_Load(object sender, EventArgs e)
        {
                
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public int Chi { get; set; }
        public int Eng { get; set; }
        public int Math { get; set; }
    }

    public static class ExtentionMethod
    {
        //public static topThree() {
    }
}
