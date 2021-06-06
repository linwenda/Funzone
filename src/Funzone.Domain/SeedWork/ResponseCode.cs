namespace Funzone.Domain.SeedWork
{
    public enum ResponseCode
    {
        Succeeded = 20000,
        Invalid = 40000,
        NotFound = 40400,
        
        UserDomainException = 50100,
        PageDomainException = 50200
    }
}