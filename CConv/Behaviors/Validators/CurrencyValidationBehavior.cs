using Xamarin.Forms;

namespace CConv.Behaviors.Validators
{
    internal class CurrencyValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnBindableTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnBindableTextChanged;
        }

        private static void OnBindableTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is Entry entry))
                return;

            var isDecimal = decimal.TryParse(e.NewTextValue, out var number);
            var isPositive = number > 0;

            entry.BackgroundColor = isDecimal && isPositive ? Color.PaleGreen : Color.Crimson;
        }
    }
}
