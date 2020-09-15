using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Detyra_2;
using Moq;

namespace UnitTesting
{
    [TestClass]
    public class SubscriptionManagerTest
    {
        [TestMethod]
        public void addSubscriberTest()
        {
            User us = new User();
            Subscription sc = new Subscription();
            us.name = "Test";
            us.email = "email1";
            sc.keywords = "fjale,fjale";
            var mock = new Mock<IDataManager>();
            mock.Setup(x => x.kontrolloValid(It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.kontrolloEmail(It.IsAny<string>())).Returns(false);
            UserRegister userRegister = new UserRegister();
             var rez = userRegister.addSuscriber(us,sc, mock.Object);

            Assert.AreEqual("Shtimi u krye me sukses", rez);
        }

        [TestMethod]
        public void sendNotificationTest()
        {
           // string s = null; //per te bere fail mocknews ne rastet kur nuk ka mesazh
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("Emer", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("TipiAbonimi", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["Emer"] = "user";
            dr["Email"] = "email"; //to fail remove @...
            dr["TipiAbonimi"] = "0";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            var mockdm = new Mock<IDataManager>();
            var mockem = new Mock<IEmailer>();
            var mocknews = new Mock<INewsRetrievercs>();
            mockdm.Setup(x => x.getEntity()).Returns(ds);
            mocknews.Setup(x => x.Kontrollo(It.IsAny<string>())).Returns("test");
            //mocknews.Setup(x => x.Kontrollo(It.IsAny<string>())).Returns(s); // to fail

            UserRegister userRegister = new UserRegister();
            var rez = userRegister.sendNotification(mocknews.Object,mockdm.Object);
            Assert.AreEqual(true, rez);
        }
    }
}
