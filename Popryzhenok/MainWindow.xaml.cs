using System;
using System.Collections;
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

namespace Popryzhenok
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PopryzhenokDBEntities context = new PopryzhenokDBEntities();
        public MainWindow()
        {
            InitializeComponent();
            AgentList.ItemsSource = context.Agent.ToList();
            SorterCB.ItemsSource = new List<string>() 
            { 
                "По возрастанию наименования","По убыванию наименования","По возрастанию скидки","По убыванию скидки","По возрастанию приоритета","По убыванию приоритета"
            };
            var items = new List<AgentsType>()
            { new AgentsType { Name = "Все типы" }};
            items.AddRange(context.AgentsType.ToList());
            FilterCB.ItemsSource = items;



        }

        private void SorterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(SorterCB.SelectedIndex)
            {
                case 0:
                    AgentList.ItemsSource = context.Agent.OrderBy(x => x.AgentName).ToList();
                    break;
                case 1:
                    AgentList.ItemsSource = context.Agent.OrderByDescending(x => x.AgentName).ToList();
                    break;
                case 2:
                    AgentList.ItemsSource = context.Agent.OrderBy(x => x.Sale).ToList();
                    break;
                case 3:
                    AgentList.ItemsSource = context.Agent.OrderByDescending(x => x.Sale).ToList();
                    break;
                case 4:
                    AgentList.ItemsSource = context.Agent.OrderBy(x => x.Priority).ToList();
                    break;
                case 5:
                    AgentList.ItemsSource = context.Agent.OrderByDescending(x => x.Priority).ToList();
                    break;
                default:
                    AgentList.ItemsSource = context.Agent.ToList();
                    break;
            }
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getByfilter();

        }

        private void getByfilter()
        {
            var agentList = AgentList.ItemsSource;
            if (FilterCB.SelectedIndex == 0)
            {
                agentList = (agentList as List<Agent>).ToList();
            }
            else
            {
                agentList = (agentList as List<Agent>).Where(x => x.AgentsType.Name == ((AgentsType)FilterCB.SelectedItem).Name).ToList();
            }
            if (String.IsNullOrEmpty(SearchTB.Text))
            {

                agentList = (agentList as List<Agent>).ToList();
            }
            else
            {

                agentList = (agentList as List<Agent>).Where(x => x.AgentName.Contains(SearchTB.Text) ||  x.Phone.Contains(SearchTB.Text) || x.Email.Contains(SearchTB.Text)).ToList();
            }
            AgentList.ItemsSource = agentList; 
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            getByfilter();
        }

        private void AgentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AgentList.SelectedItems.Count > 1) 
            { 
                ChangePriorityBTN.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityBTN.Visibility = Visibility.Hidden;
            }
        }

        private void ChangePriorityBTN_Click(object sender, RoutedEventArgs e)
        {
            var agentList = AgentList.SelectedItems;
            ChangePriorityAgentWindow changePriorityAgentWindow = new ChangePriorityAgentWindow(agentList);
            this.Close();
            changePriorityAgentWindow.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if((sender as Grid).DataContext is Agent agent)
            {
                ChangeAgentWindow changeAgentWindow = new ChangeAgentWindow(agent);
                this.Close();
                changeAgentWindow.Show();
            }
        }

        private void AddAgentBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow addAgentWindow = new AddAgentWindow();
            this.Close();
            addAgentWindow.Show();
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
