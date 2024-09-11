using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        int _position = -1;
        List<NWDataSet.ProductPhotoRow> _photolist {  get; set; }
        public Frm作業_2()
        {
            InitializeComponent();
        }

        private void btnAllBicycle_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            // 將資料庫中的數據填充到本地的表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);

            // 定義一個 LINQ 查詢
            IEnumerable<NWDataSet.ProductPhotoRow> q = from o in this.nwDataSet1.ProductPhoto
                                                 orderby o.ModifiedDate
                                                 select o; 

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            _photolist = q.ToList();
            this.dataGridView1.DataSource = _photolist;
        }

        private void btnRegionBike_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);

            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.ProductPhotoRow> q = from o in this.nwDataSet1.ProductPhoto
                                                       where o.ModifiedDate >= dateTimePickerStart.Value 
                                                             && o.ModifiedDate <= dateTimePickerEnd.Value
                                                       orderby o.ModifiedDate
                                                       select o;

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            _photolist = q.ToList();
            this.dataGridView1.DataSource = _photolist;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            //清空資料
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image = null;
            }

            if (_photolist[_position] != null)
            {
                // 將 byte[] 轉換為 MemoryStream，並從中創建 Image
                using (MemoryStream ms = new MemoryStream(_photolist[_position].LargePhoto))
                {
                    Image bikeImage = Image.FromStream(ms);  // 創建 Image 物件
                    this.pictureBox1.Image = bikeImage;  // 將圖片顯示在 PictureBox 中
                }
            }
            else
            {
                // 如果沒有圖片資料，清空 PictureBox
                this.pictureBox1.Image = null;
            }
        }

        private void btnYearBike_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);

            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.ProductPhotoRow> q = from o in this.nwDataSet1.ProductPhoto
                                                       where o.ModifiedDate.Year == Convert.ToInt32(comboBoxYear.SelectedItem)
                                                       orderby o.ModifiedDate
                                                       select o;

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            _photolist = q.ToList();
            this.dataGridView1.DataSource = _photolist;
        }

        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            this.comboBoxYear.Text = "請選擇年份";
            // 將資料庫中的數據填充到本地的表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);

            // 定義一個 LINQ 查詢
            IEnumerable<int> q = (from o in this.nwDataSet1.ProductPhoto
                                 orderby o.ModifiedDate
                                 select o.ModifiedDate.Year).Distinct();
            foreach (int i in q)
            {
                comboBoxYear.Items.Add(i);
            }

            //============季度==============
            this.comboBoxSeason.Text = "請選擇季度";
            string season = "";
            for(int i=1;i<5;i++)
            {
                season = $"第{i}季";
                comboBoxYear.Items.Add(season);
            }

        }

        private void btnSeasonBike_Click(object sender, EventArgs e)
        {
            Season s1 = new Season { seasonName = "第1季", seasonnumb = new int[]{ 1,2,3} };
            Season s2 = new Season { seasonName = "第2季", seasonnumb = new int[] { 4, 5, 6 } };
            Season s3 = new Season { seasonName = "第3季", seasonnumb = new int[] { 7, 8, 9 } };
            Season s4 = new Season { seasonName = "第4季", seasonnumb = new int[] { 10, 11, 12 } };

            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);


            // 定義一個 LINQ 查詢，篩選 Orders 表中 ShipRegion 和 ShipPostalCode 不為空的訂單
            IEnumerable<NWDataSet.ProductPhotoRow> q = from o in this.nwDataSet1.ProductPhoto
                                                       //where 
                                                       orderby o.ModifiedDate.Month
                                                       select o;

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            _photolist = q.ToList();
            this.dataGridView1.DataSource = _photolist;
        }
    }
    public class Season 
    {
        public string seasonName {  get; set; }
        public int[] seasonnumb {  get; set; }
    }
}
