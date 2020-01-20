<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ModificarReserva.aspx.vb" Inherits="RetoASP.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
        </div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <p>
            &nbsp;</p>
        <asp:GridView ID="GridView3" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
