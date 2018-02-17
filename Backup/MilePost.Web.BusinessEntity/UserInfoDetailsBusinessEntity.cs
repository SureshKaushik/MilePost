using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilePost.Web.BusinessEntity
{
    public class UserInfoDetailsBusinessEntity
    {
        int userId;
        int roleId;
        string role = string.Empty;
        string userName = string.Empty;
        string password = string.Empty;
        string emailId = string.Empty;
        string phoneNumber = string.Empty;
        int status;

        #region UserInfo Details
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        #endregion
    }
}
