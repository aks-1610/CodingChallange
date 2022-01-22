/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service class for the rain fall trends
 *****************************************************************************/

namespace FloodDetection.Service
{
    using FloodDetection.Models;
    using FloodDetection.Service.ServiceInterface;

    /// <summary>
    /// Service class for the rain fall trends
    /// </summary>
    public class RainfallReadingService : IRainfallReadingService
    {
        #region Private members

        private List<Device> devices;
        private List<RainfallReading> readings;
        private readonly IDataFileReadingService? dataFileReadingService;

        #endregion

        #region public Constructor

        /// <summary>
        /// Constructor to initialize the local variables
        /// </summary>
        /// <param name="dataFileReadingService">IDataFileReadingService</param>
        public RainfallReadingService(IDataFileReadingService dataFileReadingService)
        {
            this.dataFileReadingService = dataFileReadingService;
        }

        /// <summary>
        /// Constructor to initialize the local variables
        /// </summary>
        /// <param name="Devices">List<Device></param>
        /// <param name="RainfallReadings">List<RainfallReading></param>
        public RainfallReadingService(List<Device> Devices, List<RainfallReading> RainfallReadings)
        {
            this.devices = Devices;
            this.readings = RainfallReadings;

            dataFileReadingService = null;
        }

        #endregion

        #region Public functions

        /// <summary>
        /// 
        /// </summary>
        public void ReadDataFiles()
        {
            this.devices = dataFileReadingService.GetDeviceList();
            this.readings = dataFileReadingService.GetRainfallReading();
        }

        /// <summary>
        ///  GetRainFallTrends
        /// </summary>
        /// <param name="startTime">DateTime</param>
        /// <param name="iThreshhold">int</param>
        /// <returns>List<RainFallTrend></returns>
        public List<RainFallTrend> GetRainFallTrends(DateTime startTime, int iThreshhold)
        {
            DateTime intialStartTime = startTime;
            List<RainFallTrend> rainFallTrendsList = new List<RainFallTrend>();

            foreach (Device device in devices)
            {
                //Improving the performance by narrowing the rainfall reading list
                var rainReadings = readings.Where(x => x.DeviceId == device.Id).OrderBy(x => x.Time).ToList();

                if (null != rainReadings && rainReadings.Count > 0)
                {
                    RainFallTrend rainFallTrend = new RainFallTrend()
                    {
                        DeviceName = device.Name,
                        Location = device.Location,
                        Trend = "-",
                    };

                    startTime = intialStartTime;

                    String time = String.Concat(startTime, " - ", startTime.AddHours(iThreshhold)).ToString();
                    int avgRainfall = 0;
                    int totalThreshHoldCount = 1;
                    string strTrend = String.Empty;

                    foreach (RainfallReading reading in rainReadings)
                    {
                        if (reading.Time >= startTime && reading.Time <= startTime.AddHours(iThreshhold))
                        {
                            avgRainfall += reading.Reading;
                            totalThreshHoldCount++;
                        }
                        else if (reading.Time > startTime.AddHours(iThreshhold))
                        {
                            rainFallTrend.Rain = avgRainfall / totalThreshHoldCount;
                            rainFallTrend.Time = time;
                            rainFallTrendsList.Add(rainFallTrend);

                            rainFallTrend = new RainFallTrend()
                            {
                                DeviceName = device.Name,
                                Location = device.Location,
                            };

                            startTime = reading.Time;

                            time = String.Concat(startTime, " - ", startTime.AddHours(iThreshhold)).ToString(); ;

                            avgRainfall = reading.Reading;
                            totalThreshHoldCount = 1;
                        }
                    }

                    rainFallTrend.Rain = avgRainfall / totalThreshHoldCount;
                    rainFallTrend.Time = time;
                    rainFallTrendsList.Add(rainFallTrend);
                }
            }

            //Evaluate the trends
            EvaluateTrends(rainFallTrendsList);

            return rainFallTrendsList;
        }

        /// <summary>
        /// Function for evaluating the trend
        /// </summary>
        /// <param name="rainFallTrendsList">List of RainFallTrend</param>
        public void EvaluateTrends(List<RainFallTrend> rainFallTrendsList)
        {
            int iPreviousRain = 0;
            string strDeviceName = string.Empty;
            foreach (RainFallTrend rainFallTrend in rainFallTrendsList)
            {
                if (String.IsNullOrWhiteSpace(strDeviceName) || strDeviceName != rainFallTrend.DeviceName)
                {
                    strDeviceName = rainFallTrend.DeviceName;
                    iPreviousRain = rainFallTrend.Rain;
                    continue;
                }

                if (iPreviousRain > rainFallTrend.Rain)
                {
                    rainFallTrend.Trend = "Decreasing";
                }
                else if (iPreviousRain < rainFallTrend.Rain)
                {
                    rainFallTrend.Trend = "Increasing";
                }

                iPreviousRain = rainFallTrend.Rain;
            }
        }
    }

    #endregion
}