using System;
using System.Web.UI;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfPerfil : Page
// ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsuario.Text = Context.User.Identity.Name;
                var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));

                if (oEntUsuario != null)
                {
                    txtIdUsuario.Text = oEntUsuario.Usuario;
                    txtNombre.Text = oEntUsuario.Nombre;
                    txtEmail.Text = oEntUsuario.Email;


                    if (oEntUsuario.IdTipoUsuario != (int)Util.Util.TipoUsuarioEnum.Administrador)
                    {
                        menuMantenimientos.Visible = false;
                    }

                    if (oEntUsuario.IdTipoUsuario == (int) Util.Util.TipoUsuarioEnum.Cliente)
                    {
                        var oUsuarioCliente =
                            new UsuarioCliente().Consultar(new UsuarioClienteInfo(oEntUsuario.Usuario, null, null));
                        if (oUsuarioCliente != null)
                        {
                            txtCliente.Text = oUsuarioCliente.Cliente.RazonSocial;
                        }
                    }
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
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

            if (!Util.Util.EsCorreoElectronicoValido(txtEmail.Text.Trim()))
            {
                Util.Util.AlternarMensaje(false, "Debe ingresar un correo electrónico válido", alertaError, alertaExito,
                                          lblError, lblExito);
                return;                
            }

            if (!String.IsNullOrEmpty(txtNuevaContrasena.Text))
            {
                if (String.IsNullOrEmpty(txtContrasena.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe confirmar la contraseña del usuario", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;
                }

                var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));

                if (!Util.Security.VerifyHash(txtContrasena.Text, "SHA512", oEntUsuario.Contraseña))
                {
                    Util.Util.AlternarMensaje(false, "La contaseña actual ingresada no es correcta.", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;                    
                }

                if (!txtNuevaContrasena.Text.Trim().Equals(txtConfirmaContrasena.Text.Trim()))
                {
                    Util.Util.AlternarMensaje(false,
                                              "La contraseña ingresada y la de confirmación no son iguales. Por favor verifique.",
                                              alertaError, alertaExito, lblError, lblExito);
                    return;
                }
            }

            var oUsuario = new UsuarioInfo();
            oUsuario.Usuario = txtIdUsuario.Text;
            oUsuario.Nombre = txtNombre.Text.Trim();
            oUsuario.Email = txtEmail.Text.Trim();
            oUsuario.Contraseña = String.IsNullOrEmpty(txtNuevaContrasena.Text) ? null : Util.Security.ComputeHash(txtNuevaContrasena.Text.Trim(), "SHA512", null);

            if (new Usuario().Actualizar(oUsuario))
            {
                Util.Util.AlternarMensaje(true, "Se actualizó el usuario con éxito.", alertaError, alertaExito,
                          lblError, lblExito);
            }
        }
    }
}