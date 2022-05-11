using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Med.DB;
using Med.Models.BindingModel;
using Med.Windows;

namespace Med.Models.ViewModel
{
    public class ViewModelMed : BaseCommand.BaseRelayCommand
    {
        public ObservableCollection<RecordBindingModel> Table { get => table; set { table = value; OnPropertyChanged(); } }

        //Параметр поиска
        public override int ParamSearch { get => paramSearch; set { paramSearch = value; OnPropertyChanged(); } }
        //Поиск
        public override string Search
        {
            get => search;
            set
            {
                search = value;

                Table.Clear();
                switch (ParamSearch)
                {
                    case 0:
                        Table = new ObservableCollection<RecordBindingModel>(new ViewModelRecord().Table.Where(x => 
                        x.ID_Doctor == User.Authorization.IdDoctor && x.SPatient.Contains(Search)).GroupBy(x => x.Policy_number).Select(x => x.FirstOrDefault()).Distinct());
                        break;
                    case 1:
                        Table = new ObservableCollection<RecordBindingModel>(new ViewModelRecord().Table.Where(x =>
                        x.ID_Doctor == User.Authorization.IdDoctor && x.Policy_number.ToString().Contains(Search)).GroupBy(x => x.Policy_number).Select(x => x.FirstOrDefault()).Distinct());
                        break;
                    case 2:
                        Table = new ObservableCollection<RecordBindingModel>(new ViewModelRecord().Table.Where(x =>
                        x.ID_Doctor == User.Authorization.IdDoctor && x.Card_number.ToString().Contains(Search)).GroupBy(x => x.Policy_number).Select(x => x.FirstOrDefault()).Distinct());
                        break;
                }
                OnPropertyChanged();
            }
        }
        public ViewModelMed()
        {
            Table = new ObservableCollection<RecordBindingModel>(new ViewModelRecord().Table.Where(x => x.ID_Doctor == User.Authorization.IdDoctor).GroupBy(x => x.Policy_number).Select(x => x.FirstOrDefault()).Distinct());
            var data = SQL.Execute<DoctorBindingModel>($"SELECT * FROM [dbo].[Doctor] WHERE ID={User.Authorization.IdDoctor}");
            Image = data.Image;
            ChartWork = $"График работы: {data.ChartWork}";
            Specialization = $"Специализация: {data.Specialization}";
            CountPatient = $"Кол-во пациентов: {Table.Count}";
            Access = User.Authorization.CheckStat;    
        }
        //Команда добавить
        public override RelayCommand Add => add ?? (add = new RelayCommand(arg =>
        {
            WindowRecord newDataRecord = new WindowRecord();
            newDataRecord.ShowDialog();
            Table = new ObservableCollection<RecordBindingModel>((newDataRecord.DataContext as ViewModelRecord).Table.Where(x => x.ID_Doctor == User.Authorization.IdDoctor).GroupBy(x => x.Policy_number).Select(x => x.FirstOrDefault()).Distinct());
            CountPatient = $"Кол-во пациентов: {Table.Count}";

        }));
        public RelayCommand Doctor => doctor ?? (doctor = new RelayCommand(arg =>
        {
            WindowDoctor newDataDoctor = new WindowDoctor();
            newDataDoctor.ShowDialog();
        }));
        public RelayCommand Patient => patient ?? (patient = new RelayCommand(arg =>
        {
            WindowPatient newDataPatient = new WindowPatient();
            newDataPatient.ShowDialog();
        }));
        public RelayCommand Diseases => diseases ?? (diseases = new RelayCommand(arg =>
        {
            WindowDiseases newDataDiseases = new WindowDiseases();
            newDataDiseases.ShowDialog();
        }));
        public RelayCommand Authorization => authorization ?? (authorization = new RelayCommand(arg =>
        {
            WindowAdmin newDataAdmin = new WindowAdmin();
            newDataAdmin.ShowDialog();
        }));

        //Команда закрыть окно
        public override RelayCommand Close => close ?? (close = new RelayCommand(arg =>
        {
            (arg as Window).Close();
        }));
        public RelayCommand DBClick => dbclcik ?? (dbclcik = new RelayCommand(arg =>
        {
            //ObservableCollection<PatientBindingModel>
            User.PatientId = (arg as BindingModel.RecordBindingModel).ID_Patient;
            Windows.WindowDetails details = new WindowDetails();
            details.ShowDialog();
        }));

        public string ChartWork { get => chartWork; set { chartWork = value; OnPropertyChanged(); } }
        public string Specialization { get => specialization; set { specialization = value; OnPropertyChanged(); } }
        public BitmapImage Image { get => image; set { image = value; OnPropertyChanged(); } }

        public string CountPatient { get => countPatient; set { countPatient = value; OnPropertyChanged(); } }

        public bool Access { get => access; set { access = value; OnPropertyChanged(); } }


        private RelayCommand doctor;
        private RelayCommand patient;
        private RelayCommand diseases;
        private RelayCommand authorization;
        private RelayCommand dbclcik;
        
        
        
        
        
       
        private ObservableCollection<RecordBindingModel> table;
        private string chartWork;
        private BitmapImage image;
        private string specialization;
        private string countPatient;
        private bool access;
        
       
    }
}
