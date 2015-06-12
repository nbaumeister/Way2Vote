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

        public const string _FRAGETYP = "einfach";

        public static GenericRepository<Umfrage> UmfrageRepo;
        public static GenericRepository<Votum> VotumRepo;
        public static GenericRepository<AspNetUsers> UserRepo;
        public static GenericRepository<Antwort> AntwortRepo;
        public static GenericRepository<Frage> FrageRepo;

        public UmfragenLogic(Entities context)
        {
            _context = context;

            UmfrageRepo = new GenericRepository<Umfrage>(context);
            VotumRepo = new GenericRepository<Votum>(context);
            UserRepo = new GenericRepository<AspNetUsers>(context);
            AntwortRepo = new GenericRepository<Antwort>(context);
            FrageRepo = new GenericRepository<Frage>(context);
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

        public List<Umfrage> BeendeteUmfragen()
        {
            DateTime today = DateTime.Now.Date;

            return UmfrageRepo.Get().Where(u => u.End_Termin < today).ToList();
        }

        public Umfrage UmfrageErstellen()
        {
            var u = new Umfrage();
            u.Frage = new List<Frage>();

            var f = new Frage();
            f.Antwort = new List<Antwort>();
            f.Antwort.Add(new Antwort());

            u.Frage.Add(f);

            return u;
        }

        public void UmfrageErstellen(Umfrage Umfrage, string UserId)
        {
            if (UserId != null && Umfrage != null)
            {
                Umfrage.AspNetUsersId = UserId;

                UmfrageRepo.Insert(Umfrage);

                foreach (var frage in Umfrage.Frage)
                {
                    frage.Fragetyp = _FRAGETYP;

                    FrageRepo.Insert(frage);

                    foreach (var antwort in frage.Antwort)
                    {
                        AntwortRepo.Insert(antwort);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void UmfrageLoeschen(int UmfrageId)
        {
            Umfrage umfrage = UmfrageRepo.GetByID(UmfrageId);

            if (umfrage != null)
            {
                int fragenLength = umfrage.Frage.Count;
                List<Frage> fragen = umfrage.Frage.ToList();
                for (int i = 0; i < fragenLength; i++)
                {
                    int antwortLength = fragen[i].Antwort.Count;
                    List<Antwort> antworten = fragen[i].Antwort.ToList();
                    for (int y = 0; y < antwortLength; y++)
                    {
                        int votaLength = antworten[y].Votum.Count;
                        List<Votum> vota = antworten[y].Votum.ToList();
                        for (int z = 0; z < votaLength; z++)
                        {
                            VotumRepo.Delete(vota[z]);
                        }
                        _context.SaveChanges();

                        AntwortRepo.Delete(antworten[y]);
                    }
                    _context.SaveChanges();
                    FrageRepo.Delete(fragen[i]);
                }
                _context.SaveChanges();

                UmfrageRepo.Delete(umfrage);
                _context.SaveChanges();
            }

        }

        public Umfrage UmfrageAnzeigen(int id)
        {
            return UmfrageRepo.GetByID(id);
        }
    }
}
