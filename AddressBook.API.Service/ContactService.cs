using AddressBook.API.DataInterface;
using AddressBook.API.DataModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.API.Service
{
    public class ContactService : ServiceStack.ServiceInterface.Service
    {
        public IContactRepository contactRepository { get; set; }
        public ContactResponseDTO Post(ContactDTO request)
        {
            return contactRepository.Save(request);
        }
        public ContactResponseDTO Put(ContactDTO request)
        {
            return contactRepository.Update(request);
        }
        public ContactResponseDTO Delete(ContactDTO request)
        {
            return contactRepository.Delete(request);
        }
        public ContactsResponseDTO Get(ContactsDTO request)
        {
            return contactRepository.GetContacts(request);
        }
        public ContactDetailResponseDTO Get(ContactDetailDTO request)
        {
            return contactRepository.GetDetail(request);
        }

    }
}
