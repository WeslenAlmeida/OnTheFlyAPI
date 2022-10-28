﻿using Aircraft.Utils.Interface;

namespace Aircraft.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string AircraftCollectionName { get; set; }
        public string DeadfileCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }    
    }
}
