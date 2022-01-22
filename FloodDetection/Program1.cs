using FloodDetection.Models;
using FloodDetection.Service;
using FloodDetection.Service.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;
/// <summary>
/// 
/// </summary>
public class Program1
{
    #region Private variables

    private const string _deviceDataFile = @"Devices.csv";
    private const string _rainfallReadingsDataFile1 = @"Data1.csv";
    private const string _rainfallReadingsDataFile2 = @"Data2.csv";

    #endregion

    /// <summary>
    /// PSVM
    /// </summary>
    public static void Main1()
    {
        DateTime startTime = DateTime.Parse("06-05-2020  9:00:00 AM");
        int threshHold = 4;

        //Get the current directory
        string dataFileFolderName = @"C:\Users\Singh_anj\source\repos\FloodDetection\FloodDetection\Data"; // We can get this from config file as well

        var deviceDataFile = Path.Combine(dataFileFolderName, _deviceDataFile);

        //We can loop through the folder to get the list of the reading data files as well
        List<string> readingDataFileList = new List<string>();
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile1));
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile2));

        var services = new ServiceCollection();

        services.AddSingleton<IDataFileReadingService>(new DataFileReadingService(deviceDataFile, readingDataFileList));

        services.AddSingleton<IRainfallReadingService>(p => new RainfallReadingService(p.GetService<IDataFileReadingService>()));

        var provider = services.BuildServiceProvider();

        var demoService = provider.GetRequiredService<IRainfallReadingService>();

        List<RainFallTrend> rainTrends = demoService.GetRainFallTrends(startTime, threshHold);

        foreach (RainFallTrend rainFallTrend in rainTrends)
        {
            Console.WriteLine(rainFallTrend.DeviceName + "\t" + rainFallTrend.Time + "\t" + rainFallTrend.Trend);
        }

        Console.ReadKey();


        var dataFile = string.Empty;

        ;
    }



    #region Private Functions

    /// <summary>
    /// ConfigureServices
    /// </summary>
    /// <param name="services">ServiceCollection</param>
    private static void ConfigureServices(ServiceCollection services)
    {

    }

    #endregion
}