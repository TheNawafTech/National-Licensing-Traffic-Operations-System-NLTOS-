using nclsDetainLicenseBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Operations_.Settings;
using nfrmShowPersonDetails;
using LocalDrivingLicenseList;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmManageDetainedLicense : Form
    {
        private DataGridViewRow _rightClickedRow;

        public frmManageDetainedLicense()
        {
            InitializeComponent();
        }

        void DesignDataGridView()
        {

            dgvDetainedLicense.DefaultCellStyle.Font =
    new Font("Segoe UI", 12F, FontStyle.Regular);

            dgvDetainedLicense.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvDetainedLicense.RowTemplate.Height = 35; // يكبر ارتفاع الصف عشان يريح العين
            dgvDetainedLicense.ReadOnly = true;
            dgvDetainedLicense.BorderStyle = BorderStyle.None;
            dgvDetainedLicense.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(238, 239, 249);
            dgvDetainedLicense.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetainedLicense.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dgvDetainedLicense.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dgvDetainedLicense.BackgroundColor = System.Drawing.Color.White;
            dgvDetainedLicense.EnableHeadersVisualStyles = false;
            dgvDetainedLicense.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDetainedLicense.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 25, 72);
            dgvDetainedLicense.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvDetainedLicense.AllowUserToAddRows = false;

            // يخلي الأعمدة تتمدد بالتساوي داخل الجدول
            dgvDetainedLicense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // يخلي النص داخل الأعمدة يكون في المنتصف
            dgvDetainedLicense.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetainedLicense.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // جعل الأعمدة الأخرى تتناسب تلقائيًا
            dgvDetainedLicense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // ثم خصّص عمود FullName بعرض ثابت
            dgvDetainedLicense.Columns["Full Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDetainedLicense.Columns["Full Name"].Width = 200; // 👈 غيّر الرقم حسب رغبتك (150 أو 250 مثلاً)
            
            //dgvDetainedLicense.Columns["DrivingClass"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void frmManageDetainedLicense_Load(object sender, EventArgs e)
        {
            DataTable Dt = ClsDetainLicenseBusinessLayer.GetAllDetainedLicense();

            dgvDetainedLicense.DataSource = Dt;
            dgvDetainedLicense.Columns["PersonID"].Visible = false;

            lbRecords.Text = (dgvDetainedLicense.Rows.Count - 1).ToString();
            cbFindBy.SelectedIndex = 0;

            
            DesignDataGridView();
        }

        private void btnRelaseDetainLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();

            // Refresh:
            frmManageDetainedLicense_Load(sender, e);
        }

        private void BtnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();

            // Refresh:
            frmManageDetainedLicense_Load(sender, e);
        }

        private void ApplyFilter()
        {
            if (!(dgvDetainedLicense.DataSource is DataTable dt))
                return;

            string filterText = txtFindBy.Text.Trim();

            switch (cbFindBy.SelectedItem?.ToString())
            {
                case "None":
                    txtFindBy.Visible = false;
                    break;

                case "Detain ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "D.ID", filterText);
                    break;

                case "Is Released":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "Is Released", filterText);
                    break;

                case "National No.":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "N.No.", filterText);
                    break;

                case "Full Name":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "Full Name", filterText);
                    break;

                case "Release Application ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "Release App.ID", filterText);
                    break;

                default:
                    MessageBox.Show("Invalid filter type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Clear();
            txtFindBy.Focus();
           
            ApplyFilter();
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)dgvDetainedLicense.CurrentRow.DataBoundItem;
            int PersonID = Convert.ToInt16(row["PersonID"].ToString());

            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)dgvDetainedLicense.CurrentRow.DataBoundItem;
            int LicenseID = Convert.ToInt16(row["L.ID"].ToString());

            frmShowLicense frm = new frmShowLicense(LicenseID, true);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)dgvDetainedLicense.CurrentRow.DataBoundItem;
            int PersonID = Convert.ToInt16(row["PersonID"].ToString());

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)dgvDetainedLicense.CurrentRow.DataBoundItem;
            int LicenseID = Convert.ToInt16(row["L.ID"].ToString());

            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID, false);
            frm.ShowDialog();
        }
        private void dgvDetainedLicense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _rightClickedRow = dgvDetainedLicense.Rows[e.RowIndex];
            }

            DataRowView row = (DataRowView)dgvDetainedLicense.CurrentRow.DataBoundItem;

            bool isEmpty = row["Release App.ID"] == DBNull.Value
                           || string.IsNullOrWhiteSpace(row["Release App.ID"].ToString());

            releaseDetainedLicenseToolStripMenuItem.Enabled = isEmpty;
        }

        private void dgvDetainedLicense_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.Button == MouseButtons.Right)
            {
                dgvDetainedLicense.ClearSelection();
                dgvDetainedLicense.Rows[e.RowIndex].Selected = true;
                dgvDetainedLicense.CurrentCell = dgvDetainedLicense.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvDetainedLicense.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

            if (!(dgvDetainedLicense.CurrentRow.DataBoundItem is DataRowView row))
            {
                e.Cancel = true;
                return;
            }

            bool canRelease = string.IsNullOrWhiteSpace(row["Release App.ID"]?.ToString());
            bool isReleased = string.Equals(row["Is Released"]?.ToString(), "Yes", StringComparison.OrdinalIgnoreCase);

            releaseDetainedLicenseToolStripMenuItem.Enabled = canRelease && !isReleased;
        }
    }
}
