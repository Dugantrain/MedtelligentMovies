using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Extensions;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Models;
namespace MedtelligentMovies.Web.Admin
{
    
    public partial class ManageUsers : System.Web.UI.Page
    {
        [Dependency]
        public IUserService UserService { get; set; }

        private IEnumerable<User> _users; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _users = UserService.GetUsers(0, Int32.MaxValue);
                ViewState["Users"] = _users;
            }
            else
            {
                _users = (IEnumerable<User>) ViewState["Users"];
            }
            gvUsers.DataSource = _users;
            gvUsers.DataBind();
        }

        protected void UsernameValidation(object source, ServerValidateEventArgs arguments)
        {

            var userName = arguments.Value;
            var existingUser = UserService.GetUserByUsername(txtUserName.Text);
            if (existingUser != null)
            {
                arguments.IsValid = false;
            }

        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (
               String.IsNullOrEmpty(txtUserName.Text) ||
               String.IsNullOrEmpty(txtPassword.Text) ||
               String.IsNullOrEmpty(txtEmail.Text) ||
               String.IsNullOrEmpty(txtFirstName.Text) ||
               String.IsNullOrEmpty(txtLastName.Text)) { return; }

            var user = new User
            {
                IsAdministrator = true,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                UserName = txtUserName.Text,
                EncryptedPassword = txtPassword.Text.Encrypt(),
                Email = txtEmail.Text
            };
            UserService.Create(user);
            _users = UserService.GetUsers(0,Int32.MaxValue);
            ViewState["Users"] = _users;
            gvUsers.DataSource = _users;
            gvUsers.DataBind();

            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtLastName.Text = String.Empty;
        }
    }
}