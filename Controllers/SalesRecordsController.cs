using Microsoft.AspNetCore.Mvc;
using salesWebMvc.Services;

namespace salesWebMvc.Controllers
{
    public class SalesRecordsController:Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? mindate, DateTime? maxDate)
        {
            if(!mindate.HasValue)
            {
                mindate = new DateTime(2018, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
      
            ViewData["minDate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDate(mindate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? mindate, DateTime? maxDate)
        {
            if (!mindate.HasValue)
            {
                mindate = new DateTime(2018, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateGrouping(mindate, maxDate);

            return View(result);
        }
    }
}
