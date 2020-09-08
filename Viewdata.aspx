<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewdata.aspx.cs" Inherits="DbMgmAdmin.Viewdata" %>

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
<SCRIPT language="JavaScript1.2">
function openwindow(s)
{
	window.open(s,"mywindow","menubar=0,resizable=1");
}
</SCRIPT>

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
								<td height="5" colspan="2"><font size="2" color="#006699="arial"><%Response.Write ("Welcome: " + Session["userfullname"] + " :: <a href =logout.aspx>Logout</a>"); %></font><br>
									<br>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="10"><font size="2" color="#006699="arial"></font><br>
									<h5>&nbsp;&nbsp;<A href="dbmain.aspx"><SPAN style="TEXT-DECORATION: none"><FONT face="arial" color="#006699">&lt;&lt; 
										Back to Main Page</h5>
									</SPAN></FONT></A></td>
							</tr>
							<tr>
								<td width="4%"></td>
								<td vAlign="top" align="center">
									<form id="frmViewState" action="ViewData.aspx" method="post" runat="server">
										<TABLE height="85" cellSpacing="0" cellPadding="0" width="372" border="0" ms_2d_layout="TRUE">
											<TR vAlign="top">
												<TD width="10" height="15"></TD>
												<TD width="362" valign="middle">
													<asp:Button id="Button1" runat="server" Text="<- Back to List"></asp:Button><asp:Label id="lblstatus" runat="server" Font-Size="Smaller" ForeColor="#004000" Font-Names="Verdana"
														Font-Bold="True">&nbsp;</t>VIEW RECORD: Ready </asp:Label><br>
													<br>
												</TD>
											</TR>
											<TR vAlign="top">
												<TD height="15"></TD>
												<TD><asp:table id="Table2" width="361" runat="server" border="1" height="45" GridLines="Both" HorizontalAlign="Center"
														Font-Name="Verdana" Font-Size="8pt" CellPadding="1" CellSpacing="0"></asp:table></TD>
											</TR>
											<TR vAlign="top">
												<TD height="15"></TD>
												<TD></TD>
											</TR>
											<TR vAlign="top">
												<TD height="25"></TD>
												<TD><asp:Button id="btnBack" runat="server" Text="<- Back to List"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:button id="btnSubmit" runat="server" text="Update Data"></asp:button>&nbsp;&nbsp;&nbsp;
													<asp:button id="Button2" runat="server" text="Delete Data"></asp:button>
												</TD>
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
