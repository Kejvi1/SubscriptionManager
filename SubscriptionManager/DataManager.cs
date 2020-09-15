using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;

namespace Detyra_2
{
    public class DataManager : IDataManager
    {
        string connectionstring = "connectionstring";
        //funksioni per marrjen e te dhenave te perdoruesve
        public virtual DataSet getEntity()
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = "Select * From Subscription";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }


        }

        //funksioni per shtimin e te dhenave te perdoruesve
        public virtual void storeEntity(User us, Subscription sc)
        {
            int n = 0;
            
            //kryej shtimin nqs emaili nuk ekziston
            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                string query = "Insert Into Subscription(Emer,Email,TipiAbonimi) Values(@emer,@email,@tipiabonimi)";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@emer", us.name);
                cmd.Parameters.AddWithValue("@email", us.email);
                if (sc.categories == null)
                {
                    cmd.Parameters.AddWithValue("@tipiabonimi", sc.keywords);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tipiabonimi", sc.categories);
                }
                n = cmd.ExecuteNonQuery();

            }


           

        }



        public virtual string[] getEmailFromDataSet()
        {
            string[] emailet = new string[30];
            DataSet ds = getEntity();
            DataRow dr;
            for(int i = 0;i<ds.Tables[0].Rows.Count;i++)
            {
                dr = ds.Tables[0].Rows[i];
                string email = dr.ItemArray.GetValue(1).ToString();
                emailet[i] = email;
            }
            return emailet;
        }


        public virtual bool kontrolloValid(string adresa)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(adresa);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public virtual bool kontrolloEmail(string email)
        {
            DataManager dm = new DataManager();
            string[] emailet = dm.getEmailFromDataSet();
            foreach (var em in emailet)
            {
                if (em == email)
                {
                    return true;
                    
                }

            }
            return false;
        }
    }
}