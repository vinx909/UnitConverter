using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UnitConverter.ApplicationCore;
using UnitConverter.ApplicationCore.Interfaces;

namespace UnitConverter.WPF
{
    class ViewModel : INotifyPropertyChanged
    {
        private string convertTextString = "{0} {1}  =  {2} {3}";
        private readonly IConverter converter;
        private ICommand convertCommand;
        private ConverOption selectedConvertOption;
        private string conversionText;

        public List<ConverOption> ConverOptions { get; set; }
        public ConverOption SelectedConvertOption { get => selectedConvertOption; set { if (selectedConvertOption != value) { selectedConvertOption = value; RaiseProppertyChanged(); } } }
        public decimal Value { get; set; }
        public string ConversionText { get => conversionText; set { if (conversionText != value) { conversionText = value; RaiseProppertyChanged(); } } }

        public ICommand ConvertCommand { get => convertCommand; }
        public event PropertyChangedEventHandler PropertyChanged;

        internal ViewModel() : this(null) { }
        public ViewModel(IConverter converter)
        {
            convertCommand = new CommandHandler(() => Convert(), () => { return SelectedConvertOption != null; });

            ConverOptions = new()
            {
                new() { Name = "meter naar centimeter", StartUnit = Unit.meter, EndUnit = Unit.centimeter },
                new() { Name = "centimeter naar meter", StartUnit = Unit.centimeter, EndUnit = Unit.meter },
                new() { Name = "centimeter naar millimeter", StartUnit = Unit.centimeter, EndUnit = Unit.millimeter },
                new() { Name = "millimeter naar centimeter", StartUnit = Unit.millimeter, EndUnit = Unit.centimeter },
                new() { Name = "meter naar inch", StartUnit = Unit.meter, EndUnit = Unit.inch },
                new() { Name = "inch naar meter", StartUnit = Unit.inch, EndUnit = Unit.meter },
            };
            this.converter = converter;
        }

        public void Convert()
        {
            ConversionText = string.Format(convertTextString, Value, SelectedConvertOption.StartUnit, converter.Convert(Value, SelectedConvertOption.StartUnit, SelectedConvertOption.EndUnit), SelectedConvertOption.EndUnit);
        }

        public class ConverOption
        {
            public string Name { get; set; }
            public Unit StartUnit { get; set; }
            public Unit EndUnit { get; set; }
        }
        protected virtual void RaiseProppertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class CommandHandler : ICommand
    {
        private Action action;
        private Func<bool> canDoAction;

        public CommandHandler(Action p1, Func<bool> p2)
        {
            this.action = p1;
            this.canDoAction = p2;
        }

        //public event EventHandler CanExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canDoAction();
        }

        public void Execute(object parameter)
        {
            action.Invoke();
        }
    }
}
