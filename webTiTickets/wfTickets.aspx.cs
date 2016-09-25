using System;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
    // ReSharper disable InconsistentNaming
    public partial class wfTickets : Page
    // ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsuario.Text = Context.User.Identity.Name;
                Util.Util.CargaCategoriaProblema(cboCategoria, true);
                Util.Util.CargaNivelUrgencia(cboNivelUrgencia, true);
                Util.Util.CargaEstadosTicket(cboEstadoTicket, false);
                Util.Util.CargarHoras(cboHora, cboMinutos);


                var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));
                hfTipoUsuario.Value = oEntUsuario.IdTipoUsuario.ToString();
                hfMailDestinatario.Value = oEntUsuario.Email;

                if (oEntUsuario.IdTipoUsuario == (int)Util.Util.TipoUsuarioEnum.Administrador)
                {
                    mnuMantenimiento.Visible = true;
                }

                if (Session["NroTicket"] != null)
                {
                    var nNroTicket = Session["NroTicket"].ToString();
                    if (!String.IsNullOrEmpty(nNroTicket))
                    {
                        txtNroTicket.Text = nNroTicket;
                        hfNuevo.Value = "F";
                        CargarTicket();
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                    }
                }
                else
                {
                    hfNuevo.Value = "N";
                    if (Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Cliente)
                        ConsultarCliente();
                    ActivarDesactivarCampos(true);
                    OcultarMostrarCampos();
                    txtFechaTicket.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
                }
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            cboUnidadNegocio.Enabled = bEstado;
            cboSede.Enabled = bEstado;
            cboCategoria.Enabled = bEstado;
            cboNivelUrgencia.Enabled = bEstado;
            txtTitulo.Enabled = bEstado;
            txtDetalle.Enabled = bEstado;
            txtSolucion.Enabled = bEstado;
            txtObservaciones.Enabled = bEstado;
            txtTicketCliente.Enabled = bEstado;
            cboUsuarioAtencion.Enabled = bEstado;
            txtFechaTicket.Enabled = false;
            txtOrdenServicio.Enabled = bEstado;
        }

        private void OcultarMostrarCampos()
        {
            if (hfNuevo.Value.Equals("N"))
            {
                btnAsignar.Visible = false;
                btnAtender.Visible = false;
                btnCerrar.Visible = false;
                btnEsperaRepuesto.Visible = false;
                rowObservaciones.Visible = false;
                rowSolucion.Visible = false;
                rowOrdenServicio.Visible = false;
                btnAgregarRepuesto.Visible = false;
                btnReabrirTicket.Visible = false;
                txtOrdenServicio.Visible = false;
                btnAnular.Visible = false;
                //tituloRepuesto.Visible = false;
                btnGrabar.Visible = true;
                btnBuscarCliente.Visible = true;
                btnAfectoTarifa.Visible = false;
                if (Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Moderador)
                {
                    txtFechaTicket.Enabled = true;
                }
            }
            else
            {
                rowObservaciones.Visible = true;
                rowSolucion.Visible = true;
                rowOrdenServicio.Visible = true;
                btnCancelar.Text = "Regresar";
                txtOrdenServicio.Visible = true;
                btnBuscarCliente.Visible = false;
                

                if (Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Cliente)
                {
                    if (cboEstadoTicket.SelectedValue.Equals("1"))
                    {
                        txtTitulo.Enabled = true;
                        txtDetalle.Enabled = true;
                    }
                    txtSolucion.Enabled = false;
                    txtObservaciones.Enabled = false;
                    btnAsignar.Visible = false;
                    btnAtender.Visible = false;
                    btnCerrar.Visible = false;
                    btnAnular.Visible = false;
                    btnEsperaRepuesto.Visible = false;
                    btnAgregarRepuesto.Visible = false;
                    btnReabrirTicket.Enabled = cboEstadoTicket.SelectedValue.Equals("5");
                }
                if (Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Moderador)
                {
                    //btnGrabar.Visible = false;
                    btnAsignar.Visible = true;
                    //btnAtender.Visible = false;

                    btnCerrar.Visible = true;
                    //btnEsperaRepuesto.Visible = false;
                    //.Visible = false;

                    //Temporal
                    btnGrabar.Visible = true;
                    btnAtender.Visible = true;
                    btnAgregarRepuesto.Visible = true;
                    btnEsperaRepuesto.Visible = true;
                    btnAnular.Visible = true;
                    btnAfectoTarifa.Visible = true;

                    if (cboEstadoTicket.SelectedValue.Equals("2") || cboEstadoTicket.SelectedValue.Equals("3") || cboEstadoTicket.SelectedValue.Equals("4") || cboEstadoTicket.SelectedValue.Equals("6"))
                    {
                        btnAsignar.Enabled = false;
                        txtFechaTicket.Enabled = false;


                        //Temporal
                        txtSolucion.Enabled = true;
                        txtObservaciones.Enabled = true;
                        btnAgregarRepuesto.Enabled = true;
                        txtTicketCliente.Enabled = true;

                        cboUnidadNegocio.Enabled = true;
                        cboSede.Enabled = true;
                        txtFechaTicket.Enabled = true;
                        cboCategoria.Enabled = true;
                        txtOrdenServicio.Enabled = true;
                        btnAnular.Enabled = true;
                    }
                    if (cboEstadoTicket.SelectedValue.Equals("5"))
                    {
                        btnGrabar.Visible = false;
                        btnAsignar.Visible = false;
                        btnCerrar.Visible = false;
                        btnAtender.Visible = false;
                        btnEsperaRepuesto.Visible = false;
                        txtFechaTicket.Enabled = false;
                        btnAnular.Visible = false;
                    }
                    if (cboEstadoTicket.SelectedValue.Equals("6"))
                    {
                        btnEsperaRepuesto.Enabled = false;
                        btnAnular.Enabled = true;
                    }
                }
                if ((Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Técnico))
                {
                    btnGrabar.Visible = false;
                    btnAsignar.Visible = false;
                    btnAtender.Visible = true;
                    btnCerrar.Visible = false;
                    btnAnular.Visible = false;
                    btnEsperaRepuesto.Visible = true;
                    btnReabrirTicket.Visible = false;

                    if (cboEstadoTicket.SelectedValue.Equals("2") || cboEstadoTicket.SelectedValue.Equals("3") || cboEstadoTicket.SelectedValue.Equals("6"))
                    {
                        txtSolucion.Enabled = true;
                        txtObservaciones.Enabled = true;
                        btnAgregarRepuesto.Enabled = true;
                        txtOrdenServicio.Enabled = true;
                    }

                    if (cboEstadoTicket.SelectedValue.Equals("4") || cboEstadoTicket.SelectedValue.Equals("5"))
                    {
                        btnAtender.Enabled = false;
                        btnEsperaRepuesto.Enabled = false;
                        btnAgregarRepuesto.Enabled = false;
                    }
                    if (cboEstadoTicket.SelectedValue.Equals("6"))
                    {
                        btnEsperaRepuesto.Enabled = false;
                    }
                }

                if (cboEstadoTicket.SelectedValue.Equals("7"))
                {
                    btnGrabar.Visible = false;
                    btnAsignar.Visible = false;
                    btnAtender.Visible = false;
                    btnCerrar.Visible = false;
                    btnEsperaRepuesto.Visible = false;
                    btnReabrirTicket.Visible = false;
                    btnAnular.Visible = false;
                }
            }

        }

        private void ConsultarCliente()
        {
            var oUsuarioCliente = new UsuarioCliente().Consultar(new UsuarioClienteInfo(lblUsuario.Text, null, 1));
            txtIdCliente.Text = oUsuarioCliente.IdCliente.ToString();
            txtCliente.Text = oUsuarioCliente.Cliente.RazonSocial;
            Util.Util.CargaUnidadNegocioXCliente(cboUnidadNegocio, true, oUsuarioCliente.IdCliente);

            //Util.Util.CargaSedeCliente(cboSede, oUsuarioCliente.IdCliente, true);
        }

        private void CargarTicket()
        {
            try
            {
                var oTicket =
                    new Ticket().Consultar(new TicketInfo(int.Parse(txtNroTicket.Text), null, null, null, null, null,
                                                          null,
                                                          null) { IdUsuarioAsignado = null });

                txtIdCliente.Text = oTicket.IdCliente.ToString();
                txtCliente.Text = oTicket.Cliente.RazonSocial;

                Util.Util.CargaSedeCliente(cboSede, oTicket.IdCliente, null, true);

                cboSede.SelectedValue = oTicket.IdSede.ToString();

                var oEntSede = new SedeCliente().Consultar(new SedeClienteInfo(oTicket.IdSede, null, null));

                txtDireccionSede.Text = oTicket.Sede.Direccion;

                if (oEntSede != null)
                {
                    txtDireccionSede.Text += " - " + oEntSede.Distrito.Descripcion + " - " +
                                             oEntSede.Provincia.Descripcion + " - " + oEntSede.Departamento.Descripcion;
                    txtContactoSede.Text = oEntSede.NombreContacto + " - " + oEntSede.CargoContacto;
                }

                Util.Util.CargaUsuarioSede(cboUsuarioAtencion, oTicket.IdSede, true);
                if (oTicket.IdUsuarioSede != 0)
                    cboUsuarioAtencion.SelectedValue = oTicket.IdUsuarioSede.ToString();


                cboCategoria.SelectedValue = oTicket.IdCategoriaProblema.ToString();
                cboNivelUrgencia.SelectedValue = oTicket.IdNivelUrgencia.ToString();
                cboEstadoTicket.SelectedValue = oTicket.IdEstadoTicket.ToString();
                txtTitulo.Text = oTicket.Titulo;
                txtDetalle.Text = oTicket.Detalle;
                txtSolucion.Text = oTicket.Solucion;
                txtObservaciones.Text = oTicket.Observaciones;
                txtTecnicoAsignado.Text = oTicket.UsuarioAsignado;
                txtOrdenServicio.Text = oTicket.OrdenServicio;
                txtTicketCliente.Text = oTicket.NroTicketCliente;
                chkCostoCero.Checked = !oTicket.CostoCero.Equals("0");
                if (oTicket.FechaTicket != null)
                {
                    txtFechaTicket.Text = oTicket.FechaTicket.Value.ToShortDateString();
                    txtFechaTicketC.Text = txtFechaTicket.Text;
                }

                if (hfTipoUsuario.Value.Equals("3"))
                {
                    if (oTicket.IdEstadoTicket == (int)Util.Util.EstadoTicketsEnum.Asignado)
                    {
                        var oEntAsignado =
                            new TicketRegistro().Consultar(new TicketRegistroInfo(oTicket.NroTicket,
                                                                                  (int)Util.Util.EstadoTicketsEnum.Asignado,
                                                                                  null, null, null, lblUsuario.Text));
                        if (oEntAsignado != null)
                        {
                            if (oEntAsignado.IdUsuarioAsignado.Equals(lblUsuario.Text))
                            {
                                if (
                                    new TicketRegistro().Registrar(new TicketRegistroInfo(oTicket.NroTicket,
                                                                                          (int)
                                                                                          Util.Util.EstadoTicketsEnum
                                                                                              .Recibido, null,
                                                                                          lblUsuario.Text, null, null)))
                                {
                                    Util.Util.AlternarMensaje(true, "Al ser la primera vez que abre un ticket al que ha sido asignado este se ha marcado con estado RECIBIDO", alertaError, alertaExito, lblError, lblExito);
                                    //UpdatePanel2.Update();
                                }
                            }
                        }
                    }
                }



                var oListaRegistros = new TicketRegistro().Listar(new TicketRegistroInfo(oTicket.NroTicket, null, null, null, null, null));
                gvRegistros.DataSource = oListaRegistros;
                gvRegistros.DataBind();

                var oListaDetalle = new TicketDetalle().Listar(new TicketDetalleInfo { NroTicket = Int32.Parse(txtNroTicket.Text) });
                gvRepuestos.DataSource = oListaDetalle;
                gvRepuestos.DataBind();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtIdCliente.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el cliente al que pertencece el ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboSede.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar la sede del cliente al que pertencece el ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboCategoria.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar la categoría del ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboNivelUrgencia.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el nivel de urgencia del ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtTitulo.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el título del ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtDetalle.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el detalle del ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }
                if (String.IsNullOrEmpty(txtFechaTicket.Text.Trim()))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar la fecha del ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }
                if (!Util.Util.EsFechaValida(txtFechaTicket.Text.Trim()))
                {
                    Util.Util.AlternarMensaje(false, "La fecha del no tiene un formato válido. Debe ser dd/mm/yyyy.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                var oEntTicket = new TicketInfo
                    {
                        IdCliente = Int32.Parse(txtIdCliente.Text),
                        IdSede = Int32.Parse(cboSede.SelectedValue),
                        FechaTicket = DateTime.Parse(txtFechaTicket.Text),
                        IdCategoriaProblema = Int32.Parse(cboCategoria.SelectedValue),
                        IdNivelUrgencia = Int32.Parse(cboNivelUrgencia.SelectedValue),
                        Titulo = txtTitulo.Text.Trim(),
                        Detalle = txtDetalle.Text.Trim(),
                        IdUsuario = lblUsuario.Text,
                        Solucion = txtSolucion.Text.Trim(),
                        Observaciones = txtObservaciones.Text.Trim(),
                        NroTicketCliente = String.IsNullOrEmpty(txtTicketCliente.Text) ? null : txtTicketCliente.Text.Trim(),
                        IdUsuarioSede = cboUsuarioAtencion.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboUsuarioAtencion.SelectedValue),
                        OrdenServicio = String.IsNullOrEmpty(txtOrdenServicio.Text) ? null : txtOrdenServicio.Text.Trim()
                    };
                int? nIdTicket = null;
                if (hfNuevo.Value.Equals("N"))
                {
                    if (new Ticket().Registrar(oEntTicket, ref nIdTicket))
                    {
                        txtNroTicket.Text = nIdTicket.ToString();
                        hfNuevo.Value = "F";
                        CargarTicket();
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                        Util.Util.AlternarMensaje(true, "Se registró el ticket con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                            var sTitulo = "Mesa de Ayuda TI Consulting - Ticket Registrado Nro. " +
                                             nIdTicket.ToString();
                            var sMensajeCorreo = new StringBuilder();
                            sMensajeCorreo.Append("<b>Ticket Registro Nro. ");
                            sMensajeCorreo.Append(nIdTicket.ToString());
                            sMensajeCorreo.Append("</b>");
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Cliente: ");
                            sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Sede: ");
                            sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Dirección: ");
                            sMensajeCorreo.Append(txtDireccionSede.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Contacto de la Sede: ");
                            sMensajeCorreo.Append(txtContactoSede.Text);
                            if (!cboUsuarioAtencion.SelectedValue.Equals("-1"))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Usuario a Atender: ");
                                sMensajeCorreo.Append(cboUsuarioAtencion.SelectedItem.Text);
                            }
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Categoría y Nivel de Urgencia: ");
                            sMensajeCorreo.Append(cboCategoria.SelectedItem.Text);
                            sMensajeCorreo.Append(" - ");
                            sMensajeCorreo.Append(cboNivelUrgencia.SelectedItem.Text);


                            if (!String.IsNullOrEmpty(txtTicketCliente.Text))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Nro. de Ticket del Cliente: ");
                                sMensajeCorreo.Append(txtTicketCliente.Text);
                            }

                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Título: ");
                            sMensajeCorreo.Append(txtTitulo.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Detalle: ");
                            sMensajeCorreo.Append(txtDetalle.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("<b>Su ticket será atendido a la brevedad</b>");

                            Util.Util.EnviarMail(hfMailDestinatario.Value, sTitulo, sMensajeCorreo.ToString(), sMailModerador);
                        }
                        //UpdatePanel2.Update();
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar el ticket.", alertaError, alertaExito,
                                                  lblError, lblExito);
                        //UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntTicket.NroTicket = Int32.Parse(txtNroTicket.Text);
                    if (new Ticket().Actualizar(oEntTicket))
                    {
                        CargarTicket();
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                        Util.Util.AlternarMensaje(true, "Se actualizó el ticket con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar el ticket.", alertaError, alertaExito,
                          lblError, lblExito);
                    }
                }


            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["NroTicket"] = null;
            Response.Redirect("wfListaTickets.aspx");
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalAsignarTecnico').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnConsultarTecnico_Click(object sender, EventArgs e)
        {
            var sNombreTecnico = String.IsNullOrEmpty(txtTecnicoNombre.Text.Trim())
                                        ? null
                                        : txtTecnicoNombre.Text.Trim();
            //gvTecnicos.DataSource = new Usuario().ListarTecnicosLibres(new UsuarioInfo(null, null, sNombreTecnico, (int)Util.Util.TipoUsuarioEnum.Técnico));
            gvTecnicos.DataSource = new Usuario().Listar(new UsuarioInfo(null, null, sNombreTecnico, (int)Util.Util.TipoUsuarioEnum.Técnico));
            gvTecnicos.DataBind();
        }

        protected void gvTecnicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvTecnicos.DataKeys[index];
                String sIdTecnico = null;

                if (dataKey != null)
                    sIdTecnico = dataKey.Value.ToString();

                if (e.CommandName.Equals("Seleccionar"))
                {
                    //txtObservaciones.Text = sIdTecnico;
                    var oEnTecnico = new Usuario().Consultar(new UsuarioInfo(sIdTecnico, null, null, null));

                    if (
                        new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                              (int)Util.Util.EstadoTicketsEnum.Asignado,
                                                                              null, lblUsuario.Text, null, sIdTecnico)))
                    {
                        CargarTicket();
                        Util.Util.AlternarMensaje(true, "Se asignó el técnico seleccionado al ticket.", alertaError, alertaExito, lblError, lblExito);

                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                            var sTitulo = "Mesa de Ayuda TI Consulting - Ticket Asignado Nro. " +
                                             txtNroTicket.Text;
                            var sMensajeCorreo = new StringBuilder();
                            sMensajeCorreo.Append("<b>Ticket Registro Nro. ");
                            sMensajeCorreo.Append(txtNroTicket.Text);
                            sMensajeCorreo.Append("</b>");
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Cliente: ");
                            sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Sede: ");
                            sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Dirección: ");
                            sMensajeCorreo.Append(txtDireccionSede.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Contacto de la Sede: ");
                            sMensajeCorreo.Append(txtContactoSede.Text);
                            if (!cboUsuarioAtencion.SelectedValue.Equals("-1"))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Usuario a Atender: ");
                                sMensajeCorreo.Append(cboUsuarioAtencion.SelectedItem.Text);
                            }
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Categoría y Nivel de Urgencia: ");
                            sMensajeCorreo.Append(cboCategoria.SelectedItem.Text);
                            sMensajeCorreo.Append(" - ");
                            sMensajeCorreo.Append(cboNivelUrgencia.SelectedItem.Text);


                            if (!String.IsNullOrEmpty(txtTicketCliente.Text))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Nro. de Ticket del Cliente: ");
                                sMensajeCorreo.Append(txtTicketCliente.Text);
                            }

                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Título: ");
                            sMensajeCorreo.Append(txtTitulo.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Detalle: ");
                            sMensajeCorreo.Append(txtDetalle.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("<b>Por favor tomar la atención correspondiente.</b>");

                            Util.Util.EnviarMail(oEnTecnico.Email, sTitulo, sMensajeCorreo.ToString(), sMailModerador);

                        }
                        //UpdatePanel2.Update();
                    }

                    var sb = new StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#modalAsignarTecnico').modal('hide');");
                    sb.Append(@"</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Hide", sb.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);

                    ActivarDesactivarCampos(false);
                    OcultarMostrarCampos();

                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnAtender_Click(object sender, EventArgs e)
        {
            try
            {
                hfNuevo.Value = "AT";
                MostrarModalConfirmacion();

            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                hfNuevo.Value = "CI";
                MostrarModalConfirmacion();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnConsultarCliente_Click(object sender, EventArgs e)
        {
            var sRazonSocial = String.IsNullOrEmpty(txtRazonSocialCliente.Text.Trim())
                            ? null
                            : txtRazonSocialCliente.Text.Trim();
            gvClientes.DataSource = new Cliente().Listar(new ClienteInfo { RazonSocial = sRazonSocial });
            gvClientes.DataBind();
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvClientes.DataKeys[index];
                int? nIdCliente = null;

                if (dataKey != null)
                    nIdCliente = Int32.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    //txtObservaciones.Text = sIdTecnico;
                    var oEntCliente = new Cliente().Consultar(new ClienteInfo { IdCliente = nIdCliente });

                    if (oEntCliente != null)
                    {
                        txtIdCliente.Text = oEntCliente.IdCliente.ToString();
                        txtCliente.Text = oEntCliente.RazonSocial;
                        Util.Util.CargaUnidadNegocioXCliente(cboUnidadNegocio, true, oEntCliente.IdCliente);

                        //Util.Util.CargaSedeCliente(cboSede, oEntCliente.IdCliente, true);
                    }

                    var sb = new StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#modalConsultaCliente').modal('hide');");
                    sb.Append(@"</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Hide", sb.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);

                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalConsultaCliente').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void cboSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nIdSede = Int32.Parse(cboSede.SelectedValue);
            var oEntSede = new SedeCliente().Consultar(new SedeClienteInfo { IdSedeCliente = nIdSede });
            if (oEntSede != null)
            {
                txtDireccionSede.Text = oEntSede.Direccion + " - " + oEntSede.Distrito.Descripcion + " - " + oEntSede.Provincia.Descripcion + " - " + oEntSede.Departamento.Descripcion;
                txtContactoSede.Text = oEntSede.NombreContacto + " - " + oEntSede.CargoContacto;
            }

            Util.Util.CargaUsuarioSede(cboUsuarioAtencion, nIdSede, true);
        }

        private void MostrarModalConfirmacion()
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalConfirmacion').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnSiGrabar_Click(object sender, EventArgs e)
        {
            try
            {

                if (hfNuevo.Value.Equals("AT"))
                {
                    if (String.IsNullOrEmpty(txtSolucion.Text.Trim()))
                    {
                        Util.Util.AlternarMensaje(false, "Debe indicar una solución para el ticket.", alertaError,
                                                  alertaExito, lblError, lblExito);
                        return;
                    }

                    var oEntTicket = new TicketInfo
                        {
                            NroTicket = int.Parse(txtNroTicket.Text),
                            Solucion = txtSolucion.Text,
                            Observaciones = txtObservaciones.Text,
                            OrdenServicio = txtOrdenServicio.Text
                        };

                    if (
                        new Ticket().Actualizar(oEntTicket) &&
                        new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                              (int)Util.Util.EstadoTicketsEnum.Atendido,
                                                                              null,
                                                                              lblUsuario.Text, null, null)))
                    {
                        CargarTicket();
                        Util.Util.AlternarMensaje(true, "Se actualizó el estado del ticket a ATENDIDO", alertaError,
                                                  alertaExito, lblError, lblExito);
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();

                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                            var sTitulo = "Mesa de Ayuda TI Consulting - Ticket Atendido Nro. " +
                                          txtNroTicket.Text;
                            var sMensajeCorreo = new StringBuilder();
                            sMensajeCorreo.Append("<b>El Ticket Nro. ");
                            sMensajeCorreo.Append(txtNroTicket.Text);
                            sMensajeCorreo.Append(" ha sido atendido.</b>");
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Cliente: ");
                            sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Sede: ");
                            sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Dirección: ");
                            sMensajeCorreo.Append(txtDireccionSede.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Contacto de la Sede: ");
                            sMensajeCorreo.Append(txtContactoSede.Text);
                            if (!cboUsuarioAtencion.SelectedValue.Equals("-1"))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Usuario a Atender: ");
                                sMensajeCorreo.Append(cboUsuarioAtencion.SelectedItem.Text);
                            }
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Categoría y Nivel de Urgencia: ");
                            sMensajeCorreo.Append(cboCategoria.SelectedItem.Text);
                            sMensajeCorreo.Append(" - ");
                            sMensajeCorreo.Append(cboNivelUrgencia.SelectedItem.Text);


                            if (!String.IsNullOrEmpty(txtTicketCliente.Text))
                            {
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Nro. de Ticket del Cliente: ");
                                sMensajeCorreo.Append(txtTicketCliente.Text);
                            }

                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Título: ");
                            sMensajeCorreo.Append(txtTitulo.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Detalle: ");
                            sMensajeCorreo.Append(txtDetalle.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("<b>Solución: ");
                            sMensajeCorreo.Append(txtSolucion.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Observaciones: ");
                            sMensajeCorreo.Append(txtObservaciones.Text.Trim());
                            sMensajeCorreo.Append("<p/></b>");
                            sMensajeCorreo.Append("<b>Por favor tomar la atención correspondiente.</b>");

                            Util.Util.EnviarMail(sMailModerador, sTitulo, sMensajeCorreo.ToString(), null);
                        }
                        //UpdatePanel2.Update();
                    }
                }
                if (hfNuevo.Value.Equals("CI"))
                {
                    if (
                        new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                              (int)Util.Util.EstadoTicketsEnum.Cerrado,
                                                                              null,
                                                                              lblUsuario.Text, null, null)))
                    {
                        CargarTicket();
                        Util.Util.AlternarMensaje(true, "El ticket ha sido CERRADO", alertaError,
                                                  alertaExito, lblError, lblExito);
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                        //UpdatePanel2.Update();
                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var oEntRegistro =
                                new TicketRegistro().Consultar(new TicketRegistroInfo(Int32.Parse(txtNroTicket.Text), 1,
                                                                                      null, null, null, null));
                            if (oEntRegistro != null)
                            {
                                var oEntMailUsuarioCliente =
                                    new Usuario().Consultar(new UsuarioInfo(oEntRegistro.IdUsuario, null, null, null));
                                var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                                var sTitulo = "Mesa de Ayuda TI Consulting - Ticket Cerrado Nro. " +
                                              txtNroTicket.Text;
                                var sMensajeCorreo = new StringBuilder();
                                sMensajeCorreo.Append("<b>El Ticket Nro. ");
                                sMensajeCorreo.Append(txtNroTicket.Text);
                                sMensajeCorreo.Append(" ha sido cerrado.</b>");
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Título: ");
                                sMensajeCorreo.Append(txtTitulo.Text);
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Detalle: ");
                                sMensajeCorreo.Append(txtDetalle.Text);
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("<b>Solución: ");
                                sMensajeCorreo.Append(txtSolucion.Text);
                                sMensajeCorreo.Append("<p/>");
                                sMensajeCorreo.Append("Observaciones: ");
                                sMensajeCorreo.Append(txtObservaciones.Text);
                                sMensajeCorreo.Append("<p/></b>");
                                sMensajeCorreo.Append("<b>Agradecremos su validación.</b>");

                                Util.Util.EnviarMail(oEntMailUsuarioCliente.Email, sTitulo, sMensajeCorreo.ToString(),
                                                     sMailModerador);
                            }


                        }
                    }
                }
                if (hfNuevo.Value.Equals("RE"))
                {
                    if (
                        new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                              (int)
                                                                              Util.Util.EstadoTicketsEnum
                                                                                  .EsperaRepuesto,
                                                                              null,
                                                                              lblUsuario.Text, null, null)))
                    {
                        CargarTicket();
                        Util.Util.AlternarMensaje(true, "El ticket has sido marcado EN ESPERA DE REPUESTO",
                                                  alertaError,
                                                  alertaExito, lblError, lblExito);
                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                            var sTitulo = "Mesa de Ayuda TI Consulting - Ticket En Espera de Repuesto Nro. " +
                                          txtNroTicket.Text;
                            var sMensajeCorreo = new StringBuilder();
                            sMensajeCorreo.Append("<b>El Ticket Nro. ");
                            sMensajeCorreo.Append(txtNroTicket.Text);
                            sMensajeCorreo.Append(" se encuentra en espera de repuesto.</b>");
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Cliente: ");
                            sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Sede: ");
                            sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Dirección: ");
                            sMensajeCorreo.Append(txtDireccionSede.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Contacto de la Sede: ");
                            sMensajeCorreo.Append(txtContactoSede.Text);

                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Título: ");
                            sMensajeCorreo.Append(txtTitulo.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Detalle: ");
                            sMensajeCorreo.Append(txtDetalle.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("<b>Solución: ");
                            sMensajeCorreo.Append(txtSolucion.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Observaciones: ");
                            sMensajeCorreo.Append(txtObservaciones.Text.Trim());
                            sMensajeCorreo.Append("<p/></b>");
                            sMensajeCorreo.Append("<b>Por favor tomar la atención correspondiente.</b>");

                            Util.Util.EnviarMail(sMailModerador, sTitulo, sMensajeCorreo.ToString(), null);
                        }
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                        //UpdatePanel2.Update();
                    }
                }
                if (hfNuevo.Value.Equals("AN"))
                {
                    if (
                        new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                              (int)
                                                                              Util.Util.EstadoTicketsEnum
                                                                                  .Anulado,
                                                                              null,
                                                                              lblUsuario.Text, null, null)))
                    {
                        CargarTicket();
                        Util.Util.AlternarMensaje(true, "El ticket ha sido ANULADO",
                                                  alertaError,
                                                  alertaExito, lblError, lblExito);
                        if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                        {
                            var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                            var sTitulo = "Mesa de Ayuda TI Consulting - Ticket Anulado Nro. " +
                                          txtNroTicket.Text;
                            var sMensajeCorreo = new StringBuilder();
                            sMensajeCorreo.Append("<b>El Ticket Nro. ");
                            sMensajeCorreo.Append(txtNroTicket.Text);
                            sMensajeCorreo.Append(" ha sido anulado.</b>");
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Cliente: ");
                            sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Sede: ");
                            sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Dirección: ");
                            sMensajeCorreo.Append(txtDireccionSede.Text);
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Contacto de la Sede: ");
                            sMensajeCorreo.Append(txtContactoSede.Text);

                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Título: ");
                            sMensajeCorreo.Append(txtTitulo.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Detalle: ");
                            sMensajeCorreo.Append(txtDetalle.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("<b>Solución: ");
                            sMensajeCorreo.Append(txtSolucion.Text.Trim());
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Observaciones: ");
                            sMensajeCorreo.Append(txtObservaciones.Text.Trim());
                            sMensajeCorreo.Append("<p/></b>");

                            Util.Util.EnviarMail(sMailModerador, sTitulo, sMensajeCorreo.ToString(), null);
                        }
                        ActivarDesactivarCampos(false);
                        OcultarMostrarCampos();
                        //UpdatePanel2.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message,
                                          alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void cboUnidadNegocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nIdUnidadNegocio = cboUnidadNegocio.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboUnidadNegocio.SelectedValue);
            Util.Util.CargaSedeCliente(cboSede, Int32.Parse(txtIdCliente.Text), nIdUnidadNegocio, true);
        }

        protected void btnEsperaRepuesto_Click(object sender, EventArgs e)
        {
            try
            {
                hfNuevo.Value = "RE";
                MostrarModalConfirmacion();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnBuscarRepuesto_Click(object sender, EventArgs e)
        {
            var sDescripcionRepuesto = String.IsNullOrEmpty(txtRepuestoNombre.Text.Trim())
                                        ? null
                                        : txtRepuestoNombre.Text.Trim();
            gvRepuestosBuscar.DataSource = new Repuesto().Listar(new RepuestoInfo { Descripcion = sDescripcionRepuesto });
            gvRepuestosBuscar.DataBind();
        }

        protected void gvRepuestosBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvRepuestosBuscar.DataKeys[index];
                String sIdRepuesto = null;

                if (dataKey != null)
                    sIdRepuesto = dataKey.Value.ToString();

                if (e.CommandName.Equals("Seleccionar"))
                {
                    if (sIdRepuesto != null)
                    {
                        RepuestoInfo oRepuesto = new Repuesto().Consultar(new RepuestoInfo { IdRepuesto = Int32.Parse(sIdRepuesto) });

                        if (new TicketDetalle().Registrar(new TicketDetalleInfo(Int32.Parse(txtNroTicket.Text), oRepuesto.IdRepuesto, null,
                                                                                Double.Parse(txtCantidadRepuesto.Text), oRepuesto.IdMoneda, null, oRepuesto.PrecioActual) { UsuarioCreacion = lblUsuario.Text }))
                        {
                            CargarTicket();
                            Util.Util.AlternarMensaje(true, "Repuesto agregado al ticket.", alertaError, alertaExito, lblError, lblExito);

                        }
                    }

                    var sb = new StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#modalAsignarRepuesto').modal('hide');");
                    sb.Append(@"</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Hide", sb.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);

                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnAgregarRepuesto_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalAsignarRepuesto').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnReabrirTicket_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalReabrirTicket').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnSiReAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (new TicketRegistro().Registrar(new TicketRegistroInfo(int.Parse(txtNroTicket.Text),
                                                                          (int)Util.Util.EstadoTicketsEnum.Registrado,
                                                                          null,
                                                                          lblUsuario.Text, null, null) { Observacion = txtMotivoReapertura.Text }))
                {
                    CargarTicket();
                    Util.Util.AlternarMensaje(true, "El ticket ha sido marcado reabierto. Vuelve al estado REGISTRADO",
                                              alertaError,
                                              alertaExito, lblError, lblExito);

                    if (ConfigurationManager.AppSettings["EnviarMail"].Equals("1"))
                    {
                        var sMailModerador = ConfigurationManager.AppSettings["MailModerador"];
                        var sTitulo = "Mesa de Ayuda TI Consulting - Ticket ReAbierto Nro. " +
                                         txtNroTicket.Text;
                        var sMensajeCorreo = new StringBuilder();
                        sMensajeCorreo.Append("<b>Ticket Registro Nro. ");
                        sMensajeCorreo.Append(txtNroTicket.Text);
                        sMensajeCorreo.Append("</b>");
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Cliente: ");
                        sMensajeCorreo.Append(txtRazonSocialCliente.Text);
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Sede: ");
                        sMensajeCorreo.Append(cboSede.SelectedItem.Text);
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Dirección: ");
                        sMensajeCorreo.Append(txtDireccionSede.Text);
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Contacto de la Sede: ");
                        sMensajeCorreo.Append(txtContactoSede.Text);
                        if (!cboUsuarioAtencion.SelectedValue.Equals("-1"))
                        {
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Usuario a Atender: ");
                            sMensajeCorreo.Append(cboUsuarioAtencion.SelectedItem.Text);
                        }
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Categoría y Nivel de Urgencia: ");
                        sMensajeCorreo.Append(cboCategoria.SelectedItem.Text);
                        sMensajeCorreo.Append(" - ");
                        sMensajeCorreo.Append(cboNivelUrgencia.SelectedItem.Text);


                        if (!String.IsNullOrEmpty(txtTicketCliente.Text))
                        {
                            sMensajeCorreo.Append("<p/>");
                            sMensajeCorreo.Append("Nro. de Ticket del Cliente: ");
                            sMensajeCorreo.Append(txtTicketCliente.Text);
                        }

                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Título: ");
                        sMensajeCorreo.Append(txtTitulo.Text);
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("Detalle: ");
                        sMensajeCorreo.Append(txtDetalle.Text);
                        sMensajeCorreo.Append("<p/>");
                        sMensajeCorreo.Append("<b>Su ticket será atendido a la brevedad</b>");

                        Util.Util.EnviarMail(hfMailDestinatario.Value, sTitulo, sMensajeCorreo.ToString(), sMailModerador);
                    }


                    ActivarDesactivarCampos(false);
                    OcultarMostrarCampos();
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer volver a abrir el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                hfNuevo.Value = "AN";
                MostrarModalConfirmacion();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NroTicket"] = null;
                hfNuevo.Value = "N";
                if (Int32.Parse(hfTipoUsuario.Value) == (int)Util.Util.TipoUsuarioEnum.Cliente)
                    ConsultarCliente();
                ActivarDesactivarCampos(true);
                OcultarMostrarCampos();
                LimpiarCampos();
                txtFechaTicket.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void LimpiarCampos()
        {
            txtNroTicket.Text = "";
            txtIdCliente.Text = "";
            txtCliente.Text = "";
            txtDireccionSede.Text = "";
            txtContactoSede.Text = "";
            cboUsuarioAtencion.Items.Clear();
            cboCategoria.SelectedValue = "-1";
            cboNivelUrgencia.SelectedValue = "-1";
            cboEstadoTicket.SelectedValue = "1";
            txtTecnicoAsignado.Text = "";
            txtTecnicoNombre.Text = "";
            txtTitulo.Text = "";
            txtDetalle.Text = "";
            txtTicketCliente.Text = "";
            txtSolucion.Text = "";
            txtObservaciones.Text = "";
            txtOrdenServicio.Text = "";
            gvRepuestos.DataSource = null;
            gvRepuestos.DataBind();
            gvRegistros.DataSource = null;
            gvRegistros.DataBind();
        }

        protected void gvRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvRepuestos.DataKeys[index];
                int? nIdRepuesto = null;
                int? nNroTicket = null;

                if (dataKey != null)
                {
                    nIdRepuesto = int.Parse(dataKey.Value.ToString());
                    nNroTicket = int.Parse(txtNroTicket.Text);
                }

                if (e.CommandName.Equals("Eliminar"))
                {
                    var oDetalleTicket = new TicketDetalleInfo { NroTicket = nNroTicket, IdRepuesto = nIdRepuesto };
                    if (new TicketDetalle().Eliminar(oDetalleTicket))
                    {
                        Util.Util.AlternarMensaje(true, "Se eliminó el repuesto correctamente.",
                                                  alertaError,
                                                  alertaExito, lblError, lblExito);

                        var oListaDetalle = new TicketDetalle().Listar(new TicketDetalleInfo { NroTicket = Int32.Parse(txtNroTicket.Text) });
                        gvRepuestos.DataSource = oListaDetalle;
                        gvRepuestos.DataBind();

                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo eliminar el repuesto.",
                         alertaError,
                         alertaExito, lblError, lblExito);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer eliminar el repuesto del ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnAfectoTarifa_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalConfirmaTarifa').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnSiGrabarTarifa_Click(object sender, EventArgs e)
        {
            try
            {
                //Si estaba marcado, entonces se inactiva. Y viceversa
                var oEntTicket = new TicketInfo
                {
                    NroTicket = int.Parse(txtNroTicket.Text),
                    CostoCero = chkCostoCero.Checked ? "0" : "1"
                };
                if (new Ticket().Actualizar(oEntTicket))
                {
                    CargarTicket();
                    Util.Util.AlternarMensaje(true, "El estado de tarifa del Ticket fue actualizado",
                                              alertaError,
                                              alertaExito, lblError, lblExito);
                    ActivarDesactivarCampos(false);
                    OcultarMostrarCampos();
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer actualizar el estado de tarifa del ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnCambiarHoraRegistro_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalHoraTicket').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnGrabarHora_Click(object sender, EventArgs e)
        {
            try
            {

                var dFecha = Convert.ToDateTime(txtFechaTicketC.Text);
                var nHora = Int32.Parse(cboHora.Text);
                var nMinutos = Int32.Parse(cboMinutos.Text);
                
                var dFechaHoraNueva = new DateTime(dFecha.Year, dFecha.Month, dFecha.Day, nHora, nMinutos, 0 );
                

                var oTicketRegistro = new TicketRegistroInfo
                    {
                        NroTicket = Int32.Parse(txtNroTicket.Text),
                        FechaHoraRegistro = dFechaHoraNueva
                    };

                if (new TicketRegistro().ActualizarHoraRegistro(oTicketRegistro))
                {
                    CargarTicket();
                    Util.Util.AlternarMensaje(true, "La hora de registro del Ticket fue actualizada",
                                              alertaError,
                                              alertaExito, lblError, lblExito);
                    ActivarDesactivarCampos(false);
                    OcultarMostrarCampos();
                }
                else
                {
                    Util.Util.AlternarMensaje(false, "NO se pudo actualizar la hora de registro del Ticket",
                          alertaError,
                          alertaExito, lblError, lblExito);
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer la hora de registro del ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }
    }
}