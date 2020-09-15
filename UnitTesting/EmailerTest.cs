using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Detyra_2;
using Moq;

namespace UnitTesting
{
    [TestClass]
    public class EmailerTest
    {
        [TestMethod]
        public void sendEmailtestGood()
        {
            //Arrange
            
            Mock<IEmailer> moq = new Mock<IEmailer>();
            Emailer em = new Emailer();
            var email = "email1";
            var mesazhi = "test";
            bool expected = true;
            //Act
            
            
            moq.Setup(y => y.shtonjoftimin(It.IsAny<DateTime>(), email)).Returns(true);
            bool actual = em.sendEmail(email, mesazhi);
            //Assert
            //moq.Verify(a => a.shtonjoftimin(It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(true,moq.Object.shtonjoftimin(It.IsAny<DateTime>(), email));
            moq.Verify(y => y.shtonjoftimin(It.IsAny<DateTime>(), email), Times.Once());
            
        }





        [TestMethod]
        public void shtonjoftiminTestGood()
        {
            //Arrange
            string dt = "2019-11-18 14:01:01";
            string adresa = "email2";
            Emailer em = new Emailer();
            DataManager dm = new DataManager();
            Mock<IDataManager> mockdm = new Mock<IDataManager>();
            Mock<IEmailer> mockem = new Mock<IEmailer>();
            string[] email = new string[] { "email1", "email2" };
            //act
            //bejme mock emailet qe do te na kthehen nga lista dhe shikojme nqs ekzekutohet nje here ky si funksion
            mockdm.Setup(a => a.getEmailFromDataSet()).Returns(email);
            var amock = mockdm.Object.getEmailFromDataSet();
            var actual = em.shtonjoftimin(Convert.ToDateTime(dt), amock[0]);
            //assert
            Assert.AreNotEqual(adresa, amock);//krahasojme qe emaili i kthyer nuk eshte i njejte me emailet e listes
            mockdm.Verify(a => a.getEmailFromDataSet(), Times.Once());//kontrollojme nqs therritet nje here metoda e marrjes se emaileve
            
            Assert.AreEqual(true, actual);//kontrollojme nqs modifikimi behet me sukses per emailet mock
        }


    }
}
