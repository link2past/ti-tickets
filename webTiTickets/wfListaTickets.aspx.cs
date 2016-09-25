using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;
using System.Linq;

namespace webTiTickets
{
    // ReSharper disable InconsistentNaming
    public partial class wfListaTickets : Page
    // ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstadosTicket(cboEstadoTicket, true);
                    Util.Util.CargaUsuario(cboTecnico, (int) Util.Util.TipoUsuarioEnum.Técnico, true);
                    Util.Util.CargaCliente(cboCliente, true);

                    cboCliente.SelectedValue = "-1";

                    var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));
                    hfIdTipoUsuario.Value = oEntUsuario.IdTipoUsuario.ToString();
                    if (oEntUsuario.IdTipoUsuario == (int)Util.Util.TipoUsuarioEnum.Cliente)
                    {
                        ConsultarCliente();
                        btnNuevoTicket.Visible = true;
                        cboCliente.Enabled = false;
                        cboTecnico.Enabled = false;
                        cboSede.Enabled = false;
                    }
                    else
                    {
                        btnNuevoTicket.Visible = oEntUsuario.IdTipoUsuario == (int)Util.Util.TipoUsuarioEnum.Moderador;

                        lblUsuarioNombre.Text = oEntUsuario.Nombre;
                        if (oEntUsuario.IdTipoUsuario != null)
                            ListarTickets(new TicketInfo(), (Util.Util.TipoUsuarioEnum)oEntUsuario.IdTipoUsuario);
                    }
                    if (oEntUsuario.IdTipoUsuario == (int) Util.Util.TipoUsuarioEnum.Administrador)
                    {
                        mnuMantenimiento.Visible = true;
                    }

                    txtFechaDesde.Text =
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
                    txtFechaHasta.Text =
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
                }
                catch (Exception ex)
                {
                    Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        private void ConsultarCliente()
        {
            var oUsuarioCliente = new UsuarioCliente().Consultar(new UsuarioClienteInfo(lblUsuario.Text, null, 1));
            if (oUsuarioCliente != null)
            {
                lblUsuarioNombre.Text = oUsuarioCliente.Usuario.Nombre;
                hfIdCliente.Value = oUsuarioCliente.IdCliente.ToString();
                lblClienteNombre.Text = oUsuarioCliente.Cliente.RazonSocial;
                ListarTickets(new TicketInfo(null, Int32.Parse(hfIdCliente.Value), null, null, null, null, null, null),
                              Util.Util.TipoUsuarioEnum.Cliente);
            }
            else
            {
                Util.Util.AlternarMensaje(false, "El usuario no está asociado a ningún cliente, por lo tanto no podrá ver la información de los tickets registrados", alertaError, alertaExito, lblError, lblExito);
            }

        }

        private void ListarTickets(TicketInfo oParametrosTicket, Util.Util.TipoUsuarioEnum eTipoUsuario)
        {
            List<TicketInfo> oListaTickets;
            if (eTipoUsuario == Util.Util.TipoUsuarioEnum.Cliente)
            {
                oListaTickets = (List<TicketInfo>)new Ticket().Listar(oParametrosTicket);
                lblRegistros.Text = "Total de Registros: " + oListaTickets.Count;
                gvTickets.DataSource = oListaTickets;
            }
            else
            {
                if (eTipoUsuario == Util.Util.TipoUsuarioEnum.Técnico)
                {
                    var oTicketsAsignados =
                        new TicketRegistro().Listar(new TicketRegistroInfo(null,
                                                                              (int)Util.Util.EstadoTicketsEnum.Asignado,
                                                                              null, null, null, lblUsuario.Text));
                    var oTicketsRecibidos =
                        new TicketRegistro().Listar(new TicketRegistroInfo(null,
                                                                              (int)Util.Util.EstadoTicketsEnum.Recibido,
                                                                              null, null, null, lblUsuario.Text));
                    oListaTickets = (List<TicketInfo>)new Ticket().Listar(oParametrosTicket);

                    var joinedAsignados = from oTicketInfo in oListaTickets
                                          join oTicketRegistro in oTicketsAsignados
                                              on oTicketInfo.NroTicket equals oTicketRegistro.NroTicket
                                          select oTicketInfo;

                    var joinedRecibidos = from oTicketInfo in oListaTickets
                                          join oTicketRegistro in oTicketsRecibidos
                                              on oTicketInfo.NroTicket equals oTicketRegistro.NroTicket
                                          orderby oTicketInfo.FechaTicket descending, oTicketInfo.NroTicket descending
                                          select oTicketInfo
                                          ;

                    var oListaFiltrados = new List<TicketInfo>();
                    oListaFiltrados.AddRange(joinedAsignados);
                    oListaFiltrados.AddRange(joinedRecibidos);
                    lblRegistros.Text = "Total de Registros: " + oListaFiltrados.Count;
                    gvTickets.DataSource = oListaFiltrados;
                }
                if (eTipoUsuario == Util.Util.TipoUsuarioEnum.Moderador ||
                    eTipoUsuario == Util.Util.TipoUsuarioEnum.Administrador)
                {
                    int? nIdCliente = cboCliente.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboCliente.SelectedValue);
                    int? nIdSede = nIdCliente.HasValue ? (cboSede.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboSede.SelectedValue)) : null;
                    String nIdTecnico = cboTecnico.SelectedValue.Equals("-1") ? null : cboTecnico.SelectedValue;

                    oListaTickets = (List<TicketInfo>)new Ticket().Listar(new TicketInfo(){FechaDesde = oParametrosTicket.FechaDesde, FechaHasta = oParametrosTicket.FechaHasta, 
                        IdEstadoTicket = oParametrosTicket.IdEstadoTicket, IdCliente = nIdCliente, IdUsuarioAsignado = nIdTecnico, IdSede = nIdSede});
                    lblRegistros.Text = "Total de Registros: " + oListaTickets.Count;
                    gvTickets.DataSource = oListaTickets;
                }
            }
            gvTickets.DataBind();
        }



        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? dFechaDesde;
                DateTime? dFechaHasta;

                if (String.IsNullOrEmpty(txtFechaDesde.Text.Trim()))
                {
                    dFechaDesde = null;
                }
                else
                {
                    if (!Util.Util.EsFechaValida(txtFechaDesde.Text.Trim()))
                    {
                        Util.Util.AlternarMensaje(false, "La fecha de inicio de búsqueda no tiene un formato válido. Debe ser dd/mm/yyyy.", alertaError, alertaExito, lblError, lblExito);
                        UpdatePanel2.Update();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        return;
                    }
                    dFechaDesde = DateTime.Parse(txtFechaDesde.Text.Trim());
                }

                if (String.IsNullOrEmpty(txtFechaHasta.Text.Trim()))
                {
                    dFechaHasta = null;
                }
                else
                {
                    if (!Util.Util.EsFechaValida(txtFechaHasta.Text.Trim()))
                    {
                        Util.Util.AlternarMensaje(false, "La fecha de fin de búsqueda no tiene un formato válido. Debe ser dd/mm/yyyy.", alertaError, alertaExito, lblError, lblExito);
                        UpdatePanel2.Update();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        return;
                    }
                    dFechaHasta = DateTime.Parse(txtFechaHasta.Text.Trim());
                }

                var nEstadoTicket = cboEstadoTicket.SelectedValue.Equals("-1")
                                           ? (int?)null
                                           : Int32.Parse(cboEstadoTicket.SelectedValue);

                var nCliente = String.IsNullOrEmpty(hfIdCliente.Value) ? (int?)null : Int32.Parse(hfIdCliente.Value);

                ListarTickets(new TicketInfo(null, nCliente, null, null, null, dFechaDesde, dFechaHasta, nEstadoTicket), (Util.Util.TipoUsuarioEnum)int.Parse(hfIdTipoUsuario.Value));
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                UpdatePanel2.Update();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

        protected void btnNuevoTicket_Click(object sender, EventArgs e)
        {
            Session["NroTicket"] = null;
            Response.Redirect("wfTickets.aspx");
        }

        protected void gvTickets_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvTickets.DataKeys[index];
                int? nNroTicket = null;

                if (dataKey != null)
                    nNroTicket = int.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["NroTicket"] = nNroTicket;
                    Response.Redirect("wfTickets.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                UpdatePanel2.Update();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

        protected void gvTickets_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var drv = (TicketInfo)e.Row.DataItem;
                if (drv != null && drv.IdEstadoTicket == 1)
                {
                    e.Row.CssClass = "warning";
                }
                if (drv != null && drv.IdEstadoTicket == 2)
                {
                    e.Row.CssClass = "info";
                }
                if (drv != null && drv.IdEstadoTicket == 3)
                {
                    e.Row.CssClass = "active";
                }
                if (drv != null && drv.IdEstadoTicket == 3)
                {
                    e.Row.CssClass = "success";
                }
                if (drv != null && drv.IdEstadoTicket == 4)
                {
                    e.Row.CssClass = "danger";
                }
            }
        }

        protected void gvTickets_PreRender(object sender, EventArgs e)
        {
            //var gv = (GridView) sender;
            //var pagerRow = (GridViewRow) gv.BottomPagerRow;
            //if (pagerRow != null && pagerRow.Visible == false)
            //    pagerRow.Visible = true;
        }

        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboCliente.SelectedValue.Equals("-1"))
            {
                Util.Util.CargaSedeCliente(cboSede, Int32.Parse(cboCliente.SelectedValue), null, true);
            }
        }
    }
}