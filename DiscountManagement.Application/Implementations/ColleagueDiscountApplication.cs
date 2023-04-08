using DiscountManagement.Application.Contracts;
using DiscountManagement.Application.Shared;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Implementations
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository repository) => _repository = repository;

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _repository.Create(colleagueDiscount);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(command.Id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_repository.DoesExist(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate && cd.Id == command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id) => _repository.GetDetails(id);

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Remove();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Restore();
            _repository.Save();
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel) => _repository.Search(searchModel);
    }
}