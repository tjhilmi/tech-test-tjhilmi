using System.Diagnostics;
using System.Reflection.PortableExecutable;
using HmxLabs.TechTest.Models;

namespace HmxLabs.TechTest.Loaders
{
    public class FxTradeLoader : ITradeLoader
    {
        private const char Seperator = '¬';

        public IEnumerable<ITrade> LoadTrades()
        {
            var tradeList = new TradeList();
            LoadTradesFromFile(DataFile, tradeList);

            return tradeList;
        }

        public string? DataFile { get; set; }

        private FxTrade CreateTradeFromLine(string line_)
        {

            var items = line_.Split(new[] { Seperator });
            var trade = new FxTrade(items[8]);
            trade.TradeDate = DateTime.Parse(items[1]);
            trade.Instrument = items[2] + items[3];
            trade.Counterparty = items[7];
            trade.Notional = Double.Parse(items[4]);
            trade.Rate = Double.Parse(items[5]);
            trade.ValueDate = DateTime.Parse(items[6]);

            if (items[0].Equals("FxSpot"))
            {
                trade.TradeType = FxTrade.FxSpotTradeType;
            }
            else if (items[0].Equals("FxFwd"))
            {
                trade.TradeType = FxTrade.FxForwardTradeType;
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
                string? line;

                while ((line = stream.ReadLine()) != null)
                {

                    if ((lineCount >= 2) && (!line.StartsWith("END")))
                    {
                        tradeList_.Add(CreateTradeFromLine(line));
                    }
                    lineCount++;
                }

            }
        }
    }
}

