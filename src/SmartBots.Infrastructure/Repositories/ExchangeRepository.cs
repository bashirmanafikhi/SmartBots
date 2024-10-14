using SmartBots.Application.Features.Exchange;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Infrastructure.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly IUserOwnedEntityRepository<Exchange> _repository;

        public ExchangeRepository(IUserOwnedEntityRepository<Exchange> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Exchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> AddAsync(Exchange item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.AddAsync(item, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(Exchange item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _repository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<IList<Exchange>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<IList<Exchange>> GetFilteredAsync(
            Expression<Func<Exchange, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetFilteredAsync(filter, cancellationToken);
        }

        public IQueryable<Exchange> Query()
        {
            return _repository.Query();
        }

        public async Task<List<ExchangeDto>> GetCurrentUserItemsAsync(
            CancellationToken cancellationToken = default)
        {
            (var result, _) = await _repository.GetCurrentUserItemsAsync<ExchangeDto>(cancellationToken: cancellationToken);

            return result;
        }
    }
}
