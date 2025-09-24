﻿using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Extensions
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if (product == null) return null;
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summary,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Brand = product.Brand,
                Type = product.Type,
                CreateDate = product.CreateDate,
            };
        }

        public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        {
            return products.Select(p=>p.ToResponse());
        }

        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
        {
            return new Pagination<ProductResponse>(
                pagination.PageIndex,
                pagination.PageSize,
                pagination.Count,
                pagination.Data.Select(p => p.ToResponse()).ToList()
            );
        }

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Brand = brand,
                Type = type,
                CreateDate = DateTimeOffset.UtcNow
            };
        }

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Id = existing.Id,
                Name = command.Name,
                Summary = command.Summary,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Brand = brand,
                Type = type,
                CreateDate = DateTimeOffset.UtcNow,
            };
        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if(product == null) return null;
            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Summary,
                product.Description,
                product.ImageFile,
                product.Price,
                new BrandDto(product.Brand.Id, product.Brand.Name),
                new TypeDto(product.Type.Id, product.Type.Name),
                DateTimeOffset.UtcNow
            );
        }

        [HttpPut("{id}")]
        public static UpdateProductCommand ToCommand(this UpdateProductDto dto, string id)
        {
            
            return new UpdateProductCommand
            {
                Id = id,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                BrandId = dto.BrandId,
                TypeId = dto.TypeId
            };
        }
    }
}
