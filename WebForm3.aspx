<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm3.aspx.vb" Inherits="RetoASP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            width: 647px;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style5 {
            width: 207px;
            height: 23px;
        }
        .auto-style6 {
            width: 173px;
            height: 23px;
        }
        .auto-style7 {
            height: 23px;
        }
        .auto-style8 {
            width: 20%;
        }
        .auto-style9 {
            width: 258px;
        }
    </style>
</head>
<body>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server">
        <p>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4"><asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <DayStyle BackColor="#CCCCCC" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                <TodayDayStyle BackColor="#999999" ForeColor="White" />
            </asp:Calendar>
                    </td>
                    <td>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="249px" NextPrevFormat="ShortMonth" Width="327px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
            <DayStyle BackColor="#CCCCCC" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
            <TodayDayStyle BackColor="#999999" ForeColor="White" />
        </asp:Calendar>
                    </td>
                </tr>
            </table>
    </p>
        <p>
            &nbsp;&nbsp;&nbsp; &nbsp;</p>
        <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </p>
    <p>
        <table class="auto-style2">
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style5">
                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style6">
                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style7">
                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style7"></td>
            </tr>
        </table>
        </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Buscar" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Atras" />
        </p>
        <p>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True">
            </asp:GridView>
        </p>
    </form>
</body>
</html>
