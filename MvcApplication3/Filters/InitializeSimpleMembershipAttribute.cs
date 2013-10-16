using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication3.Models;

namespace MvcApplication3.Filters {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute {
		private static SimpleMembershipInitializer _initializer;
		private static object _initializerLock = new object();
		private static bool _isInitialized;

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
		}

		private class SimpleMembershipInitializer {
			public SimpleMembershipInitializer() {
				Database.SetInitializer<UsersContext>(null);

				try {
					using (var context = new UsersContext()) {
						if (!context.Database.Exists()) {
							((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
						}
					}
					WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
				} catch (Exception ex) {
					throw new InvalidOperationException("データベースの初期化エラー", ex);
				}
			}
		}
	}
}
