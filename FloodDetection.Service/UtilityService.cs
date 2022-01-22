/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Utility service class
 *****************************************************************************/

namespace FloodDetection.Service
{
    /// <summary>
    /// Interbal class for Utility Service
    /// </summary>
    internal class UtilityService
    {
        /// <summary>
        /// Function to return if Path is valid or not
        /// </summary>
        /// <param name="filePath">string</param>
        /// <returns>bool</returns>
        public static bool IsPathValid(string filePath)
        {
            bool isPathValid = false;

            if (!String.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                isPathValid = true;
            }

            return isPathValid;
        }
    }
}
