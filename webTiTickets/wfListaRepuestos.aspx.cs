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
    public partial class wfListaRepuestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstados(cboEstado, true);
                    CargarRepuestos();
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);  
            }

        }

        private void CargarRepuestos()
        {
            int? nIdRepuesto = String.IsNullOrEmpty(txtIdRepuesto.Text) ? (int?)null : Int32.Parse(txtIdRepuesto.Text);
            var sDescripcion = String.IsNullOrEmpty(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            var oListaRepuestos = new Repuesto().Listar(new RepuestoInfo(){IdRepuesto = nIdRepuesto, Descripcion = sDescripcion, IdEstado = nIdEstado});
            gvRepuestos.DataSource = oListaRepuestos;
            gvRepuestos.DataBind();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["RepuestoEdit"] = null;
            Response.Redirect("wfRepuesto.aspx");
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarRepuestos();
        }

        protected void gvRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvRepuestos.DataKeys[index];
                int? nIdRepuesto = null;

                if (dataKey != null)
                    nIdRepuesto = int.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["RepuestoEdit"] = nIdRepuesto;
                    Response.Redirect("wfRepuesto.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                UpdatePanel2.Update();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

    }
}