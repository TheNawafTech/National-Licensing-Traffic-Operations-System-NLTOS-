using LocalDrivingLicenseList;
using nClsInternationalLicenseBusinessLayer;
using nClsLicenesDataLayer;
using nClsLicenseBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmReplacementForDamgedLicense : Form
    {
        int _PersonID;
        int ReplacmentAppID = 0;
        int _ReplacementlicenseID = 0;
        public frmReplacementForDamgedLicense()
        {
            InitializeComponent();
        }

        void _SetDefultValues()
        {
            lbAppDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = ClsCurrentUser.ClsGlobal.CurrentUser.FullName;
            lbAppFees.Text = "5";
            RnDamgedLicense.Checked = true;
        }

        string GetExpireDate(string licenseID)
        {
            return ClsLicenesBusinessLayer.GetLicenseExpireDate(Convert.ToInt32(licenseID)).ToString();
        }

        bool CheckIsLicenseExpired(string licenseID)
        {
            string ExpireDate = GetExpireDate(licenseID);
            int licenseIDInt = Convert.ToInt32(licenseID);

            lbOldLicenseID.Text = licenseID.ToString();

            return true;
        }

        static public bool IsLicenseExpired(int licenseID)
        {
            return ClsLicenesDataLayer.IsLicenseExpiredFromSQL(licenseID);
        }
        public bool IsLicenseActive(string licenseID)
        {
            return ClsLicenesBusinessLayer.IsLicenseActive(Convert.ToInt32(licenseID));
        }

        void SearchMethod(string licenseID, bool IsEnableTextBoxOnSearch = true)
        {
            if (!int.TryParse(licenseID, out int LicenseID))
            {
                MessageBox.Show("License number must contain numbers only.");
                return;
            }

            usFindDriverLicenseInfo1.SetLicenseInfoAtScreen(LicenseID);


            if (!CheckIsLicenseExpired(licenseID))
                return;

            if (!IsLicenseActive(licenseID))
            {
                btnIssue.Enabled = false;
                MessageBox.Show($"The selected license is not active , choose an active license.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                lnkbLicenseHistory.Enabled = true;
                //lnkbShowNewLicenseInfo.Enabled = true;

                int _LLicenseID;

                if (!int.TryParse(licenseID, out _LLicenseID))
                {
                    MessageBox.Show("License number must contain numbers only.");
                    return;
                }

                return;
            }
            btnIssue.Enabled = true;
            lnkbLicenseHistory.Enabled = true;
        }

        private void frmReplacementForDamgedLicense_Load(object sender, EventArgs e)
        {
            _SetDefultValues();

            usFindDriverLicenseInfo1.OnSearchClicked += SearchMethod;
        }

        private void RdDamgedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lbTitle.Text = "Replacement for Damged License";
            lbAppFees.Text = "5";
        }

        private void RdLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lbTitle.Text = "Replacement for Lost License";
            lbAppFees.Text = "10";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkbShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ReplacmentAppID != 0)
            {
                frmShowLicense frm = new frmShowLicense(Convert.ToString(ReplacmentAppID));
                frm.ShowDialog();
                return;
            }


            string __AppID = Convert.ToString(ClsLicenesBusinessLayer.GetAppIDByLicenseID(_ReplacementlicenseID));

            frmShowLicense frmShowLicense = new frmShowLicense(__AppID);
            frmShowLicense.ShowDialog();
        }

        private void lnkbLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int personID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID
               (Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            _PersonID = personID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        
        private void btnIssue_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to issue a replacement license?", "Confirm Issuance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            _PersonID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID
               (Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            if (result == DialogResult.Yes)
            {
                if (ClsLicenesBusinessLayer.ReplacmentLicense(lbTitle.Text, ref ReplacmentAppID, ref _ReplacementlicenseID,
                    Convert.ToInt32(lbOldLicenseID.Text), DateTime.Now, lbAppFees.Text, _PersonID))
                {
                    lbReplacementAppID.Text = ReplacmentAppID.ToString();
                    lbReplacedLicenseID.Text = _ReplacementlicenseID.ToString();

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show($"License Replaced successfully!");
                        btnIssue.Enabled = false;
                        lnkbShowNewLicenseInfo.Enabled = true;
                    }
                }
            }
        }

      
    }
}
