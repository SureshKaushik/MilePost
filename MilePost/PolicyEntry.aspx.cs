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
using System.Collections.Generic;
using MilePost.Web.BusinessLogic;
using System.Text;

namespace MilePost
{
    public partial class PolicyEntry : System.Web.UI.Page
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
            }
            
        }
        /// <summary>
        /// This method is usefult to Load the grid.
        /// </summary>
        protected void LoadGrid()
        {
            DataSet ds = null;
            UserInfoDetailsBusinessEntity userInfo = (UserInfoDetailsBusinessEntity)Session["UserInfo"];
            MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
            try
            {
                ds = milePostBuzObj.GetPolicyDetails(userInfo);
                Cache[CommonConstants.PolicyUnapproved] = ds;
                if (ds.Tables[0].Rows.Count > CommonConstants.StatusZero)
                {
                    grdView1.Visible = CommonConstants.True;
                    grdView1.DataSource = ds;
                    grdView1.DataBind();
                    txtUserName.Text = userInfo.UserName;
                    txtMailId.Text = userInfo.EmailId;
                    lblNoRecords.Visible = CommonConstants.False;
                }
                else
                {
                    txtUserName.Text = userInfo.UserName;
                    txtMailId.Text = userInfo.EmailId;
                    grdView1.Visible = CommonConstants.False;
                    lblNoRecords.Visible = CommonConstants.True;
                } 
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.PolicyEntry);
                Response.Redirect(CommonConstants.Error);
            }
            finally
            {
                //ds.Dispose();
            }
                      
        }
       /// <summary>
        /// Adding new policy detials into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PolicyDetailsBusinessEntity policyDetails = new PolicyDetailsBusinessEntity();
            MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
            UserInfoDetailsBusinessEntity userInfo = (UserInfoDetailsBusinessEntity)Session[CommonConstants.UserInfo];
            int addStatus = CommonConstants.StatusZero;
            bool exist = false;
            int status = 0;
            bool sendMailStatus = false;
            DataSet dsPolicyUnapprove = (DataSet)Cache[CommonConstants .PolicyUnapproved]; 
            DataSet dsAllPolicyNo = milePostBuzObj.GetAllPolicyNumber(); 
            try
            {
                policyDetails.PolicyNo = txtPolicyNo.Text.Trim();
                policyDetails.UserId = userInfo.UserId;
                policyDetails.UserName = userInfo.UserName;
                policyDetails.RequestDate = txtRequestDate.Text.Trim();
                policyDetails.PhoneNumber = userInfo.PhoneNumber.Trim();
                policyDetails.BusinessReason = txtBusinessReason.Text.Trim();
                policyDetails.RootSegmentCount = Int16.Parse(txtRootSegCount.Text.Trim());
                policyDetails.FaceSegmentCount = Int16.Parse(txtFaceSegCount.Text.Trim());
                policyDetails.MachineSegmentCount = Int16.Parse(txtMachineSegCount.Text.Trim());

                if (Session[CommonConstants.SelectedRowIndex] != null)
                {                   
                    status = milePostBuzObj.UpdatePolicyDetails(policyDetails);                   
                    
                    if (status == CommonConstants.StatusOne)
                    {
                        Session.Remove(CommonConstants.SelectedRowIndex);
                        exist = true;
                        ScriptManager.RegisterStartupScript(this, GetType(),CommonConstants.ShowAlert, CommonConstants.SuccessUpdate, true);
                        txtPolicyNo.Enabled = CommonConstants.True;
                        txtPolicyNo.Text = string.Empty;
                        txtRequestDate.Text = string.Empty;
                        txtRootSegCount.Text = string.Empty;
                        txtBusinessReason.Text = string.Empty;
                        txtMachineSegCount.Text = string.Empty;
                        txtFaceSegCount.Text = string.Empty;
                    }
                    LoadGrid();
                }
                else
                {                   
                    for (int i = 0; i < dsPolicyUnapprove.Tables[0].Rows.Count; i++)
                    {
                        if (txtPolicyNo.Text.ToUpper() == Convert.ToString(dsPolicyUnapprove.Tables[0].Rows[i][2]))
                        {
                            status = milePostBuzObj.UpdatePolicyDetails(policyDetails);
                            if (status == CommonConstants.StatusOne)
                            {
                                LoadGrid();
                                exist = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.SuccessUpdate, true);
                                txtPolicyNo.Text = string.Empty;
                                txtRequestDate.Text = string.Empty;
                                txtRootSegCount.Text = string.Empty;
                                txtBusinessReason.Text = string.Empty;
                                txtMachineSegCount.Text = string.Empty;
                                txtFaceSegCount.Text = string.Empty;
                            }
                            break;
                        }
                    }
                    
                    if (dsAllPolicyNo.Tables[0].Rows.Count > CommonConstants.StatusOne)
                    {
                        for (int i = 0; i < dsAllPolicyNo.Tables[0].Rows.Count; i++)
                        {
                            if (txtPolicyNo.Text.ToUpper() == Convert.ToString(dsAllPolicyNo.Tables[0].Rows[i][0]))
                            {
                                if (Convert.ToChar(dsAllPolicyNo.Tables[0].Rows[i][7]) == CommonConstants.ApproveIndN || Convert.ToChar(dsAllPolicyNo.Tables[0].Rows[i][7]) == CommonConstants.ApproveIndn)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.AddFailure, true);
                                    exist = true;
                                    break;
                                }
                                else
                                {
                                    if (Convert.ToChar(dsAllPolicyNo.Tables[0].Rows[i][7]) == CommonConstants.ApproveIndY || Convert.ToChar(dsAllPolicyNo.Tables[0].Rows[i][7]) == CommonConstants.ApproveIndy)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.AlreadyApproved, true);
                                        if (lblNoRecords.Visible)
                                        {
                                            lblNoRecords.Visible = CommonConstants.False;
                                        }
                                        exist = true;
                                        break;
                                    }                                     
                                }
                            }
                           
                        } 
                    }
                    if(!exist)
                    {
                        addStatus = milePostBuzObj.AddPolicyDetails(policyDetails);
                        if (addStatus == CommonConstants.StatusOne)
                        {
                            policyDetails.MailId = userInfo.EmailId;
                           // sendMailStatus = milePostBuzObj.SendEmail(policyDetails);
                            if (sendMailStatus)
                            {
                                LoadGrid();
                                //ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.MailSuccess, true);                               
                            }
                            ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.SuccessAdded, true);
                            txtPolicyNo.Text = string.Empty;
                            txtRequestDate.Text = string.Empty;
                            txtRootSegCount.Text = string.Empty;
                            txtBusinessReason.Text = string.Empty;
                            txtMachineSegCount.Text = string.Empty;
                            txtFaceSegCount.Text = string.Empty;
                        }
                        LoadGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleLogging.AddtoLogFile(ex.ToString(), CommonConstants.PolicyEntry);
                Response.Redirect(CommonConstants.Error);
            }
            finally
            {
                Session.Remove(CommonConstants.SelectedRowIndex);
            }                

        }
        /// <summary>
        /// Reset the all the controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (!txtPolicyNo.Enabled)
            {
                txtPolicyNo.Enabled = CommonConstants.True;
            }
            txtPolicyNo.Text = string.Empty;
            txtRequestDate.Text = string.Empty;
            txtRootSegCount.Text = string.Empty;
            txtBusinessReason.Text = string.Empty;
            txtMachineSegCount.Text = string.Empty;
            txtFaceSegCount.Text = string.Empty;
        }
        /// <summary>
        /// Fatching row details into Textboxes on Edit button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals(CommonConstants.EditRow))
            {                
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = grdView1.Rows[index];
                    Session[CommonConstants.SelectedRowIndex] = row.RowIndex;
                    txtPolicyNo.Text = row.Cells[0].Text;
                    txtPolicyNo.Enabled = CommonConstants.False;
                    txtUserName.Text = row.Cells[2].Text;
                    txtRequestDate.Text = row.Cells[3].Text;
                    txtMailId.Text = row.Cells[4].Text;
                    TextBox tbox = (TextBox)row.FindControl("GrdTxtBusinessReason");
                    txtBusinessReason.Text = tbox.Text.ToString();                
                    txtRootSegCount.Text = row.Cells[6].Text;
                    txtMachineSegCount.Text = row.Cells[7].Text;
                    txtFaceSegCount.Text = row.Cells[8].Text;                     
                
            }
            if (e.CommandName.Equals(CommonConstants.DeleteRow))
            {
                int status;                
                int index = Convert.ToInt32(e.CommandArgument);
                PolicyDetailsBusinessEntity policyDetails = new PolicyDetailsBusinessEntity();
                MilePostBuzLogic milePostBuzObj = new MilePostBuzLogic();
                UserInfoDetailsBusinessEntity userInfo = (UserInfoDetailsBusinessEntity)Session[CommonConstants.UserInfo];
                try
                {
                    GridViewRow row = grdView1.Rows[index];
                    policyDetails.PolicyNo = row.Cells[0].Text;
                    policyDetails.UserId = userInfo.UserId;
                    status = milePostBuzObj.DeletePolicyDetails(policyDetails);
                    if (status == CommonConstants.StatusOne)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), CommonConstants.ShowAlert, CommonConstants.SuccessDeleted, true);
                        LoadGrid();
                    }
                }
                catch (Exception ex)
                {
                    HandleLogging.AddtoLogFile(ex.Message, CommonConstants.PolicyEntry);
                    Response.Redirect(CommonConstants.Error);
                }
                finally
                {
                    if (!txtPolicyNo.Enabled)
                    {
                        txtPolicyNo.Enabled = CommonConstants.True;
                    }
                    txtPolicyNo.Text = string.Empty;
                    txtRequestDate.Text = string.Empty;
                    txtRootSegCount.Text = string.Empty;
                    txtBusinessReason.Text = string.Empty;
                    txtMachineSegCount.Text = string.Empty;
                    txtFaceSegCount.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Page indexing change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                grdView1.PageIndex = e.NewPageIndex;
                LoadGrid();      
            }
        }

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
                            //Session was timed Out and New Session was started
                            Response.Redirect(CommonConstants.Login);
                        }
                    }
                }
            }
        }   
   
    }
}
