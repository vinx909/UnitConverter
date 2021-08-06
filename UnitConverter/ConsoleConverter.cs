using System;
using UnitConverter.ApplicationCore;
using UnitConverter.ApplicationCore.Interfaces;
using UnitConverter.ApplicationCore.Services;
using SimpleDependencyProvider;
using System.Globalization;

namespace UnitConverter.Console
{
    class ConsoleConverter
    {
        private enum State
        {
            mainMenu,
            convert,
            close
        }

        private const char option1 = '1';
        private const char option2 = '2';
        private const char option3 = '3';
        private const char option4 = '4';
        private const char option5 = '5';
        private const char option6 = '6';
        private const char option7 = '7';

        private const string exceptionMessage = "something went wrong and you've been send back to the main menu";
        private const string mainOptions = "{0}. meter naar centimeter\r\n{1}. centimeter naar meter\r\n{2}. centimeter naar millimeter\r\n{3}. millimeter naar centimeter\r\n{4}. meter naar inch\r\n{5}. inch naar meter\r\n{6}. afsluiten";
        private const string convertMenu = "{0}. opnieuw berekenen\r\n{1}. terug\r\n{2}. afsluiten";
        private readonly IConverter converter;
        private readonly ILogger logger;

        private State currentState;
        private Unit startUnit;
        private Unit endUnit;

        public ConsoleConverter(IConverter converter, ILogger logger)
        {
            this.converter = converter;
            this.logger = logger;
        }

        static void Main(string[] args)
        {
            DependencyProvider.newProvide<IConverter>(() => { return new Converter(DependencyProvider.Get<ILogger>()); });
            DependencyProvider.newProvide<ILogger>(() => { return new TextLogger(); });
            new ConsoleConverter(DependencyProvider.Get<IConverter>(), DependencyProvider.Get<ILogger>()).Run();
        }

        private void Run()
        {
            currentState = State.mainMenu;
            while (currentState!=State.close)
            {
                try
                {
                    switch (currentState)
                    {
                        case State.mainMenu:
                            RunMainMenu();
                            break;
                        case State.convert:
                            RunConvert();
                            break;
                    }
                }
                catch (Exception e)
                {
                    logger.LogException(e);
                    SwitchToMainMenu(true);
                }
            }
        }

        private void RunMainMenu()
        {
            System.Console.WriteLine(string.Format(mainOptions, option1, option2, option3, option4, option5, option6, option7));
            while (true)
            {
                switch (System.Console.ReadKey().KeyChar)
                {
                    case option1:
                        SwitchToConvert(Unit.meter, Unit.centimeter);
                        return;
                    case option2:
                        SwitchToConvert(Unit.centimeter, Unit.meter);
                        return;
                    case option3:
                        SwitchToConvert(Unit.centimeter, Unit.millimeter);
                        return;
                    case option4:
                        SwitchToConvert(Unit.millimeter, Unit.centimeter);
                        return;
                    case option5:
                        SwitchToConvert(Unit.meter, Unit.inch);
                        return;
                    case option6:
                        SwitchToConvert(Unit.inch, Unit.meter);
                        return;
                    case option7:
                        CloseConsole();
                        return;
                } 
            }
        }
        private void RunConvert()
        {
            System.Console.WriteLine(converter.Convert(ParseTry(System.Console.ReadLine()), startUnit, endUnit));
            System.Console.WriteLine(string.Format(convertMenu, option1, option2, option3));
            while (true)
            {
                switch (System.Console.ReadKey().KeyChar)
                {
                    case option1:
                        System.Console.WriteLine();
                        return;
                    case option2:
                        SwitchToMainMenu();
                        return;
                    case option3:
                        CloseConsole();
                        return;
                }
            }
        }

        private void EmptryScreen()
        {
            System.Console.Clear();
        }
        private Decimal ParseTry(string toParse)
        {
            /*
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo provider = new CultureInfo("en-GB");
            Decimal.Parse(toParse, style, provider);
            */
            Decimal returnValue = 0;
            Decimal.TryParse(toParse, out returnValue);
            return returnValue;
        }

        private void SwitchToMainMenu(bool exceptionThrown = false)
        {
            EmptryScreen();
            if (exceptionThrown)
            {
                System.Console.WriteLine(exceptionMessage);
            }
            currentState = State.mainMenu;
        }
        private void SwitchToConvert(Unit startUnit, Unit endUnit)
        {
            EmptryScreen();
            this.startUnit = startUnit;
            this.endUnit = endUnit;
            currentState = State.convert;
        }
        private void CloseConsole()
        {
            currentState = State.close;
        }
    }
}
