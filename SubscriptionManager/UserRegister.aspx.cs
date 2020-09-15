using System;
using System.Data;

namespace Detyra_2
{
    public partial class UserRegister : System.Web.UI.Page
    {
        User us = new User();
        Subscription sc = new Subscription();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //shfaqim vendet ku perdoruesi mund te zgjedh kategorine ose te shkruaj fjale kyce ne baze te zgjedhjes qe ka bere
        protected void DdlSubscribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlSubscribe.SelectedItem.Value == "0")
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
        }

        //funksioni per shtimin e perdoruesave ne databaze
        protected void Regjistrohu_Click(object sender, EventArgs e)
        {
            us.name = Emri.Text;
            us.email = Email.Text;
            string abonimi = DdlSubscribe.SelectedItem.Text;
            

            if (abonimi == "Fjale kyce")
            {
                sc.keywords = FK.Text.ToString();

            }
            else
            {
                sc.categories = DdlKategorite.SelectedItem.Text;

            }
            if (Emri.Text != "" && Email.Text != "")
            {


                IDataManager dm = null;
                Rezultati.Text = addSuscriber(us, sc, dm);




            }
            else
            {
                Rezultati.Text = "<span style='color:red'>Plotesoni kutizat!</span>";
            }

        }

        public string addSuscriber(User us, Subscription sc,IDataManager dm)
        {
            
            if (dm == null)
            {
                dm = new DataManager();//inicializo klasen data manager ku ndodhen funksionet per leximin dhe shtimin e te dhenave ne db
            }

            //ruajme emrin dhe emailin e perdoruesit te atributet name dhe email te klases user
            if (dm.kontrolloValid(us.email) == false)
            {
                return "Emaili jo valid";

            }
            else
            {

                    if (dm.kontrolloEmail(us.email))
                    {
                        return "Emaile te njejta!";
                    }
                }
                dm.storeEntity(us, sc);
                return "Shtimi u krye me sukses";
            }
        



        //funksioni per dergimin e notification kur shtohen lajme te reja
        protected void Dergo_Click(object sender, EventArgs e)
        {
            INewsRetrievercs news = null;
            IDataManager dm = null;
            bool dergo = sendNotification(news,dm);
            if (dergo == true)
            {
                Rezultati.Text = "<span style='color:green'>Emailet u derguan me sukses!</span>";
            }
            else
            {
                Rezultati.Text = "<span style='color:red'>Nuk ka lajme te reja per tu derguar!</span>";
            }
        }

        public bool sendNotification(INewsRetrievercs news,IDataManager dm)
        {
            Emailer em = new Emailer();
            if (news == null)
            {
                news = new NewsRetrievercs(); //inicializojme klasen ku ndodhet api per newsretriever
            }
            DataSet ds;
            DataRow dr;
            if (dm == null)
            {
                dm = new DataManager();
            }
            ds = dm.getEntity(); //ruajme te dhenat e ketyre perdoruesve ne dataset

            var adresa = "";
            var fjalekyce = "";
            var mesazhi = "";
            bool email = false;
            //bredhim te dhenat e ketyre perdoruesve dhe per secilin prej tyre dergojme emailin duke percaktuar adresen dhe fjalenkyce qe ai ka shkruar ose kategorine qe ka perzgjedh
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i]; //ruan rreshtat e te dhenave ne datarow
                adresa = dr.ItemArray.GetValue(1).ToString(); //akseson vleren e emailit per cdo rresht dhe e ruan te variabli adresa
                fjalekyce = dr.ItemArray.GetValue(2).ToString(); //akseson vleren e fjaleskyce ose kategorise per cdo rresht dhe e ruan ne variabel
                mesazhi = news.Kontrollo(fjalekyce);
                if (mesazhi != null)
                {
                    email = em.sendEmail(adresa, mesazhi);
                    
                }
                
            }
            
            return email; 
        }
    }
}