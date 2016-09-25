<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaEnvioMail.aspx.cs"
    Inherits="webTiTickets.PruebaEnvioMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Destino"></asp:Label>
        <asp:TextBox ID="txtDestino" runat="server" Width="280px"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Asunto"></asp:Label>
        <asp:TextBox ID="txtAsunto" runat="server" Width="281px"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Cuerpo"></asp:Label>
        <asp:TextBox ID="txtCuerpo" runat="server" Width="275px"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
    </div>
    <div>
        <asp:TextBox ID="txtMensaje" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
