using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.ProductImages
{
    public class ProductImageCreateRequestValidator : AbstractValidator<ProductImageCreateRequest>
    {
        public ProductImageCreateRequestValidator()
        {
            RuleFor(x => x.Caption)
                .MaximumLength(200).WithMessage("The caption must not exceed 200 characters."); // exceed equals over

            RuleFor(x => x.IsDefault).NotNull().WithMessage("IsDefault must be specified (true or false).");

            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("Image file is required.")
                .Must(file => file.Length <= 5 * 1024 * 1024)
                    .WithMessage("Image file size must not exceed 5MB.")
                .Must(file => new List<string> { ".jpg", ".jpeg", ".png" }
                    .Contains(Path.GetExtension(file.FileName).ToLower()))
                    .WithMessage("Only .jpg, .jpeg, or .png files are allowed.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be non-negative.");
        }
    }
}