using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace When_Can_I_Legally_Buy_Alcohol
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, int> _countries = new Dictionary<string, int>();

        private readonly List<string> _keys = new List<string>();

        public MainPage()
        {
            InitializeComponent();

            _countries.Add("USA", 21);
            _countries.Add("Canada", 18);
            _countries.Add("Mexico", 18);
            _countries.Add("Cuba", 16);

            _keys = _countries.Keys.ToList();

            Country.ItemsSource = _keys;
        }

        private void CalculateYears(object sender, System.EventArgs e)
        {
            int legalAge = GetCountryYears();
            // First sees if country was selected
            DateTime birthday = Age.Date;
            if (legalAge != 0)
            {
                // Gets today's date without any time
                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                // Gets your age by subtracting today's year from your birth year
                int age = today.Year - birthday.Year;

                Console.WriteLine(age);

                // Checks to see if you have even been born, makes age negative to indicate
                if (today.DayOfYear < birthday.DayOfYear)
                {
                    age = -1;
                }

                // Adds the legal age years to your birthday
                DateTime dateOfLegalAge = birthday.AddYears(legalAge);

                // If the date of the legal age is before today, add one year
                if (dateOfLegalAge < today)
                {
                    dateOfLegalAge = dateOfLegalAge.AddYears(1);
                }

                // Gets the amount of days left before you turn of age in a TimeSpan (can be negative)
                TimeSpan timeUntilLegalAge = dateOfLegalAge - today;

                // gets the days from the TimeSpan
                int days = timeUntilLegalAge.Days;


                if (age >= legalAge)
                {
                    CalculatedTime.Text = $"You are {age}. You became of legal age to drink on {dateOfLegalAge.ToString("MM/dd/yyyy")}.";
                } 
                else if (age >= 0)
                {
                    CalculatedTime.Text = $"You are less than 1 year old. You will become of age to drink on {dateOfLegalAge.ToString("MM/dd/yyyy")} in {days} days.";
                }
                else
                {
                    CalculatedTime.Text = $"You have not been born yet. If you are born on {birthday.ToString("MM/dd/yyyy")}, you will come of age to drink on {dateOfLegalAge.ToString("MM/dd/yyyy")}, {days} days after you are born.";
                }
             }
            
            
        }

        private int GetCountryYears()
        {
            CalculatedTime.Text = "";
            LegalAge.Text = "";

            string country = (String) Country.SelectedItem;

            int years = 0;
            if (country != null)
            {
                // Gets country name if selected country is not null
                CalculatedTime.TextColor = Color.CadetBlue;

                foreach(string key in _keys)
                {
                    if (country == key)
                    {
                        years = _countries[key];
                        LegalAge.Text = $"The legal age in {key} is {years}.";
                        break;
                    }
                }
            } 
            else
            {
                // If null, asks for a country.
                CalculatedTime.Text = "Please select a country.";
                CalculatedTime.TextColor = Color.Red;
            }
            return years;
        }
    }
}
