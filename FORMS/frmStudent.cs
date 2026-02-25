using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace CrossWord
{
    public partial class frmStudent : Form
    {
        public frmStudent()
        {
            InitializeComponent();
        }
        public static bool IsOpen = false;
        bool IsNew = false, IsEdit = false, IsSearch = false;
        private void frmStudent_Load(object sender, EventArgs e)
        {
            IsOpen = true;
            #region Load Data From Table Student
            Student obj = new Student();
            txtID.Text = (obj.GetMax(StudentFields.ID) + 1).ToString();
            txtStudentName.Text = obj.StudentName.Trim();
            DG.DataSource = obj.GetAll();
            if (DG.RowCount > 0 && !IsNew)
                DG_CellClick(null, new DataGridViewCellEventArgs(0, 0));
            DG.Columns[StudentFields.ID.ToString()].Visible = false;
            DG.Columns[StudentFields.StudentName.ToString()].Width = 280;
            #endregion
        }
        private void frmStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsOpen = false;
        }
        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
                return;
            #region Load Data
            txtID.Text = DG[StudentFields.ID.ToString(), e.RowIndex].Value.ToString();
            txtStudentName.Text = DG[StudentFields.StudentName.ToString(), e.RowIndex].Value.ToString().Trim();
            #endregion
            #region Disable Edit
            txtID.Enabled = false;
            txtStudentName.Enabled = false;
            IsEdit = false;
            IsNew = false;
            IsSearch = false;
            #endregion
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            #region New Record
            txtStudentName.Enabled = true;
            IsNew = true;
            frmStudent_Load(null, null);
            #endregion
            txtStudentName.Focus();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            #region Enable Edit
            txtStudentName.Enabled = true;
            IsEdit = true;
            #endregion
            txtStudentName.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Save New Record For Table Student
            Student obj = new Student();
            if (!DBA.IsNumber(txtID.Text.Trim()))
            {
                MessageBox.Show("ID Field Must Be Numeric!");
                txtID.Text = "0";
                txtID.Focus();
                return;
            }
            obj.ID = int.Parse(txtID.Text.Trim());
            obj.StudentName = txtStudentName.Text.Trim();
            if (IsNew)
            {
                obj.NewStudent();
                MessageBox.Show("New Record Successfully Inserted.");
                IsNew = false;
            }
            else if (IsEdit)
            {
                obj.SetAll();
                MessageBox.Show("Record Successfully Updated.");
                IsEdit = false;
            }
            frmStudent_Load(null, null);
            #endregion
            DG.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            #region Delete
            if (DG.RowCount <= 0) return;
            Student obj = new Student(int.Parse(txtID.Text.Trim()));
            obj.DeleteWhere(StudentFields.ID, CND.EQUAL);
            frmStudent_Load(null, null);
            #endregion
            DG.Focus();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                #region Search
                if (!IsSearch)
                {
                    txtStudentName.Enabled = true;
                    txtStudentName.Text = "";
                    IsSearch = true;
                    return;
                }
                IsSearch = false;
                Student obj = new Student();
                txtStudentName.Enabled = false;
                obj.StudentName = txtStudentName.Text.Trim();
                DG.DataSource = obj.GetWhereLike(StudentFields.StudentName);
                #endregion
                txtStudentName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occure During Student Searching!\nSystem Exception : " + ex.Message);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}