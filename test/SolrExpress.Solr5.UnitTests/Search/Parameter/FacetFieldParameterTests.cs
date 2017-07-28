﻿using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetFieldParameterTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                Action<IFacetFieldParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                };
                var expected1 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""Id"": {
                      ""terms"": {
                        ""field"": ""id""
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1);
                };
                var expected2 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""Id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc);
                };
                var expected3 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""Id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1,
                        ""sort"": {
                          ""count"": ""asc""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                var expected4 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""Id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1,
                        ""sort"": {
                          ""count"": ""asc""
                        },
                        ""limit"": 10
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                var expected5 = JObject.Parse(@"
                {
                    ""facet"": {
                    ""Id"": {
                        ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1,
                        ""domain"": {
                            ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                            ]
                        },
                        ""sort"": {
                            ""count"": ""asc""
                        },
                        ""limit"": 10
                        }
                    }
                    }
                }");

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void FacetFieldParameterTheory001(Action<IFacetFieldParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            expressionBuilder.LoadDocument();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            var actual = string.Join("&", container);

            Assert.Equal(expectd.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetFieldParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetFieldParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
