<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MilePost.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
            
    
    <div class="space-row-dv">
        <div class="label-dv" style="width: 160px;">
            <asp:Label ID="Label1" runat="server" Text="Username:" CssClass="label-field"></asp:Label>
        </div>
        <div>
            <asp:TextBox CssClass="text-field" ID="txtUsername" runat="server" ></asp:TextBox>
        </div>
    </div>
    <div class="space-row-dv">
        <div class="label-dv" style="width: 160px;">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="label-field"></asp:Label>
        </div>
        <div>
            <asp:TextBox CssClass="text-field" TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
        </div>
    </div>
    <div style="margin: 15px 0px 30px 0px;">
        <div class="btn-submit" style="width: 65px; margin-right: 30px;">
            <asp:Button ID="btnSubmit" CssClass="" runat="server" Text="Submit" OnClientClick="javascript:return validControl();"
                OnClick="btnSubmit_Click" />
        </div>
        <div>
            <asp:Button ID="btnReset" CssClass="" runat="server" Text="Reset" OnClick="btnReset_Click" />
        </div>
    </div>
    </form>
</body>
</html>
