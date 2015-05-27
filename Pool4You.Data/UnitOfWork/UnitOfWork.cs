using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pool4You.Data
{
    public class UnitOfWork : IDisposable
    {
        private Entities context = new Entities();

        private GenericRepository<AspNetUsers> userRepository;
        private GenericRepository<Antwort> antwortRepository;
        private GenericRepository<Frage> frageRepository;
        private GenericRepository<Umfrage> umfrageRepository;
        private GenericRepository<Votum> votumRepository;

        public GenericRepository<AspNetUsers> AspNetUserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<AspNetUsers>(context);
                return userRepository;
            }
        }

        public GenericRepository<Antwort> AntwortRepository
        {
            get
            {
                if (this.antwortRepository == null)
                    this.antwortRepository = new GenericRepository<Antwort>(context);
                return antwortRepository;
            }
        }

        public GenericRepository<Frage> FrageRepository
        {
            get
            {
                if (this.frageRepository == null)
                    this.frageRepository = new GenericRepository<Frage>(context);
                return frageRepository;
            }
        }

        public GenericRepository<Umfrage> UmfrageRepository
        {
            get
            {
                if (this.umfrageRepository == null)
                    this.umfrageRepository = new GenericRepository<Umfrage>(context);
                return umfrageRepository;
            }
        }

        public GenericRepository<Votum> Votum
        {
            get
            {
                if (this.votumRepository == null)
                    this.votumRepository = new GenericRepository<Votum>(context);
                return votumRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}