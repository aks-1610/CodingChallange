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
    /// Class to manage Device
    /// </summary>
    public class Device
    {

        /// <summary>
        /// Gets and Sets the device Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and Sets the Device Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and Sets the Location
        /// </summary>
        public string Location { get; set; }
    }
}
