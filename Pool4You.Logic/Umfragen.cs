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
        Entities _context;

        public static GenericRepository<Umfrage> UmfrageRepo;
        public static GenericRepository<Votum> VotumRepo;
        public static GenericRepository<AspNetUsers> UserRepo;
        public static GenericRepository<Antwort> AntwortRepo;

        public UmfragenLogic(Entities context)
        {
            _context = context;

            UmfrageRepo = new GenericRepository<Umfrage>(context);
            VotumRepo = new GenericRepository<Votum>(context);
            UserRepo = new GenericRepository<AspNetUsers>(context);
            AntwortRepo = new GenericRepository<Antwort>(context);
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

        public bool VotumVeraendern(string UserId, int UmfrageId, IList<Votum> vota)
        {
            if (UserId != null && vota != null)
            {
                Umfrage umfrage = UmfrageRepo.GetByID(UmfrageId);

                foreach (var votum in vota)
                {
                    if (votum.Id == 0)
                    {
                        SetVotumValues(UserId, votum, votum.AntwortId);

                        VotumRepo.Insert(votum);
                    }
                    else
                    {
                        var existingVotum = VotumRepo.GetByID(votum.Id);

                        SetVotumValues(UserId, existingVotum, votum.AntwortId);

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

        private static void SetVotumValues(string UserId, Votum Votum, int AntwortId)
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
