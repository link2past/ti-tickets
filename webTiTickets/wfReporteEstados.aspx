<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfReporteEstados.aspx.cs" Inherits="webTiTickets.wfReporteEstados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TI Consulting - Reporte de Tickets por Estados</title>
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
                    <li class="dropdown" id="menuMantenimientos" runat="server">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Mantenimientos
                            <span class="caret"></span></a>
                            <ul class="dropdown-menu" role="menu">
                                <li class="dropdown-header">Personas</li>
                                <li><a href="wfListaClientes.aspx">Clientes</a></li>
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
                            <li class="active"><a href="wfReporteEstados.aspx">Reporte por Estados</a></li>
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
    <div class="container">
        <p />
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
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
        <p />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <hr />
                <h2>
                    Reporte de Tickets por Estados</h2>
                <hr />
                <div class="row">
                    <label class="col-xs-2" for="txtCliente">
                        Cliente</label>
                    <div class="col-xs-1">
                        <asp:TextBox ID="txtIdCliente" CssClass="form-control input-sm disabled" Enabled="False"
                            runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtCliente" CssClass="form-control input-sm disabled" Enabled="False"
                            runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        <asp:LinkButton ID="btnBuscarCliente" runat="server" CssClass="btn" OnClick="btnBuscarCliente_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                    </div>
                </div>
                <p />
                <div class="row">
                    <div class="col-xs-2">
                        <label>
                            Estado del Ticket</label></div>
                    <div class="col-xs-4">
                        <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control dropdown input-sm">
                        </asp:DropDownList>
                    </div>
                </div>
                <p />
                <div class="row">
                    <div class="col-xs-2">
                        <label>
                            Fecha Desde:</label></div>
                    <div class="col-xs-4">
                        <div class="input-group date">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:TextBox ID="txtFechaDesde" CssClass="form-control input-sm" data-date-format="dd/mm/yyyy"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <label>
                            Fecha Hasta:</label></div>
                    <div class="col-xs-4">
                        <div class="input-group date">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:TextBox ID="txtFechaHasta" CssClass="form-control input-sm" data-date-format="dd/mm/yyyy"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p />
                <div class="row">
                    <div class="col-xs-2">
                        <asp:Button ID="btnConsultar" CssClass="btn btn-primary" runat="server" Text="Consultar"
                            OnClick="btnConsultar_Click" />
                        <asp:Button ID="btnExcel" CssClass="btn btn-success" runat="server" Text="Excel"
                            OnClick="btnExcel_Click" />
                    </div>
                </div>
                <hr />
                <div class="container">
                    <div class="row">
                        <asp:GridView ID="gvExcel" runat="server" Visible="False" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-condensed">
                            <Columns>
                                <asp:TemplateField HeaderText="Nro Ticket">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketE" runat="server" Text='<%#Eval("NroTicket")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nro Ticket Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketClienteE" runat="server" Text='<%#Eval("Ticket.NroTicketCliente")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Ticket">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaE" runat="server" Text='<%#Eval("Ticket.FechaTicket", "{0:d}")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClienteE" runat="server" Text='<%#Eval("Ticket.Cliente.RazonSocial")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sede">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSedeE" runat="server" Text='<%#Eval("Ticket.Sede.Nombre")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unidad Negocio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnidadNegocioE" runat="server" Text='<%#Eval("Ticket.Sede.UnidadNegocio.Descripcion")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro de Costo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCentroCostoE" runat="server" Text='<%#Eval("Ticket.Sede.CentroCosto")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Titulo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTituloE" runat="server" Text='<%#Eval("Ticket.Titulo")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detalle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDetalleE" runat="server" Text='<%#Eval("Ticket.Detalle")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Solución">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolucionE" runat="server" Text='<%#Eval("Ticket.Solucion")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolucionE" runat="server" Text='<%#Eval("Ticket.EstadoTicket.Descripcion")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Categoria">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolucionE" runat="server" Text='<%#Eval("Ticket.CategoriaProblema.Descripcion")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orden Servicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdenServicioE" runat="server" Text='<%#Eval("Ticket.OrdenServicio")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tarifa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolucionE" runat="server" Text='<%#Eval("Tarifa")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Repuestos">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolucionE" runat="server" Text='<%#Eval("TotalRepuestos")%>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <asp:Repeater ID="rptTickets" runat="server" OnItemDataBound="rptTickets_ItemDataBound">
                            <HeaderTemplate>
                                <hr />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Cliente</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Ticket.Cliente.RazonSocial")%>'></asp:Label></div>
                                        <div class="col-xs-3">
                                            <label>
                                                Centro de Costo</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("Ticket.Sede.CentroCosto")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Categoría</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="lblCategoria" runat="server" Text='<%#Eval("Ticket.CategoriaProblema.Descripcion")%>'></asp:Label></div>
                                        <div class="col-xs-3">
                                            <label>
                                                Sede</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="lblSede" runat="server" Text='<%#Eval("Ticket.Sede.Nombre")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Ticket</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="lblTicket" runat="server" Text='<%#Eval("NroTicket")%>'></asp:Label></div>
                                        <div class="col-xs-3">
                                            <label>
                                                Ticket de Cliente</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Ticket.NroTicketCliente")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Fecha de Registro</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="lblFechaRegistro" runat="server" Text='<%#Eval("Ticket.FechaTicket", "{0:d}")%>'></asp:Label></div>
                                        <div class="col-xs-3">
                                            <label>
                                                Estado</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Ticket.EstadoTicket.Descripcion")%>'></asp:Label></div>
                                    </div>
                                   <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Orden de Servicio</label></div>
                                        <div class="col-xs-3">
                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("Ticket.OrdenServicio")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Detalle</label></div>
                                        <div class="col-xs-6">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Ticket.Detalle")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Solucion</label></div>
                                        <div class="col-xs-6">
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Ticket.Solucion")%>'></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <asp:GridView ID="gvRepuestos" CssClass="table table-striped table-bordered table-condensed"
                                            runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Repuesto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRepuesto" runat="server" Text='<%#Eval("DetalleTicket.Repuesto.Descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("DetalleTicket.Cantidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Total Repuestos</label></div>
                                        <div class="col-xs-2">
                                            <asp:Label ID="lblTotalRepuesto" runat="server" Text='<%#Eval("TotalRepuestos")%>'
                                                CssClass="badge"></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>
                                                Tarifa</label></div>
                                        <div class="col-xs-2">
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Tarifa")%>' CssClass="badge"></asp:Label></div>
                                    </div>
                                    <hr />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <p />
                        <h2>
                            Total por Estado</h2>
                        <hr />
                        <asp:GridView ID="gvResultadoEstado" CssClass="table table-striped table-bordered table-condensed"
                            runat="server">
                        </asp:GridView>
                        <p />
                        <h2>
                            Total por Cliente</h2>
                        <hr />
                        <asp:GridView ID="gvResultadoCliente" CssClass="table table-striped table-bordered table-condensed"
                            runat="server">
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <!--Modal Consulta de Cliente-->
        <fieldset>
            <div id="modalConsultaCliente" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblConsultaCliente"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblConsultaCliente" class="modal-title">
                                Consulta de Clientes</h3>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <asp:TextBox ID="txtRazonSocialCliente" CssClass="form-control input-sm" placeholder="Indique la razón social del cliente a buscar..."
                                            runat="server"></asp:TextBox>
                                        <asp:Button ID="btnConsultarCliente" runat="server" CssClass="btn btn-default" Text="Consultar"
                                            OnClick="btnConsultarCliente_Click" />
                                    </div>
                                    <br />
                                    <asp:GridView ID="gvClientes" CssClass="table table-striped table-bordered table-condensed"
                                        runat="server" AutoGenerateColumns="False" DataKeyNames="IdCliente" OnRowCommand="gvClientes_RowCommand">
                                        <Columns>
                                            <asp:ButtonField CommandName="Seleccionar" ButtonType="Button" Text="Sel." ControlStyle-CssClass="btn btn-small btn-link">
                                                <ControlStyle CssClass="btn btn-small btn-link" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="IdCliente" HeaderText="Id Cliente" ReadOnly="True" />
                                            <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <input id="btnCancelarCliente" class="btn" type="button" data-dismiss="modal" value="Salir" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    </form>
    <!--Pie-->
    <footer>
        <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <hr/>
                <p/>TI Consulting - 2015        </div>
        </div>
        </div>
    </footer>
</body>
<script src="bootstrap/js/jquery-1.11.2.js" type="text/javascript"></script>
<!--script src="bootstrap/js/bootstrap.js" type="text/javascript"></script-->
<script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
<script>
    function pageLoad() {
        $(function () {
            window.prettyPrint && prettyPrint();
            $('#txtFechaDesde').datepicker().on('changeDate', function (e) { $(this).datepicker('hide'); });
            $('#txtFechaHasta').datepicker().on('changeDate', function (e) { $(this).datepicker('hide'); });
        });
    }

    function ToTopOfPage(sender, args) {
        setTimeout("window.scrollTo(0, 0)", 0);
    }
</script>
</html>
