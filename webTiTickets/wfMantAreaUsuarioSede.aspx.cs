using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantAreaUsuarioSede : System.Web.UI.Page
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

                    Session["ListaAreaUsuario"] = null;

                    CargarAreas(new AreaUsuarioSedeInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void CargarAreas(AreaUsuarioSedeInfo oParametros)
        {
            var oListaAreas = (List<AreaUsuarioSedeInfo>)new AreaUsuarioSede().Listar(oParametros);
            Session["ListaAreaUsuario"] = oListaAreas;
            gvAreaUsuario.DataSource = oListaAreas;
            gvAreaUsuario.DataBind();
        }

        private void Bind()
        {
            var oListaAreas = new List<AreaUsuarioSedeInfo>();
            if (Session["ListaAreaUsuario"] != null)
            {
                oListaAreas = (List<AreaUsuarioSedeInfo>)Session["ListaAreaUsuario"];
            }
            Session["ListaAreaUsuario"] = oListaAreas;
            gvAreaUsuario.DataSource = oListaAreas;
            gvAreaUsuario.DataBind();

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaCategorias = new List<AreaUsuarioSedeInfo>();
            if (Session["ListaAreaUsuario"] != null)
                oListaCategorias = (List<AreaUsuarioSedeInfo>)Session["ListaAreaUsuario"];
            oListaCategorias.Add(new AreaUsuarioSedeInfo());
            Bind();

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtAreaUsuario.Text) ? null : txtAreaUsuario.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarAreas(new AreaUsuarioSedeInfo(null, sDescripcion, nIdEstado, null, null, null));
        }

        protected void gvAreaUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAreaUsuario.EditIndex = -1;
            Bind();
        }

        protected void gvAreaUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAreaUsuario.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvAreaUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvAreaUsuario.Rows[e.RowIndex];

                var lblIdAreaUsuarioG = (Label)fila.FindControl("lblIdAreaUsuarioG");
                var txtDescripcionG = (TextBox)fila.FindControl("txtDescripcionG");
                var cboEstadoG = (DropDownList)fila.FindControl("cboEstadoG");

                if (String.IsNullOrEmpty(txtDescripcionG.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la descripción del área.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboEstadoG.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el estado del área.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                var oEntAreaUsuario = new AreaUsuarioSedeInfo();

                if (String.IsNullOrEmpty(lblIdAreaUsuarioG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntAreaUsuario.IdAreaUsuarioSede = Int32.Parse(lblIdAreaUsuarioG.Text);

                oEntAreaUsuario.Descripcion = txtDescripcionG.Text;
                oEntAreaUsuario.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntAreaUsuario.UsuarioCreacion = lblUsuario.Text;
                    if (new AreaUsuarioSede().Registrar(oEntAreaUsuario, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró el área con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvAreaUsuario.EditIndex = -1;
                        CargarAreas(new AreaUsuarioSedeInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar el área.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntAreaUsuario.UsuarioModificacion = lblUsuario.Text;
                    if (new AreaUsuarioSede().Actualizar(oEntAreaUsuario))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó el área.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvAreaUsuario.EditIndex = -1;
                        CargarAreas(new AreaUsuarioSedeInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar el área.",
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

        protected void gvAreaUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}