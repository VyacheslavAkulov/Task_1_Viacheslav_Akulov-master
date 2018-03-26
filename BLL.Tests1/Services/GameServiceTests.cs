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
        public void GameServiceCreateWithGame()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Create(It.IsNotNull<Game>()));
            _unitOfWorkMock.Setup(x => x.Commit());

            _gameService.Create(new Game());

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameServiceCreateWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => _gameService.Create(null));
        }

        [Fact]
        public void GameServiceDeleteWithAnyString()
        {
            string key = "string";
            _unitOfWorkMock.Setup(x => x.GameRepository.Delete(key));
            _unitOfWorkMock.Setup(x => x.Commit());
            _gameService.Delete(key);

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameServiceDeleteWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => _gameService.Delete(null));
        }

        [Fact]
        public void GameServiceGetAllTest()
        {
            var expect = new List<Game> { new Game() };
            _unitOfWorkMock.Setup(x => x.GameRepository.GetAll()).Returns(expect);

            var actual = _gameService.GetAll();

            _unitOfWorkMock.VerifyAll();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameServiceGetWithAnyKey()
        {
            var expect = new Game();
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(It.IsAny<string>())).Returns(expect);

            var actual = _gameService.Get(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameServiceGetWithNull()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.Get(null));
        }

        [Fact]
        public void GameServiceUpdateWithGame()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Update(It.IsNotNull<Game>()));
            _unitOfWorkMock.Setup(x => x.Commit());

            _gameService.Update(new Game());

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void GameServiceUpdateWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => _gameService.Update(null));
        }

        [Fact]
        public void GameServiceGetGamesByPlatformTypeWithKey()
        {
            var expect = new List<Game> { new Game() };
            var game = new PlatformType { Games = expect };
            _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.Get(It.IsAny<string>())).Returns(game);

            var actual = _gameService.GetGamesByPlatformType(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();

            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameServiceGetGamesByPlatformTypeWithNull()
        {
            _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.GetGamesByPlatformType(null));
        }

        [Fact]
        public void GameServiceGetGamesByGenreWithKey()
        {
            var expect = new List<Game> { new Game() };
            var game = new Genre { Games = expect };
            _unitOfWorkMock.Setup(x => x.GenreRepository.Get(It.IsAny<string>())).Returns(game);

            var actual = _gameService.GetGamesByGenre(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();

            Assert.Equal(expect, actual);
        }

        [Fact]
        public void GameServiceGetGetGamesByGenreWithNull()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(null));

            Assert.Throws<KeyNotFoundException>(() => _gameService.GetGamesByGenre(null));
        }
    }
}