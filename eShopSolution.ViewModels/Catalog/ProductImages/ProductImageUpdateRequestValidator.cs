using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.ProductImages
{
    public class ProductImageUpdateRequestValidator : AbstractValidator<ProductImageUpdateRequest>
    {
        public ProductImageUpdateRequestValidator()
        {
            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("Image file is required.")
                .Must(file => file.Length <= 5 * 1024 * 1024)
                    .WithMessage("Image file size must not exceed 5MB.")
                .Must(file => new List<string> { ".jpg", ".jpeg", ".png" }
                    .Contains(Path.GetExtension(file.FileName).ToLower()))
                    .WithMessage("Only .jpg, .jpeg, or .png files are allowed.");
        }
    }
}