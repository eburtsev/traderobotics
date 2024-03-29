﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visifire.Charts;
using TradeRobotics.Model;
using System.IO;
using System.Reflection;
using TradeRobotics.TradeLibrary;
using TradeRobotics.TradeAdapters;

namespace TradeRobotics.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TradeRobotics.DataProviders.TestDataProvider DataProvider;
        TestTradeAdapter TradeAdapter;
        IRobot Robot;

        public MainWindow()
        {
            InitializeComponent();
            TradeRobotics.DataProviders.DataContext.Init();
            LoadRobots();
            LoadDataSeries();
        }

        #region Fill controls
        private void LoadRobots()
        {
            const string robotsDirectory = @".\Robots\";

            // Get file names from data directory
            string[] robotFiles = Directory.GetFiles(robotsDirectory, "*.dll", SearchOption.TopDirectoryOnly);
            // Load list of all robot classes
            List<Type> robotTypes = new List<Type>();
            foreach (string robotFile in robotFiles)
            {
                Assembly assembly =  System.Reflection.Assembly.LoadFrom(robotFile);
                Type[] types = assembly.GetExportedTypes();
                var newRobotTypes = types.Where<Type>(type => type.GetInterface(typeof(IRobot).Name, false) != null);
                robotTypes.AddRange(newRobotTypes);
            }

            RobotsComboBox.ItemsSource = robotTypes;

        }

        private void LoadDataSeries()
        {
            // Get file names from data directory
            string[] dataFiles = Directory.GetFiles(TradeRobotics.DataProviders.DataContext.DataDirectory, "*.csv", SearchOption.TopDirectoryOnly);
            List<string> fileNames = new List<string>();
            foreach (string file in dataFiles)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                fileNames.Add(fileName);
            }
            // Add to list
            DataSeriesList.ItemsSource = fileNames;
        }

        #endregion

        
        private void DataSeriesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Display clicked data series in the chart
            if (DataProvider != null)
            {
                DataProvider.Tick -= this.OnTestTick;
            }
            DataProvider = new DataProviders.TestDataProvider();
            var barCollection = DataProvider.LoadFromFile(DataSeriesList.SelectedItem.ToString()+".csv");
            PriceChart.LoadPriceChart(barCollection);
            DataProvider.Tick += OnTestTick;
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataProvider == null
                || RobotsComboBox.SelectedItem == null)
                return;
            
            // Create robot
            Type robotType = RobotsComboBox.SelectedItem as Type;
            Robot = Activator.CreateInstance(robotType) as IRobot;
            Robot.TradeAdapter = TradeAdapter = new TestTradeAdapter();
            
            //DataProvider.Tick += OnTestTick;
            TestProgress.Value = 1;            
            DataProvider.BeginTest(Robot);
        }
        private void OnTestTick(object sender, TickEventArgs e)
        {
            TestProgress.Value = (Convert.ToDouble(e.LastBarIndex) / Convert.ToDouble(DataProvider.DataSeries.Count)) * 100.0;
            // If test ends
            if (e.LastBarIndex >= e.DataSeries.Count - 1)
            {
                this.OrdersControl.Orders = TradeAdapter.Orders;
                //this.OrdersControl.OrdersDataGrid.DataContext = TradeAdapter.Orders;
                
            }
        }
    }
}
