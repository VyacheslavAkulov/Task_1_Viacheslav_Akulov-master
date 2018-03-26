using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using Xunit;

namespace Task1ViacheslavAkulov.Tests.Routes
{
    public class RouteTests
    {
        public RouteTests()
        {
            RouteTable.Routes.Clear();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        [Fact]
        public void RouteTest_NewGameRoute_GameCreate()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/new");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Create", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_NewCommentRoute_CommnetKeyNewComment()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/Key/newcomment");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Comment", routeData.Values["Controller"]);
            Assert.Equal("Key", routeData.Values["gamekey"]);
            Assert.Equal("NewComment", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_GameCommentsRoute_CommentKeyGetCommnetsByGame()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/Key/comments");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Comment", routeData.Values["Controller"]);
            Assert.Equal("Key", routeData.Values["gamekey"]);
            Assert.Equal("GetCommnetsByGame", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTestGameDownloadGameRoute_GameKeyDownload()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/Key/download");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Key", routeData.Values["gamekey"]);
            Assert.Equal("Download", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTestGame_GameActinGameKeyRoute_GameRemoveKey()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/Remove/Key");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Remove", routeData.Values["action"]);
            Assert.Equal("Key", routeData.Values["gamekey"]);
        }

        [Fact]
        public void RouteTest_GameDetailsRoute_GameKey()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/Key");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Key", routeData.Values["key"]);
            Assert.Equal("Details", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_GamesRoute_Games()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/games");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("GetGames", routeData.Values["action"]);
        }

        [Fact]
        public void RouteTest_DefaultRoute_GameUpdateId()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns("~/Game/Update/Id");
            RouteData routeData = RouteTable.Routes.GetRouteData(httpContextMock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Game", routeData.Values["Controller"]);
            Assert.Equal("Update", routeData.Values["action"]);
            Assert.Equal("Id", routeData.Values["id"]);
        }
    }
}
