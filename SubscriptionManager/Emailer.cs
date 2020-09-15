using System;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Detyra_2
{
    public class Emailer : IEmailer
    {
        public  string derguesi = ConfigurationManager.AppSettings["derguesi"];
        public  string fjalekalimi = ConfigurationManager.AppSettings["fjalekalimi"];
        public bool sendEmail(string adresa, string mesazhi)
        {

                //dergo email ne rastet kur mesazhi ka permbajtje dhe modifikimi i kolones njoftimi i fundit do te behet ne rastet kur mesazhi ka permbajtje 
                if (mesazhi != "")
                {
                    var fromAddress = "email";// Gmail Address from where you send the mail
                    var toAddress = adresa;
                    const string fromPassword = "password";//Password of your gmail address
                    string subject = "News";
                    string body = "News Web Application \n";
                    body += "Mesazhi: " + mesazhi + "\n";

                    var smtp = new System.Net.Mail.SmtpClient();
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        smtp.Timeout = 20000;
                    }
                    smtp.Send(fromAddress, toAddress, subject, body);
                    var njoftimi = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss");
                    shtonjoftimin(Convert.ToDateTime(njoftimi), adresa);
                    return true;
                }

                else
                {
                    return false;
                }

            
        }

        //funksioni per te modifikuar kolonen kur eshte bere njoftimi i fundit
        public virtual bool shtonjoftimin(DateTime njoftimi, string adresa)
        {
            string connectionstring = "connectionstring";
            DataManager dm = new DataManager();
            User us = new User();

            
                
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {

                        string query = "Update Subscription Set NjoftimFundit=@njoftimi Where Email=@adresa";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@njoftimi", njoftimi);
                        cmd.Parameters.AddWithValue("@adresa", adresa);
                        cmd.ExecuteNonQuery();

                    }
                    return true;

                
        }

        
    }
}