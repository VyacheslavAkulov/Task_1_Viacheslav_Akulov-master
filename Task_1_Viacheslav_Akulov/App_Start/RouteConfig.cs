using System.Web.Mvc;
using System.Web.Routing;

namespace Task1ViacheslavAkulov
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewGame", url: "games/new",
                defaults: new { controller = "Game", action = "Create" }
            );

            routes.MapRoute(
                name: "NewComment", url: "game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "NewComment" }
            );

            routes.MapRoute(
                name: "GameComments", url: "game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "GetCommnetsByGame" }
            );

            routes.MapRoute(
                name: "DownloadGame", url: "game/{gamekey}/download",
                defaults: new { controller = "Game", action = "Download" }
            );

            routes.MapRoute(
                name: "GameActionGameKey", url: "games/{action}/{gamekey}",
                defaults: new { controller = "Game" }
            );

            routes.MapRoute(
                name: "GameDetails",
                url: "game/{key}",
                defaults: new { controller = "Game", action = "Details" }
                );

            routes.MapRoute(
                name: "Games",
                url: "games",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "GetGames", id = UrlParameter.Optional }
            );
        }
    }
}
