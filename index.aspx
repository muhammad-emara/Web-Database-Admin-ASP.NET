<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DbMgmAdmin.FileBrowser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.DataBase Management Admin:. - Welcome </title>

    <%
if (Session["Admin"] == "true")
{
Response.Redirect("dbmain.aspx");
}
else{
 }
%>
</head>
<body>
    <center>
			<script language="javascript">
	function validate_length1(oSrc, args)
	{
		args.Isvalid=true;
		if(args.Value)
		{
			var length = args.Value.length;
			if( length<4)
			{
				args.IsValid=false;
				
			}
		}
	}</script>
			<TABLE cellSpacing="0" cellPadding="0" width="798" border="0" bgColor="white">
				<TR>
					<TD width="605" colSpan="2">
						<!--include file="includes/top.htm"--></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="405">
						<TABLE border="1" id="Table1" height="300" cellSpacing="0" cellPadding="0" width="100%"
							bordercolorlight="#ffffff" bordercolordark="#999999" bgColor="white" valign="top">
							<tr vAlign="top" height="47" width="405">
								<td><IMG alt="" src="images/Admin.gif"></td>
							</tr>
							<tr>
								<td vAlign="top" align="center">
									<form runat="server">
										<TABLE id="Table1" height="112" cellSpacing="1" cellPadding="1" border="0">
											<TR>
												<TD colSpan="3">
													&nbsp;</TD>
											</TR>
											<TR>
												<TD colSpan="3">
													<asp:Label id="lblError" runat="server" Font-Names="Arial" Font-Size="Smaller" ForeColor="Red"></asp:Label></TD>
											</TR>
											<TR>
												<TD width="71"><asp:label id="EmailID" runat="server" Font-Size="10pt" Font-Names="Arial" Width="88px">Username</asp:label></TD>
												<TD width="151"><asp:textbox id="txtemail" runat="server" Width="152px"></asp:textbox></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD width="71"><asp:label id="Password" runat="server" Font-Size="10pt" Font-Names="Arial">Password</asp:label></TD>
												<TD width="151"><asp:textbox id="txtpwd" runat="server" Width="152px" TextMode="Password"></asp:textbox></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD width="71"></TD>
												<TD width="151"><asp:button id="Log" runat="server" Text="Login"></asp:button>&nbsp;&nbsp;
													<asp:button id="btnLogin" runat="server" Text="Reset"></asp:button></TD>
												<TD></TD>
											</TR>
											<TR>
												<td></td>
												<TD colSpan="2"><FONT face="Verdana" size="1"><EM> <a href="ForgotPwd.aspx">
																<span style="TEXT-DECORATION: none">
																	<FONT color="#ff3300"></FONT></a></SPAN></EM></FONT></TD>
											</TR>
											
											<TR>
												
												<TD colSpan="3"><FONT face="Arial" size="2">&nbsp</TD>
											</TR>
											
										</TABLE>
										
									</form>
								</td>
							</tr>
							<tr>
								<td colspan="2"><!--#include file="includes/bot.htm"--></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</center>
</body>
</html>
