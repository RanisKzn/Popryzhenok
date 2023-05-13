using System;
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
    /// Логика взаимодействия для ChangeAgentWindow.xaml
    /// </summary>
    public partial class ChangeAgentWindow : Window
    {

        PopryzhenokDBEntities context = new PopryzhenokDBEntities();
        public ChangeAgentWindow(Agent agent)
        {
            InitializeComponent();
            AgentCard.DataContext = agent;
            TypeCB.ItemsSource = context.AgentsType.ToList();
            TypeCB.SelectedItem = ((List<AgentsType>)TypeCB.ItemsSource).Find(x => x.AgentTypeID == agent.AgentTypeID);
            var consistAgent = context.ProductSale.Where(x => x.AgentId == agent.AgentId).FirstOrDefault();
            if (consistAgent != null)
            {
                DeleteBTN.Visibility = Visibility.Hidden;

            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            using(PopryzhenokDBEntities popryzhenokDBEntities = new PopryzhenokDBEntities())
            {
                var agent = (Agent)AgentCard.DataContext;
                agent.AgentTypeID = ((AgentsType)TypeCB.SelectedItem).AgentTypeID;
                popryzhenokDBEntities.Agent.AddOrUpdate(agent);
                popryzhenokDBEntities.SaveChanges();
                MessageBox.Show("Norm");
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            using (PopryzhenokDBEntities popryzhenokDBEntities = new PopryzhenokDBEntities())
            {
                var id = ((Agent)AgentCard.DataContext).AgentId;
                popryzhenokDBEntities.Agent.Remove(popryzhenokDBEntities.Agent.Where(x => x.AgentId == id).FirstOrDefault());
                popryzhenokDBEntities.SaveChanges();
                MessageBox.Show("Norm");
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
