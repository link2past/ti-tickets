﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;
using System.Linq;
using System.Text;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfReporteClienteUn : Page
// ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["Reporte"] = null;
                    lblUsuario.Text = Context.User.Identity.Name;
                    txtFechaDesde.Text =
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
                    txtFechaHasta.Text =
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString();
                    Util.Util.CargaUnidadNegocio(cboUnidadNegocio, true);

                    var oEntUsuario = new Usuario().Consultar(new UsuarioInfo(lblUsuario.Text, null, null, null));

                    if (oEntUsuario.IdTipoUsuario != (int)Util.Util.TipoUsuarioEnum.Administrador)
                    {
                        menuMantenimientos.Visible = false;
                    }

                    if (oEntUsuario.IdTipoUsuario == (int) Util.Util.TipoUsuarioEnum.Cliente)
                    {
                        var oUsuarioCliente =
                            new UsuarioCliente().Consultar(new UsuarioClienteInfo(lblUsuario.Text, null, 1));
                        txtIdCliente.Text = oUsuarioCliente.IdCliente.ToString();
                        txtCliente.Text = oUsuarioCliente.Cliente.RazonSocial;
                        btnBuscarCliente.Enabled = false;
                    }
                    else
                    {
                        btnBuscarCliente.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                var nIdCliente = String.IsNullOrEmpty(txtIdCliente.Text) ? (int?) null : Int32.Parse(txtIdCliente.Text);
                var nIdUnidadNegocio = cboUnidadNegocio.SelectedValue.Equals("-1")
                                           ? (int?) null
                                           : Int32.Parse(cboUnidadNegocio.SelectedValue);
                var dFechaDesde = String.IsNullOrEmpty(txtFechaDesde.Text)
                                      ? (DateTime?) null
                                      : DateTime.Parse(txtFechaDesde.Text);
                var dFechaHasta = String.IsNullOrEmpty(txtFechaHasta.Text)
                                      ? (DateTime?) null
                                      : DateTime.Parse(txtFechaHasta.Text);

                var oResultado =
                    new ReporteClienteUn().Procesar(new ReporteClienteUnInfo(null, nIdCliente, null, nIdUnidadNegocio,
                                                                             dFechaDesde, dFechaHasta));
                var oResultadoExcel = new ReporteClienteUn().Procesar2(new ReporteClienteUnInfo(null, nIdCliente, null, nIdUnidadNegocio,
                                                                             dFechaDesde, dFechaHasta));

                Session["Reporte"] = oResultadoExcel;

                var totalUnidad = from ticket in oResultado
                                  group ticket by ticket.Ticket.Sede.UnidadNegocio.Descripcion
                                  into newGroup
                                  select
                                      new
                                          {
                                              UnidadNegocio = newGroup.Key,
                                              TotalRepuestos = newGroup.Sum(x => x.TotalRepuestos),
                                              TotalAtencion = newGroup.Sum(x => x.Tarifa),
                                              Suma = newGroup.Sum(x => x.TotalRepuestos + x.Tarifa)
                                          };
                var totalCliente = from ticket in oResultado
                                   group ticket by ticket.Ticket.Cliente.RazonSocial
                                   into newGroup
                                   select
                                       new
                                           {
                                               Cliente = newGroup.Key,
                                               TotalRepuestos = newGroup.Sum(x => x.TotalRepuestos),
                                               TotalAtencion = newGroup.Sum(x => x.Tarifa),
                                               Suma = newGroup.Sum(x => x.TotalRepuestos + x.Tarifa)
                                           };

                rptTickets.DataSource = oResultado;
                rptTickets.DataBind();

                gvResultadoUnidad.DataSource = totalUnidad;
                gvResultadoUnidad.DataBind();

                gvResultadoCliente.DataSource = totalCliente;
                gvResultadoCliente.DataBind();
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void rptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var gvRepuestosTicket = (GridView)e.Item.FindControl("gvRepuestos");
                    var lblNroTicket = (Label)e.Item.FindControl("lblTicket");
                    gvRepuestosTicket.AutoGenerateColumns = false;
                    gvRepuestosTicket.DataSource =
                        new ReporteClienteUn().ProcesarDetalle(new ReporteClienteUnInfo(Int32.Parse(lblNroTicket.Text), null, null,
                                                                                 null, null, null));
                    gvRepuestosTicket.DataBind();
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
            
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalConsultaCliente').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void btnConsultarCliente_Click(object sender, EventArgs e)
        {
            var sRazonSocial = String.IsNullOrEmpty(txtRazonSocialCliente.Text.Trim())
                ? null
                : txtRazonSocialCliente.Text.Trim();
            gvClientes.DataSource = new Cliente().Listar(new ClienteInfo { RazonSocial = sRazonSocial });
            gvClientes.DataBind();
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvClientes.DataKeys[index];
                int? nIdCliente = null;

                if (dataKey != null)
                    nIdCliente = Int32.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    //txtObservaciones.Text = sIdTecnico;
                    var oEntCliente = new Cliente().Consultar(new ClienteInfo { IdCliente = nIdCliente });

                    if (oEntCliente != null)
                    {
                        txtIdCliente.Text = oEntCliente.IdCliente.ToString();
                        txtCliente.Text = oEntCliente.RazonSocial;

                        //Util.Util.CargaSedeCliente(cboSede, oEntCliente.IdCliente, true);
                    }

                    var sb = new StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#modalConsultaCliente').modal('hide');");
                    sb.Append(@"</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Hide", sb.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);

                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al seleccionar el cliente: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
                //UpdatePanel2.Update();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReporteClienteUnidadNegocio.xls");
            Response.Charset = "";
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());


            var datos = (List<ReporteClienteUnInfo>)Session["Reporte"];

            gvExcel.Visible = true;
            gvExcel.DataSource = datos;
            gvExcel.DataBind();

            var stringWrite = new System.IO.StringWriter();
            var htmlWrite = new HtmlTextWriter(stringWrite);

            var frm = new HtmlForm();
            gvExcel.Parent.Controls.Add(frm);
            gvResultadoCliente.Parent.Controls.Add(frm);
            gvResultadoUnidad.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvExcel);
            frm.Controls.Add(gvResultadoCliente);
            frm.Controls.Add(gvResultadoUnidad);

            frm.RenderControl(htmlWrite);
            Response.Write("<table>");
            Response.Write(stringWrite.ToString());
            Response.Write("</table>");
            Response.End();
            gvExcel.Visible = false;

        }

        private void PrepareForExport(GridView gv)
        {
            gv.DataBind();

            //Change the Header Row back to white color
            gv.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            for (int k = 0; k < gv.HeaderRow.Cells.Count; k++)
            {
                gv.HeaderRow.Cells[k].Style.Add("background-color", "green");
            }

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                GridViewRow row = gv.Rows[i];

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    for (int j = 0; j < gv.Rows[i].Cells.Count; j++)
                    {
                        row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }
            }            
        }
    }
}