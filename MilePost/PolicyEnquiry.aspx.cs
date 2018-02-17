using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MilePost.Web.BusinessEntity;
using MilePost.Web.BusinessLogic;

namespace MilePost
{
    public partial class PolicyEnquiry : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            SessionExpiry();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    if (!GrdView2.Visible)
            //    {
            //        GrdView2.Visible = CommonConstants.False;
            //    }
            //}
            if (lblNoRecords.Visible)
            {
                lblNoRecords.Visible = CommonConstants.False;
            }
        }

        /// <summary>
        /// This method will search all the policies depnding upon the search criteria.
        /// <param name="object">sender</param>
        /// <param name="EventArgs">e</param>        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        /// <summary>
        /// Page indexing change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                GrdView2.PageIndex = e.NewPageIndex;
                LoadGrid();
            }
        }
        /// <summary>
        /// This method is usefult to Load the grid.
        /// </summary>
        protected void LoadGrid()
        {
            DataSet ds = null;
            PolicyDetailsBusinessEntity policyDetails = new PolicyDetailsBusinessEntity();
            UserInfoDetailsBusinessEntity userInfo = (UserInfoDetailsBusinessEntity)Session[CommonConstants.UserInfo];
            MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
            try
            {
                policyDetails.UserId = userInfo.UserId;
                policyDetails.PolicyNo = txtPolicyNo.Text.ToUpper();
                policyDetails.StartDate = txtRequestDate.Text;
                policyDetails.EndDate = txtEndDate.Text;

                ds = milePostBuzObj.GetPolicyEnquiry(policyDetails);
                if (ds.Tables[0].Rows.Count > CommonConstants.StatusZero)
                {
                    GrdView2.Visible = CommonConstants.True;
                    GrdView2.DataSource = ds;
                    GrdView2.DataBind();
                }
                else
                {
                    if (GrdView2.Visible)
                    {
                        GrdView2.Visible = CommonConstants.False;
                    }
                    lblNoRecords.Visible = CommonConstants.True;
                }
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.PolicyEnquiry);
                Response.Redirect(CommonConstants.Error);
            }
            finally
            {
                ds.Dispose();
            }

        }
        /// <summary>
        /// This method invokes when teh user click on the logout button.
        /// <param name="object">sender</param>
        /// <param name="EventArgs">e</param>        
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Remove(CommonConstants.UserName);
            Response.Redirect(CommonConstants.Logout);
        }
        public void SessionExpiry()
        {
            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    HttpCookie newSessionIdCookie = Request.Cookies[CommonConstants.ASPSessionId];
                    if (newSessionIdCookie != null)
                    {
                        string newSessionIdCookieValue = newSessionIdCookie.Value;
                        if (newSessionIdCookieValue != string.Empty)
                        {
                            // Session was timed Out and New Session was started
                            Response.Redirect(CommonConstants.Login);
                        }
                    }
                }
            }
        }
    }
}
