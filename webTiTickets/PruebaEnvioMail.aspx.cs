using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace webTiTickets
{
    public partial class PruebaEnvioMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage("requerimientos@ticonsultingsac.com", txtDestino.Text);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "localhost";
                mail.Subject = txtAsunto.Text;
                mail.Body = txtCuerpo.Text;
                mail.IsBodyHtml = true;
                client.Send(mail);
                txtMensaje.Text = "Mensaje Enviado";
            }
            catch (Exception ex)
            {
                txtMensaje.Text = ex.Message;
            }

        }
    }
}