using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
    // ReSharper disable InconsistentNaming
    public partial class wfUsuario : System.Web.UI.Page
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
                    Util.Util.CargaTipoUsuario(cboTipoUsuario, true);

                    if (Session["UsuarioEdit"] != null)
                    {
                        hfNuevo.Value = "F";
                        txtIdUsuario.Text = Session["UsuarioEdit"].ToString();
                        CargarUsuario();
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

        private void CargarUsuario()
        {
            var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(txtIdUsuario.Text, null, null, null));
            if (oEntUsuario != null)
            {
                txtNombre.Text = oEntUsuario.Nombre;
                txtEmail.Text = oEntUsuario.Email;
                cboEstado.SelectedValue = oEntUsuario.IdEstado.ToString();
                cboTipoUsuario.SelectedValue = oEntUsuario.IdTipoUsuario.ToString();
                txtDiasCambio.Text = oEntUsuario.DiasSolicitudCambio.ToString();
                chkRequiereCambio.Checked = oEntUsuario.RequiereCambioClave.Equals("1");
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            txtIdUsuario.Enabled = bEstado;
            txtNombre.Enabled = bEstado;
            txtContrasena.Enabled = bEstado;
            txtConfirmaContrasena.Enabled = bEstado;
            txtEmail.Enabled = bEstado;
            txtDiasCambio.Enabled = bEstado;
            cboEstado.Enabled = bEstado;
            cboTipoUsuario.Enabled = bEstado;
            chkRequiereCambio.Enabled = bEstado;
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
                if (String.IsNullOrEmpty(txtIdUsuario.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el ID del usuario", alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el nombre del usuario", alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtEmail.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el correo electrónico del usuario", alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }

                if (hfNuevo.Value.Equals("N"))
                {
                    if (String.IsNullOrEmpty(txtContrasena.Text))
                    {
                        Util.Util.AlternarMensaje(false, "Debe indicar la contraseña del usuario", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        return;
                    }

                    if (String.IsNullOrEmpty(txtConfirmaContrasena.Text))
                    {
                        Util.Util.AlternarMensaje(false, "Debe confirmar la contraseña del usuario", alertaError,
                                                  alertaExito, lblError, lblExito);
                        return;
                    }

                    if (!txtContrasena.Text.Trim().Equals(txtConfirmaContrasena.Text.Trim()))
                    {
                        Util.Util.AlternarMensaje(false,
                                                  "La contraseña ingresada y la de confirmación no son iguales. Por favor verifique.",
                                                  alertaError, alertaExito, lblError, lblExito);
                        return;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(txtContrasena.Text))
                    {
                        if (String.IsNullOrEmpty(txtConfirmaContrasena.Text))
                        {
                            Util.Util.AlternarMensaje(false, "Debe confirmar la contraseña del usuario", alertaError,
                                                      alertaExito, lblError, lblExito);
                            return;
                        }

                        if (!txtContrasena.Text.Trim().Equals(txtConfirmaContrasena.Text.Trim()))
                        {
                            Util.Util.AlternarMensaje(false,
                                                      "La contraseña ingresada y la de confirmación no son iguales. Por favor verifique.",
                                                      alertaError, alertaExito, lblError, lblExito);
                            return;
                        }
                    }
                }

                if (cboEstado.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el estado del usuario", alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }

                if (cboTipoUsuario.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar el tipo de usuario", alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtDiasCambio.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar los días requeridos para cambiar de contraseña",
                                              alertaError, alertaExito,
                                              lblError, lblExito);
                    return;
                }
                if (!Util.Util.EsEnteroPositivo(txtDiasCambio.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe indicar un número válido para los días requeridos para cambiar de contraseña",
                                              alertaError, alertaExito,
                                              lblError, lblExito);
                    return;    
                }

                var oEntUsuario = new UsuarioInfo();
                oEntUsuario.Usuario = txtIdUsuario.Text.Trim();
                oEntUsuario.Nombre = txtNombre.Text.Trim();
                oEntUsuario.Email = txtEmail.Text.Trim();
                oEntUsuario.IdTipoUsuario = Int32.Parse(cboTipoUsuario.SelectedValue);
                oEntUsuario.IdEstado = Int32.Parse(cboEstado.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                    oEntUsuario.Contraseña = Util.Security.ComputeHash(txtContrasena.Text.Trim(), "SHA512", null);
                else
                {
                    if (!String.IsNullOrEmpty(txtContrasena.Text))
                    {
                        oEntUsuario.Contraseña = Util.Security.ComputeHash(txtContrasena.Text.Trim(), "SHA512", null);
                    }
                }

                oEntUsuario.DiasSolicitudCambio = Int32.Parse(txtDiasCambio.Text);
                oEntUsuario.RequiereCambioClave = chkRequiereCambio.Checked ? "1" : "0";

                if (hfNuevo.Value.Equals("N"))
                {
                    if (new Usuario().Registrar(oEntUsuario))
                    {
                        hfNuevo.Value = "F";
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se registró el usuario con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo registrar el usuario.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                }
                else
                {
                    if (new Usuario().Actualizar(oEntUsuario))
                    {
                        hfNuevo.Value = "F";
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se actualizó el usuario con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo registrar el usuario.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                }
            }

            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["UsuarioEdit"] = null;
            Response.Redirect("wfListaUsuarios.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["UsuarioEdit"] = null;
            hfNuevo.Value = "N";
            ActivarDesactivarCampos(true);
            txtIdUsuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtConfirmaContrasena.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDiasCambio.Text = string.Empty;
            chkRequiereCambio.Checked = false;
            cboEstado.SelectedValue = "-1";
            cboTipoUsuario.SelectedValue = "-1";
            txtIdUsuario.Focus();
        }


    }
}