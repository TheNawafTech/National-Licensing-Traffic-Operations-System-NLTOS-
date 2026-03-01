using ClsCurrentUser;
using DVLD_Controls;
using frmViotion;
using LocalDrivingLicenseList;
using nClsInternationalLicenseBusinessLayer;
using nClsLicenseBusinessLayer;
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


namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmRenewLicenseApp : Form
    {
        string _AppID = "0";
        int _licenseID = 0;
        void SetLeftValues(ref int licenseID, ref string ExpireDate )
        {
            lbLicenseFees.Text = "20";
            lbTotalFees.Text = "27";

            lbExpirationDate.Text = DateTime.TryParse(ExpireDate, out _) ?
                DateTime.Now.AddYears(10).ToString("yyyy/MM/dd") : "??";

            lbOldLicenseID.Text = licenseID.ToString();
        }

        string GetExpireDate(string licenseID)
        {
            return  ClsLicenesBusinessLayer.GetLicenseExpireDate(Convert.ToInt32(licenseID)).ToString();
        }
       
        bool CheckIsLicenseExpired(string licenseID)
        {
            string ExpireDate = GetExpireDate(licenseID);
            int licenseIDInt = Convert.ToInt32(licenseID);

            SetLeftValues(ref licenseIDInt, ref ExpireDate);

            if (!ClsLicenesBusinessLayer.IsLicenseExpired(licenseIDInt))
            {
                if (lbOldLicenseID.Text == "??")
                {
                   // MessageBox.Show($"Invalid License Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    lbShowLicenseHistory.Enabled = true;
                    return false;
                };

                MessageBox.Show($"Selected license is not yet expiared, it will expire on: {ExpireDate}",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                lbShowLicenseHistory.Enabled = true;

                return false;
            }
            return true;
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
                btnRenew.Enabled = false;
                MessageBox.Show($"The selected license is not active. It might have been renewed with another License ID.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
              
                lbShowLicenseHistory.Enabled = true;
                //lbShowNewLicenseInfo.Enabled = true;

                int _LLicenseID;

                if (!int.TryParse(licenseID, out _LLicenseID))
                {
                    MessageBox.Show("License number must contain numbers only.");
                    return;
                }

                return;
            }
                btnRenew.Enabled = true;
            lbShowLicenseHistory.Enabled = true;
        }


        public frmRenewLicenseApp()
        {
            InitializeComponent();
        }

        void SetDefaultValues()
        {
            DateTime AppDate = DateTime.Now;
            DateTime IssueDate = DateTime.Now;

            lbAppDate.Text = AppDate.ToString("yyyy/MM/dd");
            lbIssueDate.Text = IssueDate.ToString("yyyy/MM/dd");
            lbAppFees.Text = "7";

            lbCreatedBy.Text = ClsGlobal.CurrentUser.FullName;
        }
        private void frmRenewLicenseApp_Load(object sender, EventArgs e)
        {
            usFindDriverLicenseInfo1.OnSearchClicked += SearchMethod;

            SetDefaultValues();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int personID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID
                (Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            int RenewAppID = -1;
            int RenewlicenseID = -1;

            DialogResult result = MessageBox.Show("Are you sure you want to renew this license?", "Confirm Renewal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            int PersonID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID(Convert.ToString(usFindDriverLicenseInfo1.DriverID));

            if (ClsLicenesBusinessLayer.RenewLicense
                (ref RenewAppID, ref RenewlicenseID,
                Convert.ToInt32(lbOldLicenseID.Text),
                Convert.ToDateTime(lbExpirationDate.Text), lbTotalFees.Text, PersonID))
            {
                lbRenewAppID.Text = RenewAppID.ToString();
                lbRenwedLicenseID.Text = RenewlicenseID.ToString();

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show($"License renewed successfully!");
                    btnRenew.Enabled = false;
                    lbShowNewLicenseInfo.Enabled = true;
                    _AppID = Convert.ToString(RenewAppID);
                }
            }
        }

        private void usFindDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void lbShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_AppID != "0")
            {
                frmShowLicense frm = new frmShowLicense(_AppID);
                frm.ShowDialog();
                return;
            }


            string __AppID = Convert.ToString(ClsLicenesBusinessLayer.GetAppIDByLicenseID(_licenseID));

            frmShowLicense frmShowLicense = new frmShowLicense(__AppID);
            frmShowLicense.ShowDialog();
        }
    }
}
