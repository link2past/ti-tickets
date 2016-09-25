<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webTiTickets.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TI Consulting - Mesa de Ayuda</title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/docs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background: url(./bootstrap/img/FondoLogin.png) no-repeat center center fixed;
            -webkit-background-size: contain;
            -moz-background-size: contain;
            -o-background-size: contain;
            background-size: contain;
        }
        
        .panel-default
        {
            opacity: 0.9;
            margin-top: 30px;
        }
        .form-group.last
        {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <!--Cuerpo-->
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-7">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon-lock"></span>Ingrese sus Credenciales
                    </div>
                    <div class="panel-body">
                        <form id="form1" runat="server" class="form-signin">
                        <div class="form-group">
                            <asp:TextBox ID="txtUsuario" CssClass="form-control" placeholder="Usuario" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Contraseña" TextMode="Password"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group last">
                            <asp:Button ID="btnIngresar" CssClass="btn btn-lg btn-primary" runat="server" Text="Ingresar"
                                OnClick="btnIngresar_Click" />
                        </div>
                        <div class="alert alert-success alert-dismissible" id="alertaExito" role="alert"
                            runat="server" visible="False">
                            <button type="button" class="close" data-dismiss="alert">
                                &times;</button>
                            <asp:Label ID="lblExito" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-danger alert-dismissible" id="alertaError" role="alert" runat="server"
                            visible="False">
                            <button type="button" class="close" data-dismiss="alert">
                                &times;</button>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Pie-->
    <!--div id="footer">
        <div class="container">
            <p class="muted credit">
                TI Consulting - 2015</p>
        </div>
    </div-->
</body>
</html>
