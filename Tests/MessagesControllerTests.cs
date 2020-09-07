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
            _messageRepo.Verify(repo => repo.DeleteMessage(It.IsAny<int>()));
        }

        [Test]
        public void UpdateCalledTest()
        {
            _controller.UpdateMessage(_message);
            _messageRepo.Verify(repo => repo.UpdateMessage(_message));
        }

        [Test]
        public void CreateCalledTest()
        {
            _controller.CreateMessage(_message);
            _messageRepo.Verify(repo => repo.CreateMessage(_message));
        }
    }
}