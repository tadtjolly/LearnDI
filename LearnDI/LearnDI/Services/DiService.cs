using LearnDI.Connection;
using LearnDI.EntityDbContext;
using LearnDI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDI.Services
{
    public class DiService : IDiService
    {
        private readonly DiDbContext _dbDIDatabase;
        public DiService(DiDbContext dbDIDatabase)
        {
            _dbDIDatabase = dbDIDatabase;
        }

        public string returnDataDi()
        {
            var t = new DateTime();
            var db = _dbDIDatabase.DiEntity.FirstOrDefault(x => x.id == 1);
            return "Anh Vòi To";
        }
    }
}
