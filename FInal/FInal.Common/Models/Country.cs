using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FInal.Common.Models
{
    public class Currency
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Language
    {
        public string Iso6391 { get; set; }

        public string Iso6392 { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Translations
    {
        public string De { get; set; }

        public string Es { get; set; }

        public string Fr { get; set; }

        public string Ja { get; set; }


        public string It { get; set; }

        public string Br { get; set; }

        public string Pt { get; set; }

        public string Nl { get; set; }

        public string Hr { get; set; }

        public string Fa { get; set; }
    }

    public class RegionalBloc
    {
        public string Acronym { get; set; }

        public string Name { get; set; }

        public List<string> OtherAcronyms { get; set; }
        public List<string> OtherNames { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Country
    {
        public string Name { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public List<string> CallingCodes { get; set; }
        public string Capital { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public int Population { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        public List<double> Latlng { get; set; }
        public string Demonym { get; set; }
        public double? Area { get; set; }
        public double? Gini { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Borders { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public Dictionary<string, string> Translations { get; set; }
        public string Flag { get; set; }
        public List<object> RegionalBlocs { get; set; }
        public string Cioc { get; set; }
    }
}
