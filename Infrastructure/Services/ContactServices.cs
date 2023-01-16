using Dapper;
using Domain.Dtos;
using Npgsql;

namespace Infrastructure.Services;

public class ContactServices
{
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=Contacts_db;User Id=postgres;Password=2004";
   // private string _connection = "Server=127.0.0.1;Port=5432;Database=Contacts_db;User Id=postgres;Password=2004";
   public List<ContactDto> GetContact()
   {
       using (var connection = new NpgsqlConnection(_connectionString))
       {
           string sql = "select * from Contacts";
           var result = connection.Query<ContactDto>(sql);
           return result.ToList();
       }
   }

   public List<ContactDto> GetContactByName(string name)
   {
       using (var connection  = new NpgsqlConnection(_connectionString))
       {
           string sql = $"select * from Contacts where Name = '{name}'";
           var result = connection.Query<ContactDto>(sql);
           return result.ToList();
       }

   }


   public bool AddContact(ContactDto contactDto)
   {
       using (var connection  = new NpgsqlConnection(_connectionString))
       {  
           string sql  = 
           $"insert into Contacts(id, Name, Address, Telephone) "+
           $"values({contactDto.Id},'{contactDto.Name}','{contactDto.Address}','{contactDto.Telephone}')";
           var result = connection.Execute(sql);
           if (result > 0) return true;
           return false;
       }
   }

   public bool UpdateContact(ContactDto contactDto)
   {
       using (var connection = new NpgsqlConnection(_connectionString))
       {
           string sql =
               $" update Contacts " +
               $" set Name = '{contactDto.Name}',Address = '{contactDto.Address}',Telephone = '{contactDto.Telephone}' "+
               $" where Id = {contactDto.Id}";
           var result = connection.Execute(sql);
           if (result>0) return true;
           return false;
       }
   }

   public bool DeleteContact(int id)
   {
       using (var connection  = new NpgsqlConnection(_connectionString))
       {
           string sql = $"delete from Contacts where Id  = {id}";
           var result = connection.Execute(sql);
           if (result > 0) return true;
           return false;
       }
   }



}