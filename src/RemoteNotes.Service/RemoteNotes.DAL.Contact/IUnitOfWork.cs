namespace RemoteNotes.DAL.Contact
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }

        INoteRepository NoteRepository { get; }

        IUserRepository UserRepository { get; }

        void Commit();
    }
}