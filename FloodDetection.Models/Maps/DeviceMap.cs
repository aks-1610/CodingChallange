/******************************************************************************
 *                          © 2022 Interfuze                               *
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
    /// Mapping class for Device
    /// </summary>
    public class DeviceMap : ClassMap<Device>
    {
        #region Public Constructor

        /// <summary>
        /// DeviceMap
        /// </summary>
        public DeviceMap()
        {
            Map(m => m.Id).Name("Device ID");
            Map(m => m.Name).Name("Device Name");
            Map(m => m.Location);
        }

        #endregion
    }
}
