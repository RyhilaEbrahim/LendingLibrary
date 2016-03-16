using System.Web.Mvc;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.DB;
using Chillisoft.LendingLibrary.DB.Repositories;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IBorrowerRepository _borrowerRepository;

        

        public ImageController(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public ImageController() : this(new BorrowerRepository(new LendingLibraryDbContext()))
        {
        }

        public ActionResult Show(int id)
        {
            var borrower = _borrowerRepository.Get(id);
            var photo = borrower.Photo;

            var contentType = borrower.ContentType;

            return File(photo, contentType);
        }
    }
}