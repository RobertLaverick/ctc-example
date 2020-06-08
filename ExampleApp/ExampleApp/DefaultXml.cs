using System;
using System.Xml.Linq;

namespace ExampleApp
{
    public class DefaultXml
    {
        public static string ArrivalNotification = XElement.Load(@"ArrivalNotificationExample.xml").ToString();
    }
}
