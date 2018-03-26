using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Interfaces;
using Models;
using Moq;
using Newtonsoft.Json;
using Task1ViacheslavAkulovControllers;
using Xunit;

namespace Task1ViacheslavAkulov.Tests.Controllers
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _commentService;

        private readonly CommentController _commentController;

        public CommentControllerTests()
        {
            _commentService = new Mock<ICommentService>();

            _commentController = new CommentController(_commentService.Object);
        }

        [Fact]
        public void CommentControllerTests_Create_WithSimpleJsonGame_Complete()
        {
            var comment = new Comment();
            var commentJson = JsonConvert.SerializeObject(comment);
            var expect = "Comment created";
            _commentService.Setup(x => x.Create(comment));

            JsonResult actual = _commentController.NewComment(commentJson) as JsonResult;

            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void CommentControllerTests_Create_WithNull_HttpNotFound()
        {
            int statusCode = 404;

            HttpStatusCodeResult actual = _commentController.NewComment(string.Empty) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }

        [Fact]
        public void CommentControllerTests_GetCommnetsByGame_WithSimpleKey_Complete()
        {
            var comment = new Comment();
            var comments = new List<Comment> { comment };
            var expect = JsonConvert.SerializeObject(comments);
            _commentService.Setup(x => x.GetCommnetsByGame(It.IsAny<string>())).Returns(comments);

            JsonResult actual = _commentController.GetCommnetsByGame(It.IsAny<string>()) as JsonResult;

            _commentService.VerifyAll();
            Assert.Equal(expect, actual.Data.ToString());
        }

        [Fact]
        public void CommentControllerTests_GetCommnetsByGame_WithNullKey_HttpNotFound()
        {
            int statusCode = 404;
            _commentService.Setup(x => x.GetCommnetsByGame(null));

            HttpStatusCodeResult actual = _commentController.GetCommnetsByGame(null) as HttpStatusCodeResult;

            Assert.Equal(statusCode, actual.StatusCode);
        }
    }
}