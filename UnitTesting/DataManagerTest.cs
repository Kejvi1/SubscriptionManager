using Microsoft.VisualStudio.TestTools.UnitTesting;
using Detyra_2;
using System.Data;
using Moq;

namespace UnitTesting
{
    [TestClass]
    public class DataManagerTest
    {

        [TestMethod]
        public void getEntityValidCall()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("Emer", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("TipiAbonimi", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["Emer"] = "user1";
            dr["Email"] = "email";
            dr["TipiAbonimi"] = "fjale";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            var mock = new Mock<IDataManager>();
            mock.Setup(x => x.getEntity()).Returns(ds);
            var actual = mock.Object.getEntity();
            var expected = ds;
            Assert.IsTrue(actual != null);
            Assert.AreEqual(expected.Tables[0].Rows.Count, actual.Tables[0].Rows.Count);
            
        }
        
        [TestMethod]
        public void storeEntityValidCall()
        {
            User user = new User();
            Subscription subscription = new Subscription();
            user.name = "user2";
            user.email = "email2";
            subscription.keywords = "fjale";
            var mock = new Mock<IDataManager>();
            mock.Setup(x => x.storeEntity(user, subscription));

            mock.Object.storeEntity(user, subscription);
            mock.Verify(x=>x.storeEntity(user, subscription), Times.Once());
        }

        [TestMethod]
        public void kontrolloValidTestTrue()
        {
            DataManager dm = new DataManager();
            string email = "email3";
            bool pergjigjja = dm.kontrolloValid(email);
            Assert.IsTrue(pergjigjja);
        }

        [TestMethod]
        public void kontrolloEmailTest()
        {
            var mockdm = new Mock<IDataManager>();
            DataManager dm = new DataManager();
            string[] email = new string[] { "email4", "email1" };
            mockdm.Setup(x => x.getEmailFromDataSet()).Returns(email);
            var expected = "email2";
            Assert.IsFalse(dm.kontrolloEmail(expected)); //kthen false ne rastet kur emailet jane te ndryshme, duhet te kontrollojme qe rezultati eshte false
        }

        [TestMethod]
        public void getEmailFromDataSetTest()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("Emer", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("TipiAbonimi", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["Emer"] = "user1";
            dr["Email"] = "email1";
            dr["TipiAbonimi"] = "fjale";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            var mock = new Mock<IDataManager>();
            mock.Setup(x => x.getEntity()).Returns(ds);
            var actual = mock.Object.getEntity().Tables[0].Rows[0]["Email"];
            var expected = ds.Tables[0].Rows[0]["Email"];
            Assert.AreEqual(expected, actual);
        }

    }
}
