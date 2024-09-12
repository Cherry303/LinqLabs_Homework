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
        bool _isSeason = false;
        List<NWDataSet.ProductPhotoRow> _photolist { get; set; }
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

            _isSeason = false;
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

            _isSeason = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // 轉回DataGridVeiw物件
            var gridView = (DataGridView)sender;


            //int currentID = int.Parse(gridView.CurrentRow.Cells["ProductPhotoID"].Value.ToString());
            

            //清空資料
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image = null;
            }

            if (_isSeason == false && _photolist[_position] != null)
            {
                // 將 byte[] 轉換為 MemoryStream，並從中創建 Image
                using (MemoryStream ms = new MemoryStream(_photolist[_position].LargePhoto))
                {
                    Image bikeImage = Image.FromStream(ms);  // 創建 Image 物件
                    this.pictureBox1.Image = bikeImage;  // 將圖片顯示在 PictureBox 中
                }
            }
            if (_isSeason == false && _photolist[_position] == null)
            {
                // 如果沒有圖片資料，清空 PictureBox
                this.pictureBox1.Image = null;
            }

            if (_isSeason == true)
            {

                int currentID = 0;  // 初始化一個整數變數 currentID，用來保存從 DataGridView 當前行中獲取的 ProductPhotoID

                // 嘗試將 DataGridView 當前行的 ProductPhotoID 轉換為整數，並賦值給 currentID
                if (int.TryParse(gridView.CurrentRow.Cells["ProductPhotoID"].Value.ToString(), out currentID))
                {
                    // 如果轉換成功，執行下面的查詢
                    //var q1 = from o in this.nwDataSet1.ProductPhoto
                    //         where o.ModifiedDate.Month.distinctSeason() == comboBoxSeason.SelectedItem.ToString()
                    //         select o.ProductPhotoID;
                    //int CurrentID = q1.ToList()[_position];


                    // 定義一個 LINQ 查詢
                    var q2 = from o in this.nwDataSet1.ProductPhoto
                             where o.ModifiedDate.Month.distinctSeason() == comboBoxSeason.SelectedItem.ToString()
                             && o.ProductPhotoID == currentID
                             select o.LargePhoto;
                    var f = q2.FirstOrDefault();

                    // 將 byte[] 轉換為 MemoryStream，並從中創建 Image
                    using (MemoryStream ms = new MemoryStream(f))
                    {
                        Image bikeImage = Image.FromStream(ms);  // 創建 Image 物件
                        this.pictureBox1.Image = bikeImage;  // 將圖片顯示在 PictureBox 中
                    }

                }


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

            _isSeason = false;
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
            for (int i = 1; i < 5; i++)
            {
                season = $"第{i}季";
                comboBoxSeason.Items.Add(season);
            }

        }

        private void btnSeasonBike_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            // 將資料庫中的 Orders 表數據填充到本地的 nwDataSet1.Orders 表中
            this.productPhotoTableAdapter1.Fill(this.nwDataSet1.ProductPhoto);

            // 定義一個 LINQ 查詢
            var q = from o in this.nwDataSet1.ProductPhoto
                    where o.ModifiedDate.Month.distinctSeason() == comboBoxSeason.SelectedItem.ToString()  // 使用擴充方法篩選季節
                    orderby o.ModifiedDate.Month
                    select new
                    {
                        o.ThumbNailPhoto,
                        o.ThumbnailPhotoFileName,
                        o.ProductPhotoID,
                        o.LargePhoto,
                        o.LargePhotoFileName,
                        o.ModifiedDate,
                        Season = o.ModifiedDate.Month.distinctSeason()  // 使用擴充方法獲取季節
                    };

            // 將篩選和排序後的結果轉換為列表，並綁定到 DataGridView 控制項上，顯示在 UI 中
            var photolist = q.ToList();
            this.dataGridView1.DataSource = photolist;

            _isSeason = true;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            //
        }
    }
    // 擴充方法定義
    public static class IntExtend
    {
        // 擴充的方法，用於將月份轉換為對應的季節
        public static string distinctSeason(this int month)
        {
            if (month <= 3)
            {
                return "第1季";
            }
            else if (month > 3 && month <= 6)
            {
                return "第2季";
            }
            else if (month > 6 && month <= 9)
            {
                return "第3季";
            }
            else
            {
                return "第4季";
            }
        }
    }
}
