using OXamarin.Models;
using Prism.Behaviors;
using Prism.Commands;
using Xamarin.Forms;

namespace OXamarin.Behaviors
{
    public class ItemSelectedToCommandBehaviorDrink : ItemSelectedToCommandBaseBehavior
    {
        public new static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(DelegateCommand<Drink>), typeof(EventToCommandBehavior));

        public new DelegateCommand<Drink> Command
        {
            get { return (DelegateCommand<Drink>)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //選択された飲料名を保持
            var drink = new Drink
            {
                DrinkName = e.SelectedItem.ToString()
            };

            if (Command.CanExecute(drink))
            {
                Command.Execute(drink);
            }
        }
    }
}