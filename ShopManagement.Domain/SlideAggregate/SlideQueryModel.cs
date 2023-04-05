using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.SlideAggregate
{
    public class SlideQueryModel : CreateSlide
    {
    }

    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
