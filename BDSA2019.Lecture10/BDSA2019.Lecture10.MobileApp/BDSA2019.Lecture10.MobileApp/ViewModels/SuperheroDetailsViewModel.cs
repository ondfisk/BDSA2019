using System;
using BDSA2019.Lecture10.MobileApp.Models;

namespace BDSA2019.Lecture10.MobileApp.ViewModels
{
    public class SuperheroDetailsViewModel : BaseViewModel
    {
        public SuperheroDetailsDTO Superhero { get; set; }
        public SuperheroDetailsViewModel(SuperheroListDTO item = null)
        {
            //Title = item?.Text;
            //Superhero = item;
        }
    }
}
