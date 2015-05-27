using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pool4You.Data;

namespace Pool4You.Logic
{
    public class UmfragenLogic
    {
        public static GenericRepository<Umfrage> repo;

        public UmfragenLogic(Entities context)
        {
            repo = new GenericRepository<Umfrage>(context);
        }

        public List<Umfrage> ZugaenglicheUmfragenAuswaehlen(int UserId)
        {
            return repo.Get().ToList();
        }
    }
}
