using daguirreS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace daguirreS5.Repositories
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection _conn;

        public string statusMessage { get; set; }

        public PersonRepository(string Path)
        {
            _dbPath = Path;
        }

        private void Init()
        {
            if (_conn is not null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        // INSERTAR
        public void AddNewPerson(string name, int age)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Se requiere un nombre válido");
                result = _conn.Insert(new Persona { Nombre = name, Edad = age });
                statusMessage = $"{result} registro agregado [Nombre: {name}]";
            }
            catch (Exception ex)
            {
                statusMessage = $"Error al agregar {name}. Error: {ex.Message}";
            }
        }

        // SELECCIONAR
        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = $"Error al obtener los datos. {ex.Message}";
            }
            return new List<Persona>();
        }

        // ACTUALIZAR
        public void UpdatePerson(int id, string name, int age)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Se requiere un nombre válido");
                result = _conn.Update(new Persona { Id = id, Nombre = name, Edad = age });
                statusMessage = $"{result} registro actualizado [Id: {id}]";
            }
            catch (Exception ex)
            {
                statusMessage = $"Error al actualizar el Id {id}. Error: {ex.Message}";
            }
        }

        // ELIMINAR
        public void DeletePerson(int id)
        {
            int result = 0;
            try
            {
                Init();
                result = _conn.Delete<Persona>(id);
                statusMessage = $"{result} registro eliminado [Id: {id}]";
            }
            catch (Exception ex)
            {
                statusMessage = $"Error al eliminar el Id {id}. Error: {ex.Message}";
            }
        }
    }
}
