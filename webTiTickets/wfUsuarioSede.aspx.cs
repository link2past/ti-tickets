using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfUsuarioSede : System.Web.UI.Page
// ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsuario.Text = Context.User.Identity.Name;
                Util.Util.CargaEstados(cboEstado, true);
                Util.Util.CargaAreaUsuarioSede(cboArea, true);

                if (Session["UsuarioSedeEdit"] != null)
                {
                    hfNuevo.Value = "F";
                    txtIdUsuarioSede.Text = Session["UsuarioSedeEdit"].ToString();
                    CargarUsuario();
                }
                else
                {
                    hfNuevo.Value = "N";
                    txtIdSede.Text = Session["SedeClienteEdit"].ToString();
                    ConsultarSede();
                    ActivarDesactivarCampos(true);
                }
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            txtIdCliente.Enabled = bEstado;
            txtCliente.Enabled = bEstado;
            txtIdSede.Enabled = bEstado;
            txtSede.Enabled = bEstado;
            txtNombre.Enabled = bEstado;
            cboArea.Enabled = bEstado;
            cboEstado.Enabled = bEstado;
        }

        private void ConsultarSede()
        {
            var oEntSede =
                new SedeCliente().Consultar(new SedeClienteInfo {IdSedeCliente = Int32.Parse(txtIdSede.Text)});
            if (oEntSede != null)
            {
                txtIdCliente.Text = oEntSede.IdCliente.ToString();
                txtCliente.Text = oEntSede.Cliente.RazonSocial;
                txtSede.Text = oEntSede.Nombre;
            }
        }

        private void CargarUsuario()
        {
            var oEntUsuarioSede =
                new UsuarioSede().Consultar(new UsuarioSedeInfo {IdUsuarioSede = Int32.Parse(txtIdUsuarioSede.Text)});

            if (oEntUsuarioSede != null)
            {
                txtIdCliente.Text = oEntUsuarioSede.IdCliente.ToString();
                txtCliente.Text = oEntUsuarioSede.Cliente.RazonSocial;
                txtIdSede.Text = oEntUsuarioSede.IdSede.ToString();
                txtSede.Text = oEntUsuarioSede.Sede.Nombre;
                txtNombre.Text = oEntUsuarioSede.Nombre;
                cboArea.SelectedValue = oEntUsuarioSede.IdAreaUsuarioSede.ToString();
                cboEstado.SelectedValue = oEntUsuarioSede.IdEstado.ToString();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["UsuarioSedeEdit"] = null;
            hfNuevo.Value = "N";
            txtIdCliente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtIdSede.Text = string.Empty;
            txtSede.Text = string.Empty;
            txtNombre.Text = string.Empty;
            cboArea.SelectedValue = "-1";
            cboEstado.SelectedValue = "-1";
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIdCliente.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar el cliente al que pertencece la sede del usuario.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (String.IsNullOrEmpty(txtIdSede.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar la sede a la que pertencece el cliente.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar el nombre del usuario.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboArea.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar el área a la que pertenece el usuario.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboEstado.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar el estado del usuario.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            var oEntUsuario = new UsuarioSedeInfo();
            oEntUsuario.IdUsuarioSede = String.IsNullOrEmpty(txtIdUsuarioSede.Text)
                                            ? (int?) null
                                            : Int32.Parse(txtIdUsuarioSede.Text);
            oEntUsuario.IdCliente = Int32.Parse(txtIdCliente.Text);
            oEntUsuario.IdSede = Int32.Parse(txtIdSede.Text);
            oEntUsuario.Nombre = txtNombre.Text.Trim();
            oEntUsuario.IdAreaUsuarioSede = Int32.Parse(cboArea.SelectedValue);
            oEntUsuario.IdEstado = Int32.Parse(cboEstado.SelectedValue);

            if (hfNuevo.Value.Equals("N"))
            {
                int? nIdUsuario = null;
                oEntUsuario.UsuarioCreacion = lblUsuario.Text;
                if (new UsuarioSede().Registrar(oEntUsuario, ref nIdUsuario))
                {
                    hfNuevo.Value = "F";
                    txtIdUsuarioSede.Text = nIdUsuario.ToString();
                    ActivarDesactivarCampos(false);
                    Util.Util.AlternarMensaje(true, "Se registró el usuario de la sede con éxito.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
                else
                {
                    Util.Util.AlternarMensaje(true, "No se pudo registrar el usuario de la sede.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
            }
            else
            {
                oEntUsuario.UsuarioModificacion = lblUsuario.Text;
                if (new UsuarioSede().Actualizar(oEntUsuario))
                {
                    hfNuevo.Value = "F";
                    ActivarDesactivarCampos(false);
                    Util.Util.AlternarMensaje(true, "Se actualizó el usuario de la sede con éxito.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
                else
                {
                    Util.Util.AlternarMensaje(true, "No se pudo actualizar el usuario de la sede.", alertaError,
                          alertaExito,
                          lblError, lblExito);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["SedeClienteEdit"] = txtIdSede.Text;
            Response.Redirect("wfSedeCliente.aspx");
        }

    }
}