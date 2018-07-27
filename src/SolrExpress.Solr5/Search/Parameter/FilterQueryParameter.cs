using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    public sealed class FilterQueryParameter<TDocument> : IFilterQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JToken _result;

        public SearchQuery<TDocument> Query { get; set; }
        public string Value { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var paramsJObj = (JObject)container["params"] ?? new JObject();
            var jArray = (JArray)paramsJObj["fq"] ?? new JArray();
            jArray.Add(this._result);
            paramsJObj["fq"] = jArray;
            container["params"] = paramsJObj;
        }

        public void Execute()
        {
            Checker.IsTrue<ArgumentException>(this.Query == null && string.IsNullOrWhiteSpace(this.Value));
            var value = this.Query?.Execute() ?? this.Value;
            this._result = value;
        }
    }
}
