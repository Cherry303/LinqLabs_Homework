using LinqLabs.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        // 創建一個 DirectoryInfo 物件，指定路徑為 "C:\windows"
        System.IO.DirectoryInfo _dir = new System.IO.DirectoryInfo(@"C:\windows");

        System.IO.FileInfo[] _files = null;

        // 創建一個新的資料庫上下文（Entity Framework）
        NorthwindEntities dbContext = new NorthwindEntities();

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
            var mathNoPass = _studentScores.Where(n => n.Math < 60).Select(n => new { n.Name, n.Math }); //Where接受一個委託
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
                           Average = Math.Round(((o.Chi + o.Eng + o.Math) / 3.0), 2) // 計算三科的平均分，並且四捨五入到小數點後2位
                       }).OrderByDescending(n => n.Sum); //按照全部學生的總分高至低排序

            dataGridView1.DataSource = cal.ToList(); //填裝dataGridView

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
                         Name = string.Join(",", g.OrderByDescending(s => s.Chi).Select(s => s.Name))

                     }).Select(n => new { n.Level, n.Name });

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
            if (n >= 90)
            {
                result = "優良";
            }
            else if (n >= 70 && n <= 89)
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
            labelchange.Text = "依照年份/月份給訂單分組";

            // 首先按年份分組，再按月份進行內部分組
            var q = dbContext.Orders
                    .AsEnumerable()  // 將查詢轉換為本地查詢
                    .GroupBy(p => p.OrderDate.Value.Year)  // 按年份進行分組
                    .Select(g => new
                    {
                        Year = g.Key,  // 分組的鍵 (年份)
                        MonthlyGroups = g.GroupBy(p => p.OrderDate.Value.Month)  // 在每個年份分組內按月份進行分組
                    });

            treeView1.Nodes.Clear();  // 清空 TreeView

            // 遍歷按年份分組的結果
            foreach (var yearGroup in q)
            {
                // 新增年份作為主節點
                TreeNode yearNode = treeView1.Nodes.Add($"{yearGroup.Year}年");

                // 遍歷該年份內按月份進行分組的結果
                foreach (var monthGroup in yearGroup.MonthlyGroups)
                {
                    // 新增月份作為子節點
                    TreeNode monthNode = yearNode.Nodes.Add($"{monthGroup.Key}月 (訂單數量: {monthGroup.Count()})");

                    // 遍歷該月份的訂單，將每個訂單作為子節點加入到月份節點下
                    foreach (var order in monthGroup)
                    {
                        monthNode.Nodes.Add($"訂單ID: {order.OrderID}, 日期: {order.OrderDate.Value.ToShortDateString()}");
                    }
                }
            }
        }

        private void Frm作業_3_Load(object sender, EventArgs e)
        {

            try //獲取電腦中的資料時，可能會有權限問題，所以要用try/catch
            {
                _files = _dir.GetFiles();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"無法訪問部分文件或目錄：{ex.Message}");
                return; // 出現異常時退出方法，避免後續操作
            }
        }

        private void btnFileSize(object sender, EventArgs e)
        {
            labelchange.Text = "依照檔案大小分組檔案";



            // 使用 LINQ 查詢，根據文件大小將文件分組
            var q = from f in _files
                    group f by f.Length >= 50000 ? "大" : "小" into g
                    select new
                    {
                        Size = g.Key,
                        myGroup = g
                    };

            // 清空 TreeView 以避免多次顯示
            treeView1.Nodes.Clear();

            // 迭代查詢結果，將每個分組顯示到 TreeView
            foreach (var size in q)
            {
                //宣告一個主節點tree，然後是由treeView1的樹狀節點新增的一段字串當成這個tree主節點的值
                TreeNode tree = this.treeView1.Nodes.Add($"{size.Size} ({size.myGroup.Count()})");

                // 迭代分組中的每個文件，將文件名和大小作為子節點添加到主節點tree中
                foreach (var item in size.myGroup)
                {
                    tree.Nodes.Add($"檔案名稱是 {item.Name}，檔案大小是 {item.Length} bytes");
                }
            }

        }

        public string compareSize(int n)
        {
            string result = "";
            if (n >= 90)
            {
                result = "大";
            }
            else if (n >= 70 && n <= 89)
            {
                result = "佳";
            }
            else
            {
                result = "待加強";
            }
            return result;
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

        private void button6_Click(object sender, EventArgs e)
        {
            labelchange.Text = "依照檔案年分大小分組檔案";


            // 使用 LINQ 查詢，根據文件大小將文件分組
            var q = from f in _files
                    group f by f.CreationTime.Year >= 2022 ? "小年紀檔案" : "大年紀檔案" into g
                    select new
                    {
                        Size = g.Key,
                        myGroup = g
                    };

            // 清空 TreeView 以避免多次顯示
            treeView1.Nodes.Clear();

            // 迭代查詢結果，將每個分組顯示到 TreeView
            foreach (var size in q)
            {
                //宣告一個主節點tree，然後是由treeView1的樹狀節點新增的一段字串當成這個tree主節點的值
                TreeNode tree = this.treeView1.Nodes.Add($"{size.Size} ({size.myGroup.Count()})");

                // 迭代分組中的每個文件，將文件名和大小作為子節點添加到主節點tree中
                foreach (var item in size.myGroup)
                {
                    tree.Nodes.Add($"檔案名稱是 {item.Name}，檔案建立日期是 {item.CreationTime}");
                }
            }
        }

        private void btnPriceGroup_Click(object sender, EventArgs e)
        {
            var q = dbContext.Products
                    .AsEnumerable()
                    .GroupBy(p => comparePrice((decimal)p.UnitPrice))  // UnitPrice明確轉換成decimal
                    .Select(g => new
                    {
                        myPrice = g.Key,  // 分組的鍵 (高價/中價/低價)
                        myGroup = g,    // 分組的產品
                        myCount = g.Count()
                    });

           treeView1 .Nodes.Clear();
            foreach(var group in q)
            {
                TreeNode main = treeView1.Nodes.Add($"{group.myPrice}產品有{group.myCount}個");
                
                foreach(var item in group.myGroup)
                {
                    main.Nodes.Add($"產品名稱為{item.ProductName}，產品價錢為{item.UnitPrice:C2}");
                }
            }


        }
        public string comparePrice(decimal n)
        {
            if (n >= 300)
                return "高價";
            else if (n >= 150 && n <= 299)
                return "中價";
            else
                return "低價";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            labelchange.Text = "依照年份給訂單分組";
            var q = dbContext.Orders
                    .AsEnumerable()
                    .GroupBy(p => p.OrderDate.Value.Year)
                    .Select(g => new
                    {
                        Year = g.Key,  // 分組的鍵 (高價/中價/低價)
                        myGroup = g,    // 分組的產品
                        myCount = g.Count()
                    });

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode main = treeView1.Nodes.Add($"{group.Year}年的產品有{group.myCount}個");

                foreach (var item in group.myGroup)
                {
                    main.Nodes.Add($"{item.OrderID}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal q = dbContext.Order_Details.Select(
                        n => new {
                            Total = n.Quantity * n.UnitPrice 
                         }).Count();

            this.listBox1.Items.Clear();

            listBox1.Items.Add($"總銷售金額是{q:C}");
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            //【銷售總金額最高的5名銷售員】:
            //把每個訂單的銷售總金額加入到Order表
            //塞選出Order表內同一個EmployeeID的銷售總金額加總
            //篩選出銷售總金額最高的5名EmployeeID
            //join Employee表的LastName+FirstName

            labelchange.Text = "銷售總金額最高的5名業務員";
            // 將 DataGridView 的資料來源設為 null，清空之前的內容
            dataGridView1.DataSource = null;

            // 1. 查詢從 Order_Details 和 Orders 中提取訂單明細和訂單，並按 EmployeeID 進行分組
            var a = from od in this.dbContext.Order_Details
                    join o in this.dbContext.Orders
                    on od.OrderID equals o.OrderID  // 以 OrderID 連接兩個表，將每個訂單明細與對應的訂單進行匹配
                    group new { od, o } by o.EmployeeID into g  // 按 EmployeeID 進行分組，g 是每個員工的訂單和訂單明細的集合
                    select new
                    {
                        EmployeeID = g.Key,  // 分組的鍵，即 EmployeeID，代表員工ID
                        TotalSellPrice = g.Sum(x => x.od.UnitPrice * x.od.Quantity)  // 計算每個員工的總銷售金額，單價乘以數量並加總
                    };

            // 2. 查詢銷售總金額最高的 5 名員工，並從 Employees 表中提取員工的姓名
            var top5Employees = (from em in this.dbContext.Employees
                                 join sales in a
                                 on em.EmployeeID equals sales.EmployeeID  // 根據 EmployeeID 連接 Employees 表和銷售金額分組結果
                                 orderby sales.TotalSellPrice descending  // 按銷售總金額降序排列，銷售額高的在前
                                 select new
                                 {
                                     FullName = em.FirstName + " " + em.LastName,  // 將員工的 FirstName 和 LastName 組合成全名顯示
                                     TotalSales = sales.TotalSellPrice,  // 原始總銷售金額
                                 }).Take(5);  // 取銷售總金額最高的前 5 名員工

            var result = top5Employees.AsEnumerable().Select(s => new
            {
                FullName = s.FullName,
                TotalSales = $"{s.TotalSales:C}"  // 格式化為貨幣格式
            });
 

            dataGridView1.DataSource = result.ToList();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= null;

            var t = (from p in dbContext.Products
                    join c in dbContext.Categories
                    on p.CategoryID equals c.CategoryID
                    orderby p.UnitPrice descending
                    select new
                    {
                        p.ProductName,
                        p.UnitPrice,
                        c.CategoryName
                    }).Take(5);

            dataGridView1.DataSource = t.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool a = dbContext.Products.Any(p => p.UnitPrice > 300);
            MessageBox.Show($"產品中是否有大於300元的商品 : {a}");
        }
    }

}
