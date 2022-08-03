using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Admin.UserManager
{
	[Microsoft.AspNetCore.Authorization.Authorize
		(Roles = Infrastructure.Constant.Role.Admin)]
	public class IndexModel : Infrastructure.BasePageModelWithDatabase
	{
		#region Constructor(s)
		public IndexModel
			(Data.DatabaseContext databaseContext,
			Microsoft.Extensions.Logging.ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
		{
			Logger = logger;

			ViewModel = new();
		}
		#endregion /Constructor(s)

		#region Property(ies)
		// **********
		private Microsoft.Extensions.Logging.ILogger<IndexModel> Logger { get; }
		// **********

		// **********
		public ViewModels.Shared.PaginationWithDataViewModel
			<ViewModels.Pages.Admin.UserManager.GetUserItemViewModel> ViewModel
		{ get; private set; }
		// **********
		#endregion /Property(ies)

		// TO DO: Let Users Select Page Size
		#region OnGet
		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.IActionResult>
			OnGetAsync(int pageSize = 10, int pageNumber = 1)
		{
			try
			{
				if (pageNumber > 0)
				{
					// **************************************************
					ViewModel = new ViewModels.Shared.PaginationWithDataViewModel
						<ViewModels.Pages.Admin.UserManager.GetUserItemViewModel>
					{
						PageInformation = new()
						{
							PageSize = pageSize,
							PageNumber = pageNumber,
						},
					};
					// **************************************************

					// **************************************************
					ViewModel.PageInformation.TotalCount =
						await DatabaseContext.Users.CountAsync();

					if (ViewModel.PageInformation.TotalCount > 0)
					{
						ViewModel.Data =
							await DatabaseContext.Users
							.OrderBy(current => current.Ordering)
							.Skip((pageNumber - 1) * pageSize)
							.Take(pageSize)
							.Select(current => new ViewModels.Pages.Admin.UserManager.GetUserItemViewModel
							{
								Id = current.Id,

								Username = current.Username,
								EmailAddress = current.EmailAddress,

								IsActive = current.IsActive,
								IsUndeletable = current.IsUndeletable,

								InsertDateTime = current.InsertDateTime,
								UpdateDateTime = current.UpdateDateTime,
							})
							.AsNoTracking()
							.ToListAsync()
							;
					}
					// **************************************************
				}

				//if ((ViewModel == null) || (ViewModel.Data == null) || (ViewModel.Data.Any() == false))
				//{
				//	return RedirectToPage(pageName: "/admin/usermanager/create");
				//}
			}
			catch (System.Exception ex)
			{
				Logger.LogError(message: ex.Message);

				//System.Console.WriteLine(value: ex.Message);

				AddToastError(message: Resources.Messages.Errors.UnexpectedError);
			}
			finally
			{
				await DisposeDatabaseContextAsync();
			}

			return Page();
		}
		#endregion /OnGet
	}
}