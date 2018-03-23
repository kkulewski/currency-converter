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
            set => SetProperty(ref _source, value);
        }

        public decimal Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }
    }
}
