using System;
using System.Collections.Generic;
using System.Text;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        private decimal _source;

        private decimal _target;

        public ConvertViewModel()
        {
            Source = 5.00M;
            Target = 10.00M;
        }

        public decimal Source
        {
            get => _source;

            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        public decimal Target
        {
            get => _target;

            set
            {
                _target = value;
                OnPropertyChanged(nameof(Target));
            }
        }
    }
}
