using System;

namespace HmxLabs.TechTest.Models
{
    public class FxTrade : BaseTrade
    {
        public FxTrade(string tradeId_)
        {
            if (string.IsNullOrWhiteSpace(tradeId_))
            {
                throw new ArgumentException("A valid non null, non empty trade ID must be provided");
            }

            TradeId = tradeId_;
        }

        private string tradeType;
        public const string FxSpotTradeType = "FxSpot";
        public const string FxForwardTradeType = "FxFwd";

        public override string TradeType
        {
            get { return tradeType; }
            set { tradeType = value; }
        }

        public DateTime ValueDate { get; set; }
    }
}
