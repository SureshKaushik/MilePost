<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MilePost.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="login-top-dv">
           <img src="Images/MetLife_Logo_svg.jpg" alt ="logo" class="header-dv"/>
            <div style="text-align: right; padding-right: 10px;">
                Welcome,
                <%=Session["UserName"].ToString()%>
                </div>
            <div style="text-align: right; padding-right: 10px;">
                <asp:LinkButton runat="server" ID="btnSignOut" OnClick="btnSignOut_Click">Sign out</asp:LinkButton>
            </div>
        </div>
    <div class="login-left-dv">
            <div style="margin: 30px 0px 0px 10px;" class="hyper-link-text">
                <% string role = String.Empty; %>
                <% if (Session["Role"].ToString() != null)
                   {
                       role = Session["Role"].ToString();
                   }%>
                <%if (role == "Business User")
                  { %>
                <asp:HyperLink ID="hlPolicyEntry" NavigateUrl="~/PolicyEntry.aspx" runat="server">Overwrite Request</asp:HyperLink>
                <%} %>
            </div>
            <%if (role == "Account Team")
              { %>
            <div style="margin: 5px 0px 0px 10px;" class="hyper-link-text">
                <asp:HyperLink ID="hlAccountApproval" NavigateUrl="~/AccountApproval.aspx" runat="server">Account Approval</asp:HyperLink>
            </div>
            <%} %>
            <div style="margin: 5px 0px 0px 10px;" class="hyper-link-text">
                <asp:HyperLink ID="hlPolicyEnquiry" NavigateUrl="~/PolicyEnquiry.aspx" runat="server">Overwrite Audit Log</asp:HyperLink>
            </div>
        </div>
        <div class="content-area-div" style="height: 580px;">
        You have Successfull Loged In.
        </div>
    </form>
</body>
</html>
