
using AutoMapper;
using RedisAndUoW.Domain.Models;
using RedisAndUoW.Repository.Interfaces;
using RedisAndUoW.Services.Interfaces;

namespace RedisAndUoW.Services.Implements
{
    public class AuthenicationService : BaseService<AuthenicationService>, IAuthenticationService
    {
        public AuthenicationService(IUnitOfWork<DecorationShopDBContext> unitOfWork, ILogger<AuthenicationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }


    }
}
