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
        public static GenericRepository<Umfrage> UmfrageRepo;
        public static GenericRepository<Votum> VotumRepo;
        public static GenericRepository<AspNetUsers> UserRepo;

        public UmfragenLogic(Entities context)
        {
            UmfrageRepo = new GenericRepository<Umfrage>(context);
            VotumRepo = new GenericRepository<Votum>(context);
            UserRepo = new GenericRepository<AspNetUsers>(context);
        }

        public List<Umfrage> ZugaenglicheUmfragenAuswaehlen(string UserId)
        {
            DateTime today = DateTime.Now.Date;

            return UmfrageRepo.Get().Where(u => u.End_Termin >= today && u.Start_Termin <= today).ToList();
        }

        public List<Votum> VotumVeraendern(string UserId, int UmfrageId)
        {
            Umfrage umfrage = UmfrageRepo.GetByID(UmfrageId);

            List<Votum> vota = new List<Votum>();

            foreach (Frage f in umfrage.Frage)
            {
                Votum votum = VotumRepo.Get().FirstOrDefault(v => v.Antwort.Frage.Equals(f));

                if (votum == null)
                {
                    votum = new Votum();
                    votum.Antwort = f.Antwort.FirstOrDefault();
                    votum.AntwortId = f.Antwort.FirstOrDefault().Id;

                    votum.AspNetUsers = UserRepo.GetByID(UserId);
                    votum.AspNetUsersId = UserId;
                }

                vota.Add(votum);
            }

            return vota;
        }
    }
}
