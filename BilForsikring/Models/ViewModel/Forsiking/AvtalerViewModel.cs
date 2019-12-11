using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilForsikring.Models.ViewModel.Forsiking
{
    public class AvtalerViewModel
    {
        public Guid Id { get; set; }              
     
        [Display(Name = "Avtalenummer")]
        public string Avtalenummer { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }        

    }
}