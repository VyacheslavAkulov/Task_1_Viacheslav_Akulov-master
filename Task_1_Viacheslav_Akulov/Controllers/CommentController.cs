using System.Text;
using System.Web.Mvc;
using BusinessLogicLayer.Interfaces;
using Models;
using Newtonsoft.Json;

namespace Task1ViacheslavAkulovControllers
{
	public class CommentController : Controller
	{
		private readonly ICommentService comment_Service;

		public CommentController(ICommentService commentService)
		{
			this.comment_Service = commentService;
		}

		[HttpPost]
		public ActionResult NewComment(string commentJson)
		{
			var comment = JsonConvert.DeserializeObject<Comment>(commentJson);

			if (comment == null)
			{
				return HttpNotFound();
			}
			comment_Service.Create(comment);

			return Json("Comment created", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetCommnetsByGame(string gamekey)
		{
			var game = comment_Service.GetCommnetsByGame(gamekey);

			if (game == null)
			{
				return HttpNotFound();
			}
			var str = JsonConvert.SerializeObject(game);

			return Json(str, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
		}
	}
}
