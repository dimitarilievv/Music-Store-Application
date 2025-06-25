using MusicStore.Domain.Domain_Models;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using MusicStore.Service.Interface.MusicStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Service.Implementation
{
    public class LabelService : ILabelService
    {
        private readonly IRepository<Label> _labelRepository;

        public LabelService(IRepository<Label> labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public List<Label> GetAll()
        {
            return _labelRepository.GetAll(selector: x => x).ToList();
        }

        public Label? GetById(Guid id)
        {
            return _labelRepository.Get(selector: x => x, predicate: x => x.Id == id);
        }

        public Label Insert(Label label)
        {
            label.Id = Guid.NewGuid();
            return _labelRepository.Insert(label);
        }

        public Label Update(Label label)
        {
            return _labelRepository.Update(label);
        }

        public Label DeleteById(Guid id)
        {
            var label = GetById(id);
            if (label == null)
                throw new Exception("Label not found");

            _labelRepository.Delete(label);
            return label;
        }

        public bool Exists(Guid id)
        {
            return _labelRepository.Get(x => x.Id == id) != null;
        }

        
    }
}
