using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace TechnologyVision
{
    class AppManager : INotifyPropertyChanged
    {
        private int _year;

        public int Year 
        { 
            get { return _year; }
            set 
            {
                _year = value;
                OnPropertyChanged("Year");
                switch (_year)
                {
                    case 1: YearLabel = "2012"; break;
                    case 2: YearLabel = "2013"; break;
                    case 3: YearLabel = "2014"; break;
                    case 4: YearLabel = "2015"; break;
                    case 5: YearLabel = "2016"; break;
                    case 6: YearLabel = "2017"; break;
                    case 7: YearLabel = "2018"; break;
                    case 8: YearLabel = "2019"; break;
                    case 9: YearLabel = "2020"; break;
                    case 10: YearLabel = "2030"; break;
                    case 11: YearLabel = "2040"; break;
                }
                UpdateCurrentYear();
            } 
        }

        private string _yearLabel;

        public string YearLabel
        {
            get { return _yearLabel; }
            set
            {
                _yearLabel = value;
                OnPropertyChanged("YearLabel");
            }
        }

        public List<TechnoLine> TechnoLines;

        public void Init()
        {
            _year = 1;
            _yearLabel = "2012";
            Load();
            UpdateCurrentYear();
        }

        private void Load()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<TechnoLine>));
            TextReader textReader = new StreamReader("Resources\\Content.xml");
            TechnoLines = (List<TechnoLine>)deserializer.Deserialize(textReader);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateCurrentYear()
        {
            foreach (TechnoLine tl in TechnoLines)
            {
                tl.UpdateCurrentYear(YearLabel);
            }
        }
    }
}
