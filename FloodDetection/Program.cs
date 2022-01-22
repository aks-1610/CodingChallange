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
    static DataFileReadingService? objDataFileReadingService;
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

        //We can loop through the folder to get the list of the reading data files as well
        List<string> readingDataFileList = new List<string>();
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile1));
        readingDataFileList.Add(Path.Combine(dataFileFolderName, _rainfallReadingsDataFile2));

        Console.WriteLine("Reading data csv files..");
        objDataFileReadingService = new DataFileReadingService(deviceDataFile, readingDataFileList);

        if (null != objDataFileReadingService)
        {
            //Get the device list
            List<Device> devices = GetDevices(deviceDataFile);
            List<RainfallReading> rainfallReadings = GetRainfallReadingData();

            List<RainFallTrend> rainTrends = GetRainFallTrends(devices, rainfallReadings, startTime, threshHold);

            foreach (RainFallTrend rainFallTrend in rainTrends)
            {
                Console.WriteLine(rainFallTrend.DeviceName + "\t" + rainFallTrend.Time + "\t" + rainFallTrend.Trend);
            }
        }
        else
        {
            Console.WriteLine("Data files can not be read");
        }

        Console.ReadKey();
        //
    }

    #region Private Functions

    /// <summary>
    /// Function to get the device list
    /// </summary>
    /// <param name="deviceDataFile">string</param>
    /// <returns>List<Device></returns>
    private static List<Device> GetDevices(string deviceDataFile)
    {

        List<Device> devices = null;

        try
        {
            devices = objDataFileReadingService.GetDeviceList();
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
    private static List<RainfallReading> GetRainfallReadingData()
    {
        List<RainfallReading> rainfallReading = null;

        try
        {
            rainfallReading = objDataFileReadingService.GetRainfallReading();
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