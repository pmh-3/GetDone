using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace getDone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<GetdoneItem> taskItems = new List<GetdoneItem>();
        int counter = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTask(object sender, RoutedEventArgs e)
        {
            //Task must have title
            if(Title.Text == "")
            {
                return;
            }

            //update Task info
            foreach (var ti in taskItems)
            {
                if (ti.Title == Title.Text)
                {
                    ti.Notes = Notes.Text;
                    ti.Date = Date.Text;
                    ti.Priority = ConvertPriorityInput(Priority.Text);
                    TaskList.Items.Refresh();

                    //Clear fields after add
                    Notes.Text = Priority.Text = Title.Text = "";
                    Date.Text = "/ /";

               
                    //TaskList.Items.Add(taskItems[counter]);
                    return;
                }
            }

            //New Task!
            counter++;
            taskItems.Add(new GetdoneItem() { id = counter, Title = Title.Text, Notes = Notes.Text, Date = Date.Text, Priority = ConvertPriorityInput(Priority.Text)});
            TaskList.Items.Add(taskItems[counter]);
            

            //Clear fields after add
            Notes.Text = Priority.Text = Title.Text = "";
            Date.Text = "/ /";

            //TaskList.ItemsSource = taskItems;
            //sort
            if (counter > 1)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(taskItems);
                view.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Ascending));
            }
  

        }
        private int ConvertPriorityInput(string s)
        {
            try
            {
                int num = Int32.Parse(s);
                return num;
            }
            catch(FormatException)
            {
                return 1;
            }
        }
        private void EditTask(object sender, RoutedEventArgs e)
        {

            Date.Text = taskItems[TaskList.Items.IndexOf(TaskList.SelectedItem)].Date;
            Title.Text = taskItems[TaskList.Items.IndexOf(TaskList.SelectedItem)].Title;
            Notes.Text = taskItems[TaskList.Items.IndexOf(TaskList.SelectedItem)].Notes;
            Priority.Text = taskItems[TaskList.Items.IndexOf(TaskList.SelectedItem)].Priority.ToString();
           
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {

            counter--;
            taskItems.Remove((GetdoneItem)((Button)sender).DataContext);
            TaskList.Items.Remove
             (((Button)sender).DataContext);
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


    public class GetdoneItem
    {

        public int id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public int Priority { get; set;  }

        public bool isDone { get; set; }


    }

}
