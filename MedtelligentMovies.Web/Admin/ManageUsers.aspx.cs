using System;
using System.Collections.Generic;
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

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (
               String.IsNullOrEmpty(UserNameTextBox.Text) ||
               String.IsNullOrEmpty(EmailTextBox.Text) ||
               String.IsNullOrEmpty(FirstNameTextBox.Text) ||
               String.IsNullOrEmpty(LastNameTextBox.Text)) { return; }

            var user = new User
            {
                IsAdministrator = true,
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                UserName = UserNameTextBox.Text,
                Email = EmailTextBox.Text
            };
            UserService.Create(user);
            _users = UserService.GetUsers(0,Int32.MaxValue);
            ViewState["Users"] = _users;
            gvUsers.DataSource = _users;
            gvUsers.DataBind();

            UserNameTextBox.Text = String.Empty;
            EmailTextBox.Text = String.Empty;
            FirstNameTextBox.Text = String.Empty;
            LastNameTextBox.Text = String.Empty;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            UserNameTextBox.Text = String.Empty;
            EmailTextBox.Text = String.Empty;
            FirstNameTextBox.Text = String.Empty;
            LastNameTextBox.Text = String.Empty;
        }
    }
}