using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using BillsServiceLibrary.Models;
using System.Globalization;

namespace BillsServiceLibrary.Map
{
    public class BillMap : ClassMap<Bills>
    {
        public BillMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.BillId).Ignore();
            Map(m => m.UsersXBills).Ignore();
        }
    }
}
