using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MainService.DAL;

namespace MainService.BLL.Words.Manager;

public class UserWordManager : IUserWordManager
{
    private readonly IRepository<UserWord> _repository;
    private readonly IKeysRepository<UserWord> _userWordRepository;

    public UserWordManager(IRepository<UserWord> repository,  IKeysRepository<UserWord> userWordRepository)
    {
        _repository = repository;
        _userWordRepository = userWordRepository;
    }

    public async Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        return await _userWordRepository.GetByIdsAsync(userId, wordId, cancellationToken);
    }

    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _userWordRepository.GetAllByUserIdAsync(userId, cancellationToken);
    }

    public async Task AddAsync(UserWord userWord, CancellationToken cancellationToken)
    {
        userWord.AddedAt = DateTime.UtcNow;
        await _repository.AddItemAsync(userWord, cancellationToken);
    }

    public async Task DeleteAsync(UserWord userWord, CancellationToken cancellationToken)
    {
        await _repository.DeleteItemAsync(userWord, cancellationToken);
    }
}