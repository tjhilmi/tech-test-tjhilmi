using System.Xml;
using HmxLabs.TechTest.Models;

namespace HmxLabs.TechTest.RiskSystem
{
    public class PricingConfigLoader
    {
        public string? ConfigFile { get; set; }

        public PricingEngineConfig LoadConfig()
        {

            XmlDocument ConfigFile_ = new XmlDocument();
            ConfigFile_.Load(ConfigFile);

            XmlNodeList Clist = ConfigFile_.GetElementsByTagName("PricingEngines")[0].ChildNodes;

            PricingEngineConfig configs = new PricingEngineConfig();


            for (int i = 0; i < Clist.Count; i++)
            {
                configs.Add(CreateConfigFromAttributes(Clist[i].Attributes));
            }

            return configs;

        }

        private PricingEngineConfigItem CreateConfigFromAttributes(XmlAttributeCollection Attr)
        {

            PricingEngineConfigItem PEngine = new PricingEngineConfigItem();
            PEngine.Assembly = Attr.GetNamedItem("assembly").InnerText;
            PEngine.TradeType = Attr.GetNamedItem("tradeType").InnerText;
            PEngine.TypeName = Attr.GetNamedItem("pricingEngine").InnerText;

            return PEngine;
        }


    }
}