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

        /// <summary>
        /// variable to hold data file path
        /// </summary>
        private readonly List<Device> devices;
        private readonly List<RainfallReading> readings;

        #endregion

        #region public Constructor

        /// <summary>
        /// Constructor to initialize the local variables
        /// </summary>
        /// <param name="Devices">List<Device></param>
        /// <param name="RainfallReadings">List<RainfallReading></param>
        public RainfallReadingService(List<Device> Devices, List<RainfallReading> RainfallReadings)
        {
            this.devices = Devices;
            this.readings = RainfallReadings;
        }

        #endregion

        #region Public functions

        /// <summary>
        ///  GetRainFallTrends
        /// </summary>
        /// <param name="startTime">DateTime</param>
        /// <param name="iThreshhold">int</param>
        /// <returns>List<RainFallTrend></returns>
        public List<RainFallTrend> GetRainFallTrends(DateTime startTime, int iThreshhold)
        {
            DateTime intialStartTime = startTime;
            List<RainFallTrend> rainFallTrends1 = new List<RainFallTrend>();
            //Dictionary<string, RainFallTrend> rainFallTrends = new Dictionary<string, RainFallTrend>();

            foreach (Device device in devices)
            {
                RainFallTrend rainFallTrend = new RainFallTrend()
                {
                    DeviceName = device.Name,
                    Location = device.Location,
                };

                startTime = intialStartTime;

                String time = String.Concat(startTime, " - ", startTime.AddHours(iThreshhold)).ToString();
                int avgRainfall = 0;

                foreach (RainfallReading reading in readings)
                {
                    if (reading.DeviceId == device.Id && reading.Time >= startTime && reading.Time <= startTime.AddHours(iThreshhold))
                    {
                        avgRainfall += reading.Reading;
                    }
                    else if (reading.Time > startTime.AddHours(iThreshhold))
                    {
                        rainFallTrend.Rain = avgRainfall;
                        rainFallTrend.Time = time;
                        rainFallTrends1.Add(rainFallTrend);

                        rainFallTrend = new RainFallTrend()
                        {
                            DeviceName = device.Name,
                            Location = device.Location,
                        };

                        startTime = reading.Time;

                        time = String.Concat(startTime, " - ", startTime.AddHours(iThreshhold)).ToString(); ;

                        //avgRainfall = 0;
                    }
                }

                rainFallTrend.Rain = avgRainfall;
                rainFallTrend.Time = time;
                rainFallTrends1.Add(rainFallTrend);
            }
            return rainFallTrends1;
        }

    }



    #endregion

}