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
                var rainReadings = readings.Where(x => x.DeviceId == device.Id).OrderBy(x => x.Time).ToList();
                if (null != rainReadings && rainReadings.Count > 0)
                {
                    RainFallTrend rainFallTrend = new RainFallTrend()
                    {
                        DeviceName = device.Name,
                        Location = device.Location,
                    };

                    startTime = intialStartTime;

                    String time = String.Concat(startTime, " - ", startTime.AddHours(iThreshhold)).ToString();
                    int avgRainfall = 0;
                    int totalThreshHoldCount = 1;
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
            return rainFallTrendsList;
        }

    }

    #endregion

}