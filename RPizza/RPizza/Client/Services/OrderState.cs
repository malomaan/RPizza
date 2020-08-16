using RPizza.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPizza.Client.Services
{
    //*Patron APP State
    public class OrderState //AppState
    {
        public Pizza ConfiguringPizza { get; private set; }
        public bool ShowingConfigureGialog { get; private set; }
        public Order Order { get; private set; } = new Order();

        public void ShowConfigurePizzaGialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowingConfigureGialog = true;
        }

        public void CancelConfigurePizzaGialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureGialog = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigureGialog = false;
        }
        public void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            Order = new Order();
        }


        public void ReplaceOrder(Order order)
        {
            Order = order;
        }
    }
}
