using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Popryzhenok
{
    /// <summary>
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {

        PopryzhenokDBEntities context = new PopryzhenokDBEntities();
        Agent agent;
        public AddAgentWindow()
        {
            InitializeComponent();
            agent = new Agent();
            AgentCard.DataContext = agent;
            TypeCB.ItemsSource = context.AgentsType.ToList();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            using (PopryzhenokDBEntities popryzhenokDBEntities = new PopryzhenokDBEntities())
            {
                var agent = (Agent)AgentCard.DataContext;
                agent.AgentTypeID = ((AgentsType)TypeCB.SelectedItem).AgentTypeID;
                popryzhenokDBEntities.Agent.AddOrUpdate(agent);
                popryzhenokDBEntities.SaveChanges();
                MessageBox.Show("Norm");
            }
        }
    }
}
