/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service interface for reading rain fall
 *****************************************************************************/

namespace FloodDetection.Service.ServiceInterface
{
    using FloodDetection.Models;

    /// <summary>
    /// Service interface for rain fall trends
    /// </summary>
    public interface IRainfallReadingService
    {
        /// <summary>
        ///  GetRainFallTrends
        /// </summary>
        /// <param name="startTime">DateTime</param>
        /// <param name="iThreshhold">int</param>
        /// <returns>List<RainFallTrend></returns>
        List<RainFallTrend> GetRainFallTrends(DateTime startTime, int iThreshhold);

        /// <summary>
        ///  Function to read the data files
        /// </summary>
        void ReadDataFiles();

        /// <summary>
        /// Function for evaluating the trend
        /// </summary>
        /// <param name="rainFallTrendsList">List of RainFallTrend</param>
        void EvaluateTrends(List<RainFallTrend> rainFallTrendsList);
    }
}
