using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Filter Query parameter
    /// </summary>
    public interface IFilterQueryParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Value to include in query
        /// </summary>
        SearchQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Plain value to include in query
        /// </summary>
        string Value { get; set; }

    }
}
