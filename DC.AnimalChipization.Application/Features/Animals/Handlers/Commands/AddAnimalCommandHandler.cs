using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Animals.Handlers.Commands
{
    public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommandMessage, AnimalDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddAnimalCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AnimalDto> Handle(AddAnimalCommandMessage request, CancellationToken cancellationToken)
        {
            if (AnimalTypesHasDuplicates(request.AnimalTypes))
            {
                throw new ConflictException();
            }

            var relatedData = await GetRelatedData(request);
            var entity = CreateAnimalEntity(request, relatedData);

            await _unitOfWork.Animals.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AnimalDto>(entity);
        }
        
        private bool AnimalTypesHasDuplicates(List<long> animalTypes)
        {
            return animalTypes.Distinct().ToList().Count != animalTypes.Count;
        }
        
        private async Task<(List<AnimalTypeEntity>, AccountEntity, LocationEntity)> GetRelatedData(AddAnimalCommandMessage request)
        {
            var animalTypeEntities = await _unitOfWork.AnimalTypes.ListAsync(new AnimalTypeFilter
            {
                Ids = request.AnimalTypes
            }, null);

            if (animalTypeEntities.Count != request.AnimalTypes.Count)
            {
                throw new NotFoundException();
            }

            var account = await _unitOfWork.Accounts.GetByIdAsync(request.ChipperId);

            if (account == null)
            {
                throw new NotFoundException();
            }

            var location = await _unitOfWork.Locations.GetByIdAsync(request.ChippingLocationId);

            if (location == null)
            {
                throw new NotFoundException();
            }

            return (animalTypeEntities, account, location);
        }

        private AnimalEntity CreateAnimalEntity(AddAnimalCommandMessage request,
            (List<AnimalTypeEntity> Types, AccountEntity Chipper, LocationEntity Location) relatedData)
        {
            var animalEntity = _mapper.Map<AnimalEntity>(request);

            animalEntity.LifeStatus = (int)LifeStatus.Alive;
            animalEntity.ChippingDateTime = DateTime.UtcNow;

            animalEntity.AnimalTypes = relatedData.Types;
            animalEntity.Chipper = relatedData.Chipper;
            animalEntity.ChippingLocation = relatedData.Location;

            return animalEntity;
        }
    }
}
