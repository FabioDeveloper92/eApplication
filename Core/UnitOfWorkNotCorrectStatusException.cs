using System;

namespace Infrastructure.Core
{
    public class UnitOfWorkNotCorrectStatusException : Exception
    {
        public UnitOfWorkNotCorrectStatusException(UnitOfWorkStatus expected, UnitOfWorkStatus found) : base(
            $"Expected status: {expected} but found: {found}")
        {
        }
    }
}
