using FloodDetection.Models;
using FloodDetection.Service;
/// <summary>
/// 
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
        string dataFileFolderName = @"C:\Users\Singh_anj\source\repos\FloodDetection\FloodDetection\Data"; // We can get this from config file as well

        var deviceDataFile = Path.Combine(dataFileFolderName, _deviceDataFile);
        var dataFile = string.Empty;

        List<RainfallReading> rainfallReadings = new List<RainfallReading>();

        Console.WriteLine("Reading Device csv file");
        //Read the CSV files
        List<Device> devices = GetDevices(deviceDataFile);

        Console.WriteLine("Successfully read the device file.");

        Console.WriteLine("Reading data files..");

        dataFile = Path.Combine(dataFileFolderName, _rainfallReadingsDataFile1);

        if (!String.IsNullOrWhiteSpace(dataFile))
        {
            rainfallReadings.AddRange(GetRainfallReadingData(dataFile));
        }

        dataFile = Path.Combine(dataFileFolderName, _rainfallReadingsDataFile2);

        if (!String.IsNullOrWhiteSpace(dataFile))
        {
            rainfallReadings.AddRange(GetRainfallReadingData(dataFile));
        }

        List<RainFallTrend> rainTrends = GetRainFallTrends(devices, rainfallReadings, startTime, threshHold);

        foreach (RainFallTrend rainFallTrend in rainTrends)
        {
            Console.WriteLine(rainFallTrend.DeviceName + "\t" + rainFallTrend.Time + "\t" + rainFallTrend.Trend);
        }

        //Console.WriteLine("Any other key to exit without reading data");
    }

    #region Private Functions

    /// <summary>
    /// Function to get the device list
    /// </summary>
    /// <param name="deviceDataFile">string</param>
    /// <returns>List<Device></returns>
    private static List<Device> GetDevices(string deviceDataFile)
    {
        DataFileReadingService deviceService = new DataFileReadingService(deviceDataFile);
        List<Device> devices = null;

        try
        {
            devices = deviceService.GetDeviceList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return devices;
    }


    /// <summary>
    /// Function to get the list of Rainfall readings
    /// </summary>
    /// <param name="dataFile">string</param>
    /// <returns>List of RainfallReading</returns>
    private static List<RainfallReading> GetRainfallReadingData(string dataFile)
    {
        DataFileReadingService rainfallReadingService = new DataFileReadingService(dataFile);
        List<RainfallReading> rainfallReading = null;

        try
        {
            rainfallReading = rainfallReadingService.GetRainfallReading();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return rainfallReading;
    }

    /// <summary>
    /// GetRainFallTrends
    /// </summary>
    /// <param name="devices">List<Device></param>
    /// <param name="rainfallReadings">List<RainfallReading></param>
    /// <returns>Dictionary<string, RainFallTrend></returns>

    private static List<RainFallTrend> GetRainFallTrends(List<Device> devices, List<RainfallReading> rainfallReadings, DateTime startTime, int iThreshHold)
    {
        List<RainFallTrend> rainTrends = null;
        if (null != devices && null != rainfallReadings)
        {
            RainfallReadingService rainfallReadingService = new RainfallReadingService(devices, rainfallReadings);

            rainTrends = rainfallReadingService.GetRainFallTrends(startTime, iThreshHold);
        }

        return rainTrends;
    }

    #endregion
}