using System.Linq.Expressions;
using MainService.AL.Features.UserFlashCards.DTO.Request;
using MainService.AL.Features.UserFlashCards.Services;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Models.TranslationsModel;
using MainService.DAL.Models.UserFlashCardModel;
using MainService.DAL.Models.UserWordModel;
using MainService.DAL.Models.WordsModel;
using MainService.DAL.Repositories.Translations;
using MainService.DAL.Repositories.UserFlashCards;
using MainService.DAL.Repositories.UserWords;
using MapsterMapper;
using Moq;

namespace MainService.Tests.ServicesTests;

public class UserFlashCardsServiceTest
{
    private readonly Mock<IUserWordRepository> _userWordRepoMock = new();
    private readonly Mock<ITranslationRepository> _translationRepoMock = new();
    private readonly Mock<IUserFlashCardsRepository> _flashCardsRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private UserFlashCardsService _service;
    private CancellationToken _ct = CancellationToken.None;

    [Fact]
    public async Task GenerateFromUserWordsAsync_ShouldReturnFlashcards()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var wordId = Guid.NewGuid();

        var userWord = new UserWord
        {
            UserId = userId,
            WordId = wordId,
            Word = new Word { Id = wordId, Text = "hello" }
        };

        _userWordRepoMock
            .Setup(r => r.GetAllByUserIdAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MainService.DAL.Abstractions.PaginatedList<UserWord>(new List<UserWord> { userWord }, 1, 1));

        var translation = new Translation
        {
            Id = Guid.NewGuid(),
            FromWordId = wordId,
            FromWord = new Word { Id = wordId, Text = "hello" },
            ToWordId = Guid.NewGuid(),
            ToWord = new Word { Id = Guid.NewGuid(), Text = "hola" }
        };

        _translationRepoMock
            .Setup(r => r.GetAsync(
                It.IsAny<Expression<Func<Translation, bool>>?>(),
                It.IsAny<Func<IQueryable<Translation>, IOrderedQueryable<Translation>>?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<Expression<Func<Translation, object>>[]?>()))
            .ReturnsAsync(new List<Translation> { translation } as IEnumerable<Translation>);

        _flashCardsRepoMock
            .Setup(r => r.AddAsync(It.IsAny<UserFlashCard>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _service = new UserFlashCardsService(
            _userWordRepoMock.Object,
            _translationRepoMock.Object,
            _flashCardsRepoMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        var dto = new RequestUserFlashCardDto { UserId = userId, Count = 1, Title = "test" };

        // Act
        var result = await _service.GenerateFromUserWordsAsync(dto, _ct);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Items);
        Assert.Single(result.Items);
        Assert.Equal("hello", result.Items[0].Word);
        Assert.Equal("hola", result.Items[0].Translation);
    }
}
