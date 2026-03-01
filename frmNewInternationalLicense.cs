using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsDVLDControls;
using DriversBusinessLayer;
using LocalDrivingLicenseList;
using nClsInternationalLicenseBusinessLayer;
using frmShowInternationalLicenseInfo;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmNewInternationalLicense : Form
    {
        ClsInternationalLicense I_License = new ClsInternationalLicense();

        int _LocalAppID;
        int PersonID = 0;
        int DriverID = 0;
        public frmNewInternationalLicense()
        {
            InitializeComponent();

            usFindDriverLicenseInfo1.OnSearchClicked += usFindDriverLicenseInfo1_OnSearchClicked;
            usFindDriverLicenseInfo1.GetDriverID += usFindDriverLicenseInfo1_GetDriverID;

        }

        private void usFindDriverLicenseInfo1_OnSearchClicked(string Value, bool IsEnableTextBoxOnSearch = true)
        {
            usInternationalApp1.SetLocalLicense(Value);
            this._LocalAppID = Convert.ToInt16(Value);
            lbShowLicenseHistory.Enabled = true;


        }

        private void usFindDriverLicenseInfo1_GetDriverID(string Value)
        {
            DriverID = Convert.ToInt16(Value);
            PersonID = ClsInternationalBusinessLayer.GetPersonID_By_DriverID(Value);
            
            I_License.DriverID = DriverID;
            I_License.PersonID = PersonID;

        }

        private void usFindDriverLicenseInfo1_Load(object sender, EventArgs e)
        {
            usInternationalApp1.LoadInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(usFindDriverLicenseInfo1.StrLicenseID))
            {
                MessageBox.Show("Please fill in all required fields in the international license application.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!(usFindDriverLicenseInfo1.LocalClass == "Class 3 - Ordinary driving license\n"
                && usFindDriverLicenseInfo1.IsActive))
            {
                MessageBox.Show("A local Class 3 ordinary driving license is required to apply for an international license.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            };


            int InternationaLicenseID = ClsDriversBusinessLayer.IsDriverHasTheLicense(ref I_License.DriverID);

            if (InternationaLicenseID != 0 && InternationaLicenseID != -1)
            {
                MessageBox.Show($"Person already have an active international license with ID = {InternationaLicenseID}", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        bool Set_I_LicenseInfo()
        {           

            I_License.LocalAppID = this._LocalAppID;
            I_License.isActive = true;

            I_License.IssueDate = DateTime.Now.ToString("MM/dd/yyyy");
            I_License.ExpirationDate = DateTime.Now.AddYears(1).ToString("MM/dd/yyyy");

            return true;

        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            DialogResult result;

           if (!Set_I_LicenseInfo())
            {
                return;
            }


            result = MessageBox.Show(
                "Are you sure you want to issue the license?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (!ValidateInputs())
                return;


            if (result == DialogResult.Yes)
            {
                                   
                if (ClsInternationalBusinessLayer.AddInternationalLicense(ref I_License))
                {
                    MessageBox.Show($"International license issued successfully with ID = {I_License.I_LicenseID}",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);


                    usInternationalApp1.SetAppIdAndLicenseID(I_License.AppID, I_License.I_LicenseID);
                    lbShowLicenseInfo.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Failed to issue the international license ❌",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {

        }

        private void lbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();

        }

        private void lbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClsInternationalBusinessLayer.Get_International_License_Info(ref I_License);

            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(I_License);

            frm.ShowDialog();
        }
    }
}