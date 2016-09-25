using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                //var oUsuario = new UsuarioInfo(txtUsuario.Text.Trim(), txtPassword.Text.Trim(), null, null);
                var oUsuario = new Usuario().Consultar(new UsuarioInfo(txtUsuario.Text.Trim(), null, null, null));
                //if (new Usuario().ValidarUsuario(oUsuario))
                //{
                //    oUsuario = new Usuario().Consultar(oUsuario);
                //    Session["Usuario"] = oUsuario;
                //    FormsAuthentication.RedirectFromLoginPage(oUsuario.Usuario, false);
                //}
                //else
                //{
                //    alertaError.Visible = true;
                //    lblError.Text = "Usuario o Contraseña inválido. Por favor vuelva a intentar.";
                //}
                if (oUsuario != null)
                {
                    if (Util.Security.VerifyHash(txtPassword.Text, "SHA512", oUsuario.Contraseña))
                    {
                        oUsuario.Contraseña = null;
                        Session["Usuario"] = oUsuario;
                        FormsAuthentication.RedirectFromLoginPage(oUsuario.Usuario, false);
                    }
                    else
                    {
                        alertaError.Visible = true;
                        lblError.Text = "Usuario o Contraseña inválido. Por favor vuelva a intentar.";
                    }
                }
            }
            catch (Exception ex)
            {
                alertaError.Visible = true;
                lblError.Text = "Ocurrió el siguiente error: " + ex.Message;
            }
        }
    }
}