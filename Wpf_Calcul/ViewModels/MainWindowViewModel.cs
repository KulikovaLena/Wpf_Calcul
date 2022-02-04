using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf_Calcul.Models;

namespace Wpf_Calcul.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string calcText="0";
        public string CalcText
        {
            get => calcText;
            set
            {
                calcText = value;
                OnPropertyChanged();
            }
        }

        #region =
        public ICommand Number1Command { get; }
        private void OnNumber1CommandExecute(object buttonValue)
        {
            try
            {
                CalcText = Insertion.Calc(CalcText).ToString().Replace(',', '.');
                if (CalcText == "∞" || CalcText == "-∞")
                    CalcText = "error";
            }
            catch
            {
                CalcText = "error";
            }
            
        }
        private bool CanNumber1ComandCanExecuted(object p)
        {
            string sEnd = CalcText.Substring(CalcText.Length - 1);
            if (sEnd == "." || sEnd == "+" || sEnd == "-" || sEnd == "*" || sEnd == "/")
                return false;
            else
                return true;
        }
        #endregion

        #region 0 1 2 3 4 5 6 7 8 9
        public ICommand Number2Command { get; }
        private void OnNumber2CommandExecute(object buttonValue)
        {
            
            if (CalcText == "0")
                CalcText = buttonValue.ToString();
            else
                CalcText += buttonValue.ToString();
        }
        private bool CanNumber2ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region + - * /
        public ICommand Number3Command { get; }
        private void OnNumber3CommandExecute(object buttonValue)
        {
                CalcText += buttonValue.ToString();
        }
        private bool CanNumber3ComandCanExecuted(object p)
        {
            if (CalcText == "error" || CalcText.Substring(CalcText.Length - 1) == "+" || CalcText.Substring(CalcText.Length - 1) == "-" || CalcText.Substring(CalcText.Length - 1) == "*" || CalcText.Substring(CalcText.Length - 1) == "/" || CalcText.Substring(CalcText.Length - 1) == ".")
                return false;
            else
                return true;
        }
        #endregion

        #region +/-
        public ICommand Number4Command { get; }
        private void OnNumber4CommandExecute(object buttonValue)
        {
            string firstSym = CalcText.Substring(0,1);
            if (firstSym == "0")
                CalcText = "0";
            if (firstSym != "-" && firstSym != "0")
                CalcText = "-" + CalcText;
            if (firstSym == "-" && firstSym != "0")
                CalcText = CalcText.Remove(0, 1);
            
                
        }
        private bool CanNumber4ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region CE
        public ICommand Number0Command { get; }
        private void OnNumber0CommandExecute(object buttonValue)
        {
            CalcText = "0";
        }
        private bool CanNumber0ComandCanExecuted(object p)
        {
            return true;
        }
        #endregion

        #region ←
        public ICommand Number5Command { get; }
        private void OnNumber5CommandExecute(object buttonValue)
        {
            string firstSym = CalcText.Substring(0, 1);
            int ind = CalcText.Length - 1;
            if (firstSym == "0" || CalcText.Length == 1)
                CalcText = "0";
            else
                CalcText = CalcText.Remove(ind);
        }
        private bool CanNumber5ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region .
        public ICommand Number6Command { get; }
        private void OnNumber6CommandExecute(object buttonValue)
        {
            CalcText += buttonValue.ToString();
        }
        private bool CanNumber6ComandCanExecuted(object p)
        {
            string str = "";
            for (int i=0; i<CalcText.Length; i++)
                if (!char.IsDigit(CalcText[i]))
                {
                    str += CalcText[i];
                    
                }
            if (str.Length!=0 && str.Substring(str.Length - 1) == ".")
                return false;

            if (CalcText == "error" || CalcText.Substring(CalcText.Length - 1) == "+" || CalcText.Substring(CalcText.Length - 1) == "-" || CalcText.Substring(CalcText.Length - 1) == "*" || CalcText.Substring(CalcText.Length - 1) == "/" || CalcText.Substring(CalcText.Length - 1) == ".")
                return false;
            else
                return true;
        }
        #endregion

        #region 1/x
        public ICommand Number7Command { get; }
        private void OnNumber7CommandExecute(object buttonValue)
        {
            double x = Insertion.Calc(CalcText);
            CalcText = Convert.ToString(1/x).Replace(',', '.');
        }
        private bool CanNumber7ComandCanExecuted(object p)
        {
            if (CalcText=="0" || CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region x*x
        public ICommand Number8Command { get; }
        private void OnNumber8CommandExecute(object buttonValue)
        {
            double x = Insertion.Calc(CalcText);
            CalcText = Convert.ToString(x*x).Replace(',', '.');
        }
        private bool CanNumber8ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region √x
        public ICommand Number9Command { get; }
        private void OnNumber9CommandExecute(object buttonValue)
        {
            if (Insertion.Calc(CalcText) < 0)
                CalcText = "error";
            else
                CalcText = Insertion.Square(Insertion.Calc(CalcText)).Replace(',', '.');
        }
        private bool CanNumber9ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        #region %
        public ICommand Number10Command { get; }
        private void OnNumber10CommandExecute(object buttonValue)
        {
            double x = Insertion.Calc(CalcText);
            CalcText = Convert.ToString(x / 100).Replace(',', '.');
        }
        private bool CanNumber10ComandCanExecuted(object p)
        {
            if (CalcText == "error")
                return false;
            else
                return true;
        }
        #endregion

        public MainWindowViewModel()
        {
            Number2Command = new RelayCommand(OnNumber2CommandExecute, CanNumber2ComandCanExecuted);
            Number1Command = new RelayCommand(OnNumber1CommandExecute, CanNumber1ComandCanExecuted);
            Number3Command = new RelayCommand(OnNumber3CommandExecute, CanNumber3ComandCanExecuted);
            Number4Command = new RelayCommand(OnNumber4CommandExecute, CanNumber4ComandCanExecuted);
            Number0Command = new RelayCommand(OnNumber0CommandExecute, CanNumber0ComandCanExecuted);
            Number5Command = new RelayCommand(OnNumber5CommandExecute, CanNumber5ComandCanExecuted);
            Number6Command = new RelayCommand(OnNumber6CommandExecute, CanNumber6ComandCanExecuted);
            Number7Command = new RelayCommand(OnNumber7CommandExecute, CanNumber7ComandCanExecuted);
            Number8Command = new RelayCommand(OnNumber8CommandExecute, CanNumber8ComandCanExecuted);
            Number9Command = new RelayCommand(OnNumber9CommandExecute, CanNumber9ComandCanExecuted);
            Number10Command = new RelayCommand(OnNumber10CommandExecute, CanNumber10ComandCanExecuted);
        }
        
    }
}
