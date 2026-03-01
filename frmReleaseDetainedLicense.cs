using ClsCurrentUser;
using LocalDrivingLicenseList;
using nclsDetainLicenseBusinessLayer;
using nClsInternationalLicenseBusinessLayer;
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
using static nclsDetainLicenseBusinessLayer.ClsDetainLicenseBusinessLayer;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private string _Notes;
        private int _LicenseID = -1;
        private bool _IsEnableTextBoxOnSearch = true;


        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID, bool IsEnableTextBox)
        {
            InitializeComponent();
            this._LicenseID = LicenseID;
            this._IsEnableTextBoxOnSearch = IsEnableTextBox;

            SearchMethod(Convert.ToString(this._LicenseID), _IsEnableTextBoxOnSearch);
        }

        ClsDetainLicenseInfo _DetainInfo = new ClsDetainLicenseInfo();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool VlaidateInputs(int LicenseID)
        {
            if (!ClsLicenesBusinessLayer.IsLicenseExsists(LicenseID))
            {
                return true;
            }

            if (!usFindDriverLicenseInfo1.isDetained)
            {
                MessageBox.Show("selected license is not detained");
                btnRelease.Enabled = false;
                return true;
            }
            else
            {
                btnRelease.Enabled = true;
                lnkNotes.Enabled = true;
                return false;
            }
        }

        void SetDetainInfoAtScreen(int licenseID)
        {

            ClsDetainLicenseInfo DetainInfo = ClsDetainLicenseBusinessLayer.GetDetainLicenseInfoByLicenseID(licenseID);

            if (DetainInfo.DetainID == 0)
            {
                lbDetainID.Text = "0";
                lbDetainReason.Text = "";
                lbFineFees.Text = "0";
                lbCreatedBy.Text = "";
                _Notes = "";
            }
            else
            {
                lbDetainID.Text = Convert.ToString(DetainInfo.DetainID);
                lbDetainReason.Text = DetainInfo.DetainReason;
                lbDetainDate.Text = DetainInfo.DetainDate.ToString();
                lbFineFees.Text = Convert.ToString(DetainInfo.DetainFee);
                lbCreatedBy.Text = DetainInfo.CreatedBy;
                _Notes = DetainInfo.Notes;
                lbTotalFees.Text = (DetainInfo.DetainFee + 15).ToString();
            }
        }

        void SearchMethod(string licenseID, bool EnableTextBoxOnSearch = true)
        {
            if (!int.TryParse(licenseID, out int LicenseID))
            {
                MessageBox.Show("License number must contain numbers only.");
                return;
            }

            usFindDriverLicenseInfo1.SetLicenseInfoAtScreen(LicenseID, EnableTextBoxOnSearch);
            
            lbLicenseID.Text = usFindDriverLicenseInfo1.StrLicenseID.ToString();
            
            lnkbLicenseHistory.Enabled = true;
            lnkShowDriverInfo.Enabled = true;


            if (VlaidateInputs(LicenseID))
                return;
            else
                btnRelease.Enabled = true;

            SetDetainInfoAtScreen(LicenseID);
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            
            usFindDriverLicenseInfo1.OnSearchClicked += SearchMethod;
            
            lbCreatedBy.Text = ClsGlobal.CurrentUser.FullName;

        }

        private void lnkShowDriverInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int licenseID;

            if (!int.TryParse(lbLicenseID.Text, out licenseID))
            {
                licenseID = 0;
            }


            string __AppID = Convert.ToString(ClsLicenesBusinessLayer.GetAppIDByLicenseID(licenseID));

            frmShowLicense frmShowLicense = new frmShowLicense(__AppID);
            frmShowLicense.ShowDialog();

        }

        bool ValidateLicenseHistoryButton()
        {
            if (lbLicenseID.Text == "0" || lbLicenseID.Text == "??" || lbLicenseID.Text == "")
            {
                MessageBox.Show("Please search for a license first.");
                return false;
            }
            return true;
        }
        private void lnkbLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!ValidateLicenseHistoryButton())
                return;


            int personID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID
  (Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            //_PersonID = personID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        int ReleaseLicense()
        {
            int ReleaseAppID = -1;
            if (lbLicenseID.Text == "0" || lbLicenseID.Text == "??" || lbLicenseID.Text == "")
            {
                MessageBox.Show("Please search for a license first.");
                return -1;
            }

            int PersonID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID(Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            ReleaseAppID = ClsInternationalBusinessLayer.CreateApplication(PersonID, 5, Convert.ToDecimal(lbTotalFees.Text));

            if (ClsDetainLicenseBusinessLayer.ReleaseDetainedLicense(Convert.ToInt32(lbLicenseID.Text)) && ClsDetainLicenseBusinessLayer.ReleaseDetainedLicense_DetainTabe(Convert.ToInt32(lbDetainID.Text), ReleaseAppID))
            {
                return ReleaseAppID;
            }

            return -1;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to release this license?", "Confirm Release", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (ReleaseLicense() != -1)
                {
                    btnRelease.Enabled = false;
                    usFindDriverLicenseInfo1.SetLicenseInfoAtScreen(Convert.ToInt32(lbLicenseID.Text));
                   
                    MessageBox.Show("License released successfully.");
                }
                else
                    MessageBox.Show("Failed to release the license. Please try again.");
            }
        }

        private void usFindDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void lnkNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNotes frm = new frmNotes(_Notes);
            frm.ShowDialog();
        }
    }
}