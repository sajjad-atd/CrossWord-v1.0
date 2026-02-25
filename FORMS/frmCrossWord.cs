using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrossWord
{
    public partial class frmCrossWord : Form
    {
        public static string[] str2 = new string[11];
        public static int[] result = new int[6];
        string[] str1 = new string[11];
        string[] lb = new string[11];
        string strResult = "";
        Point lastPoint;
        int lastClick;
        Pen pn = new Pen(Color.Black);

        public frmCrossWord()
        {
            InitializeComponent();
        }

        private void frmCrossWord_Load(object sender, EventArgs e)
        {
            lastClick = -1;
            lastPoint.X = lastPoint.Y = -1;
            pn.Width = 2;
            str2[0] = "";
            str1 = (string[])str2.Clone();
            Random r = new Random();
            int count = 0, arrNo;
            while (count != 5)
            {
                arrNo = r.Next(6);
                if (str2[arrNo] != "")
                {
                    count++;
                    lb[arrNo] = str2[arrNo];
                    lb[arrNo + 5] = str2[count + 5];
                    str2[arrNo] = "";
                }
            }
            lb1.Text = lb[1]; lb11.Text = lb[1 + 5];
            lb2.Text = lb[2]; lb22.Text = lb[2 + 5];
            lb3.Text = lb[3]; lb33.Text = lb[3 + 5];
            lb4.Text = lb[4]; lb44.Text = lb[4 + 5];
            lb5.Text = lb[5]; lb55.Text = lb[5 + 5];
            str2 = (string[])str1.Clone();
        }

        private void chk11_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick == -1)
            {
                chk11.Checked = false;
                return;
            }
            chk11.Enabled = false;
            str2[lastClick] = lb[lastClick];
            str2[lastClick + 5] = lb[1 + 5];
            Graphics g;
            g = this.CreateGraphics();
            g.DrawLine(pn, lastPoint.X, lastPoint.Y, chk11.Location.X, chk11.Location.Y);
            g.Dispose();
            lastClick = -1;
        }

        private void chk22_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick == -1)
            {
                chk22.Checked = false;
                return;
            }
            chk22.Enabled = false;
            str2[lastClick] = lb[lastClick];
            str2[lastClick + 5] = lb[2 + 5];
            Graphics g;
            g = this.CreateGraphics();
            g.DrawLine(pn, lastPoint.X, lastPoint.Y, chk22.Location.X, chk22.Location.Y);
            g.Dispose();
            lastClick = -1;
        }

        private void chk33_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick == -1)
            {
                chk33.Checked = false;
                return;
            }
            chk33.Enabled = false;
            str2[lastClick] = lb[lastClick];
            str2[lastClick + 5] = lb[3 + 5];
            Graphics g;
            g = this.CreateGraphics();
            g.DrawLine(pn, lastPoint.X, lastPoint.Y, chk33.Location.X, chk33.Location.Y);
            g.Dispose();
            lastClick = -1;
        }

        private void chk44_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick == -1)
            {
                chk44.Checked = false;
                return;
            }
            chk44.Enabled = false;
            str2[lastClick] = lb[lastClick];
            str2[lastClick + 5] = lb[4 + 5];
            Graphics g;
            g = this.CreateGraphics();
            g.DrawLine(pn, lastPoint.X, lastPoint.Y, chk44.Location.X, chk44.Location.Y);
            g.Dispose();
            lastClick = -1;
        }

        private void chk55_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick == -1)
            {
                chk55.Checked = false;
                return;
            }
            chk55.Enabled = false;
            str2[lastClick] = lb[lastClick];
            str2[lastClick + 5] = lb[5 + 5];
            Graphics g;
            g = this.CreateGraphics();
            g.DrawLine(pn, lastPoint.X, lastPoint.Y, chk55.Location.X, chk55.Location.Y);
            g.Dispose();
            lastClick = -1;
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick != -1)
            {
                chk1.Checked = false;
                return;
            }
            chk1.Enabled = false;
            lastClick = 1;
            lastPoint = chk1.Location;
        }
        
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick != -1)
            {
                chk2.Checked = false;
                return;
            }
            chk2.Enabled = false;
            lastClick = 2;
            lastPoint = chk2.Location;
        }

        private void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick != -1)
            {
                chk3.Checked = false;
                return;
            }
            chk3.Enabled = false;
            lastClick = 3;
            lastPoint = chk3.Location;
        }

        private void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick != -1)
            {
                chk4.Checked = false;
                return;
            }
            chk4.Enabled = false;
            lastClick = 4;
            lastPoint = chk4.Location;
        }

        private void chk5_CheckedChanged(object sender, EventArgs e)
        {
            if (lastClick != -1)
            {
                chk5.Checked = false;
                return;
            }
            chk5.Enabled = false;
            lastClick = 5;
            lastPoint = chk5.Location;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            strResult = "";
            if (chk11.Enabled == true || chk22.Enabled == true || chk33.Enabled == true || chk44.Enabled == true || chk55.Enabled == true) return;
            for (int i = 1; i < 6; i++)
            {
                if (str1[i] == str2[i] && str1[i + 5] == str2[i + 5])
                    strResult += str1[i] + " = True\n";
                else 
                {
                    strResult += str1[i] + " = " + str1[i + 5] + "\n";
                    result[i] = -1;
                }
            }
            lbResult.Text = strResult;
        }
        
        private void frmLessonList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chk11.Enabled == true || chk22.Enabled == true || chk33.Enabled == true || chk44.Enabled == true || chk55.Enabled == true)
                for (int i = 1; i < 6; i++)
                    result[i] = -1;
        }
    }
}