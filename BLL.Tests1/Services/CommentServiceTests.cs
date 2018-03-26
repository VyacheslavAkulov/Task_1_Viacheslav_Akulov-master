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
    using Assert = Xunit.Assert;

    // [TestClass]
    public class CommentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private ICommentService _commentService;

        public CommentServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _commentService = new CommentService(_unitOfWorkMock.Object);
        }

        [Fact]
        public void CommentServiceCreateWithNotNullComment()
        {
            _unitOfWorkMock.Setup(x => x.CommentRepository.Create(It.IsNotNull<Comment>()));
            _unitOfWorkMock.Setup(x => x.Commit());

            _commentService.Create(new Comment());

            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public void CommentServiceCreateWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => _commentService.Create(null));
        }

        [Fact]
        public void CommentServiceGetCommnetsByGameWithKey()
        {
            var expect = new List<Comment> { new Comment() };
            var game = new Game { Comments = expect };
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(It.IsAny<string>())).Returns(game);

            var actual = _commentService.GetCommnetsByGame(It.IsAny<string>());

            _unitOfWorkMock.VerifyAll();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void CommentServiceGetCommnetsByGameWithNull()
        {
            _unitOfWorkMock.Setup(x => x.GameRepository.Get(null));

            Assert.Throws<NullReferenceException>(() => _commentService.GetCommnetsByGame(null));
        }
    }
}