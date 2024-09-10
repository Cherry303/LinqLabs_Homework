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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
        }

        private void btnAllBicycle_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.productTableAdapter1.Fill(this.nwDataSet1.Product);

            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.ProductRow> q = from o in this.nwDataSet1.Product 
                                                  where !o.IsColorNull()
                                                  && !o.IsSizeUnitMeasureCodeNull()
                                                  && !o.IsSellEndDateNull()
                                                  //&& !o.IsDiscontinuedDateNull()
                                                 orderby o.SellStartDate descending  
                                                 select o; 

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
