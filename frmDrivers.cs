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
using DriversBusinessLayer;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmDrivers : Form
    {

        DataTable Dt = new DataTable();

        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDriverscs_Load(object sender, EventArgs e)
        {
            cbFliterBy.SelectedIndex = 0;

            // Loading the data from the database:
            Dt = ClsDriversBusinessLayer.GetAllDrivers();
            dataGridView1.DataSource = Dt;

            DesignDataGridView();
        }


        void DesignDataGridView()
        {
            // عناوين الأعمدة
            dataGridView1.Columns["DriverID"].HeaderText = "Driver ID";
            dataGridView1.Columns["PersonID"].HeaderText = "Person ID";
            dataGridView1.Columns["NationalNumber"].HeaderText = "National No";
            dataGridView1.Columns["FullName"].HeaderText = "Full Name";
            dataGridView1.Columns["CreatedDate"].HeaderText = "Date";
            dataGridView1.Columns["isActive"].HeaderText = "Active Licenses";

            // سلوك عام
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;

            // ❌ لا Dock
            dataGridView1.Dock = DockStyle.None;

            // ✅ خلي مكانه ثابت ويتمدد مع الفورم
            dataGridView1.Anchor = AnchorStyles.Top
                                  | AnchorStyles.Left
                                  | AnchorStyles.Right
                                  | AnchorStyles.Bottom;

            // تكبير الخط
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // تكبير الارتفاع
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 45;
            dataGridView1.RowTemplate.Height = 40;

            // ألوان
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // محاذاة النص
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // الأعمدة تتمدد
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // عمود الاسم أعرض
            dataGridView1.Columns["FullName"].FillWeight = 200;
        }

        private void ApplyFilter()
        {
            if (!(dataGridView1.DataSource is DataTable dt))
                return;

            string filterText = txtFindBy.Text.Trim();

            switch (cbFliterBy.SelectedItem?.ToString())
            {
                case "None":
                    txtFindBy.Visible = false;
                    break;

                case "Driver ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "DriverID", filterText);
                    break;

                case "Person ID":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "PersonID", filterText);
                    break;

                case "National No":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "NationalNumber", filterText);
                    break;

                case "Full Name":
                    Operations_.Settings.ApplyTools(ref txtFindBy, ref dt, "FullName", filterText);
                    break;

                default:
                    MessageBox.Show("Invalid filter type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void cbFliterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Focus();

            ApplyFilter();
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
    }
}
