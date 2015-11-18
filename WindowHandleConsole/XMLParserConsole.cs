using ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WindowHandleConsole
{
    class XMLParserConsole
    {
        private const string XML_PATH_GEN_CONFIGURATION = @"Gen_Configuration.xml";
        private const string CONFIGURATION = "Configuration";
        private const string ID = "ID";
        private const string OPTION = "Option";
        private const string TITLE = "Title";
        private const string LABLE = "Lable";
        private const string TEXT = "Text";
        private const string EmptyString = "";

        public static ConfigurationBaseId ParseConfiguration(string iScriptId)
        {
            ConfigurationBaseId configuration = new ConfigurationBaseId();
            try
            {
                XDocument doc = XDocument.Load(XML_PATH_GEN_CONFIGURATION);
                IEnumerable<XElement> listConfiguration = doc.Descendants(CONFIGURATION);
                foreach (XElement config in listConfiguration)
                {
                    if (iScriptId == config.Attribute(ID).Value)
                    {
                        configuration.IdConfig = iScriptId;
                        configuration.ListOption =
                            (from option in config.Descendants(OPTION)
                             where option.Attribute(TEXT).Value != EmptyString
                             select new Option() { Title = option.Attribute(TITLE).Value, Lable = option.Attribute(LABLE).Value, Text = option.Attribute(TEXT).Value }).ToList<Option>();
                    }
                }
            }
            catch (Exception ex)
            {
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(string.Format("[DEBUG]: Exception Parse Configuration Console : {0} ", logger));
            }
            return configuration;
        }
    }
}
