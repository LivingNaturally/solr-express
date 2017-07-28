﻿using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using Newtonsoft.Json.Linq;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class BoostParameter<TDocument> : IBoostParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        BoostFunctionType IBoostParameter<TDocument>.BoostFunctionType { get; set; }

        SearchQuery IBoostParameter<TDocument>.Query { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IBoostParameter<TDocument>)this;
            var boostFunction = parameter.BoostFunctionType.ToString().ToLower();

            this._result = new JProperty(boostFunction, parameter.Query.Execute());
        }
    }
}
