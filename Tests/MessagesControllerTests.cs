using Bump.Controllers;
using Data.Repo;
using Entities;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class MessagesControllerTests
    {
        private readonly Mock<IMessageRepo> _messageRepo = new Mock<IMessageRepo>();
        private readonly Message _message = It.IsAny<Message>();
        private MessageController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new MessageController(_messageRepo.Object);
        }

        [Test]
        public void DeleteCalledTest()
        {
            _controller.DeleteMessage(1);
            _messageRepo.Verify(repo => repo.DeleteMessage(1));
        }

        [Test]
        public void UpdateCalledTest()
        {
            _controller.UpdatePost(new Bump.Models.Message(_message));
            _messageRepo.Verify(repo => repo.UpdateMessage(_message.Id, _message.Content, _message.Media));
        }

        [Test]
        public void CreateCalledTest()
        {
            _controller.CreateMessage(_message.Theme);
            _messageRepo.Verify(repo => repo.CreateMessage(_message));
        }
    }
}