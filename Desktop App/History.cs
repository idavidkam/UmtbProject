using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop_App
{
    class History : INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if(_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private string _fieldA;

        public string FieldA
        {
            get { return _fieldA; }
            set
            {
                if (_fieldA != value)
                {
                    _fieldA = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set
            {
                if (_operator != value)
                {
                    _operator = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _fieldB;
        public string FieldB
        {
            get { return _fieldB; }
            set
            {
                if (_fieldB != value)
                {
                    _fieldB = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
