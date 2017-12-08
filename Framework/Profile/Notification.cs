
namespace Hyperbridge.Profile
{
    public class Notification
    {
        public int index;
        public string text;
        public string type;
        public string date;

        //Add this to the Json file to get a notification:
        //"notifications": [ {"index": 0,"text": "This is a Test Notification","type": "TestType","date": "17 12 07"} ]
    }
}
