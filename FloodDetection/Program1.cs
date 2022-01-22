using FloodDetection.Models;
using FloodDetection.Service;
using FloodDetection.Service.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class to depict the use of Dependency injection
/// </summary>
public class Program
{
    #region Private variables

    private const string _deviceDataFile = @"Devices.csv";
    private const string _rainfallReadingsDataFile1 = @"Data1.csv";
    private const string _rainfallReadingsDataFile2 = @"Data2.csv";

    #endregion

    /// <summary>
    /// PSVM
    /// </summary>
    public static void Main()
    {
        DateTime startTime = DateTime.Parse("06-05-2020  9:00:00 AM");
        int threshHold = 4;

        //Get the current directory
        string dataFileFolderName = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\..\Data"); //@"C:\Users\Singh_anj\source\repos\FloodDetection\FloodDetection\Data"; // We can get this from config file as well

        var deviceDataFile = Path.Combine(dataFileFolderName, _deviceDataFile);

        //We can loop through the folder to get the list of the reading data files as well
        List<string> readingDataFileList = new List<string>();
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile1));
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile2));

        var services = new ServiceCollection();

        //Passing the parameters to constructors while creating the the service instance
        services.AddSingleton<IDataFileReadingService>(new DataFileReadingService(deviceDataFile, readingDataFileList));

        //Injecting the IDataFileReadingService to RainfallReadingService
        services.AddSingleton<IRainfallReadingService>(p => new RainfallReadingService(p.GetService<IDataFileReadingService>()));

        //Build the providers
        var provider = services.BuildServiceProvider();

        //Get the Rainfall reading service from DI provier
        var objRainFallReadingService = provider.GetRequiredService<IRainfallReadingService>();
        objRainFallReadingService.ReadDataFiles();

        List<RainFallTrend> rainTrends = objRainFallReadingService.GetRainFallTrends(startTime, threshHold);

        foreach (RainFallTrend rainFallTrend in rainTrends)
        {
            Console.WriteLine(rainFallTrend.DeviceName + "\t" + rainFallTrend.Time + "\t" + rainFallTrend.TrendColor + "\t" + rainFallTrend.Trend);
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