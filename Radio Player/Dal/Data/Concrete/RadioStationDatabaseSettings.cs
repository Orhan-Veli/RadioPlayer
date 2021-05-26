using Radio_Player.Dal.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Data.Concrete
{
    public class RadioStationDatabaseSettings : IRadioStationDatabaseSettings
    {
        public string RadioCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
