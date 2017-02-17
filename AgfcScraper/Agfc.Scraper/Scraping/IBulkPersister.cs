using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agfc.Scraper.Scraping
{
   public interface IBulkPersister
    {
         Task<int> SaveBulkAsync(IEnumerable<string> sources);
        
    }
}
