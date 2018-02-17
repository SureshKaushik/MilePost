<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountApproval.aspx.cs" Inherits="MilePost.AccountApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div class="login-top-dv">
           <img src="Images/MetLife_Logo_svg.jpg" alt ="logo" class="header-dv"/>
            <div style="text-align: right; padding-right: 10px;">
                Welcome,
                <%=Session["UserName"].ToString()%></div>
            <div style="text-align: right; padding-right: 10px;">
                <asp:LinkButton runat="server" ID="btnSignOut" OnClick="btnSignOut_Click">Sign out</asp:LinkButton>
            </div>
        </div>
        <div class="login-left-dv">
            <div style="margin: 30px 0px 0px 10px;" class="hyper-link-text">
                <asp:HyperLink ID="hlPolicyEntry" NavigateUrl="" runat="server">Account Approval</asp:HyperLink>
            </div>
            <div style="margin: 5px 0px 0px 10px;" class="hyper-link-text">
                <asp:HyperLink ID="hlPolicyEnquiry" NavigateUrl="~/PolicyEnquiry.aspx" runat="server">Overwrite Audit Log</asp:HyperLink>
            </div>
        </div>
        <div class="content-area-div">
            <div style="" class="white-title-text-block">
                <div style="float: left;">
                    <span style="" class="span-page-header">ACCOUNT APPROVAL</span>
                </div>
                <div style="border: 0px red solid;">
                    <span id="clock">
                        <script language="JavaScript" type="text/javascript">
        <!-- Begin
        var dayarray=new Array("Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday")
        var montharray=new Array("January","February","March","April","May","June","July","August","September","October","November","December")
        function getthedate(){
        var mydate=new Date()
        var year=mydate.getYear()
        if (year < 1000)
        year+=1900
        var day=mydate.getDay()
        var month=mydate.getMonth()
        var daym=mydate.getDate()
        if (daym<10)
        daym="0"+daym
        var hours=mydate.getHours()
        var minutes=mydate.getMinutes()
        var seconds=mydate.getSeconds()
        var dn="AM"
        if (hours>=12)
        dn="PM"
        if (hours>12){
        hours=hours-12
        }
        {
         d = new Date();
         Time24H = new Date();
         Time24H.setTime(d.getTime() + (d.getTimezoneOffset()*60000) + 3600000);
         InternetTime = Math.round((Time24H.getHours()*60+Time24H.getMinutes()) / 1.44);
         if (InternetTime < 10) InternetTime = '00'+InternetTime;
         else if (InternetTime < 100) InternetTime = '0'+InternetTime;
        }
        if (hours==0)
        hours=12
        if (minutes<=9)
        minutes="0"+minutes
        if (seconds<=9)
        seconds="0"+seconds
        //change font size here
        var cdate=dayarray[day]+", "+daym+" "+montharray[month]+" "+year+" | "+hours+":"+minutes+":"+seconds+" "+dn+""
        if (document.all)
        document.all.clock.innerHTML=cdate
        else if (document.getElementById)
        document.getElementById("clock").innerHTML=cdate
        else
        document.write(cdate)
        }
        if (!document.all&&!document.getElementById)
        getthedate()
        function goforit(){
        if (document.all||document.getElementById)
        setInterval("getthedate()",1000)
        }
        window.onload=goforit
        //  End -->
                        </script>
                    </span>
                </div>
            </div>
             <div class="space-row-dv">
                    <asp:Label class="infotext" ID="lblNoRecords" runat="server" Text="No policy exists for approval."
                        Visible="False" Font-Bold="true" Font-Size="Small"></asp:Label>
            </div>
            <div style="border: 0px red solid; height: 7500px;">
             <div class="" style="border: 0px black solid; margin: 0px 10px 10px 50px;">
                <asp:GridView runat="server" ID="GridView1" BackColor="White" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" Style="text-align: center" CellPadding="4"
                    AllowPaging="True" Width="988px" AutoGenerateColumns="False"
                    DataKeyNames="POLICY_NUMBER" OnPageIndexChanging="GridView1_PageIndexChanging" 
                    PageSize="5" Visible="false">
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <Columns>
                        <asp:BoundField HeaderText="POLICY NO" DataField="POLICY_NUMBER">
                            <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="USER ID" DataField="USER_ID">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="USER NAME" DataField="USER_NAME">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="REQUEST DATE" DataField="REQUEST_DATE">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>                       
                      <asp:BoundField HeaderText="USER MAILID" DataField="EMAIL_ID">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                       
                        <%--<asp:BoundField HeaderText="BUSINESS REASON" DataField="BUSINESS_REASON">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="BUSINESS REASON">                               
                            <ItemTemplate>
                                <asp:TextBox ID="GrdTxtBusinessReason" Enabled="true" ReadOnly="true"  CssClass="textarea-scrollbar-color" BorderWidth="0" Font-Size="11px" Font-Names="Verdana" ForeColor="#003399" TextMode="MultiLine" runat="server" Wrap="true" Text='<%# Eval("BUSINESS_REASON")%>'/>
                            </ItemTemplate>
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />                               
                         </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="APPROVER ID" DataField="APPROVER_ID_ACCTNG">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>  --%> 
                         <%--<asp:BoundField HeaderText="APPROVER NAME" DataField="APPROVER_NAME">
                            <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField> --%>
                        <asp:BoundField HeaderText="ROOT SEGMENT COUNT" DataField="ROOT_SEG_CNT">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="MACHINE SEGMENT COUNT" DataField="MACHINE_SEG_CNT">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="FACE SEGMENT COUNT" DataField="FACE_SEG_CNT">
                            <ControlStyle Font-Names="Verdana" />
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Smaller" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Approve">
                            <ItemTemplate>
                                <asp:RadioButton ID="rbApprove" runat="server" GroupName = "rbGroup1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                 <asp:RadioButton ID="rbReject" runat="server" GroupName = "rbGroup1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle BackColor="#99CCCC" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
                </div>
                 <div class="space-row-dv">
                    <asp:Label class="infotext" ID="lblNotSelected" runat="server"
                        Visible="False" Font-Bold="true" Font-Size="Small"></asp:Label>
                </div>
                <div class="space-row-dv" id = "dvReasonReject">
                    <div style="float: left; width: 100px;">
                        <asp:Label ID="lblBusinessReason" runat="server" Text="Reason:" CssClass="label-field"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox CssClass="text-field-area" ID="txtReasonReject" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div style="margin: 15px 0px 30px 0px;">
                    <div class="btn-submit" style="width: 65px; margin-right: 35px; margin-left: 121px;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
                    </div>
                    <div>
                        <asp:Button ID="btnReset" CssClass="" runat="server" Text="Reset"
                             onclick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>   
   <asp:HiddenField ID ="hdnReject" runat ="server" />
    
     </form>
</body>
</html>
