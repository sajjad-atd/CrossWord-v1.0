using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace CrossWord
{
    public partial class frmDictionary : Form
    {
        public frmDictionary()
        {
            InitializeComponent();
        }
        public static bool IsOpen = false;
        bool IsNew = false, IsEdit = false, IsSearch = false;
        private void frmDictionary_Load(object sender, EventArgs e)
        {
            IsOpen = true;
            #region Load Data From Table Dictionary
            Dictionary obj = new Dictionary();
            txtSNo.Text = (obj.GetMax(DictionaryFields.SNo) + 1).ToString();
            txtEnglish.Text = obj.English.Trim();
            txtUrdu.Text = obj.Urdu.Trim();
            DG.DataSource = obj.GetAll();
            if (DG.RowCount > 0 && !IsNew)
                DG_CellClick(null, new DataGridViewCellEventArgs(0, 0));
            for (int i = 0; DG.ColumnCount > i; i++)
                DG.Columns[i].Visible = false;
            DG.Columns[DictionaryFields.English.ToString()].Visible = true;
            DG.Columns[DictionaryFields.English.ToString()].Width = 200;
            DG.Columns[DictionaryFields.Urdu.ToString()].Visible = true;
            DG.Columns[DictionaryFields.Urdu.ToString()].Width = 500;
            #endregion
        }
        private void frmDictionary_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsOpen = false;
        }
        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
                return;
            #region Load Data
            txtSNo.Text = DG[DictionaryFields.SNo.ToString(), e.RowIndex].Value.ToString();
            txtEnglish.Text = DG[DictionaryFields.English.ToString(), e.RowIndex].Value.ToString().Trim();
            txtUrdu.Text = DG[DictionaryFields.Urdu.ToString(), e.RowIndex].Value.ToString().Trim();
            #endregion
            #region Disable Edit
            txtSNo.Enabled = false;
            txtEnglish.Enabled = false;
            txtUrdu.Enabled = false;
            IsEdit = false;
            IsNew = false;
            IsSearch = false;
            #endregion
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            #region New Record
            txtEnglish.Enabled = true;
            txtUrdu.Enabled = true;
            IsNew = true;
            frmDictionary_Load(null, null);
            #endregion
            txtEnglish.Focus();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            #region Enable Edit
            txtEnglish.Enabled = true;
            txtUrdu.Enabled = true;
            IsEdit = true;
            #endregion
            txtEnglish.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Save New Record For Table Dictionary
            Dictionary obj = new Dictionary();
            if (!DBA.IsNumber(txtSNo.Text.Trim()))
            {
                MessageBox.Show("SNo Field Must Be Numeric!");
                txtSNo.Text = "0";
                txtSNo.Focus();
                return;
            }
            obj.SNo = int.Parse(txtSNo.Text.Trim());
            obj.English = txtEnglish.Text.Trim();
            obj.Urdu = txtUrdu.Text.Trim();
            if (IsNew)
            {
                obj.NewDictionary();
                //MessageBox.Show("New Record Successfully Inserted.");
                IsNew = false;
            }
            else if (IsEdit)
            {
                obj.SetAll();
                MessageBox.Show("Record Successfully Updated.");
                IsEdit = false;
            }
            frmDictionary_Load(null, null);
            #endregion
            DG.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            #region Delete
            if (DG.RowCount <= 0) return;
            Dictionary obj = new Dictionary(int.Parse(txtSNo.Text.Trim()));
            obj.DeleteWhere(DictionaryFields.SNo, CND.EQUAL);
            frmDictionary_Load(null, null);
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
                    txtEnglish.Enabled = true;
                    txtEnglish.Text = "";
                    txtUrdu.Enabled = true;
                    txtUrdu.Text = "";
                    IsSearch = true;
                    return;
                }
                IsSearch = false;
                Dictionary obj = new Dictionary();
                txtEnglish.Enabled = false;
                obj.English = txtEnglish.Text.Trim();
                txtUrdu.Enabled = false;
                obj.Urdu = txtUrdu.Text.Trim();
                DG.DataSource = obj.GetWhereLike(DictionaryFields.English, DictionaryFields.Urdu);
                #endregion
                txtEnglish.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occure During Dictionary Searching!\nSystem Exception : " + ex.Message);
            }
        }

        private void DG_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                DG_CellClick(sender, new DataGridViewCellEventArgs(DG.CurrentCell.ColumnIndex, DG.CurrentRow.Index));
        }
    }
}