using DC.AnimalChipization.Application.Common.Exceptions;

namespace DC.AnimalChipization.Application.Features.Accounts.Exceptions
{
    public class AccountDuplicateEmailException : ConflictException
    {
        public AccountDuplicateEmailException() { }

        public AccountDuplicateEmailException(string message) : base(message) { }

        public AccountDuplicateEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}
