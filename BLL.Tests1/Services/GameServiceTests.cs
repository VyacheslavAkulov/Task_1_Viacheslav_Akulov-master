using System;
using System.Collections.Generic;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfases;
using Models;
using Moq;
using Xunit;

namespace BusinessLogicLayer.Tests.Services
{
    using Assert = Assert;

    public class GameServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IGameService _gameService;

        public GameServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gameService = new GameService(_unitOfWorkMock.Object);
        }

        [Fact]
        public void GameServiceCreateWithGame_SuccesfulyComplete()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Create(It.IsNotNull<Game>()));
            _unitOfWorkMock.Setup(x => x.Commit());

            _gameService.Create(new Game());

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameService_Create_WithNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _gameService.Create(null));
        }

        [Fact]
        public void GameService_Delete_WithAnyString_SuccesfulyComplete()
        {
            string key = "string";
            _unitOfWorkMock.Setup(x => x.GameRepository.Delete(key));
            _unitOfWorkMock.Setup(x => x.Commit());
            _gameService.Delete(key);

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameService_Delete_WithNull_KeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _gameService.Delete(null));
        }

        [Fact]
        public void GameService_GetAll_Games()
        {
            var expect = new List<Game> { new Game() };
            _unitOfWorkMock.Setup(x => x.GameRepository.GetAll()).Returns(expect);

            var actual = _gameService.GetAll();

            _unitOfWorkMock.VerifyAll();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameService_Get_WithAnyKey_SuccesfulyComplete()
        {
            var expect = new Game();
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(It.IsAny<string>())).Returns(expect);

            var actual = _gameService.Get(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameService_Get_WithNull_KeyNotFoundException()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.Get(null));
        }

        [Fact]
        public void GameService_Update_WithGame_SuccesfulyComplete()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Update(It.IsNotNull<Game>()));
            _unitOfWorkMock.Setup(x => x.Commit());

            _gameService.Update(new Game());

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameService_Update_WithNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _gameService.Update(null));
        }

        [Fact]
        public void GameService_GetGamesByPlatformType_WithKey_Equal()
        {
            var expect = new List<Game> { new Game() };
            var game = new PlatformType { Games = expect };
            _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.Get(It.IsAny<string>())).Returns(game);

            var actual = _gameService.GetGamesByPlatformType(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();

            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameService_GetGamesByPlatformType_WithNull_KeyNotFoundException()
        {
            _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.GetGamesByPlatformType(null));
        }

        [Fact]
        public void GameService_GetGamesByGenre_WithKey_Equal()
        {
            var expect = new List<Game> { new Game() };
            var game = new Genre { Games = expect };
            _unitOfWorkMock.Setup(x => x.GenreRepository.Get(It.IsAny<string>())).Returns(game);

            var actual = _gameService.GetGamesByGenre(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();

            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameService_GetGamesByGenre_WithNull_KeyNotFoundException()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.GetGamesByGenre(null));
        }
    }
}