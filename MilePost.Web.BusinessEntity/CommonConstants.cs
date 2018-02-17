using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilePost.Web.BusinessEntity
{
    /// <summary>
    /// Summary description for CommonConstants.
    /// This class and method can be used as the basis for a DB2 CLR procedure.
    /// </summary>
    public class CommonConstants
    {
        //Login Page
        public const string UserName = "UserName";
        public const string Role = "Role";
        public const string UserInfo = "UserInfo";
        public const string Home = "Home.aspx";
        public const string Login = "Login.aspx";
        public const string Error = "Error.aspx";

        //Overwrite Request
        public const string AllPolicyDetails = "AllPolicyDetails";
        public const string SelectedRowIndex = "SelectedRowIndex";
        public const string PolicyNumber = "Policy Number ";
        public const string AlreadyExixts = " already exists.";
        public const string EditRow = "EditRow";
        public const string DeleteRow = "DeleteRow";
        public const string ASPSessionId = "ASP.NET_SessionId";
        public const string PolicyEntry = "PolicyEntry.aspx";
        public const string Logout = "Logout.aspx";
        public const string PolicyUnapproved = "PolicyUnapproved";
        public const string ShowAlert = "showalert";
        public const string SuccessUpdate = "alert('Policy number has been successfully updated.');";
        public const char ApproveIndN = 'N';
        public const char ApproveIndn = 'n';
        public const string AddFailure = "alert('Policy Number has already been raised by another user. User cannot add this Policy number.');";
        public const char ApproveIndY = 'Y';
        public const char ApproveIndy = 'y';
        public const string AlreadyApproved = "alert('Policy has already approved. User cannot modify the Policy.');";
        public const string SuccessAdded = "alert('You have successfully added the policy.');";
        public const string SuccessDeleted = "alert('You have successfully deleted the policy.');";
        public const string MailSuccessApprove = "alert('Approval mail notification sent successfully.');";
        public const string MailSuccessReject = "alert('Rejection mail notification sent successfully.');";
        public const string MailSuccess = "alert('Mail notification sent successfully.');";

        //Account Approval
        public const string CheckedItems = "CheckedItems";
        public const string CheckedRejected = "CheckedRejected";
        public const string RbApprove = "rbApprove";
        public const string RbReject = "rbReject";
        public const string AccountApproval = "AccountApproval.aspx";
        public const string PolicyEnquiry = "PolicyEnquiry.aspx";
        public const string Reject = "Reject";
        public const string MsgProvideReason = "Please provide reason for the rejected Policy.";
        public const string MsgSelectApprove = "Please select only one policy for Approve. Please click on Reset button to clear the selection.";
        public const string MsgSelectReject = "Please select only one policy for Reject. Please click on Reset button to clear the selection.";
        public const string MsgSelectAppRej = "Please select only one policy for Approve or reject.Please click on Reset button to clear the selection.";
        public const bool False = false;
        public const bool True = true;
        public const string SuccessApprove = "alert('Policy number has been successfully approved.');";
        public const string ApproveInd = "App";
        public const string RejectInd = "Rej";
        public const string SucccessReject = "alert('Policy number has been successfully rejected.');";

        //Calendar 
        public const string language = @"<script language=javascript>";
        public const string TextVal = "window.opener.document.all.txtRequestDate.value='";
        public const string DateFormat = "MM/dd/yyyy";

        public const string WinClose = "'; window.close();";
        public const string ScriptClose = "</script>";
        public const string TextOverwriteval = "window.opener.document.all.txtEndDate.value='";

        //Data Access
        public const string ConnectionStr = "ConnectionStr";
        public const string Logo1 = "Logo1";
        public const string SmtpPort = "SMTPPort";
        public const string Name = "name";
        public const string Password = "psd";
        public const string UserId = "USER_ID";
        public const string RoleId = "ROLE_ID";
        public const string EmailId = "EMAIL_ID";
        public const string PhoneNo = "PHONE_NUMBER";
        public const string Roleid = "roleid";
        public const string Role1 = "ROLE";
        public const string DateFormat1 = "MM/dd/yyyy";
        public const string UserID = "userid";
        public const string FromEmailAddress = "FromEmaiAddress";
        public const string SmtpServer = "SMTPServer";
        public const string PolicyNo = "policyNo";
        public const string ImageId = "myImageID";
        public const string StartDate = "startDate";
        public const string EndDate = "endDate";

        public const string RequestDate1 = "requestDate";
        public const string TargetDate = "targetDate";
        public const string NoOfContractsImpacted = "noOfCntImp";
        public const string BusinessReason = "businessReason";
        public const string RootSegmentCount = "rootSegCont";
        public const string MachineSegmentCount = "machineSegCnt";
        public const string FaceSegmentCount = "faceSegCnt";

        public const string ApproverId = "APPROVERID";
        public const string PNo = "P_NOS";
        public const int StatusOne = 1;
        public const int StatusZero = 0;
        public const string invertedComma = "'";

        //HandleLogging

        public const string LogPath = "LogPath";
        public const string Log = "Log_";
        public const string ExtentionLogFile = ".txt";
        public const string StartLogging = "-------------------START-------------";
        public const string EndLogging = "-------------------END-------------";
        public const string SourcePage = "Source Page : ";
        public const string LogFileDateFormat = "MM-DD-YYYY";


        //Mail Part

        public const string MailType = "text/html";
        public const string MailSubjectPolicyEntry1 = "Policy Overwrite Request - Policy No: ";
        public const string MailSubjectPolicyEntry2 = " Date: ";
        public const string MailSubjectApproval = "Policy Overwrite Request Approved - Policy No: ";
        public const string MailSubjectRejection = "Policy Overwrite Request Rejected - Policy No: ";
        public const string MailBodyApproval_Initial = "<div>The below policy has been approved for the overwrite request with the following details:</div>";
        public const string MailBodyRejection_Initial = "<div>Policy overwrite request has been rejected due to the following reason :</div>";
        public const string MailBusinessReason = "";
        public const string DIV = "</div>";
        public const string BODY = "</body>";
        public const string HTML = "</html>";
        public const string StartNewLine = "<br>";
        public const string EndNewLine = "<br />";
        public const string MailType_Approval = "App";
        public const string Div_BusinessReason = "<div>Business Reason: ";
        public const string Div_RejectedReason = "<div>Rejection Reason: ";
        public const string Div_PolicyNumber = "<div>Policy Number: ";
        public const string Div_RootSegmentCNT = "<div>Root Segment Count: ";
        public const string Div_MachineSegmentCNT = "<div>Machine Segment Count: ";
        public const string Div_FaceSegmentCNT = "<div>Face Segment Count: ";



        public const string SProcUserInfo = "GET_USER_INFO";
        public const string SProcUserRole = "GET_USER_ROLE";
        public const string SProcGetPolicyDetails = "GET_POLICY_DETAIL";
        public const string SProcGetPolicyEnquiry = "GET_POLICY_ENQUIRY";
        public const string SProcGetPolicyEnquiryDate = "GET_POLICY_ENQUIRY_DATE";
        public const string SprocUpdatePolicyDetails = "UPDATE_POLICY_DETAIL";
        public const string SProcDeletePolicyDetails = "DELETE_POLICY_DETAIL";
        public const string SProcGetAllPolicyDetails = "GET_ALL_POLICY_DETAIL";
        public const string SProcPolicyEnquiryPNo = "GET_POLICY_ENQUIRY_PNO";
        public const string SProcGetPolicyApprUnappr = "GET_POLICY_APPR_UNAPPR";
        public const string SProcUpdatePolicyApproval = "UPDATE_POLICY_APPROVAL";
        public const string SProcInsertPolicyDetails = "INSERT_POLICY_DETAIL";
        public const string SProcGetApproversEmailId = "GET_APPROVERS_EMAILID";
        public const string SProcGetUserName = "GET_USER_NAME";
        public const string SProcGetAllPolicyApproved = "GET_ALL_POLICY_APPROVED";  


        //public const string SProcUserInfo = "E0384DBA.SPW_E0384GUI ";
        //public const string SProcUserRole = "E0384DBA.SPW_E0384GUR";
        //public const string SProcGetPolicyDetails = "E0384DBA.SPW_E0384GPD";
        //public const string SProcGetAllPolicyDetails = "E0384DBA.SPW_E0384APD";
        //public const string SProcGetPolicyEnquiryDate = "E0384DBA.SPW_E0384PED";
        //public const string SProcInsertPolicyDetails = "E0384DBA.SPW_E0384IPD";
        //public const string SprocUpdatePolicyDetails = "E0384DBA.SPW_E0384UPD";
        //public const string SProcDeletePolicyDetails = "E0384DBA.SPW_E0384DPD";
        //public const string SProcGetPolicyApprUnappr = "E0384DBA.SPW_E0384PAU";
        //public const string SProcUpdatePolicyApproval = "E0384DBA.SPW_E0384UPA";
        //public const string SProcPolicyEnquiryPNo = "E0384DBA.SPW_E0384PEP";
        //public const string SProcGetPolicyEnquiry = "E0384DBA.SPW_E0384GPE";
        //public const string SProcGetApproversEmailId = "E0384DBA.SPW_E0384GAE";
        //public const string SProcGetUserName = "E0384DBA.SPW_E0384GUN";
        //public const string SProcGetAllPolicyApproved = "E0384DBA.SPW_E0384APA";

        #region tableName without Schema Name

        public const string tblUserInfo = "T_USER_INFO";
        public const string tblUserRole = "T_ROLE_MASTER";
        public const string tblPolicyDetails = "T_POLICY_DETAILS";

        #endregion

        #region tableName with Schema Name

        //public const string tblUserInfo = "E0384DBA.T_USER_INFO";
        //public const string tblUserRole = "E0384DBA.T_ROLE_MASTER";
        //public const string tblPolicyDetails = "E0384DBA.T_POLICY_DETAILS";

        #endregion

        #region tableName with E0384DB Schema Name

        //public const string tblUserInfo = "E0384DB.T_USER_INFO";
        //public const string tblUserRole = "E0384DB.T_ROLE_MASTER";
        //public const string tblPolicyDetails = "E0384DB.T_POLICY_DETAILS";

        #endregion
    }
}
