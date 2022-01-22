/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service interface for rain fall trends
 *****************************************************************************/

namespace FloodDetection.Service.ServiceInterface
{
    using FloodDetection.Models;

    /// <summary>
    /// Service interface for reading data files
    /// </summary>
    public interface IDataFileReadingService
    {
        /// <summary>
        /// Function to get the device List
        /// </summary>
        /// <returns>List of Devices</returns>
        List<Device> GetDeviceList();

        /// <summary>
        /// Function to get the list if RainfallReadings
        /// </summary>
        /// <returns>List of RainfallReading</returns>
        public List<RainfallReading> GetRainfallReading();
    }
}
