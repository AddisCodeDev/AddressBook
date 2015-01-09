using AddressBook.API.DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.API.DataModel.Operations;
using ServiceStack.OrmLite;
using System.Data;
using AddressBook.API.DataModel.Types;

namespace AddressBook.API.DataContext
{
    public class ContactRepository : IContactRepository
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public IDbConnection DbConnection { get; set; }

        public ContactRepository(IDbConnectionFactory _DbConnectionFactory)
        {
            DbConnectionFactory = _DbConnectionFactory;
        }
        public ContactResponseDTO Delete(ContactDTO request)
        {
            ContactResponseDTO response = new ContactResponseDTO();
            Contact ContactToDelete = request as Contact;
            using (DbConnection = DbConnectionFactory.OpenDbConnection())
            {
                DbConnection.Delete<Contact>(ContactToDelete);
            }
            return response;
        }

        public ContactsResponseDTO GetContacts(ContactsDTO request)
        {
            ContactsResponseDTO response = new ContactsResponseDTO();
            using (DbConnection = DbConnectionFactory.OpenDbConnection())
            {
                response.Contacts  = DbConnection.Select<Contact>().ToList<Contact>() ;
            }
            return response;
        }

        public ContactDetailResponseDTO GetDetail(ContactDetailDTO request)
        {
            ContactDetailResponseDTO response = new ContactDetailResponseDTO();
            
            using (DbConnection = DbConnectionFactory.OpenDbConnection())
            {
                response.Contact = DbConnection.Select<Contact>().Where(c => c.Id == request.Id).FirstOrDefault() ;
            }
            return response;
        }

        public ContactResponseDTO Save(ContactDTO request)
        {
            ContactResponseDTO response = new ContactResponseDTO();
            Contact newContact = request as Contact;
            using (DbConnection = DbConnectionFactory.OpenDbConnection())
            {
                DbConnection.Insert(newContact);
            }
            return response;
        }

        public ContactResponseDTO Update(ContactDTO request)
        {
            ContactResponseDTO response = new ContactResponseDTO();
            Contact updatedContact = request as Contact;
            using (DbConnection = DbConnectionFactory.OpenDbConnection())
            {
                DbConnection.Update(updatedContact);
            }
            return response;
        }
    }
}
