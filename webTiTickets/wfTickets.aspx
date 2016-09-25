<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfTickets.aspx.cs" Inherits="webTiTickets.wfTickets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TI Consulting - Tickets</title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/docs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
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
                    <li class="active"><a href="wfListaTickets.aspx">Listado de Tickets</a></li>
                    <li class="dropdown" id="mnuMantenimiento" runat="server" Visible="False">
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
                                <a href="javascrip:void(0);">Perfil</a>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
            <ContentTemplate>
                <p />
                <h4>
                    Registro de Tickets</h4>
                <hr />
                <div class="container">
                    <div class="row">
                        <label class="col-xs-2" for="txtNroTicket">
                            Nro. Ticket</label>
                        <div class="col-xs-1">
                            <asp:TextBox ID="txtNroTicket" CssClass="form-control input-sm disabled" Enabled="False"
                                runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hfNuevo" runat="server" />
                            <asp:HiddenField ID="hfTipoUsuario" runat="server" />
                            <asp:HiddenField ID="hfMailDestinatario" runat="server" />
                        </div>
                    </div>
                    <p />
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
                                Unidad de Negocio</label></div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="cboUnidadNegocio" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True" OnSelectedIndexChanged="cboUnidadNegocio_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <label>
                                Sede del Cliente</label></div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="cboSede" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True" OnSelectedIndexChanged="cboSede_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="txtDireccionSede">
                            Dirección</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtDireccionSede" CssClass="form-control input-sm" runat="server"
                                Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            <label for="txtContactoSede">
                                Contacto de la Sede</label></div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtContactoSede" CssClass="form-control input-sm" runat="server"
                                Enabled="False"></asp:TextBox>
                        </div>
                        <div class="col-xs-2">
                            <label for="cboCategoria">
                                Usuario Atención</label></div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="cboUsuarioAtencion" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="cboCategoria">
                            Categoría</label>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboCategoria" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="cboNivelUrgencia">
                            Nivel de Urgencia</label>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboNivelUrgencia" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            <label for="cboEstadoTicket">
                                Estado</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="cboEstadoTicket" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <label for="txtTecnicoAsignado">
                                Técnico Asignado</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtTecnicoAsignado" CssClass="form-control input-sm" runat="server"
                                Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="txtTicketCliente">
                            Nro. Ticket del Cliente</label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtTicketCliente" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-xs-2" for="txtFechaTicket">
                            Fecha Ticket</label>
                        <div class="col-xs-3 input-group date">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:TextBox ID="txtFechaTicket" CssClass="form-control input-sm" runat="server"
                                data-date-format="dd/mm/yyyy" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="txtTitulo">
                            Título</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtTitulo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <label class="col-xs-2" for="txtDetalle">
                            Detalle</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtDetalle" MaxLength="500" CssClass="form-control input-sm" TextMode="MultiLine"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row" id="rowSolucion" runat="server">
                        <label class="col-xs-2" for="txtSolucion">
                            Solución</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtSolucion" MaxLength="500" CssClass="form-control input-sm" TextMode="MultiLine"
                                runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row" id="rowObservaciones" runat="server">
                        <label class="col-xs-2" for="txtObservaciones">
                            Observaciones</label>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtObservaciones" MaxLength="500" CssClass="form-control input-sm"
                                TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <p />
                    <div class="row" id="rowOrdenServicio" runat="server">
                        <label class="col-xs-2" for="txtOrdenServicio">
                            Orden de Servicio</label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtOrdenServicio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-xs-2" for="chkCostoCero">
                            No Afecto a la Tarifa</label>
                        <div class="col-xs-3">
                            <asp:CheckBox ID="chkCostoCero" CssClass="checkbox" Enabled="False" runat="server" />
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="btn-group">
                            <asp:Button ID="btnNuevo" CssClass="btn btn-default" runat="server" Text="Nuevo"
                                OnClick="btnNuevo_Click" />
                            <asp:Button ID="btnGrabar" CssClass="btn btn-primary" runat="server" Text="Grabar Ticket"
                                OnClick="btnGrabar_Click" />
                            <asp:Button ID="btnAsignar" runat="server" Text="Asignar Ticket" CssClass="btn btn-info"
                                OnClick="btnAsignar_Click" />
                            <asp:Button ID="btnAtender" runat="server" Text="Ticket Atendido" CssClass="btn btn-warning"
                                OnClick="btnAtender_Click" />
                            <asp:Button ID="btnCerrar" runat="server" Text="Ticket Cerrado" CssClass="btn btn-success"
                                OnClick="btnCerrar_Click" />
                            <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar"
                                OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="btn-group">
                            <asp:Button ID="btnEsperaRepuesto" runat="server" Text="En espera de repuesto" CssClass="btn btn-danger"
                                OnClick="btnEsperaRepuesto_Click" />
                            <asp:Button ID="btnReabrirTicket" runat="server" Text="Reabrir Ticket" CssClass="btn btn-info"
                                OnClick="btnReabrirTicket_Click" />
                            <asp:Button ID="btnAnular" runat="server" Text="Anular Ticket" CssClass="btn btn-danger"
                                OnClick="btnAnular_Click" />
                            <asp:Button ID="btnAfectoTarifa" runat="server" Text="Afectación de Tarifa" CssClass="btn btn-warning"
                                OnClick="btnAfectoTarifa_Click" />
                        </div>
                    </div>
                    <div class="row">
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
                    </div>
                </div>
                <p />
                <hr />
                <h4 id="tituloRepuesto" runat="server">
                    Repuestos utilizados
                </h4>
                <p />
                <div class="row">
                    <asp:Button ID="btnAgregarRepuesto" CssClass="btn btn-primary" runat="server" Text="Agregar Repuesto"
                        OnClick="btnAgregarRepuesto_Click" />
                </div>
                <asp:GridView ID="gvRepuestos" CssClass="table table-striped table-bordered table-condensed"
                    AutoGenerateColumns="False" runat="server" DataKeyNames="IdRepuesto" OnRowCommand="gvRepuestos_RowCommand">
                    <Columns>
                        <asp:ButtonField CommandName="Eliminar" ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-xs btn-link"
                            HeaderStyle-Width="5px">
                            <ControlStyle CssClass="btn btn-xs btn-link" />
                            <HeaderStyle Width="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle Width="5px" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="IdRepuesto" HeaderText="Id Repuesto" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblRepuesto" runat="server" Text='<%#Eval("Repuesto.Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:Label ID="lblCantidadG" runat="server" Text='<%#Eval("Cantidad") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Moneda">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioUnitarioG" runat="server" Text='<%#Eval("IdMoneda") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Unitario">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioUnitarioG" runat="server" Text='<%#Eval("Precio") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <p />
                <hr />
                <h4>
                    Historial del Ticket</h4>
                <hr />
                <div class="row">
                    <asp:Button ID="btnCambiarHoraRegistro" runat="server" Text="Cambiar Hora de Registro de Ticket"
                        CssClass="btn btn-info" OnClick="btnCambiarHoraRegistro_Click" />
                </div>
                <p />
                <asp:GridView ID="gvRegistros" CssClass="table table-striped table-bordered table-condensed"
                    AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:BoundField DataField="NroTicket" HeaderText="Nro. Ticket" ReadOnly="True" />
                        <asp:BoundField DataField="IdEstadoTicket" HeaderText="Id Estado" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Nro. Ticket">
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoTicketG" runat="server" Text='<%#Eval("EstadoTicket.Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Usuario Registro">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuarioRegistroG" runat="server" Text='<%#Eval("Usuario.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdUsuarioAsignado" HeaderText="Técnico Asignado" ReadOnly="True" />
                        <asp:BoundField DataField="Observacion" HeaderText="Observaciones" ReadOnly="True" />
                        <asp:BoundField DataField="FechaHoraRegistro" HeaderText="Fecha Registro" ReadOnly="True"
                            HeaderStyle-Width="15px" DataFormatString="{0:g}" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--Modal Agregar Repuesto-->
        <fieldset>
            <div id="modalAsignarRepuesto" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblAsignarRepuesto"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblAsignarRepuesto" class="modal-title">
                                Asignar Asignar Repuesto a Ticket</h3>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <asp:TextBox ID="txtRepuestoNombre" CssClass="form-control input-sm" placeholder="Indique el nombre del repuesto a buscar..."
                                            runat="server"></asp:TextBox>
                                        <asp:Button ID="btnBuscarRepuesto" runat="server" CssClass="btn btn-default" Text="Consultar"
                                            OnClick="btnBuscarRepuesto_Click" />
                                    </div>
                                    <p />
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>
                                                Cantidad</label>
                                        </div>
                                        <div class="col-xs-2">
                                            <asp:TextBox ID="txtCantidadRepuesto" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:GridView ID="gvRepuestosBuscar" CssClass="table table-striped table-bordered table-condensed"
                                        runat="server" AutoGenerateColumns="False" DataKeyNames="IdRepuesto" runat="server"
                                        OnRowCommand="gvRepuestosBuscar_RowCommand">
                                        <Columns>
                                            <asp:ButtonField CommandName="Seleccionar" ButtonType="Button" Text="Sel." ControlStyle-CssClass="btn btn-small btn-link">
                                                <ControlStyle CssClass="btn btn-small btn-link" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="IdRepuesto" HeaderText="Id Usuario" ReadOnly="True" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="True" />
                                            <asp:BoundField DataField="IdMoneda" HeaderText="Moneda" ReadOnly="True" />
                                            <asp:BoundField DataField="PrecioActual" HeaderText="Precio Actual" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
        <!--Modal Asignación de Técnico-->
        <fieldset>
            <div id="modalAsignarTecnico" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblAsignarTecnico"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblAsignarTecnico" class="modal-title">
                                Asignar TéAsignar Técnico a Ticket</h3>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <asp:TextBox ID="txtTecnicoNombre" CssClass="form-control input-sm" placeholder="Indique el nombre del técnico a buscar..."
                                            runat="server"></asp:TextBox>
                                        <asp:Button ID="btnConsultarTecnico" runat="server" CssClass="btn btn-default" Text="Consultar"
                                            OnClick="btnConsultarTecnico_Click" />
                                    </div>
                                    <br />
                                    <asp:GridView ID="gvTecnicos" CssClass="table table-striped table-bordered table-condensed"
                                        runat="server" AutoGenerateColumns="False" DataKeyNames="Usuario" OnRowCommand="gvTecnicos_RowCommand"
                                        EmptyDataText="No hay técnicos disponibles en este momento">
                                        <Columns>
                                            <asp:ButtonField CommandName="Seleccionar" ButtonType="Button" Text="Sel." ControlStyle-CssClass="btn btn-small btn-link">
                                                <ControlStyle CssClass="btn btn-small btn-link" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="Usuario" HeaderText="Id Técnico" ReadOnly="True" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Técnico" ReadOnly="True" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="Label1" runat="server" Text="El técnico seleccionado será asignado al ticket"
                                CssClass="label label-warning"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <input id="btnCancelarTecnico" class="btn" type="button" data-dismiss="modal" value="Salir" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <!--Modal Confirmación-->
        <fieldset>
            <div id="modalConfirmacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblConfirmacion"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblConfirmacion" class="modal-title">
                                Cambiar estado de Ticket</h3>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMensaje" runat="server" Text="¿Está seguro de cambiar el estado del Ticket?"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSiGrabar" runat="server" Text="Si" CssClass="btn btn-primary"
                                OnClick="btnSiGrabar_Click" />
                            <input id="btnNoGrabar" class="btn" type="button" data-dismiss="modal" value="No" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <!--Modal Confirmación-->
        <fieldset>
            <div id="modalConfirmaTarifa" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblConfirmacionT"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblConfirmacionT" class="modal-title">
                                Ticket Afecto a la Tarifa Regular</h3>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label2" runat="server" Text="¿Está seguro de cambiar si el Ticket estará afecto a la tarifa regular o no?"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSiGrabarTarifa" runat="server" Text="Si" CssClass="btn btn-primary"
                                OnClick="btnSiGrabarTarifa_Click" />
                            <input id="Button2" class="btn" type="button" data-dismiss="modal" value="No" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <!--Modal Reabrir Ticket-->
        <fieldset>
            <div id="modalReabrirTicket" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblReabrir"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="mlblReabrir" class="modal-title">
                                ¿Está seguro de volver a abrir el Ticket?</h3>
                        </div>
                        <div class="modal-body">
                            <span class="badge">Por favor ingrese el motivo de la re-apertura del ticket</span>
                            <p />
                            <p />
                            <asp:TextBox ID="txtMotivoReapertura" runat="server" MaxLength="100" CssClass="form-control input-sm"
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSiReAbrir" runat="server" Text="Si" CssClass="btn btn-primary"
                                OnClick="btnSiReAbrir_Click" />
                            <input id="btnNoReAbrir" class="btn" type="button" data-dismiss="modal" value="No" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <!--Modal Cambio de Hora de Registro-->
        <fieldset>
            <div id="modalHoraTicket" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mlblHoraTicket"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 id="H1" class="modal-title">
                                Cambio de Hora de registro del Ticket</h3>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <label class="col-xs-3" for="txtFechaTicketC">
                                    Fecha Ticket</label>
                                <div class="col-xs-3 date">
                                    <asp:TextBox ID="txtFechaTicketC" CssClass="form-control input-sm" runat="server"
                                        data-date-format="dd/mm/yyyy" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                            <p />
                            <p />
                            <span class="badge">Por favor indique la nueva hora de ingreso del ticket</span>
                            <p />
                            <div class="row">
                                <div class="col-xs-3">
                                    <label>
                                        Hora</label></div>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="cboHora" runat="server" CssClass="form-control dropdown input-sm"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2">
                                    <label>
                                        Minutos</label></div>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="cboMinutos" runat="server" CssClass="form-control dropdown input-sm"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnGrabarHora" runat="server" Text="Grabar" 
                                CssClass="btn btn-primary" onclick="btnGrabarHora_Click" />
                            <input id="btnCancelarHora" class="btn" type="button" data-dismiss="modal" value="Cancelar" />
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
                <p>TI Consulting - 2015</div>
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
            $('#txtFechaTicket').datepicker().on('changeDate', function (e) { $(this).datepicker('hide'); });
        });
    }

    function ToTopOfPage(sender, args) {
        setTimeout("window.scrollTo(0, 0)", 0);
    }
</script>
</html>
