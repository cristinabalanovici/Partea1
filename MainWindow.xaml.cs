using StomatologieModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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


namespace Partea1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New, 
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        StomatologieEntitiesModel ctx = new StomatologieEntitiesModel();
        CollectionViewSource doctorVSource;
        CollectionViewSource pontajVSource;
        CollectionViewSource concediuVSource;
        CollectionViewSource titluVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Data.CollectionViewSource titluViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("titluViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // titluViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource doctorViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("doctorViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // doctorViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource pontajViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pontajViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // pontajViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource concediuViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("concediuViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // concediuViewSource.Source = [generic data source]
            doctorVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("doctorViewSource")));
            pontajVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pontajViewSource")));
            concediuVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("concediuViewSource")));
            titluVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("titluViewSource")));
            doctorVSource.Source = ctx.Doctors.Local;
            pontajVSource.Source = ctx.Pontajs.Local;
            concediuVSource.Source = ctx.Concedius.Local;
            titluVSource.Source = ctx.Titlus.Local;
            ctx.Doctors.Load();
            ctx.Pontajs.Load();
            ctx.Concedius.Load();
            ctx.Titlus.Load();
        }

        private void ReInitialize()
        {
            Panel panel = gbOperations.Content as Panel;
            foreach(Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void setValidationBinding()
        {
            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = doctorVSource;
            numeValidationBinding.Path = new PropertyPath("Nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding);
            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = doctorVSource;
            prenumeValidationBinding.Path = new PropertyPath("Prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            prenumeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            setValidationBinding();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

            setValidationBinding();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            doctorVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            doctorVSource.View.MoveCurrentToNext();
        }

        private void btnNextPontaj_Click(object sender, RoutedEventArgs e)
        {
            pontajVSource.View.MoveCurrentToNext();
        }

        private void btnPrevPontaj_Click(object sender, RoutedEventArgs e)
        {
            pontajVSource.View.MoveCurrentToPrevious();
        }

        private void btnNextConcediu_Click(object sender, RoutedEventArgs e)
        {
            concediuVSource.View.MoveCurrentToNext();
        }

        private void btnPrevConcediu_Click(object sender, RoutedEventArgs e)
        {
            concediuVSource.View.MoveCurrentToPrevious();
        }

        private void btnPrevTitlu_Click(object sender, RoutedEventArgs e)
        {
            titluVSource.View.MoveCurrentToPrevious();
        }

        private void btnNextTitlu_Click(object sender, RoutedEventArgs e)
        {
            titluVSource.View.MoveCurrentToNext(); 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlStomatologie.SelectedItem as TabItem;
            switch (ti.Header)
            {
                case "Doctori":
                    SaveDoctors();
                    break;
                case "Pontaj":
                    SavePontaj();
                    break;
                case "Concedii":
                    SaveConcediu();
                    break;
                case "Titluri":
                    SaveTitlu();
                    break;
            }
        }
        private void SaveDoctors()
        {
            Doctor doctor = null;
            if (action == ActionState.New)
            {
                try
                {
                    doctor = new Doctor()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                    };
                    ctx.Doctors.Add(doctor);
                    doctorVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (action == ActionState.Edit)
                {
                    try
                    {
                        doctor = (Doctor)doctorDataGrid.SelectedItem;
                        doctor.Nume = numeTextBox.Text.Trim();
                        doctor.Prenume = prenumeTextBox.Text.Trim();
                        ctx.SaveChanges();
                    }
                    catch(DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (action==ActionState.Delete)
                {
                    try
                    {
                        doctor = (Doctor)doctorDataGrid.SelectedItem;
                        ctx.Doctors.Remove(doctor);
                        ctx.SaveChanges();
                    }
                    catch(DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    doctorVSource.View.Refresh();
                }
            }
        }

        private void SavePontaj()
        {
            Pontaj pontaj = null;
            if (action == ActionState.New)
            {
                try
                {
                    pontaj = new Pontaj()
                    {
                        Data = (DateTime)dataDatePicker.SelectedDate.Value,
                        OraStart = decimal.Parse(oraStartTextBox.Text),
                        OraFinal = decimal.Parse(oraFinalTextBox.Text)
                    };
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (action == ActionState.Edit)
                {
                    try
                    {
                        pontaj = (Pontaj)pontajDataGrid.SelectedItem;
                        pontaj.Data = (DateTime)dataDatePicker.SelectedDate.Value;
                        pontaj.OraStart = decimal.Parse(oraStartTextBox.Text);
                        pontaj.OraFinal = decimal.Parse(oraFinalTextBox.Text);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (action == ActionState.Delete)
                {
                    try
                    {
                        pontaj = (Pontaj)pontajDataGrid.SelectedItem;
                        ctx.Pontajs.Remove(pontaj);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    pontajVSource.View.Refresh();
                }

            }
        }


        private void SaveConcediu()
        {
            Concediu concediu = null;
            if(action == ActionState.New)
            {
                try
                {
                    concediu = new Concediu()
                    {
                        DataStart = (DateTime)dataStartDatePicker.SelectedDate.Value.Date,
                        DataFinal = (DateTime)dataFinalDatePicker.SelectedDate.Value.Date
                    };
                }
                catch(DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (action == ActionState.Edit)
                {
                    try
                    {
                        concediu = (Concediu)concediuDataGrid.SelectedItem;
                        concediu.DataStart = (DateTime)dataStartDatePicker.SelectedDate.Value.Date;
                        concediu.DataFinal = (DateTime)dataFinalDatePicker.SelectedDate.Value.Date;
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (action == ActionState.Delete)
                {
                    try
                    {
                        concediu = (Concediu)concediuDataGrid.SelectedItem;
                        ctx.Concedius.Remove(concediu);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    concediuVSource.View.Refresh();
                }
            }
        } 

        private void SaveTitlu()
        {
            Titlu titlu = null;
            if (action == ActionState.New)
            {
                try
                {
                    titlu = new Titlu()
                    {
                        Titlu1 = titlu1TextBox.Text.Trim()
                    };
                    ctx.Titlus.Add(titlu);
                    titluVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch(DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (action == ActionState.Edit)
                {
                    try
                    {
                        titlu = (Titlu)titluDataGrid.SelectedItem;
                        titlu.Titlu1 = titlu1TextBox.Text.Trim();
                        ctx.SaveChanges();
                    }
                    catch(DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (action == ActionState.Delete)
                {
                    try
                    {
                        titlu = (Titlu)titluDataGrid.SelectedItem;
                        ctx.Titlus.Remove(titlu);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    titluVSource.View.Refresh();
                }
            }
        }

        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)selectedButton.Parent;
            foreach(Button B in panel.Children.OfType<Button>())
            {
                if (B != selectedButton)
                {
                    B.IsEnabled = false;
                }
            }
            gbActions.IsEnabled = true;
        }

        
    }
}
