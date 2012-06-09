using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Surface.Presentation.Input;

namespace TechnologyVision
{
	/// <summary>
	/// Interaction logic for TechnoControl.xaml
	/// </summary>
	public partial class TechnoControl : UserControl
	{
        private readonly MatrixTransform tableTransform;

        public bool Activated { get; set; }

		public TechnoControl()
		{
			this.InitializeComponent();
            tableTransform = new MatrixTransform(Matrix.Identity);
            this.RenderTransform = tableTransform;
            this.RenderTransformOrigin = new Point(0.5, 0.5);

		    Activated = false;

            LayoutRoot.IsVisibleChanged += LayoutRootIsVisibleChanged;
		}

        private void LayoutRootIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	    {
	        if (e.OldValue == null || e.NewValue == null) return;

	        if (!(e.OldValue is bool) || !(e.NewValue is bool)) return;
	        
            if( !(bool)e.OldValue && (bool)e.NewValue)
	        {
	            var animation = (Storyboard)FindResource("ShowEllipseControl");
	            animation.Begin();
	        }
	        else if ((bool)e.OldValue && !(bool)e.NewValue)
	        {
                var animation = (Storyboard)FindResource("HideEllipseControl");
                animation.Stop();
	        }
	    }

	    private void LayoutRoot_TouchDown(object sender, InputEventArgs e)
        {
            var yi = DataContext as YearItem;
            if (yi == null)
                return;
            yi.Orientation = e.Device.GetOrientation(null) + 90;
            yi.Position = e.Device.GetPosition(null);
            yi.Position.X -= 100;
            yi.Position.Y -= 50;
            (DataContext as YearItem).ShowText = true;
        }

        private void LayoutRoot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var yi = DataContext as YearItem;
            if (yi == null)
                return;
            yi.Orientation = e.Device.GetOrientation(null);
            yi.Position = e.Device.GetPosition(null);
            yi.Position.X -= 100;
            yi.Position.Y -= 50;
            (DataContext as YearItem).ShowText = true;
        }
	}
}