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
    public partial class wfListaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargarDepartamentos(cboDepartamento, true);
                    Util.Util.CargaEstados(cboEstado, true);

                    ListarClientes();
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void ListarClientes()
        {
            var sRazonSocial = String.IsNullOrEmpty(txtRazonSocial.Text.Trim()) ? null : txtRazonSocial.Text.Trim();
            var sNroDi = String.IsNullOrEmpty(txtNroDi.Text.Trim()) ? null : txtNroDi.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            var sIdDepartamento = cboDepartamento.SelectedValue.Equals("-1") ? null : cboDepartamento.SelectedValue;
            String sProvincia = null;
            String sDistrito = null;
            if (sIdDepartamento != null)
            {
                sProvincia = cboProvincia.SelectedValue.Equals("-1")
                                 ? null
                                 : cboProvincia.SelectedValue;

                if (sProvincia != null)
                {
                    sDistrito = cboDistrito.SelectedValue.Equals("-1")
                                    ? null
                                    : cboDistrito.SelectedValue;
                }
            }

            var oListaClientes =
                new Cliente().Listar(new ClienteInfo()
                    {
                        RazonSocial = sRazonSocial,
                        NroDi = sNroDi,
                        IdDepartamento = sIdDepartamento,
                        IdProvincia = sProvincia,
                        IdDistrito = sDistrito,
                        IdEstado = nIdEstado
                    });

            gvClientes.DataSource = oListaClientes;
            gvClientes.DataBind();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["ClienteEdit"] = null;
            Response.Redirect("wfCliente.aspx");
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                ListarClientes();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvClientes.DataKeys[index];
                int? nIdCliente = null;

                if (dataKey != null)
                    nIdCliente = int.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["ClienteEdit"] = nIdCliente;
                    Response.Redirect("wfCliente.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                UpdatePanel2.Update();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            }
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.Util.CargarProvincias(cboDepartamento, cboProvincia, cboDistrito, true);
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.Util.CargarDistritos(cboDepartamento, cboProvincia, cboDistrito, true);
        }
    }
}