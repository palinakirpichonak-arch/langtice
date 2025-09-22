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

        public async Task<Word> AddWordAsync(Word word, CancellationToken cancellationToken)
        {
            await _wordManager.AddAsync(word, cancellationToken);
            return word;
        }

        public async Task DeleteWordAsync(Word word, CancellationToken cancellationToken)
        {
            await _wordManager.DeleteAsync(word, cancellationToken);
        }
    }
}