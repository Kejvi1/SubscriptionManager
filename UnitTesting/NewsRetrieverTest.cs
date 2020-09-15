using Detyra_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Data;

namespace UnitTesting
{
    [TestClass]
    public class NewsRetrieverTest
    {
        [TestMethod]
        public void NewsApiFunctionTestFjaleKyceSakte()
        {
            //Arrange
            Mock<IDataManager> moqdm = new Mock<IDataManager>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("Emer", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("TipiAbonimi", typeof(string)));
            dt.Columns.Add(new DataColumn("NjoftimFundit", typeof(DateTime)));
            DataRow dr = dt.NewRow();
            dr["Emer"] = "user1";
            dr["Email"] = "email1";
            dr["TipiAbonimi"] = "fjale";
            dr["NjoftimFundit"] = "2019-10-17";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            var fjala = "Samsung , Apple"; //beje vetem samsung per te bere fail
            var newsApiClient = new NewsApiClient("key");//vendosi nje vlere tjeter per fail

                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = fjala,
                    Language = Languages.EN

                });

                moqdm.Setup(x => x.getEntity()).Returns(ds);
                var rez = moqdm.Object.getEntity();
                DateTime.TryParse(ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(), out DateTime njoftimi);
                foreach (var article in articlesResponse.Articles)
                {
                    //Assert
                    Assert.AreEqual(fjala.ToLower().Contains(","), true);
                    Assert.AreEqual(ds.Tables[0].Rows.Count, rez.Tables[0].Rows.Count);
                    Assert.IsTrue(article.PublishedAt > njoftimi);
                    Assert.AreEqual(Statuses.Ok, articlesResponse.Status);
                    Assert.IsTrue(articlesResponse.TotalResults > 0);
                    Assert.IsTrue(articlesResponse.Articles.Count > 0);
                    Assert.IsNull(articlesResponse.Error);
                    moqdm.Verify(x => x.getEntity(), Times.Once());
                }
            
            }

        [TestMethod]
        public void NewsApiFunctionTestKategoriSakte()
        {
            //Arrange
            Mock<DataManager> moqdm = new Mock<DataManager>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("Emer", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("TipiAbonimi", typeof(string)));
            dt.Columns.Add(new DataColumn("NjoftimFundit", typeof(DateTime)));
            DataRow dr = dt.NewRow();
            dr["Emer"] = "user1";
            dr["Email"] = "email1";
            dr["TipiAbonimi"] = "fjale";
            dr["NjoftimFundit"] = "2019-10-17";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            var fjala = "Business";//vendos nje fjale e aq per fail
            var newsApiClient = new NewsApiClient("key");//vendos nje vlere tjeter per celesin per fail

            //Act
            Categories cat = (Categories)Enum.Parse(typeof(Categories), fjala);
            var articlesResponse = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest
            {
                Category = cat,
                Language = Languages.EN


            });

            moqdm.Setup(x => x.getEntity()).Returns(ds);
            var rez = moqdm.Object.getEntity();
            DateTime.TryParse(ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(), out DateTime njoftimi);
            foreach (var article in articlesResponse.Articles)
            {
                //Assert
                Assert.AreEqual(ds.Tables[0].Rows.Count, rez.Tables[0].Rows.Count);
                Assert.IsTrue(article.PublishedAt > njoftimi);
                Assert.AreEqual(Statuses.Ok, articlesResponse.Status);
                Assert.IsTrue(articlesResponse.TotalResults > 0);
                Assert.IsTrue(articlesResponse.Articles.Count > 0);
                Assert.IsNull(articlesResponse.Error);
                moqdm.Verify(x => x.getEntity(), Times.Once());
            }
        }
        
    }
    }
