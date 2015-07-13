namespace CQRSProfile
{
    public interface IEventBus
    {
        void Publish(object @event);
    }

    public interface IEventHandler<in T>
    {
        void Handle(T @event);
    }

    public class NameChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PhotoUrlChanged
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class FriendAdded
    {
        public int Id { get; set; }
        public int FriendId { get; set; }
    }
}