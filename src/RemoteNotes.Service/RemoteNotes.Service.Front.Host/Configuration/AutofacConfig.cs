using System.Configuration;
using Autofac;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Core.Entities;
using RemoteNotes.DAL.Repositories;
using RemoteNotes.Service.Front.Contract;

namespace RemoteNotes.Service.Front.Host.Configuration
{
    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            // DAL dependencies
            var connectionString = ConfigurationManager.ConnectionStrings["MsSqlConnectionConnectionString"].ConnectionString;
            var options = new DbContextOptionsBuilder<RemoteNotesDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            containerBuilder.RegisterType<RemoteNotesDbContext>().As<DbContext>()
                .WithParameter(new TypedParameter(typeof(DbContextOptions<RemoteNotesDbContext>), options));
            containerBuilder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterType<AccountRepository>().As<IRepository<Account>>();
            containerBuilder.RegisterType<AccountRepository>().As<IAccountRepository>();
            containerBuilder.RegisterType<NoteRepository>().As<IRepository<Note>>();
            containerBuilder.RegisterType<NoteRepository>().As<INoteRepository>();
            containerBuilder.RegisterType<UserRepository>().As<IRepository<User>>();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // BLL dependencies

            // WCF
            containerBuilder.RegisterType<RemoteNotesService>().As<IRemoteNotesService>();

            return containerBuilder.Build();
        }
    }
}