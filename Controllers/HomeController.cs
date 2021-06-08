using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeatureToogle.Models;
using Microsoft.FeatureManagement.Mvc;
using Microsoft.FeatureManagement;

namespace FeatureToogle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [FeatureGate("PrivacyFeature")]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> FeatureMessage()
        {
            if (await _featureManager.IsEnabledAsync("FeatureMessage"))
                ViewData["Mensagem"] = "Feature Ativada!";
            else
                ViewData["Mensagem"] = "Feature Desativada!";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
