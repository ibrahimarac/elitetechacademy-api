using Microsoft.EntityFrameworkCore.Storage;

namespace Elitetech.Academy.Domain.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        public IAnnouncementRepository AnnouncementRepository { get; }


        IDbContextTransaction BeginTransaction();
        Task<bool> CommitAsync();
    }
}
