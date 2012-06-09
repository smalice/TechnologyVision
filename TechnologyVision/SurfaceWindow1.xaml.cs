using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Surface;
using Microsoft.Surface.Presentation.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TechnologyVision
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        AppManager manager = new AppManager();
        bool _isSendingAfterDrop = false;

        private ObservableCollection<YearItem> _targetItems;

        public ObservableCollection<YearItem> TargetItems
        {
            get { return _targetItems ?? (_targetItems = new ObservableCollection<YearItem>()); }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            manager.Init();
            myGrid.DataContext = manager;
            //technoControl.DataContext = manager.TechnoLines[0].YearItems[0];
            AddTechnoItems();
            //TODO:1
            scatterViewTarget.ItemsSource = TargetItems;
        }

        void AddTechnoItems()
        {
            foreach (TechnoLine tl in manager.TechnoLines)
            {
                foreach (YearItem yt in tl.YearItems)
                {
                    TechnoControl newTC= new TechnoControl() { DataContext=yt,  HorizontalAlignment= HorizontalAlignment.Center,  VerticalAlignment= VerticalAlignment.Center };
                    Grid.SetRow(newTC,1);
                    Grid.SetColumn(newTC,tl.ID-1);
                    mainGrid.Children.Add(newTC);
                    yt.PropertyChanged += yt_PropertyChanged;
                }
            }
        }

        void yt_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var yi = sender as YearItem;
            if (e.PropertyName == "ShowText" && yi.ShowText && !TargetItems.Contains(yi))
            {
                TargetItems.Add(sender as YearItem);
                var item = scatterViewTarget.Items[scatterViewTarget.Items.Count - 1];
                _isSendingAfterDrop = true;
                scatterViewTarget.Activate(item);
            }
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void SurfaceButton_Click(object sender, RoutedEventArgs e)
        {
            var content = (sender as SurfaceButton).DataContext as YearItem;
            content.ShowText = false;
            TargetItems.Remove(content);
        }

        private void scatterViewTarget_ContainerActivated(object sender, RoutedEventArgs e)
        {
            if (!_isSendingAfterDrop) return;
            var scatterViewItem = e.OriginalSource as ScatterViewItem;
            var yi = scatterViewItem.DataContext as YearItem;
            scatterViewItem.Center = yi.Position;
            scatterViewItem.Orientation = yi.Orientation;
            scatterViewItem.MaxHeight = 600;
            scatterViewItem.MaxWidth = 800;
            scatterViewItem.Name = "scatterItem";
            scatterViewItem.Background = null;
            scatterViewItem.ShowsActivationEffects = true;

            _isSendingAfterDrop = false;
            
        }
    }
}