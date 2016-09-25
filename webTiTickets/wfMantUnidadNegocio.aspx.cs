using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantUnidadNegocio : System.Web.UI.Page
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

                    Session["ListaUnidadNegocio"] = null;

                    CargarUnidadNegocio(new UnidadNegocioInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void CargarUnidadNegocio(UnidadNegocioInfo oParametros)
        {
            var oListaUnidadNegocio = (List<UnidadNegocioInfo>)new UnidadNegocio().Listar(oParametros);
            Session["ListaUnidadNegocio"] = oListaUnidadNegocio;
            gvUnidadNegocio.DataSource = oListaUnidadNegocio;
            gvUnidadNegocio.DataBind();
        }

        private void Bind()
        {
            var oListaUnidadNegocio = new List<UnidadNegocioInfo>();
            if (Session["ListaUnidadNegocio"] != null)
            {
                oListaUnidadNegocio = (List<UnidadNegocioInfo>)Session["ListaUnidadNegocio"];
            }
            Session["ListaUnidadNegocio"] = oListaUnidadNegocio;
            gvUnidadNegocio.DataSource = oListaUnidadNegocio;
            gvUnidadNegocio.DataBind();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaUnidadNegocio = new List<UnidadNegocioInfo>();
            if (Session["ListaUnidadNegocio"] != null)
                oListaUnidadNegocio = (List<UnidadNegocioInfo>)Session["ListaUnidadNegocio"];
            oListaUnidadNegocio.Add(new UnidadNegocioInfo());
            Bind();

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtUnidadNegocio.Text) ? null : txtUnidadNegocio.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarUnidadNegocio(new UnidadNegocioInfo(null, sDescripcion, nIdEstado, null, null, null));
        }

        protected void gvUnidadNegocio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUnidadNegocio.EditIndex = -1;
            Bind();
        }

        protected void gvUnidadNegocio_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvUnidadNegocio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUnidadNegocio.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvUnidadNegocio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvUnidadNegocio.Rows[e.RowIndex];
                //var nFila = e.RowIndex + 1;

                var lblIdUnidadNegocioG = (Label)fila.FindControl("lblIdUnidadNegocioG");
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

                var oEntUnidadNegocio = new UnidadNegocioInfo();

                if (String.IsNullOrEmpty(lblIdUnidadNegocioG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntUnidadNegocio.IdUnidadNegocio = Int32.Parse(lblIdUnidadNegocioG.Text);

                oEntUnidadNegocio.Descripcion = txtDescripcionG.Text;
                oEntUnidadNegocio.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntUnidadNegocio.UsuarioCreacion = lblUsuario.Text;
                    if (new UnidadNegocio().Registrar(oEntUnidadNegocio, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró la unidad de negocio con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvUnidadNegocio.EditIndex = -1;
                        CargarUnidadNegocio(new UnidadNegocioInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar la unidad de negocio.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntUnidadNegocio.UsuarioModificacion = lblUsuario.Text;
                    if (new UnidadNegocio().Actualizar(oEntUnidadNegocio))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó la unidad de negocio con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvUnidadNegocio.EditIndex = -1;
                        CargarUnidadNegocio(new UnidadNegocioInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar la unidad de negocio.",
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