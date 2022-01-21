/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service class for the RainFallTrend
 *****************************************************************************/

namespace FloodDetection.Models
{
    public class RainFallTrend
    {
        /// <summary>
        /// Gets or Sets DeviceName
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or Sets Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or Sets Rain
        /// </summary>
        public int Rain { get; set; }

        /// <summary>
        /// Gets or Sets Trend
        /// </summary>
        public string Trend
        {
            get
            {
                string returnTrend = string.Empty;
                if (Rain <= 10)
                {
                    returnTrend = "Green";
                }
                else if (Rain <= 15)
                {
                    returnTrend = "Amber";
                }
                else
                {
                    returnTrend = "Red";
                }

                return returnTrend;
            }
            private set { }
        }

        /// <summary>
        /// Gets or Sets Time range
        /// </summary>
        public string Time { get; set; }
    }
}
