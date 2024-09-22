using AutoMapper;
using RedisAndUoW.Domain.Models;
using RedisAndUoW.Repository.Interfaces;
using RedisAndUoW.Services.Interfaces;

namespace RedisAndUoW.Services.Implements
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<DecorationShopDBContext> unitOfWork, ILogger<AccountService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
    }
}
