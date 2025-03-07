using HmxLabs.TechTest.Models;

namespace HmxLabs.TechTest.Loaders
{
    public class BondTradeLoader : ITradeLoader
    {
        private const char Seperator = ',';

        public IEnumerable<ITrade> LoadTrades()
        {
            var tradeList = new TradeList();
            LoadTradesFromFile(DataFile, tradeList);

            return tradeList;
        }

        public string? DataFile { get; set; }

        private BondTrade CreateTradeFromLine(string line_)
        {
            
            var items = line_.Split(new[] {Seperator});
            var trade = new BondTrade(items[6]);
            trade.TradeDate = DateTime.Parse(items[1]);
            trade.Instrument = items[2];
            trade.Counterparty = items[3];
            trade.Notional = Double.Parse(items[4]);
            trade.Rate = Double.Parse(items[5]);

            //From TradeId we will assume Supra types are Government Bonds
            if (items[0].Equals("GovBond") || items[0].Equals("Supra"))
            {
                trade.TradeType = BondTrade.GovBondTradeType;
            }
            else if (items[0].Equals("CorpBond"))
            {
                trade.TradeType = BondTrade.CorpBondTradeType;
            }

                return trade;
        }

        private void LoadTradesFromFile(string? filename_, TradeList tradeList_)
        {
            if (null == filename_)
                throw new ArgumentNullException(nameof(filename_));
            
            var stream = new StreamReader(filename_);

            using (stream)
            {
                var lineCount = 0;
                while (!stream.EndOfStream)
                {
                    if (0 == lineCount)
                    {
                        stream.ReadLine();
                    }
                    else
                    {
                        tradeList_.Add(CreateTradeFromLine(stream.ReadLine()!));    
                    }
                    lineCount++;
                }
            }
        }
    }
}
