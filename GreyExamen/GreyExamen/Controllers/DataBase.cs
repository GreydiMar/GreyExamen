using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using GreyExamen.Models;
using System.Threading.Tasks;
using GreyExamen.Controllers;

namespace GreyExamen.Controllers
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbconexion;


        public DataBase(string dbpath)
        {
            dbconexion = new SQLiteAsyncConnection(dbpath);


            dbconexion.CreateTableAsync<Contactos>();
        }



        public Task<List<Contactos>> ObtenerListaContactos()
        {
            return dbconexion.Table<Contactos>().ToListAsync();
        }



        public Task<int> AddContactos(Contactos contactos)
        {
            if (contactos.Id != 0)
            {


                return dbconexion.UpdateAsync(contactos);
            }
            else
            {
                return dbconexion.InsertAsync(contactos);
            }
        }


        public Task<Contactos> ObtenerContactos(int pid)
        {
            return dbconexion.Table<Contactos>()
                .Where(i => i.Id == pid)
                .FirstOrDefaultAsync();
        }


        public Task<int> DelContactos(Contactos contactos)
        {
            return dbconexion.DeleteAsync(contactos);
        }
    }
}
