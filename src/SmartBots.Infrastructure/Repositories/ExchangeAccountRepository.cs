using SmartBots.Application.Features.Exchange;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories
{
    public class ExchangeAccountRepository : IExchangeAccountRepository
    {
        private readonly IUserOwnedEntityRepository<ExchangeAccount> _repository;

        public ExchangeAccountRepository(IUserOwnedEntityRepository<ExchangeAccount> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ExchangeAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> AddAsync(ExchangeAccount item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.AddAsync(item, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(ExchangeAccount item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<IList<ExchangeAccount>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<IList<ExchangeAccount>> GetFilteredAsync(
            Expression<Func<ExchangeAccount, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetFilteredAsync(filter, cancellationToken);
        }

        public IQueryable<ExchangeAccount> Query()
        {
            return _repository.Query();
        }

        public async Task<List<ExchangeAccountDto>> GetCurrentUserItemsAsync(
            CancellationToken cancellationToken = default)
        {
            (var result, _) = await _repository.GetCurrentUserItemsAsync<ExchangeAccountDto>(cancellationToken: cancellationToken);

            return result;
        }
    }
}
