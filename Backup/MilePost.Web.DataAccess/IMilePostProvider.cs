using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilePost.Web.BusinessEntity;
using System.Data;

namespace MilePost.Web.DataAccess
{
    public interface IMilePostProvider
    {
        #region Override Request
        UserInfoDetailsBusinessEntity CheckUserAuth(UserInfoDetailsBusinessEntity userInfo);
        DataSet GetPolicyDetails(UserInfoDetailsBusinessEntity userInfoDetails);
        DataSet GetAllPolicyNumber();
        DataSet GetAllApprovedPolicyNo();
        int AddPolicyDetails(PolicyDetailsBusinessEntity addPolicyDetails);
        int UpdatePolicyDetails(PolicyDetailsBusinessEntity updatePolicyDetails);
        int DeletePolicyDetails(PolicyDetailsBusinessEntity deletePolicyDetails);
        bool SendEmail(PolicyDetailsBusinessEntity policyDetails);
        bool SendApproveRejectMail(PolicyDetailsBusinessEntity policyDetails, UserInfoDetailsBusinessEntity userInfoDetails, string mailType);
        #endregion

        #region Account Approval
        DataSet GetAllApprovalDetails();
        int UpdateApprovalDetails(PolicyDetailsBusinessEntity updatePolicyDetails);
        #endregion

        #region Override Audit Log
        DataSet GetPolicyEnquiry(PolicyDetailsBusinessEntity getPolicyEnquiry);
        //void AddtoLogFile1(string Message, string WebPage);
        #endregion
    }
}
