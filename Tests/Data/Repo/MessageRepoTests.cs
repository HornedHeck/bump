using Data.Api.Live;
using Data.Api.Local;
using Data.Repo;
using Entities;
using Moq;
using NUnit.Framework;

namespace Tests.Data.Repo {

    public class MessageRepoTests {

        private readonly Mock< ILiveUpdate > Live = new Mock< ILiveUpdate >();

        private readonly Mock< ILocalApi > LocalApi = new Mock< ILocalApi >();

        private MessageRepo Repo;

        [SetUp]
        public void SetUp() {
            Repo = new MessageRepo( LocalApi.Object , Live.Object );
        }

        [Test]
        public void CreateMessageSuccess() {
            var message = new Message();

            Repo.CreateMessage( message );

            LocalApi.Verify( api => api.CreateMessage( message ) );
            Live.Verify( live => live.NotifyMessageCreated( message ) );
        }

        [Test]
        public void DeleteMessage() {
            Repo.DeleteMessage( 1 );

            LocalApi.Verify( api => api.DeleteMessage( 1 ) );
        }

        [Test]
        public void UpdateMessageSuccess() {
            const int Id = 1;
            const string Content = "Content";
            var Media = new[] {1L};
            var Message = new Message {
                Content = Content ,
                Id = Id ,
                Media = Media
            };

            LocalApi
                .Setup( api => api.GetMessage( Id ) )
                .Returns( Message );

            Repo.UpdateMessage( Id , Content , Media );

            LocalApi.Verify( api => api.UpdateMessage( Message ) );
        }

        [Test]
        public void GetMessageSuccess() {
            Repo.GetMessage( 1 );

            LocalApi.Verify( api => api.GetMessage( 1 ) );
        }

        [Test]
        public void VoteUp() {
            const int MessageId = 1;
            var Vote = new Vote {UserId = "1"};

            Repo.VoteUp( MessageId , Vote );

            LocalApi.Verify( api => api.VoteUp( MessageId , Vote ) );
        }

    }

}