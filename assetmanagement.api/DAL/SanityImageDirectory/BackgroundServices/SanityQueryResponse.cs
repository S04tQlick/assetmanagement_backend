namespace AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;

public class SanityQueryResponse
{
    public List<object>? Result { get; set; }
}

// public class SanityQueryResponse<T>
// {
//     public T[]? Result { get; set; }
//     public QueryStats? Stats { get; set; }
// }
//
// public class QueryStats
// {
//     public double? Ms { get; set; }
// }