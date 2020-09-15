using System;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Threading.Tasks;
using System.Data;

namespace Detyra_2
{
    public class NewsRetrievercs : INewsRetrievercs
    {
        //funksioni qe ekzekuton funksionin per kerkimin e lajmeve
        public string Kontrollo(string fjala)
        {
            // This method runs asynchronously.
            if (fjala != "Business" && fjala != "Entertainment" && fjala != "Health" && fjala != "Science" && fjala != "Sports" && fjala != "Technology")
            {
                var t = Task.Run(() => NewsApiFunction(fjala)).Result;
                return t;
            }
            else
            {
                var t = Task.Run(() => NewsApiFunctionKategori(fjala)).Result;
                return t;
            }
        }

        private static string apiKey = System.Configuration.ConfigurationManager.AppSettings["apiKey"];
        //funksioni per kerkimin e lajmeve ne baze te fjales kyce apo kategorise
        public virtual string NewsApiFunction(string fjala)
        {
            // init with your API key
            var newsApiClient = new NewsApiClient(apiKey);
            var rez = "";
            
            DataManager dm = new DataManager();
            DataSet ds = dm.getEntity();
            //nqs eshte fjale kyce e ndare me presje

            string[] tekst = fjala.Split(','); //nda fjalet kyce duke i vendosur ne nje vektor duke i hequr presjen
            string f = string.Join(" , ", tekst);//nda fjalet kyce me presje me hapesire per te bere kerkimin e lajmeve
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = f,
                Language = Languages.EN

            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                //per cdo vlere qe mban objekti dataset
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr;
                    dr = ds.Tables[0].Rows[i]; //ruan rreshtat e te dhenave ne datarow
                    var dt = dr.ItemArray.GetValue(3); //akseson vleren e dates kur eshte bere njoftimi i fundit
                                                       //nqs perdoruesi eshte njoftuar me perpara per fjalet kyce te ndara me presje
                    if (dt.ToString() != "NULL") // 
                    {
                        DateTime.TryParse(dt.ToString(), out DateTime njoftimi);
                        //njoftoje vetem per lajmet e fundit
                        foreach (var article in articlesResponse.Articles)
                        {

                            if (article.PublishedAt > njoftimi)
                            {
                                rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                            }

                        }
                    }


                    //perndryshe njoftoje per te gjitha lajmet per fjalet kyce te ndara me presje
                    else
                    {
                        foreach (var article in articlesResponse.Articles)
                        {
                            rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                        }
                    }
                }

                return rez;
            }
            //nqs eshte fjale kyce e ndare pa presje dhe nuk eshte kategori
            else if (fjala != "Business" && fjala != "Entertainment" && fjala != "Health" && fjala != "Science" && fjala != "Sports" && fjala != "Technology")
            {
                articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = fjala,
                    Language = Languages.EN

                });
                if (articlesResponse.Status == Statuses.Ok)
                {
                    //per cdo vlere qe mban objekti dataset
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = ds.Tables[0].Rows[i]; //ruan rreshtat e te dhenave ne datarow
                        var dt = dr.ItemArray.GetValue(3); //akseson vleren e dates kur eshte bere njoftimi i fundit
                                                           //nqs perdoruesi eshte njoftuar me perpara per fjalet kyce te ndara me presje
                        if (dt.ToString() != "NULL") // 
                        {
                            DateTime.TryParse(dt.ToString(), out DateTime njoftimi);
                            //njoftoje vetem per lajmet e fundit
                            foreach (var article in articlesResponse.Articles)
                            {

                                if (article.PublishedAt > njoftimi)
                                {
                                    rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                                }

                            }
                        }
                        //nqs nuk eshte njoftuar me perpara per ate fjale kyce pa presje
                        else
                        {
                            foreach (var article in articlesResponse.Articles)
                            {
                                rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                            }
                        }

                    }
                }
            }
            return rez;
        }
            //nqs eshte kategori
            public virtual string NewsApiFunctionKategori(string fjala) {
            {
                // init with your API key
                var newsApiClient = new NewsApiClient(apiKey);
                var rez = "";
                DataManager dm = new DataManager();
                DataSet ds = dm.getEntity();
                Categories cat = (Categories)Enum.Parse(typeof(Categories), fjala);
                    var articlesResponse = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
                    {
                        Category = cat,
                        Language = Languages.EN


                    });
                    if (articlesResponse.Status == Statuses.Ok)
                    {
                    //per cdo vlere qe mban objekti dataset
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = ds.Tables[0].Rows[i]; //ruan rreshtat e te dhenave ne datarow
                        var dt = dr.ItemArray.GetValue(3); //akseson vleren e dates kur eshte bere njoftimi i fundit
                                                           //nqs perdoruesi eshte njoftuar me perpara per fjalet kyce te ndara me presje
                        if (dt.ToString() != "NULL") // 
                        {
                            DateTime.TryParse(dt.ToString(), out DateTime njoftimi);
                            //njoftoje vetem per lajmet e fundit
                            foreach (var article in articlesResponse.Articles)
                            {

                                if (article.PublishedAt > njoftimi)
                                {
                                    rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                                }

                            }
                        }
                        //nqs nuk eshte njoftuar me perpara per ate kategori
                        else
                        {
                            foreach (var article in articlesResponse.Articles)
                            {
                                rez += article.Title + " " + article.Url + " " + article.PublishedAt + "\n";
                            }
                        }

                    }
                    }
                return rez;
            }
                
            }

        }
    }

