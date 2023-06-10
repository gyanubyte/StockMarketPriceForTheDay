using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zerodha.Models;
using Zerodha.ServiceContracts;
using Zerodha.Services;

namespace Zerodha.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinhubService _myService;
        private readonly TradingOptions  _tradingOption;
        public HomeController(IFinhubService myService, IOptions<TradingOptions> tradingOption)
        {
            _myService = myService;
            _tradingOption = tradingOption.Value;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(_tradingOption.DefaultStockName == null)
            {
                _tradingOption.DefaultStockName = "GOOGL";
            }
            Dictionary<string,object> result =   await _myService.GetStockDetails(_tradingOption.DefaultStockName);
            Stock stock = new Stock() { StockSymbol = _tradingOption.DefaultStockName,CurrentPrice = Convert.ToDouble(result["c"].ToString()), HightestPrice = Convert.ToDouble(result["h"].ToString())
           , LowestPrice = Convert.ToDouble(result["l"].ToString()),OpenPrice = Convert.ToDouble(result["o"].ToString())};
            return View(stock);
        }
    }
}
