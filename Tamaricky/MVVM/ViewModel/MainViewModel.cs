using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tamaricky.Core;

namespace Tamaricky.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            ImageSource = "Images/dummy.png";
            IsSleeping = false;

            ButtonClickCommand = new RelayCommand(
                (o) => !IsSleeping || (string)o == "sleep",
                (o) =>
                {
                    switch ((string)o)
                    {
                        case "care":
                            Console.WriteLine((string)o);
                            break;

                        case "feed":
                            Console.WriteLine((string)o);
                            break;

                        case "play":
                            Console.WriteLine((string)o);
                            break;

                        case "sleep":
                            Console.WriteLine((string)o);
                            IsSleeping ^= true;
                            break;

                        default:
                            break;
                    }
                }
            );
        }

        public RelayCommand ButtonClickCommand { get; set; }

        public bool IsSleeping { get; set; }

        private string _imageSource;

        public string ImageSource {
            get => _imageSource;
            set {
                _imageSource = value;
                this.OnPropertyChanged();
            }
        }
    }
}
