using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilePost.Web.BusinessEntity;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace MilePost.Web.DataAccess
{
    /// <summary>
    /// Decribes different operations like Add, Update and Delete the policy details.
    /// </summary>
    public class MilePostProvider : IMilePostProvider
    {
        string connStr = string.Empty;
        SqlConnection conn = null;
        SQLQuery sqlQry = new SQLQuery();
        string logo1 = ConfigurationManager.AppSettings[CommonConstants.Logo1];
        string fromEmailAddress = ConfigurationManager.AppSettings[CommonConstants.FromEmailAddress];
        string smtpServer = ConfigurationManager.AppSettings[CommonConstants.SmtpServer];
        string smtpPort = ConfigurationManager.AppSettings[CommonConstants.SmtpPort];

        public MilePostProvider()
        {
            //Get connection string from web.config file
            connStr = ConfigurationManager.ConnectionStrings[CommonConstants.ConnectionStr].ToString();
        }

        #region IMilePostProvider Members

        /// <summary>
        /// CheckUserAuth method is userful to check the authorization of the user.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfoDetailsBusinessEntity CheckUserAuth(UserInfoDetailsBusinessEntity userInfo)
        {
            userInfo.Status = CommonConstants.StatusZero;
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.GetUserInfoQuery(userInfo.UserName, userInfo.Password), conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userInfo.UserId = (int)reader[CommonConstants.UserId];
                        userInfo.RoleId = (int)reader[CommonConstants.RoleId];
                        //userInfo.PhoneNumber = Convert.ToString(reader[CommonConstants.PhoneNo]);
                        userInfo.EmailId = Convert.ToString(reader[CommonConstants.EmailId]);
                        userInfo.Status = CommonConstants.StatusOne;
                    }
                }
                reader.Close();
                // Retrieve the user Role details from T_ROLE_MASTER table
                SqlCommand cmdRole = new SqlCommand(sqlQry.GetUserRoleQuery(userInfo.RoleId), conn);
                cmdRole.CommandType = CommandType.Text;

                reader = cmdRole.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userInfo.Role = reader[CommonConstants.Role1].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return userInfo;
        }

        /// <summary>
        /// GetPolicyDetails method is useful to retrieve the policy details from the T_Policy_Details tabel.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetPolicyDetails(UserInfoDetailsBusinessEntity infoDetails)
        {
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.GetPolicyDetailsQuery(infoDetails.UserId), conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// GetAllPolicyNumber method is useful to retrieve all the Policy Numbers available in Table.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetAllPolicyNumber()
        {
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.GetAllPolicyDetailQuery(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// GetAllApprovedPolicyNo method is useful to retrieve all the policy no which has been approved.
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllApprovedPolicyNo()
        {
            DataSet ds = new DataSet();
            //try
            //{
            //    conn = new SqlConnection(connStr);
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(sqlQry.GetAllPolicyApproved(), conn);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    adapter.Fill(ds);
            //}
            //catch (SqlException ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    conn.Close();
            //}
            return ds;
        }

        /// <summary>
        /// GetPolicyEnquiry method is useful to retrieve the all policy details from database.
        /// </summary>
        /// <param name="getPolicyEnquiry"></param>
        /// <returns>DataSet</returns>
        public DataSet GetPolicyEnquiry(PolicyDetailsBusinessEntity getPolicyEnquiry)
        {
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                if (getPolicyEnquiry.PolicyNo != string.Empty && (getPolicyEnquiry.StartDate != string.Empty && getPolicyEnquiry.EndDate != string.Empty))
                {
                    SqlCommand cmd = new SqlCommand(sqlQry.GetPolicyEnquiryQuery(getPolicyEnquiry), conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
                else if (getPolicyEnquiry.StartDate != string.Empty && getPolicyEnquiry.EndDate != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand(sqlQry.GetPolicyEnquiryDateQuery(getPolicyEnquiry), conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(sqlQry.PolicyEnquiryPolicyNoQuery(getPolicyEnquiry), conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }

                for (Int32 i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows[i][6].ToString() != "")
                    {
                        //if (Convert.ToInt32(ds.Tables[0].Rows[i][6]) == 0)
                        //{
                        //    ds.Tables[0].Rows[i][6] = System.DBNull.Value;
                        //}
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// AddPolicyDetails method is useful to add the override request details into T_Policy_Details table.
        /// </summary>
        /// <param name="addPolicyDetails"></param>
        /// <returns></returns>
        public int AddPolicyDetails(PolicyDetailsBusinessEntity addPolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlQry.InsertPolicyDetailsQuery(addPolicyDetails), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();


                //SqlCommand cmd = new SqlCommand(CommonConstants.SProcInsertPolicyDetails, conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("UserId", addPolicyDetails.UserId);
                //cmd.Parameters.AddWithValue("PolicyNo", addPolicyDetails.PolicyNo);
                //cmd.Parameters.AddWithValue("RequestDate", addPolicyDetails.RequestDate);
                //cmd.Parameters.AddWithValue("BusinessReason", addPolicyDetails.BusinessReason);
                //cmd.Parameters.AddWithValue("RootSegmentCount", addPolicyDetails.RootSegmentCount);
                //cmd.Parameters.AddWithValue("FaceSegmentCount", addPolicyDetails.FaceSegmentCount);
                //cmd.Parameters.AddWithValue("MachineSegmentCount", addPolicyDetails.MachineSegmentCount); 
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.ExecuteNonQuery();
                status = CommonConstants.StatusOne;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        /// <summary>
        /// Invokes SendEmail method of the MilePostProvider class to send mail to Account Team
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool SendEmail(PolicyDetailsBusinessEntity policyDetails)
        {
            bool status = true;
            char[] sepArray = new char[] { ',', ';' };
            StringBuilder emailBody = null;
            StringBuilder toEmailAddress = null;
            string ccEmailAddress = string.Empty;
            string bccEmailAddress = string.Empty;
            string errorMessage = string.Empty;
            string username = policyDetails.UserName;
            string policyNo = policyDetails.PolicyNo;
            string phoneNo = policyDetails.PhoneNumber;
            string date = DateTime.Today.ToString(CommonConstants.DateFormat);
            int pmrRecordCnt = policyDetails.RootSegmentCount;
            int machineRecordCnt = policyDetails.MachineSegmentCount;
            int faceRecordCnt = policyDetails.FaceSegmentCount;
            string targetDate = policyDetails.OverwriteDate;
            string requestDate = policyDetails.RequestDate;
            MailMessage mailMessage = null;
            DataSet ds = null;
            List<String> lstEmailAddress = new List<String>();
            string emailAddress = string.Empty;

            try
            {
                ds = new DataSet();
                conn = new SqlConnection(connStr);
                toEmailAddress = new StringBuilder();
                emailBody = new StringBuilder();
                //Mail Message
                mailMessage = new MailMessage();
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.GetApproversEmailIdQuery(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > CommonConstants.StatusZero)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        emailAddress = ds.Tables[0].Rows[i][0].ToString();
                        lstEmailAddress.Add(emailAddress);
                    }
                }

                if (lstEmailAddress.Count > 0)
                {
                    for (int i = 0; i < lstEmailAddress.Count; i++)
                    {
                        toEmailAddress.Append(lstEmailAddress[i].ToString());
                        toEmailAddress.Append(";");
                    }
                }
                toEmailAddress.Remove(toEmailAddress.Length - 1, 1);

                string toEmailAddress1 = toEmailAddress.ToString();


                mailMessage.From = new MailAddress(fromEmailAddress);
                if (!string.IsNullOrEmpty(toEmailAddress1))
                {
                    string[] toArray = toEmailAddress1.Split(sepArray);
                    foreach (string tomailAddr in toArray)
                        mailMessage.To.Add(new MailAddress(tomailAddr));
                    toArray = null;
                }
                //ccEmailAddress = policyDetails.MailId;
                //mailMessage.CC.Add(new MailAddress(ccEmailAddress));

                if (!string.IsNullOrEmpty(bccEmailAddress))
                {
                    string[] bccArray = bccEmailAddress.Split(sepArray);
                    foreach (string bccmailAddr in bccArray)
                        mailMessage.Bcc.Add(new MailAddress(bccmailAddr));
                    bccArray = null;
                }
                //---------------------------------
                //mailMessage.To.Add(new MailAddress("Suresh.Kaushik@cognizant.com"));


                //----------------------------------------
                mailMessage.Subject = CommonConstants.MailSubjectPolicyEntry1 + policyNo + CommonConstants.MailSubjectPolicyEntry2 + requestDate;
                emailBody.AppendLine("<div id=\"pnlLoggedIn\" style=\"width:480px;\">");

                emailBody.Append(@"<html><body><div><img src=cid:myImageID> </div>");
                emailBody.AppendLine("<br>");
                emailBody.Append("<div>IS Memorandum</div>");
                emailBody.AppendLine("<br>");
                emailBody.AppendLine("<div style=\"text-align:right;\"><b>Date: </b>" + date + "</div>");
                emailBody.AppendLine("<div style=\"text-align:right;\"><b>Phone No:</b> " + phoneNo + "</div>");
                emailBody.AppendLine("<hr />");
                emailBody.AppendLine("<div>The following policy has been included for Special Overwrite:</div>");
                emailBody.Append("<br />");
                emailBody.Append("<div style=\"text-align:center;text-decoration:underline;font-weight:bold;\">");
                emailBody.Append(username);
                emailBody.Append("</div><div style=\"text-align:center;\">");
                emailBody.Append(policyNo + " 0 – Tests in IMSK </div> ");
                emailBody.AppendLine("<br />");
                emailBody.Append("<div style = \"text-decoration:underline;font-weight:bold;\">Reason for the overwrite</div>");
                emailBody.AppendLine("<div style=\"text-decoration:underline;font-weight:bold;\">");
                emailBody.AppendLine(policyNo);
                emailBody.Append("</div>");
                emailBody.AppendLine("<div>" + policyDetails.BusinessReason + "</div>");
                emailBody.AppendLine("<br />");
                emailBody.AppendLine("<div>The first overwrite job will delete " + pmrRecordCnt + " PMRs.</div>");
                emailBody.Append("<div> The second overwrite job will insert ");
                emailBody.Append(pmrRecordCnt + " PMRs; insert " + machineRecordCnt + " machine segments and " + faceRecordCnt + " Face segments.</div>");
                emailBody.AppendLine("<br />");
                //emailBody.AppendLine("<div style = \"float:left;\">Please submit the following jobs from ");
                //emailBody.Append("<b> SYSCMN.PROD.PRODJOBS </b>");
                //emailBody.Append("unless noted:</div>");

                //emailBody.AppendLine("<ul><li><pre style=\"font-family:Verdana;font-size:10px;\"><b>E0385WGP            stop the WIFY job</b></pre> </li>");
                //emailBody.AppendLine("<li><pre style=\"font-family:Verdana;font-size:10px;\"><b>E0384O1                Special Overwrite job       (in E0384T.GRPLIB.JCL)</b></pre></li>");
                //emailBody.AppendLine("<li><pre style=\"font-family:Verdana;font-size:10px;\"><b>E0384O2P              Special Overwrite job       (in E0384T.GRPLIB.JCL)</b></pre></li>");
                //emailBody.AppendLine("<li><pre style=\"font-family:Verdana;font-size:10px;\"><b>E0385WHP            WIFY prep job </b></pre></li>");
                //emailBody.AppendLine("<li><pre style=\"font-family:Verdana;font-size:10px;\"><b>E0385WFP            restart the WIFY job </b></pre> </li>");
                //emailBody.Append("</ul>");

                //emailBody.AppendLine("<br />");
                //emailBody.AppendLine("<div style = \"float:left;\">The parameters needed for the execution of job ");
                //emailBody.Append("<b> E0384O2P </b>");
                //emailBody.Append("reside in ");
                //emailBody.Append("<b>E0384T.TEMP.PARAMFL(CYV).</b>");
                //emailBody.Append("They should be set up prior to running the jobs:</div>");

                //emailBody.AppendLine("<div style = \"font-family:Verdana;font-size:10px; \">PARAMETER000 CYCPOVRT " + targetDate + " CYCLE DATE *** USE PREVIOUS DAY’S <b>PARMS</b></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">TYPRUN 001                                 NEVER	         <b>WHEN RUNNING SPECIAL</b></pre>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">FMCYCDT 002 CC <b>" + targetDate + "</b>            CYCLE DATE              <b>OVERWRITE ***</b></pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">FMEXADT 003 CC <b>" + targetDate + "</b>            HIGH ACTIVITY DATE (MON USE FRI HIGH)</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">DIVCYCDT 004 CC <b>" + targetDate + "</b>           (NEVER) INTRODUCTION CYCLE DATE</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">DIVEXADT 005 CC <b>" + targetDate + "</b>           (NEVER) INTRODUCTION EXACT DATE</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">HIACTYDT 006 CC <b>" + targetDate + "</b>           HIGH ACTIVITY DATE (MON USE FRI HIGH)</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">ENVIRON 007 D                        CONSTANT</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">CHKPTFREQ008 1000                  CONSTANT</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">EQUITYDB 013x Y                      UPDATE EQUITY INDEX DB Y OR N</pre></div>");
                //emailBody.AppendLine("<div><pre style=\"font-family:Verdana;font-size:10px;\">CYCLE-ID 014 T                       CYCLE IDENT; T=TRADITIONAL CYCLE</pre></div>");            


                emailBody.Append("</body>");
                emailBody.Append("</html>");

                mailMessage.Body = emailBody.ToString();
                mailMessage.IsBodyHtml = true;

                //create Alrternative HTML view
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, CommonConstants.MailType);
                ////Add Image
                //LinkedResource theEmailImage = new LinkedResource(logo1);
                //theEmailImage.ContentId = CommonConstants.ImageId;

                ////Add the Image to the Alternate view
                //htmlView.LinkedResources.Add(theEmailImage);

                //Add view to the Email Message
                ////mailMessage.AlternateViews.Add(htmlView);
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.Port = Int16.Parse(smtpPort);
                smtpClient.Send(mailMessage);

            }
            catch (System.Net.Mail.SmtpException exception)
            {
                errorMessage = exception.Message;
                status = false;
            }
            finally
            {
                if (mailMessage != null)
                {
                    mailMessage.Dispose();
                    mailMessage = null;
                }
                sepArray = null;
            }
            return status;
        }

        /// <summary>
        /// Invokes SendApproveRejectMail method of the MilePostProvider class to send trigger mail on policy approval or policy reject to Account Team.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool SendApproveRejectMail(PolicyDetailsBusinessEntity policyDetails, UserInfoDetailsBusinessEntity userInfoDetails, string mailType)
        {
            bool status = true;
            char[] sepArray = new char[] { ',', ';' };
            StringBuilder emailBody = null;

            string errorMessage = string.Empty;
            string policyNo = policyDetails.PolicyNo;
            string phoneNo = userInfoDetails.PhoneNumber;
            string date = DateTime.Today.ToString(CommonConstants.DateFormat);
            int pmrRecordCnt = policyDetails.RootSegmentCount;
            int machineRecordCnt = policyDetails.MachineSegmentCount;
            int faceRecordCnt = policyDetails.FaceSegmentCount;
            string businessReason = policyDetails.BusinessReason;
            string rejectedReason = policyDetails.RejectedReason;
            MailMessage mailMessage = null;
            string emailAddress = string.Empty;

            try
            {
                conn = new SqlConnection(connStr);
                emailBody = new StringBuilder();
                //New code
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.UpdatePolicyRejectQuery(policyDetails, userInfoDetails), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                //status = CommonConstants.StatusOne;
                /////////////////

                //Mail Message
                mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(fromEmailAddress);
                mailMessage.To.Add(new MailAddress(policyDetails.MailId));
                mailMessage.CC.Add(new MailAddress(userInfoDetails.EmailId));

                emailBody.Append(@"<html><body><div><img src=cid:myImageID> </div>");
                emailBody.AppendLine(CommonConstants.StartNewLine);
                emailBody.Append("<div style=\"font-family:Verdana;font-size:10px;\">");
                emailBody.AppendLine("<div style=\"text-align:right;\"><b>Date: </b>" + date + "</div>");

                if (mailType == CommonConstants.MailType_Approval)
                {
                    mailMessage.Subject = CommonConstants.MailSubjectApproval + policyNo;
                    emailBody.AppendLine("<hr />");
                    emailBody.AppendLine(CommonConstants.MailBodyApproval_Initial);
                    emailBody.Append(CommonConstants.Div_PolicyNumber + policyNo + CommonConstants.DIV);
                    emailBody.Append(CommonConstants.Div_BusinessReason + businessReason + CommonConstants.DIV);
                }
                else
                {
                    mailMessage.Subject = CommonConstants.MailSubjectRejection + policyNo;
                    emailBody.AppendLine("<div style=\"text-align:right;\"><b>Phone No:</b> " + phoneNo + "</div>");
                    emailBody.AppendLine("<hr />");
                    emailBody.AppendLine(CommonConstants.MailBodyRejection_Initial);
                    emailBody.Append(CommonConstants.Div_PolicyNumber + policyNo + CommonConstants.DIV);
                    emailBody.Append(CommonConstants.Div_BusinessReason + businessReason + CommonConstants.DIV);
                    emailBody.Append(CommonConstants.Div_RejectedReason + rejectedReason + CommonConstants.DIV);
                }
                emailBody.Append(CommonConstants.EndNewLine);
                emailBody.Append(CommonConstants.DIV);
                emailBody.Append(CommonConstants.BODY);
                emailBody.Append(CommonConstants.HTML);

                mailMessage.Body = emailBody.ToString();
                mailMessage.IsBodyHtml = true;

                //create Alrternative HTML view
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, CommonConstants.MailType);
                //Add Image
                LinkedResource theEmailImage = new LinkedResource(logo1);
                theEmailImage.ContentId = CommonConstants.ImageId;

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage);

                //Add view to the Email Message
                mailMessage.AlternateViews.Add(htmlView);
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.Port = 25;
                smtpClient.Send(mailMessage);

            }
            catch (System.Net.Mail.SmtpException exception)
            {
                errorMessage = exception.Message;
                status = false;
            }
            finally
            {
                if (mailMessage != null)
                {
                    mailMessage.Dispose();
                    mailMessage = null;
                }
                sepArray = null;
            }
            return status;
        }
        /// <summary>
        /// UpdatePolicyDetails method is useful to update the policy Details into database.
        /// </summary>
        /// <param name="updatePolicyDetails"></param>
        /// <returns></returns>
        public int UpdatePolicyDetails(PolicyDetailsBusinessEntity updatePolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.UpdatePolicyDetailsQuery(updatePolicyDetails), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                status = CommonConstants.StatusOne;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        /// <summary>
        /// DeletePolicyDetails method is useful to delete the specific Policy Number from the database.
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns>int</returns>
        public int DeletePolicyDetails(PolicyDetailsBusinessEntity deletePolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.DeletePolicyDetailsQuery(deletePolicyDetails), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                status = CommonConstants.StatusOne;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        /// <summary>
        /// GetAllApprovalDetails method retrieve all the approved and rejected account details from database.
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet GetAllApprovalDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.GetPolicyApprUnapprQuery(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                for (Int32 i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows[i][6].ToString() != "")
                    {
                        //if (Convert.ToInt32(ds.Tables[0].Rows[i][6]) == 0)
                        //{
                        //    ds.Tables[0].Rows[i][6] = System.DBNull.Value;
                        //}
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// UpdateApprovalDetails method is useful to update the account approval details into database. 
        /// </summary>
        /// <param name="updatePolicyDetails"></param>
        public int UpdateApprovalDetails(PolicyDetailsBusinessEntity updateApprovalStatus)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQry.UpdatePolicyApprovalQuery(updateApprovalStatus), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                status = CommonConstants.StatusOne;

            }
            catch (System.InvalidCastException e)
            {
                throw e;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();

            }
            return status;
        }
        #endregion
    }
}

