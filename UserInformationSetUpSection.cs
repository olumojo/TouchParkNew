using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace TouchPark
{
    public class UserInformationSetUpSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {

            List<UserInfomation> myConfigObject = new List<UserInfomation>();

            foreach (XmlNode childNode in section.ChildNodes)
            {
                var singleConfigObj = new UserInfomation();
                foreach (XmlAttribute attrib in childNode.Attributes)
                {                   
                    switch (attrib.Name)
                    {
                        case "username":
                            singleConfigObj.username = attrib.Value;
                            break;
                        case "userInformationID":
                            singleConfigObj.userInformationID = long.Parse(attrib.Value);
                            break;
                        case "passcode":
                            singleConfigObj.passcode = attrib.Value;
                            break;
                        default:
                            break;
                    }                   
                }
                myConfigObject.Add(singleConfigObj);
            }
            return myConfigObject;
        }
    }
}
