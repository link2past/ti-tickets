using System;
using TIEntidades;
using TILogicaNegocio;
using System.Web.Security;

namespace webTiTickets
{
// ReSharper disable InconsistentNaming
    public partial class wfSedeCliente : System.Web.UI.Page
// ReSharper restore InconsistentNaming
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsuario.Text = Context.User.Identity.Name;
                Util.Util.CargaEstados(cboEstado, true);
                Util.Util.CargaUnidadNegocio(cboUnidadNegocio, true);
                Util.Util.CargarDepartamentos(cboDepartamento, true);

                if (Session["SedeClienteEdit"] != null)
                {
                    hfNuevo.Value = "F";
                    txtIdSede.Text = Session["SedeClienteEdit"].ToString();
                    CargarSedeCliente();
                }
                else
                {
                    hfNuevo.Value = "N";
                    txtIdCliente.Text = Session["IdClienteSede"].ToString();
                    ConsultarCliente();
                    ActivarDesactivarCampos(true);
                }
            }
        }

        private void ConsultarCliente()
        {
            var oEntCliente = new Cliente().Consultar(new ClienteInfo {IdCliente = Int32.Parse(txtIdCliente.Text)});
            if (oEntCliente != null)
            {
                txtCliente.Text = oEntCliente.RazonSocial;
            }
        }

        private void ActivarDesactivarCampos(Boolean bEstado)
        {
            txtCliente.Enabled = bEstado;
            txtNombre.Enabled = bEstado;
            txtDireccion.Enabled = bEstado;
            cboDepartamento.Enabled = bEstado;
            cboProvincia.Enabled = bEstado;
            cboDistrito.Enabled = bEstado;
            cboEstado.Enabled = bEstado;
            txtTelefono.Enabled = bEstado;
            cboUnidadNegocio.Enabled = bEstado;
            txtContacto.Enabled = bEstado;
            txtCargo.Enabled = bEstado;
        }

        private void CargarSedeCliente()
        {
            var oEntSedeCliente =
                new SedeCliente().Consultar(new SedeClienteInfo(Int32.Parse(txtIdSede.Text), null, null));

            if (oEntSedeCliente != null)
            {
                txtNombre.Text = oEntSedeCliente.Nombre;
                txtIdCliente.Text = oEntSedeCliente.IdCliente.ToString();
                txtCliente.Text = oEntSedeCliente.Cliente.RazonSocial;
                txtDireccion.Text = oEntSedeCliente.Direccion;
                cboDepartamento.SelectedValue = oEntSedeCliente.IdDepartamento;

                Util.Util.CargarProvincias(cboDepartamento, cboProvincia, cboDistrito, true);
                cboProvincia.SelectedValue = oEntSedeCliente.IdProvincia;

                Util.Util.CargarDistritos(cboDepartamento, cboProvincia, cboDistrito, true);
                cboDistrito.SelectedValue = oEntSedeCliente.IdDistrito;

                txtTelefono.Text = oEntSedeCliente.Telefono;
                txtContacto.Text = oEntSedeCliente.NombreContacto;
                txtCargo.Text = oEntSedeCliente.CargoContacto;

                cboEstado.SelectedValue = oEntSedeCliente.IdEstado.ToString();
                cboUnidadNegocio.SelectedValue = oEntSedeCliente.IdUnidadNegocio.ToString();
                txtCentroCosto.Text = oEntSedeCliente.CentroCosto;

                var oListaUsuariosSede =
                    new UsuarioSede().Listar(new UsuarioSedeInfo {IdSede = oEntSedeCliente.IdSedeCliente});
                gvUsuarioSede.DataSource = oListaUsuariosSede;
                gvUsuarioSede.DataBind();
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.Util.CargarProvincias(cboDepartamento, cboProvincia, cboDistrito, true);
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util.Util.CargarDistritos(cboDepartamento, cboProvincia, cboDistrito, true);
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIdCliente.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar el cliente al que pertencece la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar el nombre de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (String.IsNullOrEmpty(txtDireccion.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe indicar la dirección de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboDepartamento.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar el departamento de la ubicación de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboProvincia.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar la provincia de la ubicación de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboDistrito.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar el distrito de la ubicación de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (String.IsNullOrEmpty(txtTelefono.Text))
            {
                Util.Util.AlternarMensaje(false, "Debe ingresar el teléfono del cliente.", alertaError, alertaExito, lblError, lblExito);
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

            if (cboUnidadNegocio.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar la unidad de negocio de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            if (cboEstado.SelectedValue.Equals("-1"))
            {
                Util.Util.AlternarMensaje(false, "Debe seleccionar el estado de la sede.", alertaError, alertaExito, lblError, lblExito);
                return;
            }

            var oEntSede = new SedeClienteInfo();
            oEntSede.IdSedeCliente = String.IsNullOrEmpty(txtIdSede.Text) ? (int?)null : Int32.Parse(txtIdSede.Text.Trim());
            oEntSede.IdCliente = Int32.Parse(txtIdCliente.Text);
            oEntSede.Nombre = txtNombre.Text;
            oEntSede.Direccion = txtDireccion.Text;
            oEntSede.IdDepartamento = cboDepartamento.SelectedValue;
            oEntSede.IdProvincia = cboProvincia.SelectedValue;
            oEntSede.IdDistrito = cboDistrito.SelectedValue;
            oEntSede.Telefono = txtTelefono.Text;
            oEntSede.NombreContacto = txtContacto.Text;
            oEntSede.CargoContacto = txtCargo.Text;
            oEntSede.IdUnidadNegocio = Int32.Parse(cboUnidadNegocio.SelectedValue);
            oEntSede.IdEstado = Int32.Parse(cboEstado.SelectedValue);
            oEntSede.CentroCosto = String.IsNullOrEmpty(txtCentroCosto.Text) ? null : txtCentroCosto.Text;

            if (hfNuevo.Value.Equals("N"))
            {
                int? nIdSedeCliente = null;
                oEntSede.UsuarioCreacion = lblUsuario.Text;
                if (new SedeCliente().Registrar(oEntSede, ref nIdSedeCliente))
                {
                    hfNuevo.Value = "F";
                    txtIdSede.Text = nIdSedeCliente.ToString();
                    ActivarDesactivarCampos(false);
                    Util.Util.AlternarMensaje(true, "Se registró la sede del cliente con éxito.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
                else
                {
                    Util.Util.AlternarMensaje(true, "No se pudo registrar la sede del cliente.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
            }
            else
            {
                oEntSede.UsuarioModificacion = lblUsuario.Text;
                if (new SedeCliente().Actualizar(oEntSede))
                {
                    hfNuevo.Value = "F";
                    ActivarDesactivarCampos(false);
                    Util.Util.AlternarMensaje(true, "Se actualizó la sede del cliente con éxito.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
                else
                {
                    Util.Util.AlternarMensaje(true, "No se pudo actualizar la sede del cliente.", alertaError,
                                              alertaExito,
                                              lblError, lblExito);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEdit"] = txtIdCliente.Text;
            Response.Redirect("wfCliente.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["SedeClienteEdit"] = null;
            hfNuevo.Value = "N";
            txtIdCliente.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtContacto.Text = string.Empty;
            txtCargo.Text = string.Empty;
            cboDepartamento.SelectedValue = "-1";
            cboProvincia.Items.Clear();
            cboDistrito.Items.Clear();
            cboEstado.SelectedValue = "-1";
            cboUnidadNegocio.SelectedValue = "-1";
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Session["UsuarioSedeEdit"] = null;
            Session["SedeClienteEdit"] = txtIdSede.Text;
            Response.Redirect("wfUsuarioSede.aspx");
        }

        protected void gvUsuarioSede_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dataKey = gvUsuarioSede.DataKeys[index];
                int? sUsuario = null;

                if (dataKey != null)
                    sUsuario = Int32.Parse(dataKey.Value.ToString());

                if (e.CommandName.Equals("Seleccionar"))
                {
                    Session["UsuarioSedeEdit"] = sUsuario;
                    Session["SedeClienteEdit"] = null;
                    Response.Redirect("wfUsuarioSede.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.Util.AlternarMensaje(false, "Ocurrió el siguiente error al listar la información: " + ex.Message, alertaError, alertaExito, lblError, lblExito);
            }
        }
    }
}