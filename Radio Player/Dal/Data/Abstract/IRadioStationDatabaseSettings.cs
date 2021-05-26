using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Data.Abstract
{
    public interface IRadioStationDatabaseSettings
    {
        string RadioCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
