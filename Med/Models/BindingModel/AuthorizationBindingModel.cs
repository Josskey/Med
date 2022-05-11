namespace Med.Models.BindingModel
{
    public class AuthorizationBindingModel : OnPropertyChangedClass
    {
        public int ID { get => id; set { id = value; OnPropertyChanged(); } }
        public string Login { get => login; set { login = value; OnPropertyChanged(); } }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }
        public string Access
        {
            get => access; set
            {
                access = value;
                if (Access == "Полный")
                    CheckStat = true;
                else
                    CheckStat = false;
                OnPropertyChanged();
            }
        }
        public int IdDoctor { get => idDoctor; set { idDoctor = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        public bool CheckStat { get => checkState; set { checkState = value; OnAllPropertyChanged(); } }
        private int id;
        private string login;
        private string password;
        private string access;
        private int idDoctor;
        private string name;
        private bool checkState;
    }
    public class User
    {
        public static AuthorizationBindingModel Authorization { get; set; }
        public static int PatientId { get; set; }
    }
}
