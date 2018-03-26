using System;
using System.Collections.Generic;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfases;
using Models;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Game service interface
    /// </summary>
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Game> GetAll()
        {
            Logger.Log.Info("Getting a list of games");

            return unitOfWork.GameRepository.GetAll();
        }

        /// <summary>
        /// Get game by key
        /// </summary>
        /// <param name="key">Game key</param>
        /// <returns>Game</returns>
        public Game Get(object key)
        {
            Logger.Log.Info($"Getting game details { key }");

            return unitOfWork.GameRepository.Get(key) ?? throw new KeyNotFoundException($"Game {key} not found");
        }

        /// <summary>
        /// Create new game
        /// </summary>
        /// <param name="item">New game</param>
        public void Create(Game item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item)); ;
            }

            Logger.Log.Info($"Create new game {item.Key},{item.Name}");

            unitOfWork.GameRepository.Create(item);

            unitOfWork.Commit();
        }

        /// <summary>
        /// Updating game
        /// </summary>
        /// <param name="item">Updating item</param>
        public void Update(Game item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Logger.Log.Info($"Updating game {item.Key}");

            unitOfWork.GameRepository.Update(item);

            unitOfWork.Commit();

        }

        /// <summary>
        /// Delete game by key
        /// </summary>
        /// <param name="key">Game key</param>
        public void Delete(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Logger.Log.Info($"Delete the game{key}");

            unitOfWork.GameRepository.Delete(key);

            unitOfWork.Commit();
        }

        /// <summary>
        /// Get all games by platform type
        /// </summary>
        /// <param name="platformType">Platform type</param>
        /// <returns>Games by platform type</returns>
        public IEnumerable<Game> GetGamesByPlatformType(string platformType)
        {
            Logger.Log.Info($"Get games by platform type {platformType}");

            return unitOfWork.PlatformTypeRepository.Get(platformType)?.Games 
                ?? throw new KeyNotFoundException($"Platform {platformType} not found");
        }

        /// <summary>
        /// Get all games by genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>>Games by genre</returns>
        public IEnumerable<Game> GetGamesByGenre(string genre)
        {
            Logger.Log.Info($"Get games by genre {genre}");

            return unitOfWork.GenreRepository?.Get(genre).Games 
                ?? throw new KeyNotFoundException($"Genre {genre} not found");
        }
    }
}