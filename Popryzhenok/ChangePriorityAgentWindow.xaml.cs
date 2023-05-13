using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace Popryzhenok
{
    /// <summary>
    /// Логика взаимодействия для ChangePriorityAgentWindow.xaml
    /// </summary>
    public partial class ChangePriorityAgentWindow : Window
    {
        IList agents;
        public ChangePriorityAgentWindow(IList agents)
        {
            InitializeComponent();
            this.agents = agents;
        }

        private void ChangeBTN_Click(object sender, RoutedEventArgs e)
        {
            using(PopryzhenokDBEntities context = new PopryzhenokDBEntities())
            {
                foreach(Agent agent in agents) { 
                   agent.Priority = AgentPriorityTB.Text;
                   context.Agent.AddOrUpdate(agent);
                }
                
                context.SaveChanges();
            }
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
