<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="Detyra_2.UserRegister" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" method="post" runat="server">
        
		    <h2 class="text-center">Regjistrohu</h2>
           
                                                         
                                                    <asp:Label ID="LblEmer" runat="server" Text="Emri:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="Emri" runat="server" placeholder="Vendosni emrin"></asp:TextBox>
                        
                                                            <br />

                        <asp:Label ID="LblEmail" runat="server" Text="Email:" Font-Bold="true"/>

                         <asp:TextBox ID="Email" runat="server" placeholder="emer.mbiemer@gmail.com"></asp:TextBox>                        
                                                    <br />
        <asp:Label ID="Label2" runat="server" Text="Menyra e abonimit:" Font-Bold="true"></asp:Label>
        <asp:DropDownList ID="DdlSubscribe" runat="server" OnSelectedIndexChanged="DdlSubscribe_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem>--Select--</asp:ListItem>
            <asp:ListItem Value="0">Fjale kyce</asp:ListItem>
            <asp:ListItem Value="1">Kategori Lajmesh</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="false">
        <asp:Label ID="Label1" runat="server" Text="Vendosni fjalet kyce te ndara me presje:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="FK" runat="server" placeholder="fjale1,fjale2"></asp:TextBox>
            </asp:Panel>
       
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <asp:Label ID="Label3" runat="server" Text="Perzgjidhni kategorine:" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="DdlKategorite" runat="server">
                <asp:ListItem>--Select</asp:ListItem>
                <asp:ListItem>Business</asp:ListItem>
                <asp:ListItem>Entertainment</asp:ListItem>
                <asp:ListItem>Health</asp:ListItem>
                <asp:ListItem>Science</asp:ListItem>
                <asp:ListItem>Sports</asp:ListItem>
                <asp:ListItem>Technology</asp:ListItem>
            </asp:DropDownList>
        </asp:Panel>
        <br />
        <asp:Button ID="Regjistrohu" runat="server" Text="Regjistrohu" OnClick="Regjistrohu_Click" />
        <br />
        <asp:Button ID="Dergo" runat="server" Text="Dergo" OnClick="Dergo_Click" />
        <br />
        <asp:Label ID="Rezultati" runat="server" ></asp:Label>
    </form>
</body>
</html>
