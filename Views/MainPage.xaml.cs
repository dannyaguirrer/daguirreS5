using daguirreS5.Models;

namespace daguirreS5.Views
{
    public partial class MainPage : ContentPage
    {
        private int _idSeleccionado = 0;   // 0 = no hay registro seleccionado

        public MainPage()
        {
            InitializeComponent();
            CargarPersonas();
        }

        // SELECCIONAR
        private void CargarPersonas()
        {
            cvPersonas.ItemsSource = App.PersonRepo.GetAllPeople();
        }

        // INSERTAR
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (!int.TryParse(txtEdad.Text, out int edad))
            {
                lblStatus.Text = "Ingrese una edad válida";
                return;
            }
            App.PersonRepo.AddNewPerson(txtNombre.Text, edad);
            lblStatus.Text = App.PersonRepo.statusMessage;
            Limpiar();
            CargarPersonas();
        }

        // ACTUALIZAR
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                lblStatus.Text = "Seleccione una persona de la lista";
                return;
            }
            if (!int.TryParse(txtEdad.Text, out int edad))
            {
                lblStatus.Text = "Ingrese una edad válida";
                return;
            }
            App.PersonRepo.UpdatePerson(_idSeleccionado, txtNombre.Text, edad);
            lblStatus.Text = App.PersonRepo.statusMessage;
            Limpiar();
            CargarPersonas();
        }

        // ELIMINAR
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                lblStatus.Text = "Seleccione una persona de la lista";
                return;
            }
            bool confirmar = await DisplayAlert("Eliminar",
                $"¿Eliminar a {txtNombre.Text}?", "Sí", "No");
            if (!confirmar)
                return;

            App.PersonRepo.DeletePerson(_idSeleccionado);
            lblStatus.Text = App.PersonRepo.statusMessage;
            Limpiar();
            CargarPersonas();
        }

        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {
            Limpiar();
        }

        // Al tocar una fila, se cargan sus datos en el formulario
        private void cvPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Persona p)
            {
                _idSeleccionado = p.Id;
                lblId.Text = $"Id: {p.Id}";
                txtNombre.Text = p.Nombre;
                txtEdad.Text = p.Edad.ToString();
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            lblId.Text = "Id: (nuevo)";
            txtNombre.Text = string.Empty;
            txtEdad.Text = string.Empty;
            cvPersonas.SelectedItem = null;
        }
    }
}
