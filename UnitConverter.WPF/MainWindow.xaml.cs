using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleDependencyProvider;
using UnitConverter.ApplicationCore.Interfaces;
using UnitConverter.ApplicationCore.Services;
using UnitConverter.Infrastructure.Data;

namespace UnitConverter.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SetupDependencyProvider();

            InitializeComponent();
            DataContext = new ViewModel(DependencyProvider.Get<IConverter>());
        }

        private static void SetupDependencyProvider()
        {
            const string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MeterConvertor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DependencyProvider.newProvide<MeterConvertorDBContext>(() => { return new(connectionstring); });
            DependencyProvider.newProvide<IMeterConvertorRepository>(() => { return new MeterConvertorRepository(DependencyProvider.Get<MeterConvertorDBContext>()); });
            DependencyProvider.newProvide<IConverter>(() => { return new Converter(DependencyProvider.Get<ILogger>()); });
            DependencyProvider.newProvide<ILogger>(() => { return new DatabaseLogger(DependencyProvider.Get<IMeterConvertorRepository>()); });
        }
    }
}
