using Newtonsoft.Json;
using System;
using System.Net;

namespace QuantoCustaRDCTa10reais
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal PreçoEmReais = 1;

            WebClient webClient = new WebClient();
            dynamic APIbitcoinprice = JsonConvert.DeserializeObject(webClient.DownloadString("https://blockchain.info/ticker"));
            dynamic APIcrex24_RDCTBTC_orderbook = JsonConvert.DeserializeObject(webClient.DownloadString(new Uri("https://api.crex24.com/v2/public/orderBook?instrument=RDCT-BTC&limit=20000")));

            decimal precoBTCreais = APIbitcoinprice.BRL.last;

            decimal BTC_a_ser_trocado_por_RDCT = 0.00000001m;

            for (int i = 0; APIcrex24_RDCTBTC_orderbook.sellLevels[i].price < PreçoEmReais / precoBTCreais; i++)
            {
                    BTC_a_ser_trocado_por_RDCT += Convert.ToDecimal(APIcrex24_RDCTBTC_orderbook.sellLevels[i].price) * Convert.ToDecimal(APIcrex24_RDCTBTC_orderbook.sellLevels[i].volume);
            }

            Console.WriteLine("São necessários BTC " + Math.Round(BTC_a_ser_trocado_por_RDCT, 8) + " ou R$ " + Math.Round(BTC_a_ser_trocado_por_RDCT * precoBTCreais, 2) + " para levar o preço do RDCT a R$ " + Math.Round(PreçoEmReais, 2));
            Console.ReadKey();
        }
    }
}
