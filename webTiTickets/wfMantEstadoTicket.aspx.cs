using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantEstadoTicket : System.Web.UI.Page
// ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstados(cboEstado, true);

                    Session["ListaEstadosTicket"] = null;

                    CargarCategorias(new EstadoTicketInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void CargarCategorias(EstadoTicketInfo oParametros)
        {
            var oListaEstadosTicket = (List<EstadoTicketInfo>) new EstadoTicket().Listar(oParametros);
            Session["ListaEstadosTicket"] = oListaEstadosTicket;
            gvEstadoTicket.DataSource = oListaEstadosTicket;
            gvEstadoTicket.DataBind();
        }

        private void Bind()
        {
            var oListaEstadosTicket = new List<EstadoTicketInfo>();
            if (Session["ListaEstadosTicket"] != null)
            {
                oListaEstadosTicket = (List<EstadoTicketInfo>)Session["ListaEstadosTicket"];
            }
            Session["ListaEstadosTicket"] = oListaEstadosTicket;
            gvEstadoTicket.DataSource = oListaEstadosTicket;
            gvEstadoTicket.DataBind();

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void gvEstadoTicket_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEstadoTicket.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvEstadoTicket_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvEstadoTicket.Rows[e.RowIndex];
                //var nFila = e.RowIndex + 1;

                var lblIdEstadoTicketG = (Label)fila.FindControl("lblIdEstadoTicketG");
                var txtDescripcionG = (TextBox)fila.FindControl("txtDescripcionG");
                var cboEstadoG = (DropDownList)fila.FindControl("cboEstadoG");

                if (String.IsNullOrEmpty(txtDescripcionG.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la descripción del estado de ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboEstadoG.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el estado del estado de ticket.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                var oEntEstadoTicket = new EstadoTicketInfo();

                if (String.IsNullOrEmpty(lblIdEstadoTicketG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntEstadoTicket.IdEstadoTicket = Int32.Parse(lblIdEstadoTicketG.Text);

                oEntEstadoTicket.Descripcion = txtDescripcionG.Text;
                oEntEstadoTicket.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntEstadoTicket.UsuarioCreacion = lblUsuario.Text;
                    if (new EstadoTicket().Registrar(oEntEstadoTicket, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró el estado de ticket con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvEstadoTicket.EditIndex = -1;
                        CargarCategorias(new EstadoTicketInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar el estado de ticket.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntEstadoTicket.UsuarioModificacion = lblUsuario.Text;
                    if (new EstadoTicket().Actualizar(oEntEstadoTicket))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó el estado de ticket con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvEstadoTicket.EditIndex = -1;
                        CargarCategorias(new EstadoTicketInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar el estado de ticket.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }

            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void gvEstadoTicket_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEstadoTicket.EditIndex = -1;
            Bind();
        }

        protected void gvEstadoTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var cboEstadoG = (DropDownList)e.Row.FindControl("cboEstadoG");
                if (cboEstadoG != null)
                {
                    Util.Util.CargaEstados(cboEstadoG, true);
                }
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaEstadosTicket = new List<EstadoTicketInfo>();
            if (Session["ListaEstadosTicket"] != null)
                oListaEstadosTicket = (List<EstadoTicketInfo>)Session["ListaEstadosTicket"];
            oListaEstadosTicket.Add(new EstadoTicketInfo());
            Bind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtEstadoTicket.Text) ? null : txtEstadoTicket.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarCategorias(new EstadoTicketInfo(null, sDescripcion, nIdEstado, null, null, null));
        }
    }
}