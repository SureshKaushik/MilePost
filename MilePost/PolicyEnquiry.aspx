<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyEnquiry.aspx.cs" Inherits="MilePost.PolicyEnquiry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Overwrite Audit Log</title>    
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/MasterScripts.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 800px;">
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
                <asp:HyperLink ID="hlPolicyEnquiry" NavigateUrl="" runat="server">Overwrite Audit Log</asp:HyperLink>
            </div>
        </div>
        <div class="content-area-div" style="height: 580px;">
            <div class="white-title-text-block">
                <div style="float: left;">
                    <span style="" class="span-page-header">OVERWRITE AUDIT LOG</span>
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
            <div style="height: 535px;">
                <div class="space-row-dv">
                    <div class="label-dv" style="width: 150px;">
                        <asp:Label ID="lblPolicyId" runat="server" Text="Policy No:" CssClass="label-field"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox CssClass="text-field" ID="txtPolicyNo" runat="server" MaxLength = "8"></asp:TextBox>
                    </div>
                </div>
                <div class="space-row-dv">
                    <div class="label-dv" style="width: 150px;">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="label-field"></asp:Label>
                    </div>
                    <div style="float: left; margin-right: 1px;">
                        <asp:TextBox CssClass="text-field" ID="txtRequestDate" ForeColor="black" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:ImageButton ID="imgCalRequest" runat="server" OnClientClick="winOpenRequestDate()"
                            ImageUrl="Images/calender_Image1.jpg" />
                    </div>
                </div>
                <div class="space-row-dv" style="float: left; margin-left: 23px; width: 500px;">
                    <div class="label-dv" style="width: 150px;">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="label-field"></asp:Label>
                    </div>
                    <div style="float: left; margin-right: 1px;">
                        <asp:TextBox ID="txtEndDate" ForeColor="black" runat="server" CssClass="text-field"></asp:TextBox>
                    </div>
                    <div style="float: left">
                        <asp:ImageButton ID="imgCalTarget" runat="server" OnClientClick="winOpenOverwriteDate()"
                            ImageUrl="Images/calender_Image1.jpg" />
                    </div>
                    <div class="btn-submit" style="width: 65px; margin-left: 30px;">
                        <asp:Button ID="btnSearch" CssClass="" runat="server" Text="Search" OnClientClick="javascript:return validatePolicyEnquiry();"
                            OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div id="dvGridView" style="margin: 50px 10px 10px 50px;">
                    <asp:GridView runat="server" ID="GrdView2" BackColor="White" BorderWidth="1px" Style="text-align: center" 
                        CellPadding="4" AllowPaging="true" Width="988px" AutoGenerateColumns="false" 
                        DataKeyNames="USER_ID" BorderStyle="Solid" BorderColor="Black" PageSize="5" ShowHeader="true"
                        Visible="false" OnPageIndexChanging="GridView2_PageIndexChanging" EmptyDataText = "No" EmptyDataRowStyle-BorderWidth="1px" EmptyDataRowStyle-BorderStyle="Solid">
                        <RowStyle BackColor="White" ForeColor="#003399"/>
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
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="REQUEST DATE" DataField="REQUEST_DATE">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                             <%--<asp:BoundField HeaderText="OVERWRITE DATE" DataField="OVERWRITE_DATE">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="USER MAILID" DataField="EMAIL_ID">
                                <ControlStyle Font-Names="Verdana" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                           <%-- <asp:BoundField HeaderText="BUSINESS REASON" DataField="BUSINESS_REASON">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
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
                            <asp:BoundField HeaderText="APPROVER ID" DataField="APPROVER_ID_ACCTNG">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="APPROVER NAME" DataField="APPROVER_NAME">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="APPROVED" DataField="APPROVAL_IND_ACCTNG">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="LOADED IN PROD" DataField="LOADED_IN_PROD_IND">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ROOT SEG CNT" DataField="ROOT_SEG_CNT">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="MACHINE SEG CNT" DataField="MACHINE_SEG_CNT">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="FACE SEG CNT" DataField="FACE_SEG_CNT">
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Font-Size="Smaller" />
                            </asp:BoundField>
                        </Columns>                      
                       <FooterStyle BackColor="#99CCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle BackColor="#99CCCC" Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
                <div class="space-row-dv">
                    <asp:Label class="infotext" ID="lblNoRecords" runat="server" Text="No Policy exists for this user."
                        Visible="False" Font-Bold="true" Font-Size="Small"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
