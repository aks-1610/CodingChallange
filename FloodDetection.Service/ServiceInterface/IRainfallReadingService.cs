using FloodDetection.Models;

namespace FloodDetection.Service.ServiceInterface
{
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
    }
}
