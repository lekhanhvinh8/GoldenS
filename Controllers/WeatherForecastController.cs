using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GoldenS.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoldenS.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly AppDbContext appDbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        this.appDbContext = appDbContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("/log1")]
    public async Task<IActionResult> Log1()
    {
        var user = await appDbContext.Users.Where(u => 1 == 1).ToListAsync();
        return Ok(user);
    }

    [HttpGet("/log2")]
    public async Task<IActionResult> Log2()
    {
        var result = await appDbContext.NonKeyTable.FirstOrDefaultAsync();
        result.Name = "vinh123123";
        await appDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("/log3")]
    public async Task<IActionResult> Log3()
    {
        try
        {
            var response = GetRandomString();

            var status = response.Status;
            var IsCompleted = response.IsCompleted;
            var IsCanceled = response.IsCanceled;
            var IsFaulted = response.IsFaulted;


            Console.WriteLine(JsonConvert.SerializeObject(status) 
                + " " + JsonConvert.SerializeObject(IsCompleted)
                + " " + JsonConvert.SerializeObject(IsCanceled)
                + " " + JsonConvert.SerializeObject(IsFaulted));

            throw new Exception("545");


            response.Wait();

            status = response.Status;
            IsCompleted = response.IsCompleted;
            IsCanceled = response.IsCanceled;
            IsFaulted = response.IsFaulted;


            Console.WriteLine(JsonConvert.SerializeObject(status) 
                + " " + JsonConvert.SerializeObject(IsCompleted)
                + " " + JsonConvert.SerializeObject(IsCanceled)
                + " " + JsonConvert.SerializeObject(IsFaulted));


            var result = response.Result;

         

            
        }
        catch (AggregateException ex)
        {
            Console.WriteLine(JsonConvert.SerializeObject(ex));
            Console.WriteLine(ex.ToString());

            var innerEx = ex.InnerException;
            Console.WriteLine(JsonConvert.SerializeObject(ex.InnerException));


            // // Log detailed exception information
            // foreach (var innerEx in ex.InnerExceptions)
            // {
            // }
        }
        


        return Ok();

    }


    private Task<string> GetRandomString()
    {
        return GetRandomString1();
    }

    private async Task<string> GetRandomString1()
    {
        await Task.Delay(5000);
        throw new Exception("123");
        return "vinh";
    }

}
