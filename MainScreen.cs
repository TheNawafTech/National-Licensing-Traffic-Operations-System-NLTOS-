using ClsCurrentUser;
using ClsUser_BusinessLayer;
using frmCurrentUser;
using frmPeople;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using frmApplicationTypes;
using frmManageTestTypes;
using frmNewLocalLicense;
using LocalDrivingLicenseList;
using clsDVLDControls;
using DVLD_Controls;
using DVLDBusinessLayer;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmMainScreen : Form
    {
        public frmMainScreen()
        {
            InitializeComponent();
        }

        public frmMainScreen(ClsUser user)
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmPeople.frmPeople();
            form.ShowDialog();

        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers frmUsers = new frmUsers();
            frmUsers.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCurrentUser.frmUserInfo frm = new frmCurrentUser.frmUserInfo(ClsGlobal.CurrentUser);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword.frmChangePassword frm = new frmChangePassword.frmChangePassword(ClsGlobal.CurrentUser);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypes.frmApplicationTypes frm = new frmApplicationTypes.frmApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestType frm = new frmManageTestType();
            frm.ShowDialog();
        }

        private void frmMainScreen_Load(object sender, EventArgs e)
        {

        }

        private void retakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApp frm = new frmLocalDrivingLicenseApp();
            frm.ShowDialog();
        }

        private void localLicenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrLicense frm = new frmNewLocalDrLicense();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApp frm = new frmLocalDrivingLicenseApp();
            frm.ShowDialog();
        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void InternationalDrivingLicenseApplicationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInternationalLicenses frm = new frmInternationalLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
        }

        private void rnewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApp frm = new frmRenewLicenseApp();
            frm.ShowDialog();
        }

        private void replacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamgedLicense frm = new frmReplacementForDamgedLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicense frm = new frmManageDetainedLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void relseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
           
            frm.ShowDialog();
        }
    }
}
