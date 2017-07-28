﻿using System;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Signature to information about search result
    /// </summary>
    public interface IInformationResult<TDocument> : ISearchResult
        where TDocument : Document
    {
        Information Data { get; set; }
    }
}
