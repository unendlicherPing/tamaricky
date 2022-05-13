using System;
using System.Windows;
using System.Windows.Threading;
using WPFCore.MVVM.Core;
using Windows;

namespace Tamaricky.MVVM.Model
{
    class MainModel : ObservableObject
    {
        // Settings / Balancing
        private readonly TimeSpan UPDATE_STATS_INTERVAL = new TimeSpan(0, 0, 4); // hour, minutes, seconds
        private readonly TimeSpan UPDATE_AGE_INTERVAL   = new TimeSpan(0, 1, 0); // hour, minutes, seconds

        private readonly int PRIMARY_STAT_CHANGE        =                     3;
        private readonly int SECONDARY_STAT_CHANGE      =                     1;

        public MainModel()
        {
            // Setting Commands
            StatChangeButtonCommand = new DelegateCommand(
                (o) => { return !IsSleeping || (string)o == "sleep"; },
                (o) =>
                {
                    switch ((string)o)
                    {
                        case "care":
                            Health += Health <= 100 - PRIMARY_STAT_CHANGE? PRIMARY_STAT_CHANGE   : 0;
                            Fun    -= Fun    >=     SECONDARY_STAT_CHANGE? SECONDARY_STAT_CHANGE : 0;

                            break;

                        case "feed":
                            Hunger -= Hunger >=    PRIMARY_STAT_CHANGE? PRIMARY_STAT_CHANGE   : 0;
                            Health -= Health >=  SECONDARY_STAT_CHANGE? SECONDARY_STAT_CHANGE : 0;

                            break;

                        case "play":
                            Fun    += Fun    <= 100 - PRIMARY_STAT_CHANGE?  PRIMARY_STAT_CHANGE   : 0;
                            Energy -= Energy >=     SECONDARY_STAT_CHANGE?  SECONDARY_STAT_CHANGE : 0;
                            break;

                        case "sleep":
                            IsSleeping ^= true;
                            break;

                        default:  
                            break;
                    }

                    Console.WriteLine((string)o);
                }
            );

            // Setting Fields
            Health = 100;
            Hunger =   0;
            Fun    = 100;
            Energy = 100;
            Age    =   0;

            RickPicture = "/Images/dummy.png";
            updateRickPicture();

            IsSleeping = false;
            IsDead     = false;

            // Setting Events
            UpdateStatsEvent.Tick += (s, e) =>
            {
                Health --;
                Hunger ++;
                Fun    --;

                if (IsSleeping)
                {
                    if (Energy < 100)
                    {
                        Energy++;
                    }
                }
                else Energy--;

                 Console.WriteLine($"Health: {Health}");
                 Console.WriteLine($"Hunger: {Hunger}");
                 Console.WriteLine($"Fun: {Fun}");
                 Console.WriteLine($"Energy: {Energy}");
            };
            UpdateStatsEvent.Interval = UPDATE_STATS_INTERVAL;
            UpdateStatsEvent.Start();

            UpdateAgeEvent.Tick      += (s, e) =>
            {
                Age ++;

                if(Age >= 54)
                {
                    IsDead = true;
                }

                Console.WriteLine($"Age: {Age}");
            };
            UpdateAgeEvent.Interval   = UPDATE_AGE_INTERVAL;
            UpdateAgeEvent.Start();
        }

        // Subs
        private void updateRickPicture()
        {
            int age = 0;

            if (Age < 16)
                age = 0;
            else if (Age < 25)
                age = 16;
            else if (Age < 40)
                age = 25;
            else if (Age < 50)
                age = 40;
            else if (Age < 54)
                age = 50;
            else age = 54;

            RickPicture = $"/Images/age{age}.png";
        }

        // Fields
        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; RaisePropertyChanged(); if (value <= 0) IsDead = true; }
        }

        private int _hunger;
        public int Hunger
        {
            get { return _hunger; }
            set { _hunger = value; RaisePropertyChanged(); if (value >= 100) IsDead = true; }
        }

        private int _fun;
        public int Fun
        {
            get { return _fun; }
            set { _fun = value; RaisePropertyChanged(); if (value <= 0) IsDead = true; }
        }

        private int _energy;
        public int Energy
        {
            get { return _energy; }
            set { _energy = value; RaisePropertyChanged(); if (value <= 0) IsDead = true; }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { 
                _age = value;
                updateRickPicture();
                RaisePropertyChanged();
            }
        }

        private string _rickPicture;
        public string RickPicture
        {
            get { return _rickPicture; }
            set { _rickPicture = value; RaisePropertyChanged(); }
        }

        private bool _isSleeping;
        public bool IsSleeping
        {
            get { return _isSleeping; }
            set {
                _isSleeping = value;
                RaisePropertyChanged();
                RaisePropertyChanged("StatChangeButtonCommand");
            }
        }

        private bool _isDead;
        public bool IsDead
        {
            get { return _isDead; }
            set { 
                if (value)
                {
                    MessageBox.Show("You loose!");
                    Environment.Exit(0);
                }

                _isDead = value;
                RaisePropertyChanged(); 
            }
        }

        // Commands
        private DelegateCommand _statChangeButtonCommand;
        public DelegateCommand StatChangeButtonCommand
        {
            get { return _statChangeButtonCommand; }
            set { _statChangeButtonCommand = value; RaisePropertyChanged(); }
        }

        // Events
        public DispatcherTimer UpdateStatsEvent { get; set; } = new DispatcherTimer();
        public DispatcherTimer UpdateAgeEvent { get; set; } = new DispatcherTimer();
    }
}
