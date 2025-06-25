using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Domain_Models;

namespace MusicStore.Service.Interface
{
    namespace MusicStore.Service.Interface
    {
        public interface ILabelService
        {
            List<Label> GetAll();
            Label? GetById(Guid id);
            Label Insert(Label label);
            Label Update(Label label);
            Label DeleteById(Guid id);
            bool Exists(Guid id);
        }
    }
}
