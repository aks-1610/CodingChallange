/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Model class for device
 *****************************************************************************/

namespace FloodDetection.Models
{
    /// <summary>
    /// Class to manage RainfallReading
    /// </summary>
    public class RainfallReading
    {

        /// <summary>
        /// Gets and Sets the device Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets and Sets the Time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets and Sets the Rainfall reading
        /// </summary>
        public int Reading { get; set; }
    }
}
