using LinqLabs;
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
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
            // 創建 DirectoryInfo 對象，用來訪問目錄中的文件
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            // 獲取目錄中的所有文件
            System.IO.FileInfo[] files = dir.GetFiles();

            // 使用 LINQ 查詢篩選出擴展名為 ".log" 的文件
            var f = from p in files
                    where p.Extension == ".log"
                    select p;

            // 將篩選出的文件顯示在 DataGridView 中
            this.dataGridView1.DataSource = f.ToList();

        }

        private void btnOrderAll(object sender, EventArgs e)
        {
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.OrdersRow> q = from o in this.nwDataSet1.Orders  // 從 nwDataSet1.Orders 表中取得每一行訂單資料
                                                 where !o.IsShipRegionNull() && !o.IsShipPostalCodeNull()  // 篩選出 ShipRegion 和 ShipPostalCode 不為空的訂單
                                                 orderby o.OrderDate.Month descending  // 根據訂單的月份 (OrderDate.Month) 進行降序排列
                                                 select o;  // 選擇符合條件的訂單

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            this.dataGridView1.DataSource = q.ToList();

        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            // 取得所有不重複的年份並塞入 comboBoxOrderYear
            IEnumerable<int> years = (from o in this.nwDataSet1.Orders
                                      where !o.IsShipRegionNull() && !o.IsShipPostalCodeNull()
                                      orderby o.OrderDate.Year descending
                                      select o.OrderDate.Year)     // 取出年份
                                      .Distinct();     // 過濾重複的年份

            // 清空 comboBoxOrderYear 以防止多次重複添加
            this.comboBoxoOrderYear.Items.Clear();
            this.comboBoxoOrderYear.Text = "請選擇訂單年份";

            // 將不重複的年份添加到 comboBoxOrderYear 中
            foreach (int year in years)
            {
                this.comboBoxoOrderYear.Items.Add(years.ToList());  // 將年份添加到 ComboBox 控制項中
            }


        }
    }
}
