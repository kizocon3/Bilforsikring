using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilForsikring.Models.ViewModel.Forsiking
{
    public class KunderViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [Display(Name = "Bilens registreringsnummer")]
        public string BilensRegistreringsNr { get; set; }

        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [Display(Name = "Din bonus")]
        public string DinBonus { get; set; }

        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [Display(Name = "Fødselsnummer")]
        public string FødselsNumber { get; set; }

        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [Display(Name = "Fornavn")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [Display(Name = "Etternavn")]
        public string Etternavn { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "Felten kan ikke være tom")]
        [EmailAddress(ErrorMessage = "* Skriv in en gyldig epostadresse")]      
        public string Epost { get; set; }

    }
}