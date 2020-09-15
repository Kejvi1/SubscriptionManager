using System.Data;

namespace Detyra_2
{
    public interface IDataManager
    {
        string[] getEmailFromDataSet();
        DataSet getEntity();
        bool kontrolloEmail(string email);
        bool kontrolloValid(string adresa);
        void storeEntity(User us, Subscription sc);
    }
}