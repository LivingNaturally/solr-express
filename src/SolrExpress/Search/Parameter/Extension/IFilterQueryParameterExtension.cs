using SolrExpress.Search.Query;

namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure local filter query parameter
    /// </summary>
    public static class IFilterQueryParameterExtension
    {
        /// <summary>
        /// Configure value to include in query
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="query">Value to include in query</param>
        public static IFilterQueryParameter<TDocument> Query<TDocument>(this IFilterQueryParameter<TDocument> parameter, SearchQuery<TDocument> query)
            where TDocument : Document
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure plain value to include in query
        /// </summary>
        /// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Plain value to include in query</param>
        public static IFilterQueryParameter<TDocument> Value<TDocument>(this IFilterQueryParameter<TDocument> parameter, string value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }


        //public static DocumentSearch<TDocument> FilterQueryParameter<TDocument>(this DocumentSearch<TDocument> documentSearch, string value)
        //    where TDocument : Document
        //{
        //    var parameter = documentSearch.ServiceProvider.GetService<IFilterQueryParameter<TDocument>>();

        //    parameter.Value = value;

        //    documentSearch.Add(parameter);

        //    return documentSearch;
        //}
    }
}
