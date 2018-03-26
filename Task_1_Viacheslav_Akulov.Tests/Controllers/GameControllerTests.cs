using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using BusinessLogicLayer.Interfaces;
using Models;
using Moq;
using Newtonsoft.Json;
using Task1ViacheslavAkulovControllers;
using Xunit;

namespace Task1ViacheslavAkulov.Tests.Controllers
{
	public class GameControllerTests
    {
        private readonly Mock<IGameService> _gameService;

        private readonly GameController _gameController;

        public GameControllerTests()
        {
            _gameService = new Mock<IGameService>();

            _gameController = new GameController(_gameService.Object);
        }

        [Fact]
        public void GameControllerTests_Details_WithSimpleKey_SuccesfullyComplete()
        {
            var game = new Game();
            var expect = JsonConvert.SerializeObject(game);
            _gameService.Setup(x => x.Get(It.IsAny<string>())).Returns(game);
          
            JsonResult actual = _gameController.Details(It.IsAny<string>()) as JsonResult;

            _gameService.VerifyAll();
            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void GameControllerTests_Details_WithNullKey_HttpNotFound()
        {
            int statusCode = 404;
            _gameService.Setup(x => x.Get(null));

            HttpStatusCodeResult actual = _gameController.Details(It.IsAny<string>()) as HttpStatusCodeResult;

            _gameService.VerifyAll();
            Assert.Equal(statusCode, actual.StatusCode);
        }

        [Fact]
        public void GameControllerTests_GetAll_ReturnGameCollection()
        {
            var games = new List<Game> {new Game()};
            var expect = JsonConvert.SerializeObject(games);
            _gameService.Setup(x => x.GetAll()).Returns(games);

            var actual = _gameController.GetGames() as JsonResult;

            Assert.Equal(expect, actual.Data);
        }

        [Fact]
        public void GameControllerTests_Create_WithSimpleJsonGame_SuccesfullyComplete()
        {
            var game = new Game();
            var gameJson = JsonConvert.SerializeObject(game);
            var expect = "Game created";
            _gameService.Setup(x => x.Create(game));

            JsonResult actual = _gameController.Create(gameJson) as JsonResult;

            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void GameControllerTests_Create_WithNull_HttpNotFound()
        {
            int statusCode = 404;
            
            HttpStatusCodeResult actual = _gameController.Create(string.Empty) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }

        [Fact]
        public void GameControllerTests_Update_WithSimpleJsonGame_SuccesfullyComplete()
        {
            var game = new Game {Key = "gameKey" };
            var gameJson = JsonConvert.SerializeObject(game);
            var expect = "Game gameKey updated";
            _gameService.Setup(x => x.Update(game));

            JsonResult actual = _gameController.Update(gameJson) as JsonResult;

            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void GameControllerTests_Update_WithNull_HttpNotFound()
        {
            int statusCode = 404;

            HttpStatusCodeResult actual = _gameController.Update(string.Empty) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }

        [Fact]
        public void GameControllerTests_Remove_WithSimpleJsonGame_SuccesfullyComplete()
        {
            var gameKey = "gameKey";
            var expect = "Game gameKey removed";
            _gameService.Setup(x => x.Delete(gameKey));
            JsonResult actual = _gameController.Remove(gameKey) as JsonResult;

            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void GameControllerTests_Remove_WithNull_HttpNotFound()
        {
            int statusCode = 404;

            HttpStatusCodeResult actual = _gameController.Remove(null) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }

        //[Fact]
        //public void GameControllerTestsDownloadWithGameKey()
        //{
        //    string FileName = "Game.dat";
        //    string gameKey = "gameKey";
        //    var game = new Game();
        //    _gameService.Setup(x => x.Get(gameKey)).Returns(game);

        //    FilePathResult actual = _gameController.Download(gameKey) as FilePathResult;

        //    _gameService.VerifyAll();
        //    Assert.Equal(FileName, actual.FileName);
        //}

        [Fact]
        public void GameControllerTests_Download_WithNull_HttpNotFound()
        {
            int statusCode = 404;

            HttpStatusCodeResult actual = _gameController.Download(null) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }
    }
}