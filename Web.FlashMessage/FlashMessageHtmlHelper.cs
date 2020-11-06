using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Vereyon.Web {
	/// <summary>
	/// Twitter Bootstrap based HTML renderer for Flash Messages rendering the messages as alerts.
	/// </summary>
	public static class FlashMessageHtmlHelper {

		/// <summary>
		/// Renders any queued flash messages as a Twitter Bootstrap alerta and returns the html code. 
		/// </summary>
		/// <param name="html"></param>
		/// /// <param name="dismissable">Indicates if the messages should be dismissable</param>
		/// <returns></returns>
		public static IHtmlString RenderFlashMessages(this HtmlHelper html, bool dismissable = true) {

			// Retrieve queued messages.
			var messages = FlashMessage.Retrieve(html.ViewContext.HttpContext);
			var output = "";

			foreach(var message in messages) {
				output += RenderFlashMessage(message, dismissable);
			}

			return html.Raw(output);
		}

		/// <summary>
		/// Renders the passed flash message as a Twitter Bootstrap alert component.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="dismissable">Indicates if this message should be dismissable</param>
		/// <returns></returns>
		public static string RenderFlashMessage(FlashMessageModel message, bool dismissable = true) {
			var cssClasses = message.Type.GetCssStyle();
			var icon = message.Type.GetIconBody();
			string result = $"<div class=\"alert alert-custom alert-notice alert-light-{cssClasses} fade show\" role=\"alert\">";
			result += icon;
			result += "<div class=\"alert-text font-weight-bold\">";

			if(!string.IsNullOrWhiteSpace(message.Title))
				result += "<strong>" + HttpUtility.HtmlEncode(message.Title) + "</strong> ";

			if(message.IsHtml)
				result += message.Message;
			else
				result += HttpUtility.HtmlEncode(message.Message);

			result += "</div>";

			if(dismissable) {
				result += "<div class=\"alert-close\">";
				result += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">";
				result += "<span aria-hidden=\"true\"><i class=\"ki ki-close\"></i></span>";
				result += "</button>";
				result += "</div>";
			}

			result += "</div>";

			return result;
		}

		/// <summary>
		/// Returns the Twitter bootstrap css style for the passed message type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private static string GetCssStyle(this FlashMessageType type) {
			switch(type) {
				case FlashMessageType.Danger:
					return "danger";
				default:
				case FlashMessageType.Info:
					return "info";
				case FlashMessageType.Warning:
					return "warning";
				case FlashMessageType.Confirmation:
					return "success";
			}
		}

		private static string GetIconBody(this FlashMessageType type) {
			var icon = type.GetIcon();
			var cssClasses = type.GetCssStyle();
			var result = "<div class=\"alert-icon\">";
			result += $"<span class=\"svg-icon svg-icon-3x svg-icon-{cssClasses}\">";
			result += "<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" width=\"24px\" height=\"24px\" viewBox=\"0 0 24 24\" version=\"1.1\">";
			result += "<g stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\">";
			result += "<rect x=\"0\" y=\"0\" width=\"24\" height=\"24\" />";
			result += icon;
			result += "</g>";
			result += "</svg>";
			result += "</span>";
			result += "</div>";

			return result;
		}

		private static string GetIcon(this FlashMessageType type) {
			var result = "";
			switch(type) {
				case FlashMessageType.Danger:
					result += "<circle fill=\"#000000\" opacity=\"0.3\" cx=\"12\" cy=\"12\" r=\"10\" />";
					result += "<path d=\"M12.0355339,10.6213203 L14.863961,7.79289322 C15.2544853,7.40236893 15.8876503,7.40236893 16.2781746,7.79289322 C16.6686989,8.18341751 16.6686989,8.81658249 16.2781746,9.20710678 L13.4497475,12.0355339 L16.2781746,14.863961 C16.6686989,15.2544853 16.6686989,15.8876503 16.2781746,16.2781746 C15.8876503,16.6686989 15.2544853,16.6686989 14.863961,16.2781746 L12.0355339,13.4497475 L9.20710678,16.2781746 C8.81658249,16.6686989 8.18341751,16.6686989 7.79289322,16.2781746 C7.40236893,15.8876503 7.40236893,15.2544853 7.79289322,14.863961 L10.6213203,12.0355339 L7.79289322,9.20710678 C7.40236893,8.81658249 7.40236893,8.18341751 7.79289322,7.79289322 C8.18341751,7.40236893 8.81658249,7.40236893 9.20710678,7.79289322 L12.0355339,10.6213203 Z\" fill=\"#000000\" />";
					break;
				default:
				case FlashMessageType.Info:
					result += "<circle fill=\"#000000\" opacity=\"0.3\" cx=\"12\" cy=\"12\" r=\"10\" />";
					result += "<rect fill=\"#000000\" x=\"11\" y=\"10\" width=\"2\" height=\"7\" rx=\"1\" />";
					result += "<rect fill=\"#000000\" x=\"11\" y=\"7\" width=\"2\" height=\"2\" rx=\"1\" />";
					break;
				case FlashMessageType.Warning:
					result += "<circle fill=\"#000000\" opacity=\"0.3\" cx=\"12\" cy=\"12\" r=\"10\" />";
					result += "<rect fill=\"#000000\" x=\"11\" y=\"7\" width=\"2\" height=\"8\" rx=\"1\" />";
					result += "<rect fill=\"#000000\" x=\"11\" y=\"16\" width=\"2\" height=\"2\" rx=\"1\" />";
					break;
				case FlashMessageType.Confirmation:
					result += "<circle fill=\"#000000\" opacity=\"0.3\" cx=\"12\" cy=\"12\" r=\"10\" />";
					result += "<path d=\"M16.7689447,7.81768175 C17.1457787,7.41393107 17.7785676,7.39211077 18.1823183,7.76894473 C18.5860689,8.1457787 18.6078892,8.77856757 18.2310553,9.18231825 L11.2310553,16.6823183 C10.8654446,17.0740439 10.2560456,17.107974 9.84920863,16.7592566 L6.34920863,13.7592566 C5.92988278,13.3998345 5.88132125,12.7685345 6.2407434,12.3492086 C6.60016555,11.9298828 7.23146553,11.8813212 7.65079137,12.2407434 L10.4229928,14.616916 L16.7689447,7.81768175 Z\" fill=\"#000000\" fill-rule=\"nonzero\" />";
					break;
			}
			return result;
		}

		/*
         * http://forums.asp.net/t/1752236.aspx/1?ASP+NET+MVC+Multiple+Forms+Validation+Summaries
        public static void RenderValidationFlashMessage(this HtmlHelper html, string message)
        {
            if(html.ViewData.ModelState.IsValid
        }*/
	}


}
