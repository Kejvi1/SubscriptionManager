
namespace Detyra_2
{
    public class Subscription
    {
        private string _keywords;
        private string _categories;

        public string keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
        public string categories
        {
            get { return _categories; }
            set { _categories = value; }
        }
    }
}