using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace SOP_Client
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitializeUsersDataGridView();
            RefreshUserData();
            IsLoggedIn();
        }

        RestClient restClient = new RestClient("http://localhost/sop/server/users.php");
        RestClient loginClient = new RestClient("http://localhost/sop/server/login.php");

        bool IsLoggedIn()
        {
            if(CurrentUser.username == null)
            {
                ManageButtons(false);
                new UserHandler(loginClient, this, RequestType.LOGIN).Show();
                return false;
            }
            return true;
        }
        void InitializeUsersDataGridView()
        {
            usersData.Columns.Add("id", "ID");
            usersData.Columns.Add("username", "Username");
            usersData.Columns.Add("password", "Password");
        }
        public void RefreshUserData()
        {
            RestRequest request = new RestRequest();
            try
            {
                RestResponse response = restClient.Get(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.StatusDescription);
                }
                else
                {
                    Response res = restClient.Deserialize<Response>(response).Data;
                    usersData.Rows.Clear();
                    foreach (var user in res.Users)
                    {
                        usersData.Rows.Add(user.ID, user.username, user.password);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            RefreshUserData();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (IsLoggedIn())
            {
                ManageButtons(false);
                new UserHandler(restClient, this, RequestType.POST).Show();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (IsLoggedIn())
            {
                ManageButtons(false);
                new UserHandler(restClient, this, RequestType.PUT).Show();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (IsLoggedIn())
            {
                ManageButtons(false);
                new UserHandler(restClient, this, RequestType.DELETE).Show();
            }
        }

        public void ManageButtons(bool enable)
        {
            getButton.Enabled = enable;
            addButton.Enabled = enable;
            updateButton.Enabled = enable;
            deleteButton.Enabled = enable;
        }
    }
}
