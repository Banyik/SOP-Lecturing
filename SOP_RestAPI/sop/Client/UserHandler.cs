using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SOP_Client
{
    public partial class UserHandler : Form
    {
        public UserHandler(RestClient restClient, Main mainForm, RequestType requestType)
        {
            InitializeComponent();
            this.restClient = restClient;
            this.mainForm = mainForm;
            this.requestType = requestType;
            SetFormElements();
        }

        void SetFormElements()
        {
            switch (requestType)
            {
                case RequestType.POST:
                    idLabel.Visible = false;
                    idBox.Visible = false;
                    userHandlerButton.Text = "ADD";
                    break;
                case RequestType.PUT:
                    userHandlerButton.Text = "UPDATE";
                    break;
                case RequestType.DELETE:
                    userHandlerButton.Text = "DELETE";
                    passwordLabel.Visible = false;
                    passwordBox.Visible = false;
                    break;
                case RequestType.LOGIN:
                    userHandlerButton.Text = "LOGIN";
                    idLabel.Visible = false;
                    idBox.Visible = false;
                    break;
                default:
                    break;
            }
        }

        RestClient restClient;
        Main mainForm;
        RequestType requestType;
        void AddUser()
        {
            RestRequest request = new RestRequest();
            request.AddParameter("username", usernameBox.Text);
            request.AddParameter("password", passwordBox.Text);
            try
            {
                RestResponse response = restClient.Post(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.StatusDescription);
                }
                else
                {
                    mainForm.RefreshUserData();
                    mainForm.ManageButtons(true);
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        void UpdateUser()
        {
            RestRequest request = new RestRequest();
            request.AddBody(new
            {
                id = idBox.Text,
                username = usernameBox.Text,
                password = passwordBox.Text,
            });
            try
            {
                RestResponse response = restClient.Put(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.StatusDescription);
                }
                else
                {
                    mainForm.RefreshUserData();
                    mainForm.ManageButtons(true);
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        void DeleteUser()
        {
            RestRequest request = new RestRequest();
            request.AddBody(new
            {
                id = idBox.Text,
                username = usernameBox.Text,
            });
            try
            {
                RestResponse response = restClient.Delete(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.StatusDescription);
                }
                else
                {
                    mainForm.RefreshUserData();
                    mainForm.ManageButtons(true);
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        void LoginUser()
        {
            RestRequest request = new RestRequest();
            request.AddParameter("username", usernameBox.Text);
            request.AddParameter("password", passwordBox.Text);
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
                    if(res.error == 0)
                    {
                        CurrentUser.username = usernameBox.Text;
                        CurrentUser.password = passwordBox.Text;
                        MessageBox.Show(res.message);
                        mainForm.RefreshUserData();
                        mainForm.ManageButtons(true);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(res.message);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void userHandlerButton_Click(object sender, EventArgs e)
        {
            switch (requestType)
            {
                case RequestType.POST:
                    AddUser();
                    break;
                case RequestType.PUT:
                    UpdateUser();
                    break;
                case RequestType.DELETE:
                    DeleteUser();
                    break;
                case RequestType.LOGIN:
                    LoginUser();
                    break;
                default:
                    break;
            }
        }
    }
}
