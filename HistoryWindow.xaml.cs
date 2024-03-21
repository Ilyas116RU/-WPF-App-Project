using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private Collection<string> computationHistory;

        public ObservableCollection<string> ComputationHistory { get; set; }

        public HistoryWindow(ObservableCollection<string> computationHistory)
        {
            InitializeComponent();
            ComputationHistory = computationHistory;
            DataContext = this; // Установка DataContext на экземпляр HistoryWindow
        }



        public HistoryWindow(Collection<string> computationHistory)
        {
            this.computationHistory = computationHistory;

        }
    }
}
