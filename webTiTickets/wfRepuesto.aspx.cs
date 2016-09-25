using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;
using System.Text;
using System.Web.UI;

namespace webTiTickets
{
    public partial class wfRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstados(cboEstado, true);
                    Util.Util.CargarMoneda(cboMoneda, true);

                    if (Session["RepuestoEdit"] != null)
                    {
                        hfNuevo.Value = "F";
                        txtIdRepuesto.Text = Session["RepuestoEdit"].ToString();
                        CargarRepuesto();
                    }
                    else
                    {
                        hfNuevo.Value = "N";
                        ActivarDesactivarCampos(true);
                    }
                }


            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            txtDescripcion.Enabled = bEstado;
            cboMoneda.Enabled = bEstado;
            cboEstado.Enabled = bEstado;
            txtPrecioActual.Enabled = bEstado;
        }

        private void CargarRepuesto()
        {
            var oRepuesto = new Repuesto().Consultar(new RepuestoInfo() {IdRepuesto = Int32.Parse(txtIdRepuesto.Text)});

            if (oRepuesto != null)
            {
                txtDescripcion.Text = oRepuesto.Descripcion;
                cboEstado.SelectedValue = oRepuesto.IdEstado.ToString();
                cboMoneda.SelectedValue = oRepuesto.IdMoneda;
                txtPrecioActual.Text = oRepuesto.PrecioActual.ToString();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["RepuestoEdit"] = null;
            ActivarDesactivarCampos(true);
            hfNuevo.Value = "N";
            txtIdRepuesto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cboMoneda.SelectedValue = "-1";
            txtPrecioActual.Text = string.Empty;
            cboEstado.SelectedValue = "-1";
            txtDescripcion.Focus();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la descripción del repuesto.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboMoneda.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar la moneda del precio del repuesto.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtPrecioActual.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el precio del repuesto.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (!Util.Util.EsNumerico(txtPrecioActual.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar un número válido en el precio del repuesto.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboEstado.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el estado del repuesto.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                var oEntRepuesto = new RepuestoInfo();
                oEntRepuesto.IdRepuesto = String.IsNullOrEmpty(txtIdRepuesto.Text) ? (int?)null : Int32.Parse(txtIdRepuesto.Text);
                oEntRepuesto.Descripcion = txtDescripcion.Text;
                oEntRepuesto.IdMoneda = cboMoneda.SelectedValue;
                oEntRepuesto.PrecioActual = Double.Parse(txtPrecioActual.Text);
                oEntRepuesto.IdEstado = Int32.Parse(cboEstado.SelectedValue);
                oEntRepuesto.StockActual = 0;

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nIdRepuesto = null;
                    oEntRepuesto.UsuarioCreacion = lblUsuario.Text;
                    if (new Repuesto().Registrar(oEntRepuesto, ref nIdRepuesto))
                    {
                        hfNuevo.Value = "F";
                        txtIdRepuesto.Text = nIdRepuesto.ToString();
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se registró el repuesto con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo registrar el repuesto.", alertaError, alertaExito,
                          lblError, lblExito);
                    }
                }
                else
                {
                    oEntRepuesto.UsuarioModificacion = lblUsuario.Text;
                    if (new Repuesto().Actualizar(oEntRepuesto))
                    {
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se actualizó el repuesto con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo actualizar el repuesto.", alertaError, alertaExito,
                          lblError, lblExito);                        
                    }
                }

            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el repuesto: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["RepuestoEdit"] = null;
            Response.Redirect("wfListaRepuestos.aspx");
        }
    }
}