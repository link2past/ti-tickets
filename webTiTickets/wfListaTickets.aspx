<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfListaTickets.aspx.cs"
    Inherits="webTiTickets.wfListaTickets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TI Consulting - Listado de Tickets</title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/docs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table
        {
            font-size: 12px;
        }
    </style>
    <script src="bootstrap/js/bs.pagination.js" type="text/javascript"></script>
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
        <p />
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <hr />
                    <h2>
                        Cliente</h2>
                    <hr />
                    <div class="row">
                        <div class="col-xs-2">
                            Usuario</div>
                        <div class="col-xs-4">
                            <asp:Label ID="lblUsuarioNombre" runat="server" Text="" CssClass="text-info"></asp:Label>
                            <asp:HiddenField ID="hfIdTipoUsuario" runat="server" />
                        </div>
                        <div class="col-xs-2">
                            Cliente</div>
                        <div class="col-xs-4">
                            <asp:HiddenField ID="hfIdCliente" runat="server" />
                            <asp:Label ID="lblClienteNombre" runat="server" Text="" CssClass="text-info"></asp:Label>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            <asp:Button ID="btnNuevoTicket" CssClass="btn btn-primary" runat="server" Text="Registrar Nuevo Ticket"
                                OnClick="btnNuevoTicket_Click" />
                        </div>
                    </div>
                    <hr />
                    <h2>
                        Listado de Tickets</h2>
                    <hr />
                    <h4>
                        Búsqueda</h4>
                    <div class="row">
                        <div class="col-xs-2">
                            Fecha Desde:</div>
                        <div class="col-xs-4">
                            <div class="input-group date">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:TextBox ID="txtFechaDesde" CssClass="form-control input-sm" data-date-format="dd/mm/yyyy"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            Fecha Hasta:</div>
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
                            Estado de Ticket</div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboEstadoTicket" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            Técnico Asignado</div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboTecnico" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p />
                    <div class="row">
                        <div class="col-xs-2">
                            Cliente</div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboCliente" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True" 
                                onselectedindexchanged="cboCliente_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            Sede</div>
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cboSede" runat="server" CssClass="form-control dropdown input-sm"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-2">
                            <asp:Button ID="btnConsultar" CssClass="btn btn-primary" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-xs-2">
                            <asp:Label ID="lblRegistros" runat="server" Text="Total de Registros: 0"></asp:Label></div>
                    </div>
                    <p />
                    <asp:GridView ID="gvTickets" CssClass="table table-striped table-bordered table-condensed"
                        AutoGenerateColumns="False" runat="server" DataKeyNames="NroTicket" OnRowCommand="gvTickets_RowCommand"
                        OnRowDataBound="gvTickets_RowDataBound" PageSize="5" 
                        PagerStyle-CssClass="bs-pagination">
                        <Columns>
                            <asp:ButtonField CommandName="Seleccionar" ButtonType="Button" Text="Sel." ControlStyle-CssClass="btn btn-xs btn-link"
                                HeaderStyle-Width="5px">
                                <ControlStyle CssClass="btn btn-xs btn-link" />
                                <HeaderStyle Width="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="5px" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Nro" HeaderStyle-Width="5px" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdTicketG" runat="server" Text='<%#Eval("NroTicket") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="5px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblClienteG" runat="server" Text='<%#Eval("Cliente.RazonSocial") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sede" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblSedeClienteG" runat="server" Text='<%#Eval("Sede.Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FechaTicket" HeaderText="Fecha Ticket" ReadOnly="True"
                                HeaderStyle-Width="15px" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle">
                                <HeaderStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UsuarioAsignado" HeaderText="Usuario Asignado" ReadOnly="True"
                                HeaderStyle-Width="15px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <HeaderStyle Width="15px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Categoría" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoriaProblemaG" runat="server" Text='<%#Eval("CategoriaProblema.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Urgencia" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblNivelUrgenciaG" runat="server" Text='<%#Eval("NivelUrgencia.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Titulo" HeaderText="Titulo" ReadOnly="True" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="IdEstadoTicket" HeaderText="IdEstado" ReadOnly="True"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="False" />
                            <asp:TemplateField HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoTicketG" runat="server" Text='<%#Eval("EstadoTicket.Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TiempoTranscurrido" HeaderText="Tiempo Cierre (HH:MM)"
                                ReadOnly="True" HeaderStyle-Width="15px">
                                <HeaderStyle Width="15px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvTickets" EventName="PageIndexChanging" />
                </Triggers>
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
<!--script src="bootstrap/js/bs.pagination.js" type="text/javascript"></script-->
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
