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
        public Frm作業_3()
        {
            InitializeComponent();

            //hint
            //students_scores = new List<Student>()
            //                             {
            //                                new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
            //                                new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
            //                                new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
            //                                new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
            //                                new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
            //                                new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

            //                              };
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 共幾個 學員成績 ?

            // 找出 前面三個 的學員所有科目成績

            // 找出 後面兩個的學員所有科目成績

            // 找出 Name "aaa","bbb","ccc" 的學員國文英文科目成績

            // 找出學員 "bbb" 的成績

            // 找出除了 "bbb" 學員的所有成績 ("bbb" 退學)

            // 找出 "aaa","bbb" "ccc" 學員 國文數學兩科 科目成績 且 數學不及格 ... 是誰

            #endregion
        }

        private void button37_Click(object sender, EventArgs e)
        {
            // 個人成績 sum, min, max, avg
            // 統計 每個學生個人成績 並排序
        }

    }
}
