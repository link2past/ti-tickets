using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfMantCategoria : System.Web.UI.Page
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

                    Session["ListaCategorias"] = null;

                    CargarCategorias(new CategoriaProblemaInfo());
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }

        }

        private void CargarCategorias(CategoriaProblemaInfo oParametros)
        {
            var oListaCategorias = (List<CategoriaProblemaInfo>)new CategoriaProblema().Listar(oParametros);
            Session["ListaCategorias"] = oListaCategorias;
            gvCategorias.DataSource = oListaCategorias;
            gvCategorias.DataBind();
        }

        private void Bind()
        {
            var oListaCategorias = new List<CategoriaProblemaInfo>();
            if (Session["ListaCategorias"] != null)
            {
                oListaCategorias = (List<CategoriaProblemaInfo>) Session["ListaCategorias"];
            }
            Session["ListaCategorias"] = oListaCategorias;
            gvCategorias.DataSource = oListaCategorias;
            gvCategorias.DataBind();

        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;
            hfNuevo.Value = "F";
            Bind();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            Bind();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var fila = gvCategorias.Rows[e.RowIndex];

                var lblIdCategoriaG = (Label)fila.FindControl("lblIdCategoriaG");
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

                var oEntCategoria = new CategoriaProblemaInfo();

                if (String.IsNullOrEmpty(lblIdCategoriaG.Text))
                    hfNuevo.Value = "N";

                if (!hfNuevo.Value.Equals("N"))
                    oEntCategoria.IdCategoriaProblema = Int32.Parse(lblIdCategoriaG.Text);

                oEntCategoria.Descripcion = txtDescripcionG.Text;
                oEntCategoria.IdEstado = Int32.Parse(cboEstadoG.SelectedValue);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nId = null;
                    oEntCategoria.UsuarioCreacion = lblUsuario.Text;
                    if (new CategoriaProblema().Registrar(oEntCategoria, ref nId))
                    {
                        Util.Util.AlternarMensaje(true, "Se registró la categoría de problema con éxito.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvCategorias.EditIndex = -1;
                        CargarCategorias(new CategoriaProblemaInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo registrar la categoría de problema.",
                                                  alertaError, alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                    }
                }
                else
                {
                    oEntCategoria.UsuarioModificacion = lblUsuario.Text;
                    if (new CategoriaProblema().Actualizar(oEntCategoria))
                    {
                        Util.Util.AlternarMensaje(true, "Se actualizó la categoría de problema.", alertaError,
                                                  alertaExito,
                                                  lblError, lblExito);
                        UpdatePanel2.Update();
                        gvCategorias.EditIndex = -1;
                        CargarCategorias(new CategoriaProblemaInfo());
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se pudo actualizar la categoría de problema.",
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

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var oListaCategorias = new List<CategoriaProblemaInfo>();
            if (Session["ListaCategorias"] != null)
                oListaCategorias = (List<CategoriaProblemaInfo>)Session["ListaCategorias"];
            oListaCategorias.Add(new CategoriaProblemaInfo());
            Bind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var sDescripcion = String.IsNullOrEmpty(txtCategoria.Text) ? null : txtCategoria.Text.Trim();
            var nIdEstado = cboEstado.SelectedValue.Equals("-1") ? (int?)null : Int32.Parse(cboEstado.SelectedValue);

            CargarCategorias(new CategoriaProblemaInfo(null, sDescripcion, nIdEstado, null, null, null));
        }

        protected void gvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
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