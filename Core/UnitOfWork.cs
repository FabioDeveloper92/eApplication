using System;
using System.Data;

namespace Infrastructure.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; }
        private UnitOfWorkStatus _status;
        private bool _disposed;

        public UnitOfWork(IDbConnection connection)
        {
            Connection = connection;
            Transaction = Connection.BeginTransaction();

            _status = UnitOfWorkStatus.Pending;
        }

        public virtual void Commit()
        {
            if (_status != UnitOfWorkStatus.Pending)
                throw new UnitOfWorkNotCorrectStatusException(UnitOfWorkStatus.Pending, _status);

            try
            {
                Transaction.Commit();

                _status = UnitOfWorkStatus.Committed;
            }
            catch
            {
                Rollback();

                throw;
            }
        }

        public void Rollback()
        {
            Transaction.Rollback();

            _status = UnitOfWorkStatus.Rollbacked;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //~ is the destructor
        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (Transaction != null)
                {
                    if (_status == UnitOfWorkStatus.Pending)
                        Transaction.Rollback();

                    Transaction.Dispose();
                }
            }

            _disposed = true;
        }
    }

}
