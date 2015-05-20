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
        [Dependency]
        public IUserContextService UserContextService { get; set; }

        private IEnumerable<User> _users;
        private bool _isUserNameValid;
        private bool _isEmailValid;
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

        protected void EmailUniqueValidation(object source, ServerValidateEventArgs arguments)
        {
            //When creating new user.
            if (String.IsNullOrEmpty(hdnId.Value))
            {
                var email = arguments.Value;
                var existingUser = UserService.GetUserByEmail(email);
                if (existingUser != null)
                {
                    arguments.IsValid = false;
                    return;
                }
            }
            _isEmailValid = true;
        }

        protected void UsernameUniqueValidation(object source, ServerValidateEventArgs arguments)
        {
            //When creating new user.
            if (String.IsNullOrEmpty(hdnId.Value))
            {
                var userName = arguments.Value;
                var existingUser = UserService.GetUserByUsername(userName);
                if (existingUser != null)
                {
                    arguments.IsValid = false;
                    return;
                }
            }
            _isUserNameValid = true;
        }

        protected void PopulateFieldsForUpdate(object sender, EventArgs e)
        {
            var lnkUpdate = (Button)sender;
            var userId = Convert.ToInt32(lnkUpdate.CommandArgument);
            var user = UserService.GetUserById(userId);
            hdnId.Value = user.Id.ToString();
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
            txtPassword.Text = user.EncryptedPassword.Decrypt();
            txtUserName.Text = user.UserName;
            InsertUserUpdatePanel.Update();
        }

        protected void DeleteUser(object sender, EventArgs e)
        {
            var lnkRemove = (Button)sender;
            var userId = Convert.ToInt32(lnkRemove.CommandArgument);
            UserService.DeleteUser(userId);
            _users = UserService.GetUsers(0, Int32.MaxValue);
            ViewState["Users"] = _users;
            gvUsers.DataSource = _users;
            gvUsers.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (
               String.IsNullOrEmpty(txtUserName.Text) ||
               String.IsNullOrEmpty(txtPassword.Text) ||
               String.IsNullOrEmpty(txtEmail.Text) ||
               String.IsNullOrEmpty(txtFirstName.Text) ||
               String.IsNullOrEmpty(txtLastName.Text) || 
                !_isUserNameValid ||
                !_isEmailValid) { return; }

            if (!String.IsNullOrEmpty(hdnId.Value))
            {
                //Update
                var user = UserService.GetUserById(Convert.ToInt32(hdnId.Value));
                if (user.FirstName != txtFirstName.Text) UserService.ChangeFirstName(user.Id, txtFirstName.Text);
                if (user.LastName != txtLastName.Text) UserService.ChangeLastName(user.Id, txtLastName.Text);
                if (user.UserName != txtUserName.Text) UserService.ChangeUserName(user.Id, txtUserName.Text);
                if (user.Email != txtEmail.Text) UserService.ChangeEmail(user.Id, txtEmail.Text);
                if (user.EncryptedPassword.Decrypt() != txtPassword.Text) UserService.ChangePassword(user.Id, txtPassword.Text);
            }
            else
            {
                //Create
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
            }

            
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

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var contextUserName = UserContextService.GetUserName();
            var contextUser = UserService.GetUserByUsername(contextUserName);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var deleteButton = (Button) e.Row.FindControl("btnDelete");
                var updateButton = (Button) e.Row.FindControl("btnUpdate");
                var user = (User) e.Row.DataItem;
                //Can't delete or update the original Admin User.
                if (user.Id == 1)
                {
                    updateButton.Visible = false;
                    deleteButton.Visible = false;
                }
                //Can't delete yourself.
                if (contextUser.Id == user.Id)
                {
                    deleteButton.Visible = false;
                }

                var clickPostback = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onmouseover",
                    "ChangeMouseOverRowColor('" + gvUsers.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedUserId.ClientID + "')");
                e.Row.Attributes.Add("onClick",
                    "ChangeSelectedRowColorOnClick('" + gvUsers.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedUserId.ClientID + "');" + clickPostback);
                e.Row.Attributes.Add("onmouseout",
                    "PreserveClickedRowStyleOnMouseOut('" + gvUsers.ClientID + "','" + hdnSelectedUserId.ClientID + "')");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnId.Value = String.Empty;
            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            InsertUserUpdatePanel.Update();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUsers.Rows)
            {
                if (row.RowIndex == gvUsers.SelectedIndex)
                {
                    var userId = gvUsers.DataKeys[row.RowIndex]["Id"];
                    var user = UserService.GetUserById(Convert.ToInt32(userId));
                    hdnId.Value = user.Id.ToString();
                    txtFirstName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                    txtEmail.Text = user.Email;
                    txtPassword.Text = user.EncryptedPassword.Decrypt();
                    txtUserName.Text = user.UserName;
                }
            }
        }
    }
}