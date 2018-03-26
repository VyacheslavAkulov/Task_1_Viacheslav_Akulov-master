using System;
using DataAccessLayer;
using DataAccessLayer.Interfases;
using Models;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Providing access to repositories
    /// Defining the general context for repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly Lazy<GenericRepository<Game>> _lazyGameRepository;
        private readonly Lazy<GenericRepository<Genre>> _lazyGenreRepository;
        private readonly Lazy<GenericRepository<Comment>> _lazyCommentRepository;
        private readonly Lazy<GenericRepository<PlatformType>> _lazyPlatformTypeRepository;


        public UnitOfWork(string connection)
        {
            _context = new DatabaseContext(connection);

            _lazyGameRepository = new Lazy<GenericRepository<Game>>(
                () => new GenericRepository<Game>(_context));
            _lazyGenreRepository = new Lazy<GenericRepository<Genre>>(
                () => new GenericRepository<Genre>(_context));
            _lazyCommentRepository = new Lazy<GenericRepository<Comment>>(
                () => new GenericRepository<Comment>(_context));
            _lazyPlatformTypeRepository = new Lazy<GenericRepository<PlatformType>>(
                () => new GenericRepository<PlatformType>(_context));

        }


        public IGenericRepository<Game> GameRepository => _lazyGameRepository.Value;


        public IGenericRepository<Genre> GenreRepository => _lazyGenreRepository.Value;

        /// <summary>
        /// Getter for Comment repository
        /// </summary>
        public IGenericRepository<Comment> CommentRepository => _lazyCommentRepository.Value;

        /// <summary>
        /// Getter for PlatformType repository
        /// </summary>
        public IGenericRepository<PlatformType> PlatformTypeRepository => _lazyPlatformTypeRepository.Value;

        /// <summary>
        /// Commit database changes
        /// </summary>
        public void Commit() => _context.SaveChanges();
    }
}