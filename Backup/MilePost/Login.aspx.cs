using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MilePost.Web.BusinessEntity;
using MilePost.Web.BusinessLogic;

namespace MilePost
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserInfoDetailsBusinessEntity userInfo = new UserInfoDetailsBusinessEntity();
            MilePostBuzLogic milepostBuz = new MilePostBuzLogic();

            userInfo.UserName = txtUsername.Text;
            userInfo.Password = txtPassword.Text;

            userInfo = milepostBuz.CheckUserAuth(userInfo);

            Session["UserInfo"] = userInfo;
            Session["UserName"] = userInfo.UserName;
            Session["Role"] = userInfo.Role;

            if (userInfo.Status ==1)
            {
                Response.Redirect("Home.aspx");
               
            }
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}
