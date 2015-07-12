using Pool4You.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool4You.Logic
{
    public class VotumVeraendernK
    {
         Entities _context;

        public const string _FRAGETYP = "einfach";

        public static GenericRepository<Umfrage> UmfrageRepo;
        public static GenericRepository<Votum> VotumRepo;
        public static GenericRepository<AspNetUsers> UserRepo;
        public static GenericRepository<Antwort> AntwortRepo;
        public static GenericRepository<Frage> FrageRepo;

        public VotumVeraendernK(Entities context)
        {
            _context = context;

            UmfrageRepo = new GenericRepository<Umfrage>(context);
            VotumRepo = new GenericRepository<Votum>(context);
            UserRepo = new GenericRepository<AspNetUsers>(context);
            AntwortRepo = new GenericRepository<Antwort>(context);
            FrageRepo = new GenericRepository<Frage>(context);
        }


        public List<Votum> GibVotum(string UserId, int UmfrageId)
        {
            Umfrage umfrage = UmfrageRepo.GetByID(UmfrageId);

            List<Votum> vota = new List<Votum>();

            foreach (Frage f in umfrage.Frage)
            {
                Votum votum = VotumRepo.Get().FirstOrDefault(v => v.Antwort.Frage.Equals(f) && v.AspNetUsersId == UserId);

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

        public bool SchreibeVotum(string UserId, int UmfrageId, IList<Votum> vota)
        {
            if (UserId != null && vota != null)
            {
                foreach (var votum in vota)
                {
                    if (votum.Id == 0)
                    {
                        VotumValuesSetzen(UserId, votum, votum.AntwortId);

                        VotumRepo.Insert(votum);
                    }
                    else
                    {
                        var existingVotum = VotumRepo.GetByID(votum.Id);

                        VotumValuesSetzen(UserId, existingVotum, votum.AntwortId);

                        VotumRepo.Update(existingVotum);
                    }
                }

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        private static void VotumValuesSetzen(string UserId, Votum Votum, int AntwortId)
        {
            Votum.AspNetUsersId = UserId;
            Votum.AspNetUsers = UserRepo.GetByID(UserId);

            Votum.AntwortId = AntwortId;
            Votum.Antwort = AntwortRepo.GetByID(AntwortId);

            Votum.Datum = DateTime.Now;
            Votum.Uhrzeit = DateTime.Now.TimeOfDay;
        }
    }
}
