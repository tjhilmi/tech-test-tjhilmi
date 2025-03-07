namespace HmxLabs.TechTest.Models
{
    public class BondTrade : BaseTrade
    {

        public BondTrade(string tradeId_)
        {
            if (string.IsNullOrWhiteSpace(tradeId_))
            {
                throw new ArgumentException("A valid non null, non empty trade ID must be provided");
            }
            
            TradeId = tradeId_;
        }

        private string tradeType;
        public const string GovBondTradeType = "GovBond";
        public const string CorpBondTradeType = "CorpBond";

        public override string TradeType 
        { 
            get { return tradeType; }
            set { tradeType = value; }
        }
    }
}
