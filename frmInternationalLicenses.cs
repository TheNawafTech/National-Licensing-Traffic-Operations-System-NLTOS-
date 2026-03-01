using LocalDrivingLicenseList;
using nClsLocalAppLicenseBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nClsInternationalLicenseBusinessLayer;
using nfrmShowPersonDetails;
using DVLDBusinessLayer;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmInternationalLicenses : Form
    {
        DataTable Dt = new DataTable();
        private DataGridViewRow _rightClickedRow;

        public frmInternationalLicenses()
        {
            InitializeComponent();
        }

        void DesignDataGridView()
        {
            // Header Text
            dgvDrivingLicenes.Columns["InternationalDrivingLicenseApplicationID"].HeaderText = "I.D.L.AppID";
            dgvDrivingLicenes.Columns["LocalAppID"].HeaderText = "Local AppID";
            dgvDrivingLicenes.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvDrivingLicenes.Columns["DriverID"].HeaderText = "Driver ID";
            dgvDrivingLicenes.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvDrivingLicenes.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvDrivingLicenes.Columns["IsActive"].HeaderText = "Is Active";


            dgvDrivingLicenes.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);


            // ReadOnly & Rows
            dgvDrivingLicenes.ReadOnly = true;
            dgvDrivingLicenes.AllowUserToAddRows = false;

            // ====== الألوان (نفسها بالضبط) ======
            dgvDrivingLicenes.BorderStyle = BorderStyle.None;
            dgvDrivingLicenes.BackgroundColor = Color.White;

            dgvDrivingLicenes.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(238, 239, 249);

            dgvDrivingLicenes.DefaultCellStyle.SelectionBackColor =
                Color.DarkTurquoise;

            dgvDrivingLicenes.DefaultCellStyle.SelectionForeColor =
                Color.WhiteSmoke;

            dgvDrivingLicenes.EnableHeadersVisualStyles = false;

            dgvDrivingLicenes.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvDrivingLicenes.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(20, 25, 72);

            dgvDrivingLicenes.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            // ====== المحاذاة ======
            dgvDrivingLicenes.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvDrivingLicenes.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // ====== الحل الأساسي للمشكلة ======
            dgvDrivingLicenes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;   // لا تكررها

            dgvDrivingLicenes.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;              // لا يكسر العناوين

            dgvDrivingLicenes.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;
        }


        private void frmInternationalLicenses_Load(object sender, EventArgs e)
        {
            Dt = ClsInternationalBusinessLayer.GetAllInternationalsLicences_();

            dgvDrivingLicenes.DataSource = Dt;

            DesignDataGridView();


            cbFindBy.SelectedIndex = 0;
        }

        private void btnNewLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();

            frm.ShowDialog();

            //refresh:
            frmInternationalLicenses_Load(sender, e);
        }

        private void ApplyFilter()
        {
            if (!(dgvDrivingLicenes.DataSource is DataTable dt))
                return;

            string filterText = txtFindBy.Text.Trim();

            switch (cbFindBy.SelectedItem?.ToString())
            {
                case "None":
                    txtFindBy.Visible = false;
                    break;

                case "I.D.L.AppID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "InternationalDrivingLicenseApplicationID", filterText);
                    break;

                case "Local AppID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "LocalAppID", filterText);
                    break;

                case "Application ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "ApplicationID", filterText);
                    break;

                case "Driver ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "DriverID", filterText);
                    break;

                case "Issue Date":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "IssueDate", filterText);
                    break;

                case "Expiration Date":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "ExpirationDate", filterText);
                    break;

                case "Is Active":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "IsActive", filterText);
                    break;

                default:
                    MessageBox.Show("Invalid filter type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Focus();

            ApplyFilter();

        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        public void OpenTestAppointmentsForm(int TestType)
        {
            if (_rightClickedRow == null)
            {
                MessageBox.Show("Please right-click on a row first.");
                return;
            }

            int LocalApp = Convert.ToInt32(_rightClickedRow.Cells["LocalDrivingLicenseApplicationID"].Value);

        }

        public int GetPersonID()
        {
            int PersonID = Convert.ToInt16
                (ClsInternationalBusinessLayer.GetPersonID_By_DriverID
                (Convert.ToString(_rightClickedRow.Cells["DriverID"].Value)));

            return PersonID;
        }
        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = GetPersonID();

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = GetPersonID();

            nfrmShowPersonDetails.frmShowPersonDetails frm = new nfrmShowPersonDetails.frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void dgvDrivingLicenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _rightClickedRow = dgvDrivingLicenes.Rows[e.RowIndex];
            }

        }

        private void dgvDrivingLicenes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDrivingLicenes.ClearSelection();
                dgvDrivingLicenes.Rows[e.RowIndex].Selected = true;
                _rightClickedRow = dgvDrivingLicenes.Rows[e.RowIndex];
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalApp = Convert.ToInt16(_rightClickedRow.Cells["LocalAppID"].Value.ToString());

            frmShowLicense frm = new frmShowLicense(LocalApp);
            frm.ShowDialog();
        }
    }
}
