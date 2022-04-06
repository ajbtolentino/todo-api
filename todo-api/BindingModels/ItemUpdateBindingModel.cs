using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreMastersTodoList.Api.BindingModels
{
	public class UpdateTodoModel
	{
        [Required]
		[StringLength(128, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime TargetDate { get; set; }
    }
}

