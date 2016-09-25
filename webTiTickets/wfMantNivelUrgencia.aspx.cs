using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantNivelUrgencia : System.Web.UI.Page
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

                    Session["ListaNivelUrgencia"] = null;

                    CargarNivelUrgencia(new NivelUrgenciaInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void CargarNivelUrgencia(NivelUrgenciaInfo oParametros)
        {
            var oListaNivelUrgencia = (List<NivelUrgenciaInfo>)new NivelUrgencia().Listar(oParametros);
            Session["ListaNivelUrgencia"] = oListaNivelUrgencia;
            gvNivelUrgencia.DataSource = oListaNivelUrgencia;
            gvNivelUrgencia.DataBind();
        }

        private void Bind()
        {
            var oListaNivelUrgencia = new List<NivelUrgenciaInfo>();
            if (Session["ListaNivelUrgencia"] != null)
            {
                oListaNivelUrgencia = (List<NivelUrgenciaInfo>)Session["ListaNivelUrgencia"];
            }
            Session["ListaNivelUrgencia"] = oListaNivelUrgencia;
            gvNivelUrgencia.DataSource = oListaNivelUrgencia;
            gvNivelUrgencia.DataBind();

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaNivelUrgencia = new List<NivelUrgenciaInfo>();
            if (Session["ListaNivelUrgencia"] != null)
                oListaNivelUrgencia = (List<NivelUrgenciaInfo>)Session["ListaNivelUrgencia"];
            oListaNivelUrgencia.Add(new NivelUrgenciaInfo());
            Bind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtNivelUrgencia.Text) ? null : txtNivelUrgencia.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarNivelUrgencia(new NivelUrgenciaInfo(null, sDescripcion, nIdEstado, null, null, null));
        }

        protected void gvNivelUrgencia_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvNivelUrgencia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNivelUrgencia.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvNivelUrgencia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNivelUrgencia.EditIndex = -1;
            Bind();
        }

        protected void gvNivelUrgencia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvNivelUrgencia.Rows[e.RowIndex];
                //var nFila = e.RowIndex + 1;

                var lblIdNivelUrgenciaG = (Label)fila.FindControl("lblIdNivelUrgenciaG");
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

                var oEntNivelUrgencia = new NivelUrgenciaInfo();

                if (String.IsNullOrEmpty(lblIdNivelUrgenciaG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntNivelUrgencia.IdNivelUrgencia = Int32.Parse(lblIdNivelUrgenciaG.Text);

                oEntNivelUrgencia.Descripcion = txtDescripcionG.Text;
                oEntNivelUrgencia.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntNivelUrgencia.UsuarioCreacion = lblUsuario.Text;
                    if (new NivelUrgencia().Registrar(oEntNivelUrgencia, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró el nivel de urgencia con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvNivelUrgencia.EditIndex = -1;
                        CargarNivelUrgencia(new NivelUrgenciaInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar el nivel de urgencia.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntNivelUrgencia.UsuarioModificacion = lblUsuario.Text;
                    if (new NivelUrgencia().Actualizar(oEntNivelUrgencia))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó el nivel de urgencia con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvNivelUrgencia.EditIndex = -1;
                        CargarNivelUrgencia(new NivelUrgenciaInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar el nivel de urgencia.",
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