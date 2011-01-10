﻿using TradeRobotics.DataProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TradeRobotics.Model;
using System.Collections.Generic;

namespace TradeRobotics.Test
{
    
    
    /// <summary>
    ///This is a test class for HistoryDataProviderTest and is intended
    ///to contain all HistoryDataProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HistoryDataProviderTest
    {
            private TestContext testContextInstance;

            /// <summary>
            ///Gets or sets the test context which provides
            ///information about and functionality for the current test run.
            ///</summary>
            public TestContext TestContext
            {
                get
                {
                    return testContextInstance;
                }
                set
                {
                    testContextInstance = value;
                }
            }

            #region Additional test attributes
            // 
            //You can use the following additional attributes as you write your tests:
            //
            //Use ClassInitialize to run code before running the first test in the class
            //[ClassInitialize()]
            //public static void MyClassInitialize(TestContext testContext)
            //{
            //}
            //
            //Use ClassCleanup to run code after all tests in a class have run
            //[ClassCleanup()]
            //public static void MyClassCleanup()
            //{
            //}
            //
            //Use TestInitialize to run code before running each test
            [TestInitialize()]
            public void MyTestInitialize()
            {
                // Set data directory to test project data folder
                TradeRobotics.DataProviders.DataContext.DataDirectory = @"..\..\..\TradeRobotics.Test\Data\";
            }
            //
            //Use TestCleanup to run code after each test has run
            //[TestCleanup()]
            //public void MyTestCleanup()
            //{
            //}
            //
            #endregion


            /// <summary>
            ///A test for LoadBars
            ///</summary>
            [TestMethod()]
            public void LoadBarsTest()
            {
                try
                {
                    HistoryDataProvider target = new HistoryDataProvider(); // TODO: Initialize to an appropriate value

                    StockDataSeries dataSeries;
                    dataSeries = target.LoadBars(@"sber_m5.csv");
                    Assert.AreNotEqual(dataSeries, null);
                    Assert.AreNotEqual(dataSeries.Count, 0);
                    Assert.AreEqual(dataSeries.Symbol, "sber");
                    Assert.AreEqual(dataSeries.Period, 5);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }

            }

            /// <summary>
            ///A test for LoadBars
            ///</summary>
            [TestMethod()]
            public void LoadQuotesTest()
            {
                try
                {
                    HistoryDataProvider target = new HistoryDataProvider(); // TODO: Initialize to an appropriate value
                    string symbol = "sbrf-3.10"; // TODO: Initialize to an appropriate value

                    List<Quote> quotes;
                    quotes = target.LoadQuotes(symbol);
                    Assert.AreNotEqual(quotes, null);
                    Assert.AreNotEqual(quotes.Count, 0);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }

            }

            /// <summary>
            ///A test for LoadBars
            ///</summary>
            [TestMethod()]
            public void LoadQuotesByDateTest()
            {
                try
                {
                    HistoryDataProvider target = new HistoryDataProvider(); // TODO: Initialize to an appropriate value
                    string symbol = "sbrf-3.10"; // TODO: Initialize to an appropriate value
                    DateTime date = new DateTime(2010, 3, 4);

                    List<Quote> quotes;
                    quotes = target.LoadQuotes(symbol, date);
                    Assert.AreNotEqual(quotes, null);
                    Assert.AreNotEqual(quotes.Count, 0);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }

            }


            /// <summary>
            ///A test for LoadQuotesFromFile
            ///</summary>
            [TestMethod()]
            [DeploymentItem("TradeRobotics.DataProviders.History.dll")]
            public void LoadQuotesFromFileTest()
            {
                try
                {
                    HistoryDataProvider_Accessor target = new HistoryDataProvider_Accessor(); // TODO: Initialize to an appropriate value

                    string filePath = string.Concat(TradeRobotics.DataProviders.DataContext.DataDirectory, "sbrf-3.10_2010.03.04_quotes.csv");
                    var quotes = target.LoadQuotesFromFile(filePath);
                    Assert.AreNotEqual(quotes, null);
                    Assert.AreNotEqual(quotes.Count, 0);

                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }

            }

            /// <summary>
            ///A test for LoadDepth
            ///</summary>
            [TestMethod()]
            public void LoadDepthTest()
            {
                HistoryDataProvider target = new HistoryDataProvider(); // TODO: Initialize to an appropriate value
                target.DataSeries = new StockDataSeries();
                target.DataSeries.Symbol = "sbrf-3.10";
                DateTime date = new DateTime(2010,3,4); // TODO: Initialize to an appropriate value
                target.LoadDepth(date);
                Assert.IsTrue(target.DataSeries.Depth.Count > 0);
            }
    }
}
