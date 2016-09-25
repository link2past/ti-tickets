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
    public partial class wfListaUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstados(cboEstado, true);
                    Util.Util.CargaTipoUsuario(cboTipoUsuario, true);
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                }
            }
        }

        private void CargarUsuarios()
        {
            var sIdUsuario = String.IsNullOrEmpty(txtIdUsuario.Text) ? null : txtIdUsuario.Text;
            var sNombre = String.IsNullOrEmpty(txtNombre.Text) ? null : txtNombre.Text;
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);
            var nIdTipoUsuario = cboTipoUsuario.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboTipoUsuario.SelectedValue);

            var oListaUsuarios =
                new Usuario().Listar(new UsuarioInfo() {Usuario = sIdUsuario, Nombre = sNombre, IdEstado = nIdEstado, IdTipoUsuario = nIdTipoUsuario});

            gvUsuarios.DataSource = oListaUsuarios;
            gvUsuarios.DataBind();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvUsuarios.DataKeys[index];
                String sUsuario = null;

                if (dataKey != null)
                    sUsuario = dataKey.Value.ToString();

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["UsuarioEdit"] = sUsuario;
                    Response.Redirect("wfUsuario.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                UpdatePanel2.Update();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["UsuarioEdit"] = null;
            Response.Redirect("wfUsuario.aspx");
        }
    }
}