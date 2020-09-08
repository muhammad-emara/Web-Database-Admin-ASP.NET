<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestImage.aspx.cs" Inherits="DbMgmAdmin.TestImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form runat="server">
			<asp:datagrid id="DataGrid1" runat="server" BorderStyle="Solid" AllowPaging="True" ForeColor="Black"
				GridLines="Vertical" CellPadding="3" BackColor="White" BorderWidth="1px" BorderColor="#999999"
				Width="526px" Height="272px" PageSize="10" PagerStyle-PrevPageText="Prev" PagerStyle-NextPageText="Next"
				PagerStyle-HorizontalAlign="Right" PagerStyle-Mode="NumericPages" AutoGenerateColumns="False">
				<FooterStyle Font-Size="Small" Font-Names="Arial" Font-Bold="True" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
				<ItemStyle Font-Size="Smaller" Font-Names="Arial"></ItemStyle>
				<HeaderStyle Font-Size="Smaller" Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="Black"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Image">
						<ItemTemplate>
							<asp:Image Width="100" Height="75" ImageUrl='<%# FormatURL((int)DataBinder.Eval(Container.DataItem, "ID")) %>' Runat=server ID="Image1" NAME="Image1"/>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle NextPageText="Next" Font-Size="Small" Font-Names="Arial" Font-Bold="True" PrevPageText="Prev"
					HorizontalAlign="Right" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>&nbsp;
		</form>
</body>
</html>
