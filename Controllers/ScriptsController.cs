using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Database;
using YatraBackend.Services;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScriptsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Finance()
    {
        var financeMetadata = new string[]
        {
            "Finance",
            "Application",
            "Machine Learning",
            "Predictive Analytics",
            "Financial Data",
            "Investment",
            "Portfolio Management",
            "Risk Assessment",
            "Algorithmic Trading",
            "Asset Allocation",
            "Financial Modeling",
            "Data Analysis",
            "Predictive Modeling",
            "Trading Strategies",
            "Market Trends",
            "Financial Forecasting",
            "Decision Support",
            "Investment Strategies",
            "Quantitative Analysis",
            "Market Analysis",
            "Portfolio Optimization",
            "Risk Management",
            "Financial Planning",
            "Stock Market",
            "Cryptocurrency",
            "Financial Algorithms",
            "Data Mining",
            "Sentiment Analysis",
            "Credit Scoring",
            "Fraud Detection",
            "Real-time Data",
            "Market Volatility",
            "Economic Indicators",
            "Trading Signals",
            "Pattern Recognition",
            "Financial Services",
            "Performance Evaluation",
            "Trading Platform",
            "Backtesting",
            "Robo-Advisors",
            "Wealth Management",
            "Equity Research",
            "Financial Technology (FinTech)",
            "Alternative Data",
            "Capital Markets",
            "Fundamental Analysis",
            "Technical Analysis",
            "Market Liquidity",
            "Financial Instruments",
            "Automated Trading",
            "QR Payments",
            "Online Banking",
            "Payment Processing",
            "Digital Wallets",
            "Transaction Security",
            "Account Management",
            "Mobile Banking",
            "Bill Payment",
            "Fund Transfers",
            "Account Alerts"
        };

        var domain = await dbContext.Domains
            .Where(x => x.Name.Equals("Finance"))
            .FirstOrDefaultAsync();

        domain.Metadata = financeMetadata.ToList();

        dbContext.Update(domain);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Health()
    {
        var healthMetadata = new string[]
        {
            "Healthcare",
            "Medical",
            "Health",
            "Telemedicine",
            "Electronic Health Records",
            "EHR",
            "Medical Imaging",
            "Health Information Technology",
            "Health Data",
            "Patient Care",
            "Clinical Decision Support",
            "Health Monitoring",
            "Medical Devices",
            "Remote Patient Monitoring",
            "Health Informatics",
            "Medical Records",
            "Health Analytics",
            "Health Management",
            "Health Apps",
            "Health Wearables",
            "Medical Diagnosis",
            "Disease Management",
            "Healthcare Technology",
            "Digital Health",
            "Health Information Exchange",
            "Medical Sensors",
            "Health Tracking",
            "Health Sensors",
            "Health AI",
            "Medical Research",
            "Healthcare Innovation",
            "Medical Software",
            "Health Education",
            "Population Health",
            "Public Health",
            "Health IT",
            "Healthcare Analytics",
            "Healthcare Management",
            "Healthcare Services",
            "Healthcare Apps",
            "Clinical Data",
            "Healthcare Systems",
            "Healthcare Solutions",
            "Healthcare Providers",
            "Healthcare Industry",
            "Healthcare Informatics",
            "Medical Technology",
            "Health Tech",
            "Healthcare Delivery",
            "Healthcare Professionals",
            "Medical Informatics",
            "Healthcare Data",
            "Healthcare Trends",
            "Patient Engagement"
        };

        var domain = await dbContext.Domains
            .Where(x => x.Name == "Health")
            .FirstOrDefaultAsync();

        domain.Metadata = healthMetadata.ToList();

        dbContext.Domains.Update(domain);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> Recommend(Guid id)
    {
        var query = await dbContext.Metadatas
            .FirstOrDefaultAsync(x => x.Id == id);

        var metadata = await dbContext.Metadatas
            .Where(x => x.Id != id)
            .ToListAsync();

        var result = MLHelper.Recommend(query, metadata);
        
        return Ok(result);
    }
}