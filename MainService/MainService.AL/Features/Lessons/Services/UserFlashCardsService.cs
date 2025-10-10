using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.AL.Features.Lessons.Services;
using MainService.BLL.Data.Users;
using MainService.DAL.Features.Lessons;
using MapsterMapper;
using MongoDB.Bson;

namespace MainService.AL.Features.FlashCards.Services
{
    public class UserFlashCardsService : IUserFlashCardsService
    {
        private readonly IUserFlashCardsRepository _flashCardsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserFlashCardsService(
            IUserFlashCardsRepository flashCardsRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _flashCardsRepository = flashCardsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserFlashCards>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var all = await _flashCardsRepository.GetAllAsync(cancellationToken);
            return all.Where(fc => fc.UserId == userId);
        }
        public async Task<UserFlashCards?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _flashCardsRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<UserFlashCards> GenerateFromUserWordsAsync(Guid userId, string? title, int count, CancellationToken cancellationToken)
        {
            var userWordsPage = await _unitOfWork.UserWords.GetAllByUserIdAsync(userId, 1, int.MaxValue, cancellationToken);
            var translations = await _unitOfWork.Translations.GetAllItemsAsync(cancellationToken);

            var flashCards = userWordsPage.Items
                .Select(uw =>
                {
                    var translation = translations.FirstOrDefault(t => t.FromWordId == uw.WordId);
                    if (translation == null) return null;

                    return new FlashCard
                    {
                        Word = translation.FromWord.Text,
                        Translation = translation.ToWord.Text
                    };
                })
                .Where(fc => fc != null)
                .Take(count)
                .ToList()!;

            var entity = new UserFlashCards
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = userId,
                Title = title ?? "Generated Flashcards",
                Items = flashCards
            };

            await _flashCardsRepository.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            await _flashCardsRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
