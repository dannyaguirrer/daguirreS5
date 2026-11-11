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
        public PersonRepository (string Path)
        {
            _dbPath = Path;
            
        }
        private void Init()
        {
            if (_conn is not null)
                return;
            _conn = new (_dbPath);
            _conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string name, int age)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");
                result = _conn.Insert(new Persona { Nombre = name, Edad = age });
                statusMessage = $"{result} records added [Name: {name}]";
            }
            catch (Exception ex)
            {
                statusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }
    }
}
