using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Popryzhenok
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Agent> agents;
        PopryzhenokDBEntities context = new PopryzhenokDBEntities();
        public MainWindow()
        {
            InitializeComponent();
            agents = context.Agent.ToList(); 
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
            setItemSource();
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            setItemSource();

        }
        private List<Agent> getBySearch(List<Agent> agents)
        {

            if (String.IsNullOrEmpty(SearchTB.Text))
            {
                return agents;
            }
            else
            {

                agents = agents.Where(x => x.AgentName.Contains(SearchTB.Text) || x.Phone.Contains(SearchTB.Text) || x.Email.Contains(SearchTB.Text)).ToList();
            }
            return agents;
        }
        private List<Agent> getBySorted(List<Agent> agents)
        {

            switch (SorterCB.SelectedIndex)
            {
                case 0:
                    agents = agents.OrderBy(x => x.AgentName).ToList();
                    break;
                case 1:
                    agents = agents.OrderByDescending(x => x.AgentName).ToList();
                    break;
                case 2:
                    agents = agents.OrderBy(x => x.Sale).ToList();
                    break;
                case 3:
                    agents = agents.OrderByDescending(x => x.Sale).ToList();
                    break;
                case 4:
                    agents = agents.OrderBy(x => x.Priority).ToList();
                    break;
                case 5:
                    agents = agents.OrderByDescending(x => x.Priority).ToList();
                    break;
                default:
                    agents = agents.ToList();
                    break;
            }
            return agents;
        }
        private void setItemSource()
        {
            var agents = this.agents;
            var filteredAgents = getByfilter(agents);
            var searchedAgents = getBySearch(filteredAgents);
            var sortedAgents = getBySorted(searchedAgents);
            AgentList.ItemsSource = sortedAgents;
            

        }
        private List<Agent> getByfilter(List<Agent> agents)
        {
            if (FilterCB.SelectedIndex == 0)
            {
                agents = context.Agent.ToList();
            }
            else
            {
                agents = context.Agent.Where(x => x.AgentsType.Name == ((AgentsType)FilterCB.SelectedItem).Name).ToList();
            }
            return agents;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            setItemSource();
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
        private void AddAgentBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow addAgentWindow = new AddAgentWindow();
            this.Close();
            addAgentWindow.Show();
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Grid).DataContext is Agent agent)
            {
                ChangeAgentWindow changeAgentWindow = new ChangeAgentWindow(agent);
                this.Close();
                changeAgentWindow.Show();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
