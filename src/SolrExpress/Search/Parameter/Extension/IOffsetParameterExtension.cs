﻿namespace SolrExpress.Search.Parameter.Extension
{
    /// <summary>
    /// Extensions to configure in offset parameter
    /// </summary>
    public static class IOffsetParameterExtension
    {
        /// <summary>
        /// Configure value of offset
        /// </summary>
		/// <param name="parameter">Parameter to configure</param>
        /// <param name="value">Value of offset</param>
        public static IOffsetParameter<TDocument> Value<TDocument>(this IOffsetParameter<TDocument> parameter, long value)
            where TDocument : Document
        {
            parameter.Value = value;

            return parameter;
        }
    }
}
