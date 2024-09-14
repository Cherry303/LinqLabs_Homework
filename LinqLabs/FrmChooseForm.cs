using LinqLabs.作業;
using MyHomeWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class FrmChooseForm : Form
    {
        public FrmChooseForm()
        {
            InitializeComponent();
        }

        private void btnHome1_Click(object sender, EventArgs e)
        {
            // 隱藏當前視窗，而不是關閉
            this.Hide();

            // 創建並顯示新的視窗
            Frm作業_1 f = new Frm作業_1();
            f.Show();

        }

        private void btnHome2_Click(object sender, EventArgs e)
        {
            // 隱藏當前視窗，而不是關閉
            this.Hide();

            // 創建並顯示新的視窗
            Frm作業_2 f = new Frm作業_2();
            f.Show();
        }

        private void btnHome3_Click(object sender, EventArgs e)
        {
            // 隱藏當前視窗，而不是關閉
            this.Hide();

            // 創建並顯示新的視窗
            Frm作業_3 f = new Frm作業_3();
            f.Show();
        }
    }
}
