using MailKit.Net.Smtp;
using MimeKit;
using AppNFe.Core.DominioProblema.Email;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppNFe.Core.Utilitarios
{
    public class UtilitarioEmail
    {
        private MimeMessage MontaMensagemEmail(Email email)
        {
            MimeMessage mensagem = new MimeMessage();
            if (email != null)
            {
                mensagem.From.Add(new MailboxAddress(email.Identificacao, string.IsNullOrEmpty(email.Remetente)? email.ConfiguracaoSMTP.Usuario : email.Remetente ));
                mensagem.Subject = email.Assunto;
                mensagem.To.AddRange(MontaDestinatarios(email.Destinatarios));
                mensagem.Cc.AddRange(MontaDestinatarios(email.DestinatariosCopia));
                mensagem.Bcc.AddRange(MontaDestinatarios(email.DestinatariosOcultos));

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = email.Mensagem;
                mensagem.Body = bodyBuilder.ToMessageBody();
            }
            return mensagem;
        }
        private InternetAddressList MontaDestinatarios(List<string> emails)
        {
            InternetAddressList listaEmails = new InternetAddressList();

            if (emails != null && emails.Count > 0)
            {
                foreach (var itemEmail in emails)
                {
                    if (itemEmail.Contains("@"))
                        listaEmails.Add(new MailboxAddress("", itemEmail));
                }
            }
            return listaEmails;
        }
        public RetornoEnvioEmail EnviarEmail(Email email)
        {
            RetornoEnvioEmail retornoEnvioEmail = new RetornoEnvioEmail(false);
            try
            {
                MimeMessage mensagem = MontaMensagemEmail(email);        
                using (var clienteSMTP = new SmtpClient())
                {
                    clienteSMTP.Connect(email.ConfiguracaoSMTP.Servidor, int.Parse(email.ConfiguracaoSMTP.Porta), email.ConfiguracaoSMTP.SSL);
                    clienteSMTP.Authenticate(email.ConfiguracaoSMTP.Usuario, email.ConfiguracaoSMTP.Senha);
                    clienteSMTP.Send(mensagem);
                    clienteSMTP.Disconnect(true);
                }
                retornoEnvioEmail.Enviado = true;
                retornoEnvioEmail.Mensagem = "E-mail enviado com sucesso!";
            }
            catch (Exception e)
            {
                retornoEnvioEmail = new RetornoEnvioEmail(false,e.Message);
            }
            return retornoEnvioEmail;
        }

        public async Task<RetornoEnvioEmail> EnviarEmailAsync(Email email)
        {
            RetornoEnvioEmail retornoEnvioEmail = new RetornoEnvioEmail(false);
            try
            {
                MimeMessage mensagem = MontaMensagemEmail(email);
                using (var clienteSMTP = new SmtpClient())
                {
                    await clienteSMTP.ConnectAsync(email.ConfiguracaoSMTP.Servidor, int.Parse(email.ConfiguracaoSMTP.Porta), email.ConfiguracaoSMTP.SSL);
                    await clienteSMTP.AuthenticateAsync(email.ConfiguracaoSMTP.Usuario, email.ConfiguracaoSMTP.Senha);
                    await clienteSMTP.SendAsync(mensagem);
                    await clienteSMTP.DisconnectAsync(true);
                }
                retornoEnvioEmail.Enviado = true;
                retornoEnvioEmail.Mensagem = "E-mail enviado com sucesso!";
            }
            catch (Exception e)
            {
                retornoEnvioEmail = new RetornoEnvioEmail(false, e.Message);
            }
            return retornoEnvioEmail;
        }        
    }
}
