using MainService.AL.Exceptions;
using MainService.AL.Features.UserFlashCards.DTO.Request;
using MainService.DAL.Models.UserFlashCardModel;
using MainService.DAL.Repositories.Translations;
using MainService.DAL.Repositories.UserFlashCards;
using MainService.DAL.Repositories.UserWords;
using MongoDB.Bson;

namespace MainService.AL.Features.UserFlashCards.Services
{
    public class UserFlashCardsService : IUserFlashCardsService
    {
        private readonly IUserWordRepository _userWordRepository;
        private readonly ITranslationRepository _translationRepository;
        private readonly IUserFlashCardsRepository _flashCardsRepository;

        public UserFlashCardsService(
            IUserWordRepository userWordRepository,
            ITranslationRepository translationRepository,
            IUserFlashCardsRepository flashCardsRepository)
        {
            _userWordRepository = userWordRepository;
            _translationRepository = translationRepository;
            _flashCardsRepository = flashCardsRepository;
        }
        public async Task<IEnumerable<UserFlashCard>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var all = await _flashCardsRepository.GetAllAsync(cancellationToken);
            return all.Where(fc => fc.UserId == userId);
        }
        public async Task<UserFlashCard?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var flashCards = await _flashCardsRepository.GetByIdAsync(id, cancellationToken);
            if (flashCards == null)
                throw new NotFoundException("Flash card not found");
            return flashCards;
        }

        public async Task<UserFlashCard> GenerateFromUserWordsAsync(RequestUserFlashCardDto dto, Guid userId, CancellationToken cancellationToken)
        {
            var userWordsPage = await _userWordRepository.GetAllByUserIdAsync(userId, 1, dto.Count, cancellationToken);
            var translations = await _translationRepository.GetAsync(
                tracking: false,
                cancellationToken: cancellationToken,
                includes: [t => t.FromWord, t => t.ToWord]);

            var flashCards = userWordsPage.Items
                .Select(uw =>
                {
                    var translation = translations.FirstOrDefault(t => t.FromWordId == uw.WordId || t.ToWordId == uw.WordId);
                    if (translation == null) return null;

                    if (translation.FromWordId == uw.WordId)
                    {
                        return new FlashCard
                        {
                            Word = translation.FromWord.Text,
                            Translation = translation.ToWord.Text
                        };
                    }

                    return new FlashCard
                    {
                        Word = translation.ToWord.Text,
                        Translation = translation.FromWord.Text
                    };
                })
                .Where(fc => fc != null)
                .Take(dto.Count)
                .ToList()!;

            System.Console.WriteLine("Flashcards count" + flashCards.Count);

            var entity = new UserFlashCard
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = userId,
                Title = dto.Title ?? "Generated Flashcards",
                Items = flashCards,
                CreatedAt = DateTime.UtcNow
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
