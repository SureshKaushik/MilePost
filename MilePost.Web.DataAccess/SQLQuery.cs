using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilePost.Web.BusinessEntity;

namespace MilePost.Web.DataAccess
{
    public class SQLQuery
    {
        string queryString = string.Empty;

        public string GetUserInfoQuery(string uName, string password)
        {
            try
            {
                queryString = "SELECT * FROM "+ CommonConstants.tblUserInfo+ " WHERE USER_NAME = " + CommonConstants.invertedComma + uName + CommonConstants.invertedComma + " and PASSWORD = " + CommonConstants.invertedComma + password + CommonConstants.invertedComma;
                return queryString;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetUserRoleQuery(int roleID)
        {
            try
            {
                queryString = " SELECT ROLE FROM " + CommonConstants.tblUserRole + " WHERE ROLE_ID = " + roleID;
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetPolicyDetailsQuery(int userId)
        {
            try
            {
                //queryString = "SELECT TPD.*,TUI.USER_NAME,TUI.EMAIL_ID FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.USER_ID = " + userId;
                queryString = "SELECT TPD.*,TUI.USER_NAME,TUI.EMAIL_ID FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.USER_ID = " + userId + " AND APPROVAL_IND_ACCTNG IN ('N','n') ";
                //queryString = "SELECT TPD.*,TUI.USER_NAME,TUI.EMAIL_ID FROM E0384DB.T_POLICY_DETAILS TPD INNER JOIN E0384DB.T_USER_INFO TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.USER_ID = 1046063 AND APPROVAL_IND_ACCTNG IN ('N','n') ";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetAllPolicyDetailQuery()
        {
            try
            {
                queryString = "SELECT TPD.POLICY_NUMBER AS POLICY_NUMBER,TPD.BUSINESS_REASON AS BUSINESS_REASON, TPD.ROOT_SEG_CNT AS ROOT_SEG, TPD.MACHINE_SEG_CNT AS MACHINE_SEG, TPD.FACE_SEG_CNT AS FACE_SEG, TUI.USER_ID AS USER_ID, TUI.EMAIL_ID AS EMAIL_ID, TPD.APPROVAL_IND_ACCTNG FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.LOADED_IN_PROD_IND IN ('N','n') ORDER BY TPD.POLICY_NUMBER";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string InsertPolicyDetailsQuery(PolicyDetailsBusinessEntity addPolicyDetails)
        {
            try
            {
                queryString = "INSERT INTO " + CommonConstants.tblPolicyDetails + "(USER_ID,POLICY_NUMBER,REQUEST_DATE, BUSINESS_REASON,APPROVAL_IND_ACCTNG,LOADED_IN_PROD_IND,ROOT_SEG_CNT,MACHINE_SEG_CNT,FACE_SEG_CNT ) VALUES (" + addPolicyDetails.UserId + "," + CommonConstants.invertedComma + addPolicyDetails.PolicyNo + CommonConstants.invertedComma + "," + CommonConstants.invertedComma + addPolicyDetails.RequestDate + CommonConstants.invertedComma + "," + CommonConstants.invertedComma + addPolicyDetails.BusinessReason + CommonConstants.invertedComma + "," + "'N'" + "," + "'N'" + "," + addPolicyDetails.RootSegmentCount + "," + addPolicyDetails.MachineSegmentCount + "," + addPolicyDetails.FaceSegmentCount + ")";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string UpdatePolicyDetailsQuery(PolicyDetailsBusinessEntity updatePolicyDetails)
        {
            try
            {
                queryString = "UPDATE " + CommonConstants.tblPolicyDetails + " SET REQUEST_DATE = " + CommonConstants.invertedComma + updatePolicyDetails.RequestDate + CommonConstants.invertedComma + "," + "BUSINESS_REASON = " + CommonConstants.invertedComma + updatePolicyDetails.BusinessReason + CommonConstants.invertedComma + "," + "ROOT_SEG_CNT = " + updatePolicyDetails.RootSegmentCount + "," + " MACHINE_SEG_CNT =" + updatePolicyDetails.MachineSegmentCount + "," + "FACE_SEG_CNT = " + updatePolicyDetails.FaceSegmentCount + " WHERE USER_ID = " + updatePolicyDetails.UserId + " AND " + " POLICY_NUMBER = " + CommonConstants.invertedComma + updatePolicyDetails.PolicyNo + CommonConstants.invertedComma;
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string DeletePolicyDetailsQuery(PolicyDetailsBusinessEntity deletePolicyDetails)
        {
            try
            {
                queryString = "DELETE FROM  " + CommonConstants.tblPolicyDetails + " WHERE USER_ID = " + deletePolicyDetails.UserId + " AND POLICY_NUMBER = " + CommonConstants.invertedComma + deletePolicyDetails.PolicyNo + CommonConstants.invertedComma;
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetPolicyEnquiryQuery(PolicyDetailsBusinessEntity getPolicyEnquiry)
        {
            try
            {
                //queryString = " SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.POLICY_NUMBER = " + CommonConstants.invertedComma + getPolicyEnquiry.PolicyNo + CommonConstants.invertedComma + " AND ((TO_DATE(REQUEST_DATE,'MM/DD/YYYY') >= TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + ",'MM/DD/YYYY')) AND (TO_DATE(REQUEST_DATE,'MM/DD/YYYY') <= TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + ",'MM/DD/YYYY'))) ORDER BY TPD.POLICY_NUMBER";
                
                //queryString = " SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.POLICY_NUMBER = " + CommonConstants.invertedComma + getPolicyEnquiry.PolicyNo + CommonConstants.invertedComma + " AND (REQUEST_DATE >= TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + ",'MM/DD/YYYY') AND (REQUEST_DATE <=TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + ",'MM/DD/YYYY'))) ORDER BY TPD.POLICY_NUMBER";

                queryString = " SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.POLICY_NUMBER = " + CommonConstants.invertedComma + getPolicyEnquiry.PolicyNo + CommonConstants.invertedComma + " AND REQUEST_DATE >=" + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + " AND REQUEST_DATE <=" + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + " ORDER BY TPD.POLICY_NUMBER";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetPolicyEnquiryDateQuery(PolicyDetailsBusinessEntity getPolicyEnquiry)
        {
            try
            {
                //queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + "  WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE ((TO_DATE(REQUEST_DATE,'MM-dd-yyyy') >= " + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + ") AND (TO_DATE(REQUEST_DATE,'MM-dd-yyyy') <= " + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + "))ORDER BY TPD.POLICY_NUMBER";
                //queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE REQUEST_DATE BETWEEN " + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + " AND " + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + " ORDER BY TPD.POLICY_NUMBER";
                //queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE REQUEST_DATE BETWEEN " + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + " AND " + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma; 
                
                //queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE REQUEST_DATE >= TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + ",'MM/DD/YYYY') AND REQUEST_DATE <=TO_DATE(" + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + ",'MM/DD/YYYY') ORDER BY TPD.POLICY_NUMBER";

                queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE REQUEST_DATE >= " + CommonConstants.invertedComma + getPolicyEnquiry.StartDate + CommonConstants.invertedComma + " AND REQUEST_DATE <= " + CommonConstants.invertedComma + getPolicyEnquiry.EndDate + CommonConstants.invertedComma + " ORDER BY TPD.POLICY_NUMBER";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string PolicyEnquiryPolicyNoQuery(PolicyDetailsBusinessEntity getPolicyEnquiry)
        {
            try
            {
                queryString = "SELECT TPD.*, TUI.USER_NAME, TUI.EMAIL_ID, (SELECT USER_NAME FROM " + CommonConstants.tblUserInfo + " WHERE USER_ID = TPD.APPROVER_ID_ACCTNG) AS APPROVER_NAME FROM " + CommonConstants.tblPolicyDetails + " TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.POLICY_NUMBER = " + CommonConstants.invertedComma + getPolicyEnquiry.PolicyNo + CommonConstants.invertedComma + "ORDER BY TPD.POLICY_NUMBER";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetPolicyApprUnapprQuery()
        {
            try
            {
                queryString = "SELECT TPD.* ,TUI.* FROM " + CommonConstants.tblPolicyDetails + "  TPD INNER JOIN " + CommonConstants.tblUserInfo + " TUI ON TPD.USER_ID = TUI.USER_ID WHERE TPD.APPROVAL_IND_ACCTNG = 'N' OR TPD.APPROVAL_IND_ACCTNG = 'n' ORDER BY TPD.POLICY_NUMBER";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string UpdatePolicyApprovalQuery(PolicyDetailsBusinessEntity updateApprovalStatus)
        {
            try
            {
                queryString = "UPDATE " + CommonConstants.tblPolicyDetails + " SET APPROVER_ID_ACCTNG =  " + updateApprovalStatus.UserId + "," + " APPROVAL_IND_ACCTNG = 'Y' WHERE POLICY_NUMBER   = " + CommonConstants.invertedComma + updateApprovalStatus.PolicyNo + CommonConstants.invertedComma;
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string UpdatePolicyRejectQuery(PolicyDetailsBusinessEntity policyDetails, UserInfoDetailsBusinessEntity userInfoDetails)
        {
            try
            {
                queryString = "UPDATE " + CommonConstants.tblPolicyDetails + " SET APPROVER_ID_ACCTNG =  " + userInfoDetails.UserId + " WHERE POLICY_NUMBER   = " + CommonConstants.invertedComma + policyDetails.PolicyNo + CommonConstants.invertedComma;
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }

        public string GetApproversEmailIdQuery()
        {
            try
            {
                queryString = "SELECT EMAIL_ID FROM " + CommonConstants.tblUserInfo + " WHERE ROLE_ID IN (SELECT ROLE_ID FROM " + CommonConstants.tblUserRole + " WHERE ROLE LIKE '%A%')";
                return queryString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                queryString = string.Empty;
            }
        }
    }
}

