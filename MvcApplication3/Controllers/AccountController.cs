using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication3.Filters;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers {
	[Authorize]
	[InitializeSimpleMembership]
	public class AccountController : Controller {

		[AllowAnonymous]
		public ActionResult Login(string returnUrl) {
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl) {
			if (ModelState.IsValid){
				if(!WebSecurity.UserExists(model.UserName)) {
					ModelState.AddModelError("", "存在しないユーザー名です。");
				} else if (!WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe)) {
					ModelState.AddModelError("", "パスワードが違います。");
				} else {
					return RedirectToLocal(returnUrl);
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOut() {
			WebSecurity.Logout();

			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public ActionResult Register() {
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel model) {
			if (ModelState.IsValid) {
				try {
					WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
					WebSecurity.Login(model.UserName, model.Password);
					return RedirectToAction("Index", "Home");
				} catch (MembershipCreateUserException e) {
					ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
				}
			}
			return View(model);
		}


		#region ヘルパー
		private ActionResult RedirectToLocal(string returnUrl) {
			if (Url.IsLocalUrl(returnUrl)) {
				return Redirect(returnUrl);
			} else {
				return RedirectToAction("Index", "Home");
			}
		}

		public enum ManageMessageId {
			ChangePasswordSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
		}

		private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
			switch (createStatus) {
				case MembershipCreateStatus.DuplicateUserName:
					return "このユーザー名は既に存在します。別のユーザー名を入力してください。";

				case MembershipCreateStatus.DuplicateEmail:
					return "その電子メール アドレスのユーザー名は既に存在します。別の電子メール アドレスを入力してください。";

				case MembershipCreateStatus.InvalidPassword:
					return "指定されたパスワードは無効です。有効なパスワードの値を入力してください。";

				case MembershipCreateStatus.InvalidEmail:
					return "指定された電子メール アドレスは無効です。値を確認してやり直してください。";

				case MembershipCreateStatus.InvalidAnswer:
					return "パスワードの回復用に指定された回答が無効です。値を確認してやり直してください。";

				case MembershipCreateStatus.InvalidQuestion:
					return "パスワードの回復用に指定された質問が無効です。値を確認してやり直してください。";

				case MembershipCreateStatus.InvalidUserName:
					return "指定されたユーザー名は無効です。値を確認してやり直してください。";

				case MembershipCreateStatus.ProviderError:
					return "認証プロバイダーからエラーが返されました。入力を確認してやり直してください。問題が解決しない場合は、システム管理者に連絡してください。";

				case MembershipCreateStatus.UserRejected:
					return "ユーザーの作成要求が取り消されました。入力を確認してやり直してください。問題が解決しない場合は、システム管理者に連絡してください。";

				default:
					return "不明なエラーが発生しました。入力を確認してやり直してください。問題が解決しない場合は、システム管理者に連絡してください。";
			}
		}
		#endregion
	}
}
