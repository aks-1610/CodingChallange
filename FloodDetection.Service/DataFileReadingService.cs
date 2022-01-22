/******************************************************************************
 *                          © 2022 Interfuze                               *
 *                          All Rights Reserved.                              *
 *                                                                            *
 ******************************************************************************
 *
 * Modification History:
 * Task#    Dev     Date        Description
 * 
 * 1001     AKS     21Jan22     Created the Service class for the device
 *****************************************************************************/

namespace FloodDetection.Service
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using FloodDetection.Models;
    using FloodDetection.Models.Maps;
    using FloodDetection.Service.ServiceInterface;
    using System.Globalization;

    /// <summary>
    /// Service class for reading data files
    /// </summary>
    public class DataFileReadingService : IDataFileReadingService
    {
        #region Private members

        /// <summary>
        /// variable to hold data file path
        /// </summary>
        private readonly string _deviceDataFilePath;
        private readonly List<string> _dataFilePathList;

        #endregion

        #region public Constructor

        /// <summary>
        /// Constructor to initialize the local variables
        /// </summary>
        /// <param name="DeviceFilePath">string</param>
        /// <param name="DataFilePathList">List of string</param>
        public DataFileReadingService(string DataFilePath, List<string> DataFilePathList)
        {
            this._deviceDataFilePath = DataFilePath;
            _dataFilePathList = DataFilePathList;
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Function to get the device List
        /// </summary>
        /// <returns>List of Devices</returns>
        public List<Device> GetDeviceList()
        {
            List<Device> deviceList;

            //Check if file path is valid
            if (!UtilityService.IsPathValid(_deviceDataFilePath))
            {
                throw new Exception("File path is not valid");
            }
            else
            {
                deviceList = new List<Device>();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    IgnoreBlankLines = false,
                };

                using (var reader = new StreamReader(_deviceDataFilePath))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Context.RegisterClassMap<DeviceMap>();

                        var isHeader = true;
                        while (csv.Read())
                        {
                            if (isHeader)
                            {
                                csv.ReadHeader();
                                isHeader = false;
                                continue;
                            }

                            if (string.IsNullOrEmpty(csv.GetField(0)))
                            {
                                isHeader = true;
                                continue;
                            }

                            deviceList.Add(csv.GetRecord<Device>());
                        }
                    }
                }
            }
            return deviceList;
        }


        /// <summary>
        /// Function to get the list if RainfallReadings
        /// </summary>
        /// <returns>List of RainfallReading</returns>
        public List<RainfallReading> GetRainfallReading()
        {
            List<RainfallReading>? rainfallReadingList = new List<RainfallReading>();

            foreach (string strDataFilePath in _dataFilePathList)
            {
                //Check if file path is valid
                if (!UtilityService.IsPathValid(strDataFilePath))
                {
                    throw new Exception("File path is not valid");
                }
                else
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        IgnoreBlankLines = false,
                    };

                    using (var reader = new StreamReader(strDataFilePath))
                    {
                        using (var csv = new CsvReader(reader, config))
                        {
                            csv.Context.RegisterClassMap<RainfallReadingMap>();

                            var isHeader = true;
                            while (csv.Read())
                            {
                                if (isHeader)
                                {
                                    csv.ReadHeader();
                                    isHeader = false;
                                    continue;
                                }

                                if (string.IsNullOrEmpty(csv.GetField(0)))
                                {
                                    isHeader = true;
                                    continue;
                                }

                                rainfallReadingList.Add(csv.GetRecord<RainfallReading>());
                            }
                        }
                    }
                }
            }

            return rainfallReadingList;
        }


        // We can add more functions for Writing to Device CSV
        // For ex:
        // public Device GetDevice(int deviceId);
        // public bool WriteDeviceData(Device device);
        // public RainfallReading GetRainfallReadingByDevice(int deviceId);
        // public bool WriteRainfallReadingDate(RainfallReading rainfallReading);

        #endregion
    }
}