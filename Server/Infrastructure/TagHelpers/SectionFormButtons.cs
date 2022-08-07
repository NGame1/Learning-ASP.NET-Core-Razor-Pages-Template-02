﻿namespace Infrastructure.TagHelpers
{
	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement
		(tag: "section-form-buttons",
		ParentTag = "section-form",
		TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.NormalOrSelfClosing)]
	public class SectionFormButtons : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
	{
		public SectionFormButtons() : base()
		{
		}

		public async override System.Threading.Tasks.Task ProcessAsync
			(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
			Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
		{
			// **************************************************
			var originalContents =
				await
				output.GetChildContentAsync();
			// **************************************************

			// **************************************************
			var body =
				new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("div");

			body.AddCssClass(value: "mb-3");

			body.InnerHtml.AppendHtml(content: originalContents);
			// **************************************************

			// **************************************************
			output.TagName = null;

			output.TagMode =
				Microsoft.AspNetCore.Razor
				.TagHelpers.TagMode.StartTagAndEndTag;

			output.Content.SetHtmlContent(htmlContent: body);
			// **************************************************
		}
	}
}