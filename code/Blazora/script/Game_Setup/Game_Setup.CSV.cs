using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Blazora.Script
{
    public partial class Game_Setup
    { 
        private async static Task<List<dynamic>> from_csv_filepath_get_list_record (string csv_filepath)
        {
            CsvConfiguration config = new CsvConfiguration (CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            HttpClient http = new HttpClient ();
            http.BaseAddress = new Uri (Program.base_adress_str);
            http.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            Stream st = await http.GetStreamAsync (csv_filepath);
            

            IEnumerable<dynamic> records;
            using (var reader = new StreamReader (st))
            using (var csv = new CsvReader (reader, config))
            {
                records = csv.GetRecords<dynamic> ();
                return records.ToList ();
            }

        }

    }
}
