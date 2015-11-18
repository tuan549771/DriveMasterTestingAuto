using System.Collections.Generic;

namespace WindowHandleConsole
{
    class ConfigurationBaseId
    {
        public List<Option> ListOption ;
        public string IdConfig { get; set; }

        public ConfigurationBaseId() 
        {
            ListOption = new List<Option>();
            IdConfig = string.Empty;
        }
    }
}
