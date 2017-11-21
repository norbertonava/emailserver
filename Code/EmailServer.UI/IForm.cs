namespace EmailServer.UI
{
    public interface IForm
    {
        void Start();
        void Pause();
        void Save();
        void EnableToolbar(bool save, bool start, bool pause);
    }
}
