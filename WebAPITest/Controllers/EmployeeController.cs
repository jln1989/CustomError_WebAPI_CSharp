namespace WebAPITest.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using WebAPITest.CustomError;

    /// <summary>
    /// EmployeeController Class
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EmployeeController : ApiController
    {

        EntityModel context = new EntityModel();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of Employees</returns>
        [ActionName("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {

            return Ok(context.Employees);

        }

        /// <summary>
        /// Read Operation
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>Particular Employee Detail</returns>
        [ActionName("GetById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            Employee data = context.Employees.Where(k => k.Id == id).FirstOrDefault();

            if (data == null)
            {
                //string p = data.EmailAddress;
                string message = string.Format("No Employee found with ID = {0}", id);
                CustomErrorClass theError = new CustomErrorClass()
                {
                    Code = "1000",
                    Message = message
                };

                return new CustomErrorResult(theError, Request);
            }
            return Ok(data); ;
        }

        /// <summary>
        /// Adds the department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>Added Department</returns>
        [HttpPost]
        [ActionName("AddDepartment")]
        public IHttpActionResult AddDepartment([FromBody] Department department)
        {
            if (department == null)

            {
                CustomErrorClass theError = new CustomErrorClass()
                {
                    Code = "1001",
                    Message = "Request error. Please check request"
                };

                return new CustomErrorResult(theError, Request);
            }
            var newDepartment = context.Departments.Add(department);
            if (newDepartment == null)
            {
                string message = "Error in adding department";
                CustomErrorClass theError = new CustomErrorClass()
                {
                    Code = "1002",
                    Message = message
                };
                return new CustomErrorResult(theError, Request);
            }
            context.SaveChanges();
            return Ok(newDepartment);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}
