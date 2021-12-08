using System;
using System.Collections.Generic;
using BankingExplorationApp.Models;

namespace BankingExplorationApp.ViewModels
{
    public class topMoversModel
    {
      
        private List<Stocks> activityCollection = new List<Stocks>();

        public topMoversModel()
        {
            //Activity
            activityCollection.Add(new Stocks("Amazon.png", "Today, 08:00am", "Amazon Drew", "$200.00"));
            activityCollection.Add(new Stocks("Spotify.png", "31 Jan, 11:00am", "Spotify Premiun", "-$8.90"));
            activityCollection.Add(new Stocks("McDonals.pmg", "29 Jan, 03:10am", "McDonals's", "-$62.80"));
            activityCollection.Add(new Stocks("Amazon.png", "Today, 08:00am", "Amazon Drew", "$200.00"));
            activityCollection.Add(new Stocks("Amazon.png", "Today, 08:00am", "Amazon Drew", "$200.00"));
            activityCollection.Add(new Stocks("Amazon.png", "Today, 08:00am", "Amazon Drew", "$200.00"));
            activityCollection.Add(new Stocks("Amazon.png", "Today, 08:00am", "Amazon Drew", "$200.00"));
        }

        public List<Stocks> ActivitiesCollection
        {
            get { return activityCollection; }
            set { activityCollection = value; }
        }
    }
}
