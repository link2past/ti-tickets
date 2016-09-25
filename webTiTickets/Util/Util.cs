using System;
using System.Globalization;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TIEntidades;
using TILogicaNegocio;

namespace webTiTickets.Util
{
    public class Util
    {
        public enum EstadoTicketsEnum
        {
            Registrado = 1,
            Asignado = 2,
            Recibido = 3,
            Atendido = 4,
            Cerrado = 5,
            EsperaRepuesto = 6,
            Anulado = 7
        };

        public enum TipoUsuarioEnum
        {
            Cliente = 1,
            Moderador = 2,
            Técnico = 3,
            Administrador = 4
        };

        public static void CargarDepartamentos(DropDownList cboDepartamento, Boolean bInsertarSeleccion)
        {
            cboDepartamento.DataSource = new Ubigeo().Listar(new UbigeoInfo(null, "00", "00", null));
            cboDepartamento.DataValueField = "IdDepartamento";
            cboDepartamento.DataTextField = "Descripcion";
            cboDepartamento.DataBind();
            if (bInsertarSeleccion)
                cboDepartamento.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboDepartamento.SelectedIndex = -1;
        }

        public static void CargarProvincias(DropDownList cboDepartamento, DropDownList cboProvincia,
                                            DropDownList cboDistrito, Boolean bInsertarSeleccion)
        {
            if (cboDepartamento.Items.Count > 1)
            {
                String sDepartamento = cboDepartamento.SelectedValue;
                if (!sDepartamento.Equals("-1"))
                {
                    cboProvincia.DataSource = new Ubigeo().Listar(new UbigeoInfo(sDepartamento, null, "00", null));
                    cboProvincia.DataValueField = "IdProvincia";
                    cboProvincia.DataTextField = "Descripcion";
                    cboProvincia.DataBind();
                    cboProvincia.Items.RemoveAt(0);
                    if (bInsertarSeleccion)
                        cboProvincia.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
                    cboProvincia.SelectedIndex = -1;
                    if (cboDistrito != null)
                        cboDistrito.Items.Clear();
                }
                else
                {
                    cboProvincia.Items.Clear();
                    if (cboDistrito != null)
                        cboDistrito.Items.Clear();
                }
            }
        }

        public static void CargarDistritos(DropDownList cboDepartamento, DropDownList cboProvincia,
                                           DropDownList cboDistrito, Boolean bInsertarSeleccion)
        {
            if (cboProvincia.Items.Count > 1)
            {
                String sDepartamento = cboDepartamento.SelectedValue;
                String sProvincia = cboProvincia.SelectedValue;

                if (!sProvincia.Equals("-1"))
                {
                    cboDistrito.DataSource =
                        new Ubigeo().Listar(new UbigeoInfo(sDepartamento, sProvincia, null, null));
                    cboDistrito.DataValueField = "IdDistrito";
                    cboDistrito.DataTextField = "Descripcion";
                    cboDistrito.DataBind();
                    cboDistrito.Items.RemoveAt(0);
                    if (bInsertarSeleccion)
                        cboDistrito.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
                    cboDistrito.SelectedIndex = -1;
                }
                else
                {
                    cboDistrito.Items.Clear();
                }
            }
        }

        public static void CargaUsuario(DropDownList cboUsuario, int? nTipoUsuario, Boolean bInsertarSeleccion)
        {
            cboUsuario.DataSource =
                new Usuario().Listar(new UsuarioInfo {IdTipoUsuario = nTipoUsuario});
            cboUsuario.DataValueField = "Usuario";
            cboUsuario.DataTextField = "Nombre";
            cboUsuario.DataBind();
            if (bInsertarSeleccion)
                cboUsuario.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboUsuario.SelectedIndex = -1;
        }

        public static void CargaCliente(DropDownList cboCliente, Boolean bInsertarSeleccion)
        {
            cboCliente.DataSource =
                new Cliente().Listar(new ClienteInfo());
            cboCliente.DataValueField = "IdCliente";
            cboCliente.DataTextField = "RazonSocial";
            cboCliente.DataBind();
            if (bInsertarSeleccion)
                cboCliente.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboCliente.SelectedIndex = -1;
        }

        public static void CargaAreaUsuarioSede(DropDownList cboAreaUsuario, Boolean bInsertarSeleccion)
        {
            cboAreaUsuario.DataSource =
                new AreaUsuarioSede().Listar(new AreaUsuarioSedeInfo(null, null, 1, null, null, null));
            cboAreaUsuario.DataValueField = "IdAreaUsuarioSede";
            cboAreaUsuario.DataTextField = "Descripcion";
            cboAreaUsuario.DataBind();
            if (bInsertarSeleccion)
                cboAreaUsuario.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboAreaUsuario.SelectedIndex = -1;
        }

        public static void CargaEstadosTicket(DropDownList cboEstadoTicket, Boolean bInsertarSeleccion)
        {
            cboEstadoTicket.DataSource = new EstadoTicket().Listar(new EstadoTicketInfo(null, null, 1, null, null, null));
            cboEstadoTicket.DataValueField = "IdEstadoTicket";
            cboEstadoTicket.DataTextField = "Descripcion";
            cboEstadoTicket.DataBind();
            if (bInsertarSeleccion)
                cboEstadoTicket.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboEstadoTicket.SelectedIndex = -1;
        }

        public static void CargarMoneda(DropDownList cboMoneda, Boolean bInsertarSeleccion)
        {
            cboMoneda.DataSource = new Moneda().Listar(new MonedaInfo());
            cboMoneda.DataValueField = "IdMoneda";
            cboMoneda.DataTextField = "Descripcion";
            cboMoneda.DataBind();
            if (bInsertarSeleccion)
                cboMoneda.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboMoneda.SelectedIndex = -1;
        }
        
        public static void CargaCategoriaProblema(DropDownList cboCategoriaProblema, Boolean bInsertarSeleccion)
        {
            cboCategoriaProblema.DataSource = new CategoriaProblema().Listar(new CategoriaProblemaInfo(null, null, 1, null, null, null));
            cboCategoriaProblema.DataValueField = "IdCategoriaProblema";
            cboCategoriaProblema.DataTextField = "Descripcion";
            cboCategoriaProblema.DataBind();
            if (bInsertarSeleccion)
                cboCategoriaProblema.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboCategoriaProblema.SelectedIndex = -1;
        }

        public static void CargaNivelUrgencia(DropDownList cboNivelUrgencia, Boolean bInsertarSeleccion)
        {
            cboNivelUrgencia.DataSource = new NivelUrgencia().Listar(new NivelUrgenciaInfo(null, null, 1, null, null, null));
            cboNivelUrgencia.DataValueField = "IdNivelUrgencia";
            cboNivelUrgencia.DataTextField = "Descripcion";
            cboNivelUrgencia.DataBind();
            if (bInsertarSeleccion)
                cboNivelUrgencia.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboNivelUrgencia.SelectedIndex = -1;
        }

        public static void CargaSedeCliente(DropDownList cboSedeCliente, int? nIdCliente, int? nIdUnidadNegocio, Boolean bInsertarSeleccion)
        {
            cboSedeCliente.DataSource = new SedeCliente().Listar(new SedeClienteInfo(null, nIdCliente, null){IdUnidadNegocio = nIdUnidadNegocio});
            cboSedeCliente.DataValueField = "IdSedeCliente";
            cboSedeCliente.DataTextField = "Nombre";
            cboSedeCliente.DataBind();
            if (bInsertarSeleccion)
                cboSedeCliente.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboSedeCliente.SelectedIndex = -1;
        }

        public static void CargaUsuarioSede(DropDownList cboUsuarioSede, int? nIdSede, Boolean bInsertarSeleccion)
        {
            cboUsuarioSede.DataSource = new UsuarioSede().Listar(new UsuarioSedeInfo() {IdSede = nIdSede});
            cboUsuarioSede.DataValueField = "IdUsuarioSede";
            cboUsuarioSede.DataTextField = "Nombre";
            cboUsuarioSede.DataBind();
            if (bInsertarSeleccion)
                cboUsuarioSede.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboUsuarioSede.SelectedIndex = -1;
        }

        public static void CargaEstados(DropDownList cboEstado, Boolean bInsertarSeleccion)
        {
            cboEstado.DataSource = new Estado().Listar(new EstadoInfo());
            cboEstado.DataValueField = "IdEstado";
            cboEstado.DataTextField = "Descripcion";
            cboEstado.DataBind();
            if (bInsertarSeleccion)
                cboEstado.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboEstado.SelectedIndex = -1;
        }

        public static void CargaTipoUsuario(DropDownList cboTipoUsuario, Boolean bInsertarSeleccion)
        {
            cboTipoUsuario.DataSource = new TipoUsuario().Listar(new TipoUsuarioInfo());
            cboTipoUsuario.DataValueField = "IdTipoUsuario";
            cboTipoUsuario.DataTextField = "Descripcion";
            cboTipoUsuario.DataBind();
            if (bInsertarSeleccion)
                cboTipoUsuario.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboTipoUsuario.SelectedIndex = -1;
        }

        public static void CargaUnidadNegocio(DropDownList cboUnidadNegocio, Boolean bInsertarSeleccion)
        {
            cboUnidadNegocio.DataSource = new UnidadNegocio().Listar(new UnidadNegocioInfo());
            cboUnidadNegocio.DataValueField = "IdUnidadNegocio";
            cboUnidadNegocio.DataTextField = "Descripcion";
            cboUnidadNegocio.DataBind();
            if (bInsertarSeleccion)
                cboUnidadNegocio.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboUnidadNegocio.SelectedIndex = -1;
        }

        public static void CargaUnidadNegocioXCliente(DropDownList cboUnidadNegocio, Boolean bInsertarSeleccion, int? nIdCliente)
        {
            cboUnidadNegocio.DataSource = new UnidadNegocio().ListarPorCliente(new UnidadNegocioInfo(), nIdCliente);
            cboUnidadNegocio.DataValueField = "IdUnidadNegocio";
            cboUnidadNegocio.DataTextField = "Descripcion";
            cboUnidadNegocio.DataBind();
            if (bInsertarSeleccion)
                cboUnidadNegocio.Items.Insert(0, new ListItem("--SELECCIONE--", "-1"));
            cboUnidadNegocio.SelectedIndex = -1;
        }

        public static void AlternarMensaje(Boolean bEstado, String sMensaje, HtmlGenericControl divError, HtmlGenericControl divExito, Label lblError, Label lblExito)
        {
            if (bEstado)
            {
                divError.Visible = false;
                lblError.Text = String.Empty;
                if (divExito != null)
                {
                    divExito.Visible = true;
                    lblExito.Text = sMensaje;
                }
            }
            else
            {
                divError.Visible = true;
                lblError.Text = sMensaje;
                if (divExito != null)
                {
                    divExito.Visible = false;
                    lblExito.Text = string.Empty;
                }
            }
        }

        public static Boolean EsFechaValida(string sFecha)
        {
            //string strRegex = @"((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1- 9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))";
            //MMDDYYYY
            //([1-9]|1[012])[- /.]([1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d
            //DDMMYYYY
            const string strRegex = @"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d";
            return new System.Text.RegularExpressions.Regex(strRegex).IsMatch(sFecha);
        }

        public static Boolean EsNumerico(string sValor)
        {
            const string strRegex = @"^[+-]?\d+\.?\d*$";
            return System.Text.RegularExpressions.Regex.IsMatch(sValor, strRegex);
        }

        public static bool EsEnteroPositivo(string sValor)
        {
            const string strRegex = "[^0-9]";
            return !System.Text.RegularExpressions.Regex.IsMatch(sValor, strRegex);
        }

        public static Boolean EsHoraValida(string sHora)
        {
            //string strRegex = @"([01]?[0-9]|2[0-3]):[0-5][0-9]";
            const string strRegex = @"^([0-1]?\d|2[0-3]):([0-5]\d)$";
            return System.Text.RegularExpressions.Regex.IsMatch(sHora, strRegex);
        }

        public static Boolean EsCorreoElectronicoValido(String sCorreoElectronico)
        {
            const string strRegex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return System.Text.RegularExpressions.Regex.IsMatch(sCorreoElectronico, strRegex);
        }

        public static DateTime? HoraMinima(DateTime? dFecha)
        {
            if (dFecha != null)
                return new DateTime(dFecha.Value.Year, dFecha.Value.Month, dFecha.Value.Day, 0, 0, 0);
            return null;
        }

        public static DateTime? HoraMaxima(DateTime? dFecha)
        {
            if (dFecha != null)
                return new DateTime(dFecha.Value.Year, dFecha.Value.Month, dFecha.Value.Day, 23, 59, 59);
            return null;
        }

        public static void CargarHoras(DropDownList cboHoras, DropDownList cboMinutos)
        {
            for (var i = 0; i <= 23; i++)
            {
                var sHora = i.ToString(CultureInfo.InvariantCulture);
                cboHoras.Items.Insert(i, new ListItem(sHora.PadLeft(2, '0'), sHora.PadLeft(2, '0')));
            }

            for (var j = 0; j <= 59; j++)
            {
                var sMinutos = j.ToString(CultureInfo.InvariantCulture);
                cboMinutos.Items.Insert(j, new ListItem(sMinutos.PadLeft(2, '0'), sMinutos.PadLeft(2, '0')));
            }
        }

        public static void EnviarMail(String sDestinatario, String sAsunto, String sCuerpo, String sMailModerador)
        {
            var mail = new MailMessage("requerimientos@ticonsultingsac.com", sDestinatario);
            var client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "localhost";
            if (!String.IsNullOrEmpty(sMailModerador))
            {
                mail.CC.Add(new MailAddress(sMailModerador));    
            }
            mail.Subject = sAsunto;
            mail.Body = sCuerpo;
            mail.IsBodyHtml = true;
            client.Send(mail);
        }
    }
}