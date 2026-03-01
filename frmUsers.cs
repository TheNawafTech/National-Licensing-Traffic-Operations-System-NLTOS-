using ClsUser_BusinessLayer;
using FrmAddNewUser_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using frmChangePassword;
using Operations_;
using DVLDBusinessLayer;
namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmUsers : Form
    {
        ClsUser User = new ClsUser();

        DataGridViewRow selectedRow;

        string UserID = "-1";

        public frmUsers()
        {
            InitializeComponent();
        }

        public void _DesignTheTable()
        {
            dgvUsers.Columns["UserID"].HeaderText = "User ID";
            dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
            dgvUsers.Columns["FullName"].HeaderText = "Full Name";
            dgvUsers.Columns["UserName"].HeaderText = "UserName";
            dgvUsers.Columns["IsActive"].HeaderText = "Is Active";

            dgvUsers.ReadOnly = true;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(238, 239, 249);
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dgvUsers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dgvUsers.BackgroundColor = System.Drawing.Color.White;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 25, 72);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // عناوين الأعمدة
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Regular);

            // بيانات الصفوف
            dgvUsers.DefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Regular);

            // تكبير الصفوف ليتناسب مع الخط
            dgvUsers.RowTemplate.Height = 35;

        }

        private void _ApplyFilterOnActive()
        {
            if (!(dgvUsers.DataSource is DataTable dt))
                return;

            if (cbActiveStatus.SelectedItem == null)
            {
                dt.DefaultView.RowFilter = "";
                return;
            }

            string selected = cbActiveStatus.SelectedItem.ToString();
            string rowFilter = "";

            switch (selected)
            {
                case "Yes":
                    rowFilter = "IsActive = true";
                    break;

                case "No":
                    rowFilter = "IsActive = false";
                    break;

                case "All":
                default:
                    rowFilter = "";
                    break;
            }

            dt.DefaultView.RowFilter = rowFilter;
        }

        private void _DesiginFinding()
        {
            cbActiveStatus.Visible = false;
            txtSearchValue.Text = "";
        }

        private void _ApplyFilter()
        {
            if (!(dgvUsers.DataSource is DataTable dt))
                return;

            string filterText = txtSearchValue.Text.Trim();

            switch (cbFindBy.SelectedItem?.ToString())
            {
                case "None":
                    {
                        txtSearchValue.Visible = false;
                        cbActiveStatus.Visible = false;
                        txtSearchValue.Text = "";

                        break;
                    }
                case "Person ID":
                    {
                        Operations_.Settings.ApplyTools(ref txtSearchValue, ref dt, "PersonID", filterText);
                        break;

                    }

                case "User ID":
                    {
                        Operations_.Settings.ApplyTools(ref txtSearchValue, ref dt, "UserID", filterText);
                        break;
                    }

                case "UserName":
                    {
                        Operations_.Settings.ApplyTools(ref txtSearchValue, ref dt, "UserName", filterText);
                        break;

                    }
                case "Full Name":
                    {
                        Operations_.Settings.ApplyTools(ref txtSearchValue, ref dt, "FullName", filterText);
                        break;
                    }
                case "Is Active":
                    {
                        
                        txtSearchValue.Visible = false;
                        cbActiveStatus.Visible = true;
                        cbActiveStatus.Focus();

                        break;
                    }

                default:
                    MessageBox.Show("Invalid filter type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void _ResetTheValues()
        {
            cbFindBy.SelectedIndex = 0;
            cbActiveStatus.SelectedIndex = 0;

            dgvUsers.DataSource = ClsUser.GetAllUsers();

            lbRecords.Text = (dgvUsers.RowCount - 1).ToString();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _ResetTheValues();
            _DesignTheTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            FrmAddNewUser_.frmAddAndUpdateUser frm = new frmAddAndUpdateUser();
            frm.ShowDialog();

            _ResetTheValues(); // Refresh the data
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchValue.Focus();
            _DesiginFinding();

            _ApplyFilter();

        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void cbActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyFilterOnActive();
        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvUsers.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvUsers.ClearSelection();
                    dgvUsers.Rows[hit.RowIndex].Selected = true;
                    dgvUsers.CurrentCell = dgvUsers.Rows[hit.RowIndex].Cells[0];
                }
            }
        }

        private bool _VaidateRowSelection()
        {
            if (User == null)
            {
                MessageBox.Show("User not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
            if (selectedRow.Cells["UserID"].Value == null)
            {
                MessageBox.Show("Invalid selection. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void _SelectFromMenue()
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                selectedRow = dgvUsers.SelectedRows[0];
            }
            else
            {
                selectedRow = null;
            }
        }

        private void showDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                _SelectFromMenue();

                 UserID = selectedRow.Cells["UserID"].Value.ToString();
                User = ClsUser.FindByUserID(int.Parse(UserID));


                frmCurrentUser.frmUserInfo frm = new frmCurrentUser.frmUserInfo(User);
                frm.ShowDialog();
                
                _ResetTheValues(); // Refresh the data
            }
           
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                _SelectFromMenue();

                UserID = (selectedRow.Cells["UserID"].Value.ToString());
                User = ClsUser.FindByUserID(int.Parse(UserID));


                frmChangePassword.frmChangePassword frm = new frmChangePassword.frmChangePassword(User);
                frm.ShowDialog();

                _ResetTheValues(); // Refresh the data
            }
           
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                _SelectFromMenue();

                UserID = (selectedRow.Cells["UserID"].Value.ToString());

                User = ClsUser.FindByUserID(int.Parse(UserID));

                if (ClsUser.DeleteUser(User))
                {
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ResetTheValues(); // Refresh the data
                }
                else
                {
                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                _SelectFromMenue();


                frmAddAndUpdateUser frmAddNewUser = new frmAddAndUpdateUser();
                frmAddNewUser.ShowDialog();

                _ResetTheValues(); // Refresh the data
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                _SelectFromMenue();

                UserID = (selectedRow.Cells["UserID"].Value.ToString());

               ClsUser User = ClsUser.FindByUserID(int.Parse(UserID));

                frmAddAndUpdateUser frm = new frmAddAndUpdateUser(User);
                frm.ShowDialog();

                _ResetTheValues(); // Refresh the data
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                MessageBox.Show("This feature is not implemented yet.", "Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_VaidateRowSelection())
                    return;

                MessageBox.Show("This feature is not implemented yet.", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
