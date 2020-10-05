using Bump.Auth;
using Bump.Controllers;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Controllers
{
    public class MessagesControllerTests
    {
        private readonly Mock<IMessageRepo> _messageRepo = new Mock<IMessageRepo>();
        private readonly UserManager<BumpUser> _userManager = new MockUserManager();

        private readonly MessageVM _message = new MessageVM
        {
            Id = 1,
            Author = new BumpUser
            {
                Id = "1",
                UserName = "UserName"
            },
            Content = "Content",
            Theme = 1
        };

        private MessageController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new MessageController(_messageRepo.Object, _userManager);
        }

        [Test]
        public void DeleteCalledTest()
        {
            var message = new Message(
                id: 1,
                author: new User("1"),
                content: "Content",
                media: new long[0],
                theme: 1
            );
            _messageRepo
                .Setup(repo => repo.GetMessage(message.Id))
                .Returns(message);
            
            _controller.DeleteMessage(message.Id);
            _messageRepo.Verify(repo => repo.DeleteMessage(It.IsAny<int>()));
        }

        [Test]
        public void UpdateCalledTest()
        {
            _controller.UpdatePost(_message);
            _messageRepo.Verify(repo => repo.UpdateMessage(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<long[]>()
            ));
        }

        [Test]
        public void CreateCalledTest()
        {
            _controller.CreatePost(_message);
            _messageRepo.Verify(repo => repo.CreateMessage(It.IsAny<Message>()));
        }
    }
}