using System.Net.Mail;

public class Correo 
{
    public string body;

    public string PopulateBody(string userName, string title, string description)
    {

        string var0 = title;
        string var1 = userName;
        string var2 = description;


        string template = @"<html lang=""en"">
                 <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                <head><title></title></head>
                <body>
                <img src = ""http://mail.vtr.net/~jo.inostroza/Logo.png""/><br/><br/>
                <h2> {0} <h2/><br/>
                 <div style = ""border-top:3px solid red""></ div >
                    <span style =""font-family:Arial;font-size:10pt"">
                     Estimado(a)<b> {1} </b>:<br/><br/>
                          <br/><br> {2}
                <br/><br/>
                Atte. <br/>
                Centro de relaciones exteriores Montreal.
                </span>
                </body>
                </html>";
        return body = string.Format(template, var0,var1,var2);
        
        /*
         return body = @"<html lang=""en"">
                 <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                <head><title></title></head>
                <body>
                <img src = ""http://mail.vtr.net/~jo.inostroza/Logo.png""/><br/><br/>
                 <div style = ""border-top:3px solid red""></ div >
                    <span style =""font-family:Arial;font-size:10pt"">
                     Hello<b> </b>,<br/><br/>
                          A new article has been published on ASPSnippets.<br/><br/>        
                {Description}
                <br/><br/>
                Thanks <br/>
                ASPSnippets
                </span>
                </body>
                </html>";
         */
    }

    public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("extranjeria.montreal@gmail.com");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "extranjeria.montreal@gmail.com";
            NetworkCred.Password = "Nick.6831";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }
    }
    /*public void SendHtmlFormattedEmail(string fromMail, string pass, string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(fromMail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = fromMail;
            NetworkCred.Password = pass;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }
    }*/
}