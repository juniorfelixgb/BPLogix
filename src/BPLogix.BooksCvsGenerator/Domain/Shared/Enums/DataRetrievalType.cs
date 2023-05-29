namespace BPLogix.BooksCvsGenerator.Domain.Shared.Enums
{
    /// <summary>
    /// Server: 1 - The application has not yet encountered this ISBN number and the data is retrieved using the API.
    /// Cache : 2 - The application has already encountered this ISBN number and the data is retrieved from a local cache.
    /// </summary>
    public enum DataRetrievalType
    {
        None,
        Server,
        Cache,
    }
}
