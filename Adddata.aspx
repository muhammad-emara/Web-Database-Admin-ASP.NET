﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adddata.aspx.cs" Inherits="DbMgmAdmin.Adddata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%
if (Session["Admin"] == "true")
{
}
else{
Response.Redirect("index.aspx");

 }
%>

</head>
<body>
    <center>
			<TABLE cellSpacing="0" cellPadding="0" width="798" bgColor="#ffffff" border="0">
				<TR>
					<TD vAlign="top" bgColor="#ffffff" rowSpan="2">
					</TD>
					<TD width="605" colSpan="2">
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" width="405">
						<TABLE id="Table1" height="407" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff"
							border="0" valign="top">
							<tr vAlign="top">
								<td colSpan="2"><IMG alt="" src="images/Admin.gif"></td>
							</tr>
							<tr>
								<td height="5" colspan="2"><font size="2" color="#00900a"><%Response.Write ("Welcome: " + Session["userfullname"] + " :: <a href =logout.aspx>Logout</a>"); %></font><br>
									<br>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="10"><font color="#006699" size="2" face="arial"></font><br>
									<h5>&nbsp;&nbsp;<A href="dbmain.aspx"><SPAN style="TEXT-DECORATION: none"><FONT face="arial" color="#006699">&lt;&lt; 
										Back to Main Page</h5>
									</SPAN></FONT></A></td>
							</tr>
							<tr>
								<td width="4%"></td>
								<td vAlign="top" align="center">
									<form id="frmViewState" action="test.aspx" method="post" enctype="multipart/form-data"
										runat="server">
										<TABLE height="85" cellSpacing="0" cellPadding="0" width="372" border="0" ms_2d_layout="TRUE">
											<TR vAlign="top">
												<TD width="10" height="15"></TD>
												<TD vAlign="middle"><asp:button id="Button1" runat="server" Text="<- Back to List"></asp:button>&nbsp;<asp:label id="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#004000"
														Font-Size="Smaller"></t>ADD RECORD: Ready </asp:label><br>
													<br>
												</TD>
											</TR>
											<TR vAlign="top">
												<TD height="15"></TD>
												<TD><asp:table id="Table2" runat="server" Font-Size="8pt" CellSpacing="0" CellPadding="1" Font-Name="Verdana"
														HorizontalAlign="Center" GridLines="Both" height="45" border="1" width="361"></asp:table></TD>
											</TR>
											<TR vAlign="top">
												<TD height="15"></TD>
												<TD></TD>
											</TR>
											<TR vAlign="top">
												<TD height="25"></TD>
												<TD><asp:button id="btnBack" runat="server" Text="<- Back to List"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:button id="btnSubmit" runat="server" text="Add Data"></asp:button></TD>
											</TR>
										</TABLE>
									</form>
								</td>
							</tr>
							<tr>
								<td colSpan="2"><!--#include file="includes/bot.htm"--></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</center>
	</BODY>
</body>
</html>
