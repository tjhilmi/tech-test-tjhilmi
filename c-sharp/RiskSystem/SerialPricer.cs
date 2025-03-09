using System;
using System.Collections.Generic;
using System.Reflection;
using HmxLabs.TechTest.Models;

namespace HmxLabs.TechTest.RiskSystem
{
    public class SerialPricer
    {
        public void Price(IEnumerable<IEnumerable<ITrade>> tradeContainters_, IScalarResultReceiver resultReceiver_)
        {
            LoadPricers();

            foreach (var tradeContainter in tradeContainters_)
            {
                foreach (var trade in tradeContainter)
                {
                    if (!_pricers.ContainsKey(trade.TradeType))
                    {
                        resultReceiver_.AddError(trade.TradeId, "No Pricing Engines available for this trade type");
                        continue;
                    }

                    var pricer = _pricers[trade.TradeType];
                    pricer.Price(trade, resultReceiver_);
                }
            }
        }

        private void LoadPricers()
        {
            var pricingConfigLoader = new PricingConfigLoader { ConfigFile = @".\PricingConfig\PricingEngines.xml" };
            var pricerConfig = pricingConfigLoader.LoadConfig();

            foreach (var configItem in pricerConfig)
            {

                object EngineType = Activator.CreateInstance(configItem.Assembly, configItem.TypeName);

                _pricers.Add(configItem.TradeType, (IPricingEngine)EngineType);
            }
        }

        private readonly Dictionary<string, IPricingEngine> _pricers = new Dictionary<string, IPricingEngine>();
    }
}