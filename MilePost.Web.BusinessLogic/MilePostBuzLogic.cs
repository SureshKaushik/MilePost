using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilePost.Web.BusinessEntity;
using System.Data;
using MilePost.Web.DataAccess;

namespace MilePost.Web.BusinessLogic
{
    public class MilePostBuzLogic : IMilePostBuzLogic
    {
        public MilePostBuzLogic() { }
        #region IMilePost Members
        /// <summary>
        /// Invokes the CheckUserCredential method of the MilePostProvider class to check the credentails authentication
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfoDetailsBusinessEntity CheckUserAuth(UserInfoDetailsBusinessEntity userInfo)
        {
            UserInfoDetailsBusinessEntity userInfoBe = new UserInfoDetailsBusinessEntity();
            MilePostProvider provider = new MilePostProvider();
            try
            {
                userInfo = provider.CheckUserAuth(userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userInfo;
        }

        /// <summary>
        /// Invokes the GetPolicyDetails method of the MilePostProvider class to retrive the policy details.
        /// </summary>
        /// <param name="userInfoDetails"></param>
        /// <returns></returns>
        public DataSet GetPolicyDetails(UserInfoDetailsBusinessEntity userInfoDetails)
        {
            DataSet ds = new DataSet();
            try
            {
                MilePostProvider provider = new MilePostProvider();
                ds = provider.GetPolicyDetails(userInfoDetails);
            }
            catch (DataException ex)
            {
                throw ex;
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
                MilePostProvider provider = new MilePostProvider();
                ds = provider.GetAllPolicyNumber();
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// GetAllPolicyNumber method is useful to retrieve all the Policy Numbers available in Table.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetAllApprovedPolicyNo()
        {
            DataSet ds = new DataSet();
            try
            {
                MilePostProvider provider = new MilePostProvider();
                ds = provider.GetAllApprovedPolicyNo();
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// GetPolicyEnquiry method is useful to retrieve override audit log details from the database.
        /// </summary>
        /// <param name="getPolicyEnquiry"></param>
        /// <returns>DataSet</returns>
        public DataSet GetPolicyEnquiry(PolicyDetailsBusinessEntity getPolicyEnquiry)
        {
            DataSet ds = new DataSet();
            try
            {
                MilePostProvider provider = new MilePostProvider();
                ds = provider.GetPolicyEnquiry(getPolicyEnquiry);
            }
            catch (DataException ex)
            {
                throw ex;
            }

            return ds;
        }

        /// <summary>
        /// Invokes the AddPolicyDetails method of the MilePostProvider class to add the policy details.
        /// </summary>
        /// <param name="addPolicyDetails"></param>
        /// <returns></returns>
        public int AddPolicyDetails(PolicyDetailsBusinessEntity addPolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.AddPolicyDetails(addPolicyDetails);
            }
            catch (DataException ex)
            {
                throw ex;
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
            bool status = false;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.SendEmail(policyDetails);
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return status;
        }
        /// <summary>
        /// Invokes SendApproveRejectMail method of the MilePostProvider class to send trigger mail on policy approval and policy reject to Business Team
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool SendApproveRejectMail(PolicyDetailsBusinessEntity policyDetails, UserInfoDetailsBusinessEntity userInfoDetails, string mailType)
        {
            bool status = false;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.SendApproveRejectMail(policyDetails, userInfoDetails, mailType);
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return status;
        }
        /// <summary>
        /// Invokes the UpdatePolicyDetails method of the MilePostProvider class to update the policy details.
        /// </summary>
        /// <param name="updatePolicyDetails"></param>
        /// <returns></returns>
        public int UpdatePolicyDetails(PolicyDetailsBusinessEntity updatePolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.UpdatePolicyDetails(updatePolicyDetails);
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return status;
        }
        /// <summary>
        /// Invokes the DeletePolicyDetails method of the MilePostProvider class to delete the policy details.
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public int DeletePolicyDetails(PolicyDetailsBusinessEntity deletePolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.DeletePolicyDetails(deletePolicyDetails);
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return status;
        }
        /// <summary>
        /// GetAllApprovalDetails method is useful to retrieve all the account approval details from database.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetAllApprovalDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                MilePostProvider provider = new MilePostProvider();
                ds = provider.GetAllApprovalDetails();
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// UpdateApprovalDetails method is useful to update all the policy which is in approve and 
        /// </summary>
        /// <param name="updatePolicyDetails"></param>
        public int UpdateApprovalDetails(PolicyDetailsBusinessEntity updatePolicyDetails)
        {
            int status = CommonConstants.StatusZero;
            try
            {
                MilePostProvider provider = new MilePostProvider();
                status = provider.UpdateApprovalDetails(updatePolicyDetails);
            }
            catch (DataException ex)
            {
                throw ex;
            }
            return status;
        }

        #endregion
    }
}
