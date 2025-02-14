using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_App
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _operators;
        public ObservableCollection<string> Operators
        {
            get { return _operators; }
            set
            {
                _operators = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Operators"));
            }
        }

        private ObservableCollection<History> _histories = new ObservableCollection<History>();
        public ObservableCollection<History> LastHistory
        {
            get { return _histories; }
            set
            {
                _histories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastHistory"));
            }
        }

        public MainViewModel()
        {
            Operators = new ObservableCollection<string>();
            Logic logic = new Logic();
            List<string> ops = logic.GetOperatorsList();
            foreach (string op in ops)
                Operators.Add(op);

            
        }

        public void LoadHistoryData(string op)
        {
            Logic logic = new Logic();
            DataTable data = logic.GetLast3Records(op);

            if (data!= null && data.Rows.Count > 0)
            {
                LastHistory.Clear();  // Clear existing data

                foreach (DataRow row in data.Rows)
                {
                    History history = new History
                    {
                        Date = Convert.ToDateTime(row["CreateDate"]),
                        FieldA = row["Field1"].ToString(),
                        Operator = row["Operator"].ToString(),
                        FieldB = row["Field2"].ToString(),
                        Result = row["Result"].ToString(),
                    };

                    LastHistory.Add(history);
                }
            }
        }
    }
}
