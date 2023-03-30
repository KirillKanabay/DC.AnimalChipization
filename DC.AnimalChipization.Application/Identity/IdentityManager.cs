using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Identity.Contracts;
using DC.AnimalChipization.Application.Identity.Models;
using DC.AnimalChipization.Data.Common.UoW;

namespace DC.AnimalChipization.Application.Identity
{
    public class IdentityManager : IIdentityManager
    {
        protected ApplicationUser CurrentUser { get; private set; }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IdentityManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Authenticate user with given email and password
        /// </summary>
        /// <exception cref="UnauthorizedException">Throws when credentials are incorrect</exception>
        public async Task<ApplicationUser> AuthenticateAsync(string email, string password)
        {
            var userEntity = await _unitOfWork.Accounts.Authenticate(email, password);

            if (userEntity != null)
            {
                CurrentUser = _mapper.Map<ApplicationUser>(userEntity);
            }
            else
            {
                throw new UnauthorizedException();
            }

            return CurrentUser;
        }

        public ApplicationUser GetCurrentUser() => CurrentUser;
    }
}
