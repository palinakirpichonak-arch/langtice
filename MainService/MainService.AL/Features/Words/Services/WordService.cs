using MainService.AL.Words.DTO;
using MainService.AL.Words.Interfaces;
using MainService.BLL.Words.Interfaces;
using MainService.DAL.Models;

namespace MainService.BLL.Words.Service
{
    public class WordService : IWordService
    {
        private readonly IWordManager _wordManager;

        public WordService(IWordManager wordManager)
        {
            _wordManager = wordManager;
        }

        public async Task<Word> GetWordByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _wordManager.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Word>> GetAllWordsAsync(CancellationToken cancellationToken)
        {
            return await _wordManager.GetAllAsync(cancellationToken);
        }

        public async Task<Word> AddWordAsync(WordDTO wordDTO, CancellationToken cancellationToken)
        {
            Word word = new()
            {
                Id = Guid.NewGuid(),
                Text = wordDTO.Text,
                LanguageId = wordDTO.LanguageId
            };
            await _wordManager.AddAsync(word, cancellationToken);
            return word;
        }

        public async Task DeleteWordAsync(Guid id, CancellationToken cancellationToken)
        {
            var word = await _wordManager.GetByIdAsync(id, cancellationToken);
            await _wordManager.DeleteAsync(word, cancellationToken);
        }
    }
}