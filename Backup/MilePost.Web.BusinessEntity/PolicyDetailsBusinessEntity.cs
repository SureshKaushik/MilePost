using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilePost.Web.BusinessEntity
{
    public class PolicyDetailsBusinessEntity
    {
        int policyId;
        string policyNo;
        int userId;
        string userName = string.Empty;
        string phoneNumber = string.Empty;
        string requestDate = string.Empty;
        string startDate = string.Empty;
        string endDate = string.Empty;
        string mailid = string.Empty;
        string overwriteDate = string.Empty;
        string businessReason = string.Empty;
        string rejecetdReason = string.Empty;
        int approvalid;
        int rootSegCount;
        int faceSegCount;
        int machineSegCount;

        #region Policy Details
        public int PolicyId
        {
            get { return policyId; }
            set { policyId = value; }
        }

        public string PolicyNo
        {
            get { return policyNo; }
            set { policyNo = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }

        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string MailId
        {
            get { return mailid; }
            set { mailid = value; }
        }

        public string OverwriteDate
        {
            get { return overwriteDate; }
            set { overwriteDate = value; }
        }

        public string BusinessReason
        {
            get { return businessReason; }
            set { businessReason = value; }
        }
        public string RejectedReason
        {
            get { return rejecetdReason; }
            set { rejecetdReason = value; }
        }
        public int ApprovalId
        {
            get { return approvalid; }
            set { approvalid = value; }
        }
        public int RootSegmentCount
        {
            get { return rootSegCount; }
            set { rootSegCount = value; }
        }
        public int MachineSegmentCount
        {
            get { return machineSegCount; }
            set { machineSegCount = value; }
        }
        public int FaceSegmentCount
        {
            get { return faceSegCount; }
            set { faceSegCount = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        #endregion
    }
}
