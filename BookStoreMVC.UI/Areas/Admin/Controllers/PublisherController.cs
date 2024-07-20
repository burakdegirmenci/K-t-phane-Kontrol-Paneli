namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
	public class PublisherController : AdminBaseController
	{
		private readonly IPublisherServices publisherServices;

		public PublisherController(IPublisherServices publisherServices)
		{
			this.publisherServices = publisherServices;
		}

		public async Task<IActionResult> Index()
		{
			var result = await publisherServices.GetAllAsync();
            var publisherListVMs = result.Data.Adapt<List<AdminPublisherListVM>>();
            if (!result.IsSuccess)
			{
				NotifiyError(result.Messages);
				return View(publisherListVMs);
			}
			
			NotifiySuccess(result.Messages);
			return View(publisherListVMs);
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AdminPublisherCreateVM model)
		{
			var result = await publisherServices.CreateAsync(model.Adapt<PublisherCreateDTO>());
			if (!result.IsSuccess)
			{
				return View(model);
			}
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
		}


		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await publisherServices.DeleteAsync(id);
			if (!result.IsSuccess)
			{
				NotifiyError(result.Messages);
				return RedirectToAction("Index");
			}
			NotifiySuccess(result.Messages);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(Guid id)
		{
			var result = await publisherServices.GetByIdAsync(id);
			var publisherUpdateVm = result.Data.Adapt<AdminPublisherUpdateVM>();
			return View(publisherUpdateVm);

		}

		[HttpPost]
		public async Task<IActionResult> Update(AdminPublisherUpdateVM model)
		{
			var result = await publisherServices.UpdateAsync(model.Adapt<PublisherUpdateDTO>());
			if (!result.IsSuccess)
			{
                NotifiyError(result.Messages);
                //await Console.Out.WriteAsync(result.Messages);
                return RedirectToAction("Index");
			}
			NotifiySuccess(result.Messages);

			return RedirectToAction("Index");
		}
	}
}
