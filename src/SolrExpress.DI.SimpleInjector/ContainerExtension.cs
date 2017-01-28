﻿using SimpleInjector;
using SolrExpress.Utility;
using System;

namespace SolrExpress.DI.SimpleInjector
{
    /// <summary>
    /// Configure SolrExpress DI using SimpleInjector engine
    /// </summary>
    public static class ContainerExtension
    {
        /// <summary>
        /// Add SolrExpress services
        /// </summary>
        /// <param name="container">Container used in SimpleInjector engine</param>
        /// <returns>Container used in SimpleInjector engine</returns>
        public static Container AddSolrExpress<TDocument>(this Container container, Action<SolrExpressBuilder<TDocument>> builder)
            where TDocument : IDocument
        {
            var solrExpressServiceProvider = new SolrExpressServiceProvider<TDocument>();
            var solrExpressBuilder = new SolrExpressBuilder<TDocument>(solrExpressServiceProvider);

            container.Register(() => solrExpressServiceProvider, Lifestyle.Singleton);
            container.Register<DocumentCollection<TDocument>>(Lifestyle.Singleton);

            CoreDependecyInjection.Configure(solrExpressServiceProvider, solrExpressBuilder.Options);

            builder.Invoke(solrExpressBuilder);

            return container;
        }
    }
}
