using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantTipoUsuario : System.Web.UI.Page
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

                    Session["ListaTipoUsuario"] = null;

                    CargarCategorias(new TipoUsuarioInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void CargarCategorias(TipoUsuarioInfo oParametros)
        {
            var oListaTipoUsuario = (List<TipoUsuarioInfo>)new TipoUsuario().Listar(oParametros);
            Session["ListaTipoUsuario"] = oListaTipoUsuario;
            gvTipoUsuario.DataSource = oListaTipoUsuario;
            gvTipoUsuario.DataBind();
        }

        private void Bind()
        {
            var oListaTipoUsuario = new List<TipoUsuarioInfo>();
            if (Session["ListaTipoUsuario"] != null)
            {
                oListaTipoUsuario = (List<TipoUsuarioInfo>)Session["ListaTipoUsuario"];
            }
            Session["ListaTipoUsuario"] = oListaTipoUsuario;
            gvTipoUsuario.DataSource = oListaTipoUsuario;
            gvTipoUsuario.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaTipoUsuario = new List<TipoUsuarioInfo>();
            if (Session["ListaTipoUsuario"] != null)
                oListaTipoUsuario = (List<TipoUsuarioInfo>)Session["ListaTipoUsuario"];
            oListaTipoUsuario.Add(new TipoUsuarioInfo());
            Bind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtTipoUsuario.Text) ? null : txtTipoUsuario.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarCategorias(new TipoUsuarioInfo(null, sDescripcion, nIdEstado, null, null, null));
        }

        protected void gvTipoUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipoUsuario.EditIndex = -1;
            Bind();
        }

        protected void gvTipoUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipoUsuario.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvTipoUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void gvTipoUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvTipoUsuario.Rows[e.RowIndex];
                //var nFila = e.RowIndex + 1;

                var lblIdTipoUsuarioG = (Label)fila.FindControl("lblIdTipoUsuarioG");
                var txtDescripcionG = (TextBox)fila.FindControl("txtDescripcionG");
                var cboEstadoG = (DropDownList)fila.FindControl("cboEstadoG");

                if (String.IsNullOrEmpty(txtDescripcionG.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la descripción de la categoría.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboEstadoG.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el estado de la categoría.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                var oEntTipoUsuario = new TipoUsuarioInfo();

                if (String.IsNullOrEmpty(lblIdTipoUsuarioG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntTipoUsuario.IdTipoUsuario = Int32.Parse(lblIdTipoUsuarioG.Text);

                oEntTipoUsuario.Descripcion = txtDescripcionG.Text;
                oEntTipoUsuario.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntTipoUsuario.UsuarioCreacion = lblUsuario.Text;
                    if (new TipoUsuario().Registrar(oEntTipoUsuario, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró el tipo de usuario con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvTipoUsuario.EditIndex = -1;
                        CargarCategorias(new TipoUsuarioInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar el tipo de usuario.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntTipoUsuario.UsuarioModificacion = lblUsuario.Text;
                    if (new TipoUsuario().Actualizar(oEntTipoUsuario))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó el tipo de usuario con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvTipoUsuario.EditIndex = -1;
                        CargarCategorias(new TipoUsuarioInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar el tipo de usuario.",
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
    }
}