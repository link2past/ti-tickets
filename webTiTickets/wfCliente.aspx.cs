using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;
using System.Text;
using System.Web.UI;

namespace webTiTickets
{
    public partial class wfCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUsuario.Text = Context.User.Identity.Name;
                    Util.Util.CargaEstados(cboEstado, true);
                    Util.Util.CargarDepartamentos(cboDepartamento, true);
                    Util.Util.CargarMoneda(cboMoneda, true);

                    if (Session["ClienteEdit"] != null)
                    {
                        hfNuevo.Value = "F";
                        txtIdCliente.Text = Session["ClienteEdit"].ToString();
                        CargarCliente();
                    }
                    else
                    {
                        hfNuevo.Value = "N";
                        ActivarDesactivarCampos(true);
                        btnAgregarSede.Enabled = false;
                        btnAgregarUsuario.Enabled = false;
                    }
                }


            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            txtRazonSocial.Enabled = bEstado;
            txtNroDi.Enabled = bEstado;
            txtDireccion.Enabled = bEstado;
            txtEmail.Enabled = bEstado;
            cboDepartamento.Enabled = bEstado;
            cboProvincia.Enabled = bEstado;
            cboDistrito.Enabled = bEstado;
            cboEstado.Enabled = bEstado;
            txtTelefono.Enabled = bEstado;
            txtEmail.Enabled = bEstado;
            txtContacto.Enabled = bEstado;
            txtCargo.Enabled = bEstado;
            cboMoneda.Enabled = bEstado;
            txtTarifaDiurna.Enabled = bEstado;
            txtTarifaNocturna.Enabled = bEstado;
        }

        private void CargarCliente()
        {
            var oEntCliente = new Cliente().Consultar(new ClienteInfo() { IdCliente = Int32.Parse(txtIdCliente.Text) });

            if (oEntCliente != null)
            {
                txtRazonSocial.Text = oEntCliente.RazonSocial;
                txtNroDi.Text = oEntCliente.NroDi;
                txtDireccion.Text = oEntCliente.Direccion;
                cboDepartamento.SelectedValue = oEntCliente.IdDepartamento;

                Util.Util.CargarProvincias(cboDepartamento, cboProvincia, cboDistrito, true);
                cboProvincia.SelectedValue = oEntCliente.IdProvincia;

                Util.Util.CargarDistritos(cboDepartamento, cboProvincia, cboDistrito, true);
                cboDistrito.SelectedValue = oEntCliente.IdDistrito;

                txtTelefono.Text = oEntCliente.Telefono;
                txtEmail.Text = oEntCliente.Email;
                txtContacto.Text = oEntCliente.NombreContacto;
                txtCargo.Text = oEntCliente.CargoContacto;

                cboEstado.SelectedValue = oEntCliente.IdEstado.ToString();
                cboMoneda.SelectedValue = oEntCliente.IdMoneda;
                txtTarifaDiurna.Text = oEntCliente.TarifaDiurna.ToString();
                txtTarifaNocturna.Text = oEntCliente.TarifaNocturna.ToString();

                btnAgregarSede.Enabled = true;
                btnAgregarUsuario.Enabled = true;

                var oListaSedes = new SedeCliente().Listar(new SedeClienteInfo(null, oEntCliente.IdCliente, null));
                gvSedes.DataSource = oListaSedes;
                gvSedes.DataBind();

                var oListaUsuarios =
                    new UsuarioCliente().Listar(new UsuarioClienteInfo(null, oEntCliente.IdCliente, null));
                gvUsuariosCliente.DataSource = oListaUsuarios;
                gvUsuariosCliente.DataBind();
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

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la razón social o nombre del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtNroDi.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el número de documento de identidad del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtDireccion.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la dirección del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboDepartamento.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el departamento de la ubicación del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboProvincia.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar la provincia de la ubicación del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboDistrito.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el distrito de la ubicación del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtTelefono.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el teléfono del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtEmail.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el correo electrónico del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtContacto.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el nombre del contacto del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtCargo.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar el cargo del contacto del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboEstado.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar el estado del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (cboMoneda.SelectedValue.Equals("-1"))
                {
                    Util.Util.AlternarMensaje(false, "Debe seleccionar la moneda de la tarifa del cliente.", alertaError, alertaExito, lblError, lblExito);
                    return;
                }

                if (String.IsNullOrEmpty(txtTarifaDiurna.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la tarifa diurna del cliente.", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;
                }
                if (!Util.Util.EsNumerico(txtTarifaDiurna.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar un número válido en la tarifa diurna del cliente.", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;                        
                }

                if (String.IsNullOrEmpty(txtTarifaNocturna.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar la tarifa nocturna del cliente.", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;
                }
                if (!Util.Util.EsNumerico(txtTarifaNocturna.Text))
                {
                    Util.Util.AlternarMensaje(false, "Debe ingresar un número válido en la tarifa nocturna del cliente.", alertaError,
                                              alertaExito, lblError, lblExito);
                    return;
                }

                var oEntCliente = new ClienteInfo();
                oEntCliente.IdCliente = String.IsNullOrEmpty(txtIdCliente.Text) ? (int?)null : Int32.Parse(txtIdCliente.Text.Trim());
                oEntCliente.RazonSocial = txtRazonSocial.Text.Trim();
                oEntCliente.NroDi = txtNroDi.Text.Trim();
                oEntCliente.Direccion = txtDireccion.Text.Trim();
                oEntCliente.IdDepartamento = cboDepartamento.SelectedValue;
                oEntCliente.IdProvincia = cboProvincia.SelectedValue;
                oEntCliente.IdDistrito = cboDistrito.SelectedValue;
                oEntCliente.Telefono = txtTelefono.Text.Trim();
                oEntCliente.Email = txtEmail.Text.Trim();
                oEntCliente.NombreContacto = txtContacto.Text.Trim();
                oEntCliente.CargoContacto = txtCargo.Text.Trim();
                oEntCliente.IdEstado = Int32.Parse(cboEstado.SelectedValue);
                oEntCliente.IdMoneda = cboMoneda.SelectedValue;
                oEntCliente.TarifaDiurna = Double.Parse(txtTarifaDiurna.Text);
                oEntCliente.TarifaNocturna = Double.Parse(txtTarifaNocturna.Text);

                if (hfNuevo.Value.Equals("N"))
                {
                    int? nIdCliente = null;
                    oEntCliente.UsuarioCreacion = lblUsuario.Text;
                    if (new Cliente().Registrar(oEntCliente, ref nIdCliente))
                    {
                        hfNuevo.Value = "F";
                        txtIdCliente.Text = nIdCliente.ToString();
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se registró el cliente con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo registrar el cliente.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                }
                else
                {
                    oEntCliente.UsuarioModificacion = lblUsuario.Text;
                    if (new Cliente().Actualizar(oEntCliente))
                    {
                        hfNuevo.Value = "F";
                        ActivarDesactivarCampos(false);
                        Util.Util.AlternarMensaje(true, "Se actualizó el cliente con éxito.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                    else
                    {
                        Util.Util.AlternarMensaje(false, "No se puedo actualizar el cliente.", alertaError, alertaExito,
                                                  lblError, lblExito);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el cliente: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEdit"] = null;
            Response.Redirect("wfListaClientes.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["ClienteEdit"] = null;
            hfNuevo.Value = "N";
            txtIdCliente.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtNroDi.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContacto.Text = string.Empty;
            txtCargo.Text = string.Empty;
            cboDepartamento.SelectedValue = "-1";
            cboProvincia.Items.Clear();
            cboDistrito.Items.Clear();
            txtRazonSocial.Focus();
        }

        protected void btnAgregarSede_Click(object sender, EventArgs e)
        {
            Session["IdClienteSede"] = txtIdCliente.Text;
            Session["SedeClienteEdit"] = null;
            Response.Redirect("wfSedeCliente.aspx");
        }

        protected void gvSedes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvSedes.DataKeys[index];
                int? nIdSede = null;

                if (dataKey != null)
                    nIdSede = int.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["SedeClienteEdit"] = nIdSede;
                    //Session["IdClienteSede"] = txtIdCliente.Text;
                    Response.Redirect("wfSedeCliente.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void gvUsuariosCliente_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvUsuariosCliente.DataKeys[index];
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
            }
        }

        protected void gvUsuarioAsignar_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvUsuarioAsignar.DataKeys[index];
                String sIdUsuario = null;

                if (dataKey != null)
                    sIdUsuario = dataKey.Value.ToString();

                if (e.CommandName.Equals("Seleccionar"))
                {
                    if (new UsuarioCliente().Registrar(new UsuarioClienteInfo(sIdUsuario, Int32.Parse(txtIdCliente.Text), 1){UsuarioCreacion = lblUsuario.Text}))
                    {
                        CargarCliente();
                        Util.Util.AlternarMensaje(true, "Se asignó el usuario seleccionado al ticket.", alertaError, alertaExito, lblError, lblExito);
                        //UpdatePanel2.Update();
                        gvUsuarioAsignar.DataSource = null;
                        gvUsuarioAsignar.DataBind();

                        var sb = new StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#modalAsignarUsuario').modal('hide');");
                        sb.Append(@"</script>");
                        ClientScript.RegisterStartupScript(GetType(), "Hide", sb.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al querer grabar el ticket: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }

        protected void btnConsultarUsuario_Click(object sender, EventArgs e)
        {
            var sNombreUsuario = String.IsNullOrEmpty(txtUsuarioNombre.Text.Trim())
                            ? null
                            : txtUsuarioNombre.Text.Trim();
            gvUsuarioAsignar.DataSource = new UsuarioCliente().ListarPendientes();
            gvUsuarioAsignar.DataBind();
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalAsignarUsuario').modal('show');");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(GetType(), "Show", sb.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ModalScript", sb.ToString(), false);
        }
    }
}