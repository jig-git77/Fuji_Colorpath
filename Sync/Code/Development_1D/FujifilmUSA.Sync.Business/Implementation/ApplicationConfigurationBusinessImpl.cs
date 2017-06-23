using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System.Data;
using System.Xml.Linq;
namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ApplicationConfigurationBusinessImpl : IApplicationConfigurationBusiness
    {

        public List<Entity.ApplicationConfiguration> GetApplicationConfiguration()
        {
            List<Entity.ApplicationConfiguration> configurationData = new List<Entity.ApplicationConfiguration>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsApplicationConfiguration = DataAccessFactory.Instance.GetDsResult("dbo.GetApplicationConfiguration");

                    if (dsApplicationConfiguration.Tables[0].Rows.Count > 0)
                    {

                        configurationData = dsApplicationConfiguration.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new Entity.ApplicationConfiguration()
                                        {
                                            ApplicationConfigurationId = DataReader.ReadInt(x, "ApplicationConfigurationID"),
                                            ConfigurationKey = DataReader.ReadString(x, "ConfigurationKey"),
                                            ConfigurationValue = DataReader.ReadString(x, "ConfigurationValue")
                                        }).ToList();
                    }
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GettingApplicationConfiguration"));
            return configurationData;
        }

        public int UpdateApplicationConfiguration(List<Entity.ApplicationConfiguration> applicationConfiguration)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
               () =>
               {
                   var applicationConfigurationXML = new XElement("ApplicationConfiguration",
                                                                 applicationConfiguration.
                                                                     Select(x =>
                                                                            new XElement("Configuration",
                                                                                new XAttribute("Id", System.Convert.ToInt32(x.ApplicationConfigurationId)),
                                                                                     new XAttribute("Key", System.Convert.ToString(x.ConfigurationKey)),
                                                                                         new XAttribute("Value", System.Convert.ToString(x.ConfigurationValue))

                                                                                )).ToList());

                   returnValue = DataAccessFactory.
                                   Instance.
                                   ExecuteProcedure(
                                       "[dbo].[UpdateApplicationConfiguration]",
                                       new object[]
                                    {
                                       applicationConfigurationXML.ToString()
                                    });
                   if (returnValue == -1)
                   {
                       //throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateDevice" };
                   }
               },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateApplicationConfiguration"));
            return returnValue;
        }

        public event ErrorHandler ErrorOccured;
    }
}
