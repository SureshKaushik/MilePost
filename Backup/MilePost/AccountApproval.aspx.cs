using System;
using System.Collections;
using System.Collections.Generic;
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
using MilePost.Web.BusinessLogic;
using MilePost.Web.BusinessEntity;
using System.Web.UI.MobileControls;

namespace MilePost
{
    public partial class AccountApproval : System.Web.UI.Page
    {
       protected void Page_Init(object sender, EventArgs e)
        {
            SessionExpiry();
        }
       protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadGrid();
                Session.Remove(CommonConstants.CheckedItems);
                Session.Remove(CommonConstants.CheckedRejected);
            }
         }
        /// <summary>
        /// Load the grid.
        /// </summary>
        protected void LoadGrid()
        {
            DataSet dsMyDataSet = null;
            MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
            DataTable dt = new DataTable();
            try
            {
                dsMyDataSet = milePostBuzObj.GetAllApprovalDetails();
                if (dsMyDataSet.Tables[0].Rows.Count > CommonConstants.StatusZero)
                {
                    GridView1.DataSource = dsMyDataSet;
                    GridView1.Visible = CommonConstants.True;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.Visible = CommonConstants.False;
                    lblNoRecords.Visible = CommonConstants.True;
                    lblBusinessReason.Visible = CommonConstants.False;
                    txtReasonReject.Visible = CommonConstants.False;
                    btnSubmit.Visible = CommonConstants.False;
                    btnReset.Visible = CommonConstants.False;
                }
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.AccountApproval);
                Response.Redirect(CommonConstants.Error);
            }
            finally
            {
                //dsMyDataSet.Dispose();
            }
                        
        }        
       
        /// <summary>
        /// Page indexing change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SaveCheckedValues();
                GridView1.PageIndex = e.NewPageIndex;
                LoadGrid();
                PopulateCheckedValues();
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.AccountApproval);
                Response.Redirect(CommonConstants.Error);
            }              
        }
        /// <summary>
        /// This method is used to save the checkedstate of values
        /// </summary>
        private void SaveCheckedValues()
        {
            ArrayList rowIndex = new ArrayList();
            ArrayList rowIndexReject = new ArrayList();
            Boolean hdnVal = CommonConstants.False;
            try
            {
                string policyNo = string.Empty;
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    policyNo = Convert.ToString(GridView1.DataKeys[gvrow.RowIndex].Value);
                    bool resultApprove = ((RadioButton)gvrow.FindControl(CommonConstants.RbApprove)).Checked;
                    bool resultReject = ((RadioButton)gvrow.FindControl(CommonConstants.RbReject)).Checked;
                    // Check in the Session for Approve
                    if (Session[CommonConstants.CheckedItems] != null)
                        rowIndex = (ArrayList)Session[CommonConstants.CheckedItems];
                    if (resultApprove)
                    {
                        if (!rowIndex.Contains(policyNo))
                            rowIndex.Add(policyNo);
                    }
                    else
                        rowIndex.Remove(policyNo);
                    // Check in the Session for Reject
                    if (Session[CommonConstants.CheckedRejected] != null)
                        rowIndexReject = (ArrayList)Session[CommonConstants.CheckedRejected];
                    if (resultReject)
                    {
                        if (!rowIndexReject.Contains(policyNo))
                            rowIndexReject.Add(policyNo);
                        hdnVal = CommonConstants.True;
                    }
                    else
                        rowIndexReject.Remove(policyNo);
                }
                if (rowIndex != null && rowIndex.Count > CommonConstants.StatusZero)
                    Session[CommonConstants.CheckedItems] = rowIndex;

                if (rowIndexReject != null && rowIndexReject.Count > CommonConstants.StatusZero)
                    Session[CommonConstants.CheckedRejected] = rowIndexReject;
                if (hdnVal)
                {
                    this.hdnReject.Value = CommonConstants.Reject;
                }
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.AccountApproval);
                Response.Redirect(CommonConstants.Error);
            }                      
        }
        
        /// <summary>
        /// This method is used to populate the saved checkbox values
        /// </summary>
        private void PopulateCheckedValues()
        {
            ArrayList rowIndex = null;
            ArrayList rowIndexReject = null;
            try
            {
                if (Session[CommonConstants.CheckedItems] != null)
                {
                    rowIndex = (ArrayList)Session[CommonConstants.CheckedItems];
                }
                if (rowIndex != null && rowIndex.Count > CommonConstants.StatusZero)
                {
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        string policyNo = Convert.ToString(GridView1.DataKeys[gvrow.RowIndex].Value);
                        if (rowIndex.Contains(policyNo))
                        {
                            RadioButton rbApprove = (RadioButton)gvrow.FindControl(CommonConstants.RbApprove);
                            rbApprove.Checked = CommonConstants.True;
                        }
                    }
                }
                if (Session[CommonConstants.CheckedRejected] != null)
                {
                    if (Session[CommonConstants.CheckedRejected] != null)
                        rowIndexReject = (ArrayList)Session[CommonConstants.CheckedRejected];
                }
                if (rowIndexReject != null && rowIndexReject.Count > CommonConstants.StatusZero)
                {
                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        string policyNo = Convert.ToString(GridView1.DataKeys[gvrow.RowIndex].Value);
                        if (rowIndexReject.Contains(policyNo))
                        {
                            RadioButton rbApprove = (RadioButton)gvrow.FindControl(CommonConstants.RbReject);
                            rbApprove.Checked = CommonConstants.True;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.AccountApproval);
                Response.Redirect(CommonConstants.Error);
            }           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ArrayList rowIndex = null;
            ArrayList rowIndexRejected = null;
            int status;
            //bool sendMailStatus = false;
            bool sendMailStatus = true;
            SaveCheckedValues();
            UserInfoDetailsBusinessEntity userInfo = null;
            PolicyDetailsBusinessEntity policyDetails = null;
            MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
            DataSet ds = milePostBuzObj.GetAllPolicyNumber();
            try
            {
                if (Session[CommonConstants.CheckedItems] != null || Session[CommonConstants.CheckedRejected] != null)
                {
                    rowIndex = (ArrayList)Session[CommonConstants.CheckedItems];
                    rowIndexRejected = (ArrayList)Session[CommonConstants.CheckedRejected];
                    if (Session[CommonConstants.UserInfo] != null)
                    {
                        userInfo = (UserInfoDetailsBusinessEntity)Session[CommonConstants.UserInfo];
                    }
                    if (rowIndex != null)
                    {
                        if (rowIndex.Count > 1)
                        {
                            if (rowIndexRejected != null)
                            {
                                if (rowIndexRejected.Count > 1)
                                {
                                    lblNotSelected.Visible = CommonConstants.True;
                                    lblNotSelected.Text = CommonConstants.MsgSelectAppRej;
                                }
                            }
                            else
                            {
                                lblNotSelected.Visible = CommonConstants.True;
                                lblNotSelected.Text = CommonConstants.MsgSelectApprove;
                            }
                        }
                        else
                        {
                            if (rowIndexRejected != null)
                            {
                                if (rowIndexRejected.Count > 1 || rowIndexRejected.Count ==1)
                                {
                                    lblNotSelected.Visible = CommonConstants.True;
                                    lblNotSelected.Text = CommonConstants.MsgSelectAppRej;
                                }
                            }
                            else
                            {
                                policyDetails = new PolicyDetailsBusinessEntity();
                                policyDetails.UserId = userInfo.UserId;
                                policyDetails.ApprovalId = userInfo.UserId;
                                policyDetails.PolicyNo = rowIndex[0].ToString();                               

                                status = milePostBuzObj.UpdateApprovalDetails(policyDetails);
                                if (status == CommonConstants.StatusOne)
                                {
                                    LoadGrid();
                                    ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.SuccessApprove, true);

                                    if (ds.Tables[0].Rows.Count > CommonConstants.StatusZero)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            policyDetails = new PolicyDetailsBusinessEntity();
                                            if (rowIndex[0].ToString() == Convert.ToString(ds.Tables[0].Rows[i][0]))
                                            {
                                                policyDetails.PolicyNo = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                                policyDetails.BusinessReason = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                                policyDetails.MailId = Convert.ToString(ds.Tables[0].Rows[i][6]);
                                                break;
                                            }
                                        }
                                    }
                                    //sendMailStatus = milePostBuzObj.SendApproveRejectMail(policyDetails, userInfo, CommonConstants.ApproveInd);
                                    if (sendMailStatus)
                                    {
                                        LoadGrid();
                                        if (lblNotSelected.Visible)
                                        {
                                            lblNotSelected.Visible = CommonConstants.False;
                                        }
                                    }
                                  
                                }
                                if (lblNotSelected.Visible)
                                {
                                    lblNotSelected.Visible = CommonConstants.False;
                                }
                            }                            
                        }
                    }
                    else
                    {
                        if (rowIndexRejected != null)
                        {
                            if (rowIndexRejected.Count > 1)
                            {
                                lblNotSelected.Visible = CommonConstants.True;
                                lblNotSelected.Text = CommonConstants.MsgSelectReject;
                            }
                            else
                            {
                                if (txtReasonReject.Text == string.Empty)
                                {
                                    lblNotSelected.Visible = CommonConstants.True;
                                    lblNotSelected.Text = CommonConstants.MsgProvideReason;
                                }
                                else
                                {
                                    if (ds.Tables[0].Rows.Count > CommonConstants.StatusZero)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            policyDetails = new PolicyDetailsBusinessEntity();
                                            if (rowIndexRejected[0].ToString() == Convert.ToString(ds.Tables[0].Rows[i][0]))
                                            {
                                                policyDetails.PolicyNo = rowIndexRejected[0].ToString();
                                                policyDetails.BusinessReason = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                                policyDetails.MailId = Convert.ToString(ds.Tables[0].Rows[i][6]);
                                                policyDetails.RejectedReason = txtReasonReject.Text;                                               
                                                break;
                                            }                                            
                                        }
                                    }
                                    //sendMailStatus = milePostBuzObj.SendApproveRejectMail(policyDetails, userInfo, CommonConstants.RejectInd);
                                    if (sendMailStatus)
                                    {
                                        LoadGrid();
                                        ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.SucccessReject, true);
                                        txtReasonReject.Text = string.Empty;
                                        if (lblNotSelected.Visible)
                                        {
                                            lblNotSelected.Visible = CommonConstants.False;
                                        }
                                    }
                                }
                            }
                        }                        
                    }
                }
                else
                {
                    lblNotSelected.Visible = CommonConstants.True;
                    lblNotSelected.Text = CommonConstants.MsgSelectAppRej;
                }               
            }                 
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.AccountApproval);
                Response.Redirect(CommonConstants.Error);
            }
            finally
            {
                Session.Remove(CommonConstants.CheckedItems);
                Session.Remove(CommonConstants.CheckedRejected);               
            }
        }
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session[CommonConstants.UserName] = null;
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
                            // This means Session was timed Out and New Session was started
                            Response.Redirect(CommonConstants.Login);
                        }
                    }
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.LoadGrid();
                if (lblNotSelected.Visible)
                {
                    lblNotSelected.Visible = false;
                }
                txtReasonReject.Text = string.Empty;
                Session.Remove(CommonConstants.CheckedItems);
                Session.Remove(CommonConstants.CheckedRejected);
            }
        }

      }    
}
