using ClsUser_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ClsCurrentUser;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class FrmLoginScreen : Form
    {
        public FrmLoginScreen()
        {
            InitializeComponent();
        }

        private void FrmLoginScreen_Load(object sender, EventArgs e)
        {
            var creds = SecureCredentialFile.LoadCredentials();
            if (creds.username != null)
            {
                txtUserName.Text = creds.username;
                txtPassword.Text = creds.password;
                chRememberMe.Checked = true;
            }
        }

        private void _ShowLoginFailureMessage(ClsUser User)
        {
            if (User == null)
            {
                MessageBox.Show("Invalid username or password. Please try again.");
                return;
            }

            if (User.IsActive == 0)
            {
                MessageBox.Show("The account is not active , please contact your Admin");
                return;
            }
        }
     
        private void btnLogin_Click(object sender, EventArgs e)
        {
            ClsUser User = new ClsUser(txtUserName.Text, txtPassword.Text);

            User = User.CheckLoginUser();

            if (User != null) 
            {

                if (chRememberMe.Checked)
                    SecureCredentialFile.SaveCredentials(txtUserName.Text, txtPassword.Text);
                else
                    SecureCredentialFile.DeleteCredentials();

                if (User.IsActive == 0)
                {
                    MessageBox.Show("The account is not active , please contact your Admin");
                    return;
                }

                ClsGlobal.CurrentUser = User;
                frmMainScreen MainScreen = new frmMainScreen();
                MainScreen.Show();
                
                return;
            }

            _ShowLoginFailureMessage(User);
        }
    }
}
