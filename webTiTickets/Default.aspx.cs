using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConsultarCliente();
            }
            
        }

        private void ConsultarCliente()
        {
            lblUsuario.Text = Context.User.Identity.Name;
            var oUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));
            
            if (oUsuario.IdTipoUsuario != (int)Util.Util.TipoUsuarioEnum.Administrador)
            {
                Response.Redirect("wfListaTickets.aspx");
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }
    }
}