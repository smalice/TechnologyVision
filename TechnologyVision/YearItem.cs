using System.ComponentModel;
using System.Windows;

namespace TechnologyVision
{
    public class YearItem : INotifyPropertyChanged
    {
        public Point Position;
        public double Orientation;

        string year;

        public string Year
        {
            set { year = value; OnPropertyChanged("Year"); }
            get { return year; }
        }

        string name;

        public string Name
        {
            set { name = value; OnPropertyChanged("Name"); }
            get { return name; }
        }

        string text;

        public string Text
        {
            set { text = value; OnPropertyChanged("Text"); }
            get { return text; }
        }

        bool isVisible = false;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        bool showText = false;

        public bool ShowText
        {
            get { return showText; }
            set
            {
                showText = value;
                OnPropertyChanged("ShowText");
            }
        }

        internal void UpdateCurrentYear(string YearLabel)
        {
            if (Year == YearLabel)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
