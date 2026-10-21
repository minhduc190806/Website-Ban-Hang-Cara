using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MinhDucWeb.Areas.Admin
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var role = session.GetString("AUTH_ROLE");
            var user = session.GetString("AUTH_USER");

            // Kiểm tra: Nếu chưa đăng nhập HOẶC quyền không phải Admin
            if (string.IsNullOrEmpty(user) || role != "Admin")
            {
                var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;

                // Nếu là request Ajax thì trả về 401 Unauthorized
                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Lưu thông báo cảnh báo ra TempData
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.TempData["ErrorMessage"] = "CẢNH BÁO: Bạn chưa đăng nhập tài khoản Quản trị viên (Admin)! Vui lòng đăng nhập với tài khoản admin / admin123 để truy cập.";
                }

                // Chuyển hướng người dùng về trang Đăng nhập kèm returnUrl
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "", returnUrl = returnUrl });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
