using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application.Implementations
{
    public class PaymentMethod
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public static List<PaymentMethod> GetMethods() => new List<PaymentMethod>()
        {
                new PaymentMethod{Id = 1, Name="پرداخت آنلاین", Description = "در این صورت شما به درگاه پرداخت اینترنتی هدایت شده و در لحظه پرداخت وجه را انجام خواهید داد"},
                new PaymentMethod{Id = 2, Name="پرداخت نقدی", Description = "در این صورت، ابتدا سفارش ثبت شده و سپس وجه به صورت نقدی در زمان تحویل کالا، دریافت خواهد شد"}
        };

        public static PaymentMethod Get(long id) => GetMethods().FirstOrDefault(m => m.Id == id);
    }
}
