﻿using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Update;

namespace SolrExpress.Solr4.Update
{
    public sealed class AtomicUpdate<TDocument> : IAtomicUpdate<TDocument>
        where TDocument : IDocument
    {
        private TDocument[] _documents;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Add informed documents in SOLR collection
        /// If a doc with same id exists in collection, the document is updated
        /// </summary>
        /// <param name="documents">Documents to add</param>
        public void Configure(params TDocument[] documents)
        {
            Checker.IsNull(documents);
            Checker.IsEmpty(documents);

            this._documents = documents;
        }

        /// <summary>
        /// Create atomic update command
        /// </summary>
        /// <param name="jObject">Container to parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            foreach (var document in this._documents)
            {
                var jPropertyDoc = new JProperty("doc", JObject.FromObject(document));

                var jPropertyAdd = new JProperty("add", new JObject(jPropertyDoc));

                jObject.Add(jPropertyAdd);
            }
        }
    }
}