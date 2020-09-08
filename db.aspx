<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="db.aspx.cs" Inherits="DbMgmAdmin.db" %>

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
					<TD vAlign="top" width="405">
						<TABLE id="Table1" height="559" cellSpacing="0" cellPadding="0" width="572" bgColor="#ffffff"
							border="0" valign="top">
							<tr vAlign="top">
								<td colSpan="2"><IMG alt="" src="images/Admin.gif"></td>
							</tr>
							<tr>
								<td colSpan="2" height="5"><FONT face="arial" color="#006900" size="2"><%Response.Write ("Welcome: " + Session["userfullname"] + " :: <a href =logout.aspx>Logout</a>"); %></FONT><BR>
									<BR>
								</td>
							</tr>
							<TR>
								<td colSpan="2" height="10"><FONT face="arial" color="#006900" size="2"></FONT><BR>
									<H5>&nbsp;&nbsp;<A href="dbmain.aspx"><SPAN style="TEXT-DECORATION: none">
												<font face="arial" color="#006699">&lt;&lt; Back to Main Page</H5>
									</SPAN></FONT></A></td>
							</TR>
							<tr>
								<td>&nbsp;</td>
								<td vAlign="top" align="center">
									<form runat="server">
										<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR vAlign="top">
												<TD width="10" height="15"></TD>
												<TD vAlign="middle"><asp:button id="Button1" runat="server" Text="Add Record" width="95px"></asp:button><asp:textbox id="txtSQL" runat="server" Width="66%" Font-Names="Verdana"></asp:textbox><asp:button id="Button2" runat="server" Text="Run Query" Width="78px"></asp:button><br>
												</TD>
											</TR>
											<TR vAlign="top">
												<TD width="10" height="15"></TD>
												<TD vAlign="middle" align="right"><asp:label id="lblSQLError" runat="server" ToolTip="w3schools.com - Learn to write SQL queries (new window)"></asp:label>
													<!--		<br>
													<asp:label id="lblstatus" runat="server" Font-Size="XX-Small" ForeColor="Gray" Font-Names="Verdana">&nbsp;</t>VIEW ALL RECORD: </asp:label>--></TD>
											</TR>
											<TR vAlign="top">
												<TD width="10" height="15"></TD>
												<TD><asp:datagrid id="DGtable" runat="server" Width="100%" Font-Names="Verdana" AllowSorting="True"
														AllowPaging="True" ShowFooter="True" CellPadding="4" BackColor="White" BorderWidth="1px" BorderStyle="None"
														BorderColor="LightSteelBlue" Font-Size="X-Small" PageSize="5">
														<FooterStyle Font-Size="X-Small" Font-Names="Verdana" ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
														<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="GhostWhite"></SelectedItemStyle>
														<AlternatingItemStyle ForeColor="Gray" BackColor="White"></AlternatingItemStyle>
														<ItemStyle ForeColor="Gray" BackColor="White"></ItemStyle>
														<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" ForeColor="Gray" BackColor="White"></HeaderStyle>
														<PagerStyle Font-Size="X-Small" Font-Names="Arial" HorizontalAlign="Left" ForeColor="#330099"
															Position="TopAndBottom" BackColor="GhostWhite" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
											<!--<tr>
												<td></td>
												<td align="left"><asp:TextBox id="txtSQL1" runat="server" Font-Names="Verdana" Width="82%"></asp:TextBox>
													<asp:Button Width = 17% id="Button21" runat="server" Text="Run Query"></asp:Button><br>
													<asp:Label id="lblSQLError1" runat="server" ToolTip="w3schools.com - Learn to write SQL queries (new window)"></asp:Label>
												</td>
											</tr>--></TABLE>
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
</body>
</html>
