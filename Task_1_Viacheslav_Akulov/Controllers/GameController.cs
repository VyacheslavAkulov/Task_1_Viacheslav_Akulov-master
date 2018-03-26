using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using BusinessLogicLayer.Interfaces;
using Models;
using Newtonsoft.Json;

namespace Task1ViacheslavAkulovControllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult Details(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game == null)
            {
                return HttpNotFound();
            }

            var str = JsonConvert.SerializeObject(game);

            return Json(str, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetGames()
        {
            var games = _gameService.GetAll();

            var str = JsonConvert.SerializeObject(games);

            return Json(str, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(string gameJson)
        {

            var game = JsonConvert.DeserializeObject<Game>(gameJson);

            if (game == null)
            {
                return HttpNotFound();
            }

            _gameService.Create(game);

            return Json("Game created", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(string gameJson)
        {
            var game = JsonConvert.DeserializeObject<Game>(gameJson);

            if (game == null)
            {
                return HttpNotFound();
            }

            _gameService.Update(game);

            return Json($"Game { game.Key } updated", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string gameKey)
        {
            if (gameKey == null)
            {
                return HttpNotFound();
            }

            _gameService.Delete(gameKey);

            return Json($"Game {gameKey} removed", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game == null)
            {
                return HttpNotFound();
            }

            using (BinaryWriter writer = new BinaryWriter(System.IO.File.Open(Server.MapPath("~/Files/Game.dat"), FileMode.Create)))
            {
                writer.Write($"key:{game.Key} Name: {game.Name}");
            }

            return File(Server.MapPath("~/Files/Game.dat"), "application/dat", "Game.dat");

        }
    }
}