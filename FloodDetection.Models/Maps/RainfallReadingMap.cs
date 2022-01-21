/******************************************************************************
 *                          © 2022 Interfuze                                  *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service class for the device map
 *****************************************************************************/

namespace FloodDetection.Models.Maps
{
    using CsvHelper.Configuration;

    /// <summary>
    /// Mapping class for RainfallReading
    /// </summary>
    public class RainfallReadingMap : ClassMap<RainfallReading>
    {
        #region Public Constructor

        /// <summary>
        /// DeviceMap
        /// </summary>
        public RainfallReadingMap()
        {
            Map(m => m.DeviceId).Name("Device ID");
            Map(m => m.Time);
            Map(m => m.Reading).Name("Rainfall");
        }

        #endregion
    }
}
