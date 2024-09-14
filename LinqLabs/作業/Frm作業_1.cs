using LinqLabs;
using System;
using System.Collections;
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
        int _orderposition = -1;

        System.IO.FileInfo[] _files {  get; set; }
        List<NWDataSet.OrdersRow> _orderlist {  get; set; }

        int _page = -1;

        private int _countPerPage;
        public int countPerPage {
            get 
            {
                //_countPerPage = textBoxPageCount.Text == null ? 10 : Convert.ToInt32(this.textBoxPageCount.Text);

                // 用三元運算式判斷 TextBox 中是否有值或值是否為空白字串，若為空白則返回預設值 10，否則嘗試將 TextBox 的值轉為整數
                _countPerPage = string.IsNullOrWhiteSpace(textBoxPageCount.Text)
                                ? 10  // 如果 TextBox 是空白字串或為 null，則每頁顯示的產品數量設置為 10（預設值）
                                : int.TryParse(textBoxPageCount.Text, out var result) // 嘗試將 TextBox 中的文本轉換為整數
                                    ? result  // 如果轉換成功，將轉換後的數值 (result) 賦給 _countPerPage
                                    : 10;  // 如果轉換失敗（非數字），則設置 _countPerPage 為 10（預設值）

                return _countPerPage;
            }
        }

        public Frm作業_1()
        {
            InitializeComponent();

            //一開始就往容器中加入資料
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            

            if (_page >= -1 && _page < nwDataSet1.Products.Count()/countPerPage)
            { _page++; 

            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * _page)
                                                                    .Take(countPerPage).ToList();
            }
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "有log副檔名的檔案";

            dataGridView1.DataSource = null;

            // 使用 LINQ 查詢篩選出擴展名為 ".log" 的文件
            var f = from p in _files
                    where p.Extension == ".log"
                    select p;

            // 將篩選出的文件顯示在 DataGridView 中
            this.dataGridView1.DataSource = f.ToList();

        }

        private void btnOrderAll(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";

            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.OrdersRow> q = from o in this.nwDataSet1.Orders  // 從 nwDataSet1.Orders 表中取得每一行訂單資料
                                                 where !o.IsShipRegionNull() && !o.IsShipPostalCodeNull()  // 篩選出 ShipRegion 和 ShipPostalCode 不為空的訂單
                                                 && !o.IsShippedDateNull()
                                                 orderby o.OrderDate.Year descending  // 根據訂單的月份 (OrderDate.Month) 進行降序排列
                                                 select o;  // 選擇符合條件的訂單

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            this.dataGridView1.DataSource = q.ToList();

        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            // 創建 DirectoryInfo 對象，用來訪問目錄中的文件
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            // 獲取目錄中的所有文件
            _files = dir.GetFiles();

            //====================================================================

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
                this.comboBoxoOrderYear.Items.Add(year);  // 將年份添加到 ComboBox 控制項中
            }



        }

        private void btnOrderAndOrderDetail_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";

            if (comboBoxoOrderYear.SelectedItem != null)
            {
                // 清空 DataGridView 的資料
                dataGridView1.DataSource = null;

                // 重新填充 Orders 表的資料到DataSet
                this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

                // 使用 LINQ 查詢篩選資料
                IEnumerable<NWDataSet.OrdersRow> orderdata = from o in this.nwDataSet1.Orders
                                                             where o.OrderDate.Year == Convert.ToInt32(comboBoxoOrderYear.SelectedItem)
                                                                   && !o.IsShipRegionNull()
                                                                   && !o.IsShipPostalCodeNull()
                                                             orderby o.OrderDate.Year descending
                                                             select o;

                // 將篩選結果顯示在 DataGridView 中
                _orderlist= orderdata.ToList();
                dataGridView1.DataSource = _orderlist;

                //=========== 訂單詳情 ==============

                int orderIDindex = _orderlist[_orderposition].OrderID;

                // 清空 DataGridView 的資料
                dataGridView2.DataSource = null;

                // 重新填充 Order_Details 表的資料
                this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

                // 使用 LINQ 查詢篩選資料
                IEnumerable<NWDataSet.Order_DetailsRow> orderDetail = from o in this.nwDataSet1.Order_Details
                                                               where o.OrderID == orderIDindex
                                                               select o;

                // 將篩選結果顯示在 DataGridView 中
                dataGridView2.DataSource = orderDetail.ToList();

            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _orderposition = e.RowIndex;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if(_orderlist!= null)
            {
                int orderIDindex = _orderlist[_orderposition].OrderID;

                // 清空 DataGridView 的資料
                dataGridView2.DataSource = null;

                // 重新填充 Order_Details 表的資料
                this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

                // 使用 LINQ 查詢篩選資料
                IEnumerable<NWDataSet.Order_DetailsRow> orderDetail = from o in this.nwDataSet1.Order_Details
                                                                      where o.OrderID == orderIDindex
                                                                      select o;

                // 將篩選結果顯示在 DataGridView 中
                dataGridView2.DataSource = orderDetail.ToList();
            }
            
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            lblMaster.Text = "產品列表";

            _page = 0;

            this.dataGridView1.DataSource = this.nwDataSet1.Products.Take(countPerPage).ToList();

        }

        private void btn2017Creaded_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "2024年建立的檔案";

            dataGridView1.DataSource = null;

            // 使用 LINQ 查詢篩選出擴展名為 ".log" 的文件
            var f = from p in _files
                    where p.CreationTime.Year==2024
                    select p;

            // 將篩選出的文件顯示在 DataGridView 中
            this.dataGridView1.DataSource = f.ToList();
        }

        private void btnBigfile_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "大檔案";

            dataGridView1.DataSource = null;

            // 使用 LINQ 查詢篩選出擴展名為 ".log" 的文件
            var f = from p in _files
                    where p.Length>500000
                    select p;

            // 將篩選出的文件顯示在 DataGridView 中
            this.dataGridView1.DataSource = f.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (_page > 0)
            {
                _page--;

                this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * _page)
                                                                        .Take(countPerPage).ToList();
            }
        }

        private void textBoxPageCount_TextChanged(object sender, EventArgs e)
        {
            _page = -1;
        }
    }
}
