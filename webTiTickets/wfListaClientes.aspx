<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfListaClientes.aspx.cs"
    Inherits="webTiTickets.wfListaClientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TI Consulting - Listado de Clientes</title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/docs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!--Cabecera-->
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                  <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </button>
                  <a class="navbar-brand" href="#">TI Consulting - Mesa de Ayuda</a>
              </div>
            <div id="navbar" class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li><a href="wfListaTickets.aspx">Listado de Tickets</a></li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Mantenimientos
                            <span class="caret"></span></a>
                            <ul class="dropdown-menu" role="menu">
                                <li class="dropdown-header">Personas</li>
                                <li class="active"><a href="wfListaClientes.aspx">Clientes</a></li>
                                <li><a href="wfListaUsuarios.aspx">Usuarios</a></li>
                                <li class="dropdown-header">Inventarios</li>
                                <li><a href="wfListaRepuestos.aspx">Repuestos</a></li>
                                <li class="dropdown-header">Tablas</li>
                                <li><a href="wfMantCategoria.aspx">Categorías de Problema</a></li>
                                <li><a href="wfMantEstadoTicket.aspx">Estados de Ticket</a></li>
                                <li><a href="wfMantNivelUrgencia.aspx">Niveles de Urgencia</a></li>
                                <li><a href="wfMantTipoUsuario.aspx">Tipos de Usuario</a></li>
                                <li><a href="wfMantUnidadNegocio.aspx">Unidades de Negocio</a></li>
                                <li><a href="wfMantAreaUsuarioSede.aspx">Areas de Usuarios de Sede</a></li>
                            </ul>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Reportes
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="wfReporteClienteUn.aspx">Reporte por Cliente y Unidad de Negocio</a></li>
                            <li><a href="wfReporteCategorias.aspx">Reporte por Categorías</a></li>
                            <li><a href="wfReporteEstados.aspx">Reporte por Estados</a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li class="dropdown">
                        <a href="#" data-toggle="dropdown">
                            <span class="glyphicon glyphicon-user"></span>
                            <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
                            <b class="caret"></b>
                        </a> 
                        <ul class="dropdown-menu">
                            <li>
                                <a href="wfPerfil.aspx">Perfil</a>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <asp:LinkButton ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" 
                                    onclick="btnCerrarSesion_Click"></asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <!--Cuerpo-->
    <p />
    <p />
    <div class="container">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-success" id="alertaExito" runat="server" visible="False">
                        <button type="button" class="close" data-dismiss="alert">
                            &times;</button>
                        <asp:Label ID="lblExito" runat="server"></asp:Label>
                    </div>
                    <div class="alert alert-danger" id="alertaError" runat="server" visible="False">
                        <button type="button" class="close" data-dismiss="alert">
                            &times;</button>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <hr />
                    <h2>
                        Listado de Clientes</h2>
                    <hr />
                    <h4>
                        Búsqueda</h4>
                    <div class="row">
                        <div class="col-xs-2">
                            Razón Social:</div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtRazonSocial" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-2">
                            Nro. DI:</div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtNroDi" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            Departamento</div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cboDepartamento" runat="server" 
                                CssClass="form-control dropdown input-sm" AutoPostBack="True" 
                                onselectedindexchanged="cboDepartamento_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            Provincia</div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cboProvincia" runat="server" 
                                CssClass="form-control dropdown input-sm" AutoPostBack="True" 
                                onselectedindexchanged="cboProvincia_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            Distrito</div>
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cboDistrito" runat="server" CssClass="form-control dropdown input-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            Estado</div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control dropdown input-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p/>
                    <div class="row">
                        <div class="btn-group">
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Cliente" CssClass="btn btn-info"
                                OnClick="btnNuevo_Click" />
                            <asp:Button ID="btnConsultar" CssClass="btn btn-primary" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />
                        </div>
                    </div>
                    <hr />
                    <asp:GridView ID="gvClientes" CssClass="table table-striped table-bordered table-condensed"
                        AutoGenerateColumns="False" runat="server" DataKeyNames="IdCliente" 
                        onrowcommand="gvClientes_RowCommand">
                        <Columns>
                            <asp:ButtonField CommandName="Seleccionar" ButtonType="Button" Text="Sel." ControlStyle-CssClass="btn btn-small btn-link">
                                <ControlStyle CssClass="btn btn-small btn-link" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="IdCliente" HeaderText="Id Cliente" ReadOnly="True" />
                            <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" ReadOnly="True" />
                            <asp:BoundField DataField="NroDi" HeaderText="Nro. DI" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Departamento">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartamentoG" runat="server" Text='<%#Eval("Departamento.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provincia">
                                <ItemTemplate>
                                    <asp:Label ID="lblProvinciaG" runat="server" Text='<%#Eval("Provincia.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Distrito">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistritoG" runat="server" Text='<%#Eval("Distrito.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoG" runat="server" Text='<%#Eval("Estado.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
    <!--Pie-->
    <footer>
        <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <hr/>
                <p>TI Consulting - 2015</p>
            </div>
        </div>
        </div>
    </footer>
</body>
<script src="bootstrap/js/jquery-1.11.2.js" type="text/javascript"></script>
<!--script src="bootstrap/js/bootstrap.js" type="text/javascript"></script-->
<script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
<script>
    function ToTopOfPage(sender, args) {
        setTimeout("window.scrollTo(0, 0)", 0);
    }
</script>
</html>
